Imports System.Data
Imports System.Data.OleDb

Imports DevExpress.XtraReports.UI

Public Class fpengajuan2

    Public dv As DataView
    Public position As Integer
    Public addstat As Boolean

    Private dvmanager2 As Data.DataViewManager
    Private dv2 As Data.DataView

    Private dvmanager3 As Data.DataViewManager
    Private dv3 As Data.DataView

    Dim jmlada As Integer
    Dim jmltotal_sebelumnya As Double
    Dim jmlkas_sebelumnya As Double
    Dim jmlreal_sebelumnya As Double

    Private Sub kosongkan()
        tbukti.EditValue = "<< New >>"
        tmohon.EditValue = ""
        tnote.EditValue = ""
        ttot.EditValue = 0
        tkas.EditValue = 0
        treal.EditValue = 0

        open2()
        open3(tbukti.EditValue)

    End Sub

    Private Sub isi_nama_orderan_sub()

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = "select distinct nama_orderan from tr_pengajuan2"
            Dim dsgroup As DataSet = New DataSet()
            dsgroup = Clsmy.GetDataSet(sql, cn)

            Dim dt As DataTable = dsgroup.Tables(0)

            For i As Integer = 0 To dt.Rows.Count - 1
                rc_namaorder.Items.Add(dt.Rows(i)("nama_orderan").ToString)
            Next


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

    Private Sub isi_satuan_sub()

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = "select distinct satuan from tr_pengajuan2"
            Dim dsgroup As DataSet = New DataSet()
            dsgroup = Clsmy.GetDataSet(sql, cn)

            Dim dt As DataTable = dsgroup.Tables(0)

            For i As Integer = 0 To dt.Rows.Count - 1
                rc_satuan.Items.Add(dt.Rows(i)("satuan").ToString)
            Next


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

    Private Function cekbukti(ByVal cn As OleDbConnection, ByVal sqltrans As OleDbTransaction) As String


        Dim tahun As String = Year(ttgl.EditValue)
        tahun = Microsoft.VisualBasic.Right(tahun, 2)
        Dim bulan As String = Month(ttgl.EditValue)

        If bulan.Length = 1 Then
            bulan = "0" & bulan
        End If

        Dim bulantahun As String = String.Format("{0}{1}", tahun, bulan)

        Dim sql As String = String.Format("select max(nobukti) from tr_pengajuan where len(nobukti)=13 and nobukti like 'PBI.{0}%'", bulantahun)

        '   sql = String.Format(" {0} and tanggal<'2014/11/07'", sql)

        Dim cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
        Dim drd As OleDbDataReader = cmd.ExecuteReader

        Dim nilai As Integer = 0

        If drd.HasRows Then
            If drd.Read Then

                If Not drd(0).ToString.Equals("") Then
                    nilai = Microsoft.VisualBasic.Right(drd(0).ToString, 5)
                End If

            End If
        End If

        nilai = nilai + 1
        Dim kbukti As String = nilai

        Select Case kbukti.Length
            Case 1
                kbukti = "0000" & nilai
            Case 2
                kbukti = "000" & nilai
            Case 3
                kbukti = "00" & nilai
            Case 4
                kbukti = "0" & nilai
            Case Else
                kbukti = nilai
        End Select

        Return String.Format("PBI.{0}{1}{2}", tahun, bulan, kbukti)

    End Function

    Private Sub isi()

        Dim cn As OleDbConnection = Nothing
        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = String.Format("select aj.nobukti,aj.tanggal,aj.idbu,aj.kd_divisi,depart.nama_departemen,aj.nama_pemohon,aj.non_b, " & _
            "aj.catatan, aj.total, aj.jml_kas, aj.jml_realisasi,aj.jenis,aj.cross_b " & _
            "from tr_pengajuan aj inner join ms_divisi div on aj.kd_divisi=div.kd_divisi  " & _
            "inner join ms_departemen depart on div.kd_depart=depart.kd_depart where aj.nobukti='{0}'", dv(position)("nobukti").ToString)

            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            If drd.Read Then
                If drd("nobukti").ToString.Trim.Length > 0 Then

                    If drd("jenis").ToString = 2 Then
                        btsimpan.Enabled = False

                        btadd2.Enabled = False
                        btedit2.Enabled = False
                        btdel2.Enabled = False

                        GridColumn1.OptionsColumn.AllowEdit = False
                        GridColumn2.OptionsColumn.AllowEdit = False
                        GridColumn3.OptionsColumn.AllowEdit = False
                        GridColumn4.OptionsColumn.AllowEdit = False
                        btdel.Enabled = False

                        ttgl.Enabled = False
                        tunit.Enabled = False
                        tdivisi.Enabled = False
                        ttot.Enabled = False
                        tkas.Enabled = False
                        treal.Enabled = False
                        tmohon.Enabled = False

                    Else
                        btsimpan.Enabled = True

                        btadd2.Enabled = True
                        btedit2.Enabled = True
                        btdel2.Enabled = True

                        GridColumn1.OptionsColumn.AllowEdit = True
                        GridColumn2.OptionsColumn.AllowEdit = True
                        GridColumn3.OptionsColumn.AllowEdit = True
                        GridColumn4.OptionsColumn.AllowEdit = True
                        btdel.Enabled = True

                        'ComboBox di grid2

                        ttgl.Enabled = True
                        tunit.Enabled = True
                        tdivisi.Enabled = True
                        ttot.Enabled = True
                        tkas.Enabled = True
                        treal.Enabled = True
                        tmohon.EditValue = True

                    End If

                    tbukti.EditValue = drd("nobukti").ToString
                    ttgl.EditValue = convert_date_to_ind(drd("tanggal").ToString)
                    tmohon.EditValue = drd("nama_pemohon").ToString

                    If drd("non_b").ToString = 1 Then
                        cnonb.Checked = True
                    Else
                        cnonb.Checked = False
                    End If

                    If drd("cross_b").ToString = 1 Then
                        skross.Checked = True
                    Else
                        skross.Checked = False
                    End If


                    tunit.EditValue = Integer.Parse(drd("idbu").ToString)
                    tdivisi.EditValue = drd("kd_divisi").ToString
                    tdepart.EditValue = drd("nama_departemen").ToString
                    tnote.EditValue = drd("catatan").ToString

                    ttot.EditValue = drd("total").ToString
                    jmltotal_sebelumnya = drd("total").ToString

                    tkas.EditValue = drd("jml_kas").ToString
                    jmlkas_sebelumnya = drd("jml_kas").ToString

                    treal.EditValue = drd("jml_realisasi").ToString
                    jmlreal_sebelumnya = drd("jml_realisasi").ToString

                    open2()
                    open3(tbukti.EditValue)

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

    End Sub

    Private Sub open2()

        grid2.DataSource = Nothing

        Dim sql As String = String.Format("select noid,nama_orderan,spesifikasi,qty,satuan from tr_pengajuan2 where nobukti='{0}'", tbukti.EditValue)

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

    Private Sub open3(ByVal nobukti As String)

        grid3.DataSource = Nothing

        Dim sql As String = String.Format("select cast(b.tahun as varchar(4)) + '/' + cast(b.bulan as varchar(2))  as thn_bln,c.nama_akun,b.akun_opex_bp,a.jml,a.noid_akund,a.jml,a.jml_real,a.noid,b.detail_aktifitas " & _
        "from tr_pengajuan3 a inner join ms_akun_detail b on a.noid_akund=b.noid inner join ms_akun c on b.kd_akun=c.kd_akun where a.nobukti='{0}'", nobukti)

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

    Private Sub isi_unitusaha()
        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = String.Format("select * from ms_bu where idbu in ({0})", idbu_ku)
            Dim ds As DataSet = New DataSet()
            ds = Clsmy.GetDataSet(sql, cn)


            tunit.Properties.DataSource = ds.Tables(0)

            If idbu_defaultku.Length > 0 Then
                tunit.EditValue = Integer.Parse(idbu_defaultku)
            End If


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

    Private Sub isi_divisi()
        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = "select a.kd_divisi,a.nama_divisi,b.nama_departemen from ms_divisi a inner join ms_departemen b on a.kd_depart=b.kd_depart"

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
                    sql = String.Format("{0} where a.kd_divisi in ({1})", sql, kd_divaja)
                End If

            End If

            Dim ds As DataSet = New DataSet()
            ds = Clsmy.GetDataSet(sql, cn)

            tdivisi.Properties.DataSource = ds.Tables(0)

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

    Private Sub hapus2()

        If IsNothing(dv2) Then
            Return
        End If

        If dv2.Count <= 0 Then
            Return
        End If

        Dim cn As OleDbConnection = Nothing
        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim noid As String = dv2(Me.BindingContext(dv2).Position)("noid").ToString

            Dim sql As String = String.Format("delete from tr_pengajuan2 where noid={0}", noid)
            Using cmd As OleDbCommand = New OleDbCommand(sql, cn)
                cmd.ExecuteNonQuery()
            End Using

            dv2(Me.BindingContext(dv2).Position).Delete()


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

    Private Sub hapus3()

        If IsNothing(dv3) Then
            Return
        End If

        If dv3.Count <= 0 Then
            Return
        End If

        Dim cn As OleDbConnection = Nothing
        Dim sqltrans As OleDbTransaction = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            sqltrans = cn.BeginTransaction

            Dim noid As String = dv3(Me.BindingContext(dv3).Position)("noid").ToString

            Dim sqlsel As String = String.Format("select a.jml_real,a.noid_akund,b.jenis from tr_pengajuan3 a inner join tr_pengajuan b on a.nobukti=b.nobukti where a.noid='{0}'", noid)
            Dim cmdsel As OleDbCommand = New OleDbCommand(sqlsel, cn, sqltrans)
            Dim drdsel As OleDbDataReader = cmdsel.ExecuteReader

            If drdsel.Read Then
                If IsNumeric(drdsel("jml_real").ToString) Then

                    Dim sqlbalik As String = ""
                    If drdsel("jenis").ToString = 1 Then
                        sqlbalik = String.Format("update ms_akun_detail set jml_order= jml_order - {0} where noid='{1}'", drdsel("jml_real").ToString, drdsel("noid_akund").ToString)
                    ElseIf drdsel("jenis").ToString = 2 Then
                        sqlbalik = String.Format("update ms_akun_detail set jml_pakai= jml_pakai - {0} where noid='{1}'", drdsel("jml_real").ToString, drdsel("noid_akund").ToString)
                    End If

                    Using cmdbalik As OleDbCommand = New OleDbCommand(sqlbalik, cn, sqltrans)
                        cmdbalik.ExecuteNonQuery()
                    End Using

                End If
            End If
            drdsel.Close()

            Dim sql As String = String.Format("delete from tr_pengajuan3 where noid={0}", noid)
            Using cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                cmd.ExecuteNonQuery()
            End Using

            If addstat Then
                treal.EditValue = treal.EditValue - Double.Parse(dv3(Me.BindingContext(dv3).Position)("jml_real").ToString)
            Else

                If dv(position)("jenis").ToString = "ORDER" Then
                    treal.EditValue = treal.EditValue - Double.Parse(dv3(Me.BindingContext(dv3).Position)("jml_real").ToString)
                End If

            End If

            ttot.EditValue = ttot.EditValue - Double.Parse(dv3(Me.BindingContext(dv3).Position)("jml_real").ToString)

            dv3(Me.BindingContext(dv3).Position).Delete()

            sqltrans.Commit()

        Catch ex As Exception
            sqltrans.Rollback()
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

        orow("nobukti") = tbukti.EditValue
        orow("tanggal") = convert_date_to_ind(ttgl.EditValue)
        orow("nama_bu") = tunit.Text
        orow("nama_departemen") = tdepart.EditValue
        orow("nama_divisi") = tdivisi.Text
        orow("nama_pemohon") = tmohon.EditValue
        orow("non_b") = IIf(cnonb.Checked, 1, 0)
        orow("sbatal") = 0
        orow("total") = ttot.EditValue
        orow("jml_realisasi") = treal.EditValue
        orow("jenis") = "ORDER"
        orow("jml_print") = jmlada
        orow("namauser") = userprog

        If skross.Checked = True Then
            orow("cross_b") = 1
            orow("cross_verif") = 1
        Else
            orow("cross_b") = 0
            orow("cross_verif") = 0
        End If

        dv.EndInit()
    End Sub

    Private Sub updateview(ByVal jenis As Integer)

        dv(position)("nama_bu") = tunit.Text
        dv(position)("tanggal") = convert_date_to_ind(ttgl.EditValue)
        dv(position)("nama_departemen") = tdepart.EditValue
        dv(position)("nama_divisi") = tdivisi.Text
        dv(position)("nama_pemohon") = tmohon.EditValue
        dv(position)("non_b") = IIf(cnonb.Checked, 1, 0)
        dv(position)("total") = ttot.EditValue
        dv(position)("jml_realisasi") = treal.EditValue

        If jenis = 1 Then
            dv(position)("jenis") = "ORDER"
        Else
            dv(position)("jenis") = "COMPLETE"
        End If

    End Sub

    Private Sub simpan(ByVal isijenis As Integer)

        Dim cn As OleDbConnection = Nothing
        Dim sqltrans As OleDbTransaction = Nothing
        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim isikross As Integer = 0
            If skross.Checked = True Then
                isikross = 1
            End If

            '' cek kalo kross
            If addstat = False Then

                If isijenis = 2 Then

                    If isikross = 1 Then


                        Dim adaperubahan As Boolean = False

                        If (Double.Parse(ttot.EditValue) > jmltotal_sebelumnya) Then
                            adaperubahan = True
                        End If

                        If (Double.Parse(tkas.EditValue) > jmlkas_sebelumnya) Then
                            'adaperubahan = True
                        End If

                        If (Double.Parse(treal.EditValue) > jmlreal_sebelumnya) Then
                            adaperubahan = True
                        End If


                        If adaperubahan Then
                            If st_divku <> 1 Then
                                If st_crossbudget = 1 Then

                                    Dim sqlc As String = String.Format("select distinct nobukti from tr_pengajuan3 a3 inner join ms_akun_detail b3 on a3.noid_akund=b3.noid " & _
                                    "where b3.kd_divisi in (select kd_divisi from ms_usersys5 where namauser='{0}') and nobukti='{1}'", userprog, tbukti.EditValue)

                                    Dim cmdc As OleDbCommand = New OleDbCommand(sqlc, cn)
                                    Dim drdc As OleDbDataReader = cmdc.ExecuteReader
                                    If drdc.Read Then
                                        If drdc("nobukti").ToString.Length > 0 Then

                                            Dim sqlup As String = String.Format("update tr_pengajuan set verif_cros=1,nama_verif='{0}' where nobukti='{1}'", userprog, tbukti.EditValue)
                                            Using cmdup As OleDbCommand = New OleDbCommand(sqlup, cn)
                                                cmdup.ExecuteNonQuery()
                                            End Using

                                        Else
                                            GoTo masuk_lock
                                        End If
                                    Else
                                        GoTo masuk_lock
                                    End If
                                    drdc.Close()

                                Else

