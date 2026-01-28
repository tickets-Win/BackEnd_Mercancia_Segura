Namespace MercanciaSegura.DOM.Modelos

    Public Class Cliente
        Public Property ClienteId As Integer
        Public Property TipoSeguroId As Integer?
        Public Property OrigenClienteId As Integer?
        Public Property TipoCuentaId As Integer?
        Public Property TipoSectorId As Integer?
        Public Property RegimenFiscalId As Integer?
        Public Property BeneficiarioPreferenteId As Integer?
        Public Property EstatusId As Integer?
        Public Property Clave As String
        Public Property TipoPersona As String
        Public Property Rfc As String
        Public Property RfcGenerico As String
        Public Property Telefono As String
        Public Property CorreoElectronico As String
        Public Property Nacionalidad As String
        Public Property Calle As String
        Public Property NumeroInt As String
        Public Property NumeroExt As String
        Public Property Pais As String
        Public Property Municipio As String
        Public Property Poblacion As String
        Public Property Colonia As String
        Public Property Estado As String
        Public Property Cp As String
        Public Property ApellidoPaterno As String
        Public Property ApellidoMaterno As String
        Public Property Nombres As String
        Public Property NombreCompleto As String

        Public Property CuotaMinimaInternacional As Decimal?
        Public Property CuotaMinimaNacional As Decimal?
        Public Property CuotaAplicableInternacional As Decimal?
        Public Property CuotaAplicableNacional As Decimal?

        Public Property FechaActualizacion As DateTime?
        Public Property FechaRegistro As DateTime = DateTime.Now
    End Class

End Namespace
