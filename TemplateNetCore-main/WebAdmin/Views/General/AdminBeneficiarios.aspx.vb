Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports WebAdmin.MercanciaSegura.DOM.Modelos

Public Class AdminBeneficiarios
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            pnlDatosFisica.Visible = True
            DropdownHelpers.CargarTipoPersona(ddlTipoPersona)
            DropdownHelpers.CargarRFCGenerico(ddlRFCGenerico)
            CargarBeneficiarios()
        End If
    End Sub

    Protected Sub btnAgregarBeneficiarios_Click(sender As Object, e As EventArgs)
        pnlFormularioBeneficiario.Visible = True
        PnlEncabezado.Visible = False
        PnlTabla.Visible = False
    End Sub

    Protected Sub txtBuscarBeneficiarios_TextChanged(sender As Object, e As EventArgs)
        Dim api As New ConsumoApi()
        Dim json As String = api.GetCargarBeneficiarios()

        Dim lista As List(Of BeneficiarioPreferente) =
        JsonConvert.DeserializeObject(Of List(Of BeneficiarioPreferente))(json)

        Dim texto As String = txtBuscarBeneficiarios.Text.Trim().ToLower()

        Dim filtrados = lista.Where(Function(v) _
            v.NombreCompleto.ToLower().Contains(texto) OrElse
            v.Rfc.ToLower().Contains(texto)).ToList()

        gvBeneficiariosPreferentes.DataSource = filtrados
        gvBeneficiariosPreferentes.DataBind()
    End Sub

    Protected Sub ddlTipoPersona_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim tipoPersonaId As Integer = Convert.ToInt32(ddlTipoPersona.SelectedValue)
        If ddlTipoPersona.SelectedValue = "1" Then
            pnlNombreCompleto.Visible = True
            pnlRazonSocial.Visible = False
            pnlDatosFisica.Visible = True

        ElseIf ddlTipoPersona.SelectedValue = "2" Then
            pnlNombreCompleto.Visible = False
            pnlRazonSocial.Visible = True
            pnlDatosFisica.Visible = False
        End If
    End Sub

    Protected Sub gvBeneficiariosPreferentes_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        If e.CommandName = "Editar" Then
            Dim beneficiarioId As Integer = Convert.ToInt32(e.CommandArgument)
            pnlFormularioBeneficiario.Visible = True
            PnlTabla.Visible = False
            PnlEncabezado.Visible = False
            lblMensaje.Text = "Editar beneficiario"

            EditarBeneficiario(beneficiarioId)
        End If

        If e.CommandName = "Eliminar" Then
            Dim api As New ConsumoApi()
            Dim beneficiarioId As Integer = Convert.ToInt32(e.CommandArgument)

            Dim eliminado As String = api.DeleteBeneficiario(beneficiarioId)

            CargarBeneficiarios()

            If Not String.IsNullOrEmpty(eliminado) Then
                Dim respuestaObj As JObject = JObject.Parse(eliminado)
                Dim mensaje As String

                If respuestaObj("message") IsNot Nothing Then
                    mensaje = respuestaObj("message").ToString()
                Else
                    mensaje = "Beneficiario eliminado correctamente"
                End If

                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "toast",
        "showToast('" & mensaje & "', 'success');", True)
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "toast",
                "showToast('Error al eliminar el beneficiario', 'danger');", True)
            End If
        End If
    End Sub

    Protected Sub gvBeneficiariosPreferentes_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        gvBeneficiariosPreferentes.PageIndex = e.NewPageIndex
        CargarBeneficiarios()
    End Sub

    Protected Sub CargarBeneficiarios()
        Dim api As New ConsumoApi
        Dim cargarBeneficiarios As String = api.GetCargarBeneficiarios()

        Dim listaBeneficiarios As List(Of BeneficiarioPreferente) = JsonConvert.DeserializeObject(Of List(Of BeneficiarioPreferente))(cargarBeneficiarios)

        gvBeneficiariosPreferentes.DataSource = listaBeneficiarios
        gvBeneficiariosPreferentes.DataBind()
    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs)
        Dim api As New ConsumoApi()

        Dim tipoPersonaId As Integer = Convert.ToInt32(ddlTipoPersona.SelectedValue)

        Dim nombreCompleto As String
        If tipoPersonaId = 1 Then
            nombreCompleto = txtNombre.Text.Trim() & " " & txtApellidoP.Text.Trim() & " " & txtApellidoM.Text.Trim()
        Else
            nombreCompleto = txtRazonSocial.Text.Trim()
        End If

        Dim rfcGenericoId As Integer? = Nothing

        If Not String.IsNullOrWhiteSpace(ddlRFCGenerico.SelectedValue) AndAlso ddlRFCGenerico.SelectedValue <> "0" Then
            rfcGenericoId = Convert.ToInt32(ddlRFCGenerico.SelectedValue)
        End If

        Dim beneficiarios As New BeneficiarioPreferente With {
        .TipoPersonaId = tipoPersonaId,
        .Clave = txtClave.Text,
        .Nacionalidad = txtNacionalidad.Text,
        .ApellidoPaterno = txtApellidoP.Text,
        .ApellidoMaterno = txtApellidoM.Text,
        .Nombre = txtNombre.Text,
        .NombreCompleto = nombreCompleto,
        .RFC = txtRFC.Text,
        .RfcGenericoId = rfcGenericoId,
        .Pais = ddlPais.SelectedValue,
        .Estado = txtEstado.Text,
        .Municipio = txtMunicipio.Text,
        .Calle = txtCalle.Text,
        .NumeroInt = txtNumeroInt.Text,
        .NumeroExt = txtNumeroExt.Text,
        .Colonia = txtColonia.Text,
        .Cp = txtCP.Text,
        .Poblacion = txtPoblacion.Text
        }

        Dim json As String

        json = JsonConvert.SerializeObject(beneficiarios, Formatting.Indented)
        System.Diagnostics.Debug.WriteLine("JSON enviado a API:" & json)

        Dim respuesta As String = ""
        Dim mensajeToast As String = ""

        If Not String.IsNullOrEmpty(hfBeneficiarioId.Value) Then
            Dim beneficiarioId As Integer = Convert.ToInt32(hfBeneficiarioId.Value)
            respuesta = api.PutEditarBeneficiario(beneficiarioId, json)
            mensajeToast = "Beneficiario editado correctamente"
        Else
            respuesta = api.PostBeneficiario(json)
            mensajeToast = "Beneficiario agregado correctamente"
        End If

        System.Diagnostics.Debug.WriteLine("Respuesta API:" & respuesta)

        Dim respuestaObj As JObject = JObject.Parse(respuesta)

        If respuestaObj("errors") IsNot Nothing Then
            Dim erroresJson As String = JsonConvert.SerializeObject(respuestaObj("errors"))
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "apiErrors",
            "showToast('No se pudo guardar el beneficiario', 'danger'); mostrarErroresApi(" & erroresJson & ");", True)
            Exit Sub
        End If

        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "toast",
        "showToast('" & mensajeToast & "', 'success');", True)

        CargarBeneficiarios()
        pnlFormularioBeneficiario.Visible = False
        PnlTabla.Visible = True
        PnlEncabezado.Visible = True
        LimpiarFormulario()
    End Sub

    Protected Sub EditarBeneficiario(beneficiarioId As Integer)
        Dim api As New ConsumoApi()
        Dim objBeneficiario As String = api.GetBeneficiarioId(beneficiarioId)

        Dim beneficiario As BeneficiarioPreferente = JsonConvert.DeserializeObject(Of BeneficiarioPreferente)(objBeneficiario)

        hfBeneficiarioId.Value = beneficiario.BeneficiarioPreferenteId.ToString()

        ddlTipoPersona.SelectedValue = beneficiario.TipoPersonaId.ToString()
        txtClave.Text = beneficiario.Clave
        txtNacionalidad.Text = beneficiario.Nacionalidad
        txtApellidoP.Text = beneficiario.ApellidoPaterno
        txtApellidoM.Text = beneficiario.ApellidoMaterno
        txtNombre.Text = beneficiario.Nombre
        txtRazonSocial.Text = If(beneficiario.TipoPersonaId = 2, beneficiario.NombreCompleto, "")
        txtNombreCompleto.Text = If(beneficiario.TipoPersonaId = 1, beneficiario.NombreCompleto, "")
        txtRFC.Text = beneficiario.RFC
        ddlRFCGenerico.SelectedValue = beneficiario.RfcGenericoId
        ddlPais.SelectedValue = beneficiario.Pais
        txtEstado.Text = beneficiario.Estado
        txtMunicipio.Text = beneficiario.Municipio
        txtCalle.Text = beneficiario.Calle
        txtNumeroInt.Text = beneficiario.NumeroInt
        txtNumeroExt.Text = beneficiario.NumeroExt
        txtColonia.Text = beneficiario.Colonia
        txtCP.Text = beneficiario.Cp
        txtPoblacion.Text = beneficiario.Poblacion

        pnlFormularioBeneficiario.Visible = True
        PnlTabla.Visible = False
        PnlEncabezado.Visible = False
    End Sub

    Private Sub LimpiarFormulario()

        hfBeneficiarioId.Value = ""

        ddlTipoPersona.SelectedIndex = 0
        ddlRFCGenerico.SelectedIndex = 0
        ddlPais.SelectedIndex = 0

        txtNombre.Text = ""
        txtApellidoP.Text = ""
        txtApellidoM.Text = ""
        txtRazonSocial.Text = ""
        txtRFC.Text = ""
        txtClave.Text = ""
        txtNacionalidad.Text = ""

        txtCalle.Text = ""
        txtNumeroInt.Text = ""
        txtNumeroExt.Text = ""
        txtColonia.Text = ""
        txtCP.Text = ""
        txtPoblacion.Text = ""

    End Sub

    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs)
        CargarBeneficiarios()
        pnlFormularioBeneficiario.Visible = False
        PnlTabla.Visible = True
        PnlEncabezado.Visible = True
        LimpiarFormulario()
    End Sub
End Class