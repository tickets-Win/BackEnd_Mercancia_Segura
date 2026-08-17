Imports Newtonsoft.Json
Imports WebAdmin.MercanciaSegura.DOM.Modelos

Public Class AdminSiniestros
    Inherits System.Web.UI.Page

    ''' <summary>
    ''' Fila del listado. El cliente no está en el siniestro: sale del certificado
    ''' al que apunta.
    ''' </summary>
    Public Class RenglonSiniestro
        Public Property SiniestroId As Integer
        Public Property NumeroReporte As String
        Public Property Cliente As String
        Public Property FechaSiniestro As DateTime
        Public Property NumeroSiniestro As String
        Public Property TipoSiniestro As String
        Public Property MontoReclamo As Decimal?
    End Class

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CargarSiniestros()
        End If
    End Sub

#Region "Datos"

    ''' <summary>
    ''' Los certificados se consultan una vez por petición: los usan el listado,
    ''' los combos en cascada y el guardado.
    ''' </summary>
    Private Function Certificados() As List(Of Certificado)

        If Items("Certificados") Is Nothing Then
            Dim api As New ConsumoApi()

            Items("Certificados") = If(Deserializar(Of Certificado)(api.GetCertificados()),
                                       New List(Of Certificado))
        End If

        Return DirectCast(Items("Certificados"), List(Of Certificado))
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

#Region "Listado"

    Protected Sub CargarSiniestros()

        Dim api As New ConsumoApi()

        Dim lista = Deserializar(Of Siniestro)(api.GetSiniestros())

        If lista Is Nothing Then
            Avisar("No se pudieron cargar los siniestros.", "danger")
            lista = New List(Of Siniestro)
        End If

        Dim busqueda As String = txtBuscarSiniestro.Text.Trim()

        Dim renglones = lista.Select(Function(s) ArmarRenglon(s)).ToList()

        If busqueda.Length > 0 Then
            renglones = renglones.Where(Function(r)
                                            Return Contiene(r.NumeroReporte, busqueda) OrElse
                                                   Contiene(r.Cliente, busqueda) OrElse
                                                   Contiene(r.NumeroSiniestro, busqueda) OrElse
                                                   Contiene(r.TipoSiniestro, busqueda)
                                        End Function).ToList()
        End If

        Dim ultimaPagina As Integer = 0

        If renglones.Count > 0 Then
            ultimaPagina = CInt(Math.Ceiling(renglones.Count / CDbl(gvSiniestros.PageSize))) - 1
        End If

        If gvSiniestros.PageIndex > ultimaPagina Then gvSiniestros.PageIndex = ultimaPagina

        gvSiniestros.DataSource = renglones
        gvSiniestros.DataBind()
    End Sub

    Private Function ArmarRenglon(s As Siniestro) As RenglonSiniestro

        Dim cert = Certificados().FirstOrDefault(Function(c) c.CertificadoId = s.CertificadoId)

        Return New RenglonSiniestro With {
            .SiniestroId = s.SiniestroId,
            .NumeroReporte = If(String.IsNullOrWhiteSpace(s.NReporte), "(sin reporte)", s.NReporte),
            .Cliente = If(cert Is Nothing, String.Empty, cert.NombreCliente),
            .FechaSiniestro = s.FechaApertura,
            .NumeroSiniestro = "SIN-" & s.SiniestroId.ToString("00000"),
            .TipoSiniestro = s.NombreTipoSiniestro,
            .MontoReclamo = s.MontoDeReclamo
        }
    End Function

    Private Function Contiene(valor As String, busqueda As String) As Boolean
        If String.IsNullOrEmpty(valor) Then Return False

        Return Globalization.CultureInfo.InvariantCulture.CompareInfo.IndexOf(
            valor, busqueda,
            Globalization.CompareOptions.IgnoreCase Or Globalization.CompareOptions.IgnoreNonSpace) >= 0
    End Function

    Protected Sub txtBuscarSiniestro_TextChanged(sender As Object, e As EventArgs)
        gvSiniestros.PageIndex = 0

        CargarSiniestros()
    End Sub

    Protected Sub gvSiniestros_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        gvSiniestros.PageIndex = e.NewPageIndex

        CargarSiniestros()
    End Sub

#End Region

