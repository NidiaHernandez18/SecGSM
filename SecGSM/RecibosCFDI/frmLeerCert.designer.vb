<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLeerCert
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtVigencia = New System.Windows.Forms.TextBox()
        Me.txtCertCli = New System.Windows.Forms.TextBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.lblserie = New System.Windows.Forms.TextBox()
        Me.txtTipo = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtRFC = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtParametros = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmdAplicar = New System.Windows.Forms.Button()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtRazonSocial = New System.Windows.Forms.TextBox()
        Me.cmdCertCli = New System.Windows.Forms.Button()
        Me.FileDialog = New System.Windows.Forms.OpenFileDialog()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(15, 46)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(108, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Certificado del cliente"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(15, 72)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(92, 13)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Vigencia en años:"
        '
        'txtVigencia
        '
        Me.txtVigencia.BackColor = System.Drawing.SystemColors.Control
        Me.txtVigencia.Location = New System.Drawing.Point(155, 72)
        Me.txtVigencia.Name = "txtVigencia"
        Me.txtVigencia.ReadOnly = True
        Me.txtVigencia.Size = New System.Drawing.Size(51, 20)
        Me.txtVigencia.TabIndex = 5
        '
        'txtCertCli
        '
        Me.txtCertCli.Location = New System.Drawing.Point(155, 46)
        Me.txtCertCli.Name = "txtCertCli"
        Me.txtCertCli.Size = New System.Drawing.Size(245, 20)
        Me.txtCertCli.TabIndex = 3
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.BackColor = System.Drawing.Color.SlateGray
        Me.GroupBox3.Controls.Add(Me.lblserie)
        Me.GroupBox3.Controls.Add(Me.txtTipo)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Controls.Add(Me.Label11)
        Me.GroupBox3.Controls.Add(Me.Label6)
        Me.GroupBox3.Controls.Add(Me.txtRFC)
        Me.GroupBox3.Controls.Add(Me.Label8)
        Me.GroupBox3.Controls.Add(Me.txtParametros)
        Me.GroupBox3.Controls.Add(Me.Label3)
        Me.GroupBox3.Controls.Add(Me.cmdAplicar)
        Me.GroupBox3.Controls.Add(Me.Label10)
        Me.GroupBox3.Controls.Add(Me.txtRazonSocial)
        Me.GroupBox3.Controls.Add(Me.cmdCertCli)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.txtCertCli)
        Me.GroupBox3.Controls.Add(Me.txtVigencia)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.ForeColor = System.Drawing.Color.White
        Me.GroupBox3.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(499, 323)
        Me.GroupBox3.TabIndex = 0
        Me.GroupBox3.TabStop = False
        '
        'lblserie
        '
        Me.lblserie.BackColor = System.Drawing.SystemColors.Control
        Me.lblserie.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblserie.Location = New System.Drawing.Point(155, 153)
        Me.lblserie.Name = "lblserie"
        Me.lblserie.ReadOnly = True
        Me.lblserie.Size = New System.Drawing.Size(245, 20)
        Me.lblserie.TabIndex = 67
        '
        'txtTipo
        '
        Me.txtTipo.AutoSize = True
        Me.txtTipo.Location = New System.Drawing.Point(403, 75)
        Me.txtTipo.Name = "txtTipo"
        Me.txtTipo.Size = New System.Drawing.Size(10, 13)
        Me.txtTipo.TabIndex = 66
        Me.txtTipo.Tag = ""
        Me.txtTipo.Text = "-"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(303, 72)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(84, 13)
        Me.Label1.TabIndex = 65
        Me.Label1.Text = "Tipo Certificado:"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(15, 187)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(116, 13)
        Me.Label11.TabIndex = 64
        Me.Label11.Text = "Parámetros de sistema:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.Label6.Location = New System.Drawing.Point(15, 156)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(103, 13)
        Me.Label6.TabIndex = 55
        Me.Label6.Text = "Serie del certificado:"
        '
        'txtRFC
        '
        Me.txtRFC.BackColor = System.Drawing.SystemColors.Control
        Me.txtRFC.Location = New System.Drawing.Point(155, 124)
        Me.txtRFC.Name = "txtRFC"
        Me.txtRFC.ReadOnly = True
        Me.txtRFC.Size = New System.Drawing.Size(245, 20)
        Me.txtRFC.TabIndex = 7
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(15, 124)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(40, 13)
        Me.Label8.TabIndex = 59
        Me.Label8.Tag = ""
        Me.Label8.Text = "R.F.C.:"
        '
        'txtParametros
        '
        Me.txtParametros.BackColor = System.Drawing.SystemColors.Control
        Me.txtParametros.Location = New System.Drawing.Point(155, 187)
        Me.txtParametros.Multiline = True
        Me.txtParametros.Name = "txtParametros"
        Me.txtParametros.ReadOnly = True
        Me.txtParametros.Size = New System.Drawing.Size(326, 86)
        Me.txtParametros.TabIndex = 11
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(15, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(281, 18)
        Me.Label3.TabIndex = 54
        Me.Label3.Text = "1. Indica los datos del certificado del cliente:"
        '
        'cmdAplicar
        '
        Me.cmdAplicar.ForeColor = System.Drawing.Color.Black
        Me.cmdAplicar.Location = New System.Drawing.Point(382, 279)
        Me.cmdAplicar.Name = "cmdAplicar"
        Me.cmdAplicar.Size = New System.Drawing.Size(99, 26)
        Me.cmdAplicar.TabIndex = 14
        Me.cmdAplicar.Text = "Aplicar"
        Me.cmdAplicar.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(15, 98)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(73, 13)
        Me.Label10.TabIndex = 51
        Me.Label10.Text = "Razon Social:"
        '
        'txtRazonSocial
        '
        Me.txtRazonSocial.BackColor = System.Drawing.SystemColors.Control
        Me.txtRazonSocial.Location = New System.Drawing.Point(155, 98)
        Me.txtRazonSocial.Name = "txtRazonSocial"
        Me.txtRazonSocial.ReadOnly = True
        Me.txtRazonSocial.Size = New System.Drawing.Size(326, 20)
        Me.txtRazonSocial.TabIndex = 6
        '
        'cmdCertCli
        '
        Me.cmdCertCli.ForeColor = System.Drawing.Color.Black
        Me.cmdCertCli.Location = New System.Drawing.Point(406, 46)
        Me.cmdCertCli.Name = "cmdCertCli"
        Me.cmdCertCli.Size = New System.Drawing.Size(75, 23)
        Me.cmdCertCli.TabIndex = 4
        Me.cmdCertCli.Text = "Leer"
        Me.cmdCertCli.UseVisualStyleBackColor = True
        '
        'frmLeerCert
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(523, 347)
        Me.Controls.Add(Me.GroupBox3)
        Me.MaximizeBox = False
        Me.Name = "frmLeerCert"
        Me.Text = "Generación de Certificados de uso de sistemas a clientes"
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtCertCli As System.Windows.Forms.TextBox
    Friend WithEvents txtVigencia As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents cmdCertCli As System.Windows.Forms.Button
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtRazonSocial As System.Windows.Forms.TextBox
    Friend WithEvents cmdAplicar As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtRFC As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtParametros As System.Windows.Forms.TextBox
    Friend WithEvents txtTipo As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblserie As System.Windows.Forms.TextBox
    Friend WithEvents FileDialog As System.Windows.Forms.OpenFileDialog

End Class
