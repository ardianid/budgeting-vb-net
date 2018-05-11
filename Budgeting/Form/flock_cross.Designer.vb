<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class flock_cross
    Inherits DevExpress.XtraEditors.XtraForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.tuser = New DevExpress.XtraEditors.TextEdit()
        Me.tpwd = New DevExpress.XtraEditors.TextEdit()
        Me.btok = New DevExpress.XtraEditors.SimpleButton()
        Me.btclose = New DevExpress.XtraEditors.SimpleButton()
        Me.bet_getfrom = New DevExpress.XtraEditors.SimpleButton()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl5 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl6 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl7 = New DevExpress.XtraEditors.LabelControl()
        Me.XtraTabControl1 = New DevExpress.XtraTab.XtraTabControl()
        Me.XtraTabPage1 = New DevExpress.XtraTab.XtraTabPage()
        Me.lblget = New DevExpress.XtraEditors.LabelControl()
        CType(Me.tuser.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tpwd.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.XtraTabControl1.SuspendLayout()
        Me.XtraTabPage1.SuspendLayout()
        Me.SuspendLayout()
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(40, 27)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(29, 13)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "User :"
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(16, 53)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(53, 13)
        Me.LabelControl2.TabIndex = 1
        Me.LabelControl2.Text = "Password :"
        '
        'tuser
        '
        Me.tuser.Location = New System.Drawing.Point(75, 24)
        Me.tuser.Name = "tuser"
        Me.tuser.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tuser.Properties.Appearance.Options.UseFont = True
        '   Me.tuser.Properties.LookAndFeel.TouchUIMode = DevExpress.LookAndFeel.TouchUIMode.[False]
        Me.tuser.Size = New System.Drawing.Size(236, 20)
        Me.tuser.TabIndex = 4
        '
        'tpwd
        '
        Me.tpwd.Location = New System.Drawing.Point(75, 50)
        Me.tpwd.Name = "tpwd"
        Me.tpwd.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tpwd.Properties.Appearance.Options.UseFont = True
        '  Me.tpwd.Properties.LookAndFeel.TouchUIMode = DevExpress.LookAndFeel.TouchUIMode.[False]
        Me.tpwd.Properties.PasswordChar = Global.Microsoft.VisualBasic.ChrW(64)
        Me.tpwd.Size = New System.Drawing.Size(236, 20)
        Me.tpwd.TabIndex = 5
        '
        'btok
        '
        Me.btok.Location = New System.Drawing.Point(201, 97)
        Me.btok.Name = "btok"
        Me.btok.Size = New System.Drawing.Size(52, 23)
        Me.btok.TabIndex = 6
        Me.btok.Text = "&Ok"
        '
        'btclose
        '
        Me.btclose.Location = New System.Drawing.Point(259, 97)
        Me.btclose.Name = "btclose"
        Me.btclose.Size = New System.Drawing.Size(52, 23)
        Me.btclose.TabIndex = 7
        Me.btclose.Text = "&Close"
        '
        'bet_getfrom
        '
        Me.bet_getfrom.Location = New System.Drawing.Point(75, 97)
        Me.bet_getfrom.Name = "bet_getfrom"
        Me.bet_getfrom.Size = New System.Drawing.Size(84, 23)
        Me.bet_getfrom.TabIndex = 8
        Me.bet_getfrom.Text = "&Get verifikasi"
        '
        'Timer1
        '
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(23, 12)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(272, 13)
        Me.LabelControl3.TabIndex = 9
        Me.LabelControl3.Text = "Transaksi ini telah mengambil budget dari akun opex lain,"
        '
        'LabelControl4
        '
        Me.LabelControl4.Location = New System.Drawing.Point(23, 31)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(550, 13)
        Me.LabelControl4.TabIndex = 10
        Me.LabelControl4.Text = "sebelumnya sudah disetujui dan sekarang terjadi perubahan, maka anda harus memint" & _
    "a otorisasi persetujuann lagi"
        '
        'LabelControl5
        '
        Me.LabelControl5.Location = New System.Drawing.Point(24, 256)
        Me.LabelControl5.Name = "LabelControl5"
        Me.LabelControl5.Size = New System.Drawing.Size(382, 13)
        Me.LabelControl5.TabIndex = 11
        Me.LabelControl5.Text = "Anda dapat meminta administrator untuk memasukkan user dan password atau,"
        '
        'LabelControl6
        '
        Me.LabelControl6.Location = New System.Drawing.Point(24, 275)
        Me.LabelControl6.Name = "LabelControl6"
        Me.LabelControl6.Size = New System.Drawing.Size(280, 13)
        Me.LabelControl6.TabIndex = 12
        Me.LabelControl6.Text = "anda dapat meminta verifikasi dan klik tombol get verifikasi"
        '
        'LabelControl7
        '
        Me.LabelControl7.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelControl7.Location = New System.Drawing.Point(24, 294)
        Me.LabelControl7.Name = "LabelControl7"
        Me.LabelControl7.Size = New System.Drawing.Size(505, 13)
        Me.LabelControl7.TabIndex = 13
        Me.LabelControl7.Text = "* program akan melihat apakah transaksi ini sudah diverifkasi administrator setia" & _
    "p 5 dtk"
        '
        'XtraTabControl1
        '
        Me.XtraTabControl1.Location = New System.Drawing.Point(23, 64)
        Me.XtraTabControl1.Name = "XtraTabControl1"
        Me.XtraTabControl1.SelectedTabPage = Me.XtraTabPage1
        Me.XtraTabControl1.Size = New System.Drawing.Size(364, 161)
        Me.XtraTabControl1.TabIndex = 14
        Me.XtraTabControl1.TabPages.AddRange(New DevExpress.XtraTab.XtraTabPage() {Me.XtraTabPage1})
        '
        'XtraTabPage1
        '
        Me.XtraTabPage1.Controls.Add(Me.tuser)
        Me.XtraTabPage1.Controls.Add(Me.LabelControl1)
        Me.XtraTabPage1.Controls.Add(Me.LabelControl2)
        Me.XtraTabPage1.Controls.Add(Me.tpwd)
        Me.XtraTabPage1.Controls.Add(Me.btok)
        Me.XtraTabPage1.Controls.Add(Me.btclose)
        Me.XtraTabPage1.Controls.Add(Me.bet_getfrom)
        Me.XtraTabPage1.Controls.Add(Me.lblget)
        Me.XtraTabPage1.Name = "XtraTabPage1"
        Me.XtraTabPage1.Size = New System.Drawing.Size(358, 133)
        Me.XtraTabPage1.Text = "Form Verifikasi"
        '
        'lblget
        '
        Me.lblget.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblget.Appearance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblget.Location = New System.Drawing.Point(75, 78)
        Me.lblget.Name = "lblget"
        Me.lblget.Size = New System.Drawing.Size(97, 13)
        Me.lblget.TabIndex = 15
        Me.lblget.Text = "*Get from database"
        '
        'flock_cross
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(605, 332)
        Me.Controls.Add(Me.XtraTabControl1)
        Me.Controls.Add(Me.LabelControl7)
        Me.Controls.Add(Me.LabelControl6)
        Me.Controls.Add(Me.LabelControl5)
        Me.Controls.Add(Me.LabelControl4)
        Me.Controls.Add(Me.LabelControl3)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "flock_cross"
        Me.Text = "Lock proses perubahan cross budet"
        CType(Me.tuser.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tpwd.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.XtraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.XtraTabControl1.ResumeLayout(False)
        Me.XtraTabPage1.ResumeLayout(False)
        Me.XtraTabPage1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tuser As DevExpress.XtraEditors.TextEdit
    Friend WithEvents tpwd As DevExpress.XtraEditors.TextEdit
    Friend WithEvents btok As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btclose As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents bet_getfrom As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl5 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl6 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl7 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents XtraTabControl1 As DevExpress.XtraTab.XtraTabControl
    Friend WithEvents XtraTabPage1 As DevExpress.XtraTab.XtraTabPage
    Friend WithEvents lblget As DevExpress.XtraEditors.LabelControl
End Class
