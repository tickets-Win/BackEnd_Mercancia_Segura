Imports Newtonsoft.Json
Imports WebAdmin.MercanciaSegura.DOM.Modelos
Public Class AdminCliente
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            pnlTabs.Visible = False
            pnlGestionarCredito.Visible = False
            pnlEstadoCuenta.Visible = False
            DropdownHelpers.CargarTipoPersona(ddlTipoPersona)
            DropdownHelpers.CargarTipoEstatus(ddlEstatus)
            DropdownHelpers.CargarTipoSeguro(ddlSeguroContrata)
            DropdownHelpers.CargarTipoCuenta(ddlTipoCuenta)
            DropdownHelpers.CargarOrigenCliente(ddlOrigenCliente)
            DropdownHelpers.CargarTipoSector(ddlSector)
            DropdownHelpers.CargarRFCGenerico(ddlRFCGenerico)
            DropdownHelpers.CargarTipoCorreo(ddlTipoCorreo)
            DropdownHelpers.CargarVendedores(ddlNombreVendedor)
            Dim tipoPersonaId As Integer = If(ddlTipoPersona.SelectedValue IsNot Nothing, Convert.ToInt32(ddlTipoPersona.SelectedValue), 1)
            DropdownHelpers.CargarRegimenFiscal(ddlRegimenFiscal, tipoPersonaId)
            DropdownHelpers.CargarTipoTarifa(ddlTipoTarifaSecos, ddlTipoRefrigerados, ddlTipoIsotaques)
            cargarClientes()
            ddlEstado.Items.Clear()
            ddlMunicipio.Items.Clear()

            If Session("Cliente") Is Nothing Then
                Session("Cliente") = New Cliente()
            End If
        End If
    End Sub

    Protected Sub btnAgregarCliente_Click(sender As Object, e As EventArgs)
        Dim tipoPersonaId As Integer = Convert.ToInt32(ddlTipoPersona.SelectedValue)

        pnlFormularioCliente.Visible = True
        PnlTabla.Visible = False
        PnlEncabezado.Visible = False
        lblMensaje.Text = "Nuevo Registro"
        pnlTabs.Visible = True
        pnlGestionarCredito.Visible = True
        pnlEstadoCuenta.Visible = True

        DropdownHelpers.CargarRegimenFiscal(ddlRegimenFiscal, tipoPersonaId)
    End Sub

    Protected Sub ddlTipoPersona_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim tipoPersonaId As Integer = Convert.ToInt32(ddlTipoPersona.SelectedValue)
        If ddlTipoPersona.SelectedValue = "1" Then
            pnlDatosFiscales.Visible = True
            pnlRazonSocial.Visible = False
            pnlNombreCompleto.Visible = True

        ElseIf ddlTipoPersona.SelectedValue = "2" Then
            pnlDatosFiscales.Visible = False
            pnlRazonSocial.Visible = True
            pnlNombreCompleto.Visible = False
        End If
        DropdownHelpers.CargarRegimenFiscal(ddlRegimenFiscal, tipoPersonaId)
    End Sub

    Protected Sub cargarClientes()
        Dim api As New ConsumoApi()
        Dim cargarClientes As String = api.GetCargarClientes()

        Dim listaClientes As List(Of Cliente) = JsonConvert.DeserializeObject(Of List(Of Cliente))(cargarClientes)

        gvClientes.DataSource = listaClientes
        gvClientes.DataBind()
    End Sub
    Protected Sub gvClientes_RowCommand(sender As Object, e As GridViewCommandEventArgs)

    End Sub

    Protected Sub gvClientes_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        gvClientes.PageIndex = e.NewPageIndex
        cargarClientes()
    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs)
        Dim api As New ConsumoApi()

        Dim tipoPersonaId As Integer = Convert.ToInt32(ddlTipoPersona.SelectedValue)

        Dim nombreCompleto As String
        If tipoPersonaId = 1 Then
            nombreCompleto = txtNombre.Text.Trim() & " " & txtApellidoP.Text.Trim() & " " & txtApellidoM.Text.Trim()
        Else
            nombreCompleto = txtRazonSocial.Text.Trim()
        End If

        Dim cliente As New Cliente With {
        .TipoPersonaId = tipoPersonaId,
        .Clave = txtClave.Text,
        .EstatusId = Convert.ToInt32(ddlEstatus.SelectedValue),
        .ApellidoPaterno = txtApellidoP.Text,
        .ApellidoMaterno = txtApellidoM.Text,
        .Nombres = txtNombre.Text,
        .NombreCompleto = nombreCompleto,
        .RegimenFiscalId = ddlRegimenFiscal.SelectedValue,
        .Rfc = txtRFC.Text,
        .RfcGenericoId = ddlRFCGenerico.SelectedValue,
        .FechaRegistro = Date.Now,
        .TipoSeguroId = ddlSeguroContrata.SelectedValue,
        .TipoCuentaId = ddlTipoCuenta.SelectedValue,
        .OrigenClienteId = ddlOrigenCliente.SelectedValue,
        .TipoSectorId = ddlSector.SelectedValue,
        .Telefono = txtTelefono.Text,
        .CorreoElectronico = txtCorreo.Text,
        .Nacionalidad = txtNacionalidad.Text,
        .Genero = ddlGenero.SelectedValue,
        .Pais = ddlPais.SelectedValue,
        .Estado = ddlEstado.SelectedValue,
        .Municipio = ddlMunicipio.SelectedValue,
        .Colonia = txtColonia.Text,
        .Calle = txtCalle.Text,
        .Cp = txtCP.Text,
        .NumeroInt = txtNumeroInterior.Text,
        .NumeroExt = txtNumeroExterior.Text,
        .Poblacion = ddlPoblacion.SelectedValue,
        .CuotaAplicableInternacional = txtCuotaInternacional.Text,
        .CuotaAplicableNacional = txtCuotaNacional.Text,
        .CuotaMinimaInternacional = txtMinimoInternacional.Text,
        .CuotaMinimaNacional = txtMinimoNacional.Text
        }

        Dim clienteSession As Cliente = CType(Session("Cliente"), Cliente)
        cliente.ClienteVendedor = clienteSession.ClienteVendedor


        '        Dim listaCuotas As New List(Of Cuota)

        '        If Not String.IsNullOrWhiteSpace(txtCuotaSecos.Text) Then
        '            listaCuotas.Add(New Cuota With {
        '            .Monto = Convert.ToDecimal(txtCuotaSecos.Text),
        '            .TipoCuotaId = 1,
        '            .TipoTarifaId = Convert.ToInt32(ddlTipoTarifaSecos.SelectedValue)
        '            })
        '        End If

        '        If Not String.IsNullOrWhiteSpace(txtCuotaRefrigerados.Text) Then
        '            listaCuotas.Add(New Cuota With {
        '            .Monto = Convert.ToDecimal(txtCuotaRefrigerados.Text),
        '            .TipoCuotaId = 2,
        '            .TipoTarifaId = Convert.ToInt32(ddlTipoRefrigerados.SelectedValue)
        '            })
        '        End If

        '        If Not String.IsNullOrWhiteSpace(txtCuotaIsotanques.Text) Then
        '            listaCuotas.Add(New Cuota With {
        '            .Monto = Convert.ToDecimal(txtCuotaIsotanques.Text),
        '            .TipoCuotaId = 3,
        '            .TipoTarifaId = Convert.ToInt32(ddlTipoIsotaques.SelectedValue)
        '            })
        '        End If

        '        cliente.Cuota = listaCuotas        

        '        Dim clienteCredito As New ClienteCredito With {
        '    .DiasDeCredito = If(String.IsNullOrWhiteSpace(txtDiasCredito.Text), Nothing, Convert.ToInt32(txtDiasCredito.Text)),
        '    .MetodoDePago = txtMetodoPago.Text,
        '    .NumeroCuenta = txtNumeroCuenta.Text,
        '    .LimiteDeCredito = If(String.IsNullOrWhiteSpace(txtLimiteCredito.Text), Nothing, Convert.ToDecimal(txtLimiteCredito.Text)),
        '    .DiasDePago = txtDiasPago.Text,
        '    .DiasDeRevision = txtDiasRevision.Text,
        '    .Saldo = If(String.IsNullOrWhiteSpace(txtSaldo.Text), Nothing, Convert.ToDecimal(txtSaldo.Text))
        '}

        Dim json As String = JsonConvert.SerializeObject(cliente)
        Dim respuesta As String

        Dim totalVendedores = cliente.ClienteVendedor.Count


        respuesta = api.PostCliente(json)


    End Sub

    Public Sub CargarVendedores()
        Dim api As New ConsumoApi()
        Dim cargarVendedores As String = api.GetCargarVendedores()

        Dim listavendedores As List(Of Vendedor) = JsonConvert.DeserializeObject(Of List(Of Vendedor))(cargarVendedores)

        gvListaVendedores.DataSource = listavendedores
        gvListaVendedores.DataBind()
    End Sub
    Protected Sub ddlPais_SelectedIndexChanged(sender As Object, e As EventArgs)
        ddlEstado.Items.Clear()
        ddlEstado.Items.Add(New ListItem("Selecciona un estado", ""))

        Select Case ddlPais.SelectedValue
            Case "MX"
                ddlEstado.Items.Add(New ListItem("CDMX", "CDMX"))
                ddlEstado.Items.Add(New ListItem("PUEBLA", "PUE"))
            Case "US"
                ddlEstado.Items.Add(New ListItem("California", "CA"))
                ddlEstado.Items.Add(New ListItem("Texas", "TX"))
        End Select

        ddlMunicipio.Items.Clear()
        ddlMunicipio.Items.Add(New ListItem("Selecciona un municipio", ""))
    End Sub
    Protected Sub ddlEstado_SelectedIndexChanged(sender As Object, e As EventArgs)
        ddlMunicipio.Items.Clear()
        ddlMunicipio.Items.Add(New ListItem("Selecciona un municipio", ""))

        Select Case ddlEstado.SelectedValue
            Case "CDMX"
                ddlMunicipio.Items.Add(New ListItem("Álvaro Obregón", "AO"))
                ddlMunicipio.Items.Add(New ListItem("Coyoacán", "CO"))
            Case "JAL"
                ddlMunicipio.Items.Add(New ListItem("Guadalajara", "GDL"))
                ddlMunicipio.Items.Add(New ListItem("Zapopan", "ZAP"))
            Case "CA"
                ddlMunicipio.Items.Add(New ListItem("Los Ángeles", "LA"))
                ddlMunicipio.Items.Add(New ListItem("San Francisco", "SF"))
            Case "TX"
                ddlMunicipio.Items.Add(New ListItem("Houston", "HOU"))
                ddlMunicipio.Items.Add(New ListItem("Dallas", "DAL"))
        End Select
    End Sub

    Protected Sub gvListaVendedores_RowCommand(sender As Object, e As GridViewCommandEventArgs)

    End Sub

    Protected Sub gvListaVendedores_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)

    End Sub

    Protected Sub ddlNombreVendedor_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim ddl As DropDownList = CType(sender, DropDownList)
        If ddl.SelectedIndex > 0 Then
            Dim comision As Decimal = Convert.ToDecimal(ddl.SelectedItem.Attributes("data-comision"))
            txtComision.Text = comision.ToString("N2")
        Else
            txtComision.Text = ""
        End If
    End Sub

    Protected Sub btnAgregarCorreo_Click(sender As Object, e As EventArgs)

    End Sub

    Protected Sub btnGuardarVendedor_Click(sender As Object, e As EventArgs)
        Dim cliente As Cliente = CType(Session("Cliente"), Cliente)

        Dim vendedorId As Integer = Convert.ToInt32(ddlNombreVendedor.SelectedValue)
        Dim comision As Decimal = Convert.ToDecimal(txtComision.Text)

        If cliente.ClienteVendedor.Any(Function(v) v.VendedorId = vendedorId) Then
            Exit Sub
        End If

        cliente.ClienteVendedor.Add(New ClienteVendedor With {
        .VendedorId = vendedorId,
        .Comision = comision
    })

        Session("Cliente") = cliente

        CargarGridVendedores()
        CerrarModal()
    End Sub

    Public Sub CargarGridVendedores()
        Dim cliente As Cliente = CType(Session("Cliente"), Cliente)

        gvListaVendedores.DataSource = cliente.ClienteVendedor
        gvListaVendedores.DataBind()
    End Sub

    Private Sub CerrarModal()
        ScriptManager.RegisterStartupScript(Me, Me.GetType(),
        "cerrarModal",
        "var modal = bootstrap.Modal.getInstance(document.getElementById('modalAgregarVendedor')); modal.hide();",
        True)
    End Sub

End Class