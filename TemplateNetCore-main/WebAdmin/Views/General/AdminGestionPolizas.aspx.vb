Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports WebAdmin.MercanciaSegura.DOM.Modelos

Public Class AdminGestionPolizas
    Inherits System.Web.UI.Page

    Private ReadOnly Property BienesSession As List(Of Bien)
        Get
            If Session("BienesTemporal") Is Nothing Then
                Session("BienesTemporal") = New List(Of Bien)()
            End If
            Return CType(Session("BienesTemporal"), List(Of Bien))
        End Get
    End Property

    Private ReadOnly Property CoberturasSession As List(Of Cobertura)
        Get
            If Session("CoberturasTemporal") Is Nothing Then
                Session("CoberturasTemporal") = New List(Of Cobertura)()
            End If
            Return CType(Session("CoberturasTemporal"), List(Of Cobertura))
        End Get
    End Property

    Private Property RiesgosSession As List(Of RiesgoCubierto)
        Get
            If Session("RiesgosTemporal") Is Nothing Then
                Session("RiesgosTemporal") = New List(Of RiesgoCubierto)()
            End If
            Return CType(Session("RiesgosTemporal"), List(Of RiesgoCubierto))
        End Get
        Set(value As List(Of RiesgoCubierto))
            Session("RiesgosTemporal") = value
        End Set
    End Property

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
            DropdownHelpers.CargarTipoEstatusPoliza(ddlEstatusPoliza)
            CargarBienesGrid()
        End If
    End Sub

    Protected Sub btnAgregarPoliza_Click(sender As Object, e As EventArgs)
        pnlFormularioPolizas.Visible = True
        pnlEncabezado.Visible = False
        PnlTabla.Visible = False
        lblMensaje.Text = "Nuevo Registro"

        ddlEstatusPoliza.SelectedValue = "1"
        ddlEstatusPoliza.Enabled = False
        ddlProducto.Enabled = True

        pnlMercancia.Visible = True
        pnlContenedor.Visible = False

        LimpiarFormulario()

        hfPolizaId.Value = ""
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

    Private Sub MostrarProducto(productoId As Integer)

        If productoId = 1 Then
            pnlNombreInternoPoliza.Visible = False
            pnlMercancia.Visible = True
            pnlContenedor.Visible = False
            pnlMediosTransporte.Visible = False

        ElseIf productoId = 2 Then
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

    Protected Sub gvPolizas_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        gvPolizas.PageIndex = e.NewPageIndex
        cargarPolizas()
    End Sub
    Protected Sub gvPolizas_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        If e.CommandName = "Editar" Then
            Dim polizaId As Integer = Convert.ToInt32(e.CommandArgument)
            pnlFormularioPolizas.Visible = True
            PnlTabla.Visible = False
            pnlEncabezado.Visible = False
            lblMensaje.Text = "Editar Póliza"

            EditarPoliza(polizaId)
        End If

        If e.CommandName = "Eliminar" Then
            Dim api As New ConsumoApi()
            Dim polizaId As Integer = Convert.ToInt32(e.CommandArgument)

            Dim eliminado As String = api.DeletePoliza(polizaId)

            cargarPolizas()

            If Not String.IsNullOrEmpty(eliminado) Then
                Dim respuestaObj As JObject = JObject.Parse(eliminado)
                Dim mensaje As String

                If respuestaObj("message") IsNot Nothing Then
                    mensaje = respuestaObj("message").ToString()
                Else
                    mensaje = "Poliza eliminada correctamente"
                End If

                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "toast",
        "showToast('" & mensaje & "', 'success');", True)
            Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "toast",
                "showToast('Error al eliminar la poliza', 'danger');", True)
            End If
        End If
    End Sub

    Private Function EsMercanciaSeleccionada() As Boolean
        Return Not String.IsNullOrWhiteSpace(txtNombreInternoPoliza1.Text)
    End Function

    Private Function EsContenedorSeleccionado() As Boolean
        Return Not String.IsNullOrWhiteSpace(txtNombreInternoPolizaContenedor.Text)
    End Function

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
            .VigenciaDel = If(String.IsNullOrEmpty(txtVigenciaDel.Text), CType(Nothing, DateTime?), Convert.ToDateTime(txtVigenciaDel.Text)),
            .VigenciaHasta = If(String.IsNullOrEmpty(txtVigenciaHasta.Text), CType(Nothing, DateTime?), Convert.ToDateTime(txtVigenciaHasta.Text)),
            .EstatusPolizaId = Convert.ToInt32(ddlEstatusPoliza.SelectedValue),
            .FormaPagoId = ddlFormaPago.SelectedValue,
            .MonedaId = ddlMoneda.SelectedValue,
            .ClaveAgente = txtClaveAgente.Text,
            .FolioPoliza = txtFolioPoliza.Text
            }

        If EsMercanciaSeleccionada() Then

            Dim polizaMercanciaList As New List(Of PolizaMercancia)
            If Not String.IsNullOrWhiteSpace(txtNombreInternoPoliza1.Text) Then
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
            .IndemnizacionOtros = txtOtrosBasesIndemnizacion1.Text,
            .CuotaGeneralPoliza = If(String.IsNullOrWhiteSpace(txtCuotaGeneralPoliza1.Text), Nothing, Convert.ToDecimal(txtCuotaGeneralPoliza1.Text)),
            .Medicamentos = If(String.IsNullOrWhiteSpace(txtMedicamentos1.Text), Nothing, Convert.ToDecimal(txtMedicamentos1.Text)),
            .CobreAluminioAcero = If(String.IsNullOrWhiteSpace(txtCobreAluminioAcero1.Text), Nothing, Convert.ToDecimal(txtCobreAluminioAcero1.Text)),
            .MedicamentosControlados = If(String.IsNullOrWhiteSpace(txtMedicamentosControlados1.Text), Nothing, Convert.ToDecimal(txtMedicamentosControlados1.Text)),
            .EqContratistas = If(String.IsNullOrWhiteSpace(txtEQ1.Text), Nothing, Convert.ToDecimal(txtEQ1.Text)),
            .PrimaNeta = If(String.IsNullOrWhiteSpace(txtPrimaNetaMercancia1.Text), Nothing, Convert.ToDecimal(txtPrimaNetaMercancia1.Text)),
            .DerechoPoliza = If(String.IsNullOrWhiteSpace(txtDerechoPolizaMercancia1.Text), Nothing, Convert.ToDecimal(txtDerechoPolizaMercancia1.Text)),
            .OtroPrima = If(String.IsNullOrWhiteSpace(txtOtrosMercancia1.Text), Nothing, Convert.ToDecimal(txtOtrosMercancia1.Text)),
            .IVA = If(String.IsNullOrWhiteSpace(txtIVAMercancia1.Text), Nothing, Convert.ToDecimal(txtIVAMercancia1.Text)),
            .PrimaTotal = If(String.IsNullOrWhiteSpace(txtPrimaTotalMercancia1.Text), Nothing, Convert.ToDecimal(txtPrimaTotalMercancia1.Text)),
            .RiesgoCubierto = RiesgosSession.
    Where(Function(r) r.AdministracionBienId = 1).
    ToList()
        })
            End If
            If Not String.IsNullOrWhiteSpace(txtNombreInternoPoliza2.Text) Then
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
            .IndemnizacionOtros = txtOtrosBasesIndemnizacion2.Text,
            .CuotaGeneralPoliza = If(String.IsNullOrWhiteSpace(txtCuotaGeneralPoliza2.Text), Nothing, Convert.ToDecimal(txtCuotaGeneralPoliza2.Text)),
            .Medicamentos = If(String.IsNullOrWhiteSpace(txtMedicamentos2.Text), Nothing, Convert.ToDecimal(txtMedicamentos2.Text)),
            .CobreAluminioAcero = If(String.IsNullOrWhiteSpace(txtCobreAluminioAcero2.Text), Nothing, Convert.ToDecimal(txtCobreAluminioAcero2.Text)),
            .MedicamentosControlados = If(String.IsNullOrWhiteSpace(txtMedicamentosControlados2.Text), Nothing, Convert.ToDecimal(txtMedicamentosControlados2.Text)),
            .EqContratistas = If(String.IsNullOrWhiteSpace(txtEQ2.Text), Nothing, Convert.ToDecimal(txtEQ2.Text)),
            .PrimaNeta = If(String.IsNullOrWhiteSpace(txtPrimaNetaMercancia2.Text), Nothing, Convert.ToDecimal(txtPrimaNetaMercancia2.Text)),
            .DerechoPoliza = If(String.IsNullOrWhiteSpace(txtDerechoPolizaMercancia2.Text), Nothing, Convert.ToDecimal(txtDerechoPolizaMercancia2.Text)),
            .OtroPrima = If(String.IsNullOrWhiteSpace(txtOtrosMercancia2.Text), Nothing, Convert.ToDecimal(txtOtrosMercancia2.Text)),
            .IVA = If(String.IsNullOrWhiteSpace(txtIVAMercancia2.Text), Nothing, Convert.ToDecimal(txtIVAMercancia2.Text)),
            .PrimaTotal = If(String.IsNullOrWhiteSpace(txtPrimaTotalMercancia2.Text), Nothing, Convert.ToDecimal(txtPrimaTotalMercancia2.Text)),
            .RiesgoCubierto = RiesgosSession.
