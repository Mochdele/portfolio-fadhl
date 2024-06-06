Imports System.Data.SqlClient

Public Class User

    Sub kosongkan()
        'TextBox1.Text = ""
        TextBox1.Clear()
        TextBox2.Clear()
        ComboBox1.Text = ""
        TextBox3.Clear()
        TextBox6.Clear()
        TextBox1.Focus()
    End Sub

    Sub databaru()
        TextBox2.Clear()
        ComboBox1.Text = ""
        TextBox3.Clear()
        TextBox6.Clear()
        TextBox2.Focus()
    End Sub

    Sub ketemu()
        TextBox2.Text = dr("NAMA_User") 'dr(1)
        ComboBox1.Text = dr("status_User") 'dr(2)
        TextBox3.Text = dr("Pwd_User")
        TextBox2.Focus()
    End Sub

    Sub tampilgrid()
        Call koneksi()
        da = New SqlDataAdapter("select * from TBLUser", conn)
        ds = New DataSet
        da.Fill(ds)
        dgv.DataSource = ds.Tables(0)
        dgv.ReadOnly = True
    End Sub

    Sub tampilstatus_User()
        Call koneksi()
        cmd = New SqlCommand("select distinct status_User from TBLUser", conn)
        dr = cmd.ExecuteReader
        Do While dr.Read
            ComboBox1.Items.Add(dr(0))
        Loop
    End Sub

    Sub carikode()
        Call koneksi()
        cmd = New SqlCommand("select * from TBLUser where KODE_User='" & TextBox1.Text & "'", conn)
        dr = cmd.ExecuteReader
        dr.Read()
    End Sub

    Private Sub user_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.CenterToScreen() 'ketengahkan
        Call tampilgrid()
        Call tampilstatus_User()
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
                Dim simpan As String = "insert into TBLUser values ('" & TextBox1.Text & "', '" & TextBox2.Text & "','" & ComboBox1.Text & "','" & TextBox3.Text & "')"

                cmd = New SqlCommand(simpan, conn)
                cmd.ExecuteNonQuery()
                Call kosongkan()
                Call tampilgrid()
                Call tampilstatus_User()
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
                Dim edit As String = "update TBLUser set nama_user='" & TextBox2.Text & "', Status_User='" & ComboBox1.Text & "', Pwd_User='" & TextBox3.Text & "' where Kode_User='" & TextBox1.Text & "'"

                cmd = New SqlCommand(edit, conn)
                cmd.ExecuteNonQuery()
                Call kosongkan()
                Call tampilgrid()
                Call tampilstatus_User()
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
            Dim hapus As String = "delete from TBLuser where KODE_User='" & TextBox1.Text & "'"
            cmd = New SqlCommand(hapus, conn)
            cmd.ExecuteNonQuery()
            Call kosongkan()
            Call tampilgrid()
            Call tampilstatus_User()
        Else 'jika NO
            Call kosongkan()
        End If
    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Call kosongkan()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Me.Close()
    End Sub

    Private Sub TextBox6_TextChanged(sender As Object, e As EventArgs) Handles TextBox6.TextChanged
        Call koneksi()
        da = New SqlDataAdapter("select * from TBLUser where Nama_User like '%" & TextBox6.Text & "%'", conn)
        ds = New DataSet
        da.Fill(ds)
        dgv.DataSource = ds.Tables(0)
        dgv.ReadOnly = True
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged

    End Sub
End Class