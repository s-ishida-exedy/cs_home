
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


            strSQL = "SELECT DOCFIN_BKGNO FROM [T_EXL_CSWORKSTATUS] WHERE [T_EXL_CSWORKSTATUS].DOCFIN_BKGNO = '" & Trim(e.Row.Cells(26).Text) & "' "


            'ＳＱＬコマンド作成
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行
            dataread = dbcmd.ExecuteReader()

            strbkg = ""
            '結果を取り出す
            While (dataread.Read())
                strbkg += dataread("DOCFIN_BKGNO")

                '書類作成状況
                If Trim(e.Row.Cells(26).Text) = strbkg Then

                    e.Row.BackColor = Drawing.Color.DarkSalmon
                    e.Row.Cells(1).Text = e.Row.Cells(1).Text & " " & "書類済"

                End If

            End While

            'クローズ処理
            dataread.Close()
            dbcmd.Dispose()



            strSQL = "SELECT DECFIN_BKGNO FROM [T_EXL_CSWORKSTATUS] WHERE [T_EXL_CSWORKSTATUS].DECFIN_BKGNO = '" & Trim(e.Row.Cells(26).Text) & "' "


            'ＳＱＬコマンド作成
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行
            dataread = dbcmd.ExecuteReader()

            strbkg = ""
            '結果を取り出す
            While (dataread.Read())
                strbkg += dataread("DECFIN_BKGNO")

                '書類作成状況
                If Trim(e.Row.Cells(26).Text) = strbkg Then

                    e.Row.BackColor = Drawing.Color.DarkGray
                    e.Row.Cells(1).Text = "通関済"

                End If

            End While

            'クローズ処理
            dataread.Close()
            dbcmd.Dispose()


            If e.Row.Cells(12).Text = "LCL" Then

                e.Row.BackColor = Drawing.Color.DarkGray
                e.Row.Cells(13).Text = "LCL"

            End If



            strSQL = "SELECT ITK_BKGNO FROM [T_EXL_CSWORKSTATUS] WHERE [T_EXL_CSWORKSTATUS].ITK_BKGNO = '" & Trim(e.Row.Cells(26).Text) & "' "


            'ＳＱＬコマンド作成
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行
            dataread = dbcmd.ExecuteReader()

            strbkg = ""
            '結果を取り出す
            While (dataread.Read())
                strbkg += dataread("ITK_BKGNO")

                '書類作成状況
                If Trim(e.Row.Cells(26).Text) = strbkg Then

                    e.Row.BackColor = Drawing.Color.DarkGray
                    e.Row.Cells(1).Text = "通関委託"

                    'Call itaku(e.Row.Cells(25).Text)


                End If

            End While

            'クローズ処理
            dataread.Close()
            dbcmd.Dispose()

            cnn.Close()
            cnn.Dispose()



            If e.Row.Cells(1).Text = "当日必須" Then

                e.Row.BackColor = Drawing.Color.LightGreen


            End If


            If e.Row.Cells(1).Text = "EXDCUT" Then


                e.Row.BackColor = Drawing.Color.LightBlue

            End If

            If e.Row.Cells(36).Text = "1" Then


                e.Row.Cells(0).BackColor = Drawing.Color.YellowGreen


            End If


        End If

        '不要行非表示


        e.Row.Cells(10).Visible = False
        e.Row.Cells(11).Visible = False
        e.Row.Cells(12).Visible = False



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
        e.Row.Cells(24).Visible = False

        e.Row.Cells(27).Visible = False
        e.Row.Cells(30).Visible = False
        e.Row.Cells(32).Visible = False


        e.Row.Cells(34).Visible = False
        e.Row.Cells(35).Visible = False
        e.Row.Cells(36).Visible = False
        e.Row.Cells(37).Visible = False



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
            Dim Command = cnn.CreateCommand

            'データベース接続を開く
            cnn.Open()



            strSQL = "SELECT DOCFIN_BKGNO FROM [T_EXL_CSWORKSTATUS] WHERE [T_EXL_CSWORKSTATUS].DOCFIN_BKGNO = '" & Trim(e.Row.Cells(26).Text) & "' "


            'ＳＱＬコマンド作成
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行
            dataread = dbcmd.ExecuteReader()

            strbkg = ""
            '結果を取り出す
            While (dataread.Read())
                strbkg += dataread("DOCFIN_BKGNO")

                '書類作成状況
                If Trim(e.Row.Cells(26).Text) = strbkg Then

                    e.Row.BackColor = Drawing.Color.DarkSalmon
                    e.Row.Cells(1).Text = e.Row.Cells(1).Text & " " & "書類済"

                End If

            End While

            'クローズ処理
            dataread.Close()
            dbcmd.Dispose()



            strSQL = "SELECT DECFIN_BKGNO FROM [T_EXL_CSWORKSTATUS] WHERE [T_EXL_CSWORKSTATUS].DECFIN_BKGNO = '" & Trim(e.Row.Cells(26).Text) & "' "


            'ＳＱＬコマンド作成
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行
            dataread = dbcmd.ExecuteReader()

            strbkg = ""
            '結果を取り出す
            While (dataread.Read())
                strbkg += dataread("DECFIN_BKGNO")

                '書類作成状況
                If Trim(e.Row.Cells(26).Text) = strbkg Then

                    e.Row.BackColor = Drawing.Color.DarkGray
                    e.Row.Cells(1).Text = "通関済"

                End If

            End While

            'クローズ処理
            dataread.Close()
            dbcmd.Dispose()


            If e.Row.Cells(12).Text = "LCL" Then

                e.Row.BackColor = Drawing.Color.DarkGray
                e.Row.Cells(13).Text = "LCL"

            End If



            strSQL = "SELECT ITK_BKGNO FROM [T_EXL_CSWORKSTATUS] WHERE [T_EXL_CSWORKSTATUS].ITK_BKGNO = '" & Trim(e.Row.Cells(26).Text) & "' "


            'ＳＱＬコマンド作成
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行
            dataread = dbcmd.ExecuteReader()

            strbkg = ""
            '結果を取り出す
            While (dataread.Read())
                strbkg += dataread("ITK_BKGNO")

                '書類作成状況
                If Trim(e.Row.Cells(26).Text) = strbkg Then

                    e.Row.BackColor = Drawing.Color.DarkGray
                    e.Row.Cells(1).Text = "通関委託"

                    'Call itaku(e.Row.Cells(25).Text)


                End If

            End While

            'クローズ処理
            dataread.Close()
            dbcmd.Dispose()

            cnn.Close()
            cnn.Dispose()



            If e.Row.Cells(1).Text = "当日必須" Then

                e.Row.BackColor = Drawing.Color.LightGreen


            End If


            If e.Row.Cells(1).Text = "EXDCUT" Then


                e.Row.BackColor = Drawing.Color.LightBlue

            End If


            If e.Row.Cells(36).Text = "1" Then


                e.Row.Cells(0).BackColor = Drawing.Color.YellowGreen

            End If



        End If

        '不要行非表示


        e.Row.Cells(10).Visible = False
        e.Row.Cells(11).Visible = False
        e.Row.Cells(12).Visible = False



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
        e.Row.Cells(24).Visible = False

        e.Row.Cells(27).Visible = False
        e.Row.Cells(30).Visible = False
        e.Row.Cells(32).Visible = False


        e.Row.Cells(34).Visible = False
        e.Row.Cells(35).Visible = False
        e.Row.Cells(36).Visible = False
        e.Row.Cells(37).Visible = False

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


    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click


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

        If Panel2.Visible = False Then



            For I = 0 To GridView1.Rows.Count - 1
                If CType(GridView1.Rows(I).Cells(0).Controls(1), CheckBox).Checked Then


                    If GridView1.Rows(I).Cells(26).Text = "&nbsp;" Or GridView1.Rows(I).Cells(6).Text = "&nbsp;" Then

                        MsgBox("未確定のためフォルダを作成できません。" & vbCrLf & "客先:" & GridView1.Rows(I).Cells(4).Text & vbCrLf & "ETD:" & vbCrLf & GridView1.Rows(I).Cells(8).Text)

                    Else



                        'FIN_FLGを更新
                        strSQL = ""
                        strSQL = strSQL & "UPDATE T_EXL_CSANKEN SET FLG03 ='1' "
                        strSQL = strSQL & "WHERE BOOKING_NO = '" & GridView1.Rows(I).Cells(26).Text & "'"

                        Command.CommandText = strSQL
                        ' SQLの実行
                        Command.ExecuteNonQuery()

                        '            Response.Redirect("anken_booking02.aspx")


                    End If

                Else



                End If
            Next


            GridView1.DataBind()

        Else

            For I = 0 To GridView3.Rows.Count - 1
            If CType(GridView3.Rows(I).Cells(0).Controls(1), CheckBox).Checked Then


                If GridView3.Rows(I).Cells(26).Text = "&nbsp;" Or GridView3.Rows(I).Cells(6).Text = "&nbsp;" Then

                    MsgBox("未確定のためフォルダを作成できません。" & vbCrLf & "客先:" & GridView3.Rows(I).Cells(4).Text & vbCrLf & "ETD:" & vbCrLf & GridView3.Rows(I).Cells(8).Text)

                Else



                    'FIN_FLGを更新
                    strSQL = ""
                    strSQL = strSQL & "UPDATE T_EXL_CSANKEN SET FLG03 ='1' "
                    strSQL = strSQL & "WHERE BOOKING_NO = '" & GridView3.Rows(I).Cells(26).Text & "'"

                    Command.CommandText = strSQL
                    ' SQLの実行
                    Command.ExecuteNonQuery()

                    '            Response.Redirect("anken_booking02.aspx")


                End If

            Else



            End If
        Next


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


        End If


    End Sub

    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load

        'Panel1.Visible = True
        'Panel2.Visible = False


    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        'Dim p As New System.Diagnostics.Process
        'p.StartInfo.FileName = “C:\Users\T43529\OneDrive - 株式会社エクセディ\デスクトップ\新ツール\通関フォルダ作成_委託メール作成.xls”
        'p.Start()


        Response.Redirect("通関フォルダ作成_委託メール作成.xls")

    End Sub
End Class
