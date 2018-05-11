Imports Budgeting.Clsmy
Imports System.Data
Imports System.Data.OleDb
Imports DevExpress.XtraBars

Public Class login

    Dim fmfile As Integer = 0
    Dim fmmaster As Integer = 0
    Dim fmtransaksi As Integer = 0

    Private Sub login_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        tuser.Focus()
    End Sub

    Private Sub btbatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btbatal.Click
        Application.Exit()
    End Sub

    Private Sub btmasuk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btmasuk.Click

        If tuser.Text = "" Then
            MsgBox("Nama user harus diisi", MsgBoxStyle.Information, "Informasi")
            tuser.Focus()
            Exit Sub
        End If

        If tpwd.Text = "" Then
            MsgBox("Password harus diisi", MsgBoxStyle.Information, "Informasi")
            tpwd.Focus()
            Exit Sub
        End If

        open_wait()

        Dim cn As OleDbConnection = New OleDbConnection
        userprog = tuser.Text.Trim
        pwd = tpwd.Text.Trim

        Try


            cn = Clsmy.open_conn

            Dim sql As String = String.Format("select nonaktif,jenisuser,initial_us,sbalik_opex,sall_div,sall_user,namauser,sverif_cross from ms_usersys where namauser='{0}' and pwd=HASHBYTES('md5','{1}')", tuser.Text.Trim, tpwd.Text.Trim)
            Dim comd = New OleDbCommand(sql, cn)
            Dim dre As OleDbDataReader = comd.ExecuteReader

            If dre.Read Then

                If dre(0).ToString = "1" Then
                    close_wait()
                    MsgBox("User anda sudah tidak aktif, hubungi admin", vbOKOnly + vbInformation, "Informasi")
                Else

                    initial_user = dre("initial_us").ToString
                    stbalik_opex = Integer.Parse(dre("sbalik_opex").ToString)
                    st_divku = Integer.Parse(dre("sall_div").ToString)
                    st_alluserku = Integer.Parse(dre("sall_user").ToString)
                    userprog = dre("namauser").ToString
                    st_crossbudget = Integer.Parse(dre("sverif_cross").ToString)

                    setmenu()
                    setmenu2()
                    set_idbu()
                    '  setmenu2()
                    futama1.bar_user.Caption = "User : " & userprog.Trim

                    If st_crossbudget = 1 Then
                        futama1.btverif_cross.Visibility = BarItemVisibility.Always
                    Else
                        futama1.btverif_cross.Visibility = BarItemVisibility.Never
                    End If



                    Me.Close()


                End If

            Else
                close_wait()
                MsgBox("User/Password tidak ditemukan...", vbOKOnly + vbInformation, "Informasi")
                tuser.Focus()
            End If

            close_wait()

        Catch ex As Exception

            close_wait()

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

    Public Sub setmenu()

        Dim ds As DataSet
        Dim sql As String = String.Format("select a.kodemenu,a.t_active,a.t_add,a.t_edit,a.t_del,a.t_lap,b.namaform,b.submenu2,b.submenu1 from ms_usersys2 a inner join ms_menu b on a.kodemenu=b.kodemenu where a.namauser='{0}'", userprog.Trim)


        Dim cn2 As New OleDbConnection
        

        Try

            cn2 = Clsmy.open_conn

            ds = New DataSet
            ds = Clsmy.GetDataSet(sql, cn2)

            dtmenu = New DataTable
            dtmenu.Clear()

            dtmenu = ds.Tables(0)

            If ds.Tables(0).Rows.Count > 0 Then

                FillItemBranch(futama1.Bar2.ItemLinks)

                '  If fmfile = 1 Then
                futama1.mfile.Visibility = BarItemVisibility.Always
                'Else
                '    futama.mfile.Visibility = BarItemVisibility.Never
                ' End If

                Dim sqlcek As String = String.Format("select distinct jenislap from ms_menu2 where kodemenu in (select kodemenu from ms_usersys3 where namauser='{0}')", tuser.Text.Trim)
                Dim cmdcek As OleDbCommand = New OleDbCommand(sqlcek, cn2)
                Dim drcek As OleDbDataReader = cmdcek.ExecuteReader

                If drcek.HasRows Then
                    While drcek.Read

                        If Integer.Parse(drcek(0).ToString) = 1 Then
                            fmmaster = 1
                        ElseIf Integer.Parse(drcek(0).ToString) = 2 Then
                            fmtransaksi = 1
                        End If

                    End While
                End If
                drcek.Close()

                If fmmaster = 1 Then
                    futama1.mmaster.Visibility = BarItemVisibility.Always
                Else
                    futama1.mmaster.Visibility = BarItemVisibility.Never
                End If

                If fmtransaksi = 1 Then
                    futama1.mtrans.Visibility = BarItemVisibility.Always
                Else
                    futama1.mtrans.Visibility = BarItemVisibility.Never
                End If

                futama1.Bar2.Visible = True

            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Informasi")
        Finally
            If Not cn2 Is Nothing Then
                If cn2.State = ConnectionState.Open Then
                    cn2.Close()
                    cn2.Dispose()
                End If
            End If
        End Try

    End Sub

    Public Sub setmenu2()

        Dim ds As DataSet
        Dim sql As String = String.Format("select a.kodemenu,a.t_lap,b.namaform from ms_usersys3 a inner join ms_menu b on a.kodemenu=b.kodemenu where a.namauser='{0}'", userprog.Trim)


        Dim cn2 As New OleDbConnection

        Try

            cn2 = Clsmy.open_conn

            ds = New DataSet
            ds = Clsmy.GetDataSet(sql, cn2)

            dtmenu2 = New DataTable
            dtmenu2.Clear()

            dtmenu2 = ds.Tables(0)

                If Not cn2 Is Nothing Then
                    If cn2.State = ConnectionState.Open Then
                        cn2.Close()
                        cn2.Dispose()
                    End If
                End If


        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Informasi")
        Finally
            If Not cn2 Is Nothing Then
                If cn2.State = ConnectionState.Open Then
                    cn2.Close()
                    cn2.Dispose()
                End If
            End If
        End Try

    End Sub

    Public Sub set_idbu()

        idbu_ku = ""
        namabu_ku = ""
        idbu_defaultku = ""

        Dim cn As OleDbConnection = Nothing
        Try

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            Dim sql As String = String.Format("select a.idbu,b.nama_bu,a.sdef from ms_usersys4 a inner join ms_bu b on a.idbu=b.idbu where namauser='{0}'", userprog.Trim)
            Dim cmd As OleDbCommand = New OleDbCommand(sql, cn)
            Dim drd As OleDbDataReader = cmd.ExecuteReader

            Dim a As Integer = 0
            If drd.HasRows Then
                While drd.Read

                    If a = 0 Then
                        idbu_ku = String.Format("'{0}'", drd("idbu").ToString)
                    Else
                        idbu_ku = String.Format("{0},'{1}'", idbu_ku, drd("idbu").ToString)
                    End If

                    If drd("sdef").ToString = 1 Then
                        namabu_ku = drd("nama_bu").ToString
                        idbu_defaultku = drd("idbu").ToString
                    End If

                    a = a + 1

                End While
            End If
            drd.Close()

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

    Private Sub FillItemBranch(ByVal links As DevExpress.XtraBars.BarItemLinkCollection)
        For Each link As DevExpress.XtraBars.BarItemLink In links
            If TypeOf link.Item Is DevExpress.XtraBars.BarButtonItem Then

                If link.Item.Name.ToString.Substring(0, 2) = "NO" Then
                    link.Item.Visibility = BarItemVisibility.Always
                Else

                    Dim namabar As String = link.Item.Name.ToString

                    Dim rows() As DataRow = dtmenu.Select(String.Format("kodemenu='{0}'", namabar))
                    Dim i2 As Integer = 0

                    If rows.Length = 0 Then
                        link.Item.Visibility = BarItemVisibility.Never
                    Else

                        For i2 = 0 To rows.GetUpperBound(0)


                            If Convert.ToInt16(rows(i2)("t_active")) = 1 Then

                                link.Item.Visibility = BarItemVisibility.Always

                                If rows(i2)("submenu2").ToString.Equals("file") Then
                                    fmfile = 1
                                ElseIf rows(i2)("submenu2").ToString.Equals("master") Then
                                    fmmaster = 1
                                ElseIf rows(i2)("submenu2").ToString.Equals("transaksi") Then
                                    fmtransaksi = 1
                                End If

                            Else
                                link.Item.Visibility = BarItemVisibility.Never
                            End If
                        Next

                    End If
                End If

            End If
            If TypeOf link.Item Is DevExpress.XtraBars.BarSubItem Then
                FillItemBranch(CType(link.Item, DevExpress.XtraBars.BarSubItem).ItemLinks)
            End If
        Next
    End Sub

    Private Sub tuser_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles tuser.KeyDown
        If e.KeyCode = 13 Then
            tpwd.Focus()
        End If
    End Sub

    Private Sub tpwd_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles tpwd.KeyDown
        If e.KeyCode = 13 Then
            btmasuk.PerformClick()
        End If
    End Sub

    Private Sub login_Load(sender As Object, e As EventArgs) Handles Me.Load

        PanelControl4.Top = Me.Height / 2 - PanelControl4.Height / 2
        PanelControl4.Left = Me.Width / 2 - PanelControl4.Width / 2

    End Sub

    Private Sub login_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        PanelControl4.Top = Me.Height / 2 - PanelControl4.Height / 2
        PanelControl4.Left = Me.Width / 2 - PanelControl4.Width / 2
    End Sub

End Class