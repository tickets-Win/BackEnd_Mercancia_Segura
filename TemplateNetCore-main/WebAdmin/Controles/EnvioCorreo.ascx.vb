Imports Newtonsoft.Json
Imports WebAdmin.MercanciaSegura.DOM.Modelos

''' <summary>
''' Pantalla de envío de correo reutilizable. La página que lo usa lo coloca
''' dentro de su UpdatePanel, llama a Abrir() y atiende los eventos Cancelado y
''' Enviado para volver a mostrar su propio listado.
''' </summary>
Public Class EnvioCorreoControl
    Inherits System.Web.UI.UserControl

    ''' <summary>Se cancela el envío o se regresa con la flecha.</summary>
    Public Event Cancelado As EventHandler

    ''' <summary>El correo salió correctamente.</summary>
    Public Event Enviado As EventHandler

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        ' El aviso se apaga al empezar cada petición: Page_Load corre antes que
        ' los eventos, así que un Avisar() de este mismo clic sí se ve.
        pnlAviso.Visible = False

        ' Los adjuntos suben contra el handler, no por postback, asi que ya no
        ' hace falta ni RegisterPostBackControl ni el enctype del formulario.
        ' El script necesita saber en que llave de Session guardarlos.
        hfLlaveAdjuntos.Value = LlaveAdjuntos
    End Sub

    ''' <summary>
    ''' Llave de Session donde viven los adjuntos. Incluye el id del control para
    ''' que dos pantallas no se pisen.
    ''' </summary>
    Private ReadOnly Property LlaveAdjuntos As String
        Get
            Return "AdjuntosCorreo_" & UniqueID
        End Get
    End Property

#Region "Estado"

    ''' <summary>
    ''' Valores para sustituir los {{campos}} de la plantilla. Los pone la página
    ''' que abre el control, porque cada módulo sabe de dónde saca sus datos.
    ''' </summary>
    Private Property ValoresCampos As Dictionary(Of String, String)
        Get
            Dim guardado As String = TryCast(ViewState("ValoresCampos"), String)

            If String.IsNullOrEmpty(guardado) Then Return New Dictionary(Of String, String)

            Return JsonConvert.DeserializeObject(Of Dictionary(Of String, String))(guardado)
        End Get
        Set(value As Dictionary(Of String, String))
            ViewState("ValoresCampos") = JsonConvert.SerializeObject(value)
        End Set
    End Property

    ''' <summary>
    ''' Cuerpo en HTML. Viaja en base64 dentro del hidden: en claro, la validación
    ''' de peticiones de ASP.NET lo tomaría por un intento de XSS y tumbaría la
    ''' página. Apagar esa validación expondría todo el formulario, así que se
    ''' prefiere codificar solo este campo.
    ''' </summary>
    Private Property Cuerpo As String
        Get
            Return edCuerpo.Html
        End Get
        Set(value As String)
            edCuerpo.Html = value
        End Set
    End Property

    ''' <summary>
    ''' Adjuntos de la captura en curso. Van en Session porque se cargan en un
    ''' postback y se mandan en otro. La llave incluye el id del control para que
    ''' dos pantallas no se pisen.
    ''' </summary>
    Private ReadOnly Property Adjuntos As List(Of ArchivoAdjunto)
        Get
            If Session(LlaveAdjuntos) Is Nothing Then
                Session(LlaveAdjuntos) = New List(Of ArchivoAdjunto)
            End If

            Return DirectCast(Session(LlaveAdjuntos), List(Of ArchivoAdjunto))
        End Get
    End Property

#End Region

