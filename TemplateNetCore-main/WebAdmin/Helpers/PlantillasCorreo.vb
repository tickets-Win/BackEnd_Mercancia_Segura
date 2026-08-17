Imports WebAdmin.MercanciaSegura.DOM.Modelos

''' <summary>
''' Catálogo de plantillas de correo y sustitución de los campos {{...}}.
''' Es el único lugar donde viven las plantillas: lo usan la pantalla de
''' Plantillas y la de Envío de correo. Cuando exista el endpoint, se cambia
''' Todas() por la llamada al API y lo demás sigue igual.
''' </summary>
Public Module PlantillasCorreo

    Public ReadOnly Categorias As String() = {
        "Directorio", "Documentos", "Pagos", "Endosos",
        "Siniestros", "Reclamaciones", "General"
    }

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

    Private Const Remitente As String = "Mercancia Segura"

    Public Function Todas() As List(Of PlantillaCorreo)

        Return New List(Of PlantillaCorreo) From {
            New PlantillaCorreo With {
                .PlantillaCorreoId = 1,
                .Categoria = "Directorio",
                .Remitente = Remitente,
                .Nombre = "Primer contacto cliente",
                .Asunto = "Bienvenida a Mercancía Segura",
                .Cuerpo = "Estimado(a) {{Nombre Completo}}," & vbCrLf & vbCrLf &
                          "Agradecemos su interés en nuestro servicio. Atendiendo a su solicitud de información " &
                          "acerca de nuestro sistema Mercancía Segura, nos es grato poder atenderle." & vbCrLf & vbCrLf &
                          "Quedamos a sus órdenes.",
                .Adjuntos = 0
            },
            New PlantillaCorreo With {
                .PlantillaCorreoId = 2,
                .Categoria = "Directorio",
                .Remitente = Remitente,
                .Nombre = "Bienvenida a nuevo cliente",
                .Asunto = "Bienvenido a Mercancía Segura",
                .Cuerpo = "Estimado(a) {{Nombre Completo}}," & vbCrLf & vbCrLf &
                          "Le damos la bienvenida. A partir de hoy su ejecutivo asignado es {{Ejecutivo}}, " &
                          "quien atenderá cualquier requerimiento relacionado con su cuenta." & vbCrLf & vbCrLf &
                          "Quedamos a sus órdenes.",
                .Adjuntos = 1
            },
            New PlantillaCorreo With {
                .PlantillaCorreoId = 3,
                .Categoria = "Documentos",
                .Remitente = Remitente,
                .Nombre = "Envío de póliza",
                .Asunto = "Envío de póliza {{Póliza}}",
                .Cuerpo = "Estimado(a) {{Nombre Completo}}," & vbCrLf & vbCrLf &
                          "Adjuntamos la póliza {{Póliza}} correspondiente a su cobertura contratada." & vbCrLf & vbCrLf &
                          "Quedamos a sus órdenes.",
                .Adjuntos = 2
            },
            New PlantillaCorreo With {
                .PlantillaCorreoId = 4,
                .Categoria = "Documentos",
                .Remitente = Remitente,
                .Nombre = "Solicitud de documentación",
                .Asunto = "Documentación pendiente",
                .Cuerpo = "Estimado(a) {{Nombre Completo}}," & vbCrLf & vbCrLf &
                          "Para continuar con su trámite requerimos la documentación pendiente. " &
                          "En cuanto la recibamos daremos seguimiento." & vbCrLf & vbCrLf &
                          "Quedamos a sus órdenes.",
                .Adjuntos = 0
            },
            New PlantillaCorreo With {
                .PlantillaCorreoId = 5,
                .Categoria = "Pagos",
                .Remitente = Remitente,
                .Nombre = "Recordatorio de pago",
                .Asunto = "Recordatorio de pago",
                .Cuerpo = "Estimado(a) {{Nombre Completo}}," & vbCrLf & vbCrLf &
                          "Le recordamos que tiene un pago próximo a vencer correspondiente a la póliza {{Póliza}}." & vbCrLf & vbCrLf &
                          "Quedamos a sus órdenes.",
                .Adjuntos = 0
            },
            New PlantillaCorreo With {
                .PlantillaCorreoId = 6,
                .Categoria = "Pagos",
                .Remitente = Remitente,
                .Nombre = "Confirmación de pago",
                .Asunto = "Confirmación de pago recibido",
                .Cuerpo = "Estimado(a) {{Nombre Completo}}," & vbCrLf & vbCrLf &
                          "Confirmamos la recepción de su pago. Adjuntamos el comprobante correspondiente." & vbCrLf & vbCrLf &
                          "Quedamos a sus órdenes.",
                .Adjuntos = 1
            },
            New PlantillaCorreo With {
                .PlantillaCorreoId = 7,
                .Categoria = "Endosos",
                .Remitente = Remitente,
                .Nombre = "Notificación de endoso",
                .Asunto = "Endoso aplicado a su póliza {{Póliza}}",
                .Cuerpo = "Estimado(a) {{Nombre Completo}}," & vbCrLf & vbCrLf &
                          "Le informamos que se aplicó un endoso a su póliza {{Póliza}}." & vbCrLf & vbCrLf &
                          "Quedamos a sus órdenes.",
                .Adjuntos = 1
            },
            New PlantillaCorreo With {
                .PlantillaCorreoId = 8,
                .Categoria = "Siniestros",
                .Remitente = Remitente,
                .Nombre = "Aviso de siniestro recibido",
                .Asunto = "Hemos recibido su reporte de siniestro",
                .Cuerpo = "Estimado(a) {{Nombre Completo}}," & vbCrLf & vbCrLf &
                          "Hemos recibido su reporte de siniestro y lo turnamos al área correspondiente." & vbCrLf & vbCrLf &
                          "Quedamos a sus órdenes.",
                .Adjuntos = 0
            },
            New PlantillaCorreo With {
                .PlantillaCorreoId = 9,
                .Categoria = "Siniestros",
                .Remitente = Remitente,
                .Nombre = "Seguimiento de siniestro",
                .Asunto = "Seguimiento a su siniestro",
                .Cuerpo = "Estimado(a) {{Nombre Completo}}," & vbCrLf & vbCrLf &
                          "Le compartimos el avance de su siniestro. Cualquier novedad se la haremos saber." & vbCrLf & vbCrLf &
                          "Quedamos a sus órdenes.",
                .Adjuntos = 0
            },
            New PlantillaCorreo With {
                .PlantillaCorreoId = 10,
                .Categoria = "Reclamaciones",
                .Remitente = Remitente,
                .Nombre = "Acuse de reclamación",
                .Asunto = "Acuse de su reclamación",
                .Cuerpo = "Estimado(a) {{Nombre Completo}}," & vbCrLf & vbCrLf &
                          "Acusamos recibo de su reclamación y la turnamos al área correspondiente." & vbCrLf & vbCrLf &
                          "Quedamos a sus órdenes.",
                .Adjuntos = 0
            },
            New PlantillaCorreo With {
                .PlantillaCorreoId = 11,
                .Categoria = "General",
                .Remitente = Remitente,
                .Nombre = "Aviso general",
                .Asunto = "Información de su interés",
                .Cuerpo = "Estimado(a) {{Nombre Completo}}," & vbCrLf & vbCrLf &
                          "Le compartimos la siguiente información de su interés." & vbCrLf & vbCrLf &
                          "Quedamos a sus órdenes.",
                .Adjuntos = 0
            }
        }

    End Function

    Public Function PorCategoria(categoria As String) As List(Of PlantillaCorreo)
        Return Todas().Where(Function(p) p.Categoria = categoria).ToList()
    End Function

    Public Function PorId(plantillaCorreoId As Integer) As PlantillaCorreo
        Return Todas().FirstOrDefault(Function(p) p.PlantillaCorreoId = plantillaCorreoId)
    End Function

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

End Module
