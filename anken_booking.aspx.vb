
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.Common
Imports System.Console
Imports ClosedXML.Excel
Imports MailKit.Net.Smtp
Imports MailKit.Security
Imports MimeKit
Imports MimeKit.Text
Imports System.IO
Partial Class yuusen
    Inherits System.Web.UI.Page

    Public strRow As String
    Public strProcess As String

    Private Sub GridView1_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound

        '最終更新年月日取得
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strinv As String = ""
        Dim strbkg As String = ""


        Dim wday As String = ""
        Dim wday2 As String = ""
        Dim wday3 As String = ""
        Dim dt1 As DateTime = DateTime.Now
        Dim Kaika00 As String = ""

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

            'strSQL = "SELECT DOCFIN_BKGNO FROM [T_EXL_CSWORKSTATUS] WHERE [T_EXL_CSWORKSTATUS].DOCFIN_BKGNO = '" & Trim(e.Row.Cells(26).Text) & "' "
            strSQL = "SELECT BKGNO FROM [T_EXL_WORKSTATUS00] WHERE [T_EXL_WORKSTATUS00].BKGNO = '" & Trim(e.Row.Cells(26).Text) & "' "
            strSQL = strSQL & "AND [T_EXL_WORKSTATUS00].ID = '002' "

            'ＳＱＬコマンド作成
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行
            dataread = dbcmd.ExecuteReader()
            strbkg = ""
            '結果を取り出す
            While (dataread.Read())
                strbkg += dataread("BKGNO")
                '書類作成状況
                If Trim(e.Row.Cells(26).Text) = strbkg Then
                    e.Row.BackColor = Drawing.Color.Khaki
                    e.Row.Cells(1).Text = e.Row.Cells(1).Text & " " & "書類済"
                End If
            End While

            'クローズ処理
            dataread.Close()
            dbcmd.Dispose()

            'strSQL = "SELECT DECFIN_BKGNO FROM [T_EXL_CSWORKSTATUS] WHERE [T_EXL_CSWORKSTATUS].DECFIN_BKGNO = '" & Trim(e.Row.Cells(26).Text) & "' "
            strSQL = "SELECT BKGNO FROM [T_EXL_WORKSTATUS00] WHERE [T_EXL_WORKSTATUS00].BKGNO = '" & Trim(e.Row.Cells(26).Text) & "' "
            strSQL = strSQL & "AND [T_EXL_WORKSTATUS00].ID = '003' "

            'ＳＱＬコマンド作成
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行
            dataread = dbcmd.ExecuteReader()

            strbkg = ""
            '結果を取り出す
            While (dataread.Read())
                strbkg += dataread("BKGNO")
                '書類作成状況
                If Trim(e.Row.Cells(26).Text) = strbkg Then
                    e.Row.BackColor = Drawing.Color.DarkGray
                    e.Row.Cells(1).Text = "通関済"
                End If
            End While

            'クローズ処理
            dataread.Close()
            dbcmd.Dispose()



            'strSQL = "SELECT ITK_BKGNO FROM [T_EXL_CSWORKSTATUS] WHERE [T_EXL_CSWORKSTATUS].ITK_BKGNO = '" & Trim(e.Row.Cells(26).Text) & "' "
            strSQL = "SELECT BKGNO FROM [T_EXL_WORKSTATUS00] WHERE [T_EXL_WORKSTATUS00].BKGNO = '" & Trim(e.Row.Cells(26).Text) & "' "
            strSQL = strSQL & "AND [T_EXL_WORKSTATUS00].ID = '001' "

            'ＳＱＬコマンド作成
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行
            dataread = dbcmd.ExecuteReader()
            strbkg = ""
            '結果を取り出す
            While (dataread.Read())
                strbkg += dataread("BKGNO")
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

            If e.Row.Cells(12).Text = "LCL" Then
                e.Row.BackColor = Drawing.Color.DarkGray
                e.Row.Cells(13).Text = "LCL"
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
        Dim strSQL As String = ""
        Dim strinv As String = ""
        Dim strbkg As String = ""

        Dim wday As String = ""
        Dim wday2 As String = ""
        Dim wday3 As String = ""
        Dim dt1 As DateTime = DateTime.Now
        Dim Kaika00 As String = ""

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

            'strSQL = "SELECT DOCFIN_BKGNO FROM [T_EXL_CSWORKSTATUS] WHERE [T_EXL_CSWORKSTATUS].DOCFIN_BKGNO = '" & Trim(e.Row.Cells(26).Text) & "' "

            strSQL = "SELECT BKGNO FROM [T_EXL_WORKSTATUS00] WHERE [T_EXL_WORKSTATUS00].BKGNO = '" & Trim(e.Row.Cells(26).Text) & "' "
            strSQL = strSQL & "AND [T_EXL_WORKSTATUS00].ID = '002' "

            'ＳＱＬコマンド作成
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行
            dataread = dbcmd.ExecuteReader()
            strbkg = ""
            '結果を取り出す
            While (dataread.Read())
                strbkg += dataread("BKGNO")
                '書類作成状況
                If Trim(e.Row.Cells(26).Text) = strbkg Then
                    e.Row.BackColor = Drawing.Color.DarkSalmon
                    e.Row.Cells(1).Text = e.Row.Cells(1).Text & " " & "書類済"
                End If
            End While

            'クローズ処理
            dataread.Close()
            dbcmd.Dispose()

            'strSQL = "SELECT DECFIN_BKGNO FROM [T_EXL_CSWORKSTATUS] WHERE [T_EXL_CSWORKSTATUS].DECFIN_BKGNO = '" & Trim(e.Row.Cells(26).Text) & "' "
            strSQL = "SELECT BKGNO FROM [T_EXL_WORKSTATUS00] WHERE [T_EXL_WORKSTATUS00].BKGNO = '" & Trim(e.Row.Cells(26).Text) & "' "
            strSQL = strSQL & "AND [T_EXL_WORKSTATUS00].ID = '003' "

            'ＳＱＬコマンド作成
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行
            dataread = dbcmd.ExecuteReader()
            strbkg = ""
            '結果を取り出す
            While (dataread.Read())
                strbkg += dataread("BKGNO")
                '書類作成状況
                If Trim(e.Row.Cells(26).Text) = strbkg Then
                    e.Row.BackColor = Drawing.Color.DarkGray
                    e.Row.Cells(1).Text = "通関済"
                End If
            End While

            'クローズ処理
            dataread.Close()
            dbcmd.Dispose()



            'strSQL = "SELECT ITK_BKGNO FROM [T_EXL_CSWORKSTATUS] WHERE [T_EXL_CSWORKSTATUS].ITK_BKGNO = '" & Trim(e.Row.Cells(26).Text) & "' "
            strSQL = "SELECT BKGNO FROM [T_EXL_WORKSTATUS00] WHERE [T_EXL_WORKSTATUS00].BKGNO = '" & Trim(e.Row.Cells(26).Text) & "' "
            strSQL = strSQL & "AND [T_EXL_WORKSTATUS00].ID = '001' "

            'ＳＱＬコマンド作成
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行
            dataread = dbcmd.ExecuteReader()

            strbkg = ""
            '結果を取り出す
            While (dataread.Read())
                strbkg += dataread("BKGNO")
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

            If e.Row.Cells(12).Text = "LCL" Then
                e.Row.BackColor = Drawing.Color.DarkGray
                e.Row.Cells(13).Text = "LCL"
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

            DropDownList2.Items.Clear()
            DropDownList2.DataSource = SqlDataSource2
            DropDownList2.DataTextField = "STATUS"
            DropDownList2.DataValueField = "STATUS"
            DropDownList2.DataBind()

            DropDownList2.Items.Insert(0, "--Select--")

        ElseIf DropDownList1.Text = "シート" Then
            GridView3.DataSource = SqlDataSource5
            GridView3.DataBind()

            DropDownList2.Items.Clear()
            DropDownList2.DataSource = SqlDataSource3
            DropDownList2.DataTextField = "FORWARDER"
            DropDownList2.DataValueField = "FORWARDER"
            DropDownList2.DataBind()

            DropDownList2.Items.Insert(0, "--Select--")

        ElseIf DropDownList1.Text = "海貨業者" Then
            GridView3.DataSource = SqlDataSource6
            GridView3.DataBind()

            DropDownList2.Items.Clear()
            DropDownList2.DataSource = SqlDataSource4
            DropDownList2.DataTextField = "FORWARDER02"
            DropDownList2.DataValueField = "FORWARDER02"
            DropDownList2.DataBind()

            DropDownList2.Items.Insert(0, "--Select--")

        ElseIf DropDownList1.Text = "客先コード" Then
            GridView3.DataSource = SqlDataSource9
            GridView3.DataBind()

            DropDownList2.Items.Clear()
            DropDownList2.DataSource = SqlDataSource8
            DropDownList2.DataTextField = "CUST"
            DropDownList2.DataValueField = "CUST"
            DropDownList2.DataBind()

            DropDownList2.Items.Insert(0, "--Select--")

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

        Dim Kaika00 As String = ""
        Dim deccnt As Long
        Dim lng14 As Long
        Dim lng15 As Long
        Dim WDAYNO00 As String = ""
        Dim WDAY00 As String = ""
        Dim WDAY01 As String = ""
        Dim strSQL As String = ""
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
            deccnt = DEC_GET(GridView1.Rows(I).Cells(26).Text)

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

    End Sub

    Private Function DEC_GET(STRBOOKING_NO As String) As Integer

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

        DEC_GET = 0

        Dim dt1 As DateTime = DateTime.Now

        Dim ts1 As New TimeSpan(100, 0, 0, 0)
        Dim ts2 As New TimeSpan(100, 0, 0, 0)
        Dim dt2 As DateTime = dt1 + ts1
        Dim dt3 As DateTime = dt1 - ts1

        'データベース接続を開く
        cnn.Open()

        strSQL = ""
        strSQL = strSQL & "SELECT COUNT(*) AS RecCnt FROM T_EXL_WORKSTATUS00 WHERE "
        strSQL = strSQL & "T_EXL_WORKSTATUS00.ID = '003' "
        strSQL = strSQL & "AND T_EXL_WORKSTATUS00.BKGNO = '" & STRBOOKING_NO & "' "
        strSQL = strSQL & "AND T_EXL_WORKSTATUS00.REGDATE  between '" & dt3 & "' AND '" & dt2 & "' "


        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        While (dataread.Read())
            DEC_GET = dataread("RecCnt")
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()

        cnn.Close()
        cnn.Dispose()

    End Function
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        Dim strPath00(3) As String      '依頼書、タイムスケジュールのパスと作成先のパス
        Dim strPath01(3) As String      '

        Dim MyStr As String = ""
        Dim strLog As String            '問題報告ログ
        Dim strFile0 As String = ""         'ファイル名(依頼書)
        Dim strFile1 As String = ""         'ファイル名(タイムスケジュール)
        Dim strFol As String = ""         'フォルダ名
        Dim iptbx As String = ""         'フォルダ名
        Dim strfol001 As String = ""
        Dim CODE1 As String = ""
        Dim CODE2 As String = ""
        Dim myPath As String = ""
        Dim F_dir As String = ""
        Dim itkflg As String = ""
        Dim Ccode As String = ""
        Dim Cname As String = ""
        Dim strirai As String = ""
        Dim hensyuuiraisyo As String = ""
        Dim madef00 As String = ""
        Dim madef01 As String = "処理履歴："


        Dim I As Integer

        If Panel2.Visible = False Then
            For I = 0 To GridView1.Rows.Count - 1
                If CType(GridView1.Rows(I).Cells(0).Controls(1), CheckBox).Checked Then
                    If GridView1.Rows(I).Cells(26).Text = "&nbsp;" Or GridView1.Rows(I).Cells(6).Text = "&nbsp;" Then
                        MsgBox("未確定のためフォルダを作成できません。" & vbCrLf & "客先:" & GridView1.Rows(I).Cells(4).Text & vbCrLf & "ETD:" & vbCrLf & GridView1.Rows(I).Cells(8).Text)
                    Else

                        strPath00(0) = "\\svnas201\exd06100\COMMON\生産管理本部\ＣＳチーム\案件抽出\a)自社通関依頼書（客先別）WEB\"
                        strPath00(1) = "\\svnas201\exd06100\COMMON\生産管理本部\ＣＳチーム\案件抽出\b)タイムスケジュール（客先別）\"
                        strPath01(0) = "\\svnas201\exd06100\COMMON\生産管理本部\ＣＳチーム\案件抽出\WEB_test\"
                        'strPath01(0) = "\\svnas201\exd06100\COMMON\生産管理本部\ＣＳチーム\案件抽出\"
                        strPath01(1) = "\\svnas201\EXD06101\DISC_COMMON\自社通関輸出書類\"

                        '問題報告ログ初期化
                        strLog = ""

                        'ファイル検索
                        strFile0 = Dir(strPath00(0) & "自社通関依頼書　EXEDY *(" & Replace(GridView1.Rows(I).Cells(4).Text, "/", "-") & ")*.xlsx", vbNormal)
                        If strFile0 = "" Then
                            strLog = strLog & Right("0000" & I, 5) & ",原紙なし" & Chr(10)
                            madef00 = 0
                            GoTo Step00
                        End If

                        '委託検索
                        itkflg = ""
                        itkflg = get_itakuhanntei(GridView1.Rows(I).Cells(6).Text)

                        If itkflg = "1" Then
                            'Page.ClientScript.RegisterStartupScript(Me.GetType, "確認", "<script language='JavaScript'>confirm('委託：客先：" & Replace(GridView1.Rows(I).Cells(4).Text, " / ", " - ") & "、IV-" & Replace(GridView1.Rows(I).Cells(6).Text, "/", "-") & "');</script>", False)
                            madef00 = 1
                            GoTo Step00
                        End If

                        '1_________________________________________________

                        strfol001 = Dir(strPath01(0) & "*(" & Replace(GridView1.Rows(I).Cells(4).Text, "/", "-") & ")*" & Replace(GridView1.Rows(I).Cells(6).Text, "/", "-"), vbDirectory)

                        'If strfol001 <> "" Then
                        '    Page.ClientScript.RegisterStartupScript(Me.GetType, "確認", "<script language='JavaScript'>confirm('フォルダ作成済み：客先：" & Replace(GridView1.Rows(I).Cells(4).Text, " / ", " - ") & "、IV-" & Replace(GridView1.Rows(I).Cells(6).Text, "/", "-") & "');</script>", False)
                        'madef00 = 2
                        '    GoTo Step00

                        'End If

                        'strfol001 = Dir(strPath01(1) & "*(" & Replace(GridView1.Rows(I).Cells(4).Text, "/", "-") & ")*" & Replace(GridView1.Rows(I).Cells(5).Text, "/", "-") & "*", vbDirectory)


                        'If strfol001 <> "" Then
                        '    Page.ClientScript.RegisterStartupScript(Me.GetType, "確認", "<script language='JavaScript'>confirm('フォルダ作成済み：客先：" & Replace(GridView1.Rows(I).Cells(4).Text, " / ", " - ") & "、IV-" & Replace(GridView1.Rows(I).Cells(6).Text, "/", "-") & "');</script>", False)
                        'madef00 = 2
                        '    GoTo Step00

                        'End If


                        'strfol001 = Dir(strPath01(1) & Format(DateAdd("m", -1, Now()), "yyyyMM") & "\" & "*(" & Replace(GridView1.Rows(I).Cells(4).Text, "/", "-") & ")*" & Replace(GridView1.Rows(I).Cells(5).Text, "/", "-") & "*", vbDirectory)


                        'If strfol001 <> "" Then
                        '    Page.ClientScript.RegisterStartupScript(Me.GetType, "確認", "<script language='JavaScript'>confirm('フォルダ作成済み：客先：" & Replace(GridView1.Rows(I).Cells(4).Text, " / ", " - ") & "、IV-" & Replace(GridView1.Rows(I).Cells(6).Text, "/", "-") & "');</script>", False)
                        'madef00 = 2
                        '    GoTo Step00

                        'End If


                        'strfol001 = Dir(strPath01(1) & Format(DateAdd("m", 0, Now()), "yyyyMM") & "\" & "*(" & Replace(GridView1.Rows(I).Cells(4).Text, "/", "-") & ")*" & Replace(GridView1.Rows(I).Cells(5).Text, "/", "-") & "*", vbDirectory)


                        'If strfol001 <> "" Then
                        '    Page.ClientScript.RegisterStartupScript(Me.GetType, "確認", "<script language='JavaScript'>confirm('フォルダ作成済み：客先：" & Replace(GridView1.Rows(I).Cells(4).Text, " / ", " - ") & "、IV-" & Replace(GridView1.Rows(I).Cells(6).Text, "/", "-") & "');</script>", False)
                        'madef00 = 2
                        '    GoTo Step00

                        'End If

                        'strfol001 = Dir(strPath01(1) & Format(DateAdd("m", 1, Now()), "yyyyMM") & "\" & "*(" & Replace(GridView1.Rows(I).Cells(4).Text, "/", "-") & ")*" & Replace(GridView1.Rows(I).Cells(5).Text, "/", "-") & "*", vbDirectory)


                        'If strfol001 <> "" Then
                        '    Page.ClientScript.RegisterStartupScript(Me.GetType, "確認", "<script language='JavaScript'>confirm('フォルダ作成済み：客先：" & Replace(GridView1.Rows(I).Cells(4).Text, " / ", " - ") & "、IV-" & Replace(GridView1.Rows(I).Cells(6).Text, "/", "-") & "');</script>", False)
                        'madef00 = 2
                        '    GoTo Step00

                        'End If

                        '2_________________________________________________

                        'フォルダ作成(既にあればスキップ)
                        '検索したファイル名から作成
                        strFol = Replace(strFile0, "自社通関依頼書　EXEDY ", "-")
                        strFol = Replace(strFol, "IV-0000", "IV-" & GridView1.Rows(I).Cells(6).Text)
                        strFol = Replace(strFol, "/", "-")

                        'ここを帰るとフォルダ作成先が変わる
                        Dim dt1 As DateTime = DateTime.Parse(GridView1.Rows(I).Cells(7).Text)
                            strFol = strPath01(0) & Format(dt1, "yyMMdd") & strFol ' Wb0.Path & "\" & Format(Ws0.Cells(i, 1), "yymmdd") & strFol

                        strFol = Left(strFol, Len(strFol) - 4)
                        MyStr = Dir(strFol, vbDirectory)
                        If MyStr <> "" Then
                            strLog = strLog & Right("0000" & I, 5) & ",同一フォルダ有り" & Chr(10)
                            madef00 = 2
                            GoTo Step00
                        End If

                        'MkDir strFol                                                                                   '格納先
                        My.Computer.FileSystem.CreateDirectory(strFol)


                        '3_________________________________________________



                        '追加 住所ファイル

                        myPath = "\\svnas201\exd06100\COMMON\生産管理本部\ＣＳチーム\案件抽出\q_住所" '--- フォルダを作成した場所のパス

                        iptbx = Left(Replace(GridView1.Rows(I).Cells(6).Text, "IV-", ""), 4)
                        Call copy_custfile(iptbx, Cname, Ccode)

                        F_dir = Dir(myPath & "\" & Ccode & "*", vbDirectory)

                        If F_dir <> "" Then
                            '処理
                            'objFSO.CopyFile myPath & "\" & F_dir, strFol & "\" & F_dir, True 
                            System.IO.File.Copy(myPath & "\" & F_dir, strFol & "\" & F_dir)
                        Else

                            F_dir = Dir(myPath & "\*" & Cname & "*", vbDirectory)

                            If F_dir <> "" Then
                                '処理
                                'objFSO.CopyFile myPath & "\" & F_dir, strFol & "\" & F_dir, True
                                System.IO.File.Copy(myPath & "\" & F_dir, strFol & "\" & F_dir)
                            Else
                            End If
                        End If


                        '4_________________________________________________


                        strirai = Dir(strPath00(0) & "*自社通関依頼書*" & Ccode & "*xlsx")
                        MyStr = Replace(strFile0, "IV-0000", "IV-" & GridView1.Rows(I).Cells(6).Text)

                        System.IO.File.Copy(strPath00(0) & strirai, strFol & "\" & MyStr)

                        hensyuuiraisyo = strFol & "\" & MyStr

                        Dim workbook = New XLWorkbook(hensyuuiraisyo)
                        Dim ws1 As IXLWorksheet = workbook.Worksheet(1)


                        '転記
                        ws1.Cell(4, 1).Value = GridView1.Rows(I).Cells(7).Text   '通関予定日
                        '        ws2.Range("B1") = Ws0.Cells(i, 2)   '通関予定日

                        ws1.Cell(11, 6).Value = GridView1.Rows(I).Cells(7).Text  'カット日 
                        '        ws2.Range("B20") = Ws0.Cells(i, 2)   '通関予定日

                        ws1.Cell(11, 7).Value = GridView1.Rows(I).Cells(8).Text  'POSITION(ETD)
                        '        ws2.Range("B7") = Ws0.Cells(i, 9)   'POSITION(ETD)
                        '        ws2.Range("B7").NumberFormatLocal = "yyyy/m/d"

                        ws1.Cell(11, 8).Value = GridView1.Rows(I).Cells(9).Text 'ETA 
                        '        ws2.Range("B21") = Ws0.Cells(i, 10)  'ETA

                        ws1.Cell(14, 2).Value = "'" & GridView1.Rows(I).Cells(26).Text  'BOOKING NO.
                        '        ws2.Range("B8") = "'" & Ws0.Cells(i, 13)  'BOOKING NO.

                        ws1.Cell(11, 9).Value = GridView1.Rows(I).Cells(27).Text  'CARRIER(船社)
                        '        ws2.Range("B22") = Ws0.Cells(i, 14)  'CARRIER(船社)

                        ws1.Cell(11, 1).Value = GridView1.Rows(I).Cells(28).Text & " ()" 'VESSEL(船舶コード） '船舶コード課題
                        '        ws2.Range("B5") = Ws0.Cells(i, 11) 'VESSEL(船舶コード） '船舶コード課題

                        ws1.Cell(11, 4).Value = GridView1.Rows(I).Cells(29).Text  'VOY.NO.(航海番号)
                        '        ws2.Range("B6") = Ws0.Cells(i, 12)  'VOY.NO.(航海番号)

                        'MyStr = "確認要"           'REF番号
                        'MyStr = Left(MyStr, InStr(1, MyStr, "-"))
                        'ws1.Cell(4, 5).Value = MyStr
                        '        ws2.Range("C1") = MyStr

                        Dim MyStr2 As String = "---" 'REF番号
                        ws1.Cell(4, 6).Value = MyStr
                        '        ws2.Range("D1") = MyStr

                        '港チェック(現段階では相違があれば、色を付ける→後々は訂正をする方向で)
                        Dim erflg As Long = 0
                        'PLACE OF RECEIPT(荷受地)
                        If ws1.Cell(16, 1).Value <> GridView1.Rows(I).Cells(30).Text Then

                            ws1.Cell(16, 1).Style.Fill.BackgroundColor = XLColor.Red
                            erflg = 1
                        End If
                        'PORT OF LOADING(積出港)
                        If ws1.Cell(16, 3).Value <> GridView1.Rows(I).Cells(31).Text Then
                            ws1.Cell(16, 3).Style.Fill.BackgroundColor = XLColor.Red
                            erflg = 1
                        End If
                        'PORT OF DISCHARGE(揚地)
                        If ws1.Cell(16, 5).Value <> GridView1.Rows(I).Cells(32).Text Then
                            ws1.Cell(16, 5).Style.Fill.BackgroundColor = XLColor.Red
                            erflg = 1
                        End If
                        'PLACE OF DELIVERY(配送先)
                        If ws1.Cell(16, 7).Value <> GridView1.Rows(I).Cells(33).Text Then
                            ws1.Cell(16, 7).Style.Fill.BackgroundColor = XLColor.Red
                            erflg = 1
                        End If


                        '------------　18/04追記  港コードも色付け　--------------
                        If ws1.Cell(16, 3).Style.Fill.BackgroundColor = XLColor.Red Then
                            ws1.Cell(25, 1).Style.Fill.BackgroundColor = XLColor.Red
                            erflg = 1
                        End If

                        If ws1.Cell(16, 5).Style.Fill.BackgroundColor = XLColor.Red Then
                            ws1.Cell(25, 6).Style.Fill.BackgroundColor = XLColor.Red
                            erflg = 1
                        End If

                        If ws1.Cell(16, 7).Style.Fill.BackgroundColor = XLColor.Red Then
                            ws1.Cell(30, 1).Style.Fill.BackgroundColor = XLColor.Red
                            erflg = 1
                        End If
                        '---------------------------------------------------------

                        '------------　21/03追記  Bookingsheetからデータ取得　--------------
                        If erflg = 1 Then

                            Dim niuke As String = ""
                            Dim tsumi As String = ""
                            Dim ageti As String = ""
                            Dim haisou As String = ""

                            Call get_bookingdata(I, niuke, tsumi, ageti, haisou, GridView1.Rows(I).Cells(6).Text)

                            ws1.Cell(2, 12).Value = niuke
                            ws1.Cell(3, 12).Value = tsumi
                            ws1.Cell(4, 12).Value = ageti
                            ws1.Cell(5, 12).Value = haisou

                            ws1.Cell(2, 11).Value = "荷受地"
                            ws1.Cell(3, 11).Value = "積出港"
                            ws1.Cell(4, 11).Value = "揚げ港"
                            ws1.Cell(5, 11).Value = "配送先"

                            'Call Minatocode01(ageti, CODE1)
                            'Call Minatocode02(haisou, CODE2)

                            'ws1.Cell(4, 13).Value = CODE1
                            'ws1.Cell(5, 13).Value = CODE2
                            'ws1.Cell(1, 13).Value = "過去実績"

                        End If

                        workbook.SaveAs(hensyuuiraisyo)

                        If erflg = 1 Then
                            My.Computer.FileSystem.RenameFile(hensyuuiraisyo, "E_" & MyStr)
                        End If

Step00:

                        If madef00 = "" Then
                            madef01 = madef01 & "\n" & "作成済み　　　　 客先：" & GridView1.Rows(I).Cells(4).Text & " IVNO：" & GridView1.Rows(I).Cells(6).Text
                        ElseIf madef00 = "0" Then
                            madef01 = madef01 & "\n" & "依頼書なし　　　 客先：" & GridView1.Rows(I).Cells(4).Text & " IVNO：" & GridView1.Rows(I).Cells(6).Text
                        ElseIf madef00 = "1" Then
                            madef01 = madef01 & "\n" & "委託案件　　　　 客先：" & GridView1.Rows(I).Cells(4).Text & " IVNO：" & GridView1.Rows(I).Cells(6).Text
                        ElseIf madef00 = "2" Then
                            madef01 = madef01 & "\n" & "同一フォルダあり 客先：" & GridView1.Rows(I).Cells(4).Text & " IVNO：" & GridView1.Rows(I).Cells(6).Text
                        End If
                        madef00 = ""

                    End If
                Else
                End If
            Next

            GridView1.DataBind()
            Panel1.Visible = True
            Panel2.Visible = False
        Else
            For I = 0 To GridView3.Rows.Count - 1
                If CType(GridView3.Rows(I).Cells(0).Controls(1), CheckBox).Checked Then
                    If GridView3.Rows(I).Cells(26).Text = "&nbsp;" Or GridView3.Rows(I).Cells(6).Text = "&nbsp;" Then
                        MsgBox("未確定のためフォルダを作成できません。" & vbCrLf & "客先:" & GridView3.Rows(I).Cells(4).Text & vbCrLf & "ETD:" & vbCrLf & GridView3.Rows(I).Cells(8).Text)
                    Else

                        strPath00(0) = "\\svnas201\exd06100\COMMON\生産管理本部\ＣＳチーム\案件抽出\a)自社通関依頼書（客先別）WEB\"
                        strPath00(1) = "\\svnas201\exd06100\COMMON\生産管理本部\ＣＳチーム\案件抽出\b)タイムスケジュール（客先別）\"
                        strPath01(0) = "\\svnas201\exd06100\COMMON\生産管理本部\ＣＳチーム\案件抽出\WEB_test\"
                        'strPath01(0) = "\\svnas201\exd06100\COMMON\生産管理本部\ＣＳチーム\案件抽出\"
                        strPath01(1) = "\\svnas201\EXD06101\DISC_COMMON\自社通関輸出書類\"

                        '問題報告ログ初期化
                        strLog = ""

                        'ファイル検索
                        strFile0 = Dir(strPath00(0) & "自社通関依頼書　EXEDY *(" & Replace(GridView3.Rows(I).Cells(4).Text, "/", "-") & ")*.xlsx", vbNormal)
                        If strFile0 = "" Then
                            strLog = strLog & Right("0000" & I, 5) & ",原紙なし" & Chr(10)
                            madef00 = 0
                            GoTo Step01
                        End If

                        '委託検索
                        itkflg = ""
                        itkflg = get_itakuhanntei(GridView3.Rows(I).Cells(6).Text)

                        If itkflg = "1" Then
                            madef00 = 1
                            'Page.ClientScript.RegisterStartupScript(Me.GetType, "確認", "<script language='JavaScript'>confirm('委託：客先：" & Replace(GridView3.Rows(I).Cells(4).Text, " / ", " - ") & "、IV-" & Replace(GridView3.Rows(I).Cells(6).Text, "/", "-") & "');</script>", False)
                            GoTo Step01
                        End If

                        '1_________________________________________________

                        strfol001 = Dir(strPath01(0) & "*(" & Replace(GridView3.Rows(I).Cells(4).Text, "/", "-") & ")*" & Replace(GridView3.Rows(I).Cells(6).Text, "/", "-"), vbDirectory)

                        'If strfol001 <> "" Then
                        'madef00 = 2
                        '    Page.ClientScript.RegisterStartupScript(Me.GetType, "確認", "<script language='JavaScript'>confirm('フォルダ作成済み：客先：" & Replace(GridView3.Rows(I).Cells(4).Text, " / ", " - ") & "、IV-" & Replace(GridView3.Rows(I).Cells(6).Text, "/", "-") & "');</script>", False)
                        '    GoTo Step01

                        'End If

                        'strfol001 = Dir(strPath01(1) & "*(" & Replace(GridView1.Rows(I).Cells(4).Text, "/", "-") & ")*" & Replace(GridView3.Rows(I).Cells(5).Text, "/", "-") & "*", vbDirectory)


                        'If strfol001 <> "" Then
                        'madef00 = 2
                        '    Page.ClientScript.RegisterStartupScript(Me.GetType, "確認", "<script language='JavaScript'>confirm('フォルダ作成済み：客先：" & Replace(GridView3.Rows(I).Cells(4).Text, " / ", " - ") & "、IV-" & Replace(GridView3.Rows(I).Cells(6).Text, "/", "-") & "');</script>", False)
                        '    GoTo Step01

                        'End If


                        'strfol001 = Dir(strPath01(1) & Format(DateAdd("m", -1, Now()), "yyyyMM") & "\" & "*(" & Replace(GridView3.Rows(I).Cells(4).Text, "/", "-") & ")*" & Replace(GridView3.Rows(I).Cells(5).Text, "/", "-") & "*", vbDirectory)


                        'If strfol001 <> "" Then
                        'madef00 = 2
                        '    Page.ClientScript.RegisterStartupScript(Me.GetType, "確認", "<script language='JavaScript'>confirm('フォルダ作成済み：客先：" & Replace(GridView3.Rows(I).Cells(4).Text, " / ", " - ") & "、IV-" & Replace(GridView3.Rows(I).Cells(6).Text, "/", "-") & "');</script>", False)
                        '    GoTo Step01

                        'End If


                        'strfol001 = Dir(strPath01(1) & Format(DateAdd("m", 0, Now()), "yyyyMM") & "\" & "*(" & Replace(GridView3.Rows(I).Cells(4).Text, "/", "-") & ")*" & Replace(GridView3.Rows(I).Cells(5).Text, "/", "-") & "*", vbDirectory)


                        'If strfol001 <> "" Then
                        'madef00 = 2
                        '    Page.ClientScript.RegisterStartupScript(Me.GetType, "確認", "<script language='JavaScript'>confirm('フォルダ作成済み：客先：" & Replace(GridView3.Rows(I).Cells(4).Text, " / ", " - ") & "、IV-" & Replace(GridView3.Rows(I).Cells(6).Text, "/", "-") & ");</script>", False)
                        '    GoTo Step01

                        'End If

                        'strfol001 = Dir(strPath01(1) & Format(DateAdd("m", 1, Now()), "yyyyMM") & "\" & "*(" & Replace(GridView3.Rows(I).Cells(4).Text, "/", "-") & ")*" & Replace(GridView3.Rows(I).Cells(5).Text, "/", "-") & "*", vbDirectory)


                        'If strfol001 <> "" Then
                        'madef00 = 2
                        '    Page.ClientScript.RegisterStartupScript(Me.GetType, "確認", "<script language='JavaScript'>confirm('フォルダ作成済み：客先：" & Replace(GridView3.Rows(I).Cells(4).Text, " / ", " - ") & "、IV-" & Replace(GridView3.Rows(I).Cells(6).Text, "/", "-") & ");</script>", False)
                        '    GoTo Step01

                        'End If

                        '2_________________________________________________

                        'フォルダ作成(既にあればスキップ)
                        '検索したファイル名から作成
                        strFol = Replace(strFile0, "自社通関依頼書　EXEDY ", "-")
                        strFol = Replace(strFol, "IV-0000", "IV-" & GridView3.Rows(I).Cells(6).Text)
                        strFol = Replace(strFol, "/", "-")

                        'ここを帰るとフォルダ作成先が変わる
                        Dim dt1 As DateTime = DateTime.Parse(GridView3.Rows(I).Cells(7).Text)
                        strFol = strPath01(0) & Format(dt1, "yyMMdd") & strFol ' Wb0.Path & "\" & Format(Ws0.Cells(i, 1), "yymmdd") & strFol

                        strFol = Left(strFol, Len(strFol) - 4)
                        MyStr = Dir(strFol, vbDirectory)
                        If MyStr <> "" Then
                            strLog = strLog & Right("0000" & I, 5) & ",同一フォルダ有り" & Chr(10)
                            madef00 = 2
                            GoTo Step01
                        End If

                        'MkDir strFol                                                                                   '格納先
                        My.Computer.FileSystem.CreateDirectory(strFol)


                        '3_________________________________________________


                        '追加 住所ファイル

                        myPath = "\\svnas201\exd06100\COMMON\生産管理本部\ＣＳチーム\案件抽出\q_住所" '--- フォルダを作成した場所のパス

                        iptbx = Left(Replace(GridView3.Rows(I).Cells(6).Text, "IV-", ""), 4)
                        Call copy_custfile(iptbx, Cname, Ccode)

                        F_dir = Dir(myPath & "\" & Ccode & "*", vbDirectory)

                        If F_dir <> "" Then
                            '処理
                            'objFSO.CopyFile myPath & "\" & F_dir, strFol & "\" & F_dir, True 
                            System.IO.File.Copy(myPath & "\" & F_dir, strFol & "\" & F_dir)
                        Else

                            F_dir = Dir(myPath & "\*" & Cname & "*", vbDirectory)

                            If F_dir <> "" Then

                                '処理
                                'objFSO.CopyFile myPath & "\" & F_dir, strFol & "\" & F_dir, True 
                                System.IO.File.Copy(myPath & "\" & F_dir, strFol & "\" & F_dir)

                            Else
                            End If
                        End If


                        '4_________________________________________________

                        strirai = Dir(strPath00(0) & "*自社通関依頼書*" & Ccode & "*xlsx")
                        MyStr = Replace(strFile0, "IV-0000", "IV-" & GridView3.Rows(I).Cells(6).Text)

                        System.IO.File.Copy(strPath00(0) & strirai, strFol & "\" & MyStr)

                        hensyuuiraisyo = strFol & "\" & MyStr

                        Dim workbook = New XLWorkbook(hensyuuiraisyo)
                        Dim ws1 As IXLWorksheet = workbook.Worksheet(1)


                        '転記
                        ws1.Cell(4, 1).Value = GridView3.Rows(I).Cells(7).Text   '通関予定日
                        '        ws2.Range("B1") = Ws0.Cells(i, 2)   '通関予定日

                        ws1.Cell(11, 6).Value = GridView3.Rows(I).Cells(7).Text  'カット日
                        '        ws2.Range("B20") = Ws0.Cells(i, 2)   '通関予定日

                        ws1.Cell(11, 7).Value = GridView3.Rows(I).Cells(8).Text  'POSITION(ETD)
                        '        ws2.Range("B7") = Ws0.Cells(i, 9)   'POSITION(ETD)
                        '        ws2.Range("B7").NumberFormatLocal = "yyyy/m/d"

                        ws1.Cell(11, 8).Value = GridView3.Rows(I).Cells(9).Text 'ETA
                        '        ws2.Range("B21") = Ws0.Cells(i, 10)  'ETA

                        ws1.Cell(14, 2).Value = "'" & GridView3.Rows(I).Cells(26).Text  'BOOKING NO.
                        '        ws2.Range("B8") = "'" & Ws0.Cells(i, 13)  'BOOKING NO.

                        ws1.Cell(11, 9).Value = GridView3.Rows(I).Cells(27).Text  'CARRIER(船社)
                        '        ws2.Range("B22") = Ws0.Cells(i, 14)  'CARRIER(船社)

                        ws1.Cell(11, 1).Value = GridView3.Rows(I).Cells(28).Text & " ()" 'VESSEL(船舶コード） '船舶コード課題
                        '        ws2.Range("B5") = Ws0.Cells(i, 11) 'VESSEL(船舶コード） '船舶コード課題

                        ws1.Cell(11, 4).Value = GridView3.Rows(I).Cells(29).Text  'VOY.NO.(航海番号)
                        '        ws2.Range("B6") = Ws0.Cells(i, 12)  'VOY.NO.(航海番号)

                        'MyStr = "確認要"           'REF番号
                        'MyStr = Left(MyStr, InStr(1, MyStr, "-"))
                        'ws1.Cell(4, 5).Value = MyStr
                        '        ws2.Range("C1") = MyStr

                        Dim MyStr2 As String = "---" 'REF番号
                        ws1.Cell(4, 6).Value = MyStr
                        '        ws2.Range("D1") = MyStr

                        '港チェック(現段階では相違があれば、色を付ける→後々は訂正をする方向で)

                        Dim erflg As Long = 0
                        'PLACE OF RECEIPT(荷受地)
                        If ws1.Cell(16, 1).Value <> GridView3.Rows(I).Cells(30).Text Then

                            ws1.Cell(16, 1).Style.Fill.BackgroundColor = XLColor.Red
                            erflg = 1
                        End If
                        'PORT OF LOADING(積出港)
                        If ws1.Cell(16, 3).Value <> GridView3.Rows(I).Cells(31).Text Then
                            ws1.Cell(16, 3).Style.Fill.BackgroundColor = XLColor.Red
                            erflg = 1
                        End If
                        'PORT OF DISCHARGE(揚地)
                        If ws1.Cell(16, 5).Value <> GridView3.Rows(I).Cells(32).Text Then
                            ws1.Cell(16, 5).Style.Fill.BackgroundColor = XLColor.Red
                            erflg = 1
                        End If
                        'PLACE OF DELIVERY(配送先)
                        If ws1.Cell(16, 7).Value <> GridView3.Rows(I).Cells(33).Text Then
                            ws1.Cell(16, 7).Style.Fill.BackgroundColor = XLColor.Red
                            erflg = 1
                        End If

                        '------------　18/04追記  港コードも色付け　--------------
                        If ws1.Cell(16, 3).Style.Fill.BackgroundColor = XLColor.Red Then
                            ws1.Cell(25, 1).Style.Fill.BackgroundColor = XLColor.Red
                            erflg = 1
                        End If

                        If ws1.Cell(16, 5).Style.Fill.BackgroundColor = XLColor.Red Then
                            ws1.Cell(25, 6).Style.Fill.BackgroundColor = XLColor.Red
                            erflg = 1
                        End If

                        If ws1.Cell(16, 7).Style.Fill.BackgroundColor = XLColor.Red Then
                            ws1.Cell(30, 1).Style.Fill.BackgroundColor = XLColor.Red
                            erflg = 1
                        End If
                        '---------------------------------------------------------

                        '------------　21/03追記  Bookingsheetからデータ取得　--------------
                        If erflg = 1 Then

                            Dim niuke As String = ""
                            Dim tsumi As String = ""
                            Dim ageti As String = ""
                            Dim haisou As String = ""

                            Call get_bookingdata(I, niuke, tsumi, ageti, haisou, GridView1.Rows(I).Cells(6).Text)

                            ws1.Cell(2, 12).Value = niuke
                            ws1.Cell(3, 12).Value = tsumi
                            ws1.Cell(4, 12).Value = ageti
                            ws1.Cell(5, 12).Value = haisou

                            ws1.Cell(2, 11).Value = "荷受地"
                            ws1.Cell(3, 11).Value = "積出港"
                            ws1.Cell(4, 11).Value = "揚げ港"
                            ws1.Cell(5, 11).Value = "配送先"

                            'Call Minatocode01(ageti, CODE1)
                            'Call Minatocode02(haisou, CODE2)

                            'ws1.Cell(4, 13).Value = CODE1
                            'ws1.Cell(5, 13).Value = CODE2
                            'ws1.Cell(1, 13).Value = "過去実績"

                        End If

                        workbook.SaveAs(hensyuuiraisyo)


                        If erflg = 1 Then

                            My.Computer.FileSystem.RenameFile(hensyuuiraisyo, "E_" & MyStr)

                        End If


Step01:

                        If madef00 = "" Then
                            madef01 = madef01 & "\n" & "作成済み　　　　 客先：" & GridView1.Rows(I).Cells(4).Text & " IVNO：" & GridView1.Rows(I).Cells(6).Text
                        ElseIf madef00 = "0" Then
                            madef01 = madef01 & "\n" & "依頼書なし　　　 客先：" & GridView1.Rows(I).Cells(4).Text & " IVNO：" & GridView1.Rows(I).Cells(6).Text
                        ElseIf madef00 = "1" Then
                            madef01 = madef01 & "\n" & "委託案件　　　　 客先：" & GridView1.Rows(I).Cells(4).Text & " IVNO：" & GridView1.Rows(I).Cells(6).Text
                        ElseIf madef00 = "2" Then
                            madef01 = madef01 & "\n" & "同一フォルダあり 客先：" & GridView1.Rows(I).Cells(4).Text & " IVNO：" & GridView1.Rows(I).Cells(6).Text
                        End If
                        madef00 = ""

                    End If
                Else
                End If
            Next

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

        End If

        Page.ClientScript.RegisterStartupScript(Me.GetType, "確認", "<script language='JavaScript'>confirm('フォルダ作成完了しました。フィルタがクリアされ全件表示します。\n" & madef01 & "');</script>", False)

    End Sub


    Private Function get_itakuhanntei(ivno As String) As String

        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String = ""
        Dim strinv As String

        get_itakuhanntei = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=KBHWPM02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        Dim dt1 As DateTime = DateTime.Now

        Dim ts1 As New TimeSpan(100, 0, 0, 0)
        Dim ts2 As New TimeSpan(100, 0, 0, 0)
        Dim dt2 As DateTime = dt1 + ts1
        Dim dt3 As DateTime = dt1 - ts1

        'データベース接続を開く
        cnn.Open()

        strSQL = ""
        strSQL = strSQL & "SELECT INVNO FROM T_EXL_WORKSTATUS00 WHERE "
        strSQL = strSQL & "T_EXL_WORKSTATUS00.ID = '001' "
        strSQL = strSQL & "AND T_EXL_WORKSTATUS00.INVNO = '" & ivno & "' "
        strSQL = strSQL & "AND T_EXL_WORKSTATUS00.REGDATE > '" & dt3 & "' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        strinv = ""
        '結果を取り出す 
        While (dataread.Read())
            strinv = Convert.ToString(dataread("INVNO"))        'ETD(計上日)
            If strinv = "" Then
            Else
                get_itakuhanntei = 1
            End If
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()

    End Function

    Private Sub copy_custfile(iptbx As String, ByRef Cname As String, ByRef Ccode As String)

        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String = ""
        Dim strinv As String = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=svdpo051;Initial Catalog=BPTB001;User Id=ado_bptb001;Password=ado_bptb001"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        Dim dt1 As DateTime = DateTime.Now

        Dim ts1 As New TimeSpan(100, 0, 0, 0)
        Dim ts2 As New TimeSpan(100, 0, 0, 0)
        Dim dt2 As DateTime = dt1 + ts1
        Dim dt3 As DateTime = dt1 - ts1

        'データベース接続を開く
        cnn.Open()

        strSQL = "SELECT distinct T_INV_HD_TB.CUSTCODE,T_INV_HD_TB.CUSTNAME "
        strSQL = strSQL & "FROM T_INV_HD_TB "
        strSQL = strSQL & "WHERE T_INV_HD_TB.OLD_INVNO like '%" & iptbx & "%' "
        strSQL = strSQL & "AND T_INV_HD_TB.BLDATE BETWEEN '" & dt3 & "' AND '" & dt2 & "' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        Cname = ""
        Ccode = ""
        '結果を取り出す 
        While (dataread.Read())
            Cname = Trim(Convert.ToString(dataread("CUSTNAME")))        '客先目
            Ccode = Trim(Convert.ToString(dataread("CUSTCODE")))        '客先コード
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()

    End Sub

    Private Sub get_bookingdata(i As String, ByRef niuke As String, ByRef tsumi As String, ByRef ageti As String, ByRef haisou As String, ivno As String)

        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String = ""
        Dim strinv As String = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=KBHWPM02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        Dim dt1 As DateTime = DateTime.Now

        Dim ts1 As New TimeSpan(100, 0, 0, 0)
        Dim ts2 As New TimeSpan(100, 0, 0, 0)
        Dim dt2 As DateTime = dt1 + ts1
        Dim dt3 As DateTime = dt1 - ts1

        'データベース接続を開く
        cnn.Open()

        strSQL = ""
        strSQL = strSQL & "SELECT * FROM T_BOOKING "
        strSQL = strSQL & " WHERE INVOICE_NO like '%" & ivno & "%' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        strinv = ""
        '結果を取り出す 
        While (dataread.Read())
            niuke = Convert.ToString(dataread("PLACE_OF_RECEIPT"))
            tsumi = Convert.ToString(dataread("LOADING_PORT"))
            ageti = Convert.ToString(dataread("DISCHARGING_PORT"))
            haisou = Convert.ToString(dataread("PLACE_OF_DELIVERY"))
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()

    End Sub
End Class
