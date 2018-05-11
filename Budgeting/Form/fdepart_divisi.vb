Imports System.Data
Imports System.Data.OleDb
Public Class fdepart_divisi

    Private bs1 As BindingSource
    Private dvmanager As Data.DataViewManager
    Private dv1 As Data.DataView

    Private bs2 As BindingSource
    Private dvmanager2 As Data.DataViewManager
    Private dv2 As Data.DataView

    Private Sub open_depart()

        Dim sql As String = "select a.kd_depart,a.nama_departemen from ms_departemen a"
        Dim cn As OleDbConnection = Nothing

        grid1.DataSource = Nothing

        Try

            dv1 = Nothing

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim ds As DataSet
            ds = New DataSet()
            ds = Clsmy.GetDataSet(sql, cn)

            dvmanager = New DataViewManager(ds)
            dv1 = dvmanager.CreateDataView(ds.Tables(0))

            bs1 = New BindingSource
            bs1.DataSource = dv1
            bn1.BindingSource = bs1

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

    Private Sub open_divisi()

        Dim sql As String = "select b.kd_depart,b.nama_departemen,a.kd_divisi,a.nama_divisi from ms_divisi a inner join ms_departemen b on a.kd_depart=b.kd_depart"
        Dim cn As OleDbConnection = Nothing

        grid2.DataSource = Nothing

        Try

            dv2 = Nothing

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim ds As DataSet
            ds = New DataSet()
            ds = Clsmy.GetDataSet(sql, cn)

            dvmanager2 = New DataViewManager(ds)
            dv2 = dvmanager2.CreateDataView(ds.Tables(0))

            bs2 = New BindingSource
            bs2.DataSource = dv2
            bn2.BindingSource = bs2

            grid2.DataSource = bs2


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

    Private Sub cari_depart()

        Dim sql As String = "select a.kd_depart,a.nama_departemen from ms_departemen a"
        Dim cn As OleDbConnection = Nothing

        grid1.DataSource = Nothing

        Try

            dv1 = Nothing

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            If tfind.Text.Trim.Length > 0 Then

                If tcbofind.SelectedIndex = 0 Then
                    sql = String.Format("{0} where a.kd_depart like '%{1}%'", sql, tfind.Text.Trim)
                End If

                If tcbofind.SelectedIndex = 1 Then
                    sql = String.Format("{0} where a.nama_departemen like '%{1}%'", sql, tfind.Text.Trim)
                End If

            End If

            Dim ds As DataSet
            ds = New DataSet()
            ds = Clsmy.GetDataSet(sql, cn)

            dvmanager = New DataViewManager(ds)
            dv1 = dvmanager.CreateDataView(ds.Tables(0))

            bs1 = New BindingSource
            bs1.DataSource = dv1
            bn1.BindingSource = bs1

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

    Private Sub cari_divisi()

        Dim sql As String = "select b.kd_depart,b.nama_departemen,a.kd_divisi,a.nama_divisi from ms_divisi a inner join ms_departemen b on a.kd_depart=b.kd_depart"
        Dim cn As OleDbConnection = Nothing

        grid2.DataSource = Nothing

        Try

            dv2 = Nothing

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            If tfind2.Text.Trim.Length > 0 Then

                If tcbofind2.SelectedIndex = 0 Then
                    sql = String.Format("{0} where a.kd_divisi like '%{1}%'", sql, tfind2.Text.Trim)
                End If

                If tcbofind2.SelectedIndex = 1 Then
                    sql = String.Format("{0} where a.nama_divisi like '%{1}%'", sql, tfind2.Text.Trim)
                End If

                If tcbofind2.SelectedIndex = 2 Then
                    sql = String.Format("{0} where a.nama_departemen like '%{1}%'", sql, tfind2.Text.Trim)
                End If

            End If

            Dim ds As DataSet
            ds = New DataSet()
            ds = Clsmy.GetDataSet(sql, cn)

            dvmanager2 = New DataViewManager(ds)
            dv2 = dvmanager2.CreateDataView(ds.Tables(0))

            bs2 = New BindingSource
            bs2.DataSource = dv2
            bn2.BindingSource = bs2

            grid2.DataSource = bs2

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

    Private Sub hapus()

        Dim sql As String = String.Format("delete from ms_departemen where kd_depart='{0}'", dv1(Me.BindingContext(bs1).Position)("kd_depart").ToString)

        Dim cn As OleDbConnection = Nothing
        Dim comd As OleDbCommand = Nothing

        Try

            open_wait()

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sqltrans As OleDbTransaction = cn.BeginTransaction

            Dim sqlc1 As String = String.Format("select kd_divisi from ms_divisi where kd_depart='{0}'", dv1(Me.BindingContext(bs1).Position)("kd_depart").ToString)
            Dim cmd1 As OleDbCommand = New OleDbCommand(sqlc1, cn, sqltrans)
            Dim drd1 As OleDbDataReader = cmd1.ExecuteReader

            If drd1.Read Then
                If Not (drd1(0).ToString = "") Then
                    MsgBox("Data sudah dipakai, tidak dapat dihapus", vbOKOnly + vbInformation)
                    sqltrans.Rollback()
                    Return
                End If
            End If
            drd1.Close()


            comd = New OleDbCommand(sql, cn, sqltrans)
            comd.ExecuteNonQuery()

            Clsmy.InsertToLog(cn, "btdepart", 0, 0, 1, 0, dv1(Me.BindingContext(bs1).Position)("kd_depart").ToString, "departemen", sqltrans)

            sqltrans.Commit()

            close_wait()

            dv1.Delete(bs1.Position)

            MsgBox("Data telah dihapus...", vbOKOnly + vbInformation, "Informasi")

        Catch ex As Exception
            close_wait()
            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
        Finally

            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If

        End Try


    End Sub

    Private Sub hapus_divisi()

        Dim sql As String = String.Format("delete from ms_divisi where kd_divisi='{0}'", dv2(Me.BindingContext(bs2).Position)("kd_divisi").ToString)

        Dim cn As OleDbConnection = Nothing
        Dim comd As OleDbCommand = Nothing

        Try

            open_wait()

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sqltrans As OleDbTransaction = cn.BeginTransaction

            Dim sqlc As String = String.Format("select noid from ms_akun_detail where kd_divisi='{0}'", dv2(Me.BindingContext(bs2).Position)("kd_divisi").ToString)
            Dim cmdc As OleDbCommand = New OleDbCommand(sqlc, cn, sqltrans)
            Dim drdc As OleDbDataReader = cmdc.ExecuteReader

            If drdc.Read Then
                If Not (drdc(0).ToString = "") Then
                    MsgBox("Data sudah dipakai, tidak dapat dihapus", vbOKOnly + vbInformation)
                    sqltrans.Rollback()
                    Return
                End If
            End If
            drdc.Close()

            Dim sqlc1 As String = String.Format("select nobukti from tr_pengajuan where kd_divisi='{0}'", dv2(Me.BindingContext(bs2).Position)("kd_divisi").ToString)
            Dim cmdc1 As OleDbCommand = New OleDbCommand(sqlc1, cn, sqltrans)
            Dim drdc1 As OleDbDataReader = cmdc1.ExecuteReader

            If drdc1.Read Then
                If Not (drdc1(0).ToString) = "" Then
                    MsgBox("Data sudah dipakai, tidak dapat dihapus", vbOKOnly + vbInformation)
                    sqltrans.Rollback()
                    Return
                End If
            End If
            drdc1.Close()

            comd = New OleDbCommand(sql, cn, sqltrans)
            comd.ExecuteNonQuery()

            Clsmy.InsertToLog(cn, "btdepart", 0, 0, 1, 0, dv2(Me.BindingContext(bs2).Position)("kd_divisi").ToString, "divisi", sqltrans)

            sqltrans.Commit()

            close_wait()

            dv2.Delete(bs2.Position)

            MsgBox("Data telah dihapus...", vbOKOnly + vbInformation, "Informasi")

        Catch ex As Exception
            close_wait()
            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
        Finally

            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If

        End Try


    End Sub

    Private Sub Get_Aksesform()

        Dim rows() As DataRow = dtmenu.Select(String.Format("NAMAFORM='{0}'", Me.Name.ToUpper.ToString))

        If Convert.ToInt16(rows(0)("t_add")) = 1 Then
            tsadd.Enabled = True
            tsadd2.Enabled = True
        Else
            tsadd.Enabled = False
            tsadd2.Enabled = False
        End If

        If Convert.ToInt16(rows(0)("t_edit")) = 1 Then
            tsedit.Enabled = True
            tsedit2.Enabled = True
        Else
            tsedit.Enabled = False
            tsedit2.Enabled = False
        End If

        If Convert.ToInt16(rows(0)("t_del")) = 1 Then
            tsdel.Enabled = True
            tsdel2.Enabled = True
        Else
            tsdel.Enabled = False
            tsdel2.Enabled = False
        End If

    End Sub


    Private Sub fdepart_divisi_Load(sender As Object, e As EventArgs) Handles Me.Load
        Get_Aksesform()

        open_depart()
        open_divisi()

        tcbofind.SelectedIndex = 0
        tcbofind2.SelectedIndex = 0

    End Sub

    Private Sub tsfind_Click(sender As Object, e As EventArgs) Handles tsfind.Click
        cari_depart()
    End Sub

    Private Sub tsfind2_Click(sender As Object, e As EventArgs) Handles tsfind2.Click
        cari_divisi()
    End Sub

    Private Sub tfind_KeyDown(sender As Object, e As KeyEventArgs) Handles tfind.KeyDown
        If e.KeyCode = 13 Then
            cari_depart()
        End If
    End Sub

    Private Sub tfind2_KeyDown(sender As Object, e As KeyEventArgs) Handles tfind2.KeyDown
        If e.KeyCode = 13 Then
            cari_divisi()
        End If
    End Sub

    Private Sub tsref_Click(sender As Object, e As EventArgs) Handles tsref.Click
        tfind.Text = ""
        open_depart()
    End Sub

    Private Sub tsref2_Click(sender As Object, e As EventArgs) Handles tsref2.Click
        tfind2.Text = ""
        open_divisi()
    End Sub

    Private Sub tsdel_Click(sender As Object, e As EventArgs) Handles tsdel.Click
        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count <= 0 Then
            Return
        End If

        If MsgBox("Yakin akan dihapus ?", vbYesNo + vbQuestion, "Konfirmasi") = MsgBoxResult.No Then
            Return
        End If

        hapus()

    End Sub

    Private Sub tsdel2_Click(sender As Object, e As EventArgs) Handles tsdel2.Click
        If IsNothing(dv2) Then
            Return
        End If

        If dv2.Count <= 0 Then
            Return
        End If

        If MsgBox("Yakin akan dihapus ?", vbYesNo + vbQuestion, "Konfirmasi") = MsgBoxResult.No Then
            Return
        End If

        hapus_divisi()

    End Sub

    Private Sub tsadd_Click(sender As Object, e As EventArgs) Handles tsadd.Click
        Using fkar2 As New fdepart_divisi2 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = True, .position = 0}
            fkar2.ShowDialog()
        End Using
    End Sub

    Private Sub tsedit_Click(sender As Object, e As EventArgs) Handles tsedit.Click

        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count <= 0 Then
            Return
        End If

        Using fkar2 As New fdepart_divisi2 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = False, .position = bs1.Position}
            fkar2.ShowDialog()
        End Using

    End Sub

    Private Sub tsadd2_Click(sender As Object, e As EventArgs) Handles tsadd2.Click
        Using fkar2 As New fdepart_divisi3 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv2, .addstat = True, .position = 0}
            fkar2.ShowDialog()
        End Using
    End Sub

    Private Sub tsedit2_Click(sender As Object, e As EventArgs) Handles tsedit2.Click

        If IsNothing(dv2) Then
            Return
        End If

        If dv2.Count <= 0 Then
            Return
        End If

        Using fkar2 As New fdepart_divisi3 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv2, .addstat = False, .position = bs2.Position}
            fkar2.ShowDialog()
        End Using

    End Sub


End Class