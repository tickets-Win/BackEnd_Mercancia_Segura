Imports System
Imports Newtonsoft.Json
Imports WebAdmin.MercanciaSegura.DOM.Modelos

Public Class login
    Inherits System.Web.UI.Page

    Private Property Customer As Usuario
        Get
            Return Session("Usuario")
        End Get
        Set(value As Usuario)
            Session("Usuario") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnLogin_Click(sender As Object, e As EventArgs)

        If String.IsNullOrWhiteSpace(txtEmail.Text) Or String.IsNullOrWhiteSpace(txtPassword.Text) Then
            lblMensaje.Text = "❌ Los campos correo y contraseña son obligatorios."
            lblMensaje.Visible = True
            lblMensaje.CssClass = "mensaje-error visible"

            Dim script As String = "setTimeout(function() {" &
                               "var lbl = document.getElementById('" & lblMensaje.ClientID & "');" &
                               "lbl.style.display = 'none';" &
                               "}, 3000);"
            ClientScript.RegisterStartupScript(Me.GetType(), "hideMensaje", script, True)
            Return
        End If

        Dim api As New ConsumoApi()

        Dim objUsuario As New Usuario With {
            .UsuarioNombre = txtEmail.Text.Trim(),
            .Password = txtPassword.Text.Trim()
            }
        Dim resultado As String

        resultado = api.Login(objUsuario.UsuarioNombre, objUsuario.Password)

        If resultado.TrimStart().StartsWith("{") Then

            Dim data = Newtonsoft.Json.Linq.JObject.Parse(resultado)
            Dim tokenValue = data.SelectToken("token")?.ToString()

            If Not String.IsNullOrEmpty(tokenValue) Then
                Customer = objUsuario
                Session("token") = tokenValue

                lblMensaje.Visible = False
                lblMensaje.CssClass = "mensaje-error"

                Response.Redirect("../General/Inicio.aspx")
                Return
            End If
        End If
        lblMensaje.Text = "❌ Usuario o contraseña incorrectos"
        lblMensaje.Visible = True
        lblMensaje.CssClass = "mensaje-error visible"

        Dim script2 As String = "setTimeout(function() {" &
                            "var lbl = document.getElementById('" & lblMensaje.ClientID & "');" &
                            "lbl.style.display = 'none';" &
                            "}, 3000);"
        ClientScript.RegisterStartupScript(Me.GetType(), "hideMensaje2", script2, True)
    End Sub
End Class

