Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports WebAdmin.MercanciaSegura.DOM.Modelos
Public Class AdminVendedor
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            pnlDatosFisica.Visible = True
            pnlRazonSocial.Visible = False
            DropdownHelpers.CargarTipoPersona(ddlTipoPersona)
            DropdownHelpers.cargarTipoVendedor(ddlTipoVendedor)
            CargarVendedores()
            txtFechaRegistro.Text = DateTime.Now.ToString("yyyy-MM-dd")
            CargarDatos()
        End If
    End Sub

    Protected Sub btnAgregarVendedor_Click(sender As Object, e As EventArgs)
        pnlFormularioVendedor.Visible = True
        PnlTabla.Visible = False
        PnlEncabezado.Visible = False
        lblMensaje.Text = "Nuevo Registro"

        ddlEstatus.SelectedValue = "1"
        ddlEstatus.Enabled = False
        ddlTipoPersona.Enabled = True
    End Sub

    Protected Sub ddlTipoPersona_SelectedIndexChanged(sender As Object, e As EventArgs)
        If ddlTipoPersona.SelectedValue = "1" Then
            pnlRazonSocial.Visible = False
            pnlDatosFisica.Visible = True
            pnlNombreCompleto.Visible = True
            pnlGenero.Visible = True

        ElseIf ddlTipoPersona.SelectedValue = "2" Then
            pnlRazonSocial.Visible = True
            pnlDatosFisica.Visible = False
            pnlNombreCompleto.Visible = False
            pnlGenero.Visible = False
        End If
    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs)

        ' El marcado .required lo revisa JavaScript. Si el JS no corrio, o el
        ' catalogo llego vacio y el combo quedo sin opciones, esto es lo unico
        ' que impide seguir con datos incompletos.
        Dim falta As String = ValidarVendedor()

        If falta <> "" Then
            Avisar(falta, "danger")
            Exit Sub
        End If

        Dim api As New ConsumoApi()

        Dim tipoPersonaId As Integer = Convertir.EnteroO(ddlTipoPersona.SelectedValue, 0)
        Dim tipoVendedorId As Integer = Convertir.EnteroO(ddlTipoVendedor.SelectedValue, 0)

        Dim comisionValue As Decimal = 0
        Dim texto As String = txtComision.Text.Replace("%", "").Trim()

        Decimal.TryParse(texto, comisionValue)

        If comisionValue > 100 Then comisionValue = 100
        If comisionValue < 0 Then comisionValue = 0

        Dim nombreCompleto As String
        If tipoPersonaId = 1 Then
            nombreCompleto = txtNombre.Text.Trim() & " " & txtApellidoP.Text.Trim() & " " & txtApellidoM.Text.Trim()
        Else
            nombreCompleto = txtRazonSocial.Text.Trim()
        End If

        Dim correo As String = txtCorreo.Text.Trim()

        If correo = "" Then
            correo = Nothing
        End If

        Dim vendedor As New Vendedor With {
        .ApellidoPaterno = txtApellidoP.Text,
        .ApellidoMaterno = txtApellidoM.Text,
        .Nombres = txtNombre.Text,
        .NombreCompleto = nombreCompleto,
        .TipoPersonaId = tipoPersonaId,
        .TipoVendedorId = tipoVendedorId,
        .Estatus = (ddlEstatus.SelectedValue = "1"),
        .Clave = txtClave.Text,
        .Rfc = txtRFC.Text,
        .Domicilio = txtDomicilio.Text,
        .Cp = txtCP.Text,
        .Colonia = txtColonia.Text,
        .Estado = txtEstado.Text,
        .Genero = ddlGenero.SelectedValue,
        .Telefono = txtTelefono.Text,
        .CorreoElectronico = correo,
        .Observaciones = txtObservaciones.Text,
        .Comision = comisionValue,
        .FechaRegistro = Date.Now
            }

        Dim json As String = JsonConvert.SerializeObject(vendedor)
        Dim respuesta As String
        Dim mensajeToast As String = ""
        Dim esExito As Boolean = False

        If String.IsNullOrEmpty(hfVendedorId.Value) Then
            respuesta = api.PostVendedor(json)
        Else
            Dim vendedorId As Integer = Convertir.EnteroO(hfVendedorId.Value, 0)
            respuesta = api.PutEditarVendedores(vendedorId, json)
        End If

        Dim respuestaObj As JObject = JObject.Parse(respuesta)

        If respuestaObj("errors") IsNot Nothing Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "apiErrors",
        "showToast('No se pudo guardar el vendedor', 'danger');", True)
        Else
            mensajeToast = If(String.IsNullOrEmpty(hfVendedorId.Value),
                                    "Vendedor agregado correctamente",
                                    "Vendedor editado correctamente")
            ClientScript.RegisterStartupScript(Me.GetType(), "toast",
        "showToast('" & mensajeToast & "', 'success');", True)


            CargarVendedores()
            pnlFormularioVendedor.Visible = False
            PnlTabla.Visible = True
            PnlEncabezado.Visible = True
            LimpiarFormulario()
        End If

    End Sub

    Public Sub CargarVendedores()
        Dim api As New ConsumoApi()
        Dim cargarVendedores As String = api.GetCargarVendedores()

        Dim listavendedores As New List(Of Vendedor)

        If Not String.IsNullOrWhiteSpace(cargarVendedores) AndAlso
           cargarVendedores <> "null" AndAlso
           Not cargarVendedores.StartsWith("ERROR") Then

            listavendedores = JsonConvert.DeserializeObject(Of List(Of Vendedor))(cargarVendedores)
            If listavendedores Is Nothing Then listavendedores = New List(Of Vendedor)
        End If

        ' El filtro vive aquí y no en el TextChanged, para que la paginación no
        ' pierda la búsqueda al cambiar de página.
        Dim busqueda As String = txtBuscarVendedor.Text.Trim()

        If busqueda.Length > 0 Then
            listavendedores = listavendedores.
                Where(Function(v) Contiene(v.NombreCompleto, busqueda) OrElse
                                  Contiene(v.Rfc, busqueda) OrElse
                                  Contiene(v.Clave, busqueda)).
                ToList()
        End If

        Dim ultimaPagina As Integer = 0

        If listavendedores.Count > 0 Then
            ultimaPagina = CInt(Math.Ceiling(listavendedores.Count / CDbl(gvVendedores.PageSize))) - 1
        End If

        If gvVendedores.PageIndex > ultimaPagina Then
            gvVendedores.PageIndex = ultimaPagina
        End If

        gvVendedores.DataSource = listavendedores
        gvVendedores.DataBind()
    End Sub

    ''' <summary>
    ''' Búsqueda parcial que ignora mayúsculas y acentos, y tolera nulos.
    ''' </summary>
    Private Function Contiene(valor As String, busqueda As String) As Boolean
        If String.IsNullOrEmpty(valor) Then Return False

        Return Globalization.CultureInfo.InvariantCulture.CompareInfo.IndexOf(
            valor, busqueda,
            Globalization.CompareOptions.IgnoreCase Or Globalization.CompareOptions.IgnoreNonSpace) >= 0
    End Function

    Protected Sub gvVendedores_PageIndexChanging(sender As Object, e As GridViewPageEventArgs)
        gvVendedores.PageIndex = e.NewPageIndex
        CargarVendedores()
    End Sub

    Private Sub CargarDatos()
        Dim estatusBD As Boolean = True

        If estatusBD Then
            ddlEstatus.SelectedValue = "1"
        Else
            ddlEstatus.SelectedValue = "0"
        End If
    End Sub

    Private Sub LimpiarFormulario()
        txtNombre.Text = ""
        txtApellidoP.Text = ""
        txtApellidoM.Text = ""
        txtRazonSocial.Text = ""
        txtClave.Text = ""
        txtRFC.Text = ""
        txtDomicilio.Text = ""
        txtCP.Text = ""
        txtColonia.Text = ""
        txtEstado.Text = ""
        ddlGenero.SelectedIndex = 0
        txtTelefono.Text = ""
        txtCorreo.Text = ""
        txtObservaciones.Text = ""
        txtComision.Text = ""
    End Sub

    Protected Sub gvVendedores_RowCommand(sender As Object, e As GridViewCommandEventArgs)
        If e.CommandName = "Correo" Then
            AbrirCorreo(Convertir.EnteroO(Convert.ToString(e.CommandArgument), 0))
            Exit Sub
        End If

        If e.CommandName = "Editar" Then
            Dim vendedorId As Integer = Convertir.EnteroO(Convert.ToString(e.CommandArgument), 0)
            pnlFormularioVendedor.Visible = True
            PnlTabla.Visible = False
            PnlEncabezado.Visible = False
            lblMensaje.Text = "Editar Vendedor"

            EditarVendedor(vendedorId)
        End If

        If e.CommandName = "Eliminar" Then
            Dim api As New ConsumoApi()
            Dim vendedorId As Integer = Convertir.EnteroO(Convert.ToString(e.CommandArgument), 0)

            Dim eliminado As String = api.DeleteVendedores(vendedorId)

            CargarVendedores()

            Dim respuestaObj As JObject = JObject.Parse(eliminado)
            Dim mensaje As String = respuestaObj("message").ToString()

            If eliminado IsNot Nothing AndAlso eliminado <> "" Then
                ClientScript.RegisterStartupScript(Me.GetType(), "toast",
                    "showToast('" & mensaje & "', 'success');", True)
            Else
                ClientScript.RegisterStartupScript(Me.GetType(), "toast",
                    "showToast('Error al eliminar el vendedor', 'danger');", True)
            End If


        End If
    End Sub

    Protected Sub EditarVendedor(vendedorId As Integer)
        Dim api As New ConsumoApi()
        Dim objvendedor As String = api.GetVendedorId(vendedorId)

        Dim vendedor As Vendedor = JsonConvert.DeserializeObject(Of Vendedor)(objvendedor)

        hfVendedorId.Value = vendedor.VendedorId.ToString()

        If Not IsPostBack Then
            DropdownHelpers.CargarTipoPersona(ddlTipoPersona)
            DropdownHelpers.CargarTipoVendedor(ddlTipoVendedor)
        End If

        hfTipoPersona.Value = vendedor.TipoPersonaId.ToString()
        ddlTipoPersona.SelectedValue = vendedor.TipoPersonaId.ToString()
        ddlTipoVendedor.SelectedValue = vendedor.TipoVendedorId.ToString()
        txtNombre.Text = vendedor.Nombres
        txtApellidoP.Text = vendedor.ApellidoPaterno
        txtApellidoM.Text = vendedor.ApellidoMaterno
        txtRazonSocial.Text = If(vendedor.TipoPersonaId = 2, vendedor.NombreCompleto, "")
        txtNombreCompleto.Text = If(vendedor.TipoPersonaId = 1, vendedor.NombreCompleto, "")
        txtClave.Text = vendedor.Clave
        txtRFC.Text = vendedor.Rfc
        txtDomicilio.Text = vendedor.Domicilio
        txtCP.Text = vendedor.Cp
        txtColonia.Text = vendedor.Colonia
        txtEstado.Text = vendedor.Estado
        ddlGenero.SelectedValue = vendedor.Genero
        txtTelefono.Text = vendedor.Telefono
        txtCorreo.Text = vendedor.CorreoElectronico
        txtObservaciones.Text = vendedor.Observaciones
        txtComision.Text = Convertir.NumeroO(Convert.ToString(vendedor.Comision), 0).ToString("0.00") & "%"
        ddlEstatus.SelectedValue = If(vendedor.Estatus, "1", "0")
        ddlEstatus.Enabled = True
        ddlTipoPersona.Enabled = False
        If vendedor.TipoPersonaId = 1 Then
            pnlRazonSocial.Visible = False
            pnlDatosFisica.Visible = True
            pnlNombreCompleto.Visible = True
            pnlGenero.Visible = True
        Else
            pnlRazonSocial.Visible = True
            pnlDatosFisica.Visible = False
            pnlNombreCompleto.Visible = False
            pnlGenero.Visible = False
        End If
    End Sub

    Protected Sub btnCancelar_Click(sender As Object, e As EventArgs)
        CargarVendedores()
        pnlFormularioVendedor.Visible = False
        PnlTabla.Visible = True
        PnlEncabezado.Visible = True
        LimpiarFormulario()
    End Sub

    Protected Sub txtBuscarVendedor_TextChanged(sender As Object, e As EventArgs)
        ' Una búsqueda nueva siempre arranca en la primera página.
        gvVendedores.PageIndex = 0

        CargarVendedores()
    End Sub

    Protected Sub gvVendedores_RowDataBound(sender As Object, e As GridViewRowEventArgs)
        If e.Row.RowType <> DataControlRowType.DataRow Then Exit Sub

        RegistrarPostbackCompleto(e.Row)
    End Sub

    ''' <summary>
    ''' Los botones de la tabla muestran pnlFormularioVendedor, que vive fuera del
    ''' UpdatePanel del listado. Con un postback parcial esos cambios de visibilidad
    ''' no llegan al navegador y la pantalla queda en blanco, así que se fuerzan a
    ''' postback completo. Al estar dentro de una plantilla del GridView no se
    ''' pueden declarar como PostBackTrigger por ID.
    ''' </summary>
    Private Sub RegistrarPostbackCompleto(contenedor As Control)
        Dim sm As ScriptManager = ScriptManager.GetCurrent(Page)

        If sm Is Nothing Then Exit Sub

        For Each ctl As Control In contenedor.Controls
            If TypeOf ctl Is IButtonControl Then sm.RegisterPostBackControl(ctl)

            If ctl.HasControls() Then RegistrarPostbackCompleto(ctl)
        Next
    End Sub

    Protected Sub ddlTipoEstatusCliente_SelectedIndexChanged(sender As Object, e As EventArgs)
        FiltrarVendedoresPorEstatus()
    End Sub

    Public Sub FiltrarVendedoresPorEstatus()
        Dim api As New ConsumoApi()
        Dim json As String = api.GetCargarVendedores()

        Dim lista As List(Of Vendedor) =
            JsonConvert.DeserializeObject(Of List(Of Vendedor))(json)

        Select Case ddlTipoEstatusCliente.SelectedValue
            Case "1"
                lista = lista.Where(Function(v) v.Estatus = True).ToList()

            Case "2"
                lista = lista.Where(Function(v) v.Estatus = False).ToList()

            Case Else
        End Select

        gvVendedores.DataSource = lista
        gvVendedores.DataBind()
    End Sub

    ''' <summary>
    ''' Abre el control de envio de correo. Es el mismo control que usan los demas
    ''' modulos: aqui solo se le pasan el destinatario y los datos del registro.
    ''' </summary>
    Private Sub AbrirCorreo(registroId As Integer)

        ucCorreo.Abrir(DestinatariosDe(registroId), ValoresDe(registroId))

        PnlEncabezado.Visible = False
        PnlTabla.Visible = False
        pnlFormularioVendedor.Visible = False
    End Sub

    Protected Sub ucCorreo_Cancelado(sender As Object, e As EventArgs)
        VolverDelCorreo()
    End Sub

    Protected Sub ucCorreo_Enviado(sender As Object, e As EventArgs)
        VolverDelCorreo()
    End Sub

    Private Sub VolverDelCorreo()
        PnlEncabezado.Visible = True
        PnlTabla.Visible = True
        pnlFormularioVendedor.Visible = False
    End Sub

    Private Function DestinatariosDe(registroId As Integer) As String

        Dim v = VendedorDe(registroId)

        If v Is Nothing Then Return String.Empty

        Return If(v.CorreoElectronico, String.Empty)
    End Function

    Private Function ValoresDe(registroId As Integer) As Dictionary(Of String, String)

        Dim valores As New Dictionary(Of String, String)

        Dim v = VendedorDe(registroId)
        If v Is Nothing Then Return valores

        If Not String.IsNullOrWhiteSpace(v.NombreCompleto) Then
            valores("Nombre Completo") = v.NombreCompleto
            valores("Nombre") = v.NombreCompleto.Split(" "c)(0)
            valores("Vendedor") = v.NombreCompleto
        End If

        If Not String.IsNullOrWhiteSpace(v.Rfc) Then valores("RFC") = v.Rfc
        If Not String.IsNullOrWhiteSpace(v.CorreoElectronico) Then valores("Correo") = v.CorreoElectronico

        Return valores
    End Function

    Private Function VendedorDe(registroId As Integer) As Vendedor

        Dim api As New ConsumoApi()
        Dim json As String = api.GetVendedorId(registroId)

        If String.IsNullOrWhiteSpace(json) OrElse json.StartsWith("ERROR") Then Return Nothing

        Return JsonConvert.DeserializeObject(Of Vendedor)(json)
    End Function

