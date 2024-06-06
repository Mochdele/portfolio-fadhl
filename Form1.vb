Imports System.Data.SqlClient 'wajib mugholazhoh
Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Call koneksi() 'konek ke database
        da = New SqlDataAdapter("select * from TBLBarang", conn) 'minta data dari tabel tertentu
        ds = New DataSet
        da.Fill(ds)
        dgv.DataSource = ds.Tables(0)
        dgv.ReadOnly = True

        'format ribuan
        dgv.Columns(3).DefaultCellStyle.Format = "#,0"

        'angka rata kanan
        dgv.Columns(3).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight


        'jumlah record dlm dgv
        TextBox8.Text = dgv.RowCount - 1 'krn baris akhir tdk ada datanya

        'total stok...?
        Call hitungstok()

        'warna dgv
        dgv.RowsDefaultCellStyle.BackColor = Color.AliceBlue

        dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.AntiqueWhite

    End Sub

    Sub hitungstok()
        Dim x As Integer
        Dim y As Integer

        For baris As Integer = 0 To dgv.RowCount - 1
            x = x + dgv.Rows(baris).Cells("STOK").Value
            y = y + dgv.Rows(baris).Cells("HARGA_JUAL").Value
        Next
        TextBox9.Text = x
        TextBox10.Text = y
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.CenterToScreen() 'ketengahkan

        Call koneksi()
        cmd = New SqlCommand("select * from TBLBarang", conn)
        dr = cmd.ExecuteReader
        Do While dr.Read
            ListBox1.Items.Add(dr(0)) 'kode
            ComboBox1.Items.Add(dr("NAMA_BARANG"))
        Loop



    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged

        Call koneksi()
        cmd = New SqlCommand("select * from TBLBarang where KODE_BARANG='" & ListBox1.Text & "'", conn)
        dr = cmd.ExecuteReader
        dr.Read()
        If dr.HasRows Then
            TextBox1.Text = dr(0)
            TextBox2.Text = dr(1)
            TextBox3.Text = dr(2)
            TextBox4.Text = dr(3)
            TextBox5.Text = dr(4)
            TextBox6.Text = dr("stok")
        End If
    End Sub

    Private Sub TextBox7_TextChanged(sender As Object, e As EventArgs) Handles TextBox7.TextChanged
        Call koneksi()
        da = New SqlDataAdapter("select * from TBLBarang where NAMA_BARANG like '%" & TextBox7.Text & "%'", conn)
        ds = New DataSet
        da.Fill(ds)
        dgv.DataSource = ds.Tables(0)
        dgv.ReadOnly = True
    End Sub

    'hapus data dlm dgv
    Private Sub dgv_KeyDown(sender As Object, e As KeyEventArgs) Handles dgv.KeyDown

        On Error Resume Next
        If e.KeyCode = Keys.Escape Or e.KeyCode = Keys.Delete Then
            dgv.Rows.Remove(dgv.CurrentRow)
            Call hitungstok()
        End If
    End Sub

    Private Sub dgv_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgv.CellMouseClick

        On Error Resume Next
        TextBox1.Text = dgv.Rows(e.RowIndex).Cells(0).Value
        TextBox2.Text = dgv.Rows(e.RowIndex).Cells(1).Value
        TextBox3.Text = dgv.Rows(e.RowIndex).Cells(2).Value
        TextBox4.Text = dgv.Rows(e.RowIndex).Cells(3).Value
        TextBox5.Text = dgv.Rows(e.RowIndex).Cells(4).Value
        TextBox6.Text = dgv.Rows(e.RowIndex).Cells(5).Value

    End Sub

    'HANYA ANGKA YG BOLEH DIENTRI
    Private Sub TextBox4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox4.KeyPress

        If Not ((e.KeyChar >= "0" And e.KeyChar <= "9") Or e.KeyChar = vbBack) Then
            e.Handled = True
        End If
    End Sub

    'hanya abjad yg boleh dientri
    Private Sub TextBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TextBox3.KeyPress
        If ((e.KeyChar >= "0" And e.KeyChar <= "9") And e.KeyChar <> vbBack) Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

    End Sub

    'FOCUS KURSOR
    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then
            TextBox7.Focus()
        End If
    End Sub

    Private Sub dgv_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgv.CellContentClick

    End Sub
End Class
