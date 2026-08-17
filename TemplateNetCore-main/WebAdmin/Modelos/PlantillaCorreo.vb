Namespace MercanciaSegura.DOM.Modelos

    ''' <summary>
    ''' Plantilla de correo. Todavía no existe endpoint para ellas, así que el
    ''' catálogo vive en PlantillasCorreo; cuando lo haya, solo cambia el origen.
    ''' </summary>
    Public Class PlantillaCorreo
        Public Property PlantillaCorreoId As Integer
        Public Property Categoria As String
        Public Property Remitente As String
        Public Property Nombre As String
        Public Property Asunto As String
        Public Property Cuerpo As String
        Public Property Adjuntos As Integer

        ''' <summary>
        ''' Recorte del cuerpo para la tarjeta del listado de plantillas.
        ''' </summary>
        Public ReadOnly Property Vista As String
            Get
                If String.IsNullOrEmpty(Cuerpo) Then Return String.Empty

                Dim plano As String = Cuerpo.Replace(Environment.NewLine, " ").Trim()

                If plano.Length <= 90 Then Return plano

                Return plano.Substring(0, 90) & ".."
            End Get
        End Property
    End Class

End Namespace
