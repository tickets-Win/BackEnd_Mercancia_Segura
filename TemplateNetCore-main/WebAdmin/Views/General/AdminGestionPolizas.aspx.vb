Imports Newtonsoft.Json
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
            CargarBienesGrid()
        End If
    End Sub

    Protected Sub btnAgregarPoliza_Click(sender As Object, e As EventArgs)
        pnlFormularioPolizas.Visible = True
        pnlEncabezado.Visible = False
        PnlTabla.Visible = False
        lblMensaje.Text = "Nuevo Registro"

        LimpiarFormulario()
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
            .FolioPoliza = txtFolioPoliza.Text
            }

        Dim polizaMercanciaList As New List(Of PolizaMercancia)

        If Not String.IsNullOrWhiteSpace(txtNombreInternoPoliza1.Text) OrElse
       Not String.IsNullOrWhiteSpace(txtNombreInternoPoliza2.Text) OrElse
       Not String.IsNullOrWhiteSpace(txtNombreInternoPoliza3.Text) Then

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
        .EqContratistas = If(String.IsNullOrWhiteSpace(txtEQ1.Text), Nothing, Convert.ToDecimal(txtEQ1.Text)),
        .PrimaNeta = If(String.IsNullOrWhiteSpace(txtPrimaNetaMercancia1.Text), Nothing, Convert.ToDecimal(txtPrimaNetaMercancia1.Text)),
        .DerechoPoliza = If(String.IsNullOrWhiteSpace(txtDerechoPolizaMercancia1.Text), Nothing, Convert.ToDecimal(txtDerechoPolizaMercancia1.Text)),
        .OtroPrima = If(String.IsNullOrWhiteSpace(txtOtrosMercancia1.Text), Nothing, Convert.ToDecimal(txtOtrosMercancia1.Text)),
        .IVA = If(String.IsNullOrWhiteSpace(txtIVAMercancia1.Text), Nothing, Convert.ToDecimal(txtIVAMercancia1.Text)),
        .PrimaTotal = If(String.IsNullOrWhiteSpace(txtPrimaTotalMercancia1.Text), Nothing, Convert.ToDecimal(txtPrimaTotalMercancia1.Text))
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
            .EqContratistas = If(String.IsNullOrWhiteSpace(txtEQ2.Text), Nothing, Convert.ToDecimal(txtEQ2.Text)),
            .PrimaNeta = If(String.IsNullOrWhiteSpace(txtPrimaNetaMercancia2.Text), Nothing, Convert.ToDecimal(txtPrimaNetaMercancia2.Text)),
            .DerechoPoliza = If(String.IsNullOrWhiteSpace(txtDerechoPolizaMercancia2.Text), Nothing, Convert.ToDecimal(txtDerechoPolizaMercancia2.Text)),
            .OtroPrima = If(String.IsNullOrWhiteSpace(txtOtrosMercancia2.Text), Nothing, Convert.ToDecimal(txtOtrosMercancia2.Text)),
            .IVA = If(String.IsNullOrWhiteSpace(txtIVAMercancia2.Text), Nothing, Convert.ToDecimal(txtIVAMercancia2.Text)),
            .PrimaTotal = If(String.IsNullOrWhiteSpace(txtPrimaTotalMercancia2.Text), Nothing, Convert.ToDecimal(txtPrimaTotalMercancia2.Text))
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
            .EqContratistas = If(String.IsNullOrWhiteSpace(txtEQ3.Text), Nothing, Convert.ToDecimal(txtEQ3.Text)),
            .PrimaNeta = If(String.IsNullOrWhiteSpace(txtPrimaNetaMercancia3.Text), Nothing, Convert.ToDecimal(txtPrimaNetaMercancia3.Text)),
            .DerechoPoliza = If(String.IsNullOrWhiteSpace(txtDerechoPolizaMercancia3.Text), Nothing, Convert.ToDecimal(txtDerechoPolizaMercancia3.Text)),
            .OtroPrima = If(String.IsNullOrWhiteSpace(txtOtrosMercancia3.Text), Nothing, Convert.ToDecimal(txtOtrosMercancia3.Text)),
            .IVA = If(String.IsNullOrWhiteSpace(txtIVAMercancia3.Text), Nothing, Convert.ToDecimal(txtIVAMercancia3.Text)),
            .PrimaTotal = If(String.IsNullOrWhiteSpace(txtPrimaTotalMercancia3.Text), Nothing, Convert.ToDecimal(txtPrimaTotalMercancia3.Text))
        })

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
        If polizaMercanciaList.Count = 0 AndAlso
       (Not String.IsNullOrWhiteSpace(txtNombreInternoPolizaContenedor.Text) OrElse
        Not String.IsNullOrWhiteSpace(txtTrayectosAsegurados.Text) OrElse
        Not String.IsNullOrWhiteSpace(txtMedioTransporte.Text)) Then
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
        Dim json As String

        json = JsonConvert.SerializeObject(poliza, Formatting.Indented)
        System.Diagnostics.Debug.WriteLine("JSON enviado a API:" & json)

        respuesta = api.PostPoliza(json)

        cargarPolizas()
        pnlFormularioPolizas.Visible = False
        pnlEncabezado.Visible = True
        PnlTabla.Visible = True
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

        ddlProducto.SelectedIndex = 0
        ddlContratante.SelectedIndex = 0
        ddlAseguradora.SelectedIndex = 0
        ddlSubRamo.SelectedIndex = 0
        ddlFormaPago.SelectedIndex = 0
        ddlMoneda.SelectedIndex = 0
        ddlEstatus.SelectedIndex = 0

        BienesSession.Clear()
        CoberturasSession.Clear()
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

End Class