Public Class AdminGestionPolizas
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            pnlFormularioPolizas.Visible = False
            pnlNombreInternoPoliza.Visible = False
            pnlContenedor.Visible = False
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

        ElseIf ddlProducto.SelectedValue = "Contenedor" Then
            pnlNombreInternoPoliza.Visible = True
            pnlContenedor.Visible = True
            pnlMercancia.Visible = False
        End If
    End Sub
End Class