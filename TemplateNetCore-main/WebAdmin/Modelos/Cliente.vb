Namespace MercanciaSegura.DOM.Modelos

    Public Class Cliente
        Public Property ClienteId As Integer

        Public Property TipoSeguroId As Integer?
        Public Property OrigenClienteId As Integer?
        Public Property TipoCuentaId As Integer?
        Public Property TipoSectorId As Integer?
        Public Property RegimenFiscalId As Integer?
        Public Property TipoPersonaId As Integer?

        Public Property Rfc As String
        Public Property RfcGenericoId As Integer?
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

        Public Property FechaActualizacion As Date?
        Public Property FechaRegistro As Date = Date.Now
        Public Property EstatusId As Integer?
        Public Property FechaBaja As Date?

        Public Property Clave As String
        Public Property Genero As String
        Public Property Correos As New List(Of Correos)
        Public Property Cuota As New List(Of Cuota)
        Public Property BeneficiariosPreferentes As New List(Of BeneficiarioPreferente)
        Public Property ClienteVendedor As New List(Of ClienteVendedor)

    End Class

End Namespace
