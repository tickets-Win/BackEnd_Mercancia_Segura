Imports Newtonsoft.Json

Namespace MercanciaSegura.DOM.Modelos
    Public Class Cuota
        Public Property CuotaId As Integer

        Public Property TipoCuotaId As Integer?
        Public Property Monto As Decimal?
        Public Property ClienteId As Integer?
        Public Property TipoTarifaId As Integer?
    End Class
End Namespace