Where(Function(r) r.AdministracionBienId = 2).
ToList()
                                })
            End If

            If Not String.IsNullOrWhiteSpace(txtNombreInternoPoliza3.Text) Then
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
            .IndemnizacionOtros = txtOtrosBasesIndemnizacion3.Text,
            .CuotaGeneralPoliza = If(String.IsNullOrWhiteSpace(txtCuotaGeneralPoliza3.Text), Nothing, Convert.ToDecimal(txtCuotaGeneralPoliza3.Text)),
            .Medicamentos = If(String.IsNullOrWhiteSpace(txtMedicamentos3.Text), Nothing, Convert.ToDecimal(txtMedicamentos3.Text)),
            .CobreAluminioAcero = If(String.IsNullOrWhiteSpace(txtCobreAluminioAcero3.Text), Nothing, Convert.ToDecimal(txtCobreAluminioAcero3.Text)),
            .MedicamentosControlados = If(String.IsNullOrWhiteSpace(txtMedicamentosControlados3.Text), Nothing, Convert.ToDecimal(txtMedicamentosControlados3.Text)),
            .EqContratistas = If(String.IsNullOrWhiteSpace(txtEQ3.Text), Nothing, Convert.ToDecimal(txtEQ3.Text)),
            .PrimaNeta = If(String.IsNullOrWhiteSpace(txtPrimaNetaMercancia3.Text), Nothing, Convert.ToDecimal(txtPrimaNetaMercancia3.Text)),
            .DerechoPoliza = If(String.IsNullOrWhiteSpace(txtDerechoPolizaMercancia3.Text), Nothing, Convert.ToDecimal(txtDerechoPolizaMercancia3.Text)),
            .OtroPrima = If(String.IsNullOrWhiteSpace(txtOtrosMercancia3.Text), Nothing, Convert.ToDecimal(txtOtrosMercancia3.Text)),
            .IVA = If(String.IsNullOrWhiteSpace(txtIVAMercancia3.Text), Nothing, Convert.ToDecimal(txtIVAMercancia3.Text)),
            .PrimaTotal = If(String.IsNullOrWhiteSpace(txtPrimaTotalMercancia3.Text), Nothing, Convert.ToDecimal(txtPrimaTotalMercancia3.Text)),
.RiesgoCubierto = RiesgosSession.
Where(Function(r) r.AdministracionBienId = 3).
ToList()})
            End If

            poliza.PolizaMercancia = polizaMercanciaList

            End If

            Dim bienesAsegurados = BienesSession.
