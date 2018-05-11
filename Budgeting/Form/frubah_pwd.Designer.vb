<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frubah_pwd
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frubah_pwd))
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.tpwd1 = New DevExpress.XtraEditors.TextEdit()
        Me.tpwd2 = New DevExpress.XtraEditors.TextEdit()
        Me.SimpleButton1 = New DevExpress.XtraEditors.SimpleButton()
        Me.SimpleButton2 = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.tpwd3 = New DevExpress.XtraEditors.TextEdit()
        Me.PictureEdit1 = New DevExpress.XtraEditors.PictureEdit()
        CType(Me.tpwd1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tpwd2.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tpwd3.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureEdit1.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(44, 27)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(81, 13)
        Me.LabelControl1.TabIndex = 5
        Me.LabelControl1.Text = "Password Lama :"
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(47, 53)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(78, 13)
        Me.LabelControl2.TabIndex = 4
        Me.LabelControl2.Text = "Password Baru :"
        '
        'tpwd1
        '
        Me.tpwd1.Location = New System.Drawing.Point(137, 24)
        Me.tpwd1.Name = "tpwd1"
        Me.tpwd1.Properties.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.tpwd1.Properties.UseSystemPasswordChar = True
        Me.tpwd1.Size = New System.Drawing.Size(164, 20)
        Me.tpwd1.TabIndex = 0
        '
        'tpwd2
        '
        Me.tpwd2.Location = New System.Drawing.Point(137, 50)
        Me.tpwd2.Name = "tpwd2"
        Me.tpwd2.Properties.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.tpwd2.Properties.UseSystemPasswordChar = True
        Me.tpwd2.Size = New System.Drawing.Size(164, 20)
        Me.tpwd2.TabIndex = 1
        '
        'SimpleButton1
        '
        Me.SimpleButton1.Location = New System.Drawing.Point(147, 128)
        Me.SimpleButton1.Name = "SimpleButton1"
        Me.SimpleButton1.Size = New System.Drawing.Size(74, 23)
        Me.SimpleButton1.TabIndex = 3
        Me.SimpleButton1.Text = "&Ok"
        '
        'SimpleButton2
        '
        Me.SimpleButton2.Location = New System.Drawing.Point(227, 128)
        Me.SimpleButton2.Name = "SimpleButton2"
        Me.SimpleButton2.Size = New System.Drawing.Size(74, 23)
        Me.SimpleButton2.TabIndex = 4
        Me.SimpleButton2.Text = "&Keluar"
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(22, 80)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(103, 13)
        Me.LabelControl3.TabIndex = 6
        Me.LabelControl3.Text = "Verif Password Baru :"
        '
        'tpwd3
        '
        Me.tpwd3.Location = New System.Drawing.Point(137, 77)
        Me.tpwd3.Name = "tpwd3"
        Me.tpwd3.Properties.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.tpwd3.Properties.UseSystemPasswordChar = True
        Me.tpwd3.Size = New System.Drawing.Size(164, 20)
        Me.tpwd3.TabIndex = 2
        '
        'PictureEdit1
        '
        Me.PictureEdit1.EditValue = CType(resources.GetObject("PictureEdit1.EditValue"), Object)
        Me.PictureEdit1.Location = New System.Drawing.Point(12, 106)
        Me.PictureEdit1.Name = "PictureEdit1"
        Me.PictureEdit1.Properties.Appearance.BackColor = System.Drawing.Color.White
        Me.PictureEdit1.Properties.Appearance.Options.UseBackColor = True
        Me.PictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder
        Me.PictureEdit1.Size = New System.Drawing.Size(63, 46)
        Me.PictureEdit1.TabIndex = 6
        '
        'frubah_pwd
        '
        Me.Appearance.BackColor = System.Drawing.Color.White
        Me.Appearance.Options.UseBackColor = True
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(322, 164)
        Me.ControlBox = False
        Me.Controls.Add(Me.tpwd3)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.SimpleButton2)
        Me.Controls.Add(Me.SimpleButton1)
        Me.Controls.Add(Me.tpwd2)
        Me.Controls.Add(Me.tpwd1)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.PictureEdit1)
        Me.Name = "frubah_pwd"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Rubah Password"
        CType(Me.tpwd1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tpwd2.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tpwd3.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureEdit1.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tpwd1 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents tpwd2 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents SimpleButton1 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents SimpleButton2 As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tpwd3 As DevExpress.XtraEditors.TextEdit
    Friend WithEvents PictureEdit1 As DevExpress.XtraEditors.PictureEdit
End Class
