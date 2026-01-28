
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
    Public Function GetVendedorId(vendedorId As Integer) As String
        Try
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

            Using client As New HttpClient()
                Dim url As String = $"{ConfigurationManager.AppSettings("ConsultarVendedorId")}/{vendedorId}"
                Dim response As HttpResponseMessage = client.GetAsync(url).Result
                Return response.Content.ReadAsStringAsync().Result
            End Using

        Catch ex As Exception
            Return "ERROR: " & ex.Message
        End Try
    End Function

    Public Function GetTipoEstatus() As String
        Try
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

            Using client As New HttpClient()

                Dim response As HttpResponseMessage =
                        client.GetAsync(ConfigurationManager.AppSettings("TipoEstatus")).Result
                Return response.Content.ReadAsStringAsync().Result
            End Using

        Catch ex As Exception
            Return "ERROR: " & ex.Message
        End Try
    End Function
    Public Function GetTipoSeguro() As String
        Try
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

            Using client As New HttpClient()

                Dim response As HttpResponseMessage =
                        client.GetAsync(ConfigurationManager.AppSettings("TipoSeguro")).Result
                Return response.Content.ReadAsStringAsync().Result
            End Using

        Catch ex As Exception
            Return "ERROR: " & ex.Message
        End Try
    End Function
    Public Function GetTipoCuenta() As String
        Try
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

            Using client As New HttpClient()

                Dim response As HttpResponseMessage =
                        client.GetAsync(ConfigurationManager.AppSettings("TipoCuenta")).Result
                Return response.Content.ReadAsStringAsync().Result
            End Using

        Catch ex As Exception
            Return "ERROR: " & ex.Message
        End Try
    End Function
    Public Function GetOrigenCliente() As String
        Try
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

            Using client As New HttpClient()

                Dim response As HttpResponseMessage =
                        client.GetAsync(ConfigurationManager.AppSettings("OrigenCliente")).Result
                Return response.Content.ReadAsStringAsync().Result
            End Using

        Catch ex As Exception
            Return "ERROR: " & ex.Message
        End Try
    End Function
    Public Function GetTipoSector() As String
        Try
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

            Using client As New HttpClient()

                Dim response As HttpResponseMessage =
                        client.GetAsync(ConfigurationManager.AppSettings("TipoSector")).Result
                Return response.Content.ReadAsStringAsync().Result
            End Using

        Catch ex As Exception
            Return "ERROR: " & ex.Message
        End Try
    End Function
    Public Function GetRegimenFiscal() As String
        Try
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

            Using client As New HttpClient()

                Dim response As HttpResponseMessage =
                        client.GetAsync(ConfigurationManager.AppSettings("RegimenFiscal")).Result
                Return response.Content.ReadAsStringAsync().Result
            End Using

        Catch ex As Exception
            Return "ERROR: " & ex.Message
        End Try
    End Function
    Public Function GetRFCGenerico() As String
        Try
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

            Using client As New HttpClient()

                Dim response As HttpResponseMessage =
                        client.GetAsync(ConfigurationManager.AppSettings("RFCGenerico")).Result
                Return response.Content.ReadAsStringAsync().Result
            End Using

        Catch ex As Exception
            Return "ERROR: " & ex.Message
        End Try
    End Function
    Public Function GetCargarClientes() As String
        Try
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

            Using client As New HttpClient()

                Dim response As HttpResponseMessage =
                        client.GetAsync(ConfigurationManager.AppSettings("CargarCliente")).Result
                Return response.Content.ReadAsStringAsync().Result
            End Using

        Catch ex As Exception
            Return "ERROR: " & ex.Message
        End Try
    End Function
#End Region

#Region "PUT"
    Public Function PutEditarVendedores(vendedorId As Integer, json As String) As String
        Try
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

            Using client As New HttpClient()
                Dim url As String = $"{ConfigurationManager.AppSettings("EditarVendedor")}/{vendedorId}"
                Dim content As New StringContent(json, Encoding.UTF8, "application/json")
                Dim response As HttpResponseMessage = client.PutAsync(url, content).Result
                Return response.Content.ReadAsStringAsync().Result
            End Using

        Catch ex As Exception
            Return "ERROR: " & ex.Message
        End Try
    End Function
#End Region

#Region "DELETE"
    Public Function DeleteVendedores(vendedorId As Integer) As String
        Try
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

            Using client As New HttpClient()
                Dim url As String = $"{ConfigurationManager.AppSettings("EliminarVendedor")}/{vendedorId}"
                Dim response As HttpResponseMessage = client.DeleteAsync(url).Result
                Return response.Content.ReadAsStringAsync().Result
            End Using

        Catch ex As Exception
            Return "ERROR: " & ex.Message
        End Try
    End Function
#End Region
End Class

