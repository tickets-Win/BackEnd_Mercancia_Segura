Public Class AdminEndosos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            pnlFormularioEndosos.Visible = False
        End If
    End Sub

    Protected Sub btnAgregarEndoso_Click(sender As Object, e As EventArgs)
        pnlFormularioEndosos.Visible = True
        pnlEncabezado.Visible = False
        PnlTabla.Visible = False
    End Sub

End Class