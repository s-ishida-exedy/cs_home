Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.Common
Imports System.IO

Partial Class cs_home
    Inherits System.Web.UI.Page

    Public strRow As String
    Public strProcess As String
    Public strPath As String = "C:\exp\cs_home\files"

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Label2.Text = ""

        If IsPostBack Then
            ' そうでない時処理
        Else
            Me.DropDownList2.Items.Insert(0, "-VAN日-") '先頭に空白行追加（日付）
            Me.DropDownList1.Items.Insert(0, "-場所-") '先頭に空白行追加（場所）
        End If

        '最終更新年月日取得
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String
        Dim strDate As String

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()

        strSQL = "SELECT DISTINCT UPDATE_TIME FROM T_EXL_VAN_SCH_DETAIL"
        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        strDate = ""
        '結果を取り出す 
        While (dataread.Read())
            strDate = dataread("UPDATE_TIME")
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()

        '最終更新年月日を表示
        Me.Label2.Text = strDate & " 更新"
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'ダウンロードボタン押下
        Dim strChanged As String    'サーバー上のフルパス
        Dim strFileNm As String     'ファイル名

        'ファイル名を取得する
        Dim strTxtFiles() As String = IO.Directory.GetFiles(strPath, "VAN_*.xlsx")

        strChanged = strTxtFiles(0)
        strFileNm = Path.GetFileName(strChanged)

        'Contentをクリア
        Response.ClearContent()

        'Contentを設定
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("shift-jis")
        Response.ContentType = "application/vnd.ms-excel"

        '表示ファイル名を指定
        Dim fn As String = HttpUtility.UrlEncode(strFileNm)
        Response.AddHeader("Content-Disposition", "attachment;filename=" + fn)

        'ダウンロード対象ファイルを指定
        Response.WriteFile(strChanged)

        'ダウンロード実行
        Response.Flush()
        Response.End()
    End Sub

    Private Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList1.SelectedIndexChanged
        Dim Dataobj As New DBAccess
        Dim strDate As String = ""
        Dim strPlace As String = ""

        Select Case DropDownList1.SelectedValue
            Case "01:本社"
                strPlace = "0H"
            Case "02:上野"
                strPlace = "1U"
            Case "03:AIR"
                strPlace = "2A"
        End Select

        If DropDownList2.SelectedValue <> "-VAN日-" Then
            strDate = DropDownList2.SelectedValue
        End If

        Dim ds As DataSet = Dataobj.GET_RESULT_VAN_SCH(strDate, strPlace)
        If ds.Tables.Count > 0 Then
            GridView1.DataSourceID = ""
            GridView1.DataSource = ds
            Session("data") = ds
        End If

        'Grid再表示
        GridView1.DataBind()
    End Sub

    Private Sub DropDownList2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList2.SelectedIndexChanged
        Dim Dataobj As New DBAccess
        Dim strDate As String = ""
        Dim strPlace As String = ""

        Select Case DropDownList1.SelectedValue
            Case "01:本社"
                strPlace = "0H"
            Case "02:上野"
                strPlace = "1U"
            Case "03:AIR"
                strPlace = "2A"
        End Select

        If DropDownList2.SelectedValue <> "-VAN日-" Then
            strDate = DropDownList2.SelectedValue
        End If

        Dim ds As DataSet = Dataobj.GET_RESULT_VAN_SCH(strDate, strPlace)
        If ds.Tables.Count > 0 Then
            GridView1.DataSourceID = ""
            GridView1.DataSource = ds
            Session("data") = ds
        End If

        'Grid再表示
        GridView1.DataBind()
    End Sub

    Private Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound
        '書類の最終状況を取得

        If e.Row.RowType = DataControlRowType.DataRow Then
            'インボイス番号取得
            Dim drv As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim strCode As String = Mid(Trim(drv("インボイスNO").ToString()), 4, 4)

            Dim dataread As SqlDataReader
            Dim dbcmd As SqlCommand
            Dim strSQL As String = ""
            Dim strDate As String = Format(Now, "yyyy/MM/dd")

            '接続文字列の作成
            Dim ConnectionString As String = String.Empty
            'SQL Server認証
            ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
            'SqlConnectionクラスの新しいインスタンスを初期化
            Dim cnn = New SqlConnection(ConnectionString)

            'データベース接続を開く
            cnn.Open()

            strSQL = strSQL & "SELECT DISTINCT VSD.* "
            strSQL = strSQL & "FROM T_EXL_VAN_SCH_DETAIL VSD "
            strSQL = strSQL & "INNER JOIN "
            strSQL = strSQL & "(SELECT AA.* "
            strSQL = strSQL & "FROM "
            strSQL = strSQL & "(SELECT "
            strSQL = strSQL & "  CUST_CD "
            strSQL = strSQL & "  , INVOICE_NO "
            strSQL = strSQL & "  , MAX(VAN_DATE) AS FINAL_VAN "
            strSQL = strSQL & "FROM "
            strSQL = strSQL & "  ( SELECT CUST_CD, INVOICE_NO, DAY01 AS VAN_DATE FROM T_BOOKING  "
            strSQL = strSQL & "    UNION ALL  "
            strSQL = strSQL & "    SELECT CUST_CD, INVOICE_NO, DAY02 AS VAN_DATE FROM T_BOOKING  "
            strSQL = strSQL & "    UNION ALL  "
            strSQL = strSQL & "    SELECT CUST_CD, INVOICE_NO, DAY03 AS VAN_DATE FROM T_BOOKING "
            strSQL = strSQL & "    UNION ALL  "
            strSQL = strSQL & "    SELECT CUST_CD, INVOICE_NO, DAY04 AS VAN_DATE FROM T_BOOKING "
            strSQL = strSQL & "    UNION ALL  "
            strSQL = strSQL & "    SELECT CUST_CD, INVOICE_NO, DAY05 AS VAN_DATE FROM T_BOOKING "
            strSQL = strSQL & "    UNION ALL  "
            strSQL = strSQL & "    SELECT CUST_CD, INVOICE_NO, DAY06 AS VAN_DATE FROM T_BOOKING "
            strSQL = strSQL & "    UNION ALL  "
            strSQL = strSQL & "    SELECT CUST_CD, INVOICE_NO, DAY07 AS VAN_DATE FROM T_BOOKING "
            strSQL = strSQL & "    UNION ALL  "
            strSQL = strSQL & "    SELECT CUST_CD, INVOICE_NO, DAY08 AS VAN_DATE FROM T_BOOKING "
            strSQL = strSQL & "    UNION ALL  "
            strSQL = strSQL & "    SELECT CUST_CD, INVOICE_NO, DAY09 AS VAN_DATE FROM T_BOOKING "
            strSQL = strSQL & "    UNION ALL  "
            strSQL = strSQL & "    SELECT CUST_CD, INVOICE_NO, DAY10 AS VAN_DATE FROM T_BOOKING "
            strSQL = strSQL & "    UNION ALL  "
            strSQL = strSQL & "    SELECT CUST_CD, INVOICE_NO, DAY11 AS VAN_DATE FROM T_BOOKING) AS TBL  "
            strSQL = strSQL & "WHERE "
            strSQL = strSQL & "  INVOICE_NO <> ''  "
            strSQL = strSQL & "  AND VAN_DATE <> '' "
            strSQL = strSQL & "GROUP BY "
            strSQL = strSQL & "  CUST_CD , INVOICE_NO)AA "
            strSQL = strSQL & "WHERE AA.FINAL_VAN = '" & strDate & "') AAA "
            '            strSQL = strSQL & "ON VSD.IVNO LIKE '%' + LEFT(AAA.INVOICE_NO,4) + '%' "
            strSQL = strSQL & "On AAA.INVOICE_NO Like '%' + RIGHT(VSD.IVNO,4) + '%'  "
            strSQL = strSQL & "WHERE VSD.IVNO Like '%" & strCode & "%' "
            strSQL = strSQL & "ORDER BY PLACE, VAN_DATE, VAN_TIME "

            'ＳＱＬコマンド作成 
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行 
            dataread = dbcmd.ExecuteReader()

            strDate = ""
            '結果を取り出す 
            While (dataread.Read())
                '最終列に値セット
                e.Row.Cells(9).Text = "★"
            End While

            'クローズ処理 
            dataread.Close()
            dbcmd.Dispose()
            cnn.Close()
            cnn.Dispose()

            '未作成の場合、フォントカラーをREDに設定
            If e.Row.Cells(10).Text = "未作成" Then
                e.Row.Cells(10).ForeColor = Drawing.Color.Red
            End If
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'リストの選択状態をリセット
        DropDownList1.SelectedIndex = 0     '場所
        DropDownList2.SelectedIndex = 0     '日付

        'データ再取得
        Dim Dataobj As New DBAccess
        Dim ds As DataSet = Dataobj.GET_RESULT_VAN_SCH("", "")
        If ds.Tables.Count > 0 Then
            GridView1.DataSourceID = ""
            GridView1.DataSource = ds
            Session("data") = ds
        End If

        'Grid再表示
        GridView1.DataBind()

    End Sub

    Private Sub GridView1_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowCreated

        'ボタンに行数をセット
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim dltButton As Button = e.Row.FindControl("Button1")
            'ボタンが存在する場合のみセット
            If Not (dltButton Is Nothing) Then
                dltButton.CommandArgument = e.Row.RowIndex.ToString()
            End If
        End If
    End Sub

    Private Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        'GridViewのボタン押下処理
        If e.CommandName = "edt" Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim data1 = Me.GridView1.Rows(index).Cells(6).Text

            'インボイス番号をキーにデータを更新
            Call Update_FLG(data1)

            'Grid再表示
            GridView1.DataBind()
        End If
    End Sub

    Private Sub Update_FLG(strIVNO As String)
        'レコードのフラグを取得する。
        Dim strSQL As String
        Dim dtNow As DateTime = DateTime.Now
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strFlg As String = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand

        'データベース接続を開く
        cnn.Open()

        'フラグを確認し、１（作成済み）なら０（未作成）にUPDATEする。
        strSQL = ""
        strSQL = strSQL & "SELECT UPD_FLG FROM T_EXL_VAN_SCH_DETAIL "
        strSQL = strSQL & "WHERE IVNO = '" & strIVNO & "' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        '結果を取り出す 
        While (dataread.Read())
            strFlg = dataread("UPD_FLG")
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()

        'フラグをUPDATE
        strSQL = ""
        strSQL = strSQL & "UPDATE T_EXL_VAN_SCH_DETAIL  "
        If strFlg = "0" Then
            strSQL = strSQL & "SET UPD_FLG = '1', UPDATE_TIME = '" & dtNow & "', UPD_PERSON = '" & Session("UsrId") & "'  "
        ElseIf strFlg = "1" Then
            strSQL = strSQL & "SET UPD_FLG = '0', UPDATE_TIME = '" & dtNow & "', UPD_PERSON = '" & Session("UsrId") & "' "
        End If
        strSQL = strSQL & "WHERE IVNO = '" & strIVNO & "' "

        Command.CommandText = strSQL
        ' SQLの実行
        Command.ExecuteNonQuery()

        cnn.Close()
        cnn.Dispose()

    End Sub
End Class
