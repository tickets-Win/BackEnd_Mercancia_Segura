Imports Newtonsoft.Json
Imports WebAdmin.MercanciaSegura.DOM.Modelos

Public Class AdminCertificados
    Inherits System.Web.UI.Page

    ''' <summary>
    ''' Fila del listado. El certificado no trae clave en todos los casos, así que
    ''' se calcula aquí una para mostrar.
    ''' </summary>
    Public Class RenglonCertificado
        Public Property CertificadoId As Integer
        Public Property Clave As String
        Public Property NombreCliente As String
        Public Property NombreEstatus As String
        Public Property FechaCotizacion As DateTime?
        Public Property TipoCotizacion As String
    End Class

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ' El aviso se apaga al empezar cada petición. Page_Load corre antes que
        ' los eventos, así que un Avisar() de este mismo clic sí se alcanza a ver;
        ' lo que ya no se queda pegado es el del clic anterior.
        pnlAviso.Visible = False

        ' Necesario para que suban los adjuntos del correo. ASP.NET solo pone
        ' este atributo si ve un FileUpload al renderizar, y el panel del correo
        ' nace oculto; cuando se muestra ya es por postback parcial y la
        ' etiqueta <form> vive fuera del UpdatePanel, asi que no se redibuja.
        Me.Form.Enctype = "multipart/form-data"

        If Not IsPostBack Then
            CargarCertificados()
        End If
    End Sub

#Region "Listado"

    Protected Sub CargarCertificados()

        Dim api As New ConsumoApi()
        Dim json As String = api.GetCertificados()

        Dim lista As New List(Of Certificado)

        If Not String.IsNullOrWhiteSpace(json) AndAlso
           json <> "null" AndAlso
           Not json.StartsWith("ERROR") Then

            lista = JsonConvert.DeserializeObject(Of List(Of Certificado))(json)
            If lista Is Nothing Then lista = New List(Of Certificado)
        Else
            Avisar("No se pudieron cargar los certificados.", "danger")
        End If

        Dim busqueda As String = txtBuscarCertificado.Text.Trim()

        If busqueda.Length > 0 Then
            lista = lista.Where(Function(c) CoincideBusqueda(c, busqueda)).ToList()
        End If

        lista = AplicarFiltroPeriodo(lista)

        Dim renglones As List(Of RenglonCertificado) =
            lista.Select(Function(c) New RenglonCertificado With {
                .CertificadoId = c.CertificadoId,
                .Clave = ClaveDe(c),
                .NombreCliente = c.NombreCliente,
                .NombreEstatus = c.NombreEstatus,
                .FechaCotizacion = c.FechaCotizacion,
                .TipoCotizacion = c.TipoCotizacion
            }).ToList()

        ' Al filtrar, la página en la que estabas puede dejar de existir.
        Dim ultimaPagina As Integer = 0

        If renglones.Count > 0 Then
            ultimaPagina = CInt(Math.Ceiling(renglones.Count / CDbl(gvCertificados.PageSize))) - 1
        End If

        If gvCertificados.PageIndex > ultimaPagina Then
            gvCertificados.PageIndex = ultimaPagina
        End If

        gvCertificados.DataSource = renglones
        gvCertificados.DataBind()
    End Sub

    ''' <summary>
    ''' La clave la genera el API al crear el certificado. Aquí solo se muestra;
    ''' si algún registro viejo no la trae, se marca en vez de inventar una.
    ''' </summary>
    Private Function ClaveDe(c As Certificado) As String
        If String.IsNullOrWhiteSpace(c.ClaveCertificado) Then Return "(sin clave)"

        Return c.ClaveCertificado
    End Function

    Private Function CoincideBusqueda(c As Certificado, busqueda As String) As Boolean
        Return Contiene(ClaveDe(c), busqueda) OrElse
               Contiene(c.NombreCliente, busqueda) OrElse
               Contiene(c.Asegurado, busqueda) OrElse
               Contiene(c.NombreEstatus, busqueda) OrElse
               Contiene(c.NumeroPoliza, busqueda) OrElse
               Contiene(c.TipoCotizacion, busqueda) OrElse
               Contiene(c.CotizacionId.ToString(), busqueda)
    End Function

    ''' <summary>
    ''' Comparación que ignora mayúsculas y acentos.
    ''' </summary>
    Private Function Contiene(valor As String, busqueda As String) As Boolean
        If String.IsNullOrEmpty(valor) Then Return False

        Return Globalization.CultureInfo.InvariantCulture.CompareInfo.IndexOf(
            valor, busqueda,
            Globalization.CompareOptions.IgnoreCase Or Globalization.CompareOptions.IgnoreNonSpace) >= 0
    End Function

    ''' <summary>
    ''' Filtra por la fecha de registro del certificado.
    ''' </summary>
    Private Function AplicarFiltroPeriodo(lista As List(Of Certificado)) As List(Of Certificado)

        Dim hoy As Date = Date.Today

        Select Case ddlPeriodo.SelectedValue

            Case "1"    ' Hoy
                Return lista.Where(Function(c) c.FechaRegistro.Date = hoy).ToList()

            Case "2"    ' Mes actual
                Return lista.Where(Function(c) c.FechaRegistro.Year = hoy.Year AndAlso
                                               c.FechaRegistro.Month = hoy.Month).ToList()

            Case "3"    ' Mes anterior
                Dim mesAnterior As Date = hoy.AddMonths(-1)

                Return lista.Where(Function(c) c.FechaRegistro.Year = mesAnterior.Year AndAlso
                                               c.FechaRegistro.Month = mesAnterior.Month).ToList()

            Case Else
                Return lista
        End Select
    End Function

    Protected Sub txtBuscarCertificado_TextChanged(sender As Object, e As EventArgs)
        ' Una búsqueda nueva siempre arranca en la primera página.
        gvCertificados.PageIndex = 0

        CargarCertificados()
    End Sub

    Protected Sub ddlPeriodo_SelectedIndexChanged(sender As Object, e As EventArgs)
        gvCertificados.PageIndex = 0

        CargarCertificados()
    End Sub

    Protected Sub gvCertificados_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        gvCertificados.PageIndex = e.NewPageIndex

        CargarCertificados()
    End Sub

