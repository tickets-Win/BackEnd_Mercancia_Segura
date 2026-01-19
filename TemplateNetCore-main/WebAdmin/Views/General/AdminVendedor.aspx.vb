Public Class AdminVendedor
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            pnlRazonSocial.Visible = False
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

        ElseIf ddlTipoPersona.SelectedValue = "M" Then
            pnlRazonSocial.Visible = True
            pnlDatosFisica.Visible = False
        End If
    End Sub
End Class