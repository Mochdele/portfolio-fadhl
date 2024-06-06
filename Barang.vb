Imports System.Data.SqlClient

Public Class Barang

    Sub kosongkan()
        'TextBox1.Text = ""
        TextBox1.Clear()
        TextBox2.Clear()
        ComboBox1.Text = ""
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox5.Clear()
        TextBox6.Clear()
        TextBox1.Focus()
    End Sub

    Sub databaru()
        TextBox2.Clear()
        ComboBox1.Text = ""
        TextBox3.Clear()
        TextBox4.Clear()
        TextBox5.Clear()
        TextBox6.Clear()
        TextBox2.Focus()
    End Sub

    Sub ketemu()
        TextBox2.Text = dr("NAMA_BARANG") 'dr(1)
        ComboBox1.Text = dr("SATUAN") 'dr(2)
        TextBox3.Text = dr("HARGA_BELI")
        TextBox4.Text = dr("HARGA_JUAL")
        TextBox5.Text = dr("STOK")
        TextBox2.Focus()
    End Sub

    Sub tampilgrid()
        Call koneksi()
        da = New SqlDataAdapter("select * from TBLBarang", conn)
        ds = New DataSet
        da.Fill(ds)
        dgv.DataSource = ds.Tables(0)
        dgv.ReadOnly = True
    End Sub

    Sub tampilsatuan()
        Call koneksi()
        cmd = New SqlCommand("select distinct satuan from TBLBarang", conn)
        dr = cmd.ExecuteReader
        Do While dr.Read
            ComboBox1.Items.Add(dr(0))
        Loop
    End Sub

    Sub carikode()
        Call koneksi()
        cmd = New SqlCommand("select * from TBLBarang where KODE_BARANG='" & TextBox1.Text & "'", conn)
        dr = cmd.ExecuteReader
        dr.Read()
    End Sub

    Private Sub Barang_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.CenterToScreen() 'ketengahkan
        Call tampilgrid()
        Call tampilsatuan()
        Call kosongkan()


    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown

        If e.KeyCode = Keys.Enter Then
            Call carikode()
            If dr.HasRows Then 'jika adata ada
                Call ketemu()
            Else 'jika data tidak ada
                Call databaru()
            End If
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            If TextBox1.Text = "" Then
                MsgBox("Kode harus diisi")
                Exit Sub
            End If

            Call carikode()
            If Not dr.HasRows Then
                Call koneksi()
                Dim simpan As String = "insert into TBLBarang values ('" & TextBox1.Text & "','" & TextBox2.Text & "','" & ComboBox1.Text & "','" & TextBox3.Text & "','" & TextBox4.Text & "','" & TextBox5.Text & "')"

                cmd = New SqlCommand(simpan, conn)
                cmd.ExecuteNonQuery()
                Call kosongkan()
                Call tampilgrid()
                Call tampilsatuan()
            End If
        Catch ex As Exception
            MsgBox(ex.Message) 'tampilkan salahnya apa....
        End Try


    End Sub


    Private Sub dgv_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgv.CellMouseClick

        On Error Resume Next
        'textbox1 diisi kode diambil dari dgv
        TextBox1.Text = dgv.Rows(e.RowIndex).Cells(0).Value
        Call carikode()
        If dr.HasRows Then
            Call ketemu()
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Try
            If TextBox1.Text = "" Then
                MsgBox("Kode harus diisi")
                Exit Sub
            End If

            Call carikode()
            If dr.HasRows Then 'JIKA ADANYA ADA
                Call koneksi()
                Dim edit As String = "update TBLBarang set NAMA_BARANG='" & TextBox2.Text & "',SATUAN='" & ComboBox1.Text & "',HARGA_BELI='" & TextBox3.Text & "',HARGA_JUAL='" & TextBox4.Text & "',STOK='" & TextBox5.Text & "' where KODE_BARANG='" & TextBox1.Text & "'"

                cmd = New SqlCommand(edit, conn)
                cmd.ExecuteNonQuery()
                Call kosongkan()
                Call tampilgrid()
                Call tampilsatuan()
            End If
        Catch ex As Exception
            MsgBox(ex.Message) 'tampilkan salahnya apa....
        End Try

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        'jika kode masih kosong...?
        'jika kode tdk ada di database...?
        'jika kode sdh diisi dan kode itu ada maka ...?

        If TextBox1.Text = "" Then 'jika kode kosong
            MsgBox("Kode harus diisi")
            Exit Sub
        End If

        Call carikode()
        If Not dr.HasRows Then 'jika kode tidak ada di tabel
            MsgBox("Kode tidak terdaftar")
            Exit Sub
        End If

        'jika kode sdh diisi dan kdoenya ada di tabel....
        If MessageBox.Show("yakin akan dihapus...?", "Perhatian.....", MessageBoxButtons.YesNo) = vbYes Then '
            Call koneksi()
            Dim hapus As String = "delete from TBLBarang where KODE_BARANG='" & TextBox1.Text & "'"
            cmd = New SqlCommand(hapus, conn)
            cmd.ExecuteNonQuery()
            Call kosongkan()
            Call tampilgrid()
            Call tampilsatuan()
        Else 'jika NO
            Call kosongkan()
        End If
    End Sub
End Class