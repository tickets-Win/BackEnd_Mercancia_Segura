Imports System.Web
Imports System.Web.SessionState
Imports Newtonsoft.Json
Imports WebAdmin.MercanciaSegura.DOM.Modelos

''' <summary>
''' Recibe y quita los adjuntos del correo sin recargar la página.
'''
''' Un FileUpload de WebForms obliga a un postback completo —no viaja en una
''' actualización parcial—, y eso recargaba toda la pantalla a media captura.
''' Aquí el archivo se sube por su cuenta y solo se redibuja la lista.
'''
''' Los archivos se guardan en la misma llave de Session que usa el control, para
''' que al enviar los encuentre igual que antes.
''' </summary>
Public Class AdjuntosCorreo
    Implements IHttpHandler, IRequiresSessionState

    ''' <summary>Tope por archivo, igual que el que ya validaba el control.</summary>
    Private Const MaximoMB As Integer = 10

    Public Sub ProcessRequest(context As HttpContext) Implements IHttpHandler.ProcessRequest

        context.Response.ContentType = "application/json"

        Dim llave As String = context.Request("llave")

        If String.IsNullOrWhiteSpace(llave) OrElse Not llave.StartsWith("AdjuntosCorreo_") Then
            Responder(context, False, "Petición inválida.", Nothing)
            Exit Sub
        End If

        Dim lista As List(Of ArchivoAdjunto) = Adjuntos(context, llave)

        Select Case context.Request("accion")

            Case "subir"
                Dim avisos As New List(Of String)

                For i As Integer = 0 To context.Request.Files.Count - 1

                    Dim archivo As HttpPostedFile = context.Request.Files(i)

                    If archivo Is Nothing OrElse archivo.ContentLength = 0 Then Continue For

                    If archivo.ContentLength > MaximoMB * 1024 * 1024 Then
                        avisos.Add(archivo.FileName & " pasa de " & MaximoMB & " MB")
                        Continue For
                    End If

                    Dim contenido(archivo.ContentLength - 1) As Byte
                    archivo.InputStream.Read(contenido, 0, archivo.ContentLength)

                    lista.Add(New ArchivoAdjunto With {
                        .Nombre = IO.Path.GetFileName(archivo.FileName),
                        .TipoMime = archivo.ContentType,
                        .Contenido = contenido
                    })
                Next

                Responder(context, True, String.Join("; ", avisos), lista)

            Case "quitar"
                Dim indice As Integer

                If Integer.TryParse(context.Request("indice"), indice) AndAlso
                   indice >= 0 AndAlso indice < lista.Count Then

                    lista.RemoveAt(indice)
                End If

                Responder(context, True, String.Empty, lista)

            Case Else
                ' Solo devolver la lista actual.
                Responder(context, True, String.Empty, lista)
        End Select
    End Sub

    Private Function Adjuntos(context As HttpContext, llave As String) As List(Of ArchivoAdjunto)

        If context.Session(llave) Is Nothing Then
            context.Session(llave) = New List(Of ArchivoAdjunto)
        End If

        Return DirectCast(context.Session(llave), List(Of ArchivoAdjunto))
    End Function

    ''' <summary>
    ''' Devuelve solo nombre y tamaño: el contenido del archivo no tiene por qué
    ''' viajar de regreso al navegador.
    ''' </summary>
    Private Sub Responder(context As HttpContext, ok As Boolean, aviso As String,
                          lista As List(Of ArchivoAdjunto))

        Dim archivos = If(lista, New List(Of ArchivoAdjunto)).
            Select(Function(a) New With {.nombre = a.Nombre, .tamanio = a.Tamanio}).
            ToList()

        context.Response.Write(JsonConvert.SerializeObject(
            New With {.ok = ok, .aviso = aviso, .archivos = archivos}))
    End Sub

    Public ReadOnly Property IsReusable As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class
