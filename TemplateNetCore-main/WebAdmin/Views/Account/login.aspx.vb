Imports Modelos
Imports Modelos.Modelos.Enumeraciones

Public Class login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnLogin_Click(sender As Object, e As EventArgs)
        Dim api As New ConsumoApi()

        Dim usuario As String = txtEmail.Text.Trim()
        Dim password As String = txtPassword.Text.Trim()

        Dim resultado = api.PostLogin(usuario, password)

        If resultado.ErrorID = TipoErroresAPI.Exito Then
            Response.Write("LOGIN OK<br/>")
            Response.Write(resultado.JSON)
        Else
            Response.Write("ERROR<br/>")
            Response.Write(resultado.JSON)
        End If

    End Sub
End Class