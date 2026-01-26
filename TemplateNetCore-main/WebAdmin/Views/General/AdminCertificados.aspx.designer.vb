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


Partial Public Class AdminCertificados

    '''<summary>
    '''Control pnlEncabezado.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents pnlEncabezado As Global.System.Web.UI.WebControls.Panel

    '''<summary>
    '''Control btnAgregarCertificado.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnAgregarCertificado As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control txtBuscarCotizacion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtBuscarCotizacion As Global.System.Web.UI.WebControls.TextBox

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
    '''Control pnlFormularioCotizaciones.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents pnlFormularioCotizaciones As Global.System.Web.UI.WebControls.Panel

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
    '''Control ddlTipoCotizacion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents ddlTipoCotizacion As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control txtFechaCotizacion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtFechaCotizacion As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control ddlCliente.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents ddlCliente As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control ddlNombreInternoPoliza.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents ddlNombreInternoPoliza As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control ddlBeneficiarioPreferente.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents ddlBeneficiarioPreferente As Global.System.Web.UI.WebControls.DropDownList

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
    '''Control txtMoneda.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtMoneda As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtSumaAsegurada.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtSumaAsegurada As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control pnlContenedor.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents pnlContenedor As Global.System.Web.UI.WebControls.Panel

    '''<summary>
    '''Control txtTamanoTipoContenedor.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtTamanoTipoContenedor As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtContenedores.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtContenedores As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtOpcionCuota.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtOpcionCuota As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtTarifa.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtTarifa As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control pnlMercanciaFormulario.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents pnlMercanciaFormulario As Global.System.Web.UI.WebControls.Panel

    '''<summary>
    '''Control txtSubRamo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtSubRamo As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control ddlTransito.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents ddlTransito As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control ddlClasificacion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents ddlClasificacion As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control ddlSubclasificación.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents ddlSubclasificación As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control txtDescripcionMercancia.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtDescripcionMercancia As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtTipoEmpaque.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtTipoEmpaque As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtOrigen.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtOrigen As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtDestino.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtDestino As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtMediosConduccion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtMediosConduccion As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtMedioTransporte.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtMedioTransporte As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtObservaciones.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtObservaciones As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control pnlMercancia.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents pnlMercancia As Global.System.Web.UI.WebControls.Panel

    '''<summary>
    '''Control ddlCoberturas.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents ddlCoberturas As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control btnAgregarCobertura.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnAgregarCobertura As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control pnlBienesAsegurados.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents pnlBienesAsegurados As Global.System.Web.UI.WebControls.Panel

    '''<summary>
    '''Control ddlbienesasegurados.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents ddlbienesasegurados As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control btnbienesasegurados.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnbienesasegurados As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control pnlCuotaAplicableMercancia.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents pnlCuotaAplicableMercancia As Global.System.Web.UI.WebControls.Panel

    '''<summary>
    '''Control txtMedidasSeguridad.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtMedidasSeguridad As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtDeducibles.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtDeducibles As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control chkCuotaAplicableN.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents chkCuotaAplicableN As Global.System.Web.UI.HtmlControls.HtmlInputCheckBox

    '''<summary>
    '''Control chkCuotaAplicableI.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents chkCuotaAplicableI As Global.System.Web.UI.HtmlControls.HtmlInputCheckBox

    '''<summary>
    '''Control txtCuotaAplicable.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtCuotaAplicable As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control chkCuotaMinimaN.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents chkCuotaMinimaN As Global.System.Web.UI.HtmlControls.HtmlInputCheckBox

    '''<summary>
    '''Control chkCuotaMinimaI.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents chkCuotaMinimaI As Global.System.Web.UI.HtmlControls.HtmlInputCheckBox

    '''<summary>
    '''Control txtCuotaMinima.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtCuotaMinima As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtTipoCambio.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtTipoCambio As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control ddlMonedaCotizar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents ddlMonedaCotizar As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control txtPrimaYSeguramiento.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtPrimaYSeguramiento As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtGastosExpedicion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtGastosExpedicion As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtSubtotal.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtSubtotal As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtIVA.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtIVA As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtTotalSeguroMercancia.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtTotalSeguroMercancia As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtTotalSeguroContenedor.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtTotalSeguroContenedor As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtTotalPagar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtTotalPagar As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control pnlCuotaAplicableContenedor.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents pnlCuotaAplicableContenedor As Global.System.Web.UI.WebControls.Panel

    '''<summary>
    '''Control txtCuotaSecos.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtCuotaSecos As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control ddlTipoTarifaSecos.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents ddlTipoTarifaSecos As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control txtCuotaRefrigerados.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtCuotaRefrigerados As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control ddlTipoRefrigerados.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents ddlTipoRefrigerados As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control txtCuota2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtCuota2 As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control ddlTipoIsotaques.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents ddlTipoIsotaques As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control txtTotalTarifa.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtTotalTarifa As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtGastosExpedicionContenedor.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtGastosExpedicionContenedor As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtIvaContenedor.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtIvaContenedor As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtTotalContenedor.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtTotalContenedor As Global.System.Web.UI.WebControls.TextBox
End Class
