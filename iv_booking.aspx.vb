Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.Common
Partial Class cs_home
    Inherits System.Web.UI.Page

    Public strRow As String
    Public strProcess As String

    Private Sub GridView1_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowCreated

        'コード列非表示処理
        If e.Row.RowType = DataControlRowType.DataRow OrElse e.Row.RowType = DataControlRowType.Header Then
            'e.Row.Cells(10).Visible = False
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
        '更新時間取得
        strSQL = "SELECT UPDATE_TIME FROM T_COMPARE_INV_HD ORDER BY UPDATE_TIME DESC"
        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        strDate = ""
        '結果を取り出す 
        While (dataread.Read())
            strDate = dataread("UPDATE_TIME")
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()

        '最終更新年月日を表示
        Me.Label2.Text = strDate & " 更新"

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        '「確認済を非表示にする」チェックボックスの動作
        Dim Dataobj As New DBAccess

        If CheckBox1.Checked = False Then
            '確認済みも表示する
            Dim ds As DataSet = Dataobj.GET_RESULT_IV_HEADER()
            If ds.Tables.Count > 0 Then
                GridView1.DataSourceID = ""
                GridView1.DataSource = ds
                Session("data") = ds
            End If
        Else
            '確認済みは非表示にする
            Dim ds As DataSet = Dataobj.GET_RESULT_IV_HEADER_02()
            If ds.Tables.Count > 0 Then
                GridView1.DataSourceID = ""
                GridView1.DataSource = ds
                Session("data") = ds
            End If
        End If

        'Grid再表示
        GridView1.DataBind()

    End Sub
End Class
