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

        Dim dt1 As DateTime = DateTime.Now



        If e.Row.RowType = DataControlRowType.DataRow Then


            Dim str1 As String = Left(e.Row.Cells(2).Text, 4)
            Select Case str1
                Case "C258"

                    cno = 8

                Case "C255"

                    cno = 10

                Case Else

                    cno = 7

            End Select

            Dim dt3 As DateTime = DateTime.Parse(e.Row.Cells(7).Text)
            Dim ts1 As New TimeSpan(cno, 0, 0, 0)
            Dim dt2 As DateTime = dt3 - ts1


            e.Row.Cells(6).Text = dt2


        End If



        '搬入日作成

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()

        'ヘッダー以外に処理
        If e.Row.RowType = DataControlRowType.DataRow Then


            '対象の日付以下の日付の最大値を取得

            strSQL = "SELECT MAX(WORKDAY) AS WDAY01 FROM [T_EXL_CSWORKDAY] WHERE [T_EXL_CSWORKDAY].WORKDAY < '" & e.Row.Cells(6).Text & "' "


            'ＳＱＬコマンド作成 
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行 
            dataread = dbcmd.ExecuteReader()


            '結果を取り出す 
            While (dataread.Read())
                wday2 = dataread("WDAY01")
            End While


            If Weekday(dt1) > 6 Then

                cno = 7 - Weekday(dt1) + 6

            Else

                cno = 6 - Weekday(dt1) + 7

            End If



            If e.Row.RowType = DataControlRowType.DataRow Then



                e.Row.Cells(5).Text = wday2

                Dim dt3 As DateTime = DateTime.Parse(e.Row.Cells(5).Text)
                Dim ts1 As New TimeSpan(cno, 0, 0, 0)
                Dim dt2 As DateTime = dt1 + ts1


                If dt3 < dt2 Then



                    e.Row.BackColor = Drawing.Color.Salmon

                    If (e.Row.Cells(10).Text.Length = 6) And dt3 < dt2 Then

                        e.Row.Cells(10).Text = "AC要"
                        e.Row.Cells(10).BackColor = Drawing.Color.Red
                        e.Row.Cells(10).ForeColor = Drawing.Color.White

                    End If


                End If


                e.Row.Cells(5).Text = e.Row.Cells(5).Text & " (" & dt3.ToString("ddd") & ")"


            End If


            'クローズ処理 
            dataread.Close()
            dbcmd.Dispose()

        End If



        strSQL = "SELECT LCLARGD_INVNO FROM [T_EXL_CSWORKSTATUS] WHERE [T_EXL_CSWORKSTATUS].LCLARGD_INVNO = '" & Left(e.Row.Cells(3).Text, 4) & "' "


        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        strinv = ""
        '結果を取り出す 
        While (dataread.Read())
            strinv += dataread("LCLARGD_INVNO")

            '書類作成状況
            If Left(e.Row.Cells(3).Text, 4) = strinv Then

                If e.Row.Cells(10).Text = "AC要" Then
                    e.Row.Cells(10).Text = " Booking依頼済み"
                End If

            End If

        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()



        cnn.Close()
        cnn.Dispose()

    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged
        Dim sch_ID As String = GridView1.SelectedValue.ToString
        '選択された行のSCH_ID
        strRow = sch_ID
    End Sub

    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load

        Me.Label1.Text = ""

        '最終更新年月日取得
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String
        Dim strinv As String
        Dim cno As Long
        Dim wno As Long
        Dim wday As String
        Dim wday2 As String

        Dim dt1 As DateTime = DateTime.Now


        If Weekday(dt1) > 6 Then

            cno = 7 - Weekday(dt1) + 6

        Else

            cno = 6 - Weekday(dt1) + 7

        End If


        Dim ts1 As New TimeSpan(cno, 0, 0, 0)
        Dim dt2 As DateTime = dt1 + ts1


        '最終更新年月日を表示
        Me.Label1.Text = "対象期間：" & dt1.ToShortDateString & " (" & dt1.ToString("ddd") & ") " & "~ " & dt2.ToShortDateString & " (" & dt2.ToString("ddd") & ") "



    End Sub


End Class
