Imports System.Data.SqlClient
Imports System.Data

Partial Class cs_home
    Inherits System.Web.UI.Page

    Private Sub GridView1_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowCreated

        '列非表示処理
        If e.Row.RowType = DataControlRowType.DataRow OrElse e.Row.RowType = DataControlRowType.Header Then
            'キー項目のETDをテーブルのデータのままセットし、その項目は非表示にする。
            e.Row.Cells(18).Visible = False
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

    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load

        AddHandler GridView1.RowCommand, AddressOf GridView1_RowCommand
    End Sub

    Private Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName = "edt" Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim data1 = Me.GridView1.Rows(index).Cells(18).Text     '非表示としたキー項目
            Dim data2 = Me.GridView1.Rows(index).Cells(3).Text
            Dim data3 = Me.GridView1.Rows(index).Cells(5).Text
            Dim data4 = Me.GridView1.Rows(index).Cells(1).Text

            Session("strEtd") = data1
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

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        'チェックボックスの制御
        Dim strValue As String = ""

        If CheckBox1.Checked = True Then
            '「未」以外も表示する。
            strValue = "True"
        Else
            strValue = "False"
        End If

        Dim Dataobj As New DBAccess

        'データの取得
        Dim ds As DataSet = Dataobj.GET_RESULT_EPA(strValue)
        If ds.Tables.Count > 0 Then
            GridView1.DataSourceID = ""
            GridView1.DataSource = ds
            GridView1.DataBind()
        End If
    End Sub
End Class
