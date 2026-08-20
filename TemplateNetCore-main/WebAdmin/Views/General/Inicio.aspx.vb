Imports Newtonsoft.Json
Imports WebAdmin.MercanciaSegura.DOM.Modelos

Public Class Inicio
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ' El aviso se apaga al empezar cada petición: Page_Load corre antes que
        ' los eventos, así que un AvisarCambio() de este mismo clic sí se ve.
        pnlAvisoCambio.Visible = False

        If Not IsPostBack Then
            CargarIndicadores()
            CargarTipoCambio()
        End If

    End Sub

#Region "Tipo de cambio"

    ''' <summary>
    ''' El tipo de cambio vive en la moneda, no en una tabla aparte: son las
    ''' columnas Tipo_Cambio y Tipo_Cambio_Ventanilla. Solo aplica al dólar.
    ''' </summary>
    Private Const MonedaDolar As Integer = 2

    Private Sub CargarTipoCambio()

        Dim dolar = MonedaDolares()

        If dolar Is Nothing Then
            AvisarCambio("No se pudo consultar el tipo de cambio.", "warning")
            Exit Sub
        End If

        txtUsdDof.Text = Importe(dolar.TipoCambio)
        txtUsdBancario.Text = Importe(dolar.TipoCambioVentanilla)
    End Sub

    Private Function MonedaDolares() As Moneda

        Dim api As New ConsumoApi()

        Dim monedas = Deserializar(Of Moneda)(api.GetMoneda())

        If monedas Is Nothing Then Return Nothing

        Return monedas.FirstOrDefault(Function(m) m.MonedaId = MonedaDolar)
    End Function

    Protected Sub btnActualizarCambio_Click(sender As Object, e As EventArgs)

        Dim dof As Decimal? = MontoDe(txtUsdDof)
        Dim bancario As Decimal? = MontoDe(txtUsdBancario)

        If Not dof.HasValue AndAlso Not bancario.HasValue Then
            AvisarCambio("Captura al menos uno de los dos tipos de cambio.", "warning")
            Exit Sub
        End If

        If (dof.HasValue AndAlso dof.Value <= 0) OrElse
           (bancario.HasValue AndAlso bancario.Value <= 0) Then

            AvisarCambio("El tipo de cambio debe ser mayor a cero.", "warning")
            Exit Sub
        End If

        Dim peticion = New With {
            .tipoCambio = dof,
            .tipoCambioVentanilla = bancario
        }

        Dim api As New ConsumoApi()
        Dim respuesta As String = api.PutTipoCambio(MonedaDolar, JsonConvert.SerializeObject(peticion))

        If String.IsNullOrWhiteSpace(respuesta) OrElse respuesta.StartsWith("ERROR") Then
            AvisarCambio("No se pudo actualizar: " & DetalleDeError(respuesta), "danger")
            Exit Sub
        End If

        AvisarCambio("Tipo de cambio actualizado.", "success")

        CargarTipoCambio()
    End Sub

    ''' <summary>Deja el importe con dos decimales, sin símbolo, para poder editarlo.</summary>
    Private Function Importe(valor As Decimal?) As String

        If Not valor.HasValue OrElse valor.Value = 0 Then Return String.Empty

        Return valor.Value.ToString("N4").TrimEnd("0"c).TrimEnd("."c)
    End Function

    Private Function MontoDe(txt As TextBox) As Decimal?

        If txt Is Nothing OrElse String.IsNullOrWhiteSpace(txt.Text) Then Return Nothing

        Dim limpio As String = Text.RegularExpressions.Regex.Replace(txt.Text, "[^0-9.\-]", "")

        Dim valor As Decimal

        If Not Decimal.TryParse(limpio, valor) Then Return Nothing

        Return valor
    End Function

    Private Function DetalleDeError(respuesta As String) As String

        If String.IsNullOrWhiteSpace(respuesta) Then Return "sin respuesta del servidor"

        Dim separador As Integer = respuesta.IndexOf(":"c)

        If separador < 0 Then Return respuesta.Trim()

        Return respuesta.Substring(separador + 1).Trim()
    End Function

    Private Sub AvisarCambio(mensaje As String, tipo As String)
        lblAvisoCambio.Text = mensaje
        pnlAvisoCambio.CssClass = "alert alert-" & tipo & " py-2"
        pnlAvisoCambio.Visible = True
    End Sub

#End Region

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