Where(Function(b) b.TipoBienId = 1 OrElse b.TipoBienId = 0).Select(Function(b)
                                                                   Return New Bien With {
    .AdministracionBienId = b.AdministracionBienId,
    .Nombre = b.Nombre,
    .TipoBienId = 1
}
                                                               End Function).ToList()

        Dim bienesExcluidos = BienesSession.
    Where(Function(b) b.TipoBienId = 2).Select(Function(b)
                                                   Return New Bien With {
            .AdministracionBienId = b.AdministracionBienId,
            .Nombre = b.Nombre,
            .TipoBienId = 2
        }
                                               End Function).ToList()

        Dim bienesSujetosConsulta = BienesSession.
    Where(Function(b) b.TipoBienId = 3).Select(Function(b)
                                                   Return New Bien With {
            .AdministracionBienId = b.AdministracionBienId,
            .Nombre = b.Nombre,
            .TipoBienId = 3
        }
                                               End Function).ToList()

        Dim bienesContenedor = BienesSession.
    Where(Function(b) b.AdministracionBienId = 4 AndAlso b.TipoBienId = 4).Select(Function(b)
                                                                                      Return New Bien With {
            .AdministracionBienId = b.AdministracionBienId,
            .Nombre = b.Nombre,
            .TipoBienId = 4
        }
                                                                                  End Function).ToList()

        poliza.Bien = bienesAsegurados.Concat(bienesExcluidos).Concat(bienesSujetosConsulta).Concat(bienesContenedor).ToList()

        If EsContenedorSeleccionado() Then
            Dim polizaContenedor As New PolizaContenedor With {
            .NombreInternoPoliza = txtNombreInternoPolizaContenedor.Text,
            .TrayectosAsegurados = txtTrayectosAsegurados.Text,
            .MedioTransporte = txtMedioTransporte.Text,
            .PorContenedor = If(String.IsNullOrWhiteSpace(txtPorContenedor.Text), Nothing, Convert.ToDecimal(txtPorContenedor.Text)),
            .Ferrocarril = If(String.IsNullOrWhiteSpace(txtFerrocarril.Text), Nothing, Convert.ToDecimal(txtFerrocarril.Text)),
            .Terrestre = If(String.IsNullOrWhiteSpace(txtTerrestreMontosPoliza.Text), Nothing, Convert.ToDecimal(txtTerrestreMontosPoliza.Text)),
            .CuotaAplicable = If(String.IsNullOrWhiteSpace(txtCuotaAplicable.Text), Nothing, Convert.ToDecimal(txtCuotaAplicable.Text)),
            .ManiobrasRescate = If(String.IsNullOrWhiteSpace(txtManiobrasRescateContenedor.Text), Nothing, Convert.ToDecimal(txtManiobrasRescateContenedor.Text)),
            .DanioMaterial = If(String.IsNullOrWhiteSpace(txtDañoMaterial.Text), Nothing, Convert.ToDecimal(txtDañoMaterial.Text)),
            .Robo = If(String.IsNullOrWhiteSpace(txtRobo.Text), Nothing, Convert.ToDecimal(txtRobo.Text)),
            .PerdidaTotal = If(String.IsNullOrWhiteSpace(txtPerdidaTotal.Text), Nothing, Convert.ToDecimal(txtPerdidaTotal.Text)),
            .PerdidaParcial = If(String.IsNullOrWhiteSpace(txtPerdidaParcial.Text), Nothing, Convert.ToDecimal(txtPerdidaParcial.Text)),
            .PrimaNeta = If(String.IsNullOrWhiteSpace(txtPrimaNetaC.Text), Nothing, Convert.ToDecimal(txtPrimaNetaC.Text)),
            .OtroPrima = If(String.IsNullOrWhiteSpace(txtOtrosMontosPolizaC.Text), Nothing, Convert.ToDecimal(txtOtrosMontosPolizaC.Text)),
            .DerechoPoliza = If(String.IsNullOrWhiteSpace(txtDerechoPolizaC.Text), Nothing, Convert.ToDecimal(txtDerechoPolizaC.Text)),
            .IVA = If(String.IsNullOrWhiteSpace(txtIVAC.Text), Nothing, Convert.ToDecimal(txtIVAC.Text)),
            .PrimaTotal = If(String.IsNullOrWhiteSpace(txtTotalC.Text), Nothing, Convert.ToDecimal(txtTotalC.Text)),
            .Cobertura = CoberturasSession.ToList()
        }

            poliza.PolizaContenedor = polizaContenedor

        End If

        Dim respuesta As String
        Dim mensajeToast As String = ""
        Dim json As String

        json = JsonConvert.SerializeObject(poliza, Formatting.Indented)
        System.Diagnostics.Debug.WriteLine("JSON enviado a API:" & json)

        If Not String.IsNullOrEmpty(hfPolizaId.Value) Then
            Dim polizaId As Integer = Convert.ToInt32(hfPolizaId.Value)
            respuesta = api.PutEditarPoliza(polizaId, json)
            If respuesta.Contains("error") Then
                mensajeToast = "No se edito la poliza"
            Else
                mensajeToast = "Poliza editada correctamente"
            End If
        Else
            respuesta = api.PostPoliza(json)
            If respuesta.Contains("error") Then
                mensajeToast = "Error al agregar poliza"
            Else
                mensajeToast = "Poliza agregada correctamente"
            End If
        End If

        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "toast",
        "showToast('" & mensajeToast & "', 'success');", True)

        cargarPolizas()
        pnlFormularioPolizas.Visible = False
        pnlEncabezado.Visible = True
        PnlTabla.Visible = True
    End Sub

    Protected Sub EditarPoliza(polizaId As Integer)
        Dim api As New ConsumoApi()
        Dim objpoliza As String = api.GetPolizaId(polizaId)

        Dim poliza As Poliza = JsonConvert.DeserializeObject(Of Poliza)(objpoliza)

        hfPolizaId.Value = poliza.PolizaId.ToString()

        ddlProducto.SelectedValue = poliza.ProductoId
        MostrarProducto(poliza.ProductoId)
        txtTipoPoliza.Text = poliza.TipoPoliza
        txtNumeroPoliza.Text = poliza.NumeroPoliza
        ddlContratante.SelectedValue = poliza.ContratanteId
        ddlAseguradora.SelectedValue = poliza.AseguradoraId
        ddlSubRamo.SelectedValue = poliza.SubRamoId
        If poliza.VigenciaDel.HasValue Then
            txtVigenciaDel.Text = poliza.VigenciaDel.Value.ToString("yyyy-MM-dd")
        End If

        If poliza.VigenciaHasta.HasValue Then
            txtVigenciaHasta.Text = poliza.VigenciaHasta.Value.ToString("yyyy-MM-dd")
        End If
        ddlFormaPago.SelectedValue = poliza.FormaPagoId
        ddlMoneda.SelectedValue = poliza.MonedaId
        txtClaveAgente.Text = poliza.ClaveAgente
        txtFolioPoliza.Text = poliza.FolioPoliza

        ddlProducto.Enabled = False
        ddlEstatusPoliza.Enabled = True

        If Not String.IsNullOrEmpty(poliza.EstatusPolizaId) AndAlso ddlEstatusPoliza.Items.FindByValue(poliza.EstatusPolizaId) IsNot Nothing Then
            ddlEstatusPoliza.SelectedValue = poliza.EstatusPolizaId
        End If

        BienesSession.Clear()
        If poliza.Bien IsNot Nothing Then
            For Each b In poliza.Bien
                BienesSession.Add(b)
            Next
        End If

        RiesgosSession = poliza.PolizaMercancia.SelectMany(Function(pm) pm.RiesgoCubierto).ToList()

        CoberturasSession.Clear()
        If poliza.PolizaContenedor IsNot Nothing AndAlso poliza.PolizaContenedor.Cobertura IsNot Nothing Then
            For Each c In poliza.PolizaContenedor.Cobertura
                CoberturasSession.Add(c)
            Next
        End If

        GvBienes.DataSource = BienesSession.Where(Function(b) b.TipoBienId = 1 AndAlso b.AdministracionBienId = 1).ToList()
        GvBienes.DataBind()

        GvBienes2.DataSource = BienesSession.Where(Function(b) b.TipoBienId = 1 AndAlso b.AdministracionBienId = 2).ToList()
        GvBienes2.DataBind()

        GvBienes3.DataSource = BienesSession.Where(Function(b) b.TipoBienId = 1 AndAlso b.AdministracionBienId = 3).ToList()
        GvBienes3.DataBind()

        GvBienesExcluidos1.DataSource = BienesSession.Where(Function(b) b.TipoBienId = 2 AndAlso b.AdministracionBienId = 1).ToList()
        GvBienesExcluidos1.DataBind()

        GvBienesExcluidos2.DataSource = BienesSession.Where(Function(b) b.TipoBienId = 2 AndAlso b.AdministracionBienId = 2).ToList()
        GvBienesExcluidos2.DataBind()

        GvBienesExcluidos3.DataSource = BienesSession.Where(Function(b) b.TipoBienId = 2 AndAlso b.AdministracionBienId = 3).ToList()
        GvBienesExcluidos3.DataBind()

        GvBienesSujetosConsulta.DataSource = BienesSession.Where(Function(b) b.TipoBienId = 3 AndAlso b.AdministracionBienId = 1).ToList()
        GvBienesSujetosConsulta.DataBind()

        GvBienesSujetosConsulta2.DataSource = BienesSession.Where(Function(b) b.TipoBienId = 3 AndAlso b.AdministracionBienId = 2).ToList()
        GvBienesSujetosConsulta2.DataBind()

        GvBienesSujetosConsulta3.DataSource = BienesSession.Where(Function(b) b.TipoBienId = 3 AndAlso b.AdministracionBienId = 3).ToList()
        GvBienesSujetosConsulta3.DataBind()

        GvViajeCompleto.DataSource = RiesgosSession.Where(Function(b) b.TipoRiesgoId = 1 AndAlso b.AdministracionBienId = 1).ToList()
        GvViajeCompleto.DataBind()

        GvViajeCompleto2.DataSource = RiesgosSession.Where(Function(b) b.TipoRiesgoId = 1 AndAlso b.AdministracionBienId = 2).ToList()
        GvViajeCompleto2.DataBind()

        GvViajeCompleto3.DataSource = RiesgosSession.Where(Function(b) b.TipoRiesgoId = 1 AndAlso b.AdministracionBienId = 3).ToList()
        GvViajeCompleto3.DataBind()

        GvContinuacionViaje.DataSource = RiesgosSession.Where(Function(b) b.TipoRiesgoId = 2 AndAlso b.AdministracionBienId = 1).ToList()
        GvContinuacionViaje.DataBind()

        GvContinuacionViaje2.DataSource = RiesgosSession.Where(Function(b) b.TipoRiesgoId = 2 AndAlso b.AdministracionBienId = 2).ToList()
        GvContinuacionViaje2.DataBind()

        GvContinuacionViaje3.DataSource = RiesgosSession.Where(Function(b) b.TipoRiesgoId = 2 AndAlso b.AdministracionBienId = 3).ToList()
        GvContinuacionViaje3.DataBind()

        GvCoberturasAdicionales.DataSource = RiesgosSession.Where(Function(b) b.TipoRiesgoId = 3 AndAlso b.AdministracionBienId = 1).ToList()
        GvCoberturasAdicionales.DataBind()

        GvCoberturasAdicionales2.DataSource = RiesgosSession.Where(Function(b) b.TipoRiesgoId = 3 AndAlso b.AdministracionBienId = 2).ToList()
        GvCoberturasAdicionales2.DataBind()

        GvCoberturasAdicionales3.DataSource = RiesgosSession.Where(Function(b) b.TipoRiesgoId = 3 AndAlso b.AdministracionBienId = 3).ToList()
        GvCoberturasAdicionales3.DataBind()

        GvBienContenedor.DataSource = BienesSession.Where(Function(b) b.TipoBienId = 4 AndAlso b.AdministracionBienId = 4).ToList()
        GvBienContenedor.DataBind()

        GvCoberturasContenedor.DataSource = CoberturasSession
        GvCoberturasContenedor.DataBind()


        For Each pm In poliza.PolizaMercancia

            Select Case pm.AdministracionBienId

                Case 1
                    txtNombreInternoPoliza1.Text = pm.NombreInternoPoliza
                    txtTerrestreAereo1.Text = pm.TerrestreAereo
                    txtMaritimo1.Text = pm.Maritimo
                    txtPaqueteria1.Text = pm.PaqueteriaMensajeria
                    txtDeducibles1.Text = pm.Deducibles
                    txtCompras1.Text = pm.Compras
                    txtVentas1.Text = pm.Ventas
                    txtMaquila1.Text = pm.Maquila
                    txtBienesUsados1.Text = pm.BienesUsados
                    txtEmbarquesEntreFiliales1.Text = pm.EmbarqueFiliales
                    txtOtrosBasesIndemnizacion1.Text = pm.IndemnizacionOtros
                    txtCuotaGeneralPoliza1.Text = pm.CuotaGeneralPoliza
                    txtMedicamentos1.Text = pm.Medicamentos
                    txtCobreAluminioAcero1.Text = pm.CobreAluminioAcero
                    txtMedicamentosControlados1.Text = pm.MedicamentosControlados
                    txtEQ1.Text = pm.EqContratistas
                    txtPrimaNetaMercancia1.Text = pm.PrimaNeta
                    txtDerechoPolizaMercancia1.Text = pm.DerechoPoliza
                    txtOtrosMercancia1.Text = pm.OtroPrima
                    txtIVAMercancia1.Text = pm.IVA
                    txtPrimaTotalMercancia1.Text = pm.PrimaTotal

                Case 2
                    txtNombreInternoPoliza2.Text = pm.NombreInternoPoliza
                    txtTerrestreAereo2.Text = pm.TerrestreAereo
                    txtMaritimo2.Text = pm.Maritimo
                    txtPaqueteria2.Text = pm.PaqueteriaMensajeria
                    txtDeducibles2.Text = pm.Deducibles
                    txtCompras2.Text = pm.Compras
                    txtVentas2.Text = pm.Ventas
                    txtMaquila2.Text = pm.Maquila
                    txtBienesUsados2.Text = pm.BienesUsados
                    txtEmbarquesEntreFiliales2.Text = pm.EmbarqueFiliales
                    txtOtrosBasesIndemnizacion2.Text = pm.IndemnizacionOtros
                    txtCuotaGeneralPoliza2.Text = pm.CuotaGeneralPoliza
                    txtMedicamentos2.Text = pm.Medicamentos
                    txtCobreAluminioAcero2.Text = pm.CobreAluminioAcero
                    txtMedicamentosControlados2.Text = pm.MedicamentosControlados
                    txtEQ2.Text = pm.EqContratistas
                    txtPrimaNetaMercancia2.Text = pm.PrimaNeta
                    txtDerechoPolizaMercancia2.Text = pm.DerechoPoliza
                    txtOtrosMercancia2.Text = pm.OtroPrima
                    txtIVAMercancia2.Text = pm.IVA
                    txtPrimaTotalMercancia2.Text = pm.PrimaTotal

                Case 3
                    txtNombreInternoPoliza3.Text = pm.NombreInternoPoliza
                    txtTerrestreAereo3.Text = pm.TerrestreAereo
                    txtMaritimo3.Text = pm.Maritimo
                    txtPaqueteria3.Text = pm.PaqueteriaMensajeria
                    txtDeducibles3.Text = pm.Deducibles
                    txtCompras3.Text = pm.Compras
                    txtVentas3.Text = pm.Ventas
                    txtMaquila3.Text = pm.Maquila
                    txtBienesUsados3.Text = pm.BienesUsados
                    txtEmbarquesEntreFiliales3.Text = pm.EmbarqueFiliales
                    txtOtrosBasesIndemnizacion3.Text = pm.IndemnizacionOtros
                    txtCuotaGeneralPoliza3.Text = pm.CuotaGeneralPoliza
                    txtMedicamentos3.Text = pm.Medicamentos
                    txtCobreAluminioAcero3.Text = pm.CobreAluminioAcero
                    txtMedicamentosControlados3.Text = pm.MedicamentosControlados
                    txtEQ3.Text = pm.EqContratistas
                    txtPrimaNetaMercancia3.Text = pm.PrimaNeta
                    txtDerechoPolizaMercancia3.Text = pm.DerechoPoliza
                    txtOtrosMercancia3.Text = pm.OtroPrima
                    txtIVAMercancia3.Text = pm.IVA
                    txtPrimaTotalMercancia3.Text = pm.PrimaTotal


            End Select

        Next

        If poliza.PolizaContenedor IsNot Nothing Then

            txtNombreInternoPolizaContenedor.Text = poliza.PolizaContenedor.NombreInternoPoliza
            txtTrayectosAsegurados.Text = poliza.PolizaContenedor.TrayectosAsegurados
            txtMedioTransporte.Text = poliza.PolizaContenedor.MedioTransporte
            txtPorContenedor.Text = poliza.PolizaContenedor.PorContenedor
            txtFerrocarril.Text = poliza.PolizaContenedor.Ferrocarril
            txtTerrestreMontosPoliza.Text = poliza.PolizaContenedor.Terrestre
            txtCuotaAplicable.Text = poliza.PolizaContenedor.CuotaAplicable
            txtManiobrasRescateContenedor.Text = poliza.PolizaContenedor.ManiobrasRescate
            txtPrimaNetaC.Text = poliza.PolizaContenedor.PrimaNeta
            txtOtrosMontosPolizaC.Text = poliza.PolizaContenedor.OtroPrima
            txtDerechoPolizaC.Text = poliza.PolizaContenedor.DerechoPoliza
            txtIVAC.Text = poliza.PolizaContenedor.IVA
            txtTotalC.Text = poliza.PolizaContenedor.PrimaTotal
            txtDañoMaterial.Text = poliza.PolizaContenedor.DanioMaterial
            txtRobo.Text = poliza.PolizaContenedor.Robo
            txtPerdidaParcial.Text = poliza.PolizaContenedor.PerdidaParcial
            txtPerdidaTotal.Text = poliza.PolizaContenedor.PerdidaTotal

        End If

    End Sub

    Private Sub CargarBienesGrid()

        GvBienes.DataSource = BienesSession.
        Where(Function(b) b.AdministracionBienId = 1 AndAlso b.TipoBienId = 1).ToList()
        GvBienes.DataBind()

        GvBienes2.DataSource = BienesSession.
        Where(Function(b) b.AdministracionBienId = 2 AndAlso b.TipoBienId = 1).ToList()
        GvBienes2.DataBind()

        GvBienes3.DataSource = BienesSession.
        Where(Function(b) b.AdministracionBienId = 3 AndAlso b.TipoBienId = 1).ToList()
        GvBienes3.DataBind()


        GvBienesExcluidos1.DataSource = BienesSession.
        Where(Function(b) b.AdministracionBienId = 1 AndAlso b.TipoBienId = 2).ToList()
        GvBienesExcluidos1.DataBind()

        GvBienesExcluidos2.DataSource = BienesSession.
        Where(Function(b) b.AdministracionBienId = 2 AndAlso b.TipoBienId = 2).ToList()
        GvBienesExcluidos2.DataBind()

        GvBienesExcluidos3.DataSource = BienesSession.
        Where(Function(b) b.AdministracionBienId = 3 AndAlso b.TipoBienId = 2).ToList()
        GvBienesExcluidos3.DataBind()


        GvBienesSujetosConsulta.DataSource = BienesSession.
        Where(Function(b) b.AdministracionBienId = 1 AndAlso b.TipoBienId = 3).ToList()
        GvBienesSujetosConsulta.DataBind()

        GvBienesSujetosConsulta2.DataSource = BienesSession.
        Where(Function(b) b.AdministracionBienId = 2 AndAlso b.TipoBienId = 3).ToList()
        GvBienesSujetosConsulta2.DataBind()

        GvBienesSujetosConsulta3.DataSource = BienesSession.
        Where(Function(b) b.AdministracionBienId = 3 AndAlso b.TipoBienId = 3).ToList()
        GvBienesSujetosConsulta3.DataBind()

        GvBienContenedor.DataSource = BienesSession.
        Where(Function(b) b.AdministracionBienId = 4 AndAlso b.TipoBienId = 4).ToList()
        GvBienContenedor.DataBind()

    End Sub


    Protected Sub GvBienes_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        GvBienes.PageIndex = e.NewPageIndex
        CargarBienesGrid()
    End Sub
    Protected Sub GvBienes2_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        GvBienes2.PageIndex = e.NewPageIndex
        CargarBienesGrid()
    End Sub
    Protected Sub GvBienes3_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        GvBienes3.PageIndex = e.NewPageIndex
        CargarBienesGrid()
    End Sub
    Protected Sub GvBienes_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        If e.CommandName = "Eliminar" Then
            EliminarBien(Convert.ToInt32(e.CommandArgument))
        End If
    End Sub

    Protected Sub GvBienes2_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        If e.CommandName = "Eliminar" Then
            EliminarBien(Convert.ToInt32(e.CommandArgument))
        End If
    End Sub

    Protected Sub GvBienes3_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        If e.CommandName = "Eliminar" Then
            EliminarBien(Convert.ToInt32(e.CommandArgument))
        End If
    End Sub
    Protected Sub btnAgregaBienAsegurado_Click(sender As Object, e As EventArgs)
        AgregarBien(txtBienesAsegurados, 1, 1)
    End Sub

    Protected Sub btnAgregaBienAsegurado2_Click(sender As Object, e As EventArgs)
        AgregarBien(txtBienesAsegurados2, 2, 1)
    End Sub

    Protected Sub btnAgregaBienAsegurado3_Click(sender As Object, e As EventArgs)
        AgregarBien(txtBienesAsegurados3, 3, 1)
    End Sub



    Private Sub AgregarBien(txt As TextBox, administracionId As Integer, tipoBienId As Integer)
        Dim nombreBien As String = txt.Text.Trim()
        If String.IsNullOrEmpty(nombreBien) Then Return

        Dim nuevoBien As New Bien With {
         .BienId = If(BienesSession.Count = 0, 1, BienesSession.Max(Function(b) b.BienId) + 1),
        .Nombre = nombreBien,
        .AdministracionBienId = administracionId,
        .TipoBienId = tipoBienId
    }

        BienesSession.Add(nuevoBien)
        Session("BienesTemporal") = BienesSession

        txt.Text = ""
        CargarBienesGrid()
    End Sub

    Private Sub EliminarBien(id As Integer)
        Dim bienEliminar = BienesSession.FirstOrDefault(Function(b) b.BienId = id)
        If bienEliminar IsNot Nothing Then
            BienesSession.Remove(bienEliminar)
            CargarBienesGrid()
        End If
    End Sub

    Protected Sub GvBienesExcluidos1_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        If e.CommandName = "Eliminar" Then
            EliminarBien(Convert.ToInt32(e.CommandArgument))
        End If
    End Sub

    Protected Sub GvBienesExcluidos1_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        GvBienesExcluidos1.PageIndex = e.NewPageIndex
        CargarBienesGrid()
    End Sub

    Protected Sub btnAgregacollapseBienesExcluidos_Click(sender As Object, e As EventArgs)
        AgregarBien(txtBienesExcluidos, 1, 2)
    End Sub

    Protected Sub GvBienesExcluidos2_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        If e.CommandName = "Eliminar" Then
            EliminarBien(Convert.ToInt32(e.CommandArgument))
        End If
    End Sub

    Protected Sub GvBienesExcluidos2_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        GvBienesExcluidos2.PageIndex = e.NewPageIndex
        CargarBienesGrid()
    End Sub

    Protected Sub btnAgregacollapseBienesExcluidos2_Click(sender As Object, e As EventArgs)
        AgregarBien(txtBienesExcluidos2, 2, 2)
    End Sub

    Protected Sub GvBienesExcluidos3_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        If e.CommandName = "Eliminar" Then
            EliminarBien(Convert.ToInt32(e.CommandArgument))
        End If
    End Sub

    Protected Sub GvBienesExcluidos3_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        GvBienesExcluidos3.PageIndex = e.NewPageIndex
        CargarBienesGrid()
    End Sub

    Protected Sub btnAgregacollapseBienesExcluidos3_Click(sender As Object, e As EventArgs)
        AgregarBien(txtBienesExcluidos3, 3, 2)
    End Sub

    Private Sub LimpiarFormulario()
        txtTipoPoliza.Text = ""
        txtNumeroPoliza.Text = ""
        txtClaveAgente.Text = ""
        txtFolioPoliza.Text = ""
        txtPrimaNetaMercancia1.Text = ""
        txtDerechoPolizaMercancia1.Text = ""
        txtOtrosBasesIndemnizacion1.Text = ""
        txtIVAC.Text = ""
        txtPrimaTotalMercancia1.Text = ""
        txtOtrosMercancia1.Text = ""

        txtNombreInternoPoliza1.Text = ""
        txtNombreInternoPoliza2.Text = ""
        txtNombreInternoPoliza3.Text = ""
        txtTerrestreAereo1.Text = ""
        txtTerrestreAereo2.Text = ""
        txtTerrestreAereo3.Text = ""
        txtMaritimo1.Text = ""
        txtMaritimo2.Text = ""
        txtMaritimo3.Text = ""
        txtPaqueteria1.Text = ""
        txtPaqueteria2.Text = ""
        txtPaqueteria3.Text = ""
        txtDeducibles1.Text = ""
        txtDeducibles2.Text = ""
        txtDeducibles3.Text = ""
        txtCompras1.Text = ""
        txtCompras2.Text = ""
        txtCompras3.Text = ""
        txtVentas1.Text = ""
        txtVentas2.Text = ""
        txtVentas3.Text = ""
        txtMaquila1.Text = ""
        txtMaquila2.Text = ""
        txtMaquila3.Text = ""
        txtBienesUsados1.Text = ""
        txtBienesUsados2.Text = ""
        txtBienesUsados3.Text = ""
        txtEmbarquesEntreFiliales1.Text = ""
        txtEmbarquesEntreFiliales2.Text = ""
        txtEmbarquesEntreFiliales3.Text = ""
        txtOtrosMercancia1.Text = ""
        txtOtrosMercancia2.Text = ""
        txtOtrosMercancia3.Text = ""
        txtCuotaGeneralPoliza1.Text = ""
        txtCuotaGeneralPoliza2.Text = ""
        txtCuotaGeneralPoliza3.Text = ""
        txtMedicamentos1.Text = ""
        txtMedicamentos2.Text = ""
        txtMedicamentos3.Text = ""
        txtCobreAluminioAcero1.Text = ""
        txtCobreAluminioAcero2.Text = ""
        txtCobreAluminioAcero3.Text = ""
        txtMedicamentosControlados1.Text = ""
        txtMedicamentosControlados2.Text = ""
        txtMedicamentosControlados3.Text = ""
        txtEQ1.Text = ""
        txtEQ2.Text = ""
        txtEQ3.Text = ""

        txtTrayectosAsegurados.Text = ""
        txtMedioTransporte.Text = ""
        txtPorContenedor.Text = ""
        txtFerrocarril.Text = ""
        txtTerrestreMontosPoliza.Text = ""
        txtCuotaAplicable.Text = ""
        txtDañoMaterial.Text = ""
        txtRobo.Text = ""
        txtPerdidaTotal.Text = ""
        txtPerdidaParcial.Text = ""
        txtCoberturaContenedor.Text = ""
        txtManiobrasRescateContenedor.Text = ""
        txtPrimaNetaC.Text = ""
        txtOtrosMontosPolizaC.Text = ""
        txtDerechoPolizaC.Text = ""
        txtTotalC.Text = ""

        ddlProducto.SelectedIndex = 0
        ddlContratante.SelectedIndex = 0
        ddlAseguradora.SelectedIndex = 0
        ddlSubRamo.SelectedIndex = 0
        ddlFormaPago.SelectedIndex = 0
        ddlMoneda.SelectedIndex = 0
        ddlEstatusPoliza.SelectedIndex = 0

        BienesSession.Clear()
        CoberturasSession.Clear()
        RiesgosSession.Clear()
        CargarRiesgos()
        CargarBienesGrid()
        CargarCoberturasGrid()
    End Sub

    Protected Sub GvBienesSujetosConsulta_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        If e.CommandName = "Eliminar" Then
            EliminarBien(Convert.ToInt32(e.CommandArgument))
        End If
    End Sub

    Protected Sub GvBienesSujetosConsulta_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        GvBienesSujetosConsulta.PageIndex = e.NewPageIndex
        CargarBienesGrid()
    End Sub

    Protected Sub btnAgregaBienesSujetosConsulta_Click(sender As Object, e As EventArgs)
        AgregarBien(txtbienesSujetosConsulta, 1, 3)
    End Sub

    Protected Sub GvBienesSujetosConsulta2_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        If e.CommandName = "Eliminar" Then
            EliminarBien(Convert.ToInt32(e.CommandArgument))
        End If
    End Sub

    Protected Sub GvBienesSujetosConsulta2_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        GvBienesSujetosConsulta2.PageIndex = e.NewPageIndex
        CargarBienesGrid()
    End Sub

    Protected Sub btnAgregaBienesSujetosConsulta2_Click(sender As Object, e As EventArgs)
        AgregarBien(txtbienesSujetosConsulta2, 2, 3)
    End Sub

    Protected Sub GvBienesSujetosConsulta3_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        If e.CommandName = "Eliminar" Then
            EliminarBien(Convert.ToInt32(e.CommandArgument))
        End If
    End Sub

    Protected Sub GvBienesSujetosConsulta3_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        GvBienesSujetosConsulta3.PageIndex = e.NewPageIndex
        CargarBienesGrid()
    End Sub

    Protected Sub btnAgregaBienesSujetosConsulta3_Click(sender As Object, e As EventArgs)
        AgregarBien(txtbienesSujetosConsulta3, 3, 3)
    End Sub

    Protected Sub GvBienContenedor_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        If e.CommandName = "Eliminar" Then
            EliminarBien(Convert.ToInt32(e.CommandArgument))
        End If
    End Sub

    Protected Sub GvBienContenedor_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        GvBienContenedor.PageIndex = e.NewPageIndex
        CargarBienesGrid()
    End Sub

    Protected Sub btnAgregarBienAseguradoContenedor_Click(sender As Object, e As EventArgs)
        AgregarBien(txtBienesAseguradosContenedor, 4, 4)
    End Sub

    Protected Sub GvCoberturasContenedor_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        If e.CommandName = "Eliminar" Then
            Dim id As Integer = Convert.ToInt32(e.CommandArgument)
            Dim coberturaEliminar = CoberturasSession.FirstOrDefault(Function(c) c.CoberturaId = id)
            If coberturaEliminar IsNot Nothing Then
                CoberturasSession.Remove(coberturaEliminar)
                CargarCoberturasGrid()
            End If
        End If
    End Sub

    Protected Sub GvCoberturasContenedor_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        GvCoberturasContenedor.PageIndex = e.NewPageIndex
        CargarCoberturasGrid()
    End Sub

    Protected Sub btnAgregarCobertura_Click(sender As Object, e As EventArgs)
        Dim nombreCobertura As String = txtCoberturaContenedor.Text.Trim()
        If String.IsNullOrEmpty(nombreCobertura) Then Return

        Dim nuevaCobertura As New Cobertura With {
            .CoberturaId = If(CoberturasSession.Count = 0, 1, CoberturasSession.Max(Function(c) c.CoberturaId) + 1),
            .Nombre = nombreCobertura
        }

        CoberturasSession.Add(nuevaCobertura)
        Session("CoberturasTemporal") = CoberturasSession

        txtCoberturaContenedor.Text = ""
        CargarCoberturasGrid()
    End Sub
    Private Sub CargarCoberturasGrid()
        GvCoberturasContenedor.DataSource = CoberturasSession
        GvCoberturasContenedor.DataBind()
    End Sub


    Private Sub AgregarRiesgo(txt As TextBox, tipoRiesgoId As Integer, administracionId As Integer)

        Dim nombreRiesgo As String = txt.Text.Trim()
        If String.IsNullOrEmpty(nombreRiesgo) Then Return

        Dim nuevoRiesgo As New RiesgoCubierto With {
    .RiesgoCubiertoId = If(RiesgosSession.Count = 0, 1, RiesgosSession.Max(Function(r) r.RiesgoCubiertoId) + 1),
    .Nombre = nombreRiesgo,
    .TipoRiesgoId = tipoRiesgoId,
    .AdministracionBienId = administracionId,
    .PolizaMercanciaId = 0
    }

        RiesgosSession.Add(nuevoRiesgo)
        Session("RiesgosTemporal") = RiesgosSession

        txt.Text = ""
        CargarRiesgos()

    End Sub

    Private Sub EliminarRiesgo(id As Integer)

        Dim riesgoEliminar = RiesgosSession.FirstOrDefault(Function(r) r.RiesgoCubiertoId = id)

        If riesgoEliminar IsNot Nothing Then
            RiesgosSession.Remove(riesgoEliminar)
            Session("RiesgosTemporal") = RiesgosSession
            CargarRiesgos()

        End If

    End Sub

    Private Sub CargarRiesgos()

        CargarGridRiesgos(GvViajeCompleto, 1, 1)
        CargarGridRiesgos(GvViajeCompleto2, 1, 2)
        CargarGridRiesgos(GvViajeCompleto3, 1, 3)

        CargarGridRiesgos(GvContinuacionViaje, 2, 1)
        CargarGridRiesgos(GvContinuacionViaje2, 2, 2)
        CargarGridRiesgos(GvContinuacionViaje3, 2, 3)

        CargarGridRiesgos(GvCoberturasAdicionales, 3, 1)
        CargarGridRiesgos(GvCoberturasAdicionales2, 3, 2)
        CargarGridRiesgos(GvCoberturasAdicionales3, 3, 3)
    End Sub
    Protected Sub GvViajeCompleto_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        If e.CommandName = "Eliminar" Then
            Dim id As Integer = Convert.ToInt32(e.CommandArgument)
            EliminarRiesgo(id)
        End If
    End Sub

    Protected Sub GvViajeCompleto_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        GvViajeCompleto.PageIndex = e.NewPageIndex
        CargarGridRiesgos(GvViajeCompleto, 1, 1)
    End Sub

    Private Sub CargarGridRiesgos(grid As GridView, tipoRiesgoId As Integer, administracionId As Integer)
        grid.DataSource = RiesgosSession.
        Where(Function(r) r.TipoRiesgoId = tipoRiesgoId AndAlso r.AdministracionBienId = administracionId).ToList()
        grid.DataBind()
    End Sub


    Protected Sub BtnAgregarCoberturaV_Click(sender As Object, e As EventArgs)
        AgregarRiesgo(txtViajeCompleto, 1, 1)
    End Sub

    Protected Sub GvViajeCompleto2_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        If e.CommandName = "Eliminar" Then
            Dim id As Integer = Convert.ToInt32(e.CommandArgument)
            EliminarRiesgo(id)
        End If
    End Sub

    Protected Sub GvViajeCompleto2_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        GvViajeCompleto.PageIndex = e.NewPageIndex
        CargarGridRiesgos(GvViajeCompleto2, 1, 1)
    End Sub

    Protected Sub BtnAgregarCoberturaV2_Click(sender As Object, e As EventArgs)
        AgregarRiesgo(txtviajeCompleto2, 1, 2)
    End Sub

    Protected Sub GvViajeCompleto3_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        If e.CommandName = "Eliminar" Then
            Dim id As Integer = Convert.ToInt32(e.CommandArgument)
            EliminarRiesgo(id)
        End If
    End Sub

    Protected Sub GvViajeCompleto3_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        GvViajeCompleto.PageIndex = e.NewPageIndex
        CargarGridRiesgos(GvViajeCompleto3, 1, 2)
    End Sub

    Protected Sub BtnAgregarCoberturaV3_Click(sender As Object, e As EventArgs)
        AgregarRiesgo(txtviajeCompleto3, 1, 3)
    End Sub

    Protected Sub GvContinuacionViaje_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        If e.CommandName = "Eliminar" Then
            Dim id As Integer = Convert.ToInt32(e.CommandArgument)
            EliminarRiesgo(id)
        End If
    End Sub

    Protected Sub GvContinuacionViaje_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        GvContinuacionViaje.PageIndex = e.NewPageIndex
        CargarGridRiesgos(GvContinuacionViaje, 2, 1)
    End Sub

    Protected Sub btnagregarContinuacionViaje_Click(sender As Object, e As EventArgs)
        AgregarRiesgo(txtcontinuacionViajeC, 2, 1)
    End Sub

    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs)
        cargarPolizas()
        pnlFormularioPolizas.Visible = False
        PnlTabla.Visible = True
        pnlEncabezado.Visible = True
        LimpiarFormulario()
    End Sub

    Protected Sub GvContinuacionViaje2_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        If e.CommandName = "Eliminar" Then
            Dim id As Integer = Convert.ToInt32(e.CommandArgument)
            EliminarRiesgo(id)
        End If
    End Sub

    Protected Sub GvContinuacionViaje2_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        GvContinuacionViaje2.PageIndex = e.NewPageIndex
        CargarGridRiesgos(GvContinuacionViaje2, 2, 2)
    End Sub

    Protected Sub btnagregarContinuacionViaje2_Click(sender As Object, e As EventArgs)
        AgregarRiesgo(txtcontinuacionViajeC2, 2, 2)
    End Sub

    Protected Sub GvContinuacionViaje3_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        If e.CommandName = "Eliminar" Then
            Dim id As Integer = Convert.ToInt32(e.CommandArgument)
            EliminarRiesgo(id)
        End If
    End Sub

    Protected Sub GvContinuacionViaje3_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        GvContinuacionViaje3.PageIndex = e.NewPageIndex
        CargarGridRiesgos(GvContinuacionViaje3, 2, 3)
    End Sub

    Protected Sub btnagregarContinuacionViaje3_Click(sender As Object, e As EventArgs)
        AgregarRiesgo(txtcontinuacionViajeC3, 2, 3)
    End Sub


    Protected Sub GvCoberturasAdicionales_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        If e.CommandName = "Eliminar" Then
            Dim id As Integer = Convert.ToInt32(e.CommandArgument)
            EliminarRiesgo(id)
        End If
    End Sub

    Protected Sub GvCoberturasAdicionales_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        GvCoberturasAdicionales.PageIndex = e.NewPageIndex
        CargarGridRiesgos(GvCoberturasAdicionales, 3, 1)
    End Sub

    Protected Sub btnAgregarCcoberturasAdicionales_Click(sender As Object, e As EventArgs)
        AgregarRiesgo(txtcoberturasAdicionales, 3, 1)

    End Sub

    Protected Sub GvCoberturasAdicionales2_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        If e.CommandName = "Eliminar" Then
            Dim id As Integer = Convert.ToInt32(e.CommandArgument)
            EliminarRiesgo(id)
        End If
    End Sub

    Protected Sub GvCoberturasAdicionales2_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        GvCoberturasAdicionales2.PageIndex = e.NewPageIndex
        CargarGridRiesgos(GvCoberturasAdicionales2, 3, 2)
    End Sub

    Protected Sub btnAgregarCcoberturasAdicionales2_Click(sender As Object, e As EventArgs)
        AgregarRiesgo(txtcoberturasAdicionales2, 3, 2)
    End Sub

    Protected Sub GvCoberturasAdicionales3_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        If e.CommandName = "Eliminar" Then
            Dim id As Integer = Convert.ToInt32(e.CommandArgument)
            EliminarRiesgo(id)
        End If
    End Sub

    Protected Sub GvCoberturasAdicionales3_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        GvCoberturasAdicionales3.PageIndex = e.NewPageIndex
        CargarGridRiesgos(GvCoberturasAdicionales3, 3, 3)
    End Sub

    Protected Sub btnAgregarCcoberturasAdicionales3_Click(sender As Object, e As EventArgs)
        AgregarRiesgo(txtcoberturasAdicionales3, 3, 3)
    End Sub


End Class