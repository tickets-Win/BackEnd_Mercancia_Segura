Imports Newtonsoft.Json
Imports WebAdmin.MercanciaSegura.DOM.Modelos

Public Class AdminEndosos
    Inherits System.Web.UI.Page

    ''' <summary>
    ''' Fila del listado. El endoso no guarda vigencia propia: "Desde" y "Hasta"
    ''' salen del certificado al que apunta.
    ''' </summary>
    Public Class RenglonEndoso
        Public Property EndosoId As Integer
        Public Property Endoso As String
        Public Property Tipo As String
        Public Property Desde As DateTime?
        Public Property Hasta As DateTime?
        Public Property Concepto As String
    End Class

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ' El aviso se apaga al empezar cada petición: Page_Load corre antes que
        ' los eventos, así que un Avisar() de este mismo clic sí se ve.
        pnlAviso.Visible = False

        If Not IsPostBack Then
            CargarEndosos()
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

    Protected Sub CargarEndosos()

        Dim api As New ConsumoApi()

        Dim lista = Deserializar(Of Endoso)(api.GetEndosos())

        If lista Is Nothing Then
            Avisar("No se pudieron cargar los endosos.", "danger")
            lista = New List(Of Endoso)
        End If

        Dim renglones = lista.Select(Function(en) ArmarRenglon(en)).ToList()

        Dim busqueda As String = txtBuscarEndoso.Text.Trim()

        If busqueda.Length > 0 Then
            renglones = renglones.Where(Function(r)
                                            Return Contiene(r.Endoso, busqueda) OrElse
                                                   Contiene(r.Tipo, busqueda) OrElse
                                                   Contiene(r.Concepto, busqueda)
                                        End Function).ToList()
        End If

        Dim ultimaPagina As Integer = 0

        If renglones.Count > 0 Then
            ultimaPagina = CInt(Math.Ceiling(renglones.Count / CDbl(gvEndosos.PageSize))) - 1
        End If

        If gvEndosos.PageIndex > ultimaPagina Then gvEndosos.PageIndex = ultimaPagina

        gvEndosos.DataSource = renglones
        gvEndosos.DataBind()
    End Sub

    Private Function ArmarRenglon(en As Endoso) As RenglonEndoso

        Dim cert = Certificados().FirstOrDefault(Function(c) c.CertificadoId = en.CertificadoId)

        Return New RenglonEndoso With {
            .EndosoId = en.EndosoId,
            .Endoso = If(String.IsNullOrWhiteSpace(en.NumeroEndoso), "(sin número)", en.NumeroEndoso),
            .Tipo = en.NombreTipoEndoso,
            .Desde = If(en.VigenciaDel.HasValue, en.VigenciaDel,
                        If(cert Is Nothing, Nothing, CType(cert.FechaInicio, DateTime?))),
            .Hasta = If(en.VigenciaHasta.HasValue, en.VigenciaHasta,
                        If(cert Is Nothing, Nothing, CType(cert.FechaFin, DateTime?))),
            .Concepto = en.Descripcion
        }
    End Function

    Private Function Contiene(valor As String, busqueda As String) As Boolean
        If String.IsNullOrEmpty(valor) Then Return False

        Return Globalization.CultureInfo.InvariantCulture.CompareInfo.IndexOf(
            valor, busqueda,
            Globalization.CompareOptions.IgnoreCase Or Globalization.CompareOptions.IgnoreNonSpace) >= 0
    End Function

    Protected Sub txtBuscarEndoso_TextChanged(sender As Object, e As EventArgs)
        gvEndosos.PageIndex = 0

        CargarEndosos()
    End Sub

    Protected Sub gvEndosos_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        gvEndosos.PageIndex = e.NewPageIndex

        CargarEndosos()
    End Sub

#End Region

