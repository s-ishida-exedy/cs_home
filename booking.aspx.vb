Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing

Partial Class cs_home
    Inherits System.Web.UI.Page

    Public strRow As String
    Public strProcess As String

    Private Sub GridView1_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowCreated

        'コード列非表示処理
        If e.Row.RowType = DataControlRowType.DataRow OrElse e.Row.RowType = DataControlRowType.Header Then
            'e.Row.Cells(10).Visible = False
        End If

        Dim row As GridViewRow = e.Row

        ' データ行である場合に、onmouseover／onmouseout属性を追加（1）
        If row.RowType = DataControlRowType.DataRow Then

            ' onmouseover属性を設定
            row.Attributes("onmouseover") = "setBg(this, '#CC99FF')"

            ' データ行が通常行／代替行であるかで処理を分岐（2）
            If row.RowState = DataControlRowState.Normal Then
                row.Attributes("onmouseout") =
                  String.Format("setBg(this, '{0}')",
                    ColorTranslator.ToHtml(GridView1.RowStyle.BackColor))
            Else
                row.Attributes("onmouseout") =
                  String.Format("setBg(this, '{0}')",
                    ColorTranslator.ToHtml(
                      GridView1.AlternatingRowStyle.BackColor))
            End If
        End If

    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged
        Dim sch_ID As String = GridView1.SelectedValue.ToString
        '選択された行のSCH_ID
        strRow = sch_ID
    End Sub

    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load
        If IsPostBack Then
            ' そうでない時処理
        Else
            Me.DropDownList1.Items.Insert(0, "-ｽﾃｰﾀｽ-") '先頭に空白行追加
            Me.DropDownList2.Items.Insert(0, "-海貨業者-") '先頭に空白行追加
        End If

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

        strSQL = "SELECT DATA_UPD FROM T_EXL_DATA_UPD WHERE DATA_CD = '008'"
        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        strDate = ""
        '結果を取り出す 
        While (dataread.Read())
            strDate += dataread("DATA_UPD")
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()

        '最終更新年月日を表示
        Me.Label2.Text = strDate & " 更新"

    End Sub

    Private Sub Make_Grid()
        'GRIDを作成する。

        Dim Dataobj As New DBAccess
        Dim strSts As String = DropDownList1.SelectedValue
        Dim strFwd As String = DropDownList2.SelectedValue

        If strSts = "-ｽﾃｰﾀｽ-" Then
            strSts = ""
        End If
        If strFwd = "-海貨業者-" Then
            strFwd = ""
        End If

        'データの取得
        Dim ds As DataSet = Dataobj.GET_BOOKING_DATA(strSts, strFwd, Trim(Me.TextBox1.Text), Trim(Me.TextBox2.Text))
        If ds.Tables.Count > 0 Then
            GridView1.DataSourceID = ""
            GridView1.DataSource = ds
            GridView1.DataBind()
        End If
    End Sub

    Private Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList1.SelectedIndexChanged
        'ステータス選択
        Call Make_Grid()
    End Sub

    Private Sub DropDownList2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList2.SelectedIndexChanged
        '海貨業者選択
        Call Make_Grid()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        '絞込ボタン押下
        Call Make_Grid()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'リセットボタン押下
        DropDownList1.SelectedIndex = 0
        DropDownList2.SelectedIndex = 0
        TextBox1.Text = ""
        TextBox2.Text = ""

        Make_Grid()
    End Sub

End Class
