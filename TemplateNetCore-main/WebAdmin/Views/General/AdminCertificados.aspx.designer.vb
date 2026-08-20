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
    '''Control UpCertificados.
    '''</summary>
    Protected WithEvents UpCertificados As Global.System.Web.UI.UpdatePanel

    '''<summary>
    '''Control pnlAviso.
    '''</summary>
    Protected WithEvents pnlAviso As Global.System.Web.UI.WebControls.Panel

    '''<summary>
    '''Control lblAviso.
    '''</summary>
    Protected WithEvents lblAviso As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control pnlListado.
    '''</summary>
    Protected WithEvents pnlListado As Global.System.Web.UI.WebControls.Panel

    '''<summary>
    '''Control txtBuscarCertificado.
    '''</summary>
    Protected WithEvents txtBuscarCertificado As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control ddlPeriodo.
    '''</summary>
    Protected WithEvents ddlPeriodo As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control PnlTabla.
    '''</summary>
    Protected WithEvents PnlTabla As Global.System.Web.UI.WebControls.Panel

    '''<summary>
    '''Control gvCertificados.
    '''</summary>
    Protected WithEvents gvCertificados As Global.System.Web.UI.WebControls.GridView

    '''<summary>
    '''Control pnlDetalle.
    '''</summary>
    Protected WithEvents pnlDetalle As Global.System.Web.UI.WebControls.Panel

    '''<summary>
    '''Control lnkVolver.
    '''</summary>
    Protected WithEvents lnkVolver As Global.System.Web.UI.WebControls.LinkButton

    '''<summary>
    '''Control txtClave.
    '''</summary>
    Protected WithEvents txtClave As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtEstatus.
    '''</summary>
    Protected WithEvents txtEstatus As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtFechaRegistro.
    '''</summary>
    Protected WithEvents txtFechaRegistro As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtAsegurado.
    '''</summary>
    Protected WithEvents txtAsegurado As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtSumaAsegurada.
    '''</summary>
    Protected WithEvents txtSumaAsegurada As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtVigenciaDel.
    '''</summary>
    Protected WithEvents txtVigenciaDel As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtVigenciaHasta.
    '''</summary>
    Protected WithEvents txtVigenciaHasta As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtCotizacionId.
    '''</summary>
    Protected WithEvents txtCotizacionId As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtTipo.
    '''</summary>
    Protected WithEvents txtTipo As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtFechaCotizacion.
    '''</summary>
    Protected WithEvents txtFechaCotizacion As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtNumeroPoliza.
    '''</summary>
    Protected WithEvents txtNumeroPoliza As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtMoneda.
    '''</summary>
    Protected WithEvents txtMoneda As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtCliente.
    '''</summary>
    Protected WithEvents txtCliente As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtCotVigenciaDel.
    '''</summary>
    Protected WithEvents txtCotVigenciaDel As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtCotVigenciaHasta.
    '''</summary>
    Protected WithEvents txtCotVigenciaHasta As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control pnlDetalleMercancia.
    '''</summary>
    Protected WithEvents pnlDetalleMercancia As Global.System.Web.UI.WebControls.Panel

    '''<summary>
    '''Control txtTransito.
    '''</summary>
    Protected WithEvents txtTransito As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtClasificacion.
    '''</summary>
    Protected WithEvents txtClasificacion As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtSubclasificacion.
    '''</summary>
    Protected WithEvents txtSubclasificacion As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtDescripcionMercancia.
    '''</summary>
    Protected WithEvents txtDescripcionMercancia As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtTipoEmpaque.
    '''</summary>
    Protected WithEvents txtTipoEmpaque As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtOrigen.
    '''</summary>
    Protected WithEvents txtOrigen As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtDestino.
    '''</summary>
    Protected WithEvents txtDestino As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtMediosConduccion.
    '''</summary>
    Protected WithEvents txtMediosConduccion As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtMedioTransporte.
    '''</summary>
    Protected WithEvents txtMedioTransporte As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtObservaciones.
    '''</summary>
    Protected WithEvents txtObservaciones As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtMedidasSeguridad.
    '''</summary>
    Protected WithEvents txtMedidasSeguridad As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtDeducibles.
    '''</summary>
    Protected WithEvents txtDeducibles As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtCuotaAplicable.
    '''</summary>
    Protected WithEvents txtCuotaAplicable As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtCuotaMinima.
    '''</summary>
    Protected WithEvents txtCuotaMinima As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtTipoCambio.
    '''</summary>
    Protected WithEvents txtTipoCambio As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtCotSumaAsegurada.
    '''</summary>
    Protected WithEvents txtCotSumaAsegurada As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control pnlDetalleContenedor.
    '''</summary>
    Protected WithEvents pnlDetalleContenedor As Global.System.Web.UI.WebControls.Panel

    '''<summary>
    '''Control gvContenedores.
    '''</summary>
    Protected WithEvents gvContenedores As Global.System.Web.UI.WebControls.GridView

    '''<summary>
    '''Control gvBienes.
    '''</summary>
    Protected WithEvents gvBienes As Global.System.Web.UI.WebControls.GridView

    '''<summary>
    '''Control gvCoberturas.
    '''</summary>
    Protected WithEvents gvCoberturas As Global.System.Web.UI.WebControls.GridView

    '''<summary>
    '''Control txtPrima.
    '''</summary>
    Protected WithEvents txtPrima As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtGastosExpedicion.
    '''</summary>
    Protected WithEvents txtGastosExpedicion As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtSubtotal.
    '''</summary>
    Protected WithEvents txtSubtotal As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtIVA.
    '''</summary>
    Protected WithEvents txtIVA As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control txtTotal.
    '''</summary>
    Protected WithEvents txtTotal As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control ucCorreo.
    '''</summary>
    Protected WithEvents ucCorreo As Global.WebAdmin.EnvioCorreoControl
End Class
