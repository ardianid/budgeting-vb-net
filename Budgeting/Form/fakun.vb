Imports DevExpress.XtraTreeList
Imports System.Data
Imports System.Data.OleDb

Public Class fakun

    Private bs1 As BindingSource
    Private dvmanager As DataViewManager
    Private dv1 As DataView

    Private Sub opendata()

        Dim sql As String = String.Format("select a.kd_akun_m as ParentID,a.kd_akun as ID,a.kd_akun,a.nama_akun,a.idbu,b.nama_bu from ms_akun a inner join ms_bu b on a.idbu=b.idbu where b.idbu in ({0})", idbu_ku)

        Dim cn As OleDbConnection = Nothing

        tre1.DataSource = Nothing

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

            tre1.DataSource = bs1

            'tre1.ParentFieldName = "kd_akun_m"
            'tre1.KeyFieldName = "kd_akun2"
            tre1.ExpandAll()


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

    Private Sub cari()

        Dim sql As String = String.Format("select a.kd_akun_m as ParentID,a.kd_akun as ID,a.kd_akun,a.nama_akun,a.idbu,b.nama_bu from ms_akun a inner join ms_bu b on a.idbu=b.idbu where a.nama_akun like '%{0}%' and  b.idbu in ({1})", tfind.Text.Trim, idbu_ku)
        Dim cn As OleDbConnection = Nothing

        tre1.DataSource = Nothing

        Try

            open_wait()

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

            tre1.DataSource = bs1

            'tre1.ParentFieldName = "kd_akun_m"
            'tre1.KeyFieldName = "kd_akun2"
            tre1.ExpandAll()

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

    Private Sub delete_data()

        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count <= 0 Then
            Return
        End If

        If dv1(bs1.Position)("kd_akun").ToString = "" Then
            MsgBox("Silahkan pilih data yang akan dihapus", vbOKOnly + vbQuestion, "Konfirmasi")
            Return
        End If

        If MsgBox("Yakin akan dihapus ?", vbYesNo + vbQuestion, "Konfirmasi") = MsgBoxResult.No Then
            Return
        End If


        Dim cn As OleDbConnection = Nothing
        Try

            open_wait()
            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sqlc As String = String.Format("select kd_akun from ms_akun_detail where kd_akun='{0}'", dv1(bs1.Position)("kd_akun").ToString)
            Dim cmdc As OleDbCommand = New OleDbCommand(sqlc, cn)
            Dim drdc As OleDbDataReader = cmdc.ExecuteReader

            If drdc.Read Then
                If Not (drdc(0).ToString = "") Then
                    MsgBox("Akun sudah dipakai, tidak bisa dibatalkan", vbOKOnly + vbInformation, "Informasi")
                    Return
                End If
            End If
            drdc.Close()

            Dim sql As String = String.Format("delete from ms_akun where kd_akun='{0}'", dv1(bs1.Position)("kd_akun").ToString)
            Using cmd As OleDbCommand = New OleDbCommand(sql, cn)
                cmd.ExecuteNonQuery()
            End Using

            Clsmy.InsertToLog(cn, "btakun", 0, 0, 1, 0, dv1(bs1.Position)("kd_akun").ToString, "", Nothing)

            dv1(bs1.Position).Delete()
            close_wait()
            'MsgBox("Data dihapus", vbOKOnly + vbInformation, "Informasi")

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

    Private Sub fakun_Load(sender As Object, e As EventArgs) Handles Me.Load

        tcbofind.SelectedIndex = 0

        Get_Aksesform()


        opendata()

    End Sub

    Private Sub tsedit_Click(sender As Object, e As EventArgs) Handles tsedit.Click

        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count <= 0 Then
            Return
        End If

        Using fkar2 As New fakun2 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = False, .position = bs1.Position}
            fkar2.ShowDialog()
        End Using

    End Sub


    Private Sub tsfind_Click(sender As Object, e As EventArgs) Handles tsfind.Click
        cari()
    End Sub

    Private Sub tfind_KeyDown(sender As Object, e As KeyEventArgs) Handles tfind.KeyDown
        If e.KeyCode = 13 Then
            cari()
        End If
    End Sub

    Private Sub tsref_Click(sender As Object, e As EventArgs) Handles tsref.Click
        tfind.Text = ""
        opendata()
    End Sub

    Private Sub tsadd_Click(sender As Object, e As EventArgs) Handles tsadd.Click
        Using fkar2 As New fakun2 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = True, .position = 0}
            fkar2.ShowDialog()

            'If fkar2.get_stat = True Then

            '    If tfind.Text.Trim.Length > 0 Then
            '        cari()
            '    Else
            '        opendata()
            '    End If

            'End If

        End Using
    End Sub

    Private Sub tsdel_Click(sender As Object, e As EventArgs) Handles tsdel.Click
        delete_data()
    End Sub

End Class