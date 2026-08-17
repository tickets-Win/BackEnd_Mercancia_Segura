Imports Newtonsoft.Json
Imports WebAdmin.MercanciaSegura.DOM.Modelos
Module DropdownHelpers

    ''' <summary>
    ''' Deserializa la respuesta de un catálogo sin confiar en ella.
    '''
    ''' ConsumoApi devuelve la cadena "ERROR: ..." cuando el API no contesta, y
    ''' eso no es JSON: al intentar parsearlo se caía la página entera con un
    ''' error amarillo. Aquí un catálogo que falla deja su combo vacío, pero la
    ''' pantalla sigue en pie.
    ''' </summary>
    Private Function ListaSegura(Of T)(json As String) As List(Of T)

        If String.IsNullOrWhiteSpace(json) OrElse
           json = "null" OrElse
           json.StartsWith("ERROR") Then Return New List(Of T)

        Try
            Dim lista As List(Of T) = JsonConvert.DeserializeObject(Of List(Of T))(json)

            If lista Is Nothing Then Return New List(Of T)

            Return lista

        Catch
            ' Respuesta con una forma inesperada: mismo criterio, no romper.
            Return New List(Of T)
        End Try
    End Function

    Public Sub CargarTipoPersona(ddlTipoPersona As DropDownList)
        Dim api As New ConsumoApi()
        Dim tipoPersona As String = api.GetTipoPersona()
        Dim listaTipoPersona As List(Of TipoPersona) = ListaSegura(Of TipoPersona)(tipoPersona)

        ddlTipoPersona.DataSource = listaTipoPersona
        ddlTipoPersona.DataTextField = "Tipo"
        ddlTipoPersona.DataValueField = "TipoPersonaId"
        ddlTipoPersona.DataBind()


    End Sub

    Public Sub CargarTipoEstatus(ddlEstatus As DropDownList)
        Dim api As New ConsumoApi()
        Dim tipoEstatus As String = api.GetTipoEstatus()

        Dim listaTipoEstatus As List(Of TipoEstatus) = ListaSegura(Of TipoEstatus)(tipoEstatus)

        ddlEstatus.DataSource = listaTipoEstatus
        ddlEstatus.DataTextField = "Tipo"
        ddlEstatus.DataValueField = "EstatusId"
        ddlEstatus.DataBind()

    End Sub
    Public Sub CargarTipoEstatusPoliza(ddlEstatusPoliza As DropDownList)
        Dim api As New ConsumoApi()
        Dim tipoEstatusPoliza As String = api.GetEstatusPoliza()

        Dim listaTipoEstatusPoliza As List(Of EstatusPoliza) = ListaSegura(Of EstatusPoliza)(tipoEstatusPoliza)

        ddlEstatusPoliza.DataSource = listaTipoEstatusPoliza
        ddlEstatusPoliza.DataTextField = "Tipo"
        ddlEstatusPoliza.DataValueField = "EstatusPolizaId"
        ddlEstatusPoliza.DataBind()

    End Sub

    Public Sub CargarTipoSeguro(ddlSeguroContrata As DropDownList)
        Dim api As New ConsumoApi()
        Dim tipoSeguro As String = api.GetTipoSeguro()

        Dim listaTipoSeguro As List(Of TipoSeguro) = ListaSegura(Of TipoSeguro)(tipoSeguro)

        ddlSeguroContrata.DataSource = listaTipoSeguro
        ddlSeguroContrata.DataTextField = "Tipo"
        ddlSeguroContrata.DataValueField = "TipoSeguroId"
        ddlSeguroContrata.DataBind()
        ddlSeguroContrata.Items.Insert(0, New ListItem("-- Selecciona --", "0"))

    End Sub

    Public Sub CargarTipoCuenta(ddlTipoCuenta As DropDownList)
        Dim api As New ConsumoApi()
        Dim tipoCuenta As String = api.GetTipoCuenta()

        Dim listaTipoCuenta As List(Of TipoCuenta) = ListaSegura(Of TipoCuenta)(tipoCuenta)

        ddlTipoCuenta.DataSource = listaTipoCuenta
        ddlTipoCuenta.DataTextField = "Tipo"
        ddlTipoCuenta.DataValueField = "TipoCuentaId"
        ddlTipoCuenta.DataBind()
        ddlTipoCuenta.Items.Insert(0, New ListItem("-- Selecciona --", "0"))

    End Sub

    Public Sub CargarOrigenCliente(ddlOrigenCliente As DropDownList)
        Dim api As New ConsumoApi()
        Dim origenCliente As String = api.GetOrigenCliente()

        Dim listaorigenCliente As List(Of OrigenCliente) = ListaSegura(Of OrigenCliente)(origenCliente)

        ddlOrigenCliente.DataSource = listaorigenCliente
        ddlOrigenCliente.DataTextField = "Tipo"
        ddlOrigenCliente.DataValueField = "OrigenClienteId"
        ddlOrigenCliente.DataBind()
        ddlOrigenCliente.Items.Insert(0, New ListItem("-- Selecciona --", "0"))

    End Sub

    Public Sub CargarTipoSector(ddlSector As DropDownList)
        Dim api As New ConsumoApi()
        Dim tipoSector As String = api.GetTipoSector()

        Dim listaTipoSector As List(Of TipoSector) = ListaSegura(Of TipoSector)(tipoSector)

        ddlSector.DataSource = listaTipoSector
        ddlSector.DataTextField = "Tipo"
        ddlSector.DataValueField = "TipoSectorId"
        ddlSector.DataBind()
        ddlSector.Items.Insert(0, New ListItem("-- Selecciona --", "0"))

    End Sub

    Public Sub CargarRegimenFiscal(ddlRegimenFiscal As DropDownList, tipoPersonaId As Integer)
        Dim api As New ConsumoApi()
        Dim regimenFiscal As String = api.GetRegimenFiscal()

        Dim listaRegimenFiscal As List(Of RegimenFiscal) = ListaSegura(Of RegimenFiscal)(regimenFiscal)

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
        ddlRegimenFiscal.Items.Insert(0, New ListItem("-- Selecciona --", "0"))

    End Sub

    Public Sub CargarRFCGenerico(ddlRFCGenerico As DropDownList)
        Dim api As New ConsumoApi()
        Dim rfcGenerico As String = api.GetRFCGenerico()

        Dim listaRFCGenerico As List(Of rfcGenerico) = ListaSegura(Of rfcGenerico)(rfcGenerico)

        HttpContext.Current.Session("ListaRFCGenericos") = listaRFCGenerico

        ddlRFCGenerico.DataSource = listaRFCGenerico
        ddlRFCGenerico.DataTextField = "Tipo"
        ddlRFCGenerico.DataValueField = "RfcGenericoId"
        ddlRFCGenerico.DataBind()

        ddlRFCGenerico.Items.Insert(0, New ListItem("-- Selecciona --", "0"))

    End Sub

    Public Sub CargarTipoVendedor(ddlTipoVendedor As DropDownList)
        Dim api As New ConsumoApi()
        Dim tipoVendedor As String = api.GetTipoVendedor()

        Dim listaTipoVendedor As List(Of TipoVendedor) = ListaSegura(Of TipoVendedor)(tipoVendedor)

        ddlTipoVendedor.DataSource = listaTipoVendedor
        ddlTipoVendedor.DataTextField = "Tipo"
        ddlTipoVendedor.DataValueField = "TipoVendedorId"
        ddlTipoVendedor.DataBind()
        ddlTipoVendedor.Items.Insert(0, New ListItem("-- Selecciona --", "0"))

    End Sub
    Public Sub CargarTipoCorreo(ddlTipoCorreo As DropDownList)
        Dim api As New ConsumoApi()
        Dim tipoCorreo As String = api.GetTipoCorreo()

        Dim listaTipoCorreo As List(Of TipoCorreo) = ListaSegura(Of TipoCorreo)(tipoCorreo)

        ddlTipoCorreo.DataSource = listaTipoCorreo
        ddlTipoCorreo.DataTextField = "Tipo"
        ddlTipoCorreo.DataValueField = "TipoCorreoId"
        ddlTipoCorreo.DataBind()
        ddlTipoCorreo.Items.Insert(0, New ListItem("-- Selecciona --", "0"))

    End Sub
    Public Sub CargarTipoAseguradora(ddlTipoAseguradora As DropDownList)
        Dim api As New ConsumoApi()
        Dim tipoAseguradora As String = api.GetTipoAseguradora()

        Dim listaTipoAseguradora As List(Of Aseguradora) = ListaSegura(Of Aseguradora)(tipoAseguradora)

        ddlTipoAseguradora.DataSource = listaTipoAseguradora
        ddlTipoAseguradora.DataTextField = "Nombre"
        ddlTipoAseguradora.DataValueField = "AseguradoraId"
        ddlTipoAseguradora.DataBind()
        ddlTipoAseguradora.Items.Insert(0, New ListItem("-- Selecciona --", "0"))

    End Sub
    Public Sub CargarTipoContratante(ddlTipoContratante As DropDownList)
        Dim api As New ConsumoApi()
        Dim tipoContratante As String = api.GetTipoContratante()

        Dim listaTipoContratante As List(Of Contratante) = ListaSegura(Of Contratante)(tipoContratante)

        ddlTipoContratante.DataSource = listaTipoContratante
        ddlTipoContratante.DataTextField = "Nombre"
        ddlTipoContratante.DataValueField = "ContratanteId"
        ddlTipoContratante.DataBind()
        ddlTipoContratante.Items.Insert(0, New ListItem("-- Selecciona --", "0"))

    End Sub
    Public Sub CargarFormaPago(ddlFormaPago As DropDownList)
        Dim api As New ConsumoApi()
        Dim formaPago As String = api.GetFormaPago()

        Dim listaFormaPago As List(Of FormaPago) = ListaSegura(Of FormaPago)(formaPago)

        ddlFormaPago.DataSource = listaFormaPago
        ddlFormaPago.DataTextField = "Nombre"
        ddlFormaPago.DataValueField = "FormaPagoId"
        ddlFormaPago.DataBind()
        ddlFormaPago.Items.Insert(0, New ListItem("-- Selecciona --", "0"))

    End Sub
    Public Sub CargarTipoMoneda(ddlTipoMoneda As DropDownList)
        Dim api As New ConsumoApi()
        Dim tipoMoneda As String = api.GetMoneda()

        Dim listaTipoMoneda As List(Of Moneda) = ListaSegura(Of Moneda)(tipoMoneda)

        ddlTipoMoneda.DataSource = listaTipoMoneda
        ddlTipoMoneda.DataTextField = "Nombre"
        ddlTipoMoneda.DataValueField = "MonedaId"
        ddlTipoMoneda.DataBind()
        ddlTipoMoneda.Items.Insert(0, New ListItem("-- Selecciona --", "0"))
    End Sub

    Public Sub CargarTipoSubRamo(ddlTipoSubRamo As DropDownList)
        Dim api As New ConsumoApi()
        Dim tipoSubRamo As String = api.GetSubRamo()

        Dim listaTipoSubRamo As List(Of SubRamo) = ListaSegura(Of SubRamo)(tipoSubRamo)

        ddlTipoSubRamo.DataSource = listaTipoSubRamo
        ddlTipoSubRamo.DataTextField = "Nombre"
        ddlTipoSubRamo.DataValueField = "SubRamoId"
        ddlTipoSubRamo.DataBind()
        ddlTipoSubRamo.Items.Insert(0, New ListItem("-- Selecciona --", "0"))

    End Sub
    Public Sub CargarTipoProducto(ddlProducto As DropDownList)
        Dim api As New ConsumoApi()
        Dim tipoProducto As String = api.GetProducto()

        Dim listaProducto As List(Of Producto) = ListaSegura(Of Producto)(tipoProducto)

        ddlProducto.DataSource = listaProducto
        ddlProducto.DataTextField = "Nombre"
        ddlProducto.DataValueField = "ProductoId"
        ddlProducto.DataBind()
    End Sub
    Public Sub CargarClasificacion(ddlClasificacion As DropDownList)
        Dim api As New ConsumoApi()
        Dim clasificacion As String = api.GetClasificacion()

        Dim listaClasificacion As List(Of Clasificacion) = ListaSegura(Of Clasificacion)(clasificacion)

        ddlClasificacion.DataSource = listaClasificacion
        ddlClasificacion.DataTextField = "Nombre"
        ddlClasificacion.DataValueField = "ClasificacionId"
        ddlClasificacion.DataBind()
        ddlClasificacion.Items.Insert(0, New ListItem("-- Selecciona --", "0"))

    End Sub
    Public Sub CargarTransito(ddlTransito As DropDownList)
        Dim api As New ConsumoApi()
        Dim transito As String = api.GetTransito()

        Dim listaTransito As List(Of Transito) = ListaSegura(Of Transito)(transito)

        ddlTransito.DataSource = listaTransito
        ddlTransito.DataTextField = "Nombre"
        ddlTransito.DataValueField = "TransitoId"
        ddlTransito.DataBind()
        ddlTransito.Items.Insert(0, New ListItem("-- Selecciona --", "0"))

    End Sub
    Public Sub CargarBeneficiario(ddlBeneficiario As DropDownList)
        Dim api As New ConsumoApi()
        Dim beneficiarios As String = api.GetCargarBeneficiarios()

        Dim lstBeneficiarios As List(Of BeneficiarioPreferente) = ListaSegura(Of BeneficiarioPreferente)(beneficiarios)

        ddlBeneficiario.DataSource = lstBeneficiarios
        ddlBeneficiario.DataTextField = "NombreCompleto"
        ddlBeneficiario.DataValueField = "BeneficiarioPreferenteId"
        ddlBeneficiario.DataBind()

        ddlBeneficiario.Items.Insert(0, New ListItem("Selecciona un beneficiario", "0"))
    End Sub
    Public Sub CargarTipoTarifa(ParamArray ddls() As DropDownList)
        Dim api As New ConsumoApi()
        Dim tipoTarifaJson As String = api.GetTipoTarifa()
        Dim listaTipoTarifa As List(Of TipoTarifa) = ListaSegura(Of TipoTarifa)(tipoTarifaJson)

        For Each ddl As DropDownList In ddls
            ddl.DataSource = listaTipoTarifa
            ddl.DataTextField = "Tarifa"
            ddl.DataValueField = "TipoTarifaId"
            ddl.DataBind()
            ddl.Items.Insert(0, New ListItem("-- Selecciona --", "0"))

        Next
    End Sub

End Module
