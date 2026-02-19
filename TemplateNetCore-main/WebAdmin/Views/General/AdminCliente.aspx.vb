Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
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
            CargarVendedores()
            DropdownHelpers.CargarEstados(ddlEstado, ddlPais.SelectedValue)
            Dim tipoPersonaId As Integer = If(ddlTipoPersona.SelectedValue IsNot Nothing, Convert.ToInt32(ddlTipoPersona.SelectedValue), 1)
            DropdownHelpers.CargarRegimenFiscal(ddlRegimenFiscal, tipoPersonaId)
            DropdownHelpers.CargarTipoTarifa(ddlTipoTarifaSecos, ddlTipoRefrigerados, ddlTipoIsotaques)
            cargarClientes()
            txtFechaRegistro.Text = DateTime.Now.ToString("yyyy-MM-dd")

            ActualizarEstadoCampos(chkHabilitarCampos.Checked)
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
        ddlEstatus.SelectedValue = "1"
        ddlEstatus.Enabled = False
        ddlTipoPersona.Enabled = True
        DropdownHelpers.CargarRegimenFiscal(ddlRegimenFiscal, tipoPersonaId)
        LimpiarFormulario()
    End Sub

    Protected Sub ddlTipoPersona_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim tipoPersonaId As Integer = Convert.ToInt32(ddlTipoPersona.SelectedValue)
        If ddlTipoPersona.SelectedValue = "1" Then
            pnlDatosFiscales.Visible = True
            pnlRazonSocial.Visible = False
            pnlNombreCompleto.Visible = True
            pnlGenero.Visible = True

        ElseIf ddlTipoPersona.SelectedValue = "2" Then
            pnlDatosFiscales.Visible = False
            pnlRazonSocial.Visible = True
            pnlNombreCompleto.Visible = False
            pnlGenero.Visible = False
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
        If e.CommandName = "Editar" Then
            Dim clienteId As Integer = Convert.ToInt32(e.CommandArgument)
            pnlFormularioCliente.Visible = True
            PnlTabla.Visible = False
            PnlEncabezado.Visible = False
            lblMensaje.Text = "Editar Cliente"

            EditarCliente(clienteId)
        End If
        If e.CommandName = "Eliminar" Then
            Dim api As New ConsumoApi()
            Dim clienteId As Integer = Convert.ToInt32(e.CommandArgument)

            Dim eliminado As String = api.DeleteClientes(clienteId)

            cargarClientes()

            If Not String.IsNullOrEmpty(eliminado) Then
                Dim respuestaObj As JObject = JObject.Parse(eliminado)
                Dim mensaje As String

                If respuestaObj("message") IsNot Nothing Then
                    mensaje = respuestaObj("message").ToString()
                Else
                    mensaje = "Cliente eliminado correctamente"
                End If

                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "toast",
        "showToast('" & mensaje & "', 'success');", True)
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "toast",
                "showToast('Error al eliminar el cliente', 'danger');", True)
            End If
        End If
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

        Dim sessionCliente As Cliente = CType(Session("Cliente"), Cliente)
        If sessionCliente Is Nothing Then
            sessionCliente = New Cliente()
        End If

        If sessionCliente Is Nothing OrElse sessionCliente.ClienteVendedor.Count = 0 Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "toast", "showToast('Debe agregar al menos un vendedor', 'danger');", True)
            Exit Sub
        End If
        If sessionCliente.BeneficiarioPreferente.Count = 0 Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "toast", "showToast('Debe agregar al menos un beneficiario preferente', 'danger');", True)
            Exit Sub
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
        .Municipio = txtMunicipio.Text,
        .Colonia = txtColonia.Text,
        .Calle = txtCalle.Text,
        .Cp = txtCP.Text,
        .NumeroInt = txtNumeroInterior.Text,
        .NumeroExt = txtNumeroExterior.Text,
        .Poblacion = txtPoblacion.Text,
        .CuotaAplicableInternacional = If(String.IsNullOrWhiteSpace(txtCuotaInternacional.Text), Nothing, Convert.ToDecimal(txtCuotaInternacional.Text)),
        .CuotaAplicableNacional = If(String.IsNullOrWhiteSpace(txtCuotaNacional.Text), Nothing, Convert.ToDecimal(txtCuotaNacional.Text)),
        .CuotaMinimaInternacional = If(String.IsNullOrWhiteSpace(txtMinimoInternacional.Text), Nothing, Convert.ToDecimal(txtMinimoInternacional.Text)),
        .CuotaMinimaNacional = If(String.IsNullOrWhiteSpace(txtMinimoNacional.Text), Nothing, Convert.ToDecimal(txtMinimoNacional.Text)),
        .Correos = sessionCliente.Correos,
        .BeneficiarioPreferente = sessionCliente.BeneficiarioPreferente,
        .ClienteVendedor = sessionCliente.ClienteVendedor
        }


        Dim listaCuotas As New List(Of Cuota)

        If Not String.IsNullOrWhiteSpace(txtCuotaSecos.Text) Then
            listaCuotas.Add(New Cuota With {
            .Monto = Convert.ToDecimal(txtCuotaSecos.Text),
            .TipoCuotaId = 1,
            .TipoTarifaId = Convert.ToInt32(ddlTipoTarifaSecos.SelectedValue)
            })
        End If

        If Not String.IsNullOrWhiteSpace(txtCuotaRefrigerados.Text) Then
            listaCuotas.Add(New Cuota With {
            .Monto = Convert.ToDecimal(txtCuotaRefrigerados.Text),
            .TipoCuotaId = 2,
            .TipoTarifaId = Convert.ToInt32(ddlTipoRefrigerados.SelectedValue)
            })
        End If

        If Not String.IsNullOrWhiteSpace(txtCuotaIsotanques.Text) Then
            listaCuotas.Add(New Cuota With {
            .Monto = Convert.ToDecimal(txtCuotaIsotanques.Text),
            .TipoCuotaId = 3,
            .TipoTarifaId = Convert.ToInt32(ddlTipoIsotaques.SelectedValue)
            })
        End If

        cliente.Cuota = listaCuotas

        cliente.ClienteCredito = New ClienteCredito With {
        .DiasDeCredito = If(String.IsNullOrWhiteSpace(txtDiasCredito.Text), Nothing, Convert.ToInt32(txtDiasCredito.Text)),
        .MetodoDePago = txtMetodoPago.Text,
        .NumeroCuenta = txtNumeroCuenta.Text,
        .LimiteDeCredito = If(String.IsNullOrWhiteSpace(txtLimiteCredito.Text), Nothing, Convert.ToDecimal(txtLimiteCredito.Text)),
        .DiasDePago = txtDiasPago.Text,
        .DiasDeRevision = txtDiasRevision.Text,
        .Saldo = If(String.IsNullOrWhiteSpace(txtSaldo.Text), Nothing, Convert.ToDecimal(txtSaldo.Text))
    }


        Dim json As String

        json = JsonConvert.SerializeObject(cliente, Formatting.Indented)
        System.Diagnostics.Debug.WriteLine("JSON enviado a API:" & json)

        Dim respuesta As String = ""
        Dim mensajeToast As String = ""

        If Not String.IsNullOrEmpty(hfClienteId.Value) Then
            Dim clienteId As Integer = Convert.ToInt32(hfClienteId.Value)
            respuesta = api.PutEditarCliente(clienteId, json)
            mensajeToast = "Cliente editado correctamente"
        Else
            respuesta = api.PostCliente(json)
            mensajeToast = "Cliente agregado correctamente"
        End If

        System.Diagnostics.Debug.WriteLine("Respuesta API:" & respuesta)

        Dim respuestaObj As JObject = JObject.Parse(respuesta)

        If respuestaObj("errors") IsNot Nothing Then
            Dim erroresJson As String = JsonConvert.SerializeObject(respuestaObj("errors"))
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "apiErrors",
            "showToast('No se pudo guardar el cliente', 'danger'); mostrarErroresApi(" & erroresJson & ");", True)
            Exit Sub
        End If

        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "toast",
        "showToast('" & mensajeToast & "', 'success');", True)

        Session("Cliente") = Nothing

        cargarClientes()
        pnlFormularioCliente.Visible = False
        PnlTabla.Visible = True
        PnlEncabezado.Visible = True
        pnlTabs.Visible = False
        LimpiarFormulario()

    End Sub

    Protected Sub ddlPais_SelectedIndexChanged(sender As Object, e As EventArgs)
        DropdownHelpers.CargarEstados(ddlEstado, ddlPais.SelectedValue)

    End Sub

    Protected Sub ddlEstado_SelectedIndexChanged(sender As Object, e As EventArgs)
    End Sub

    Protected Sub gvListaVendedores_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        If e.CommandName = "Eliminar" Then
            Dim cliente As Cliente = CType(Session("Cliente"), Cliente)
            If cliente IsNot Nothing AndAlso cliente.ClienteVendedor IsNot Nothing Then
                Dim vendedorId As Integer = Convert.ToInt32(e.CommandArgument)
                cliente.ClienteVendedor.RemoveAll(Function(v) v.VendedorId = vendedorId)
                Session("Cliente") = cliente
                CargarGridVendedores()
            End If
        End If
    End Sub

    Protected Sub gvListaVendedores_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)

    End Sub

    Protected Sub btnAgregarCorreo_Click(sender As Object, e As EventArgs)

    End Sub
    Public Sub CargarVendedores(Optional cliente As Cliente = Nothing)
        Dim api As New ConsumoApi()
        Dim cargarVendedores As String = api.GetCargarVendedores()
        Dim listavendedores As List(Of Vendedor) = JsonConvert.DeserializeObject(Of List(Of Vendedor))(cargarVendedores)

        If cliente Is Nothing Then
            cliente = CType(Session("Cliente"), Cliente)
        End If

        Dim vendedoresAsignados As New List(Of Integer)
        If cliente IsNot Nothing AndAlso cliente.ClienteVendedor IsNot Nothing Then
            vendedoresAsignados = cliente.ClienteVendedor.Select(Function(cv) cv.VendedorId).ToList()
        End If

        Dim vendedoresDisponibles = listavendedores.Where(Function(v) Not vendedoresAsignados.Contains(v.VendedorId)).ToList()

        ddlNombreVendedor.Items.Clear()
        ddlNombreVendedor.Items.Add(New ListItem("Selecciona un vendedor", "0"))

        For Each v In vendedoresDisponibles
            Dim value As String = $"{v.VendedorId}|{v.Comision}|{v.TipoVendedorId}"
            ddlNombreVendedor.Items.Add(New ListItem(v.NombreCompleto, value))
        Next
    End Sub



    Protected Sub ddlNombreVendedor_SelectedIndexChanged(sender As Object, e As EventArgs)
        If ddlNombreVendedor.SelectedValue = "0" Then
            txtComision.Text = ""
        Else
            Dim parts() As String = ddlNombreVendedor.SelectedValue.Split("|"c)
            Dim comision As String = parts(1)
            txtComision.Text = comision
        End If

        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "abrirModal",
        "var myModal = new bootstrap.Modal(document.getElementById('modalAgregarVendedor')); myModal.show();", True)
    End Sub

    Protected Sub btnGuardarVendedor_Click(sender As Object, e As EventArgs)
        Dim cliente As Cliente = CType(Session("Cliente"), Cliente)
        If cliente Is Nothing Then cliente = New Cliente()

        If ddlNombreVendedor.SelectedIndex <= 0 Then Exit Sub

        Dim parts() As String = ddlNombreVendedor.SelectedValue.Split("|"c)
        Dim vendedorId As Integer = Convert.ToInt32(parts(0))
        Dim comision As Decimal = Convert.ToDecimal(parts(1))
        Dim tipoVendedorId As Integer = Convert.ToInt32(parts(2))
        Dim nombreVendedor As String = ddlNombreVendedor.SelectedItem.Text

        Dim api As New ConsumoApi()
        Dim jsonTipos As String = api.GetTipoVendedor()
        Dim listaTipos As List(Of TipoVendedor) = JsonConvert.DeserializeObject(Of List(Of TipoVendedor))(jsonTipos)

        Dim tipoTexto As String = listaTipos.FirstOrDefault(Function(t) t.TipoVendedorId = tipoVendedorId)?.Tipo

        If cliente.ClienteVendedor Is Nothing Then
            cliente.ClienteVendedor = New List(Of ClienteVendedor)()
        End If

        If cliente.ClienteVendedor.Any(Function(v) v.VendedorId = vendedorId) Then Exit Sub

        cliente.ClienteVendedor.Add(New ClienteVendedor With {
        .VendedorId = vendedorId,
        .NombreCompleto = nombreVendedor,
        .Tipo = tipoTexto,
        .Comision = comision
    })

        Session("Cliente") = cliente

        CargarGridVendedores()
        LimpiarCamposVendedor()

        CerrarModal("modalAgregarVendedor")
    End Sub


    Public Sub CargarTiposVendedores()
        Dim api As New ConsumoApi()
        Dim jsonTipos As String = api.GetTipoVendedor()
        Dim listaTipos As List(Of TipoVendedor) = JsonConvert.DeserializeObject(Of List(Of TipoVendedor))(jsonTipos)
    End Sub

    Public Sub CargarGridVendedores()
        Dim cliente As Cliente = CType(Session("Cliente"), Cliente)
        If cliente?.ClienteVendedor IsNot Nothing Then

            Dim api As New ConsumoApi()
            Dim jsonVendedores As String = api.GetCargarVendedores()
            Dim listavendedores As List(Of Vendedor) = JsonConvert.DeserializeObject(Of List(Of Vendedor))(jsonVendedores)

            Dim jsonTipos As String = api.GetTipoVendedor()
            Dim listaTipos As List(Of TipoVendedor) = JsonConvert.DeserializeObject(Of List(Of TipoVendedor))(jsonTipos)

            For Each cv In cliente.ClienteVendedor
                Dim v = listavendedores.FirstOrDefault(Function(x) x.VendedorId = cv.VendedorId)
                If v IsNot Nothing Then
                    cv.NombreCompleto = v.NombreCompleto
                    cv.Tipo = listaTipos.FirstOrDefault(Function(t) t.TipoVendedorId = v.TipoVendedorId)?.Tipo
                End If
            Next
        End If

        gvListaVendedores.DataSource = If(cliente?.ClienteVendedor, Nothing)
        gvListaVendedores.DataBind()
    End Sub

    Private Sub LimpiarCamposVendedor()
        ddlNombreVendedor.SelectedIndex = 0
        txtComision.Text = ""
    End Sub

    Private Sub CerrarModal(modalId As String)
        Dim script As String = $"
