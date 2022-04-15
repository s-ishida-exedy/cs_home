<%@ Application Language="VB" %>

<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<script Language="VB" runat="Server">

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' セッションが開始されたときに発生します。

        'セッションが切れていたらログインページにリダイレクト
        If Me.Request.RawUrl.IndexOf("login.aspx") < 0 Then
            If Session("UsrId") = "" Then
                Response.Redirect("login.aspx?mode=timeout")
            End If
        End If

    End Sub

    Sub Application_OnAuthorizeRequest(sender As Object, e As EventArgs)
        ' アクセスの可否を判定するためのフラグ（初期値は「不可」）
        Dim flag As Boolean = False
        Dim strSQL As String = ""

        Dim ConnectionString As String = String.Empty
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"

        Dim objDb As New SqlConnection(ConnectionString)
        objDb.Open()

        strSQL = "SELECT role FROM M_EXL_AUTH WHERE url=@url "

        Dim objCom1 As New SqlCommand(strSQL, objDb)
        objCom1.Parameters.Clear()
        objCom1.Parameters.Add("@url", System.Data.SqlDbType.NVarChar, 50).Value = Request.CurrentExecutionFilePath
        'objCom1.Parameters.Add("@url", Request.CurrentExecutionFilePath)
        Dim objDr1 As SqlDataReader = objCom1.ExecuteReader()

        ' カレント・ページのURLをキーにauthテーブルを検索
        ' 権限の設定情報が存在する場合にのみ以下の処理を継続する
        If objDr1.Read() Then
            ' ページへのアクセスに必要な権限情報を文字列配列pageRoleに格納
            Dim pageRole As String() = objDr1.GetString(0).Split(",")
            objDr1.Close()

            strSQL = ""
            strSQL = "SELECT role FROM M_EXL_USR WHERE uid=@uid "

            Dim objCom2 As New SqlCommand(strSQL, objDb)
            objCom2.Parameters.Add("@uid", System.Data.SqlDbType.NVarChar, 50).Value = User.Identity.Name

            Dim objDr2 As SqlDataReader = objCom2.ExecuteReader()

            ' 認証済みユーザーの権限とページが要求する権限とを比較する
            ' ユーザー権限が1つでもページ権限に合致した場合には
            ' フラグをTrueに設定
            If objDr2.Read() Then
                Dim role As String() = objDr2.GetString(0).Split(",")
                For i As Integer = 0 To role.GetUpperBound(0)
                    If Array.IndexOf(pageRole, role(i)) > -1 Then flag = True
                Next
            End If
            ' フラグがFalseの場合、カレント・ユーザーでのアクセスは
            ' 認められないものと見なして、
            ' エラー・ステータス「403 Forbidden」を発行
            If Not flag Then
                Response.Clear()
                Response.StatusCode = 403
                Response.End()
            End If
        End If
        objDb.Close()

    End Sub

</script>