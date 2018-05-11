<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class fdepart_divisi3
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
        Me.btkeluar = New DevExpress.XtraEditors.SimpleButton()
        Me.btsimpan = New DevExpress.XtraEditors.SimpleButton()
        Me.tnama = New DevExpress.XtraEditors.TextEdit()
        Me.tkode = New DevExpress.XtraEditors.TextEdit()
        Me.LabelControl3 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl2 = New DevExpress.XtraEditors.LabelControl()
        Me.LabelControl1 = New DevExpress.XtraEditors.LabelControl()
        Me.tdepart = New DevExpress.XtraEditors.LookUpEdit()
        Me.LabelControl4 = New DevExpress.XtraEditors.LabelControl()
        Me.tunit = New DevExpress.XtraEditors.TextEdit()
        CType(Me.tnama.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tkode.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tdepart.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.tunit.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btkeluar
        '
        Me.btkeluar.Location = New System.Drawing.Point(264, 112)
        Me.btkeluar.Name = "btkeluar"
        Me.btkeluar.Size = New System.Drawing.Size(75, 23)
        Me.btkeluar.TabIndex = 4
        Me.btkeluar.Text = "&Keluar"
        '
        'btsimpan
        '
        Me.btsimpan.Location = New System.Drawing.Point(183, 112)
        Me.btsimpan.Name = "btsimpan"
        Me.btsimpan.Size = New System.Drawing.Size(75, 23)
        Me.btsimpan.TabIndex = 3
        Me.btsimpan.Text = "&Simpan"
        '
        'tnama
        '
        Me.tnama.Location = New System.Drawing.Point(99, 76)
        Me.tnama.Name = "tnama"
        Me.tnama.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        '    Me.tnama.Properties.LookAndFeel.TouchUIMode = DevExpress.LookAndFeel.TouchUIMode.[False]
        Me.tnama.Size = New System.Drawing.Size(240, 20)
        Me.tnama.TabIndex = 2
        '
        'tkode
        '
        Me.tkode.Location = New System.Drawing.Point(99, 50)
        Me.tkode.Name = "tkode"
        Me.tkode.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        '   Me.tkode.Properties.LookAndFeel.TouchUIMode = DevExpress.LookAndFeel.TouchUIMode.[False]
        Me.tkode.Size = New System.Drawing.Size(81, 20)
        Me.tkode.TabIndex = 1
        '
        'LabelControl3
        '
        Me.LabelControl3.Location = New System.Drawing.Point(59, 79)
        Me.LabelControl3.Name = "LabelControl3"
        Me.LabelControl3.Size = New System.Drawing.Size(34, 13)
        Me.LabelControl3.TabIndex = 10
        Me.LabelControl3.Text = "Nama :"
        '
        'LabelControl2
        '
        Me.LabelControl2.Location = New System.Drawing.Point(62, 53)
        Me.LabelControl2.Name = "LabelControl2"
        Me.LabelControl2.Size = New System.Drawing.Size(31, 13)
        Me.LabelControl2.TabIndex = 8
        Me.LabelControl2.Text = "Kode :"
        '
        'LabelControl1
        '
        Me.LabelControl1.Location = New System.Drawing.Point(27, 27)
        Me.LabelControl1.Name = "LabelControl1"
        Me.LabelControl1.Size = New System.Drawing.Size(66, 13)
        Me.LabelControl1.TabIndex = 5
        Me.LabelControl1.Text = "Departemen :"
        '
        'tdepart
        '
        Me.tdepart.Location = New System.Drawing.Point(99, 24)
        Me.tdepart.Name = "tdepart"
        Me.tdepart.Properties.Buttons.AddRange(New DevExpress.XtraEditors.Controls.EditorButton() {New DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)})
        Me.tdepart.Properties.Columns.AddRange(New DevExpress.XtraEditors.Controls.LookUpColumnInfo() {New DevExpress.XtraEditors.Controls.LookUpColumnInfo("nama_departemen", "Nama"), New DevExpress.XtraEditors.Controls.LookUpColumnInfo("nama_bu", "Nama Unit Usaha")})
        Me.tdepart.Properties.DisplayMember = "nama_departemen"
        '   Me.tdepart.Properties.LookAndFeel.TouchUIMode = DevExpress.LookAndFeel.TouchUIMode.[False]
        Me.tdepart.Properties.NullText = ""
        Me.tdepart.Properties.PopupSizeable = False
        Me.tdepart.Properties.ShowHeader = False
        Me.tdepart.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard
        Me.tdepart.Properties.ValueMember = "kd_depart"
        Me.tdepart.Size = New System.Drawing.Size(240, 20)
        Me.tdepart.TabIndex = 0
        '
        'LabelControl4
        '
        Me.LabelControl4.Location = New System.Drawing.Point(27, 187)
        Me.LabelControl4.Name = "LabelControl4"
        Me.LabelControl4.Size = New System.Drawing.Size(120, 13)
        Me.LabelControl4.TabIndex = 11
        Me.LabelControl4.Text = "Unit Usaha/Perusahaan :"
        Me.LabelControl4.Visible = False
        '
        'tunit
        '
        Me.tunit.Enabled = False
        Me.tunit.Location = New System.Drawing.Point(153, 184)
        Me.tunit.Name = "tunit"
        Me.tunit.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        '  Me.tunit.Properties.LookAndFeel.TouchUIMode = DevExpress.LookAndFeel.TouchUIMode.[False]
        Me.tunit.Size = New System.Drawing.Size(240, 20)
        Me.tunit.TabIndex = 12
        Me.tunit.Visible = False
        '
        'fdepart_divisi3
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(373, 154)
        Me.Controls.Add(Me.tunit)
        Me.Controls.Add(Me.LabelControl4)
        Me.Controls.Add(Me.btkeluar)
        Me.Controls.Add(Me.btsimpan)
        Me.Controls.Add(Me.tnama)
        Me.Controls.Add(Me.tkode)
        Me.Controls.Add(Me.LabelControl3)
        Me.Controls.Add(Me.LabelControl2)
        Me.Controls.Add(Me.LabelControl1)
        Me.Controls.Add(Me.tdepart)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "fdepart_divisi3"
        Me.Text = "Divisi (add/edit)"
        CType(Me.tnama.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tkode.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tdepart.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.tunit.Properties, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btkeluar As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents btsimpan As DevExpress.XtraEditors.SimpleButton
    Friend WithEvents tnama As DevExpress.XtraEditors.TextEdit
    Friend WithEvents tkode As DevExpress.XtraEditors.TextEdit
    Friend WithEvents LabelControl3 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl2 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents LabelControl1 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tdepart As DevExpress.XtraEditors.LookUpEdit
    Friend WithEvents LabelControl4 As DevExpress.XtraEditors.LabelControl
    Friend WithEvents tunit As DevExpress.XtraEditors.TextEdit
End Class
