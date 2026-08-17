Imports WebAdmin.MercanciaSegura.DOM.Modelos

Public Class AdminPlantillas
    Inherits System.Web.UI.Page

    ''' <summary>
    ''' Categoría abierta. Se guarda en el ViewState para que sobreviva a los
    ''' postbacks al cambiar de una a otra.
    ''' </summary>
    Private Property CategoriaActiva As String
        Get
            Dim valor As String = TryCast(ViewState("CategoriaActiva"), String)

            If String.IsNullOrEmpty(valor) Then Return PlantillasCorreo.Categorias(0)

            Return valor
        End Get
        Set(value As String)
            ViewState("CategoriaActiva") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            CargarCategorias()
            CargarPlantillas()
        End If

    End Sub

    Protected Sub lnkNuevaPlantilla_Click(sender As Object, e As EventArgs)
        LimpiarFormulario()
        AbrirFormulario("Nueva Plantilla")
    End Sub

    Protected Sub lnkCancelar_Click(sender As Object, e As EventArgs)
        VolverAlListado()
    End Sub

    Private Sub AbrirFormulario(titulo As String)
        lblTituloFormulario.Text = titulo

        ' El combo y la lista de campos se llenan al abrir: en el alta el
        ' formulario no existe todavía cuando corre el Page_Load inicial.
        ddlCategoriaPlantilla.DataSource = PlantillasCorreo.Categorias
        ddlCategoriaPlantilla.DataBind()
        ddlCategoriaPlantilla.SelectedValue = CategoriaActiva

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
        txtNombrePlantilla.Text = String.Empty
        txtAsunto.Text = String.Empty
        txtCuerpoCorreo.Text = String.Empty
    End Sub

    Private Sub CargarCategorias()
        rptCategorias.DataSource = PlantillasCorreo.Categorias
        rptCategorias.DataBind()
    End Sub

    Private Sub CargarPlantillas()
        Dim lista As List(Of PlantillaCorreo) = PlantillasCorreo.PorCategoria(CategoriaActiva)

        rptPlantillas.DataSource = lista
        rptPlantillas.DataBind()

        ' El repeater no pinta nada cuando la lista viene vacía; el aviso lo
        ' sustituye para que la columna no se vea rota.
        lblSinPlantillas.Visible = (lista.Count = 0)
    End Sub

    Protected Sub rptCategorias_ItemCommand(source As Object, e As RepeaterCommandEventArgs)

        If e.CommandName <> "Seleccionar" Then Exit Sub

        CategoriaActiva = Convert.ToString(e.CommandArgument)

        ' Las categorías se vuelven a enlazar para que la píldora activa cambie
        ' de lugar.
        CargarCategorias()
        CargarPlantillas()
    End Sub

    ''' <summary>
    ''' Marca en azul oscuro la categoría abierta.
    ''' </summary>
    Protected Function ClaseCategoria(valor As Object) As String
        Dim nombre As String = Convert.ToString(valor)

        If nombre = CategoriaActiva Then Return "plt-categoria plt-categoria-activa"

        Return "plt-categoria"
    End Function

End Class