#Region "Cascada cliente / poliza / certificado"

    ''' <summary>
    ''' Cliente y póliza no se guardan: solo acotan la lista de certificados,
    ''' que es lo único que el siniestro referencia.
    ''' </summary>
    Private Sub CargarClientesConCertificado()

        ddlCliente.Items.Clear()
        ddlCliente.Items.Add(New ListItem("-- Selecciona --", ""))

        For Each nombre In Certificados().
            Select(Function(c) c.NombreCliente).
            Where(Function(n) Not String.IsNullOrWhiteSpace(n)).
            Distinct().
            OrderBy(Function(n) n)

            ddlCliente.Items.Add(New ListItem(nombre, nombre))
        Next
    End Sub

    Private Sub CargarPolizasDelCliente()

        ddlPolizaMaestra.Items.Clear()
        ddlPolizaMaestra.Items.Add(New ListItem("-- Selecciona --", ""))

        If ddlCliente.SelectedValue = "" Then Exit Sub

        For Each numero In Certificados().
            Where(Function(c) c.NombreCliente = ddlCliente.SelectedValue).
            Select(Function(c) c.NumeroPoliza).
            Where(Function(n) Not String.IsNullOrWhiteSpace(n)).
            Distinct().
            OrderBy(Function(n) n)

            ddlPolizaMaestra.Items.Add(New ListItem(numero, numero))
        Next
    End Sub

    Private Sub CargarCertificados()

        ddlNumeroCertificado.Items.Clear()
        ddlNumeroCertificado.Items.Add(New ListItem("-- Selecciona --", ""))

        If ddlCliente.SelectedValue = "" OrElse ddlPolizaMaestra.SelectedValue = "" Then Exit Sub

        For Each c In Certificados().
            Where(Function(x) x.NombreCliente = ddlCliente.SelectedValue AndAlso
                              x.NumeroPoliza = ddlPolizaMaestra.SelectedValue).
            OrderBy(Function(x) x.ClaveCertificado)

            Dim texto As String = If(String.IsNullOrWhiteSpace(c.ClaveCertificado),
                                     "Certificado " & c.CertificadoId,
                                     c.ClaveCertificado)

            ddlNumeroCertificado.Items.Add(New ListItem(texto, c.CertificadoId.ToString()))
        Next
    End Sub

    Protected Sub ddlCliente_SelectedIndexChanged(sender As Object, e As EventArgs)
        CargarPolizasDelCliente()
        CargarCertificados()
        MostrarSumaAsegurada()
    End Sub

    Protected Sub ddlPolizaMaestra_SelectedIndexChanged(sender As Object, e As EventArgs)
        CargarCertificados()
        MostrarSumaAsegurada()
    End Sub

    Protected Sub ddlNumeroCertificado_SelectedIndexChanged(sender As Object, e As EventArgs)
        MostrarSumaAsegurada()
    End Sub

    ''' <summary>
    ''' Muestra la suma asegurada del certificado elegido. El campo es de solo
    ''' lectura: el dato es del certificado, no se captura aquí.
    ''' </summary>
    Private Sub MostrarSumaAsegurada()

        Dim cert = CertificadoElegido()

        If cert Is Nothing Then
            txtSumaAsegurada.Text = String.Empty
            Exit Sub
        End If

        txtSumaAsegurada.Text = Importe(cert.SumaAsegurada)
    End Sub

    ''' <summary>
    ''' Suma asegurada que se guarda: se lee del certificado y no del TextBox.
    ''' Un TextBox con ReadOnly no manda su valor en el post, así que leerlo de
    ''' ahí sería frágil; además el dato es del certificado por definición.
    ''' </summary>
    Private Function SumaDelCertificado() As Decimal?

        Dim cert = CertificadoElegido()

        If cert Is Nothing Then Return Nothing

        Return cert.SumaAsegurada
    End Function

    ''' <summary>Certificado seleccionado en la cascada, o Nothing.</summary>
    Private Function CertificadoElegido() As Certificado

        Dim id As Integer

        If Not Integer.TryParse(ddlNumeroCertificado.SelectedValue, id) Then Return Nothing

        Return Certificados().FirstOrDefault(Function(c) c.CertificadoId = id)
    End Function

    ''' <summary>
    ''' Importe con signo de pesos y dos decimales. Al guardar, MontoDe quita el
    ''' símbolo y las comas, así que se puede mostrar con formato sin problema.
    ''' </summary>
    Private Function Importe(valor As Decimal?) As String

        If Not valor.HasValue Then Return String.Empty

        Return valor.Value.ToString("C2")
    End Function

    Private Sub CargarTiposSiniestro()

        ddlTipoSiniestro.Items.Clear()
        ddlTipoSiniestro.Items.Add(New ListItem("-- Selecciona --", ""))

        Dim api As New ConsumoApi()
        Dim json As String = api.GetTipoSiniestro()

        If String.IsNullOrWhiteSpace(json) OrElse
           json = "null" OrElse
           json.StartsWith("ERROR") Then

            Avisar("No se pudo cargar el catálogo de tipos de siniestro. ¿Ya se publicó el API?", "warning")
            Exit Sub
        End If

        Try
            For Each item As Newtonsoft.Json.Linq.JObject In Newtonsoft.Json.Linq.JArray.Parse(json)

                Dim id = item.GetValue("tipoSiniestroId", StringComparison.OrdinalIgnoreCase)
                Dim tipo = item.GetValue("tipo", StringComparison.OrdinalIgnoreCase)

                If id Is Nothing OrElse tipo Is Nothing Then Continue For

                ddlTipoSiniestro.Items.Add(New ListItem(tipo.ToString(), id.ToString()))
            Next
        Catch
            Avisar("El catálogo de tipos de siniestro no vino como se esperaba.", "warning")
        End Try
    End Sub

