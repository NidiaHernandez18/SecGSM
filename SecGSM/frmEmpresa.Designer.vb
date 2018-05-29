<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEmpresa
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEmpresa))
        Me.txtdirec = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtsocial = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Txtpat = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtrfc = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label7 = New System.Windows.Forms.Label
        Me.btnsalir = New System.Windows.Forms.Button
        Me.BtnOk = New System.Windows.Forms.Button
        Me.Btneliminar = New System.Windows.Forms.Button
        Me.btnEditar = New System.Windows.Forms.Button
        Me.btnguardar = New System.Windows.Forms.Button
        Me.BtnCancel = New System.Windows.Forms.Button
        Me.lstdatos = New System.Windows.Forms.ListView
        Me.c1 = New System.Windows.Forms.ColumnHeader
        Me.c2 = New System.Windows.Forms.ColumnHeader
        Me.c3 = New System.Windows.Forms.ColumnHeader
        Me.c4 = New System.Windows.Forms.ColumnHeader
        Me.c5 = New System.Windows.Forms.ColumnHeader
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtdirec
        '
        Me.txtdirec.Location = New System.Drawing.Point(15, 129)
        Me.txtdirec.Name = "txtdirec"
        Me.txtdirec.Size = New System.Drawing.Size(577, 20)
        Me.txtdirec.TabIndex = 28
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(12, 107)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(67, 19)
        Me.Label6.TabIndex = 27
        Me.Label6.Text = "Dirección"
        '
        'txtsocial
        '
        Me.txtsocial.Location = New System.Drawing.Point(12, 84)
        Me.txtsocial.Name = "txtsocial"
        Me.txtsocial.Size = New System.Drawing.Size(256, 20)
        Me.txtsocial.TabIndex = 24
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(12, 62)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(85, 19)
        Me.Label4.TabIndex = 23
        Me.Label4.Text = "Razón social"
        '
        'Txtpat
        '
        Me.Txtpat.Location = New System.Drawing.Point(449, 84)
        Me.Txtpat.Name = "Txtpat"
        Me.Txtpat.Size = New System.Drawing.Size(143, 20)
        Me.Txtpat.TabIndex = 22
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(446, 62)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(91, 19)
        Me.Label3.TabIndex = 21
        Me.Label3.Text = "Reg. Patronal"
        '
        'txtrfc
        '
        Me.txtrfc.Location = New System.Drawing.Point(283, 84)
        Me.txtrfc.Name = "txtrfc"
        Me.txtrfc.Size = New System.Drawing.Size(143, 20)
        Me.txtrfc.TabIndex = 20
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(292, 62)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(39, 19)
        Me.Label2.TabIndex = 19
        Me.Label2.Text = "RFC"
        '
        'Panel1
        '
        Me.Panel1.BackgroundImage = CType(resources.GetObject("Panel1.BackgroundImage"), System.Drawing.Image)
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(607, 59)
        Me.Panel1.TabIndex = 31
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Times New Roman", 26.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(193, 7)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(195, 40)
        Me.Label7.TabIndex = 17
        Me.Label7.Text = "EMPRESAS"
        '
        'btnsalir
        '
        Me.btnsalir.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.btnsalir.Location = New System.Drawing.Point(388, 310)
        Me.btnsalir.Name = "btnsalir"
        Me.btnsalir.Size = New System.Drawing.Size(78, 27)
        Me.btnsalir.TabIndex = 35
        Me.btnsalir.Text = "Salir"
        Me.btnsalir.UseVisualStyleBackColor = True
        '
        'BtnOk
        '
        Me.BtnOk.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.BtnOk.Location = New System.Drawing.Point(139, 310)
        Me.BtnOk.Name = "BtnOk"
        Me.BtnOk.Size = New System.Drawing.Size(82, 26)
        Me.BtnOk.TabIndex = 32
        Me.BtnOk.Text = "Nuevo"
        Me.BtnOk.UseVisualStyleBackColor = True
        '
        'Btneliminar
        '
        Me.Btneliminar.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.Btneliminar.Location = New System.Drawing.Point(304, 310)
        Me.Btneliminar.Name = "Btneliminar"
        Me.Btneliminar.Size = New System.Drawing.Size(78, 27)
        Me.Btneliminar.TabIndex = 34
        Me.Btneliminar.Text = "Borrar"
        Me.Btneliminar.UseVisualStyleBackColor = True
        '
        'btnEditar
        '
        Me.btnEditar.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.btnEditar.Location = New System.Drawing.Point(227, 309)
        Me.btnEditar.Name = "btnEditar"
        Me.btnEditar.Size = New System.Drawing.Size(78, 27)
        Me.btnEditar.TabIndex = 33
        Me.btnEditar.Text = "Editar"
        Me.btnEditar.UseVisualStyleBackColor = True
        '
        'btnguardar
        '
        Me.btnguardar.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.btnguardar.Location = New System.Drawing.Point(227, 310)
        Me.btnguardar.Name = "btnguardar"
        Me.btnguardar.Size = New System.Drawing.Size(78, 27)
        Me.btnguardar.TabIndex = 36
        Me.btnguardar.Text = "Guardar"
        Me.btnguardar.UseVisualStyleBackColor = True
        '
        'BtnCancel
        '
        Me.BtnCancel.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.BtnCancel.Location = New System.Drawing.Point(304, 310)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(78, 27)
        Me.BtnCancel.TabIndex = 37
        Me.BtnCancel.Text = "Cancel"
        Me.BtnCancel.UseVisualStyleBackColor = True
        '
        'lstdatos
        '
        Me.lstdatos.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.c1, Me.c2, Me.c3, Me.c4, Me.c5})
        Me.lstdatos.FullRowSelect = True
        Me.lstdatos.Location = New System.Drawing.Point(16, 155)
        Me.lstdatos.Name = "lstdatos"
        Me.lstdatos.Size = New System.Drawing.Size(576, 149)
        Me.lstdatos.TabIndex = 38
        Me.lstdatos.UseCompatibleStateImageBehavior = False
        Me.lstdatos.View = System.Windows.Forms.View.Details
        '
        'c1
        '
        Me.c1.Text = "Razón social"
        Me.c1.Width = 125
        '
        'c2
        '
        Me.c2.Text = "RFC"
        Me.c2.Width = 120
        '
        'c3
        '
        Me.c3.Text = "Reg. Patronal"
        Me.c3.Width = 126
        '
        'c4
        '
        Me.c4.Text = "Dirección"
        Me.c4.Width = 204
        '
        'c5
        '
        Me.c5.Text = "Numero"
        '
        'frmEmpresa
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(604, 362)
        Me.ControlBox = False
        Me.Controls.Add(Me.lstdatos)
        Me.Controls.Add(Me.btnsalir)
        Me.Controls.Add(Me.BtnOk)
        Me.Controls.Add(Me.Btneliminar)
        Me.Controls.Add(Me.btnEditar)
        Me.Controls.Add(Me.btnguardar)
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.txtdirec)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtsocial)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Txtpat)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtrfc)
        Me.Controls.Add(Me.Label2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Name = "frmEmpresa"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Empresas"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtdirec As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtsocial As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Txtpat As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtrfc As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents btnsalir As System.Windows.Forms.Button
    Friend WithEvents BtnOk As System.Windows.Forms.Button
    Friend WithEvents Btneliminar As System.Windows.Forms.Button
    Friend WithEvents btnEditar As System.Windows.Forms.Button
    Friend WithEvents btnguardar As System.Windows.Forms.Button
    Friend WithEvents BtnCancel As System.Windows.Forms.Button
    Friend WithEvents lstdatos As System.Windows.Forms.ListView
    Friend WithEvents c1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents c2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents c3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents c4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents c5 As System.Windows.Forms.ColumnHeader
End Class
