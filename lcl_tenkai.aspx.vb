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



        ''搬入日作成

        ''接続文字列の作成
        'Dim ConnectionString As String = String.Empty
        ''SQL Server認証
        'ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        ''SqlConnectionクラスの新しいインスタンスを初期化
        'Dim cnn = New SqlConnection(ConnectionString)

        ''データベース接続を開く
        'cnn.Open()

        ''ヘッダー以外に処理
        'If e.Row.RowType = DataControlRowType.DataRow Then


        '    '対象の日付以下の日付の最大値を取得

        '    strSQL = "SELECT MAX(WORKDAY) AS WDAY01 FROM [T_EXL_CSWORKDAY] WHERE [T_EXL_CSWORKDAY].WORKDAY < '" & e.Row.Cells(6).Text & "' "


        '    'ＳＱＬコマンド作成 
        '    dbcmd = New SqlCommand(strSQL, cnn)
        '    'ＳＱＬ文実行 
        '    dataread = dbcmd.ExecuteReader()


        '    '結果を取り出す 
        '    While (dataread.Read())
        '        wday2 = dataread("WDAY01")
        '    End While


        '    If Weekday(dt1) > 6 Then

        '        cno = 7 - Weekday(dt1) + 6

        '    Else

        '        cno = 6 - Weekday(dt1) + 7

        '    End If



        If e.Row.RowType = DataControlRowType.DataRow Then



            '        e.Row.Cells(6).Text = wday2

            '        Dim ts1 As New TimeSpan(cno, 0, 0, 0)
            '        Dim dt2 As DateTime = dt1 + ts1


            If e.Row.Cells(1).Text = dt1.ToShortDateString Then

                e.Row.Cells(1).BackColor = Drawing.Color.Salmon
                e.Row.Cells(2).BackColor = Drawing.Color.Salmon

            End If


            '            e.Row.BackColor = Drawing.Color.Salmon

            '            If (e.Row.Cells(11).Text.Length = 6) And dt3 < dt2 Then

            '                e.Row.Cells(11).Text = "AC要"
            '                e.Row.Cells(11).BackColor = Drawing.Color.Red
            '                e.Row.Cells(11).ForeColor = Drawing.Color.White

            '            End If


            '        End If


            '        e.Row.Cells(6).Text = e.Row.Cells(6).Text & " (" & dt3.ToString("ddd") & ")"


            '    End If


            '    'クローズ処理 
            '    dataread.Close()
            '    dbcmd.Dispose()

        End If



        'strSQL = "SELECT LCLFIN_INVNO FROM [T_EXL_CSWORKSTATUS] WHERE [T_EXL_CSWORKSTATUS].LCLFIN_INVNO = '" & Left(e.Row.Cells(4).Text, 4) & "' "

        ''ＳＱＬコマンド作成 
        'dbcmd = New SqlCommand(strSQL, cnn)
        ''ＳＱＬ文実行 
        'dataread = dbcmd.ExecuteReader()

        'strinv = ""
        ''結果を取り出す 
        'While (dataread.Read())
        '    strinv += dataread("LCLFIN_INVNO")


        '    '出荷手配状況
        '    If Left(e.Row.Cells(4).Text, 4) = strinv Then
        '        e.Row.BackColor = Drawing.Color.LightBlue
        '    End If

        'End While

        ''クローズ処理 
        'dataread.Close()
        'dbcmd.Dispose()



        'strSQL = "SELECT LCLARGD_INVNO FROM [T_EXL_CSWORKSTATUS] WHERE [T_EXL_CSWORKSTATUS].LCLARGD_INVNO = '" & Left(e.Row.Cells(4).Text, 4) & "' "


        ''ＳＱＬコマンド作成 
        'dbcmd = New SqlCommand(strSQL, cnn)
        ''ＳＱＬ文実行 
        'dataread = dbcmd.ExecuteReader()

        'strinv = ""
        ''結果を取り出す 
        'While (dataread.Read())
        '    strinv += dataread("LCLARGD_INVNO")

        '    '書類作成状況
        '    If Left(e.Row.Cells(4).Text, 4) = strinv Then

        '        If e.Row.Cells(11).Text = "AC要" Then
        '            e.Row.Cells(11).Text = " Booking依頼済み"
        '        End If

        '    End If

        'End While

        ''クローズ処理 
        'dataread.Close()
        'dbcmd.Dispose()

        'strSQL = "SELECT DOCFIN_INVNO FROM [T_EXL_CSWORKSTATUS] WHERE [T_EXL_CSWORKSTATUS].DOCFIN_INVNO = '" & Left(e.Row.Cells(4).Text, 4) & "' "


        ''ＳＱＬコマンド作成 
        'dbcmd = New SqlCommand(strSQL, cnn)
        ''ＳＱＬ文実行 
        'dataread = dbcmd.ExecuteReader()

        'strinv = ""
        ''結果を取り出す 
        'While (dataread.Read())
        '    strinv += dataread("DOCFIN_INVNO")

        '    '書類作成状況
        '    If Left(e.Row.Cells(4).Text, 4) = strinv Then
        '        e.Row.BackColor = Drawing.Color.DarkGray
        '    End If

        'End While

        ''クローズ処理 
        'dataread.Close()
        'dbcmd.Dispose()


        'cnn.Close()
        'cnn.Dispose()

        e.Row.Cells(0).Width = 50
        e.Row.Cells(1).Width = 70
        e.Row.Cells(3).Width = 120
        e.Row.Cells(3).Width = 70
        e.Row.Cells(4).Width = 100
        e.Row.Cells(5).Width = 70
        e.Row.Cells(6).Width = 70
        e.Row.Cells(7).Width = 70
        e.Row.Cells(8).Width = 70
        e.Row.Cells(9).Width = 70
        e.Row.Cells(10).Width = 50
        e.Row.Cells(11).Width = 60
        e.Row.Cells(12).Width = 60
        e.Row.Cells(13).Width = 120
        e.Row.Cells(14).Width = 10
        e.Row.Cells(15).Width = 120
        e.Row.Cells(16).Width = 10
        e.Row.Cells(17).Width = 120



        e.Row.Cells(5).Visible = False
        e.Row.Cells(6).Visible = False
        e.Row.Cells(9).Visible = False


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
