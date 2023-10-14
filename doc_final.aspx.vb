Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.Common
Imports System.IO

Partial Class cs_home
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If IsPostBack Then
            ' そうでない時処理
        Else
            Me.DropDownList2.Items.Insert(0, "-VAN日-") '先頭に空白行追加（日付）
            Me.DropDownList1.Items.Insert(0, "-場所-") '先頭に空白行追加（場所）
        End If
    End Sub

    Private Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList1.SelectedIndexChanged
        Dim Dataobj As New DBAccess
        Dim strDate As String = ""
        Dim strPlace As String = ""

        If DropDownList2.SelectedValue <> "-VAN日-" Then
            strDate = DropDownList2.SelectedValue
        ElseIf DropDownList2.SelectedValue = "-VAN日-" Then
            strDate = ""
        End If
        If DropDownList1.SelectedValue <> "-場所-" Then
            strPlace = DropDownList1.SelectedValue
        ElseIf DropDownList1.SelectedValue = "-場所-" Then
            strPlace = ""
        End If

        Dim ds As DataSet = Dataobj.GET_FINAL_DOC_DATA(strDate, strPlace)
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

        If DropDownList2.SelectedValue <> "-VAN日-" Then
            strDate = DropDownList2.SelectedValue
        ElseIf DropDownList2.SelectedValue = "-VAN日-" Then
            strDate = ""
        End If
        If DropDownList1.SelectedValue <> "-場所-" Then
            strPlace = DropDownList1.SelectedValue
        ElseIf DropDownList1.SelectedValue = "-場所-" Then
            strPlace = ""
        End If

        Dim ds As DataSet = Dataobj.GET_FINAL_DOC_DATA(strDate, strPlace)
        If ds.Tables.Count > 0 Then
            GridView1.DataSourceID = ""
            GridView1.DataSource = ds
            Session("data") = ds
        End If

        'Grid再表示
        GridView1.DataBind()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'リストの選択状態をリセット
        DropDownList1.SelectedIndex = 0     '場所
        DropDownList2.SelectedIndex = 0     '日付

        'データ再取得
        Dim Dataobj As New DBAccess
        Dim ds As DataSet = Dataobj.GET_FINAL_DOC_DATA("", "")
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
        Dim strBKG As String = ""

        If e.CommandName = "edt" Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim data1 = Me.GridView1.Rows(index).Cells(4).Text
            Dim data2 = Me.GridView1.Rows(index).Cells(3).Text
            Dim data3 = Me.GridView1.Rows(index).Cells(1).Text

            'イントラよりブッキングNOを取得
            Call GET_BKGNO(Left(data2, 4), strBKG)

            'インボイス番号をキーにデータを更新
            Call Update_FLG(data3, data1, data2, strBKG)

            'Grid再表示
            GridView1.DataBind()
        End If
    End Sub

    Private Sub GET_BKGNO(strIVNO As String, ByRef strBKG As String)
        'インボイスNOに該当するブッキングNOを取得する。
        Dim strSQL As String
        Dim dtNow As DateTime = DateTime.Now
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strFlg As String = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand

        'データベース接続を開く
        cnn.Open()

        'フラグを確認
        strSQL = ""
        strSQL = strSQL & "SELECT BOOKINGNO FROM V_T_INV_HD_TB "
        strSQL = strSQL & "WHERE OLD_INVNO = '" & strIVNO & "' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        '結果を取り出す 
        While (dataread.Read())
            strBKG = dataread("BOOKINGNO")
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()

    End Sub

    Private Sub Update_FLG(strPlace As String, strFVAN As String, strIVNO As String, strBKG As String)
        'レコードのフラグを取得する。
        Dim strSQL As String
        Dim dtNow As DateTime = DateTime.Now
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strFlg As String = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand

        'データベース接続を開く
        cnn.Open()

        'フラグを確認
        strSQL = ""
        strSQL = strSQL & "SELECT UPD_FLG FROM T_EXL_FIN_DOC "
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

        'strFlgが空白の場合：データなしのため、INSERT（未作成⇒作成済み）
        'strFlgが１以外の場合：データありのため、DELETE（作成済み⇒未作成）
        strSQL = ""
        If strFlg = "" Then
            strSQL = strSQL & "INSERT INTO T_EXL_FIN_DOC VALUES(  "
            strSQL = strSQL & "'" & strPlace & "' "
            strSQL = strSQL & ",'" & strFVAN & "' "
            strSQL = strSQL & ",'" & strIVNO & "' "
            strSQL = strSQL & ",'" & strBKG & "' "
            strSQL = strSQL & ",'1' "
            strSQL = strSQL & ",'" & dtNow & "' "
            strSQL = strSQL & ",'" & Session("UsrId") & "') "
        ElseIf strFlg = "1" Then
            strSQL = strSQL & "DELETE FROM T_EXL_FIN_DOC   "
            strSQL = strSQL & "WHERE FIN_VAN = '" & strFVAN & "' "
            strSQL = strSQL & "AND IVNO = '" & strIVNO & "' "
        End If

        Command.CommandText = strSQL
        ' SQLの実行
        Command.ExecuteNonQuery()

        cnn.Close()
        cnn.Dispose()

    End Sub

    Private Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound
        'レコードのフラグを取得する。
        Dim strSQL As String
        Dim dtNow As DateTime = DateTime.Now
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strFlg As String = ""

        If e.Row.RowType = DataControlRowType.DataRow Then
            '接続文字列の作成
            Dim ConnectionString As String = String.Empty
            'SQL Server認証
            ConnectionString = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
            'SqlConnectionクラスの新しいインスタンスを初期化
            Dim cnn = New SqlConnection(ConnectionString)
            Dim Command = cnn.CreateCommand

            'データベース接続を開く
            cnn.Open()

            'フラグを確認
            strSQL = ""
            strSQL = strSQL & "SELECT UPD_FLG FROM T_EXL_FIN_DOC "
            strSQL = strSQL & "WHERE IVNO = '" & e.Row.Cells(3).Text & "' "

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

            '未作成の場合、フォントカラーをREDに設定
            If strFlg = "" Then
                e.Row.Cells(7).Text = "未作成"
                e.Row.Cells(7).ForeColor = Drawing.Color.Red
            ElseIf strFlg = "1" Then
                e.Row.Cells(7).Text = "作成済み"
            End If

            cnn.Close()
            cnn.Dispose()
        End If
    End Sub
End Class
