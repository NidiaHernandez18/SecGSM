Imports System.Windows.Forms

Public Class ClassCone
    Enum Basededatos
        Sqlserver = 1
        Access = 2
        Mysql = 3
    End Enum

    Public Function conectarsql(ByVal Server As String, ByVal User As String, ByVal Password As String, ByVal Database As String, Optional ByVal datos As Basededatos = Basededatos.Sqlserver) As OleDb.OleDbConnection
        Dim conn As OleDb.OleDbConnection
        Dim connStr As String = ""
        conn = New OleDb.OleDbConnection
        conectarsql = conn
        'connStr = String.Format("Provider=SQLOLEDB.1;Password = " & Password & ";" & "Persist Security Info=False;" & "User ID=" & User & ";Initial Catalog=" & Database & ";" & "Data Source=" & Server & "")
        If datos = Basededatos.Sqlserver Then
            connStr = String.Format("Provider=SQLOLEDB.1;Password = " & Password & ";" & "Persist Security Info=False;" & "User ID=" & User & ";Initial Catalog=" & Database & ";" & "Data Source=" & Server & "")
            'connStr = String.Format("Provider=SQLOLEDB.1;Password = " & Password & ";" & "Persist Security Info=False;" & "User ID=" & User & ";Initial Catalog=" & Database & ";" & "Data Source=" & Server & ";COLLATE=Modern_Spanish_CI_AS")
            'connStr = String.Format("Provider=SQLOLEDB;User ID=" & User & ";Password=" & Password & ";Data Source=" & Server & ";Initial Catalog=" & Database & ";")
            'connStr = String.Format("Provider=SQLNCLI;Server=" & Server & ";Database=" & Database & ";Uid=" & User & ";Pwd = " & Password & ";")
            'collation name','Modern_Spanish_CI_AS'"
        ElseIf datos = Basededatos.Access Then
            connStr = String.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & Server & ";Jet OLEDB:Database Password=" & Password & ";")
        End If

        Try
            conn = New OleDb.OleDbConnection(connStr)
            If Not conn Is Nothing Then conn.Close()
            conn.Open()
            conectarsql = conn
        Catch ex As SqlClient.SqlException
            conectarsql = Nothing

        End Try
        ' ojo muy importante
        'connStr = String.Format("server={0};user id={1}; password={2}; database=mysql; pooling=false", Server.Text, userid.Text, Password.Text)
    End Function
    Public Function connectSQL(ByVal Server As String, ByVal User As String, ByVal Password As String, ByVal Database As String) As SqlClient.SqlConnection
        Dim conn As SqlClient.SqlConnection
        Dim connStr As String = ""
        conn = New SqlClient.SqlConnection
        connectSQL = conn
        'connStr = String.Format("Provider=SQLOLEDB.1;Password = " & Password & ";" & "Persist Security Info=False;" & "User ID=" & User & ";Initial Catalog=" & Database & ";" & "Data Source=" & Server & "")
        connStr = String.Format("Provider=SQLOLEDB.1;Password = " & Password & ";" & "Persist Security Info=False;" & "User ID=" & User & ";Initial Catalog=" & Database & ";" & "Data Source=" & Server & "")
        'connStr = String.Format("Provider=SQLOLEDB.1;Password = " & Password & ";" & "Persist Security Info=False;" & "User ID=" & User & ";Initial Catalog=" & Database & ";" & "Data Source=" & Server & ";COLLATE=Modern_Spanish_CI_AS")
        'connStr = String.Format("Provider=SQLOLEDB;User ID=" & User & ";Password=" & Password & ";Data Source=" & Server & ";Initial Catalog=" & Database & ";")
        'connStr = String.Format("Provider=SQLNCLI;Server=" & Server & ";Database=" & Database & ";Uid=" & User & ";Pwd = " & Password & ";")
        'collation name','Modern_Spanish_CI_AS'"


        Try
            conn = New SqlClient.SqlConnection(connStr)
            If Not conn Is Nothing Then conn.Close()
            conn.Open()
            connectSQL = conn
        Catch ex As SqlClient.SqlException
            connectSQL = Nothing

        End Try
        ' ojo muy importante
        'connStr = String.Format("server={0};user id={1}; password={2}; database=mysql; pooling=false", Server.Text, userid.Text, Password.Text)
    End Function
