Imports System.Data.SqlClient
Partial Class cs_home
    Inherits System.Web.UI.Page

    Public strRow As String
    Public strProcess As String


    Private Sub GridView1_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound

        ''最終更新年月日取得
        'Dim dataread As SqlDataReader
        'Dim dbcmd As SqlCommand
        'Dim strSQL As String
        'Dim strinv As String
        'Dim cno As Long
        'Dim wno As Long
        'Dim wday As String
        'Dim wday2 As String

        'Dim dt1 As DateTime = DateTime.Now

        '        Dim dt3 As DateTime = DateTime.Parse(e.Row.Cells(6).Text)
        '        Dim ts1 As New TimeSpan(cno, 0, 0, 0)
        '        Dim dt2 As DateTime = dt1 + ts1

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



        '    If e.Row.RowType = DataControlRowType.DataRow Then



        '        e.Row.Cells(6).Text = wday2

        '        Dim dt3 As DateTime = DateTime.Parse(e.Row.Cells(6).Text)
        '        Dim ts1 As New TimeSpan(cno, 0, 0, 0)
        '        Dim dt2 As DateTime = dt1 + ts1


        '        If dt3 < dt2 Then



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

        'End If



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

        If e.Row.Cells(20).Text = "1" Then
            e.Row.BackColor = Drawing.Color.DarkGray
        End If


        e.Row.Cells(0).Width = 10
        e.Row.Cells(1).Width = 120
        e.Row.Cells(2).Width = 50
        e.Row.Cells(3).Width = 100
        e.Row.Cells(4).Width = 70
        e.Row.Cells(5).Width = 100
        e.Row.Cells(6).Width = 110
        e.Row.Cells(7).Width = 70
        e.Row.Cells(8).Width = 70
        e.Row.Cells(9).Width = 70
        e.Row.Cells(10).Width = 70
        e.Row.Cells(11).Width = 50
        e.Row.Cells(12).Width = 60
        e.Row.Cells(13).Width = 60
        e.Row.Cells(14).Width = 120
        e.Row.Cells(15).Width = 10
        e.Row.Cells(16).Width = 120
        e.Row.Cells(17).Width = 10
        e.Row.Cells(18).Width = 150

        e.Row.Cells(6).Visible = False
        e.Row.Cells(7).Visible = False
        e.Row.Cells(10).Visible = False
        e.Row.Cells(11).Visible = False
        e.Row.Cells(12).Visible = False
        e.Row.Cells(13).Visible = False

        e.Row.Cells(20).Visible = False



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



        'データベース接続を開く
        cnn.Open()




        '表示ボタン　FLG03は表示
        Dim I As Integer
        For I = 0 To GridView1.Rows.Count - 1
            If CType(GridView1.Rows(I).Cells(0).Controls(1), CheckBox).Checked Then


                'FIN_FLGを更新
                strSQL = ""
                strSQL = strSQL & "UPDATE T_EXL_LCLTENKAI SET FLG03 ='1' "
                strSQL = strSQL & "WHERE CUST = '" & GridView1.Rows(I).Cells(4).Text & "'"
                strSQL = strSQL & "AND ETD = '" & GridView1.Rows(I).Cells(9).Text & "'"
                strSQL = strSQL & "AND LCL_SIZE = '" & GridView1.Rows(I).Cells(11).Text & "'"


                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

                '            Response.Redirect("anken_booking02.aspx")

                Call GET_IVDATA(GridView1.Rows(I).Cells(6).Text, 1)
                Call GET_IVDATA2(Left(GridView1.Rows(I).Cells(5).Text, 4), 1)

            Else



            End If
        Next


        GridView1.DataBind()


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




        '非表示ボタン　FLG03は非表示
        Dim I As Integer
        For I = 0 To GridView1.Rows.Count - 1
            If CType(GridView1.Rows(I).Cells(0).Controls(1), CheckBox).Checked Then




                strSQL = ""
                strSQL = strSQL & "UPDATE T_EXL_LCLTENKAI SET FLG03 ='' "
                strSQL = strSQL & "WHERE CUST = '" & GridView1.Rows(I).Cells(4).Text & "'"
                strSQL = strSQL & "AND ETD = '" & GridView1.Rows(I).Cells(9).Text & "'"
                strSQL = strSQL & "AND LCL_SIZE = '" & GridView1.Rows(I).Cells(11).Text & "'"

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

                '            Response.Redirect("anken_booking02.aspx")


                Call GET_IVDATA(GridView1.Rows(I).Cells(6).Text, 2)
                Call GET_IVDATA2(Left(GridView1.Rows(I).Cells(5).Text, 4), 2)


            Else



            End If
        Next


        GridView1.DataBind()


    End Sub



    Private Sub GET_IVDATA(bkgno As String, A As String)

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

            If A = "1" Then


                Call INS_LCL(strinv, bkgno)

            Else

                Call DEL_LCL(strinv, bkgno)

            End If

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

            bkgno = Convert.ToString(dataread("OLD_INVNO"))        'ETD(計上日)

            If A = "1" Then


                Call INS_LCL(strinv, bkgno)

            Else

                Call DEL_LCL(strinv, bkgno)

            End If

        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
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


    Private Sub DEL_LCL(strinv As String, bkgno As String)
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
            strSQL = strSQL & "T_EXL_CSWORKSTATUS.LCLFIN_INVNO = '', "
            strSQL = strSQL & "T_EXL_CSWORKSTATUS.LCLFIN_REGDATE = '', "
            strSQL = strSQL & "T_EXL_CSWORKSTATUS.LCLFIN_BKGNO = '' "
            strSQL = strSQL & "WHERE T_EXL_CSWORKSTATUS.LCLFIN_INVNO ='" & strinv & "' "


            Command.CommandText = strSQL
            ' SQLの実行
            Command.ExecuteNonQuery()

        Else



        End If





        cnn.Close()
        cnn.Dispose()

    End Sub

End Class
