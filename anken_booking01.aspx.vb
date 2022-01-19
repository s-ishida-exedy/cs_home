
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
        Dim strbkg As String
        Dim cno As Long
        Dim wno As Long
        Dim wday As String
        Dim wday2 As String
        Dim wday3 As String

        Dim dt1 As DateTime = DateTime.Now

        Dim Kaika00 As String


        If e.Row.RowType = DataControlRowType.DataRow Then





            '接続文字列の作成
            Dim ConnectionString As String = String.Empty
            'SQL Server認証
            ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
            'SqlConnectionクラスの新しいインスタンスを初期化
            Dim cnn = New SqlConnection(ConnectionString)

            'データベース接続を開く
            cnn.Open()



            strSQL = "SELECT DOCFIN_BKGNO FROM [T_EXL_CSWORKSTATUS] WHERE [T_EXL_CSWORKSTATUS].DOCFIN_BKGNO = '" & Trim(e.Row.Cells(25).Text) & "' "


            'ＳＱＬコマンド作成
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行
            dataread = dbcmd.ExecuteReader()

            strbkg = ""
            '結果を取り出す
            While (dataread.Read())
                strbkg += dataread("DOCFIN_BKGNO")

                '書類作成状況
                If Trim(e.Row.Cells(25).Text) = strbkg Then

                    e.Row.BackColor = Drawing.Color.DarkSalmon
                    e.Row.Cells(0).Text = e.Row.Cells(0).Text & " " & "書類済"

                End If

            End While

            'クローズ処理
            dataread.Close()
            dbcmd.Dispose()



            strSQL = "SELECT DECFIN_BKGNO FROM [T_EXL_CSWORKSTATUS] WHERE [T_EXL_CSWORKSTATUS].DECFIN_BKGNO = '" & Trim(e.Row.Cells(25).Text) & "' "


            'ＳＱＬコマンド作成
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行
            dataread = dbcmd.ExecuteReader()

            strinv = ""
            '結果を取り出す
            While (dataread.Read())
                strbkg += dataread("DECFIN_BKGNO")

                '書類作成状況
                If Trim(e.Row.Cells(25).Text) = strbkg Then

                    e.Row.BackColor = Drawing.Color.DarkGray
                    e.Row.Cells(0).Text = "通関済"
                    e.Row.Visible = False

                End If

            End While

            'クローズ処理
            dataread.Close()
            dbcmd.Dispose()


            If e.Row.Cells(0).Text = "LCL" Then

                e.Row.BackColor = Drawing.Color.DarkGray
                e.Row.Visible = False

            End If



            strSQL = "SELECT ITK_BKGNO FROM [T_EXL_CSWORKSTATUS] WHERE [T_EXL_CSWORKSTATUS].ITK_BKGNO = '" & Trim(e.Row.Cells(25).Text) & "' "


            'ＳＱＬコマンド作成
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行
            dataread = dbcmd.ExecuteReader()

            strinv = ""
            '結果を取り出す
            While (dataread.Read())
                strbkg += dataread("ITK_BKGNO")

                '書類作成状況
                If Trim(e.Row.Cells(25).Text) = strbkg Then

                    e.Row.BackColor = Drawing.Color.DarkGray
                    e.Row.Cells(0).Text = "通関委託"
                    e.Row.Visible = False

                End If

            End While

            'クローズ処理
            dataread.Close()
            dbcmd.Dispose()

            cnn.Close()
            cnn.Dispose()



            If e.Row.Cells(0).Text = "当日必須" Then

                e.Row.BackColor = Drawing.Color.LightGreen


            End If


            If e.Row.Cells(0).Text = "EXDCUT" Then


                e.Row.BackColor = Drawing.Color.LightBlue

            End If




        End If

        '不要行非表示

        e.Row.Cells(9).Visible = False
        e.Row.Cells(10).Visible = False
        e.Row.Cells(11).Visible = False



        e.Row.Cells(13).Visible = False
        e.Row.Cells(14).Visible = False
        e.Row.Cells(15).Visible = False
        e.Row.Cells(16).Visible = False
        e.Row.Cells(17).Visible = False
        e.Row.Cells(18).Visible = False
        e.Row.Cells(19).Visible = False
        e.Row.Cells(20).Visible = False
        e.Row.Cells(21).Visible = False
        e.Row.Cells(22).Visible = False
        e.Row.Cells(23).Visible = False

        e.Row.Cells(26).Visible = False
        e.Row.Cells(29).Visible = False
        e.Row.Cells(31).Visible = False

        e.Row.Cells(33).Visible = False
        e.Row.Cells(34).Visible = False
        e.Row.Cells(35).Visible = False
        e.Row.Cells(36).Visible = False



        If e.Row.Cells(0).Text = "未着手" Then

            e.Row.Visible = False

        End If




    End Sub


End Class
