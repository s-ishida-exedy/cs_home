
Partial Class calendar
    Inherits System.Web.UI.Page

    Private Sub Calendar1_SelectionChanged(sender As Object, e As EventArgs) Handles Calendar1.SelectionChanged
        '選択された日付を保持
        Dim sDate As String = Calendar1.SelectedDate.ToString("d")

        '変更する子フォームの要素（ID)を取得
        Dim id As String = Request.QueryString("id")

        '親フォームの要素を変更するスクリプトを作成
        Dim script As String
        script = "if(!window.opener || window.opener.closed) {" +
             "    window.close();" +
             "} else { " +
             "    window.opener.document.getElementById('" + id + "').value = '" + sDate + "';" +
             "    window.close();" +
             "}"

        ClientScript.RegisterClientScriptBlock(Me.GetType(), "calendar", script, True)
    End Sub

    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load
        If IsPostBack = True Then
            Return
        End If

        ' GETパラメータの日付を初期表示する
        Dim dateParam As DateTime
        DateTime.TryParse(Request.QueryString("date"), dateParam)
        Calendar1.VisibleDate = dateParam
        Calendar1.SelectedDate = dateParam
    End Sub
End Class
