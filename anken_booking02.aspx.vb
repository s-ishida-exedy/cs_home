
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.Common
Partial Class yuusen
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
        Dim wno2 As Long
        Dim wno3 As Long
        Dim wday As String
        Dim wday2 As String
        Dim wday3 As String
        Dim wday4 As String
        Dim strbkg As String

        Dim dt1 As DateTime = DateTime.Now





        If e.Row.RowType = DataControlRowType.DataRow Then

            '接続文字列の作成
            Dim ConnectionString As String = String.Empty
            'SQL Server認証
            ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
            'SqlConnectionクラスの新しいインスタンスを初期化
            Dim cnn = New SqlConnection(ConnectionString)
            Dim Command = cnn.CreateCommand

            'データベース接続を開く
            cnn.Open()


            strSQL = "SELECT ITK_BKGNO FROM [T_EXL_CSWORKSTATUS] WHERE [T_EXL_CSWORKSTATUS].ITK_BKGNO = '" & Trim(e.Row.Cells(9).Text) & "' "


            'ＳＱＬコマンド作成
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行
            dataread = dbcmd.ExecuteReader()

            strbkg = ""
            '結果を取り出す
            While (dataread.Read())
                strbkg += dataread("ITK_BKGNO")

                '書類作成状況
                If Trim(e.Row.Cells(9).Text) = strbkg Then

                    Call itaku(e.Row.Cells(9).Text)


                End If

            End While

            'クローズ処理
            dataread.Close()
            dbcmd.Dispose()

            cnn.Close()
            cnn.Dispose()





        End If

        If e.Row.RowType = DataControlRowType.DataRow Then

            If e.Row.Cells(1).Text = "1" Then

                e.Row.Cells(1).Text = "委託登録済"
                e.Row.BackColor = Drawing.Color.DarkSalmon


            End If

        End If



    End Sub




    Private Sub itaku(bkgno As String)
        '確認完了ボタン押下時


        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=KBHWPM02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand
        Dim strSQL As String = ""
        Dim ivno As String = ""



        'データベース接続を開く
        cnn.Open()

        'FIN_FLGを更新
        strSQL = ""
        strSQL = strSQL & "UPDATE T_EXL_CSANKEN SET FLG02 ='1' "
        strSQL = strSQL & "WHERE BOOKING_NO = '" & bkgno & "'"



        Command.CommandText = strSQL
        ' SQLの実行
        Command.ExecuteNonQuery()




    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

                GridView1.DataBind()

    End Sub


    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click


        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=KBHWPM02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand
        Dim strSQL As String = ""
        Dim ivno As String = ""



        'データベース接続を開く
        cnn.Open()



        Dim I As Integer
        For I = 0 To GridView1.Rows.Count - 1
            If CType(GridView1.Rows(I).Cells(0).Controls(1), CheckBox).Checked Then


                'FIN_FLGを更新
                strSQL = ""
                strSQL = strSQL & "UPDATE T_EXL_CSANKEN SET FLG02 ='1' "
                strSQL = strSQL & "WHERE BOOKING_NO = '" & GridView1.Rows(I).Cells(9).Text & "'"

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()


            Else



            End If
        Next



        GridView1.DataBind()



    End Sub


End Class
