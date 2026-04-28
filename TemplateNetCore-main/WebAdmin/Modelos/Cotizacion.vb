Namespace MercanciaSegura.DOM.Modelos
    Public Class Cotizacion
        Public Property CotizacionId As Integer
        Public Property PolizaId As Integer
        Public Property FechaCotizacion As DateTime
        Public Property ClienteId As Integer
        Public Property nombreCliente As String
        Public Property BeneficiarioPreferenteId As Integer?
        Public Property MonedaId As Integer

        Public Property VigenciaDel As DateTime?
        Public Property VigenciaHasta As DateTime?
        Public Property FechaCancelacion As DateTime?
        Public Property PrimaServicioDeAseguramiento As Decimal?
        Public Property Subtotal As Decimal?
        Public Property IVA As Decimal?
        Public Property Total As Decimal?
        Public Property GastosExpedicion As Decimal?
        Public Property CotizacionMercancia As CotizacionMercancia
        Public Property CotizacionContenedor As List(Of CotizacionContenedor)


    End Class

End Namespace
