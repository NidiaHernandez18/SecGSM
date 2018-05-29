<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSolCLU
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
        Dim ListViewGroup1 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Aceptadas", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup2 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("En espera", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup3 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Rechazadas", System.Windows.Forms.HorizontalAlignment.Left)
        Me.lvwSolicitudes = New System.Windows.Forms.ListView()
        Me.ColEmpresa = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColSucursal = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColFecha = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.cmdSalir = New System.Windows.Forms.Button()
        Me.cmdVerificar = New System.Windows.Forms.Button()
        Me.cmdNuevo = New System.Windows.Forms.Button()
        Me.cmdEliminar = New System.Windows.Forms.Button()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtIP = New System.Windows.Forms.TextBox()
        Me.txtEquipo = New System.Windows.Forms.TextBox()
        Me.colRFC = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.SuspendLayout()
        '
        'lvwSolicitudes
        '
        Me.lvwSolicitudes.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColEmpresa, Me.colRFC, Me.ColSucursal, Me.ColFecha})
        Me.lvwSolicitudes.FullRowSelect = True
        ListViewGroup1.Header = "Aceptadas"
        ListViewGroup1.Name = "Aceptadas"
        ListViewGroup2.Header = "En espera"
        ListViewGroup2.Name = "EnEspera"
        ListViewGroup3.Header = "Rechazadas"
        ListViewGroup3.Name = "Rechazadas"
        Me.lvwSolicitudes.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup1, ListViewGroup2, ListViewGroup3})
        Me.lvwSolicitudes.Location = New System.Drawing.Point(12, 41)
        Me.lvwSolicitudes.Name = "lvwSolicitudes"
        Me.lvwSolicitudes.Size = New System.Drawing.Size(461, 270)
        Me.lvwSolicitudes.TabIndex = 0
        Me.lvwSolicitudes.UseCompatibleStateImageBehavior = False
        Me.lvwSolicitudes.View = System.Windows.Forms.View.Details
        '
        'ColEmpresa
        '
        Me.ColEmpresa.Text = "Empresa"
        Me.ColEmpresa.Width = 260
        '
        'ColSucursal
        '
        Me.ColSucursal.Text = "Sucursal"
        '
        'ColFecha
        '
        Me.ColFecha.Text = "Fecha Req"
        Me.ColFecha.Width = 80
        '
        'cmdSalir
        '
        Me.cmdSalir.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdSalir.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdSalir.Location = New System.Drawing.Point(404, 12)
        Me.cmdSalir.Name = "cmdSalir"
        Me.cmdSalir.Size = New System.Drawing.Size(69, 23)
        Me.cmdSalir.TabIndex = 5
        Me.cmdSalir.Text = "Salir"
        '
        'cmdVerificar
        '
        Me.cmdVerificar.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdVerificar.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdVerificar.Location = New System.Drawing.Point(329, 12)
        Me.cmdVerificar.Name = "cmdVerificar"
        Me.cmdVerificar.Size = New System.Drawing.Size(69, 23)
        Me.cmdVerificar.TabIndex = 4
        Me.cmdVerificar.Text = "Verificar"
        '
        'cmdNuevo
        '
        Me.cmdNuevo.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdNuevo.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdNuevo.Location = New System.Drawing.Point(12, 12)
        Me.cmdNuevo.Name = "cmdNuevo"
        Me.cmdNuevo.Size = New System.Drawing.Size(69, 23)
        Me.cmdNuevo.TabIndex = 6
        Me.cmdNuevo.Text = "Nuevo"
        '
        'cmdEliminar
        '
        Me.cmdEliminar.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdEliminar.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdEliminar.Location = New System.Drawing.Point(87, 12)
        Me.cmdEliminar.Name = "cmdEliminar"
        Me.cmdEliminar.Size = New System.Drawing.Size(69, 23)
        Me.cmdEliminar.TabIndex = 7
        Me.cmdEliminar.Text = "Eliminar"
        Me.cmdEliminar.Visible = False
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(245, 320)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(24, 14)
        Me.Label11.TabIndex = 142
        Me.Label11.Text = "I.P.:"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(11, 320)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(48, 14)
        Me.Label12.TabIndex = 141
        Me.Label12.Text = "Equipo:"
        '
        'txtIP
        '
        Me.txtIP.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtIP.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIP.Location = New System.Drawing.Point(287, 317)
        Me.txtIP.Name = "txtIP"
        Me.txtIP.ReadOnly = True
        Me.txtIP.Size = New System.Drawing.Size(186, 22)
        Me.txtIP.TabIndex = 140
        '
        'txtEquipo
        '
        Me.txtEquipo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtEquipo.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEquipo.Location = New System.Drawing.Point(65, 317)
        Me.txtEquipo.Name = "txtEquipo"
        Me.txtEquipo.ReadOnly = True
        Me.txtEquipo.Size = New System.Drawing.Size(186, 22)
        Me.txtEquipo.TabIndex = 139
        '
        'colRFC
        '
        Me.colRFC.Text = "RFC"
        Me.colRFC.Width = 100
        '
        'frmSolCLU
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(485, 343)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.txtIP)
        Me.Controls.Add(Me.txtEquipo)
        Me.Controls.Add(Me.cmdEliminar)
        Me.Controls.Add(Me.cmdNuevo)
        Me.Controls.Add(Me.cmdSalir)
        Me.Controls.Add(Me.cmdVerificar)
        Me.Controls.Add(Me.lvwSolicitudes)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmSolCLU"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Solicitudes de Acceso a ePayroll"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lvwSolicitudes As System.Windows.Forms.ListView
    Friend WithEvents ColEmpresa As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColSucursal As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColFecha As System.Windows.Forms.ColumnHeader
    Friend WithEvents cmdSalir As System.Windows.Forms.Button
    Friend WithEvents cmdVerificar As System.Windows.Forms.Button
    Friend WithEvents cmdNuevo As System.Windows.Forms.Button
    Friend WithEvents cmdEliminar As System.Windows.Forms.Button
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtIP As System.Windows.Forms.TextBox
    Friend WithEvents txtEquipo As System.Windows.Forms.TextBox
    Friend WithEvents colRFC As System.Windows.Forms.ColumnHeader
End Class
