Imports Microsoft.VisualBasic
Imports System.Data.Common
Imports System.Data
Imports System.Data.SqlClient

Public Class mod_function
    Dim Factroy As DbProviderFactory
    Dim Conn As DbConnection
    Dim Cmd As DbCommand
    Dim Da As DbDataAdapter
    Dim Ds As DataSet
    Dim StrSQL As String

    Public Function Dbconnect() As DbConnection
        Dim settings As ConnectionStringSettings

        Factroy =
        DbProviderFactories.GetFactory("System.Data.SqlClient")

        Conn = Factroy.CreateConnection()
        settings = ConfigurationManager.
                    ConnectionStrings("EXPDBConnectionString")
        ' 接続文字列の設定
        Conn.ConnectionString = settings.ConnectionString

        Conn.Open()

        Return Conn

    End Function

    Public Shared Function HankakuEisuChk(strValue As String) As Boolean
        '半角英数チェック

        HankakuEisuChk = True

        '正規表現パターンを指定(英字a-z,A-Z,数値0-9,ハイフン)
        '        Dim r As New System.Text.RegularExpressions.Regex(“^[a-zA-Z0-9[ ]]+$”)
        Dim r As New System.Text.RegularExpressions.Regex(“^[-\a-zA-Z0-9\uFF61-\uFF9F\s]+$”)

        '半角英数字に一致しているかチェック
        If r.IsMatch(strValue) = False Then
            HankakuEisuChk = False
        End If
    End Function

    Public Shared Function Chk_Hiduke(strDate As String) As Boolean
        '引数が日付になっているかチェック
        Dim dt As DateTime
        'DateTimeに変換できるかチェック
        If DateTime.TryParse(strDate, dt) Then
            Chk_Hiduke = True
        Else
            Chk_Hiduke = False
        End If
    End Function

    Public Shared Sub DATA_UPD_TIME(strCode As String)
        '更新日時をテーブルへUPDATE
        Dim strSQL As String
        Dim strVal As String = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=KBHWPM02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand

        'データベース接続を開く
        cnn.Open()

        'データ更新
        strSQL = ""
        strSQL = strSQL & "UPDATE T_EXL_DATA_UPD SET "
        strSQL = strSQL & "DATA_UPD = '" & strVal & "' "
        strSQL = strSQL & "WHERE DATA_CD = '" & strCode & "' "

        Command.CommandText = strSQL
        ' SQLの実行
        Command.ExecuteNonQuery()

        cnn.Close()
    End Sub

    Public Shared Sub DATA_UPD_CNT(strCode As String, intCnt As Long)
        '作業件数をテーブルへUPDATE
        Dim strSQL As String
        Dim strVal As String = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=KBHWPM02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand

        'データベース接続を開く
        cnn.Open()

        'データ更新
        strSQL = ""
        strSQL = strSQL & "UPDATE T_EXL_POR_CNT SET "
        strSQL = strSQL & "DATA_CNT = " & intCnt & " "
        strSQL = strSQL & "WHERE DATA_CD = '" & strCode & "' "

        Command.CommandText = strSQL
        ' SQLの実行
        Command.ExecuteNonQuery()

        cnn.Close()
    End Sub

    Public Shared Function HankakuNumChk(strValue As String) As Boolean
        '半角数字チェック

        HankakuNumChk = True

        '正規表現パターンを指定(英字a-z,A-Z,数値0-9)
        '        Dim r As New System.Text.RegularExpressions.Regex(“^[a-zA-Z0-9[ ]]+$”)
        Dim r As New System.Text.RegularExpressions.Regex(“^[.0-9]+$”)

        '半角英数字に一致しているかチェック
        If r.IsMatch(strValue) = False Then
            HankakuNumChk = False
        End If
    End Function

    Public Shared Function GET_SHITEI_EIGYOBI(strDate As String, intTarget As Integer, strMode As String) As String
        'MODE01: 引数の日以降の営業日を取得(引数の日含む)
        'MODE02: 引数の日以前の営業日を取得(引数の日含む)

        Dim i As Integer
        Dim StrSQL As String = ""
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand

        GET_SHITEI_EIGYOBI = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=KBHWPM02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand

        'データベース接続を開く
        cnn.Open()


        '対象データ件数を取得する
        StrSQL = ""
        StrSQL = StrSQL & "SELECT "
        StrSQL = StrSQL & "  WORKDAY "
        StrSQL = StrSQL & "FROM "
        StrSQL = StrSQL & "  T_EXL_CSWORKDAY "
        If strMode = "01" Then
            StrSQL = StrSQL & "WHERE WORKDAY >= '" & strDate & "' "
            StrSQL = StrSQL & "ORDER BY WORKDAY"
        ElseIf strMode = "02" Then
            StrSQL = StrSQL & "WHERE WORKDAY <= '" & strDate & "' "
            StrSQL = StrSQL & "ORDER BY WORKDAY DESC"
        End If

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(StrSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        i = 1

        '結果を取り出す 
        While (dataread.Read())
            If intTarget = i Then
                GET_SHITEI_EIGYOBI = dataread("WORKDAY")
                Exit While
            End If

            i += 1
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()

    End Function
End Class
