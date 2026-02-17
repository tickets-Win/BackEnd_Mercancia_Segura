Namespace MercanciaSegura.DOM.Modelos
    Public Class Poliza
        Public Property PolizaId As Integer
        Public Property ProductoId As Integer?
        Public Property ContratanteId As Integer?
        Public Property AseguradoraId As Integer?
        Public Property SubRamoId As Integer?
        Public Property MonedaId As Integer?
        Public Property FormaPagoId As Integer?

        Public Property EstatusPolizaId As String
        Public Property TipoPoliza As String
        Public Property NumeroPoliza As String

        Public Property VigenciaDel As DateTime?
        Public Property VigenciaHasta As DateTime?
        Public Property OtrosPoliza As String

        Public Property PrimaNeta As Decimal?
        Public Property DerechoPoliza As Decimal?
        Public Property Otros As Decimal?
        Public Property IVA As Decimal?
        Public Property PrimaTotal As Decimal?

        Public Property FechaActualizacion As DateTime?
        Public Property FechaBaja As DateTime?
        Public Property FechaRegistro As DateTime = DateTime.Now

        Public Property Contratante As Contratante

    End Class

End Namespace
