Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports WebAdmin.MercanciaSegura.DOM.Modelos
Public Class AdminVendedor
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            pnlDatosFisica.Visible = True
            pnlRazonSocial.Visible = False
            DropdownHelpers.CargarTipoPersona(ddlTipoPersona)
            DropdownHelpers.cargarTipoVendedor(ddlTipoVendedor)
            CargarVendedores()
            txtFechaRegistro.Text = DateTime.Now.ToString("yyyy-MM-dd")
            CargarDatos()
        End If
    End Sub

    Protected Sub btnAgregarVendedor_Click(sender As Object, e As EventArgs)
        pnlFormularioVendedor.Visible = True
        PnlTabla.Visible = False
        PnlEncabezado.Visible = False
        lblMensaje.Text = "Nuevo Registro"

        ddlEstatus.SelectedValue = "1"
        ddlEstatus.Enabled = False
        ddlTipoPersona.Enabled = True
    End Sub

    Protected Sub ddlTipoPersona_SelectedIndexChanged(sender As Object, e As EventArgs)
        If ddlTipoPersona.SelectedValue = "1" Then
            pnlRazonSocial.Visible = False
            pnlDatosFisica.Visible = True
            pnlNombreCompleto.Visible = True
            pnlGenero.Visible = True

        ElseIf ddlTipoPersona.SelectedValue = "2" Then
            pnlRazonSocial.Visible = True
            pnlDatosFisica.Visible = False
            pnlNombreCompleto.Visible = False
            pnlGenero.Visible = False
        End If
    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs)

        Dim api As New ConsumoApi()

        Dim tipoPersonaId As Integer = Convert.ToInt32(ddlTipoPersona.SelectedValue)
        Dim tipoVendedorId As Integer = Convert.ToInt32(ddlTipoVendedor.SelectedValue)

        Dim comisionValue As Decimal = 0
        Decimal.TryParse(txtComision.Text.Replace("$", "").Trim(), comisionValue)

        If comisionValue > 100 Then comisionValue = 100
        If comisionValue < 0 Then comisionValue = 0

        Dim nombreCompleto As String
        If tipoPersonaId = 1 Then
            nombreCompleto = txtNombre.Text.Trim() & " " & txtApellidoP.Text.Trim() & " " & txtApellidoM.Text.Trim()
        Else
            nombreCompleto = txtRazonSocial.Text.Trim()
        End If

        Dim correo As String = txtCorreo.Text.Trim()

        If correo = "" Then
            correo = Nothing
        End If

        Dim vendedor As New Vendedor With {
        .ApellidoPaterno = txtApellidoP.Text,
        .ApellidoMaterno = txtApellidoM.Text,
        .Nombres = txtNombre.Text,
        .NombreCompleto = nombreCompleto,
        .TipoPersonaId = tipoPersonaId,
        .TipoVendedorId = tipoVendedorId,
        .Estatus = (ddlEstatus.SelectedValue = "1"),
        .Clave = txtClave.Text,
        .Rfc = txtRFC.Text,
        .Domicilio = txtDomicilio.Text,
        .Cp = txtCP.Text,
        .Colonia = txtColonia.Text,
        .Estado = txtEstado.Text,
        .Genero = ddlGenero.SelectedValue,
        .Telefono = txtTelefono.Text,
        .CorreoElectronico = correo,
        .Observaciones = txtObservaciones.Text,
        .Comision = comisionValue,
        .FechaRegistro = Date.Now
            }

        Dim json As String = JsonConvert.SerializeObject(vendedor)
        Dim respuesta As String
        Dim mensajeToast As String = ""
        Dim esExito As Boolean = False

        If String.IsNullOrEmpty(hfVendedorId.Value) Then
            respuesta = api.PostVendedor(json)
        Else
            Dim vendedorId As Integer = Convert.ToInt32(hfVendedorId.Value)
            respuesta = api.PutEditarVendedores(vendedorId, json)
        End If

        Dim respuestaObj As JObject = JObject.Parse(respuesta)

        If respuestaObj("errors") IsNot Nothing Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "apiErrors",
        "showToast('No se pudo guardar el vendedor', 'danger');", True)
        Else
            mensajeToast = If(String.IsNullOrEmpty(hfVendedorId.Value),
                                    "Vendedor agregado correctamente",
                                    "Vendedor editado correctamente")
            ClientScript.RegisterStartupScript(Me.GetType(), "toast",
        "showToast('" & mensajeToast & "', 'success');", True)


            CargarVendedores()
            pnlFormularioVendedor.Visible = False
            PnlTabla.Visible = True
            PnlEncabezado.Visible = True
            LimpiarFormulario()
        End If

    End Sub

    Public Sub CargarVendedores()
        Dim api As New ConsumoApi()
        Dim cargarVendedores As String = api.GetCargarVendedores()

        Dim listavendedores As List(Of Vendedor) = JsonConvert.DeserializeObject(Of List(Of Vendedor))(cargarVendedores)

        gvVendedores.DataSource = listavendedores
        gvVendedores.DataBind()
    End Sub

    Protected Sub gvVendedores_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        gvVendedores.PageIndex = e.NewPageIndex
        CargarVendedores()
    End Sub

    Private Sub CargarDatos()
        Dim estatusBD As Boolean = True

        If estatusBD Then
            ddlEstatus.SelectedValue = "1"
        Else
            ddlEstatus.SelectedValue = "0"
        End If
    End Sub

    Private Sub LimpiarFormulario()
        txtNombre.Text = ""
        txtApellidoP.Text = ""
        txtApellidoM.Text = ""
        txtRazonSocial.Text = ""
        txtClave.Text = ""
        txtRFC.Text = ""
        txtDomicilio.Text = ""
        txtCP.Text = ""
        txtColonia.Text = ""
        txtEstado.Text = ""
        ddlGenero.SelectedIndex = 0
        txtTelefono.Text = ""
        txtCorreo.Text = ""
        txtObservaciones.Text = ""
        txtComision.Text = ""
    End Sub

    Protected Sub gvVendedores_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        If e.CommandName = "Editar" Then
            Dim vendedorId As Integer = Convert.ToInt32(e.CommandArgument)
            pnlFormularioVendedor.Visible = True
            PnlTabla.Visible = False
            PnlEncabezado.Visible = False
            lblMensaje.Text = "Editar Vendedor"

            EditarVendedor(vendedorId)
        End If

        If e.CommandName = "Eliminar" Then
            Dim api As New ConsumoApi()
            Dim vendedorId As Integer = Convert.ToInt32(e.CommandArgument)

            Dim eliminado As String = api.DeleteVendedores(vendedorId)

            CargarVendedores()

            Dim respuestaObj As JObject = JObject.Parse(eliminado)
            Dim mensaje As String = respuestaObj("message").ToString()

            If eliminado IsNot Nothing AndAlso eliminado <> "" Then
                ClientScript.RegisterStartupScript(Me.GetType(), "toast",
                    "showToast('" & mensaje & "', 'success');", True)
            Else
                ClientScript.RegisterStartupScript(Me.GetType(), "toast",
                    "showToast('Error al eliminar el vendedor', 'danger');", True)
            End If


        End If
    End Sub

    Protected Sub EditarVendedor(vendedorId As Integer)
        Dim api As New ConsumoApi()
        Dim objvendedor As String = api.GetVendedorId(vendedorId)

        Dim vendedor As Vendedor = JsonConvert.DeserializeObject(Of Vendedor)(objvendedor)

        hfVendedorId.Value = vendedor.VendedorId.ToString()

        If Not IsPostBack Then
            DropdownHelpers.CargarTipoPersona(ddlTipoPersona)
            DropdownHelpers.CargarTipoVendedor(ddlTipoVendedor)
        End If

        hfTipoPersona.Value = vendedor.TipoPersonaId.ToString()
        ddlTipoPersona.SelectedValue = vendedor.TipoPersonaId.ToString()
        ddlTipoVendedor.SelectedValue = vendedor.TipoVendedorId.ToString()
        txtNombre.Text = vendedor.Nombres
        txtApellidoP.Text = vendedor.ApellidoPaterno
        txtApellidoM.Text = vendedor.ApellidoMaterno
        txtRazonSocial.Text = If(vendedor.TipoPersonaId = 2, vendedor.NombreCompleto, "")
        txtNombreCompleto.Text = If(vendedor.TipoPersonaId = 1, vendedor.NombreCompleto, "")
        txtClave.Text = vendedor.Clave
        txtRFC.Text = vendedor.Rfc
        txtDomicilio.Text = vendedor.Domicilio
        txtCP.Text = vendedor.Cp
        txtColonia.Text = vendedor.Colonia
        txtEstado.Text = vendedor.Estado
        ddlGenero.SelectedValue = vendedor.Genero
        txtTelefono.Text = vendedor.Telefono
        txtCorreo.Text = vendedor.CorreoElectronico
        txtObservaciones.Text = vendedor.Observaciones
        txtComision.Text = vendedor.Comision.ToString()
        ddlEstatus.SelectedValue = If(vendedor.Estatus, "1", "0")
        ddlEstatus.Enabled = True
        ddlTipoPersona.Enabled = False
        If vendedor.TipoPersonaId = 1 Then
            pnlRazonSocial.Visible = False
            pnlDatosFisica.Visible = True
            pnlNombreCompleto.Visible = True
            pnlGenero.Visible = True
        Else
            pnlRazonSocial.Visible = True
            pnlDatosFisica.Visible = False
            pnlNombreCompleto.Visible = False
            pnlGenero.Visible = False
        End If
    End Sub

    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs)
        CargarVendedores()
        pnlFormularioVendedor.Visible = False
        PnlTabla.Visible = True
        PnlEncabezado.Visible = True
        LimpiarFormulario()
    End Sub

    Protected Sub txtBuscarVendedor_TextChanged(sender As Object, e As EventArgs)
        Dim api As New ConsumoApi()
        Dim json As String = api.GetCargarVendedores()

        Dim lista As List(Of Vendedor) =
        JsonConvert.DeserializeObject(Of List(Of Vendedor))(json)

        Dim texto As String = txtBuscarVendedor.Text.Trim().ToLower()

        Dim filtrados = lista.Where(Function(v) _
            v.NombreCompleto.ToLower().Contains(texto) OrElse
            v.Rfc.ToLower().Contains(texto)).ToList()

        gvVendedores.DataSource = filtrados
        gvVendedores.DataBind()
    End Sub

    Protected Sub ddlTipoEstatusCliente_SelectedIndexChanged(sender As Object, e As EventArgs)
        FiltrarVendedoresPorEstatus()
    End Sub

    Public Sub FiltrarVendedoresPorEstatus()
        Dim api As New ConsumoApi()
        Dim json As String = api.GetCargarVendedores()

        Dim lista As List(Of Vendedor) =
            JsonConvert.DeserializeObject(Of List(Of Vendedor))(json)

        Select Case ddlTipoEstatusCliente.SelectedValue
            Case "1"
                lista = lista.Where(Function(v) v.Estatus = True).ToList()

            Case "2"
                lista = lista.Where(Function(v) v.Estatus = False).ToList()

            Case Else
        End Select

        gvVendedores.DataSource = lista
        gvVendedores.DataBind()
    End Sub
End Class