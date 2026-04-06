Namespace MercanciaSegura.DOM.Modelos
    Public Class Cotizacion
        Public Property CotizacionId As Integer
        Public Property PolizaId As Integer
        Public Property ClienteId As Integer

        Public Property FechaCotizacion As DateTime
        Public Property VigenciaDel As DateTime?
        Public Property VigenciaHasta As DateTime?
        Public Property SumaAsegurada As Decimal?
        Public Property GastosExpedicion As Decimal?
        Public Property MotivoCancelacion As String
        Public Property FechaCancelacion As DateTime?
        Public Property CotizacionMercancia As CotizacionMercancia
        Public Property CotizacionContenedor As CotizacionContenedor


    End Class

End Namespace
