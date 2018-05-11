<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fakun2
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.tkode = New DevExpress.XtraEditors.TextEdit()
        Me.tnama = New DevExpress.XtraEditors.TextEdit()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.tsub = New DevExpress.XtraEditors.LookUpEdit()
        Me.btsimpan = New DevExpress.XtraEditors.SimpleButton()
        Me.btkeluar = New DevExpress.XtraEditors.SimpleButton()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.tunit = New DevExpress.XtraEditors.LookUpEdit()
        CType(Me.tkode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tnama.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tsub.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tunit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(87, 53)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Kode Akun :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(84, 78)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(68, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Nama Akun :"
        '
        'tkode
        '
        Me.tkode.Location = New System.Drawing.Point(155, 50)
        Me.tkode.Name = "tkode"
        Me.tkode.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        ' Me.tkode.Properties.LookAndFeel.TouchUIMode = DevExpress.LookAndFeel.TouchUIMode.[False]
        Me.tkode.Size = New System.Drawing.Size(100, 20)
        Me.tkode.TabIndex = 1
        '
        'tnama
        '
        Me.tnama.Location = New System.Drawing.Point(155, 75)
        Me.tnama.Name = "tnama"
        Me.tnama.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        '  Me.tnama.Properties.LookAndFeel.TouchUIMode = DevExpress.LookAndFeel.TouchUIMode.[False]
        Me.tnama.Size = New System.Drawing.Size(305, 20)
        Me.tnama.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(71, 104)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(81, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Sub Akun dari ?"
        '
        'tsub
        '
        Me.tsub.Location = New System.Drawing.Point(155, 101)
        Me.tsub.Name = "tsub"
        Me.tsub.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.tsub.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("kd_akun", 5, "Kode"), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("nama_akun", "Nama")})
        Me.tsub.Properties.DisplayMember = "nama_akun"
        Me.tsub.Properties.LookAndFeel.UseDefaultLookAndFeel = DevExpress.LookAndFeel.TouchUIMode.[False]
        Me.tsub.Properties.NullText = ""
        Me.tsub.Properties.ValueMember = "kd_akun"
        Me.tsub.Size = New System.Drawing.Size(305, 20)
        Me.tsub.TabIndex = 3
        '
        'btsimpan
        '
        Me.btsimpan.Location = New System.Drawing.Point(304, 142)
        Me.btsimpan.Name = "btsimpan"
        Me.btsimpan.Size = New System.Drawing.Size(75, 27)
        Me.btsimpan.TabIndex = 4
        Me.btsimpan.Text = "&Simpan"
        '
        'btkeluar
        '
        Me.btkeluar.Location = New System.Drawing.Point(385, 142)
        Me.btkeluar.Name = "btkeluar"
        Me.btkeluar.Size = New System.Drawing.Size(75, 27)
        Me.btkeluar.TabIndex = 5
        Me.btkeluar.Text = "&Keluar"
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(29, 27)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(120, 13)
        Me.LabelControl1.TabIndex = 5
        Me.LabelControl1.Text = "Unit Usaha/Perusahaan :"
        '
        'tunit
        '
        Me.tunit.Location = New System.Drawing.Point(155, 24)
        Me.tunit.Name = "tunit"
        Me.tunit.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.tunit.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("nama_bu", "Nama")})
        Me.tunit.Properties.DisplayMember = "nama_bu"
        'Me.tunit.Properties.LookAndFeel. = DevExpress.LookAndFeel.TouchUIMode.[False]
        Me.tunit.Properties.NullText = ""
        Me.tunit.Properties.PopupSizeable = False
        Me.tunit.Properties.ShowHeader = False
        Me.tunit.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.tunit.Properties.ValueMember = "idbu"
        Me.tunit.Size = New System.Drawing.Size(240, 20)
        Me.tunit.TabIndex = 0
        '
        'fakun2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(485, 189)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.tunit)
        Me.Controls.Add(Me.btkeluar)
        Me.Controls.Add(Me.btsimpan)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.tnama)
        Me.Controls.Add(Me.tkode)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.tsub)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "fakun2"
        Me.Text = "Add/Edit Akun"
        CType(Me.tkode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tnama.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tsub.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tunit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents tkode As DevExpress.XtraEditors.TextEdit
    Friend WithEvents tnama As DevExpress.XtraEditors.TextEdit
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents tsub As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents btsimpan As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btkeluar As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tunit As DevExpress.XtraEditors.LookUpEdit
End Class
