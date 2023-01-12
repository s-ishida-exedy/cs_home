Imports System.Data.SqlClient
Imports System.Data

Partial Class cs_home
    Inherits System.Web.UI.Page

    Public strRow As String
    Public strProcess As String

    Private Sub GridView1_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowCreated

        '列非表示処理
        If e.Row.RowType = DataControlRowType.DataRow OrElse e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(2).Visible = False
            e.Row.Cells(8).Visible = False
            e.Row.Cells(9).Visible = False
        End If

        'ボタンに行数をセット
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim dltButton As Button = e.Row.FindControl("Button1")
            Dim dltButton2 As Button = e.Row.FindControl("Button2")
            'ボタンが存在する場合のみセット
            If Not (dltButton Is Nothing) Then
                dltButton.CommandArgument = e.Row.RowIndex.ToString()
                dltButton2.CommandArgument = e.Row.RowIndex.ToString()
            End If
        End If

    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged
        Dim sch_ID As String = GridView1.SelectedValue.ToString
        '選択された行のSCH_ID
        strRow = sch_ID
    End Sub

    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load



        '    If Session("UsrId") = "T43827" Or Session("UsrId") = "T43529" Then '
        '        Dim Script As String =
        '"<script type=""text/javascript"">" _
        '& "if(confirm( """ & "Are you Sakamute?" & """ )){" _
        '& " var url = 'back.aspx?q=';window.open(url, null);" _
        '& "}else{" _
        '& "}" _
        '& "</script>"
        '        ClientScript.RegisterStartupScript(Me.GetType, "kubun", Script)
        '    End If



    End Sub

    Private Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand

        Dim index As Integer = Convert.ToInt32(e.CommandArgument)

        If e.CommandName = "edt" Then
            Dim strId As String = Me.GridView1.Rows(index).Cells(2).Text
            Dim strUsrId As String = Me.GridView1.Rows(index).Cells(9).Text

            '他人のトピックスは編集不可
            If strUsrId <> Session("UsrId") Then
                Return
            End If

            '更新/削除モード
            Response.Redirect("topics_detail.aspx?strId=" & strId & "&strMode=02")
        ElseIf e.CommandName = "fin" Then
            Dim data1 = Me.GridView1.Rows(index).Cells(2).Text

            Call Update_FLG(data1)
        End If
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        '新規登録ボタン押下
        '登録モード
        Response.Redirect("topics_detail.aspx?strMode=03")
    End Sub

    Private Sub Update_FLG(strVal As String)
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
        Dim i As Integer = 0

        'データベース接続を開く
        cnn.Open()

        'フラグを確認し、１（作成済み）なら０（未作成）にUPDATEする。
        strSQL = ""
        strSQL = strSQL & "SELECT FIN_FLG FROM T_EXL_TOPICS "
        strSQL = strSQL & "WHERE INFO_NO = '" & strVal & "' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        '結果を取り出す 
        While (dataread.Read())
            strFlg = dataread("FIN_FLG")
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()

        'フラグをUPDATE
        strSQL = ""
        strSQL = strSQL & "UPDATE T_EXL_TOPICS "
        If strFlg = "0" Then
            strSQL = strSQL & "SET FIN_FLG = '1' "
        ElseIf strFlg = "1" Then
            strSQL = strSQL & "SET FIN_FLG = '0' "
        End If
        strSQL = strSQL & "WHERE INFO_NO = '" & strVal & "' "

        Command.CommandText = strSQL
        ' SQLの実行
        Command.ExecuteNonQuery()

        cnn.Close()
        cnn.Dispose()

        GridView1.DataBind()
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        'チェックボックスON/OFFによるデータ取得

        Dim strValue As String = ""

        If CheckBox1.Checked = True Then
            '「済」も表示する。
            strValue = "True"
        Else
            strValue = "False"
        End If

        Dim Dataobj As New DBAccess

        'データの取得
        Dim ds As DataSet = Dataobj.GET_RESULT_TOPICS(strValue)
        If ds.Tables.Count > 0 Then
            GridView1.DataSourceID = ""
            GridView1.DataSource = ds
            GridView1.DataBind()
        End If
    End Sub
End Class