masuk_lock:

                                    Dim sqlcek_sebelumremove As String = String.Format("select nama_verif,jml_print from tr_pengajuan where nobukti='{0}'", tbukti.EditValue)
                                    Dim cmdcek_Sebelumremove As OleDbCommand = New OleDbCommand(sqlcek_sebelumremove, cn)
                                    Dim drdcek_sebelumremove As OleDbDataReader = cmdcek_Sebelumremove.ExecuteReader

                                    Dim useracc_sebelumremove As String = ""
                                    If drdcek_sebelumremove.Read Then
                                        If drdcek_sebelumremove("nama_verif").ToString.Length > 0 Then
                                            useracc_sebelumremove = drdcek_sebelumremove("nama_verif").ToString

                                        End If

                                        If IsNumeric(drdcek_sebelumremove("jml_print").ToString) Then
                                            If Integer.Parse(drdcek_sebelumremove("jml_print").ToString) = 0 Then
                                                adaperubahan = False
                                            End If
                                        End If

                                    End If
                                    drdcek_sebelumremove.Close()

                                    Dim sqlremove_verif As String = String.Format("update tr_pengajuan set verif_cros=0,nama_verif=null,total_c={0},jml_kas_c={1},jml_realisasi_c={2} where nobukti='{3}'", Replace(ttot.EditValue, ",", "."), Replace(tkas.EditValue, ",", "."), Replace(treal.EditValue, ",", "."), tbukti.EditValue)
                                    Using cmdremove_verif As OleDbCommand = New OleDbCommand(sqlremove_verif, cn)
                                        cmdremove_verif.ExecuteNonQuery()
                                    End Using

                                    Using fkar2 As New flock_cross With {.StartPosition = FormStartPosition.CenterParent, .nobukti = tbukti.EditValue}
                                        fkar2.ShowDialog()

                                        If fkar2.get_status = False Then

                                            If useracc_sebelumremove.Length > 0 Then

                                                Dim sqladd_verif As String = String.Format("update tr_pengajuan set verif_cros=1,nama_verif='{0}' where nobukti='{1}'", useracc_sebelumremove, tbukti.EditValue)
                                                Using cmdadd_verif As OleDbCommand = New OleDbCommand(sqladd_verif, cn)
                                                    cmdadd_verif.ExecuteNonQuery()
                                                End Using

                                            End If

                                            MsgBox("Transaksi ini tidak tidak disetujui adanya perubahan, proses dibatalkan", vbOKOnly + vbInformation, "Informasi")
                                            Return

                                        End If

                                    End Using

                                End If


                            Else ''kalau dia boleh add divisi

                                Dim sqlup As String = String.Format("update tr_pengajuan set verif_cros=1,nama_verif='{0}' where nobukti='{1}'", userprog, tbukti.EditValue)
                                Using cmdup As OleDbCommand = New OleDbCommand(sqlup, cn)
                                    cmdup.ExecuteNonQuery()
                                End Using

                            End If

                        End If

                        End If

                    End If

                End If
                '' end klo kross

                sqltrans = cn.BeginTransaction

                Dim isi_nonb As Integer = 0
                If cnonb.Checked Then
                    isi_nonb = 1
                End If

                If addstat Then

                    Dim bukti As String = cekbukti(cn, sqltrans)
                    tbukti.EditValue = bukti

                End If

                '' simpan2 

                For i As Integer = 0 To dv2.Count - 1

                If dv2(i)("qty").ToString = "" Then
                    MsgBox("qty harus diisi...", vbOKOnly + vbExclamation, "Informasi")
                    GoTo langsung_keluar
                End If

                If Not IsNumeric(dv2(i)("qty").ToString) Then
                    MsgBox("qty harus harus angka", vbOKOnly + vbExclamation, "Informasi")
                    GoTo langsung_keluar
                End If

                If dv2(i)("satuan").ToString = "" Then
                    MsgBox("satuan harus diisi...", vbOKOnly + vbExclamation, "Informasi")
                    GoTo langsung_keluar
                End If

                If Integer.Parse(dv2(i)("noid").ToString) = 0 Then

                    Dim sql As String = String.Format("insert into tr_pengajuan2 (nobukti,nama_orderan,spesifikasi,qty,satuan) values('{0}','{1}','{2}',{3},'{4}')", tbukti.EditValue, dv2(i)("nama_orderan").ToString.ToUpper, dv2(i)("spesifikasi").ToString.ToUpper, dv2(i)("qty").ToString, dv2(i)("satuan").ToString.ToUpper)
                    Using cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                        cmd.ExecuteNonQuery()
                    End Using

                Else

                    Dim sql As String = String.Format("update tr_pengajuan2 set nama_orderan='{0}',spesifikasi='{1}',qty={2},satuan='{3}' where noid={4}", dv2(i)("nama_orderan").ToString.ToUpper, dv2(i)("spesifikasi").ToString.ToUpper, dv2(i)("qty").ToString, dv2(i)("satuan").ToString.ToUpper, dv2(i)("noid").ToString)
                    Using cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                        cmd.ExecuteNonQuery()
                    End Using

                End If

                Next

                '' akhir simpan 2

                '' simpan 3

                If isi_nonb = 0 Then



                    If addstat = False Then

                        Dim sqlcek As String = String.Format("select a.noid_akund,a.jml_real,b.jenis from tr_pengajuan3 a inner join tr_pengajuan b on a.nobukti=b.nobukti where a.nobukti='{0}'", tbukti.EditValue)
                        Dim cmdcek As OleDbCommand = New OleDbCommand(sqlcek, cn, sqltrans)
                        Dim drdcek As OleDbDataReader = cmdcek.ExecuteReader

                        Dim sqlupcek As String = ""

                        If drdcek.HasRows Then
                            While drdcek.Read

                                If drdcek("jenis").ToString = 1 Then
                                sqlupcek = String.Format("update ms_akun_detail set [jml_order]= [jml_order] - {0} where [noid]='{1}'", drdcek("jml_real").ToString, drdcek("noid_akund").ToString)
                                ElseIf drdcek("jenis").ToString = 2 Then
                                sqlupcek = String.Format("update ms_akun_detail set [jml_pakai]= [jml_pakai] - {0} where [noid]='{1}'", drdcek("jml_real").ToString, drdcek("noid_akund").ToString)
                                End If

                                Using cmdupcek As OleDbCommand = New OleDbCommand(sqlupcek, cn, sqltrans)
                                    cmdupcek.ExecuteNonQuery()
                                End Using

                            End While
                        End If
                        drdcek.Close()


                    End If

                    Dim bln As Integer = Month(ttgl.EditValue)
                    Dim thn As Integer = Year(ttgl.EditValue)

                    For n As Integer = 0 To dv3.Count - 1

                        Dim thn3 As String = dv3(n)("thn_bln").ToString.Trim.Substring(0, 4)
                        Dim bln3 As String = ""


                        If dv3(n)("thn_bln").ToString.Trim.Length = 6 Then
                            bln3 = dv3(n)("thn_bln").ToString.Trim.Substring(5, 1)
                        Else
                            bln3 = dv3(n)("thn_bln").ToString.Trim.Substring(5, 2)
                        End If


                        If Integer.Parse(thn3) < thn Then
                            MsgBox("Tahun opex tidak boleh kecil dari tanggal pengajuan", vbOKOnly + vbEmpty, "Informasi")
                            sqltrans.Rollback()
                            Return
                        End If

                        Dim salahbln As Boolean = False
                        If Integer.Parse(bln3) < bln Then
                            If Integer.Parse(dv3(n)("thn_bln").ToString.Trim.Substring(1, 4)) <= thn Then
                                salahbln = True
                            End If
                        End If

                        If salahbln Then
                            MsgBox("Bulan opex tidak boleh kecil dari tanggal pengajuan", vbOKOnly + vbEmpty, "Informasi")
                            sqltrans.Rollback()
                            Return
                        End If

                        Dim sql_selisih As String = String.Format("select jml - (jml_order+jml_pakai) as sisa from ms_akun_detail where noid='{0}'", dv3(n)("noid_akund").ToString)
                        Dim cmdsel As OleDbCommand = New OleDbCommand(sql_selisih, cn, sqltrans)
                        Dim drdsel As OleDbDataReader = cmdsel.ExecuteReader

                        Dim jmlsisa As Double = 0
                        If drdsel.Read Then
                            If IsNumeric(drdsel("sisa").ToString) Then
                                jmlsisa = Double.Parse(drdsel("sisa").ToString)
                            End If
                        End If
                        drdsel.Close()

                    'If Integer.Parse(dv3(n)("noid").ToString) <> 0 Then

                    '    Dim sql3_sebelum As String = String.Format("select jml_real from tr_pengajuan3 where noid={0} and noid_akund='{1}'", dv3(n)("noid").ToString, dv3(n)("noid_akund").ToString)
                    '    Dim cmd3_sebelum As OleDbCommand = New OleDbCommand(sql3_sebelum, cn, sqltrans)
                    '    Dim drd3_sebelum As OleDbDataReader = cmd3_sebelum.ExecuteReader

                    '    If drd3_sebelum.Read Then
                    '        If IsNumeric(drd3_sebelum(0).ToString) Then
                    '            jmlsisa = jmlsisa + Double.Parse(drd3_sebelum(0).ToString)
                    '        End If
                    '    End If
                    '    drd3_sebelum.Close()
                    'End If


                        If jmlsisa < Double.Parse(dv3(n)("jml_real").ToString) Then
                            MsgBox("Sisa Budget open sudah tidak mencukupi", vbOKOnly + vbExclamation, "Informasi")
                            sqltrans.Rollback()
                            Return
                        End If

                        If Integer.Parse(dv3(n)("noid").ToString) = 0 Then

                            Dim sql As String = String.Format("insert into tr_pengajuan3 (nobukti,noid_akund,jml,jml_real) values ('{0}','{1}',{2},{3})", _
                                                             tbukti.EditValue, dv3(n)("noid_akund").ToString, dv3(n)("jml").ToString, dv3(n)("jml_real").ToString)
                            Using cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                                cmd.ExecuteNonQuery()
                            End Using

                            Dim sqlbalik As String = ""
                            If isijenis = 1 Then
                                sqlbalik = String.Format("update ms_akun_detail set jml_order= jml_order + {0} where noid='{1}'", dv3(n)("jml_real").ToString, dv3(n)("noid_akund").ToString)
                            ElseIf isijenis = 2 Then
                                sqlbalik = String.Format("update ms_akun_detail set jml_pakai= jml_pakai + {0} where noid='{1}'", dv3(n)("jml_real").ToString, dv3(n)("noid_akund").ToString)
                            End If

                            Using cmdbalik As OleDbCommand = New OleDbCommand(sqlbalik, cn, sqltrans)
                                cmdbalik.ExecuteNonQuery()
                            End Using

                        Else


                            Dim sqlbalik As String = ""
                            If isijenis = 1 Then

                                sqlbalik = String.Format("update ms_akun_detail set jml_order= jml_order + {0} where noid='{1}'", dv3(n)("jml_real").ToString, dv3(n)("noid_akund").ToString)

                                Using cmdbalik As OleDbCommand = New OleDbCommand(sqlbalik, cn, sqltrans)
                                    cmdbalik.ExecuteNonQuery()
                                End Using

                            ElseIf isijenis = 2 Then

                                Dim sqlcek2 As String = String.Format("select a.noid_akund,a.jml,b.jenis from tr_pengajuan3 a inner join tr_pengajuan b on a.nobukti=b.nobukti where a.noid='{0}'", dv3(n)("noid").ToString)
                                Dim cmdcek2 As OleDbCommand = New OleDbCommand(sqlcek2, cn, sqltrans)
                                Dim drdcek2 As OleDbDataReader = cmdcek2.ExecuteReader

                                If drdcek2.Read Then
                                    If IsNumeric(drdcek2("noid_akund").ToString) Then

                                        Dim sqlbalikinlagi As String = String.Format("update ms_akun_detail set jml_order= jml_order - {0} where noid='{1}'", drdcek2("jml").ToString, dv3(n)("noid_akund").ToString)
                                        Using cmdbalikinlagi As OleDbCommand = New OleDbCommand(sqlbalikinlagi, cn, sqltrans)
                                            cmdbalikinlagi.ExecuteNonQuery()
                                        End Using

                                    End If
                                End If
                                drdcek2.Close()

                                sqlbalik = String.Format("update ms_akun_detail set jml_pakai= jml_pakai + {0} where noid='{1}'", dv3(n)("jml_real").ToString, dv3(n)("noid_akund").ToString)

                                Using cmdbalik As OleDbCommand = New OleDbCommand(sqlbalik, cn, sqltrans)
                                    cmdbalik.ExecuteNonQuery()
                                End Using

                            End If

                            Dim sql As String = ""
                            If isijenis = 1 Then
                                sql = String.Format("update tr_pengajuan3 set jml={0},jml_real={0} where noid={1}", dv3(n)("jml_real").ToString, dv3(n)("noid").ToString)
                            Else
                                sql = String.Format("update tr_pengajuan3 set jml_real={0} where noid={1}", dv3(n)("jml_real").ToString, dv3(n)("noid").ToString)
                            End If

                            Using cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                                cmd.ExecuteNonQuery()
                            End Using

                        End If

                    Next

                End If

                '' akhir simpan 3


                '' simpan 1



                If addstat Then

                    Dim sql As String = ""

                If isikross = 1 Then

                    If st_crossbudget = 1 Then

                        If st_divku = 1 Then
                            sql = String.Format("insert into tr_pengajuan(nobukti,tanggal,kd_divisi,nama_pemohon,catatan,idbu,non_b,total,jml_kas,jml_realisasi,jenis,namauser,cross_b,verif_cros,nama_verif,total_c,jml_kas_c,jml_realisasi_c) values ('{0}','{1}','{2}','{3}','{4}',{5},{6},{7},{8},{9},{10},'{11}',{12},1,'{13}',{7},{8},{9})", _
                            tbukti.EditValue, convert_date_to_eng(ttgl.EditValue), tdivisi.EditValue, tmohon.EditValue, tnote.EditValue, tunit.EditValue, isi_nonb, Replace(ttot.EditValue, ",", "."), Replace(tkas.EditValue, ",", "."), Replace(treal.EditValue, ",", "."), isijenis, userprog, isikross, userprog)
                        Else

                            Dim sqlc As String = String.Format("select distinct nobukti from tr_pengajuan3 a3 inner join ms_akun_detail b3 on a3.noid_akund=b3.noid " & _
                                    "where b3.kd_divisi in (select kd_divisi from ms_usersys5 where namauser='{0}') and nobukti='{1}'", userprog, tbukti.EditValue)

                            Dim cmdc As OleDbCommand = New OleDbCommand(sqlc, cn, sqltrans)
                            Dim drdc As OleDbDataReader = cmdc.ExecuteReader
                            If drdc.Read Then
                                If drdc("nobukti").ToString.Length > 0 Then

                                    sql = String.Format("insert into tr_pengajuan(nobukti,tanggal,kd_divisi,nama_pemohon,catatan,idbu,non_b,total,jml_kas,jml_realisasi,jenis,namauser,cross_b,verif_cros,nama_verif,total_c,jml_kas_c,jml_realisasi_c) values ('{0}','{1}','{2}','{3}','{4}',{5},{6},{7},{8},{9},{10},'{11}',{12},1,'{13}',{7},{8},{9})", _
                                    tbukti.EditValue, convert_date_to_eng(ttgl.EditValue), tdivisi.EditValue, tmohon.EditValue, tnote.EditValue, tunit.EditValue, isi_nonb, Replace(ttot.EditValue, ",", "."), Replace(tkas.EditValue, ",", "."), Replace(treal.EditValue, ",", "."), isijenis, userprog, isikross, userprog)

                                Else
                                    GoTo goto_kayabiasa
                                End If
                            Else
                                GoTo goto_kayabiasa
                            End If
                            drdc.Close()

                        End If

                        
                    Else

                        GoTo goto_kayabiasa

                    End If
                Else

