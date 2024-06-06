Imports System.Data.SqlClient

Public Class Penjualan
    Sub faktis()
        Call koneksi()
        cmd = New SqlCommand("select faktur from tblpenjualan order by faktur desc", conn)
        dr = cmd.ExecuteReader
        dr.Read()
        If Not dr.HasRows Then
            Lfaktur.Text = Format(Today, "yyMMdd") + "0001"
        Else
            If Microsoft.VisualBasic.Left(dr.Item("faktur"), 6) = Format(Today, "yyMMdd") Then
                Lfaktur.Text = dr.Item("faktur") + 1
            Else
                Lfaktur.Text = Format(Today, "yyMMdd") + "0001"
            End If
        End If
    End Sub

    Sub tampilcustomer()
        Call koneksi()
        cmd = New SqlCommand("Select * from tblcustomer", conn)
        dr = cmd.ExecuteReader
        Do While dr.Read
            ComboBox1.Items.Add(dr("kode_customer"))
        Loop
    End Sub

    Sub hitungbarang()
        Dim hitung As Integer = 0
        For baris As Integer = 0 To dgv.RowCount - 1
            hitung = hitung + dgv.Rows(baris).Cells(3).Value
        Next
    End Sub

    Sub hitungharga()
        Dim hitung As Integer = 0
        For baris As Integer = 0 To dgv.RowCount - 1
            hitung = hitung + dgv.Rows(baris).Cells(4).Value
        Next
        Ltotalharga.Text = hitung

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        Call koneksi()
        cmd = New SqlCommand("select * from tblcustomer where kode_customer='" & ComboBox1.Text & "'", conn)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            Lnamacustomer.Text = dr("nama_customer")
        End If
    End Sub

    Private Sub Penjualan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.CenterToScreen()
        Call bersihkan()
        Call faktis()
        Ltanggal.Text = Format(Today, "dd MMMM yyyy")
        Call tampilcustomer()

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Me.Close()
    End Sub

    Private Sub dgv_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles dgv.CellEndEdit
        If e.ColumnIndex = 0 Then
            dgv.Rows(e.RowIndex).Cells(0).Value = UCase(dgv.Rows(e.RowIndex).Cells(0).Value)
            For barisatas As Integer = 0 To dgv.RowCount - 1
                For barisbawah As Integer = barisatas + 1 To dgv.RowCount - 1
                    If dgv.Rows(barisbawah).Cells("kode").Value = dgv.Rows(barisatas).Cells("kode").Value Then
                        dgv.Rows(barisatas).Cells("jumlah").Value = dgv.Rows(barisatas).Cells("jumlah").Value + 1
                        dgv.Rows(barisatas).Cells("subtotal").Value = dgv.Rows(barisatas).Cells("Jumlah").Value * dgv.Rows(barisatas).Cells("Harga").Value

                        dgv.Rows.Remove(dgv.CurrentRow)
                        SendKeys.Send("{down}")
                        Call hitungharga()
                        Exit Sub
                    End If
                Next
            Next

            Call koneksi()
            cmd = New SqlCommand("select * from tblbarang where left(kode_barang, 4)= '" & dgv.Rows(e.RowIndex).Cells(0).Value & "'", conn)
            dr = cmd.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                dgv.Rows(e.RowIndex).Cells(1).Value = dr.Item("nama_barang")

                dgv.Rows(e.RowIndex).Cells(2).Value = FormatNumber(dr("harga_jual"), 0)
                dgv.Columns(2).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight


                dgv.Rows(e.RowIndex).Cells(3).Value = 1
                dgv.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight


                dgv.Rows(e.RowIndex).Cells(4).Value = dgv.Rows(e.RowIndex).Cells(2).Value * dgv.Rows(e.RowIndex).Cells(3).Value
                dgv.Rows(e.RowIndex).Cells("subtotal").Value = FormatNumber(dgv.Rows(e.RowIndex).Cells("subtotal").Value, 0)
                dgv.Columns("subtotal").DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight


            Else
                MsgBox("Kode barang tidak ditemukan")
                SendKeys.Send("{up}")
                dgv.Rows(e.RowIndex).Cells(0).Value = ""
            End If
        End If


        If e.ColumnIndex = 3 Then
            koneksi()
            cmd = New SqlCommand("select * from tblbarang where left(kode_barang,4)= '" & dgv.Rows(e.RowIndex).Cells("kode").Value & "'", conn)
            dr = cmd.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                If dgv.Rows(e.RowIndex).Cells("jumlah").Value > dr("stok") Then
                    MsgBox("stok hanya ada " & dr("stok") & "")
                    dgv.Rows(e.RowIndex).Cells("jumlah").Value = dr("stok")
                End If
            End If
            dgv.Rows(e.RowIndex).Cells(4).Value = dgv.Rows(e.RowIndex).Cells(2).Value * dgv.Rows(e.RowIndex).Cells(3).Value
        End If
        Call hitungharga()
        Call hitungbarang()
    End Sub

    Private Sub dgv_KeyDown(sender As Object, e As KeyEventArgs) Handles dgv.KeyDown
        If e.KeyCode = Keys.Escape Or e.KeyCode = Keys.Delete Then
            dgv.Rows.Remove(dgv.CurrentRow)
            Call hitungharga()
            Call hitungbarang()
        End If
        If e.KeyCode = Keys.Enter Then
            Tdibayar.Focus()
        End If
    End Sub
    Private Sub Tdibayar_KeyDown(sender As Object, e As KeyEventArgs) Handles Tdibayar.KeyDown
        If e.KeyCode = Keys.Enter Then
            If Val(Tdibayar.Text) < Val(Ltotalharga.Text) Then
                MsgBox("Pembayaran Kurang")
                Exit Sub
            ElseIf Val(Tdibayar.Text) = Val(Ltotalharga.Text) Then
                Lkembali.Text = 0
                Button1.Focus()
            ElseIf Val(Tdibayar.Text) > Val(Ltotalharga.Text) Then
                Lkembali.Text = Val(Tdibayar.Text) - Val(Ltotalharga.Text)
                Button1.Focus()
            End If

        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If ComboBox1.Text = "" Or Tdibayar.Text = "" Or dgv.RowCount - 1 = 0 Then
            MsgBox("Transaksi belum lengkap")
            Exit Sub
        End If

        Call koneksi()
        Dim simpanpenjualan As String = "insert into tblpenjualan values('" & Lfaktur.Text & "','" & Format(DateValue(Ltanggal.Text), "MM/dd/yyyy") & " ','" & ComboBox1.Text & "','" & Ltotalharga.Text & "','" & Tdibayar.Text & "','" & Lkembali.Text & "','USR01')"
        cmd = New SqlCommand(simpanpenjualan, conn)
        cmd.ExecuteNonQuery()

        For baris As Integer = 0 To dgv.RowCount - 2
            Call koneksi()
            Dim simpandetail As String = "insert into tbldetailjual values ('" & Lfaktur.Text & "', '" & dgv.Rows(baris).Cells(0).Value & "', '" & Val(Microsoft.VisualBasic.Str(dgv.Rows(baris).Cells("harga").Value)) & "', '" & Val(Microsoft.VisualBasic.Str(dgv.Rows(baris).Cells("jumlah").Value)) & "', '" & Val(Microsoft.VisualBasic.Str(dgv.Rows(baris).Cells("subTotal").Value)) & "')"
            cmd = New SqlCommand(simpandetail, conn)
            cmd.ExecuteNonQuery()

            Call koneksi()
            cmd = New SqlCommand("select * from tblbarang where kode_barang = '" & dgv.Rows(baris).Cells(0).Value & "'", conn)
            dr = cmd.ExecuteReader
            dr.Read()
            If dr.HasRows Then
                Call koneksi()
                Dim kurangistok As String = "update tblbarang set stok = '" & dr.Item("stok") - dgv.Rows(baris).Cells(3).Value & "' where kode_barang = '" & dgv.Rows(baris).Cells(0).Value & "'"
                cmd = New SqlCommand(kurangistok, conn)
                cmd.ExecuteNonQuery()
            End If
        Next
        If MessageBox.Show("Apakah yakin data akan dicetak?", "", MessageBoxButtons.YesNo) = vbYes Then
            cetak.Show()
            cetak.crv.SelectionFormula = "{tblpenjualan.faktur}" & Lfaktur.Text & "'"
            cetak.crv.ReportSource = "faktur.rpt"
            cetak.crv.RefreshReport()
        End If
        Call bersihkan()
        Call faktis()
    End Sub
    Sub bersihkan()
        ComboBox1.Text = ""
        Ltotalharga.Text = ""
        Tdibayar.Text = ""
        Lkembali.Text = ""
        dgv.Rows.Clear()
    End Sub
End Class