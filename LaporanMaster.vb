Public Class LaporanMaster
    Private Sub LaporanMaster_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        crv.ReportSource = "Customer.rpt"
        crv.RefreshReport()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        crv.ReportSource = "barang.rpt"
        crv.RefreshReport()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        crv.ReportSource = "user.rpt"
        crv.RefreshReport()
    End Sub
End Class