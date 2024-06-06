Imports System.Data.SqlClient
Public Class Login

    Dim hitung As Integer
    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        Call koneksi()
        cmd = New SqlCommand("select * from TBLUser where Nama_User='" & UsernameTextBox.Text & "' and Pwd_User='" & PasswordTextBox.Text & "'", conn)
        dr = cmd.ExecuteReader
        dr.Read()
        ' If dr.HasRows Then
        'MsgBox("password salah coy !")
        'PasswordTextBox.Focus()
        'Exit Sub
        'End If
        If Not dr.HasRows Then
            MsgBox("GAGAL LOGIN SIR !!")
            UsernameTextBox.Clear()
            PasswordTextBox.Clear()
            UsernameTextBox.Focus()
        Else
            Me.Visible = False
            MenuUtama.Show()
            MenuUtama.Panel1.Text = dr.Item("Kode_User")
            MenuUtama.Panel2.Text = dr.Item("Nama_User")
            MenuUtama.Panel3.Text = UCase(dr.Item("Status_User"))

            'hak akses
            'visible=false = menu tak terlihat
            'enabled = false = menu terlihat tapi tak dapat di klik
            If MenuUtama.Panel3.Text <> "ADMIN" Then
                MenuUtama.Button1.Enabled = False
            Else
                MenuUtama.Button1.Enabled = True
            End If

            If MenuUtama.Panel3.Text = "USER" Then
                MenuUtama.UserToolStripMenuItem.Enabled = False
                MenuUtama.BarangToolStripMenuItem.Enabled = False
            ElseIf MenuUtama.Panel3.Text = "OPERATOR" Then
                MenuUtama.SupplierToolStripMenuItem.Enabled = False
                MenuUtama.CustomerToolStripMenuItem.Enabled = False
            End If


        End If

        MsgBox("login gagal lur")
        hitung = hitung + 1
        If hitung = 3 Then
            End
        End If


    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        Me.Close()
        End
    End Sub


    Private Sub UsernameTextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles UsernameTextBox.KeyPress
        If e.KeyChar = Chr(13) Then
            PasswordTextBox.Focus()
        End If
    End Sub


    Private Sub PasswordTextBox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles PasswordTextBox.KeyPress
        If e.KeyChar = Chr(13) Then
            OK.Focus()
        End If
    End Sub

    Private Sub UsernameTextBox_TextChanged(sender As Object, e As EventArgs) Handles UsernameTextBox.TextChanged

    End Sub

    Private Sub Login_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.CenterToScreen()

    End Sub
End Class
