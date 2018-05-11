Imports System.Data
Imports System.Data.OleDb

Public Class fbu

    Private bs1 As BindingSource
    Private dvmanager As DataViewManager
    Private dv1 As DataView

    Dim stadd As Boolean
    Dim stedd As Boolean
    Dim stdell As Boolean

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

    End Sub

    Private Sub opendata()

        Const sql As String = "select * from ms_bu"
        Dim cn As OleDbConnection = Nothing

        grid1.DataSource = Nothing

        Try

            '  open_wait()

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

            '   close_wait()


        Catch ex As OleDb.OleDbException
            'close_wait()
            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
        Finally

            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If

        End Try

    End Sub

    Private Sub delete_data()

        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count <= 0 Then
            Return
        End If

        If MsgBox("Yakin akan dihapus ?", vbYesNo + vbQuestion, "Konfirmasi") = MsgBoxResult.No Then
            Return
        End If

        Dim cn As OleDbConnection = Nothing
        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sqlcek As String = String.Format("select idbu from ms_usersys4 where idbu={0}", dv1(bs1.Position)("idbu").ToString)
            Dim cmdcek As OleDbCommand = New OleDbCommand(sqlcek, cn)
            Dim drdcek As OleDbDataReader = cmdcek.ExecuteReader

            If drdcek.Read Then
                If drdcek(0).ToString.Trim.Length > 0 Then
                    MsgBox("Unit usaha/perusahaan sudah dipakai,hapus dibatalkan", vbOKOnly + vbInformation, "Informasi")
                    Return
                End If
            End If
            drdcek.Close()

            Dim sqlc2 As String = String.Format("select kd_akun from ms_akun where idbu={0}", dv1(bs1.Position)("idbu").ToString)
            Dim cmd2 As OleDbCommand = New OleDbCommand(sqlc2, cn)
            Dim drd2 As OleDbDataReader = cmd2.ExecuteReader

            If drd2.Read Then
                If Not (drd2(0).ToString = "") Then
                    MsgBox("Unit usaha/perusahaan sudah dipakai,hapus dibatalkan", vbOKOnly + vbInformation, "Informasi")
                    Return
                End If
            End If
            drd2.Close()

            Dim sqlc3 As String = String.Format("select nobukti from tr_pengajuan where idbu={0}", dv1(bs1.Position)("idbu").ToString)
            Dim cmd3 As OleDbCommand = New OleDbCommand(sqlc3, cn)
            Dim drd3 As OleDbDataReader = cmd3.ExecuteReader

            If drd3.Read Then
                If Not (drd3(0).ToString = "") Then
                    MsgBox("Unit usaha/perusahaan sudah dipakai,hapus dibatalkan", vbOKOnly + vbInformation, "Informasi")
                    Return
                End If
            End If
            drd3.Close()

            Dim sqlc4 As String = String.Format("select noid from ms_akun_detail where idbu={0}", dv1(bs1.Position)("idbu").ToString)
            Dim cmd4 As OleDbCommand = New OleDbCommand(sqlc4, cn)
            Dim drd4 As OleDbDataReader = cmd4.ExecuteReader

            If drd4.Read Then
                If Not (drd4(0).ToString = "") Then
                    MsgBox("Unit usaha/perusahaan sudah dipakai,hapus dibatalkan", vbOKOnly + vbInformation, "Informasi")
                    Return
                End If
            End If
            drd4.Close()

            Dim sql As String = String.Format("delete from ms_bu where idbu={0}", dv1(bs1.Position)("idbu").ToString)
            Using cmd As OleDbCommand = New OleDbCommand(sql, cn)
                cmd.ExecuteNonQuery()
            End Using

            Clsmy.InsertToLog(cn, "btakun", 0, 0, 1, 0, dv1(bs1.Position)("idbu").ToString, "", Nothing)

            dv1(bs1.Position).Delete()

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

    Private Sub fbu_Load(sender As Object, e As EventArgs) Handles Me.Load

        Get_Aksesform()
        opendata()

    End Sub

    Private Sub tsref_Click(sender As Object, e As EventArgs) Handles tsref.Click
        opendata()
    End Sub

    Private Sub tsdel_Click(sender As Object, e As EventArgs) Handles tsdel.Click
        delete_data()
    End Sub

    Private Sub tsadd_Click(sender As Object, e As EventArgs) Handles tsadd.Click
        Using fkar2 As New fbu2 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = True, .position = 0}
            fkar2.ShowDialog()
        End Using
    End Sub

    Private Sub tsedit_Click(sender As Object, e As EventArgs) Handles tsedit.Click
        If IsNothing(dv1) Then
            Exit Sub
        End If

        If dv1.Count < 1 Then
            Exit Sub
        End If

        Using fkar2 As New fbu2 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = False, .position = bs1.Position}
            fkar2.ShowDialog()
        End Using
    End Sub

End Class