#End Region

#Region "Alta y edicion"

    Protected Sub btnAgregarSiniestro_Click(sender As Object, e As EventArgs)

        LimpiarFormulario()

        If Certificados().Count = 0 Then
            Avisar("No hay certificados registrados. Primero confirma una cotización para generar uno.", "warning")
            Exit Sub
        End If

        AbrirFormulario("Nuevo Registro")
    End Sub

    Private Sub AbrirFormulario(titulo As String)

        lblMensaje.Text = titulo

        pnlEncabezado.Visible = False
        PnlTabla.Visible = False
        pnlFormularioSiniestros.Visible = True
    End Sub

    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs)
        VolverAlListado()
    End Sub

    Private Sub VolverAlListado()

        pnlFormularioSiniestros.Visible = False
        pnlEncabezado.Visible = True
        PnlTabla.Visible = True

        CargarSiniestros()
    End Sub

    ''' <summary>
    ''' Deja el formulario como recién abierto: sin esto conserva en el ViewState
    ''' lo de la captura anterior.
    ''' </summary>
    Private Sub LimpiarFormulario()

        hfSiniestroId.Value = String.Empty

        txtFolio.Text = String.Empty
        txtFechaApertura.Text = Date.Today.ToString("yyyy-MM-dd")
        txtFechaCierre.Text = String.Empty
        txtMercancia.Text = String.Empty
        txtLugarSiniestro.Text = String.Empty
        txtMontoReclamo.Text = String.Empty
        txtMontoIndemnizacion.Text = String.Empty
        txtSumaAsegurada.Text = String.Empty

        CargarClientesConCertificado()
        CargarPolizasDelCliente()
        CargarCertificados()
        CargarTiposSiniestro()
    End Sub

    Private Sub EditarSiniestro(siniestroId As Integer)

        Dim api As New ConsumoApi()
        Dim json As String = api.GetSiniestroId(siniestroId)

        If String.IsNullOrWhiteSpace(json) OrElse json.StartsWith("ERROR") Then
            Avisar("No se pudo consultar el siniestro.", "danger")
            Exit Sub
        End If

        Dim s As Siniestro = JsonConvert.DeserializeObject(Of Siniestro)(json)

        If s Is Nothing Then
            Avisar("No se pudo consultar el siniestro.", "danger")
            Exit Sub
        End If

        LimpiarFormulario()

        hfSiniestroId.Value = s.SiniestroId.ToString()

        txtFolio.Text = s.NReporte
        txtFechaApertura.Text = s.FechaApertura.ToString("yyyy-MM-dd")
        txtFechaCierre.Text = If(s.FechaCierre.HasValue, s.FechaCierre.Value.ToString("yyyy-MM-dd"), String.Empty)
        txtMercancia.Text = s.Mercancia
        txtLugarSiniestro.Text = s.LugarDeSiniestro
        txtMontoReclamo.Text = Importe(s.MontoDeReclamo)
        txtMontoIndemnizacion.Text = Importe(s.MontoDeIndemnizacion)
        txtSumaAsegurada.Text = Importe(s.SumaAsegurada)

        ' La cascada se reconstruye desde el certificado guardado hacia arriba.
        Dim cert = Certificados().FirstOrDefault(Function(c) c.CertificadoId = s.CertificadoId)

        If cert IsNot Nothing Then
            SeleccionarValor(ddlCliente, cert.NombreCliente)
            CargarPolizasDelCliente()

            SeleccionarValor(ddlPolizaMaestra, cert.NumeroPoliza)
            CargarCertificados()

            SeleccionarValor(ddlNumeroCertificado, cert.CertificadoId.ToString())
        End If

        If s.TipoSiniestroId.HasValue Then
            SeleccionarValor(ddlTipoSiniestro, s.TipoSiniestroId.Value.ToString())
        End If

        AbrirFormulario("Editar siniestro")
    End Sub

    Private Sub SeleccionarValor(ddl As DropDownList, valor As String)

        ddl.ClearSelection()

        If String.IsNullOrEmpty(valor) Then Exit Sub

        Dim item = ddl.Items.FindByValue(valor)

        If item IsNot Nothing Then item.Selected = True
    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs)

        Dim certificadoId As Integer

        If Not Integer.TryParse(ddlNumeroCertificado.SelectedValue, certificadoId) Then
            Avisar("Elige el certificado al que corresponde el siniestro.", "warning")
            Exit Sub
        End If

        Dim apertura As Date

        If Not Date.TryParse(txtFechaApertura.Text, apertura) Then
            Avisar("Captura la fecha de apertura.", "warning")
            Exit Sub
        End If

        Dim cierre As Date?

        If Not String.IsNullOrWhiteSpace(txtFechaCierre.Text) Then
            Dim leida As Date

            If Not Date.TryParse(txtFechaCierre.Text, leida) Then
                Avisar("La fecha de cierre no es válida.", "warning")
                Exit Sub
            End If

            If leida < apertura Then
                Avisar("La fecha de cierre no puede ser anterior a la de apertura.", "warning")
                Exit Sub
            End If

            cierre = leida
        End If

        Dim siniestro As New Siniestro With {
            .CertificadoId = certificadoId,
            .NReporte = txtFolio.Text.Trim(),
            .FechaApertura = apertura,
            .FechaCierre = cierre,
            .TipoSiniestroId = IdSeleccionado(ddlTipoSiniestro),
            .Mercancia = txtMercancia.Text.Trim(),
            .LugarDeSiniestro = txtLugarSiniestro.Text.Trim(),
            .SumaAsegurada = SumaDelCertificado(),
            .MontoDeReclamo = MontoDe(txtMontoReclamo),
            .MontoDeIndemnizacion = MontoDe(txtMontoIndemnizacion)
        }

        Dim api As New ConsumoApi()
        Dim json As String = JsonConvert.SerializeObject(siniestro)

        Dim siniestroId As Integer
        Dim esEdicion As Boolean = Integer.TryParse(hfSiniestroId.Value, siniestroId)

        Dim respuesta As String

        If esEdicion Then
            respuesta = api.PutEditarSiniestro(siniestroId, json)
        Else
            respuesta = api.PostSiniestro(json)
        End If

        If String.IsNullOrWhiteSpace(respuesta) OrElse respuesta.StartsWith("ERROR") Then
            Avisar("No se pudo guardar: " & DetalleDeError(respuesta), "danger")
            Exit Sub
        End If

        Avisar(If(esEdicion, "Siniestro editado correctamente", "Siniestro agregado correctamente"), "success")

        hfSiniestroId.Value = String.Empty

        VolverAlListado()
    End Sub

