Imports System.Text
Imports Newtonsoft.Json
Imports WebAdmin.MercanciaSegura.DOM.Modelos

Public Class AdminReportes
    Inherits System.Web.UI.Page

    ''' <summary>
    ''' Renglón del reporte. Se arma a partir del certificado, que es lo que
    ''' amarra cliente, póliza, tipo, estatus, fecha y suma asegurada.
    ''' </summary>
    Public Class RenglonReporte
        Public Property Folio As String
        Public Property Cliente As String
        Public Property Poliza As String
        Public Property Certificado As String
        Public Property Tipo As String
        Public Property Estatus As String
        Public Property FechaEmision As DateTime
        Public Property SumaAsegurada As Decimal
    End Class

    Private Const TodosLosClientes As String = ""
    Private Const TodasLasAseguradoras As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            CargarFiltros()
            GenerarReporte()
        End If

    End Sub

#Region "Filtros"

    Private Sub CargarFiltros()

        Dim api As New ConsumoApi()

        ' Clientes. El certificado trae el nombre del cliente, no su id, así que
        ' el filtro trabaja por nombre.
        ddlCliente.Items.Clear()
        ddlCliente.Items.Add(New ListItem("Todos los clientes", TodosLosClientes))

        Dim clientes = Deserializar(Of Cliente)(api.GetCargarClientes())

        If clientes IsNot Nothing Then
            For Each c In clientes.
                Where(Function(x) Not String.IsNullOrWhiteSpace(x.NombreCompleto)).
                OrderBy(Function(x) x.NombreCompleto)

                ddlCliente.Items.Add(New ListItem(c.NombreCompleto, c.NombreCompleto))
            Next
        End If

        ' Aseguradoras: salen de las pólizas, que son las que la traen.
        ddlAseguradora.Items.Clear()
        ddlAseguradora.Items.Add(New ListItem("Todas las aseguradoras", TodasLasAseguradoras))

        For Each nombre In Polizas().
            Select(Function(p) p.nombreAseguradora).
            Where(Function(n) Not String.IsNullOrWhiteSpace(n)).
            Distinct().
            OrderBy(Function(n) n)

            ddlAseguradora.Items.Add(New ListItem(nombre, nombre))
        Next

        ' Estatus de los certificados.
        ddlEstatus.Items.Clear()
        ddlEstatus.Items.Add(New ListItem("Todos los estatus", ""))

        For Each nombre In Certificados().
            Select(Function(c) c.NombreEstatus).
            Where(Function(n) Not String.IsNullOrWhiteSpace(n)).
            Distinct().
            OrderBy(Function(n) n)

            ddlEstatus.Items.Add(New ListItem(nombre, nombre))
        Next
    End Sub

    Protected Sub lnkBuscar_Click(sender As Object, e As EventArgs)
        gvResultados.PageIndex = 0

        GenerarReporte()
    End Sub

    Protected Sub lnkLimpiar_Click(sender As Object, e As EventArgs)

        txtFechaInicio.Text = String.Empty
        txtFechaFin.Text = String.Empty

        ResetearCombo(ddlCliente)
        ResetearCombo(ddlAseguradora)
        ResetearCombo(ddlTipoReporte)
        ResetearCombo(ddlEstatus)

        gvResultados.PageIndex = 0

        GenerarReporte()
    End Sub

    Private Sub ResetearCombo(ddl As DropDownList)
        ddl.ClearSelection()

        If ddl.Items.Count > 0 Then ddl.Items(0).Selected = True
    End Sub

    Protected Sub gvResultados_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        gvResultados.PageIndex = e.NewPageIndex

        GenerarReporte()
    End Sub

    ''' <summary>
    ''' Imprime el reporte completo. Se quita el paginado antes de dibujar: si no,
    ''' saldrían solo los ocho renglones de la página en pantalla. El diálogo se
    ''' abre desde el cliente una vez que la página ya se pintó entera.
    ''' </summary>
    Protected Sub lnkImprimir_Click(sender As Object, e As EventArgs)

        gvResultados.AllowPaging = False

        GenerarReporte()

        ClientScript.RegisterStartupScript(Me.GetType(), "imprimir",
            "window.addEventListener('load', function () { window.print(); });", True)
    End Sub

#End Region

#Region "Datos"

    ''' <summary>
    ''' Las consultas se cachean por petición: el reporte las usa varias veces
    ''' (indicadores, gráficas y tabla) y no tiene caso repetirlas.
    ''' </summary>
    Private Function Certificados() As List(Of Certificado)

        If Items("Certificados") Is Nothing Then
            Dim api As New ConsumoApi()

            Items("Certificados") = If(Deserializar(Of Certificado)(api.GetCertificados()),
                                       New List(Of Certificado))
        End If

        Return DirectCast(Items("Certificados"), List(Of Certificado))
    End Function

    Private Function Polizas() As List(Of Poliza)

        If Items("Polizas") Is Nothing Then
            Dim api As New ConsumoApi()

            Items("Polizas") = If(Deserializar(Of Poliza)(api.GetCargarPolizas()),
                                  New List(Of Poliza))
        End If

        Return DirectCast(Items("Polizas"), List(Of Poliza))
    End Function

    Private Function Siniestros() As List(Of Siniestro)

        If Items("Siniestros") Is Nothing Then
            Dim api As New ConsumoApi()

            Items("Siniestros") = If(Deserializar(Of Siniestro)(api.GetSiniestros()),
                                     New List(Of Siniestro))
        End If

        Return DirectCast(Items("Siniestros"), List(Of Siniestro))
    End Function

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

#End Region

#Region "Generacion del reporte"

    Private Sub GenerarReporte()

        Dim renglones = RenglonesFiltrados()

        ' Indicadores
        litTotalCertificados.Text = renglones.Count.ToString("N0")
        litTotalPolizas.Text = PolizasFiltradas().Count.ToString("N0")
        litTotalSiniestros.Text = SiniestrosFiltrados().Count.ToString("N0")
        litSumaAsegurada.Text = FormatoMillones(renglones.Sum(Function(r) r.SumaAsegurada))

        ' Gráficas
        DibujarBarras(renglones)
        DibujarLinea()
        DibujarDona(renglones)

        ' Tabla
        Dim ultimaPagina As Integer = 0

        If renglones.Count > 0 Then
            ultimaPagina = CInt(Math.Ceiling(renglones.Count / CDbl(gvResultados.PageSize))) - 1
        End If

        If gvResultados.PageIndex > ultimaPagina Then gvResultados.PageIndex = ultimaPagina

        gvResultados.DataSource = renglones
        gvResultados.DataBind()

        litEncontrados.Text = renglones.Count & If(renglones.Count = 1, " registro encontrado", " registros encontrados")

        If renglones.Count = 0 Then
            litMostrando.Text = "Sin resultados"

        ElseIf Not gvResultados.AllowPaging Then
            ' Vista de impresión: salen todos.
            litMostrando.Text = String.Format("{0} resultados", renglones.Count)

        Else
            Dim desde As Integer = gvResultados.PageIndex * gvResultados.PageSize + 1
            Dim hasta As Integer = Math.Min(desde + gvResultados.PageSize - 1, renglones.Count)

            litMostrando.Text = String.Format("Mostrando {0}-{1} de {2} resultados", desde, hasta, renglones.Count)
        End If
    End Sub

    ''' <summary>
    ''' Aplica todos los filtros sobre los certificados y los deja como renglones.
    ''' </summary>
    Private Function RenglonesFiltrados() As List(Of RenglonReporte)

        Dim lista = Certificados().AsEnumerable()

        Dim desde As Date
        If Date.TryParse(txtFechaInicio.Text, desde) Then
            lista = lista.Where(Function(c) c.FechaRegistro.Date >= desde.Date)
        End If

        Dim hasta As Date
        If Date.TryParse(txtFechaFin.Text, hasta) Then
            lista = lista.Where(Function(c) c.FechaRegistro.Date <= hasta.Date)
        End If

        If ddlCliente.SelectedValue <> TodosLosClientes Then
            lista = lista.Where(Function(c) c.NombreCliente = ddlCliente.SelectedValue)
        End If

        If ddlTipoReporte.SelectedValue <> "" Then
            lista = lista.Where(Function(c) c.TipoCotizacion = ddlTipoReporte.SelectedValue)
        End If

        If ddlEstatus.SelectedValue <> "" Then
            lista = lista.Where(Function(c) c.NombreEstatus = ddlEstatus.SelectedValue)
        End If

        ' La aseguradora vive en la póliza, no en el certificado: se cruza por
        ' número de póliza.
        If ddlAseguradora.SelectedValue <> TodasLasAseguradoras Then

            Dim delaAseguradora As New HashSet(Of String)

            For Each p In Polizas().Where(Function(x) x.nombreAseguradora = ddlAseguradora.SelectedValue)
                If Not String.IsNullOrWhiteSpace(p.NumeroPoliza) Then delaAseguradora.Add(p.NumeroPoliza)
            Next

            lista = lista.Where(Function(c) c.NumeroPoliza IsNot Nothing AndAlso delaAseguradora.Contains(c.NumeroPoliza))
        End If

        Dim ordenados = lista.OrderByDescending(Function(c) c.FechaRegistro).ToList()

        Dim renglones As New List(Of RenglonReporte)

        For i As Integer = 0 To ordenados.Count - 1

            Dim c = ordenados(i)

            renglones.Add(New RenglonReporte With {
                .Folio = "RPT-" & (i + 1).ToString("000"),
                .Cliente = c.NombreCliente,
                .Poliza = c.NumeroPoliza,
                .Certificado = If(String.IsNullOrWhiteSpace(c.ClaveCertificado), "(sin clave)", c.ClaveCertificado),
                .Tipo = c.TipoCotizacion,
                .Estatus = c.NombreEstatus,
                .FechaEmision = c.FechaRegistro,
                .SumaAsegurada = c.SumaAsegurada
            })
        Next

        Return renglones
    End Function

    Private Function PolizasFiltradas() As List(Of Poliza)

        Dim lista = Polizas().AsEnumerable()

        If ddlAseguradora.SelectedValue <> TodasLasAseguradoras Then
            lista = lista.Where(Function(p) p.nombreAseguradora = ddlAseguradora.SelectedValue)
        End If

        Return lista.ToList()
    End Function

    Private Function SiniestrosFiltrados() As List(Of Siniestro)

        Dim lista = Siniestros().AsEnumerable()

        Dim desde As Date
        If Date.TryParse(txtFechaInicio.Text, desde) Then
            lista = lista.Where(Function(s) s.FechaApertura.Date >= desde.Date)
        End If

        Dim hasta As Date
        If Date.TryParse(txtFechaFin.Text, hasta) Then
            lista = lista.Where(Function(s) s.FechaApertura.Date <= hasta.Date)
        End If

        Return lista.ToList()
    End Function

