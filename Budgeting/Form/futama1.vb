Imports DevExpress.XtraBars
Imports System.Data.OleDb
Imports Budgeting.Clsmy
Imports DevExpress.Skins
Public Class futama1

    Private Sub disable_bar()

        FillItemBranch(Bar2.ItemLinks)

        Bar2.Visible = False

    End Sub

    Private Sub FillItemBranch(ByVal links As DevExpress.XtraBars.BarItemLinkCollection)
        For Each link As DevExpress.XtraBars.BarItemLink In links
            If TypeOf link.Item Is DevExpress.XtraBars.BarButtonItem Then
                link.Item.Visibility = BarItemVisibility.Never
            End If
            If TypeOf link.Item Is DevExpress.XtraBars.BarSubItem Then
                FillItemBranch(CType(link.Item, DevExpress.XtraBars.BarSubItem).ItemLinks)
            End If
        Next
    End Sub

    Public Sub LoadOtherForm(ByVal fname As Form)

        open_wait()
        Cursor = Cursors.WaitCursor


        fname.MdiParent = Me
        fname.Show()


        Cursor = Cursors.Default
        close_wait()

    End Sub

    Private Sub btnuser_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles btnuser.ItemClick

        open_wait()
        Cursor = Cursors.WaitCursor

        fuser.MdiParent = Me
        fuser.Show()

        Cursor = Cursors.Default
        close_wait()

    End Sub

    Private Sub futama_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Application.Exit()
    End Sub

    Private Sub futama_Load(sender As Object, e As EventArgs) Handles Me.Load

        disable_bar()

        bar_tgl.Caption = DateValue(Date.Now)

        Try
            Dim cn As OleDbConnection

            cn = New OleDbConnection
            cn = Clsmy.open_conn

            cn.Close()
            cn.Dispose()



            LoadLOgin()



        Catch ex As Exception

            fsettdbase.MdiParent = Me
            fsettdbase.Show()

            'If ex.Message.ToString.Equals("Object reference not set to an instance of an object.") Then
            '    MsgBox("ok")
            'End If

        End Try

    End Sub

    Public Sub LoadLOgin()
        Dim fmlogin As New login With {.MdiParent = Me, .WindowState = FormWindowState.Maximized}
        fmlogin.Show()
    End Sub

    Private Sub NO_logof_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles NO_sm_logoff.ItemClick

        For Each f As Form In Me.MdiChildren
            f.Close()
        Next

        disable_bar()

        userprog = ""
        pwd = ""
        initial_user = ""
        stbalik_opex = 0
        st_divku = 0
        st_alluserku = 0
        st_crossbudget = 0

        btverif_cross.Visibility = BarItemVisibility.Never

        bar_user.Caption = "User : "

        Dim fmlogin As New login With {.MdiParent = Me, .WindowState = FormWindowState.Maximized}
        fmlogin.Show()

    End Sub

    Private Sub Timer1_Tick(sender As System.Object, e As System.EventArgs) Handles Timer1.Tick
        bar_jam.Caption = Format(Now, "hh:mm:ss tt")
    End Sub

    Private Sub NO_ch_pwd_ItemClick(sender As System.Object, e As DevExpress.XtraBars.ItemClickEventArgs) Handles NO_sm_rubahpwd.ItemClick
        frubah_pwd.ShowDialog(Me)
    End Sub

    Private Sub btakun_ItemClick(sender As Object, e As ItemClickEventArgs) Handles btakun.ItemClick
        fakun.MdiParent = Me
        fakun.Show()
    End Sub

    Private Sub btbu_ItemClick(sender As Object, e As ItemClickEventArgs) Handles btbu.ItemClick
        fbu.MdiParent = Me
        fbu.Show()
    End Sub

    Private Sub btdepart_ItemClick(sender As Object, e As ItemClickEventArgs) Handles btdepart.ItemClick
        fdepart_divisi.MdiParent = Me
        fdepart_divisi.Show()
    End Sub

    Private Sub btopex2_ItemClick(sender As Object, e As ItemClickEventArgs) Handles btopex2.ItemClick
        fbudget_akun.MdiParent = Me
        fbudget_akun.Show()
    End Sub

    Private Sub btbeli_ItemClick(sender As Object, e As ItemClickEventArgs) Handles btbeli.ItemClick
        fpengajuan.MdiParent = Me
        fpengajuan.Show()
    End Sub

    Private Sub btverif_cross_ItemClick(sender As Object, e As ItemClickEventArgs) Handles btverif_cross.ItemClick
        fverif_cross.ShowDialog()
    End Sub

End Class