Imports Budgeting.Clsmy
Imports System.Data
Imports System.Data.OleDb

Public Class fsettdbase

    Private Sub SimpleButton1_Click(sender As System.Object, e As System.EventArgs) Handles SimpleButton1.Click

        If (tserver.Text.Trim.Length <= 0 Or tdbase.Text.Trim.Length <= 0 Or tuser.Text.Trim.Length <= 0 Or tpwd.Text.Trim.Length <= 0) Then
            MsgBox("Semua data harus diisi...", vbInformation + vbOKOnly, "Informasi")
            Return
        End If

        Try

            Dim hasil As String = Clsmy.WriteContent(tserver.Text.Trim, tdbase.Text.Trim, tuser.Text.Trim, tpwd.Text.Trim)

            Me.Close()
            futama1.LoadLOgin()

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try


    End Sub

    Private Sub SimpleButton2_Click(sender As System.Object, e As System.EventArgs) Handles SimpleButton2.Click

        Dim cn As OleDbConnection = Nothing

        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn_test(tserver.Text.Trim, tdbase.Text.Trim, tuser.Text.Trim, tpwd.Text.Trim)

            MsgBox("Test setting ok...", vbOKOnly + vbInformation, "Informasi")

        Catch ex As Exception
            MsgBox(ex.ToString)
        Finally
            If Not cn Is Nothing Then
                If cn.State = ConnectionState.Open Then
                    cn.Close()
                    cn.Dispose()
                End If
            End If
        End Try

    End Sub
End Class