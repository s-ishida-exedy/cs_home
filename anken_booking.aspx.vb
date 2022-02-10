
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.Common
Imports System.Console
Imports ClosedXML.Excel
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

        DropDownList1.Items.Clear()

        DropDownList1.Items.Insert(0, "--Select--")
        DropDownList1.Items.Insert(1, "進捗状況")
        DropDownList1.Items.Insert(2, "シート")
        DropDownList1.Items.Insert(3, "海貨業者")
        DropDownList1.Items.Insert(4, "客先コード")

        DropDownList2.Items.Clear()
        DropDownList2.Items.Insert(0, "--Select--")

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

        Dim Kaika00 As String

        Dim deccnt As Long
        Dim lng14 As Long
        Dim lng15 As Long

        Dim WDAYNO00 As String
        Dim WDAY00 As String
        Dim WDAY01 As String

        Dim strSQL As String
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand


        Dim dt1 As DateTime = DateTime.Now

        'If IsPostBack = True Then


        'Else


        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
            'SQL Server認証
            ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
            'SqlConnectionクラスの新しいインスタンスを初期化
            Dim cnn = New SqlConnection(ConnectionString)

            'データベース接続を開く
            cnn.Open()

            '対象の日付以下の日付の最大値を取得

            strSQL = ""
            strSQL = "SELECT T_EXL_CSWORKDAY.WORKDAY_NO FROM T_EXL_CSWORKDAY WHERE T_EXL_CSWORKDAY.WORKDAY <= '" & dt1.ToShortDateString & "' "

            'ＳＱＬコマンド作成 
            dbcmd = New SqlCommand(strSQL, cnn)
                'ＳＱＬ文実行 
                dataread = dbcmd.ExecuteReader()


            '結果を取り出す 
            While (dataread.Read())

                WDAYNO00 = dataread("WORKDAY_NO")

            End While


            'クローズ処理 
            dataread.Close()
            dbcmd.Dispose()


            strSQL = ""
            strSQL = "SELECT T_EXL_CSWORKDAY.WORKDAY FROM T_EXL_CSWORKDAY WHERE T_EXL_CSWORKDAY.WORKDAY_NO = '" & Val(WDAYNO00) + 1 & "' "

            'ＳＱＬコマンド作成 
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行 
            dataread = dbcmd.ExecuteReader()


            '結果を取り出す 
            While (dataread.Read())

                WDAY00 = dataread("WORKDAY")

            End While


            'クローズ処理 
            dataread.Close()
            dbcmd.Dispose()


            strSQL = ""
            strSQL = "SELECT T_EXL_CSWORKDAY.WORKDAY FROM T_EXL_CSWORKDAY WHERE T_EXL_CSWORKDAY.WORKDAY_NO = '" & Val(WDAYNO00) + 2 & "' "

            'ＳＱＬコマンド作成 
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行 
            dataread = dbcmd.ExecuteReader()


            '結果を取り出す 
            While (dataread.Read())

                WDAY01 = dataread("WORKDAY")

            End While


            'クローズ処理 
            dataread.Close()
            dbcmd.Dispose()



            cnn.Close()
            cnn.Dispose()






            For I = 0 To GridView1.Rows.Count - 1

                deccnt = 0
                DEC_GET(GridView1.Rows(I).Cells(26).Text, deccnt)




                If Left(GridView1.Rows(I).Cells(2).Text, 2) = "上野" Then

                    If GridView1.Rows(I).Cells(7).Text <= WDAY00 Then

                        If deccnt > 0 Then

                            lng14 = lng14 + 1

                        Else


                        End If




                    ElseIf GridView1.Rows(I).Cells(7).Text <= WDAY00 And GridView1.Rows(I).Cells(7).Text > WDAY00 Then   ' 1稼働日後がEXDCUT


                        If deccnt > 0 Then

                            lng15 = lng15 + 1

                        Else

                        End If

                    End If

                Else

                    If GridView1.Rows(I).Cells(7).Text <= dt1.ToShortDateString Then


                        If deccnt > 0 Then

                            lng14 = lng14 + 1

                        Else

                        End If


                    ElseIf GridView1.Rows(I).Cells(7).Text <= WDAY00 And GridView1.Rows(I).Cells(7).Text > dt1.ToShortDateString Then ' 1稼働日後がEXDCUT

                        If deccnt > 0 Then

                            lng15 = lng15 + 1

                        Else

                        End If


                    End If

                End If


            Next



        'End If


    End Sub

    Private Function DEC_GET(STRBOOKING_NO As String, ByRef intCnt As Integer)

        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=KBHWPM02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand
        Dim strSQL As String = ""
        Dim ivno As String = ""
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand

        Dim dataread2 As SqlDataReader
        Dim dbcmd2 As SqlCommand



        Dim dt1 As DateTime = DateTime.Now

        Dim ts1 As New TimeSpan(100, 0, 0, 0)
        Dim ts2 As New TimeSpan(100, 0, 0, 0)
        Dim dt2 As DateTime = dt1 + ts1
        Dim dt3 As DateTime = dt1 - ts1

        'データベース接続を開く
        cnn.Open()

        strSQL = ""
        strSQL = strSQL & "SELECT COUNT(*) AS RecCnt FROM T_EXL_CSWORKSTATUS WHERE "
        strSQL = strSQL & "DECFIN_BKGNO = '" & STRBOOKING_NO & "' "
        strSQL = strSQL & "AND DECFIN_REGDATE between '" & dt3 & "' AND '" & dt2 & "' "


        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        While (dataread.Read())

            intCnt = dataread("RecCnt")

        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()

        cnn.Close()
        cnn.Dispose()

    End Function


    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        'Dim p As New System.Diagnostics.Process
        'p.StartInfo.FileName = “C:\Users\T43529\OneDrive - 株式会社エクセディ\デスクトップ\新ツール\通関フォルダ作成_委託メール作成.xls”
        'p.Start()

        Response.Redirect("通関フォルダ作成_委託メール作成.xls")



        'Dim dt As New DataTable("GridView_Data")
        'For Each cell As TableCell In GridView1.HeaderRow.Cells
        '    dt.Columns.Add(cell.Text)
        'Next
        'For Each row As GridViewRow In GridView1.Rows
        '    dt.Rows.Add()
        '    For i As Integer = 0 To row.Cells.Count - 1
        '        dt.Rows(dt.Rows.Count - 1)(i) = row.Cells(i).Text
        '    Next
        'Next
        'Using wb As New XLWorkbook()
        '    wb.Worksheets.Add(dt)
        '    wb.SaveAs("C:\Users\T43529\OneDrive - 株式会社エクセディ\デスクトップ\新ツール\aaa.xlsx")

        'End Using

    End Sub


End Class
