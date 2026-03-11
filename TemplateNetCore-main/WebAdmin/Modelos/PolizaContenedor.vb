Namespace MercanciaSegura.DOM.Modelos

    Public Class PolizaContenedor
        Public Property PolizaContenedorId As Integer
        Public Property PolizaId As Integer?

        Public Property NombreInternoPoliza As String

        Public Property PorContenedor As Decimal?
        Public Property Ferrocarril As Decimal?
        Public Property Terrestre As Decimal?
        Public Property CuotaAplicable As Decimal?
        Public Property ManiobrasRescate As Decimal?
        Public Property DanioMaterial As Decimal?
        Public Property Robo As Decimal?
        Public Property PerdidaParcial As Decimal?
        Public Property PerdidaTotal As Decimal?

        Public Property TrayectosAsegurados As String
        Public Property MedioTransporte As String

        Public Property PrimaNeta As Decimal?
        Public Property DerechoPoliza As Decimal?
        Public Property OtroPrima As Decimal?
        Public Property IVA As Decimal?
        Public Property PrimaTotal As Decimal?

        Public Property Poliza As Poliza

        Public Property Cobertura As New List(Of Cobertura)


    End Class

End Namespace