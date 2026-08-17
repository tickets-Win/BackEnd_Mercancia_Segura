Imports Newtonsoft.Json
Imports WebAdmin.MercanciaSegura.DOM.Modelos

Public Class AdminPlantillas
    Inherits System.Web.UI.Page

    ''' <summary>
    ''' Categoría abierta. Se guarda en el ViewState para que sobreviva a los
    ''' postbacks al cambiar de una a otra.
    ''' </summary>
    Private Property CategoriaActiva As Integer
        Get
            Dim valor As Object = ViewState("CategoriaActiva")

            If valor IsNot Nothing Then Return CInt(valor)

            Dim primera = PlantillasCorreo.Categorias().FirstOrDefault()

            If primera Is Nothing Then Return 0

            Return primera.CategoriaPlantillaId
        End Get
        Set(value As Integer)
            ViewState("CategoriaActiva") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            CargarCategorias()
            CargarPlantillas()
        End If

    End Sub

#Region "Listado"

    Private Sub CargarCategorias()

        Dim categorias = PlantillasCorreo.Categorias()

        If categorias.Count = 0 Then
            Avisar("No se pudieron cargar las categorías. ¿Ya se publicó el API?", "warning")
        End If

        rptCategorias.DataSource = categorias
        rptCategorias.DataBind()
    End Sub

    Private Sub CargarPlantillas()

        Dim lista = PlantillasCorreo.PorCategoria(CategoriaActiva)

        rptPlantillas.DataSource = lista
        rptPlantillas.DataBind()

        ' El repeater no pinta nada cuando la lista viene vacía; el aviso lo
        ' sustituye para que la columna no se vea rota.
        lblSinPlantillas.Visible = (lista.Count = 0)
    End Sub

    Protected Sub rptCategorias_ItemCommand(source As Object, e As RepeaterCommandEventArgs)

        If e.CommandName <> "Seleccionar" Then Exit Sub

        Dim id As Integer
        If Not Integer.TryParse(Convert.ToString(e.CommandArgument), id) Then Exit Sub

        CategoriaActiva = id

        ' Las categorías se vuelven a enlazar para que la píldora activa cambie
        ' de lugar.
        CargarCategorias()
        CargarPlantillas()
    End Sub

    ''' <summary>Marca en azul oscuro la categoría abierta.</summary>
    Protected Function ClaseCategoria(valor As Object) As String

        Dim id As Integer

        If Integer.TryParse(Convert.ToString(valor), id) AndAlso id = CategoriaActiva Then
            Return "plt-categoria plt-categoria-activa"
        End If

        Return "plt-categoria"
    End Function

    Protected Sub rptPlantillas_ItemCommand(source As Object, e As RepeaterCommandEventArgs)

        Dim id As Integer
        If Not Integer.TryParse(Convert.ToString(e.CommandArgument), id) Then Exit Sub

        Select Case e.CommandName
            Case "Editar"
                EditarPlantilla(id)
            Case "Duplicar"
                DuplicarPlantilla(id)
            Case "Eliminar"
                EliminarPlantilla(id)
        End Select
    End Sub

#End Region

#Region "Alta y edicion"

    Protected Sub lnkNuevaPlantilla_Click(sender As Object, e As EventArgs)
        LimpiarFormulario()
        AbrirFormulario("Nueva Plantilla")
    End Sub

    Protected Sub lnkCancelar_Click(sender As Object, e As EventArgs)
        VolverAlListado()
    End Sub

    Private Sub AbrirFormulario(titulo As String)

        lblTituloFormulario.Text = titulo

        rptCampos.DataSource = PlantillasCorreo.Campos
        rptCampos.DataBind()

        pnlListado.Visible = False
        pnlFormulario.Visible = True
    End Sub

    Private Sub VolverAlListado()

        pnlFormulario.Visible = False
        pnlListado.Visible = True

        CargarCategorias()
        CargarPlantillas()
    End Sub

    ''' <summary>
    ''' Deja el formulario como recién abierto. Sin esto conserva en el ViewState
    ''' lo de la captura anterior.
    ''' </summary>
    Private Sub LimpiarFormulario()

        hfPlantillaId.Value = String.Empty

        txtNombrePlantilla.Text = String.Empty
        txtAsunto.Text = String.Empty
        txtCuerpoCorreo.Text = String.Empty

        CargarComboCategorias()

        SeleccionarValor(ddlCategoriaPlantilla, CategoriaActiva.ToString())
    End Sub

    Private Sub CargarComboCategorias()

        ddlCategoriaPlantilla.Items.Clear()

        For Each c In PlantillasCorreo.Categorias()
            ddlCategoriaPlantilla.Items.Add(New ListItem(c.Nombre, c.CategoriaPlantillaId.ToString()))
        Next
    End Sub

    Private Sub SeleccionarValor(ddl As DropDownList, valor As String)

        ddl.ClearSelection()

        If String.IsNullOrEmpty(valor) Then Exit Sub

        Dim item = ddl.Items.FindByValue(valor)

        If item IsNot Nothing Then item.Selected = True
    End Sub

    Private Sub EditarPlantilla(plantillaId As Integer)

        Dim p = PlantillasCorreo.PorId(plantillaId)

        If p Is Nothing Then
            Avisar("No se pudo consultar la plantilla.", "danger")
            Exit Sub
        End If

        LimpiarFormulario()

        hfPlantillaId.Value = p.PlantillaCorreoId.ToString()

        txtNombrePlantilla.Text = p.Nombre
        txtAsunto.Text = p.Asunto
        txtCuerpoCorreo.Text = p.CuerpoHtml

        SeleccionarValor(ddlCategoriaPlantilla, p.CategoriaPlantillaId.ToString())

        AbrirFormulario("Editar Plantilla")
    End Sub

    ''' <summary>
    ''' Copia la plantilla y abre el formulario con los datos, sin guardar todavía:
    ''' así se puede ajustar el nombre antes de crearla.
    ''' </summary>
    Private Sub DuplicarPlantilla(plantillaId As Integer)

        Dim p = PlantillasCorreo.PorId(plantillaId)

        If p Is Nothing Then
            Avisar("No se pudo consultar la plantilla.", "danger")
            Exit Sub
        End If

        LimpiarFormulario()

        ' Sin id: al guardar se crea una nueva.
        txtNombrePlantilla.Text = p.Nombre & " (copia)"
        txtAsunto.Text = p.Asunto
        txtCuerpoCorreo.Text = p.CuerpoHtml

        SeleccionarValor(ddlCategoriaPlantilla, p.CategoriaPlantillaId.ToString())

        AbrirFormulario("Duplicar Plantilla")
    End Sub

    Protected Sub lnkGuardar_Click(sender As Object, e As EventArgs)

        If String.IsNullOrWhiteSpace(txtNombrePlantilla.Text) Then
            Avisar("Captura el nombre de la plantilla.", "warning")
            Exit Sub
        End If

        Dim categoriaId As Integer

        If Not Integer.TryParse(ddlCategoriaPlantilla.SelectedValue, categoriaId) Then
            Avisar("Elige la categoría de la plantilla.", "warning")
            Exit Sub
        End If

        Dim peticion = New With {
            .categoriaPlantillaId = categoriaId,
            .nombre = txtNombrePlantilla.Text.Trim(),
            .asunto = txtAsunto.Text.Trim(),
            .cuerpoHtml = txtCuerpoCorreo.Text,
            .activa = True
        }

        Dim api As New ConsumoApi()
        Dim json As String = JsonConvert.SerializeObject(peticion)

        Dim plantillaId As Integer
        Dim esEdicion As Boolean = Integer.TryParse(hfPlantillaId.Value, plantillaId)

        Dim respuesta As String

        If esEdicion Then
            respuesta = api.PutEditarPlantillaCorreo(plantillaId, json)
        Else
            respuesta = api.PostPlantillaCorreo(json)
        End If

        If String.IsNullOrWhiteSpace(respuesta) OrElse respuesta.StartsWith("ERROR") Then
            Avisar("No se pudo guardar: " & DetalleDeError(respuesta), "danger")
            Exit Sub
        End If

        Avisar(If(esEdicion, "Plantilla editada correctamente", "Plantilla agregada correctamente"), "success")

        ' La categoría elegida pasa a ser la que se muestra al regresar.
        CategoriaActiva = categoriaId

        PlantillasCorreo.Refrescar()

        VolverAlListado()
    End Sub

    Private Sub EliminarPlantilla(plantillaId As Integer)

        Dim api As New ConsumoApi()
        Dim respuesta As String = api.DeletePlantillaCorreo(plantillaId)

        If String.IsNullOrWhiteSpace(respuesta) OrElse respuesta.StartsWith("ERROR") Then
            Avisar("No se pudo eliminar: " & DetalleDeError(respuesta), "danger")
            Exit Sub
        End If

        Avisar("Plantilla eliminada correctamente", "success")

        PlantillasCorreo.Refrescar()

        CargarCategorias()
        CargarPlantillas()
    End Sub

#End Region

#Region "Utilerias"

    ''' <summary>
    ''' Saca el motivo de un "ERROR: ..." sin dar por hecho el largo del prefijo.
    ''' </summary>
    Private Function DetalleDeError(respuesta As String) As String

        If String.IsNullOrWhiteSpace(respuesta) Then Return "sin respuesta del servidor"

        Dim separador As Integer = respuesta.IndexOf(":"c)

        If separador < 0 Then Return respuesta.Trim()

        Return respuesta.Substring(separador + 1).Trim()
    End Function

    Private Sub Avisar(mensaje As String, tipo As String)
        lblAviso.Text = mensaje
        pnlAviso.CssClass = "alert alert-" & tipo
        pnlAviso.Visible = True
    End Sub

#End Region

End Class
