<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmusuarios2
    Inherits SecGSM.frmusuarios

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
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.cboSucursales = New System.Windows.Forms.ComboBox
        Me.cboEmpresas = New System.Windows.Forms.ComboBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.PictureBox2 = New System.Windows.Forms.PictureBox
        Me.lblAplicacion = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.lblVersion = New System.Windows.Forms.Label
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.Panel1.Controls.Add(Me.lblVersion)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.lblAplicacion)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.cboEmpresas)
        Me.Panel1.Controls.Add(Me.PictureBox2)
        Me.Panel1.Controls.Add(Me.cboSucursales)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Size = New System.Drawing.Size(343, 256)
        Me.Panel1.TabIndex = 0
        Me.Panel1.Controls.SetChildIndex(Me.Label1, 0)
        Me.Panel1.Controls.SetChildIndex(Me.txtuser, 0)
        Me.Panel1.Controls.SetChildIndex(Me.Label2, 0)
        Me.Panel1.Controls.SetChildIndex(Me.txtpass, 0)
        Me.Panel1.Controls.SetChildIndex(Me.btnAceptar, 0)
        Me.Panel1.Controls.SetChildIndex(Me.Label3, 0)
        Me.Panel1.Controls.SetChildIndex(Me.btnsalir, 0)
        Me.Panel1.Controls.SetChildIndex(Me.Label4, 0)
        Me.Panel1.Controls.SetChildIndex(Me.cboSucursales, 0)
        Me.Panel1.Controls.SetChildIndex(Me.PictureBox2, 0)
        Me.Panel1.Controls.SetChildIndex(Me.cboEmpresas, 0)
        Me.Panel1.Controls.SetChildIndex(Me.PictureBox1, 0)
        Me.Panel1.Controls.SetChildIndex(Me.Label5, 0)
        Me.Panel1.Controls.SetChildIndex(Me.lblAplicacion, 0)
        Me.Panel1.Controls.SetChildIndex(Me.Label6, 0)
        Me.Panel1.Controls.SetChildIndex(Me.lblVersion, 0)
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(26, 39)
        Me.PictureBox1.Size = New System.Drawing.Size(48, 53)
        '
        'txtpass
        '
        Me.txtpass.Location = New System.Drawing.Point(98, 176)
        Me.txtpass.Size = New System.Drawing.Size(225, 20)
        Me.txtpass.TabIndex = 7
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(31, 179)
        Me.Label2.TabIndex = 6
        '
        'txtuser
        '
        Me.txtuser.Location = New System.Drawing.Point(98, 150)
        Me.txtuser.Size = New System.Drawing.Size(225, 20)
        Me.txtuser.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(31, 153)
        Me.Label1.TabIndex = 4
        '
        'btnAceptar
        '
        Me.btnAceptar.Location = New System.Drawing.Point(150, 202)
        Me.btnAceptar.TabIndex = 8
        '
        'btnsalir
        '
        Me.btnsalir.Location = New System.Drawing.Point(248, 202)
        Me.btnsalir.TabIndex = 9
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(31, 126)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(51, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Sucursal:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(31, 100)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(51, 13)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Empresa:"
        '
        'cboSucursales
        '
        Me.cboSucursales.FormattingEnabled = True
        Me.cboSucursales.Location = New System.Drawing.Point(98, 123)
        Me.cboSucursales.Name = "cboSucursales"
        Me.cboSucursales.Size = New System.Drawing.Size(225, 21)
        Me.cboSucursales.TabIndex = 3
        '
        'cboEmpresas
        '
        Me.cboEmpresas.FormattingEnabled = True
        Me.cboEmpresas.Location = New System.Drawing.Point(98, 96)
        Me.cboEmpresas.Name = "cboEmpresas"
        Me.cboEmpresas.Size = New System.Drawing.Size(225, 21)
        Me.cboEmpresas.TabIndex = 1
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(161, 78)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(94, 14)
        Me.Label5.TabIndex = 114
        Me.Label5.Text = "Inicio de Sesión"
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = Global.SecGSM.My.Resources.Resources.Capa_1
        Me.PictureBox2.Location = New System.Drawing.Point(0, -8)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(343, 32)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox2.TabIndex = 115
        Me.PictureBox2.TabStop = False
        '
        'lblAplicacion
        '
        Me.lblAplicacion.Font = New System.Drawing.Font("Arial", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAplicacion.Location = New System.Drawing.Point(94, 41)
        Me.lblAplicacion.Name = "lblAplicacion"
        Me.lblAplicacion.Size = New System.Drawing.Size(229, 19)
        Me.lblAplicacion.TabIndex = 116
        Me.lblAplicacion.Text = "Aplicación"
        Me.lblAplicacion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label6.Location = New System.Drawing.Point(96, 237)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(159, 14)
        Me.Label6.TabIndex = 117
        Me.Label6.Text = "Grupo Sistecom de México S.C."
        '
        'lblVersion
        '
        Me.lblVersion.Font = New System.Drawing.Font("Arial", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVersion.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblVersion.Location = New System.Drawing.Point(261, 60)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.Size = New System.Drawing.Size(62, 18)
        Me.lblVersion.TabIndex = 118
        Me.lblVersion.Text = "3.5.0"
        '
        'frmusuarios2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(341, 254)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmusuarios2"
        Me.Text = ""
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cboSucursales As System.Windows.Forms.ComboBox
    Friend WithEvents cboEmpresas As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents lblAplicacion As System.Windows.Forms.Label
    Public WithEvents lblVersion As System.Windows.Forms.Label
End Class
