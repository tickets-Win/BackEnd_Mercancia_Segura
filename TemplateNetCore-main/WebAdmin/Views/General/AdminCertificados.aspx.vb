Public Class AdminCertificados
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            pnlMercancia.Visible = False
        End If
    End Sub

    Protected Sub btnAgregarCertificado_Click(sender As Object, e As EventArgs)
        pnlFormularioCotizaciones.Visible = True
        pnlEncabezado.Visible = False
        PnlTabla.Visible = False
        lblMensaje.Text = "Nuevo Registro"
        pnlMercancia.Visible = True
    End Sub

    Protected Sub ddlTipoCotizacion_SelectedIndexChanged(sender As Object, e As EventArgs)
        If ddlTipoCotizacion.SelectedValue = "Mercancia" Then
            pnlContenedor.Visible = False
            pnlMercanciaFormulario.Visible = True
            pnlCuotaAplicableMercancia.Visible = True
            pnlBienesAsegurados.Visible = False
            pnlCuotaAplicableContenedor.Visible = False

        ElseIf ddlTipoCotizacion.SelectedValue = "Contenedor" Then
            pnlContenedor.Visible = True
            pnlMercanciaFormulario.Visible = False
            pnlCuotaAplicableMercancia.Visible = False
            pnlBienesAsegurados.Visible = True
            pnlCuotaAplicableContenedor.Visible = True

        End If
    End Sub
End Class