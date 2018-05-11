Imports System.Data
Imports System.Data.OleDb
Imports Budgeting.Clsmy

Public Class fuser

    Private bs1 As BindingSource
    Private ds As DataSet
    Private dvmanager As Data.DataViewManager
    Private dv1 As Data.DataView

    Public Sub refresh_data()
        opendata()
    End Sub

    Private Sub Get_Aksesform()

        Dim rows() As DataRow = dtmenu.Select(String.Format("NAMAFORM='{0}'", Me.Name.ToUpper.ToString))

        If Convert.ToInt16(rows(0)("t_add")) = 1 Then
            tsadd.Enabled = True
        Else
            tsadd.Enabled = False
        End If

        If Convert.ToInt16(rows(0)("t_edit")) = 1 Then
            tsedit.Enabled = True
        Else
            tsedit.Enabled = False
        End If

        If Convert.ToInt16(rows(0)("t_del")) = 1 Then
            tsdel.Enabled = True
        Else
            tsdel.Enabled = False
        End If

        'If Convert.ToInt16(rows(0)("t_lap")) = 1 Then

        'Else

        'End If

    End Sub

    Private Sub opendata()

        Const sql As String = "select * from ms_usersys"
        Dim cn As OleDbConnection = Nothing

        grid1.DataSource = Nothing

        Try

            open_wait()

            dv1 = Nothing

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            ds = New DataSet()
            ds = Clsmy.GetDataSet(sql, cn)

            dvmanager = New DataViewManager(ds)
            dv1 = dvmanager.CreateDataView(ds.Tables(0))

            bs1 = New BindingSource
            bs1.DataSource = dv1
            bn1.BindingSource = bs1

            grid1.DataSource = bs1

            close_wait()


        Catch ex As OleDb.OleDbException
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

    Private Sub cari()

        'bs1.DataSource = Nothing
        grid1.DataSource = Nothing
        Dim cn As OleDbConnection = Nothing

        Dim sql As String = String.Format("select * from ms_usersys where namauser like upper('%{0}%') order by namauser asc", tfind.Text.Trim)

        Try

            open_wait()

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            ds = New DataSet()
            ds = Clsmy.GetDataSet(sql, cn)

            dvmanager = New DataViewManager(ds)
            dv1 = dvmanager.CreateDataView(ds.Tables(0))

            bs1.DataSource = dv1
            bn1.BindingSource = bs1

            grid1.DataSource = bs1

            close_wait()

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

    Private Sub hapus_data()

        Dim sql As String = String.Format("delete from ms_usersys2 where namauser='{0}'", dv1(Me.BindingContext(bs1).Position)("namauser").ToString)
        Dim sql2 As String = String.Format("delete from ms_usersys where namauser='{0}'", dv1(Me.BindingContext(bs1).Position)("namauser").ToString)
        ' Dim sql3 As String = String.Format("delete from ms_usersys3 where namauser='{0}'", dv1(Me.BindingContext(bs1).Position)("namauser").ToString)

        Dim cn As OleDbConnection = Nothing
        Dim comd As OleDbCommand = Nothing
        Dim comd2 As OleDbCommand = Nothing
        ' Dim comd3 As OleDbCommand = Nothing

        Try

            open_wait()

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sqltrans As OleDbTransaction = cn.BeginTransaction

            'comd3 = New OleDbCommand(sql3, cn, sqltrans)
            'comd3.ExecuteNonQuery()

            comd = New OleDbCommand(sql, cn, sqltrans)
            comd.ExecuteNonQuery()

            comd2 = New OleDbCommand(sql2, cn, sqltrans)
            comd2.ExecuteNonQuery()

            Clsmy.InsertToLog(cn, "btnuser", 0, 0, 1, 0, dv1(Me.BindingContext(bs1).Position)("namauser").ToString, "", sqltrans)

            sqltrans.Commit()

            close_wait()

            MsgBox("Data telah dihapus...", vbOKOnly + vbInformation, "Informasi")

            opendata()

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

    Private Sub fuser_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        tcbofind.SelectedIndex = 0

        Get_Aksesform()

        opendata()
    End Sub

    Private Sub tsfind_Click(sender As System.Object, e As System.EventArgs) Handles tsfind.Click
        cari()
    End Sub

    Private Sub tfind_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles tfind.KeyDown
        If e.KeyCode = 13 Then
            cari()
        End If
    End Sub

    Private Sub tsadd_Click(sender As System.Object, e As System.EventArgs) Handles tsadd.Click

        Cursor = Cursors.WaitCursor

        Using userdetail As New fuser2 With {.StartPosition = FormStartPosition.CenterScreen, .addstat = True}
            userdetail.ShowDialog(Me)
        End Using

        Cursor = Cursors.Default

    End Sub

    Private Sub tsedit_Click(sender As System.Object, e As System.EventArgs) Handles tsedit.Click

        If dv1.Count <= 0 Then
        Else

            Cursor = Cursors.WaitCursor

            Dim userdetail As New fuser2 With {.StartPosition = FormStartPosition.CenterScreen,
                                                     .addstat = False,
                                                     .nama = dv1(Me.BindingContext(bs1).Position)("namauser").ToString,
                                                     .nonaktifkan = dv1(Me.BindingContext(bs1).Position)("nonaktif").ToString,
                                                     .jenisuser = dv1(Me.BindingContext(bs1).Position)("jenisuser").ToString,
                                                     .initial_dia = dv1(Me.BindingContext(bs1).Position)("initial_us").ToString,
                                                     .sbalik_opex = dv1(Me.BindingContext(bs1).Position)("sbalik_opex").ToString,
                                                     .st_div = dv1(Me.BindingContext(bs1).Position)("sall_div").ToString,
                                                     .st_user = dv1(Me.BindingContext(bs1).Position)("sall_user").ToString,
                                                     .st_kross = dv1(Me.BindingContext(bs1).Position)("sverif_cross").ToString}

            userdetail.ShowDialog(Me)


            Cursor = Cursors.Default

        End If

    End Sub

    Private Sub tsref_Click(sender As System.Object, e As System.EventArgs) Handles tsref.Click
        tsfind.Text = ""
        opendata()
    End Sub

    Private Sub tsdel_Click(sender As System.Object, e As System.EventArgs) Handles tsdel.Click
        If dv1.Count <= 0 Then
        Else

            If MsgBox("Yakin akan dihapus ?", vbYesNo + vbQuestion, "Konfirmasi") = vbNo Then
                Exit Sub
            End If

            hapus_data()
        End If

    End Sub

End Class