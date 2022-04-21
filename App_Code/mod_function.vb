Imports Microsoft.VisualBasic
Imports System.Data.Common
Imports System.Data
Imports System.Data.SqlClient
Imports Oracle.DataAccess.Client

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

    Public Shared Function GET_SHITEI_EIGYOBI(strDate As String, intTarget As Integer) As String
        '引数の日以降の営業日を取得(引数の日含む)
        Dim i As Integer
        Dim StrSQL As String = ""

        GET_SHITEI_EIGYOBI = ""

        'データベースへの接続を開く
        'Dim conn As New OracleConnection
        'conn.ConnectionString = "User Id=EXL;Password=EXL;Data Source=EUCDB"
        'conn.Open()
        'Dim cmmd As New OracleCommand
        'cmmd.Connection = conn

        ''対象データ件数を取得する
        'StrSQL = ""
        'StrSQL = StrSQL & "SELECT "
        'StrSQL = StrSQL & "  CAL_DATE "
        'StrSQL = StrSQL & "FROM "
        'StrSQL = StrSQL & "  EXPJ.M_CAL "
        'StrSQL = StrSQL & "WHERE "
        'StrSQL = StrSQL & "  CAL_NO = 1 "
        'StrSQL = StrSQL & "  AND HOLIDAY_FLG = 0 "
        'StrSQL = StrSQL & "  AND CAL_DATE >= '" & strDate & "' "
        'StrSQL = StrSQL & "ORDER BY CAL_DATE"


        'cmmd.CommandText = StrSQL
        'Dim dr As OracleDataReader = cmmd.ExecuteReader

        'i = 1

        'While (dr.Read())
        '    If intTarget = i Then
        '        GET_SHITEI_EIGYOBI = dr("CAL_DATE")
        '    End If

        '    i += 1

        'End While

        'dr.Close()
        'conn.Close()

    End Function
End Class
