﻿
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
        ConnectionString = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
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
                If Trim(e.Row.Cells(9).Text) = Trim(strbkg) Then
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
                strbkg = ""
                strbkg += dataread("FLG02")
            End While

            If strbkg = "1" Then
                e.Row.Cells(1).Text = "委託登録済"
                e.Row.BackColor = Drawing.Color.DarkSalmon
            End If
        End If


        If e.Row.RowType = DataControlRowType.DataRow Then

            If Trim(e.Row.Cells(19).Text) = "LCL" Then

            Else

                e.Row.Cells(19).Text = "FCL"

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
    Private Sub GridView2_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView2.RowDataBound

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

        If e.Row.RowType = DataControlRowType.DataRow Then
            'strSQL = "SELECT ITK_BKGNO FROM [T_EXL_CSWORKSTATUS] WHERE [T_EXL_CSWORKSTATUS].ITK_BKGNO = '" & Trim(e.Row.Cells(9).Text) & "' "

            'Dim ts1 As New TimeSpan(60, 0, 0, 0)
            'Dim ts2 As New TimeSpan(60, 0, 0, 0)
            'Dim dt4 As DateTime = DateTime.Parse(e.Row.Cells(5).Text)

            'Dim dt2 As DateTime = dt4 + ts1
            'Dim dt3 As DateTime = dt4 - ts1

            If Trim(e.Row.Cells(0).Text) = "委託登録済" Then

                e.Row.BackColor = Drawing.Color.DarkSalmon
            End If
        End If



    End Sub

    Private Sub itaku(bkgno As String)
        '確認完了ボタン押下時

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
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
        strSQL = strSQL & "WHERE BOOKING_NO = '" & Trim(bkgno) & "'"

        Command.CommandText = strSQL
        ' SQLの実行
        Command.ExecuteNonQuery()

        cnn.Close()
        cnn.Dispose()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim madef01 As String = ""
        Call makefld(madef01)

        Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "確認", "<script language='JavaScript'>confirm('" & madef01 & "');</script>", False)

    End Sub
    Private Function get_itakuhanntei(ivno As String) As String

        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String
        Dim strinv As String

        get_itakuhanntei = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
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

        'INVOICENOが最大のものを取得
        strSQL = strSQL & "AND T_INV_HD_TB.INVOICENO = "
        strSQL = strSQL & "(SELECT MAX(T_INV_HD_TB.INVOICENO) AS IVNO "
        strSQL = strSQL & "FROM T_INV_HD_TB "

        strSQL = strSQL & "WHERE T_INV_HD_TB.HEADTITLE Like 'INVOICE%' "
        strSQL = strSQL & "AND T_INV_HD_TB.OLD_INVNO like '%" & iptbx & "%' "
        strSQL = strSQL & "AND T_INV_HD_TB.BLDATE BETWEEN '" & dt3 & "' AND '" & dt2 & "' "
        strSQL = strSQL & "AND T_INV_HD_TB.ORG_INVOICENO IS NULL "
        strSQL = strSQL & ") "

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
        ConnectionString = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
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
        ConnectionString = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand
        Dim strSQL As String = ""
        Dim ivno As String = ""

        'トランザクションの開始
        Dim tran As SqlTransaction = Nothing



        'データベース接続を開く
        cnn.Open()

        Dim I As Integer
        For I = 0 To GridView1.Rows.Count - 1
            If CType(GridView1.Rows(I).Cells(0).Controls(1), CheckBox).Checked Then


                '★
                tran = cnn.BeginTransaction

                'FIN_FLGを更新
                strSQL = ""
                strSQL = strSQL & "UPDATE T_EXL_CSANKEN SET FLG02 ='1' "
                strSQL = strSQL & "WHERE BOOKING_NO = '" & Trim(GridView1.Rows(I).Cells(9).Text) & "'"

                '★
                Try
                    Command.CommandText = strSQL
                    'トランザクションの設定
                    Command.Transaction = tran
                    ' SQLの実行
                    Command.ExecuteNonQuery()
                    'トランザクションの実行
                    tran.Commit()
                Catch ex As Exception
                    'トランザクションをキャンセル
                    If tran IsNot Nothing Then
                        tran.Rollback()
                    End If

                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "確認", "<script language='JavaScript'>confirm('処理が失敗しました。" & ex.Message & ex.StackTrace & "');</script>", False)

                Finally
                    tran.Dispose()
                End Try

                Call GET_IVDATA(Trim(GridView1.Rows(I).Cells(9).Text), "1")
            Else
            End If
        Next
        GridView1.DataBind()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
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
                strSQL = strSQL & "WHERE BOOKING_NO = '" & Trim(GridView1.Rows(I).Cells(9).Text) & "'"

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

                Call GET_IVDATA(Trim(GridView1.Rows(I).Cells(9).Text), "2")
            Else
            End If
        Next
        GridView1.DataBind()

        cnn.Close()
        cnn.Dispose()

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
        ConnectionString = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
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

            strSQL = ""
            strSQL = strSQL & "UPDATE T_EXL_WORKSTATUS00 SET "

            strSQL = strSQL & "T_EXL_WORKSTATUS00.INVNO = '" & strinv & "', "
            strSQL = strSQL & "T_EXL_WORKSTATUS00.REGDATE = '" & Format(Now(), "yyyy/MM/dd") & "', "
            strSQL = strSQL & "T_EXL_WORKSTATUS00.BKGNO = '" & bkgno & "' "
            strSQL = strSQL & "WHERE T_EXL_WORKSTATUS00.INVNO ='" & strinv & "' "
            strSQL = strSQL & "AND T_EXL_WORKSTATUS00.BKGNO = '" & bkgno & "' "
            strSQL = strSQL & "AND T_EXL_WORKSTATUS00.ID = '001' "

        Else

            strSQL = ""
            strSQL = strSQL & "INSERT INTO T_EXL_WORKSTATUS00 VALUES("
            strSQL = strSQL & " '001' "
            strSQL = strSQL & ",'" & strinv & "' "
            strSQL = strSQL & ",'" & bkgno & "' "
            strSQL = strSQL & ",'" & Format(Now(), "yyyy/MM/dd") & "' "
            strSQL = strSQL & ",'" & Session("UsrId") & "_02" & "' "
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
        ConnectionString = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
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
        ConnectionString = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
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

        Dim gcnt As Long = 0

        Dim ercnt00 As Long

        Dim ercnt01 As Long

        Dim dt1 As DateTime = DateTime.Now

        gcnt = GridView1.Rows.Count

        If gcnt >= 1 Then
            Label20.Visible = False
        Else
            Label20.Visible = True
        End If


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

            If IsPostBack = True Then

            Else
                Call makefld(madef01)
            End If

            Label10.Visible = True
                Label11.Visible = False


            Else
                Label7.Text = "×"
            Label10.Visible = False
            Label11.Visible = True

        End If

        Dim strupddate02 As Date

        strSQL = ""
        strSQL = strSQL & "SELECT T_EXL_DATA_UPD.DATA_UPD FROM T_EXL_DATA_UPD "
        strSQL = strSQL & "WHERE T_EXL_DATA_UPD.DATA_CD ='013' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        While (dataread.Read())
            strupddate02 = Trim(dataread("DATA_UPD"))
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()

        Dim dt00 As String = dt1.ToShortDateString
        Dim dt03 As String = strupddate02.ToShortDateString

        If dt00 = dt03 Then
            Label22.Text = "済"
        Else
            Label22.Text = "未"
        End If


        Button4.Attributes.Add("onclick", "return confirm('メール送信します。よろしいですか？');")


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
        strSQL = strSQL & "SELECT T_EXL_DATA_UPD.DATA_UPD FROM T_EXL_DATA_UPD "
        strSQL = strSQL & "WHERE T_EXL_DATA_UPD.DATA_CD ='012' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        While (dataread.Read())
            strupddate01 = Trim(dataread("DATA_UPD"))
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
            ercnt00 = Trim(dataread("RecCnt"))
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()


        strSQL = ""
        strSQL = strSQL & "SELECT COUNT(*) AS RecCnt FROM T_EXL_CSANKEN "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        While (dataread.Read())
            ercnt01 = Trim(dataread("RecCnt"))
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()


        'If IsPostBack = True Then
        'Else
        '    strSQL = ""
        '    strSQL = strSQL & "SELECT M_EXL_KAIKA_CHANGE.NAME_EG FROM M_EXL_KAIKA_CHANGE WHERE M_EXL_KAIKA_CHANGE.NAME_JPN = 'DAYS'  "

        '    'ＳＱＬコマンド作成 
        '    dbcmd = New SqlCommand(strSQL, cnn)
        '    'ＳＱＬ文実行 
        '    dataread = dbcmd.ExecuteReader()

        '    While (dataread.Read())
        '        DropDownList6.SelectedValue = dataread("NAME_EG")
        '    End While

        '    'クローズ処理 
        '    dataread.Close()
        '    dbcmd.Dispose()
        'End If




        Dim dt01 As String = strupddate00.ToShortDateString
        Dim dt02 As String = strupddate01.ToShortDateString


        If ercnt00 = 0 Or ercnt01 = 0 Then

            Panel1.Visible = False
            Panel2.Visible = True

        Else

            If dt00 = dt01 And dt02 = dt00 Then

                Panel1.Visible = True
                Panel2.Visible = False

            Else

                Panel1.Visible = False
                Panel2.Visible = True

            End If

        End If




        cnn.Close()
        cnn.Dispose()

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged

        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
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

            If Label3.Text = "済" And Label4.Text = "済" Then

                Dim struid As String = Session("UsrId")
                Call Mail030(kbn, struid)

                Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "確認", "<script language='JavaScript'>confirm('登録し通知メールを送信しました。');</script>", False)


            End If

        Else
            Label3.Text = "未"
        End If



    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged

        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
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

            If Label3.Text = "済" And Label4.Text = "済" Then

                Dim struid As String = Session("UsrId")
                Call Mail030(kbn, struid)
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "確認", "<script language='JavaScript'>confirm('登録し通知メールを送信しました。');</script>", False)


            End If
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

        Dim WDAY02 As String

        Dim WDAYNO00 As String
        Dim WDAY00 As String
        Dim WDAY01 As String
        Dim WDAY03 As String

        Dim dt1 As DateTime = DateTime.Now

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        Call mail_update()

        'データベース接続を開く
        cnn.Open()

        strSQL = ""
        strSQL = "SELECT T_EXL_CSWORKDAY.WORKDAY_NO FROM T_EXL_CSWORKDAY WHERE T_EXL_CSWORKDAY.WORKDAY <= '" & dt1.ToShortDateString & "' "
        strSQL = strSQL & "ORDER BY T_EXL_CSWORKDAY.WORKDAY_NO "

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
        strSQL = strSQL & "ORDER BY T_EXL_CSWORKDAY.WORKDAY_NO "

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
        strSQL = strSQL & "ORDER BY T_EXL_CSWORKDAY.WORKDAY_NO "

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

        strSQL = ""
        strSQL = "SELECT T_EXL_CSWORKDAY.WORKDAY FROM T_EXL_CSWORKDAY WHERE T_EXL_CSWORKDAY.WORKDAY_NO = '" & Val(WDAYNO00) + 3 & "' "
        strSQL = strSQL & "ORDER BY T_EXL_CSWORKDAY.WORKDAY_NO "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        '結果を取り出す 
        While (dataread.Read())
            WDAY02 = dataread("WORKDAY")
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()

        strSQL = ""
        strSQL = "SELECT T_EXL_CSWORKDAY.WORKDAY FROM T_EXL_CSWORKDAY WHERE T_EXL_CSWORKDAY.WORKDAY_NO = '" & Val(WDAYNO00) + 4 & "' "
        strSQL = strSQL & "ORDER BY T_EXL_CSWORKDAY.WORKDAY_NO "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        '結果を取り出す 
        While (dataread.Read())
            WDAY03 = dataread("WORKDAY")
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()



        Dim yusen As String = ""
        Dim kin As String = ""
        Dim nihontoran As String = ""
        Dim nitsu As String = ""

        Dim struid As String = Session("UsrId")
        Call Mail01(WDAY00, WDAY01, yusen, struid)
        Call Mail02(WDAY01, WDAY02, kin, struid)
        Call Mail03(WDAY01, WDAY02, nihontoran, struid)
        Call Mail04(WDAY01, WDAY02, nitsu, struid)
        Call Mail05(WDAY02, WDAY03, kin, struid)


        Dim mailmsg As String = ""
        If yusen <> "" Then
            mailmsg = mailmsg & "," & "郵船"
        End If

        If kin <> "" Then
            mailmsg = mailmsg & "," & "近鉄"
        End If

        If nihontoran <> "" Then
            mailmsg = mailmsg & "," & "日本トランス"
        End If

        If nitsu <> "" Then
            mailmsg = mailmsg & "," & "日通"
        End If

        Dim strupddate02 As Date

        strSQL = ""
        strSQL = strSQL & "SELECT T_EXL_DATA_UPD.DATA_UPD FROM T_EXL_DATA_UPD "
        strSQL = strSQL & "WHERE T_EXL_DATA_UPD.DATA_CD ='013' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        While (dataread.Read())
            strupddate02 = Trim(dataread("DATA_UPD"))
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()

        Dim dt00 As String = dt1.ToShortDateString
        Dim dt03 As String = strupddate02.ToShortDateString

        If dt00 = dt03 Then
            Label22.Text = "済"
        Else
            Label22.Text = "未"
        End If

        cnn.Close()
        cnn.Dispose()

        If mailmsg = "" Then
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "確認", "<script language='JavaScript'>confirm('対象なしです。');</script>", False)
        Else
            mailmsg = "対象：" & mailmsg
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "確認", "<script language='JavaScript'>confirm('" & mailmsg & "にメールを送信しました。');</script>", False)
        End If

    End Sub


    Sub Mail01(WDAY00 As String, WDAY01 As String, ByRef r As String, struid As String)
        '郵船

        'メールの送信に必要な情報
        Dim smtpHostName As String = "svsmtp01.exedy.co.jp"
        Dim smtpPort As Integer = 25

        ' メールの内容

        Dim strfrom As String = GET_from(struid)
        Dim strto As String = GET_ToAddress2("01", 1)
        strto = Left(strto, Len(strto) - 1)

        Dim strcc As String = GET_ToAddress2("01", 2) + GET_from(struid)


        Dim strsyomei As String = GET_syomei(struid)

        Dim f As String = ""

        'Dim s01 As String = ""
        's01 = mailsub(WDAY00)

        'メールの件名
        Dim subject As String = "【ご連絡】EXD通関委託　" & WDAY00 & "分"
        'message.Subject = ConvertBase64Subject(System.Text.Encoding.GetEncoding("csISO2022JP"), _MailTitle)


        'メールの本文
        Dim body As String = ""

        body = body + "－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－<br/>"
        body = body + "このメールはシステムから送信されています。<br/>"
        body = body + "心当たりが無い場合、エクセディ　CSチーム担当者までご連絡ください。<br/>"
        body = body + "－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－<br/>"

        body = body & "<html><body>郵船ロジスティクス ご担当者様<br>いつもお世話になっております。<br><br>" & WDAY00 & "弊社CUT分で通関委託がございます。<br>（LCL案件は全て委託となります。）<br><br>" 'お忙しい中とは存じますが、宜しくお願い申し上げます。</p>以上になります。</body></html>" ' UriBodyC()
        Dim t As String = "<html><body><Table border='1' style='Font-Size:12px;'><tr><td>客先</td><td>INVOICE NO. / BKG NO.</td><td>積出港</td><td>仕向地</td></tr>"

        t = t & body01("郵船")
        f = body02("郵船")

        t = t & "</Table></body></html>"

        body = body & t

        Dim body2 As String = "<br>" ' UriBodyC()

        body = body & body2 & "お忙しい中とは存じますが、宜しくお願い申し上げます。<br>以上になります。<br><br></body></html>" & strsyomei

        body = "<font size=" & Chr(34) & "2" & Chr(34) & ">" & body & "</font>"
        body = "<font face=" & Chr(34) & "Meiryo UI" & Chr(34) & ">" & body & "</font>"

        If f = "" Then
            body = "<html><body>郵船ロジスティクス ご担当者様<br>いつもお世話になっております。<br><br>" & WDAY00 & "弊社CUT分で通関委託案件はございません。<br>（LCL案件は全て委託となります。）<br><br>宜しくお願い申し上げます。<br>以上になります。<br><br></body></html>" & strsyomei
            body = "<font size=" & Chr(34) & "2" & Chr(34) & ">" & body & "</font>"
            body = "<font face=" & Chr(34) & "Meiryo UI" & Chr(34) & ">" & body & "</font>"
        End If

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
            r = "1y"
        End Using

    End Sub

    Sub Mail02(WDAY00 As String, WDAY01 As String, ByRef r As String, struid As String)



        'メールの送信に必要な情報
        Dim smtpHostName As String = "svsmtp01.exedy.co.jp"
        Dim smtpPort As Integer = 25

        ' メールの内容

        Dim strfrom As String = GET_from(struid)
        'Dim strto As String = GET_from(struid)
        'Dim strcc As String = GET_from(struid) + "," + "r-fukao@exedy.com"
        'Dim strto As String = GET_ToAddress(3, 1)
        Dim strto As String = GET_ToAddress2("02", 1)
        strto = Left(strto, Len(strto) - 1)

        Dim strcc As String = GET_ToAddress2("02", 2) + GET_from(struid)

        Dim strsyomei As String = GET_syomei(struid)

        'strto = GET_from(struid)
        'strcc = GET_from(struid)

        Dim f As String = ""


        'Dim s01 As String = ""
        's01 = mailsub(WDAY01)

        'メールの件名
        Dim subject As String = "【ご連絡】EXD通関委託　" & WDAY01 & "分"

        'メールの本文
        Dim body As String = ""

        body = body + "－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－<br/>"
        body = body + "このメールはシステムから送信されています。<br/>"
        body = body + "心当たりが無い場合、エクセディ　CSチーム担当者までご連絡ください。<br/>"
        body = body + "－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－<br/>"

        body = body & "<html><body>近鉄エクスプレス ご担当者様<br>いつもお世話になっております。<br><br>" & WDAY01 & "弊社CUT分で通関委託がございます。<br>（LCL案件は全て委託となります。）<br><br>" 'お忙しい中とは存じますが、宜しくお願い申し上げます。</p>以上になります。</body></html>" ' UriBodyC()

        Dim t As String = "<html><body><Table border='1' style='Font-Size:12px;'><tr><td>客先</td><td>INVOICE NO. / BL NO.</td><td>積出港</td><td>仕向地</td></tr>"

        t = t & body01("近鉄")
        f = body02("近鉄")

        t = t & "</Table></body></html>"

        body = body & t

        Dim body2 As String = "<br>" ' UriBodyC()

        body = body & body2 & "お忙しい中とは存じますが、宜しくお願い申し上げます。<br>以上になります。<br></body></html>" & strsyomei

        body = "<font size=" & Chr(34) & " 2" & Chr(34) & ">" & body & "</font>"
        body = "<font face=" & Chr(34) & " Meiryo UI" & Chr(34) & ">" & body & "</font>"

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

    End Sub

    Sub Mail03(WDAY00 As String, WDAY01 As String, ByRef r As String, struid As String)

        'メールの送信に必要な情報
        Dim smtpHostName As String = "svsmtp01.exedy.co.jp"
        Dim smtpPort As Integer = 25

        ' メールの内容

        Dim strfrom As String = GET_from(struid)
        ' Dim strto As String = GET_from(struid)
        ' Dim strcc As String = GET_from(struid) + "," + "r-fukao@exedy.com"

        'Dim strto As String = GET_ToAddress(4, 1)
        Dim strto As String = GET_ToAddress2("03", 1)
        strto = Left(strto, Len(strto) - 1)

        Dim strcc As String = GET_ToAddress2("03", 2) + GET_from(struid)

        Dim strsyomei As String = GET_syomei(struid)

        Dim f As String = ""

        'Dim s01 As String = ""
        's01 = mailsub(WDAY01)

        'メールの件名
        Dim subject As String = "【ご連絡】EXD通関委託　" & WDAY01 & "分"


        'メールの本文
        Dim body As String = ""

        body = body + "－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－<br/>"
        body = body + "このメールはシステムから送信されています。<br/>"
        body = body + "心当たりが無い場合、エクセディ　CSチーム担当者までご連絡ください。<br/>"
        body = body + "－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－<br/>"

        body = body & "<html><body>日本トランスポート ご担当者様<br>いつもお世話になっております。<br><br>" & WDAY01 & "弊社CUT分で通関委託がございます。<br>（LCL案件は全て委託となります。）<br><br>" 'お忙しい中とは存じますが、宜しくお願い申し上げます。</p>以上になります。</body></html>" ' UriBodyC()
        Dim t As String = "<html><body><Table border='1' style='Font-Size:12px;'><tr><td>客先</td><td>INVOICE NO. / BKG NO.</td><td>積出港</td><td>仕向地</td></tr>"

        t = t & body01("日ト")
        f = body02("日ト")

        t = t & "</Table></body></html>"

        body = body & t

        Dim body2 As String = "<br>" ' UriBodyC()

        body = body & body2 & "お忙しい中とは存じますが、宜しくお願い申し上げます。<br>以上になります。<br><br></body></html>" & strsyomei

        body = "<font size=" & Chr(34) & " 2" & Chr(34) & ">" & body & "</font>"
        body = "<font face=" & Chr(34) & " Meiryo UI" & Chr(34) & ">" & body & "</font>"

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

    End Sub

    Sub Mail04(WDAY00 As String, WDAY01 As String, ByRef r As String, struid As String)

        'メールの送信に必要な情報
        Dim smtpHostName As String = "svsmtp01.exedy.co.jp"
        Dim smtpPort As Integer = 25

        ' メールの内容

        Dim strfrom As String = GET_from(struid)
        'Dim strto As String = GET_from(struid)
        'Dim strcc As String = GET_from(struid) + "," + "r-fukao@exedy.com"

        'Dim strto As String = GET_ToAddress(5, 1)
        Dim strto As String = GET_ToAddress2("04", 1)
        strto = Left(strto, Len(strto) - 1)

        Dim strcc As String = GET_ToAddress2("04", 2) + GET_from(struid)

        Dim strsyomei As String = GET_syomei(struid)

        Dim f As String = ""

        'Dim s01 As String = ""
        's01 = mailsub(WDAY01)

        'メールの件名
        Dim subject As String = "【ご連絡】EXD通関委託　" & WDAY01 & "分"


        'メールの本文
        Dim body As String = ""

        body = body + "－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－<br/>"
        body = body + "このメールはシステムから送信されています。<br/>"
        body = body + "心当たりが無い場合、エクセディ　CSチーム担当者までご連絡ください。<br/>"
        body = body + "－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－<br/>"


        'メールの本文
        body = body & "<html><body>日本通運 ご担当者様<br>いつもお世話になっております。<br><br>" & WDAY01 & "弊社CUT分で通関委託がございます。<br>（LCL案件は全て委託となります。）<br><br>" ' UriBodyC()

        Dim t As String = "<html><body><Table border='1' style='Font-Size:12px;'><tr><td>客先</td><td>INVOICE NO. / BKG NO.</td><td>積出港</td><td>仕向地</td></tr>"

        t = t & body01("日通")
        f = body02("日通")

        t = t & "</Table></body></html>"

        body = body & t

        Dim body2 As String = "<br>" ' UriBodyC()

        body = body & body2 & "お忙しい中とは存じますが、宜しくお願い申し上げます。<br>以上になります。<br><br></body></html>" & strsyomei

        body = "<font size=" & Chr(34) & " 2" & Chr(34) & ">" & body & "</font>"
        body = "<font face=" & Chr(34) & " Meiryo UI" & Chr(34) & ">" & body & "</font>"

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

    End Sub

    Sub Mail05(WDAY00 As String, WDAY01 As String, ByRef r As String, struid As String)



        'メールの送信に必要な情報
        Dim smtpHostName As String = "svsmtp01.exedy.co.jp"
        Dim smtpPort As Integer = 25

        ' メールの内容

        Dim strfrom As String = GET_from(struid)
        'Dim strto As String = GET_from(struid)
        'Dim strcc As String = GET_from(struid) + "," + "r-fukao@exedy.com"
        'Dim strto As String = GET_ToAddress(3, 1)
        Dim strto As String = GET_ToAddress2("02", 1)
        strto = Left(strto, Len(strto) - 1)

        Dim strcc As String = GET_ToAddress2("02", 2) + GET_from(struid)

        Dim strsyomei As String = GET_syomei(struid)

        'strto = GET_from(struid)
        'strcc = GET_from(struid)

        Dim f As String = ""


        'Dim s01 As String = ""
        's01 = mailsub(WDAY01)

        'メールの件名
        Dim subject As String = "【ご連絡】EXD通関委託　" & WDAY01 & "分"

        'メールの本文
        Dim body As String = ""

        body = body + "－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－<br/>"
        body = body + "このメールはシステムから送信されています。<br/>"
        body = body + "心当たりが無い場合、エクセディ　CSチーム担当者までご連絡ください。<br/>"
        body = body + "－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－<br/>"

        body = body & "<html><body>近鉄エクスプレス ご担当者様<br>いつもお世話になっております。<br><br>" & WDAY01 & "弊社CUT分で通関委託がございます。<br>（LCL案件は全て委託となります。）<br><br>" 'お忙しい中とは存じますが、宜しくお願い申し上げます。</p>以上になります。</body></html>" ' UriBodyC()

        Dim t As String = "<html><body><Table border='1' style='Font-Size:12px;'><tr><td>客先</td><td>INVOICE NO. / BL NO.</td><td>積出港</td><td>仕向地</td></tr>"

        t = t & body01("上近")
        f = body02("上近")

        t = t & "</Table></body></html>"

        body = body & t

        Dim body2 As String = "<br>" ' UriBodyC()

        body = body & body2 & "お忙しい中とは存じますが、宜しくお願い申し上げます。<br>以上になります。<br></body></html>" & strsyomei

        body = "<font size=" & Chr(34) & " 2" & Chr(34) & ">" & body & "</font>"
        body = "<font face=" & Chr(34) & " Meiryo UI" & Chr(34) & ">" & body & "</font>"

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

    End Sub
    Private Function body01(A As String) As String

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
        ConnectionString = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        Dim dt1 As DateTime = DateTime.Now

        Dim ts1 As New TimeSpan(100, 0, 0, 0)
        Dim ts2 As New TimeSpan(100, 0, 0, 0)
        Dim dt2 As DateTime = dt1 + ts1
        Dim dt3 As DateTime = dt1 - ts1

        body01 = ""

        'データベース接続を開く
        cnn.Open()

        strSQL = ""
        strSQL = strSQL & "SELECT T_EXL_CSANKEN.CUST, T_EXL_CSANKEN.INVOICE, T_EXL_CSANKEN.BOOKING_NO, T_EXL_CSANKEN.FLG05, T_EXL_CSANKEN.LOADING_PORT, T_EXL_CSANKEN.PLACE_OF_DELIVERY "
        strSQL = strSQL & "FROM T_EXL_CSANKEN "
        strSQL = strSQL & "WHERE T_EXL_CSANKEN.FLG01 ='1' "
        strSQL = strSQL & "AND T_EXL_CSANKEN.FLG02 ='1' "
        strSQL = strSQL & "AND T_EXL_CSANKEN.FORWARDER IN ('" & A & "') "

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

            If A = "近鉄" Or A = "上近" Then
                BOOKING_NO = Convert.ToString(dataread("FLG05"))
            Else
                BOOKING_NO = Convert.ToString(dataread("BOOKING_NO"))
            End If

            DISCHARGING_PORT = Convert.ToString(dataread("LOADING_PORT"))
            PLACE_OF_DELIVERY = Convert.ToString(dataread("PLACE_OF_DELIVERY"))
            body01 = body01 & "<tr><td>" & CUST & "</td><td>" & INVOICE & " / " & BOOKING_NO & "</td><td>" & DISCHARGING_PORT & "</td><td>" & PLACE_OF_DELIVERY & "</td></tr>"
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()

        cnn.Close()
        cnn.Dispose()

    End Function


    Private Function body02(A As String) As String

        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String

        body02 = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
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
        strSQL = strSQL & "AND T_EXL_CSANKEN.LCL_QTY <>'LCL' "
        strSQL = strSQL & "AND T_EXL_CSANKEN.FORWARDER IN ('" & A & "') "


        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        '結果を取り出す 
        While (dataread.Read())
            body02 = "1"
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()

        cnn.Close()
        cnn.Dispose()

    End Function

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
        'strPath01(0) = "\\svnas201\exd06100\COMMON\生産管理本部\ＣＳチーム\案件抽出\WEB_test\"
        strPath01(0) = "\\svnas201\exd06100\COMMON\生産管理本部\ＣＳチーム\案件抽出\"
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
            itkflg = get_itakuhanntei(Left(GridView1.Rows(I).Cells(5).Text, 4))

            If itkflg = "1" Then
                madef00 = 1
                GoTo Step00
            End If

            '1_________________________________________________

            strfol001 = Dir(strPath01(0) & "*(" & Replace(GridView1.Rows(I).Cells(4).Text, "/", "-") & ")*" & Replace(GridView1.Rows(I).Cells(5).Text, "/", "-"), vbDirectory)

            If strfol001 <> "" Then
                madef00 = 2
                GoTo Step00

            End If

            strfol001 = Dir(strPath01(1) & "*(" & Replace(GridView1.Rows(I).Cells(4).Text, "/", "-") & ")*" & Replace(GridView1.Rows(I).Cells(5).Text, "/", "-") & "*", vbDirectory)


            If strfol001 <> "" Then
                madef00 = 2
                GoTo Step00

            End If


            strfol001 = Dir(strPath01(1) & Format(DateAdd("m", -1, Now()), "yyyyMM") & "\" & "*(" & Replace(GridView1.Rows(I).Cells(4).Text, "/", "-") & ")*" & Replace(GridView1.Rows(I).Cells(5).Text, "/", "-") & "*", vbDirectory)


            If strfol001 <> "" Then
                madef00 = 2
                GoTo Step00

            End If


            strfol001 = Dir(strPath01(1) & Format(DateAdd("m", 0, Now()), "yyyyMM") & "\" & "*(" & Replace(GridView1.Rows(I).Cells(4).Text, "/", "-") & ")*" & Replace(GridView1.Rows(I).Cells(5).Text, "/", "-") & "*", vbDirectory)


            If strfol001 <> "" Then
                madef00 = 2
                GoTo Step00

            End If

            strfol001 = Dir(strPath01(1) & Format(DateAdd("m", 1, Now()), "yyyyMM") & "\" & "*(" & Replace(GridView1.Rows(I).Cells(4).Text, "/", "-") & ")*" & Replace(GridView1.Rows(I).Cells(5).Text, "/", "-") & "*", vbDirectory)


            If strfol001 <> "" Then
                madef00 = 2
                GoTo Step00

            End If

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


            If IsNumeric(Trim(iptbx)) Then
                Call copy_custfile(iptbx, Cname, Ccode)
            Else
                Ccode = Trim(GridView1.Rows(I).Cells(4).Text)
                Cname = Cname01(Ccode)
            End If


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

            '恐らく不要
            'Call Get_allinv_k(Trim(GridView1.Rows(I).Cells(5).Text), Trim(GridView1.Rows(I).Cells(9).Text))

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


    Sub Mail030(kbn As String, struid As String)
        '通知メール

        Dim strfrom As String = GET_from(struid)

        Dim strsyomei As String = GET_syomei(struid)

        'メールの送信に必要な情報
        Dim smtpHostName As String = "svsmtp01.exedy.co.jp"
        Dim smtpPort As Integer = 25

        ' メールの内容

        'Dim strto As String = GET_ToAddress(0, 1) '宛先
        Dim strto As String = GET_ToAddress2("05", 1) '宛先
        strto = Left(strto, Len(strto) - 1)

        'Dim strcc As String = GET_ToAddress(0, 0) + GET_from(struid)  'CC 
        Dim strcc As String = GET_ToAddress2("05", 2) + GET_from(struid)  'CC 


        Dim f As String = ""
        Dim dt1 As DateTime = DateTime.Now

        If kbn = "A" Then

            kbn = "ｱﾌﾀ"

        ElseIf kbn = "K" Then

            kbn = "KD"

        End If

        'メールの件名
        'Dim subject As String = "【ご連絡・自動配信】" & kbn & " LS ７or９「試作限定」有無確認完了　" & dt1.ToShortDateString
        Dim subject As String = "【ご連絡・自動配信】 LS ７or９「試作限定」有無確認完了　" & dt1.ToShortDateString

        'メールの本文
        Dim body As String = ""

        body = body + "<br/>"
        body = body + "－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－<br/>"
        body = body + "このメールはシステムから送信されています。<br/>"
        body = body + "心当たりが無い場合、エクセディ　CSチーム担当者までご連絡ください。<br/>"
        body = body + "－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－<br/>"

        'body = body & "<html><body>各位<br>お世話になっております。<br><br>" & "KDとｱﾌﾀ登録完了いたしました。<br>以上になります。<br><br>" & kbn & "担当<br><br>※KDとｱﾌﾀのどちらともの登録が完了した際に配信されます。</body></html>" ' UriBodyC()

        'body = "<font size=" & Chr(34) & " 3" & Chr(34) & ">" & body & "</font>"
        'body = "<font face=" & Chr(34) & " Meiryo UI" & Chr(34) & ">" & body & "</font>"






        'メールの本文
        body = body & "<html><body><p>各位<p>お世話になっております。<p>KDとｱﾌﾀ登録完了いたしました。</p></body></html>" ' UriBodyC()

        Dim t As String = ””
        GridView1.DataBind()

        If GridView1.Rows.Count >= 1 Then

            t = t & "<html><body><Table border='1' style='Font-Size:14px;'><tr style='background-color: #6fbfd1;'><td>状況</td><td>シート</td><td>海貨業者</td><td>客先コード	</td><td>INVOICE</td><td>進捗状況</td><td>CUT</td><td>ETD</td><td>BOOKING_NO</td><td>出荷形態</td></tr>"

            For I = 0 To GridView1.Rows.Count - 1
                Call iptdata(GridView1.Rows(I).Cells(1).Text, GridView1.Rows(I).Cells(2).Text, GridView1.Rows(I).Cells(3).Text, GridView1.Rows(I).Cells(4).Text, GridView1.Rows(I).Cells(5).Text, Trim(GridView1.Rows(I).Cells(7).Text), GridView1.Rows(I).Cells(8).Text, GridView1.Rows(I).Cells(9).Text, GridView1.Rows(I).Cells(10).Text)
                If GridView1.Rows(I).Cells(1).Text = "委託登録済" Then
                    t = t & "<tr><td style='background-color: #FF9E8C;'>" & GridView1.Rows(I).Cells(1).Text & "</td><td style='background-color: #FF9E8C;'>" & GridView1.Rows(I).Cells(2).Text & "</td><td style='background-color: #FF9E8C;'>" & GridView1.Rows(I).Cells(3).Text & "</td><td style='background-color: #FF9E8C;'>" & GridView1.Rows(I).Cells(4).Text & "</td><td style='background-color: #FF9E8C;'>" & GridView1.Rows(I).Cells(5).Text & "</td><td style='background-color: #FF9E8C;'>" & Trim(GridView1.Rows(I).Cells(6).Text) & "</td><td style='background-color: #FF9E8C;'>" & Trim(GridView1.Rows(I).Cells(7).Text) & "</td><td style='background-color: #FF9E8C;'>" & GridView1.Rows(I).Cells(8).Text & "</td><td style='background-color: #FF9E8C;'>" & GridView1.Rows(I).Cells(9).Text & "</td><td style='background-color: #FF9E8C;'>" & GridView1.Rows(I).Cells(10).Text & "</td>"
                Else
                    t = t & "<tr><td>" & GridView1.Rows(I).Cells(1).Text & "</td><td>" & GridView1.Rows(I).Cells(2).Text & "</td><td>" & GridView1.Rows(I).Cells(3).Text & "</td><td>" & GridView1.Rows(I).Cells(4).Text & "</td><td>" & GridView1.Rows(I).Cells(5).Text & "</td><td>" & Trim(GridView1.Rows(I).Cells(6).Text) & "</td><td>" & Trim(GridView1.Rows(I).Cells(7).Text) & "</td><td>" & GridView1.Rows(I).Cells(8).Text & "</td><td>" & GridView1.Rows(I).Cells(9).Text & "</td><td>" & GridView1.Rows(I).Cells(10).Text & "</td>"
                End If
            Next


        Else
            t = t & "<p>対象なし<p>"
        End If

        t = t & "</Table>"

        body = body & t

        Dim body2 As String = "</p>" ' UriBodyC()

        body = body & body2 & "<p>" & kbn & "担当</p><p></p><p></p>※KDとｱﾌﾀのどちらともの登録が完了した際に配信されます。</body></html>"

        body = "<font size=" & Chr(34) & " 2" & Chr(34) & ">" & body & "</font>"
        body = "<font face=" & Chr(34) & " Meiryo UI" & Chr(34) & ">" & body & "</font>"



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
        ConnectionString = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()

        strSQL = strSQL & "Select MAIL_ADD FROM M_EXL_LCL_DEC_MAIL "
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
        ConnectionString = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
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
        ConnectionString = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
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
        ConnectionString = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
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

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click



        Dim struid As String = Session("UsrId")
        Dim strfrom As String = GET_from(struid)
        Dim strto As String = "r-fukao@exedy.com"
        'Dim strto As String = "r-fukao@exedy.com,r-fukao@exedy.com"

        Dim strsyomei As String = GET_syomei(struid)

        'メールの送信に必要な情報
        Dim smtpHostName As String = "svsmtp01.exedy.co.jp"
        Dim smtpPort As Integer = 25

        ' メールの内容


        'メールの件名
        Dim subject As String = "【異常報告】案件抽出ページ"

        'メールの本文
        Dim body As String = "<html><body>案件抽出ページから異常報告です。<br>http://k3hwpm01/EXP/cs_home/anken_booking02.aspx</body></html>" ' UriBodyC()

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

        Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "確認", "<script language='JavaScript'>confirm('メールを送信しました。');</script>", False)

    End Sub


    Private Sub Get_allinv_k(strInv As String, strbkg As String)

        Dim strSQL As String = ""
        Dim ivno As String = ""
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand

        Dim dt1 As DateTime = DateTime.Now

        Dim ts1 As New TimeSpan(100, 0, 0, 0)
        Dim ts2 As New TimeSpan(100, 0, 0, 0)
        Dim dt2 As DateTime = dt1 + ts1
        Dim dt3 As DateTime = dt1 - ts1

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
        strSQL = strSQL & "WHERE T_INV_HD_TB.BLDATE BETWEEN '" & dt3 & "' AND '" & dt2 & "' "
        strSQL = strSQL & "GROUP BY T_INV_HD_TB.BOOKINGNO, T_INV_HD_TB.OLD_INVNO, T_INV_HD_TB.SHIPPEDPER, T_INV_HD_TB.VOYAGENO, T_INV_HD_TB.IOPORTDATE, T_INV_HD_TB.CUTDATE "
        strSQL = strSQL & "HAVING T_INV_HD_TB.BOOKINGNO like '%" & strbkg & "%' "

        strSQL = strSQL & "order by T_INV_HD_TB.CUTDATE Desc "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()


        '結果を取り出す 
        While (dataread.Read())
            Call addRecord_K(dataread("OLD_INVNO"))
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()


    End Sub

    Private Sub iptdata(strstu As String, strsheet As String, strkaika As String, strcust As String, strinv As String, strcut As String, stretd As String, strbkg As String, strtype As String)

        Dim strSQL As String = ""
        Dim ivno As String = ""
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand

        Dim intCnt As Integer
        Dim itkcnt As String

        Dim dt1 As DateTime = DateTime.Now

        Dim ts1 As New TimeSpan(100, 0, 0, 0)
        Dim ts2 As New TimeSpan(100, 0, 0, 0)
        Dim dt2 As DateTime = dt1 + ts1
        Dim dt3 As DateTime = dt1 - ts1

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand

        'データベース接続を開く
        cnn.Open()


        strSQL = ""
        strSQL = strSQL & "SELECT count(BKGNO) as cnt FROM T_EXL_ANKEN_HISTORY WHERE "
        strSQL = strSQL & "T_EXL_ANKEN_HISTORY.BKGNO = '" & strbkg & "' "


        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()


        '結果を取り出す 
        While (dataread.Read())

            intCnt = dataread("cnt")

        End While


        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()


        '既存データの有無を判定
        If intCnt > 0 Then

            '既存データありの場合、UPDATE
            strSQL = ""
            strSQL = strSQL & "UPDATE T_EXL_ANKEN_HISTORY Set "
            strSQL = strSQL & "T_EXL_ANKEN_HISTORY.STATUS = '" & Replace(strstu, "&nbsp;", Format(Now, "yyyy/MM/dd")) & " ', "
            strSQL = strSQL & "T_EXL_ANKEN_HISTORY.BKGSHEET = '" & strsheet & " ', "
            strSQL = strSQL & "T_EXL_ANKEN_HISTORY.FORWARDER = '" & strkaika & " ', "
            strSQL = strSQL & "T_EXL_ANKEN_HISTORY.CUST = '" & strcust & " ', "
            strSQL = strSQL & "T_EXL_ANKEN_HISTORY.INVOICE = '" & strinv & " ', "
            strSQL = strSQL & "T_EXL_ANKEN_HISTORY.CUT = '" & strcut & " ', "
            strSQL = strSQL & "T_EXL_ANKEN_HISTORY.ETD = '" & stretd & " ', "
            strSQL = strSQL & "T_EXL_ANKEN_HISTORY.SHIP_TYPE = '" & Format(Now, "yyyy/MM/dd") & " ' "
            strSQL = strSQL & "WHERE T_EXL_ANKEN_HISTORY.BKGNO = '" & Trim(strbkg) & "' "

            Command.CommandText = strSQL
            ' SQLの実行
            Command.ExecuteNonQuery()

        Else


            '既存データが無いのでINSERTする
            strSQL = ""
            strSQL = strSQL & "INSERT INTO T_EXL_ANKEN_HISTORY VALUES("
            strSQL = strSQL & " '" & IIf(Replace(strstu, "&nbsp;", "") = "", "", Replace(strstu, "&nbsp;", Format(Now, "yyyy/MM/dd"))) & "', "
            strSQL = strSQL & " '" & IIf(strsheet = "", "", strsheet) & "', "
            strSQL = strSQL & " '" & IIf(strkaika = "", "", strkaika) & "', "
            strSQL = strSQL & " '" & IIf(strcust = "", "", strcust) & "', "
            strSQL = strSQL & " '" & IIf(strinv = "", "", strinv) & "', "
            strSQL = strSQL & " '" & IIf(strcut = "", "", strcut) & "', "
            strSQL = strSQL & " '" & IIf(stretd = "", "", stretd) & "', "
            strSQL = strSQL & " '" & IIf(strbkg = "", "", strbkg) & "', "
            strSQL = strSQL & " '" & Format(Now, "yyyy/MM/dd") & "' "
            strSQL = strSQL & ")"

            Command.CommandText = strSQL
            ' SQLの実行
            Command.ExecuteNonQuery()


        End If

        cnn.Close()
        cnn.Dispose()


    End Sub


    Private Sub addRecord_K(strIVNO As String)

        Dim strSQL As String = ""
        Dim ivno As String = ""
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand

        Dim TDATE As String = ""
        Dim CUT As String = ""
        Dim CUST As String = ""
        Dim SUMMARY_INVO As String = ""
        Dim INVOICE_NO As String = ""
        Dim LOADING_PORT As String = ""
        Dim DESTINATION As String = ""
        Dim KANNRINO As String = ""
        Dim BOOKING_NO As String = ""
        Dim IFLG As String = ""
        Dim IV_COUNT As String = ""
        Dim CONTAINER As String = ""
        Dim REF01 As String = ""
        Dim REF02 As String = ""
        Dim REV_KANNRINO As String = ""
        Dim SALES As String = ""
        Dim CHECK01 As String = ""
        Dim itkcnt As String = ""
        Dim LCLF As String = ""

        Dim intCnt As Integer

        Dim dt1 As DateTime = DateTime.Now

        Dim ts1 As New TimeSpan(100, 0, 0, 0)
        Dim ts2 As New TimeSpan(100, 0, 0, 0)
        Dim dt2 As DateTime = dt1 + ts1
        Dim dt3 As DateTime = dt1 - ts1

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand

        'データベース接続を開く
        cnn.Open()

        strSQL = ""
        strSQL = strSQL & "SELECT * FROM T_EXL_DECKANRIHYO WHERE "
        strSQL = strSQL & "T_EXL_DECKANRIHYO.INVOICE_NO = '" & strIVNO & "' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()


        '結果を取り出す 
        While (dataread.Read())

            TDATE = dataread("TDATE")
            CUT = dataread("CUT")
            CUST = dataread("CUST")
            SUMMARY_INVO = dataread("SUMMARY_INVO")
            INVOICE_NO = dataread("INVOICE_NO")
            LOADING_PORT = dataread("LOADING_PORT")
            DESTINATION = dataread("DESTINATION")
            KANNRINO = dataread("KANNRINO")
            BOOKING_NO = dataread("BOOKING_NO")
            IFLG = dataread("IFLG")
            IV_COUNT = dataread("IV_COUNT")
            CONTAINER = dataread("CONTAINER")
            REF01 = dataread("REF01")
            REF02 = dataread("REF02")
            REV_KANNRINO = dataread("REV_KANNRINO")
            SALES = dataread("SALES")
            CHECK01 = dataread("CHECK01")

            intCnt = 1

        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()



        strSQL = ""
        strSQL = strSQL & "SELECT * FROM T_EXL_CSANKEN WHERE "
        strSQL = strSQL & "T_EXL_CSANKEN.INVOICE like '%" & strIVNO & "%' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()


        '結果を取り出す 
        While (dataread.Read())

            TDATE = Format(Now(), "yyyy/MM/dd")
            CUT = dataread("CUT_DATE")
            CUST = dataread("CUST")
            SUMMARY_INVO = dataread("INVOICE")
            INVOICE_NO = strIVNO
            LOADING_PORT = dataread("LOADING_PORT")
            DESTINATION = dataread("DESTINATION")
            KANNRINO = ""
            BOOKING_NO = dataread("BOOKING_NO")
            IFLG = ""
            IV_COUNT = ""
            CONTAINER = dataread("CONTAINER")
            REF01 = ""
            REF02 = ""
            REV_KANNRINO = ""
            SALES = ""
            CHECK01 = ""
            LCLF = dataread("LCL_QTY")

        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()




        '既存データの検索
        strSQL = ""
        strSQL = strSQL & "SELECT ITK_BKGNO FROM T_EXL_CSWORKSTATUS WHERE "
        strSQL = strSQL & "T_EXL_CSWORKSTATUS.ITK_BKGNO like '%" & BOOKING_NO & "%' "

        strSQL = ""
        strSQL = strSQL & "SELECT BKGNO FROM T_EXL_WORKSTATUS00 WHERE "
        strSQL = strSQL & "T_EXL_WORKSTATUS00.ID = '001' "
        strSQL = strSQL & "AND T_EXL_WORKSTATUS00.BKGNO = '" & BOOKING_NO & "' "
        strSQL = strSQL & "AND T_EXL_WORKSTATUS00.REGDATE > '" & Format(dt3, "yyyy/MM/dd") & "' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()


        '結果を取り出す 
        While (dataread.Read())

            itkcnt = dataread("bkgno")

        End While


        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()

        If itkcnt <> "" Then

            IFLG = "1"

        End If

        If LCLF = "LCL" Then

            IFLG = "1"

        End If



        '既存データの有無を判定
        If intCnt > 0 Then

            '既存データありの場合、UPDATE
            strSQL = ""
            strSQL = strSQL & "UPDATE T_EXL_DECKANRIHYO SET "
            strSQL = strSQL & "T_EXL_DECKANRIHYO.TDATE = '" & TDATE & "', "
            strSQL = strSQL & "T_EXL_DECKANRIHYO.CUT = '" & CUT & " ', "
            strSQL = strSQL & "T_EXL_DECKANRIHYO.CUST = '" & CUST & " ', "
            strSQL = strSQL & "T_EXL_DECKANRIHYO.SUMMARY_INVO = '" & SUMMARY_INVO & " ', "
            strSQL = strSQL & "T_EXL_DECKANRIHYO.INVOICE_NO = '" & Trim(strIVNO) & " ', "
            strSQL = strSQL & "T_EXL_DECKANRIHYO.LOADING_PORT = '" & LOADING_PORT & " ', "
            strSQL = strSQL & "T_EXL_DECKANRIHYO.DESTINATION = '" & DESTINATION & " ', "

            strSQL = strSQL & "T_EXL_DECKANRIHYO.BOOKING_NO = '" & BOOKING_NO & " ', "
            strSQL = strSQL & "T_EXL_DECKANRIHYO.IFLG = '" & IIf(IFLG = "", 0, IFLG) & "', "
            strSQL = strSQL & "T_EXL_DECKANRIHYO.IV_COUNT = '" & IV_COUNT & " ', "
            strSQL = strSQL & "T_EXL_DECKANRIHYO.CONTAINER = '" & CONTAINER & " ', "
            strSQL = strSQL & "T_EXL_DECKANRIHYO.REF01 = '" & REF01 & " ', "
            strSQL = strSQL & "T_EXL_DECKANRIHYO.Ref02 = '" & "" & " ', "
            strSQL = strSQL & "T_EXL_DECKANRIHYO.REV_KANNRINO = '" & "" & " ' "
            strSQL = strSQL & "WHERE T_EXL_DECKANRIHYO.INVOICE_NO = '" & Trim(strIVNO) & "' "

            Command.CommandText = strSQL
            ' SQLの実行
            Command.ExecuteNonQuery()




        Else


            '既存データが無いのでINSERTする
            strSQL = ""
            strSQL = strSQL & "INSERT INTO T_EXL_DECKANRIHYO VALUES("
            strSQL = strSQL & " '" & TDATE & "', "
            strSQL = strSQL & " '" & IIf(CUT = "", "", CUT) & "', "
            strSQL = strSQL & " '" & IIf(CUST = "", "", CUST) & "', "
            strSQL = strSQL & " '" & IIf(SUMMARY_INVO = "", "", SUMMARY_INVO) & "', "
            strSQL = strSQL & " '" & Trim(strIVNO) & "', "
            strSQL = strSQL & " '" & IIf(LOADING_PORT = "", "", LOADING_PORT) & "', "
            strSQL = strSQL & " '" & IIf(DESTINATION = "", "", DESTINATION) & "', "
            strSQL = strSQL & " '" & IIf(KANNRINO = "", "", KANNRINO) & "', "
            strSQL = strSQL & " '" & IIf(BOOKING_NO = "", "", BOOKING_NO) & "', "
            strSQL = strSQL & " '" & IIf(IFLG = "", "0", IFLG) & "', "
            strSQL = strSQL & " '" & IIf(IV_COUNT = "", "", IV_COUNT) & "', "
            strSQL = strSQL & " '" & IIf(CONTAINER = "", "", CONTAINER) & "', "
            strSQL = strSQL & " '" & IIf(REF01 = "", "", REF01) & "', "
            strSQL = strSQL & " '" & "" & "', "
            strSQL = strSQL & " '" & "" & "', "
            strSQL = strSQL & " '" & "" & "', "
            strSQL = strSQL & " '" & "" & "' "
            strSQL = strSQL & ")"


            Command.CommandText = strSQL
            ' SQLの実行
            Command.ExecuteNonQuery()


        End If





        cnn.Close()
        cnn.Dispose()


    End Sub

    Private Sub mail_update()

        Dim strSQL As String = ""
        Dim ivno As String = ""
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand

        Dim strVal As String

        Dim intCnt As Integer
        Dim itkcnt As String

        Dim dt1 As DateTime = DateTime.Now

        Dim ts1 As New TimeSpan(100, 0, 0, 0)
        Dim ts2 As New TimeSpan(100, 0, 0, 0)
        Dim dt2 As DateTime = dt1 + ts1
        Dim dt3 As DateTime = dt1 - ts1

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand

        'データベース接続を開く
        cnn.Open()

        strVal = Format(Now, "yyyy/MM/dd hh:mm:ss")

        '更新
        strSQL = ""
        strSQL = strSQL & "UPDATE T_EXL_DATA_UPD SET "
        strSQL = strSQL & "DATA_UPD = '" & strVal & "' "
        strSQL = strSQL & "WHERE DATA_CD = '013' "

        Command.CommandText = strSQL
        ' SQLの実行
        Command.ExecuteNonQuery()

        cnn.Close()
        cnn.Dispose()


    End Sub

    Function Cname01(custcode As String) As String

        Dim strSQL As String = ""
        Dim ivno As String = ""
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand

        Dim dt1 As DateTime = DateTime.Now

        Dim ts1 As New TimeSpan(100, 0, 0, 0)
        Dim ts2 As New TimeSpan(100, 0, 0, 0)
        Dim dt2 As DateTime = dt1 + ts1
        Dim dt3 As DateTime = dt1 - ts1

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=svdpo051;Initial Catalog=BPTB001;User Id=ado_bptb001;Password=ado_bptb001"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()


        Cname01 = ""
        strSQL = "SELECT M_CUST_TB.CUSTCODE, M_CUST_TB.CUSTNAME "
        strSQL = strSQL & "FROM M_CUST_TB "
        strSQL = strSQL & "WHERE M_CUST_TB.CUSTCODE = '" & custcode & "' "


        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()


        '結果を取り出す 
        While (dataread.Read())
            Cname01 = Trim(dataread("CUSTNAME"))
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()


    End Function

    'Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click



    '    '最終更新年月日取得
    '    Dim dataread As SqlDataReader
    '    Dim dbcmd As SqlCommand
    '    Dim strSQL As String = ""
    '    Dim strinv As String = ""
    '    Dim strbkg As String = ""


    '    Dim wday As String = ""
    '    Dim wday2 As String = ""
    '    Dim wday3 As String = ""
    '    Dim dt1 As DateTime = DateTime.Now
    '    Dim Kaika00 As String = ""



    '    Dim ts1 As New TimeSpan(80, 0, 0, 0)
    '    Dim ts2 As New TimeSpan(80, 0, 0, 0)
    '    Dim dt2 As DateTime = dt1 + ts1
    '    Dim dt3 As DateTime = dt1 - ts1


    '    Dim WDAY00 As String = ""
    '    Dim WDAY01 As String = ""
    '    Dim WDAY02 As String = ""
    '    Dim WDAY03 As String = ""
    '    Dim WDAY04 As String = ""
    '    Dim WDAY05 As String = ""

    '    Dim WDAYNO00 As String = ""

    '    Dim STRFORWARDER As String = ""
    '    Dim STRFORWARDER02 As String = ""
    '    Dim strcust As String = ""
    '    Dim STRCUT_DATE As String = ""
    '    Dim STRBOOKING_NO As String = ""
    '    Dim STRINVOICE As String = ""

    '    Dim STRFLG01 As String = ""

    '    '接続文字列の作成
    '    Dim ConnectionString As String = String.Empty

    '    'SQL Server認証
    '    ConnectionString = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"

    '    'SqlConnectionクラスの新しいインスタンスを初期化
    '    Dim cnn = New SqlConnection(ConnectionString)
    '    Dim cnn02 = New SqlConnection(ConnectionString)
    '    Dim Command = cnn02.CreateCommand
    '    'データベース接続を開く
    '    cnn.Open()
    '    cnn02.Open()

    '    strSQL = "SELECT T_EXL_CSWORKDAY.WORKDAY_NO FROM T_EXL_CSWORKDAY WHERE T_EXL_CSWORKDAY.WORKDAY <= '" & Format(dt1, "yyyy/MM/dd") & "' "
    '    strSQL = strSQL & "ORDER BY T_EXL_CSWORKDAY.WORKDAY_NO "

    '    'ＳＱＬコマンド作成
    '    dbcmd = New SqlCommand(strSQL, cnn)
    '    'ＳＱＬ文実行
    '    dataread = dbcmd.ExecuteReader()
    '    '結果を取り出す
    '    While (dataread.Read())
    '        WDAYNO00 = dataread("WORKDAY_NO")
    '    End While

    '    'クローズ処理
    '    dataread.Close()
    '    dbcmd.Dispose()


    '    strSQL = ""
    '    strSQL = "SELECT T_EXL_CSWORKDAY.WORKDAY FROM T_EXL_CSWORKDAY WHERE T_EXL_CSWORKDAY.WORKDAY_NO = '" & Val(WDAYNO00) + 1 & "' "
    '    strSQL = strSQL & "ORDER BY T_EXL_CSWORKDAY.WORKDAY_NO "

    '    'ＳＱＬコマンド作成
    '    dbcmd = New SqlCommand(strSQL, cnn)
    '    'ＳＱＬ文実行
    '    dataread = dbcmd.ExecuteReader()
    '    '結果を取り出す
    '    While (dataread.Read())
    '        WDAY00 = dataread("WORKDAY")
    '    End While

    '    'クローズ処理
    '    dataread.Close()
    '    dbcmd.Dispose()

    '    strSQL = ""
    '    strSQL = "SELECT T_EXL_CSWORKDAY.WORKDAY FROM T_EXL_CSWORKDAY WHERE T_EXL_CSWORKDAY.WORKDAY_NO = '" & Val(WDAYNO00) + 2 & "' "
    '    strSQL = strSQL & "ORDER BY T_EXL_CSWORKDAY.WORKDAY_NO "

    '    'ＳＱＬコマンド作成
    '    dbcmd = New SqlCommand(strSQL, cnn)
    '    'ＳＱＬ文実行
    '    dataread = dbcmd.ExecuteReader()
    '    '結果を取り出す
    '    While (dataread.Read())
    '        WDAY01 = dataread("WORKDAY")
    '    End While

    '    'クローズ処理
    '    dataread.Close()
    '    dbcmd.Dispose()

    '    strSQL = ""
    '    strSQL = "SELECT T_EXL_CSWORKDAY.WORKDAY FROM T_EXL_CSWORKDAY WHERE T_EXL_CSWORKDAY.WORKDAY_NO = '" & Val(WDAYNO00) + 3 & "' "
    '    strSQL = strSQL & "ORDER BY T_EXL_CSWORKDAY.WORKDAY_NO "

    '    'ＳＱＬコマンド作成
    '    dbcmd = New SqlCommand(strSQL, cnn)
    '    'ＳＱＬ文実行
    '    dataread = dbcmd.ExecuteReader()
    '    '結果を取り出す
    '    While (dataread.Read())
    '        WDAY02 = dataread("WORKDAY")
    '    End While

    '    'クローズ処理
    '    dataread.Close()
    '    dbcmd.Dispose()







    '    strSQL = ""
    '    strSQL = "SELECT T_EXL_CSWORKDAY.WORKDAY FROM T_EXL_CSWORKDAY WHERE T_EXL_CSWORKDAY.WORKDAY_NO = '" & Val(WDAYNO00) + 1 + DropDownList6.SelectedValue & "' "
    '    strSQL = strSQL & "ORDER BY T_EXL_CSWORKDAY.WORKDAY_NO "

    '    'ＳＱＬコマンド作成
    '    dbcmd = New SqlCommand(strSQL, cnn)
    '    'ＳＱＬ文実行
    '    dataread = dbcmd.ExecuteReader()
    '    '結果を取り出す
    '    While (dataread.Read())
    '        WDAY03 = dataread("WORKDAY")
    '    End While

    '    'クローズ処理
    '    dataread.Close()
    '    dbcmd.Dispose()

    '    strSQL = ""
    '    strSQL = "SELECT T_EXL_CSWORKDAY.WORKDAY FROM T_EXL_CSWORKDAY WHERE T_EXL_CSWORKDAY.WORKDAY_NO = '" & Val(WDAYNO00) + 2 + DropDownList6.SelectedValue & "' "
    '    strSQL = strSQL & "ORDER BY T_EXL_CSWORKDAY.WORKDAY_NO "

    '    'ＳＱＬコマンド作成
    '    dbcmd = New SqlCommand(strSQL, cnn)
    '    'ＳＱＬ文実行
    '    dataread = dbcmd.ExecuteReader()
    '    '結果を取り出す
    '    While (dataread.Read())
    '        WDAY04 = dataread("WORKDAY")
    '    End While

    '    'クローズ処理
    '    dataread.Close()
    '    dbcmd.Dispose()


    '    strSQL = ""
    '    strSQL = "SELECT T_EXL_CSWORKDAY.WORKDAY FROM T_EXL_CSWORKDAY WHERE T_EXL_CSWORKDAY.WORKDAY_NO = '" & Val(WDAYNO00) + 3 + DropDownList6.SelectedValue & "' "
    '    strSQL = strSQL & "ORDER BY T_EXL_CSWORKDAY.WORKDAY_NO "

    '    'ＳＱＬコマンド作成
    '    dbcmd = New SqlCommand(strSQL, cnn)
    '    'ＳＱＬ文実行
    '    dataread = dbcmd.ExecuteReader()
    '    '結果を取り出す
    '    While (dataread.Read())
    '        WDAY05 = dataread("WORKDAY")
    '    End While

    '    'クローズ処理
    '    dataread.Close()
    '    dbcmd.Dispose()


    '    strSQL = ""
    '    strSQL = "SELECT T_EXL_CSANKEN.FORWARDER, T_EXL_CSANKEN.FORWARDER02, T_EXL_CSANKEN.CUST, T_EXL_CSANKEN.INVOICE, T_EXL_CSANKEN.CUT_DATE, T_EXL_CSANKEN.BOOKING_NO "
    '    strSQL = strSQL & "FROM T_EXL_CSANKEN "


    '    'ＳＱＬコマンド作成
    '    dbcmd = New SqlCommand(strSQL, cnn)
    '    'ＳＱＬ文実行
    '    dataread = dbcmd.ExecuteReader()
    '    '結果を取り出す
    '    While (dataread.Read())


    '        STRFORWARDER = dataread("FORWARDER")
    '        STRFORWARDER02 = dataread("FORWARDER02")
    '        strcust = dataread("CUST")
    '        STRCUT_DATE = dataread("CUT_DATE")
    '        STRINVOICE = dataread("INVOICE")
    '        STRBOOKING_NO = dataread("BOOKING_NO")


    '        If Trim(STRFORWARDER) = "近鉄" And DateValue(STRCUT_DATE) >= DateValue(WDAY01) And DateValue(STRCUT_DATE) < DateValue(WDAY04) Then
    '            STRFLG01 = 1
    '        ElseIf Trim(STRFORWARDER) = "日ト" And DateValue(STRCUT_DATE) >= DateValue(WDAY01) And DateValue(STRCUT_DATE) < DateValue(WDAY04) Then
    '            STRFLG01 = 1
    '        ElseIf Trim(STRFORWARDER) = "日通" And DateValue(STRCUT_DATE) >= DateValue(WDAY01) And DateValue(STRCUT_DATE) < DateValue(WDAY04) Then
    '            STRFLG01 = 1
    '        ElseIf Trim(STRFORWARDER) = "郵船" And DateValue(STRCUT_DATE) >= DateValue(WDAY00) And DateValue(STRCUT_DATE) < DateValue(WDAY03) Then
    '            STRFLG01 = 1
    '        ElseIf Trim(STRFORWARDER) = "上野" And Trim(STRFORWARDER02) = "近鉄エクスプレス" And DateValue(STRCUT_DATE) >= DateValue(WDAY04) And DateValue(STRCUT_DATE) < DateValue(WDAY05) Then  '変更AAA
    '            STRFLG01 = 1
    '        ElseIf Trim(STRFORWARDER) = "上野" And DateValue(STRCUT_DATE) >= DateValue(WDAY00) And DateValue(STRCUT_DATE) < DateValue(WDAY03) Then
    '            STRFLG01 = 1
    '        ElseIf Trim(STRFORWARDER) = "その他" And DateValue(STRCUT_DATE) >= DateValue(WDAY00) And DateValue(STRCUT_DATE) < DateValue(WDAY03) Then
    '            STRFLG01 = 1
    '        End If

    '        If STRFLG01 = "1" Then

    '            strSQL = ""
    '            strSQL = strSQL & "UPDATE T_EXL_CSANKEN SET FLG01 = '" & STRFLG01 & "' "
    '            strSQL = strSQL & "WHERE T_EXL_CSANKEN.BOOKING_NO = '" & STRBOOKING_NO & "'"
    '            strSQL = strSQL & "AND T_EXL_CSANKEN.CUST  = '" & strcust & "'"
    '            strSQL = strSQL & "AND T_EXL_CSANKEN.INVOICE = '" & STRINVOICE & "' "

    '            Command.CommandText = strSQL
    '            ' SQLの実行
    '            Command.ExecuteNonQuery()

    '            STRFLG01 = ""

    '        Else
    '            strSQL = ""
    '            strSQL = strSQL & "UPDATE T_EXL_CSANKEN SET FLG01 = '' "
    '            strSQL = strSQL & "WHERE T_EXL_CSANKEN.BOOKING_NO = '" & STRBOOKING_NO & "'"
    '            strSQL = strSQL & "AND T_EXL_CSANKEN.CUST  = '" & strcust & "'"
    '            strSQL = strSQL & "AND T_EXL_CSANKEN.INVOICE = '" & STRINVOICE & "' "

    '            Command.CommandText = strSQL
    '            ' SQLの実行
    '            Command.ExecuteNonQuery()

    '        End If

    '    End While

    '    GridView1.DataBind()


    '    'クローズ処理
    '    dataread.Close()
    '    dbcmd.Dispose()

    '    cnn.Close()
    '    cnn.Dispose()

    '    cnn02.Close()
    '    cnn02.Dispose()


    'End Sub

    Function mailsub(wd01 As String) As String

        '最終更新年月日取得
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim dt1 As DateTime = DateTime.Now

        Dim WDAY00 As String = ""
        Dim WDAY01 As String = ""
        Dim WDAY02 As String = ""
        Dim WDAY03 As String = ""
        Dim WDAY04 As String = ""
        Dim WDAY05 As String = ""

        Dim WDAYNO00 As String = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty

        'SQL Server認証
        ConnectionString = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"

        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)
        Dim cnn02 = New SqlConnection(ConnectionString)
        Dim Command = cnn02.CreateCommand
        'データベース接続を開く
        cnn.Open()
        cnn02.Open()

        Dim a As String = ""
        'a = DropDownList6.SelectedValue
        a = 0

        strSQL = "SELECT T_EXL_CSWORKDAY.WORKDAY_NO FROM T_EXL_CSWORKDAY WHERE T_EXL_CSWORKDAY.WORKDAY <= '" & wd01 & "' "
        strSQL = strSQL & "ORDER BY T_EXL_CSWORKDAY.WORKDAY_NO "

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
        strSQL = "SELECT T_EXL_CSWORKDAY.WORKDAY FROM T_EXL_CSWORKDAY WHERE T_EXL_CSWORKDAY.WORKDAY_NO = '" & Val(WDAYNO00) + Val(a) - 1 & "' "
        strSQL = strSQL & "ORDER BY T_EXL_CSWORKDAY.WORKDAY_NO "

        'ＳＱＬコマンド作成
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行
        dataread = dbcmd.ExecuteReader()
        '結果を取り出す
        While (dataread.Read())
            WDAY00 = dataread("WORKDAY")
        End While

        'If wd01 = WDAY00 Then

        mailsub = wd01

            'Else
            '    mailsub = wd01 & "-" & WDAY00

            'End If


            'クローズ処理
            dataread.Close()
        dbcmd.Dispose()


        cnn.Close()
        cnn.Dispose()


    End Function

    'Private Sub DropDownList6_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList6.SelectedIndexChanged



    '    '最終更新年月日取得
    '    Dim dataread As SqlDataReader
    '    Dim dbcmd As SqlCommand
    '    Dim strSQL As String = ""
    '    Dim strinv As String = ""
    '    Dim strbkg As String = ""


    '    Dim wday As String = ""
    '    Dim wday2 As String = ""
    '    Dim wday3 As String = ""
    '    Dim dt1 As DateTime = DateTime.Now
    '    Dim Kaika00 As String = ""



    '    Dim ts1 As New TimeSpan(80, 0, 0, 0)
    '    Dim ts2 As New TimeSpan(80, 0, 0, 0)
    '    Dim dt2 As DateTime = dt1 + ts1
    '    Dim dt3 As DateTime = dt1 - ts1


    '    Dim WDAY00 As String = ""
    '    Dim WDAY01 As String = ""
    '    Dim WDAY02 As String = ""
    '    Dim WDAY03 As String = ""
    '    Dim WDAY04 As String = ""
    '    Dim WDAY05 As String = ""

    '    Dim WDAYNO00 As String = ""

    '    Dim STRFORWARDER As String = ""
    '    Dim STRFORWARDER02 As String = ""
    '    Dim strcust As String = ""
    '    Dim STRCUT_DATE As String = ""
    '    Dim STRBOOKING_NO As String = ""
    '    Dim STRINVOICE As String = ""

    '    Dim STRFLG01 As String = ""

    '    '接続文字列の作成
    '    Dim ConnectionString As String = String.Empty

    '    'SQL Server認証
    '    ConnectionString = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"

    '    'SqlConnectionクラスの新しいインスタンスを初期化
    '    Dim cnn = New SqlConnection(ConnectionString)
    '    Dim cnn02 = New SqlConnection(ConnectionString)
    '    Dim Command = cnn02.CreateCommand
    '    'データベース接続を開く
    '    cnn.Open()
    '    cnn02.Open()

    '    Dim a As String = ""
    '    a = DropDownList6.SelectedValue

    '    strSQL = "SELECT T_EXL_CSWORKDAY.WORKDAY_NO FROM T_EXL_CSWORKDAY WHERE T_EXL_CSWORKDAY.WORKDAY <= '" & Format(dt1, "yyyy/MM/dd") & "' "
    '    strSQL = strSQL & "ORDER BY T_EXL_CSWORKDAY.WORKDAY_NO "

    '    'ＳＱＬコマンド作成
    '    dbcmd = New SqlCommand(strSQL, cnn)
    '    'ＳＱＬ文実行
    '    dataread = dbcmd.ExecuteReader()
    '    '結果を取り出す
    '    While (dataread.Read())
    '        WDAYNO00 = dataread("WORKDAY_NO")
    '    End While

    '    'クローズ処理
    '    dataread.Close()
    '    dbcmd.Dispose()


    '    strSQL = ""
    '    strSQL = "SELECT T_EXL_CSWORKDAY.WORKDAY FROM T_EXL_CSWORKDAY WHERE T_EXL_CSWORKDAY.WORKDAY_NO = '" & Val(WDAYNO00) + 1 & "' "
    '    strSQL = strSQL & "ORDER BY T_EXL_CSWORKDAY.WORKDAY_NO "

    '    'ＳＱＬコマンド作成
    '    dbcmd = New SqlCommand(strSQL, cnn)
    '    'ＳＱＬ文実行
    '    dataread = dbcmd.ExecuteReader()
    '    '結果を取り出す
    '    While (dataread.Read())
    '        WDAY00 = dataread("WORKDAY")
    '    End While

    '    'クローズ処理
    '    dataread.Close()
    '    dbcmd.Dispose()

    '    strSQL = ""
    '    strSQL = "SELECT T_EXL_CSWORKDAY.WORKDAY FROM T_EXL_CSWORKDAY WHERE T_EXL_CSWORKDAY.WORKDAY_NO = '" & Val(WDAYNO00) + 2 & "' "
    '    strSQL = strSQL & "ORDER BY T_EXL_CSWORKDAY.WORKDAY_NO "

    '    'ＳＱＬコマンド作成
    '    dbcmd = New SqlCommand(strSQL, cnn)
    '    'ＳＱＬ文実行
    '    dataread = dbcmd.ExecuteReader()
    '    '結果を取り出す
    '    While (dataread.Read())
    '        WDAY01 = dataread("WORKDAY")
    '    End While

    '    'クローズ処理
    '    dataread.Close()
    '    dbcmd.Dispose()

    '    strSQL = ""
    '    strSQL = "SELECT T_EXL_CSWORKDAY.WORKDAY FROM T_EXL_CSWORKDAY WHERE T_EXL_CSWORKDAY.WORKDAY_NO = '" & Val(WDAYNO00) + 3 & "' "
    '    strSQL = strSQL & "ORDER BY T_EXL_CSWORKDAY.WORKDAY_NO "

    '    'ＳＱＬコマンド作成
    '    dbcmd = New SqlCommand(strSQL, cnn)
    '    'ＳＱＬ文実行
    '    dataread = dbcmd.ExecuteReader()
    '    '結果を取り出す
    '    While (dataread.Read())
    '        WDAY02 = dataread("WORKDAY")
    '    End While

    '    'クローズ処理
    '    dataread.Close()
    '    dbcmd.Dispose()




    '    strSQL = ""
    '    strSQL = "SELECT T_EXL_CSWORKDAY.WORKDAY FROM T_EXL_CSWORKDAY WHERE T_EXL_CSWORKDAY.WORKDAY_NO = '" & Val(WDAYNO00) + 1 + DropDownList6.SelectedValue & "' "
    '    strSQL = strSQL & "ORDER BY T_EXL_CSWORKDAY.WORKDAY_NO "

    '    'ＳＱＬコマンド作成
    '    dbcmd = New SqlCommand(strSQL, cnn)
    '    'ＳＱＬ文実行
    '    dataread = dbcmd.ExecuteReader()
    '    '結果を取り出す
    '    While (dataread.Read())
    '        WDAY03 = dataread("WORKDAY")
    '    End While

    '    'クローズ処理
    '    dataread.Close()
    '    dbcmd.Dispose()

    '    strSQL = ""
    '    strSQL = "SELECT T_EXL_CSWORKDAY.WORKDAY FROM T_EXL_CSWORKDAY WHERE T_EXL_CSWORKDAY.WORKDAY_NO = '" & Val(WDAYNO00) + 2 + DropDownList6.SelectedValue & "' "
    '    strSQL = strSQL & "ORDER BY T_EXL_CSWORKDAY.WORKDAY_NO "

    '    'ＳＱＬコマンド作成
    '    dbcmd = New SqlCommand(strSQL, cnn)
    '    'ＳＱＬ文実行
    '    dataread = dbcmd.ExecuteReader()
    '    '結果を取り出す
    '    While (dataread.Read())
    '        WDAY04 = dataread("WORKDAY")
    '    End While

    '    'クローズ処理
    '    dataread.Close()
    '    dbcmd.Dispose()


    '    strSQL = ""
    '    strSQL = "SELECT T_EXL_CSWORKDAY.WORKDAY FROM T_EXL_CSWORKDAY WHERE T_EXL_CSWORKDAY.WORKDAY_NO = '" & Val(WDAYNO00) + 3 + DropDownList6.SelectedValue & "' "
    '    strSQL = strSQL & "ORDER BY T_EXL_CSWORKDAY.WORKDAY_NO "

    '    'ＳＱＬコマンド作成
    '    dbcmd = New SqlCommand(strSQL, cnn)
    '    'ＳＱＬ文実行
    '    dataread = dbcmd.ExecuteReader()
    '    '結果を取り出す
    '    While (dataread.Read())
    '        WDAY05 = dataread("WORKDAY")
    '    End While

    '    'クローズ処理
    '    dataread.Close()
    '    dbcmd.Dispose()


    '    strSQL = ""
    '    strSQL = "SELECT T_EXL_CSANKEN.FORWARDER, T_EXL_CSANKEN.FORWARDER02, T_EXL_CSANKEN.CUST, T_EXL_CSANKEN.INVOICE, T_EXL_CSANKEN.CUT_DATE, T_EXL_CSANKEN.BOOKING_NO "
    '    strSQL = strSQL & "FROM T_EXL_CSANKEN "


    '    'ＳＱＬコマンド作成
    '    dbcmd = New SqlCommand(strSQL, cnn)
    '    'ＳＱＬ文実行
    '    dataread = dbcmd.ExecuteReader()
    '    '結果を取り出す
    '    While (dataread.Read())


    '        STRFORWARDER = dataread("FORWARDER")
    '        STRFORWARDER02 = dataread("FORWARDER02")
    '        strcust = dataread("CUST")
    '        STRCUT_DATE = dataread("CUT_DATE")
    '        STRINVOICE = dataread("INVOICE")
    '        STRBOOKING_NO = dataread("BOOKING_NO")


    '        If Trim(STRFORWARDER) = "近鉄" And DateValue(STRCUT_DATE) >= DateValue(WDAY01) And DateValue(STRCUT_DATE) < DateValue(WDAY04) Then
    '            STRFLG01 = 1
    '        ElseIf Trim(STRFORWARDER) = "日ト" And DateValue(STRCUT_DATE) >= DateValue(WDAY01) And DateValue(STRCUT_DATE) < DateValue(WDAY04) Then
    '            STRFLG01 = 1
    '        ElseIf Trim(STRFORWARDER) = "日通" And DateValue(STRCUT_DATE) >= DateValue(WDAY01) And DateValue(STRCUT_DATE) < DateValue(WDAY04) Then
    '            STRFLG01 = 1
    '        ElseIf Trim(STRFORWARDER) = "郵船" And DateValue(STRCUT_DATE) >= DateValue(WDAY00) And DateValue(STRCUT_DATE) < DateValue(WDAY03) Then
    '            STRFLG01 = 1
    '        ElseIf Trim(STRFORWARDER) = "上野" And Trim(STRFORWARDER02) = "近鉄エクスプレス" And DateValue(STRCUT_DATE) >= DateValue(WDAY04) And DateValue(STRCUT_DATE) < DateValue(WDAY05) Then  '変更AAA
    '            STRFLG01 = 1
    '        ElseIf Trim(STRFORWARDER) = "上野" And DateValue(STRCUT_DATE) >= DateValue(WDAY00) And DateValue(STRCUT_DATE) < DateValue(WDAY03) Then
    '            STRFLG01 = 1
    '        ElseIf Trim(STRFORWARDER) = "その他" And DateValue(STRCUT_DATE) >= DateValue(WDAY00) And DateValue(STRCUT_DATE) < DateValue(WDAY03) Then
    '            STRFLG01 = 1
    '        End If

    '        If STRFLG01 = "1" Then

    '            strSQL = ""
    '            strSQL = strSQL & "UPDATE T_EXL_CSANKEN SET FLG01 = '" & STRFLG01 & "' "
    '            strSQL = strSQL & "WHERE T_EXL_CSANKEN.BOOKING_NO = '" & STRBOOKING_NO & "'"
    '            strSQL = strSQL & "AND T_EXL_CSANKEN.CUST  = '" & strcust & "'"
    '            strSQL = strSQL & "AND T_EXL_CSANKEN.INVOICE = '" & STRINVOICE & "' "

    '            Command.CommandText = strSQL
    '            ' SQLの実行
    '            Command.ExecuteNonQuery()

    '            STRFLG01 = ""

    '        Else
    '            strSQL = ""
    '            strSQL = strSQL & "UPDATE T_EXL_CSANKEN SET FLG01 = '' "
    '            strSQL = strSQL & "WHERE T_EXL_CSANKEN.BOOKING_NO = '" & STRBOOKING_NO & "'"
    '            strSQL = strSQL & "AND T_EXL_CSANKEN.CUST  = '" & strcust & "'"
    '            strSQL = strSQL & "AND T_EXL_CSANKEN.INVOICE = '" & STRINVOICE & "' "

    '            Command.CommandText = strSQL
    '            ' SQLの実行
    '            Command.ExecuteNonQuery()

    '        End If

    '    End While


    '    strSQL = ""
    '    strSQL = strSQL & "UPDATE M_EXL_KAIKA_CHANGE SET NAME_EG = '" & DropDownList6.SelectedValue & "' "
    '    strSQL = strSQL & "WHERE M_EXL_KAIKA_CHANGE.NAME_JPN = 'DAYS'"


    '    Command.CommandText = strSQL
    '    ' SQLの実行
    '    Command.ExecuteNonQuery()

    '    GridView1.DataBind()


    '    'クローズ処理
    '    dataread.Close()
    '    dbcmd.Dispose()

    '    cnn.Close()
    '    cnn.Dispose()

    '    cnn02.Close()
    '    cnn02.Dispose()

    'End Sub
End Class
