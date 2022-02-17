Imports System.Data.SqlClient
Partial Class cs_home
    Inherits System.Web.UI.Page

    Public strRow As String
    Public strProcess As String

    Private Sub GridView1_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowCreated

        '列非表示処理
        If e.Row.RowType = DataControlRowType.DataRow OrElse e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(1).Visible = False
            e.Row.Cells(7).Visible = False
        End If

        'ボタンに行数をセット
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim dltButton As Button = e.Row.FindControl("Button1")
            'ボタンが存在する場合のみセット
            If Not (dltButton Is Nothing) Then
                dltButton.CommandArgument = e.Row.RowIndex.ToString()
            End If
        End If

    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged
        Dim sch_ID As String = GridView1.SelectedValue.ToString
        '選択された行のSCH_ID
        strRow = sch_ID
    End Sub

    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load

        AddHandler GridView1.RowCommand, AddressOf GridView1_RowCommand
    End Sub

    Private Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName = "edt" Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)

            Dim strId As String = Me.GridView1.Rows(index).Cells(1).Text
            '更新/削除モード
            Response.Redirect("topics_detail.aspx?strId=" & strId & "&strMode=02")
        End If
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        '新規登録ボタン押下
        '登録モード
        Response.Redirect("topics_detail.aspx?strMode=03")
    End Sub
End Class
