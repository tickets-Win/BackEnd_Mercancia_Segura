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


Partial Public Class AdminPlantillas

    '''<summary>Control pnlAviso.</summary>
    Protected WithEvents pnlAviso As Global.System.Web.UI.WebControls.Panel

    '''<summary>Control lblAviso.</summary>
    Protected WithEvents lblAviso As Global.System.Web.UI.WebControls.Label

    '''<summary>Control hfPlantillaId.</summary>
    Protected WithEvents hfPlantillaId As Global.System.Web.UI.WebControls.HiddenField

    '''<summary>
    '''Control pnlListado.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents pnlListado As Global.System.Web.UI.WebControls.Panel

    '''<summary>
    '''Control pnlFormulario.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents pnlFormulario As Global.System.Web.UI.WebControls.Panel

    '''<summary>
    '''Control lnkRegresar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lnkRegresar As Global.System.Web.UI.WebControls.LinkButton

    '''<summary>
    '''Control lblTituloFormulario.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblTituloFormulario As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control lnkCancelar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lnkCancelar As Global.System.Web.UI.WebControls.LinkButton

    '''<summary>
    '''Control lnkGuardar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lnkGuardar As Global.System.Web.UI.WebControls.LinkButton

    '''<summary>
    '''Control txtNombrePlantilla.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtNombrePlantilla As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control ddlCategoriaPlantilla.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents ddlCategoriaPlantilla As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control txtAsunto.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtAsunto As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control ddlFuente.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents ddlFuente As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control ddlTamanioFuente.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents ddlTamanioFuente As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control ddlEstiloTexto.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents ddlEstiloTexto As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control txtCuerpoCorreo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtCuerpoCorreo As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control rptCampos.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents rptCampos As Global.System.Web.UI.WebControls.Repeater

    '''<summary>
    '''Control txtBuscarPlantilla.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtBuscarPlantilla As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control lnkNuevaPlantilla.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lnkNuevaPlantilla As Global.System.Web.UI.WebControls.LinkButton

    '''<summary>
    '''Control UpPlantillas.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents UpPlantillas As Global.System.Web.UI.UpdatePanel

    '''<summary>
    '''Control rptCategorias.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents rptCategorias As Global.System.Web.UI.WebControls.Repeater

    '''<summary>
    '''Control rptPlantillas.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents rptPlantillas As Global.System.Web.UI.WebControls.Repeater

    '''<summary>
    '''Control lblSinPlantillas.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblSinPlantillas As Global.System.Web.UI.WebControls.Label
End Class
