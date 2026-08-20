''' <summary>
''' Editor de texto enriquecido reutilizable. Expone el contenido como HTML por
''' la propiedad Html; quien lo usa no tiene que saber nada del hidden ni del
''' base64.
''' </summary>
Public Class EditorHtml
    Inherits System.Web.UI.UserControl

    ''' <summary>
    ''' Contenido en HTML.
    '''
    ''' Viaja en base64 dentro del hidden: en claro, la validación de peticiones
    ''' de ASP.NET lo tomaría por un intento de XSS y tumbaría la página. Apagar
    ''' esa validación expondría todo el formulario, así que se prefiere
    ''' codificar solo este campo.
    ''' </summary>
    Public Property Html As String
        Get
            If String.IsNullOrWhiteSpace(hfHtml.Value) Then Return String.Empty

            Try
                Return Text.Encoding.UTF8.GetString(Convert.FromBase64String(hfHtml.Value))
            Catch
                ' Por si alguna vez llegara sin codificar.
                Return hfHtml.Value
            End Try
        End Get
        Set(value As String)
            ' Se escribe en los dos lados: el hidden es lo que viaja al postear,
            ' y el literal es lo que se ve de inmediato en el área editable sin
            ' depender de que el script alcance a inyectarlo.
            litCuerpo.Text = If(value, String.Empty)

            If String.IsNullOrEmpty(value) Then
                hfHtml.Value = String.Empty
                Exit Property
            End If

            hfHtml.Value = Convert.ToBase64String(Text.Encoding.UTF8.GetBytes(value))
        End Set
    End Property

    ''' <summary>
    ''' El mismo contenido sin etiquetas. Sirve para revisar si hay algo escrito
    ''' o para buscar campos sin resolver sin que estorbe el marcado.
    ''' </summary>
    Public ReadOnly Property Texto As String
        Get
            Dim html As String = Me.Html

            If String.IsNullOrEmpty(html) Then Return String.Empty

            Return Server.HtmlDecode(
                Text.RegularExpressions.Regex.Replace(html, "<[^>]+>", " "))
        End Get
    End Property

End Class
