Imports System.Data
Imports System.Data.OleDb
Imports System.IO
Imports DevExpress.XtraEditors
Imports Excel = Microsoft.Office.Interop.Excel
Imports ExcelAutoFormat = Microsoft.Office.Interop.Excel.XlRangeAutoFormat

Public Class fbudget_akun

    Private bs1 As BindingSource
    Private dvmanager As DataViewManager
    Private dv1 As DataView

    Private Function ShowSaveFileDialog(ByVal title As String, ByVal filter As String) As String
        Dim dlg As New SaveFileDialog()
        Dim name As String = Application.ProductName
        Dim n As Integer = name.LastIndexOf(".") + 1
        If n > 0 Then
            name = name.Substring(n, name.Length - n)
        End If
        dlg.Title = "Export To " & title
        dlg.FileName = name
        dlg.Filter = filter
        If dlg.ShowDialog() = DialogResult.OK Then
            Return dlg.FileName
        End If
        Return ""
    End Function

    Private Sub OpenFile(ByVal fileName As String)
        If XtraMessageBox.Show("Anda ingin membuka file ?", "Export To...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Try
                Dim process As New System.Diagnostics.Process()
                process.StartInfo.FileName = fileName
                process.StartInfo.Verb = "Open"
                process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal
                process.Start()
            Catch
                DevExpress.XtraEditors.XtraMessageBox.Show(Me, "Data tidak ditemukan", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
        '   progressBarControl1.Position = 0
    End Sub

    Private Sub open_data()

        Dim cn As OleDbConnection = Nothing

        grid1.DataSource = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = String.Format("select " & _
            "akd.noid,case akd.bulan " & _
            "when 1 then " & _
            "'Januari' " & _
            "when 2 then " & _
            "'Februari' " & _
            "when 3 then " & _
            "'Maret' " & _
            "when 4 then " & _
            "'April' " & _
            "when 5 then " & _
            "'Mei' " & _
            "when 6 then " & _
            "'Juni' " & _
            "when 7 then " & _
            "'Juli' " & _
            "when 8 then " & _
            "'Agustus' " & _
            "when 9 then " & _
            "'September' " & _
            "when 10 then " & _
            "'Oktober' " & _
            "when 11 then " & _
            "'November' " & _
            "when 12 then " & _
            "'Desember' end as 'nama_bulan', " & _
            "akd.kd_akun, akn.nama_akun, bu.nama_bu, dpr.nama_departemen, div.nama_divisi, akd.akun_opex_bp, " & _
            "akd.jenis_opex, akd.aktifitas, akd.detail_aktifitas, akd.jml,akd.jml_order,akd.jml_pakai,(akd.jml - (akd.jml_order+akd.jml_pakai)) as sisa_bu " & _
            "from ms_akun_detail akd inner join ms_bu bu on akd.idbu=bu.idbu " & _
            "inner join ms_akun akn on akd.kd_akun=akn.kd_akun inner join ms_divisi div on akd.kd_divisi=div.kd_divisi " & _
            "inner join ms_departemen dpr on div.kd_depart=dpr.kd_depart " & _
            "where akd.tahun = {0} ", tthn.Text)

            dv1 = Nothing

            cn = New OleDbConnection
            cn = Clsmy.open_conn

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
                    sql = String.Format("{0} and div.kd_divisi in ({1})", sql, kd_divaja)
                End If

            End If

            sql = String.Format(" {0} order by akn.kd_akun,akd.bulan,akd.idbu,dpr.kd_depart,div.kd_divisi asc", sql)

            Dim ds As DataSet
            ds = New DataSet()
            ds = Clsmy.GetDataSet(sql, cn)

            dvmanager = New DataViewManager(ds)
            dv1 = dvmanager.CreateDataView(ds.Tables(0))

            bs1 = New BindingSource
            bs1.DataSource = dv1
            bn1.BindingSource = bs1

            grid1.DataSource = bs1

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
    Private Sub Get_Aksesform()

        Dim rows() As DataRow = dtmenu.Select(String.Format("NAMAFORM='{0}'", Me.Name.ToUpper.ToString))

        If Convert.ToInt16(rows(0)("t_add")) = 1 Then
            tsadd.Enabled = True
        Else
            tsadd.Enabled = False
        End If

        If Convert.ToInt16(rows(0)("t_del")) = 1 Then
            tsdel.Enabled = True
        Else
            tsdel.Enabled = False
        End If

        If Convert.ToInt16(rows(0)("t_lap")) = 1 Then
            'ExportToExcelToolStripMenuItem.Enabled = True
            'ExportToTextToolStripMenuItem.Enabled = True
        Else
            'ExportToExcelToolStripMenuItem.Enabled = False
            'ExportToTextToolStripMenuItem.Enabled = False
        End If

        If Convert.ToInt16(rows(0)("t_edit")) = 1 Then

            GridColumn1.OptionsColumn.AllowEdit = True 'akun_opex_bp
            GridColumn3.OptionsColumn.AllowEdit = True 'jenis opex
            GridColumn4.OptionsColumn.AllowEdit = True ' aktifitas
            GridColumn2.OptionsColumn.AllowEdit = True ' detail aktivity
            cl_jml.OptionsColumn.AllowEdit = True ' jml

        Else


            GridColumn1.OptionsColumn.AllowEdit = False 'akun_opex_bp
            GridColumn3.OptionsColumn.AllowEdit = False 'jenis opex
            GridColumn4.OptionsColumn.AllowEdit = False ' aktifitas
            GridColumn2.OptionsColumn.AllowEdit = False ' detail aktivity
            cl_jml.OptionsColumn.AllowEdit = False ' jml

        End If

    End Sub

    Private Sub delete_data()

        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count <= 0 Then
            Return
        End If

        Dim cn As OleDbConnection = Nothing
        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn


            Dim ceksql As String = String.Format("select jml_order,jml_pakai from ms_akun_detail where noid='{0}'", dv1(bs1.Position)("noid").ToString)
            Dim cmd As OleDbCommand = New OleDbCommand(ceksql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            Dim jmlorder As Double = 0
            Dim jmlpakai As Double = 0
            If drd.Read Then

                If IsNumeric(drd("jml_order").ToString) Then
                    jmlorder = drd("jml_order").ToString
                End If

                If IsNumeric(drd("jml_pakai").ToString) Then
                    jmlpakai = drd("jml_pakai").ToString
                End If

            End If
            drd.Close()

            dv1(bs1.Position)("jml_order") = jmlorder
            dv1(bs1.Position)("jml_pakai") = jmlpakai

            If jmlorder > 0 Or jmlpakai > 0 Then
                MsgBox("Tidak dapat dihapus karna sudah terpakai", vbOKOnly + vbExclamation, "Konfirmasi")
                Return
            End If

            If MsgBox("Yakin akan dihapus ?", vbYesNo + vbQuestion, "Konfirmasi") = MsgBoxResult.No Then
                Return
            End If

            Dim sqldel As String = String.Format("delete from ms_akun_detail where noid={0}", dv1(bs1.Position)("noid").ToString)
            Using cmddel As OleDbCommand = New OleDbCommand(sqldel, cn)
                cmddel.ExecuteNonQuery()
            End Using

            Clsmy.InsertToLog(cn, "btopex2", 0, 0, 1, 0, dv1(bs1.Position)("noid").ToString, "", Nothing)

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

    Private Sub fbudget_akun_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        tthn.Focus()
    End Sub

    Private Sub fbudget_akun_Load(sender As Object, e As EventArgs) Handles Me.Load

        Get_Aksesform()

        tthn.Text = Year(Now)

        open_data()

    End Sub

    Private Sub tthn_KeyDown(sender As Object, e As KeyEventArgs) Handles tthn.KeyDown
        If e.KeyCode = 13 Then
            open_data()
        End If
    End Sub

    Private Sub tsfind_Click(sender As Object, e As EventArgs) Handles tsfind.Click
        open_data()
    End Sub

    Private Sub tsdel_Click(sender As Object, e As EventArgs) Handles tsdel.Click
        delete_data()
    End Sub

    Private Sub tsadd_Click(sender As Object, e As EventArgs) Handles tsadd.Click

        With OpenFileDialog1
            .FileName = ""
            .Title = "Open a excel file.."
            .Filter = "Excel 2007 (*.xlsx)|*.xlsx|Excel 2003 (*.xls)|*.xls"
        End With

        Dim result As DialogResult = OpenFileDialog1.ShowDialog()


        If result = Windows.Forms.DialogResult.OK Then

            Dim path As String = OpenFileDialog1.FileName

            Try

                'cn = New OleDbConnection
                'cn = Clsmy.open_conn_excel(path)

                fbudget_akun2.StartPosition = FormStartPosition.CenterScreen
                fbudget_akun2.lokasi = path
                fbudget_akun2.ShowDialog()

                If fbudget_akun2.get_stat = True Then
                    open_data()
                End If


            Catch ex As Exception
                MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
            Finally

                'If Not cn Is Nothing Then
                'If cn.State = ConnectionState.Open Then
                'cn.Close()
                'End If
                'End If
            End Try

        End If

    End Sub

    Private Sub GridView1_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanged

        If IsNothing(dv1) Then
            Return
        End If

        If IsNothing(dv1) Then
            Return
        End If


        Dim noid As String = dv1(bs1.Position)("noid").ToString

        Dim cn As OleDbConnection = Nothing
        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            If e.Column.FieldName = "akun_opex_bp" Then

                Dim sql As String = String.Format("update ms_akun_detail set akun_opex_bp='{0}' where noid='{1}'", e.Value, noid)
                Using cmd As OleDbCommand = New OleDbCommand(sql, cn)
                    cmd.ExecuteNonQuery()
                End Using

                Clsmy.InsertToLog(cn, "btopex2", 0, 1, 0, 0, noid, e.Value, Nothing)

            ElseIf e.Column.FieldName = "jenis_opex" Then

                Dim sql As String = String.Format("update ms_akun_detail set jenis_opex='{0}' where noid='{1}'", e.Value, noid)
                Using cmd As OleDbCommand = New OleDbCommand(sql, cn)
                    cmd.ExecuteNonQuery()
                End Using

                Clsmy.InsertToLog(cn, "btopex2", 0, 1, 0, 0, noid, e.Value, Nothing)

            ElseIf e.Column.FieldName = "aktifitas" Then

                Dim sql As String = String.Format("update ms_akun_detail set aktifitas='{0}' where noid='{1}'", e.Value, noid)
                Using cmd As OleDbCommand = New OleDbCommand(sql, cn)
                    cmd.ExecuteNonQuery()
                End Using

                Clsmy.InsertToLog(cn, "btopex2", 0, 1, 0, 0, noid, e.Value, Nothing)

            ElseIf e.Column.FieldName = "detail_aktifitas" Then

                Dim sql As String = String.Format("update ms_akun_detail set detail_aktifitas='{0}' where noid='{1}'", e.Value, noid)
                Using cmd As OleDbCommand = New OleDbCommand(sql, cn)
                    cmd.ExecuteNonQuery()
                End Using

                Clsmy.InsertToLog(cn, "btopex2", 0, 1, 0, 0, noid, e.Value, Nothing)

            ElseIf e.Column.FieldName = "jml" Then

                Dim sql As String = String.Format("update ms_akun_detail set jml={0} where noid='{1}'", e.Value, noid)
                Using cmd As OleDbCommand = New OleDbCommand(sql, cn)
                    cmd.ExecuteNonQuery()
                End Using

                Dim sqlcek As String = String.Format("select jml,jml_order,jml_pakai from ms_akun_detail where noid='{0}'", noid)
                Dim cmdcek As OleDbCommand = New OleDbCommand(sqlcek, cn)
                Dim drdcek As OleDbDataReader = cmdcek.ExecuteReader

                Dim jml0 As Double = 0
                Dim order0 As Double = 0
                Dim pakai0 As Double = 0

                If drdcek.Read Then

                    If IsNumeric(drdcek("jml").ToString) Then

                        jml0 = drdcek("jml").ToString
                        order0 = drdcek("jml_order").ToString
                        pakai0 = drdcek("jml_pakai").ToString

                    End If

                End If
                drdcek.Close()

                Dim sisa As Double = jml0 - (order0 + pakai0)

                dv1(bs1.Position)("jml") = jml0
                dv1(bs1.Position)("jml_order") = order0
                dv1(bs1.Position)("jml_pakai") = pakai0
                dv1(bs1.Position)("sis_bu") = sisa

                Clsmy.InsertToLog(cn, "btopex2", 0, 1, 0, 0, noid, e.Value, Nothing)

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


    Private Sub ExportToExcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportToExcelToolStripMenuItem.Click
        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count <= 0 Then
            Return
        End If

        Dim fileName As String = ShowSaveFileDialog("Excel 2007", "Microsoft Excel|*.xlsx")

        If fileName = String.Empty Then
            Return
        End If

        GridView1.ExportToXlsx(fileName)
        OpenFile(fileName)
    End Sub

    Private Sub ExportToTextToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExportToTextToolStripMenuItem.Click
        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count <= 0 Then
            Return
        End If

        Dim fileName As String = ShowSaveFileDialog("Text Files", "Text Files|*.txt")

        If fileName = String.Empty Then
            Return
        End If

        GridView1.ExportToText(fileName)
        OpenFile(fileName)
    End Sub

End Class