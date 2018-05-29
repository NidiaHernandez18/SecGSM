<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBiometria
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBiometria))
        Me.Huella3 = New System.Windows.Forms.PictureBox()
        Me.Huella2 = New System.Windows.Forms.PictureBox()
        Me.Huella1 = New System.Windows.Forms.PictureBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.img = New System.Windows.Forms.ImageList(Me.components)
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblUsuario = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        CType(Me.Huella3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Huella2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Huella1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Huella3
        '
        Me.Huella3.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Huella3.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Huella3.Location = New System.Drawing.Point(190, 261)
        Me.Huella3.Name = "Huella3"
        Me.Huella3.Size = New System.Drawing.Size(149, 181)
        Me.Huella3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.Huella3.TabIndex = 18
        Me.Huella3.TabStop = False
        '
        'Huella2
        '
        Me.Huella2.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Huella2.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Huella2.Location = New System.Drawing.Point(15, 261)
        Me.Huella2.Name = "Huella2"
        Me.Huella2.Size = New System.Drawing.Size(149, 181)
        Me.Huella2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.Huella2.TabIndex = 17
        Me.Huella2.TabStop = False
        '
        'Huella1
        '
        Me.Huella1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Huella1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Huella1.Location = New System.Drawing.Point(12, 54)
        Me.Huella1.Name = "Huella1"
        Me.Huella1.Size = New System.Drawing.Size(149, 181)
        Me.Huella1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.Huella1.TabIndex = 16
        Me.Huella1.TabStop = False
        '
        'Label30
        '
        Me.Label30.BackColor = System.Drawing.Color.White
        Me.Label30.Font = New System.Drawing.Font("Calibri", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.ForeColor = System.Drawing.Color.Black
        Me.Label30.Location = New System.Drawing.Point(12, 9)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(348, 42)
        Me.Label30.TabIndex = 15
        Me.Label30.Text = "I. Seleccione la huella que desea registrar y coloque el dedo en el lector hasta " & _
            "que se le indique que ha sido capturada."
        '
        'img
        '
        Me.img.ImageStream = CType(resources.GetObject("img.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.img.TransparentColor = System.Drawing.Color.Transparent
        Me.img.Images.SetKeyName(0, "FIR")
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(187, 54)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(57, 15)
        Me.Label1.TabIndex = 19
        Me.Label1.Text = "Usuario"
        '
        'lblUsuario
        '
        Me.lblUsuario.AutoSize = True
        Me.lblUsuario.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUsuario.Location = New System.Drawing.Point(241, 102)
        Me.lblUsuario.Name = "lblUsuario"
        Me.lblUsuario.Size = New System.Drawing.Size(0, 15)
        Me.lblUsuario.TabIndex = 20
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(187, 144)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(152, 54)
        Me.Label2.TabIndex = 21
        Me.Label2.Text = "1. Doble Clic para Registrar ó Eliminar huella"
        '
        'frmBiometria
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(364, 453)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lblUsuario)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Huella3)
        Me.Controls.Add(Me.Huella2)
        Me.Controls.Add(Me.Huella1)
        Me.Controls.Add(Me.Label30)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmBiometria"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Biometría"
        CType(Me.Huella3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Huella2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Huella1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Huella3 As System.Windows.Forms.PictureBox
    Friend WithEvents Huella2 As System.Windows.Forms.PictureBox
    Friend WithEvents Huella1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents img As System.Windows.Forms.ImageList
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblUsuario As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
End Class
