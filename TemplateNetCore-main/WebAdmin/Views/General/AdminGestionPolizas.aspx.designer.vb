'------------------------------------------------------------------------------
' <generado automáticamente>
'     Este código fue generado por una herramienta.
'
'     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
'     se vuelve a generar el código. 
' </generado automáticamente>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Partial Public Class AdminGestionPolizas

    '''<summary>
    '''Control pnlEncabezado.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents pnlEncabezado As Global.System.Web.UI.WebControls.Panel

    '''<summary>
    '''Control btnAgregarPoliza.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnAgregarPoliza As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control txtBuscarPolizas.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtBuscarPolizas As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control ddlTipoPolizas.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents ddlTipoPolizas As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control PnlTabla.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents PnlTabla As Global.System.Web.UI.WebControls.Panel

    '''<summary>
    '''Control gvPolizas.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents gvPolizas As Global.System.Web.UI.WebControls.GridView

    '''<summary>
    '''Control pnlFormularioPolizas.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents pnlFormularioPolizas As Global.System.Web.UI.WebControls.Panel

    '''<summary>
    '''Control lblMensaje.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblMensaje As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control btnCancelar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnCancelar As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control btnGuardar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnGuardar As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control ddlProducto.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents ddlProducto As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control txtTipoPoliza.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtTipoPoliza As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtNumeroPoliza.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtNumeroPoliza As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control ddlContratante.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents ddlContratante As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control ddlAseguradora.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents ddlAseguradora As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control ddlSubRamo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents ddlSubRamo As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control txtVigenciaDel.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtVigenciaDel As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtVigenciaHasta.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtVigenciaHasta As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control ddlEstatus.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents ddlEstatus As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control ddlFormaPago.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents ddlFormaPago As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control ddlMoneda.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents ddlMoneda As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control txtClaveAgente.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtClaveAgente As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtFolioPoliza.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtFolioPoliza As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control pnlNombreInternoPoliza.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents pnlNombreInternoPoliza As Global.System.Web.UI.WebControls.Panel

    '''<summary>
    '''Control txtNombreInternoPolizaContenedor.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtNombreInternoPolizaContenedor As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtTrayectosAsegurados.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtTrayectosAsegurados As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control pnlMediosTransporte.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents pnlMediosTransporte As Global.System.Web.UI.WebControls.Panel

    '''<summary>
    '''Control txtMedioTransporte.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtMedioTransporte As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control pnlMercancia.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents pnlMercancia As Global.System.Web.UI.WebControls.Panel

    '''<summary>
    '''Control txtNombreInternoPoliza1.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtNombreInternoPoliza1 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtBienesAsegurados.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtBienesAsegurados As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control btnAgregaBienAsegurado.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnAgregaBienAsegurado As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control txtBienesExcluidos.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtBienesExcluidos As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control btnAgregacollapseBienesExcluidos.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnAgregacollapseBienesExcluidos As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control txtbienesSujetosConsulta.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtbienesSujetosConsulta As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control btnAgregaBienesSujetosConsulta.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnAgregaBienesSujetosConsulta As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control txtTerrestreAereo1.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtTerrestreAereo1 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtMaritimo1.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtMaritimo1 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtPaqueteria1.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtPaqueteria1 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtViajeCompleto.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtViajeCompleto As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control BtnAgregarCoberturaV.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents BtnAgregarCoberturaV As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control txtcontinuacionViajeC.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtcontinuacionViajeC As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control btnagregarContinuacionViaje.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnagregarContinuacionViaje As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control txtcoberturasAdicionales.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtcoberturasAdicionales As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control btnAgregarCcoberturasAdicionales.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnAgregarCcoberturasAdicionales As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control txtDeducibles1.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtDeducibles1 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtCompras1.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtCompras1 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtVentas1.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtVentas1 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtMaquila1.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtMaquila1 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtBienesUsados1.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtBienesUsados1 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtEmbarquesEntreFiliales1.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtEmbarquesEntreFiliales1 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtOtrosBasesIndemnizacion1.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtOtrosBasesIndemnizacion1 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtCuotaGeneralPoliza1.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtCuotaGeneralPoliza1 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtMedicamentos1.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtMedicamentos1 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtCobreAluminioAcero1.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtCobreAluminioAcero1 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtMedicamentosControlados1.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtMedicamentosControlados1 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtEQ1.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtEQ1 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control chkEQAplica.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents chkEQAplica As Global.System.Web.UI.HtmlControls.HtmlInputCheckBox

    '''<summary>
    '''Control txtPrimaNetaMercancia1.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtPrimaNetaMercancia1 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtDerechoPolizaMercancia1.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtDerechoPolizaMercancia1 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtOtrosMercancia1.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtOtrosMercancia1 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtIVAMercancia1.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtIVAMercancia1 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtPrimaTotalMercancia1.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtPrimaTotalMercancia1 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtNombreInternoPoliza2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtNombreInternoPoliza2 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtBienesAsegurados2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtBienesAsegurados2 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control btnAgregaBienAsegurado2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnAgregaBienAsegurado2 As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control txtBienesExcluidos2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtBienesExcluidos2 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control btnAgregacollapseBienesExcluidos2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnAgregacollapseBienesExcluidos2 As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control txtbienesSujetosConsulta2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtbienesSujetosConsulta2 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control btnAgregaBienesSujetosConsulta2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnAgregaBienesSujetosConsulta2 As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control txtTerrestreAereo2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtTerrestreAereo2 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtMaritimo2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtMaritimo2 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtPaqueteria2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtPaqueteria2 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtviajeCompleto2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtviajeCompleto2 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control BtnAgregarCoberturaV2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents BtnAgregarCoberturaV2 As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control txtcontinuacionViajeC2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtcontinuacionViajeC2 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control btnagregarContinuacionViaje2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnagregarContinuacionViaje2 As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control txtcoberturasAdicionales2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtcoberturasAdicionales2 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control btnAgregarCcoberturasAdicionales2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnAgregarCcoberturasAdicionales2 As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control txtDeducibles2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtDeducibles2 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtCompras2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtCompras2 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtVentas2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtVentas2 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtMaquila2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtMaquila2 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtBienesUsados2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtBienesUsados2 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtEmbarquesEntreFiliales2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtEmbarquesEntreFiliales2 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtOtrosBasesIndemnizacion2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtOtrosBasesIndemnizacion2 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtCuotaGeneralPoliza2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtCuotaGeneralPoliza2 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtMedicamentos2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtMedicamentos2 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtCobreAluminioAcero2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtCobreAluminioAcero2 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtMedicamentosControlados2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtMedicamentosControlados2 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtEQ2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtEQ2 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control chkEQAplica2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents chkEQAplica2 As Global.System.Web.UI.HtmlControls.HtmlInputCheckBox

    '''<summary>
    '''Control txtPrimaNetaMercancia2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtPrimaNetaMercancia2 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtDerechoPolizaMercancia2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtDerechoPolizaMercancia2 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtOtrosMercancia2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtOtrosMercancia2 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtIVAMercancia2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtIVAMercancia2 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtPrimaTotalMercancia2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtPrimaTotalMercancia2 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtNombreInternoPoliza3.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtNombreInternoPoliza3 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtBienesAsegurados3.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtBienesAsegurados3 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control btnAgregaBienAsegurado3.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnAgregaBienAsegurado3 As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control txtBienesExcluidos3.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtBienesExcluidos3 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control btnAgregacollapseBienesExcluidos3.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnAgregacollapseBienesExcluidos3 As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control txtbienesSujetosConsulta3.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtbienesSujetosConsulta3 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control btnAgregaBienesSujetosConsulta3.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnAgregaBienesSujetosConsulta3 As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control txtTerrestreAereo3.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtTerrestreAereo3 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtMaritimo3.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtMaritimo3 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtPaqueteria3.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtPaqueteria3 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtviajeCompleto3.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtviajeCompleto3 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control BtnAgregarCoberturaV3.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents BtnAgregarCoberturaV3 As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control txtcontinuacionViajeC3.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtcontinuacionViajeC3 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control btnagregarContinuacionViaje3.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnagregarContinuacionViaje3 As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control txtcoberturasAdicionales3.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtcoberturasAdicionales3 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control btnAgregarCcoberturasAdicionales3.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnAgregarCcoberturasAdicionales3 As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control txtDeducibles3.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtDeducibles3 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtCompras3.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtCompras3 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtVentas3.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtVentas3 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtMaquila3.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtMaquila3 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtBienesUsados3.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtBienesUsados3 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtEmbarquesEntreFiliales3.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtEmbarquesEntreFiliales3 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtOtrosBasesIndemnizacion3.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtOtrosBasesIndemnizacion3 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtCuotaGeneralPoliza3.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtCuotaGeneralPoliza3 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtMedicamentos3.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtMedicamentos3 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtCobreAluminioAcero3.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtCobreAluminioAcero3 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtMedicamentosControlados3.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtMedicamentosControlados3 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtEQ3.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtEQ3 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control chkEQAplica3.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents chkEQAplica3 As Global.System.Web.UI.HtmlControls.HtmlInputCheckBox

    '''<summary>
    '''Control txtPrimaNetaMercancia3.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtPrimaNetaMercancia3 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtDerechoPolizaMercancia3.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtDerechoPolizaMercancia3 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtOtrosMercancia3.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtOtrosMercancia3 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtIVAMercancia3.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtIVAMercancia3 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtPrimaTotalMercancia3.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtPrimaTotalMercancia3 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control pnlContenedor.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents pnlContenedor As Global.System.Web.UI.WebControls.Panel

    '''<summary>
    '''Control txtBienesAseguradosContenedor.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtBienesAseguradosContenedor As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control btnAgregarBienAseguradoContenedor.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnAgregarBienAseguradoContenedor As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control txtCoberturas.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtCoberturas As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control btnAgregarCobertura.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnAgregarCobertura As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control txtPrimaNeta.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtPrimaNeta As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtOtrosMontosPoliza.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtOtrosMontosPoliza As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtDerechoPoliza.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtDerechoPoliza As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtIVA.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtIVA As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtTotal.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtTotal As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtPorContenedor.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtPorContenedor As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtFerrocarril.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtFerrocarril As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtTerrestreMontosPoliza.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtTerrestreMontosPoliza As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtCuotaAplicable.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtCuotaAplicable As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtManiobrasRescateContenedor.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtManiobrasRescateContenedor As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtDañoMaterial.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtDañoMaterial As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtRobo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtRobo As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtPerdidaTotal.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtPerdidaTotal As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtPerdidaParcial.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtPerdidaParcial As Global.System.Web.UI.WebControls.TextBox
End Class
