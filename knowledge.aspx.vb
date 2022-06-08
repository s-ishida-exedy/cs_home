Imports System.Data.SqlClient
Imports System.Data.Common
Imports System.Data

Partial Class cs_home
    Inherits System.Web.UI.Page

    Public strRow As String
    Dim Factroy As DbProviderFactory
    Dim Conn As DbConnection
    Dim Cmd As DbCommand
    Dim Da As DbDataAdapter
    Dim Ds As DataSet

    Public Function Dbconnect() As DbConnection
        Dim settings As ConnectionStringSettings

        Factroy =
        DbProviderFactories.GetFactory("System.Data.SqlClient")

        Conn = Factroy.CreateConnection()
        settings = ConfigurationManager.
                    ConnectionStrings("EXPDBConnectionString")
        ' 接続文字列の設定
        Conn.ConnectionString = settings.ConnectionString

        Conn.Open()

        Return Conn

    End Function

    Private Sub GridView1_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowCreated

        'ボタンに行数をセット
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim dltButton As Button = e.Row.FindControl("Button1")
            'ボタンが存在する場合のみセット
            If Not (dltButton Is Nothing) Then
                dltButton.CommandArgument = e.Row.RowIndex.ToString()
            End If
        End If

        'コード列非表示処理
        If e.Row.RowType = DataControlRowType.DataRow OrElse e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(1).Visible = False
            e.Row.Cells(6).Visible = False
        End If

    End Sub

    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load
        If IsPostBack Then
            ' そうでない時処理
        Else
            Me.DropDownList1.Items.Insert(0, "-SELECT-") '先頭に空白行追加
            Me.DropDownList2.Items.Insert(0, "") '先頭に空白行追加
            Me.DropDownList3.Items.Insert(0, "") '先頭に空白行追加

            Call Del_Session()            'セッション情報クリア
        End If

        AddHandler GridView1.RowCommand, AddressOf GridView1_RowCommand

    End Sub

    Private Sub Del_Session()
        'セッション情報をクリア
        Session.Remove("strMode")
        Session.Remove("strSEQ")

    End Sub

    Private Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList1.SelectedIndexChanged
        '大分類の変更タイミング

        Dim strSQL As String = ""

        'ドロップダウンリスト２をクリア
        DropDownList2.Items.Clear()

        '選択されたコードを取得する
        Dim strCode As String = DropDownList1.SelectedValue

        strSQL = ""
        strSQL = strSQL & "SELECT KO.CAT_SEC, CA.DESCRIPTION "
        strSQL = strSQL & "FROM T_EXL_KNOWLEDGE KO "
        strSQL = strSQL & "INNER JOIN M_EXL_CATEGORY CA "
        strSQL = strSQL & "ON KO.CAT_SEC = CA.CODE "
        strSQL = strSQL & "WHERE CAT_PRI = '" & strCode & "' "
        strSQL = strSQL & "ORDER BY CAT_SEC  "

        Conn = Me.Dbconnect
        Cmd = Conn.CreateCommand

        Cmd.CommandText = strSQL

        Da = Factroy.CreateDataAdapter()
        Da.SelectCommand = Cmd
        Ds = New DataSet
        Da.Fill(Ds)

        DropDownList2.DataSource = Ds.Tables(0)
        DropDownList2.DataValueField = Ds.Tables(0).Columns(0).ColumnName.ToString
        DropDownList2.DataTextField = Ds.Tables(0).Columns(1).ColumnName.ToString
        DropDownList2.DataBind()

        DropDownList2.Items.Insert(0, "-SELECT-")

        Call Make_Grid()
    End Sub

    Private Sub DropDownList2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList2.SelectedIndexChanged
        '中分類の変更タイミング

        Dim strSQL As String = ""

        'ドロップダウンリスト２をクリア
        DropDownList3.Items.Clear()

        '選択されたコードを取得する
        Dim strCode As String = DropDownList1.SelectedValue
        Dim strCode2 As String = DropDownList2.SelectedValue

        strSQL = ""
        strSQL = strSQL & "SELECT KO.CAT_TER, CA.DESCRIPTION "
        strSQL = strSQL & "FROM T_EXL_KNOWLEDGE KO "
        strSQL = strSQL & "INNER JOIN M_EXL_CATEGORY CA "
        strSQL = strSQL & "ON KO.CAT_TER = CA.CODE "
        strSQL = strSQL & "WHERE CAT_PRI = '" & strCode & "' "
        strSQL = strSQL & "AND   CAT_SEC = '" & strCode2 & "' "
        strSQL = strSQL & "ORDER BY CAT_TER  "

        Conn = Me.Dbconnect
        Cmd = Conn.CreateCommand

        Cmd.CommandText = strSQL

        Da = Factroy.CreateDataAdapter()
        Da.SelectCommand = Cmd
        Ds = New DataSet
        Da.Fill(Ds)

        DropDownList3.DataSource = Ds.Tables(0)
        DropDownList3.DataValueField = Ds.Tables(0).Columns(0).ColumnName.ToString
        DropDownList3.DataTextField = Ds.Tables(0).Columns(1).ColumnName.ToString
        DropDownList3.DataBind()

        DropDownList3.Items.Insert(0, "-SELECT-")

        Call Make_Grid()
    End Sub

    Private Sub DropDownList3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList3.SelectedIndexChanged
        '小分類の変更タイミング

        Call Make_Grid()

    End Sub

    Private Sub Make_Grid()
        'GRIDを作成する。

        Dim Dataobj As New DBAccess
        Dim strCode As String = DropDownList1.SelectedValue
        Dim strCode2 As String = DropDownList2.SelectedValue
        Dim strCode3 As String = DropDownList3.SelectedValue
        Dim strKeyword As String = Trim(TextBox1.Text)

        If strCode = "-SELECT-" Then
            strCode = ""
        End If
        If strCode2 = "-SELECT-" Then
            strCode2 = ""
        End If
        If strCode3 = "-SELECT-" Then
            strCode3 = ""
        End If

        'データの取得
        Dim ds As DataSet = Dataobj.GET_RESULT_KNOWLEDGE(strCode, strCode2, strCode3, strKeyword)
        If ds.Tables.Count > 0 Then
            GridView1.DataSourceID = ""
            GridView1.DataSource = ds
            GridView1.DataBind()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        '検索ボタン押下
        Call Make_Grid()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'リセットボタン押下
        DropDownList1.SelectedIndex = 0
        DropDownList2.SelectedIndex = 0
        DropDownList3.SelectedIndex = 0
        TextBox1.Text = ""

        Call Make_Grid()
    End Sub

    Private Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName = "edt" Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim data1 = Me.GridView1.Rows(index).Cells(1).Text

            Session("strMode") = "0"    '更新モード
            Session("strSEQ") = data1
            Response.Redirect("know_detail.aspx")
        End If

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        '新規登録ボタン押下

        Session("strMode") = "1"    '登録モード
        Response.Redirect("know_detail.aspx")
    End Sub
End Class
