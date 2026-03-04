Imports Newtonsoft.Json

Namespace MercanciaSegura.DOM.Modelos

    Public Class BeneficiarioPreferente

        Public Property BeneficiarioPreferenteId As Integer

        Public Property RFC As String
        Public Property ClienteId As Integer?
        Public Property Clave As String
        Public Property TipoPersonaId As Integer?
        Public Property ApellidoPaterno As String
        Public Property ApellidoMaterno As String
        Public Property Nombre As String
        Public Property NombreCompleto As String
        Public Property RfcGenericoId As Integer?
        Public Property Calle As String
        Public Property NumeroInt As String
        Public Property NumeroExt As String
        Public Property Pais As String
        Public Property Poblacion As String
        Public Property Colonia As String
        Public Property Cp As String
        Public Property Nacionalidad As String
        Public Property FechaActualizacion As Date?
        Public Property FechaRegistro As Date = Date.Now
    End Class
End Namespace