#End Region

#Region "Acciones"

    Protected Sub gvCertificados_RowCommand(sender As Object, e As GridViewCommandEventArgs)

        Dim certificadoId As Integer
        If Not Integer.TryParse(Convert.ToString(e.CommandArgument), certificadoId) Then Exit Sub

        Select Case e.CommandName
            Case "Detalle"
                VerDetalle(certificadoId)
            Case "Correo"
                AbrirCorreo(certificadoId)
            Case "Eliminar"
                EliminarCertificado(certificadoId)
        End Select
    End Sub

    Private Sub VerDetalle(certificadoId As Integer)

        Dim api As New ConsumoApi()
        Dim json As String = api.GetCertificadoId(certificadoId)

        If String.IsNullOrWhiteSpace(json) OrElse json.StartsWith("ERROR") Then
            Avisar("No se pudo consultar el certificado.", "danger")
            Exit Sub
        End If

        Dim c As Certificado = JsonConvert.DeserializeObject(Of Certificado)(json)

        If c Is Nothing Then
            Avisar("No se pudo consultar el certificado.", "danger")
            Exit Sub
        End If

        txtClave.Text = ClaveDe(c)
        txtEstatus.Text = c.NombreEstatus
        txtFechaRegistro.Text = c.FechaRegistro.ToString("dd/MM/yyyy")
        txtAsegurado.Text = c.Asegurado
        txtSumaAsegurada.Text = c.SumaAsegurada.ToString("C2")
        txtVigenciaDel.Text = c.FechaInicio.ToString("dd/MM/yyyy")
        txtVigenciaHasta.Text = c.FechaFin.ToString("dd/MM/yyyy")

        LlenarDetalleCotizacion(c)

        pnlListado.Visible = False
        pnlDetalle.Visible = True
    End Sub

    ''' <summary>
    ''' Trae la cotización que originó el certificado y la muestra completa, con
    ''' los mismos campos que la pantalla de cotizaciones pero de solo lectura.
    ''' </summary>
    Private Sub LlenarDetalleCotizacion(cert As Certificado)

        txtCotizacionId.Text = cert.CotizacionId.ToString()
        txtTipo.Text = cert.TipoCotizacion
        txtFechaCotizacion.Text = FechaCorta(cert.FechaCotizacion)
        txtNumeroPoliza.Text = cert.NumeroPoliza
        txtCliente.Text = cert.NombreCliente
        txtTotal.Text = Importe(cert.Total)

        Dim api As New ConsumoApi()
        Dim json As String = api.GetCotizacionId(cert.CotizacionId)

        If String.IsNullOrWhiteSpace(json) OrElse json.StartsWith("ERROR") Then
            Avisar("El certificado se cargó, pero no se pudo traer el detalle de la cotización.", "warning")
            Exit Sub
        End If

        Dim cot As Cotizacion = JsonConvert.DeserializeObject(Of Cotizacion)(json)
        If cot Is Nothing Then Exit Sub

        ' Encabezado
        txtMoneda.Text = NombreDeCatalogo(api.GetMoneda(), cot.MonedaId, "monedaId", "tipo")
        txtCotVigenciaDel.Text = FechaCorta(cot.VigenciaDel)
        txtCotVigenciaHasta.Text = FechaCorta(cot.VigenciaHasta)

        ' Importes
        txtPrima.Text = Importe(cot.PrimaServicioDeAseguramiento)
        txtGastosExpedicion.Text = Importe(cot.GastosExpedicion)
        txtSubtotal.Text = Importe(cot.Subtotal)
        txtIVA.Text = Importe(cot.IVA)
        txtTotal.Text = Importe(cot.Total)

        Dim esContenedor As Boolean = (cot.CotizacionMercancia Is Nothing)

        pnlDetalleMercancia.Visible = Not esContenedor
        pnlDetalleContenedor.Visible = esContenedor

        If Not esContenedor Then
            LlenarMercancia(api, cot.CotizacionMercancia)
        Else
            LlenarContenedores(api, cot.CotizacionContenedor)
        End If

        gvBienes.DataSource = If(cot.BienCotizacion, New List(Of BienCotizacion))
        gvBienes.DataBind()

        gvCoberturas.DataSource = If(cot.CoberturaCotizacion, New List(Of CoberturaCotizacion))
        gvCoberturas.DataBind()
    End Sub

    Private Sub LlenarMercancia(api As ConsumoApi, m As CotizacionMercancia)

        If m Is Nothing Then Exit Sub

        txtTransito.Text = NombreDeCatalogo(api.GetTransito(), m.TransitoId, "transitoId", "nombre")
        txtClasificacion.Text = NombreDeCatalogo(api.GetClasificacion(), m.ClasificacionId, "clasificacionId", "nombre")
        txtSubclasificacion.Text = m.SubClasificacion
        txtDescripcionMercancia.Text = m.DescripcionMercancia
        txtTipoEmpaque.Text = m.TipoEmpaque
        txtOrigen.Text = m.Origen
        txtDestino.Text = m.Destino
        txtMediosConduccion.Text = m.MedioDeConduccion
        txtMedioTransporte.Text = m.MedioDeTransporte
        txtObservaciones.Text = m.Observaciones

        txtMedidasSeguridad.Text = m.MedidasDeSeguridadAdicionales
        txtDeducibles.Text = m.Deducibles

        txtCuotaAplicable.Text = Importe(m.CuotaAplicable)
        txtCuotaMinima.Text = Importe(m.CuotaMinima)
        txtTipoCambio.Text = Importe(m.TipoCambioCotizar)
        txtCotSumaAsegurada.Text = Importe(m.SumaAsegurada)
    End Sub

    ''' <summary>
    ''' Renglón de la tabla de contenedores. Tipo y tamaño se guardan como id, así
    ''' que hay que resolverlos contra sus catálogos para mostrarlos.
    ''' </summary>
    Public Class RenglonContenedor
        Public Property NumeroContenedor As String
        Public Property Tipo As String
        Public Property Tamanio As String
        Public Property Referencia As String
        Public Property Cuota As Decimal?
        Public Property LR As Decimal?
        Public Property TC As Decimal?
        Public Property PrimaUnitariaUSD As Decimal?
        Public Property PrimaUnitariaMXN As Decimal?
        Public Property Total As Decimal?
    End Class

    Private Sub LlenarContenedores(api As ConsumoApi, lista As List(Of CotizacionContenedor))

        Dim contenedores As List(Of CotizacionContenedor) =
            If(lista, New List(Of CotizacionContenedor))

        Dim tipos As String = api.GetTipoContenedor()
        Dim tamanios As String = api.GetTamanioContenedor()

        Dim renglones As List(Of RenglonContenedor) =
            contenedores.Select(Function(c) New RenglonContenedor With {
                .NumeroContenedor = c.NumeroContenedor,
                .Tipo = NombreDeCatalogo(tipos, c.TipoContenedorId, "tipoContenedorId", "tipo"),
                .Tamanio = NombreDeCatalogo(tamanios, c.TamanioContendorId, "tamanioContenedorId", "tamanio"),
                .Referencia = c.Referencia,
                .Cuota = c.Cuota,
                .LR = c.LR,
                .TC = c.TC,
                .PrimaUnitariaUSD = c.PrimaUnitariaUSD,
                .PrimaUnitariaMXN = c.PrimaUnitariaMXN,
                .Total = c.Total
            }).ToList()

        gvContenedores.DataSource = renglones
        gvContenedores.DataBind()
    End Sub

    ''' <summary>
    ''' Resuelve el nombre de un id contra el JSON de un catálogo. Se busca por
    ''' nombre de propiedad porque cada catálogo del API los llama distinto.
    ''' </summary>
    Private Function NombreDeCatalogo(json As String, id As Integer?, campoId As String, campoNombre As String) As String

        If Not id.HasValue OrElse id.Value = 0 Then Return String.Empty

        If String.IsNullOrWhiteSpace(json) OrElse
           json = "null" OrElse
           json.StartsWith("ERROR") Then Return id.Value.ToString()

        Try
            Dim lista As Newtonsoft.Json.Linq.JArray = Newtonsoft.Json.Linq.JArray.Parse(json)

            For Each item As Newtonsoft.Json.Linq.JObject In lista
                Dim valor = item.GetValue(campoId, StringComparison.OrdinalIgnoreCase)

                Dim leido As Integer

                If valor IsNot Nothing AndAlso
                   Integer.TryParse(valor.ToString(), leido) AndAlso
                   leido = id.Value Then

                    Dim nombre = item.GetValue(campoNombre, StringComparison.OrdinalIgnoreCase)

                    If nombre IsNot Nothing Then Return nombre.ToString()
                End If
            Next

        Catch
            ' Si el catálogo no viene como se espera, mejor mostrar el id que romper.
        End Try

        Return id.Value.ToString()
    End Function

    Private Function FechaCorta(fecha As DateTime?) As String
        If Not fecha.HasValue Then Return String.Empty

        Return fecha.Value.ToString("dd/MM/yyyy")
    End Function

    Private Function Importe(valor As Decimal?) As String
        If Not valor.HasValue Then Return String.Empty

        Return valor.Value.ToString("C2")
    End Function

