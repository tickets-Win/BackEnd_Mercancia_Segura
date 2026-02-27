Public Class AdminBeneficiarios
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            pnlDatosFisica.Visible = True
            DropdownHelpers.CargarTipoPersona(ddlTipoPersona)
            DropdownHelpers.CargarRFCGenerico(ddlRFCGenerico)
        End If
    End Sub

    Protected Sub btnAgregarBeneficiarios_Click(sender As Object, e As EventArgs)
        pnlFormularioBeneficiario.Visible = True
        PnlEncabezado.Visible = False
    End Sub

    Protected Sub txtBuscarBeneficiarios_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Protected Sub ddlTipoPersona_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim tipoPersonaId As Integer = Convert.ToInt32(ddlTipoPersona.SelectedValue)
        If ddlTipoPersona.SelectedValue = "1" Then
            pnlNombreCompleto.Visible = True
            pnlRazonSocial.Visible = False
            pnlDatosFisica.Visible = True

        ElseIf ddlTipoPersona.SelectedValue = "2" Then
            pnlNombreCompleto.Visible = False
            pnlRazonSocial.Visible = True
            pnlDatosFisica.Visible = False
        End If
    End Sub
End Class