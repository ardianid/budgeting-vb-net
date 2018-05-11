Imports System.Data
Imports System.Data.OleDb
Public Class fbudget_akun2

    Public lokasi As String
    Private DtSet As DataSet
    Private stat_upload As Boolean

    Public ReadOnly Property get_stat As String
        Get
            Return stat_upload
        End Get
    End Property

    Private Sub open_data()

        Dim cn As OleDbConnection = Nothing
        grid1.DataSource = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn_excel(lokasi)

            Dim MyCommand As OleDbDataAdapter = New System.Data.OleDb.OleDbDataAdapter _
                ("select '' as status,* from [Sheet1$]", cn)
            MyCommand.TableMappings.Add("Table", "TestTable")
            DtSet = New System.Data.DataSet
            MyCommand.Fill(DtSet)


            grid1.DataSource = DtSet.Tables(0)


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

    Private Sub fbudget_akun2_Load(sender As Object, e As EventArgs) Handles Me.Load
        open_data()

        stat_upload = False

        TextEdit1.Visible = False

    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click

        If IsNothing(DtSet.Tables(0)) Then
            MsgBox("Tidak ada data yang akan diproses", vbOKOnly + vbExclamation, "Konfrimasi")
            Return
        End If

        If IsNothing(DtSet.Tables(0).Rows.Count <= 0) Then
            MsgBox("Tidak ada data yang akan diproses", vbOKOnly + vbExclamation, "Konfrimasi")
            Return
        End If

        Dim cn As OleDbConnection = Nothing
        Dim sqltrans As OleDbTransaction = Nothing
        Try

            TextEdit1.Visible = True
            '  Timer1.Start()

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            sqltrans = cn.BeginTransaction

            Dim dt As DataTable = DtSet.Tables(0)

            Dim tahunins As String = ""

            For i As Integer = 0 To dt.Rows.Count - 1

                Dim sql_bu As String = String.Format("select idbu from ms_bu where idbu={0}", dt(i)(3).ToString)
                Dim cmd_bu As OleDbCommand = New OleDbCommand(sql_bu, cn, sqltrans)
                Dim drd_bu As OleDbDataReader = cmd_bu.ExecuteReader

                Dim adabu As Boolean = False
                If drd_bu.Read Then
                    If Not (drd_bu(0).ToString = "") Then
                        adabu = True
                    Else

                        If Integer.Parse(drd_bu(0).ToString) > 0 Then
                            adabu = True
                        End If

                    End If
                End If
                drd_bu.Close()

                If adabu = False Then
                    MsgBox(String.Format("Unit Bisnis salah pada baris ke {0}", i + 1), vbOKOnly + vbExclamation, "Konfirmasi")
                    sqltrans.Rollback()

                    Timer1.Stop()
                    TextEdit1.Visible = False

                    Return
                End If

                Dim sql_div As String = String.Format("select kd_divisi from ms_divisi where kd_divisi='{0}'", dt(i)(4).ToString)
                Dim cmd_div As OleDbCommand = New OleDbCommand(sql_div, cn, sqltrans)
                Dim drd_div As OleDbDataReader = cmd_div.ExecuteReader

                Dim adadiv As Boolean = False
                If drd_div.Read Then
                    If Not (drd_div(0).ToString = "") Then
                        adadiv = True
                    Else
                        If drd_div(0).ToString.Length > 0 Then
                            adadiv = True
                        End If

                    End If
                End If
                drd_div.Close()

                If adadiv = False Then
                    MsgBox(String.Format("Divisi salah pada baris ke {0}", i + 1), vbOKOnly + vbExclamation, "Konfirmasi")
                    sqltrans.Rollback()

                    Timer1.Stop()
                    TextEdit1.Visible = False

                    Return
                End If

                Dim sql_akun As String = String.Format("select kd_akun from ms_akun where kd_akun='{0}'", dt(i)(9).ToString)
                Dim cmd_akun As OleDbCommand = New OleDbCommand(sql_akun, cn, sqltrans)
                Dim drd_akun As OleDbDataReader = cmd_akun.ExecuteReader

                Dim adaakun As Boolean = False
                If drd_akun.Read Then
                    If Not (drd_akun(0).ToString = "") Then
                        adaakun = True
                    Else

                        If drd_akun(0).ToString.Length > 0 Then
                            adaakun = True
                        End If

                    End If
                End If
                drd_akun.Close()

                If adaakun = False Then
                    MsgBox(String.Format("Akun salah pada baris ke {0}", i + 1), vbOKOnly + vbExclamation, "Konfirmasi")
                    sqltrans.Rollback()

                    Timer1.Stop()
                    TextEdit1.Visible = False

                    Return
                End If

                Dim sqlcek As String = String.Format("select noid from ms_akun_detail where tahun={0} and bulan={1} and idbu={2} and kd_divisi='{3}' and kd_akun='{4}' and detail_aktifitas='{5}'", _
                                                    dt(i)(1).ToString, dt(i)(2).ToString, dt(i)(3).ToString, dt(i)(4).ToString, dt(i)(9).ToString, dt(i)(7).ToString)
                Dim cmdcek As OleDbCommand = New OleDbCommand(sqlcek, cn, sqltrans)
                Dim drdcek As OleDbDataReader = cmdcek.ExecuteReader

                Dim adacek As Boolean = False
                If drdcek.Read Then
                    If Not (drdcek(0).ToString = "") Then

                        If Len(drdcek(0).ToString) > 0 Then
                            adacek = True
                        End If

                    End If
                End If
                drdcek.Close()

                Dim statusnya As String = "SUDAH ADA"

                If adacek = False Then

                    If Not IsNumeric(dt(i)(10).ToString) Then
                        MsgBox("Jumlah tidak bolek kosong, min 0. Proses dibatalkan", vbOKOnly + vbExclamation, "Konfirmasi")
                        sqltrans.Rollback()
                        Return
                    End If

                    Dim jml As Double = Double.Parse(dt(i)(10).ToString)

                    Dim sqlins As String = String.Format("insert into ms_akun_detail (tahun,bulan,idbu,kd_divisi,jenis_opex,aktifitas,detail_aktifitas,akun_opex_bp,kd_akun,jml,noid) values({0},{1},{2},'{3}','{4}','{4}','{6}','{7}','{8}',{9},'{10}')", _
                                                        dt(i)(1).ToString, dt(i)(2).ToString, dt(i)(3).ToString, dt(i)(4).ToString, dt(i)(5).ToString, dt(i)(6).ToString, dt(i)(7).ToString, dt(i)(8).ToString, dt(i)(9).ToString, Replace(jml, ",", "."), dt(i)(11).ToString)
                    Using cmdins As OleDbCommand = New OleDbCommand(sqlins, cn, sqltrans)
                        cmdins.ExecuteNonQuery()
                    End Using

                    statusnya = "OK"

                End If

                tahunins = dt(i)(1).ToString

                Application.DoEvents()

                dt(i)(0) = statusnya
                GridView1.FocusedRowHandle = i

                System.Threading.Thread.Sleep(300)

            Next

            Clsmy.InsertToLog(cn, "btopex2", 1, 0, 0, 0, tahunins, "", sqltrans)

            TextEdit1.Text = "Proses Upload Selesai.."
            sqltrans.Commit()
            stat_upload = True

        Catch ex As Exception

            stat_upload = False

            Timer1.Stop()
            TextEdit1.Visible = False

            sqltrans.Rollback()
            MsgBox(ex.ToString, MsgBoxStyle.Information, "Informasi")
        Finally

            Timer1.Stop()

            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                End If
            End If
        End Try

        


    End Sub

    Public Function MarqueeLeft(ByVal Text As String)
        Dim Str1 As String = Text.Remove(0, 1)
        Dim Str2 As String = Text(0)
        Return Str1 & Str2
    End Function

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        TextEdit1.Text = MarqueeLeft(TextEdit1.Text)
    End Sub

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        Me.Close()
    End Sub

End Class