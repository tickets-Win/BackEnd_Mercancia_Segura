Namespace MercanciaSegura.DOM.Modelos
    Public Class RegimenFiscal
        Public Property RegimenFiscalId As Integer
        Public Property Descripcion As String
        Public Property AplicaMoral As Boolean
        Public Property AplicaFisica As Boolean
        Public Property Codigo As Integer?

        Public ReadOnly Property CodigoDescripcion As String
            Get
                Return $"{Codigo} - {Descripcion}"
            End Get
        End Property
    End Class

End Namespace
