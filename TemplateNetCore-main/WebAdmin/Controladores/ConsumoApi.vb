
Imports System.Net
Imports System.Net.Http
Imports System.Text
Imports Modelos
Imports Modelos.Modelos
Imports Newtonsoft.Json

Public Class ConsumoApi
    Public Const urlPostLogin As String = "https://www.winsefweb.net/MercanciaSegura/API/0.1/auth/login"


#Region "POST"

    Public Function PostLogin(usuario As String, password As String) As MessageResult
        Dim objectMessageResult As New MessageResult
        Try
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

            Using client As New HttpClient()
                Dim body = New With {
                    .usuario = usuario,
                    .password = password
                }
                Dim JsonDataKey As String = JsonConvert.SerializeObject(body)
                Dim content = New StringContent(JsonDataKey, Encoding.UTF8, "application/json")

                client.DefaultRequestHeaders.UserAgent.ParseAdd("MiAplicacion/1.0 (VB.NET)")

                Dim response As HttpResponseMessage = client.PostAsync(urlPostLogin, content).Result
                Dim responseContent As String = response.Content.ReadAsStringAsync().Result

                objectMessageResult.JSON = responseContent

                If response.IsSuccessStatusCode Then
                    objectMessageResult.ErrorID = Enumeraciones.TipoErroresAPI.Exito
                ElseIf response.StatusCode = HttpStatusCode.BadRequest Then
                    objectMessageResult.ErrorID = Enumeraciones.TipoErroresAPI.Advertencia
                ElseIf response.StatusCode = HttpStatusCode.Unauthorized Then
                    objectMessageResult.ErrorID = Enumeraciones.TipoErroresAPI.Errors
                ElseIf response.StatusCode = HttpStatusCode.InternalServerError Then
                    objectMessageResult.ErrorID = Enumeraciones.TipoErroresAPI.Errors
                Else
                    objectMessageResult.ErrorID = Enumeraciones.TipoErroresAPI.Errors
                End If

                Return objectMessageResult
            End Using
        Catch ex As Exception
            objectMessageResult.ErrorID = Enumeraciones.TipoErroresAPI.Errors
            objectMessageResult.JSON = ex.Message
            Return objectMessageResult
        End Try
    End Function

#End Region

End Class
