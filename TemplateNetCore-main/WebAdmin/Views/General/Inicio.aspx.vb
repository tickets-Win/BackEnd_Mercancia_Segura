Imports Newtonsoft.Json
Imports WebAdmin.MercanciaSegura.DOM.Modelos

Public Class Inicio
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            CargarIndicadores()
        End If

    End Sub

    Private Sub CargarIndicadores()

        Dim api As New ConsumoApi()

        lblClientes.Text = Formato(TotalClientes(api))
        lblCotizacionesPendientes.Text = Formato(CotizacionesPendientes(api))

        ' Recibos vencidos todavía no tiene de dónde salir: la tabla Cobranza
        ' existe en la base pero no hay entidad, DbSet ni endpoint que la exponga.
        lblRecibosVencidos.Text = "—"
        pnlRecibosPendiente.Visible = True
    End Sub

    ''' <summary>
    ''' Clientes dados de alta.
    ''' </summary>
    Private Function TotalClientes(api As ConsumoApi) As Integer?

        Dim lista = Deserializar(Of Cliente)(api.GetCargarClientes())

        If lista Is Nothing Then Return Nothing

        Return lista.Count
    End Function

    ''' <summary>
    ''' Cotizaciones vivas que todavía no se confirman. El API solo devuelve las no
    ''' canceladas, y una cotización se considera confirmada cuando ya tiene
    ''' certificado, así que las pendientes son las que no aparecen en certificados.
    ''' </summary>
    Private Function CotizacionesPendientes(api As ConsumoApi) As Integer?

        Dim cotizaciones = Deserializar(Of Cotizacion)(api.GetCargarCotizaciones())

        If cotizaciones Is Nothing Then Return Nothing

        Dim certificados = Deserializar(Of Certificado)(api.GetCertificados())

        If certificados Is Nothing Then Return Nothing

        Dim confirmadas As New HashSet(Of Integer)

        For Each c As Certificado In certificados
            confirmadas.Add(c.CotizacionId)
        Next

        ' .Where(...).Count y no .Count(...): en VB, Count choca con la propiedad
        ' Count de List y no resuelve la sobrecarga de LINQ.
        Return cotizaciones.Where(Function(c) Not confirmadas.Contains(c.CotizacionId)).Count()
    End Function

    ''' <summary>
    ''' Deserializa una lista del API. Devuelve Nothing si la respuesta no sirve,
    ''' para poder distinguir "no hay registros" de "no se pudo consultar".
    ''' </summary>
    Private Function Deserializar(Of T)(json As String) As List(Of T)

        If String.IsNullOrWhiteSpace(json) OrElse
           json = "null" OrElse
           json.StartsWith("ERROR") Then Return Nothing

        Try
            Return JsonConvert.DeserializeObject(Of List(Of T))(json)
        Catch
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Un guion cuando el dato no se pudo traer: es más honesto que un cero, que
    ''' se leería como "no hay ninguno".
    ''' </summary>
    Private Function Formato(valor As Integer?) As String

        If Not valor.HasValue Then Return "—"

        Return valor.Value.ToString("N0")
    End Function

End Class
