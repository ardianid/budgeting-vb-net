Imports System.Data
Imports System.Data.OleDb

Public Class fbu2

    Public dv As DataView
    Public position As Integer
    Public addstat As Boolean

    Private Sub isi()

        tunit.EditValue = dv(position)("nama_bu").ToString
        talamat.EditValue = dv(position)("alamat_bu").ToString
        ttelp.EditValue = dv(position)("telp_bu").ToString

    End Sub

    Private Sub simpan()

        Dim cn As OleDbConnection = Nothing
        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = ""
            If addstat Then
                sql = String.Format("insert into ms_bu (nama_bu,alamat_bu,telp_bu) values ('{0}','{1}','{2}');SELECT SCOPE_IDENTITY()", tunit.EditValue, talamat.EditValue, ttelp.EditValue)
            Else
                sql = String.Format("update ms_bu set nama_bu='{0}',alamat_bu='{1}',telp_bu='{2}' where idbu={3}", tunit.EditValue, talamat.EditValue, ttelp.EditValue, dv(position)("idbu").ToString)
            End If

           
            If addstat Then

                Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
                Dim r_string As Object = cmd.ExecuteScalar

                Dim orow As DataRowView = dv.AddNew
                orow("idbu") = r_string.ToString
                orow("nama_bu") = tunit.EditValue
                orow("alamat_bu") = talamat.EditValue
                orow("telp_bu") = ttelp.EditValue
                dv.EndInit()

                Clsmy.InsertToLog(cn, "btakun", 1, 0, 0, 0, r_string.ToString, "", Nothing)

                tunit.EditValue = ""
                talamat.EditValue = ""
                ttelp.EditValue = ""
                tunit.Focus()

            Else

                Using cmd As OleDbCommand = New OleDbCommand(sql, cn)
                    cmd.ExecuteNonQuery()
                End Using

                dv(position)("nama_bu") = tunit.EditValue
                dv(position)("alamat_bu") = talamat.EditValue
                dv(position)("telp_bu") = ttelp.EditValue

                Clsmy.InsertToLog(cn, "btakun", 0, 1, 0, 0, dv(position)("idbu").ToString, "", Nothing)

                MsgBox("Data dirubah", vbOKOnly + vbInformation, "Informasi")
                Me.Close()

            End If

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

    Private Sub SimpleButton2_Click(sender As Object, e As EventArgs) Handles SimpleButton2.Click
        Me.Close()
    End Sub

    Private Sub SimpleButton1_Click(sender As Object, e As EventArgs) Handles SimpleButton1.Click

        If tunit.EditValue = "" Then
            MsgBox("Nama unit bisnis harus diisi", vbOKOnly + vbInformation, "Informasi")
            tunit.Focus()
            Return
        End If

        If talamat.EditValue = "" Then
            MsgBox("Alamat unit bisnis harus diisi", vbOKOnly + vbInformation, "Informasi")
            talamat.Focus()
            Return
        End If

        simpan()

    End Sub

    Private Sub fbu2_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        tunit.Focus()
    End Sub

    Private Sub fbu2_Load(sender As Object, e As EventArgs) Handles Me.Load
        If addstat = False Then
            isi()
        End If
    End Sub

End Class