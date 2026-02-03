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
            Dim tipoPersonaId As Integer = If(ddlTipoPersona.SelectedValue IsNot Nothing, Convert.ToInt32(ddlTipoPersona.SelectedValue), 1)
            DropdownHelpers.CargarRegimenFiscal(ddlRegimenFiscal, tipoPersonaId)
            DropdownHelpers.CargarTipoTarifa(ddlTipoTarifaSecos, ddlTipoRefrigerados, ddlTipoIsotaques)
            cargarClientes()
            ddlEstado.Items.Clear()
            ddlMunicipio.Items.Clear()
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

        '        'Dim listaCorreos As New List(Of Correos)

        '        'If Not String.IsNullOrWhiteSpace(txtCorreoFacturas.Text) Then
        '        '    For Each correo In txtCorreoFacturas.Text.Split(","c)
        '        '        listaCorreos.Add(New Correos With {
        '        '        .Correo = correo.Trim(),
        '        '        .TipoCorreoId = 1
        '        '        })
        '        '    Next
        '        'End If

        '        'If Not String.IsNullOrWhiteSpace(txtCorreosRecepcion.Text) Then
        '        '    For Each correo In txtCorreosRecepcion.Text.Split(","c)
        '        '        listaCorreos.Add(New Correos With {
        '        '        .Correo = correo.Trim(),
        '        '        .TipoCorreoId = 2
        '        '        })
        '        '    Next
        '        'End If

        '        'If Not String.IsNullOrWhiteSpace(txtCorreosAdicionales.Text) Then
        '        '    For Each correo In txtCorreosAdicionales.Text.Split(","c)
        '        '        listaCorreos.Add(New Correos With {
        '        '        .Correo = correo.Trim(),
        '        '        .TipoCorreoId = 3
        '        '        })
        '        '    Next
        '        'End If

        '        'cliente.Correos = listaCorreos

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

        'Dim vendedoresJson As String = api.GetCargarVendedores()
        'Dim listaVendedores As List(Of ClienteVendedor) = JsonConvert.DeserializeObject(Of List(Of ClienteVendedor))(vendedoresJson)

        'ddlNombreVendedor.DataSource = listaVendedores
        'ddlNombreVendedor.DataTextField = "Nombre"
        'ddlNombreVendedor.DataValueField = "VendedorId"
        'ddlNombreVendedor.DataBind()

        'ddlNombreVendedor.Items.Insert(0, New ListItem("Selecciona un vendedor", "0"))

        'Dim listaClienteVendedores As New List(Of ClienteVendedor)

        'If ddlNombreVendedor.SelectedValue <> "0" Then
        '    Dim clienteVendedor As New ClienteVendedor With {
        '        .VendedorId = Convert.ToInt32(ddlNombreVendedor.SelectedValue),
        '        .Comision = If(String.IsNullOrWhiteSpace(txtComision.Text), 0, Convert.ToDecimal(txtComision.Text))
        '    }
        '    listaClienteVendedores.Add(clienteVendedor)
        'End If

        'cliente.ClienteVendedor = listaClienteVendedores

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


        respuesta = api.PostCliente(json)


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
End Class