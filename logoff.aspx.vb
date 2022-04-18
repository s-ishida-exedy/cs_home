
Partial Class logoff
    Inherits System.Web.UI.Page

    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load
        '認証情報クリア
        FormsAuthentication.SignOut()

        '現在保持しているセッション情報をクリアする。
        Session.RemoveAll()

        'ログイン画面へ遷移
        'Response.Redirect("login.aspx")
        Response.Redirect("login.aspx?mode=logout")
    End Sub
End Class
