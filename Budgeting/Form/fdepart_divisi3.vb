Imports System.Data
Imports System.Data.OleDb
Public Class fdepart_divisi3

    Public dv As DataView
    Public position As Integer
    Public addstat As Boolean

    Private Sub kosongkan()
        tkode.EditValue = ""
        tnama.EditValue = ""
    End Sub

    Private Sub isi()
        tkode.EditValue = dv(position)("kd_divisi").ToString
        tnama.EditValue = dv(position)("nama_divisi").ToString
        tdepart.EditValue = dv(position)("kd_depart").ToString
        '  tunit.EditValue = dv(position)("nama_bu").ToString
    End Sub

    Private Sub simpan()

        Dim cn As OleDbConnection = Nothing
        Dim sqltrans As OleDbTransaction = Nothing
        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            sqltrans = cn.BeginTransaction()

            If addstat Then

                Dim sqlcek As String = String.Format("select nama_divisi from ms_divisi where kd_divisi='{0}'", tkode.EditValue)
                Dim cmdcek As OleDbCommand = New OleDbCommand(sqlcek, cn, sqltrans)
                Dim drdcek As OleDbDataReader = cmdcek.ExecuteReader

                If drdcek.Read Then
                    If Not (drdcek(0).ToString = "") Then
                        sqltrans.Rollback()
                        MsgBox("Kode divisi sudah ada", vbOKOnly + vbInformation)
                        tkode.Focus()
                        Return
                    End If
                End If

                Dim sql As String = String.Format("insert into ms_divisi (kd_divisi,nama_divisi,kd_depart) values('{0}','{1}','{2}')", tkode.EditValue, tnama.EditValue, tdepart.EditValue)
                Using cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                    cmd.ExecuteNonQuery()
                End Using

                Clsmy.InsertToLog(cn, "btdepart", 1, 0, 0, 0, tkode.EditValue, "divisi", sqltrans)

                sqltrans.Commit()

                insertview()
                kosongkan()
                tdepart.Focus()

            Else

                Dim sql As String = String.Format("update ms_divisi set nama_divisi='{0}' where kd_divisi='{1}'", tnama.EditValue, tkode.EditValue)
                Using cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                    cmd.ExecuteNonQuery()
                End Using

                Clsmy.InsertToLog(cn, "btdepart", 0, 1, 0, 0, tkode.EditValue, "diivisi", sqltrans)

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

    Private Sub tdepart_EditValueChanged(sender As Object, e As EventArgs) Handles tdepart.EditValueChanged

        'Dim cn As OleDbConnection = Nothing
        'Try

        '    cn = New OleDbConnection
        '    cn = Clsmy.open_conn

        '    Dim sql As String = String.Format("select b.nama_bu from ms_departemen a inner join ms_bu b on a.idbu=b.idbu where kd_depart='{0}'", tdepart.EditValue)
        '    Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
        '    Dim drd As OleDbDataReader = cmd.ExecuteReader

        '    tunit.EditValue = ""
        '    If drd.Read Then
        '        tunit.EditValue = drd(0).ToString
        '    End If
        '    drd.Close()

        'Catch ex As Exception
        '    MsgBox(ex.ToString)
        'Finally

        '    If Not cn Is Nothing Then
        '        If cn.State = ConnectionState.Open Then
        '            cn.Close()
        '        End If
        '    End If
        'End Try

    End Sub

    Private Sub insertview()
        Dim orow As DataRowView = dv.AddNew
        orow("kd_depart") = tdepart.EditValue
        orow("nama_departemen") = tdepart.Text.Trim
        orow("kd_divisi") = tkode.EditValue
        orow("nama_divisi") = tnama.EditValue
        'orow("nama_bu") = tunit.EditValue
        dv.EndInit()
    End Sub

    Private Sub updateview()
        dv(position)("nama_divisi") = tnama.EditValue
    End Sub

    Private Sub isi_departemen()
        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = "select a.kd_depart,a.nama_departemen from ms_departemen a"

            Dim ds As DataSet = New DataSet()
            ds = Clsmy.GetDataSet(sql, cn)

            tdepart.Properties.DataSource = ds.Tables(0)

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
            tdepart.Focus()
        Else
            tnama.Focus()
        End If
    End Sub

    Private Sub fdepart_divisi2_Load(sender As Object, e As EventArgs) Handles Me.Load

        isi_departemen()

        If addstat = False Then

            tdepart.Properties.ReadOnly = True
            tkode.Properties.ReadOnly = True

            isi()
        End If

    End Sub

    Private Sub btkeluar_Click(sender As Object, e As EventArgs) Handles btkeluar.Click
        Me.Close()
    End Sub

    Private Sub btsimpan_Click(sender As Object, e As EventArgs) Handles btsimpan.Click

        If tdepart.EditValue = "" Then
            MsgBox("Departemen harus diisi", vbOKOnly + vbInformation, "Informasi")
            tunit.Focus()
            Return
        End If

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