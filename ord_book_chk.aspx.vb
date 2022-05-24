Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.Common
Imports mod_function

Partial Class cs_home
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If IsPostBack Then
            ' そうでない時処理
        Else

        End If

        'グリッド表示
        Call GET_RESULT_BOOKING()

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        '確認済みのデータを表示する。
        If CheckBox1.Checked = True Then

            Dim Dataobj As New DBAccess
            'データの取得
            Dim ds As DataSet = Dataobj.GET_SN_COMFIRM()
            If ds.Tables.Count > 0 Then
                GridView1.DataSourceID = ""
                GridView1.DataSource = ds
                GridView1.DataBind()
            End If

        End If
    End Sub

    Private Sub GET_RESULT_BOOKING()
        'データを取得
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()
        '２営業日前を取得
        Dim strDate As String = GET_SHITEI_EIGYOBI(Now, 2, "02")

        strSQL = ""
        strSQL = strSQL & "SELECT DISTINCT SNH.SALESNOTENO, SNH.ORDERNO "
        strSQL = strSQL & "FROM V_T_SN_HD_TB SNH "
        strSQL = strSQL & "INNER JOIN V_T_SN_BD_TB SNB "
        strSQL = strSQL & "ON SNH.SALESNOTENO = SNB.SALESNOTENO "
        strSQL = strSQL & "WHERE SNB.LS = '2' "
        strSQL = strSQL & "AND SNH.NOKIYMD >= CONVERT(NVARCHAR,  GETDATE(), 111) "
        strSQL = strSQL & "AND SNB.ERRCD = '' "
        strSQL = strSQL & "AND SNH.FINISHDATE Is Not NULL "
        strSQL = strSQL & "AND SNH.NOKIYMD <> '9999/12/31 0:00:00'"
        strSQL = strSQL & "AND SNB.LEFTQTY > 0 "
        strSQL = strSQL & "AND SNH.FINISHDATE < '" & strDate & "' "
        strSQL = strSQL & "AND (SNH.CUSTCODE NOT IN ('C255','E140','E155') AND SNH.CUSTCODE NOT LIKE 'A%' AND SNH.CUSTCODE NOT LIKE 'B%') "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        Dim strORDNO As String = ""
        Dim strORDNO2 As String = ""

        While (dataread.Read())
            strORDNO = Trim(dataread("ORDERNO"))

            'オーダーNOをキーにブッキング情報が存在するか確認
            Dim arr1() As String = GET_BOOK_INFO(strORDNO).Split(",")
            'ブッキングデータに無い場合、変数に追加

            For Each c In arr1
                If c = "" Then
                    strORDNO2 += "'" & strORDNO & "',"
                End If
            Next

        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()

        '取得したstrSNNOの右側のカンマを削除する
        strORDNO2 = Mid(strORDNO2, 1, Len(strORDNO2) - 1)

        Dim Dataobj As New DBAccess
        'データの取得
        Dim ds As DataSet = Dataobj.GET_SN_RESULT(strORDNO2)
        If ds.Tables.Count > 0 Then
            GridView1.DataSourceID = ""
            GridView1.DataSource = ds
            GridView1.DataBind()
        End If

    End Sub

    Private Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        'グリッド内のボタン押下処理
        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim data1 = Me.GridView1.Rows(index).Cells(1).Text
        Dim data2 = Me.GridView1.Rows(index).Cells(2).Text
        Dim data3 = Me.GridView1.Rows(index).Cells(3).Text
        Dim data4 = Me.GridView1.Rows(index).Cells(4).Text
        Dim data5 = Me.GridView1.Rows(index).Cells(5).Text
        Dim data6 = Me.GridView1.Rows(index).Cells(6).Text
        Dim data7 = Me.GridView1.Rows(index).Cells(7).Text
        Dim strSQL As String

        'データをテーブルに登録
        If e.CommandName = "upd" Then
            '接続文字列の作成
            Dim ConnectionString As String = String.Empty
            'SQL Server認証
            ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
            'SqlConnectionクラスの新しいインスタンスを初期化
            Dim cnn = New SqlConnection(ConnectionString)
            Dim Command = cnn.CreateCommand

            'データベース接続を開く
            cnn.Open()

            strSQL = ""
            strSQL = strSQL & "INSERT INTO T_EXL_BOOKING_CHK VALUES("
            strSQL = strSQL & " '" & Trim(data1) & "' "
            strSQL = strSQL & ",'" & Trim(data2) & "' "
            strSQL = strSQL & ",'" & Trim(data3) & "' "
            strSQL = strSQL & ",'" & Trim(data4) & "' "
            strSQL = strSQL & ",'" & Trim(data5) & "' "
            strSQL = strSQL & ",'" & Trim(data6) & "' "
            strSQL = strSQL & ",'" & Trim(data7) & "' "
            strSQL = strSQL & ")"

            Command.CommandText = strSQL
            ' SQLの実行
            Command.ExecuteNonQuery()

            cnn.Close()
            cnn.Dispose()

        End If

        '画面再表示
        Call GET_RESULT_BOOKING()
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

        'コード列非表示処理
        If (e.Row.RowType = DataControlRowType.DataRow OrElse e.Row.RowType = DataControlRowType.Header) And Me.CheckBox1.Checked = True Then
            e.Row.Cells(0).Visible = False
        End If
    End Sub

    Private Function GET_BOOK_INFO(strORDNO As String) As String
        'ブッキング情報を取得

        Dim strSQL As String = ""
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand

        GET_BOOK_INFO = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()

        strSQL = ""
        strSQL = strSQL & "SELECT PONO "
        strSQL = strSQL & "FROM T_BOOKING "
        strSQL = strSQL & "WHERE T_BOOKING.PONO LIKE '%" & strORDNO & "%' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        '結果を取り出す 
        While (dataread.Read())
            GET_BOOK_INFO = Trim(dataread("PONO"))
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()
    End Function
End Class
