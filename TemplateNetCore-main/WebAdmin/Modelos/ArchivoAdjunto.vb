Namespace MercanciaSegura.DOM.Modelos

    ''' <summary>
    ''' Archivo que se va a adjuntar al correo. El contenido se guarda en memoria
    ''' porque el envío ocurre en otro postback distinto al de la carga.
    ''' </summary>
    Public Class ArchivoAdjunto
        Public Property Nombre As String
        Public Property TipoMime As String
        Public Property Contenido As Byte()

        ''' <summary>Tamaño legible, para mostrarlo en la lista.</summary>
        Public ReadOnly Property Tamanio As String
            Get
                If Contenido Is Nothing Then Return "0 KB"

                Dim kb As Double = Contenido.Length / 1024.0

                If kb < 1024 Then Return kb.ToString("N0") & " KB"

                Return (kb / 1024.0).ToString("N1") & " MB"
            End Get
        End Property
    End Class

End Namespace
