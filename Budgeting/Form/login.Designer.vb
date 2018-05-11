<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class login
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(login))
        Me.btmasuk = New DevExpress.XtraEditors.SimpleButton()
        Me.btbatal = New DevExpress.XtraEditors.SimpleButton()
        Me.PanelControl4 = New DevExpress.XtraEditors.PanelControl()
        Me.SplitContainerControl1 = New DevExpress.XtraEditors.SplitContainerControl()
        Me.PanelControl1 = New DevExpress.XtraEditors.PanelControl()
        Me.tuser = New DevExpress.XtraEditors.TextEdit()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.tpwd = New DevExpress.XtraEditors.TextEdit()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.PanelControl4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelControl4.SuspendLayout()
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainerControl1.SuspendLayout()
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tuser.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tpwd.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btmasuk
        '
        Me.btmasuk.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btmasuk.Location = New System.Drawing.Point(119, 74)
        Me.btmasuk.Name = "btmasuk"
        Me.btmasuk.Size = New System.Drawing.Size(46, 25)
        Me.btmasuk.TabIndex = 4
        Me.btmasuk.Text = "&Masuk"
        '
        'btbatal
        '
        Me.btbatal.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btbatal.Location = New System.Drawing.Point(167, 74)
        Me.btbatal.Name = "btbatal"
        Me.btbatal.Size = New System.Drawing.Size(46, 25)
        Me.btbatal.TabIndex = 5
        Me.btbatal.Text = "&Batal"
        '
        'PanelControl4
        '
        Me.PanelControl4.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.PanelControl4.Appearance.BackColor = System.Drawing.Color.Transparent
        Me.PanelControl4.Appearance.Options.UseBackColor = True
        Me.PanelControl4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PanelControl4.Controls.Add(Me.SplitContainerControl1)
        Me.PanelControl4.Location = New System.Drawing.Point(92, 82)
        Me.PanelControl4.Name = "PanelControl4"
        Me.PanelControl4.Size = New System.Drawing.Size(335, 120)
        Me.PanelControl4.TabIndex = 1
        '
        'SplitContainerControl1
        '
        Me.SplitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainerControl1.IsSplitterFixed = True
        Me.SplitContainerControl1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainerControl1.Name = "SplitContainerControl1"
        Me.SplitContainerControl1.Panel1.Controls.Add(Me.PanelControl1)
        Me.SplitContainerControl1.Panel1.Text = "Panel1"
        Me.SplitContainerControl1.Panel2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.btbatal)
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.tuser)
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.Label2)
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.tpwd)
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.btmasuk)
        Me.SplitContainerControl1.Panel2.Controls.Add(Me.Label1)
        Me.SplitContainerControl1.Panel2.Text = "Panel2"
        Me.SplitContainerControl1.Size = New System.Drawing.Size(335, 120)
        Me.SplitContainerControl1.SplitterPosition = 92
        Me.SplitContainerControl1.TabIndex = 0
        Me.SplitContainerControl1.Text = "SplitContainerControl1"
        '
        'PanelControl1
        '
        Me.PanelControl1.Appearance.BackColor = System.Drawing.Color.White
        Me.PanelControl1.Appearance.Options.UseBackColor = True
        Me.PanelControl1.AutoSize = True
        Me.PanelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PanelControl1.ContentImage = CType(resources.GetObject("PanelControl1.ContentImage"), System.Drawing.Image)
        Me.PanelControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelControl1.Location = New System.Drawing.Point(0, 0)
        Me.PanelControl1.Name = "PanelControl1"
        Me.PanelControl1.Size = New System.Drawing.Size(92, 120)
        Me.PanelControl1.TabIndex = 0
        '
        'tuser
        '
        Me.tuser.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.tuser.Location = New System.Drawing.Point(96, 25)
        Me.tuser.Name = "tuser"
        Me.tuser.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tuser.Properties.Appearance.Options.UseFont = True
        Me.tuser.Size = New System.Drawing.Size(117, 20)
        Me.tuser.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(16, 49)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(60, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Password :"
        '
        'tpwd
        '
        Me.tpwd.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.tpwd.Location = New System.Drawing.Point(96, 48)
        Me.tpwd.Name = "tpwd"
        Me.tpwd.Properties.Appearance.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tpwd.Properties.Appearance.Options.UseFont = True
        Me.tpwd.Properties.PasswordChar = Global.Microsoft.VisualBasic.ChrW(64)
        Me.tpwd.Size = New System.Drawing.Size(117, 20)
        Me.tpwd.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 27)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(36, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "User :"
        '
        'login
        '
        Me.Appearance.BackColor = System.Drawing.Color.White
        Me.Appearance.Options.UseBackColor = True
        Me.Appearance.Options.UseFont = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(521, 297)
        Me.Controls.Add(Me.PanelControl4)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LookAndFeel.SkinName = "VS2010"
        Me.LookAndFeel.UseDefaultLookAndFeel = False
        Me.Name = "login"
        Me.Text = "Login"
        CType(Me.PanelControl4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelControl4.ResumeLayout(False)
        CType(Me.SplitContainerControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainerControl1.ResumeLayout(False)
        CType(Me.PanelControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tuser.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tpwd.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PanelControl4 As DevExpress.XtraEditors.PanelControl
    Friend WithEvents btbatal As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btmasuk As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents tpwd As DevExpress.XtraEditors.TextEdit
    Friend WithEvents tuser As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents SplitContainerControl1 As DevExpress.XtraEditors.SplitContainerControl
    Friend WithEvents PanelControl1 As DevExpress.XtraEditors.PanelControl
End Class