#Region "OLEDB"
    Public Function ejecutarcommand(ByVal commando As String, ByVal conexion As OleDb.OleDbConnection) As Boolean
        Dim reader As OleDb.OleDbDataReader
        Dim tablas As New OleDb.OleDbCommand(commando, conexion)
        reader = tablas.ExecuteReader()
        Try
            ejecutarcommand = True
        Catch ex As OleDb.OleDbException
            ejecutarcommand = False
        Finally
            tablas.Dispose()
            reader.Close()
            reader = Nothing
        End Try
    End Function
    Public Function RecDatatable(ByVal sql As String, ByVal conn As OleDb.OleDbConnection, Optional ByRef datAdapter As OleDb.OleDbDataAdapter = Nothing, Optional ByVal transaction As OleDb.OleDbTransaction = Nothing, Optional ByVal Schema As Boolean = False) As DataTable
        Dim data As DataTable = New DataTable
        Dim DAtse As DataSet = New DataSet()
        Dim da As OleDb.OleDbDataAdapter
        Dim cb As OleDb.OleDbCommandBuilder
        data = New DataTable
        RecDatatable = Nothing
        Try
            da = New OleDb.OleDbDataAdapter(sql, conn)
            If Not IsNothing(transaction) Then
                da.SelectCommand.Transaction = transaction
            End If
            cb = New OleDb.OleDbCommandBuilder(da)
            If Schema Then da.FillSchema(data, SchemaType.Mapped)
            da.Fill(data)
            RecDatatable = data
            If Not IsNothing(datAdapter) Then

                datAdapter = da
                datAdapter.UpdateCommand = cb.GetUpdateCommand
                datAdapter.InsertCommand = cb.GetInsertCommand
                datAdapter.DeleteCommand = cb.GetDeleteCommand
            End If
        Catch ex As OleDb.OleDbException
            MessageBox.Show(ex.Message, "Error")
        Finally
            data = Nothing
            DAtse = Nothing
        End Try
    End Function
    Public Function RecDataSET(ByVal sql As String, ByVal conn As OleDb.OleDbConnection, Optional ByRef datAdapter As OleDb.OleDbDataAdapter = Nothing, Optional ByVal namedataset As String = "") As DataSet
        Dim data As DataSet
        Dim DAtse As DataSet = New DataSet()
        Dim da As OleDb.OleDbDataAdapter
        Dim cb As OleDb.OleDbCommandBuilder
        data = New DataSet
        RecDataSET = Nothing
        Try
            da = New OleDb.OleDbDataAdapter(sql, conn)
            cb = New OleDb.OleDbCommandBuilder(da)
            If namedataset.Length = 0 Then
                da.Fill(data)
            Else
                da.Fill(data, namedataset)
            End If

            RecDataSET = data
            If Not IsNothing(datAdapter) Then
                datAdapter = da
                datAdapter.UpdateCommand = cb.GetUpdateCommand
                datAdapter.InsertCommand = cb.GetInsertCommand
                datAdapter.DeleteCommand = cb.GetDeleteCommand

            End If
        Catch ex As OleDb.OleDbException

        Finally
            data = Nothing
            DAtse = Nothing
        End Try
    End Function
    Public Function RecDataReader(ByVal sql As String, ByVal conn As OleDb.OleDbConnection, Optional ByRef dtb As DataTable = Nothing, Optional ByVal transaction As OleDb.OleDbTransaction = Nothing) As OleDb.OleDbDataReader
        Dim cb As OleDb.OleDbCommand
        Dim data As OleDb.OleDbDataReader
        Try
            If Not IsNothing(transaction) Then
                cb = New OleDb.OleDbCommand(sql, conn, transaction)
            Else
                cb = New OleDb.OleDbCommand(sql, conn)
            End If
            data = cb.ExecuteReader
            Return data
        Catch ex As OleDb.OleDbException
            MessageBox.Show(ex.Message, "Error")
            Return Nothing
        Finally
            data = Nothing
        End Try
    End Function
    Public Sub ArrayDataSET(ByRef recdata As DataSet, ByVal sql As String, ByVal conn As OleDb.OleDbConnection, Optional ByRef datAdapter As OleDb.OleDbDataAdapter = Nothing, Optional ByVal namedataset As String = "")
        Dim DAtse As DataSet = New DataSet()
        Dim da As OleDb.OleDbDataAdapter
        Dim cb As OleDb.OleDbCommandBuilder

        Try
            If IsNothing(recdata) Then recdata = New DataSet

            da = New OleDb.OleDbDataAdapter(sql, conn)
            cb = New OleDb.OleDbCommandBuilder(da)
            If namedataset.Length = 0 Then
                da.Fill(recdata)
            Else
                da.Fill(recdata, namedataset)
            End If


            If Not IsNothing(datAdapter) Then
                datAdapter = da
                datAdapter.UpdateCommand = cb.GetUpdateCommand
                datAdapter.InsertCommand = cb.GetInsertCommand
                datAdapter.DeleteCommand = cb.GetDeleteCommand

            End If
        Catch ex As OleDb.OleDbException

        Finally
            DAtse = Nothing
        End Try
    End Sub
    Public Function ExecuteSql(ByVal Sql As String, ByVal conn As OleDb.OleDbConnection, Optional ByVal transaction As OleDb.OleDbTransaction = Nothing) As Boolean
        Try
            ExecuteSql = False
            Dim cmd As New OleDb.OleDbCommand(Sql, conn)
            If Not IsNothing(transaction) Then cmd.Transaction = transaction
            cmd.ExecuteNonQuery()
            ExecuteSql = True
        Catch ex As OleDb.OleDbException
            MessageBox.Show("the query is not correct  " + ex.Message)
        Finally

        End Try
    End Function
    Public Function ExecuteScalar(ByVal Sql As String, ByVal conn As OleDb.OleDbConnection, Optional ByVal transaction As OleDb.OleDbTransaction = Nothing) As Object
        Try
            Dim cmd As New OleDb.OleDbCommand(Sql, conn)
            If Not IsNothing(transaction) Then cmd.Transaction = transaction
            ExecuteScalar = cmd.ExecuteScalar()
        Catch ex As OleDb.OleDbException
            MessageBox.Show("the query is not correct  " + ex.Message)
            ExecuteScalar = Nothing
        Finally

        End Try
    End Function
    Public Function EjecutarProcedimiento(ByVal sp As String, ByVal conn As OleDb.OleDbConnection, Optional ByVal parametros As String = "", Optional ByVal valores As String = "") As DataTable
        Try
            Dim Param1() As String
            Dim valor1() As String
            Dim ii As Integer
            Dim j As Integer
            Dim sqlCmd As New OleDb.OleDbCommand(sp, conn)
            Dim Xtabla As DataTable
            Dim pParam As OleDb.OleDbParameter
            ' sqlCmd.CommandTimeout = 3
            sqlCmd.CommandType = CommandType.StoredProcedure
            Param1 = Split(parametros, "|")
            valor1 = Split(valores, "|")
            j = UBound(Param1)
            For ii = 0 To j
                valor1(ii) = Replace(valor1(ii), "'", "")
                pParam = New OleDb.OleDbParameter("@" & Param1(ii), OleDb.OleDbType.VarChar)
                pParam.Direction = ParameterDirection.Input
                pParam.Value = valor1(ii)
                sqlCmd.Parameters.Add(pParam)
            Next ii
            Xtabla = New DataTable

            Dim ds As New DataSet
            Dim sda As New OleDb.OleDbDataAdapter(sqlCmd)

            sda.Fill(ds)
            Xtabla = ds.Tables(0)
            Return Xtabla
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function SPSinReturn(ByVal sp As String, ByVal conn As OleDb.OleDbConnection, Optional ByVal parametros As String = "", Optional ByVal valores As String = "") As Boolean
        Try
            Dim Param1() As String
            Dim valor1() As String
            Dim ii As Integer
            Dim j As Integer
            Dim sqlCmd As New OleDb.OleDbCommand(sp, conn)
            Dim pParam As OleDb.OleDbParameter
            sqlCmd.CommandType = CommandType.StoredProcedure
            Param1 = Split(parametros, "|")
            valor1 = Split(valores, "|")
            j = UBound(Param1)
            SPSinReturn = False
            For ii = 0 To j
                pParam = New OleDb.OleDbParameter("@" & Param1(ii), OleDb.OleDbType.VarChar)
                pParam.Direction = ParameterDirection.Input
                pParam.Value = valor1(ii)
                sqlCmd.Parameters.Add(pParam)
            Next ii
            sqlCmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'Public Sub GUARDARIMAGEN(ByRef PICTURE As PictureBox, ByVal TABLE As String, ByVal FIELD As String, ByVal CONDICION As String, ByVal Conex As MySqlConnection)
    '    Try
    '        Dim ms As New IO.MemoryStream
    '        Dim arrImage() As Byte = {0}
    '        If Not PICTURE.Image Is Nothing Then
    '            PICTURE.Image.Save(ms, PICTURE.Image.RawFormat)
    '            arrImage = ms.GetBuffer
    '            ms.Close()
    '        End If

    '        Dim strSQL As String = _
    '            "UPDATE " & TABLE & " SET " & FIELD & "=" & " @Picture " & CONDICION

    '        Dim cmd As New MySqlCommand(strSQL)        'OleDb.OleDbCommand(strSQL)
    '        cmd.Connection = Conex
    '        With cmd
    '            .Parameters.Add(New MySqlParameter("@Picture", MySql.Data.MySqlClient.MySqlDbType.LongBlob)).Value = arrImage

    '        End With
    '        cmd.ExecuteNonQuery()


    '    Catch exc As Exception
    '        MessageBox.Show(exc.Message, _
    '                "Connection Failed!", MessageBoxButtons.OK, _
    '                MessageBoxIcon.Error)

    '    End Try
    'End Sub
