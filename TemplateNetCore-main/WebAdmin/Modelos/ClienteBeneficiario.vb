Namespace MercanciaSegura.DOM.Modelos
    Public Class ClienteBeneficiario
        Public Property ClienteBeneficiarioId As Integer
        Public Property ClienteId As Integer
        Public Property BeneficiarioPreferenteId As Integer

        Public Property claveBP As String
        Public Property nombreCompletoBP As String
        Public Property rfcbp As String
        Public Property rfcGenericoBP As String
        Public Property paisBP As String

        Public ReadOnly Property RFCAMostrar As String
            Get
                If Not String.IsNullOrEmpty(Me.rfcbp) Then
                    Return Me.rfcbp
                End If

                If Not String.IsNullOrEmpty(Me.rfcGenericoBP) Then
                    Return Me.rfcGenericoBP
                End If

                Return String.Empty
            End Get
        End Property
    End Class
End Namespace
