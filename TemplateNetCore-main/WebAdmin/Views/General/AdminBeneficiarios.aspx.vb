Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports WebAdmin.MercanciaSegura.DOM.Modelos

Public Class AdminBeneficiarios
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            DropdownHelpers.CargarTipoPersona(ddlTipoPersona)
            DropdownHelpers.CargarRFCGenerico(ddlRFCGenerico)
            CargarBeneficiarios()
            pnlDatosFisica.Visible = True
        End If
    End Sub

    Protected Sub btnAgregarBeneficiarios_Click(sender As Object, e As EventArgs)
        pnlFormularioBeneficiario.Visible = True
        PnlEncabezado.Visible = False
        PnlTabla.Visible = False
    End Sub

    Protected Sub txtBuscarBeneficiarios_TextChanged(sender As Object, e As EventArgs)
        ' Una búsqueda nueva siempre arranca en la primera página.
        gvBeneficiariosPreferentes.PageIndex = 0

        CargarBeneficiarios()
    End Sub

    Protected Sub gvBeneficiariosPreferentes_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType <> DataControlRowType.DataRow Then Exit Sub

        RegistrarPostbackCompleto(e.Row)
    End Sub

    ''' <summary>
    ''' Los botones de la tabla muestran pnlFormularioBeneficiario, que vive fuera
    ''' del UpdatePanel del listado. Con un postback parcial esos cambios de
    ''' visibilidad no llegan al navegador y la pantalla queda en blanco, así que se
    ''' fuerzan a postback completo. Al estar dentro de una plantilla del GridView
    ''' no se pueden declarar como PostBackTrigger por ID.
    ''' </summary>
    Private Sub RegistrarPostbackCompleto(contenedor As Control)
        Dim sm As ScriptManager = ScriptManager.GetCurrent(Page)

        If sm Is Nothing Then Exit Sub

        For Each ctl As Control In contenedor.Controls
            If TypeOf ctl Is IButtonControl Then sm.RegisterPostBackControl(ctl)

            If ctl.HasControls() Then RegistrarPostbackCompleto(ctl)
        Next
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

        Dim listaBeneficiarios As New List(Of BeneficiarioPreferente)

        If Not String.IsNullOrWhiteSpace(cargarBeneficiarios) AndAlso
           cargarBeneficiarios <> "null" AndAlso
           Not cargarBeneficiarios.StartsWith("ERROR") Then

            listaBeneficiarios = JsonConvert.DeserializeObject(Of List(Of BeneficiarioPreferente))(cargarBeneficiarios)
            If listaBeneficiarios Is Nothing Then listaBeneficiarios = New List(Of BeneficiarioPreferente)
        End If

        ' El filtro vive aquí y no en el TextChanged, para que la paginación no
        ' pierda la búsqueda al cambiar de página.
        Dim busqueda As String = txtBuscarBeneficiarios.Text.Trim()

        If busqueda.Length > 0 Then
            listaBeneficiarios = listaBeneficiarios.
                Where(Function(b) Contiene(b.NombreCompleto, busqueda) OrElse
                                  Contiene(b.Rfc, busqueda)).
                ToList()
        End If

        Dim ultimaPagina As Integer = 0

        If listaBeneficiarios.Count > 0 Then
            ultimaPagina = CInt(Math.Ceiling(listaBeneficiarios.Count / CDbl(gvBeneficiariosPreferentes.PageSize))) - 1
        End If

        If gvBeneficiariosPreferentes.PageIndex > ultimaPagina Then
            gvBeneficiariosPreferentes.PageIndex = ultimaPagina
        End If

        gvBeneficiariosPreferentes.DataSource = listaBeneficiarios
        gvBeneficiariosPreferentes.DataBind()
    End Sub

    ''' <summary>
    ''' Búsqueda parcial que ignora mayúsculas y acentos, y tolera nulos.
    ''' </summary>
    Private Function Contiene(valor As String, busqueda As String) As Boolean
        If String.IsNullOrEmpty(valor) Then Return False

        Return Globalization.CultureInfo.InvariantCulture.CompareInfo.IndexOf(
            valor, busqueda,
            Globalization.CompareOptions.IgnoreCase Or Globalization.CompareOptions.IgnoreNonSpace) >= 0
    End Function

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
        MostrarTipoPersona(beneficiario.TipoPersonaId)
        txtClave.Text = beneficiario.Clave
        txtNacionalidad.Text = beneficiario.Nacionalidad
        txtApellidoP.Text = beneficiario.ApellidoPaterno
        txtApellidoM.Text = beneficiario.ApellidoMaterno
        txtNombre.Text = beneficiario.Nombre
        txtRazonSocial.Text = If(beneficiario.TipoPersonaId = 2, beneficiario.NombreCompleto, "")
        txtNombreCompleto.Text = If(beneficiario.TipoPersonaId = 1, beneficiario.NombreCompleto, "")
        txtRFC.Text = beneficiario.RFC
        If beneficiario.RfcGenericoId.HasValue Then
            ddlRFCGenerico.SelectedValue = beneficiario.RfcGenericoId.Value.ToString()
        Else
            ddlRFCGenerico.SelectedIndex = 0
        End If
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

    Private Sub MostrarTipoPersona(tipoPersonaId As Integer)

        If tipoPersonaId = 1 Then
            pnlNombreCompleto.Visible = True
            pnlRazonSocial.Visible = False
            pnlDatosFisica.Visible = True
        Else
            pnlNombreCompleto.Visible = False
            pnlRazonSocial.Visible = True
            pnlDatosFisica.Visible = False
        End If

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