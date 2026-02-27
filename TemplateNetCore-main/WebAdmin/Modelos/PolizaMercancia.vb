Namespace MercanciaSegura.DOM.Modelos

    Public Class PolizaMercancia
        Public Property PolizaMercanciaId As Integer
        Public Property PolizaId As Integer?
        Public Property AdministracionBienId As Integer

        Public Property NombreInternoPoliza As String

        Public Property TerrestreAereo As Decimal?
        Public Property Maritimo As Decimal?
        Public Property PaqueteriaMensajeria As Decimal?

        Public Property Deducibles As String
        Public Property Compras As String
        Public Property Ventas As String
        Public Property Maquila As String
        Public Property BienesUsados As String
        Public Property EmbarqueFiliales As String
        Public Property IndemnizacionOtros As String

        Public Property Medicamentos As Decimal?
        Public Property CobreAluminioAcero As Decimal?
        Public Property MedicamentosControlados As Decimal?
        Public Property EqContratistas As Decimal?

        Public Property CuotaGeneralPoliza As Decimal?

        Public Property PrimaNeta As Decimal?
        Public Property DerechoPoliza As Decimal?
        Public Property OtroPrima As Decimal?
        Public Property IVA As Decimal?
        Public Property PrimaTotal As Decimal?
        Public Property RiesgoCubierto As New List(Of RiesgoCubierto)
    End Class

End Namespace