#Region "Catalogos y cascada"

    Private Sub CargarTiposEndoso()

        ddlTipoEndoso.Items.Clear()
        ddlTipoEndoso.Items.Add(New ListItem("-- Selecciona --", ""))

        Dim api As New ConsumoApi()
        Dim lista = Deserializar(Of TipoEndoso)(api.GetTipoEndoso())

        If lista Is Nothing Then
            Avisar("No se pudo cargar el catálogo de tipos de endoso. ¿Ya se publicó el API?", "warning")
            Exit Sub
        End If

        For Each t In lista
            ddlTipoEndoso.Items.Add(New ListItem(t.Tipo, t.TipoEndosoId.ToString()))
        Next
    End Sub

    Private Sub CargarMonedas()

        ddlMoneda.Items.Clear()
        ddlMoneda.Items.Add(New ListItem("-- Selecciona --", ""))

        Dim api As New ConsumoApi()
        Dim lista = Deserializar(Of Moneda)(api.GetMoneda())

        If lista Is Nothing Then Exit Sub

        For Each m In lista
            ddlMoneda.Items.Add(New ListItem(m.Nombre, m.MonedaId.ToString()))
        Next
    End Sub

    ''' <summary>
    ''' El beneficiario se guarda como texto en la tabla, pero se elige del
    ''' catálogo para que no se capture a mano con variantes.
    ''' </summary>
    Private Sub CargarBeneficiarios()

        ddlBeneficiarioPreferente.Items.Clear()
        ddlBeneficiarioPreferente.Items.Add(New ListItem("-- Selecciona --", ""))

        Dim api As New ConsumoApi()
        Dim lista = Deserializar(Of BeneficiarioPreferente)(api.GetCargarBeneficiarios())

        If lista Is Nothing Then Exit Sub

        For Each b In lista
            If String.IsNullOrWhiteSpace(b.NombreCompleto) Then Continue For

            ddlBeneficiarioPreferente.Items.Add(New ListItem(b.NombreCompleto, b.NombreCompleto))
        Next
    End Sub

    ''' <summary>
    ''' Cliente y póliza no se guardan: solo acotan la lista de certificados,
    ''' que es lo único que el endoso referencia.
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

        ddlPoliza.Items.Clear()
        ddlPoliza.Items.Add(New ListItem("-- Selecciona --", ""))

        If ddlCliente.SelectedValue = "" Then Exit Sub

        For Each numero In Certificados().
            Where(Function(c) c.NombreCliente = ddlCliente.SelectedValue).
            Select(Function(c) c.NumeroPoliza).
            Where(Function(n) Not String.IsNullOrWhiteSpace(n)).
            Distinct().
            OrderBy(Function(n) n)

            ddlPoliza.Items.Add(New ListItem(numero, numero))
        Next
    End Sub

    Private Sub CargarCertificados()

        ddlCertificado.Items.Clear()
        ddlCertificado.Items.Add(New ListItem("-- Selecciona --", ""))

        If ddlCliente.SelectedValue = "" OrElse ddlPoliza.SelectedValue = "" Then Exit Sub

        For Each c In Certificados().
            Where(Function(x) x.NombreCliente = ddlCliente.SelectedValue AndAlso
                              x.NumeroPoliza = ddlPoliza.SelectedValue).
            OrderBy(Function(x) x.ClaveCertificado)

            Dim texto As String = If(String.IsNullOrWhiteSpace(c.ClaveCertificado),
                                     "Certificado " & c.CertificadoId,
                                     c.ClaveCertificado)

            ddlCertificado.Items.Add(New ListItem(texto, c.CertificadoId.ToString()))
        Next
    End Sub

    Protected Sub ddlCliente_SelectedIndexChanged(sender As Object, e As EventArgs)
        CargarPolizasDelCliente()
        CargarCertificados()
    End Sub

    Protected Sub ddlPoliza_SelectedIndexChanged(sender As Object, e As EventArgs)
        CargarCertificados()
    End Sub

#End Region

#Region "Alta y edicion"

    Protected Sub btnAgregarEndoso_Click(sender As Object, e As EventArgs)

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
        pnlFormularioEndosos.Visible = True
    End Sub

    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs)
        VolverAlListado()
    End Sub

    Private Sub VolverAlListado()

        pnlFormularioEndosos.Visible = False
        pnlEncabezado.Visible = True
        PnlTabla.Visible = True

        CargarEndosos()
    End Sub

    ''' <summary>
    ''' Deja el formulario como recién abierto: sin esto conserva en el ViewState
    ''' lo de la captura anterior.
    ''' </summary>
    Private Sub LimpiarFormulario()

        hfEndosoId.Value = String.Empty

        txtNumeroEndoso.Text = String.Empty
        txtFechaElaboracion.Text = Date.Today.ToString("yyyy-MM-dd")
        txtAgente.Text = String.Empty
        txtRFC.Text = String.Empty
        txtOficina.Text = String.Empty
        txtVigenciaDel.Text = String.Empty
        txtVigenciaHasta.Text = String.Empty
        txtSumaAsegurada.Text = String.Empty
        txtPrima.Text = String.Empty
        txtIVA.Text = String.Empty
        txtTotalPagar.Text = String.Empty
        txtObservaciones.Text = String.Empty

        CargarTiposEndoso()
        CargarMonedas()
        CargarBeneficiarios()
        CargarClientesConCertificado()
        CargarPolizasDelCliente()
        CargarCertificados()
    End Sub

    Private Sub EditarEndoso(endosoId As Integer)

        Dim api As New ConsumoApi()
        Dim json As String = api.GetEndosoId(endosoId)

        If String.IsNullOrWhiteSpace(json) OrElse json.StartsWith("ERROR") Then
            Avisar("No se pudo consultar el endoso.", "danger")
            Exit Sub
        End If

        Dim en As Endoso = JsonConvert.DeserializeObject(Of Endoso)(json)

        If en Is Nothing Then
            Avisar("No se pudo consultar el endoso.", "danger")
            Exit Sub
        End If

        LimpiarFormulario()

        hfEndosoId.Value = en.EndosoId.ToString()

        txtNumeroEndoso.Text = en.NumeroEndoso
        txtFechaElaboracion.Text = en.FechaElaboracion.ToString("yyyy-MM-dd")
        txtAgente.Text = en.Agente
        txtRFC.Text = en.RFC
        txtOficina.Text = en.Oficina
        txtVigenciaDel.Text = If(en.VigenciaDel.HasValue, en.VigenciaDel.Value.ToString("yyyy-MM-dd"), String.Empty)
        txtVigenciaHasta.Text = If(en.VigenciaHasta.HasValue, en.VigenciaHasta.Value.ToString("yyyy-MM-dd"), String.Empty)
        txtSumaAsegurada.Text = Importe(en.SumaAsegurada)
        txtPrima.Text = Importe(en.PrimaServicioDeAseguramiento)
        txtIVA.Text = Importe(en.IVA)
        txtTotalPagar.Text = Importe(en.TotalAPagar)
        txtObservaciones.Text = en.Descripcion

        SeleccionarValor(ddlTipoEndoso, en.TipoEndosoId.ToString())
        SeleccionarValor(ddlBeneficiarioPreferente, en.BeneficiarioPreferente)

        If en.MonedaId.HasValue Then SeleccionarValor(ddlMoneda, en.MonedaId.Value.ToString())

        ' La cascada se reconstruye desde el certificado guardado hacia arriba.
        Dim cert = Certificados().FirstOrDefault(Function(c) c.CertificadoId = en.CertificadoId)

        If cert IsNot Nothing Then
            SeleccionarValor(ddlCliente, cert.NombreCliente)
            CargarPolizasDelCliente()

            SeleccionarValor(ddlPoliza, cert.NumeroPoliza)
            CargarCertificados()

            SeleccionarValor(ddlCertificado, cert.CertificadoId.ToString())
        End If

        AbrirFormulario("Editar endoso")
    End Sub

    Private Sub SeleccionarValor(ddl As DropDownList, valor As String)

        ddl.ClearSelection()

        If String.IsNullOrEmpty(valor) Then Exit Sub

        Dim item = ddl.Items.FindByValue(valor)

        If item IsNot Nothing Then item.Selected = True
    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs)

        Dim tipoId As Integer

        If Not Integer.TryParse(ddlTipoEndoso.SelectedValue, tipoId) Then
            Avisar("Elige el tipo de endoso.", "warning")
            Exit Sub
        End If

        Dim certificadoId As Integer

        If Not Integer.TryParse(ddlCertificado.SelectedValue, certificadoId) Then
            Avisar("Elige el certificado al que corresponde el endoso.", "warning")
            Exit Sub
        End If

        Dim elaboracion As Date

        If Not Date.TryParse(txtFechaElaboracion.Text, elaboracion) Then
            Avisar("Captura la fecha de elaboración.", "warning")
            Exit Sub
        End If

        Dim desde = Convertir.Fecha(txtVigenciaDel.Text)
        Dim hasta = Convertir.Fecha(txtVigenciaHasta.Text)

        ' La vigencia es opcional: solo la llevan los endosos que la cambian.
        If desde.HasValue AndAlso hasta.HasValue AndAlso hasta.Value < desde.Value Then
            Avisar("La nueva vigencia final no puede ser anterior a la inicial.", "warning")
            Exit Sub
        End If

        Dim endoso As New Endoso With {
            .TipoEndosoId = tipoId,
            .NumeroEndoso = txtNumeroEndoso.Text.Trim(),
            .CertificadoId = certificadoId,
            .FechaElaboracion = elaboracion,
            .Agente = txtAgente.Text.Trim(),
            .RFC = txtRFC.Text.Trim(),
            .Oficina = txtOficina.Text.Trim(),
            .BeneficiarioPreferente = ddlBeneficiarioPreferente.SelectedValue,
            .MonedaId = IdSeleccionado(ddlMoneda),
            .VigenciaDel = Convertir.Fecha(txtVigenciaDel.Text),
            .VigenciaHasta = Convertir.Fecha(txtVigenciaHasta.Text),
            .SumaAsegurada = Convertir.Numero(txtSumaAsegurada.Text),
            .PrimaServicioDeAseguramiento = Convertir.Numero(txtPrima.Text),
            .IVA = IvaDeLaPrima(),
            .TotalAPagar = TotalDeLaPrima(),
            .Descripcion = txtObservaciones.Text.Trim()
        }

        Dim api As New ConsumoApi()
        Dim json As String = JsonConvert.SerializeObject(endoso)

        Dim endosoId As Integer
        Dim esEdicion As Boolean = Integer.TryParse(hfEndosoId.Value, endosoId)

        Dim respuesta As String

        If esEdicion Then
            respuesta = api.PutEditarEndoso(endosoId, json)
        Else
            respuesta = api.PostEndoso(json)
        End If

        If String.IsNullOrWhiteSpace(respuesta) OrElse respuesta.StartsWith("ERROR") Then
            Avisar("No se pudo guardar: " & DetalleDeError(respuesta), "danger")
            Exit Sub
        End If

        Avisar(If(esEdicion, "Endoso editado correctamente", "Endoso agregado correctamente"), "success")

        hfEndosoId.Value = String.Empty

        VolverAlListado()
    End Sub

#End Region

#Region "Acciones"

    Protected Sub gvEndosos_RowCommand(sender As Object, e As GridViewCommandEventArgs)

        Dim endosoId As Integer
        If Not Integer.TryParse(Convert.ToString(e.CommandArgument), endosoId) Then Exit Sub

        Select Case e.CommandName
            Case "Editar"
                EditarEndoso(endosoId)
            Case "Eliminar"
                EliminarEndoso(endosoId)
        End Select
    End Sub

    Private Sub EliminarEndoso(endosoId As Integer)

        Dim api As New ConsumoApi()
        Dim respuesta As String = api.DeleteEndoso(endosoId)

        If String.IsNullOrWhiteSpace(respuesta) OrElse respuesta.StartsWith("ERROR") Then
            Avisar("No se pudo eliminar: " & DetalleDeError(respuesta), "danger")
            Exit Sub
        End If

        Avisar("Endoso eliminado correctamente", "success")

        CargarEndosos()
    End Sub

#End Region

#Region "Utilerias"

    Private Function IdSeleccionado(ddl As DropDownList) As Integer?

        Dim id As Integer

        If Not Integer.TryParse(ddl.SelectedValue, id) OrElse id = 0 Then Return Nothing

        Return id
    End Function

    Private Function Importe(valor As Decimal?) As String

        If Not valor.HasValue Then Return String.Empty

        Return valor.Value.ToString("C2")
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


    ''' <summary>
    ''' Misma regla que en cotizaciones: IVA sobre la prima y total = prima + IVA.
    ''' Se calcula en un solo lugar para que los importes no se contradigan entre
    ''' módulos. Los campos van de sólo lectura, así que se leen de la prima.
    ''' </summary>
    Private Const IvaPorcentaje As Decimal = 0.16D

    Protected Sub txtPrima_TextChanged(sender As Object, e As EventArgs)
        CalcularImportes()
    End Sub

    Private Sub CalcularImportes()

        Dim prima = Convertir.Numero(txtPrima.Text)

        If Not prima.HasValue Then
            txtIVA.Text = String.Empty
            txtTotalPagar.Text = String.Empty
            Exit Sub
        End If

        Dim iva As Decimal = prima.Value * IvaPorcentaje

        txtPrima.Text = Importe(prima)
        txtIVA.Text = Importe(iva)
        txtTotalPagar.Text = Importe(prima.Value + iva)
    End Sub

    ''' <summary>
    ''' IVA que se guarda. Un TextBox de sólo lectura no manda su valor en el
    ''' post, así que se recalcula de la prima en vez de leerlo de la pantalla.
    ''' </summary>
    Private Function IvaDeLaPrima() As Decimal?

        Dim prima = Convertir.Numero(txtPrima.Text)

        If Not prima.HasValue Then Return Nothing

        Return prima.Value * IvaPorcentaje
    End Function

    Private Function TotalDeLaPrima() As Decimal?

        Dim prima = Convertir.Numero(txtPrima.Text)

        If Not prima.HasValue Then Return Nothing

        Return prima.Value + (prima.Value * IvaPorcentaje)
    End Function

End Class
