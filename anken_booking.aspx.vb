﻿
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
            Dim Command = cnn.CreateCommand

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

                End If

            End While

            'クローズ処理
            dataread.Close()
            dbcmd.Dispose()


            If e.Row.Cells(0).Text = "LCL" Then

                e.Row.BackColor = Drawing.Color.DarkGray

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

                    'Call itaku(e.Row.Cells(25).Text)


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

    End Sub


    Protected Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList1.SelectedIndexChanged

        If DropDownList1.Text = "進捗状況" Then

            DropDownList2.Items.Clear()
            DropDownList2.DataSource = SqlDataSource2
            DropDownList2.DataTextField = "STATUS"
            DropDownList2.DataValueField = "STATUS"
            DropDownList2.DataBind()


            DropDownList2.Items.Insert(0, "--Select--")


        ElseIf DropDownList1.Text = "シート" Then

            DropDownList2.Items.Clear()
            DropDownList2.DataSource = SqlDataSource3
            DropDownList2.DataTextField = "FORWARDER"
            DropDownList2.DataValueField = "FORWARDER"
            DropDownList2.DataBind()


            DropDownList2.Items.Insert(0, "--Select--")

        ElseIf DropDownList1.Text = "海貨業者" Then

            DropDownList2.Items.Clear()
            DropDownList2.DataSource = SqlDataSource4
            DropDownList2.DataTextField = "FORWARDER02"
            DropDownList2.DataValueField = "FORWARDER02"
            DropDownList2.DataBind()


            DropDownList2.Items.Insert(0, "--Select--")

        ElseIf DropDownList1.Text = "客先コード" Then

            DropDownList2.Items.Clear()
            DropDownList2.DataSource = SqlDataSource8
            DropDownList2.DataTextField = "CUST"
            DropDownList2.DataValueField = "CUST"
            DropDownList2.DataBind()


            DropDownList2.Items.Insert(0, "--Select--")



        End If


    End Sub
    Protected Sub DropDownList2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList2.SelectedIndexChanged


        If DropDownList1.Text = "進捗状況" Then

            GridView3.DataSource = SqlDataSource1
            GridView3.DataBind()


        ElseIf DropDownList1.Text = "シート" Then

            GridView3.DataSource = SqlDataSource5
            GridView3.DataBind()

        ElseIf DropDownList1.Text = "海貨業者" Then

            GridView3.DataSource = SqlDataSource6
            GridView3.DataBind()

        ElseIf DropDownList1.Text = "客先コード" Then

            GridView3.DataSource = SqlDataSource9
            GridView3.DataBind()

        End If

        Panel1.Visible = False
        Panel2.Visible = True


    End Sub
    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Panel1.Visible = True
        Panel2.Visible = False

    End Sub






    Private Sub GridView3_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView3.RowDataBound

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

                End If

            End While

            'クローズ処理
            dataread.Close()
            dbcmd.Dispose()


            If e.Row.Cells(0).Text = "LCL" Then

                e.Row.BackColor = Drawing.Color.DarkGray

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


End Class
