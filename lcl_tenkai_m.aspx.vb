


Imports System.Data.SqlClient
Imports System.Data
Imports mod_function
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

        Dim flgship As Long = 0

        If e.Row.Cells(18).Text = "1" Then
            e.Row.BackColor = Drawing.Color.DarkGray
        End If

        If e.Row.RowType = DataControlRowType.DataRow Then

            'e.Row.Cells(16).Text = Replace(e.Row.Cells(16).Text, "__", "<br>")
            e.Row.Cells(17).Text = Replace(e.Row.Cells(17).Text, "__", "<br>")
            If Replace(e.Row.Cells(11).Text, "&nbsp;", "") <> "" Or Replace(e.Row.Cells(12).Text, "&nbsp;", "") <> "" Then

            Else
                e.Row.Cells(0).BackColor = Drawing.Color.Salmon
                e.Row.Cells(1).BackColor = Drawing.Color.Salmon
            End If

        End If

        flgship = 0
        flgship = GET_shipsch(Trim(e.Row.Cells(5).Text))

        If flgship = 1 Then
            e.Row.Cells(3).BackColor = Drawing.Color.Red
            e.Row.Cells(3).ForeColor = Drawing.Color.White
            e.Row.Cells(2).BackColor = Drawing.Color.Red
            e.Row.Cells(2).ForeColor = Drawing.Color.White
            e.Row.Cells(2).Text = "スケジュール未登録"
        End If


        e.Row.Cells(6).Visible = False
        e.Row.Cells(9).Visible = False
        e.Row.Cells(10).Visible = False
        e.Row.Cells(11).Visible = False
        e.Row.Cells(12).Visible = False
        e.Row.Cells(18).Visible = False

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim dltButton As ImageButton = e.Row.FindControl("ImageButton1")
            'ボタンが存在する場合のみセット
            If Not (dltButton Is Nothing) Then
                dltButton.CommandArgument = e.Row.RowIndex.ToString()
            End If
        End If

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

        Dim dt1 As DateTime = DateTime.Now

        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim intCnt As Long

        'データベース接続を開く
        cnn.Open()

        '表示ボタン　FLG03は表示
        Dim I As Integer
        For I = 0 To GridView1.Rows.Count - 1
            If CType(GridView1.Rows(I).Cells(0).Controls(1), CheckBox).Checked Then

                'FIN_FLGを更新
                strSQL = ""
                strSQL = strSQL & "UPDATE T_EXL_LCLTENKAI SET FLG03 ='1' "
                strSQL = strSQL & "WHERE CUST = '" & GridView1.Rows(I).Cells(3).Text & "'"
                strSQL = strSQL & "AND ETD = '" & GridView1.Rows(I).Cells(8).Text & "'"
                strSQL = strSQL & "AND LCL_SIZE = '" & GridView1.Rows(I).Cells(10).Text & "'"
                strSQL = strSQL & "AND FLG01 <> '1' "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

                Call GET_IVDATA(Trim(GridView1.Rows(I).Cells(5).Text), 1)
                Call GET_IVDATA2(Left(GridView1.Rows(I).Cells(4).Text, 4), 1)
                Call GET_IVDATA3(Left(GridView1.Rows(I).Cells(4).Text, 4), Trim(GridView1.Rows(I).Cells(5).Text), 1)

                strSQL = ""
                strSQL = strSQL & "SELECT COUNT(*) AS RecCnt FROM T_EXL_LCLCUSTPREADS WHERE "
                strSQL = strSQL & "T_EXL_LCLCUSTPREADS.CUSTCODE = '" & GridView1.Rows(I).Cells(3).Text & "' "


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
                    strSQL = strSQL & "UPDATE T_EXL_LCLCUSTPREADS SET ADDRESS ='" & Replace(GridView1.Rows(I).Cells(17).Text, "<br>", "__") & "' "
                    strSQL = strSQL & "WHERE CUSTCODE = '" & GridView1.Rows(I).Cells(3).Text & "'"
                Else
                    strSQL = ""
                    strSQL = strSQL & "INSERT INTO T_EXL_LCLCUSTPREADS VALUES("
                    strSQL = strSQL & "'" & GridView1.Rows(I).Cells(3).Text & "' "
                    strSQL = strSQL & ",'" & Replace(GridView1.Rows(I).Cells(17).Text, "<br>", "__") & "' "
                    strSQL = strSQL & ")"
                End If
                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()
            Else

            End If
        Next

        GridView1.DataBind()

        cnn.Close()
        cnn.Dispose()


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
                strSQL = strSQL & "WHERE CUST = '" & GridView1.Rows(I).Cells(3).Text & "'"
                strSQL = strSQL & "AND ETD = '" & GridView1.Rows(I).Cells(8).Text & "'"
                strSQL = strSQL & "AND LCL_SIZE = '" & GridView1.Rows(I).Cells(10).Text & "'"
                strSQL = strSQL & "AND FLG01 <> '1' "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

                Call GET_IVDATA(Trim(GridView1.Rows(I).Cells(5).Text), 2)
                Call GET_IVDATA2(Left(GridView1.Rows(I).Cells(4).Text, 4), 2)
                Call GET_IVDATA3(Left(GridView1.Rows(I).Cells(4).Text, 4), Trim(GridView1.Rows(I).Cells(5).Text), 2)
            Else
            End If
        Next

        GridView1.DataBind()

        cnn.Close()
        cnn.Dispose()


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

        'INVOICENOが最大のものを取得
        strSQL = strSQL & "AND T_INV_HD_TB.INVOICENO = "
        strSQL = strSQL & "(SELECT MAX(T_INV_HD_TB.INVOICENO) AS IVNO "
        strSQL = strSQL & "FROM T_INV_HD_TB "

        strSQL = strSQL & "WHERE T_INV_HD_TB.HEADTITLE Like 'INVOICE%' "
        strSQL = strSQL & "AND T_INV_HD_TB.OLD_INVNO like '%" & strinv & "%' "
        strSQL = strSQL & "AND T_INV_HD_TB.BLDATE BETWEEN '" & dt3 & "' AND '" & dt2 & "' "
        strSQL = strSQL & "AND T_INV_HD_TB.BOOKINGNO IS NOT NULL "
        strSQL = strSQL & "AND T_INV_HD_TB.ORG_INVOICENO IS NULL "
        strSQL = strSQL & ") "

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
                    Call DEL_LCL(strinv, bkgno)
                End If
            End While
            'クローズ処理 
            dataread.Close()
            dbcmd.Dispose()
        End If

        cnn.Close()
        cnn.Dispose()

    End Sub

    Private Sub GET_IVDATA3(strinv As String, strbkg As String, A As String)

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

        If strbkg = "" Or IsNothing(strbkg) = True Then
        Else
            If A = "1" Then
                Call INS_LCL(strinv, strbkg)
            Else
                Call DEL_LCL(strinv, strbkg)
            End If
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

        If bkgno = "" Or IsNothing(bkgno) = True Then

        Else

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
                strSQL = strSQL & ",'" & Session("UsrId") & "_09" & "' "
                strSQL = strSQL & ")"
            End If

            Command.CommandText = strSQL
            ' SQLの実行
            Command.ExecuteNonQuery()

        End If

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

        If bkgno = "" Or IsNothing(bkgno) = True Then

        Else

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
                strSQL = strSQL & "DELETE FROM T_EXL_WORKSTATUS00 "
                strSQL = strSQL & "WHERE T_EXL_WORKSTATUS00.ID = '005' "
                strSQL = strSQL & "AND T_EXL_WORKSTATUS00.INVNO = '" & strinv & "' "
                strSQL = strSQL & "AND T_EXL_WORKSTATUS00.BKGNO = '" & bkgno & "' "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()
            Else
            End If
        End If

        cnn.Close()
        cnn.Dispose()

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        'メールの送信に必要な情報
        Dim smtpHostName As String = "svsmtp01.exedy.co.jp"
        Dim smtpPort As Integer = 25

        ' メールの内容

        Dim struid As String = Session("UsrId")
        Dim strfrom As String = GET_from(struid)

        Dim cflg As Long

        'Dim strto As String = GET_ToAddress(1, 1) '宛先
        Dim strto As String = GET_ToAddress2("06", 1) '宛先
        strto = Left(strto, Len(strto) - 1)

        'Dim strcc As String = GET_ToAddress(1, 0) + GET_from(struid)  'CC 
        Dim strcc As String = GET_ToAddress2("06", 2) + GET_from(struid)  'CC 

        For I = 0 To GridView1.Rows.Count - 1
            If CType(GridView1.Rows(I).Cells(0).Controls(1), CheckBox).Checked Then
                cflg = 1
            Else
            End If
        Next

        If cflg = 1 Then

            'メールの件名
            Dim subject As String = "<通知>LCL案件展開　変更・追加・連絡" '"【AIR " & strIrai & "依頼" & Session("strCust") & "向け】"

            'メールの本文
            Dim body As String = "<html><body><p>各位<p>お世話になっております。<p>LCL出荷案件を展開させていただきます。</p><p>荷量の記入・希望引き取り日時・港搬入用のトラック手配をお願いいたします。</p><p>下記URLにて重量の登録をお願いいたします。登録完了後に通知メールを送信してください。</p>http://kbhwpm01/exp/cs_home/lcl_tenkai.aspx</p><p></p></body></html>" ' UriBodyC()

            Dim t As String = "<html><body><Table border='1' style='Font-Size:12px;font-family:Meiryo UI;'><tr style='background-color: #6fbfd1;'><td>備考</td><td>客先</td><td>IN_NO</td><td>カット日</td><td>出港日</td><td>M3</td><td>重量</td><td>荷量</td><td>引取希望日</td><td></td><td>搬入希望日</td><td></td><td>搬入先</td></tr>"

            For I = 0 To GridView1.Rows.Count - 1
                If CType(GridView1.Rows(I).Cells(0).Controls(1), CheckBox).Checked Then
                    't = t & "<tr><td style='background-color: #FF9E8C;'>" & GridView1.Rows(I).Cells(2).Text & "</td><td style='background-color: #FF9E8C;'>" & GridView1.Rows(I).Cells(3).Text & "</td><td style='background-color: #FF9E8C;'>" & GridView1.Rows(I).Cells(4).Text & "</td><td style='background-color: #FF9E8C;'>" & GridView1.Rows(I).Cells(7).Text & "</td><td style='background-color: #FF9E8C;'>" & GridView1.Rows(I).Cells(9).Text & "</td><td style='background-color: #FF9E8C;'>" & GridView1.Rows(I).Cells(10).Text & "</td><td style='background-color: #FF9E8C;'>" & GridView1.Rows(I).Cells(11).Text & "</td><td style='background-color: #FF9E8C;'>" & GridView1.Rows(I).Cells(12).Text & "</td><td style='background-color: #FF9E8C;'>" & GridView1.Rows(I).Cells(13).Text & "</td><td style='background-color: #FF9E8C;'>" & GridView1.Rows(I).Cells(14).Text & "</td><td style='background-color: #FF9E8C;'>" & GridView1.Rows(I).Cells(15).Text & "</td><td style='background-color: #FF9E8C;'>" & GridView1.Rows(I).Cells(16).Text & "</td><td style='background-color: #FF9E8C;'>" & GridView1.Rows(I).Cells(17).Text & "</td></tr>"
                    t = t & "<tr><td>" & GridView1.Rows(I).Cells(2).Text & "</td><td>" & GridView1.Rows(I).Cells(3).Text & "</td><td>" & GridView1.Rows(I).Cells(4).Text & "</td><td>" & GridView1.Rows(I).Cells(7).Text & "</td><td>" & GridView1.Rows(I).Cells(8).Text & "</td><td>" & GridView1.Rows(I).Cells(10).Text & "</td><td>" & GridView1.Rows(I).Cells(11).Text & "</td><td>" & GridView1.Rows(I).Cells(12).Text & "</td><td>" & GridView1.Rows(I).Cells(13).Text & "</td><td>" & GridView1.Rows(I).Cells(14).Text & "</td><td>" & GridView1.Rows(I).Cells(15).Text & "</td><td>" & GridView1.Rows(I).Cells(16).Text & "</td><td>" & GridView1.Rows(I).Cells(17).Text & "</td></tr>"
                Else
                    '                    t = t & "<tr><td>" & GridView1.Rows(I).Cells(2).Text & "</td><td>" & GridView1.Rows(I).Cells(3).Text & "</td><td>" & GridView1.Rows(I).Cells(4).Text & "</td><td>" & GridView1.Rows(I).Cells(7).Text & "</td><td>" & GridView1.Rows(I).Cells(9).Text & "</td><td>" & GridView1.Rows(I).Cells(10).Text & "</td><td>" & GridView1.Rows(I).Cells(11).Text & "</td><td>" & GridView1.Rows(I).Cells(12).Text & "</td><td>" & GridView1.Rows(I).Cells(13).Text & "</td><td>" & GridView1.Rows(I).Cells(14).Text & "</td><td>" & GridView1.Rows(I).Cells(15).Text & "</td><td>" & GridView1.Rows(I).Cells(16).Text & "</td><td>" & GridView1.Rows(I).Cells(17).Text & "</td></tr>"
                End If
            Next


            t = t & "</Table></body></html>"

            body = body & t

            Dim body2 As String = "====内容==================================</p>" & TextBox1.Text.Replace(vbCrLf, "<br/>") & "</p>" ' UriBodyC()

            body = body & body2

            body = "<font size=" & Chr(34) & "2" & Chr(34) & ">" & body & "</font>"
            body = "<font face=" & Chr(34) & "Meiryo UI" & Chr(34) & ">" & body & "</font>"

            ' MailKit におけるメールの情報
            Dim message = New MimeKit.MimeMessage()

            ' 送り元情報  
            message.From.Add(MailboxAddress.Parse(strfrom))


            ' 宛先情報  
            If strto <> "" Then
                'カンマ区切りをSPLIT
                Dim strVal() As String = strto.Split(",")
                For Each c In strVal
                    message.To.Add(New MailboxAddress("", c))
                Next
            End If


            If strcc <> "" Then
                'カンマ区切りをSPLIT
                Dim strVal() As String = strcc.Split(",")
                For Each c In strVal
                    message.Cc.Add(New MailboxAddress("", c))
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

            Call button00()
            Page.ClientScript.RegisterStartupScript(Me.GetType, "確認", "<script language='JavaScript'>confirm('メールを送信しました。');</script>", False)

            TextBox1.Text = ""

        Else


            Page.ClientScript.RegisterStartupScript(Me.GetType, "確認", "<script language='JavaScript'>confirm('新規案件にチェックが入っていません。メールは送信されません。');</script>", False)


        End If


    End Sub


    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load

        Button3.Attributes.Add("onclick", "return confirm('メールを送付します。よろしいですか？');")
        GridView1.Columns(16).ItemStyle.Wrap = True
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

    Private Function GET_ToAddress2(strkbn As String, strtocc As String) As String
        'BCCメールアドレス情報を取得
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String

        GET_ToAddress2 = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()


        strSQL = strSQL & "SELECT MAIL_ADD FROM M_EXL_MAIL01 "

        strSQL = strSQL & "WHERE TASK_CD = '" & strkbn & "' "
        strSQL = strSQL & "AND FLG = '" & strtocc & "' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        strDate = ""
        '結果を取り出す 
        While (dataread.Read())
            GET_ToAddress2 += dataread("MAIL_ADD") + ","
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


    Private Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand

        If e.CommandName = "edt" Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim data0 = Me.GridView1.Rows(index).Cells(2).Text
            Dim data1 = Me.GridView1.Rows(index).Cells(3).Text
            Dim data2 = Me.GridView1.Rows(index).Cells(4).Text
            Dim data3 = Me.GridView1.Rows(index).Cells(5).Text
            Dim data4 = Me.GridView1.Rows(index).Cells(7).Text
            Dim data5 = Me.GridView1.Rows(index).Cells(8).Text
            Dim data6 = Me.GridView1.Rows(index).Cells(9).Text

            Dim data7 = Me.GridView1.Rows(index).Cells(10).Text
            Dim data8 = Me.GridView1.Rows(index).Cells(11).Text
            Dim data9 = Me.GridView1.Rows(index).Cells(12).Text

            Dim data10 = Me.GridView1.Rows(index).Cells(13).Text
            Dim data11 = Me.GridView1.Rows(index).Cells(14).Text
            Dim data12 = Me.GridView1.Rows(index).Cells(15).Text
            Dim data13 = Me.GridView1.Rows(index).Cells(16).Text
            Dim data14 = Me.GridView1.Rows(index).Cells(17).Text
            Dim data15 = Me.GridView1.Rows(index).Cells(18).Text

            Session("strMode") = "0"    '更新モード

            Session("lstrbokou") = data0
            Session("lstrcust") = data1
            Session("lstrinv") = data2
            Session("lstrbkg") = data3
            Session("lstrcut") = data4
            Session("lstretd") = data5
            Session("lstreta") = data6
            Session("lstrm3") = data7
            Session("lstrwhg") = data8
            Session("lstrpkg") = data9
            Session("lstrp1") = data10
            Session("lstrp2") = data11
            Session("lstrm1") = data12
            Session("lstrm2") = data13
            Session("lstrpp") = data14
            Session("lstrf3") = data15

            'Dim clientScript As String = "<script language='JavaScript'> window.open('shippingmemo_detail.aspx', '', 'width=1500,height=450','scrollbars=no','status=no','toolbar=no','location=no','menubar=no','resizable=no') <" + "/script>"
            'Dim startupScript As String = "<script language='JavaScript'>  window.open('shippingmemo_detail.aspx') <" + "/script>"

            'RegisterClientScriptBlock("client", clientScript)

            Response.Redirect("lcl_tenkai_m_detail.aspx")

        End If

    End Sub


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


    Private Sub button00()

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=KBHWPM02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand
        Dim strSQL As String = ""
        Dim ivno As String = ""

        Dim dt1 As DateTime = DateTime.Now

        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim intCnt As Long

        'データベース接続を開く
        cnn.Open()

        '表示ボタン　FLG03は表示
        Dim I As Integer
        For I = 0 To GridView1.Rows.Count - 1
            If CType(GridView1.Rows(I).Cells(0).Controls(1), CheckBox).Checked Then

                'FIN_FLGを更新
                strSQL = ""
                strSQL = strSQL & "UPDATE T_EXL_LCLTENKAI SET FLG03 ='1' "
                strSQL = strSQL & "WHERE CUST = '" & GridView1.Rows(I).Cells(3).Text & "'"
                strSQL = strSQL & "AND ETD = '" & GridView1.Rows(I).Cells(8).Text & "'"
                strSQL = strSQL & "AND LCL_SIZE = '" & GridView1.Rows(I).Cells(10).Text & "'"
                strSQL = strSQL & "AND FLG01 <> '1' "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

                Call GET_IVDATA(Trim(GridView1.Rows(I).Cells(5).Text), 1)
                Call GET_IVDATA2(Left(GridView1.Rows(I).Cells(4).Text, 4), 1)
                Call GET_IVDATA3(Left(GridView1.Rows(I).Cells(4).Text, 4), Trim(GridView1.Rows(I).Cells(5).Text), 1)

                strSQL = ""
                strSQL = strSQL & "SELECT COUNT(*) AS RecCnt FROM T_EXL_LCLCUSTPREADS WHERE "
                strSQL = strSQL & "T_EXL_LCLCUSTPREADS.CUSTCODE = '" & GridView1.Rows(I).Cells(3).Text & "' "


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
                    strSQL = strSQL & "UPDATE T_EXL_LCLCUSTPREADS SET ADDRESS ='" & Replace(GridView1.Rows(I).Cells(17).Text, "<br>", "__") & "' "
                    strSQL = strSQL & "WHERE CUSTCODE = '" & GridView1.Rows(I).Cells(3).Text & "'"
                Else
                    strSQL = ""
                    strSQL = strSQL & "INSERT INTO T_EXL_LCLCUSTPREADS VALUES("
                    strSQL = strSQL & "'" & GridView1.Rows(I).Cells(3).Text & "' "
                    strSQL = strSQL & ",'" & Replace(GridView1.Rows(I).Cells(17).Text, "<br>", "__") & "' "
                    strSQL = strSQL & ")"
                End If
                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()
            Else

            End If
        Next

        GridView1.DataBind()

        cnn.Close()
        cnn.Dispose()


    End Sub

End Class
