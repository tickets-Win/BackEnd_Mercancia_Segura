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
        Public Property rfcGenerico As String
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

        Public ReadOnly Property RFCAMostrar As String
            Get
                If Not String.IsNullOrEmpty(Me.RFC) Then
                    Return Me.RFC
                End If

                If Me.RfcGenericoId.HasValue Then
                    Dim listaRFCGenericos As List(Of rfcGenerico) = TryCast(HttpContext.Current.Session("ListaRFCGenericos"), List(Of rfcGenerico))
                    If listaRFCGenericos IsNot Nothing Then
                        Dim seleccionado = listaRFCGenericos.FirstOrDefault(Function(x) x.RfcGenericoId = Me.RfcGenericoId.Value)
                        If seleccionado IsNot Nothing Then
                            Return seleccionado.Tipo
                        End If
                    End If
                End If

                Return String.Empty
            End Get
        End Property
    End Class
End Namespace