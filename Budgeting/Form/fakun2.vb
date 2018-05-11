Imports System.Data
Imports System.Data.OleDb

Public Class fakun2

    Public dv As DataView
    Public position As Integer
    Public addstat As Boolean
    Private stadd_detail As Boolean

    Public ReadOnly Property get_stat As String
        Get
            Return stadd_detail
        End Get
    End Property

    Private Sub kosongkan()

        isi_akun()

        tkode.EditValue = ""
        tnama.EditValue = ""

    End Sub
    Private Sub isi_akun()

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = "select * from ms_akun"
            Dim dsgroup As DataSet = New DataSet()
            dsgroup = Clsmy.GetDataSet(sql, cn)

            Dim dt As DataTable = dsgroup.Tables(0)

            Dim R As DataRow = dt.NewRow
            R("kd_akun") = ""
            R("kd_akun_m") = ""
            R("nama_akun") = ""

            dt.Rows.InsertAt(R, 0)

            tsub.Properties.DataSource = dt

            tsub.ItemIndex = 0

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

    Private Sub isi_unitusaha()
        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = String.Format("select * from ms_bu where idbu in ({0})", idbu_ku)
            Dim ds As DataSet = New DataSet()
            ds = Clsmy.GetDataSet(sql, cn)


            tunit.Properties.DataSource = ds.Tables(0)

            tunit.EditValue = Integer.Parse(idbu_defaultku)

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

    Private Sub isi()
        tkode.EditValue = dv(position)("kd_akun").ToString
        tnama.EditValue = dv(position)("nama_akun").ToString
        tsub.EditValue = dv(position)("ParentID").ToString
        tunit.EditValue = Integer.Parse(dv(position)("idbu").ToString)
    End Sub

    Private Sub simpan_view()
        Dim orow As DataRowView = dv.AddNew
        orow("kd_akun") = tkode.EditValue
        orow("ID") = tkode.EditValue
        orow("nama_akun") = tnama.EditValue
        orow("ParentID") = tsub.EditValue
        orow("idbu") = Integer.Parse(tunit.EditValue)
        orow("nama_bu") = tunit.Text.Trim
        dv.EndInit()
    End Sub

    Private Sub update_view()

        dv(position)("kd_akun") = tkode.EditValue
        dv(position)("ID") = tkode.EditValue
        dv(position)("nama_akun") = tnama.EditValue
        dv(position)("ParentID") = tsub.EditValue
        dv(position)("idbu") = Integer.Parse(tunit.EditValue)
        dv(position)("nama_bu") = tunit.Text.Trim

    End Sub

    Private Sub simpan()

        Dim cn As OleDbConnection = Nothing
        Dim sqltrans As OleDbTransaction = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn


            Dim sqlcek As String = String.Format("select kd_akun from ms_akun where kd_akun='{0}'", tkode.EditValue)
            Dim cmdcek As OleDbCommand = New OleDbCommand(sqlcek, cn)
            Dim drdcek As OleDbDataReader = cmdcek.ExecuteReader

            Dim adacek As Boolean = False
            If drdcek.Read Then
                If Not (drdcek(0).ToString = "") Then
                    adacek = True
                End If
            End If
            drdcek.Close()

            If addstat Then
                If adacek = True Then
                    MsgBox("No bukti sudah ada", vbOKOnly + vbExclamation, "Informasi")
                    tkode.Focus()
                    Return
                End If
            End If

            sqltrans = cn.BeginTransaction

            Dim kode_akun_m As String = ""

            If tsub.EditValue.ToString.Trim.Length = 0 Then
                kode_akun_m = tkode.EditValue
            Else
                kode_akun_m = tsub.EditValue
            End If


            Dim sql As String = ""
            If addstat Then
                sql = String.Format("insert into ms_akun (kd_akun,kd_akun_m,nama_akun,idbu) values('{0}','{1}','{2}',{3})", tkode.EditValue, kode_akun_m, tnama.EditValue, tunit.EditValue)
            Else
                sql = String.Format("update ms_akun set kd_akun_m='{0}',nama_akun='{1}' where kd_akun='{2}'", kode_akun_m, tnama.EditValue, tkode.EditValue)
            End If

            Using cmd As OleDbCommand = New OleDbCommand(sql, cn, sqltrans)
                cmd.ExecuteNonQuery()
            End Using

            sqltrans.Commit()

            If addstat Then

                stadd_detail = True

                Clsmy.InsertToLog(cn, "btakun", 1, 0, 0, 0, tkode.EditValue, "", sqltrans)

                simpan_view()

                kosongkan()
                tkode.Focus()

            Else

                stadd_detail = True

                Clsmy.InsertToLog(cn, "btakun", 0, 1, 0, 0, tkode.EditValue, "", sqltrans)

                update_view()

                MsgBox("Data dirubah..", vbOKOnly + vbInformation, "Informasi")
                Me.Close()

            End If



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

    Private Sub fakun2_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        If addstat Then
            tunit.Focus()
        Else
            tnama.Focus()
        End If

    End Sub

    Private Sub fakun2_Load(sender As Object, e As EventArgs) Handles Me.Load

        isi_akun()
        isi_unitusaha()

        stadd_detail = False

        If addstat = False Then
            isi()
            tkode.Properties.ReadOnly = True
            tunit.Properties.ReadOnly = True
            tsub.Enabled = False
        End If

    End Sub

    Private Sub btkeluar_Click(sender As Object, e As EventArgs) Handles btkeluar.Click
        Me.Close()
    End Sub

    Private Sub btsimpan_Click(sender As Object, e As EventArgs) Handles btsimpan.Click

        If IsNothing(tunit.EditValue) Then
            MsgBox("Unit usaha/perusahaan tidak boleh kosong", vbOKOnly + vbExclamation, "Informasi")
            tunit.Focus()
        End If

        If tunit.EditValue = 0 Then
            MsgBox("Unit usaha/perusahaan tidak boleh kosong", vbOKOnly + vbExclamation, "Informasi")
            tunit.Focus()
        End If

        If tkode.EditValue = "" Then
            MsgBox("Kode  tidak boleh kosong", vbOKOnly + vbExclamation, "Informasi")
            tkode.Focus()
        End If

        If tnama.EditValue = "" Then
            MsgBox("Nama tidak boleh kosong", vbOKOnly + vbExclamation, "Informasi")
            tkode.Focus()
        End If

        simpan()

    End Sub

End Class