#End Region

#Region "Graficas"

    ''' <summary>Los últimos 12 meses, del más viejo al más reciente.</summary>
    Private Function UltimosMeses() As List(Of Date)

        Dim meses As New List(Of Date)
        Dim primero As Date = New Date(Date.Today.Year, Date.Today.Month, 1)

        For i As Integer = 11 To 0 Step -1
            meses.Add(primero.AddMonths(-i))
        Next

        Return meses
    End Function

    Private Sub DibujarBarras(renglones As List(Of RenglonReporte))

        Dim meses = UltimosMeses()

        Dim conteos = meses.Select(Function(m) renglones.Where(
            Function(r) r.FechaEmision.Year = m.Year AndAlso r.FechaEmision.Month = m.Month).Count()).ToList()

        Dim maximo As Integer = If(conteos.Count = 0, 0, conteos.Max())

        Dim barras As New StringBuilder()

        For i As Integer = 0 To meses.Count - 1
            ' Altura mínima de 2% para que el mes vacío se vea como base.
            Dim alto As Integer = If(maximo = 0, 2, Math.Max(2, CInt(conteos(i) / CDbl(maximo) * 100)))

            barras.AppendFormat("<span style=""height:{0}%"" title=""{1:MMM yyyy}: {2}""></span>",
                                alto, meses(i), conteos(i))
        Next

        litBarras.Text = barras.ToString()
        litEjeBarras.Text = EtiquetasEje(meses)
    End Sub

    Private Sub DibujarLinea()

        Dim meses = UltimosMeses()
        Dim siniestros = SiniestrosFiltrados()

        Dim conteos = meses.Select(Function(m) siniestros.Where(
            Function(s) s.FechaApertura.Year = m.Year AndAlso s.FechaApertura.Month = m.Month).Count()).ToList()

        Dim maximo As Integer = If(conteos.Count = 0, 0, conteos.Max())

        Dim puntos As New StringBuilder()

        For i As Integer = 0 To meses.Count - 1
            Dim x As Integer = CInt(i / CDbl(Math.Max(1, meses.Count - 1)) * 300)
            ' El SVG crece hacia abajo: 130 es el piso y 10 el techo.
            Dim y As Integer = If(maximo = 0, 125, 125 - CInt(conteos(i) / CDbl(maximo) * 110))

            If i > 0 Then puntos.Append(" ")
            puntos.AppendFormat("{0},{1}", x, y)
        Next

        litLinea.Text =
            "<svg viewBox=""0 0 300 130"" preserveAspectRatio=""none"" width=""100%"" height=""100%"">" &
            "<polyline fill=""none"" stroke=""#1f4e8c"" stroke-width=""2"" points=""" & puntos.ToString() & """ />" &
            "</svg>"

        litEjeLinea.Text = EtiquetasEje(meses)
    End Sub

    Private Function EtiquetasEje(meses As List(Of Date)) As String

        Dim etiquetas As New StringBuilder()

        ' Una etiqueta sí y otra no, para que no se encimen.
        For i As Integer = 0 To meses.Count - 1 Step 2
            etiquetas.AppendFormat("<span>{0:MMM}</span>", meses(i))
        Next

        Return etiquetas.ToString()
    End Function

    ''' <summary>
    ''' Dona por estatus. Cada rebanada es un arco sobre la misma circunferencia,
    ''' desplazado por lo que ya llevan las anteriores.
    ''' </summary>
    Private Sub DibujarDona(renglones As List(Of RenglonReporte))

        Const circunferencia As Double = 282.74     ' 2 * PI * 45

        Dim colores As New Dictionary(Of String, String) From {
            {"Activo", "#2f9e5c"},
            {"Suspendido", "#e8963c"},
            {"Moroso", "#17406d"}
        }

        Dim grupos = renglones.
            GroupBy(Function(r) If(String.IsNullOrWhiteSpace(r.Estatus), "Sin estatus", r.Estatus)).
            Select(Function(g) New With {.Nombre = g.Key, .Total = g.Count()}).
            OrderByDescending(Function(g) g.Total).
            ToList()

        Dim total As Integer = grupos.Sum(Function(g) g.Total)

        Dim dona As New StringBuilder()
        Dim leyenda As New StringBuilder()

        dona.Append("<svg width=""120"" height=""120"" viewBox=""0 0 120 120"">")

        If total = 0 Then
            dona.Append("<circle cx=""60"" cy=""60"" r=""45"" fill=""none"" stroke=""#e3e8ef"" stroke-width=""18"" />")
            leyenda.Append("<span class=""text-muted"">Sin certificados</span>")
        Else
            Dim acumulado As Double = 0
            Dim indice As Integer = 0
            Dim reserva As String() = {"#2f9e5c", "#e8963c", "#17406d", "#8b5cf6", "#dc2626"}

            For Each g In grupos

                Dim color As String = If(colores.ContainsKey(g.Nombre), colores(g.Nombre), reserva(indice Mod reserva.Length))
                Dim largo As Double = g.Total / CDbl(total) * circunferencia

                dona.AppendFormat(
                    "<circle cx=""60"" cy=""60"" r=""45"" fill=""none"" stroke=""{0}"" stroke-width=""18"" " &
                    "stroke-dasharray=""{1:F1} {2:F1}"" stroke-dashoffset=""{3:F1}"" transform=""rotate(-90 60 60)"" />",
                    color, largo, circunferencia - largo, -acumulado)

                leyenda.AppendFormat(
                    "<span><i class=""bi bi-circle-fill"" style=""color:{0}""></i> {1} ({2})</span>",
                    color, Server.HtmlEncode(g.Nombre), g.Total)

                acumulado += largo
                indice += 1
            Next
        End If

        dona.Append("</svg>")

        litDona.Text = dona.ToString()
        litLeyenda.Text = leyenda.ToString()
    End Sub

#End Region

#Region "Exportar"

    ''' <summary>
    ''' Exporta a CSV, que Excel abre directo. Lleva BOM para que respete los
    ''' acentos, y separador de punto y coma, que es lo que espera Excel en
    ''' configuración regional de México.
    ''' </summary>
    Protected Sub lnkExportarExcel_Click(sender As Object, e As EventArgs)

        Dim renglones = RenglonesFiltrados()

        Dim csv As New StringBuilder()
        csv.AppendLine("Folio;Cliente;Poliza;Certificado;Tipo;Estatus;Fecha de emision;Suma asegurada")

        For Each r In renglones
            csv.AppendLine(String.Join(";", {
                Escapar(r.Folio),
                Escapar(r.Cliente),
                Escapar(r.Poliza),
                Escapar(r.Certificado),
                Escapar(r.Tipo),
                Escapar(r.Estatus),
                r.FechaEmision.ToString("dd/MM/yyyy"),
                r.SumaAsegurada.ToString("F2")
            }))
        Next

        Dim nombre As String = "Reporte_" & Date.Now.ToString("yyyyMMdd_HHmm") & ".csv"

        Response.Clear()
        Response.ContentType = "text/csv"
        Response.ContentEncoding = Encoding.UTF8
        Response.AddHeader("Content-Disposition", "attachment; filename=" & nombre)
        Response.BinaryWrite(Encoding.UTF8.GetPreamble())
        Response.Write(csv.ToString())
        Response.Flush()
        Response.SuppressContent = True
        HttpContext.Current.ApplicationInstance.CompleteRequest()
    End Sub

    ''' <summary>Neutraliza los separadores dentro de un campo.</summary>
    Private Function Escapar(valor As String) As String

        If String.IsNullOrEmpty(valor) Then Return String.Empty

        Return valor.Replace(";", ",").Replace(vbCr, " ").Replace(vbLf, " ")
    End Function

#End Region

#Region "Utilerias"

    Protected Function ClaseEstatus(valor As Object) As String

        Select Case Convert.ToString(valor)
            Case "Activo" : Return "rep-badge rep-badge-activo"
            Case "Suspendido" : Return "rep-badge rep-badge-vencido"
            Case "Moroso" : Return "rep-badge rep-badge-cancelado"
            Case Else : Return "rep-badge rep-badge-vencido"
        End Select
    End Function

    ''' <summary>Importes grandes en formato corto: $142.8M, $850.0K.</summary>
    Private Function FormatoMillones(valor As Decimal) As String

        If valor >= 1000000D Then Return "$" & (valor / 1000000D).ToString("N1") & "M"
        If valor >= 1000D Then Return "$" & (valor / 1000D).ToString("N1") & "K"

        Return valor.ToString("C2")
    End Function

#End Region

End Class
