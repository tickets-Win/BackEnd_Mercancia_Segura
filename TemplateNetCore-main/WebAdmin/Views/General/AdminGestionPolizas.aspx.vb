Imports Newtonsoft.Json
Imports WebAdmin.MercanciaSegura.DOM.Modelos

Public Class AdminGestionPolizas
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            pnlFormularioPolizas.Visible = False
            pnlNombreInternoPoliza.Visible = False
            pnlContenedor.Visible = False
            cargarPolizas()
            DropdownHelpers.CargarTipoProducto(ddlProducto)
            DropdownHelpers.CargarTipoContratante(ddlContratante)
            DropdownHelpers.CargarTipoAseguradora(ddlAseguradora)
            DropdownHelpers.CargarTipoSubRamo(ddlSubRamo)
            DropdownHelpers.CargarFormaPago(ddlFormaPago)
            DropdownHelpers.CargarTipoMoneda(ddlMoneda)
        End If
    End Sub

    Protected Sub btnAgregarPoliza_Click(sender As Object, e As EventArgs)
        pnlFormularioPolizas.Visible = True
        pnlEncabezado.Visible = False
        PnlTabla.Visible = False
        lblMensaje.Text = "Nuevo Registro"
    End Sub

    Protected Sub ddlProducto_SelectedIndexChanged(sender As Object, e As EventArgs)
        If ddlProducto.SelectedValue = "1" Then
            pnlNombreInternoPoliza.Visible = False
            pnlMercancia.Visible = True
            pnlContenedor.Visible = False
            pnlMediosTransporte.Visible = False


        ElseIf ddlProducto.SelectedValue = "2" Then
            pnlNombreInternoPoliza.Visible = True
            pnlContenedor.Visible = True
            pnlMercancia.Visible = False
            pnlMediosTransporte.Visible = True

        End If
    End Sub

    Protected Sub cargarPolizas()
        Dim api As New ConsumoApi
        Dim cargarPolizas As String = api.GetCargarPolizas()

        Dim lstPolizas As List(Of Poliza) = JsonConvert.DeserializeObject(Of List(Of Poliza))(cargarPolizas)

        gvPolizas.DataSource = lstPolizas
        gvPolizas.DataBind()
    End Sub
    Protected Sub gvPolizas_RowCommand(sender As Object, e As GridViewCommandEventArgs)

    End Sub

    Protected Sub gvPolizas_PageIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs)
        Dim api As New ConsumoApi()
        Dim ProductoId As Integer = Convert.ToInt32(ddlProducto.SelectedValue)

        Dim poliza As New Poliza With {
            .ProductoId = ProductoId,
            .TipoPoliza = txtTipoPoliza.Text,
            .NumeroPoliza = txtNumeroPoliza.Text,
            .ContratanteId = ddlContratante.SelectedValue,
            .AseguradoraId = ddlAseguradora.SelectedValue,
            .SubRamoId = ddlSubRamo.SelectedValue,
            .VigenciaDel = Date.Now,
            .VigenciaHasta = Date.Now,
            .EstatusPolizaId = ddlEstatus.SelectedValue,
            .FormaPagoId = ddlFormaPago.SelectedValue,
            .MonedaId = ddlMoneda.SelectedValue,
            .ClaveAgente = txtClaveAgente.Text,
            .FolioPoliza = txtFolioPoliza.Text,
            .PrimaNeta = If(String.IsNullOrWhiteSpace(txtPrimaNetaMercancia1.Text), Nothing, Convert.ToDecimal(txtPrimaNetaMercancia1.Text)),
            .DerechoPoliza = If(String.IsNullOrWhiteSpace(txtDerechoPolizaMercancia1.Text), Nothing, Convert.ToDecimal(txtDerechoPolizaMercancia1.Text)),
            .OtrosPoliza = If(String.IsNullOrWhiteSpace(txtOtrosBasesIndemnizacion1.Text), Nothing, Convert.ToDecimal(txtOtrosBasesIndemnizacion1.Text)),
            .IVA = If(String.IsNullOrWhiteSpace(txtIVA.Text), Nothing, Convert.ToDecimal(txtIVA.Text)),
            .PrimaTotal = If(String.IsNullOrWhiteSpace(txtPrimaTotalMercancia1.Text), Nothing, Convert.ToDecimal(txtPrimaTotalMercancia1.Text)),
            .Otros = If(String.IsNullOrWhiteSpace(txtOtrosMercancia1.Text), Nothing, Convert.ToDecimal(txtOtrosMercancia1.Text))
            }

        Dim polizaMercanciaList As New List(Of PolizaMercancia)

        polizaMercanciaList.Add(New PolizaMercancia With {
        .AdministracionBienId = 1,
        .NombreInternoPoliza = txtNombreInternoPoliza1.Text,
        .TerrestreAereo = If(String.IsNullOrWhiteSpace(txtTerrestreAereo1.Text), Nothing, Convert.ToDecimal(txtTerrestreAereo1.Text)),
        .Maritimo = If(String.IsNullOrWhiteSpace(txtMaritimo1.Text), Nothing, Convert.ToDecimal(txtMaritimo1.Text)),
        .PaqueteriaMensajeria = If(String.IsNullOrWhiteSpace(txtPaqueteria1.Text), Nothing, Convert.ToDecimal(txtPaqueteria1.Text)),
        .Deducibles = txtDeducibles1.Text,
        .Compras = txtCompras1.Text,
        .Ventas = txtVentas1.Text,
        .Maquila = txtMaquila1.Text,
        .BienesUsados = txtBienesUsados1.Text,
        .EmbarqueFiliales = txtEmbarquesEntreFiliales1.Text,
        .IndemnizacionOtros = txtOtrosMercancia1.Text,
        .CuotaGeneralPoliza = If(String.IsNullOrWhiteSpace(txtCuotaGeneralPoliza1.Text), Nothing, Convert.ToDecimal(txtCuotaGeneralPoliza1.Text)),
        .Medicamentos = If(String.IsNullOrWhiteSpace(txtMedicamentos1.Text), Nothing, Convert.ToDecimal(txtMedicamentos1.Text)),
        .CobreAluminioAcero = If(String.IsNullOrWhiteSpace(txtCobreAluminioAcero1.Text), Nothing, Convert.ToDecimal(txtCobreAluminioAcero1.Text)),
        .MedicamentosControlados = If(String.IsNullOrWhiteSpace(txtMedicamentosControlados1.Text), Nothing, Convert.ToDecimal(txtMedicamentosControlados1.Text)),
        .EqContratistas = If(String.IsNullOrWhiteSpace(txtEQ1.Text), Nothing, Convert.ToDecimal(txtEQ1.Text))
    })

        polizaMercanciaList.Add(New PolizaMercancia With {
        .AdministracionBienId = 2,
        .NombreInternoPoliza = txtNombreInternoPoliza2.Text,
        .TerrestreAereo = If(String.IsNullOrWhiteSpace(txtTerrestreAereo2.Text), Nothing, Convert.ToDecimal(txtTerrestreAereo2.Text)),
        .Maritimo = If(String.IsNullOrWhiteSpace(txtMaritimo2.Text), Nothing, Convert.ToDecimal(txtMaritimo2.Text)),
        .PaqueteriaMensajeria = If(String.IsNullOrWhiteSpace(txtPaqueteria2.Text), Nothing, Convert.ToDecimal(txtPaqueteria2.Text)),
        .Deducibles = txtDeducibles2.Text,
        .Compras = txtCompras2.Text,
        .Ventas = txtVentas2.Text,
        .Maquila = txtMaquila2.Text,
        .BienesUsados = txtBienesUsados2.Text,
        .EmbarqueFiliales = txtEmbarquesEntreFiliales2.Text,
        .IndemnizacionOtros = txtOtrosMercancia2.Text,
        .CuotaGeneralPoliza = If(String.IsNullOrWhiteSpace(txtCuotaGeneralPoliza2.Text), Nothing, Convert.ToDecimal(txtCuotaGeneralPoliza2.Text)),
        .Medicamentos = If(String.IsNullOrWhiteSpace(txtMedicamentos2.Text), Nothing, Convert.ToDecimal(txtMedicamentos2.Text)),
        .CobreAluminioAcero = If(String.IsNullOrWhiteSpace(txtCobreAluminioAcero2.Text), Nothing, Convert.ToDecimal(txtCobreAluminioAcero2.Text)),
        .MedicamentosControlados = If(String.IsNullOrWhiteSpace(txtMedicamentosControlados2.Text), Nothing, Convert.ToDecimal(txtMedicamentosControlados2.Text)),
        .EqContratistas = If(String.IsNullOrWhiteSpace(txtEQ2.Text), Nothing, Convert.ToDecimal(txtEQ2.Text))
    })

        polizaMercanciaList.Add(New PolizaMercancia With {
        .AdministracionBienId = 3,
        .NombreInternoPoliza = txtNombreInternoPoliza3.Text,
        .TerrestreAereo = If(String.IsNullOrWhiteSpace(txtTerrestreAereo3.Text), Nothing, Convert.ToDecimal(txtTerrestreAereo3.Text)),
        .Maritimo = If(String.IsNullOrWhiteSpace(txtMaritimo3.Text), Nothing, Convert.ToDecimal(txtMaritimo3.Text)),
        .PaqueteriaMensajeria = If(String.IsNullOrWhiteSpace(txtPaqueteria3.Text), Nothing, Convert.ToDecimal(txtPaqueteria3.Text)),
        .Deducibles = txtDeducibles3.Text,
        .Compras = txtCompras3.Text,
        .Ventas = txtVentas3.Text,
        .Maquila = txtMaquila3.Text,
        .BienesUsados = txtBienesUsados3.Text,
        .EmbarqueFiliales = txtEmbarquesEntreFiliales3.Text,
        .IndemnizacionOtros = txtOtrosMercancia3.Text,
        .CuotaGeneralPoliza = If(String.IsNullOrWhiteSpace(txtCuotaGeneralPoliza3.Text), Nothing, Convert.ToDecimal(txtCuotaGeneralPoliza3.Text)),
        .Medicamentos = If(String.IsNullOrWhiteSpace(txtMedicamentos3.Text), Nothing, Convert.ToDecimal(txtMedicamentos3.Text)),
        .CobreAluminioAcero = If(String.IsNullOrWhiteSpace(txtCobreAluminioAcero3.Text), Nothing, Convert.ToDecimal(txtCobreAluminioAcero3.Text)),
        .MedicamentosControlados = If(String.IsNullOrWhiteSpace(txtMedicamentosControlados3.Text), Nothing, Convert.ToDecimal(txtMedicamentosControlados3.Text)),
        .EqContratistas = If(String.IsNullOrWhiteSpace(txtEQ3.Text), Nothing, Convert.ToDecimal(txtEQ3.Text))
    })

        poliza.PolizaMercancia = polizaMercanciaList

        Dim polizaContenedor As New PolizaContenedor With {
            .TrayectosAsegurados = txtTrayectosAsegurados.Text,
            .MedioTransporte = txtMedioTransporte.Text,
            .PorContenedor = If(String.IsNullOrWhiteSpace(txtPorContenedor.Text), Nothing, Convert.ToDecimal(txtPorContenedor.Text)),
            .Ferrocarril = If(String.IsNullOrWhiteSpace(txtFerrocarril.Text), Nothing, Convert.ToDecimal(txtFerrocarril.Text)),
            .Terrestre = If(String.IsNullOrWhiteSpace(txtTerrestreMontosPoliza.Text), Nothing, Convert.ToDecimal(txtTerrestreMontosPoliza.Text)),
            .CuotaAplicable = If(String.IsNullOrWhiteSpace(txtCuotaAplicable.Text), Nothing, Convert.ToDecimal(txtCuotaAplicable.Text)),
            .DanioMaterial = If(String.IsNullOrWhiteSpace(txtDañoMaterial.Text), Nothing, Convert.ToDecimal(txtDañoMaterial.Text)),
            .Robo = If(String.IsNullOrWhiteSpace(txtRobo.Text), Nothing, Convert.ToDecimal(txtRobo.Text)),
            .PerdidaTotal = If(String.IsNullOrWhiteSpace(txtPerdidaTotal.Text), Nothing, Convert.ToDecimal(txtPerdidaTotal.Text)),
            .PerdidaParcial = If(String.IsNullOrWhiteSpace(txtPerdidaParcial.Text), Nothing, Convert.ToDecimal(txtPerdidaParcial.Text))
        }

        Dim respuesta As String
        Dim json As String

        json = JsonConvert.SerializeObject(poliza, Formatting.Indented)
        System.Diagnostics.Debug.WriteLine("JSON enviado a API:" & json)

        respuesta = api.PostPoliza(json)

        cargarPolizas()
        pnlFormularioPolizas.Visible = False
        pnlEncabezado.Visible = True
        PnlTabla.Visible = True
    End Sub
End Class