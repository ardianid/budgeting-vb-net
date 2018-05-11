Imports System.Data
Imports System.Data.OleDb

Imports DevExpress.XtraReports.UI

Public Class fpengajuan


    Private bs1 As BindingSource
    Private dvmanager As Data.DataViewManager
    Private dv1 As Data.DataView

    Private dvmanager2 As Data.DataViewManager
    Private dv2 As Data.DataView

    Private dvmanager3 As Data.DataViewManager
    Private dv3 As Data.DataView

    Private Sub open()

        Dim tglsebelum As String = DateAdd(DateInterval.Month, -2, Date.Now)

        Dim sql As String = "select aju.nobukti,CONVERT(VARCHAR(10),aju.tanggal,103) as tanggal,bu.nama_bu,dep.nama_departemen,div.nama_divisi,aju.nama_pemohon,aju.non_b, " & _
        "aju.sbatal,aju.total,aju.jml_realisasi, " & _
        "case aju.jenis " & _
        "when 1 then 'ORDER' " & _
        "when 2 then 'COMPLETE' " & _
        "when 3 then 'CANCEL' end as jenis,aju.jml_print,aju.namauser,aju.cross_b,(aju.cross_b + aju.verif_cros + aju.sbatal) as cross_verif " & _
        "from tr_pengajuan aju inner join ms_bu bu on aju.idbu=bu.idbu " & _
        "inner join ms_divisi div on aju.kd_divisi=div.kd_divisi inner join ms_departemen dep on div.kd_depart=dep.kd_depart "

        Dim cn As OleDbConnection = Nothing

        grid1.DataSource = Nothing
        grid2.DataSource = Nothing
        grid3.DataSource = Nothing

        Try

            dv1 = Nothing

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            If st_alluserku = 2 Then
                sql = String.Format("{0} where aju.namauser='{1}' and aju.tanggal>='{2}' order by aju.tanggal desc", sql, userprog, convert_date_to_eng(tglsebelum))

            ElseIf st_alluserku = 3 Then

                Dim namauserlain As String = String.Format("'{0}'", userprog)

                Dim sqlc As String = String.Format("select namauser2 from ms_usersys6 where namauser='{0}'", userprog)
                Dim cmdc As OleDbCommand = New OleDbCommand(sqlc, cn)
                Dim drdc As OleDbDataReader = cmdc.ExecuteReader

                If drdc.HasRows Then
                    While drdc.Read

                        namauserlain = String.Format("{0},'{1}'", namauserlain, drdc("namauser2").ToString)

                    End While
                End If
                drdc.Close()


                sql = String.Format("{0} where aju.namauser in ({1}) and aju.tanggal>='{2}' order by aju.tanggal desc", sql, namauserlain, convert_date_to_eng(tglsebelum))
            ElseIf st_alluserku = 1 Then
                sql = String.Format("{0} where  aju.tanggal>='{1}' order by aju.tanggal desc", sql, convert_date_to_eng(tglsebelum))
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

    Private Sub cari()

        Dim sql As String = "select aju.nobukti,CONVERT(VARCHAR(10),aju.tanggal,103) as tanggal,bu.nama_bu,dep.nama_departemen,div.nama_divisi,aju.nama_pemohon,aju.non_b, " & _
        "aju.sbatal,aju.total,aju.jml_realisasi, " & _
        "case aju.jenis " & _
        "when 1 then 'ORDER' " & _
        "when 2 then 'COMPLETE' " & _
        "when 3 then 'CANCEL' end as jenis,aju.jml_print,aju.namauser,aju.cross_b,(aju.cross_b + aju.verif_cros + aju.sbatal) as cross_verif " & _
        "from tr_pengajuan aju inner join ms_bu bu on aju.idbu=bu.idbu " & _
        "inner join ms_divisi div on aju.kd_divisi=div.kd_divisi inner join ms_departemen dep on div.kd_depart=dep.kd_depart"

        Dim cn As OleDbConnection = Nothing

        grid1.DataSource = Nothing
        grid2.DataSource = Nothing
        grid3.DataSource = Nothing

        Try

            dv1 = Nothing

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            If tfind.Text.Trim.Length > 0 Then

                If tcbofind.SelectedIndex = 0 Then

                    sql = String.Format("{0} where aju.nobukti like '%{1}%'", sql, tfind.Text.Trim)

                ElseIf tcbofind.SelectedIndex = 1 Then

                    If Not IsDate(tfind.Text.Trim) Then
                        MsgBox("Format pencarian salah, harus tanggal (dd/mm/yyyy)", vbOKOnly + vbExclamation, "Informasi")
                        Return
                    End If

                    sql = String.Format("{0} where aju.tanggal='{1}'", sql, convert_date_to_eng(tfind.Text.Trim))

                ElseIf tcbofind.SelectedIndex = 2 Then

                    sql = String.Format("{0} where aju.nama_pemohon like '%{1}%'", sql, tfind.Text.Trim)

                End If

                If st_alluserku = 2 Then
                    sql = String.Format("{0} and aju.namauser='{1}'", sql, userprog)
                ElseIf st_alluserku = 3 Then

                    Dim namauserlain As String = String.Format("'{0}'", userprog)

                    Dim sqlc As String = String.Format("select namauser2 from ms_usersys6 where namauser='{0}'", userprog)
                    Dim cmdc As OleDbCommand = New OleDbCommand(sqlc, cn)
                    Dim drdc As OleDbDataReader = cmdc.ExecuteReader

                    If drdc.HasRows Then
                        While drdc.Read

                            namauserlain = String.Format("{0},'{1}'", namauserlain, drdc("namauser2").ToString)

                        End While
                    End If
                    drdc.Close()


                    sql = String.Format("{0} and aju.namauser in ({1})", sql, namauserlain)

                End If

            Else

                Dim tglsebelum As String = DateAdd(DateInterval.Month, -2, Date.Now)

                If st_alluserku = 2 Then
                    sql = String.Format("{0} where aju.namauser='{1}' and aju.tanggal>='{2}' order by aju.tanggal desc", sql, userprog, convert_date_to_eng(tglsebelum))

                ElseIf st_alluserku = 3 Then

                    Dim namauserlain As String = String.Format("'{0}'", userprog)

                    Dim sqlc As String = String.Format("select namauser2 from ms_usersys6 where namauser='{0}'", userprog)
                    Dim cmdc As OleDbCommand = New OleDbCommand(sqlc, cn)
                    Dim drdc As OleDbDataReader = cmdc.ExecuteReader

                    If drdc.HasRows Then
                        While drdc.Read

                            namauserlain = String.Format("{0},'{1}'", namauserlain, drdc("namauser2").ToString)

                        End While
                    End If
                    drdc.Close()


                    sql = String.Format("{0} where aju.namauser in ({1}) and aju.tanggal>='{2}' order by aju.tanggal desc", sql, namauserlain, convert_date_to_eng(tglsebelum))

                ElseIf st_alluserku = 1 Then
                    sql = String.Format("{0} where  aju.tanggal>='{1}' order by aju.tanggal desc", sql, convert_date_to_eng(tglsebelum))
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

    Private Sub open2()

        grid2.DataSource = Nothing

        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count <= 0 Then
            Return
        End If

        Dim sql As String = String.Format("select * from tr_pengajuan2 where nobukti='{0}'", dv1(bs1.Position)("nobukti").ToString)

        Dim cn As OleDbConnection = Nothing

        Try

            dv2 = Nothing

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim ds As DataSet
            ds = New DataSet()
            ds = Clsmy.GetDataSet(sql, cn)

            dvmanager2 = New DataViewManager(ds)
            dv2 = dvmanager2.CreateDataView(ds.Tables(0))

            grid2.DataSource = dv2

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

    Private Sub open3()

        grid3.DataSource = Nothing

        If IsNothing(dv1) Then
            Return
        End If

        If dv1.Count <= 0 Then
            Return
        End If

        Dim sql As String = String.Format("select cast(b.tahun as varchar(4)) + '/' + cast(b.bulan as varchar(2))  as thn_bln,c.nama_akun,b.akun_opex_bp,a.jml,a.jml_real,b.noid,d.nama_divisi,b.detail_aktifitas " & _
        "from tr_pengajuan3 a inner join ms_akun_detail b on a.noid_akund=b.noid inner join ms_akun c on b.kd_akun=c.kd_akun inner join ms_divisi d on b.kd_divisi=d.kd_divisi where a.nobukti='{0}'", dv1(bs1.Position)("nobukti").ToString)

        Dim cn As OleDbConnection = Nothing

        Try

            dv3 = Nothing

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim ds As DataSet
            ds = New DataSet()
            ds = Clsmy.GetDataSet(sql, cn)

            dvmanager3 = New DataViewManager(ds)
            dv3 = dvmanager3.CreateDataView(ds.Tables(0))

            grid3.DataSource = dv3

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

    Private Sub batal()

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

            Dim nobukti As String = dv1(bs1.Position)("nobukti").ToString
            Dim jmlreal As Double = 0
            Dim non_b As Integer = 0
            Dim njenis As Integer = 0

            Dim sqlcek As String = String.Format("select sbatal,jml_realisasi,non_b,jenis from tr_pengajuan where nobukti='{0}'", nobukti)
            Dim cmdcek As OleDbCommand = New OleDbCommand(sqlcek, cn, sqltrans)
            Dim drdcek As OleDbDataReader = cmdcek.ExecuteReader

            If drdcek.Read Then

                If Not (drdcek("sbatal").ToString = "") Then

                    jmlreal = drdcek("jml_realisasi").ToString
                    non_b = drdcek("non_b").ToString
                    njenis = drdcek("jenis").ToString

                    If drdcek("sbatal").ToString = 1 Then
                        dv1(bs1.Position)("sbatal") = 1
                        dv1(bs1.Position)("jenis") = "CANCEL"
                        sqltrans.Rollback()
                        MsgBox("Data sudah dibatalkan", vbOKOnly + vbInformation, "Informasi")
                        Return
                    End If

                End If

            End If
            drdcek.Close()

            If MsgBox("Yakin akan dibatalkan ???", vbYesNo + vbQuestion, "Konfirmasi") = MsgBoxResult.No Then
                sqltrans.Rollback()
                Return
            End If

            Dim alasan = InputBox("Alasan Batal : ", "Alasan")

            If alasan = "" Then
                sqltrans.Rollback()
                Return
            End If

            Dim sql As String = String.Format("update tr_pengajuan set jenis=3,sbatal=1,alasan_batal='{0}' where nobukti='{1}'", alasan, nobukti)
            Using cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                cmd.ExecuteNonQuery()
            End Using

            If non_b = 0 Then

                Dim sqlakun As String = String.Format("select * from tr_pengajuan3 where nobukti='{0}'", nobukti)
                Dim cmdakun As OleDbCommand = New OleDbCommand(sqlakun, cn, sqltrans)
                Dim drdakun As OleDbDataReader = cmdakun.ExecuteReader

                If drdakun.HasRows Then
                    While drdakun.Read

                        Dim sqlbalik As String = ""
                        If njenis = 1 Then
                            sqlbalik = String.Format("update ms_akun_detail set jml_order= jml_order - {0} where noid='{1}'", drdakun("jml_real").ToString, drdakun("noid_akund").ToString)
                        ElseIf njenis = 2 Then
                            sqlbalik = String.Format("update ms_akun_detail set jml_pakai= jml_pakai - {0} where noid='{1}'", drdakun("jml_real").ToString, drdakun("noid_akund").ToString)
                        End If

                        Using cmdbalik As OleDbCommand = New OleDbCommand(sqlbalik, cn, sqltrans)
                            cmdbalik.ExecuteNonQuery()
                        End Using

                    End While
                End If
                drdakun.Close()

            End If

            Clsmy.InsertToLog(cn, "btbeli", 0, 0, 1, 0, nobukti, "", sqltrans)

            sqltrans.Commit()
            dv1(bs1.Position)("sbatal") = 1
            dv1(bs1.Position)("jenis") = "CANCEL"

            MsgBox("Data dibatalkan", vbOKOnly + vbInformation, "Informasi")


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

    Private Sub undo_data()

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

            Dim nobukti As String = dv1(bs1.Position)("nobukti").ToString
            Dim jmlreal As Double = 0
            Dim non_b As Integer = 0
            Dim njenis As Integer = 0

            Dim sqlcek As String = String.Format("select sbatal,jml_realisasi,non_b,jenis from tr_pengajuan where nobukti='{0}'", nobukti)
            Dim cmdcek As OleDbCommand = New OleDbCommand(sqlcek, cn, sqltrans)
            Dim drdcek As OleDbDataReader = cmdcek.ExecuteReader

            If drdcek.Read Then

                If Not (drdcek("sbatal").ToString = "") Then

                    jmlreal = drdcek("jml_realisasi").ToString
                    non_b = drdcek("non_b").ToString
                    njenis = drdcek("jenis").ToString

                    If drdcek("sbatal").ToString = 1 Then
                        dv1(bs1.Position)("sbatal") = 1
                        dv1(bs1.Position)("jenis") = "CANCEL"
                        sqltrans.Rollback()
                        MsgBox("Data sudah dibatalkan", vbOKOnly + vbInformation, "Informasi")
                        Return
                    End If

                End If

            End If
            drdcek.Close()

            If njenis <> 2 Then
                MsgBox("Status transaksi masih open, transaksi dibatalkan", vbOKOnly + vbInformation, "Informasi")

                If njenis = 1 Then
                    dv1(bs1.Position)("jenis") = "OPEN"
                End If

                sqltrans.Rollback()
                Return
            End If

            If MsgBox("Yakin status akan dikembalikan ???", vbYesNo + vbQuestion, "Konfirmasi") = MsgBoxResult.No Then
                sqltrans.Rollback()
                Return
            End If

            Dim alasan = InputBox("Alasan Pengembalian status pengajuan budget : ", "Alasan")

            If alasan = "" Then
                sqltrans.Rollback()
                Return
            End If

            Dim sql As String = String.Format("update tr_pengajuan set alasan_balik='{0}',jenis=1 where nobukti='{1}'", alasan, nobukti)
            Using cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                cmd.ExecuteNonQuery()
            End Using

            If non_b = 0 Then

                Dim sqlakun As String = String.Format("select * from tr_pengajuan3 where nobukti='{0}'", nobukti)
                Dim cmdakun As OleDbCommand = New OleDbCommand(sqlakun, cn, sqltrans)
                Dim drdakun As OleDbDataReader = cmdakun.ExecuteReader

                If drdakun.HasRows Then
                    While drdakun.Read

                        Dim sqlbalik As String = ""
                        If njenis = 1 Then
                            ' sqlbalik = String.Format("update ms_akun_detail set jml_order= jml_order - {0} where noid='{1}'", drdakun("jml_real").ToString, drdakun("noid_akund").ToString)
                        ElseIf njenis = 2 Then
                            sqlbalik = String.Format("update ms_akun_detail set jml_pakai= jml_pakai - {0},jml_order=jml_order+{1} where noid='{2}'", drdakun("jml_real").ToString, drdakun("jml").ToString, drdakun("noid_akund").ToString)
                        End If

                        Using cmdbalik As OleDbCommand = New OleDbCommand(sqlbalik, cn, sqltrans)
                            cmdbalik.ExecuteNonQuery()
                        End Using

                    End While
                End If
                drdakun.Close()

            End If

            dv1(bs1.Position)("jenis") = "ORDER"

            Clsmy.InsertToLog(cn, "btbeli", 0, 1, 0, 0, nobukti, alasan, sqltrans)

            sqltrans.Commit()

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

        If stbalik_opex = 1 Then
            tsundo.Enabled = True
        Else
            tsundo.Enabled = False
        End If

    End Sub


    Private Sub tsref_Click(sender As Object, e As EventArgs) Handles tsref.Click
        tfind.Text = ""
        open()
    End Sub

    Private Sub fpengajuan_Load(sender As Object, e As EventArgs) Handles Me.Load

        tcbofind.SelectedIndex = 0

        Get_Aksesform()

        open()
    End Sub

    Private Sub tsfind_Click(sender As Object, e As EventArgs) Handles tsfind.Click
        cari()
    End Sub

    Private Sub tfind_KeyDown(sender As Object, e As KeyEventArgs) Handles tfind.KeyDown
        If e.KeyCode = 13 Then
            cari()
        End If
    End Sub

    Private Sub GridView1_Click(sender As Object, e As EventArgs) Handles GridView1.Click
        open2()
        open3()
    End Sub

    Private Sub GridView1_DoubleClick(sender As Object, e As EventArgs) Handles GridView1.DoubleClick
        tsedit_Click(sender, Nothing)
    End Sub

    Private Sub GridView1_FocusedRowChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs) Handles GridView1.FocusedRowChanged
        open2()
        open3()
    End Sub

    Private Sub tsdel_Click(sender As Object, e As EventArgs) Handles tsdel.Click
        batal()
    End Sub

    Private Sub tsadd_Click(sender As Object, e As EventArgs) Handles tsadd.Click
        Using fkar2 As New fpengajuan2 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = True, .position = 0}
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

        Dim cn As OleDbConnection = Nothing
        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = String.Format("select jenis from tr_pengajuan where nobukti='{0}'", dv1(bs1.Position)("nobukti").ToString)
            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            If drd.Read Then
                If Not (drd(0).ToString = "") Then

                    If drd(0).ToString = 3 Then
                        MsgBox("Data sudah dibatalkan, tidak bisa diedit", vbOKOnly + vbInformation, "Informasi")
                        Return
                    End If

                End If
            End If
            drd.Close()

        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
        Finally

            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If
        End Try

        Using fkar2 As New fpengajuan2 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv1, .addstat = False, .position = bs1.Position}
            fkar2.ShowDialog()
        End Using
    End Sub

    Private Sub print_(ByVal jenis As Integer)

        If IsNothing(dv1) Then
            Exit Sub
        End If

        If dv1.Count < 1 Then
            Exit Sub
        End If

        Dim cn As OleDbConnection = Nothing
        Dim sqltrans As OleDbTransaction = Nothing

        Dim nobukti As String = ""

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            sqltrans = cn.BeginTransaction

            nobukti = dv1(bs1.Position)("nobukti").ToString

            Dim jmlreal As Double = 0
            Dim non_b As Integer = 0
            Dim njenis As Integer = 0

            Dim sqlcek As String = String.Format("select sbatal,jml_realisasi,non_b,jenis,cross_b,verif_cros from tr_pengajuan where nobukti='{0}'", nobukti)
            Dim cmdcek As OleDbCommand = New OleDbCommand(sqlcek, cn, sqltrans)
            Dim drdcek As OleDbDataReader = cmdcek.ExecuteReader

            If drdcek.Read Then

                If Not (drdcek("sbatal").ToString = "") Then

                    If drdcek("cross_b").ToString = 1 Then
                        If drdcek("verif_cros").ToString = 0 Then
                            MsgBox("Transaksi cross budget, dan belum diverifikasi. proses dibatalkan", vbOKOnly + vbInformation, "Informasi")
                            Return
                        End If
                    End If

                    jmlreal = drdcek("jml_realisasi").ToString
                    non_b = drdcek("non_b").ToString
                    njenis = drdcek("jenis").ToString

                    If drdcek("sbatal").ToString = 1 Then
                        dv1(bs1.Position)("sbatal") = 1
                        dv1(bs1.Position)("jenis") = "CANCEL"
                        sqltrans.Rollback()
                        MsgBox("Data sudah dibatalkan", vbOKOnly + vbInformation, "Informasi")
                        Return
                    End If

                End If

            End If
            drdcek.Close()

        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
        Finally

            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If
        End Try

        If jenis = 1 Then

            Using fkar2 As New fpr_bukti_pengajuan With {.WindowState = FormWindowState.Maximized, .nobukti = nobukti}
                fkar2.ShowDialog(Me)
            End Using

            tambah_jmlprint(nobukti)

        Else

            Dim sql As String = String.Format("select a.nobukti,CONVERT(VARCHAR(10),a.tanggal,103) as tanggal,c.nama_bu,c.alamat_bu,c.telp_bu,d.nama_divisi,e.nama_departemen, " & _
            "a.total,a.jml_kas,b.nama_orderan,a.catatan, " & _
            "case a.non_b " & _
            "when 0 then 'BUDGETING' ELSE 'NON-BUDGETING' end as jenis_bu,b.spesifikasi,b.qty,b.satuan " & _
            "from tr_pengajuan a inner join tr_pengajuan2 b on a.nobukti=b.nobukti " & _
            "inner join ms_bu c on a.idbu=c.idbu inner join ms_divisi d on a.kd_divisi=d.kd_divisi " & _
            "inner join ms_departemen e on d.kd_depart=e.kd_depart where a.nobukti='{0}'", nobukti)

            Dim sql2 As String = String.Format("select b.noid,b.akun_opex_bp,b.detail_aktifitas,a.jml_real " & _
            "from tr_pengajuan3 a inner join ms_akun_detail b on a.noid_akund=b.noid where a.nobukti='{0}'", nobukti)

            Dim ds As DataSet = New ds_buktipengajuan
            ds = Clsmy.GetDataSet(sql, cn)

            Dim ds2 As DataSet = New ds_buktipengajuan2
            ds2 = Clsmy.GetDataSet(sql2, cn)

            Dim ops As New System.Drawing.Printing.PrinterSettings

            Dim rinvoice As New r_bukti_pengajuan() With {.DataSource = ds.Tables(0)}
            rinvoice.xuser.Text = String.Format("User : {0} | Tgl : {1}", userprog, Date.Now)
            rinvoice.DataMember = rinvoice.DataMember

            rinvoice.XrSubreport2.ReportSource = New r_bukti_pengajuan2
            rinvoice.XrSubreport2.ReportSource.DataSource = ds2.Tables(0)
            rinvoice.XrSubreport2.ReportSource.DataMember = rinvoice.XrSubreport2.ReportSource.DataMember

            rinvoice.PrinterName = ops.PrinterName
            rinvoice.CreateDocument(True)
            rinvoice.Print()

            tambah_jmlprint(nobukti)

        End If

    End Sub

    Private Sub tambah_jmlprint(ByVal nobukti As String)

        Dim cn As OleDbConnection = Nothing
        Dim sqltrans As OleDbTransaction = Nothing
        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            sqltrans = cn.BeginTransaction

            Dim sql As String = String.Format("select jml_print from tr_pengajuan where nobukti='{0}'", nobukti)
            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            If drd.Read Then
                If Not (drd(0).ToString = "") Then

                    Dim jmlada As Integer = drd(0).ToString

                    jmlada = jmlada + 1

                    Dim sqlu As String = String.Format("update tr_pengajuan set jml_print={0} where nobukti='{1}'", jmlada, nobukti)
                    Using cmdu As OleDbCommand = New OleDbCommand(sqlu, cn, sqltrans)
                        cmdu.ExecuteNonQuery()
                    End Using

                    Clsmy.InsertToLog(cn, "btbeli", 0, 0, 0, 1, nobukti, "", sqltrans)

                    dv1(bs1.Position)("jml_print") = jmlada


                End If
            End If
            drd.Close()

            sqltrans.Commit()

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

    Private Sub tsprint_Click(sender As Object, e As EventArgs) Handles tsprint.Click
        print_(1)
    End Sub

    Private Sub tsprint2_Click(sender As Object, e As EventArgs) Handles tsprint2.Click
        print_(2)
    End Sub

    Private Sub tsundo_Click(sender As Object, e As EventArgs) Handles tsundo.Click
        undo_data()
    End Sub

    Private Sub ToolStripButton1_Click(sender As Object, e As EventArgs) Handles ToolStripButton1.Click

        tfind.Text = ""
        Dim hasilsql As String = ""

        Using fkar2 As New fcustom_filter With {.StartPosition = FormStartPosition.CenterParent}
            fkar2.ShowDialog()

            hasilsql = fkar2.get_sql

        End Using

        If hasilsql.Length > 0 Then

            Dim cn As OleDbConnection = Nothing

            grid1.DataSource = Nothing

            Try

                dv1 = Nothing

                cn = New OleDbConnection
                cn = Clsmy.open_conn

                If st_alluserku = 2 Then
                    hasilsql = String.Format("{0} and aju.namauser='{1}'", hasilsql, userprog)
                ElseIf st_alluserku = 3 Then

                    Dim namauserlain As String = String.Format("'{0}'", userprog)

                    Dim sqlc As String = String.Format("select namauser2 from ms_usersys6 where namauser='{0}'", userprog)
                    Dim cmdc As OleDbCommand = New OleDbCommand(sqlc, cn)
                    Dim drdc As OleDbDataReader = cmdc.ExecuteReader

                    If drdc.HasRows Then
                        While drdc.Read

                            namauserlain = String.Format("{0},'{1}'", namauserlain, drdc("namauser2").ToString)

                        End While
                    End If
                    drdc.Close()


                    hasilsql = String.Format("{0} and aju.namauser in ({1})", hasilsql, namauserlain)

                End If

                Dim ds As DataSet = New DataSet()
                ds = Clsmy.GetDataSet(hasilsql, cn)

                dvmanager = New DataViewManager(ds)
                dv1 = dvmanager.CreateDataView(ds.Tables(0))

                bs1 = New BindingSource
                bs1.DataSource = dv1
                bn1.BindingSource = bs1

                grid1.DataSource = bs1

                grid2.DataSource = Nothing
                grid3.DataSource = Nothing

            Catch ex As OleDb.OleDbException
                MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
            Finally


                If Not cn Is Nothing Then
                    If cn.State = ConnectionState.Open Then
                        cn.Close()
                    End If
                End If

            End Try

        End If

    End Sub

    Private Sub grid1_Click(sender As Object, e As EventArgs) Handles grid1.Click

    End Sub
End Class