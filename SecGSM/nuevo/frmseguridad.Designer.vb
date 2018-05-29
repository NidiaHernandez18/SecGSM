<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmseguridad
    Inherits System.Windows.Forms.Form

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
        Me.CheckBloquear = New System.Windows.Forms.CheckBox
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.CheckDes = New System.Windows.Forms.CheckBox
        Me.CheckLogon = New System.Windows.Forms.CheckBox
        Me.Txtconf = New System.Windows.Forms.TextBox
        Me.Txtpass = New System.Windows.Forms.TextBox
        Me.TxtFull = New System.Windows.Forms.TextBox
        Me.TxtUser = New System.Windows.Forms.TextBox
        Me.txtNumBloq = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.txrexpira = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Button1 = New System.Windows.Forms.Button
        Me.btnsalir = New System.Windows.Forms.Button
        Me.BtnOk = New System.Windows.Forms.Button
        Me.Btneliminar = New System.Windows.Forms.Button
        Me.btnguardar = New System.Windows.Forms.Button
        Me.BtnCancel = New System.Windows.Forms.Button
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtcorreo = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.txttel = New System.Windows.Forms.TextBox
        Me.txtext = New System.Windows.Forms.TextBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.btnEditar = New System.Windows.Forms.Button
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'CheckBloquear
        '
        Me.CheckBloquear.Enabled = False
        Me.CheckBloquear.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckBloquear.Location = New System.Drawing.Point(16, 195)
        Me.CheckBloquear.Name = "CheckBloquear"
        Me.CheckBloquear.Size = New System.Drawing.Size(136, 24)
        Me.CheckBloquear.TabIndex = 44
        Me.CheckBloquear.Text = "Bloquear cuenta"
        '
        'GroupBox3
        '
        Me.GroupBox3.Location = New System.Drawing.Point(8, 68)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(456, 8)
        Me.GroupBox3.TabIndex = 51
        Me.GroupBox3.TabStop = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(16, 105)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(96, 13)
        Me.Label4.TabIndex = 47
        Me.Label4.Text = "Confirmar clave"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(16, 81)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(38, 13)
        Me.Label3.TabIndex = 46
        Me.Label3.Text = "Clave"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(16, 44)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(107, 13)
        Me.Label2.TabIndex = 45
        Me.Label2.Text = "Nombre completo"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(16, 12)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(50, 13)
        Me.Label1.TabIndex = 44
        Me.Label1.Text = "Usuario"
        '
        'CheckDes
        '
        Me.CheckDes.Enabled = False
        Me.CheckDes.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckDes.Location = New System.Drawing.Point(16, 173)
        Me.CheckDes.Name = "CheckDes"
        Me.CheckDes.Size = New System.Drawing.Size(139, 24)
        Me.CheckDes.TabIndex = 43
        Me.CheckDes.Text = "Cuenta desabilitada"
        '
        'CheckLogon
        '
        Me.CheckLogon.Enabled = False
        Me.CheckLogon.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckLogon.Location = New System.Drawing.Point(16, 149)
        Me.CheckLogon.Name = "CheckLogon"
        Me.CheckLogon.Size = New System.Drawing.Size(296, 24)
        Me.CheckLogon.TabIndex = 42
        Me.CheckLogon.Text = "Cambiar clave cuando entre a la aplicación"
        '
        'Txtconf
        '
        Me.Txtconf.Enabled = False
        Me.Txtconf.Location = New System.Drawing.Point(136, 105)
        Me.Txtconf.Name = "Txtconf"
        Me.Txtconf.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.Txtconf.Size = New System.Drawing.Size(256, 20)
        Me.Txtconf.TabIndex = 41
        '
        'Txtpass
        '
        Me.Txtpass.Enabled = False
        Me.Txtpass.Location = New System.Drawing.Point(136, 81)
        Me.Txtpass.Name = "Txtpass"
        Me.Txtpass.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.Txtpass.Size = New System.Drawing.Size(256, 20)
        Me.Txtpass.TabIndex = 40
        '
        'TxtFull
        '
        Me.TxtFull.Enabled = False
        Me.TxtFull.Location = New System.Drawing.Point(136, 44)
        Me.TxtFull.Name = "TxtFull"
        Me.TxtFull.Size = New System.Drawing.Size(320, 20)
        Me.TxtFull.TabIndex = 39
        '
        'TxtUser
        '
        Me.TxtUser.Enabled = False
        Me.TxtUser.Location = New System.Drawing.Point(136, 12)
        Me.TxtUser.Name = "TxtUser"
        Me.TxtUser.Size = New System.Drawing.Size(320, 20)
        Me.TxtUser.TabIndex = 38
        '
        'txtNumBloq
        '
        Me.txtNumBloq.Enabled = False
        Me.txtNumBloq.Location = New System.Drawing.Point(392, 170)
        Me.txtNumBloq.Name = "txtNumBloq"
        Me.txtNumBloq.Size = New System.Drawing.Size(32, 20)
        Me.txtNumBloq.TabIndex = 45
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(178, 40)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(202, 13)
        Me.Label8.TabIndex = 60
        Me.Label8.Text = "Numero de Intentos para Bloquear"
        '
        'txrexpira
        '
        Me.txrexpira.Enabled = False
        Me.txrexpira.Location = New System.Drawing.Point(392, 197)
        Me.txrexpira.Name = "txrexpira"
        Me.txrexpira.Size = New System.Drawing.Size(32, 20)
        Me.txrexpira.TabIndex = 46
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(152, 67)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(232, 13)
        Me.Label5.TabIndex = 62
        Me.Label5.Text = "Numero de dias para que expira la clave"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(273, 344)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(63, 21)
        Me.Button1.TabIndex = 54
        Me.Button1.Text = "&Reporte"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'btnsalir
        '
        Me.btnsalir.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.btnsalir.Location = New System.Drawing.Point(340, 344)
        Me.btnsalir.Name = "btnsalir"
        Me.btnsalir.Size = New System.Drawing.Size(63, 21)
        Me.btnsalir.TabIndex = 55
        Me.btnsalir.Text = "&Salir"
        Me.btnsalir.UseVisualStyleBackColor = True
        '
        'BtnOk
        '
        Me.BtnOk.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.BtnOk.Location = New System.Drawing.Point(78, 344)
        Me.BtnOk.Name = "BtnOk"
        Me.BtnOk.Size = New System.Drawing.Size(63, 21)
        Me.BtnOk.TabIndex = 51
        Me.BtnOk.Text = "&Nuevo"
        Me.BtnOk.UseVisualStyleBackColor = True
        '
        'Btneliminar
        '
        Me.Btneliminar.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.Btneliminar.Location = New System.Drawing.Point(207, 344)
        Me.Btneliminar.Name = "Btneliminar"
        Me.Btneliminar.Size = New System.Drawing.Size(63, 21)
        Me.Btneliminar.TabIndex = 53
        Me.Btneliminar.Text = "&Borrar"
        Me.Btneliminar.UseVisualStyleBackColor = True
        '
        'btnguardar
        '
        Me.btnguardar.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.btnguardar.Location = New System.Drawing.Point(142, 344)
        Me.btnguardar.Name = "btnguardar"
        Me.btnguardar.Size = New System.Drawing.Size(63, 21)
        Me.btnguardar.TabIndex = 73
        Me.btnguardar.Text = "&Guardar"
        Me.btnguardar.UseVisualStyleBackColor = True
        '
        'BtnCancel
        '
        Me.BtnCancel.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.BtnCancel.Location = New System.Drawing.Point(207, 344)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(63, 21)
        Me.BtnCancel.TabIndex = 74
        Me.BtnCancel.Text = "&Cancel"
        Me.BtnCancel.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Label8)
        Me.GroupBox4.Controls.Add(Me.Label5)
        Me.GroupBox4.Location = New System.Drawing.Point(8, 133)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(434, 99)
        Me.GroupBox4.TabIndex = 75
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Seguridad"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(18, 25)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(111, 13)
        Me.Label7.TabIndex = 65
        Me.Label7.Text = "Correo electrónico"
        '
        'txtcorreo
        '
        Me.txtcorreo.Enabled = False
        Me.txtcorreo.Location = New System.Drawing.Point(135, 22)
        Me.txtcorreo.Name = "txtcorreo"
        Me.txtcorreo.Size = New System.Drawing.Size(277, 20)
        Me.txtcorreo.TabIndex = 48
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(18, 53)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(56, 13)
        Me.Label6.TabIndex = 66
        Me.Label6.Text = "Teléfono"
        '
        'txttel
        '
        Me.txttel.Enabled = False
        Me.txttel.Location = New System.Drawing.Point(80, 50)
        Me.txttel.Name = "txttel"
        Me.txttel.Size = New System.Drawing.Size(137, 20)
        Me.txttel.TabIndex = 49
        '
        'txtext
        '
        Me.txtext.Enabled = False
        Me.txtext.Location = New System.Drawing.Point(291, 53)
        Me.txtext.Name = "txtext"
        Me.txtext.Size = New System.Drawing.Size(120, 20)
        Me.txtext.TabIndex = 50
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.txtext)
        Me.GroupBox1.Controls.Add(Me.txttel)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.txtcorreo)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 238)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(434, 88)
        Me.GroupBox1.TabIndex = 47
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Información adicional"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(223, 50)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(62, 13)
        Me.Label9.TabIndex = 68
        Me.Label9.Text = "Extensión"
        '
        'btnEditar
        '
        Me.btnEditar.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.btnEditar.Location = New System.Drawing.Point(142, 344)
        Me.btnEditar.Name = "btnEditar"
        Me.btnEditar.Size = New System.Drawing.Size(63, 21)
        Me.btnEditar.TabIndex = 76
        Me.btnEditar.Text = "&Editar"
        Me.btnEditar.UseVisualStyleBackColor = True
        '
        'frmseguridad
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(465, 396)
        Me.Controls.Add(Me.btnEditar)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btnsalir)
        Me.Controls.Add(Me.BtnOk)
        Me.Controls.Add(Me.Btneliminar)
        Me.Controls.Add(Me.btnguardar)
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.txrexpira)
        Me.Controls.Add(Me.txtNumBloq)
        Me.Controls.Add(Me.CheckBloquear)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.CheckDes)
        Me.Controls.Add(Me.CheckLogon)
        Me.Controls.Add(Me.Txtconf)
        Me.Controls.Add(Me.Txtpass)
        Me.Controls.Add(Me.TxtFull)
        Me.Controls.Add(Me.TxtUser)
        Me.Controls.Add(Me.GroupBox4)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.KeyPreview = True
        Me.Name = "frmseguridad"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Seguridad"
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents CheckBloquear As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents CheckDes As System.Windows.Forms.CheckBox
    Friend WithEvents CheckLogon As System.Windows.Forms.CheckBox
    Friend WithEvents Txtconf As System.Windows.Forms.TextBox
    Friend WithEvents Txtpass As System.Windows.Forms.TextBox
    Friend WithEvents TxtFull As System.Windows.Forms.TextBox
    Friend WithEvents TxtUser As System.Windows.Forms.TextBox
    Friend WithEvents txtNumBloq As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txrexpira As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents btnsalir As System.Windows.Forms.Button
    Friend WithEvents BtnOk As System.Windows.Forms.Button
    Friend WithEvents Btneliminar As System.Windows.Forms.Button
    Friend WithEvents btnguardar As System.Windows.Forms.Button
    Friend WithEvents BtnCancel As System.Windows.Forms.Button
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtcorreo As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txttel As System.Windows.Forms.TextBox
    Friend WithEvents txtext As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents btnEditar As System.Windows.Forms.Button
End Class
