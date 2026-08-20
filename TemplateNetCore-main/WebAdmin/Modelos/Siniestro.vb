Namespace MercanciaSegura.DOM.Modelos
    Public Class Siniestro
        Public Property SiniestroId As Integer
        Public Property CertificadoId As Integer
        Public Property NReporte As String
        Public Property FechaApertura As DateTime
        Public Property FechaCierre As DateTime?
        Public Property TipoSiniestroId As Integer?
        Public Property Mercancia As String
        Public Property LugarDeSiniestro As String
        Public Property SumaAsegurada As Decimal?
        Public Property MontoDeReclamo As Decimal?
        Public Property MontoDeIndemnizacion As Decimal?
        Public Property TipoDeEventoId As String

        ' Datos que agrega el API.
        Public Property NombreTipoSiniestro As String
        Public Property NumeroCertificado As String
    End Class

End Namespace
