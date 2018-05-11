Imports System.Data
Imports System.Data.OleDb

Imports DevExpress.XtraReports.UI
Public Class fpr_bukti_pengajuan

    Public nobukti As String

    Private Sub loadfaktur()

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

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

            PrintControl1.PrintingSystem = rinvoice.PrintingSystem

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
    Private Sub fpr_bukti_pengajuan_Load(sender As Object, e As EventArgs) Handles Me.Load
        loadfaktur()
    End Sub

End Class