Namespace MercanciaSegura.DOM.Modelos
    Public Class Endoso
        Public Property EndosoId As Integer
        Public Property TipoEndosoId As Integer
        Public Property NumeroEndoso As String
        Public Property CertificadoId As Integer
        Public Property FechaElaboracion As DateTime
        Public Property Agente As String
        Public Property RFC As String
        Public Property Oficina As String
        Public Property BeneficiarioPreferente As String
        Public Property MonedaId As Integer?

        ' Lo que el endoso cambia. El certificado no se modifica: conserva lo
        ' que se emitio, y el endoso es la constancia del cambio.
        Public Property VigenciaDel As DateTime?
        Public Property VigenciaHasta As DateTime?
        Public Property SumaAsegurada As Decimal?
        Public Property PrimaServicioDeAseguramiento As Decimal?
        Public Property IVA As Decimal?
        Public Property TotalAPagar As Decimal?
        Public Property Descripcion As String

        ' Datos que agrega el API.
        Public Property NombreTipoEndoso As String
        Public Property NumeroCertificado As String
        Public Property Moneda As String
    End Class

    Public Class TipoEndoso
        Public Property TipoEndosoId As Integer
        Public Property Tipo As String
    End Class

End Namespace
