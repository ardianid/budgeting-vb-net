Imports System.Data
Imports System.Data.OleDb

Public Class fcustom_filter

    Dim sqlhasil As String

    Public ReadOnly Property get_sql As String
        Get

            Return sqlhasil

        End Get
    End Property

    Private Sub open_filter()

        sqlhasil = "select aju.nobukti,CONVERT(VARCHAR(10),aju.tanggal,103) as tanggal,bu.nama_bu,dep.nama_departemen,div.nama_divisi,aju.nama_pemohon,aju.non_b, " & _
        "aju.sbatal,aju.total,aju.jml_realisasi, " & _
        "case aju.jenis " & _
        "when 1 then 'ORDER' " & _
        "when 2 then 'COMPLETE' " & _
        "when 3 then 'CANCEL' end as jenis,aju.jml_print,aju.namauser,aju.cross_b,(aju.cross_b + aju.verif_cros + aju.sbatal) as cross_verif " & _
        "from tr_pengajuan aju inner join ms_bu bu on aju.idbu=bu.idbu " & _
        "inner join ms_divisi div on aju.kd_divisi=div.kd_divisi inner join ms_departemen dep on div.kd_depart=dep.kd_depart"

        sqlhasil = String.Format(" {0} where (aju.tanggal>='{1}' and aju.tanggal<='{2}')", sqlhasil, convert_date_to_eng(ttgl1.EditValue), convert_date_to_eng(ttgl2.EditValue))

        If tbukti.EditValue <> "" Then
            sqlhasil = String.Format(" {0} and aju.nobukti like '%{1}%'", sqlhasil, tbukti.EditValue)
        End If

        If tmohon.EditValue <> "" Then
            sqlhasil = String.Format(" {0} and aju.nama_pemohon like '%{1}%'", sqlhasil, tmohon.EditValue)
        End If

        If tdivisi.EditValue <> "" Then
            sqlhasil = String.Format(" {0} and div.nama_divisi like '%{1}%'", sqlhasil, tdivisi.EditValue)
        End If

        If tdepart.EditValue <> "" Then
            sqlhasil = String.Format(" {0} and dep.nama_departemen like '%{1}%'", sqlhasil, tdepart.EditValue)
        End If

        If tuser.EditValue <> "" Then
            sqlhasil = String.Format(" {0} and aju.namauser like '%{1}%'", sqlhasil, tuser.EditValue)
        End If

        If opt_bud.EditValue <> 3 Then
            sqlhasil = String.Format(" {0} and aju.non_b={1}", sqlhasil, opt_bud.EditValue)
        End If

        If opt_cros.EditValue <> 3 Then
            sqlhasil = String.Format(" {0} and aju.cross_b={1}", sqlhasil, opt_cros.EditValue)
        End If

        If opt_stat_cros.EditValue <> 3 Then
            sqlhasil = String.Format(" {0} and aju.verif_cros={1}", sqlhasil, opt_stat_cros.EditValue)
        End If

        If trincian.EditValue <> "" Then
            sqlhasil = String.Format(" {0} and aju.nobukti in (select nobukti from tr_pengajuan2 where nama_orderan like '%{1}%')", sqlhasil, trincian.EditValue)
        End If

        If topex.EditValue <> "" Then
            sqlhasil = String.Format(" {0} and aju.nobukti in (select a.nobukti from tr_pengajuan3 a inner join ms_akun_detail b on a.noid_akund=b.noid inner join ms_akun c on b.kd_akun=c.kd_akun where b.akun_opex_bp like '%{1}%')", sqlhasil, topex.EditValue)
        End If

        If tdetail.EditValue <> "" Then
            sqlhasil = String.Format(" {0} and aju.nobukti in (select a.nobukti from tr_pengajuan3 a inner join ms_akun_detail b on a.noid_akund=b.noid inner join ms_akun c on b.kd_akun=c.kd_akun where b.detail_aktifitas like '%{1}%')", sqlhasil, tdetail.EditValue)
        End If

        If takun.EditValue <> "" Then
            sqlhasil = String.Format(" {0} and aju.nobukti in (select a.nobukti from tr_pengajuan3 a inner join ms_akun_detail b on a.noid_akund=b.noid inner join ms_akun c on b.kd_akun=c.kd_akun where c.nama_akun like '%{1}%')", sqlhasil, takun.EditValue)
        End If

    End Sub

    Private Sub btclose_Click(sender As Object, e As EventArgs) Handles btclose.Click
        sqlhasil = ""
        Me.Close()
    End Sub

    Private Sub fcustom_filter_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        ttgl1.Focus()
    End Sub

    Private Sub fcustom_filter_Load(sender As Object, e As EventArgs) Handles Me.Load

        ttgl1.EditValue = Now
        ttgl2.EditValue = Now

        opt_bud.EditValue = 3
        opt_cros.EditValue = 3
        opt_stat_cros.EditValue = 3

        sqlhasil = ""
    End Sub

    Private Sub btfilter_Click(sender As Object, e As EventArgs) Handles btfilter.Click
        open_filter()
        Me.Close()
    End Sub

End Class