goto_kayabiasa:

                    sql = String.Format("insert into tr_pengajuan(nobukti,tanggal,kd_divisi,nama_pemohon,catatan,idbu,non_b,total,jml_kas,jml_realisasi,jenis,namauser,cross_b,total_c,jml_kas_c,jml_realisasi_c) values ('{0}','{1}','{2}','{3}','{4}',{5},{6},{7},{8},{9},{10},'{11}',{12},{7},{8},{9})", _
                        tbukti.EditValue, convert_date_to_eng(ttgl.EditValue), tdivisi.EditValue, tmohon.EditValue, tnote.EditValue, tunit.EditValue, isi_nonb, Replace(ttot.EditValue, ",", "."), Replace(tkas.EditValue, ",", "."), Replace(treal.EditValue, ",", "."), isijenis, userprog, isikross)

                End If

                    Using cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                        cmd.ExecuteNonQuery()
                    End Using

                    Clsmy.InsertToLog(cn, "btbeli", 1, 0, 0, 0, tbukti.EditValue, "", sqltrans)

                Else

                    Dim sql As String = ""

                    sql = String.Format("update tr_pengajuan set tanggal='{0}',kd_divisi='{1}',nama_pemohon='{2}',catatan='{3}',idbu={4},total={5},jml_kas={6},jml_realisasi={7},total_c={5},jml_kas_c={6},jml_realisasi_c={7},jenis={8} where nobukti='{9}'", _
                                                 convert_date_to_eng(ttgl.EditValue), tdivisi.EditValue, tmohon.EditValue, tnote.EditValue, tunit.EditValue, Replace(ttot.EditValue, ",", "."), Replace(tkas.EditValue, ",", "."), Replace(treal.EditValue, ",", "."), isijenis, tbukti.EditValue)


                    Using cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                        cmd.ExecuteNonQuery()
                    End Using

                    Clsmy.InsertToLog(cn, "btbeli", 0, 1, 0, 0, tbukti.EditValue, "", sqltrans)

                End If
                '' akhir simpan 1

                sqltrans.Commit()

                If addstat Then

                    If isikross = 0 Then
                        If MsgBox("Data disimpan, akan langsung dicetak bukti ?", vbYesNo + vbQuestion, "Konfirmasi") = MsgBoxResult.Yes Then
                            print_(cn)
                        End If
                    End If

                    insertview()

                    kosongkan()

                    set_autocomplete_pemohon()
                isi_nama_orderan_sub()
                isi_satuan_sub()

                    tbukti.Focus()

                Else

                    MsgBox("Data dirubah", vbOKOnly + vbInformation, "Informasi")
                    updateview(isijenis)
                    Me.Close()

                End If

