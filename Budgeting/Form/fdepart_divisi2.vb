Imports System.Data
Imports System.Data.OleDb
Public Class fdepart_divisi2

    Public dv As DataView
    Public position As Integer
    Public addstat As Boolean

    Private Sub kosongkan()
        tkode.EditValue = ""
        tnama.EditValue = ""
    End Sub

    Private Sub isi()
        tkode.EditValue = dv(position)("kd_depart").ToString
        tnama.EditValue = dv(position)("nama_departemen").ToString
        '  tunit.EditValue = Integer.Parse(dv(position)("idbu").ToString)

    End Sub

    Private Sub simpan()

        Dim cn As OleDbConnection = Nothing
        Dim sqltrans As OleDbTransaction = Nothing
        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            sqltrans = cn.BeginTransaction()

            If addstat Then

                Dim sqlcek As String = String.Format("select nama_departemen from ms_departemen where kd_depart='{0}'", tkode.EditValue)
                Dim cmdcek As OleDbCommand = New OleDbCommand(sqlcek, cn, sqltrans)
                Dim drdcek As OleDbDataReader = cmdcek.ExecuteReader

                If drdcek.Read Then
                    If Not (drdcek(0).ToString = "") Then
                        sqltrans.Rollback()
                        MsgBox("Kode departemen sudah ada", vbOKOnly + vbInformation)
                        tkode.Focus()
                        Return
                    End If
                End If

                Dim sql As String = String.Format("insert into ms_departemen (kd_depart,nama_departemen) values('{0}','{1}')", tkode.EditValue, tnama.EditValue)
                Using cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                    cmd.ExecuteNonQuery()
                End Using

                Clsmy.InsertToLog(cn, "btdepart", 1, 0, 0, 0, tkode.EditValue, "departemen", sqltrans)

                sqltrans.Commit()

                insertview()
                kosongkan()
                tkode.Focus()

            Else

                Dim sql As String = String.Format("update ms_departemen set nama_departemen='{0}' where kd_depart='{1}'", tnama.EditValue, tkode.EditValue)
                Using cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                    cmd.ExecuteNonQuery()
                End Using

                Clsmy.InsertToLog(cn, "btdepart", 0, 1, 0, 0, tkode.EditValue, "departemen", sqltrans)

                sqltrans.Commit()

                updateview()
                MsgBox("Data dirubah", vbOKOnly + vbInformation, "Informasi")
                Me.Close()

            End If

        Catch ex As Exception
            sqltrans.Rollback()
            close_wait()
            MsgBox(ex.ToString)
        Finally

            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If
        End Try


    End Sub

    Private Sub insertview()
        Dim orow As DataRowView = dv.AddNew
        ' orow("idbu") = tunit.EditValue
        ' orow("nama_bu") = tunit.Text.Trim
        orow("kd_depart") = tkode.EditValue
        orow("nama_departemen") = tnama.EditValue
        dv.EndInit()
    End Sub

    Private Sub updateview()
        dv(position)("nama_departemen") = tnama.EditValue
    End Sub

    Private Sub isi_unitusaha()
        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = String.Format("select * from ms_bu where idbu in ({0})", idbu_ku)
            Dim ds As DataSet = New DataSet()
            ds = Clsmy.GetDataSet(sql, cn)


            tunit.Properties.DataSource = ds.Tables(0)

            tunit.EditValue = Integer.Parse(idbu_defaultku)

        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally

            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If
        End Try

    End Sub

    Private Sub fdepart_divisi2_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        If addstat Then
            tkode.Focus()
        Else
            tnama.Focus()
        End If
    End Sub

    Private Sub fdepart_divisi2_Load(sender As Object, e As EventArgs) Handles Me.Load

        '   isi_unitusaha()

        If addstat = False Then

            isi()

            '  tunit.Properties.ReadOnly = True
            tkode.Properties.ReadOnly = True


        End If

    End Sub

    Private Sub btkeluar_Click(sender As Object, e As EventArgs) Handles btkeluar.Click
        Me.Close()
    End Sub

    Private Sub btsimpan_Click(sender As Object, e As EventArgs) Handles btsimpan.Click

        'If Not IsNumeric(tunit.EditValue) Then
        '    MsgBox("Unit usaha/perusahaan harus diisi", vbOKOnly + vbInformation, "Informasi")
        '    tunit.Focus()
        '    Return
        'End If

        'If tunit.EditValue = 0 Then
        '    MsgBox("Unit usaha/perusahaan harus diisi", vbOKOnly + vbInformation, "Informasi")
        '    tunit.Focus()
        '    Return
        'End If

        If tkode.EditValue = "" Then
            MsgBox("Kode harus diisi", vbOKOnly + vbInformation, "Informasi")
            tkode.Focus()
            Return
        End If

        If tnama.EditValue = "" Then
            MsgBox("Nama harus diisi", vbOKOnly + vbInformation, "Informasi")
            tnama.Focus()
            Return
        End If

        simpan()

    End Sub

End Class