Imports System.Data.SqlClient
Partial Class cs_home
    Inherits System.Web.UI.Page

    Public strRow As String
    Public strProcess As String

    Private Sub GridView1_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowCreated

        '列非表示処理
        If e.Row.RowType = DataControlRowType.DataRow OrElse e.Row.RowType = DataControlRowType.Header Then
            'e.Row.Cells(10).Visible = False
        End If

        'ボタンに行数をセット
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim dltButton As Button = e.Row.FindControl("Button1")
            'ボタンが存在する場合のみセット
            If Not (dltButton Is Nothing) Then
                dltButton.CommandArgument = e.Row.RowIndex.ToString()
            End If

            Dim updButton As Button = e.Row.FindControl("Button2")
            Dim canButton As Button = e.Row.FindControl("Button3")
            Dim delButton As Button = e.Row.FindControl("Button4")
            'ボタンが存在する場合のみセット
            If Not (updButton Is Nothing) Then
                updButton.CommandArgument = e.Row.RowIndex.ToString()
                canButton.CommandArgument = e.Row.RowIndex.ToString()
                delButton.CommandArgument = e.Row.RowIndex.ToString()
            End If
        End If

    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged
        Dim sch_ID As String = GridView1.SelectedValue.ToString
        '選択された行のSCH_ID
        strRow = sch_ID
    End Sub

    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load
        Me.Label2.Text = ""

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

        strSQL = "SELECT FINAL_DATE FROM t_booking_update01"
        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        strDate = ""
        '結果を取り出す 
        While (dataread.Read())
            strDate += dataread("FINAL_DATE")
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()

        '最終更新年月日を表示
        Me.Label2.Text = Left(strDate, 4) & "/" & Mid(strDate, 5, 2) & "/" & Mid(strDate, 7, 2) _
             & " " & Mid(strDate, 9, 2) & ":" & Mid(strDate, 11, 2) & ":" & Mid(strDate, 13, 2) & " 更新"

        AddHandler GridView1.RowCommand, AddressOf GridView1_RowCommand
    End Sub

    Private Sub GridView1_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles GridView1.RowEditing
        '編集ボタン押下
        '選択行のステータスを取得する。
        'Dim sch_ID As String = GridView1.SelectedValue.ToString
        ''選択された行のSCH_ID
        'strRow = sch_ID


    End Sub

    Private Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName = "Edit" Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            'Dim data1 = Me.GridView1.Rows(index).Cells(1).Text
            Dim row As GridViewRow = GridView1.Rows(index)





            'Me.GridView1.Rows(index).Cells(2).Text = Server.HtmlDecode(row.Cells(1).Text)

            'Dim row As GridViewRow = GridView1.Rows(index)
            'Dim item As New ListItem()
            'item.Text = Server.HtmlDecode(row.Cells(2).Text)
            'If Not CustomersListBox.Items.Contains(item) Then
            '    CustomersListBox.Items.Add(item)
            'End If
        End If
    End Sub

    Protected Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs)
        'CType(GridView1.Rows(e.RowIndex).FindControl("DropDownList1"), DropDownList)
        Dim list As DropDownList = sender
        list.SelectedValue = "09"
    End Sub

    Private Sub GridView1_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles GridView1.RowUpdating
        Dim list As DropDownList = CType(GridView1.Rows(e.RowIndex).FindControl("DropDownList1"), DropDownList)
        list.SelectedValue = "09"
    End Sub
End Class
