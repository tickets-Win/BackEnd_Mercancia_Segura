Namespace MercanciaSegura.DOM.Modelos

    ''' <summary>
    ''' Plantilla de correo tal como la devuelve el API.
    ''' </summary>
    Public Class PlantillaCorreo
        Public Property PlantillaCorreoId As Integer
        Public Property CategoriaPlantillaId As Integer
        Public Property Nombre As String
        Public Property Asunto As String
        Public Property CuerpoHtml As String
        Public Property Activa As Boolean
        Public Property FechaRegistro As DateTime
        Public Property FechaActualizacion As DateTime

        ' Dato que agrega el API.
        Public Property NombreCategoria As String

        ''' <summary>
        ''' Recorte del cuerpo, sin etiquetas, para la tarjeta del listado.
        ''' </summary>
        Public ReadOnly Property Vista As String
            Get
                If String.IsNullOrEmpty(CuerpoHtml) Then Return String.Empty

                Dim plano As String =
                    Text.RegularExpressions.Regex.Replace(CuerpoHtml, "<[^>]+>", " ")

                plano = Net.WebUtility.HtmlDecode(plano)
                plano = Text.RegularExpressions.Regex.Replace(plano, "\s+", " ").Trim()

                If plano.Length <= 90 Then Return plano

                Return plano.Substring(0, 90) & ".."
            End Get
        End Property

        ''' <summary>El remitente se muestra fijo en la tarjeta.</summary>
        Public ReadOnly Property Remitente As String
            Get
                Return "Mercancia Segura"
            End Get
        End Property

        ''' <summary>Todavía no hay adjuntos por plantilla.</summary>
        Public ReadOnly Property Adjuntos As Integer
            Get
                Return 0
            End Get
        End Property
    End Class

    Public Class CategoriaPlantilla
        Public Property CategoriaPlantillaId As Integer
        Public Property Nombre As String
        Public Property EsSistema As Boolean
    End Class

End Namespace
