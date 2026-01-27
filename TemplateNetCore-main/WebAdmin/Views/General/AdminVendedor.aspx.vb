Imports Newtonsoft.Json
Imports WebAdmin.MercanciaSegura.DOM.Modelos
Public Class AdminVendedor
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            pnlRazonSocial.Visible = False
            cargarTipoPersona()
            cargarTipoVendedor()
            CargarVendedores()
            txtFechaRegistro.Text = DateTime.Now.ToString("yyyy-MM-dd")
        End If
    End Sub

    Protected Sub btnAgregarVendedor_Click(sender As Object, e As EventArgs)
        pnlFormularioVendedor.Visible = True
        PnlTabla.Visible = False
        PnlEncabezado.Visible = False
        lblMensaje.Text = "Nuevo Registro"
    End Sub

    Protected Sub ddlTipoPersona_SelectedIndexChanged(sender As Object, e As EventArgs)
        If ddlTipoPersona.SelectedValue = "F" Then
            pnlRazonSocial.Visible = False
            pnlDatosFisica.Visible = True
            pnlNombreCompleto.Visible = True

        ElseIf ddlTipoPersona.SelectedValue = "M" Then
            pnlRazonSocial.Visible = True
            pnlDatosFisica.Visible = False
            pnlNombreCompleto.Visible = False
        End If
    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs)

        Dim api As New ConsumoApi()

        Dim tipoVendedorId As Integer = Convert.ToInt32(ddlTipoVendedor.SelectedValue)

        Dim comisionValue As Decimal = 0
        Decimal.TryParse(txtComisión.Text.Replace("$", "").Trim(), comisionValue)

        If comisionValue > 100 Then comisionValue = 100
        If comisionValue < 0 Then comisionValue = 0

        Dim vendedor As New Vendedor With {
        .ApellidoPaterno = txtApellidoP.Text,
        .ApellidoMaterno = txtApellidoM.Text,
        .Nombres = txtNombre.Text,
        .NombreCompleto = txtNombre.Text & " " & txtApellidoP.Text & " " & txtApellidoM.Text,
        .TipoPersona = 1,
        .TipoVendedorId = tipoVendedorId,
        .Estatus = (ddlEstatus.SelectedValue = "Activo"),
        .Clave = txtClave.Text,
        .Rfc = txtRFC.Text,
        .Domicilio = txtDomicilio.Text,
        .Cp = txtCP.Text,
        .Colonia = txtColonia.Text,
        .Estado = txtEstado.Text,
        .Genero = ddlGenero.SelectedValue,
        .Telefono = txtTelefono.Text,
        .CorreoElectronico = txtCorreo.Text,
        .Observaciones = txtObservaciones.Text,
        .Comision = comisionValue,
        .FechaRegistro = Date.Now
            }

        Dim json As String = JsonConvert.SerializeObject(vendedor)
        Dim respuesta As String = api.PostVendedor(json)


        Debug.WriteLine(json)


    End Sub

    Private Sub cargarTipoPersona()
        Dim api As New ConsumoApi()
        Dim tipoPersona As String = api.GetTipoPersona()

    End Sub
    Private Sub cargarTipoVendedor()
        Dim api As New ConsumoApi()
        Dim tipoVendedor As String = api.GetTipoVendedor()

        Dim listaTipoVendedor As List(Of TipoVendedor) = JsonConvert.DeserializeObject(Of List(Of TipoVendedor))(tipoVendedor)

        ddlTipoVendedor.DataSource = listaTipoVendedor
        ddlTipoVendedor.DataTextField = "Tipo"
        ddlTipoVendedor.DataValueField = "TipoVendedorId"
        ddlTipoVendedor.DataBind()
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
End Class