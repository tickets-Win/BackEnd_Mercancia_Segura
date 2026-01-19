Public Class AdminSiniestros
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnAgregarSiniestro_Click(sender As Object, e As EventArgs)
        pnlEncabezado.Visible = False
        PnlTabla.Visible = False
        pnlFormularioSiniestros.Visible = True
        lblMensaje.Text = "Nuevo Registro"
    End Sub
End Class