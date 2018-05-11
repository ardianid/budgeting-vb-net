Imports System.Data
Imports System.Data.OleDb
Public Class fpengajuan3

    Public kd_divisi As String
    Public idbu As Integer
    Public tahun As String
    Public bulan As String

    Private dvmanager1 As Data.DataViewManager
    Private dv1 As Data.DataView

    Public dv As DataView
    Public position As Integer
    Public addstat As Boolean

    Public jenis As String
    Public stkross As Integer

    Private Sub open()

        grid1.DataSource = Nothing

        Dim cn As OleDbConnection = Nothing

        Try

            dv1 = Nothing

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = "select a.noid,cast(a.tahun as varchar(4)) + '/' + cast(a.bulan as varchar(2)) as thn_bln, " & _
            "b.nama_akun,a.akun_opex_bp,a.detail_aktifitas,a.jml,(a.jml_order+a.jml_pakai) as jml_pakai,(a.jml - (a.jml_order+a.jml_pakai)) as sisa,0 as jml_guna,a.noid,c.nama_divisi " & _
            "from ms_akun_detail a inner join ms_akun b on a.kd_akun=b.kd_akun inner join ms_divisi c on a.kd_divisi=c.kd_divisi " & _
            "where "

            If bulan = 12 Then
                sql = String.Format("{0} ((tahun={1} and bulan>={2} and bulan<={3}) or (tahun={4} and bulan=1))", sql, tahun, bulan, IIf(bulan = 12, bulan, bulan + 1), tahun + 1)
            Else
                sql = String.Format("{0} (tahun={1} and bulan>={2} and bulan<={3})", sql, tahun, bulan, bulan + 1)
            End If

            If addstat = False Then
                sql = String.Format("{0} and a.noid='000'", sql)
            Else

                If Not IsNothing(dv) Then
                    If dv.Count > 0 Then

                        Dim noid_ada As String = ""
                        For i As Integer = 0 To dv.Count - 1
                            If i = 0 Then
                                noid_ada = String.Format("'{0}'", dv(i)("noid_akund").ToString)
                            Else
                                noid_ada = String.Format("{0},'{1}'", noid_ada, dv(i)("noid_akund").ToString)
                            End If
                        Next

                        If noid_ada <> "" Then
                            sql = String.Format("{0} and not(a.noid in ({1}))", sql, noid_ada)
                        End If

                    End If
                End If

            End If

            If stkross = 1 Then

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
                        sql = String.Format("{0} and not(a.kd_divisi in ({1}))", sql, kd_divaja)
                    End If

                End If

                sql = String.Format("{0} and a.idbu={1} order by a.tahun,a.bulan,b.nama_akun,a.akun_opex_bp", sql, idbu)

            Else
                sql = String.Format("{0} and a.kd_divisi='{1}' and a.idbu={2} order by a.tahun,a.bulan,b.nama_akun,a.akun_opex_bp", sql, kd_divisi, idbu)
            End If



           

            Dim ds As DataSet = New DataSet()
            ds = Clsmy.GetDataSet(sql, cn)

            dvmanager1 = New DataViewManager(ds)
            dv1 = dvmanager1.CreateDataView(ds.Tables(0))

            grid1.DataSource = dv1

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

    Private Sub isi_()

        Dim noid As String = dv(position)("noid_akund").ToString
        Dim noid3 As String = dv(position)("noid").ToString

        Dim detail_akt As String = ""
        Dim jml As Double = 0
        Dim jml_pakai As Double = 0
        Dim sisa As Double = 0

        Dim kode_opex As String = ""
        Dim nama_divisi As String = ""

        Dim jmlawal As Double = 0

        Dim cn As OleDbConnection = Nothing
        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = String.Format("select a.detail_aktifitas,a.jml,(a.jml_order+a.jml_pakai) as jml_pakai,(a.jml - (a.jml_order+a.jml_pakai)) as sisa,a.noid,b.nama_divisi from ms_akun_detail a inner join ms_divisi b on a.kd_divisi=b.kd_divisi where a.noid='{0}'", noid)
            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            With drd.Read
                If drd("jml").ToString > 0 Then

                    detail_akt = drd("detail_aktifitas").ToString
                    jml = drd("jml").ToString
                    jml_pakai = drd("jml_pakai").ToString
                    sisa = drd("sisa").ToString

                    kode_opex = drd("noid").ToString
                    nama_divisi = drd("nama_divisi").ToString

                End If
            End With
            drd.Close()

            Dim sql2 As String = String.Format("select jml_real from tr_pengajuan3 where noid={0}", noid3)
            Dim cmd2 As OleDbCommand = New OleDbCommand(sql2, cn)
            Dim drd2 As OleDbDataReader = cmd2.ExecuteReader

            If drd2.Read Then
                If IsNumeric(drd2(0).ToString) Then
                    jmlawal = Double.Parse(drd2(0).ToString)
                End If
            End If
            drd2.Close()

        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
        Finally


            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If
        End Try

        If jml > 0 Then
            Dim orow As DataRowView = dv1.AddNew

            orow("noid") = kode_opex
            orow("nama_divisi") = nama_divisi

            orow("thn_bln") = dv(position)("thn_bln").ToString
            orow("nama_akun") = dv(position)("nama_akun").ToString
            orow("akun_opex_bp") = dv(position)("akun_opex_bp").ToString
            orow("detail_aktifitas") = detail_akt

            If noid3 > 0 Then
                orow("jml") = jml

                If jmlawal = dv(position)("jml_real").ToString Then
                    orow("jml_pakai") = jml_pakai - dv(position)("jml_real").ToString
                    orow("sisa") = sisa + dv(position)("jml_real").ToString
                Else
                    orow("jml_pakai") = jml_pakai - jmlawal
                    orow("sisa") = sisa + jmlawal
                End If

            Else
                orow("jml") = jml
                orow("jml_pakai") = jml_pakai
                orow("sisa") = sisa
            End If

            orow("jml_guna") = dv(position)("jml_real").ToString

            dv1.EndInit()

        End If

    End Sub

    Private Sub isi_view()

        For i As Integer = 0 To dv1.Count - 1
            If Double.Parse(dv1(i)("jml_guna").ToString) > 0 Then
                Dim orow As DataRowView = dv.AddNew
                orow("thn_bln") = dv1(i)("thn_bln").ToString
                orow("nama_akun") = dv1(i)("nama_akun").ToString
                orow("akun_opex_bp") = dv1(i)("akun_opex_bp").ToString
                orow("detail_aktifitas") = dv1(i)("detail_aktifitas").ToString
                orow("noid_akund") = dv1(i)("noid").ToString
                orow("jml") = dv1(i)("jml_guna").ToString
                orow("jml_real") = dv1(i)("jml_guna").ToString
                orow("noid") = 0
                dv.EndInit()
            End If
        Next

    End Sub

    Private Sub update_view()

        For i As Integer = 0 To dv1.Count - 1

            dv(position)("jml_real") = dv1(i)("jml_guna").ToString

            If dv(position)("noid") = 0 Then
                dv(position)("jml") = dv1(i)("jml_guna").ToString
            Else

                If jenis = "ORDER" Then
                    dv(position)("jml") = dv1(i)("jml_guna").ToString
                End If

            End If

        Next

    End Sub

    Private Sub fpengajuan3_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        grid1.Focus()
    End Sub
    Private Sub fpengajuan3_Load(sender As Object, e As EventArgs) Handles Me.Load

        open()

        If addstat = False Then
            isi_()
        End If

    End Sub

    Private Sub btkeluar_Click(sender As Object, e As EventArgs) Handles btkeluar.Click
        Me.Close()
    End Sub


    Private Sub btok_Click(sender As Object, e As EventArgs) Handles btok.Click

        'If MsgBox("Yakin sudah benar ?", vbYesNo + vbQuestion, "Konfirmasi") = MsgBoxResult.No Then
        '    Return
        'End If

        If addstat Then
            isi_view()
        Else
            update_view()
        End If

        Me.Close()

    End Sub


End Class