
Imports System.Data.SqlClient 'namespace = ruang kerja database sqlserver
Module Module1
    Public conn As SqlConnection 'var untuk koneksi ke database

    Public da As SqlDataAdapter 'var ---tampilkan data ke DGV
    Public ds As DataSet 'val ------- tampilkan data ke dgv

    Public cmd As SqlCommand 'var --- tampilkan data ke textbox, listbox, combobox, dll
    Public dr As SqlDataReader 'var --- tampilkan data ke textbox, listbox, combobox, dll

    'data source= nama server
    'initial catalog= nama database + integrated security = true

    Public Sub koneksi()
        conn = New SqlConnection("data source=PADEL;initial catalog=DBTIPBO;integrated security=true")

        conn.Open()

    End Sub
End Module
