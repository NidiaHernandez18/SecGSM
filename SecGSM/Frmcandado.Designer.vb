<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Frmcandado
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frmcandado))
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtllave1 = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtCandado1 = New System.Windows.Forms.TextBox
        Me.BtnSalir = New System.Windows.Forms.Button
        Me.Btnllave = New System.Windows.Forms.Button
        Me.Candado = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(7, 129)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(572, 53)
        Me.Label3.TabIndex = 18
        Me.Label3.Text = "Mandar por e-mail el candado del sistema a Grupo Sistecom de  México S.C. (soport" & _
            "e@gruposistecom.com) para que le proporcione la llave de activación o por teléfo" & _
            "no al (01-899-1800127)"
        '
        'txtllave1
        '
        Me.txtllave1.Location = New System.Drawing.Point(4, 106)
        Me.txtllave1.Name = "txtllave1"
        Me.txtllave1.Size = New System.Drawing.Size(740, 20)
        Me.txtllave1.TabIndex = 24
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(6, 84)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(125, 19)
        Me.Label1.TabIndex = 23
        Me.Label1.Text = "Llave de activación"
        '
        'txtCandado1
        '
        Me.txtCandado1.Location = New System.Drawing.Point(4, 61)
        Me.txtCandado1.Name = "txtCandado1"
        Me.txtCandado1.Size = New System.Drawing.Size(740, 20)
        Me.txtCandado1.TabIndex = 22
        '
        'BtnSalir
        '
        Me.BtnSalir.Image = CType(resources.GetObject("BtnSalir.Image"), System.Drawing.Image)
        Me.BtnSalir.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.BtnSalir.Location = New System.Drawing.Point(669, 132)
        Me.BtnSalir.Name = "BtnSalir"
        Me.BtnSalir.Size = New System.Drawing.Size(75, 42)
        Me.BtnSalir.TabIndex = 26
        Me.BtnSalir.Text = "         Salir"
        Me.BtnSalir.UseVisualStyleBackColor = True
        '
        'Btnllave
        '
        Me.Btnllave.Image = CType(resources.GetObject("Btnllave.Image"), System.Drawing.Image)
        Me.Btnllave.ImageAlign = System.Drawing.ContentAlignment.TopLeft
        Me.Btnllave.Location = New System.Drawing.Point(587, 132)
        Me.Btnllave.Name = "Btnllave"
        Me.Btnllave.Size = New System.Drawing.Size(76, 42)
        Me.Btnllave.TabIndex = 25
        Me.Btnllave.Text = "          Llave"
        Me.Btnllave.UseVisualStyleBackColor = True
        '
        'Candado
        '
        Me.Candado.AutoSize = True
        Me.Candado.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Candado.Location = New System.Drawing.Point(6, 39)
        Me.Candado.Name = "Candado"
        Me.Candado.Size = New System.Drawing.Size(135, 19)
        Me.Candado.TabIndex = 180
        Me.Candado.Text = "Candado del sistema"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Candado)
        Me.GroupBox1.Controls.Add(Me.BtnSalir)
        Me.GroupBox1.Controls.Add(Me.Btnllave)
        Me.GroupBox1.Controls.Add(Me.txtllave1)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtCandado1)
        Me.GroupBox1.Location = New System.Drawing.Point(1, -2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(751, 185)
        Me.GroupBox1.TabIndex = 182
        Me.GroupBox1.TabStop = False
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.SteelBlue
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.White
        Me.Label4.Location = New System.Drawing.Point(-2, 2)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(761, 30)
        Me.Label4.TabIndex = 182
        Me.Label4.Text = "ACTIVACION DEL SISTEMA"
        '
        'Frmcandado
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(753, 183)
        Me.ControlBox = False
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "Frmcandado"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Activaciones"
        Me.TopMost = True
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtllave1 As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtCandado1 As System.Windows.Forms.TextBox
    Friend WithEvents BtnSalir As System.Windows.Forms.Button
    Friend WithEvents Btnllave As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Candado As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
End Class
