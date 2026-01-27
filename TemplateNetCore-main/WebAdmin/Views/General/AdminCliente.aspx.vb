Public Class AdminCliente
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            pnlTabs.Visible = False
            pnlGestionarCredito.Visible = False
            pnlEstadoCuenta.Visible = False
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
        If ddlTipoPersona.SelectedValue = "F" Then
            pnlDatosFiscales.Visible = True
            pnlRazonSocial.Visible = False
            pnlNombreCompleto.Visible = True

        ElseIf ddlTipoPersona.SelectedValue = "M" Then
            pnlDatosFiscales.Visible = False
            pnlRazonSocial.Visible = True
            pnlNombreCompleto.Visible = False
        End If
    End Sub
End Class