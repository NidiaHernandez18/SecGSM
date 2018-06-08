<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmlogin
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmlogin))
        Me.label5 = New System.Windows.Forms.Label()
        Me.bttn_Cancelar = New System.Windows.Forms.Button()
        Me.bttn_OK = New System.Windows.Forms.Button()
        Me.pictureBox1 = New System.Windows.Forms.PictureBox()
        Me.pictureBox2 = New System.Windows.Forms.PictureBox()
        Me.panel1 = New System.Windows.Forms.Panel()
        Me.label4 = New System.Windows.Forms.Label()
        Me.pictureBox3 = New System.Windows.Forms.PictureBox()
        Me.label3 = New System.Windows.Forms.Label()
        Me.label2 = New System.Windows.Forms.Label()
        Me.label1 = New System.Windows.Forms.Label()
        Me.txt_password = New System.Windows.Forms.TextBox()
        Me.txt_Usuario = New System.Windows.Forms.TextBox()
        Me.comB_Empresas = New System.Windows.Forms.ComboBox()
        CType(Me.pictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.panel1.SuspendLayout()
        CType(Me.pictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'label5
        '
        Me.label5.AutoSize = True
        Me.label5.Location = New System.Drawing.Point(238, 361)
        Me.label5.Name = "label5"
        Me.label5.Size = New System.Drawing.Size(187, 13)
        Me.label5.TabIndex = 21
        Me.label5.Text = "2018 Grupo Sistecom de México S. C."
        '
        'bttn_Cancelar
        '
        Me.bttn_Cancelar.Location = New System.Drawing.Point(348, 287)
        Me.bttn_Cancelar.Name = "bttn_Cancelar"
        Me.bttn_Cancelar.Size = New System.Drawing.Size(93, 39)
        Me.bttn_Cancelar.TabIndex = 20
        Me.bttn_Cancelar.Text = "Cancelar"
        Me.bttn_Cancelar.UseVisualStyleBackColor = True
        '
        'bttn_OK
        '
        Me.bttn_OK.Location = New System.Drawing.Point(191, 287)
        Me.bttn_OK.Name = "bttn_OK"
        Me.bttn_OK.Size = New System.Drawing.Size(93, 39)
        Me.bttn_OK.TabIndex = 19
        Me.bttn_OK.Text = "OK"
        Me.bttn_OK.UseVisualStyleBackColor = True
        '
        'pictureBox1
        '
        Me.pictureBox1.BackgroundImage = CType(resources.GetObject("pictureBox1.BackgroundImage"), System.Drawing.Image)
        Me.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.pictureBox1.Location = New System.Drawing.Point(533, 177)
        Me.pictureBox1.Name = "pictureBox1"
        Me.pictureBox1.Size = New System.Drawing.Size(63, 66)
        Me.pictureBox1.TabIndex = 10
        Me.pictureBox1.TabStop = False
        '
        'pictureBox2
        '
        Me.pictureBox2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.pictureBox2.BackgroundImage = CType(resources.GetObject("pictureBox2.BackgroundImage"), System.Drawing.Image)
        Me.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pictureBox2.Location = New System.Drawing.Point(43, 305)
        Me.pictureBox2.Name = "pictureBox2"
        Me.pictureBox2.Size = New System.Drawing.Size(81, 78)
        Me.pictureBox2.TabIndex = 12
        Me.pictureBox2.TabStop = False
        '
        'panel1
        '
        Me.panel1.BackColor = System.Drawing.Color.SteelBlue
        Me.panel1.Controls.Add(Me.label4)
        Me.panel1.Controls.Add(Me.pictureBox3)
        Me.panel1.Location = New System.Drawing.Point(0, 0)
        Me.panel1.Name = "panel1"
        Me.panel1.Size = New System.Drawing.Size(633, 103)
        Me.panel1.TabIndex = 18
        '
        'label4
        '
        Me.label4.AutoSize = True
        Me.label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label4.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.label4.Location = New System.Drawing.Point(161, 43)
        Me.label4.Name = "label4"
        Me.label4.Size = New System.Drawing.Size(0, 24)
        Me.label4.TabIndex = 8
        Me.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pictureBox3
        '
        Me.pictureBox3.BackgroundImage = CType(resources.GetObject("pictureBox3.BackgroundImage"), System.Drawing.Image)
        Me.pictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.pictureBox3.Location = New System.Drawing.Point(43, 3)
        Me.pictureBox3.Name = "pictureBox3"
        Me.pictureBox3.Size = New System.Drawing.Size(86, 97)
        Me.pictureBox3.TabIndex = 7
        Me.pictureBox3.TabStop = False
        '
        'label3
        '
        Me.label3.AutoSize = True
        Me.label3.Location = New System.Drawing.Point(90, 244)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(61, 13)
        Me.label3.TabIndex = 17
        Me.label3.Text = "Contraseña"
        '
        'label2
        '
        Me.label2.AutoSize = True
        Me.label2.Location = New System.Drawing.Point(90, 199)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(43, 13)
        Me.label2.TabIndex = 16
        Me.label2.Text = "Usuario"
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.Location = New System.Drawing.Point(90, 149)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(60, 26)
        Me.label1.TabIndex = 15
        Me.label1.Text = "Seleccione" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Empresa:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'txt_password
        '
        Me.txt_password.Location = New System.Drawing.Point(186, 244)
        Me.txt_password.Name = "txt_password"
        Me.txt_password.Size = New System.Drawing.Size(287, 20)
        Me.txt_password.TabIndex = 14
        Me.txt_password.UseSystemPasswordChar = True
        '
        'txt_Usuario
        '
        Me.txt_Usuario.Location = New System.Drawing.Point(186, 199)
        Me.txt_Usuario.Name = "txt_Usuario"
        Me.txt_Usuario.Size = New System.Drawing.Size(287, 20)
        Me.txt_Usuario.TabIndex = 13
        '
        'comB_Empresas
        '
        Me.comB_Empresas.FormattingEnabled = True
        Me.comB_Empresas.Location = New System.Drawing.Point(186, 154)
        Me.comB_Empresas.Name = "comB_Empresas"
        Me.comB_Empresas.Size = New System.Drawing.Size(288, 21)
        Me.comB_Empresas.TabIndex = 11
        '
        'frmlogin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(632, 395)
        Me.Controls.Add(Me.label5)
        Me.Controls.Add(Me.bttn_Cancelar)
        Me.Controls.Add(Me.bttn_OK)
        Me.Controls.Add(Me.pictureBox1)
        Me.Controls.Add(Me.pictureBox2)
        Me.Controls.Add(Me.panel1)
        Me.Controls.Add(Me.label3)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.txt_password)
        Me.Controls.Add(Me.txt_Usuario)
        Me.Controls.Add(Me.comB_Empresas)
        Me.Name = "frmlogin"
        Me.ShowIcon = False
        CType(Me.pictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.panel1.ResumeLayout(False)
        Me.panel1.PerformLayout()
        CType(Me.pictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents label5 As System.Windows.Forms.Label
    Private WithEvents bttn_Cancelar As System.Windows.Forms.Button
    Private WithEvents bttn_OK As System.Windows.Forms.Button
    Private WithEvents pictureBox1 As System.Windows.Forms.PictureBox
    Private WithEvents pictureBox2 As System.Windows.Forms.PictureBox
    Private WithEvents panel1 As System.Windows.Forms.Panel
    Private WithEvents label4 As System.Windows.Forms.Label
    Private WithEvents pictureBox3 As System.Windows.Forms.PictureBox
    Private WithEvents label3 As System.Windows.Forms.Label
    Private WithEvents label2 As System.Windows.Forms.Label
    Private WithEvents label1 As System.Windows.Forms.Label
    Private WithEvents txt_password As System.Windows.Forms.TextBox
    Private WithEvents txt_Usuario As System.Windows.Forms.TextBox
    Private WithEvents comB_Empresas As System.Windows.Forms.ComboBox
End Class
