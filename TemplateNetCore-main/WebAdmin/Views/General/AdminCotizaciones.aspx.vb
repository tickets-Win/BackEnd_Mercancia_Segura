Public Class AdminCotizaciones
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            pnlMercancia.Visible = False
        End If
    End Sub

    Protected Sub btnAgregarCotizacion_Click(sender As Object, e As EventArgs)
        pnlFormularioCotizaciones.Visible = True
        pnlEncabezado.Visible = False
        PnlTabla.Visible = False
        lblMensaje.Text = "Nuevo Registro"
        pnlMercancia.Visible = True
    End Sub


End Class