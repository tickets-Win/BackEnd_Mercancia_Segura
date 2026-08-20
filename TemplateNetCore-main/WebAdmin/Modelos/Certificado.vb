Namespace MercanciaSegura.DOM.Modelos
    Public Class Certificado
        Public Property CertificadoId As Integer
        Public Property CotizacionId As Integer
        Public Property ClaveCertificado As String
        Public Property Asegurado As String
        Public Property FechaRegistro As DateTime
        Public Property FechaInicio As DateTime
        Public Property FechaFin As DateTime
        Public Property SumaAsegurada As Decimal
        Public Property TipoEstatusId As Integer?

        ' Datos que el API agrega desde la cotización de origen.
        Public Property NombreEstatus As String
        Public Property NombreCliente As String
        Public Property NumeroPoliza As String
        Public Property FechaCotizacion As DateTime?
        Public Property TipoCotizacion As String
        Public Property Total As Decimal?
    End Class

End Namespace
