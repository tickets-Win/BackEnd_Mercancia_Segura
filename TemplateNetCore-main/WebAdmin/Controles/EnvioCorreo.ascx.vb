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

        ' Un FileUpload no manda el archivo en un postback asincrono, y la pagina
        ' no puede poner un PostBackTrigger sobre un control que vive aqui dentro:
        ' se registra desde el propio control.
        Dim gestor As ScriptManager = ScriptManager.GetCurrent(Page)

        If gestor IsNot Nothing Then gestor.RegisterPostBackControl(lnkAdjuntar)

        ' El <form> necesita multipart para que suban los adjuntos. ASP.NET solo
        ' lo pone si ve un FileUpload al renderizar, y este control nace oculto;
        ' cuando se muestra ya es por postback parcial y la etiqueta <form> vive
        ' fuera del UpdatePanel, asi que nunca se vuelve a dibujar.
        If Page.Form IsNot Nothing Then Page.Form.Enctype = "multipart/form-data"
    End Sub

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
            If String.IsNullOrWhiteSpace(hfHtml.Value) Then Return String.Empty

            Try
                Return Text.Encoding.UTF8.GetString(Convert.FromBase64String(hfHtml.Value))
            Catch
                Return hfHtml.Value
            End Try
        End Get
        Set(value As String)
            ' Se escribe en los dos lados: el hidden es lo que viaja al postear,
            ' y el literal es lo que se ve de inmediato en el área editable sin
            ' depender de que el script alcance a inyectarlo.
            litCuerpo.Text = If(value, String.Empty)

            If String.IsNullOrEmpty(value) Then
                hfHtml.Value = String.Empty
                Exit Property
            End If

            hfHtml.Value = Convert.ToBase64String(Text.Encoding.UTF8.GetBytes(value))
        End Set
    End Property

    ''' <summary>
    ''' Adjuntos de la captura en curso. Van en Session porque se cargan en un
    ''' postback y se mandan en otro. La llave incluye el id del control para que
    ''' dos pantallas no se pisen.
    ''' </summary>
    Private ReadOnly Property Adjuntos As List(Of ArchivoAdjunto)
        Get
            Dim llave As String = "AdjuntosCorreo_" & UniqueID

            If Session(llave) Is Nothing Then
                Session(llave) = New List(Of ArchivoAdjunto)
            End If

            Return DirectCast(Session(llave), List(Of ArchivoAdjunto))
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
        CargarAdjuntos()

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
        Else
            pnlAviso.Visible = False
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

    Private Sub CargarAdjuntos()
        rptAdjuntos.DataSource = Adjuntos
        rptAdjuntos.DataBind()
    End Sub

    Protected Sub lnkAdjuntar_Click(sender As Object, e As EventArgs)

        If Not fuAdjunto.HasFiles Then
            Avisar("Elige al menos un archivo antes de adjuntar.", "warning")
            Exit Sub
        End If

        Const maximoMB As Integer = 10

        For Each archivo As HttpPostedFile In fuAdjunto.PostedFiles

            If archivo Is Nothing OrElse archivo.ContentLength = 0 Then Continue For

            If archivo.ContentLength > maximoMB * 1024 * 1024 Then
                Avisar("El archivo " & archivo.FileName & " pasa de " & maximoMB & " MB y no se adjuntó.", "warning")
                Continue For
            End If

            Dim contenido(archivo.ContentLength - 1) As Byte
            archivo.InputStream.Read(contenido, 0, archivo.ContentLength)

            Adjuntos.Add(New ArchivoAdjunto With {
                .Nombre = IO.Path.GetFileName(archivo.FileName),
                .TipoMime = archivo.ContentType,
                .Contenido = contenido
            })
        Next

        CargarAdjuntos()
    End Sub

    Protected Sub rptAdjuntos_ItemCommand(source As Object, e As RepeaterCommandEventArgs)

        If e.CommandName <> "Quitar" Then Exit Sub

        Dim indice As Integer
        If Not Integer.TryParse(Convert.ToString(e.CommandArgument), indice) Then Exit Sub

        If indice >= 0 AndAlso indice < Adjuntos.Count Then Adjuntos.RemoveAt(indice)

        CargarAdjuntos()
    End Sub

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
