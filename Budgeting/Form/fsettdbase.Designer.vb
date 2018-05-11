<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fsettdbase
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
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.SimpleButton2 = New DevExpress.XtraEditors.SimpleButton()
        Me.SimpleButton1 = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.tpwd = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.tuser = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.tdbase = New DevExpress.XtraEditors.TextEdit()
        Me.tserver = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl1.SuspendLayout()
        CType(Me.tpwd.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tuser.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tdbase.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tserver.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PanelControl1
        '
        Me.PanelControl1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.PanelControl1.Controls.Add(Me.SimpleButton2)
        Me.PanelControl1.Controls.Add(Me.SimpleButton1)
        Me.PanelControl1.Controls.Add(Me.LabelControl4)
        Me.PanelControl1.Controls.Add(Me.tpwd)
        Me.PanelControl1.Controls.Add(Me.LabelControl3)
        Me.PanelControl1.Controls.Add(Me.tuser)
        Me.PanelControl1.Controls.Add(Me.LabelControl2)
        Me.PanelControl1.Controls.Add(Me.tdbase)
        Me.PanelControl1.Controls.Add(Me.tserver)
        Me.PanelControl1.Controls.Add(Me.LabelControl1)
        Me.PanelControl1.Location = New System.Drawing.Point(146, 78)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(342, 181)
        Me.PanelControl1.TabIndex = 0
        '
        'SimpleButton2
        '
        Me.SimpleButton2.Location = New System.Drawing.Point(161, 139)
        Me.SimpleButton2.Name = "SimpleButton2"
        Me.SimpleButton2.Size = New System.Drawing.Size(78, 25)
        Me.SimpleButton2.TabIndex = 8
        Me.SimpleButton2.Text = "&Test"
        '
        'SimpleButton1
        '
        Me.SimpleButton1.Location = New System.Drawing.Point(245, 139)
        Me.SimpleButton1.Name = "SimpleButton1"
        Me.SimpleButton1.Size = New System.Drawing.Size(78, 25)
        Me.SimpleButton1.TabIndex = 4
        Me.SimpleButton1.Text = "&OK"
        '
        'LabelControl4
        '
        Me.LabelControl4.Location = New System.Drawing.Point(25, 102)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(27, 13)
        Me.LabelControl4.TabIndex = 7
        Me.LabelControl4.Text = "Pwd :"
        '
        'tpwd
        '
        Me.tpwd.Location = New System.Drawing.Point(58, 99)
        Me.tpwd.Name = "tpwd"
        Me.tpwd.Properties.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.tpwd.Size = New System.Drawing.Size(265, 20)
        Me.tpwd.TabIndex = 3
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(23, 76)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(29, 13)
        Me.LabelControl3.TabIndex = 5
        Me.LabelControl3.Text = "User :"
        '
        'tuser
        '
        Me.tuser.Location = New System.Drawing.Point(58, 73)
        Me.tuser.Name = "tuser"
        Me.tuser.Properties.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.tuser.Size = New System.Drawing.Size(265, 20)
        Me.tuser.TabIndex = 2
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(15, 50)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(37, 13)
        Me.LabelControl2.TabIndex = 3
        Me.LabelControl2.Text = "Dbase :"
        '
        'tdbase
        '
        Me.tdbase.Location = New System.Drawing.Point(58, 47)
        Me.tdbase.Name = "tdbase"
        Me.tdbase.Properties.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.tdbase.Size = New System.Drawing.Size(265, 20)
        Me.tdbase.TabIndex = 1
        '
        'tserver
        '
        Me.tserver.Location = New System.Drawing.Point(58, 21)
        Me.tserver.Name = "tserver"
        Me.tserver.Size = New System.Drawing.Size(265, 20)
        Me.tserver.TabIndex = 0
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(13, 24)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(39, 13)
        Me.LabelControl1.TabIndex = 0
        Me.LabelControl1.Text = "Server :"
        '
        'fsettdbase
        '
        Me.Appearance.BackColor = System.Drawing.Color.White
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(635, 336)
        Me.Controls.Add(Me.PanelControl1)
        Me.Name = "fsettdbase"
        Me.Text = "Sett Database Utl"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl1.ResumeLayout(False)
        Me.PanelControl1.PerformLayout()
        CType(Me.tpwd.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tuser.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tdbase.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tserver.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents SimpleButton1 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tpwd As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tuser As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tdbase As DevExpress.XtraEditors.TextEdit
    Friend WithEvents tserver As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents SimpleButton2 As DevExpress.XtraEditors.SimpleButton
End Class
