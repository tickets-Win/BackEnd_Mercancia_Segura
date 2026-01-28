Imports Newtonsoft.Json
Imports WebAdmin.MercanciaSegura.DOM.Modelos
Public Class AdminCliente
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            pnlTabs.Visible = False
            pnlGestionarCredito.Visible = False
            pnlEstadoCuenta.Visible = False
            cargarTipoPersona()
            cargarTipoEstatus()
            cargarTipoSeguro()
            cargarTipoCuenta()
            cargarOrigenCliente()
            cargarTipoSector()
            cargarRegimenFiscal()
            cargarRFCGenerico()
            cargarClientes()
        End If
    End Sub

    Protected Sub btnAgregarCliente_Click(sender As Object, e As EventArgs)
        pnlFormularioCliente.Visible = True
        PnlTabla.Visible = False
        PnlEncabezado.Visible = False
        lblMensaje.Text = "Nuevo Registro"
        pnlTabs.Visible = True
        pnlGestionarCredito.Visible = True
        pnlEstadoCuenta.Visible = True
    End Sub

    Protected Sub ddlTipoPersona_SelectedIndexChanged(sender As Object, e As EventArgs)
        If ddlTipoPersona.SelectedValue = "1" Then
            pnlDatosFiscales.Visible = True
            pnlRazonSocial.Visible = False
            pnlNombreCompleto.Visible = True

        ElseIf ddlTipoPersona.SelectedValue = "2" Then
            pnlDatosFiscales.Visible = False
            pnlRazonSocial.Visible = True
            pnlNombreCompleto.Visible = False
        End If
    End Sub

    Private Sub cargarTipoPersona()
        Dim api As New ConsumoApi()
        Dim tipoPersona As String = api.GetTipoPersona()

        Dim listaTipoPersona As List(Of TipoPersona) = JsonConvert.DeserializeObject(Of List(Of TipoPersona))(tipoPersona)

        ddlTipoPersona.DataSource = listaTipoPersona
        ddlTipoPersona.DataTextField = "Tipo"
        ddlTipoPersona.DataValueField = "TipoPersonaId"
        ddlTipoPersona.DataBind()

    End Sub

    Private Sub cargarTipoEstatus()
        Dim api As New ConsumoApi()
        Dim tipoEstatus As String = api.GetTipoEstatus()

        Dim listaTipoEstatus As List(Of TipoEstatus) = JsonConvert.DeserializeObject(Of List(Of TipoEstatus))(tipoEstatus)

        ddlEstatus.DataSource = listaTipoEstatus
        ddlEstatus.DataTextField = "Tipo"
        ddlEstatus.DataValueField = "EstatusId"
        ddlEstatus.DataBind()
    End Sub

    Private Sub cargarTipoSeguro()
        Dim api As New ConsumoApi()
        Dim tipoSeguro As String = api.GetTipoSeguro()

        Dim listaTipoSeguro As List(Of TipoSeguro) = JsonConvert.DeserializeObject(Of List(Of TipoSeguro))(tipoSeguro)

        ddlSeguroContrata.DataSource = listaTipoSeguro
        ddlSeguroContrata.DataTextField = "Tipo"
        ddlSeguroContrata.DataValueField = "TipoSeguroId"
        ddlSeguroContrata.DataBind()
    End Sub

    Private Sub cargarTipoCuenta()
        Dim api As New ConsumoApi()
        Dim tipoCuenta As String = api.GetTipoCuenta()

        Dim listaTipoCuenta As List(Of TipoCuenta) = JsonConvert.DeserializeObject(Of List(Of TipoCuenta))(tipoCuenta)

        ddlTipoCuenta.DataSource = listaTipoCuenta
        ddlTipoCuenta.DataTextField = "Tipo"
        ddlTipoCuenta.DataValueField = "TipoCuentaId"
        ddlTipoCuenta.DataBind()
    End Sub
    Private Sub cargarOrigenCliente()
        Dim api As New ConsumoApi()
        Dim origenCliente As String = api.GetOrigenCliente()

        Dim listaorigenCliente As List(Of OrigenCliente) = JsonConvert.DeserializeObject(Of List(Of OrigenCliente))(origenCliente)

        ddlOrigenCliente.DataSource = listaorigenCliente
        ddlOrigenCliente.DataTextField = "Tipo"
        ddlOrigenCliente.DataValueField = "OrigenClienteId"
        ddlOrigenCliente.DataBind()
    End Sub
    Private Sub cargarTipoSector()
        Dim api As New ConsumoApi()
        Dim tipoSector As String = api.GetTipoSector()

        Dim listaTipoSector As List(Of TipoSector) = JsonConvert.DeserializeObject(Of List(Of TipoSector))(tipoSector)

        ddlSector.DataSource = listaTipoSector
        ddlSector.DataTextField = "Tipo"
        ddlSector.DataValueField = "TipoSectorId"
        ddlSector.DataBind()
    End Sub
    Private Sub cargarRegimenFiscal()
        Dim api As New ConsumoApi()
        Dim regimenFiscal As String = api.GetRegimenFiscal()

        Dim listaRegimenFiscal As List(Of RegimenFiscal) = JsonConvert.DeserializeObject(Of List(Of RegimenFiscal))(regimenFiscal)

        ddlRegimenFiscal.DataSource = listaRegimenFiscal
        ddlRegimenFiscal.DataTextField = "Descripcion"
        ddlRegimenFiscal.DataValueField = "RegimenFiscalId"
        ddlRegimenFiscal.DataBind()
    End Sub
    Private Sub cargarRFCGenerico()
        Dim api As New ConsumoApi()
        Dim rfcGenerico As String = api.GetRFCGenerico()

        Dim listaRFCGenerico As List(Of rfcGenerico) = JsonConvert.DeserializeObject(Of List(Of rfcGenerico))(rfcGenerico)

        ddlRFCGenerico.DataSource = listaRFCGenerico
        ddlRFCGenerico.DataTextField = "Tipo"
        ddlRFCGenerico.DataValueField = "RfcGenericoId"
        ddlRFCGenerico.DataBind()
    End Sub

    Protected Sub cargarClientes()
        Dim api As New ConsumoApi()
        Dim cargarClientes As String = api.GetCargarClientes()

        Dim listaClientes As List(Of Cliente) = JsonConvert.DeserializeObject(Of List(Of Cliente))(cargarClientes)

        gvClientes.DataSource = listaClientes
        gvClientes.DataBind()
    End Sub
    Protected Sub gvClientes_RowCommand(sender As Object, e As GridViewCommandEventArgs)

    End Sub

    Protected Sub gvClientes_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        gvClientes.PageIndex = e.NewPageIndex
        cargarClientes()
    End Sub
End Class