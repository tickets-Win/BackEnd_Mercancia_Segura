Imports System.Globalization
Imports System.Text.RegularExpressions

''' <summary>
''' Conversiones que no truenan. Convert.ToDecimal y Convert.ToInt32 lanzan
''' FormatException cuando el texto no es un número, y eso en WebForms sale como
''' pantalla amarilla; aquí se devuelve Nothing y la pantalla decide qué avisar.
'''
''' Los campos de importe y porcentaje se muestran formateados por JavaScript
''' ("$1,234.56 MN", "12.500000%"), así que primero se quita todo lo que no sea
''' dígito, punto o signo. El JS formatea con toLocaleString('en-US'), de ahí que
''' se lea con InvariantCulture y no con la del servidor.
''' </summary>
Public NotInheritable Class Convertir

    Private Sub New()
    End Sub


    Private Shared ReadOnly SoloNumero As New Regex("[^0-9.\-]", RegexOptions.Compiled)

    ''' <summary>Decimal o Nothing si el texto no trae un número.</summary>
    Public Shared Function Numero(texto As String) As Decimal?

        If String.IsNullOrWhiteSpace(texto) Then Return Nothing

        Dim limpio As String = SoloNumero.Replace(texto, "")

        ' Un "." o un "-" sueltos pasan el filtro pero no son un número.
        Dim valor As Decimal

        If Not Decimal.TryParse(limpio, NumberStyles.Any, CultureInfo.InvariantCulture, valor) Then
            Return Nothing
        End If

        Return valor
    End Function

    ''' <summary>Decimal con valor de respaldo, para las columnas no nulas.</summary>
    Public Shared Function NumeroO(texto As String, respaldo As Decimal) As Decimal

        Dim valor = Numero(texto)

        If Not valor.HasValue Then Return respaldo

        Return valor.Value
    End Function

    ''' <summary>Entero o Nothing. Sirve igual para TextBox y para SelectedValue.</summary>
    Public Shared Function Entero(texto As String) As Integer?

        Dim valor = Numero(texto)

        If Not valor.HasValue Then Return Nothing

        If valor.Value > Integer.MaxValue OrElse valor.Value < Integer.MinValue Then Return Nothing

        Return CInt(Decimal.Truncate(valor.Value))
    End Function

    ''' <summary>Entero con valor de respaldo.</summary>
    Public Shared Function EnteroO(texto As String, respaldo As Integer) As Integer

        Dim valor = Entero(texto)

        If Not valor.HasValue Then Return respaldo

        Return valor.Value
    End Function

    ''' <summary>
    ''' Fecha o Nothing. Los TextMode="Date" mandan yyyy-MM-dd, que la cultura
    ''' es-MX no lee bien, así que se intenta ese formato antes que el general.
    ''' </summary>
    Public Shared Function Fecha(texto As String) As DateTime?

        If String.IsNullOrWhiteSpace(texto) Then Return Nothing

        Dim valor As DateTime

        If DateTime.TryParseExact(texto.Trim(), "yyyy-MM-dd",
                                  CultureInfo.InvariantCulture,
                                  DateTimeStyles.None, valor) Then Return valor

        If DateTime.TryParse(texto, valor) Then Return valor

        Return Nothing
    End Function

    ''' <summary>True si el combo no tiene una opción valida elegida.</summary>
    Public Shared Function SinElegir(ddl As Web.UI.WebControls.DropDownList) As Boolean

        If ddl Is Nothing Then Return True

        Dim id = Entero(ddl.SelectedValue)

        Return Not id.HasValue OrElse id.Value = 0
    End Function

End Class
