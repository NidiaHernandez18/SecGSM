<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTables
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
        Me.List = New System.Windows.Forms.ListBox()
        Me.cmdVer = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'List
        '
        Me.List.FormattingEnabled = True
        Me.List.Location = New System.Drawing.Point(12, 12)
        Me.List.Name = "List"
        Me.List.Size = New System.Drawing.Size(166, 147)
        Me.List.TabIndex = 0
        '
        'cmdVer
        '
        Me.cmdVer.Location = New System.Drawing.Point(97, 167)
        Me.cmdVer.Name = "cmdVer"
        Me.cmdVer.Size = New System.Drawing.Size(81, 21)
        Me.cmdVer.TabIndex = 1
        Me.cmdVer.Text = "Ver..."
        Me.cmdVer.UseVisualStyleBackColor = True
        '
        'frmTables
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(188, 200)
        Me.Controls.Add(Me.cmdVer)
        Me.Controls.Add(Me.List)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.KeyPreview = True
        Me.Name = "frmTables"
        Me.Text = "Datatables in Dataset"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents List As System.Windows.Forms.ListBox
    Friend WithEvents cmdVer As System.Windows.Forms.Button
End Class
