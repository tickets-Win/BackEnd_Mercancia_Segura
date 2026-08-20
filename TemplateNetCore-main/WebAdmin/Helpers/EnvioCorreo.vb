Imports System.Configuration
Imports System.Net.Mail
Imports WebAdmin.MercanciaSegura.DOM.Modelos

''' <summary>
''' Envío de correo por SMTP. La configuración se lee de &lt;system.net&gt;
''' &lt;mailSettings&gt; del Web.config, así que las credenciales no viven en el
''' código. Si no está configurado, Enviar devuelve el motivo en lugar de fallar.
''' </summary>
Public Module EnvioCorreo

    ''' <summary>
    ''' Cuentas que pueden aparecer en el "De:". Se listan en la llave
    ''' CuentasCorreo del Web.config, separadas por punto y coma.
    ''' </summary>
    Public Function CuentasRemitentes() As List(Of String)

        Dim configuradas As String = ConfigurationManager.AppSettings("CuentasCorreo")

        If String.IsNullOrWhiteSpace(configuradas) Then
            ' Si no hay lista, se usa el remitente del mailSettings.
            Dim porDefecto As String = RemitentePorDefecto()

            If String.IsNullOrWhiteSpace(porDefecto) Then Return New List(Of String)

            Return New List(Of String) From {porDefecto}
        End If

        Return configuradas.
            Split(";"c).
            Select(Function(c) c.Trim()).
            Where(Function(c) c.Length > 0).
            ToList()
    End Function

    Public Function RemitentePorDefecto() As String

        Try
            Dim seccion = TryCast(
                ConfigurationManager.GetSection("system.net/mailSettings/smtp"),
                Net.Configuration.SmtpSection)

            If seccion Is Nothing Then Return String.Empty

            Return seccion.From
        Catch
            Return String.Empty
        End Try
    End Function

    ''' <summary>True cuando hay un servidor SMTP configurado.</summary>
    Public Function EstaConfigurado() As Boolean

        Try
            Using cliente As New SmtpClient()
                Return Not String.IsNullOrWhiteSpace(cliente.Host)
            End Using
        Catch
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Manda el correo. Devuelve cadena vacía si salió bien, o el motivo del
    ''' fallo. No lanza excepciones: la pantalla solo muestra el mensaje.
    ''' </summary>
    Public Function Enviar(remitente As String,
                           para As String,
                           cc As String,
                           cco As String,
                           asunto As String,
                           cuerpo As String,
                           confirmacionLectura As Boolean,
                           Optional esHtml As Boolean = False,
                           Optional adjuntos As List(Of ArchivoAdjunto) = Nothing) As String

        If Not EstaConfigurado() Then
            Return "No hay un servidor SMTP configurado en el Web.config (system.net/mailSettings)."
        End If

        Dim de As String = If(String.IsNullOrWhiteSpace(remitente), RemitentePorDefecto(), remitente)

        If String.IsNullOrWhiteSpace(de) Then
            Return "Falta la cuenta remitente."
        End If

        Try
            Using mensaje As New MailMessage()

                mensaje.From = New MailAddress(de)

                AgregarDestinatarios(mensaje.To, para)
                AgregarDestinatarios(mensaje.CC, cc)
                AgregarDestinatarios(mensaje.Bcc, cco)

                If mensaje.To.Count = 0 Then Return "Falta el destinatario."

                mensaje.Subject = asunto
                mensaje.Body = cuerpo
                mensaje.IsBodyHtml = esHtml

                If confirmacionLectura Then
                    mensaje.Headers.Add("Disposition-Notification-To", de)
                End If

                ' Los adjuntos se abren sobre un MemoryStream que tiene que seguir
                ' vivo hasta despues del Send, por eso se cierran al final.
                Dim flujos As New List(Of IO.MemoryStream)

                If adjuntos IsNot Nothing Then
                    For Each archivo As ArchivoAdjunto In adjuntos

                        If archivo Is Nothing OrElse archivo.Contenido Is Nothing Then Continue For

                        Dim flujo As New IO.MemoryStream(archivo.Contenido)
                        flujos.Add(flujo)

                        Dim tipo As String = If(String.IsNullOrWhiteSpace(archivo.TipoMime),
                                                "application/octet-stream",
                                                archivo.TipoMime)

                        mensaje.Attachments.Add(New Attachment(flujo, archivo.Nombre, tipo))
                    Next
                End If

                Try
                    Using cliente As New SmtpClient()
                        cliente.Send(mensaje)
                    End Using
                Finally
                    For Each flujo As IO.MemoryStream In flujos
                        flujo.Dispose()
                    Next
                End Try
            End Using

            Return String.Empty

        Catch ex As Exception
            Return ex.Message
        End Try
    End Function

    ''' <summary>
    ''' Acepta varios correos separados por coma o punto y coma. Los que no sean
    ''' direcciones válidas se ignoran.
    ''' </summary>
    Private Sub AgregarDestinatarios(coleccion As MailAddressCollection, lista As String)

        If String.IsNullOrWhiteSpace(lista) Then Exit Sub

        For Each direccion As String In lista.Split(","c, ";"c)

            Dim limpia As String = direccion.Trim()

            If limpia.Length = 0 Then Continue For

            Try
                coleccion.Add(New MailAddress(limpia))
            Catch
                ' Dirección mal escrita: se omite en vez de tumbar el envío.
            End Try
        Next
    End Sub

    ''' <summary>Valida el formato de una lista de correos antes de intentar enviar.</summary>
    Public Function DireccionesInvalidas(lista As String) As List(Of String)

        Dim malas As New List(Of String)

        If String.IsNullOrWhiteSpace(lista) Then Return malas

        For Each direccion As String In lista.Split(","c, ";"c)

            Dim limpia As String = direccion.Trim()

            If limpia.Length = 0 Then Continue For

            Try
                Dim m As New MailAddress(limpia)
            Catch
                malas.Add(limpia)
            End Try
        Next

        Return malas
    End Function

End Module
