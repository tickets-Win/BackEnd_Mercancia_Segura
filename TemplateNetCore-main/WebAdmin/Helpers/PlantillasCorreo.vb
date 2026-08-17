Imports Newtonsoft.Json
Imports WebAdmin.MercanciaSegura.DOM.Modelos

''' <summary>
''' Acceso a las plantillas de correo y sustitución de los campos {{...}}.
''' Es el único lugar donde se consultan: lo usan la pantalla de Plantillas y el
''' control de envío de correo.
''' </summary>
Public Module PlantillasCorreo

    ''' <summary>Campos que se pueden insertar en el cuerpo del correo.</summary>
    Public ReadOnly Campos As String() = {
        "FechaActual", "Nombre", "Nombre Completo", "Título", "RFC",
        "Vendedor", "Ejecutivo", "Correo", "Teléfono", "Póliza", "Certificado"
    }

    ''' <summary>Textos de uso frecuente, para la biblioteca.</summary>
    Public Function Biblioteca() As Dictionary(Of String, String)
        Return New Dictionary(Of String, String) From {
            {"Saludo formal", "Estimado(a) {{Nombre Completo}}:"},
            {"Despedida formal", "Quedamos a sus órdenes para cualquier duda o aclaración."},
            {"Aviso legal", "Este mensaje y sus anexos son confidenciales y de uso exclusivo del destinatario. Si lo recibió por error, favor de notificarlo y eliminarlo."},
            {"Datos de contacto", "Mercancía Segura" & vbCrLf & "Tel. {{Teléfono}}" & vbCrLf & "{{Correo}}"}
        }
    End Function

#Region "Consultas al API"

    ''' <summary>
    ''' Todas las plantillas activas. Se cachean por petición: la pantalla las usa
    ''' varias veces al pintar categorías y tarjetas.
    ''' </summary>
    Public Function Todas() As List(Of PlantillaCorreo)

        Dim contexto = HttpContext.Current

        If contexto IsNot Nothing AndAlso contexto.Items("PlantillasCorreo") IsNot Nothing Then
            Return DirectCast(contexto.Items("PlantillasCorreo"), List(Of PlantillaCorreo))
        End If

        Dim api As New ConsumoApi()
        Dim lista = Deserializar(Of PlantillaCorreo)(api.GetPlantillasCorreo())

        If lista Is Nothing Then lista = New List(Of PlantillaCorreo)

        If contexto IsNot Nothing Then contexto.Items("PlantillasCorreo") = lista

        Return lista
    End Function

    Public Function Categorias() As List(Of CategoriaPlantilla)

        Dim contexto = HttpContext.Current

        If contexto IsNot Nothing AndAlso contexto.Items("CategoriasPlantilla") IsNot Nothing Then
            Return DirectCast(contexto.Items("CategoriasPlantilla"), List(Of CategoriaPlantilla))
        End If

        Dim api As New ConsumoApi()
        Dim lista = Deserializar(Of CategoriaPlantilla)(api.GetCategoriasPlantilla())

        If lista Is Nothing Then lista = New List(Of CategoriaPlantilla)

        If contexto IsNot Nothing Then contexto.Items("CategoriasPlantilla") = lista

        Return lista
    End Function

    Public Function PorCategoria(categoriaId As Integer) As List(Of PlantillaCorreo)
        Return Todas().Where(Function(p) p.CategoriaPlantillaId = categoriaId).ToList()
    End Function

    Public Function PorId(plantillaCorreoId As Integer) As PlantillaCorreo
        Return Todas().FirstOrDefault(Function(p) p.PlantillaCorreoId = plantillaCorreoId)
    End Function

    ''' <summary>Limpia el caché tras guardar o borrar, para que se relea.</summary>
    Public Sub Refrescar()

        Dim contexto = HttpContext.Current

        If contexto Is Nothing Then Exit Sub

        contexto.Items.Remove("PlantillasCorreo")
        contexto.Items.Remove("CategoriasPlantilla")
    End Sub

    Private Function Deserializar(Of T)(json As String) As List(Of T)

        If String.IsNullOrWhiteSpace(json) OrElse
           json = "null" OrElse
           json.StartsWith("ERROR") Then Return Nothing

        Try
            Return JsonConvert.DeserializeObject(Of List(Of T))(json)
        Catch
            Return Nothing
        End Try
    End Function

#End Region

#Region "Campos {{...}}"

    ''' <summary>
    ''' Cambia los {{Campo}} del texto por sus valores. Los campos que no vengan
    ''' en el diccionario se dejan tal cual, para que se note cuáles faltaron en
    ''' lugar de mandar el correo con huecos en blanco.
    ''' </summary>
    Public Function Resolver(texto As String, valores As Dictionary(Of String, String)) As String

        If String.IsNullOrEmpty(texto) OrElse valores Is Nothing Then Return texto

        Dim resultado As String = texto

        For Each par As KeyValuePair(Of String, String) In valores
            If String.IsNullOrEmpty(par.Value) Then Continue For

            resultado = resultado.Replace("{{" & par.Key & "}}", par.Value)
        Next

        Return resultado
    End Function

    ''' <summary>Campos que quedaron sin resolver, para poder avisar antes de enviar.</summary>
    Public Function CamposSinResolver(texto As String) As List(Of String)

        Dim pendientes As New List(Of String)

        If String.IsNullOrEmpty(texto) Then Return pendientes

        For Each m As Text.RegularExpressions.Match In
            Text.RegularExpressions.Regex.Matches(texto, "\{\{([^}]+)\}\}")

            Dim campo As String = m.Groups(1).Value.Trim()

            If Not pendientes.Contains(campo) Then pendientes.Add(campo)
        Next

        Return pendientes
    End Function

#End Region

End Module
