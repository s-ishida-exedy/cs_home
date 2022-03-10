
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

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand

        'データベース接続を開く
        cnn.Open()
        If e.Row.RowType = DataControlRowType.DataRow Then
            'strSQL = "SELECT ITK_BKGNO FROM [T_EXL_CSWORKSTATUS] WHERE [T_EXL_CSWORKSTATUS].ITK_BKGNO = '" & Trim(e.Row.Cells(9).Text) & "' "

            strSQL = "SELECT BKGNO FROM [T_EXL_WORKSTATUS00] WHERE [T_EXL_WORKSTATUS00].BKGNO = '" & Trim(e.Row.Cells(9).Text) & "' "
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
                If Trim(e.Row.Cells(9).Text) = strbkg Then
                    Call itaku(e.Row.Cells(9).Text)
                End If

            End While

            'クローズ処理
            dataread.Close()
            dbcmd.Dispose()

        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            strSQL = "SELECT FLG02 FROM [T_EXL_CSANKEN] WHERE [T_EXL_CSANKEN].BOOKING_NO = '" & Trim(e.Row.Cells(9).Text) & "' "
            'ＳＱＬコマンド作成
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行
            dataread = dbcmd.ExecuteReader()

            strbkg = ""
            '結果を取り出す
            While (dataread.Read())
                strbkg += dataread("FLG02")
            End While

            If strbkg = "1" Then
                e.Row.Cells(1).Text = "委託登録済"
                e.Row.BackColor = Drawing.Color.DarkSalmon
            End If
        End If

        e.Row.Cells(10).Visible = False
        e.Row.Cells(11).Visible = False
        e.Row.Cells(12).Visible = False
        e.Row.Cells(13).Visible = False
        e.Row.Cells(14).Visible = False
        e.Row.Cells(15).Visible = False
        e.Row.Cells(16).Visible = False
        e.Row.Cells(17).Visible = False
        e.Row.Cells(18).Visible = False


        cnn.Close()
        cnn.Dispose()

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

        Dim madef01 As String = ""

        Call makefld(madef01)

        Page.ClientScript.RegisterStartupScript(Me.GetType, "確認", "<script language='JavaScript'>confirm('" & madef01 & "');</script>", False)


    End Sub
    Private Sub get_itakuhanntei(i As String, ByRef itkflg As String, ivno As String)

        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String
        Dim strinv As String

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

                itkflg = 1

            End If
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()

    End Sub

    Private Sub copy_custfile(iptbx As String, ByRef Cname As String, ByRef Ccode As String)

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
        Dim strDate As String
        Dim strinv As String

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

                Call GET_IVDATA(GridView1.Rows(I).Cells(9).Text, "1")
            Else
            End If
        Next
        GridView1.DataBind()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

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
                strSQL = strSQL & "UPDATE T_EXL_CSANKEN SET FLG02 ='' "
                strSQL = strSQL & "WHERE BOOKING_NO = '" & GridView1.Rows(I).Cells(9).Text & "'"

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

                Call GET_IVDATA(GridView1.Rows(I).Cells(9).Text, "2")
            Else
            End If
        Next
        GridView1.DataBind()

    End Sub

    Private Sub GET_IVDATA(bkgno As String, flg As String)

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
            If flg = "1" Then
                Call INS_ITK(strinv, bkgno)
            ElseIf flg = "2"
                Call DEL_ITK(strinv, bkgno)
            End If
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()

    End Sub


    Private Sub INS_ITK(strinv As String, bkgno As String)
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

        'strSQL = ""
        'strSQL = strSQL & "SELECT COUNT(*) AS RecCnt FROM T_EXL_CSWORKSTATUS WHERE "
        'strSQL = strSQL & "T_EXL_CSWORKSTATUS.ITK_INVNO = '" & strinv & "' "
        'strSQL = strSQL & "AND T_EXL_CSWORKSTATUS.ITK_BKGNO = '" & bkgno & "' "

        strSQL = ""
        strSQL = strSQL & "SELECT COUNT(*) AS RecCnt FROM T_EXL_WORKSTATUS00 WHERE "
        strSQL = strSQL & "T_EXL_WORKSTATUS00.ID = '001' "
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

            'strSQL = ""
            'strSQL = strSQL & "UPDATE T_EXL_CSWORKSTATUS SET "
            'strSQL = strSQL & "T_EXL_CSWORKSTATUS.ITK_INVNO = '" & strinv & "', "
            'strSQL = strSQL & "T_EXL_CSWORKSTATUS.ITK_REGDATE = '" & Format(Now(), "yyyy/MM/dd") & "', "
            'strSQL = strSQL & "T_EXL_CSWORKSTATUS.ITK_BKGNO = '" & bkgno & "' "
            'strSQL = strSQL & "WHERE T_EXL_CSWORKSTATUS.ITK_INVNO ='" & strinv & "' "

            strSQL = ""
            strSQL = strSQL & "UPDATE T_EXL_WORKSTATUS00 SET "
            strSQL = strSQL & "T_EXL_WORKSTATUS00.ID = '001', "
            strSQL = strSQL & "T_EXL_WORKSTATUS00.INVNO = '" & strinv & "', "
            strSQL = strSQL & "T_EXL_WORKSTATUS00.REGDATE = '" & Format(Now(), "yyyy/MM/dd") & "', "
            strSQL = strSQL & "T_EXL_WORKSTATUS00.BKGNO = '" & bkgno & "' "
            strSQL = strSQL & "WHERE T_EXL_WORKSTATUS00.INVNO ='" & strinv & "' "
            strSQL = strSQL & "AND T_EXL_WORKSTATUS00.BKGNO = '" & bkgno & "' "


        Else
            'strSQL = ""
            'strSQL = strSQL & "INSERT INTO T_EXL_CSWORKSTATUS VALUES("

            'strSQL = strSQL & " '" & "' "
            'strSQL = strSQL & ",'" & " ' "
            'strSQL = strSQL & ",'" & " ' "

            'strSQL = strSQL & ",'" & " ' "
            'strSQL = strSQL & ",'" & " ' "
            'strSQL = strSQL & ",'" & " ' "

            'strSQL = strSQL & ",'" & " ' "
            'strSQL = strSQL & ",'" & " ' "
            'strSQL = strSQL & ",'" & " ' "

            'strSQL = strSQL & ",'" & " ' "
            'strSQL = strSQL & ",'" & " ' "
            'strSQL = strSQL & ",'" & " ' "

            'strSQL = strSQL & ",'" & strinv & "' "
            'strSQL = strSQL & ",'" & Format(Now(), "yyyy/MM/dd") & "' "
            'strSQL = strSQL & ",'" & bkgno & "' "

            'strSQL = strSQL & ")"

            strSQL = ""
            strSQL = strSQL & "INSERT INTO T_EXL_WORKSTATUS00 VALUES("
            strSQL = strSQL & " '001' "
            strSQL = strSQL & ",'" & strinv & "' "
            strSQL = strSQL & ",'" & bkgno & "' "
            strSQL = strSQL & ",'" & Format(Now(), "yyyy/MM/dd") & "' "
            strSQL = strSQL & ")"



        End If

        Command.CommandText = strSQL
        ' SQLの実行
        Command.ExecuteNonQuery()

        cnn.Close()
        cnn.Dispose()

    End Sub

    Private Sub DEL_ITK(strinv As String, bkgno As String)
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

        'strSQL = ""
        'strSQL = strSQL & "SELECT COUNT(*) AS RecCnt FROM T_EXL_CSWORKSTATUS WHERE "
        'strSQL = strSQL & "T_EXL_CSWORKSTATUS.ITK_INVNO = '" & strinv & "' "
        'strSQL = strSQL & "AND T_EXL_CSWORKSTATUS.ITK_BKGNO = '" & bkgno & "' "

        strSQL = ""
        strSQL = strSQL & "SELECT COUNT(*) AS RecCnt FROM T_EXL_WORKSTATUS00 WHERE "
        strSQL = strSQL & "T_EXL_WORKSTATUS00.ID = '001' "
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
            'strSQL = ""
            'strSQL = strSQL & "DELETE FROM T_EXL_CSWORKSTATUS "
            'strSQL = strSQL & "WHERE T_EXL_CSWORKSTATUS.ITK_INVNO ='" & strinv & "' "
            'strSQL = strSQL & "AND T_EXL_CSWORKSTATUS.ITK_BKGNO ='" & bkgno & "' "

            strSQL = ""
            strSQL = strSQL & "DELETE FROM T_EXL_WORKSTATUS00 "
            strSQL = strSQL & "WHERE T_EXL_WORKSTATUS00.ID = '001' "
            strSQL = strSQL & "AND T_EXL_WORKSTATUS00.INVNO = '" & strinv & "' "
            strSQL = strSQL & "AND T_EXL_WORKSTATUS00.BKGNO ='" & bkgno & "' "


            Command.CommandText = strSQL
            ' SQLの実行
            Command.ExecuteNonQuery()
        Else
        End If

        cnn.Close()
        cnn.Dispose()

    End Sub

    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load

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
        Dim strkd As String
        Dim stram As String

        Dim dt1 As DateTime = DateTime.Now

        'データベース接続を開く
        cnn.Open()

        strSQL = ""
        strSQL = strSQL & "SELECT T_EXL_ANKENCK.FLG01,T_EXL_ANKENCK.FLG02 FROM T_EXL_ANKENCK "
        strSQL = strSQL & "WHERE T_EXL_ANKENCK.FLG03 ='1' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        While (dataread.Read())
            strkd = Trim(dataread("FLG01"))
            stram = Trim(dataread("FLG02"))
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()

        cnn.Close()
        cnn.Dispose()

        If Label3.Text = "" Then
            If strkd = "1" Then
                CheckBox1.Checked = True
                Label3.Text = "済"
            Else
                CheckBox1.Checked = False
                Label3.Text = "未"
            End If
            If stram = "1" Then
                CheckBox2.Checked = True
                Label4.Text = "済"
            Else
                CheckBox2.Checked = False
                Label4.Text = "未"
            End If

        Else
            If strkd = "1" Then
                If CheckBox1.Checked = True Then
                    CheckBox1.Checked = True
                    Label3.Text = "済"
                End If
            Else
                If CheckBox1.Checked = False Then
                    CheckBox1.Checked = False
                    Label3.Text = "未"
                End If
            End If

            If stram = "1" Then
                If CheckBox2.Checked = True Then
                    CheckBox2.Checked = True
                    Label4.Text = "済"
                End If
            Else
                If CheckBox2.Checked = False Then
                    CheckBox2.Checked = False
                    Label4.Text = "未"
                End If
            End If
        End If

        If CheckBox1.Checked = True And CheckBox2.Checked = True Then
            Label7.Text = "〇"

            Dim madef01 As String

            Call makefld(madef01)

        Else
            Label7.Text = "×"
        End If

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged

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

        Dim kbn As String = "A"

        Dim dt1 As DateTime = DateTime.Now

        'データベース接続を開く
        cnn.Open()

        If CheckBox1.Checked = True Then
            strSQL = ""
            strSQL = strSQL & "UPDATE T_EXL_ANKENCK SET "
            strSQL = strSQL & "T_EXL_ANKENCK.FLG01 = '1' "
            strSQL = strSQL & "WHERE T_EXL_ANKENCK.FLG03 ='1' "

            Command.CommandText = strSQL
            ' SQLの実行
            Command.ExecuteNonQuery()
        Else
            strSQL = ""
            strSQL = strSQL & "UPDATE T_EXL_ANKENCK SET "
            strSQL = strSQL & "T_EXL_ANKENCK.FLG01 = '0' "
            strSQL = strSQL & "WHERE T_EXL_ANKENCK.FLG03 ='1' "

            Command.CommandText = strSQL
            ' SQLの実行

            Command.ExecuteNonQuery()
        End If

        'クローズ処理 

        cnn.Close()
        cnn.Dispose()

        If CheckBox1.Checked = True Then
            Label3.Text = "済"
            Call Mail03(kbn)

        Else
            Label3.Text = "未"
        End If



    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged

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
        Dim kbn As String = "K"

        'データベース接続を開く
        cnn.Open()

        If CheckBox2.Checked = True Then
            strSQL = ""
            strSQL = strSQL & "UPDATE T_EXL_ANKENCK SET "
            strSQL = strSQL & "T_EXL_ANKENCK.FLG02 = '1' "
            strSQL = strSQL & "WHERE T_EXL_ANKENCK.FLG03 ='1' "

            Command.CommandText = strSQL
            ' SQLの実行
            Command.ExecuteNonQuery()
        Else
            strSQL = ""
            strSQL = strSQL & "UPDATE T_EXL_ANKENCK SET "
            strSQL = strSQL & "T_EXL_ANKENCK.FLG02 = '0' "
            strSQL = strSQL & "WHERE T_EXL_ANKENCK.FLG03 ='1' "

            Command.CommandText = strSQL
            ' SQLの実行

            Command.ExecuteNonQuery()
        End If
        'クローズ処理 

        cnn.Close()
        cnn.Dispose()

        If CheckBox2.Checked = True Then
            Label4.Text = "済"
            Call Mail03(kbn)
        Else
            Label4.Text = "未"
        End If

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click


        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String
        Dim strinv As String
        Dim cno As Long
        Dim wno As Long
        Dim wday As String
        Dim wday2 As String

        Dim Kaika00 As String

        Dim deccnt As Long
        Dim lng14 As Long
        Dim lng15 As Long

        Dim WDAYNO00 As String
        Dim WDAY00 As String
        Dim WDAY01 As String

        Dim dt1 As DateTime = DateTime.Now


        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()



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


        Dim yusen As String = ""
        Dim kin As String = ""

        Call Mail01(WDAY00, WDAY01, yusen)
        Call Mail02(WDAY00, WDAY01, kin)

        If yusen <> "" And kin <> "" Then

            Page.ClientScript.RegisterStartupScript(Me.GetType, "確認", "<script language='JavaScript'>confirm('郵船、近鉄にメールを送信しました。');</script>", False)

        ElseIf yusen <> "" And kin = "" Then

            Page.ClientScript.RegisterStartupScript(Me.GetType, "確認", "<script language='JavaScript'>confirm('郵船にメールを送信しました。');</script>", False)


        ElseIf yusen = "" And kin <> "" Then

            Page.ClientScript.RegisterStartupScript(Me.GetType, "確認", "<script language='JavaScript'>confirm('郵船、近鉄にメールを送信しました。');</script>", False)




        End If




    End Sub


    Sub Mail01(WDAY00 As String, WDAY01 As String, ByRef r As String)
        '郵船


        'メールの送信に必要な情報
        Dim smtpHostName As String = "svsmtp01.exedy.co.jp"
        Dim smtpPort As Integer = 25

        ' メールの内容
        Dim strfrom As String = "r-fukao@exedy.com"
        Dim strto As String = "r-fukao@exedy.com"

        Dim f As String = ""


        'メールの件名
        'Dim strIrai As String = "" '"<通知>LCL案件展開　変更・追加・連絡"

        'メールの件名
        Dim subject As String = "【ご連絡】EXD通関委託　" & WDAY00 & "分"
        'message.Subject = ConvertBase64Subject(System.Text.Encoding.GetEncoding("csISO2022JP"), _MailTitle)

        'メールの本文
        Dim body As String = "<html><body><p>各位<p>お世話になっております。<p>" & WDAY01 & "弊社CUT分で通関委託がございます。</p>お忙しい中とは存じますが、宜しくお願い申し上げます。</p>以上になります。</body></html>" ' UriBodyC()


        Dim t As String = "<html><body><Table border='1' style='Font-Size:12px;'><tr><td>客先</td><td>INVOICE NO. / BKG NO.</td><td>積出港</td><td>仕向地</td></tr>"

        Call body01(t, "郵船")
        Call body02(t, "郵船", f)

        t = t & "</Table></body></html>"

        body = body & t

        Dim body2 As String = "</p>" ' UriBodyC()

        body = body & body2

        body = "<font size=" & Chr(34) & "2" & Chr(34) & ">" & body & "</font>"
        body = "<font face=" & Chr(34) & "Meiryo UI" & Chr(34) & ">" & body & "</font>"



        If f = "" Then


            body = "たいしょうなし"


        End If






        'Dim strFilePath As String = "" '"C:\exp\cs_home\upload\" & Session("strFile")

        'Using stream = File.OpenRead(strFilePath)
        ' MailKit におけるメールの情報
        Dim message = New MimeKit.MimeMessage()

        ' 送り元情報  
        message.From.Add(MailboxAddress.Parse(strfrom))


        ' 宛先情報  
        message.To.Add(MailboxAddress.Parse(strto))
        'If Session("strCC") <> "" Then

        '    message.Cc.Add(MailboxAddress.Parse(Session("strCC")))

        'End If


        ' 表題  
        message.Subject = subject

        ' 本文
        Dim textPart = New MimeKit.TextPart(MimeKit.Text.TextFormat.Html)
        textPart.Text = body
        message.Body = textPart


        ''添付ファイル
        'Dim path = strFilePath     '添付したいファイル
        '    Dim attachment = New MimeKit.MimePart("application", "pdf") _
        '    With {
        '        .Content = New MimeKit.MimeContent(System.IO.File.OpenRead(path)),
        '        .ContentDisposition = New MimeKit.ContentDisposition(),
        '        .ContentTransferEncoding = MimeKit.ContentEncoding.Base64,
        '        .FileName = System.IO.Path.GetFileName(path)
        '    }

        Dim multipart = New MimeKit.Multipart("mixed")

        multipart.Add(textPart)
        'multipart.Add(attachment)

        message.Body = multipart

        Using client As New MailKit.Net.Smtp.SmtpClient()
            client.Connect(smtpHostName, smtpPort, MailKit.Security.SecureSocketOptions.Auto)
            client.Send(message)
            client.Disconnect(True)
            client.Dispose()
            message.Dispose()
            r = "1y"
        End Using


        'stream.Dispose()
        'End Using

        'File.Delete(strFilePath)



    End Sub

    Sub Mail02(WDAY00 As String, WDAY01 As String, ByRef r As String)



        'メールの送信に必要な情報
        Dim smtpHostName As String = "svsmtp01.exedy.co.jp"
        Dim smtpPort As Integer = 25

        ' メールの内容
        Dim strfrom As String = "r-fukao@exedy.com"
        Dim strto As String = "r-fukao@exedy.com"
        Dim f As String = ""


        'メールの件名
        'Dim strIrai As String = "" '"<通知>LCL案件展開　変更・追加・連絡"

        'メールの件名
        Dim subject As String = "【ご連絡】EXD通関委託　" & WDAY00 & "分"
        'message.Subject = ConvertBase64Subject(System.Text.Encoding.GetEncoding("csISO2022JP"), _MailTitle)

        'メールの本文
        Dim body As String = "<html><body><p>各位<p>お世話になっております。<p>" & WDAY01 & "弊社CUT分で通関委託がございます。</p>お忙しい中とは存じますが、宜しくお願い申し上げます。</p>以上になります。</body></html>" ' UriBodyC()







        Dim t As String = "<html><body><Table border='1' style='Font-Size:12px;'><tr><td>客先</td><td>INVOICE NO. / BKG NO.</td><td>積出港</td><td>仕向地</td></tr>"

        Call body01(t, "近鉄")
        Call body02(t, "近鉄", f)

        t = t & "</Table></body></html>"

        body = body & t

        Dim body2 As String = "</p>" ' UriBodyC()

        body = body & body2

        body = "<font size=" & Chr(34) & "2" & Chr(34) & ">" & body & "</font>"
        body = "<font face=" & Chr(34) & "Meiryo UI" & Chr(34) & ">" & body & "</font>"




        'Dim strFilePath As String = "" '"C:\exp\cs_home\upload\" & Session("strFile")

        'Using stream = File.OpenRead(strFilePath)
        ' MailKit におけるメールの情報
        Dim message = New MimeKit.MimeMessage()

        ' 送り元情報  
        message.From.Add(MailboxAddress.Parse(strfrom))


        ' 宛先情報  
        message.To.Add(MailboxAddress.Parse(strto))
        'If Session("strCC") <> "" Then

        '    message.Cc.Add(MailboxAddress.Parse(Session("strCC")))

        'End If


        ' 表題  
        message.Subject = subject

        ' 本文
        Dim textPart = New MimeKit.TextPart(MimeKit.Text.TextFormat.Html)
        textPart.Text = body
        message.Body = textPart


        ''添付ファイル
        'Dim path = strFilePath     '添付したいファイル
        '    Dim attachment = New MimeKit.MimePart("application", "pdf") _
        '    With {
        '        .Content = New MimeKit.MimeContent(System.IO.File.OpenRead(path)),
        '        .ContentDisposition = New MimeKit.ContentDisposition(),
        '        .ContentTransferEncoding = MimeKit.ContentEncoding.Base64,
        '        .FileName = System.IO.Path.GetFileName(path)
        '    }

        Dim multipart = New MimeKit.Multipart("mixed")

        multipart.Add(textPart)
        'multipart.Add(attachment)

        message.Body = multipart

        If f <> "" Then


            Using client As New MailKit.Net.Smtp.SmtpClient()
                client.Connect(smtpHostName, smtpPort, MailKit.Security.SecureSocketOptions.Auto)
                client.Send(message)
                client.Disconnect(True)
                client.Dispose()
                message.Dispose()
                r = "1k"
            End Using

        Else



        End If


        'stream.Dispose()
        'End Using

        'File.Delete(strFilePath)



    End Sub


    Sub body01(ByRef t As String, A As String)


        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String
        Dim strinv As String

        Dim CUST As String = ""
        Dim INVOICE As String = ""
        Dim BOOKING_NO As String = ""
        Dim DISCHARGING_PORT As String = ""
        Dim PLACE_OF_DELIVERY As String = ""

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
        strSQL = strSQL & "SELECT T_EXL_CSANKEN.CUST, T_EXL_CSANKEN.INVOICE, T_EXL_CSANKEN.BOOKING_NO, T_EXL_CSANKEN.DISCHARGING_PORT, T_EXL_CSANKEN.PLACE_OF_DELIVERY "
        strSQL = strSQL & "FROM T_EXL_CSANKEN "
        strSQL = strSQL & "WHERE T_EXL_CSANKEN.FLG01 ='1' "
        strSQL = strSQL & "AND T_EXL_CSANKEN.FLG02 ='1' "
        strSQL = strSQL & "AND T_EXL_CSANKEN.FORWARDER ='" & A & "' "



        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()



        CUST = ""
        INVOICE = ""
        BOOKING_NO = ""
        DISCHARGING_PORT = ""
        PLACE_OF_DELIVERY = ""

        '結果を取り出す 
        While (dataread.Read())

            CUST = Convert.ToString(dataread("CUST"))
            INVOICE = Convert.ToString(dataread("INVOICE"))
            BOOKING_NO = Convert.ToString(dataread("BOOKING_NO"))
            DISCHARGING_PORT = Convert.ToString(dataread("DISCHARGING_PORT"))
            PLACE_OF_DELIVERY = Convert.ToString(dataread("PLACE_OF_DELIVERY"))


            t = t & "<tr><td >" & CUST & "</td><td>" & INVOICE & " / " & BOOKING_NO & "</td><td>" & DISCHARGING_PORT & "</td><td>" & PLACE_OF_DELIVERY & "</td></tr>"



        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()



        cnn.Close()
        cnn.Dispose()


    End Sub



    Sub body02(ByRef t As String, A As String, ByRef f As String)


        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String



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
        strSQL = strSQL & "SELECT T_EXL_CSANKEN.CUST, T_EXL_CSANKEN.INVOICE, T_EXL_CSANKEN.BOOKING_NO, T_EXL_CSANKEN.DISCHARGING_PORT, T_EXL_CSANKEN.PLACE_OF_DELIVERY "
        strSQL = strSQL & "FROM T_EXL_CSANKEN "
        strSQL = strSQL & "WHERE T_EXL_CSANKEN.FLG01 ='1' "
        strSQL = strSQL & "AND T_EXL_CSANKEN.FLG02 ='1' "
        strSQL = strSQL & "AND T_EXL_CSANKEN.FORWARDER ='" & A & "' "



        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()



        f = ""


        '結果を取り出す 
        While (dataread.Read())

            f = "1"



        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()



        cnn.Close()
        cnn.Dispose()


    End Sub

    Sub makefld(ByRef madef01 As String)


        Dim strPath00(3) As String      '依頼書、タイムスケジュールのパスと作成先のパス
        Dim strPath01(3) As String      '


        Dim MyStr As String
        Dim MyDate As Date
        Dim j, k As Long
        Dim lngEndRow As Long   '最終行

        Dim strLog As String            '問題報告ログ
        Dim strFile0 As String           'ファイル名(依頼書)
        Dim strFile1 As String           'ファイル名(タイムスケジュール)
        Dim strFol As String             'フォルダ名
        Dim iptbx As String             'フォルダ名

        Dim strfol001 As String

        Dim CODE1 As String
        Dim CODE2 As String



        Dim myPath As String
        Dim F_dir As String
        Dim itkflg As String

        Dim Ccode As String = ""

        Dim Cname As String = ""

        Dim strirai As String

        Dim hensyuuiraisyo As String

        Dim madef00 As String = ""

        madef01 = "処理履歴："


        strPath00(0) = "\\svnas201\exd06100\COMMON\生産管理本部\ＣＳチーム\案件抽出\a)自社通関依頼書（客先別）WEB\"
        strPath00(1) = "\\svnas201\exd06100\COMMON\生産管理本部\ＣＳチーム\案件抽出\b)タイムスケジュール（客先別）\"

        strPath01(0) = "\\svnas201\exd06100\COMMON\生産管理本部\ＣＳチーム\案件抽出\WEB_test\"
        'strPath01(0) = "\\svnas201\exd06100\COMMON\生産管理本部\ＣＳチーム\案件抽出\"
        strPath01(1) = "\\svnas201\EXD06101\DISC_COMMON\自社通関輸出書類\"



        '問題報告ログ初期化
        strLog = ""

        Dim I As Integer
        For I = 0 To GridView1.Rows.Count - 1

            'ファイル検索
            strFile0 = Dir(strPath00(0) & "自社通関依頼書　EXEDY *(" & Replace(GridView1.Rows(I).Cells(4).Text, "/", "-") & ")*.xlsx", vbNormal)
            If strFile0 = "" Then
                strLog = strLog & Right("0000" & I, 5) & ",原紙なし" & Chr(10)

                madef00 = 0

                GoTo Step00
            End If

            '委託検索
            itkflg = 0
            Call get_itakuhanntei(I, itkflg, GridView1.Rows(I).Cells(5).Text)

            If itkflg = 1 Then

                madef00 = 1

                GoTo Step00

            End If



            '1_________________________________________________

            strfol001 = Dir(strPath01(0) & "*(" & Replace(GridView1.Rows(I).Cells(4).Text, "/", "-") & ")*" & Replace(GridView1.Rows(I).Cells(5).Text, "/", "-"), vbDirectory)


            'If strfol001 <> "" Then
            madef00 = 2
            '    GoTo Step00

            'End If

            'strfol001 = Dir(strPath01(1) & "*(" & Replace(GridView1.Rows(I).Cells(4).Text, "/", "-") & ")*" & Replace(GridView1.Rows(I).Cells(5).Text, "/", "-") & "*", vbDirectory)


            'If strfol001 <> "" Then
            madef00 = 2
            '    GoTo Step00

            'End If


            'strfol001 = Dir(strPath01(1) & Format(DateAdd("m", -1, Now()), "yyyyMM") & "\" & "*(" & Replace(GridView1.Rows(I).Cells(4).Text, "/", "-") & ")*" & Replace(GridView1.Rows(I).Cells(5).Text, "/", "-") & "*", vbDirectory)


            'If strfol001 <> "" Then
            madef00 = 2
            '    GoTo Step00

            'End If


            'strfol001 = Dir(strPath01(1) & Format(DateAdd("m", 0, Now()), "yyyyMM") & "\" & "*(" & Replace(GridView1.Rows(I).Cells(4).Text, "/", "-") & ")*" & Replace(GridView1.Rows(I).Cells(5).Text, "/", "-") & "*", vbDirectory)


            'If strfol001 <> "" Then
            madef00 = 2
            '    GoTo Step00

            'End If

            'strfol001 = Dir(strPath01(1) & Format(DateAdd("m", 1, Now()), "yyyyMM") & "\" & "*(" & Replace(GridView1.Rows(I).Cells(4).Text, "/", "-") & ")*" & Replace(GridView1.Rows(I).Cells(5).Text, "/", "-") & "*", vbDirectory)


            'If strfol001 <> "" Then
            madef00 = 2
            '    GoTo Step00

            'End If





            '2_________________________________________________

            'フォルダ作成(既にあればスキップ)
            '検索したファイル名から作成
            strFol = Replace(strFile0, "自社通関依頼書　EXEDY ", "-")
            strFol = Replace(strFol, "IV-0000", "IV-" & GridView1.Rows(I).Cells(5).Text)
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

            iptbx = Left(Replace(GridView1.Rows(I).Cells(5).Text, "IV-", ""), 4)
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
            MyStr = Replace(strFile0, "IV-0000", "IV-" & GridView1.Rows(I).Cells(5).Text)

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

            ws1.Cell(11, 8).Value = GridView1.Rows(I).Cells(10).Text 'ETA
            '        ws2.Range("B21") = Ws0.Cells(i, 10)  'ETA

            ws1.Cell(14, 2).Value = "'" & GridView1.Rows(I).Cells(9).Text  'BOOKING NO.
            '        ws2.Range("B8") = "'" & Ws0.Cells(i, 13)  'BOOKING NO.

            ws1.Cell(11, 9).Value = GridView1.Rows(I).Cells(11).Text  'CARRIER(船社)
            '        ws2.Range("B22") = Ws0.Cells(i, 14)  'CARRIER(船社)

            ws1.Cell(11, 1).Value = GridView1.Rows(I).Cells(12).Text & " ()" 'VESSEL(船舶コード） '船舶コード課題
            '        ws2.Range("B5") = Ws0.Cells(i, 11) 'VESSEL(船舶コード） '船舶コード課題

            ws1.Cell(11, 4).Value = GridView1.Rows(I).Cells(13).Text  'VOY.NO.(航海番号)
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
            If ws1.Cell(16, 1).Value <> GridView1.Rows(I).Cells(14).Text Then

                ws1.Cell(16, 1).Style.Fill.BackgroundColor = XLColor.Red
                erflg = 1
            End If
            'PORT OF LOADING(積出港)
            If ws1.Cell(16, 3).Value <> GridView1.Rows(I).Cells(15).Text Then
                ws1.Cell(16, 3).Style.Fill.BackgroundColor = XLColor.Red
                erflg = 1
            End If
            'PORT OF DISCHARGE(揚地)
            If ws1.Cell(16, 5).Value <> GridView1.Rows(I).Cells(16).Text Then
                ws1.Cell(16, 5).Style.Fill.BackgroundColor = XLColor.Red
                erflg = 1
            End If
            'PLACE OF DELIVERY(配送先)
            If ws1.Cell(16, 7).Value <> GridView1.Rows(I).Cells(17).Text Then
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


                Call get_bookingdata(I, niuke, tsumi, ageti, haisou, GridView1.Rows(I).Cells(5).Text)

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


            'madef00 = 0　依頼書雛形無し,madef00 = 1　委託,madef00 = 2　同一フォルダあり




            If madef00 = "" Then

                madef01 = madef01 & "\n" & "作成済み　　　　 客先：" & GridView1.Rows(I).Cells(4).Text & " IVNO：" & GridView1.Rows(I).Cells(5).Text

            ElseIf madef00 = "0" Then

                madef01 = madef01 & "\n" & "依頼書なし　　　 客先：" & GridView1.Rows(I).Cells(4).Text & " IVNO：" & GridView1.Rows(I).Cells(5).Text

            ElseIf madef00 = "1" Then
                madef01 = madef01 & "\n" & "委託案件　　　　 客先：" & GridView1.Rows(I).Cells(4).Text & " IVNO：" & GridView1.Rows(I).Cells(5).Text

            ElseIf madef00 = "2" Then

                madef01 = madef01 & "\n" & "同一フォルダあり 客先：" & GridView1.Rows(I).Cells(4).Text & " IVNO：" & GridView1.Rows(I).Cells(5).Text

            End If
            madef00 = ""

        Next














    End Sub


    Sub Mail03(kbn As String)
        '通知メール


        'メールの送信に必要な情報
        Dim smtpHostName As String = "svsmtp01.exedy.co.jp"
        Dim smtpPort As Integer = 25

        ' メールの内容
        Dim strfrom As String = "r-fukao@exedy.com"
        Dim strto As String = "r-fukao@exedy.com"

        Dim f As String = ""
        Dim dt1 As DateTime = DateTime.Now

        'メールの件名
        'Dim strIrai As String = "" '"<通知>LCL案件展開　変更・追加・連絡"


        If kbn = "A" Then

            kbn = "ｱﾌﾀ"

        ElseIf kbn = "K" Then

            kbn = "KD"

        End If



        'メールの件名
        Dim subject As String = "【ご連絡・自動配信】" & kbn & " LS ７or９「試作限定」有無確認完了　" & dt1.ToShortDateString
        'message.Subject = ConvertBase64Subject(System.Text.Encoding.GetEncoding("csISO2022JP"), _MailTitle)

        'メールの本文
        Dim body As String = "<html><body><p>各位<p>お世話になっております。<p>" & kbn & " 登録完了いたしました。</p>以上になります。</p>" & kbn & "担当</body></html>" ' UriBodyC()



        'Dim strFilePath As String = "" '"C:\exp\cs_home\upload\" & Session("strFile")

        'Using stream = File.OpenRead(strFilePath)
        ' MailKit におけるメールの情報
        Dim message = New MimeKit.MimeMessage()

        ' 送り元情報  
        message.From.Add(MailboxAddress.Parse(strfrom))


        ' 宛先情報  
        message.To.Add(MailboxAddress.Parse(strto))
        'If Session("strCC") <> "" Then

        '    message.Cc.Add(MailboxAddress.Parse(Session("strCC")))

        'End If


        ' 表題  
        message.Subject = subject

        ' 本文
        Dim textPart = New MimeKit.TextPart(MimeKit.Text.TextFormat.Html)
        textPart.Text = body
        message.Body = textPart


        ''添付ファイル
        'Dim path = strFilePath     '添付したいファイル
        '    Dim attachment = New MimeKit.MimePart("application", "pdf") _
        '    With {
        '        .Content = New MimeKit.MimeContent(System.IO.File.OpenRead(path)),
        '        .ContentDisposition = New MimeKit.ContentDisposition(),
        '        .ContentTransferEncoding = MimeKit.ContentEncoding.Base64,
        '        .FileName = System.IO.Path.GetFileName(path)
        '    }

        Dim multipart = New MimeKit.Multipart("mixed")

        multipart.Add(textPart)
        'multipart.Add(attachment)

        message.Body = multipart

        Using client As New MailKit.Net.Smtp.SmtpClient()
            client.Connect(smtpHostName, smtpPort, MailKit.Security.SecureSocketOptions.Auto)
            client.Send(message)
            client.Disconnect(True)
            client.Dispose()
            message.Dispose()

        End Using


        'stream.Dispose()
        'End Using

        'File.Delete(strFilePath)



    End Sub


End Class
