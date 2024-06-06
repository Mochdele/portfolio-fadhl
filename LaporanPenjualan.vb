Public Class LaporanPenjualan
    Private Sub LaporanPenjualan_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        crv.SelectionFormula = "totext({tblpenjualan.tanggal})=" & dtp1.Text & "'"

        crv.ReportSource = "( nama project crsytal report).rpt"
        crv.RefreshReport()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        crv.SelectionFormula = "totext({tblpenjualan.tanggal})=" & dtp2.Text & "'"

        crv.ReportSource = "( nama project crsytal report).rpt"
        crv.RefreshReport()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        crv.SelectionFormula = "month({tblpenjualan.tanggal}) = (" & Month(dtp4.Text) & ")year({tblpenjualan.tanggal}) = (" & Year(dtp4.Text) & ")"

        crv.ReportSource = "( nama project crsytal report).rpt" 'bulanan
        crv.RefreshReport()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        crv.SelectionFormula = "year({tblpenjualan.tanggal}) = (" & Year(dtp4.Text) & ")"

        crv.ReportSource = "( nama project crsytal report).rpt" 'bulanan
        crv.RefreshReport()
    End Sub

    Private Sub GroupBox3_Enter(sender As Object, e As EventArgs) Handles GroupBox3.Enter

    End Sub

    Private Sub GroupBox4_Enter(sender As Object, e As EventArgs) Handles GroupBox4.Enter

    End Sub

    Private Sub DateTimePicker4_ValueChanged(sender As Object, e As EventArgs) Handles dtp3.ValueChanged

    End Sub

    Private Sub dtp1_ValueChanged(sender As Object, e As EventArgs) Handles dtp1.ValueChanged

    End Sub
End Class