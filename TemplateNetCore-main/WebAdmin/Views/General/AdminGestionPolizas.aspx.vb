Imports Newtonsoft.Json
Imports WebAdmin.MercanciaSegura.DOM.Modelos

Public Class AdminGestionPolizas
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            pnlFormularioPolizas.Visible = False
            pnlNombreInternoPoliza.Visible = False
            pnlContenedor.Visible = False
            cargarPolizas()
        End If
    End Sub

    Protected Sub btnAgregarPoliza_Click(sender As Object, e As EventArgs)
        pnlFormularioPolizas.Visible = True
        pnlEncabezado.Visible = False
        PnlTabla.Visible = False
        lblMensaje.Text = "Nuevo Registro"
    End Sub

    Protected Sub ddlProducto_SelectedIndexChanged(sender As Object, e As EventArgs)
        If ddlProducto.SelectedValue = "Mercancia" Then
            pnlNombreInternoPoliza.Visible = False
            pnlMercancia.Visible = True
            pnlContenedor.Visible = False
            pnlMediosTransporte.Visible = False


        ElseIf ddlProducto.SelectedValue = "Contenedor" Then
            pnlNombreInternoPoliza.Visible = True
            pnlContenedor.Visible = True
            pnlMercancia.Visible = False
            pnlMediosTransporte.Visible = True

        End If
    End Sub

    Protected Sub cargarPolizas()
        Dim api As New ConsumoApi
        Dim cargarPolizas As String = api.GetCargarPolizas()

        Dim lstPolizas As List(Of Poliza) = JsonConvert.DeserializeObject(Of List(Of Poliza))(cargarPolizas)

        gvPolizas.DataSource = lstPolizas
        gvPolizas.DataBind()
    End Sub
    Protected Sub gvPolizas_RowCommand(sender As Object, e As GridViewCommandEventArgs)

    End Sub

    Protected Sub gvPolizas_PageIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs)
        Dim api As New ConsumoApi()
        Dim ProductoId As Integer = Convert.ToInt32(ddlProducto.SelectedValue)

        Dim poliza As New Poliza With {
            .ProductoId = ProductoId,
            .TipoPoliza = txtTipoPoliza.Text,
            .NumeroPoliza = txtNumeroPoliza.Text,
            .ContratanteId = ddlContratante.SelectedValue,
            .AseguradoraId = ddlAseguradora.SelectedValue,
            .SubRamoId = ddlSubRamo.SelectedValue,
            .VigenciaDel = Date.Now,
            .VigenciaHasta = Date.Now,
            .EstatusPolizaId = ddlEstatus.SelectedValue,
            .FormaPagoId = ddlFormaPago.SelectedValue,
            .MonedaId = ddlMoneda.SelectedValue
            }

    End Sub
End Class