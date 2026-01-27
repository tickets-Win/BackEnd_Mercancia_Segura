
Imports System.Net
Imports System.Net.Http
Imports System.Text
Imports Newtonsoft.Json
Imports System.Configuration

Public Class ConsumoApi

#Region "POST"
    Public Function Login(correo As String, password As String) As String
        Try
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

            Using client As New HttpClient()

                Dim body = New With {
                        .correo = correo,
                        .password = password
                    }

                Dim json As String = JsonConvert.SerializeObject(body)
                Dim content As New StringContent(json, Encoding.UTF8, "application/json")

                Dim response As HttpResponseMessage =
                        client.PostAsync(ConfigurationManager.AppSettings("Login"), content).Result

                Return response.Content.ReadAsStringAsync().Result
            End Using

        Catch ex As Exception
            Return "ERROR: " & ex.Message
        End Try
    End Function
    Public Function PostVendedor(body As String) As String
        Try
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

            Using client As New HttpClient()

                Dim json As String = JsonConvert.SerializeObject(body)
                Dim content As New StringContent(body, Encoding.UTF8, "application/json")

                Dim response As HttpResponseMessage =
                        client.PostAsync(ConfigurationManager.AppSettings("Vendedor"), content).Result

                Return response.Content.ReadAsStringAsync().Result
            End Using

        Catch ex As Exception
            Return "ERROR: " & ex.Message
        End Try
    End Function

#End Region

#Region "GET"
    Public Function GetTipoPersona() As String
        Try
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

            Using client As New HttpClient()

                Dim response As HttpResponseMessage =
                        client.GetAsync(ConfigurationManager.AppSettings("TipoPersona")).Result
                Return response.Content.ReadAsStringAsync().Result
            End Using

        Catch ex As Exception
            Return "ERROR: " & ex.Message
        End Try
    End Function
    Public Function GetTipoVendedor() As String
        Try
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

            Using client As New HttpClient()

                Dim response As HttpResponseMessage =
                        client.GetAsync(ConfigurationManager.AppSettings("TipoVendedor")).Result
                Return response.Content.ReadAsStringAsync().Result
            End Using

        Catch ex As Exception
            Return "ERROR: " & ex.Message
        End Try
    End Function
    Public Function GetCargarVendedores() As String
        Try
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

            Using client As New HttpClient()

                Dim response As HttpResponseMessage =
                        client.GetAsync(ConfigurationManager.AppSettings("CargarVendedores")).Result
                Return response.Content.ReadAsStringAsync().Result
            End Using

        Catch ex As Exception
            Return "ERROR: " & ex.Message
        End Try
    End Function
#End Region
End Class