langsung_keluar:

        Catch ex As Exception
            sqltrans.Rollback()
            MsgBox(ex.ToString)
        Finally

            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If
        End Try

    End Sub

    Private Sub print_(ByVal cn As OleDbConnection)


        Dim nobukti As String = tbukti.EditValue

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

        tambah_jmlprint(nobukti, cn)

    End Sub

    Private Sub tambah_jmlprint(ByVal nobukti As String, ByVal cn As OleDbConnection)

        Dim sql As String = String.Format("select jml_print from tr_pengajuan where nobukti='{0}'", nobukti)
            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            If drd.Read Then
                If Not (drd(0).ToString = "") Then

                jmlada = drd(0).ToString

                    jmlada = jmlada + 1

                    Dim sqlu As String = String.Format("update tr_pengajuan set jml_print={0} where nobukti='{1}'", jmlada, nobukti)
                    Using cmdu As OleDbCommand = New OleDbCommand(sqlu, cn)
                        cmdu.ExecuteNonQuery()
                    End Using

                Clsmy.InsertToLog(cn, "btbeli", 0, 0, 0, 1, tbukti.EditValue, "", Nothing)

                End If
            End If
            drd.Close()

        

    End Sub


    Private Sub set_autocomplete_pemohon()
        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = "select distinct nama_pemohon from tr_pengajuan"
            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            Dim aucom As New AutoCompleteStringCollection
            While drd.Read
                aucom.Add(drd(0).ToString)
            End While
            drd.Close()

            tmohon.MaskBox.AutoCompleteSource = AutoCompleteSource.CustomSource
            tmohon.MaskBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend
            tmohon.MaskBox.AutoCompleteCustomSource = aucom

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


    Private Sub tdivisi_EditValueChanged(sender As Object, e As EventArgs) Handles tdivisi.EditValueChanged

        Dim cn As OleDbConnection = Nothing
        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = String.Format("select b.nama_departemen from ms_divisi a inner join ms_departemen b on a.kd_depart=b.kd_depart where kd_divisi='{0}'", tdivisi.EditValue)
            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            tdepart.EditValue = ""
            If drd.Read Then
                tdepart.EditValue = drd(0).ToString
            End If
            drd.Close()

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

    Private Sub fpengajuan2_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        ttgl.Focus()
    End Sub

    Private Sub fpengajuan2_Load(sender As Object, e As EventArgs) Handles Me.Load

        ttgl.EditValue = Now

        isi_unitusaha()
        isi_divisi()

        isi_nama_orderan_sub()
        isi_satuan_sub()

        set_autocomplete_pemohon()

        If addstat = False Then

            isi()

            cnonb.Enabled = False
            skross.Enabled = False

        Else

            btsimpan2.Enabled = False

            kosongkan()

        End If



    End Sub

    Private Sub cnonb_CheckedChanged(sender As Object, e As EventArgs) Handles cnonb.CheckedChanged

        open3("ini kosong")
        If cnonb.Checked = True Then

            SplitContainerControl1.Panel2.Enabled = False

        Else

            SplitContainerControl1.Panel2.Enabled = True

        End If

    End Sub

    Private Sub btdel_Click(sender As Object, e As EventArgs) Handles btdel.Click
        hapus2()
    End Sub

    Private Sub btdel2_Click(sender As Object, e As EventArgs) Handles btdel2.Click
        hapus3()
    End Sub

    Private Sub GridView1_CellValueChanged(sender As Object, e As DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs) Handles GridView1.CellValueChanged

        If IsNothing(dv2) Then
            Return
        End If

        If dv2.Count <= 0 Then
            Return
        End If

        If e.Column.FieldName = "nama_orderan" Then

            Dim noid As String = dv2(Me.BindingContext(dv2).Position)("noid").ToString
            If noid = "" Then
                dv2(Me.BindingContext(dv2).Position)("noid") = 0
            End If

        End If

    End Sub

    Private Sub btadd2_Click(sender As Object, e As EventArgs) Handles btadd2.Click

        Dim stkross As Integer = 0
        If skross.Checked = True Then
            stkross = 1
        End If

        Using fkar2 As New fpengajuan3 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv3, .addstat = True, .position = 0, .kd_divisi = tdivisi.EditValue, .tahun = Year(ttgl.EditValue), .bulan = Month(ttgl.EditValue), .idbu = tunit.EditValue, .jenis = "", .stkross = stkross}
            fkar2.ShowDialog()
        End Using

        Dim jml As Double = 0
        For i As Integer = 0 To dv3.Count - 1
            jml = jml + dv3(i)("jml_real").ToString
        Next

        ttot.EditValue = jml

        If addstat Then
            treal.EditValue = jml
        Else

            If dv(position)("jenis").ToString = "ORDER" Then
                treal.EditValue = jml
            End If

        End If

    End Sub
    Private Sub btedit2_Click(sender As Object, e As EventArgs) Handles btedit2.Click

        If IsNothing(dv3) Then
            Return
        End If

        If dv3.Count <= 0 Then
            Return
        End If

        Dim stjenis As String = ""
        If addstat = False Then
            stjenis = dv(position)("jenis").ToString
        End If

        Dim stkross As Integer = 0
        If skross.Checked = True Then
            stkross = 1
        End If

        Using fkar2 As New fpengajuan3 With {.StartPosition = FormStartPosition.CenterParent, .dv = dv3, .addstat = False, .position = Me.BindingContext(dv3).Position, .kd_divisi = tdivisi.EditValue, .tahun = Year(ttgl.EditValue), .bulan = Month(ttgl.EditValue), .idbu = tunit.EditValue, .jenis = stjenis, .stkross = stkross}
            fkar2.ShowDialog()
        End Using

        Dim jml As Double = 0
        For i As Integer = 0 To dv3.Count - 1
            jml = jml + dv3(i)("jml_real").ToString
        Next

        ttot.EditValue = jml

        If addstat Then
            treal.EditValue = jml
        Else
            If stjenis = "ORDER" Then
                treal.EditValue = jml
            End If
        End If

    End Sub

    Private Sub btsimpan_Click(sender As Object, e As EventArgs) Handles btsimpan.Click

        If tbukti.EditValue = "" Then
            MsgBox("Nobukti tidak boleh kosong", vbOKOnly + vbInformation, "Informasi")
            tbukti.Focus()
            Return
        End If

        If tunit.EditValue = 0 Then
            MsgBox("Unit bisnis/perusahaan tidak boleh kosong", vbOKOnly + vbInformation, "Informasi")
            tunit.Focus()
            Return
        End If

        If tdivisi.EditValue = "" Then
            MsgBox("Divisi tidak boleh kosong", vbOKOnly + vbInformation, "Informasi")
            tdivisi.Focus()
            Return
        End If

        If ttot.EditValue <= 0 Then
            MsgBox("Total tidak boleh kosong", vbOKOnly + vbInformation, "Informasi")
            ttot.Focus()
            Return
        End If

        'If treal.EditValue <= 0 Then
        '    MsgBox("Jml Realisasi tidak boleh kosong", vbOKOnly + vbInformation, "Informasi")
        '    treal.Focus()
        '    Return
        'End If

        simpan(1)
    End Sub

    Private Sub btclose_Click(sender As Object, e As EventArgs) Handles btclose.Click
        Me.Close()
    End Sub

    Private Sub btsimpan2_Click(sender As Object, e As EventArgs) Handles btsimpan2.Click

        If tbukti.EditValue = "" Then
            MsgBox("Nobukti tidak boleh kosong", vbOKOnly + vbInformation, "Informasi")
            tbukti.Focus()
            Return
        End If

        If tunit.EditValue = 0 Then
            MsgBox("Unit bisnis/perusahaan tidak boleh kosong", vbOKOnly + vbInformation, "Informasi")
            tunit.Focus()
            Return
        End If

        If tdivisi.EditValue = "" Then
            MsgBox("Divisi tidak boleh kosong", vbOKOnly + vbInformation, "Informasi")
            tdivisi.Focus()
            Return
        End If

        If ttot.EditValue <= 0 Then
            MsgBox("Total tidak boleh kosong", vbOKOnly + vbInformation, "Informasi")
            ttot.Focus()
            Return
        End If

        If treal.EditValue <= 0 Then
            MsgBox("Jml Realisasi tidak boleh kosong", vbOKOnly + vbInformation, "Informasi")
            treal.Focus()
            Return
        End If

        simpan(2)

    End Sub

    Private Sub skross_CheckedChanged(sender As Object, e As EventArgs) Handles skross.CheckedChanged

        If addstat Then
            open3(tbukti.EditValue)
        End If

    End Sub

End Class