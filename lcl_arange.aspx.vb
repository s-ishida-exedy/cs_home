Imports System.Data.SqlClient
Imports System.Data
Imports mod_function
Imports System.Data.Common
Imports System.Console
Imports ClosedXML.Excel
Imports MailKit.Net.Smtp
Imports MailKit.Security
Imports MimeKit
Imports MimeKit.Text
Imports System.IO

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
        Dim upflg As Long = 0
        Dim strwd As String
        Dim strwd2 As Date
        Dim myMon01 As Date
        Dim myMon02 As Date
        Dim flgship As Long = 0
        Dim lhlflg As String = ""

        Dim strE22001 As String = ""
        Dim strE22002 As String = ""

        '搬入日作成

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)
        Dim cnn2 = New SqlConnection(ConnectionString)
        Dim Command = cnn2.CreateCommand
        'データベース接続を開く
        cnn.Open()
        cnn2.Open()
        'ヘッダー以外に処理
        If e.Row.RowType = DataControlRowType.DataRow Then

            strSQL = ""
            strSQL = "SELECT T_EXL_CSWORKDAY.WORKDAY_NO FROM T_EXL_CSWORKDAY WHERE T_EXL_CSWORKDAY.WORKDAY = '" & Format(Now(), "yyyy/MM/dd") & "' "


            'ＳＱＬコマンド作成 
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行 
            dataread = dbcmd.ExecuteReader()

            While (dataread.Read())
                strwd = Trim(dataread("WORKDAY_NO"))
            End While



            'クローズ処理 
            dataread.Close()
            dbcmd.Dispose()


            strSQL = ""
            strSQL = "SELECT T_EXL_CSWORKDAY.WORKDAY FROM T_EXL_CSWORKDAY WHERE T_EXL_CSWORKDAY.WORKDAY_NO = '" & Val(strwd) + 5 & "' "


            'ＳＱＬコマンド作成 
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行 
            dataread = dbcmd.ExecuteReader()

            While (dataread.Read())
                strwd2 = Trim(dataread("WORKDAY"))
            End While


            'クローズ処理 
            dataread.Close()
            dbcmd.Dispose()

            Dim ts2 As New TimeSpan(9, 0, 0, 0)
            Dim ts4 As New TimeSpan(6, 0, 0, 0)
            Dim ts3 As New TimeSpan(Weekday(dt1), 0, 0, 0)
            Dim dt4 As DateTime = dt1 + ts2


            myMon02 = dt4 - ts3 + ts4

            If strwd2 >= myMon02 Then
                lhlflg = "1"
            Else
                lhlflg = "2"
            End If

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

            '稼働日で7日先を取得
            If Weekday(dt1) > 6 Then
                If lhlflg = "1" Then
                    cno = 7 - Weekday(dt1) + 6 + 7
                ElseIf lhlflg = "2" Then
                    cno = 7 - Weekday(dt1) + 6
                End If
            Else
                If lhlflg = "1" Then
                    cno = 6 - Weekday(dt1) + 7 + 7
                ElseIf lhlflg = "2" Then
                    cno = 6 - Weekday(dt1) + 7
                End If

            End If

            If e.Row.RowType = DataControlRowType.DataRow Then

                e.Row.Cells(6).Text = wday2

                Dim dt3 As DateTime = DateTime.Parse(e.Row.Cells(6).Text)
                Dim ts1 As New TimeSpan(cno, 0, 0, 0)
                Dim dt2 As DateTime = dt1 + ts1 '金曜（最大値）


                If dt3 < dt2 Then
                        e.Row.BackColor = Drawing.Color.Salmon
                        If (e.Row.Cells(11).Text.Length = 6) And dt3 < dt2 Then
                            e.Row.Cells(11).Text = "AC要"
                            e.Row.Cells(11).BackColor = Drawing.Color.Red
                            e.Row.Cells(11).ForeColor = Drawing.Color.White
                        End If
                    End If


                e.Row.Cells(6).Text = e.Row.Cells(6).Text & " (" & dt3.ToString("ddd") & ")"
            End If

            'クローズ処理 
            dataread.Close()
            dbcmd.Dispose()
        End If


        strSQL = "SELECT INVNO FROM [T_EXL_WORKSTATUS00] WHERE [T_EXL_WORKSTATUS00].INVNO = '" & Left(e.Row.Cells(4).Text, 4) & "' "
        strSQL = strSQL & "AND [T_EXL_WORKSTATUS00].ID = '004' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        strinv = ""
        '結果を取り出す 
        While (dataread.Read())
            strinv += dataread("INVNO")
            '書類作成状況
            If Left(e.Row.Cells(4).Text, 4) = strinv Then
                If e.Row.Cells(11).Text = "AC要" Then
                    e.Row.Cells(11).Text = " Booking依頼済み"
                End If
            End If
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()

        If e.Row.Cells(11).Text = "" Or e.Row.Cells(11).Text = "&nbsp;" Then
            e.Row.Cells(11).Text = Left(e.Row.Cells(3).Text, 4) & Replace(e.Row.Cells(8).Text, "/", "")
        End If

        If e.Row.Cells(11).Text = "AC要" Or e.Row.Cells(11).Text = " Booking依頼済み" Then
            e.Row.Cells(11).Text = e.Row.Cells(11).Text & Left(e.Row.Cells(3).Text, 4) & Replace(e.Row.Cells(8).Text, "/", "")
        End If

        strSQL = "SELECT INVNO FROM [T_EXL_WORKSTATUS00] WHERE [T_EXL_WORKSTATUS00].INVNO = '" & Left(e.Row.Cells(4).Text, 4) & "' "
        strSQL = strSQL & "AND [T_EXL_WORKSTATUS00].ID = '005' "
        strSQL = strSQL & "AND [T_EXL_WORKSTATUS00].BKGNO = '" & Trim(e.Row.Cells(11).Text) & "' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        strinv = ""
        upflg = 0
        '結果を取り出す 
        While (dataread.Read())
            strinv += dataread("INVNO")
            '出荷手配状況
            If Left(e.Row.Cells(4).Text, 4) = strinv Then
                e.Row.BackColor = Drawing.Color.LightBlue
                upflg = 1
            End If
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()

        strSQL = "SELECT INVNO FROM [T_EXL_WORKSTATUS00] WHERE [T_EXL_WORKSTATUS00].INVNO = '" & Left(e.Row.Cells(4).Text, 4) & "' "
        strSQL = strSQL & "AND [T_EXL_WORKSTATUS00].ID = '002' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        strinv = ""
        '結果を取り出す 
        While (dataread.Read())
            strinv += dataread("INVNO")
            '書類作成状況
            If Left(e.Row.Cells(4).Text, 4) = strinv Then
                e.Row.BackColor = Drawing.Color.DarkGray
            End If
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()

        If upflg = 1 Then

            strSQL = "SELECT BOOKING_NO FROM [T_EXL_LCLTENKAI] WHERE [T_EXL_LCLTENKAI].INVOICE_NO like '%" & Left(e.Row.Cells(4).Text, 4) & "%' "
            strSQL = strSQL & "AND FLG01 <> '1' "


            'ＳＱＬコマンド作成 
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行 
            dataread = dbcmd.ExecuteReader()

            strinv = ""
            '結果を取り出す 
            While (dataread.Read())
                strinv += dataread("BOOKING_NO")
                '書類作成状況
                If Trim(e.Row.Cells(11).Text) <> strinv Then

                    strSQL = ""
                    strSQL = strSQL & "UPDATE T_EXL_LCLTENKAI SET "
                    strSQL = strSQL & "BOOKING_NO = '" & Trim(e.Row.Cells(11).Text) & "' "
                    strSQL = strSQL & "WHERE INVOICE_NO like '%" & Left(e.Row.Cells(4).Text, 4) & "%' "
                    strSQL = strSQL & "AND FLG01 <> '1' "

                    Command.CommandText = strSQL
                    ' SQLの実行
                    Command.ExecuteNonQuery()

                End If
            End While

            'クローズ処理 
            dataread.Close()
            dbcmd.Dispose()


            flgship = 0
            flgship = GET_shipsch(Trim(e.Row.Cells(11).Text))

            If flgship = 1 Then
                e.Row.Cells(1).BackColor = Drawing.Color.Red
                e.Row.Cells(1).ForeColor = Drawing.Color.White
                e.Row.Cells(1).Text = "スケジュール未登録"
            End If


            strE22001 = Left(e.Row.Cells(4).Text, 4)
            strE22002 = Trim(e.Row.Cells(11).Text)

            If Left(e.Row.Cells(3).Text, 4) = "E220" Then
                If (strE22001 <> "" Or strE22001 <> "&nbsp;") And (strE22002 <> "" Or strE22002 <> "&nbsp;") Then
                    flgship = 0
                    flgship = GET_e220(Trim(strE22001), Trim(strE22002))
                    If flgship = 0 Then
                        e.Row.Cells(1).BackColor = Drawing.Color.Red
                        e.Row.Cells(1).ForeColor = Drawing.Color.White
                        e.Row.Cells(1).Text = e.Row.Cells(1).Text & " タイトル、荷姿変更要"
                    End If
                End If
            End If

        End If


            cnn.Close()
        cnn.Dispose()

        cnn2.Close()
        cnn2.Dispose()

        e.Row.Cells(5).Visible = False
        e.Row.Cells(9).Visible = False

    End Sub



    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        '>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        '既存案件火のチェックをBKGNOのみからIVNO、その他のキーを考えてカウントを追加する
        '＞＞＞＞＞＞＞＞＞＞＞＞＞＞＞＞＞＞＞＞＞＞＞＞＞＞＞＞＞＞＞＞＞＞＞＞＞＞＞＞＞＞＞＞＞＞＞＞＞＞


        '接続文字列の作成
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
        Dim strDate As String
        Dim cno As Long
        Dim wno As Long
        Dim wday As String
        Dim wday2 As String
        Dim WEIGHT As String
        Dim QTY As String
        Dim PICKUP01 As String
        Dim PICKUP02 As String
        Dim MOVEIN01 As String
        Dim MOVEIN02 As String
        Dim OTHERS01 As String
        Dim FLG01 As String
        Dim FLG02 As String
        Dim FLG03 As String
        Dim FLG04 As String
        Dim FLG05 As String
        Dim PICKINPLACE As String
        Dim bkgno01 As String
        Dim intCnt As Long
        Dim straddress As String
        Dim strbkck As String
        Dim drage As String

        Dim dt1 As DateTime = DateTime.Now

        'データベース接続を開く
        cnn.Open()

        '非表示ボタン　FLG03は非表示
        Dim I As Integer
        For I = 0 To GridView1.Rows.Count - 1
            If CType(GridView1.Rows(I).Cells(0).Controls(1), CheckBox).Checked Then
                bkgno01 = ""

                '対象の日付以下の日付の最大値を取得

                strSQL = "SELECT MAX(WORKDAY) AS WDAY01 FROM [T_EXL_CSWORKDAY] WHERE [T_EXL_CSWORKDAY].WORKDAY < '" & GridView1.Rows(I).Cells(7).Text & "' "

                'ＳＱＬコマンド作成 
                dbcmd = New SqlCommand(strSQL, cnn)
                'ＳＱＬ文実行 
                dataread = dbcmd.ExecuteReader()

                '結果を取り出す 
                While (dataread.Read())
                    wday2 = dataread("WDAY01")
                End While

                Dim dt3 As DateTime = DateTime.Parse(wday2)

                wday2 = wday2 & " (" & dt3.ToString("ddd") & ")"

                'クローズ処理 
                dataread.Close()
                dbcmd.Dispose()

                If Convert.ToString(GridView1.Rows(I).Cells(11).Text) = "AC要" Or GridView1.Rows(I).Cells(11).Text = "Booking依頼済み" Or GridView1.Rows(I).Cells(11).Text = "" Or GridView1.Rows(I).Cells(11).Text = "&nbsp;" Then
                    bkgno01 = Left(GridView1.Rows(I).Cells(3).Text, 4) & Replace(GridView1.Rows(I).Cells(8).Text, "/", "")
                Else
                    bkgno01 = Trim(GridView1.Rows(I).Cells(11).Text)
                End If

                strSQL = ""
                strSQL = strSQL & "SELECT COUNT(*) AS RecCnt FROM T_EXL_LCLTENKAI WHERE "
                strSQL = strSQL & "T_EXL_LCLTENKAI.BOOKING_NO = '" & bkgno01 & "' "
                strSQL = strSQL & "AND FLG01 <> '1' "

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

                strSQL = ""
                strSQL = strSQL & "SELECT ADDRESS FROM T_EXL_LCLCUSTPREADS WHERE "
                strSQL = strSQL & "T_EXL_LCLCUSTPREADS.CUSTCODE = '" & Left(Convert.ToString(GridView1.Rows(I).Cells(3).Text), 4) & "' "

                'ＳＱＬコマンド作成 
                dbcmd = New SqlCommand(strSQL, cnn)
                'ＳＱＬ文実行 
                dataread = dbcmd.ExecuteReader()

                straddress = ""

                While (dataread.Read())
                    straddress = dataread("ADDRESS")
                End While

                'クローズ処理 
                dataread.Close()
                dbcmd.Dispose()

                If straddress = "" Then

                    strSQL = ""
                    strSQL = strSQL & "SELECT ADDRESS FROM T_EXL_LCLCUSTPREADS WHERE "
                    strSQL = strSQL & "T_EXL_LCLCUSTPREADS.CUSTCODE = 'その他' "


                    'ＳＱＬコマンド作成 
                    dbcmd = New SqlCommand(strSQL, cnn)
                    'ＳＱＬ文実行 
                    dataread = dbcmd.ExecuteReader()

                    straddress = ""

                    While (dataread.Read())
                        straddress = dataread("ADDRESS")
                    End While

                    'クローズ処理 
                    dataread.Close()
                    dbcmd.Dispose()

                End If

                If intCnt > 0 Then

                    strSQL = ""
                    strSQL = strSQL & "SELECT * FROM T_EXL_LCLTENKAI WHERE "
                    strSQL = strSQL & "T_EXL_LCLTENKAI.BOOKING_NO = '" & bkgno01 & "' "
                    strSQL = strSQL & "AND FLG01 <> '1' "

                    'ＳＱＬコマンド作成 
                    dbcmd = New SqlCommand(strSQL, cnn)
                    'ＳＱＬ文実行 
                    dataread = dbcmd.ExecuteReader()

                    While (dataread.Read())
                        WEIGHT = Convert.ToString(dataread("WEIGHT"))
                        QTY = Convert.ToString(dataread("QTY"))
                        PICKUP01 = Convert.ToString(dataread("PICKUP01"))
                        PICKUP02 = Convert.ToString(dataread("PICKUP02"))
                        MOVEIN01 = Convert.ToString(dataread("MOVEIN01"))
                        MOVEIN02 = Convert.ToString(dataread("MOVEIN02"))
                        OTHERS01 = Convert.ToString(dataread("OTHERS01"))
                        FLG01 = Convert.ToString(dataread("FLG01"))
                        FLG02 = Convert.ToString(dataread("FLG02"))
                        FLG03 = Convert.ToString(dataread("FLG03"))
                        FLG04 = Convert.ToString(dataread("FLG04"))
                        FLG05 = Convert.ToString(dataread("FLG05"))
                        PICKINPLACE = Convert.ToString(dataread("PICKINPLACE"))
                    End While

                    'クローズ処理 
                    dataread.Close()
                    dbcmd.Dispose()

                    strSQL = ""
                    strSQL = strSQL & "UPDATE T_EXL_LCLTENKAI SET "
                    strSQL = strSQL & "CONSIGNEE = '" & GridView1.Rows(I).Cells(1).Text & "', "
                    strSQL = strSQL & "DESTINATION = '" & GridView1.Rows(I).Cells(2).Text & "', "
                    strSQL = strSQL & "CUST = '" & Left(GridView1.Rows(I).Cells(3).Text, 4) & "', "
                    strSQL = strSQL & "INVOICE_NO = '" & Replace(Convert.ToString(GridView1.Rows(I).Cells(4).Text), "&nbsp;", "") & "', "
                    strSQL = strSQL & "BOOKING_NO = '" & bkgno01 & "', "
                    strSQL = strSQL & "OFFICIAL_QUOT = '" & GridView1.Rows(I).Cells(5).Text & "', "
                    strSQL = strSQL & "CUT_DATE = '" & GridView1.Rows(I).Cells(7).Text & "', "
                    strSQL = strSQL & "ETD = '" & GridView1.Rows(I).Cells(8).Text & "', "
                    strSQL = strSQL & "ETA = '" & Replace(GridView1.Rows(I).Cells(9).Text, "&nbsp;", "") & "', "
                    strSQL = strSQL & "LCL_SIZE = '" & GridView1.Rows(I).Cells(10).Text & "', "
                    strSQL = strSQL & "WEIGHT = '" & WEIGHT & "', "
                    strSQL = strSQL & "QTY = '" & QTY & "', "
                    strSQL = strSQL & "PICKUP01 = '" & PICKUP01 & "', "
                    strSQL = strSQL & "PICKUP02 = '" & PICKUP02 & "', "
                    strSQL = strSQL & "MOVEIN01 = '" & MOVEIN01 & "', "
                    strSQL = strSQL & "MOVEIN02 = '" & MOVEIN02 & "', "
                    strSQL = strSQL & "OTHERS01 = '" & OTHERS01 & "', "
                    strSQL = strSQL & "FLG01 = '" & FLG01 & "', "
                    strSQL = strSQL & "FLG02 = '" & FLG02 & "', "
                    strSQL = strSQL & "FLG03 = '" & FLG03 & "', "
                    strSQL = strSQL & "FLG04 = '" & FLG04 & "', "
                    strSQL = strSQL & "FLG05 = '" & FLG05 & "', "
                    strSQL = strSQL & "PICKINPLACE = '" & straddress & "' "
                    strSQL = strSQL & "WHERE BOOKING_NO ='" & bkgno01 & "' "
                    strSQL = strSQL & "AND FLG01 <> '1' "

                Else

                    strSQL = ""
                    strSQL = strSQL & "SELECT * FROM T_EXL_LCLTENKAI WHERE "
                    strSQL = strSQL & "T_EXL_LCLTENKAI.BOOKING_NO like '%" & Left(Convert.ToString(GridView1.Rows(I).Cells(3).Text), 4) & Replace(Convert.ToString(GridView1.Rows(I).Cells(8).Text), "/", "") & "%' "


                    'ＳＱＬコマンド作成 
                    dbcmd = New SqlCommand(strSQL, cnn)
                    'ＳＱＬ文実行 
                    dataread = dbcmd.ExecuteReader()

                    strbkck = ""

                    While (dataread.Read())
                        strbkck = dataread("BOOKING_NO")
                        WEIGHT = Convert.ToString(dataread("WEIGHT"))
                        QTY = Convert.ToString(dataread("QTY"))
                        PICKUP01 = Convert.ToString(dataread("PICKUP01"))
                        PICKUP02 = Convert.ToString(dataread("PICKUP02"))
                        MOVEIN01 = Convert.ToString(dataread("MOVEIN01"))
                        MOVEIN02 = Convert.ToString(dataread("MOVEIN02"))
                        OTHERS01 = Convert.ToString(dataread("OTHERS01"))
                        FLG01 = Convert.ToString(dataread("FLG01"))
                        FLG02 = Convert.ToString(dataread("FLG02"))
                        FLG03 = Convert.ToString(dataread("FLG03"))
                        FLG04 = Convert.ToString(dataread("FLG04"))
                        FLG05 = Convert.ToString(dataread("FLG05"))
                        PICKINPLACE = Convert.ToString(dataread("PICKINPLACE"))
                    End While

                    'クローズ処理 
                    dataread.Close()
                    dbcmd.Dispose()

                    If strbkck = "" Or strbkck = "&nbsp;" Then

                        strSQL = ""
                        strSQL = strSQL & "INSERT INTO T_EXL_LCLTENKAI VALUES('" & GridView1.Rows(I).Cells(1).Text & "','" & GridView1.Rows(I).Cells(2).Text & "','" & Left(GridView1.Rows(I).Cells(3).Text, 4)
                        strSQL = strSQL & "','" & Replace(Convert.ToString(GridView1.Rows(I).Cells(4).Text), "&nbsp;", "") & "','" & bkgno01
                        strSQL = strSQL & "','" & GridView1.Rows(I).Cells(5).Text & "','" & GridView1.Rows(I).Cells(7).Text
                        strSQL = strSQL & "','" & GridView1.Rows(I).Cells(8).Text & "','" & Replace(GridView1.Rows(I).Cells(9).Text, "&nbsp;", "") & "','" & GridView1.Rows(I).Cells(10).Text
                        strSQL = strSQL & "','','','" & Left(wday2, 10) & "','AM','" & Left(wday2, 10) & "','PM','','','','','','','" & straddress & "')"

                    Else

                        strSQL = ""
                        strSQL = strSQL & "UPDATE T_EXL_LCLTENKAI SET "
                        strSQL = strSQL & "CONSIGNEE = '" & GridView1.Rows(I).Cells(1).Text & "', "
                        strSQL = strSQL & "DESTINATION = '" & GridView1.Rows(I).Cells(2).Text & "', "
                        strSQL = strSQL & "CUST = '" & Left(GridView1.Rows(I).Cells(3).Text, 4) & "', "
                        strSQL = strSQL & "INVOICE_NO = '" & Replace(Convert.ToString(GridView1.Rows(I).Cells(4).Text), "&nbsp;", "") & "', "
                        strSQL = strSQL & "BOOKING_NO = '" & bkgno01 & "', "
                        strSQL = strSQL & "OFFICIAL_QUOT = '" & GridView1.Rows(I).Cells(5).Text & "', "
                        strSQL = strSQL & "CUT_DATE = '" & GridView1.Rows(I).Cells(7).Text & "', "
                        strSQL = strSQL & "ETD = '" & GridView1.Rows(I).Cells(8).Text & "', "
                        strSQL = strSQL & "ETA = '" & Replace(GridView1.Rows(I).Cells(9).Text, "&nbsp;", "") & "', "
                        strSQL = strSQL & "LCL_SIZE = '" & GridView1.Rows(I).Cells(10).Text & "', "
                        strSQL = strSQL & "WEIGHT = '" & WEIGHT & "', "
                        strSQL = strSQL & "QTY = '" & QTY & "', "
                        strSQL = strSQL & "PICKUP01 = '" & PICKUP01 & "', "
                        strSQL = strSQL & "PICKUP02 = '" & PICKUP02 & "', "
                        strSQL = strSQL & "MOVEIN01 = '" & MOVEIN01 & "', "
                        strSQL = strSQL & "MOVEIN02 = '" & MOVEIN02 & "', "
                        strSQL = strSQL & "OTHERS01 = '" & OTHERS01 & "', "
                        strSQL = strSQL & "FLG01 = '" & FLG01 & "', "
                        strSQL = strSQL & "FLG02 = '" & FLG02 & "', "
                        strSQL = strSQL & "FLG03 = '" & FLG03 & "', "
                        strSQL = strSQL & "FLG04 = '" & FLG04 & "', "
                        strSQL = strSQL & "FLG05 = '" & FLG05 & "', "
                        strSQL = strSQL & "PICKINPLACE = '" & straddress & "' "
                        strSQL = strSQL & "WHERE BOOKING_NO like '%" & Left(Convert.ToString(GridView1.Rows(I).Cells(3).Text), 4) & Replace(Convert.ToString(GridView1.Rows(I).Cells(8).Text), "/", "") & "%' "
                        strSQL = strSQL & "AND FLG01 <> '1' "
                    End If
                End If

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

                If FLG03 = "1" Then
                    Call GET_IVDATA2(Left(GridView1.Rows(I).Cells(4).Text, 4), "1")
                End If
            Else
            End If
        Next

        '案件展開後、展開済みの場合（FLG03 ＝１）の場合はCSSTATUSに追加する。（IVNO、BOOKINGNO）
        GridView1.DataBind()


    End Sub

    Private Sub GET_IVDATA(bkgno As String)

        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String
        Dim strinv As String

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

        strSQL = "SELECT T_INV_HD_TB.OLD_INVNO "
        strSQL = strSQL & "FROM T_INV_HD_TB LEFT JOIN T_INV_BD_TB ON T_INV_HD_TB.INVOICENO = T_INV_BD_TB.INVOICENO "
        strSQL = strSQL & "WHERE T_INV_HD_TB.BLDATE BETWEEN '" & dt3 & "' AND '" & dt2 & "' "
        strSQL = strSQL & "GROUP BY T_INV_HD_TB.BOOKINGNO, T_INV_HD_TB.OLD_INVNO, T_INV_HD_TB.SHIPPEDPER, T_INV_HD_TB.VOYAGENO, T_INV_HD_TB.IOPORTDATE, T_INV_HD_TB.CUTDATE "
        strSQL = strSQL & "HAVING T_INV_HD_TB.BOOKINGNO = '" & bkgno & "' "
        'strSQL = strSQL & "AND Sum(T_INV_BD_TB.QTY) >= 1 "
        'strSQL = strSQL & "AND Sum(T_INV_BD_TB.KIN) >= 1 "
        'strSQL = strSQL & "order by T_INV_HD_TB.CUTDATE Decs "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        strDate = ""
        '結果を取り出す 
        While (dataread.Read())
            strinv = Convert.ToString(dataread("OLD_INVNO"))        'ETD(計上日)
            Call INS_LCL(strinv, bkgno)
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()

    End Sub

    Private Sub GET_IVDATA2(strinv As String, A As String)

        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String
        Dim bkgno As String

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


        strSQL = "SELECT distinct T_INV_HD_TB.BOOKINGNO "
        strSQL = strSQL & "FROM T_INV_HD_TB LEFT JOIN T_INV_BD_TB ON T_INV_HD_TB.INVOICENO = T_INV_BD_TB.INVOICENO "
        strSQL = strSQL & "WHERE T_INV_HD_TB.BLDATE BETWEEN '" & dt3 & "' AND '" & dt2 & "' "
        strSQL = strSQL & "GROUP BY T_INV_HD_TB.BOOKINGNO, T_INV_HD_TB.OLD_INVNO, T_INV_HD_TB.SHIPPEDPER, T_INV_HD_TB.VOYAGENO, T_INV_HD_TB.IOPORTDATE, T_INV_HD_TB.CUTDATE "
        strSQL = strSQL & "HAVING T_INV_HD_TB.OLD_INVNO like '%" & strinv & "%' "
        strSQL = strSQL & "AND BOOKINGNO is not null "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        strDate = ""
        '結果を取り出す 
        While (dataread.Read())
            bkgno = Trim(Convert.ToString(dataread("BOOKINGNO")))        'ETD(計上日)
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()

        If bkgno = "" Or IsNothing(bkgno) = True Then
        Else

            strSQL = "SELECT T_INV_HD_TB.OLD_INVNO "
            strSQL = strSQL & "FROM T_INV_HD_TB LEFT JOIN T_INV_BD_TB ON T_INV_HD_TB.INVOICENO = T_INV_BD_TB.INVOICENO "

            strSQL = strSQL & "WHERE T_INV_HD_TB.BLDATE BETWEEN '" & dt3 & "' AND '" & dt2 & "' "
            strSQL = strSQL & "GROUP BY T_INV_HD_TB.BOOKINGNO, T_INV_HD_TB.OLD_INVNO, T_INV_HD_TB.SHIPPEDPER, T_INV_HD_TB.VOYAGENO, T_INV_HD_TB.IOPORTDATE, T_INV_HD_TB.CUTDATE "
            strSQL = strSQL & "HAVING T_INV_HD_TB.BOOKINGNO = '" & bkgno & "' "
            'strSQL = strSQL & "AND Sum(T_INV_BD_TB.QTY) >= 1 "
            'strSQL = strSQL & "AND Sum(T_INV_BD_TB.KIN) >= 1 "
            'strSQL = strSQL & "order by T_INV_HD_TB.CUTDATE Decs "

            'ＳＱＬコマンド作成 
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行 
            dataread = dbcmd.ExecuteReader()

            strDate = ""
            '結果を取り出す 
            While (dataread.Read())
                strinv = Convert.ToString(dataread("OLD_INVNO"))        'ETD(計上日)
                If A = "1" Then
                    Call INS_LCL(strinv, bkgno)
                Else
                End If
            End While

            'クローズ処理 
            dataread.Close()
            dbcmd.Dispose()

        End If

        cnn.Close()
        cnn.Dispose()

    End Sub
    Private Sub INS_LCL(strinv As String, bkgno As String)
        '接続文字列の作成
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

        Dim intCnt As Long

        Dim dt1 As DateTime = DateTime.Now

        'データベース接続を開く
        cnn.Open()


        strSQL = ""
        strSQL = strSQL & "SELECT COUNT(*) AS RecCnt FROM T_EXL_WORKSTATUS00 WHERE "
        strSQL = strSQL & "T_EXL_WORKSTATUS00.ID = '005' "
        strSQL = strSQL & "AND T_EXL_WORKSTATUS00.INVNO = '" & strinv & "' "
        strSQL = strSQL & "AND T_EXL_WORKSTATUS00.BKGNO = '" & bkgno & "' "



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


        If intCnt > 0 Then

            strSQL = ""
            strSQL = strSQL & "UPDATE T_EXL_WORKSTATUS00 SET "

            strSQL = strSQL & "T_EXL_WORKSTATUS00.INVNO = '" & strinv & "', "
            strSQL = strSQL & "T_EXL_WORKSTATUS00.REGDATE = '" & Format(Now(), "yyyy/MM/dd") & "', "
            strSQL = strSQL & "T_EXL_WORKSTATUS00.BKGNO = '" & bkgno & "' "
            strSQL = strSQL & "WHERE T_EXL_WORKSTATUS00.INVNO ='" & strinv & "' "
            strSQL = strSQL & "AND T_EXL_WORKSTATUS00.BKGNO = '" & bkgno & "' "
            strSQL = strSQL & "AND T_EXL_WORKSTATUS00.ID = '005' "

        Else

            strSQL = ""
            strSQL = strSQL & "INSERT INTO T_EXL_WORKSTATUS00 VALUES("
            strSQL = strSQL & " '005' "
            strSQL = strSQL & ",'" & strinv & "' "
            strSQL = strSQL & ",'" & bkgno & "' "
            strSQL = strSQL & ",'" & Format(Now(), "yyyy/MM/dd") & "' "
            strSQL = strSQL & ",'" & Session("UsrId") & "_04" & "' "
            strSQL = strSQL & ")"


        End If

        Command.CommandText = strSQL
        ' SQLの実行
        Command.ExecuteNonQuery()

        cnn.Close()
        cnn.Dispose()

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

        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=KBHWPM02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand
        Dim ivno As String = ""
        Dim dataread2 As SqlDataReader
        Dim dbcmd2 As SqlCommand
        Dim intCnt As Long
        Dim strkd As String
        Dim stram As String
        Dim strwd As String
        Dim strwd2 As Date
        Dim myMon01 As Date
        Dim myMon02 As Date

        Dim ercnt As Long
        Dim lhlflg As String = ""

        Dim dt1 As DateTime = DateTime.Now


        '稼働日で7日先を取得
        'データベース接続を開く
        cnn.Open()


        strSQL = ""
        strSQL = "SELECT T_EXL_CSWORKDAY.WORKDAY_NO FROM T_EXL_CSWORKDAY WHERE T_EXL_CSWORKDAY.WORKDAY = '" & Format(Now(), "yyyy/MM/dd") & "' "


        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        While (dataread.Read())
            strwd = Trim(dataread("WORKDAY_NO"))
        End While



        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()


        strSQL = ""
        strSQL = "SELECT T_EXL_CSWORKDAY.WORKDAY FROM T_EXL_CSWORKDAY WHERE T_EXL_CSWORKDAY.WORKDAY_NO = '" & Val(strwd) + 5 & "' "


        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        While (dataread.Read())
            strwd2 = Trim(dataread("WORKDAY"))
        End While


        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()

        Dim ts2 As New TimeSpan(9, 0, 0, 0)
        Dim ts4 As New TimeSpan(6, 0, 0, 0)
        Dim ts3 As New TimeSpan(Weekday(dt1), 0, 0, 0)
        Dim dt3 As DateTime = dt1 + ts2


        myMon02 = dt3 - ts3 + ts4

        If strwd2 >= myMon02 Then
            lhlflg = "1"
        Else
            lhlflg = "2"
        End If


        '稼働日で7日先を取得
        If Weekday(dt1) > 6 Then
            If lhlflg = "1" Then
                cno = 7 - Weekday(dt1) + 6 + 7
            ElseIf lhlflg = "2" Then
                cno = 7 - Weekday(dt1) + 6
            End If
        Else
            If lhlflg = "1" Then
                cno = 6 - Weekday(dt1) + 7 + 7
            ElseIf lhlflg = "2" Then
                cno = 6 - Weekday(dt1) + 7
            End If

        End If

        Dim ts1 As New TimeSpan(cno, 0, 0, 0)
        Dim dt2 As DateTime = dt1 + ts1

        '最終更新年月日を表示
        Me.Label1.Text = "手配対象期間：" & dt1.ToShortDateString & " (" & dt1.ToString("ddd") & ") " & "~ " & dt2.ToShortDateString & " (" & dt2.ToString("ddd") & ") "




        Dim strupddate00 As Date
        Dim strupddate01 As Date



        strSQL = ""
        strSQL = strSQL & "SELECT T_EXL_DATA_UPD.DATA_UPD FROM T_EXL_DATA_UPD "
        strSQL = strSQL & "WHERE T_EXL_DATA_UPD.DATA_CD ='008' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        While (dataread.Read())
            strupddate00 = Trim(dataread("DATA_UPD"))
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()

        strSQL = ""
        strSQL = strSQL & "SELECT COUNT(*) AS RecCnt FROM T_BOOKING "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        While (dataread.Read())
            ercnt = Trim(dataread("RecCnt"))
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()

        Dim dt00 As String = dt1.ToShortDateString
        Dim dt01 As String = strupddate00.ToShortDateString

        If ercnt = 0 Then

            Panel1.Visible = False
            Panel3.Visible = True

        Else


            If dt00 = dt01 Then
                Panel3.Visible = False
            Else
                Panel1.Visible = False
                Panel3.Visible = True
            End If

        End If

        If lhlflg = "1" Then



        End If

        'Dim dt0A = DateTime.Parse("08:00:00")
        'Dim dt1A = DateTime.Parse("08:10:00")

        'Dim dt0B = DateTime.Parse("11:50:00")
        'Dim dt1B = DateTime.Parse("12:00:00")

        'Dim dt0C = DateTime.Parse("14:55:00")
        'Dim dt1C = DateTime.Parse("15:05:00")


        'If dt1 < dt1A And dt1 > dt0A Then

        '    Panel1.Visible = False
        '    Panel3.Visible = False
        '    Panel2.Visible = True

        'End If

        'If dt1 < dt1B And dt1 > dt0B Then

        '    Panel1.Visible = False
        '    Panel3.Visible = False
        '    Panel2.Visible = True

        'End If


        'If dt1 < dt1C And dt1 > dt0C Then

        '    Panel1.Visible = False
        '    Panel3.Visible = False
        '    Panel2.Visible = True

        'End If

        cnn.Close()
        cnn.Dispose()

    End Sub


    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click



        Dim struid As String = Session("UsrId")
        Dim strfrom As String = GET_from(struid)
        '        Dim strto As String = "r-fukao@exedy.com,s-ishida@exedy.com"
        Dim strto As String = "r-fukao@exedy.com,r-fukao@exedy.com"

        Dim strsyomei As String = GET_syomei(struid)

        'メールの送信に必要な情報
        Dim smtpHostName As String = "svsmtp01.exedy.co.jp"
        Dim smtpPort As Integer = 25

        ' メールの内容


        'メールの件名
        Dim subject As String = "【異常報告】Bookingシート未更新"

        'メールの本文
        Dim body As String = "<html><body>Bookingシート未更新です。</body></html>" ' UriBodyC()

        body = "<font size=" & Chr(34) & " 3" & Chr(34) & ">" & body & "</font>"
        body = "<font face=" & Chr(34) & " Meiryo UI" & Chr(34) & ">" & body & "</font>"

        ' MailKit におけるメールの情報
        Dim message = New MimeKit.MimeMessage()

        ' 送り元情報  
        message.From.Add(MailboxAddress.Parse(strfrom))


        If strto <> "" Then
            'カンマ区切りをSPLIT
            Dim strVal() As String = strto.Split(",")
            For Each c In strVal
                message.To.Add(New MailboxAddress("", c))
            Next
        End If

        ' 表題  
        message.Subject = subject

        ' 本文
        Dim textPart = New MimeKit.TextPart(MimeKit.Text.TextFormat.Html)
        textPart.Text = body
        message.Body = textPart

        Dim multipart = New MimeKit.Multipart("mixed")

        multipart.Add(textPart)

        message.Body = multipart

        Using client As New MailKit.Net.Smtp.SmtpClient()
            client.Connect(smtpHostName, smtpPort, MailKit.Security.SecureSocketOptions.Auto)
            client.Send(message)
            client.Disconnect(True)
            client.Dispose()
            message.Dispose()
        End Using

        Page.ClientScript.RegisterStartupScript(Me.GetType, "確認", "<script language='JavaScript'>confirm('メールを送信しました。');</script>", False)

    End Sub



    Private Function GET_ToAddress(strkbn As String, strtocc As String) As String
        'BCCメールアドレス情報を取得
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String

        GET_ToAddress = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()

        strSQL = strSQL & "SELECT MAIL_ADD FROM M_EXL_LCL_DEC_MAIL "
        strSQL = strSQL & "WHERE kbn = '" & strkbn & "' "
        strSQL = strSQL & "AND TO_CC = '" & strtocc & "' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        strDate = ""
        '結果を取り出す 
        While (dataread.Read())
            GET_ToAddress += dataread("MAIL_ADD") + ","
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()

    End Function


    Private Function GET_from(struid As String) As String
        'BCCメールアドレス情報を取得
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String

        GET_from = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()

        strSQL = strSQL & "SELECT e_mail FROM M_EXL_USR "
        strSQL = strSQL & "WHERE uid = '" & struid & "' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        strDate = ""
        '結果を取り出す 
        While (dataread.Read())
            GET_from += dataread("e_mail")
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()

    End Function


    Private Function GET_syomei(struid As String) As String
        'BCCメールアドレス情報を取得
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String

        GET_syomei = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()

        strSQL = strSQL & "SELECT MEMBER_NAME,COMPANY,TEAM,TEL_NO,E_MAIL FROM M_EXL_CS_MEMBER "
        strSQL = strSQL & "WHERE code = '" & struid & "' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        strDate = ""
        '結果を取り出す 
        While (dataread.Read())
            GET_syomei += "<html><body>******************************<p></p>" + "" + dataread("MEMBER_NAME") + "<p></p>" + dataread("COMPANY") + "<p></p>" + dataread("TEL_NO") + "<p></p>" + dataread("E_MAIL") + "<p></p>" + "******************************</body></html>"
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()

    End Function


    Private Function GET_shipsch(bkgno As String) As String

        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""



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

        strSQL = "SELECT count(T_SHIPSCH_VIEW_02.BOOKINGNO) as cnt "
        strSQL = strSQL & "FROM T_SHIPSCH_VIEW_02 "
        strSQL = strSQL & "WHERE T_SHIPSCH_VIEW_02.BOOKINGNO = '" & bkgno & "' "
        strSQL = strSQL & "AND (T_SHIPSCH_VIEW_02.VANDATE IS NULL or T_SHIPSCH_VIEW_02.VANDATE < " & dt1.ToShortDateString & " ) "


        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        GET_shipsch = ""
        '結果を取り出す 
        While (dataread.Read())
            GET_shipsch = dataread("cnt")        'ETD(計上日)
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()

    End Function



    Private Function GET_e220(ivno As String, bkgno As String) As String

        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""

        Dim strp As String = ""
        Dim strt As String = ""

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

        strSQL = "SELECT DISTINCT T_INV_HD_TB.PAYMENT, T_INV_HD_TB.INVBODYTITLE "
        strSQL = strSQL & "FROM T_INV_HD_TB "
        strSQL = strSQL & "WHERE T_INV_HD_TB.BOOKINGNO = '" & bkgno & "' "
        strSQL = strSQL & "AND T_INV_HD_TB.OLD_INVNO = '" & ivno & "' "



        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        '結果を取り出す 
        While (dataread.Read())
            strp = Trim(dataread("PAYMENT"))
            strt = Trim(dataread("INVBODYTITLE"))
        End While


        If strp = "FREE OF CHARGE" And strt = "PLASTIC PLATE" Then
            GET_e220 = 1
        Else
            GET_e220 = 0
        End If

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()

    End Function


End Class