#Region "Validación"

    ''' <summary>
    ''' Devuelve el primer faltante, o cadena vacía si todo esta completo.
    ''' </summary>
    Private Function ValidarVendedor() As String

        If Convertir.SinElegir(ddlTipoPersona) Then Return "Elige el tipo de persona."
        If Convertir.SinElegir(ddlTipoVendedor) Then Return "Elige el tipo de vendedor."

        Dim esFisica As Boolean = Convertir.EnteroO(ddlTipoPersona.SelectedValue, 0) = 1

        If esFisica Then
            If txtNombre.Text.Trim() = "" Then Return "Captura el nombre."
            If txtApellidoP.Text.Trim() = "" Then Return "Captura el apellido paterno."
        ElseIf txtRazonSocial.Text.Trim() = "" Then
            Return "Captura la razón social."
        End If

        If txtClave.Text.Trim() = "" Then Return "Captura la clave."
        If txtRFC.Text.Trim() = "" Then Return "Captura el RFC."

        If Not Convertir.Fecha(txtFechaRegistro.Text).HasValue Then
            Return "Captura la fecha de registro."
        End If

        Dim correo As String = txtCorreo.Text.Trim()

        If correo <> "" AndAlso Not correo.Contains("@") Then
            Return "El correo no tiene un formato válido."
        End If

        Return ""
    End Function

    ''' <summary>Mismo toast que ya usa el resto de la pantalla.</summary>
    Private Sub Avisar(mensaje As String, tipo As String)

        ScriptManager.RegisterStartupScript(Me, Me.GetType(), "avisoVendedor",
            "showToast('" & mensaje.Replace("'", "\'") & "', '" & tipo & "');", True)
    End Sub

#End Region

End Class