Imports System.Data
Imports System.Data.OleDb
Imports Budgeting.Clsmy

Public Class frubah_pwd

    Private Function cek_pwd_sebelumnya(ByVal cn As OleDbConnection) As Boolean
        Dim hasil As Boolean = False

        Dim sql As String = String.Format("select namauser from ms_usersys where nonaktif=0 and namauser='{0}' and pwd=HASHBYTES('md5','{1}')", userprog, tpwd1.EditValue)
        Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
        Dim drd As OleDbDataReader = cmd.ExecuteReader

        If drd.Read Then
            If Not (drd(0).ToString.Equals("")) Then
                hasil = True
            End If
        End If

        drd.Close()

        Return hasil
    End Function

    Private Sub simpan()

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            If cek_pwd_sebelumnya(cn) = False Then
                MsgBox("Password sebelumnya tidak sesuai...", vbExclamation + vbOKOnly, "Error")
                tpwd1.Focus()
                Return
            End If

            Dim sql_update_pwd As String = String.Format("UPDATE ms_usersys SET pwd=HASHBYTES('md5','{0}') WHERE namauser='{1}'", tpwd2.EditValue, userprog)
            Using cmdup As OleDbCommand = New OleDbCommand(sql_update_pwd, cn)
                cmdup.ExecuteNonQuery()
            End Using

            MsgBox("Password telah dirubah, silahkan masuk kembali ke program...", vbOKOnly + vbInformation, "Informasi")
            Me.Close()
            futama1.NO_sm_logoff.PerformClick()

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

    Private Sub SimpleButton2_Click(sender As System.Object, e As System.EventArgs) Handles SimpleButton2.Click
        Me.Close()
    End Sub

    Private Sub frubah_pwd_Activated(sender As Object, e As System.EventArgs) Handles Me.Activated
        tpwd1.Focus()
    End Sub

    Private Sub SimpleButton1_Click(sender As System.Object, e As System.EventArgs) Handles SimpleButton1.Click

        If tpwd1.Text.Trim.Length = 0 Then
            MsgBox("Password lama harus diisi...!!!", vbOKOnly + vbInformation, "Informasi")
            tpwd1.Focus()
            Return
        End If

        If tpwd2.Text.Trim.Length = 0 Then
            MsgBox("Password baru harus diisi...!!!", vbOKOnly + vbInformation, "Informasi")
            tpwd2.Focus()
            Return
        End If

        If Not tpwd2.Text.Trim.ToString.Equals(tpwd3.Text.Trim) Then
            MsgBox("Password baru dan verifikasi harus sama..", vbOKOnly + vbInformation, "Informasi")
            tpwd2.Focus()
            Return
        End If

        simpan()

    End Sub

    Private Sub frubah_pwd_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        tpwd1.Text = ""
        tpwd2.Text = ""
        tpwd3.Text = ""
    End Sub

End Class