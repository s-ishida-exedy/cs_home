Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports Oracle.DataAccess.Client
Imports mod_function

Partial Class cs_home
    Inherits System.Web.UI.Page



    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If IsPostBack Then
            ' そうでない時処理
        Else

            Call GET_ZAIKO()

            Call Calc_AMI()

            Call GET_SHUKEI()
        End If
    End Sub

    Private Sub GET_ZAIKO()
        '棚マスタの在庫状況をEXPから取得しINSERTする。

        Dim dt = New DataTable()
        Dim dt1 = New DataTable()
        Dim dbcmd As SqlCommand
        Dim strSQL As String
        ' 結合用データセット
        Dim ds = New DataSet()

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く(k3hwpm02)
        cnn.Open()

        'データ件数用SQL
        strSQL = ""
        strSQL = strSQL & "SELECT TANABAN "
        strSQL = strSQL & ", SHUBETSU "
        strSQL = strSQL & ", PLACE "
        strSQL = strSQL & ", OKIBA "
        strSQL = strSQL & ", HANTEI "
        strSQL = strSQL & ", BUNKATSU "
        strSQL = strSQL & ", JOIN_RES "
        strSQL = strSQL & ", 0 AS QTY "
        strSQL = strSQL & "FROM M_EXL_TANA1 "
        strSQL = strSQL & "ORDER BY TANABAN "
        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        Dim adapter = New SqlDataAdapter(dbcmd)
        adapter.Fill(dt)

        ds.Tables.Add(dt.Copy())
        ds.Tables(ds.Tables.Count - 1).TableName = "棚マスタ"

        'データベースへの接続を開く(EXPJ)
        Dim conn As New OracleConnection
        conn.ConnectionString = "User Id=EXD017397;Password=EXD017397;Data Source=EXPJ"
        conn.Open()

        'データの取得
        Dim cmd As New OracleCommand
        cmd.Connection = conn

        strSQL = ""
        strSQL = strSQL & "SELECT "
        strSQL = strSQL & " a.SHELF_CD "
        strSQL = strSQL & " ,SUM(a.STOCK_ON_HAND_QTY) as QTY "
        strSQL = strSQL & "FROM expj.T_INV_ITEM_STOCK a "
        strSQL = strSQL & "WHERE  a.STOCK_ON_HAND_QTY > 0 "
        strSQL = strSQL & "GROUP BY  SHELF_CD "
        strSQL = strSQL & "ORDER BY  SHELF_CD "
        cmd.CommandText = strSQL

        Dim adapter1 = New OracleDataAdapter(cmd)
        adapter1.Fill(dt1)

        ds.Tables.Add(dt1.Copy())
        ds.Tables(ds.Tables.Count - 1).TableName = "EXP在庫"

        'クローズ処理 
        cmd.Dispose()
        conn.Close()
        conn.Dispose()

        Dim row As DataRow
        Dim ChildRow() As DataRow
        Dim nRecordCnt As Integer = 0
        'リレーションを貼る
        ds.Relations.Add("結合情報",
                             ds.Tables("棚マスタ").Columns("TANABAN"),
                             ds.Tables("EXP在庫").Columns("SHELF_CD"),
                              False)
        '棚マスタのレコード分ループ
        For Each row In ds.Tables("棚マスタ").Rows
            ChildRow = row.GetChildRows("結合情報")
            '結合されていた場合
            If ChildRow.Length <> 0 Then
                If ChildRow(0)("SHELF_CD").ToString() = ds.Tables("棚マスタ").Rows(nRecordCnt)("TANABAN") Then
                    ds.Tables("棚マスタ").Rows(nRecordCnt)("JOIN_RES") = "有"
                    ds.Tables("棚マスタ").Rows(nRecordCnt)("QTY") = ChildRow(0)("QTY")
                End If
                nRecordCnt = nRecordCnt + 1
                Continue For
            End If
            nRecordCnt = nRecordCnt + 1
        Next

        'T_EXL_ZAIKO_RESの既存データを削除する。
        Dim Command = cnn.CreateCommand
        strSQL = ""
        strSQL = strSQL & "DELETE FROM T_EXL_ZAIKO_RES "
        Command.CommandText = strSQL
        ' SQLの実行
        Command.ExecuteNonQuery()

        '集計した結果をT_EXL_ZAIKO_RESにINSERTする。
        Dim sql As New StringBuilder
        dbcmd.Parameters.Clear()

        sql.AppendLine("INSERT INTO dbo.T_EXL_ZAIKO_RES ")
        sql.AppendLine("       (TANABAN ")
        sql.AppendLine("       ,SHUBETSU ")
        sql.AppendLine("       ,PLACE ")
        sql.AppendLine("       ,OKIBA ")
        sql.AppendLine("       ,HANTEI ")
        sql.AppendLine("       ,BUNKATSU ")
        sql.AppendLine("       ,JOIN_RES ")
        sql.AppendLine("       ,QTY) ")
        sql.AppendLine(" SELECT * ")
        sql.AppendLine("   FROM @ResultTable ")
        sql.AppendLine("; ")

        'パラメータの作成
        Dim params(0) As SqlParameter
        'TypeにSqlDbType.Structuredを渡します。
        params(0) = New SqlParameter("@ResultTable", SqlDbType.Structured, ParameterDirection.Input)

        '###############################################################################
        'DataTable をパラメータとして渡す為には、
        'SQL Server 側にユーザー定義テーブル型を作成しておく必要がある。
        'CREATE Type ResultTableType AS TABLE
        '(TANABAN varchar(30) Not null
        ', SHUBETSU varchar(50)
        ', PLACE varchar(30)
        ', OKIBA varchar(30)
        ', HANTEI varchar(30)
        ', BUNKATSU int
        ', JOIN_RES varchar(10))
        '###############################################################################
        params(0).TypeName = "dbo.ResultTableType"  'テーブルタイプの名称を渡します。

        params(0).Value = ds.Tables("棚マスタ")    '編集したデータテーブルを渡します。
        'パラメータ配列を渡します。
        dbcmd.Parameters.AddRange(params)

        'SQLの実行
        dbcmd.CommandType = CommandType.Text
        dbcmd.CommandText = sql.ToString()
        dbcmd.ExecuteNonQuery()

        'クローズ処理 
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()
    End Sub


    Private Sub Calc_AMI()
        '網パレットの数量計算
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()

        'データ取得し、データごとに判定
        strSQL = ""
        strSQL = strSQL & "SELECT a.TANABAN,a.SHUBETSU,a.HANTEI,a.BUNKATSU "
        strSQL = strSQL & "FROM T_EXL_ZAIKO_RES a "
        strSQL = strSQL & "WHERE a.SHUBETSU IN ('OEM(アミ)','AFアミ') "
        strSQL = strSQL & "ORDER BY a.TANABAN,a.SHUBETSU "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        While (dataread.Read())
            If dataread("HANTEI") <> "対象外" Then
                Call DB_access(dataread("TANABAN"), GET_EXP_DATA(dataread("TANABAN")))
            ElseIf dataread("HANTEI") = "対象外" Then
                '対象外の場合は、分割数そのままでUPDATE
                Call DB_access(dataread("TANABAN"), dataread("BUNKATSU"))
            End If
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()

    End Sub

    Private Sub DB_access(strTANABAN As String, intAMIQTY As Integer)
        Dim StrSQL As String = ""
        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand

        'データベース接続を開く
        cnn.Open()

        StrSQL = ""
        StrSQL = StrSQL & "UPDATE T_EXL_ZAIKO_RES SET "
        StrSQL = StrSQL & "AMI_QTY = " & intAMIQTY & " "
        StrSQL = StrSQL & "WHERE TANABAN = '" & strTANABAN & "' "
        Command.CommandText = StrSQL
        ' SQLの実行
        Command.ExecuteNonQuery()

        Command.Dispose()
        cnn.Close()
    End Sub

    Private Sub GET_SHUKEI()
        '各種別の集計を取得し、ラベルにセット
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()

        strSQL = ""
        strSQL = strSQL & "SELECT * "
        strSQL = strSQL & "FROM "
        strSQL = strSQL & " (SELECT SUM(CASE WHEN SHUBETSU = 'AF CD' THEN 1 END)     AS 'AF CD許容'     FROM T_EXL_ZAIKO_RES)AA "
        strSQL = strSQL & ",(SELECT SUM(CASE WHEN SHUBETSU = 'AF CD' AND (JOIN_RES = '有' OR HANTEI = '対象外') THEN 1 END)     AS 'AF CD'         FROM T_EXL_ZAIKO_RES)BB "
        strSQL = strSQL & ",(SELECT SUM(CASE WHEN SHUBETSU = 'AF CC' THEN 1 END)     AS 'AF CC許容'     FROM T_EXL_ZAIKO_RES)CC "
        strSQL = strSQL & ",(SELECT SUM(CASE WHEN SHUBETSU = 'AF CC' AND (JOIN_RES = '有' OR HANTEI = '対象外') THEN 1 END)     AS 'AF CC'         FROM T_EXL_ZAIKO_RES)DD "
        strSQL = strSQL & ",(SELECT SUM(CASE WHEN SHUBETSU = 'AFポリ' THEN 1 END)    AS 'AFポリ許容'    FROM T_EXL_ZAIKO_RES)EE "
        strSQL = strSQL & ",(SELECT SUM(CASE WHEN SHUBETSU = 'AFポリ' AND (JOIN_RES = '有' OR HANTEI = '対象外') THEN 1 END)    AS 'AFポリ'        FROM T_EXL_ZAIKO_RES)FF "
        strSQL = strSQL & ",(SELECT CASE WHEN SHUBETSU = 'AFアミ' THEN SUM(BUNKATSU) END AS 'AFアミ許容'    FROM T_EXL_ZAIKO_RES GROUP BY SHUBETSU HAVING SHUBETSU = 'AFアミ')GG "
        strSQL = strSQL & ",(SELECT CASE WHEN SHUBETSU = 'AFアミ' THEN SUM(AMI_QTY) END AS 'AFアミ'         FROM T_EXL_ZAIKO_RES GROUP BY SHUBETSU HAVING SHUBETSU = 'AFアミ')HH "
        strSQL = strSQL & ",(SELECT SUM(CASE WHEN SHUBETSU = 'OEM(ポリ)' THEN 1 END) AS 'OEM(ポリ)許容' FROM T_EXL_ZAIKO_RES)II "
        strSQL = strSQL & ",(SELECT SUM(CASE WHEN SHUBETSU = 'OEM(ポリ)' AND (JOIN_RES = '有' OR HANTEI = '対象外') THEN 1 END) AS 'OEM(ポリ)'     FROM T_EXL_ZAIKO_RES)JJ "
        strSQL = strSQL & ",(SELECT CASE WHEN SHUBETSU = 'OEM(アミ)' THEN SUM(BUNKATSU) END AS 'OEM(アミ)許容' FROM T_EXL_ZAIKO_RES GROUP BY SHUBETSU HAVING SHUBETSU = 'OEM(アミ)')KK "
        strSQL = strSQL & ",(SELECT CASE WHEN SHUBETSU = 'OEM(アミ)' THEN SUM(AMI_QTY) END AS 'OEM(アミ)'      FROM T_EXL_ZAIKO_RES GROUP BY SHUBETSU HAVING SHUBETSU = 'OEM(アミ)')LL "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        While (dataread.Read())
            Label1.Text = Math.Round(dataread("AF CD") / dataread("AF CD許容") * 100, 1, MidpointRounding.AwayFromZero) & "%"
            Label2.Text = dataread("AF CD許容")
            Label3.Text = dataread("AF CD")
            Label4.Text = Math.Round(dataread("AF CC") / dataread("AF CC許容") * 100, 1, MidpointRounding.AwayFromZero) & "%"
            Label5.Text = dataread("AF CC許容")
            Label6.Text = dataread("AF CC")
            Label7.Text = Math.Round(dataread("AFポリ") / dataread("AFポリ許容") * 100, 1, MidpointRounding.AwayFromZero) & "%"
            Label8.Text = dataread("AFポリ許容")
            Label9.Text = dataread("AFポリ")
            Label10.Text = Math.Round(dataread("AFアミ") / dataread("AFアミ許容") * 100, 1, MidpointRounding.AwayFromZero) & "%"
            Label11.Text = dataread("AFアミ許容")
            Label12.Text = dataread("AFアミ")
            Label13.Text = Math.Round(dataread("OEM(ポリ)") / dataread("OEM(ポリ)許容") * 100, 1, MidpointRounding.AwayFromZero) & "%"
            Label14.Text = dataread("OEM(ポリ)許容")
            Label15.Text = dataread("OEM(ポリ)")
            Label16.Text = Math.Round(dataread("OEM(アミ)") / dataread("OEM(アミ)許容") * 100, 1, MidpointRounding.AwayFromZero) & "%"
            Label17.Text = dataread("OEM(アミ)許容")
            Label18.Text = dataread("OEM(アミ)")
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()
    End Sub

    Private Function GET_EXP_DATA(strTanaNo As String) As Integer
        'EXPのインベントリ―品目在庫を検索し、在庫数を探る
        Dim intCnt As Integer = 0

        GET_EXP_DATA = 0

        'データベースへの接続を開く(EXPJ)
        Dim StrSQL As String = ""
        Dim conn As New OracleConnection
        conn.ConnectionString = "User Id=EXD017397;Password=EXD017397;Data Source=EXPJ"
        conn.Open()

        'データの取得
        Dim cmd As New OracleCommand
        cmd.Connection = conn

        StrSQL = ""
        StrSQL = StrSQL & "SELECT "
        StrSQL = StrSQL & " a.ITEM_CD "
        StrSQL = StrSQL & " ,STOCK_ON_HAND_QTY "
        StrSQL = StrSQL & "FROM expj.T_INV_ITEM_STOCK a "
        StrSQL = StrSQL & "WHERE  a.STOCK_ON_HAND_QTY > 0 "
        StrSQL = StrSQL & "AND  a.SHELF_CD = '" & strTanaNo & "'"
        cmd.CommandText = StrSQL
        Dim dataread As OracleDataReader = cmd.ExecuteReader()
        Dim strSize As String = ""
        Dim intMax As Integer = 0
        While (dataread.Read())
            '品番からサイズごとの網MAX値を取得する。
            If Left(dataread("ITEM_CD"), 2) = "DU" Then
                strSize = Mid(dataread("ITEM_CD"), 3, 3)
            Else
                strSize = Mid(dataread("ITEM_CD"), 5, 3)
            End If
            intMax = GET_SIZE_MAX(strSize, strTanaNo)

            '在庫数とMAX値を比較し、何網になるか算出し、結果を変数に入れて戻す
            If intMax >= dataread("STOCK_ON_HAND_QTY") Then
                intCnt = intCnt + 1
            ElseIf intMax * 2 >= dataread("STOCK_ON_HAND_QTY") >= intMax Then
                intCnt = intCnt + 2
            ElseIf intMax * 3 >= dataread("STOCK_ON_HAND_QTY") >= intMax * 2 Then
                intCnt = intCnt + 3
            End If
        End While

        GET_EXP_DATA = intCnt

        'クローズ処理 
        cmd.Dispose()
        conn.Close()
        conn.Dispose()

    End Function

    Private Function GET_SIZE_MAX(strSize As String, strTanaNo As String) As Integer
        'CCのサイズごとの網に入るMAXを取得する
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strAMI_SIZE As String = ""

        GET_SIZE_MAX = 0

        If strTanaNo = "F48" Then
            strAMI_SIZE = "Full"
        Else
            strAMI_SIZE = "Half"
        End If

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()

        strSQL = ""
        strSQL = strSQL & "SELECT "
        strSQL = strSQL & "MAX_QTY "
        strSQL = strSQL & "FROM M_EXL_AMI_SIZE a "
        strSQL = strSQL & "WHERE  CC_SIZE = '" & strSize & "'"
        strSQL = strSQL & "AND AMI_SIZE = '" & strAMI_SIZE & "'"

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        While (dataread.Read())
            GET_SIZE_MAX = dataread("MAX_QTY")
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()

    End Function
End Class
