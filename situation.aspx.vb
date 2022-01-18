Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.Common
Partial Class cs_home
    Inherits System.Web.UI.Page

    Public strRow As String
    Public strProcess As String



    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Call GET_PORTAL_DATA()
    End Sub

    Private Sub GET_PORTAL_DATA()
        '概況ステータス情報を取得する
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()

        strSQL = ""
        strSQL = strSQL & "SELECT * FROM T_EXL_PORTAL_STATUS "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        While (dataread.Read())
            Literal1.Text = Trim(StrConv(dataread("CONTAINER"), VbStrConv.Wide))
            Literal2.Text = "更新日時：" + dataread("UPDTIME_CONTAINER")
            Literal3.Text = Trim(StrConv(dataread("VANNING"), VbStrConv.Wide))
            Literal4.Text = "更新日時：" + dataread("UPDTIME_VANNING")
            Literal5.Text = Trim(StrConv(dataread("FORTH_FLOOR"), VbStrConv.Wide))
            Literal6.Text = "更新日時：" + dataread("UPDTIME_FLOOR")
            Literal7.Text = Trim(StrConv(dataread("EED"), VbStrConv.Wide))
            Literal8.Text = "更新日時：" + dataread("UPDTIME_EED")
            Literal9.Text = Trim(StrConv(dataread("THIRD_KONPO"), VbStrConv.Wide))
            Literal10.Text = "更新日時：" + dataread("UPDTIME_KONPO")
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()

    End Sub
End Class
