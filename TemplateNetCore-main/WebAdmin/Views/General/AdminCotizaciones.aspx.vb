Imports Newtonsoft.Json
Imports WebAdmin.MercanciaSegura.DOM.Modelos
Imports System.Web.UI.HtmlControls

Public Class AdminCotizaciones
    Inherits System.Web.UI.Page

    ''' <summary>
    ''' Debe coincidir con el MS_IVA del JavaScript de la vista.
    ''' </summary>
    Private Const IVA_PORCENTAJE As Decimal = 0.16D

    Private ReadOnly Property CoberturasCotizacion As List(Of Cobertura)
        Get
            If Session("CoberturasCotizacion") Is Nothing Then
                Session("CoberturasCotizacion") = New List(Of Cobertura)()
            End If
            Return CType(Session("CoberturasCotizacion"), List(Of Cobertura))
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            pnlMercancia.Visible = False
            CargarCotizaciones()
            cargarClientes()
            CargarPolizas(ddlTipoCotizacion.SelectedValue)
            CargarTipoMoneda(ddlMoneda)
            CargarClasificacion(ddlClasificacion)
            CargarTransito(ddlTransito)

            LimpiarBeneficiarios()

            CoberturasCotizacion.Clear()
            CargarGridCoberturas()

            BienesCotizacion.Clear()
            CargarGridBienes()
        End If
    End Sub

    ''' <summary>
    ''' Bienes asegurados que el usuario va agregando a la cotización de
    ''' contenedor. Viven en Session hasta que se guarde.
    ''' </summary>
    Private ReadOnly Property BienesCotizacion As List(Of Bien)
        Get
            If Session("BienesCotizacion") Is Nothing Then
                Session("BienesCotizacion") = New List(Of Bien)()
            End If
            Return CType(Session("BienesCotizacion"), List(Of Bien))
        End Get
    End Property

    Protected Sub btnbienesasegurados_Click(sender As Object, e As EventArgs)
        Dim item As ListItem = ddlbienesasegurados.SelectedItem

        If item Is Nothing OrElse item.Value = "0" Then Exit Sub

        Dim bienId As Integer
        If Not Integer.TryParse(item.Value, bienId) Then Exit Sub

        ' No agregar dos veces el mismo.
        If BienesCotizacion.Any(Function(b) b.BienId = bienId) Then Exit Sub

        BienesCotizacion.Add(New Bien With {
            .BienId = bienId,
            .Nombre = item.Text
        })

        CargarGridBienes()
    End Sub

    Protected Sub GvBienesCotizacion_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        If e.CommandName <> "Eliminar" Then Exit Sub

        Dim bienId As Integer
        If Not Integer.TryParse(Convert.ToString(e.CommandArgument), bienId) Then Exit Sub

        BienesCotizacion.RemoveAll(Function(b) b.BienId = bienId)

        CargarGridBienes()
    End Sub

    Protected Sub GvBienesCotizacion_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        GvBienesCotizacion.PageIndex = e.NewPageIndex
        CargarGridBienes()
    End Sub

    Private Sub CargarGridBienes()
        Dim ultimaPagina As Integer = 0

        If BienesCotizacion.Count > 0 Then
            ultimaPagina = CInt(Math.Ceiling(BienesCotizacion.Count / CDbl(GvBienesCotizacion.PageSize))) - 1
        End If

        If GvBienesCotizacion.PageIndex > ultimaPagina Then
            GvBienesCotizacion.PageIndex = ultimaPagina
        End If

        GvBienesCotizacion.DataSource = BienesCotizacion
        GvBienesCotizacion.DataBind()
    End Sub

    Protected Sub btnAgregarCobertura_Click(sender As Object, e As EventArgs)
        Dim item As ListItem = ddlCoberturas.SelectedItem

        If item Is Nothing OrElse item.Value = "0" Then Exit Sub

        Dim coberturaId As Integer
        If Not Integer.TryParse(item.Value, coberturaId) Then Exit Sub

        If CoberturasCotizacion.Any(Function(c) c.CoberturaId = coberturaId) Then Exit Sub

        CoberturasCotizacion.Add(New Cobertura With {
            .CoberturaId = coberturaId,
            .Nombre = item.Text
        })

        CargarGridCoberturas()
    End Sub

    Protected Sub GvCoberturasCotizacion_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        If e.CommandName <> "Eliminar" Then Exit Sub

        Dim coberturaId As Integer
        If Not Integer.TryParse(Convert.ToString(e.CommandArgument), coberturaId) Then Exit Sub

        CoberturasCotizacion.RemoveAll(Function(c) c.CoberturaId = coberturaId)

        CargarGridCoberturas()
    End Sub

    Protected Sub GvCoberturasCotizacion_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        GvCoberturasCotizacion.PageIndex = e.NewPageIndex
        CargarGridCoberturas()
    End Sub

    Private Sub CargarGridCoberturas()
        Dim ultimaPagina As Integer = 0

        If CoberturasCotizacion.Count > 0 Then
            ultimaPagina = CInt(Math.Ceiling(CoberturasCotizacion.Count / CDbl(GvCoberturasCotizacion.PageSize))) - 1
        End If

        If GvCoberturasCotizacion.PageIndex > ultimaPagina Then
            GvCoberturasCotizacion.PageIndex = ultimaPagina
        End If

        GvCoberturasCotizacion.DataSource = CoberturasCotizacion
        GvCoberturasCotizacion.DataBind()
    End Sub

    Protected Sub btnAgregarCotizacion_Click(sender As Object, e As EventArgs)
        ' Sin limpiar, el formulario conserva en el ViewState lo de la captura
        ' o la edición anterior.
        LimpiarFormulario()

        AbrirFormulario("Nuevo Registro")
    End Sub

    ''' <summary>
    ''' Deja el formulario como recién abierto. Se llama al dar de alta una
    ''' cotización nueva y al cerrar el formulario.
    ''' </summary>
    Private Sub LimpiarFormulario()

        ' Sin id = alta nueva.
        hfCotizacionId.Value = String.Empty

        ' Datos de la cotización
        ResetearCombo(ddlMoneda)
        ResetearCombo(ddlCliente)
        ddlBeneficiarioPreferente.Items.Clear()
        txtFechaCotizacion.Text = String.Empty
        txtVigenciaDel.Text = String.Empty
        txtVigenciaHasta.Text = String.Empty
        txtSumaAsegurada.Text = String.Empty

        ' Detalle de mercancía
        txtSubRamo.Text = String.Empty
        ResetearCombo(ddlTransito)
        ResetearCombo(ddlClasificacion)
        ddlSubclasificación.Items.Clear()
        txtDescripcionMercancia.Text = String.Empty
        txtTipoEmpaque.Text = String.Empty
        txtOrigen.Text = String.Empty
        txtDestino.Text = String.Empty
        txtMediosConduccion.Text = String.Empty
        txtMedioTransporte.Text = String.Empty
        txtObservaciones.Text = String.Empty

        ' Coberturas y bienes: los combos dependen de la póliza y sus tablas viven
        ' en Session.
        ddlCoberturas.Items.Clear()
        CoberturasCotizacion.Clear()
        CargarGridCoberturas()

        ddlbienesasegurados.Items.Clear()
        BienesCotizacion.Clear()
        CargarGridBienes()

        ' Medidas de seguridad
        txtMedidasSeguridad.Text = String.Empty
        txtDeducibles.Text = String.Empty

        ' Cuota aplicable / mínima
        chkCuotaAplicableN.Checked = False
        chkCuotaAplicableI.Checked = False
        txtCuotaAplicable.Text = String.Empty
        chkCuotaMinimaN.Checked = False
        chkCuotaMinimaI.Checked = False
        txtCuotaMinima.Text = String.Empty
        txtTipoCambio.Text = String.Empty
        ResetearCombo(ddlMonedaCotizar)

        ' Prima y servicios (mercancía)
        txtPrimaYSeguramiento.Text = String.Empty
        txtGastosExpedicion.Text = String.Empty
        txtSubtotal.Text = String.Empty
        txtIVA.Text = String.Empty
        txtTotalPagar.Text = String.Empty

        ' Bloque de contenedor
        ResetearCombo(ddlUnidades)
        txtCuotaSecos.Text = String.Empty
        ResetearCombo(ddlTipoTarifaSecos)
        txtCuotaRefrigerados.Text = String.Empty
        ResetearCombo(ddlTipoRefrigerados)
        txtCuota2.Text = String.Empty
        ResetearCombo(ddlTipoIsotaques)
        txtPrimaYSeguramiento2.Text = String.Empty
        txtGastosExpedicion2.Text = String.Empty
        txtSubtotal2.Text = String.Empty
        txtIVA2.Text = String.Empty
        txtTotalPagar2.Text = String.Empty

        ' Al final: regresa el tipo a Mercancía y recarga el combo de pólizas,
        ' que puede haber quedado con las de contenedor.
        ResetearCombo(ddlTipoCotizacion)
        AplicarTipoCotizacion(ddlTipoCotizacion.SelectedValue)
    End Sub

    Private Sub ResetearCombo(ddl As DropDownList)
        ddl.ClearSelection()
        If ddl.Items.Count > 0 Then ddl.Items(0).Selected = True
    End Sub

    Private Sub AbrirFormulario(titulo As String)
        pnlFormularioCotizaciones.Visible = True
        pnlMercancia.Visible = True
        pnlEncabezado.Visible = False
        PnlTabla.Visible = False
        lblMensaje.Text = titulo
    End Sub
    Protected Sub ddlTipoCotizacion_SelectedIndexChanged(sender As Object, e As EventArgs)
        AplicarTipoCotizacion(ddlTipoCotizacion.SelectedValue)
    End Sub

    Private Sub AplicarTipoCotizacion(tipo As String)
        If tipo = "Mercancia" Then
            pnlMercanciaFormulario.Visible = True
            pnlCuotaAplicableMercancia.Visible = True
            pnlBienesAsegurados.Visible = False
            pnlCuotaAplicableContenedor.Visible = False
            pnlMedidasSeguridad.Visible = True
            pnlSumaAsegurada.Visible = True

            CargarPolizas("Mercancia")

        ElseIf tipo = "Contenedor" Then
            pnlMercanciaFormulario.Visible = False
            pnlCuotaAplicableMercancia.Visible = False
            pnlBienesAsegurados.Visible = True
            pnlCuotaAplicableContenedor.Visible = True
            pnlMedidasSeguridad.Visible = False
            pnlSumaAsegurada.Visible = False

            CargarPolizas("Contenedor")

        End If
    End Sub

    Protected Sub gvCotizaciones_RowCommand(sender As Object, e As GridViewCommandEventArgs)

        Dim cotizacionId As Integer
        If Not Integer.TryParse(Convert.ToString(e.CommandArgument), cotizacionId) Then Exit Sub

        Select Case e.CommandName
            Case "Editar"
                EditarCotizacion(cotizacionId)
            Case "Eliminar"
                CancelarCotizacion(cotizacionId)
        End Select
    End Sub

    ''' <summary>
    ''' Baja lógica. El API marca la FechaCancelacion y deja de incluirla en el
    ''' listado; el registro sigue existiendo en la base.
    ''' </summary>
    Private Sub CancelarCotizacion(cotizacionId As Integer)

        Dim api As New ConsumoApi()
        Dim respuesta As String = api.DeleteCotizacion(cotizacionId)

        If String.IsNullOrWhiteSpace(respuesta) OrElse respuesta.StartsWith("ERROR") Then
            Dim detalle As String = If(String.IsNullOrWhiteSpace(respuesta), "sin respuesta del servidor", respuesta.Substring(6).Trim())
            Avisar("No se pudo cancelar: " & detalle, "danger")
            Exit Sub
        End If

        Avisar("Cotización cancelada correctamente", "success")

        ' Si se estaba editando justo esa, se sale del modo edición.
        If hfCotizacionId.Value = cotizacionId.ToString() Then
            hfCotizacionId.Value = String.Empty
        End If

        CargarCotizaciones()
    End Sub

    ''' <summary>
    ''' Trae la cotización del API y abre el formulario con sus datos.
    ''' </summary>
    Private Sub EditarCotizacion(cotizacionId As Integer)

        Dim api As New ConsumoApi()
        Dim json As String = api.GetCotizacionId(cotizacionId)

        If String.IsNullOrWhiteSpace(json) OrElse json.StartsWith("ERROR") Then
            Avisar("No se pudo consultar la cotización.", "danger")
            Exit Sub
        End If

        Dim cotizacion As Cotizacion = Nothing

        Try
            cotizacion = JsonConvert.DeserializeObject(Of Cotizacion)(json)
        Catch ex As Exception
            cotizacion = Nothing
        End Try

        If cotizacion Is Nothing Then
            Avisar("No se encontró la cotización.", "danger")
            Exit Sub
        End If

        ' Limpia primero: hay campos que no viven en el modelo (condiciones
        ' especiales, exclusiones, el bloque de contenedor) y se quedarían con
        ' los valores de la captura anterior.
        LimpiarFormulario()

        hfCotizacionId.Value = cotizacion.CotizacionId.ToString()

        AbrirFormulario("Editar Cotización")

        LlenarDesdeCotizacion(cotizacion)
    End Sub

    Private Sub LlenarDesdeCotizacion(c As Cotizacion)

        ' El combo usa value compuesto "polizaId|detalleId" pero la cotización solo
        ' guarda el PolizaId, así que se toma la primera opción de esa póliza.
        Dim detalleId As Integer = SeleccionarPolizaPorId(c.PolizaId)

        ' Esto llena los combos que dependen de la póliza (subclasificación y
        ' coberturas) y de paso escribe moneda, vigencias y deducibles. Los valores
        ' propios de la cotización se sobreescriben abajo.
        CargarDesdePolizaId(c.PolizaId, detalleId)

        SeleccionarValor(ddlCliente, c.ClienteId)
        CargarBeneficiariosDeCliente(c.ClienteId)
        SeleccionarValor(ddlBeneficiarioPreferente, c.BeneficiarioPreferenteId)

        SeleccionarValor(ddlMoneda, c.MonedaId)

        txtFechaCotizacion.Text = TextoFecha(c.FechaCotizacion)
        txtVigenciaDel.Text = TextoFecha(c.VigenciaDel)
        txtVigenciaHasta.Text = TextoFecha(c.VigenciaHasta)

        txtPrimaYSeguramiento.Text = TextoMonto(c.PrimaServicioDeAseguramiento)
        txtGastosExpedicion.Text = TextoMonto(c.GastosExpedicion)
        txtSubtotal.Text = TextoMonto(c.Subtotal)
        txtIVA.Text = TextoMonto(c.IVA)
        txtTotalPagar.Text = TextoMonto(c.Total)

        Dim m As CotizacionMercancia = c.CotizacionMercancia

        If m Is Nothing Then Exit Sub

        txtSubRamo.Text = m.CotizacionCliente
        SeleccionarValor(ddlTransito, m.TransitoId)
        SeleccionarValor(ddlClasificacion, m.ClasificacionId)
        SeleccionarTexto(ddlSubclasificación, m.SubClasificacion)
        txtDescripcionMercancia.Text = m.DescripcionMercancia
        txtTipoEmpaque.Text = m.TipoEmpaque
        txtOrigen.Text = m.Origen
        txtDestino.Text = m.Destino
        txtMediosConduccion.Text = m.MedioDeConduccion
        txtMedioTransporte.Text = m.MedioDeTransporte
        txtObservaciones.Text = m.Observaciones
        txtMedidasSeguridad.Text = m.MedidasDeSeguridadAdicionales
        txtDeducibles.Text = m.Deducibles
        txtSumaAsegurada.Text = TextoMonto(m.SumaAsegurada)
        txtCuotaAplicable.Text = TextoMonto(m.CuotaAplicable)
        txtCuotaMinima.Text = TextoMonto(m.CuotaMinima)
        txtTipoCambio.Text = TextoMonto(m.TipoCambioCotizar)

        MarcarChecksMoneda(chkCuotaAplicableN, chkCuotaAplicableI, m.MonedaCuotaAplicableId)
        MarcarChecksMoneda(chkCuotaMinimaN, chkCuotaMinimaI, m.MonedaCuotaMinimaId)
    End Sub

    ''' <summary>
    ''' Selecciona en el combo la primera opción cuyo value empiece con el PolizaId.
    ''' Devuelve el id de detalle (mercancía o contenedor) de esa opción.
    ''' </summary>
    Private Function SeleccionarPolizaPorId(polizaId As Integer) As Integer

        ddlNombreInternoPoliza.ClearSelection()

        For Each item As ListItem In ddlNombreInternoPoliza.Items
            Dim pid As Integer
            Dim did As Integer
            PartesSeleccion(item.Value, pid, did)

            If pid = polizaId AndAlso pid <> 0 Then
                item.Selected = True
                Return did
            End If
        Next

        Return 0
    End Function

    Private Sub MarcarChecksMoneda(nacional As HtmlInputCheckBox, internacional As HtmlInputCheckBox, monedaId As Integer)
        nacional.Checked = (monedaId = 1)
        internacional.Checked = (monedaId = 2)
    End Sub

    Private Sub SeleccionarTexto(ddl As DropDownList, texto As String)
        ddl.ClearSelection()

        If String.IsNullOrWhiteSpace(texto) Then Exit Sub

        Dim item As ListItem = ddl.Items.FindByText(texto)
        If item IsNot Nothing Then item.Selected = True
    End Sub

    Private Function TextoFecha(valor As DateTime?) As String
        If Not valor.HasValue Then Return String.Empty
        Return valor.Value.ToString("yyyy-MM-dd")
    End Function

    Private Function TextoMonto(valor As Decimal?) As String
        If Not valor.HasValue Then Return String.Empty
        Return valor.Value.ToString("0.00", Globalization.CultureInfo.InvariantCulture)
    End Function

    Protected Sub gvCotizaciones_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        gvCotizaciones.PageIndex = e.NewPageIndex

        CargarCotizaciones()
    End Sub

    Protected Sub CargarCotizaciones()
        Dim api As New ConsumoApi
        Dim cargarCotizaciones As String = api.GetCargarCotizaciones()

        Dim listaCotizaciones As New List(Of Cotizacion)

        If Not String.IsNullOrWhiteSpace(cargarCotizaciones) AndAlso
           cargarCotizaciones <> "null" AndAlso
           Not cargarCotizaciones.StartsWith("ERROR") Then

            listaCotizaciones = JsonConvert.DeserializeObject(Of List(Of Cotizacion))(cargarCotizaciones)
            If listaCotizaciones Is Nothing Then listaCotizaciones = New List(Of Cotizacion)
        End If

        Dim busqueda As String = txtBuscarCotizacion.Text.Trim()

        If busqueda.Length > 0 Then
            listaCotizaciones = listaCotizaciones.
                Where(Function(c) CoincideBusqueda(c, busqueda)).
                ToList()
        End If

        listaCotizaciones = AplicarFiltroPeriodo(listaCotizaciones)

        ' Al filtrar, la página en la que estabas puede dejar de existir.
        Dim ultimaPagina As Integer = 0

        If listaCotizaciones.Count > 0 Then
            ultimaPagina = CInt(Math.Ceiling(listaCotizaciones.Count / CDbl(gvCotizaciones.PageSize))) - 1
        End If

        If gvCotizaciones.PageIndex > ultimaPagina Then
            gvCotizaciones.PageIndex = ultimaPagina
        End If

        gvCotizaciones.DataSource = listaCotizaciones
        gvCotizaciones.DataBind()
    End Sub

    Protected Sub txtBuscarCotizacion_TextChanged(sender As Object, e As EventArgs)
        ' Una búsqueda nueva siempre arranca en la primera página.
        gvCotizaciones.PageIndex = 0

        CargarCotizaciones()
    End Sub

    Protected Sub ddlTipoPolizas_SelectedIndexChanged(sender As Object, e As EventArgs)
        gvCotizaciones.PageIndex = 0

        CargarCotizaciones()
    End Sub

    ''' <summary>
    ''' Filtro del combo de periodo, sobre la fecha de cotización.
    ''' No hay opción "Canceladas": el API filtra las que tienen FechaCancelacion,
    ''' así que nunca llegan a esta lista.
    ''' </summary>
    Private Function AplicarFiltroPeriodo(lista As List(Of Cotizacion)) As List(Of Cotizacion)

        Dim filtro As Integer
        Integer.TryParse(ddlTipoPolizas.SelectedValue, filtro)

        Select Case filtro

            Case 1 ' Hoy
                Return lista.Where(Function(c) c.FechaCotizacion.Date = Date.Today).ToList()

            Case 2 ' Mes actual
                Return lista.Where(Function(c) EsDelMismoMes(c.FechaCotizacion, Date.Today)).ToList()

            Case 3 ' Mes anterior
                Return lista.Where(Function(c) EsDelMismoMes(c.FechaCotizacion, Date.Today.AddMonths(-1))).ToList()

            Case Else ' Todos
                Return lista

        End Select
    End Function

    Private Function EsDelMismoMes(fecha As DateTime, referencia As DateTime) As Boolean
        Return fecha.Year = referencia.Year AndAlso fecha.Month = referencia.Month
    End Function

    Private Function CoincideBusqueda(c As Cotizacion, busqueda As String) As Boolean
        Return Contiene(c.nombreCliente, busqueda) OrElse
               Contiene(c.NumeroPoliza, busqueda) OrElse
               Contiene(c.CotizacionId.ToString(), busqueda)
    End Function

    ''' <summary>
    ''' Búsqueda parcial que ignora mayúsculas y acentos, para que "peres"
    ''' encuentre "Pérez".
    ''' </summary>
    Private Function Contiene(valor As String, busqueda As String) As Boolean
        If String.IsNullOrEmpty(valor) Then Return False

        Return Globalization.CultureInfo.InvariantCulture.CompareInfo.IndexOf(
            valor, busqueda,
            Globalization.CompareOptions.IgnoreCase Or Globalization.CompareOptions.IgnoreNonSpace) >= 0
    End Function

    Protected Sub cargarClientes()
        Dim api As New ConsumoApi()
        Dim cargarClientes As String = api.GetCargarClientes()

        Dim listaClientes As List(Of Cliente) = JsonConvert.DeserializeObject(Of List(Of Cliente))(cargarClientes)

        ddlCliente.Items.Clear()

        ddlCliente.DataSource = listaClientes
        ddlCliente.DataTextField = "NombreCompleto"
        ddlCliente.DataValueField = "ClienteId"
        ddlCliente.DataBind()

        ddlCliente.Items.Insert(0, New ListItem("-- Selecciona --", "0"))

    End Sub

    Protected Sub CargarPolizas(tipo As String)
        Dim api As New ConsumoApi()
        Dim json As String = api.GetCargarPolizas()

        Dim listaPolizas As List(Of Poliza) =
            JsonConvert.DeserializeObject(Of List(Of Poliza))(json)

        ddlNombreInternoPoliza.Items.Clear()

        If tipo = "Mercancia" Then
            Dim listaMercancia = listaPolizas.
                Where(Function(p) p.PolizaMercancia IsNot Nothing AndAlso p.PolizaMercancia.Count > 0).
                SelectMany(Function(p) p.PolizaMercancia.
                    Select(Function(m) New With {
                        .Id = p.PolizaId & "|" & m.PolizaMercanciaId,
                        .Nombre = m.NombreInternoPoliza
                    })).
                ToList()

            ddlNombreInternoPoliza.DataSource = listaMercancia
            ddlNombreInternoPoliza.DataTextField = "Nombre"
            ddlNombreInternoPoliza.DataValueField = "Id"
            ddlNombreInternoPoliza.DataBind()

        ElseIf tipo = "Contenedor" Then
            Dim listaContenedor = listaPolizas.
                Where(Function(p) p.PolizaContenedor IsNot Nothing).
                Select(Function(p) New With {
                    .Id = p.PolizaId & "|" & p.PolizaContenedor.PolizaContenedorId,
                    .Nombre = p.PolizaContenedor.NombreInternoPoliza
                }).
                ToList()

            ddlNombreInternoPoliza.DataSource = listaContenedor
            ddlNombreInternoPoliza.DataTextField = "Nombre"
            ddlNombreInternoPoliza.DataValueField = "Id"
            ddlNombreInternoPoliza.DataBind()
        End If

        ddlNombreInternoPoliza.Items.Insert(0, New ListItem("-- Selecciona --", "0"))
    End Sub

    Protected Sub ddlCliente_SelectedIndexChanged(sender As Object, e As EventArgs)
        CargarBeneficiariosDeCliente(IdSeleccionado(ddlCliente))
    End Sub

    ''' <summary>
    ''' Consulta el cliente y llena su combo de beneficiarios. Se usa al elegir un
    ''' cliente y al abrir una cotización para editar.
    ''' </summary>
    Private Sub CargarBeneficiariosDeCliente(clienteId As Integer)

        LimpiarBeneficiarios()

        If clienteId = 0 Then Exit Sub

        Dim api As New ConsumoApi()
        Dim json As String = api.GetClienteId(clienteId)

        If String.IsNullOrWhiteSpace(json) OrElse json.StartsWith("ERROR") Then
            Avisar("No se pudo consultar el cliente.", "danger")
            Exit Sub
        End If

        Dim cliente As Cliente = Nothing

        Try
            cliente = JsonConvert.DeserializeObject(Of Cliente)(json)
        Catch ex As Exception
            cliente = Nothing
        End Try

        If cliente Is Nothing Then
            Avisar("No se encontró el cliente seleccionado.", "danger")
            Exit Sub
        End If

        CargarBeneficiariosDelCliente(cliente)
    End Sub

    Private Sub CargarBeneficiariosDelCliente(cliente As Cliente)
        ddlBeneficiarioPreferente.Items.Clear()

        If cliente.ClienteBeneficiario Is Nothing OrElse cliente.ClienteBeneficiario.Count = 0 Then Exit Sub

        ddlBeneficiarioPreferente.DataSource = cliente.ClienteBeneficiario
        ddlBeneficiarioPreferente.DataTextField = "nombreCompletoBP"
        ddlBeneficiarioPreferente.DataValueField = "beneficiarioPreferenteId"
        ddlBeneficiarioPreferente.DataBind()

        ddlBeneficiarioPreferente.Items.Insert(0, New ListItem("-- Selecciona --", "0"))
    End Sub

    Private Sub LimpiarBeneficiarios()
        ddlBeneficiarioPreferente.Items.Clear()
    End Sub

    Protected Sub ddlNombreInternoPoliza_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim polizaId As Integer
        Dim detalleId As Integer
        PartesSeleccion(ddlNombreInternoPoliza.SelectedValue, polizaId, detalleId)

        CargarDesdePolizaId(polizaId, detalleId)
    End Sub

    ''' <summary>
    ''' Consulta la póliza y vuelca sus datos al formulario. Se usa tanto al elegir
    ''' una póliza como al abrir una cotización para editar, porque los combos de
    ''' subclasificación y coberturas se alimentan del detalle de la póliza.
    ''' </summary>
    Private Sub CargarDesdePolizaId(polizaId As Integer, detalleId As Integer)

        LimpiarDatosPoliza()

        If polizaId = 0 Then Exit Sub

        Dim api As New ConsumoApi()
        Dim json As String = api.GetPolizaId(polizaId)

        If String.IsNullOrWhiteSpace(json) OrElse json.StartsWith("ERROR") Then
            Avisar("No se pudo consultar la póliza.", "danger")
            Exit Sub
        End If

        Dim poliza As Poliza = Nothing

        Try
            poliza = JsonConvert.DeserializeObject(Of Poliza)(json)
        Catch ex As Exception
            poliza = Nothing
        End Try

        If poliza Is Nothing Then
            Avisar("No se encontró la póliza seleccionada.", "danger")
            Exit Sub
        End If

        LlenarDesdePoliza(poliza, detalleId)
    End Sub

    Private Sub LlenarDesdePoliza(poliza As Poliza, detalleId As Integer)

        SeleccionarValor(ddlMoneda, poliza.MonedaId)

        If poliza.VigenciaDel.HasValue Then
            txtVigenciaDel.Text = poliza.VigenciaDel.Value.ToString("yyyy-MM-dd")
        End If

        If poliza.VigenciaHasta.HasValue Then
            txtVigenciaHasta.Text = poliza.VigenciaHasta.Value.ToString("yyyy-MM-dd")
        End If

        Dim mercancia As PolizaMercancia = Nothing

        If poliza.PolizaMercancia IsNot Nothing AndAlso poliza.PolizaMercancia.Count > 0 Then

            mercancia = poliza.PolizaMercancia.
                FirstOrDefault(Function(m) m.PolizaMercanciaId = detalleId)

            If mercancia Is Nothing Then mercancia = poliza.PolizaMercancia.First()

            txtDeducibles.Text = mercancia.Deducibles
        End If

        ' Los bienes registrados en la póliza alimentan dos combos: Subclasificación
        ' en la captura de mercancía y Bienes Asegurados en la de contenedor.
        LlenarComboBienes(ddlSubclasificación, poliza.Bien)
        LlenarComboBienes(ddlbienesasegurados, poliza.Bien)

        CargarCoberturasDePoliza(poliza, mercancia)

    End Sub

    ''' <summary>
    ''' Llena un combo con los bienes asegurados de la póliza. Si no hay ninguno lo
    ''' deja vacío, en vez de conservar los de la póliza anterior.
    ''' </summary>
    Private Sub LlenarComboBienes(ddl As DropDownList, bienes As List(Of Bien))
        ddl.Items.Clear()

        If bienes Is Nothing OrElse bienes.Count = 0 Then Exit Sub

        ddl.DataSource = bienes
        ddl.DataTextField = "Nombre"
        ddl.DataValueField = "BienId"
        ddl.DataBind()

        ddl.Items.Insert(0, New ListItem("-- Selecciona --", "0"))
    End Sub

    Private Sub CargarCoberturasDePoliza(poliza As Poliza, mercancia As PolizaMercancia)

        ddlCoberturas.Items.Clear()

        If poliza.PolizaContenedor IsNot Nothing AndAlso
           poliza.PolizaContenedor.Cobertura IsNot Nothing AndAlso
           poliza.PolizaContenedor.Cobertura.Count > 0 Then

            ddlCoberturas.DataSource = poliza.PolizaContenedor.Cobertura
            ddlCoberturas.DataTextField = "Nombre"
            ddlCoberturas.DataValueField = "CoberturaId"
            ddlCoberturas.DataBind()

        ElseIf mercancia IsNot Nothing AndAlso
               mercancia.RiesgoCubierto IsNot Nothing AndAlso
               mercancia.RiesgoCubierto.Count > 0 Then

            ddlCoberturas.DataSource = mercancia.RiesgoCubierto
            ddlCoberturas.DataTextField = "Nombre"
            ddlCoberturas.DataValueField = "RiesgoCubiertoId"
            ddlCoberturas.DataBind()

        End If

        If ddlCoberturas.Items.Count > 0 Then
            ddlCoberturas.Items.Insert(0, New ListItem("-- Selecciona --", "0"))
        End If

    End Sub
    Private Sub LimpiarDatosPoliza()
        ddlMoneda.ClearSelection()
        txtVigenciaDel.Text = String.Empty
        txtVigenciaHasta.Text = String.Empty
        txtDeducibles.Text = String.Empty
        ddlSubclasificación.Items.Clear()
        ddlCoberturas.Items.Clear()
        ddlbienesasegurados.Items.Clear()

        ' Lo agregado pertenecía a la póliza anterior: ya no aplica.
        CoberturasCotizacion.Clear()
        CargarGridCoberturas()

        BienesCotizacion.Clear()
        CargarGridBienes()
    End Sub

    Private Sub PartesSeleccion(valor As String, ByRef polizaId As Integer, ByRef detalleId As Integer)
        polizaId = 0
        detalleId = 0

        If String.IsNullOrWhiteSpace(valor) Then Exit Sub

        Dim partes As String() = valor.Split("|"c)

        Integer.TryParse(partes(0), polizaId)

        If partes.Length > 1 Then Integer.TryParse(partes(1), detalleId)
    End Sub

    Private Sub SeleccionarValor(ddl As DropDownList, valor As Integer?)
        ddl.ClearSelection()

        If Not valor.HasValue Then Exit Sub

        Dim item As ListItem = ddl.Items.FindByValue(valor.Value.ToString())

        If item IsNot Nothing Then item.Selected = True
    End Sub

    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs)
        hfCotizacionId.Value = String.Empty

        CoberturasCotizacion.Clear()
        CargarGridCoberturas()

        VolverAlListado()
    End Sub

    Private Function NuloSiCero(valor As Integer) As Integer?
        If valor = 0 Then Return Nothing
        Return valor
    End Function


    Private Sub VolverAlListado()
        pnlFormularioCotizaciones.Visible = False
        pnlMercancia.Visible = False
        pnlEncabezado.Visible = True
        PnlTabla.Visible = True
    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs)

        Dim polizaId As Integer
        Dim detalleId As Integer
        PartesSeleccion(ddlNombreInternoPoliza.SelectedValue, polizaId, detalleId)

        Dim clienteId As Integer = IdSeleccionado(ddlCliente)
        Dim monedaId As Integer = IdSeleccionado(ddlMoneda)

        If polizaId = 0 Then
            Avisar("Selecciona el nombre interno de la póliza.", "warning")
            Exit Sub
        End If

        If clienteId = 0 Then
            Avisar("Selecciona el cliente.", "warning")
            Exit Sub
        End If

        If monedaId = 0 Then
            Avisar("Selecciona la moneda.", "warning")
            Exit Sub
        End If

        Dim beneficiarioId As Integer = IdSeleccionado(ddlBeneficiarioPreferente)

        ' Los importes se recalculan aquí en vez de leerlos de los campos: los
        ' calculados son de solo lectura y su texto trae formato de moneda, así que
        ' el servidor no debe depender de que el JavaScript haya corrido.
        Dim prima As Decimal = MontoDe(txtPrimaYSeguramiento).GetValueOrDefault()
        Dim gastos As Decimal = MontoDe(txtGastosExpedicion).GetValueOrDefault()
        Dim subtotal As Decimal = prima + gastos
        Dim iva As Decimal = Decimal.Round(subtotal * IVA_PORCENTAJE, 2)

        Dim cotizacion As New Cotizacion With {
            .PolizaId = polizaId,
            .ClienteId = clienteId,
            .MonedaId = monedaId,
            .BeneficiarioPreferenteId = If(beneficiarioId = 0, CType(Nothing, Integer?), beneficiarioId),
            .FechaCotizacion = FechaDe(txtFechaCotizacion).GetValueOrDefault(Date.Now),
            .VigenciaDel = FechaDe(txtVigenciaDel),
            .VigenciaHasta = FechaDe(txtVigenciaHasta),
            .PrimaServicioDeAseguramiento = prima,
            .GastosExpedicion = gastos,
            .Subtotal = subtotal,
            .IVA = iva,
            .Total = subtotal + iva,
            .CotizacionMercancia = ArmarCotizacionMercancia()
        }

        Dim cotizacionId As Integer
        Integer.TryParse(hfCotizacionId.Value, cotizacionId)

        Dim esEdicion As Boolean = cotizacionId > 0
        If esEdicion Then cotizacion.CotizacionId = cotizacionId

        Dim api As New ConsumoApi()
        Dim json As String = JsonConvert.SerializeObject(cotizacion)

        Dim respuesta As String
        If esEdicion Then
            respuesta = api.PutEditarCotizacion(cotizacionId, json)
        Else
            respuesta = api.PostCotizacion(json)
        End If

        If String.IsNullOrWhiteSpace(respuesta) OrElse respuesta.StartsWith("ERROR") Then
            ' El API devuelve el motivo en texto plano; se muestra tal cual.
            Dim detalle As String = If(String.IsNullOrWhiteSpace(respuesta), "sin respuesta del servidor", respuesta.Substring(6).Trim())
            Avisar("No se pudo guardar: " & detalle, "danger")
            Exit Sub
        End If

        Avisar(If(esEdicion, "Cotización editada correctamente", "Cotización agregada correctamente"), "success")

        hfCotizacionId.Value = String.Empty

        CoberturasCotizacion.Clear()
        CargarGridCoberturas()

        CargarCotizaciones()

        VolverAlListado()
    End Sub


    Private Function ArmarCotizacionMercancia() As CotizacionMercancia

        Return New CotizacionMercancia With {
            .CotizacionCliente = txtSubRamo.Text,
            .TransitoId = NuloSiCero(IdSeleccionado(ddlTransito)),
            .ClasificacionId = IdSeleccionado(ddlClasificacion),
            .SubClasificacion = TextoSeleccionado(ddlSubclasificación),
            .DescripcionMercancia = txtDescripcionMercancia.Text,
            .TipoEmpaque = txtTipoEmpaque.Text,
            .Origen = txtOrigen.Text,
            .Destino = txtDestino.Text,
            .MedioDeConduccion = txtMediosConduccion.Text,
            .MedioDeTransporte = txtMedioTransporte.Text,
            .Observaciones = txtObservaciones.Text,
            .MedidasDeSeguridadAdicionales = txtMedidasSeguridad.Text,
            .Deducibles = txtDeducibles.Text,
            .SumaAsegurada = MontoDe(txtSumaAsegurada),
            .CuotaAplicable = MontoDe(txtCuotaAplicable),
            .CuotaMinima = MontoDe(txtCuotaMinima),
            .TipoCambioCotizar = MontoDe(txtTipoCambio),
            .MonedaCuotaAplicableId = MonedaDeCheck(chkCuotaAplicableN, chkCuotaAplicableI),
            .MonedaCuotaMinimaId = MonedaDeCheck(chkCuotaMinimaN, chkCuotaMinimaI),
            .MonedaCotizarId = IdSeleccionado(ddlMoneda)
        }
    End Function

    Private Function MonedaDeCheck(nacional As HtmlInputCheckBox, internacional As HtmlInputCheckBox) As Integer
        If nacional IsNot Nothing AndAlso nacional.Checked Then Return 1
        If internacional IsNot Nothing AndAlso internacional.Checked Then Return 2
        Return 0
    End Function

    Private Function MontoDe(txt As TextBox) As Decimal?
        If txt Is Nothing OrElse String.IsNullOrWhiteSpace(txt.Text) Then Return Nothing

        Dim limpio As String = Text.RegularExpressions.Regex.Replace(txt.Text, "[^0-9.\-]", "")

        Dim valor As Decimal
        If Decimal.TryParse(limpio, Globalization.NumberStyles.Any, Globalization.CultureInfo.InvariantCulture, valor) Then
            Return valor
        End If

        Return Nothing
    End Function

    Private Function FechaDe(txt As TextBox) As DateTime?
        If txt Is Nothing OrElse String.IsNullOrWhiteSpace(txt.Text) Then Return Nothing

        Dim valor As DateTime
        If DateTime.TryParse(txt.Text, valor) Then Return valor

        Return Nothing
    End Function

    Private Function IdSeleccionado(ddl As DropDownList) As Integer
        If ddl Is Nothing Then Return 0

        Dim id As Integer
        If Integer.TryParse(ddl.SelectedValue, id) Then Return id

        Return 0
    End Function

    Private Function TextoSeleccionado(ddl As DropDownList) As String
        If ddl Is Nothing OrElse ddl.SelectedItem Is Nothing Then Return String.Empty
        If ddl.SelectedValue = "0" Then Return String.Empty

        Return ddl.SelectedItem.Text
    End Function

    Private Sub Avisar(mensaje As String, tipo As String)
        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "toast",
            "showToast('" & mensaje.Replace("'", "\'") & "', '" & tipo & "');", True)
    End Sub
End Class