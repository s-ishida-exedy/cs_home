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
        End If

        'CType(e.Row(1).FindControl("Label1"), String).Text = Left(CType(GridView2.Row(1).FindControl("Label1"), String).Text, 10)

    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged
        Dim sch_ID As String = GridView1.SelectedValue.ToString
        '選択された行のSCH_ID
        strRow = sch_ID
    End Sub

    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load

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
        If e.CommandName = "edt" Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim data1 = Me.GridView1.Rows(index).Cells(2).Text
            Dim data2 = Me.GridView1.Rows(index).Cells(3).Text
            Dim data3 = Me.GridView1.Rows(index).Cells(5).Text
            Dim data4 = Me.GridView1.Rows(index).Cells(1).Text

            Session("strEtd") = Format(data1, "yyyy/mm/dd")
            Session("strIvno") = data2
            Session("strCust") = data3
            Session("strStatus") = data4
            Response.Redirect("epa_request_detail.aspx")
        End If
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        '追加登録ボタン押下
        Response.Redirect("epa_request_detail_ins.aspx")
    End Sub
End Class
