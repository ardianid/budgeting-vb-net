Imports System.Data
Imports System.Data.OleDb
Public Class fverif_cross

    Private bs1 As BindingSource
    Private dvmanager As Data.DataViewManager
    Private dv1 As Data.DataView

    Dim ds_divisi As DataSet
    Private Sub isi_divisi()
        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = "select a.kd_divisi,a.nama_divisi from ms_divisi a"

            Dim a As Integer = 0

            If st_divku <> 1 Then

                Dim kd_divaja As String = ""

                Dim sqlc As String = String.Format("select kd_divisi from ms_usersys5 where namauser='{0}'", userprog)
                Dim cmdc As OleDbCommand = New OleDbCommand(sqlc, cn)
                Dim drdc As OleDbDataReader = cmdc.ExecuteReader

                If drdc.HasRows Then
                    While drdc.Read

                        If a = 0 Then
                            kd_divaja = String.Format("'{0}'", drdc("kd_divisi").ToString)
                        Else
                            kd_divaja = String.Format("{0},'{1}'", kd_divaja, drdc("kd_divisi").ToString)
                        End If

                        a = a + 1
                    End While
                End If
                drdc.Close()

                If a > 0 Then
                    sql = String.Format("{0} where a.kd_divisi in ({1})", sql, kd_divaja)
                End If

            End If

            ds_divisi = New DataSet()
            ds_divisi = Clsmy.GetDataSet(sql, cn)

            If (a > 0) Or st_divku = 1 Then

                Dim row As DataRow = ds_divisi.Tables(0).NewRow
                row("kd_divisi") = "All"
                row("nama_divisi") = "All"
                ds_divisi.Tables(0).Rows.InsertAt(row, 0)

            End If

            tdivisi.Properties.DataSource = ds_divisi.Tables(0)
            tdivisi.ItemIndex = 0

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

    Private Sub opendata()

        Dim sql As String = "select a.nobukti,CONVERT(VARCHAR(10),a.tanggal,103) as tanggal,b.nama_divisi,a.nama_pemohon,a.total_c,a.jml_kas_c,a.jml_realisasi_c,0 as spakai,a.namauser " & _
        "from tr_pengajuan a inner join ms_divisi b on a.kd_divisi=b.kd_divisi " & _
        "where sbatal=0 and cross_b=1 and verif_cros=0"

        'If Not (tdivisi.EditValue = "All") Then
        '    sql = String.Format(" {0} and a.nobukti in (select distinct nobukti from tr_pengajuan3 a3 inner join ms_akun_detail b3 on a3.noid_akund=b3.noid where b3.kd_divisi='{1}')", sql, tdivisi.EditValue)
        'Else

        '    Dim kd_divisi As String = ""
        '    Dim noa As Integer = 0

        '    For i As Integer = 0 To ds_divisi.Tables(0).Rows.Count - 1

        '        If Not (ds_divisi.Tables(0)(i)("kd_divisi").ToString = "All") Then

        '            If noa = 0 Then
        '                kd_divisi = String.Format("'{0}'", ds_divisi.Tables(0)(i)("kd_divisi").ToString)
        '            Else
        '                kd_divisi = String.Format("{0},'{1}'", kd_divisi, ds_divisi.Tables(0)(i)("kd_divisi").ToString)
        '            End If

        '            noa = noa + 1

        '        End If

        '    Next

        '    If kd_divisi <> "" Then
        '        sql = String.Format(" {0} and a.nobukti in (select distinct nobukti from tr_pengajuan3 a3 inner join ms_akun_detail b3 on a3.noid_akund=b3.noid where b3.kd_divisi in ({1}))", sql, kd_divisi)
        '    End If

        'End If

        Dim cn As OleDbConnection = Nothing

        grid1.DataSource = Nothing

        Try

            dv1 = Nothing

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            'If st_alluserku = 2 Then
            '    sql = String.Format("{0} where aju.namauser='{1}'", sql, userprog)
            'ElseIf st_alluserku = 3 Then

            '    Dim namauserlain As String = String.Format("'{0}'", userprog)

            '    Dim sqlc As String = String.Format("select namauser2 from ms_usersys6 where namauser='{0}'", userprog)
            '    Dim cmdc As OleDbCommand = New OleDbCommand(sqlc, cn)
            '    Dim drdc As OleDbDataReader = cmdc.ExecuteReader

            '    If drdc.HasRows Then
            '        While drdc.Read

            '            namauserlain = String.Format("{0},'{1}'", namauserlain, drdc("namauser2").ToString)

            '        End While
            '    End If
            '    drdc.Close()

            '    sql = String.Format("{0} where a.namauser in ({1})", sql, namauserlain)

            'End If

            If st_divku <> 1 Then

                Dim kd_divaja As String = ""
                Dim a As Integer = 0

                Dim sqlc As String = String.Format("select kd_divisi from ms_usersys5 where namauser='{0}'", userprog)
                Dim cmdc As OleDbCommand = New OleDbCommand(sqlc, cn)
                Dim drdc As OleDbDataReader = cmdc.ExecuteReader

                If drdc.HasRows Then
                    While drdc.Read

                        If a = 0 Then
                            kd_divaja = String.Format("'{0}'", drdc("kd_divisi").ToString)
                        Else
                            kd_divaja = String.Format("{0},'{1}'", kd_divaja, drdc("kd_divisi").ToString)
                        End If

                        a = a + 1
                    End While
                End If
                drdc.Close()

                If a > 0 Then
                    sql = String.Format("{0} and a.nobukti in (select distinct nobukti from tr_pengajuan3 a3 inner join ms_akun_detail b3 on a3.noid_akund=b3.noid where b3.kd_divisi in ({1}))", sql, kd_divaja)
                End If

            End If

            sql = String.Format(" {0} order by a.tanggal,a.nobukti asc", sql)

            Dim ds As DataSet = New DataSet()
            ds = Clsmy.GetDataSet(sql, cn)

            dvmanager = New DataViewManager(ds)
            dv1 = dvmanager.CreateDataView(ds.Tables(0))

            bs1 = New BindingSource() With {.DataSource = dv1}

            grid1.DataSource = bs1


        Catch ex As OleDb.OleDbException
            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
        Finally


            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If

        End Try

    End Sub

    Private Sub fverif_cross_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        grid1.Focus()
    End Sub

    Private Sub fverif_cross_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'isi_divisi()
        opendata()

        If st_crossbudget = 0 Then
            MsgBox("Anda tidak berhak untuk akses form ini..", vbExclamation, "Error")
            Me.Close()
        End If

    End Sub

    Private Sub btkeluar_Click(sender As Object, e As EventArgs) Handles btkeluar.Click
        Me.Close()
    End Sub

    Private Sub btproses_Click(sender As Object, e As EventArgs) Handles btproses.Click

        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count <= 0 Then
            Return
        End If

        Dim cn As OleDbConnection = Nothing
        Dim sqltrans As OleDbTransaction = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            sqltrans = cn.BeginTransaction

            Dim sql As String = ""

            For i As Integer = 0 To dv1.Count - 1

                If dv1(i)("spakai") = 1 Then

                    sql = String.Format("update tr_pengajuan set verif_cros=1,nama_verif='{0}' where nobukti='{1}'", userprog, dv1(i)("nobukti").ToString)
                    Using cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                        cmd.ExecuteNonQuery()
                    End Using

                End If

            Next

            sqltrans.Commit()

            MsgBox("Data sudah diverifikasi", vbOKOnly + vbInformation, "Infomasi")
            Me.Close()

        Catch ex As Exception
            sqltrans.Rollback()
            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
        Finally


            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If
        End Try

    End Sub

    Private Sub tdivisi_EditValueChanged(sender As Object, e As EventArgs) Handles tdivisi.EditValueChanged
        opendata()
    End Sub

End Class