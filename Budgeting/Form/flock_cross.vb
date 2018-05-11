Imports System.Data
Imports System.Data.OleDb

Public Class flock_cross

    Dim statusku As Boolean
    Public nobukti As String
    Dim masihproses As Boolean

    Public ReadOnly Property get_status As String
        Get

            Return statusku

        End Get
    End Property
    Private Sub ok_proses()

        statusku = False

        Dim cn As OleDbConnection = Nothing
        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = String.Format("select namauser,sverif_cross from ms_usersys where namauser='{0}' and pwd=HASHBYTES('md5','{1}')", tuser.Text.Trim, tpwd.Text.Trim)
            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            Dim salah As Boolean = True
            If drd.Read Then
                If IsNumeric(drd("sverif_cross").ToString) Then
                    If Integer.Parse(drd("sverif_cross").ToString) = 1 Then

                        If st_divku <> 1 Then

                            Dim sqlc As String = String.Format("select distinct nobukti from tr_pengajuan3 a3 inner join ms_akun_detail b3 on a3.noid_akund=b3.noid " & _
                            "where b3.kd_divisi in (select kd_divisi from ms_usersys5 where namauser='{0}') and nobukti='{1}'", tuser.EditValue, nobukti)

                            Dim cmdc As OleDbCommand = New OleDbCommand(sqlc, cn)
                            Dim drdc As OleDbDataReader = cmdc.ExecuteReader
                            If drdc.Read Then
                                If drdc("nobukti").ToString.Length > 0 Then

                                    Dim sqlup As String = String.Format("update tr_pengajuan set verif_cros=1,nama_verif='{0}' where nobukti='{1}'", tuser.EditValue, nobukti)
                                    Using cmdup As OleDbCommand = New OleDbCommand(sqlup, cn)
                                        cmdup.ExecuteNonQuery()
                                    End Using

                                    statusku = True
                                    salah = False
                                    Me.Close()
                                End If
                            End If
                            drdc.Close()

                        Else

                            Dim sqlup As String = String.Format("update tr_pengajuan set verif_cros=1,nama_verif='{0}' where nobukti='{1}'", tuser.EditValue, nobukti)
                            Using cmdup As OleDbCommand = New OleDbCommand(sqlup, cn)
                                cmdup.ExecuteNonQuery()
                            End Using

                            statusku = True
                            salah = False
                            Me.Close()
                        End If
                        
                    End If
                Else
                End If
            End If
            drd.Close()

           

            If salah = True Then
                MsgBox("User/Password tidak ditemukan", vbOKOnly + vbInformation, "Informasi")
                tuser.Focus()
                Return
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Informasi")
        Finally

            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                    cn.Dispose()
                End If
            End If
        End Try

    End Sub

    Private Sub get_from_dbase()

        Application.DoEvents()

        bet_getfrom.Enabled = False
        btok.Enabled = False
        btclose.Enabled = False

        masihproses = True
        statusku = False

        lblget.Visible = True

        System.Threading.Thread.Sleep(350)

        Dim cn As OleDbConnection = Nothing
        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = String.Format("select verif_cros from tr_pengajuan where nobukti='{0}'", nobukti)
            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            If drd.Read Then
                If IsNumeric(drd("verif_cros").ToString) Then
                    If drd("verif_cros").ToString = 1 Then
                        Timer1.Stop()
                        statusku = True
                        lblget.Visible = False
                        Me.Close()
                    End If
                End If
            End If
            drd.Close()

            masihproses = False

            bet_getfrom.Enabled = True
            btok.Enabled = True
            btclose.Enabled = True

            lblget.Visible = False

            System.Threading.Thread.Sleep(300)

        Catch ex As Exception

            masihproses = False
            bet_getfrom.Enabled = True
            btok.Enabled = True
            btclose.Enabled = True

            lblget.Visible = False

            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Informasi")
        Finally

            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                    cn.Dispose()
                End If
            End If

            'masihproses = False
            'bet_getfrom.Enabled = True
            'btok.Enabled = True
            'btclose.Enabled = True

            'lblget.Visible = False

        End Try

    End Sub

    Private Sub flock_cross_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        statusku = False
        masihproses = False

        lblget.Visible = False

        Timer1.Interval = 5000
        Timer1.Enabled = True

    End Sub

    Private Sub btclose_Click(sender As Object, e As EventArgs) Handles btclose.Click
        statusku = False
        Timer1.Stop()
        Me.Close()
    End Sub

    Private Sub btok_Click(sender As Object, e As EventArgs) Handles btok.Click

        If tuser.Text.Trim.Length = 0 Then
            MsgBox("User tidak boleh kosong", vbOKOnly + vbInformation, "Indormasi")
            tuser.Focus()
            Return
        End If

        If tpwd.Text.Trim.Length = 0 Then
            MsgBox("User tidak boleh kosong", vbOKOnly + vbInformation, "Indormasi")
            tuser.Focus()
            Return
        End If

        ok_proses()

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        If masihproses = False Then
            get_from_dbase()
        End If

    End Sub

    Private Sub bet_getfrom_Click(sender As Object, e As EventArgs) Handles bet_getfrom.Click

        If masihproses = False Then
            get_from_dbase()
        End If

    End Sub

End Class