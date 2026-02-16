Imports Newtonsoft.Json
Imports WebAdmin.MercanciaSegura.DOM.Modelos
Module DropdownHelpers

    Public Sub CargarTipoPersona(ddlTipoPersona As DropDownList)
        Dim api As New ConsumoApi()
        Dim tipoPersona As String = api.GetTipoPersona()

        Dim listaTipoPersona As List(Of TipoPersona) = JsonConvert.DeserializeObject(Of List(Of TipoPersona))(tipoPersona)

        ddlTipoPersona.DataSource = listaTipoPersona
        ddlTipoPersona.DataTextField = "Tipo"
        ddlTipoPersona.DataValueField = "TipoPersonaId"
        ddlTipoPersona.DataBind()
    End Sub

    Public Sub CargarTipoEstatus(ddlEstatus As DropDownList)
        Dim api As New ConsumoApi()
        Dim tipoEstatus As String = api.GetTipoEstatus()

        Dim listaTipoEstatus As List(Of TipoEstatus) = JsonConvert.DeserializeObject(Of List(Of TipoEstatus))(tipoEstatus)

        ddlEstatus.DataSource = listaTipoEstatus
        ddlEstatus.DataTextField = "Tipo"
        ddlEstatus.DataValueField = "EstatusId"
        ddlEstatus.DataBind()
    End Sub

    Public Sub CargarTipoSeguro(ddlSeguroContrata As DropDownList)
        Dim api As New ConsumoApi()
        Dim tipoSeguro As String = api.GetTipoSeguro()

        Dim listaTipoSeguro As List(Of TipoSeguro) = JsonConvert.DeserializeObject(Of List(Of TipoSeguro))(tipoSeguro)

        ddlSeguroContrata.DataSource = listaTipoSeguro
        ddlSeguroContrata.DataTextField = "Tipo"
        ddlSeguroContrata.DataValueField = "TipoSeguroId"
        ddlSeguroContrata.DataBind()
    End Sub

    Public Sub CargarTipoCuenta(ddlTipoCuenta As DropDownList)
        Dim api As New ConsumoApi()
        Dim tipoCuenta As String = api.GetTipoCuenta()

        Dim listaTipoCuenta As List(Of TipoCuenta) = JsonConvert.DeserializeObject(Of List(Of TipoCuenta))(tipoCuenta)

        ddlTipoCuenta.DataSource = listaTipoCuenta
        ddlTipoCuenta.DataTextField = "Tipo"
        ddlTipoCuenta.DataValueField = "TipoCuentaId"
        ddlTipoCuenta.DataBind()
    End Sub

    Public Sub CargarOrigenCliente(ddlOrigenCliente As DropDownList)
        Dim api As New ConsumoApi()
        Dim origenCliente As String = api.GetOrigenCliente()

        Dim listaorigenCliente As List(Of OrigenCliente) = JsonConvert.DeserializeObject(Of List(Of OrigenCliente))(origenCliente)

        ddlOrigenCliente.DataSource = listaorigenCliente
        ddlOrigenCliente.DataTextField = "Tipo"
        ddlOrigenCliente.DataValueField = "OrigenClienteId"
        ddlOrigenCliente.DataBind()
    End Sub

    Public Sub CargarTipoSector(ddlSector As DropDownList)
        Dim api As New ConsumoApi()
        Dim tipoSector As String = api.GetTipoSector()

        Dim listaTipoSector As List(Of TipoSector) = JsonConvert.DeserializeObject(Of List(Of TipoSector))(tipoSector)

        ddlSector.DataSource = listaTipoSector
        ddlSector.DataTextField = "Tipo"
        ddlSector.DataValueField = "TipoSectorId"
        ddlSector.DataBind()
    End Sub

    Public Sub CargarRegimenFiscal(ddlRegimenFiscal As DropDownList, tipoPersonaId As Integer)
        Dim api As New ConsumoApi()
        Dim regimenFiscal As String = api.GetRegimenFiscal()

        Dim listaRegimenFiscal As List(Of RegimenFiscal) = JsonConvert.DeserializeObject(Of List(Of RegimenFiscal))(regimenFiscal)

        Dim listaFiltrada As List(Of RegimenFiscal)

        If tipoPersonaId = 1 Then
            listaFiltrada = listaRegimenFiscal.Where(Function(r) r.AplicaFisica).ToList()
        Else
            listaFiltrada = listaRegimenFiscal.Where(Function(r) r.AplicaMoral).ToList()
        End If

        ddlRegimenFiscal.DataSource = listaFiltrada
        ddlRegimenFiscal.DataTextField = "CodigoDescripcion"
        ddlRegimenFiscal.DataValueField = "RegimenFiscalId"
        ddlRegimenFiscal.DataBind()
    End Sub

    Public Sub CargarRFCGenerico(ddlRFCGenerico As DropDownList)
        Dim api As New ConsumoApi()
        Dim rfcGenerico As String = api.GetRFCGenerico()

        Dim listaRFCGenerico As List(Of rfcGenerico) = JsonConvert.DeserializeObject(Of List(Of rfcGenerico))(rfcGenerico)

        ddlRFCGenerico.DataSource = listaRFCGenerico
        ddlRFCGenerico.DataTextField = "Tipo"
        ddlRFCGenerico.DataValueField = "RfcGenericoId"
        ddlRFCGenerico.DataBind()
    End Sub

    Public Sub CargarTipoVendedor(ddlTipoVendedor As DropDownList)
        Dim api As New ConsumoApi()
        Dim tipoVendedor As String = api.GetTipoVendedor()

        Dim listaTipoVendedor As List(Of TipoVendedor) = JsonConvert.DeserializeObject(Of List(Of TipoVendedor))(tipoVendedor)

        ddlTipoVendedor.DataSource = listaTipoVendedor
        ddlTipoVendedor.DataTextField = "Tipo"
        ddlTipoVendedor.DataValueField = "TipoVendedorId"
        ddlTipoVendedor.DataBind()
    End Sub
    Public Sub CargarTipoCorreo(ddlTipoCorreo As DropDownList)
        Dim api As New ConsumoApi()
        Dim tipoCorreo As String = api.GetTipoCorreo()

        Dim listaTipoCorreo As List(Of TipoCorreo) = JsonConvert.DeserializeObject(Of List(Of TipoCorreo))(tipoCorreo)

        ddlTipoCorreo.DataSource = listaTipoCorreo
        ddlTipoCorreo.DataTextField = "Tipo"
        ddlTipoCorreo.DataValueField = "TipoCorreoId"
        ddlTipoCorreo.DataBind()
    End Sub
    Public Sub CargarVendedores(ddlNombreVendedor As DropDownList)
        Dim api As New ConsumoApi()
        Dim Vendedores As String = api.GetCargarVendedores()

        Dim lstVendedores As List(Of Vendedor) = JsonConvert.DeserializeObject(Of List(Of Vendedor))(Vendedores)

        ddlNombreVendedor.DataSource = lstVendedores
        ddlNombreVendedor.DataTextField = "Nombres"
        ddlNombreVendedor.DataValueField = "VendedorId"
        ddlNombreVendedor.DataBind()

        ddlNombreVendedor.Items.Insert(0, New ListItem("Selecciona un vendedor", "0"))
    End Sub
    Public Sub CargarTipoTarifa(ParamArray ddls() As DropDownList)
        Dim api As New ConsumoApi()
        Dim tipoTarifaJson As String = api.GetTipoTarifa()
        Dim listaTipoTarifa As List(Of TipoTarifa) = JsonConvert.DeserializeObject(Of List(Of TipoTarifa))(tipoTarifaJson)

        For Each ddl As DropDownList In ddls
            ddl.DataSource = listaTipoTarifa
            ddl.DataTextField = "Tarifa"
            ddl.DataValueField = "TipoTarifaId"
            ddl.DataBind()
        Next
    End Sub

    Public Sub CargarPaises(ddlPais As DropDownList)
        ddlPais.Items.Clear()
        ddlPais.Items.Add(New ListItem("Selecciona un país", ""))
        ddlPais.Items.Add(New ListItem("México", "MX"))
        ddlPais.Items.Add(New ListItem("Estados Unidos", "US"))
    End Sub

    Public Sub CargarEstados(ddlEstado As DropDownList, pais As String)
        ddlEstado.Items.Clear()
        ddlEstado.Items.Add(New ListItem("Selecciona un estado", ""))

        Select Case pais
            Case "MX"
                ddlEstado.Items.Add(New ListItem("Aguascalientes", "AGS"))
                ddlEstado.Items.Add(New ListItem("Baja California", "BC"))
                ddlEstado.Items.Add(New ListItem("Baja California Sur", "BCS"))
                ddlEstado.Items.Add(New ListItem("Campeche", "CAMP"))
                ddlEstado.Items.Add(New ListItem("Chiapas", "CHIS"))
                ddlEstado.Items.Add(New ListItem("Chihuahua", "CHIH"))
                ddlEstado.Items.Add(New ListItem("Ciudad de México", "CDMX"))
                ddlEstado.Items.Add(New ListItem("Coahuila", "COAH"))
                ddlEstado.Items.Add(New ListItem("Colima", "COL"))
                ddlEstado.Items.Add(New ListItem("Durango", "DGO"))
                ddlEstado.Items.Add(New ListItem("Estado de México", "MEX"))
                ddlEstado.Items.Add(New ListItem("Guanajuato", "GTO"))
                ddlEstado.Items.Add(New ListItem("Guerrero", "GRO"))
                ddlEstado.Items.Add(New ListItem("Hidalgo", "HGO"))
                ddlEstado.Items.Add(New ListItem("Jalisco", "JAL"))
                ddlEstado.Items.Add(New ListItem("Michoacán", "MICH"))
                ddlEstado.Items.Add(New ListItem("Morelos", "MOR"))
                ddlEstado.Items.Add(New ListItem("Nayarit", "NAY"))
                ddlEstado.Items.Add(New ListItem("Nuevo León", "NL"))
                ddlEstado.Items.Add(New ListItem("Oaxaca", "OAX"))
                ddlEstado.Items.Add(New ListItem("Puebla", "PUE"))
                ddlEstado.Items.Add(New ListItem("Querétaro", "QRO"))
                ddlEstado.Items.Add(New ListItem("Quintana Roo", "QROO"))
                ddlEstado.Items.Add(New ListItem("San Luis Potosí", "SLP"))
                ddlEstado.Items.Add(New ListItem("Sinaloa", "SIN"))
                ddlEstado.Items.Add(New ListItem("Sonora", "SON"))
                ddlEstado.Items.Add(New ListItem("Tabasco", "TAB"))
                ddlEstado.Items.Add(New ListItem("Tamaulipas", "TAM"))
                ddlEstado.Items.Add(New ListItem("Tlaxcala", "TLAX"))
                ddlEstado.Items.Add(New ListItem("Veracruz", "VER"))
                ddlEstado.Items.Add(New ListItem("Yucatán", "YUC"))
                ddlEstado.Items.Add(New ListItem("Zacatecas", "ZAC"))
            Case "US"
                ddlEstado.Items.Add(New ListItem("Florida", "FL"))
                ddlEstado.Items.Add(New ListItem("New York", "NY"))
                ddlEstado.Items.Add(New ListItem("Texas", "TX"))
        End Select
    End Sub

End Module
