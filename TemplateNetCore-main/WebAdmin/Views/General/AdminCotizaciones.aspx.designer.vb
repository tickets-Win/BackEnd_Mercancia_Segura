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


Partial Public Class AdminCotizaciones

    '''<summary>
    '''Control pnlEncabezado.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents pnlEncabezado As Global.System.Web.UI.WebControls.Panel

    '''<summary>
    '''Control btnAgregarCotizacion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents btnAgregarCotizacion As Global.System.Web.UI.WebControls.Button

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
    '''Control txtBeneficiarioPreferente.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtBeneficiarioPreferente As Global.System.Web.UI.WebControls.TextBox

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
    '''Control txtSumaAsegurada.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtSumaAsegurada As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control ddlMoneda.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents ddlMoneda As Global.System.Web.UI.WebControls.DropDownList

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
End Class
