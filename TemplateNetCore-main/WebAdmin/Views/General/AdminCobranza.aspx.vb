Public Class AdminCobranza
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    ''' <summary>
    ''' Abre los recibos de la factura en la que se dio clic. La vista es
    ''' estática todavía: solo se arman los números de recibo a partir de la
    ''' factura para que el encabezado y la tabla no se contradigan.
    ''' </summary>
    Protected Sub lnkRecibos_Command(sender As Object, e As CommandEventArgs)

        If e.CommandName <> "Recibos" Then Exit Sub

        Dim factura As String = Convert.ToString(e.CommandArgument)

        litFactura.Text = factura

        litRecibo1.Text = factura.Replace("FAC", "REC") & "-01"
        litRecibo2.Text = factura.Replace("FAC", "REC") & "-02"

        pnlListado.Visible = False
        pnlRecibos.Visible = True
    End Sub

    Protected Sub lnkVolver_Click(sender As Object, e As EventArgs)
        pnlRecibos.Visible = False
        pnlListado.Visible = True
    End Sub

    ''' <summary>
    ''' Abre el detalle del recibo. Sigue siendo estático: lo único que cambia es
    ''' la clave del encabezado, para que corresponda con el renglón elegido.
    ''' </summary>
    Protected Sub lnkVerRecibo_Command(sender As Object, e As CommandEventArgs)

        If e.CommandName <> "Detalle" Then Exit Sub

        Dim renglon As String = Convert.ToString(e.CommandArgument)

        Dim clave As String = If(renglon = "2", litRecibo2.Text, litRecibo1.Text)

        litReciboClave.Text = clave

        ' El diseño muestra en "Numero recibo" una forma corta, no la clave larga.
        litReciboCorto.Text = "REC-" & renglon.PadLeft(2, "0"c)

        pnlRecibos.Visible = False
        pnlDetalleRecibo.Visible = True
    End Sub

    Protected Sub lnkVolverRecibos_Click(sender As Object, e As EventArgs)
        pnlDetalleRecibo.Visible = False
        pnlRecibos.Visible = True
    End Sub

    ''' <summary>
    ''' Abre las facturas de la venta en la que se dio clic. Estático todavía:
    ''' solo se ajustan la venta del encabezado y el número de factura.
    ''' </summary>
    Protected Sub lnkFacturas_Command(sender As Object, e As CommandEventArgs)

        If e.CommandName <> "Facturas" Then Exit Sub

        Dim venta As String = Convert.ToString(e.CommandArgument)

        litVenta.Text = venta
        litFacturaNumero.Text = "FAC-2025-" & venta.Substring(venta.Length - 3)

        pnlListado.Visible = False
        pnlFacturas.Visible = True
    End Sub

    Protected Sub lnkVolverDeFacturas_Click(sender As Object, e As EventArgs)
        pnlFacturas.Visible = False
        pnlListado.Visible = True
    End Sub

    ''' <summary>
    ''' Comisiones de la factura que se está viendo en recibos.
    ''' </summary>
    Protected Sub lnkComisiones_Click(sender As Object, e As EventArgs)

        litFacturaComisiones.Text = litFactura.Text
        litComisionRecibo.Text = litRecibo1.Text

        pnlRecibos.Visible = False
        pnlComisiones.Visible = True
    End Sub

    Protected Sub lnkVolverARecibos_Click(sender As Object, e As EventArgs)
        pnlComisiones.Visible = False
        pnlRecibos.Visible = True
    End Sub

    ''' <summary>
    ''' Detalle de la factura. Estático todavía: solo se ajustan la clave, la
    ''' venta de origen y los números de recibo, para que no se contradigan.
    ''' </summary>
    Protected Sub lnkVerFactura_Click(sender As Object, e As EventArgs)

        Dim factura As String = litFacturaNumero.Text

        litDetFactura.Text = factura
        litDetNumero.Text = factura
        litDetVenta.Text = litVenta.Text
        litDetRecibo1.Text = factura & "-01"
        litDetRecibo2.Text = factura & "-02"

        pnlFacturas.Visible = False
        pnlDetalleFactura.Visible = True
    End Sub

    Protected Sub lnkDetVolver_Click(sender As Object, e As EventArgs)
        pnlDetalleFactura.Visible = False
        pnlListado.Visible = True
    End Sub

End Class
