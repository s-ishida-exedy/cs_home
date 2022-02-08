Imports System.Data.SqlClient


Partial Class cs_home
    Inherits System.Web.UI.Page

    Public strRow As String
    Public strProcess As String


    Private Sub GridView1_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound

        '最終更新年月日取得
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String
        Dim strinv As String
        Dim cno As Long
        Dim wno As Long
        Dim wday As String
        Dim wday2 As String

        Dim dt1 As DateTime = DateTime.Now.ToShortDateString





        If e.Row.RowType = DataControlRowType.DataRow Then



            If e.Row.Cells(1).Text = dt1.ToShortDateString Then

                e.Row.Cells(1).BackColor = Drawing.Color.Salmon
                e.Row.Cells(2).BackColor = Drawing.Color.Salmon

            End If




        End If





        e.Row.Cells(0).Width = 90
        e.Row.Cells(1).Width = 40
        e.Row.Cells(2).Width = 100
        e.Row.Cells(3).Width = 70
        e.Row.Cells(4).Width = 70
        e.Row.Cells(5).Width = 110
        e.Row.Cells(6).Width = 140
        e.Row.Cells(7).Width = 70
        e.Row.Cells(8).Width = 70
        e.Row.Cells(9).Width = 70
        e.Row.Cells(10).Width = 50
        e.Row.Cells(11).Width = 60
        e.Row.Cells(12).Width = 60
        e.Row.Cells(13).Width = 110
        e.Row.Cells(14).Width = 10
        e.Row.Cells(15).Width = 110
        e.Row.Cells(16).Width = 10
        e.Row.Cells(17).Width = 110


        e.Row.Cells(5).Visible = False
        e.Row.Cells(6).Visible = False
        e.Row.Cells(9).Visible = False
        'e.Row.Cells(12).Visible = False
        'e.Row.Cells(13).Visible = False
        'e.Row.Cells(14).Visible = False



    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged
        Dim sch_ID As String = GridView1.SelectedValue.ToString
        '選択された行のSCH_ID
        strRow = sch_ID
    End Sub

    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load

        '        Me.Label2.Text = ""

        '最終更新年月日取得
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String
        Dim strDate As String

        ''接続文字列の作成
        'Dim ConnectionString As String = String.Empty
        ''SQL Server認証
        'ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        ''SqlConnectionクラスの新しいインスタンスを初期化
        'Dim cnn = New SqlConnection(ConnectionString)

        ''データベース接続を開く
        'cnn.Open()

        'strSQL = "SELECT FINAL_DATE FROM t_booking_update01"
        ''ＳＱＬコマンド作成 
        'dbcmd = New SqlCommand(strSQL, cnn)
        ''ＳＱＬ文実行 
        'dataread = dbcmd.ExecuteReader()

        'strDate = ""
        ''結果を取り出す 
        'While (dataread.Read())
        '    strDate += dataread("FINAL_DATE")
        'End While

        ''クローズ処理 
        'dataread.Close()
        'dbcmd.Dispose()
        'cnn.Close()
        'cnn.Dispose()

        ''最終更新年月日を表示
        'Me.Label2.Text = Left(strDate, 4) & "/" & Mid(strDate, 5, 2) & "/" & Mid(strDate, 7, 2) _
        '     & " " & Mid(strDate, 9, 2) & ":" & Mid(strDate, 11, 2) & ":" & Mid(strDate, 13, 2) & " 更新"







    End Sub

End Class
