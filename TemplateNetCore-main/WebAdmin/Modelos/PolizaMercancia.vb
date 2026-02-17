Namespace MercanciaSegura.DOM.Modelos

    Public Class PolizaMercancia
        Public Property PolizaId As Integer
        Public Property NombreInternoPoliza As String
        Public Property FolioPoliza As String
        Public Property BienesAsegurados As String
        Public Property BienesExcluidos As String
        Public Property BienesSujetosAConsulta As String

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

        Public Property Poliza As Poliza
    End Class

End Namespace
