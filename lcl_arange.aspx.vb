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



                e.Row.Cells(6).Text = wday2

                Dim dt3 As DateTime = DateTime.Parse(e.Row.Cells(6).Text)
                Dim ts1 As New TimeSpan(cno, 0, 0, 0)
                Dim dt2 As DateTime = dt1 + ts1


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


        strSQL = "SELECT LCLARGD_INVNO FROM [T_EXL_CSWORKSTATUS] WHERE [T_EXL_CSWORKSTATUS].LCLARGD_INVNO = '" & Left(e.Row.Cells(4).Text, 4) & "' "


        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        strinv = ""
        '結果を取り出す 
        While (dataread.Read())
            strinv += dataread("LCLARGD_INVNO")

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

        If e.Row.Cells(11).Text = "AC要" Or e.Row.Cells(11).Text = "Booking依頼済み" Then

            e.Row.Cells(11).Text = e.Row.Cells(11).Text & Left(e.Row.Cells(3).Text, 4) & Replace(e.Row.Cells(8).Text, "/", "")

        End If



        strSQL = "SELECT LCLFIN_INVNO FROM [T_EXL_CSWORKSTATUS] WHERE [T_EXL_CSWORKSTATUS].LCLFIN_INVNO = '" & Left(e.Row.Cells(4).Text, 4) & "' "
        strSQL = strSQL & "AND [T_EXL_CSWORKSTATUS].LCLFIN_BKGNO = '" & e.Row.Cells(11).Text & "' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        strinv = ""
        '結果を取り出す 
        While (dataread.Read())
            strinv += dataread("LCLFIN_INVNO")


            '出荷手配状況
            If Left(e.Row.Cells(4).Text, 4) = strinv Then
                e.Row.BackColor = Drawing.Color.LightBlue
            End If

        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()




        strSQL = "SELECT DOCFIN_INVNO FROM [T_EXL_CSWORKSTATUS] WHERE [T_EXL_CSWORKSTATUS].DOCFIN_INVNO = '" & Left(e.Row.Cells(4).Text, 4) & "' "


        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        strinv = ""
        '結果を取り出す 
        While (dataread.Read())
            strinv += dataread("DOCFIN_INVNO")

            '書類作成状況
            If Left(e.Row.Cells(4).Text, 4) = strinv Then
                e.Row.BackColor = Drawing.Color.DarkGray
            End If

        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()


        cnn.Close()
        cnn.Dispose()




        e.Row.Cells(5).Visible = False
        e.Row.Cells(9).Visible = False

    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged
        Dim sch_ID As String = GridView1.SelectedValue.ToString
        '選択された行のSCH_ID
        strRow = sch_ID
    End Sub


    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click


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

                    bkgno01 = GridView1.Rows(I).Cells(11).Text

                End If


                strSQL = ""
                strSQL = strSQL & "SELECT COUNT(*) AS RecCnt FROM T_EXL_LCLTENKAI WHERE "
                strSQL = strSQL & "T_EXL_LCLTENKAI.BOOKING_NO = '" & bkgno01 & "' "


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

                Else



                    strSQL = ""
                    strSQL = strSQL & "SELECT BOOKING_NO FROM T_EXL_LCLTENKAI WHERE "
                    strSQL = strSQL & "T_EXL_LCLTENKAI.BOOKING_NO = '" & Left(Convert.ToString(GridView1.Rows(I).Cells(3).Text), 4) & Convert.ToString(GridView1.Rows(I).Cells(8).Text) & "' "


                    'ＳＱＬコマンド作成 
                    dbcmd = New SqlCommand(strSQL, cnn)
                    'ＳＱＬ文実行 
                    dataread = dbcmd.ExecuteReader()

                    strbkck = ""

                    While (dataread.Read())

                        strbkck = dataread("BOOKING_NO")


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
                        strSQL = strSQL & "','','',' " & wday2 & " ','AM',' " & wday2 & "','PM','','','','','','','" & straddress & "')"



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
                        strSQL = strSQL & "WHERE BOOKING_NO ='" & bkgno01 & "' "


                    End If




                End If

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()
            Else

            End If
        Next

        If FLG03 = "1" Then


            Call GET_IVDATA2(Left(GridView1.Rows(I).Cells(4).Text, 4), "1")

        End If

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

        'データベース接続を開く
        cnn.Open()


        strSQL = "SELECT T_INV_HD_TB.OLD_INVNO "
        strSQL = strSQL & "FROM T_INV_HD_TB LEFT JOIN T_INV_BD_TB ON T_INV_HD_TB.INVOICENO = T_INV_BD_TB.INVOICENO "
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

            bkgno = Convert.ToString(dataread("BOOKINGNO"))        'ETD(計上日)

        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()

        If bkgno = "" Or IsNothing(bkgno) = True Then


        Else



            strSQL = "SELECT T_INV_HD_TB.OLD_INVNO "
            strSQL = strSQL & "FROM T_INV_HD_TB LEFT JOIN T_INV_BD_TB ON T_INV_HD_TB.INVOICENO = T_INV_BD_TB.INVOICENO "
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
        strSQL = strSQL & "SELECT COUNT(*) AS RecCnt FROM T_EXL_CSWORKSTATUS WHERE "
        strSQL = strSQL & "T_EXL_CSWORKSTATUS.LCLFIN_INVNO = '" & strinv & "' "
        strSQL = strSQL & "AND T_EXL_CSWORKSTATUS.LCLFIN_BKGNO = '" & bkgno & "' "

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
            strSQL = strSQL & "UPDATE T_EXL_CSWORKSTATUS SET "
            strSQL = strSQL & "T_EXL_CSWORKSTATUS.LCLFIN_INVNO = '" & strinv & "', "
            strSQL = strSQL & "T_EXL_CSWORKSTATUS.LCLFIN_REGDATE = '" & Format(Now(), "yyyy/MM/dd") & "', "
            strSQL = strSQL & "T_EXL_CSWORKSTATUS.LCLFIN_BKGNO = '" & bkgno & "' "
            strSQL = strSQL & "WHERE T_EXL_CSWORKSTATUS.LCLFIN_INVNO ='" & strinv & "' "

        Else

            strSQL = ""
            strSQL = strSQL & "INSERT INTO T_EXL_CSWORKSTATUS VALUES("

            strSQL = strSQL & " '" & "' "
            strSQL = strSQL & ",'" & " ' "
            strSQL = strSQL & ",'" & " ' "

            strSQL = strSQL & ",'" & strinv & "' "
            strSQL = strSQL & ",'" & Format(Now(), "yyyy/MM/dd") & "' "
            strSQL = strSQL & ",'" & bkgno & "' "

            strSQL = strSQL & ",'" & " ' "
            strSQL = strSQL & ",'" & " ' "
            strSQL = strSQL & ",'" & " ' "



            strSQL = strSQL & ",'" & " ' "
            strSQL = strSQL & ",'" & " ' "
            strSQL = strSQL & ",'" & " ' "

            strSQL = strSQL & ",'" & " ' "
            strSQL = strSQL & ",'" & " ' "
            strSQL = strSQL & ",'" & " ' "


            strSQL = strSQL & ")"

        End If

        Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()



        cnn.Close()
        cnn.Dispose()

    End Sub

End Class