var myModalEl = document.getElementById('{modalId}');
if (myModalEl) {{
    var myModal = bootstrap.Modal.getInstance(myModalEl);
    if (!myModal) {{ myModal = new bootstrap.Modal(myModalEl); }}
    myModal.hide();

    var backdrops = document.getElementsByClassName('modal-backdrop');
    while(backdrops.length > 0) {{
        backdrops[0].parentNode.removeChild(backdrops[0]);
    }}

    document.body.classList.remove('modal-open');
    document.body.style.overflow = 'auto';
}}"
        ScriptManager.RegisterStartupScript(
        Me,
        Me.GetType(),
        "CerrarModal_" & modalId,
        script,
        True
    )
    End Sub


    Protected Sub GvBeneficiarioPreferente_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        If e.CommandName = "Eliminar" Then
            Dim cliente As Cliente = CType(Session("Cliente"), Cliente)
            If cliente IsNot Nothing AndAlso cliente.BeneficiarioPreferente IsNot Nothing Then
                Dim beneficiarioId As Integer = Convert.ToInt32(e.CommandArgument)
                cliente.BeneficiarioPreferente.RemoveAll(Function(b) b.BeneficiarioPreferenteId = beneficiarioId)
                Session("Cliente") = cliente
                CargarGridBeneficiarios()
            End If
        End If
    End Sub

    Protected Sub GvBeneficiarioPreferente_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)

    End Sub

    Protected Sub btnGuardarBeneficiario_Click(sender As Object, e As EventArgs)
        Dim cliente As Cliente = CType(Session("Cliente"), Cliente)
        If cliente Is Nothing Then cliente = New Cliente()

        Dim beneficiario As New BeneficiarioPreferente With {
        .Nombre = txtNombreBeneficiarioP.Text.Trim(),
        .Domicilio = txtDomicilioBeneficiario.Text.Trim(),
        .RFC = txtRFCBeneficiario.Text.Trim()
        }

        If cliente.BeneficiarioPreferente Is Nothing Then
            cliente.BeneficiarioPreferente = New List(Of BeneficiarioPreferente)
        End If

        If cliente.BeneficiarioPreferente.Any(Function(b) b.RFC = beneficiario.RFC) Then Exit Sub

        cliente.BeneficiarioPreferente.Add(beneficiario)

        Session("Cliente") = cliente

        txtNombreBeneficiarioP.Text = ""
        txtDomicilioBeneficiario.Text = ""
        txtRFCBeneficiario.Text = ""

        CargarGridBeneficiarios()
        CerrarModal("modalAgregarBeneficiario")
    End Sub

    Public Sub CargarGridBeneficiarios()
        Dim cliente As Cliente = CType(Session("Cliente"), Cliente)
        GvBeneficiarioPreferente.DataSource = If(cliente?.BeneficiarioPreferente, Nothing)
        GvBeneficiarioPreferente.DataBind()
    End Sub

    Protected Sub btnGuardarCorreo_Click(sender As Object, e As EventArgs)
        Dim cliente As Cliente = CType(Session("Cliente"), Cliente)
        If cliente Is Nothing Then cliente = New Cliente()

        If cliente.Correos Is Nothing Then
            cliente.Correos = New List(Of Correos)
        End If

        Dim nuevoId As Integer = 1
        If cliente.Correos.Count > 0 Then
            nuevoId = cliente.Correos.Max(Function(c) c.CorreoId) + 1
        End If

        Dim correo As New Correos With {
        .CorreoId = nuevoId,
        .TipoCorreoId = Convert.ToInt32(ddlTipoCorreo.SelectedValue),
        .Correo = txtCorreoAdicional.Text
        }


        cliente.Correos.Add(correo)
        Session("Cliente") = cliente

        txtCorreoAdicional.Text = ""
        ddlTipoCorreo.SelectedIndex = 0

        CargarGridCorreos()
        CerrarModal("modalCorreo")
    End Sub

    Public Sub CargarGridCorreos()
        Dim cliente As Cliente = CType(Session("Cliente"), Cliente)
        gvCorreosAdicionales.DataSource = If(cliente?.Correos, Nothing)
        gvCorreosAdicionales.DataBind()
    End Sub

    Private Sub LimpiarFormulario()
        txtNombre.Text = ""
        txtApellidoP.Text = ""
        txtApellidoM.Text = ""
        txtRazonSocial.Text = ""
        txtClave.Text = ""
        txtRFC.Text = ""
        txtTelefono.Text = ""
        txtCorreo.Text = ""
        txtNacionalidad.Text = ""
        txtColonia.Text = ""
        txtCalle.Text = ""
        txtCP.Text = ""
        txtNumeroInterior.Text = ""
        txtNumeroExterior.Text = ""
        txtPoblacion.Text = ""
        txtCuotaInternacional.Text = ""
        txtCuotaNacional.Text = ""
        txtMinimoInternacional.Text = ""
        txtMinimoNacional.Text = ""
        txtCuotaSecos.Text = ""
        txtCuotaRefrigerados.Text = ""
        txtCuotaIsotanques.Text = ""
        txtDiasCredito.Text = ""
        txtMetodoPago.Text = ""
        txtNumeroCuenta.Text = ""
        txtLimiteCredito.Text = ""
        txtDiasPago.Text = ""
        txtDiasRevision.Text = ""
        txtSaldo.Text = ""
        txtMunicipio.Text = ""

        ddlTipoPersona.SelectedIndex = 0
        ddlEstatus.SelectedIndex = 0
        ddlRegimenFiscal.SelectedIndex = 0
        ddlSeguroContrata.SelectedIndex = 0
        ddlTipoCuenta.SelectedIndex = 0
        ddlOrigenCliente.SelectedIndex = 0
        ddlSector.SelectedIndex = 0
        ddlGenero.SelectedIndex = 0
        ddlPais.SelectedIndex = 0
        ddlEstado.Items.Clear()
        ddlEstado.Items.Add(New ListItem("Selecciona un estado", ""))
        ddlRFCGenerico.SelectedIndex = 0
        ddlTipoTarifaSecos.SelectedIndex = 0
        ddlTipoRefrigerados.SelectedIndex = 0
        ddlTipoIsotaques.SelectedIndex = 0

        hfClienteId.Value = ""
        hfTipoPersona.Value = ""
        Session.Remove("Cliente")

        gvListaVendedores.DataSource = Nothing
        gvListaVendedores.DataBind()

        GvBeneficiarioPreferente.DataSource = Nothing
        GvBeneficiarioPreferente.DataBind()

        gvCorreosAdicionales.DataSource = Nothing
        gvCorreosAdicionales.DataBind()
    End Sub

    Protected Sub EditarCliente(clienteId As Integer)
        Dim api As New ConsumoApi
        Dim jsonCliente As String = api.GetClienteId(clienteId)
        Dim cliente As Cliente = JsonConvert.DeserializeObject(Of Cliente)(jsonCliente)

        hfClienteId.Value = cliente.ClienteId.ToString()

        hfTipoPersona.Value = cliente.TipoPersonaId.ToString()
        ddlTipoPersona.SelectedValue = cliente.TipoPersonaId.ToString()
        txtClave.Text = cliente.Clave
        txtNombre.Text = cliente.Nombres
        txtApellidoP.Text = cliente.ApellidoPaterno
        txtApellidoM.Text = cliente.ApellidoMaterno
        txtNombreCompleto.Text = If(cliente.TipoPersonaId = 1, cliente.NombreCompleto, "")
        txtRazonSocial.Text = If(cliente.TipoPersonaId = 2, cliente.NombreCompleto, "")
        txtRFC.Text = cliente.Rfc
        txtTelefono.Text = cliente.Telefono
        txtCorreo.Text = cliente.CorreoElectronico
        txtNacionalidad.Text = cliente.Nacionalidad
        txtColonia.Text = cliente.Colonia
        txtCalle.Text = cliente.Calle
        txtCP.Text = cliente.Cp
        txtNumeroInterior.Text = cliente.NumeroInt
        txtNumeroExterior.Text = cliente.NumeroExt
        txtPoblacion.Text = cliente.Poblacion
        txtMunicipio.Text = cliente.Municipio
        txtCuotaInternacional.Text = If(cliente.CuotaAplicableInternacional IsNot Nothing, cliente.CuotaAplicableInternacional.ToString(), "")
        txtCuotaNacional.Text = If(cliente.CuotaAplicableNacional IsNot Nothing, cliente.CuotaAplicableNacional.ToString(), "")
        txtMinimoInternacional.Text = If(cliente.CuotaMinimaInternacional IsNot Nothing, cliente.CuotaMinimaInternacional.ToString(), "")
        txtMinimoNacional.Text = If(cliente.CuotaMinimaNacional IsNot Nothing, cliente.CuotaMinimaNacional.ToString(), "")

        ddlTipoPersona.Enabled = False
        ddlEstatus.Enabled = true


        If cliente.RfcGenericoId IsNot Nothing AndAlso
       ddlRFCGenerico.Items.FindByValue(cliente.RfcGenericoId.ToString()) IsNot Nothing Then
            ddlRFCGenerico.SelectedValue = cliente.RfcGenericoId.ToString()
        Else
            ddlRFCGenerico.SelectedIndex = 0
        End If

        If cliente.TipoCuentaId IsNot Nothing AndAlso
       ddlTipoCuenta.Items.FindByValue(cliente.TipoCuentaId.ToString()) IsNot Nothing Then
            ddlTipoCuenta.SelectedValue = cliente.TipoCuentaId.ToString()
        Else
            ddlTipoCuenta.SelectedIndex = 0
        End If

        If cliente.TipoSeguroId IsNot Nothing AndAlso
       ddlSeguroContrata.Items.FindByValue(cliente.TipoSeguroId.ToString()) IsNot Nothing Then
            ddlSeguroContrata.SelectedValue = cliente.TipoSeguroId.ToString()
        Else
            ddlSeguroContrata.SelectedIndex = 0
        End If

        If cliente.OrigenClienteId IsNot Nothing AndAlso
       ddlOrigenCliente.Items.FindByValue(cliente.OrigenClienteId.ToString()) IsNot Nothing Then
            ddlOrigenCliente.SelectedValue = cliente.OrigenClienteId.ToString()
        Else
            ddlOrigenCliente.SelectedIndex = 0
        End If

        If cliente.TipoSectorId IsNot Nothing AndAlso
       ddlSector.Items.FindByValue(cliente.TipoSectorId.ToString()) IsNot Nothing Then
            ddlSector.SelectedValue = cliente.TipoSectorId.ToString()
        Else
            ddlSector.SelectedIndex = 0
        End If

        If Not String.IsNullOrEmpty(cliente.Genero) AndAlso
       ddlGenero.Items.FindByValue(cliente.Genero) IsNot Nothing Then
            ddlGenero.SelectedValue = cliente.Genero
        Else
            ddlGenero.SelectedIndex = 0
        End If

        If Not String.IsNullOrEmpty(cliente.EstatusId) AndAlso ddlEstatus.Items.FindByValue(cliente.EstatusId) IsNot Nothing Then
            ddlEstatus.SelectedValue = cliente.EstatusId
        End If
        ddlEstatus.Enabled = True


        If Not String.IsNullOrEmpty(cliente.Pais) AndAlso
       ddlPais.Items.FindByValue(cliente.Pais) IsNot Nothing Then

            ddlPais.SelectedValue = cliente.Pais
            DropdownHelpers.CargarEstados(ddlEstado, cliente.Pais)

            If Not String.IsNullOrEmpty(cliente.Estado) AndAlso
           ddlEstado.Items.FindByValue(cliente.Estado) IsNot Nothing Then
                ddlEstado.SelectedValue = cliente.Estado
            End If
        End If

        DropdownHelpers.CargarRegimenFiscal(ddlRegimenFiscal, cliente.TipoPersonaId)

        If Not String.IsNullOrEmpty(cliente.RegimenFiscalId) AndAlso
       ddlRegimenFiscal.Items.FindByValue(cliente.RegimenFiscalId) IsNot Nothing Then
            ddlRegimenFiscal.SelectedValue = cliente.RegimenFiscalId
        Else
            ddlRegimenFiscal.SelectedIndex = 0
        End If

        If cliente.Cuota IsNot Nothing Then
            For Each c In cliente.Cuota
                Select Case c.TipoCuotaId
                    Case 1
                        txtCuotaSecos.Text = If(c.Monto IsNot Nothing, c.Monto.ToString(), "")
                        If ddlTipoTarifaSecos.Items.FindByValue(c.TipoTarifaId) IsNot Nothing Then ddlTipoTarifaSecos.SelectedValue = c.TipoTarifaId
                    Case 2
                        txtCuotaRefrigerados.Text = If(c.Monto IsNot Nothing, c.Monto.ToString(), "")
                        If ddlTipoRefrigerados.Items.FindByValue(c.TipoTarifaId) IsNot Nothing Then ddlTipoRefrigerados.SelectedValue = c.TipoTarifaId
                    Case 3
                        txtCuotaIsotanques.Text = If(c.Monto IsNot Nothing, c.Monto.ToString(), "")
                        If ddlTipoIsotaques.Items.FindByValue(c.TipoTarifaId) IsNot Nothing Then ddlTipoIsotaques.SelectedValue = c.TipoTarifaId
                End Select
            Next
        End If

        If cliente.ClienteCredito IsNot Nothing Then
            txtDiasCredito.Text = If(cliente.ClienteCredito.DiasDeCredito IsNot Nothing, cliente.ClienteCredito.DiasDeCredito.ToString(), "")
            txtMetodoPago.Text = cliente.ClienteCredito.MetodoDePago
            txtNumeroCuenta.Text = cliente.ClienteCredito.NumeroCuenta
            txtLimiteCredito.Text = If(cliente.ClienteCredito.LimiteDeCredito IsNot Nothing, cliente.ClienteCredito.LimiteDeCredito.ToString(), "")
            txtDiasPago.Text = cliente.ClienteCredito.DiasDePago
            txtDiasRevision.Text = cliente.ClienteCredito.DiasDeRevision
            txtSaldo.Text = If(cliente.ClienteCredito.Saldo IsNot Nothing, cliente.ClienteCredito.Saldo.ToString(), "")
        End If

        Session("Cliente") = cliente
        CargarVendedores(cliente)
        CargarGridVendedores()
        CargarGridBeneficiarios()
        CerrarModal("modalAgregarVendedor")
        CargarGridCorreos()

        If cliente.TipoPersonaId = 1 Then
            pnlDatosFiscales.Visible = True
            pnlRazonSocial.Visible = False
            pnlNombreCompleto.Visible = True
            pnlGenero.Visible = True
        Else
            pnlDatosFiscales.Visible = False
            pnlRazonSocial.Visible = True
            pnlNombreCompleto.Visible = False
            pnlGenero.Visible = False
        End If

        pnlFormularioCliente.Visible = True
        PnlTabla.Visible = False
        PnlEncabezado.Visible = False
        pnlTabs.Visible = True
        pnlGestionarCredito.Visible = True
        pnlEstadoCuenta.Visible = True
        lblMensaje.Text = "Editar Cliente"
    End Sub

    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs)
        cargarClientes()
        pnlFormularioCliente.Visible = False
        PnlTabla.Visible = True
        PnlEncabezado.Visible = True
        pnlTabs.Visible = False
        LimpiarFormulario()
    End Sub

    Protected Sub gvCorreosAdicionales_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        If e.CommandName = "Eliminar" Then
            Dim cliente As Cliente = CType(Session("Cliente"), Cliente)
            If cliente IsNot Nothing AndAlso cliente.Correos IsNot Nothing Then
                Dim correoId As Integer = Convert.ToInt32(e.CommandArgument)
                cliente.Correos.RemoveAll(Function(v) v.CorreoId = correoId)
                Session("Cliente") = cliente
                CargarGridCorreos()
            End If
        End If
    End Sub

    Protected Sub chkHabilitarCampos_CheckedChanged(sender As Object, e As EventArgs)
        ActualizarEstadoCampos(chkHabilitarCampos.Checked)
    End Sub
    Private Sub ActualizarEstadoCampos(estaHabilitado As Boolean)
        txtDiasCredito.Enabled = estaHabilitado
        txtMetodoPago.Enabled = estaHabilitado
        txtNumeroCuenta.Enabled = estaHabilitado
        txtLimiteCredito.Enabled = estaHabilitado
        txtDiasPago.Enabled = estaHabilitado
        txtDiasRevision.Enabled = estaHabilitado
        txtSaldo.Enabled = estaHabilitado
    End Sub

    Protected Sub txtBuscarCliente_TextChanged(sender As Object, e As EventArgs)
        filtrarClientes()
    End Sub

    Protected Sub filtrarClientes()
        Dim api As New ConsumoApi()
        Dim json As String = api.GetCargarClientes()
        Dim lista As List(Of Cliente) = JsonConvert.DeserializeObject(Of List(Of Cliente))(json)

        Dim texto As String = txtBuscarCliente.Text.Trim().ToLower()
        Dim estatusSeleccionado As Integer = Convert.ToInt32(ddlTipoEstatusCliente.SelectedValue)


        Dim filtrados = lista.Where(Function(v)
                                        Dim coincideTexto As Boolean = String.IsNullOrEmpty(texto) OrElse
                                       (If(v.NombreCompleto, "").ToLower().Contains(texto)) OrElse
                                       (If(v.Rfc, "").ToLower().Contains(texto))
                                        Dim coincideEstatus As Boolean = estatusSeleccionado = 0 OrElse v.EstatusId = estatusSeleccionado
                                        Return coincideTexto AndAlso coincideEstatus
                                    End Function).ToList()

        gvClientes.DataSource = filtrados
        gvClientes.DataBind()
    End Sub

    Protected Sub ddlTipoEstatusCliente_SelectedIndexChanged(sender As Object, e As EventArgs)
        filtrarClientes()
    End Sub
End Class