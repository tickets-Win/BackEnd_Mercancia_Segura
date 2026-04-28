Imports Newtonsoft.Json
Imports WebAdmin.MercanciaSegura.DOM.Modelos
Imports System.Web.UI.HtmlControls

Public Class AdminCotizaciones
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            pnlMercancia.Visible = False
            CargarCotizaciones()
            cargarClientes()
            CargarPolizas(ddlTipoCotizacion.SelectedValue)
            CargarBeneficiarios()
            CargarTipoMoneda(ddlMoneda)
        End If
    End Sub

    Protected Sub btnAgregarCotizacion_Click(sender As Object, e As EventArgs)
        pnlFormularioCotizaciones.Visible = True
        pnlEncabezado.Visible = False
        PnlTabla.Visible = False
        lblMensaje.Text = "Nuevo Registro"
        pnlMercancia.Visible = True
    End Sub
    Protected Sub ddlTipoCotizacion_SelectedIndexChanged(sender As Object, e As EventArgs)
        If ddlTipoCotizacion.SelectedValue = "Mercancia" Then
            pnlMercanciaFormulario.Visible = True
            pnlCuotaAplicableMercancia.Visible = True
            pnlBienesAsegurados.Visible = False
            pnlCuotaAplicableContenedor.Visible = False
            pnlMedidasSeguridad.Visible = True
            pnlSumaAsegurada.Visible = True

            CargarPolizas("Mercancia")

        ElseIf ddlTipoCotizacion.SelectedValue = "Contenedor" Then
            pnlMercanciaFormulario.Visible = False
            pnlCuotaAplicableMercancia.Visible = False
            pnlBienesAsegurados.Visible = True
            pnlCuotaAplicableContenedor.Visible = True
            pnlMedidasSeguridad.Visible = False
            pnlSumaAsegurada.Visible = False

            CargarPolizas("Contenedor")

        End If
    End Sub

    Protected Sub gvCotizaciones_RowCommand(sender As Object, e As GridViewCommandEventArgs)

    End Sub

    Protected Sub gvCotizaciones_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)

    End Sub

    Protected Sub CargarCotizaciones()
        Dim api As New ConsumoApi
        Dim cargarCotizaciones As String = api.GetCargarCotizaciones()

        Dim listaCotizaciones As List(Of Cotizacion) = JsonConvert.DeserializeObject(Of List(Of Cotizacion))(cargarCotizaciones)

        gvCotizaciones.DataSource = listaCotizaciones
        gvCotizaciones.DataBind()
    End Sub

    Protected Sub cargarClientes()
        Dim api As New ConsumoApi()
        Dim cargarClientes As String = api.GetCargarClientes()

        Dim listaClientes As List(Of Cliente) = JsonConvert.DeserializeObject(Of List(Of Cliente))(cargarClientes)

        ddlCliente.Items.Clear()

        ddlCliente.DataSource = listaClientes
        ddlCliente.DataTextField = "NombreCompleto"
        ddlCliente.DataValueField = "ClienteId"
        ddlCliente.DataBind()

        ddlCliente.Items.Insert(0, New ListItem("-- Selecciona --", "0"))

    End Sub

    Protected Sub CargarPolizas(tipo As String)
        Dim api As New ConsumoApi()
        Dim json As String = api.GetCargarPolizas()

        Dim listaPolizas As List(Of Poliza) =
            JsonConvert.DeserializeObject(Of List(Of Poliza))(json)

        ddlNombreInternoPoliza.Items.Clear()

        If tipo = "Mercancia" Then
            Dim listaMercancia = listaPolizas.
                Where(Function(p) p.PolizaMercancia IsNot Nothing AndAlso p.PolizaMercancia.Count > 0).
                SelectMany(Function(p) p.PolizaMercancia).
                Select(Function(m) New With {.Id = m.PolizaMercanciaId, .Nombre = m.NombreInternoPoliza}).
                ToList()

            ddlNombreInternoPoliza.DataSource = listaMercancia
            ddlNombreInternoPoliza.DataTextField = "Nombre"
            ddlNombreInternoPoliza.DataValueField = "Id"
            ddlNombreInternoPoliza.DataBind()

        ElseIf tipo = "Contenedor" Then
            Dim listaContenedor = listaPolizas.
                Where(Function(p) p.PolizaContenedor IsNot Nothing).
                Select(Function(p) New With {.Id = p.PolizaContenedor.PolizaContenedorId, .Nombre = p.PolizaContenedor.NombreInternoPoliza}).
                ToList()

            ddlNombreInternoPoliza.DataSource = listaContenedor
            ddlNombreInternoPoliza.DataTextField = "Nombre"
            ddlNombreInternoPoliza.DataValueField = "Id"
            ddlNombreInternoPoliza.DataBind()
        End If

        ddlNombreInternoPoliza.Items.Insert(0, New ListItem("-- Selecciona --", "0"))
    End Sub

    Protected Sub CargarBeneficiarios()
        Dim api As New ConsumoApi()
        Dim json As String = api.GetCargarBeneficiarios()
        Dim listaBeneficiarios As List(Of BeneficiarioPreferente) = JsonConvert.DeserializeObject(Of List(Of BeneficiarioPreferente))(json)

        ddlBeneficiarioPreferente.DataSource = listaBeneficiarios
        ddlBeneficiarioPreferente.DataTextField = "NombreCompleto"
        ddlBeneficiarioPreferente.DataValueField = "BeneficiarioPreferenteId"
        ddlBeneficiarioPreferente.DataBind()

        ddlBeneficiarioPreferente.Items.Insert(0, New ListItem("-- Selecciona --", "0"))
    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs)
        Dim api As New ConsumoApi

        Dim cotizaciones As New Cotizacion With {
            .FechaCotizacion = Date.Now,
            .VigenciaDel = If(String.IsNullOrEmpty(txtVigenciaDel.Text), CType(Nothing, DateTime?), Convert.ToDateTime(txtVigenciaDel.Text)),
            .VigenciaHasta = If(String.IsNullOrEmpty(txtVigenciaHasta.Text), CType(Nothing, DateTime?), Convert.ToDateTime(txtVigenciaHasta.Text)).Value,
            .SumaAsegurada = If(String.IsNullOrWhiteSpace(txtSumaAsegurada.Text), Nothing, Convert.ToDecimal(txtSumaAsegurada.Text))
            }
    End Sub
End Class