#End Region

#Region "Acciones"

    Protected Sub gvSiniestros_RowCommand(sender As Object, e As GridViewCommandEventArgs)

        Dim siniestroId As Integer
        If Not Integer.TryParse(Convert.ToString(e.CommandArgument), siniestroId) Then Exit Sub

        Select Case e.CommandName
            Case "Editar"
                EditarSiniestro(siniestroId)
            Case "Eliminar"
                EliminarSiniestro(siniestroId)
        End Select
    End Sub

    Private Sub EliminarSiniestro(siniestroId As Integer)

        Dim api As New ConsumoApi()
        Dim respuesta As String = api.DeleteSiniestro(siniestroId)

        If String.IsNullOrWhiteSpace(respuesta) OrElse respuesta.StartsWith("ERROR") Then
            Avisar("No se pudo eliminar: " & DetalleDeError(respuesta), "danger")
            Exit Sub
        End If

        Avisar("Siniestro eliminado correctamente", "success")

        CargarSiniestros()
    End Sub

#End Region

#Region "Utilerias"

    Private Function IdSeleccionado(ddl As DropDownList) As Integer?

        Dim id As Integer

        If Not Integer.TryParse(ddl.SelectedValue, id) OrElse id = 0 Then Return Nothing

        Return id
    End Function

    Private Function MontoDe(txt As TextBox) As Decimal?

        If txt Is Nothing OrElse String.IsNullOrWhiteSpace(txt.Text) Then Return Nothing

        Dim limpio As String = Text.RegularExpressions.Regex.Replace(txt.Text, "[^0-9.\-]", "")

        Dim valor As Decimal

        If Not Decimal.TryParse(limpio, valor) Then Return Nothing

        Return valor
    End Function

    ''' <summary>
    ''' Saca el motivo de un "ERROR: ..." sin dar por hecho el largo del prefijo.
    ''' </summary>
    Private Function DetalleDeError(respuesta As String) As String

        If String.IsNullOrWhiteSpace(respuesta) Then Return "sin respuesta del servidor"

        Dim separador As Integer = respuesta.IndexOf(":"c)

        If separador < 0 Then Return respuesta.Trim()

        Return respuesta.Substring(separador + 1).Trim()
    End Function

    Private Sub Avisar(mensaje As String, tipo As String)
        lblAviso.Text = mensaje
        pnlAviso.CssClass = "alert alert-" & tipo
        pnlAviso.Visible = True
    End Sub

#End Region

End Class