#Region "Envio de correo"

    ''' <summary>
    ''' Abre el control de correo con los datos de ese certificado: los
    ''' destinatarios del cliente y los valores con los que se resuelven los
    ''' {{campos}} de la plantilla.
    ''' </summary>
    Private Sub AbrirCorreo(certificadoId As Integer)

        Dim cert As Certificado = TraerCertificado(certificadoId)

        If cert Is Nothing Then
            Avisar("No se pudo consultar el certificado.", "danger")
            Exit Sub
        End If

        ' La cotizacion es la que trae al cliente: el certificado solo guarda su
        ' id, y de ahi cuelgan correo, RFC y telefono.
        Dim clienteId As Integer = ClienteDelCertificado(cert)

        ucCorreo.Abrir(String.Join("; ", ListaDeCorreos(clienteId)),
                       ValoresDeCampos(cert, clienteId))

        pnlListado.Visible = False
        pnlDetalle.Visible = False
    End Sub

    Protected Sub ucCorreo_Cancelado(sender As Object, e As EventArgs)
        VolverAlListado()
    End Sub

    Protected Sub ucCorreo_Enviado(sender As Object, e As EventArgs)
        Avisar("Correo enviado correctamente.", "success")

        VolverAlListado()
    End Sub

    Private Sub VolverAlListado()

        pnlDetalle.Visible = False
        pnlListado.Visible = True

        CargarCertificados()
    End Sub

    Private Function TraerCertificado(certificadoId As Integer) As Certificado

        Dim api As New ConsumoApi()
        Dim json As String = api.GetCertificadoId(certificadoId)

        If String.IsNullOrWhiteSpace(json) OrElse
           json = "null" OrElse
           json.StartsWith("ERROR") Then Return Nothing

        Try
            Return JsonConvert.DeserializeObject(Of Certificado)(json)
        Catch
            Return Nothing
        End Try
    End Function

    Private Function ClienteDelCertificado(cert As Certificado) As Integer

        If cert.CotizacionId <= 0 Then Return 0

        Dim api As New ConsumoApi()
        Dim json As String = api.GetCotizacionId(cert.CotizacionId)

        If String.IsNullOrWhiteSpace(json) OrElse
           json = "null" OrElse
           json.StartsWith("ERROR") Then Return 0

        Try
            Dim cot As Cotizacion = JsonConvert.DeserializeObject(Of Cotizacion)(json)

            If cot Is Nothing Then Return 0

            Return cot.ClienteId
        Catch
            Return 0
        End Try
    End Function

    ''' <summary>
    ''' Valores conocidos para los {{campos}}. Lo que no se puede resolver se
    ''' deja fuera a proposito: el campo se queda visible en el texto y el
    ''' control avisa antes de enviar.
    ''' </summary>
    Private Function ValoresDeCampos(cert As Certificado, clienteId As Integer) As Dictionary(Of String, String)

        Dim valores As New Dictionary(Of String, String) From {
            {"FechaActual", Date.Today.ToString("dd/MM/yyyy")}
        }

        Agregar(valores, "Certificado", ClaveDe(cert))
        Agregar(valores, "Póliza", cert.NumeroPoliza)

        ' El asegurado es el nombre que quedo impreso en el certificado; si
        ' viniera vacio se cae al nombre del cliente del listado.
        Dim nombre As String = If(String.IsNullOrWhiteSpace(cert.Asegurado),
                                  cert.NombreCliente, cert.Asegurado)

        If Not String.IsNullOrWhiteSpace(nombre) Then
            valores("Nombre Completo") = nombre
            valores("Nombre") = nombre.Trim().Split(" "c)(0)
        End If

        Dim cliente As Cliente = TraerCliente(clienteId)

        If cliente IsNot Nothing Then
            Agregar(valores, "RFC", cliente.Rfc)
            Agregar(valores, "Teléfono", cliente.Telefono)
        End If

        Dim correos = ListaDeCorreos(clienteId)

        If correos.Count > 0 Then valores("Correo") = correos(0)

        Return valores
    End Function

    Private Sub Agregar(valores As Dictionary(Of String, String), campo As String, valor As String)

        If String.IsNullOrWhiteSpace(valor) Then Exit Sub

        valores(campo) = valor.Trim()
    End Sub

    Private Function TraerCliente(clienteId As Integer) As Cliente

        If clienteId <= 0 Then Return Nothing

        Dim api As New ConsumoApi()
        Dim json As String = api.GetClienteId(clienteId)

        If String.IsNullOrWhiteSpace(json) OrElse
           json = "null" OrElse
           json.StartsWith("ERROR") Then Return Nothing

        Try
            Return JsonConvert.DeserializeObject(Of Cliente)(json)
        Catch
            Return Nothing
        End Try
    End Function

    ''' <summary>Correos del cliente, sin repetidos.</summary>
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
            ' Si el catalogo no viene como se espera, se deja vacio el destinatario.
        End Try

        Return correos
    End Function

#End Region

    Protected Sub lnkVolver_Click(sender As Object, e As EventArgs)
        VolverAlListado()
    End Sub

    ''' <summary>
    ''' Baja física. El API rechaza el borrado si el certificado tiene siniestros
    ''' o endosos colgando.
    ''' </summary>
    Private Sub EliminarCertificado(certificadoId As Integer)

        Dim api As New ConsumoApi()
        Dim respuesta As String = api.DeleteCertificado(certificadoId)

        If String.IsNullOrWhiteSpace(respuesta) OrElse respuesta.StartsWith("ERROR") Then
            Avisar("No se pudo eliminar: " & DetalleDeError(respuesta), "danger")
            Exit Sub
        End If

        Avisar("Certificado eliminado correctamente", "success")

        CargarCertificados()
    End Sub

#End Region

#Region "Utilerias"

    ''' <summary>
    ''' Saca el motivo de un "ERROR: ..." de ConsumoApi sin dar por hecho el largo
    ''' del prefijo.
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