#Region "Apertura"

    ''' <summary>
    ''' Deja la pantalla lista y visible.
    ''' </summary>
    ''' <param name="destinatarios">Correos para el campo Para, separados por ";".</param>
    ''' <param name="valores">Valores con los que se resuelven los {{campos}}.</param>
    Public Sub Abrir(destinatarios As String, valores As Dictionary(Of String, String))

        If valores Is Nothing Then valores = New Dictionary(Of String, String)

        ' La fecha siempre se puede resolver.
        If Not valores.ContainsKey("FechaActual") Then
            valores("FechaActual") = Date.Today.ToString("dd/MM/yyyy")
        End If

        ValoresCampos = valores

        Adjuntos.Clear()

        Dim plantillas = PlantillasCorreo.Todas()

        ddlPlantilla.Items.Clear()
        ddlPlantilla.Items.Add(New ListItem("-- Sin plantilla --", ""))

        For Each p In plantillas
            ddlPlantilla.Items.Add(New ListItem(p.Nombre, p.PlantillaCorreoId.ToString()))
        Next

        ddlCuenta.Items.Clear()
        ddlCuenta.Items.Add(New ListItem("Seleccione una cuenta", ""))

        For Each cuenta As String In EnvioCorreo.CuentasRemitentes()
            ddlCuenta.Items.Add(New ListItem(cuenta, cuenta))
        Next

        rptCampos.DataSource = PlantillasCorreo.Campos
        rptCampos.DataBind()

        Dim biblioteca = PlantillasCorreo.Biblioteca()

        rptBiblioteca.DataSource = biblioteca.ToList()
        rptBiblioteca.DataBind()

        hfCampos.Value = JsonConvert.SerializeObject(
            PlantillasCorreo.Campos.Select(Function(c) "{{" & c & "}}").ToList())

        hfBiblioteca.Value = JsonConvert.SerializeObject(biblioteca.Values.ToList())

        txtPara.Text = destinatarios
        txtCC.Text = String.Empty
        txtCCO.Text = String.Empty
        chkConfirmacion.Checked = False
        chkFirma.Checked = False

        AplicarPlantilla()

        If Not EnvioCorreo.EstaConfigurado() Then
            Avisar("Puedes preparar el correo, pero falta configurar el servidor SMTP en el Web.config para poder enviarlo.", "warning")
        End If

        Me.Visible = True
    End Sub

    Private Sub AplicarPlantilla()

        Dim id As Integer

        ' Sin plantilla elegida se deja el correo en blanco para redactarlo libre.
        If Not Integer.TryParse(ddlPlantilla.SelectedValue, id) Then
            txtAsunto.Text = String.Empty
            Cuerpo = String.Empty
            Exit Sub
        End If

        Dim plantilla = PlantillasCorreo.PorId(id)
        If plantilla Is Nothing Then Exit Sub

        Dim valores = ValoresCampos

        txtAsunto.Text = PlantillasCorreo.Resolver(plantilla.Asunto, valores)

        ' El cuerpo de la plantilla ya viene en HTML desde el editor.
        Cuerpo = PlantillasCorreo.Resolver(plantilla.CuerpoHtml, valores)
    End Sub

    Protected Sub ddlPlantilla_SelectedIndexChanged(sender As Object, e As EventArgs)
        AplicarPlantilla()
    End Sub

#End Region

#Region "Adjuntos"

    ' La carga y el borrado de adjuntos los atiende Handlers/AdjuntosCorreo.ashx
    ' contra la misma llave de Session, sin postback. Aqui solo se limpian al
    ' abrir y al enviar.

#End Region

#Region "Envio"

    Protected Sub lnkEnviar_Click(sender As Object, e As EventArgs)

        If String.IsNullOrWhiteSpace(txtPara.Text) Then
            Avisar("Captura al menos un destinatario.", "warning")
            Exit Sub
        End If

        Dim invalidas As New List(Of String)
        invalidas.AddRange(EnvioCorreo.DireccionesInvalidas(txtPara.Text))
        invalidas.AddRange(EnvioCorreo.DireccionesInvalidas(txtCC.Text))
        invalidas.AddRange(EnvioCorreo.DireccionesInvalidas(txtCCO.Text))

        If invalidas.Count > 0 Then
            Avisar("Estas direcciones no son válidas: " & String.Join(", ", invalidas), "warning")
            Exit Sub
        End If

        If String.IsNullOrWhiteSpace(txtAsunto.Text) Then
            Avisar("Captura el asunto del correo.", "warning")
            Exit Sub
        End If

        Dim cuerpoHtml As String = Cuerpo

        If String.IsNullOrWhiteSpace(HtmlATexto(cuerpoHtml).Trim()) Then
            Avisar("El correo va sin contenido.", "warning")
            Exit Sub
        End If

        Dim pendientes = PlantillasCorreo.CamposSinResolver(HtmlATexto(cuerpoHtml) & " " & txtAsunto.Text)

        If pendientes.Count > 0 Then
            Avisar("Faltan datos por sustituir en el correo: " & String.Join(", ", pendientes) &
                   ". Reemplázalos antes de enviar.", "warning")
            Exit Sub
        End If

        If chkFirma.Checked Then
            cuerpoHtml &= "<p>--<br />Mercancía Segura</p>"
        End If

        Dim falla As String = EnvioCorreo.Enviar(
            ddlCuenta.SelectedValue,
            txtPara.Text,
            txtCC.Text,
            txtCCO.Text,
            txtAsunto.Text,
            cuerpoHtml,
            chkConfirmacion.Checked,
            esHtml:=True,
            adjuntos:=Adjuntos)

        If Not String.IsNullOrEmpty(falla) Then
            Avisar("No se pudo enviar el correo: " & falla, "danger")
            Exit Sub
        End If

        Adjuntos.Clear()

        Me.Visible = False

        RaiseEvent Enviado(Me, EventArgs.Empty)
    End Sub

    Protected Sub lnkCancelar_Click(sender As Object, e As EventArgs)
        Me.Visible = False

        RaiseEvent Cancelado(Me, EventArgs.Empty)
    End Sub

#End Region

#Region "Utilerias"

    ''' <summary>
    ''' Texto plano a HTML sencillo: las líneas en blanco separan párrafos y los
    ''' saltos sueltos quedan como &lt;br /&gt;.
    ''' </summary>
    Private Function TextoAHtml(texto As String) As String

        If String.IsNullOrWhiteSpace(texto) Then Return String.Empty

        Dim seguro As String = Server.HtmlEncode(texto).Replace(vbCrLf, vbLf).Replace(vbCr, vbLf)

        Dim parrafos = seguro.
            Split(New String() {vbLf & vbLf}, StringSplitOptions.None).
            Where(Function(p) p.Trim().Length > 0).
            Select(Function(p) "<p>" & p.Trim().Replace(vbLf, "<br />") & "</p>")

        Return String.Join(Environment.NewLine, parrafos)
    End Function

    ''' <summary>Quita las etiquetas para revisar el texto sin que estorbe el marcado.</summary>
    Private Function HtmlATexto(html As String) As String

        If String.IsNullOrEmpty(html) Then Return String.Empty

        Return Server.HtmlDecode(Text.RegularExpressions.Regex.Replace(html, "<[^>]+>", " "))
    End Function

    Private Sub Avisar(mensaje As String, tipo As String)
        lblAviso.Text = mensaje
        pnlAviso.CssClass = "alert alert-" & tipo
        pnlAviso.Visible = True
    End Sub

#End Region

End Class
