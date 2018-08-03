<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPermisosR
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPermisosR))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.label2 = New System.Windows.Forms.Label()
        Me.label1 = New System.Windows.Forms.Label()
        Me.comboB_Empresa = New System.Windows.Forms.ComboBox()
        Me.comboB_SelecSistema = New System.Windows.Forms.ComboBox()
        Me.panel1 = New System.Windows.Forms.Panel()
        Me.pictureBox1 = New System.Windows.Forms.PictureBox()
        Me.label4 = New System.Windows.Forms.Label()
        Me.tabControl2 = New System.Windows.Forms.TabControl()
        Me.tabPage4 = New System.Windows.Forms.TabPage()
        Me.checkLis_Lectura = New System.Windows.Forms.CheckedListBox()
        Me.checkList_modulos = New System.Windows.Forms.CheckedListBox()
        Me.comboB_modulos = New System.Windows.Forms.ComboBox()
        Me.tabControl1 = New System.Windows.Forms.TabControl()
        Me.tabPage1 = New System.Windows.Forms.TabPage()
        Me.label6 = New System.Windows.Forms.Label()
        Me.label5 = New System.Windows.Forms.Label()
        Me.txt_password = New System.Windows.Forms.TextBox()
        Me.txt_usuario = New System.Windows.Forms.TextBox()
        Me.label3 = New System.Windows.Forms.Label()
        Me.txt_nombre = New System.Windows.Forms.TextBox()
        Me.dgv_permisos = New System.Windows.Forms.DataGridView()
        Me.bttn_Cencelar = New System.Windows.Forms.Button()
        Me.bttn_Grabar = New System.Windows.Forms.Button()
        Me.bttn_Cerrar = New System.Windows.Forms.Button()
        Me.bttn_Eliminar = New System.Windows.Forms.Button()
        Me.bttn_Editar = New System.Windows.Forms.Button()
        Me.btt_Nuevo = New System.Windows.Forms.Button()
        Me.panel1.SuspendLayout()
        CType(Me.pictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabControl2.SuspendLayout()
        Me.tabPage4.SuspendLayout()
        Me.tabControl1.SuspendLayout()
        Me.tabPage1.SuspendLayout()
        CType(Me.dgv_permisos, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'label2
        '
        Me.label2.AutoSize = True
        Me.label2.Location = New System.Drawing.Point(39, 180)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(48, 13)
        Me.label2.TabIndex = 19
        Me.label2.Text = "Empresa"
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.Location = New System.Drawing.Point(39, 131)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(111, 13)
        Me.label1.TabIndex = 18
        Me.label1.Text = "Seleccion del Sistema"
        '
        'comboB_Empresa
        '
        Me.comboB_Empresa.FormattingEnabled = True
        Me.comboB_Empresa.Location = New System.Drawing.Point(38, 196)
        Me.comboB_Empresa.Name = "comboB_Empresa"
        Me.comboB_Empresa.Size = New System.Drawing.Size(192, 21)
        Me.comboB_Empresa.TabIndex = 17
        '
        'comboB_SelecSistema
        '
        Me.comboB_SelecSistema.FormattingEnabled = True
        Me.comboB_SelecSistema.Location = New System.Drawing.Point(38, 147)
        Me.comboB_SelecSistema.Name = "comboB_SelecSistema"
        Me.comboB_SelecSistema.Size = New System.Drawing.Size(192, 21)
        Me.comboB_SelecSistema.TabIndex = 16
        '
        'panel1
        '
        Me.panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.panel1.BackColor = System.Drawing.Color.SteelBlue
        Me.panel1.Controls.Add(Me.pictureBox1)
        Me.panel1.Controls.Add(Me.label4)
        Me.panel1.Location = New System.Drawing.Point(-1, -2)
        Me.panel1.Name = "panel1"
        Me.panel1.Size = New System.Drawing.Size(576, 93)
        Me.panel1.TabIndex = 15
        '
        'pictureBox1
        '
        Me.pictureBox1.BackgroundImage = CType(resources.GetObject("pictureBox1.BackgroundImage"), System.Drawing.Image)
        Me.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pictureBox1.Location = New System.Drawing.Point(43, 1)
        Me.pictureBox1.Name = "pictureBox1"
        Me.pictureBox1.Size = New System.Drawing.Size(90, 89)
        Me.pictureBox1.TabIndex = 10
        Me.pictureBox1.TabStop = False
        '
        'label4
        '
        Me.label4.AutoSize = True
        Me.label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.label4.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.label4.Location = New System.Drawing.Point(154, 31)
        Me.label4.Name = "label4"
        Me.label4.Size = New System.Drawing.Size(295, 24)
        Me.label4.TabIndex = 9
        Me.label4.Text = "Configuracion de los Permisos"
        Me.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'tabControl2
        '
        Me.tabControl2.Controls.Add(Me.tabPage4)
        Me.tabControl2.Location = New System.Drawing.Point(251, 102)
        Me.tabControl2.Name = "tabControl2"
        Me.tabControl2.SelectedIndex = 0
        Me.tabControl2.Size = New System.Drawing.Size(304, 153)
        Me.tabControl2.TabIndex = 14
        '
        'tabPage4
        '
        Me.tabPage4.BackColor = System.Drawing.Color.Transparent
        Me.tabPage4.Controls.Add(Me.checkLis_Lectura)
        Me.tabPage4.Controls.Add(Me.checkList_modulos)
        Me.tabPage4.Controls.Add(Me.comboB_modulos)
        Me.tabPage4.Location = New System.Drawing.Point(4, 22)
        Me.tabPage4.Name = "tabPage4"
        Me.tabPage4.Padding = New System.Windows.Forms.Padding(3)
        Me.tabPage4.Size = New System.Drawing.Size(296, 127)
        Me.tabPage4.TabIndex = 1
        Me.tabPage4.Text = "Modulos"
        '
        'checkLis_Lectura
        '
        Me.checkLis_Lectura.FormattingEnabled = True
        Me.checkLis_Lectura.Location = New System.Drawing.Point(162, 30)
        Me.checkLis_Lectura.Name = "checkLis_Lectura"
        Me.checkLis_Lectura.Size = New System.Drawing.Size(128, 94)
        Me.checkLis_Lectura.TabIndex = 15
        '
        'checkList_modulos
        '
        Me.checkList_modulos.FormattingEnabled = True
        Me.checkList_modulos.Location = New System.Drawing.Point(6, 30)
        Me.checkList_modulos.Name = "checkList_modulos"
        Me.checkList_modulos.Size = New System.Drawing.Size(153, 94)
        Me.checkList_modulos.TabIndex = 14
        '
        'comboB_modulos
        '
        Me.comboB_modulos.FormattingEnabled = True
        Me.comboB_modulos.Location = New System.Drawing.Point(6, 6)
        Me.comboB_modulos.Name = "comboB_modulos"
        Me.comboB_modulos.Size = New System.Drawing.Size(153, 21)
        Me.comboB_modulos.TabIndex = 0
        '
        'tabControl1
        '
        Me.tabControl1.Controls.Add(Me.tabPage1)
        Me.tabControl1.Location = New System.Drawing.Point(38, 261)
        Me.tabControl1.Name = "tabControl1"
        Me.tabControl1.SelectedIndex = 0
        Me.tabControl1.Size = New System.Drawing.Size(513, 185)
        Me.tabControl1.TabIndex = 13
        '
        'tabPage1
        '
        Me.tabPage1.BackColor = System.Drawing.Color.Transparent
        Me.tabPage1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.tabPage1.Controls.Add(Me.label6)
        Me.tabPage1.Controls.Add(Me.label5)
        Me.tabPage1.Controls.Add(Me.txt_password)
        Me.tabPage1.Controls.Add(Me.txt_usuario)
        Me.tabPage1.Controls.Add(Me.label3)
        Me.tabPage1.Controls.Add(Me.txt_nombre)
        Me.tabPage1.Controls.Add(Me.dgv_permisos)
        Me.tabPage1.Location = New System.Drawing.Point(4, 22)
        Me.tabPage1.Name = "tabPage1"
        Me.tabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.tabPage1.Size = New System.Drawing.Size(505, 159)
        Me.tabPage1.TabIndex = 0
        Me.tabPage1.Text = "Usuarios"
        '
        'label6
        '
        Me.label6.AutoSize = True
        Me.label6.Location = New System.Drawing.Point(11, 98)
        Me.label6.Name = "label6"
        Me.label6.Size = New System.Drawing.Size(53, 13)
        Me.label6.TabIndex = 11
        Me.label6.Text = "Password"
        '
        'label5
        '
        Me.label5.AutoSize = True
        Me.label5.Location = New System.Drawing.Point(11, 59)
        Me.label5.Name = "label5"
        Me.label5.Size = New System.Drawing.Size(43, 13)
        Me.label5.TabIndex = 10
        Me.label5.Text = "Usuario"
        '
        'txt_password
        '
        Me.txt_password.Location = New System.Drawing.Point(14, 114)
        Me.txt_password.Name = "txt_password"
        Me.txt_password.Size = New System.Drawing.Size(103, 20)
        Me.txt_password.TabIndex = 9
        '
        'txt_usuario
        '
        Me.txt_usuario.Location = New System.Drawing.Point(14, 75)
        Me.txt_usuario.Name = "txt_usuario"
        Me.txt_usuario.Size = New System.Drawing.Size(103, 20)
        Me.txt_usuario.TabIndex = 8
        '
        'label3
        '
        Me.label3.AutoSize = True
        Me.label3.Location = New System.Drawing.Point(11, 19)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(44, 13)
        Me.label3.TabIndex = 7
        Me.label3.Text = "Nombre"
        '
        'txt_nombre
        '
        Me.txt_nombre.Location = New System.Drawing.Point(14, 35)
        Me.txt_nombre.Name = "txt_nombre"
        Me.txt_nombre.Size = New System.Drawing.Size(182, 20)
        Me.txt_nombre.TabIndex = 1
        '
        'dgv_permisos
        '
        Me.dgv_permisos.AllowUserToAddRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.SkyBlue
        Me.dgv_permisos.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgv_permisos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.dgv_permisos.BackgroundColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.dgv_permisos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_permisos.Location = New System.Drawing.Point(209, 19)
        Me.dgv_permisos.Name = "dgv_permisos"
        Me.dgv_permisos.Size = New System.Drawing.Size(290, 129)
        Me.dgv_permisos.TabIndex = 0
        '
        'bttn_Cencelar
        '
        Me.bttn_Cencelar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bttn_Cencelar.ForeColor = System.Drawing.Color.DarkBlue
        Me.bttn_Cencelar.Image = CType(resources.GetObject("bttn_Cencelar.Image"), System.Drawing.Image)
        Me.bttn_Cencelar.Location = New System.Drawing.Point(286, 547)
        Me.bttn_Cencelar.Name = "bttn_Cencelar"
        Me.bttn_Cencelar.Size = New System.Drawing.Size(65, 59)
        Me.bttn_Cencelar.TabIndex = 25
        Me.bttn_Cencelar.Text = "Cancelar"
        Me.bttn_Cencelar.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.bttn_Cencelar.UseVisualStyleBackColor = True
        '
        'bttn_Grabar
        '
        Me.bttn_Grabar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bttn_Grabar.ForeColor = System.Drawing.Color.DarkBlue
        Me.bttn_Grabar.Image = CType(resources.GetObject("bttn_Grabar.Image"), System.Drawing.Image)
        Me.bttn_Grabar.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.bttn_Grabar.Location = New System.Drawing.Point(203, 547)
        Me.bttn_Grabar.Name = "bttn_Grabar"
        Me.bttn_Grabar.Size = New System.Drawing.Size(65, 59)
        Me.bttn_Grabar.TabIndex = 24
        Me.bttn_Grabar.Text = "Grabar"
        Me.bttn_Grabar.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.bttn_Grabar.UseVisualStyleBackColor = True
        '
        'bttn_Cerrar
        '
        Me.bttn_Cerrar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bttn_Cerrar.ForeColor = System.Drawing.Color.DarkBlue
        Me.bttn_Cerrar.Image = CType(resources.GetObject("bttn_Cerrar.Image"), System.Drawing.Image)
        Me.bttn_Cerrar.Location = New System.Drawing.Point(368, 482)
        Me.bttn_Cerrar.Name = "bttn_Cerrar"
        Me.bttn_Cerrar.Size = New System.Drawing.Size(65, 59)
        Me.bttn_Cerrar.TabIndex = 23
        Me.bttn_Cerrar.Text = "Cerrar"
        Me.bttn_Cerrar.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.bttn_Cerrar.UseVisualStyleBackColor = True
        '
        'bttn_Eliminar
        '
        Me.bttn_Eliminar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.bttn_Eliminar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bttn_Eliminar.ForeColor = System.Drawing.Color.DarkBlue
        Me.bttn_Eliminar.Image = CType(resources.GetObject("bttn_Eliminar.Image"), System.Drawing.Image)
        Me.bttn_Eliminar.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.bttn_Eliminar.Location = New System.Drawing.Point(286, 482)
        Me.bttn_Eliminar.Name = "bttn_Eliminar"
        Me.bttn_Eliminar.Size = New System.Drawing.Size(65, 59)
        Me.bttn_Eliminar.TabIndex = 22
        Me.bttn_Eliminar.Text = "Eliminar"
        Me.bttn_Eliminar.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.bttn_Eliminar.UseVisualStyleBackColor = True
        '
        'bttn_Editar
        '
        Me.bttn_Editar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.bttn_Editar.ForeColor = System.Drawing.Color.DarkBlue
        Me.bttn_Editar.Image = CType(resources.GetObject("bttn_Editar.Image"), System.Drawing.Image)
        Me.bttn_Editar.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.bttn_Editar.Location = New System.Drawing.Point(203, 482)
        Me.bttn_Editar.Name = "bttn_Editar"
        Me.bttn_Editar.Size = New System.Drawing.Size(65, 59)
        Me.bttn_Editar.TabIndex = 21
        Me.bttn_Editar.Text = "Editar"
        Me.bttn_Editar.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.bttn_Editar.UseVisualStyleBackColor = True
        '
        'btt_Nuevo
        '
        Me.btt_Nuevo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btt_Nuevo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btt_Nuevo.ForeColor = System.Drawing.Color.DarkBlue
        Me.btt_Nuevo.Image = CType(resources.GetObject("btt_Nuevo.Image"), System.Drawing.Image)
        Me.btt_Nuevo.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btt_Nuevo.Location = New System.Drawing.Point(122, 482)
        Me.btt_Nuevo.Name = "btt_Nuevo"
        Me.btt_Nuevo.Size = New System.Drawing.Size(65, 59)
        Me.btt_Nuevo.TabIndex = 20
        Me.btt_Nuevo.Text = "Nuevo"
        Me.btt_Nuevo.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btt_Nuevo.UseVisualStyleBackColor = True
        '
        'frmPermisosR
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(570, 669)
        Me.Controls.Add(Me.bttn_Cencelar)
        Me.Controls.Add(Me.bttn_Grabar)
        Me.Controls.Add(Me.bttn_Cerrar)
        Me.Controls.Add(Me.bttn_Eliminar)
        Me.Controls.Add(Me.bttn_Editar)
        Me.Controls.Add(Me.btt_Nuevo)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.comboB_Empresa)
        Me.Controls.Add(Me.comboB_SelecSistema)
        Me.Controls.Add(Me.panel1)
        Me.Controls.Add(Me.tabControl2)
        Me.Controls.Add(Me.tabControl1)
        Me.Name = "frmPermisosR"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Permisos"
        Me.panel1.ResumeLayout(False)
        Me.panel1.PerformLayout()
        CType(Me.pictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabControl2.ResumeLayout(False)
        Me.tabPage4.ResumeLayout(False)
        Me.tabControl1.ResumeLayout(False)
        Me.tabPage1.ResumeLayout(False)
        Me.tabPage1.PerformLayout()
        CType(Me.dgv_permisos, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents bttn_Cencelar As System.Windows.Forms.Button
    Private WithEvents bttn_Grabar As System.Windows.Forms.Button
    Private WithEvents bttn_Cerrar As System.Windows.Forms.Button
    Private WithEvents bttn_Eliminar As System.Windows.Forms.Button
    Private WithEvents bttn_Editar As System.Windows.Forms.Button
    Private WithEvents btt_Nuevo As System.Windows.Forms.Button
    Private WithEvents label2 As System.Windows.Forms.Label
    Private WithEvents label1 As System.Windows.Forms.Label
    Private WithEvents comboB_Empresa As System.Windows.Forms.ComboBox
    Private WithEvents comboB_SelecSistema As System.Windows.Forms.ComboBox
    Private WithEvents panel1 As System.Windows.Forms.Panel
    Private WithEvents pictureBox1 As System.Windows.Forms.PictureBox
    Private WithEvents label4 As System.Windows.Forms.Label
    Private WithEvents tabControl2 As System.Windows.Forms.TabControl
    Private WithEvents tabPage4 As System.Windows.Forms.TabPage
    Private WithEvents checkLis_Lectura As System.Windows.Forms.CheckedListBox
    Private WithEvents checkList_modulos As System.Windows.Forms.CheckedListBox
    Private WithEvents comboB_modulos As System.Windows.Forms.ComboBox
    Private WithEvents tabControl1 As System.Windows.Forms.TabControl
    Private WithEvents tabPage1 As System.Windows.Forms.TabPage
    Private WithEvents label6 As System.Windows.Forms.Label
    Private WithEvents label5 As System.Windows.Forms.Label
    Private WithEvents txt_password As System.Windows.Forms.TextBox
    Private WithEvents txt_usuario As System.Windows.Forms.TextBox
    Private WithEvents label3 As System.Windows.Forms.Label
    Private WithEvents txt_nombre As System.Windows.Forms.TextBox
    Private WithEvents dgv_permisos As System.Windows.Forms.DataGridView
End Class
