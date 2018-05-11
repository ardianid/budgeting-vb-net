Imports System.Data
Imports System.Data.OleDb
Imports Budgeting.Clsmy

Public Class fuser2

    Public addstat As Boolean = True
    Private dvmanager As Data.DataViewManager
    Private dv1 As Data.DataView

    Private dvmanager2 As Data.DataViewManager
    Private dv2 As Data.DataView

    Private dvmanager3 As Data.DataViewManager
    Private dv3 As Data.DataView

    Private dvmanager_div As Data.DataViewManager
    Private dv_div As Data.DataView

    Private dvmanager_us As Data.DataViewManager
    Private dv_us As Data.DataView

    Public nama As String
    Public nonaktifkan As Integer
    Public jenisuser As String
    Public initial_dia As String
    Public sbalik_opex As Integer
    Public st_div As Integer
    Public st_user As Integer
    Public st_kross As Integer

    Private Sub manipulate()

        Dim cn As OleDbConnection = Nothing

        Dim sql_insert As String = String.Format("INSERT INTO ms_usersys (namauser,nonaktif,pwd,jenisuser,initial_us,sbalik_opex,sall_div,sall_user,sverif_cross) VALUES('{0}',{1},HASHBYTES('md5','{2}'),'{3}','{4}',{5},{6},{7},{8})", tnama.Text.Trim, IIf(cnonaktif.Checked.Equals(True), 1, 0), tpwd.Text.Trim, cbo_kep.EditValue, tinitial.Text.Trim, IIf(cbalik.Checked.Equals(True), 1, 0), IIf(cdiv.Checked.Equals(True), 1, 0), rd_user.EditValue, IIf(ckross.Checked.Equals(True), 1, 0))
        Dim sql_update As String = String.Format("UPDATE ms_usersys SET nonaktif={0},initial_us='{1}',sbalik_opex={2},sall_div={3},sall_user={4},sverif_cross={5} WHERE namauser='{6}'", IIf(cnonaktif.Checked.Equals(True), 1, 0), tinitial.Text.Trim, IIf(cbalik.Checked.Equals(True), 1, 0), IIf(cdiv.Checked.Equals(True), 1, 0), rd_user.EditValue, IIf(ckross.Checked.Equals(True), 1, 0), tnama.Text.Trim)
        Dim sql_update_pwd As String = String.Format("UPDATE ms_usersys SET nonaktif={0},pwd=HASHBYTES('md5','{1}') WHERE namauser='{2}'", IIf(cnonaktif.Checked.Equals(True), 1, 0), tpwd.Text.Trim, tnama.Text.Trim)

        Dim sql_search As String = String.Format("select namauser from ms_usersys where namauser='{0}'", tnama.Text.Trim)
        Dim dread As OleDbDataReader
        Dim cmdsearch As OleDbCommand

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn
            If addstat.Equals(True) Then

                cmdsearch = New OleDbCommand(sql_search, cn)
                dread = cmdsearch.ExecuteReader

                If dread.Read Then

                    dread.Close()
                    MsgBox("User sudah ada", MsgBoxStyle.Information, "Informasi")
                    tnama.Focus()
                    Exit Sub
                Else
                    dread.Close()
                End If
            End If

            open_wait()

            Dim sqltrans As OleDbTransaction = cn.BeginTransaction()
            Dim cmd2 As OleDbCommand

            If addstat.Equals(True) Then
                cmd2 = New OleDbCommand(sql_insert, cn, sqltrans)
                cmd2.ExecuteNonQuery()

                Clsmy.InsertToLog(cn, "btnuser", 1, 0, 0, 0, tnama.Text.Trim, "", sqltrans)

            Else

                If tpwd.Text.Trim.Length > 0 Then
                    cmd2 = New OleDbCommand(sql_update_pwd, cn, sqltrans)
                    cmd2.ExecuteNonQuery()

                    Clsmy.InsertToLog(cn, "btnuser", 0, 1, 0, 0, tnama.Text.Trim, "", sqltrans)

                Else
                    cmd2 = New OleDbCommand(sql_update, cn, sqltrans)
                    cmd2.ExecuteNonQuery()

                    Clsmy.InsertToLog(cn, "btnuser", 0, 1, 0, 0, tnama.Text.Trim, "", sqltrans)

                End If

            End If


            manipulate_detail(cn, sqltrans)
            manipulate_detail2(cn, sqltrans)
            manipulate_unit_usaha(cn, sqltrans)

            'If cdiv.Checked = False Then
            manipulate_divisi(cn, sqltrans)
            'End If

            'If rd_user.EditValue = 3 Then
            manipulate_userlain(cn, sqltrans)
            'End If

            sqltrans.Commit()

            Me.Close()

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Informasi")

        Finally

            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If

            close_wait()
            fuser.refresh_data()

        End Try

    End Sub

    Private Sub manipulate_detail(ByVal cn As OleDbConnection, ByVal sqltans As OleDbTransaction)

        Dim sql As String
        Dim sql_ins As String
        Dim sql_edit As String
        Dim a As Integer = 0
        Dim dread As OleDbDataReader = Nothing
        Dim comd As OleDbCommand
        Dim comd2 As OleDbCommand

        For a = 0 To dv1.Count - 1

            sql = String.Format("select id from ms_usersys2 where kodemenu='{0}' and namauser='{1}'", dv1(a)(0).ToString, tnama.Text.Trim)

            comd = New OleDbCommand(sql, cn, sqltans)
            dread = comd.ExecuteReader

            If dread.Read Then

                sql_edit = String.Format("update ms_usersys2 set t_active={0},t_add={1},t_edit={2},t_del={3},t_lap={4} where id={5}", dv1(a)(3).ToString, dv1(a)(4).ToString, dv1(a)(5).ToString, dv1(a)(6).ToString, dv1(a)(7).ToString, dread(0).ToString)
                comd2 = New OleDbCommand(sql_edit, cn, sqltans)
                comd2.ExecuteNonQuery()

            Else

                sql_ins = String.Format("insert into ms_usersys2 (kodemenu,t_active,t_add,t_edit,t_del,t_lap,namauser) values('{0}',{1},{2},{3},{4},{5},'{6}')", dv1(a)(0).ToString, dv1(a)(3).ToString, dv1(a)(4).ToString, dv1(a)(5).ToString, dv1(a)(6).ToString, dv1(a)(7).ToString, tnama.Text.Trim)
                comd2 = New OleDbCommand(sql_ins, cn, sqltans)
                comd2.ExecuteNonQuery()

            End If


        Next

    End Sub

    Private Sub manipulate_detail2(ByVal cn As OleDbConnection, ByVal sqltans As OleDbTransaction)

        Dim sql As String
        Dim sql_ins As String
        Dim sql_edit As String
        Dim a As Integer = 0
        Dim dread As OleDbDataReader = Nothing
        Dim comd As OleDbCommand
        Dim comd2 As OleDbCommand

        For a = 0 To dv2.Count - 1

            sql = String.Format("select id from ms_usersys3 where kodemenu='{0}' and namauser='{1}'", dv2(a)(0).ToString, tnama.Text.Trim)

            comd = New OleDbCommand(sql, cn, sqltans)
            dread = comd.ExecuteReader

            If dread.Read Then

                sql_edit = String.Format("update ms_usersys3 set t_lap={0} where id={1}", dv2(a)(3).ToString, dread(0).ToString)
                comd2 = New OleDbCommand(sql_edit, cn, sqltans)
                comd2.ExecuteNonQuery()

            Else

                sql_ins = String.Format("insert into ms_usersys3 (kodemenu,t_lap,namauser) values('{0}',{1},'{2}')", dv2(a)(0).ToString, dv2(a)(3).ToString, tnama.Text.Trim)
                comd2 = New OleDbCommand(sql_ins, cn, sqltans)
                comd2.ExecuteNonQuery()

            End If


        Next

    End Sub

    Private Sub manipulate_unit_usaha(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction)

        Dim sqldel As String = String.Format("delete from ms_usersys4 where namauser='{0}'", tnama.EditValue)
        Using cmddel As OleDbCommand = New OleDbCommand(sqldel, cn, sqltrans)
            cmddel.ExecuteNonQuery()
        End Using

        For i As Integer = 0 To dv3.Count - 1

            If dv3(i)("spakai") = 1 Then

                Dim sqlins As String = String.Format("insert into ms_usersys4 (idbu,namauser,sdef) values({0},'{1}',0)", dv3(i)("idbu").ToString, tnama.EditValue)
                Using cmd As OleDbCommand = New OleDbCommand(sqlins, cn, sqltrans)
                    cmd.ExecuteNonQuery()
                End Using

                If dv3(i)("sdef") = 1 Then
                    Dim sql As String = String.Format("update ms_usersys4 set sdef=1 where idbu={0} and namauser='{1}'", dv3(i)("idbu").ToString, tnama.EditValue)
                    Using cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                        cmd.ExecuteNonQuery()
                    End Using
                End If


            End If

        Next

    End Sub

    Private Sub manipulate_divisi(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction)

        Dim sqldel As String = String.Format("delete from ms_usersys5 where namauser='{0}'", tnama.EditValue)
        Using cmddel As OleDbCommand = New OleDbCommand(sqldel, cn, sqltrans)
            cmddel.ExecuteNonQuery()
        End Using

        If cdiv.Checked = True Then
            Return
        End If

        For i As Integer = 0 To dv_div.Count - 1

            If dv_div(i)("spakai") = 1 Then

                Dim sqlins As String = String.Format("insert into ms_usersys5 (kd_divisi,namauser) values('{0}','{1}')", dv_div(i)("kd_divisi").ToString, tnama.EditValue)
                Using cmd As OleDbCommand = New OleDbCommand(sqlins, cn, sqltrans)
                    cmd.ExecuteNonQuery()
                End Using

            End If

        Next

    End Sub

    Private Sub manipulate_userlain(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction)

        Dim sqldel As String = String.Format("delete from ms_usersys6 where namauser='{0}'", tnama.EditValue)
        Using cmddel As OleDbCommand = New OleDbCommand(sqldel, cn, sqltrans)
            cmddel.ExecuteNonQuery()
        End Using

        If rd_user.EditValue <> 3 Then
            Return
        End If

        For i As Integer = 0 To dv_us.Count - 1

            If dv_us(i)("spakai") = 1 Then

                Dim sqlins As String = String.Format("insert into ms_usersys6 (namauser2,namauser) values('{0}','{1}')", dv_us(i)("namauser").ToString, tnama.EditValue)
                Using cmd As OleDbCommand = New OleDbCommand(sqlins, cn, sqltrans)
                    cmd.ExecuteNonQuery()
                End Using

            End If

        Next

    End Sub


    Private Sub load_detail()

        Dim ds As DataSet
        Dim cn As OleDbConnection = Nothing

        Dim sql_add As String = "SELECT A.kodemenu,A.namamenu,A.namaform,0 AS t_active,0 AS t_add,0 AS t_edit,0 AS t_del,0 AS t_lap,A.keterangan " _
           + "FROM ms_menu A"

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            ds = New DataSet()
            ds = Clsmy.GetDataSet(sql_add, cn)

            dvmanager = New DataViewManager(ds)
            dv1 = dvmanager.CreateDataView(ds.Tables(0))

            GridControl1.DataSource = dv1

            If addstat.Equals(False) Then
                cek_valid_user(cn)
            End If


        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
        Finally

            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If

        End Try

    End Sub

    Private Sub load_detail2()

        Dim ds As DataSet
        Dim cn As OleDbConnection = Nothing

        Dim sql_add As String = "SELECT A.kodemenu,A.namamenu,A.namaform,0 AS t_lap,A.keterangan,A.namagroup " _
           + "FROM ms_menu2 A"

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            ds = New DataSet()
            ds = Clsmy.GetDataSet(sql_add, cn)

            dvmanager2 = New DataViewManager(ds)
            dv2 = dvmanager2.CreateDataView(ds.Tables(0))

            grid2.DataSource = dv2

            If addstat.Equals(False) Then
                cek_valid_user2(cn)
            End If


        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
        Finally

            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If

        End Try

    End Sub

    Private Sub load_unit_usaha()

        Dim cn As OleDbConnection = Nothing
        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = "select idbu,nama_bu,0 as spakai,0 as sdef from ms_bu"

            Dim ds As DataSet
            ds = New DataSet()
            ds = Clsmy.GetDataSet(sql, cn)

            dvmanager3 = New DataViewManager(ds)
            dv3 = dvmanager3.CreateDataView(ds.Tables(0))

            grid3.DataSource = dv3

            For i As Integer = 0 To dv3.Count - 1

                Dim sqlc As String = String.Format("select idbu,sdef from ms_usersys4 where idbu={0} and namauser='{1}'", dv3(i)("idbu").ToString, tnama.EditValue)
                Dim cmdc As OleDbCommand = New OleDbCommand(sqlc, cn)
                Dim drdc As OleDbDataReader = cmdc.ExecuteReader

                If drdc.Read Then
                    If Not (drdc("idbu").ToString = "") Then
                        If drdc("idbu").ToString.Trim.Length > 0 Then
                            dv3(i)("spakai") = 1

                            If drdc("sdef").ToString = 1 Then
                                dv3(i)("sdef") = 1
                            Else
                                dv3(i)("sdef") = 0
                            End If

                        Else
                            dv3(i)("spakai") = 0
                        End If
                    End If
                End If
                drdc.Close()

            Next

        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
        Finally

            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If
        End Try

    End Sub

    Private Sub load_divisi()

        Dim cn As OleDbConnection = Nothing
        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = "select b.kd_divisi,b.nama_divisi,0 as spakai from ms_divisi b"

            Dim ds As DataSet = New DataSet()
            ds = Clsmy.GetDataSet(sql, cn)

            dvmanager_div = New DataViewManager(ds)
            dv_div = dvmanager_div.CreateDataView(ds.Tables(0))

            grid_div.DataSource = dv_div

            For i As Integer = 0 To dv_div.Count - 1

                Dim sqlc As String = String.Format("select * from ms_usersys5 where namauser='{0}' and kd_divisi='{1}'", tnama.EditValue, dv_div(i)("kd_divisi").ToString)
                Dim cmdc As OleDbCommand = New OleDbCommand(sqlc, cn)
                Dim drdc As OleDbDataReader = cmdc.ExecuteReader

                If drdc.Read Then
                    If Not (drdc("kd_divisi").ToString = "") Then
                        If drdc("kd_divisi").ToString.Trim.Length > 0 Then

                            dv_div(i)("spakai") = 1

                        Else

                            dv_div(i)("spakai") = 0

                        End If
                    End If
                End If
                drdc.Close()

            Next

        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
        Finally

            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If
        End Try

    End Sub

    Private Sub load_userlain()

        Dim cn As OleDbConnection = Nothing
        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = String.Format("select namauser,0 as spakai from ms_usersys where not(namauser='{0}')", tnama.EditValue)

            Dim ds As DataSet
            ds = New DataSet()
            ds = Clsmy.GetDataSet(sql, cn)

            dvmanager_us = New DataViewManager(ds)
            dv_us = dvmanager_us.CreateDataView(ds.Tables(0))

            grid_user.DataSource = dv_us

            For i As Integer = 0 To dv_us.Count - 1

                Dim sqlc As String = String.Format("select * from ms_usersys6 where namauser='{0}' and namauser2='{1}'", tnama.EditValue, dv_us(i)("namauser").ToString)
                Dim cmdc As OleDbCommand = New OleDbCommand(sqlc, cn)
                Dim drdc As OleDbDataReader = cmdc.ExecuteReader

                If drdc.Read Then
                    If Not (drdc("namauser2").ToString = "") Then
                        If drdc("namauser2").ToString.Trim.Length > 0 Then

                            dv_us(i)("spakai") = 1

                        Else

                            dv_us(i)("spakai") = 0

                        End If
                    End If
                End If
                drdc.Close()

            Next

        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
        Finally

            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If
        End Try

    End Sub

    Private Sub cek_valid_user(ByVal cn As OleDbConnection)

        Dim a As Integer = 0
        Dim dred As OleDbDataReader = Nothing
        Dim sql As String = ""
        Dim mycomd As OleDbCommand = Nothing
        Dim kode As String

        Try


            If Not (dv1.Count <= 0) Then

                For a = 0 To dv1.Count - 1

                    kode = dv1(a)("kodemenu").ToString

                    sql = String.Format("select t_active,t_add,t_edit,t_del,t_lap from ms_usersys2 where kodemenu='{0}' and namauser=upper('{1}')", kode, tnama.Text)

                    mycomd = New OleDbCommand With {.CommandType = CommandType.Text, .CommandText = sql, .Connection = cn}
                    dred = mycomd.ExecuteReader

                    If dred.Read Then


                        dv1(a)(3) = Convert.ToInt32(dred(0).ToString)
                        dv1(a)(4) = Convert.ToInt32(dred(1).ToString)
                        dv1(a)(5) = Convert.ToInt32(dred(2).ToString)
                        dv1(a)(6) = Convert.ToInt32(dred(3).ToString)
                        dv1(a)(7) = Convert.ToInt32(dred(4).ToString)

                    End If

                    dred.Close()

                Next

                GridControl1.Refresh()

            End If

        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
        End Try



    End Sub

    Private Sub cek_valid_user2(ByVal cn As OleDbConnection)

        Dim a As Integer = 0
        Dim dred As OleDbDataReader = Nothing
        Dim sql As String = ""
        Dim mycomd As OleDbCommand = Nothing
        Dim kode As String

        Try


            If Not (dv2.Count <= 0) Then

                For a = 0 To dv2.Count - 1

                    kode = dv2(a)("kodemenu").ToString

                    sql = String.Format("select t_lap from ms_usersys3 where kodemenu='{0}' and namauser=upper('{1}')", kode, tnama.Text)

                    mycomd = New OleDbCommand With {.CommandType = CommandType.Text, .CommandText = sql, .Connection = cn}
                    dred = mycomd.ExecuteReader

                    If dred.Read Then


                        dv2(a)(3) = Convert.ToInt32(dred(0).ToString)


                    End If

                    dred.Close()

                Next

                grid2.Refresh()

            End If

        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
        End Try



    End Sub

    Private Sub btkeluar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btkeluar.Click
        Me.Close()
    End Sub

    Private Sub fuser2_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        If addstat.Equals(True) Then
            tnama.Focus()
        Else
            tpwd.Focus()
        End If
    End Sub

    Private Sub fuser2_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If addstat.Equals(True) Then
            tnama.Enabled = True

            cnonaktif.Checked = False
            cnonaktif.Enabled = False
            cbo_kep.Enabled = True
            cbo_kep.SelectedIndex = 0
            cbalik.Checked = False

            cdiv.Checked = True
            cdiv.Text = "&Ya"
            grid_div.Enabled = False

            ckross.Checked = False
            ckross.Text = "&Tidak"

            rd_user.EditValue = 1
            grid_user.Enabled = False

        Else

            tnama.Enabled = False

            tnama.Text = nama

            If jenisuser = "Admin" Then
                cbo_kep.SelectedIndex = 0
            Else
                cbo_kep.SelectedIndex = 1
            End If

            cbo_kep.Enabled = False

            cnonaktif.Enabled = True
            cnonaktif.Checked = nonaktifkan

            If sbalik_opex = 1 Then
                cbalik.Checked = True
            Else
                cbalik.Checked = False
            End If

            If st_div = 1 Then
                cdiv.Checked = True
            Else
                cdiv.Checked = False
            End If

            If st_kross = 1 Then
                ckross.Checked = True
            Else
                ckross.Checked = False
            End If

            rd_user.EditValue = st_user

            If st_user = 3 Then
                grid_user.Enabled = True
            Else
                grid_user.Enabled = False
            End If

            tinitial.Text = initial_dia

        End If

        load_detail()
        load_detail2()
        load_unit_usaha()
        load_divisi()
        load_userlain()

        ComboBoxEdit1.SelectedIndex = 0
        ComboBoxEdit2.SelectedIndex = 0

    End Sub

    Private Sub ComboBoxEdit1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBoxEdit1.SelectedIndexChanged

        If IsNothing(dv1) Then
            Exit Sub
        End If

        If dv1.Count > 0 Then

            Dim a As Integer = 0
            For a = 0 To dv1.Count - 1

                If ComboBoxEdit1.SelectedIndex = 1 Then

                    dv1(a)(3) = 1 ' active
                    dv1(a)(4) = 1 ' add
                    dv1(a)(5) = 1 ' edit
                    dv1(a)(6) = 1 ' del
                    dv1(a)(7) = 1 ' cetak

                ElseIf ComboBoxEdit1.SelectedIndex = 2 Then

                    dv1(a)(3) = 0 ' active
                    dv1(a)(4) = 0 ' add
                    dv1(a)(5) = 0 ' edit
                    dv1(a)(6) = 0 ' del
                    dv1(a)(7) = 0 ' cetak

                End If

            Next


            GridControl1.Refresh()

        End If

    End Sub

    Private Sub ComboBoxEdit2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBoxEdit2.SelectedIndexChanged

        If IsNothing(dv2) Then
            Exit Sub
        End If

        If dv2.Count > 0 Then

            Dim a As Integer = 0
            For a = 0 To dv2.Count - 1

                If ComboBoxEdit2.SelectedIndex = 1 Then

                    dv2(a)(3) = 1 ' lap

                ElseIf ComboBoxEdit2.SelectedIndex = 2 Then

                    dv2(a)(3) = 0 ' lap

                End If

            Next


            grid2.Refresh()

        End If

    End Sub

    Private Sub tnama_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles tnama.KeyDown
        If e.KeyCode = 13 Then
            cbo_kep.Focus()
        End If
    End Sub

    Private Sub cbo_kep_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles cbo_kep.KeyDown
        If e.KeyCode = 13 Then
            tpwd.Focus()
        End If
    End Sub

    Private Sub tpwd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles tpwd.KeyDown
        If e.KeyCode = 13 Then
            tpwd2.Focus()
        End If
    End Sub

    Private Sub btsimpan_Click(sender As System.Object, e As System.EventArgs) Handles btsimpan.Click
        If tnama.Text = "" Then
            MsgBox("Nama user tidak boleh kosong", MsgBoxStyle.Information, "Informasi")
            tnama.Focus()
            Exit Sub
        End If

        If addstat.Equals(True) Then

            If tpwd.Text = "" Then
                MsgBox("Password tidak boleh kosong", MsgBoxStyle.Information, "Informasi")
                tpwd.Focus()
                Exit Sub
            End If

            If tpwd2.Text = "" Then
                MsgBox("Verifikasi password tidak boleh kosong", MsgBoxStyle.Information, "Informasi")
                tpwd2.Focus()
                Exit Sub
            End If

            If Not tpwd.Text.Equals(tpwd2.Text) Then

                MsgBox("Password dan verifikasi password tidak sama", MsgBoxStyle.Information, "Informasi")
                tpwd.Focus()
                Exit Sub
            End If

        Else

            If tpwd.Text <> "" And tpwd2.Text <> "" Then

                If Not tpwd.Text.Equals(tpwd2.Text) Then

                    MsgBox("Password dan verifikasi password tidak sama", MsgBoxStyle.Information, "Informasi")
                    tpwd.Focus()
                    Exit Sub
                End If

            End If

        End If

        manipulate()
    End Sub

    Private Sub cbo_kep_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbo_kep.SelectedIndexChanged

        'If cbo_kep.SelectedIndex = 0 Then
        '    XtraTabPage1.PageVisible = True
        'Else
        '    XtraTabPage1.PageVisible = False
        'End If

    End Sub

    Private Sub GridView3_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView3.CellValueChanged

        If IsNothing(dv3) Then
            Return
        End If

        If dv3.Count <= 0 Then
            Return
        End If

        If e.Column.FieldName = "sdef" Then

            
            If e.Value = 1 Then
                Dim idbu As Integer = dv3(Me.BindingContext(dv3).Position)("idbu")
                For i As Integer = 0 To dv3.Count - 1


                    If dv3(i)("idbu") = idbu Then
                        dv3(i)("spakai") = 1
                        dv3(i)("sdef") = 1
                    Else
                        dv3(i)("sdef") = 0
                    End If

                Next
            End If

        End If


    End Sub


    Private Sub cbalik_CheckedChanged(sender As Object, e As EventArgs) Handles cbalik.CheckedChanged
        If cbalik.Checked = True Then
            cbalik.Text = "&Ya"
        Else
            cbalik.Text = "&Tidak"
        End If
    End Sub

    Private Sub cdiv_CheckedChanged(sender As Object, e As EventArgs) Handles cdiv.CheckedChanged
        If cdiv.Checked = True Then
            cdiv.Text = "&Ya"
            grid_div.Enabled = False
        Else
            cdiv.Text = "&Tidak, hanya beberapa divisi dibawah ini"
            grid_div.Enabled = True
        End If
    End Sub


    Private Sub rd_user_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rd_user.SelectedIndexChanged
        If rd_user.EditValue = 3 Then
            grid_user.Enabled = True
        Else
            grid_user.Enabled = False
        End If
    End Sub

    Private Sub ckross_CheckedChanged(sender As Object, e As EventArgs) Handles ckross.CheckedChanged
        If ckross.Checked = True Then
            ckross.Text = "&Ya"
        Else
            ckross.Text = "&Tidak"
        End If
    End Sub

End Class