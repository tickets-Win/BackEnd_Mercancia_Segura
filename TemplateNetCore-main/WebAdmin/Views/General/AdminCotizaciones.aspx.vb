Imports Newtonsoft.Json
Imports WebAdmin.MercanciaSegura.DOM.Modelos
Imports System.Web.UI.HtmlControls

Public Class AdminCotizaciones
    Inherits System.Web.UI.Page

    ''' <summary>
    ''' Debe coincidir con el MS_IVA del JavaScript de la vista.
    ''' </summary>
    Private Const IVA_PORCENTAJE As Decimal = 0.16D

    ''' <summary>
    ''' Catálogos de contenedor serializados para el JavaScript que arma la tabla.
    ''' Esa tabla se genera en el navegador, así que los combos de Tipo y Tamaño no
    ''' son controles de servidor y hay que pasarles los datos por aquí.
    ''' Se dejan como "[]" si el API no responde, para que el script no truene.
    ''' </summary>
    Protected TiposContenedorJson As String = "[]"
    Protected TamaniosContenedorJson As String = "[]"


    Private ReadOnly Property CoberturasCotizacion As List(Of Cobertura)
        Get
            If Session("CoberturasCotizacion") Is Nothing Then
                Session("CoberturasCotizacion") = New List(Of Cobertura)()
            End If
            Return CType(Session("CoberturasCotizacion"), List(Of Cobertura))
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ' Necesario para que suban los adjuntos del correo. ASP.NET solo pone este
        ' atributo si ve un FileUpload al renderizar, y el panel del correo nace
        ' oculto; cuando se muestra ya es por postback parcial y la etiqueta
        ' <form> vive fuera del UpdatePanel, así que nunca se vuelve a dibujar.
        Me.Form.Enctype = "multipart/form-data"

        ' Se cargan también en postback: la tabla de contenedores se vuelve a
        ' generar en el cliente después de cada actualización parcial.
        CargarCatalogosContenedor()
        PrepararContenedoresJson()

        If Not IsPostBack Then
            pnlMercancia.Visible = False
            CargarCotizaciones()
            cargarClientes()
            CargarPolizas(ddlTipoCotizacion.SelectedValue)
            CargarTipoMoneda(ddlMoneda)
            CargarClasificacion(ddlClasificacion)
            CargarTransito(ddlTransito)
            CargarTipoTarifa(ddlTipoTarifaSecos, ddlTipoRefrigerados, ddlTipoIsotaques)
            CargarUnidades()

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

    ''' <summary>
    ''' Bienes de la póliza seleccionada. Se conservan completos porque al guardar
    ''' hay que mandar TipoBienId y AdministracionBienId, que el combo no lleva.
    ''' </summary>
    Private Property BienesDePoliza As List(Of Bien)
        Get
            If Session("BienesDePoliza") Is Nothing Then
                Session("BienesDePoliza") = New List(Of Bien)()
            End If
            Return CType(Session("BienesDePoliza"), List(Of Bien))
        End Get
        Set(value As List(Of Bien))
            Session("BienesDePoliza") = If(value, New List(Of Bien)())
        End Set
    End Property

    Protected Sub btnbienesasegurados_Click(sender As Object, e As EventArgs)
        Dim bienId As Integer
        If Not Integer.TryParse(ddlbienesasegurados.SelectedValue, bienId) Then Exit Sub
        If bienId = 0 Then Exit Sub

        ' No agregar dos veces el mismo.
        If BienesCotizacion.Any(Function(b) b.BienId = bienId) Then Exit Sub

        ' Se toma el bien completo de la póliza, no solo el texto del combo.
        Dim bien As Bien = BienesDePoliza.FirstOrDefault(Function(b) b.BienId = bienId)

        If bien Is Nothing Then Exit Sub

        BienesCotizacion.Add(bien)

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
        txtCondicionesEspeciales.Text = String.Empty
        txtExclusiones.Text = String.Empty

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

        ' Bloque de contenedor. El hidden se limpia aparte: Page_Load ya lo llenó
        ' con lo que venía en el Request, así que sin esto la cotización nueva
        ' arrancaría con los contenedores de la anterior.
        hfContenedores.Value = "[]"
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
        ' que puede haber quedado con las de contenedor. En un alta nueva sí se
        ' puede elegir el tipo; solo se bloquea al editar.
        ddlTipoCotizacion.Enabled = True
        ResetearCombo(ddlTipoCotizacion)
        AplicarTipoCotizacion(ddlTipoCotizacion.SelectedValue)
    End Sub

    ''' <summary>
    ''' Trae los catálogos de tipo y tamaño de contenedor y los deja listos como
    ''' JSON con la forma { id, nombre } que consume el script de la tabla.
    ''' </summary>
    Private Sub CargarCatalogosContenedor()

        Dim api As New ConsumoApi()

        TiposContenedorJson = SerializarCatalogo(Of TipoContenedor)(
            api.GetTipoContenedor(),
            Function(t) t.TipoContenedorId,
            Function(t) t.Nombre)

        TamaniosContenedorJson = SerializarCatalogo(Of TamanioContenedor)(
            api.GetTamanioContenedor(),
            Function(t) t.TamanioContenedorId,
            Function(t) t.Nombre)
    End Sub

    ''' <summary>
    ''' Convierte la respuesta del API en un JSON de { id, nombre }. Devuelve un
    ''' arreglo vacío si el endpoint falla, para no romper el JavaScript.
    ''' </summary>
    Private Function SerializarCatalogo(Of T)(json As String,
                                              obtenerId As Func(Of T, Integer),
                                              obtenerNombre As Func(Of T, String)) As String

        Try
            If String.IsNullOrWhiteSpace(json) OrElse
               json = "null" OrElse
               json.StartsWith("ERROR") Then

                Return "[]"
            End If

            Dim lista As List(Of T) = JsonConvert.DeserializeObject(Of List(Of T))(json)
            If lista Is Nothing Then Return "[]"

            Dim proyectada = lista.Select(Function(x) New With {
                .id = obtenerId(x),
                .nombre = obtenerNombre(x)
            })

            Return JsonConvert.SerializeObject(proyectada)

        Catch ex As Exception
            Return "[]"
        End Try
    End Function

    ''' <summary>
    ''' Llena el combo de unidades del 1 al 20 desde el servidor. Antes lo hacía el
    ''' JavaScript, pero ASP.NET rechaza en el postback los valores que no registró.
    ''' </summary>
    Private Sub CargarUnidades()
        ddlUnidades.Items.Clear()

        For i As Integer = 1 To 20
            ddlUnidades.Items.Add(New ListItem(i.ToString(), i.ToString()))
        Next
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
            Case "Aceptar"
                AceptarCotizacion(cotizacionId)
            Case "Editar"
                EditarCotizacion(cotizacionId)
            Case "Correo"
                AbrirCorreo(cotizacionId)
            Case "Eliminar"
                CancelarCotizacion(cotizacionId)
        End Select
    End Sub

    ''' <summary>
    ''' Abre el control de envio de correo con los datos de esa cotizacion: sus
    ''' destinatarios y los valores con los que se resuelven los {{campos}}.
    ''' </summary>
    Private Sub AbrirCorreo(cotizacionId As Integer)

        ucCorreo.Abrir(CorreosDelCliente(cotizacionId), ValoresDeCampos(cotizacionId))

        pnlEncabezado.Visible = False
        PnlTabla.Visible = False
        pnlFormularioCotizaciones.Visible = False
        pnlMercancia.Visible = False
    End Sub

    Protected Sub ucCorreo_Cancelado(sender As Object, e As EventArgs)
        VolverAlListado()
    End Sub

    Protected Sub ucCorreo_Enviado(sender As Object, e As EventArgs)
        Avisar("Correo enviado correctamente.", "success")

        VolverAlListado()
    End Sub


    ''' <summary>
    ''' Valores conocidos para los {{campos}}. Lo que no se puede resolver se deja
    ''' fuera a propósito: el campo se queda visible en el texto y se avisa antes
    ''' de enviar.
    ''' </summary>
    Private Function ValoresDeCampos(cotizacionId As Integer) As Dictionary(Of String, String)

        Dim valores As New Dictionary(Of String, String) From {
            {"FechaActual", Date.Today.ToString("dd/MM/yyyy")}
        }

        If cotizacionId <= 0 Then Return valores

        Dim api As New ConsumoApi()
        Dim json As String = api.GetCotizacionId(cotizacionId)

        If String.IsNullOrWhiteSpace(json) OrElse json.StartsWith("ERROR") Then Return valores

        Dim cot As Cotizacion = JsonConvert.DeserializeObject(Of Cotizacion)(json)
        If cot Is Nothing Then Return valores

        If Not String.IsNullOrWhiteSpace(cot.nombreCliente) Then
            valores("Nombre Completo") = cot.nombreCliente
            valores("Nombre") = cot.nombreCliente.Split(" "c)(0)
        End If

        If Not String.IsNullOrWhiteSpace(cot.NumeroPoliza) Then
            valores("Póliza") = cot.NumeroPoliza
        End If

        Dim correo As String = PrimerCorreo(cot.ClienteId)
        If Not String.IsNullOrWhiteSpace(correo) Then valores("Correo") = correo

        Dim clave As String = ClaveDelCertificado(cotizacionId)
        If Not String.IsNullOrWhiteSpace(clave) Then valores("Certificado") = clave

        Return valores
    End Function

    ''' <summary>Todos los correos del cliente, separados por punto y coma.</summary>
    Private Function CorreosDelCliente(cotizacionId As Integer) As String

        Dim api As New ConsumoApi()
        Dim json As String = api.GetCotizacionId(cotizacionId)

        If String.IsNullOrWhiteSpace(json) OrElse json.StartsWith("ERROR") Then Return String.Empty

        Dim cot As Cotizacion = JsonConvert.DeserializeObject(Of Cotizacion)(json)
        If cot Is Nothing Then Return String.Empty

        Return String.Join("; ", ListaDeCorreos(cot.ClienteId))
    End Function

    Private Function PrimerCorreo(clienteId As Integer) As String

        Dim lista = ListaDeCorreos(clienteId)

        If lista.Count = 0 Then Return String.Empty

        Return lista(0)
    End Function

    Private Function ListaDeCorreos(clienteId As Integer) As List(Of String)

        Dim correos As New List(Of String)

        If clienteId <= 0 Then Return correos

        Dim api As New ConsumoApi()
        Dim json As String = api.GetCorreosCliente(clienteId)

        If String.IsNullOrWhiteSpace(json) OrElse
           json = "null" OrElse
           json.StartsWith("ERROR") Then Return correos

        Try
            For Each item As Newtonsoft.Json.Linq.JObject In Newtonsoft.Json.Linq.JArray.Parse(json)

                Dim valor = item.GetValue("correo", StringComparison.OrdinalIgnoreCase)

                If valor Is Nothing Then Continue For

                Dim direccion As String = valor.ToString().Trim()

                If direccion.Length > 0 AndAlso Not correos.Contains(direccion) Then
                    correos.Add(direccion)
                End If
            Next
        Catch
            ' Si el catálogo no viene como se espera, se deja vacío el destinatario.
        End Try

        Return correos
    End Function

    ''' <summary>Clave del certificado de esa cotización, si ya fue confirmada.</summary>
    Private Function ClaveDelCertificado(cotizacionId As Integer) As String

        Dim api As New ConsumoApi()
        Dim json As String = api.GetCertificados()

        If String.IsNullOrWhiteSpace(json) OrElse
           json = "null" OrElse
           json.StartsWith("ERROR") Then Return String.Empty

        Dim lista As List(Of Certificado) =
            JsonConvert.DeserializeObject(Of List(Of Certificado))(json)

        If lista Is Nothing Then Return String.Empty

        Dim cert = lista.FirstOrDefault(Function(c) c.CotizacionId = cotizacionId)

        If cert Is Nothing Then Return String.Empty

        Return cert.ClaveCertificado
    End Function

    ''' <summary>
    ''' Ids de las cotizaciones que ya tienen certificado. Se consulta una sola vez
    ''' por carga del listado; el certificado es lo que marca que la cotización fue
    ''' aceptada, no hay un campo de estatus en la cotización misma.
    ''' </summary>
    Private CotizacionesAceptadas As HashSet(Of Integer)

    Private Sub CargarCotizacionesAceptadas()
        CotizacionesAceptadas = New HashSet(Of Integer)

        Dim api As New ConsumoApi()
        Dim json As String = api.GetCertificados()

        If String.IsNullOrWhiteSpace(json) OrElse
           json = "null" OrElse
           json.StartsWith("ERROR") Then Exit Sub

        Dim certificados As List(Of Certificado) =
            JsonConvert.DeserializeObject(Of List(Of Certificado))(json)

        If certificados Is Nothing Then Exit Sub

        For Each c As Certificado In certificados
            CotizacionesAceptadas.Add(c.CotizacionId)
        Next
    End Sub

    ''' <summary>
    ''' La palomita se pinta en verde cuando la cotización ya tiene certificado.
    ''' El color es solo señal: quien de verdad la apaga es PuedeAceptar.
    ''' </summary>
    Protected Function IconoAceptar(valor As Object) As String
        If YaAceptada(valor) Then Return "icon-btn action-icon text-success ms-aceptada"

        Return "icon-btn action-icon"
    End Function

    ''' <summary>
    ''' Una cotización se confirma una sola vez. Con Enabled en False el LinkButton
    ''' se pinta sin href y deja de hacer postback; la clase "disabled" de Bootstrap
    ''' no sirve aquí porque no aplica a enlaces normales.
    ''' </summary>
    Protected Function PuedeAceptar(valor As Object) As Boolean
        Return Not YaAceptada(valor)
    End Function

    ''' <summary>
    ''' El OnClientClick hay que apagarlo aparte: ASP.NET lo escribe como atributo
    ''' onclick aunque el control esté deshabilitado, y el navegador lo ejecuta
    ''' igual porque "disabled" no significa nada en un enlace. Sin esto salía el
    ''' confirm en una cotización ya certificada.
    ''' </summary>
    Protected Function ConfirmacionAceptar(valor As Object) As String
        If YaAceptada(valor) Then Return "return false;"

        Return "return confirm('¿Aceptar esta cotización? Se generará su certificado.');"
    End Function

    Protected Function TituloAceptar(valor As Object) As String
        If YaAceptada(valor) Then Return "Ya aceptada: tiene certificado"

        Return "Aceptar cotización y generar certificado"
    End Function


    ''' <summary>
    ''' Editar y cancelar se apagan por la misma razón que la palomita: una vez
    ''' que la cotización tiene certificado, ese certificado ya guardó copia del
    ''' asegurado, la vigencia y la suma asegurada, y de él cuelgan siniestros y
    ''' endosos. Tocar la cotización después dejaría esos números sin coincidir.
    ''' </summary>
    Protected Function IconoBloqueable(valor As Object) As String
        If YaAceptada(valor) Then Return "icon-btn action-icon text-muted"

        Return "icon-btn action-icon"
    End Function

    Protected Function TituloEditar(valor As Object) As String
        If YaAceptada(valor) Then Return "Ya tiene certificado: no se puede editar"

        Return "Editar"
    End Function

    Protected Function TituloEliminar(valor As Object) As String
        If YaAceptada(valor) Then Return "Ya tiene certificado: no se puede cancelar"

        Return "Eliminar"
    End Function

    Protected Function ConfirmacionEditar(valor As Object) As String
        If YaAceptada(valor) Then Return "return false;"

        Return String.Empty
    End Function

    Protected Function ConfirmacionEliminar(valor As Object) As String
        If YaAceptada(valor) Then Return "return false;"

        Return "return confirm('¿Seguro que deseas cancelar esta cotización?');"
    End Function

    Private Function YaAceptada(valor As Object) As Boolean
        If CotizacionesAceptadas Is Nothing Then Return False

        Dim id As Integer
        If Not Integer.TryParse(Convert.ToString(valor), id) Then Return False

        Return CotizacionesAceptadas.Contains(id)
    End Function

    ''' <summary>
    ''' Acepta la cotización generando su certificado. No hay campo "confirmada" en
    ''' la cotización: la existencia del certificado es lo que la marca como tal.
    ''' </summary>
    Private Sub AceptarCotizacion(cotizacionId As Integer)

        If CotizacionesAceptadas Is Nothing Then CargarCotizacionesAceptadas()

        If CotizacionesAceptadas.Contains(cotizacionId) Then
            Avisar("Esta cotización ya fue aceptada y tiene certificado.", "warning")
            Exit Sub
        End If

        ' Solo se manda la cotización: el API copia de ella el asegurado, la
        ' vigencia y la suma asegurada, y la deja con estatus Activo.
        Dim peticion = New With {.cotizacionId = cotizacionId}

        Dim api As New ConsumoApi()
        Dim respuesta As String = api.PostCertificado(JsonConvert.SerializeObject(peticion))

        If String.IsNullOrWhiteSpace(respuesta) OrElse respuesta.StartsWith("ERROR") Then
            Avisar("No se pudo aceptar la cotización: " & DetalleDeError(respuesta), "danger")
            Exit Sub
        End If

        Avisar("Cotización aceptada. Se generó su certificado.", "success")

        CargarCotizaciones()
    End Sub

    ''' <summary>
    ''' Baja lógica. El API marca la FechaCancelacion y deja de incluirla en el
    ''' listado; el registro sigue existiendo en la base.
    ''' </summary>
    Private Sub CancelarCotizacion(cotizacionId As Integer)

        If CotizacionesAceptadas Is Nothing Then CargarCotizacionesAceptadas()

        If CotizacionesAceptadas.Contains(cotizacionId) Then
            Avisar("Esta cotización ya tiene certificado: elimina primero el certificado.", "warning")
            Exit Sub
        End If

        Dim api As New ConsumoApi()
        Dim respuesta As String = api.DeleteCotizacion(cotizacionId)

        If String.IsNullOrWhiteSpace(respuesta) OrElse respuesta.StartsWith("ERROR") Then
            Avisar("No se pudo cancelar: " & DetalleDeError(respuesta), "danger")
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

        ' Red de seguridad: el icono ya viene apagado, pero un postback armado a
        ' mano llegaria aqui igual. El API tambien lo rechaza.
        If CotizacionesAceptadas Is Nothing Then CargarCotizacionesAceptadas()

        If CotizacionesAceptadas.Contains(cotizacionId) Then
            Avisar("Esta cotización ya tiene certificado, por lo que no se puede editar.", "warning")
            Exit Sub
        End If

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

        ' El tipo no se guarda como tal: se deduce de qué detalle trae la cotización.
        Dim esContenedor As Boolean = (c.CotizacionContenedor IsNot Nothing AndAlso
                                       c.CotizacionContenedor.Count > 0)

        Dim tipo As String = If(esContenedor, "Contenedor", "Mercancia")

        ' Va primero porque AplicarTipoCotizacion reconstruye el combo de pólizas
        ' según el tipo; si se hiciera después, borraría la póliza seleccionada.
        ddlTipoCotizacion.SelectedValue = tipo
        AplicarTipoCotizacion(tipo)

        ' No se permite cambiar el tipo de una cotización ya guardada: dejaría
        ' huérfano el detalle de mercancía o de contenedor que ya tiene.
        ' Un combo deshabilitado no viaja en el postback, pero conserva su valor en
        ' el ViewState, así que btnGuardar_Click lo sigue leyendo bien.
        ddlTipoCotizacion.Enabled = False

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

        ' Los importes se escriben en el bloque que corresponde al tipo, que es de
        ' donde los vuelve a leer btnGuardar_Click.
        LlenarImportes(esContenedor, c)

        ' Se repueblan bienes y contenedores ya guardados. Sin esto, al editar y
        ' guardar el API recibiría listas vacías y borraría lo que ya tenía.
        RepoblarBienes(c.BienCotizacion)
        RepoblarCoberturas(c.CoberturaCotizacion)
        RepoblarContenedores(c.CotizacionContenedor)

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

        ' Lo que se le cotizo al cliente manda sobre lo que diga hoy la poliza.
        ' CargarDesdePolizaId ya escribio los textos vivos; aqui se sustituyen
        ' por la copia que quedo guardada. Las cotizaciones anteriores a estas
        ' columnas no traen copia, y para esas se deja lo de la poliza.
        If Not String.IsNullOrWhiteSpace(m.CondicionesEspeciales) Then
            txtCondicionesEspeciales.Text = m.CondicionesEspeciales
        End If

        If Not String.IsNullOrWhiteSpace(m.Exclusiones) Then
            txtExclusiones.Text = m.Exclusiones
        End If
        txtSumaAsegurada.Text = TextoMonto(m.SumaAsegurada)
        txtCuotaAplicable.Text = TextoMonto(m.CuotaAplicable)
        txtCuotaMinima.Text = TextoMonto(m.CuotaMinima)
        txtTipoCambio.Text = TextoMonto(m.TipoCambioCotizar)

        MarcarChecksMoneda(chkCuotaAplicableN, chkCuotaAplicableI, m.MonedaCuotaAplicableId)
        MarcarChecksMoneda(chkCuotaMinimaN, chkCuotaMinimaI, m.MonedaCuotaMinimaId)
    End Sub

    ''' <summary>
    ''' Carga en la tabla los bienes que la cotización ya tenía guardados. Como esos
    ''' registros no conservan el BienId de la póliza, se usa el BienCotizacionId
    ''' como llave del renglón, que es para lo único que sirve ese campo aquí.
    ''' </summary>
    Private Sub RepoblarBienes(bienes As List(Of BienCotizacion))

        BienesCotizacion.Clear()

        If bienes IsNot Nothing Then
            For Each b As BienCotizacion In bienes
                BienesCotizacion.Add(New Bien With {
                    .BienId = b.BienCotizacionId,
                    .TipoBienId = b.TipoBienId,
                    .AdministracionBienId = b.AdministracionBienId,
                    .Nombre = b.Nombre
                })
            Next
        End If

        CargarGridBienes()
    End Sub

    ''' <summary>
    ''' Escribe prima, gastos, subtotal, IVA y total en el bloque del tipo elegido.
    ''' Mercancía y Contenedor tienen sus propios campos, y btnGuardar_Click lee los
    ''' del tipo seleccionado: escribirlos en el bloque equivocado los dejaría vacíos.
    ''' </summary>
    Private Sub LlenarImportes(esContenedor As Boolean, c As Cotizacion)

        Dim prima As TextBox = If(esContenedor, txtPrimaYSeguramiento2, txtPrimaYSeguramiento)
        Dim gastos As TextBox = If(esContenedor, txtGastosExpedicion2, txtGastosExpedicion)
        Dim subtotal As TextBox = If(esContenedor, txtSubtotal2, txtSubtotal)
        Dim iva As TextBox = If(esContenedor, txtIVA2, txtIVA)
        Dim total As TextBox = If(esContenedor, txtTotalPagar2, txtTotalPagar)

        prima.Text = TextoMonto(c.PrimaServicioDeAseguramiento)
        gastos.Text = TextoMonto(c.GastosExpedicion)
        subtotal.Text = TextoMonto(c.Subtotal)
        iva.Text = TextoMonto(c.IVA)
        total.Text = TextoMonto(c.Total)
    End Sub

    ''' <summary>
    ''' Carga en la tabla las coberturas que la cotización ya tenía guardadas. Como
    ''' se persisten sin su id de origen, se usa el CoberturaCotizacionId como llave
    ''' del renglón, que es para lo único que sirve ese campo aquí.
    ''' </summary>
    Private Sub RepoblarCoberturas(coberturas As List(Of CoberturaCotizacion))

        CoberturasCotizacion.Clear()

        If coberturas IsNot Nothing Then
            For Each c As CoberturaCotizacion In coberturas
                CoberturasCotizacion.Add(New Cobertura With {
                    .CoberturaId = c.CoberturaCotizacionId,
                    .Nombre = c.Nombre
                })
            Next
        End If

        CargarGridCoberturas()
    End Sub

    ''' <summary>
    ''' Deja la tabla de contenedores lista para que el JavaScript la repinte con
    ''' los renglones guardados, y ajusta el combo de unidades a esa cantidad.
    ''' </summary>
    Private Sub RepoblarContenedores(contenedores As List(Of CotizacionContenedor))

        If contenedores Is Nothing OrElse contenedores.Count = 0 Then
            hfContenedores.Value ="[]"
            Exit Sub
        End If

        SeleccionarValor(ddlUnidades, contenedores.Count)

        hfContenedores.Value =SerializarContenedores(contenedores)
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

    Private Sub MarcarChecksMoneda(nacional As HtmlInputCheckBox, internacional As HtmlInputCheckBox, monedaId As Integer?)
        nacional.Checked = (monedaId.GetValueOrDefault() = 1)
        internacional.Checked = (monedaId.GetValueOrDefault() = 2)
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

        ' Antes de pintar: se necesita saber cuáles ya tienen certificado para
        ' marcar la palomita.
        CargarCotizacionesAceptadas()

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

        ' Se llama desde Page_Load: si el API falla, ConsumoApi devuelve "ERROR: ..."
        ' y deserializarlo tumbaría la pantalla completa.
        Dim listaClientes As New List(Of Cliente)

        If Not String.IsNullOrWhiteSpace(cargarClientes) AndAlso
           cargarClientes <> "null" AndAlso
           Not cargarClientes.StartsWith("ERROR") Then

            listaClientes = JsonConvert.DeserializeObject(Of List(Of Cliente))(cargarClientes)
            If listaClientes Is Nothing Then listaClientes = New List(Of Cliente)
        End If

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

        ' Igual que cargarClientes: esto corre en Page_Load y no puede reventar.
        Dim listaPolizas As New List(Of Poliza)

        If Not String.IsNullOrWhiteSpace(json) AndAlso
           json <> "null" AndAlso
           Not json.StartsWith("ERROR") Then

            listaPolizas = JsonConvert.DeserializeObject(Of List(Of Poliza))(json)
            If listaPolizas Is Nothing Then listaPolizas = New List(Of Poliza)
        End If

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
        LimpiarCuotasContenedor()

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
        LlenarCuotasDelCliente(cliente)
    End Sub

    ''' <summary>
    ''' Vuelca al bloque "Cuota Aplicable Contenedor" las tarifas negociadas del
    ''' cliente. Es el inverso de cómo AdminCliente las captura: ahí se guardan con
    ''' TipoCuotaId 1, 2 y 3 según el bloque, y aquí se reparten con ese mismo id.
    '''
    ''' Estos campos no se guardan en la cotización —no existen en su contrato—, así
    ''' que siempre reflejan lo que el cliente tiene vigente.
    ''' </summary>
    Private Sub LlenarCuotasDelCliente(cliente As Cliente)

        LimpiarCuotasContenedor()

        If cliente.Cuota Is Nothing Then Exit Sub

        For Each c As Cuota In cliente.Cuota

            Select Case c.TipoCuotaId

                Case 1 ' Contenedores secos
                    txtCuotaSecos.Text = TextoMonto(c.Monto)
                    SeleccionarValor(ddlTipoTarifaSecos, c.TipoTarifaId)

                Case 2 ' Contenedores refrigerados
                    txtCuotaRefrigerados.Text = TextoMonto(c.Monto)
                    SeleccionarValor(ddlTipoRefrigerados, c.TipoTarifaId)

                Case 3 ' Isotanques
                    txtCuota2.Text = TextoMonto(c.Monto)
                    SeleccionarValor(ddlTipoIsotaques, c.TipoTarifaId)

            End Select
        Next
    End Sub

    Private Sub LimpiarCuotasContenedor()
        txtCuotaSecos.Text = String.Empty
        txtCuotaRefrigerados.Text = String.Empty
        txtCuota2.Text = String.Empty

        ResetearCombo(ddlTipoTarifaSecos)
        ResetearCombo(ddlTipoRefrigerados)
        ResetearCombo(ddlTipoIsotaques)
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

            ' Solo de lectura: son de la poliza y viajan tal cual al formato.
            txtCondicionesEspeciales.Text = mercancia.Especiales
            txtExclusiones.Text = mercancia.ExclusionesParticulares
        End If

        ' Los bienes registrados en la póliza alimentan dos combos: Subclasificación
        ' en la captura de mercancía y Bienes Asegurados en la de contenedor.
        BienesDePoliza = poliza.Bien

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

        ' Los riesgos cubiertos son los de la poliza: se copian todos al elegirla
        ' en vez de pedir que se agreguen uno por uno. El combo se queda para
        ' volver a agregar alguno que se haya quitado.
        CopiarCoberturasDePoliza()

        If ddlCoberturas.Items.Count > 0 Then
            ddlCoberturas.Items.Insert(0, New ListItem("-- Selecciona --", "0"))
        End If

    End Sub

    ''' <summary>
    ''' Pasa al grid todo lo que quedo en el combo. Se llama antes de meter el
    ''' "-- Selecciona --", asi que todos los items son coberturas reales.
    ''' </summary>
    Private Sub CopiarCoberturasDePoliza()

        For Each item As ListItem In ddlCoberturas.Items

            Dim coberturaId As Integer

            If Not Integer.TryParse(item.Value, coberturaId) OrElse coberturaId = 0 Then Continue For

            If CoberturasCotizacion.Any(Function(c) c.CoberturaId = coberturaId) Then Continue For

            CoberturasCotizacion.Add(New Cobertura With {
                .CoberturaId = coberturaId,
                .Nombre = item.Text
            })
        Next

        CargarGridCoberturas()
    End Sub
    Private Sub LimpiarDatosPoliza()
        ddlMoneda.ClearSelection()
        txtVigenciaDel.Text = String.Empty
        txtVigenciaHasta.Text = String.Empty
        txtDeducibles.Text = String.Empty
        txtCondicionesEspeciales.Text = String.Empty
        txtExclusiones.Text = String.Empty
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

        Dim esContenedor As Boolean = (ddlTipoCotizacion.SelectedValue = "Contenedor")

        ' Cada tipo de cotización captura sus importes en su propio bloque.
        ' Se recalculan aquí en vez de leer los campos calculados: esos son de solo
        ' lectura y su texto trae formato de moneda, así que el servidor no debe
        ' depender de que el JavaScript haya corrido.
        Dim prima As Decimal = MontoDe(If(esContenedor, txtPrimaYSeguramiento2, txtPrimaYSeguramiento)).GetValueOrDefault()
        Dim gastos As Decimal = MontoDe(If(esContenedor, txtGastosExpedicion2, txtGastosExpedicion)).GetValueOrDefault()
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
            .Total = subtotal + iva
        }

        ' El API exige mercancía o contenedor; se manda solo el que corresponde.
        If esContenedor Then
            Dim contenedores As List(Of CotizacionContenedor) = ArmarCotizacionContenedor()

            If contenedores.Count = 0 Then
                Avisar("Captura al menos un contenedor en la tabla.", "warning")
                Exit Sub
            End If

            cotizacion.CotizacionContenedor = contenedores
        Else
            cotizacion.CotizacionMercancia = ArmarCotizacionMercancia()
        End If

        cotizacion.BienCotizacion = ArmarBienCotizacion()
        cotizacion.CoberturaCotizacion = ArmarCoberturaCotizacion()

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
            Avisar("No se pudo guardar: " & DetalleDeError(respuesta), "danger")
            Exit Sub
        End If

        Avisar(If(esEdicion, "Cotización editada correctamente", "Cotización agregada correctamente"), "success")

        hfCotizacionId.Value = String.Empty

        CoberturasCotizacion.Clear()
        CargarGridCoberturas()

        BienesCotizacion.Clear()
        CargarGridBienes()

        CargarCotizaciones()

        VolverAlListado()
    End Sub


    ''' <summary>
    ''' Lee la tabla de contenedores desde Request.Form. Esa tabla la genera el
    ''' JavaScript con inputs HTML planos, no con controles de servidor, así que no
    ''' hay nada que consultar con .Text: los valores solo llegan por el POST.
    ''' </summary>
    Private Function ArmarCotizacionContenedor() As List(Of CotizacionContenedor)

        Dim lista As New List(Of CotizacionContenedor)

        Dim unidades As Integer
        Integer.TryParse(ddlUnidades.SelectedValue, unidades)

        For i As Integer = 1 To unidades

            Dim renglon As New CotizacionContenedor With {
                .NumeroContenedor = ValorForm("txtNumContenedor_" & i),
                .TipoContenedorId = EnteroForm("ddlTipoContenedor_" & i),
                .TamanioContendorId = EnteroForm("ddlTamanoContenedor_" & i),
                .Referencia = ValorForm("txtReferencia_" & i),
                .LR = DecimalForm("txtLR_" & i),
                .Cuota = DecimalForm("txtCuota_" & i),
                .TC = DecimalForm("txtTC_" & i),
                .PrimaUnitariaUSD = DecimalForm("txtPrimaUSD_" & i),
                .PrimaUnitariaMXN = DecimalForm("txtPrimaMXN_" & i)
            }

            renglon.Total = renglon.PrimaUnitariaUSD

            ' Los renglones que el usuario dejó en blanco no se mandan.
            If Not String.IsNullOrWhiteSpace(renglon.NumeroContenedor) OrElse
               renglon.TipoContenedorId.HasValue OrElse
               renglon.TamanioContendorId.HasValue OrElse
               renglon.LR.HasValue OrElse
               renglon.Cuota.HasValue Then

                lista.Add(renglon)
            End If
        Next

        Return lista
    End Function

    ''' <summary>
    ''' Deja en hfContenedores los renglones que el JavaScript debe repintar.
    ''' En un postback normal son los que el usuario acaba de capturar, para que la
    ''' tabla no se vacíe al regenerarse; al abrir una cotización para editar,
    ''' LlenarDesdeCotizacion los reemplaza por los que están guardados.
    ''' </summary>
    Private Sub PrepararContenedoresJson()
        If Not IsPostBack Then Exit Sub

        hfContenedores.Value =SerializarContenedores(ArmarCotizacionContenedor())
    End Sub

    Private Function SerializarContenedores(lista As List(Of CotizacionContenedor)) As String

        If lista Is Nothing OrElse lista.Count = 0 Then Return "[]"

        Dim proyectada = lista.Select(Function(c) New With {
            .numero = c.NumeroContenedor,
            .tipoId = c.TipoContenedorId,
            .tamanioId = c.TamanioContendorId,
            .lr = c.LR,
            .referencia = c.Referencia,
            .cuota = c.Cuota,
            .tc = c.TC,
            .primaUSD = c.PrimaUnitariaUSD,
            .primaMXN = c.PrimaUnitariaMXN
        })

        Return JsonConvert.SerializeObject(proyectada)
    End Function

    ''' <summary>
    ''' Convierte los bienes de la tabla al formato que espera el API. No se manda
    ''' el BienId: la cotización guarda una copia del bien, no una referencia.
    ''' Devuelve Nothing cuando no hay ninguno, porque el PUT interpreta una lista
    ''' vacía como "borra los que ya tenía".
    ''' </summary>
    Private Function ArmarBienCotizacion() As List(Of BienCotizacion)

        If BienesCotizacion.Count = 0 Then Return Nothing

        Return BienesCotizacion.
            Select(Function(b) New BienCotizacion With {
                .TipoBienId = b.TipoBienId,
                .AdministracionBienId = b.AdministracionBienId,
                .Nombre = b.Nombre
            }).
            ToList()
    End Function

    ''' <summary>
    ''' Convierte las coberturas de la tabla al formato del API. Solo se guarda el
    ''' nombre: el origen puede ser una Cobertura o un RiesgoCubierto según el tipo
    ''' de póliza, así que se persiste como copia igual que los bienes.
    ''' Devuelve Nothing cuando no hay ninguna, porque el PUT interpreta la lista
    ''' vacía como "borra las que ya tenía".
    ''' </summary>
    Private Function ArmarCoberturaCotizacion() As List(Of CoberturaCotizacion)

        If CoberturasCotizacion.Count = 0 Then Return Nothing

        Return CoberturasCotizacion.
            Select(Function(c) New CoberturaCotizacion With {
                .Nombre = c.Nombre
            }).
            ToList()
    End Function

    Private Function ValorForm(nombre As String) As String
        Return If(Request.Form(nombre), String.Empty).Trim()
    End Function

    Private Function EnteroForm(nombre As String) As Integer?
        Dim texto As String = ValorForm(nombre)
        If texto.Length = 0 Then Return Nothing

        Dim valor As Integer
        If Integer.TryParse(texto, valor) Then Return valor

        Return Nothing
    End Function

    Private Function DecimalForm(nombre As String) As Decimal?
        Dim texto As String = ValorForm(nombre)
        If texto.Length = 0 Then Return Nothing

        Dim limpio As String = Text.RegularExpressions.Regex.Replace(texto, "[^0-9.\-]", "")

        Dim valor As Decimal
        If Decimal.TryParse(limpio, Globalization.NumberStyles.Any, Globalization.CultureInfo.InvariantCulture, valor) Then
            Return valor
        End If

        Return Nothing
    End Function

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
            .CondicionesEspeciales = txtCondicionesEspeciales.Text,
            .Exclusiones = txtExclusiones.Text,
            .SumaAsegurada = MontoDe(txtSumaAsegurada),
            .CuotaAplicable = MontoDe(txtCuotaAplicable),
            .CuotaMinima = MontoDe(txtCuotaMinima),
            .TipoCambioCotizar = MontoDe(txtTipoCambio),
            .MonedaCuotaAplicableId = MonedaDeCheck(chkCuotaAplicableN, chkCuotaAplicableI),
            .MonedaCuotaMinimaId = MonedaDeCheck(chkCuotaMinimaN, chkCuotaMinimaI),
            .MonedaCotizarId = IdSeleccionado(ddlMoneda)
        }
    End Function

    ''' <summary>
    ''' Devuelve Nothing cuando no hay check marcado. Un 0 se guardaría como id de
    ''' moneda inexistente; la columna acepta nulos y eso es lo que corresponde.
    ''' </summary>
    Private Function MonedaDeCheck(nacional As HtmlInputCheckBox, internacional As HtmlInputCheckBox) As Integer?
        If nacional IsNot Nothing AndAlso nacional.Checked Then Return 1
        If internacional IsNot Nothing AndAlso internacional.Checked Then Return 2
        Return Nothing
    End Function

    ''' <summary>
    ''' Saca el motivo de un "ERROR: ..." de ConsumoApi sin dar por hecho el largo
    ''' del prefijo. Si no trae dos puntos, regresa el texto completo.
    ''' </summary>
    Private Function DetalleDeError(respuesta As String) As String
        If String.IsNullOrWhiteSpace(respuesta) Then Return "sin respuesta del servidor"

        Dim separador As Integer = respuesta.IndexOf(":"c)
        If separador < 0 Then Return respuesta.Trim()

        Return respuesta.Substring(separador + 1).Trim()
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
