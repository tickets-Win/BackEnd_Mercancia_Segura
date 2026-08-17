Namespace MercanciaSegura.DOM.Modelos

    ''' <summary>
    ''' Bien asegurado de una cotización. El API no guarda una referencia al bien de
    ''' la póliza: guarda una copia con su tipo, su administración y su nombre, para
    ''' que la cotización conserve el dato tal como estaba al momento de emitirla.
    ''' </summary>
    Public Class BienCotizacion
        Public Property BienCotizacionId As Integer
        Public Property CotizacionId As Integer?
        Public Property TipoBienId As Integer?
        Public Property AdministracionBienId As Integer?
        Public Property NombreTipoBien As String
        Public Property Nombre As String
    End Class

End Namespace