#End Region
#Region "SQL"
    Public Function msConectarSQL(ByVal Server As String, ByVal User As String, ByVal Password As String, ByVal Database As String) As SqlClient.SqlConnection
        Dim conn As SqlClient.SqlConnection
        Dim connStr As String = ""
        conn = New SqlClient.SqlConnection
        msConectarSQL = conn
        'connStr = String.Format("Provider=SQLOLEDB.1;Password = " & Password & ";" & "Persist Security Info=False;" & "User ID=" & User & ";Initial Catalog=" & Database & ";" & "Data Source=" & Server & "")
        'connStr = String.Format("Provider=SQLOLEDB.1;Password = " & Password & ";" & "Persist Security Info=False;" & "User ID=" & User & ";Initial Catalog=" & Database & ";" & "Data Source=" & Server & "")
        connStr = String.Format("Data Source=" & Server & ";Initial Catalog=" & Database & ";User Id=" & User & ";Password=" & Password & ";")
        'connStr = String.Format("Provider=SQLOLEDB.1;Password = " & Password & ";" & "Persist Security Info=False;" & "User ID=" & User & ";Initial Catalog=" & Database & ";" & "Data Source=" & Server & ";COLLATE=Modern_Spanish_CI_AS")
        'connStr = String.Format("Provider=SQLOLEDB;User ID=" & User & ";Password=" & Password & ";Data Source=" & Server & ";Initial Catalog=" & Database & ";")
        'connStr = String.Format("Provider=SQLNCLI;Server=" & Server & ";Database=" & Database & ";Uid=" & User & ";Pwd = " & Password & ";")
        'collation name','Modern_Spanish_CI_AS'"
        Try
            conn = New SqlClient.SqlConnection(connStr)
            If Not conn Is Nothing Then conn.Close()
            conn.Open()
            msConectarSQL = conn
        Catch ex As SqlClient.SqlException
            msConectarSQL = Nothing

        End Try
        ' ojo muy importante
        'connStr = String.Format("server={0};user id={1}; password={2}; database=mysql; pooling=false", Server.Text, userid.Text, Password.Text)
    End Function
    Public Function EjecutarCommand(ByVal commando As String, ByVal conexion As SqlClient.SqlConnection) As Boolean
        Dim reader As SqlClient.SqlDataReader
        Dim tablas As New SqlClient.SqlCommand(commando, conexion)
        reader = tablas.ExecuteReader()
        Try
            EjecutarCommand = True
        Catch ex As SqlClient.SqlException
            EjecutarCommand = False
        Finally
            tablas.Dispose()
            reader.Close()
            reader = Nothing
        End Try
    End Function
    Public Function RecDatatable(ByVal sql As String, ByVal conn As SqlClient.SqlConnection, Optional ByRef datAdapter As SqlClient.SqlDataAdapter = Nothing, Optional ByVal transaction As SqlClient.SqlTransaction = Nothing, Optional ByVal Schema As Boolean = False) As DataTable
        Dim data As DataTable = New DataTable
        Dim da As SqlClient.SqlDataAdapter
        Dim cb As SqlClient.SqlCommandBuilder
        'Dim cmd As New SqlClient.SqlCommand(sql, conn)
        data = New DataTable
        RecDatatable = Nothing
        Try
            da = New SqlClient.SqlDataAdapter(sql, conn)
            da.SelectCommand.Transaction = transaction
            If Not IsNothing(transaction) Then
                da.SelectCommand.Transaction = transaction

            End If

            cb = New SqlClient.SqlCommandBuilder(da)
            If Schema Then da.FillSchema(data, SchemaType.Mapped)
            da.Fill(data)


            RecDatatable = data
            If Not IsNothing(datAdapter) Then
                datAdapter = da
                datAdapter.UpdateCommand = cb.GetUpdateCommand
                datAdapter.InsertCommand = cb.GetInsertCommand
                datAdapter.DeleteCommand = cb.GetDeleteCommand
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error")
        Finally
            data = Nothing
        End Try
    End Function
    Public Function RecDataSET(ByVal sql As String, ByVal conn As SqlClient.SqlConnection, Optional ByRef datAdapter As SqlClient.SqlDataAdapter = Nothing, Optional ByVal namedataset As String = "") As DataSet
        Dim data As DataSet
        Dim DAtse As DataSet = New DataSet()
        Dim da As SqlClient.SqlDataAdapter
        Dim cb As SqlClient.SqlCommandBuilder
        data = New DataSet
        RecDataSET = Nothing
        Try
            da = New SqlClient.SqlDataAdapter(sql, conn)
            cb = New SqlClient.SqlCommandBuilder(da)
            If namedataset.Length = 0 Then
                da.Fill(data)
            Else
                da.Fill(data, namedataset)
            End If

            RecDataSET = data
            If Not IsNothing(datAdapter) Then
                datAdapter = da
                datAdapter.UpdateCommand = cb.GetUpdateCommand
                datAdapter.InsertCommand = cb.GetInsertCommand
                datAdapter.DeleteCommand = cb.GetDeleteCommand

            End If
        Catch ex As SqlClient.SqlException

        Finally
            data = Nothing
            DAtse = Nothing
        End Try
    End Function
    Public Function RecDataReader(ByVal sql As String, ByVal conn As SqlClient.SqlConnection, Optional ByRef dtb As DataTable = Nothing, Optional ByVal transaction As SqlClient.SqlTransaction = Nothing) As SqlClient.SqlDataReader
        Dim cb As SqlClient.SqlCommand
        Dim data As SqlClient.SqlDataReader = Nothing
        Try
            If Not IsNothing(transaction) Then
                cb = New SqlClient.SqlCommand(sql, conn, transaction)
            Else
                cb = New SqlClient.SqlCommand(sql, conn)
            End If
            data = cb.ExecuteReader
            Return data
        Catch ex As SqlClient.SqlException
            MessageBox.Show(ex.Message, "Error")
            Return Nothing
        Finally
            'If Not IsNothing(data) Then data.Close()
            data = Nothing
        End Try
    End Function
    Public Sub ArrayDataSET(ByRef recdata As DataSet, ByVal sql As String, ByVal conn As SqlClient.SqlConnection, Optional ByRef datAdapter As SqlClient.SqlDataAdapter = Nothing, Optional ByVal namedataset As String = "")
        Dim DAtse As DataSet = New DataSet()
        Dim da As SqlClient.SqlDataAdapter
        Dim cb As SqlClient.SqlCommandBuilder

        Try
            If IsNothing(recdata) Then recdata = New DataSet

            da = New SqlClient.SqlDataAdapter(sql, conn)
            cb = New SqlClient.SqlCommandBuilder(da)
            If namedataset.Length = 0 Then
                da.Fill(recdata)
            Else
                da.Fill(recdata, namedataset)
            End If


            If Not IsNothing(datAdapter) Then
                datAdapter = da
                datAdapter.UpdateCommand = cb.GetUpdateCommand
                datAdapter.InsertCommand = cb.GetInsertCommand
                datAdapter.DeleteCommand = cb.GetDeleteCommand

            End If
        Catch ex As SqlClient.SqlException

        Finally
            DAtse = Nothing
        End Try
    End Sub
    Public Function ExecuteSql(ByVal Sql As String, ByVal conn As SqlClient.SqlConnection, Optional ByVal transaction As SqlClient.SqlTransaction = Nothing) As Boolean
        Try
            ExecuteSql = False
            Dim cmd As New SqlClient.SqlCommand(Sql, conn)
            If Not IsNothing(transaction) Then cmd.Transaction = transaction
            cmd.CommandTimeout = 150
            cmd.ExecuteNonQuery()
            ExecuteSql = True
        Catch ex As SqlClient.SqlException
            '  MessageBox.Show("the query is not correct  " + ex.Message)
        Finally

        End Try
    End Function
    Public Function ExecuteScalar(ByVal Sql As String, ByVal conn As SqlClient.SqlConnection, Optional ByVal transaction As SqlClient.SqlTransaction = Nothing) As Object
        Try
            Dim cmd As New SqlClient.SqlCommand(Sql, conn)
            If Not IsNothing(transaction) Then cmd.Transaction = transaction
            ExecuteScalar = cmd.ExecuteScalar()
        Catch ex As SqlClient.SqlException
            MessageBox.Show("the query is not correct  " + ex.Message)
            ExecuteScalar = Nothing
        Finally

        End Try
    End Function
    Public Function EjecutarProcedimiento(ByVal sp As String, ByVal conn As SqlClient.SqlConnection, Optional ByVal parametros As String = "", Optional ByVal valores As String = "") As DataTable
        Try
            Dim Param1() As String
            Dim valor1() As String
            Dim ii As Integer
            Dim j As Integer
            Dim sqlCmd As New SqlClient.SqlCommand(sp, conn)
            Dim Xtabla As DataTable
            Dim pParam As OleDb.OleDbParameter
            ' sqlCmd.CommandTimeout = 3
            sqlCmd.CommandType = CommandType.StoredProcedure
            Param1 = Split(parametros, "|")
            valor1 = Split(valores, "|")
            j = UBound(Param1)
            For ii = 0 To j
                valor1(ii) = Replace(valor1(ii), "'", "")
                pParam = New OleDb.OleDbParameter("@" & Param1(ii), OleDb.OleDbType.VarChar)
                pParam.Direction = ParameterDirection.Input
                pParam.Value = valor1(ii)
                sqlCmd.Parameters.Add(pParam)
            Next ii
            Xtabla = New DataTable

            Dim ds As New DataSet
            Dim sda As New SqlClient.SqlDataAdapter(sqlCmd)

            sda.Fill(ds)
            Xtabla = ds.Tables(0)
            Return Xtabla
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function SPSinReturn(ByVal sp As String, ByVal conn As SqlClient.SqlConnection, Optional ByVal parametros As String = "", Optional ByVal valores As String = "") As Boolean
        Try
            Dim Param1() As String
            Dim valor1() As String
            Dim ii As Integer
            Dim j As Integer
            Dim sqlCmd As New SqlClient.SqlCommand(sp, conn)
            Dim pParam As SqlClient.SqlParameter
            sqlCmd.CommandType = CommandType.StoredProcedure
            Param1 = Split(parametros, "|")
            valor1 = Split(valores, "|")
            j = UBound(Param1)
            SPSinReturn = False
            For ii = 0 To j
                pParam = New SqlClient.SqlParameter("@" & Param1(ii), Data.SqlDbType.VarChar)
                pParam.Direction = ParameterDirection.Input
                pParam.Value = valor1(ii)
                sqlCmd.Parameters.Add(pParam)
            Next ii
            sqlCmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region
End Class
