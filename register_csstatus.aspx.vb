﻿
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.Common
Partial Class yuusen
    Inherits System.Web.UI.Page

    Public strRow As String
    Public strProcess As String


    Protected Sub DropDownList6_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList6.SelectedIndexChanged

        Dim strcust As String = ""
        Dim strbkg As String = ""

        Call get_ivdata(DropDownList6.Text, strcust, strbkg)

        DropDownList1.SelectedValue = ""
        DropDownList2.SelectedValue = ""
        DropDownList3.SelectedValue = ""
        DropDownList4.SelectedValue = ""
        DropDownList5.SelectedValue = ""

        Label3.Text = strcust
        Label4.Text = strbkg
        Label5.Text = ""
        Label6.Text = ""

    End Sub

    Protected Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList1.SelectedIndexChanged

        Dim strcust As String = ""
        Dim strbkg As String = ""

        Call get_ivdata2(DropDownList1.Text, strcust, strbkg)


        DropDownList6.SelectedValue = ""
        DropDownList2.SelectedValue = ""
        DropDownList3.SelectedValue = ""
        DropDownList4.SelectedValue = ""
        DropDownList5.SelectedValue = ""

        Label5.Text = strcust
        Label6.Text = strbkg
        Label3.Text = ""
        Label4.Text = ""

    End Sub

    Protected Sub DropDownList2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList2.SelectedIndexChanged

        DropDownList6.SelectedValue = ""
        DropDownList1.SelectedValue = ""
        DropDownList3.SelectedValue = ""
        DropDownList4.SelectedValue = ""
        DropDownList5.SelectedValue = ""

        Label5.Text = ""
        Label6.Text = ""
        Label3.Text = ""
        Label4.Text = ""

    End Sub

    Protected Sub DropDownList3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList3.SelectedIndexChanged

        DropDownList6.SelectedValue = ""
        DropDownList1.SelectedValue = ""
        DropDownList2.SelectedValue = ""
        DropDownList4.SelectedValue = ""
        DropDownList5.SelectedValue = ""

        Label5.Text = ""
        Label6.Text = ""
        Label3.Text = ""
        Label4.Text = ""

    End Sub

    Protected Sub DropDownList4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList4.SelectedIndexChanged

        DropDownList6.SelectedValue = ""
        DropDownList1.SelectedValue = ""
        DropDownList2.SelectedValue = ""
        DropDownList3.SelectedValue = ""
        DropDownList5.SelectedValue = ""

        Label5.Text = ""
        Label6.Text = ""
        Label3.Text = ""
        Label4.Text = ""

    End Sub

    Protected Sub DropDownList5_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList5.SelectedIndexChanged

        DropDownList6.SelectedValue = ""
        DropDownList1.SelectedValue = ""
        DropDownList2.SelectedValue = ""
        DropDownList4.SelectedValue = ""
        DropDownList3.SelectedValue = ""

        Label5.Text = ""
        Label6.Text = ""
        Label3.Text = ""
        Label4.Text = ""

    End Sub
    Private Sub get_ivdata(iptbx As String, ByRef strcust As String, ByRef strbkg As String)

        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String = ""
        Dim strinv As String = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        Dim ConnectionString2 As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=svdpo051;Initial Catalog=BPTB001;User Id=ado_bptb001;Password=ado_bptb001"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        Dim dt1 As DateTime = DateTime.Now

        Dim ts1 As New TimeSpan(5, 0, 0, 0)
        Dim ts2 As New TimeSpan(60, 0, 0, 0)
        Dim dt2 As DateTime = dt1 + ts2
        Dim dt3 As DateTime = dt1 - ts1

        'データベース接続を開く
        cnn.Open()


        strSQL = "SELECT OLD_INVNO,BOOKINGNO "
        strSQL = strSQL & "FROM T_INV_HD_TB "
        strSQL = strSQL & "WHERE OLD_INVNO ='" & iptbx & "' "
        strSQL = strSQL & "AND BLDATE  between '" & dt3 & "' AND '" & dt2 & "' "

        'INVOICENOが最大のものを取得
        strSQL = strSQL & "AND T_INV_HD_TB.INVOICENO = "
        strSQL = strSQL & "(SELECT MAX(T_INV_HD_TB.INVOICENO) AS IVNO "
        strSQL = strSQL & "FROM T_INV_HD_TB "

        strSQL = strSQL & "WHERE T_INV_HD_TB.HEADTITLE Like 'INVOICE%' "
        strSQL = strSQL & "AND T_INV_HD_TB.OLD_INVNO ='" & iptbx & "' "
        strSQL = strSQL & "AND T_INV_HD_TB.BLDATE between '" & dt3 & "' AND '" & dt2 & "' "

        strSQL = strSQL & "AND T_INV_HD_TB.ORG_INVOICENO IS NULL "
        strSQL = strSQL & ") "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        '結果を取り出す 
        While (dataread.Read())
            strbkg = Trim(Convert.ToString(dataread("BOOKINGNO")))        '客先目
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()

        cnn.Close()
        cnn.Dispose()


        '接続文字列の作成
        'SQL Server認証
        ConnectionString2 = "Data Source=svdpo051;Initial Catalog=BPTB001;User Id=ado_bptb001;Password=ado_bptb001"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn2 = New SqlConnection(ConnectionString2)
        'データベース接続を開く
        cnn2.Open()


        strSQL = "SELECT DISTINCT T_INV_HD_TB.CUSTCODE "
        strSQL = strSQL & "FROM T_INV_HD_TB "
        strSQL = strSQL & "WHERE "
        strSQL = strSQL & "T_INV_HD_TB.BOOKINGNO = '" & strbkg & "' "
        strSQL = strSQL & "AND T_INV_HD_TB.OLD_INVNO = '" & iptbx & "' "


        'INVOICENOが最大のものを取得
        strSQL = strSQL & "AND T_INV_HD_TB.INVOICENO = "
        strSQL = strSQL & "(SELECT MAX(T_INV_HD_TB.INVOICENO) AS IVNO "
        strSQL = strSQL & "FROM T_INV_HD_TB "

        strSQL = strSQL & "WHERE T_INV_HD_TB.HEADTITLE Like 'INVOICE%' "
        strSQL = strSQL & "AND T_INV_HD_TB.OLD_INVNO = '" & iptbx & "' "
        strSQL = strSQL & "AND T_INV_HD_TB.BOOKINGNO = '" & strbkg & "' "
        strSQL = strSQL & "AND T_INV_HD_TB.ORG_INVOICENO IS NULL "
        strSQL = strSQL & ") "
        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn2)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        '結果を取り出す 
        While (dataread.Read())
            strcust = Trim(Convert.ToString(dataread("CUSTCODE")))        '客先目
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()

        cnn2.Close()
        cnn2.Dispose()


    End Sub

    Private Sub get_ivdata2(iptbx As String, ByRef strcust As String, ByRef strbkg As String)

        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String = ""
        Dim strinv As String = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        Dim ConnectionString2 As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        Dim dt1 As DateTime = DateTime.Now

        Dim ts1 As New TimeSpan(60, 0, 0, 0)
        Dim ts2 As New TimeSpan(100, 0, 0, 0)
        Dim dt2 As DateTime = dt1 + ts1
        Dim dt3 As DateTime = dt1 - ts1

        'データベース接続を開く
        cnn.Open()


        strSQL = "SELECT INVNO,BKGNO "
        strSQL = strSQL & "FROM T_EXL_WORKSTATUS00 "
        strSQL = strSQL & "WHERE INVNO ='" & iptbx & "' "
        strSQL = strSQL & "AND REGDATE  > '" & dt3 & "' "
        strSQL = strSQL & "AND ID='001' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        '結果を取り出す 
        While (dataread.Read())
            strbkg = Trim(Convert.ToString(dataread("BKGNO")))        '客先目
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()

        cnn.Close()
        cnn.Dispose()


        '接続文字列の作成
        'SQL Server認証
        ConnectionString2 = "Data Source=svdpo051;Initial Catalog=BPTB001;User Id=ado_bptb001;Password=ado_bptb001"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn2 = New SqlConnection(ConnectionString2)
        'データベース接続を開く
        cnn2.Open()


        strSQL = "SELECT DISTINCT T_INV_HD_TB.CUSTCODE "
        strSQL = strSQL & "FROM T_INV_HD_TB "
        strSQL = strSQL & "WHERE "
        strSQL = strSQL & "T_INV_HD_TB.BOOKINGNO = '" & strbkg & "' "
        strSQL = strSQL & "AND T_INV_HD_TB.OLD_INVNO = '" & iptbx & "' "



        'INVOICENOが最大のものを取得
        strSQL = strSQL & "AND T_INV_HD_TB.INVOICENO = "
        strSQL = strSQL & "(SELECT MAX(T_INV_HD_TB.INVOICENO) AS IVNO "
        strSQL = strSQL & "FROM T_INV_HD_TB "

        strSQL = strSQL & "WHERE T_INV_HD_TB.HEADTITLE Like 'INVOICE%' "
        strSQL = strSQL & "AND T_INV_HD_TB.OLD_INVNO = '" & iptbx & "' "
        strSQL = strSQL & "AND T_INV_HD_TB.BOOKINGNO = '" & strbkg & "' "
        strSQL = strSQL & "AND T_INV_HD_TB.ORG_INVOICENO IS NULL "
        strSQL = strSQL & ") "
        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn2)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        '結果を取り出す 
        While (dataread.Read())
            strcust = Trim(Convert.ToString(dataread("CUSTCODE")))        '客先目
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()

        cnn2.Close()
        cnn2.Dispose()


    End Sub
    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        'Dim strtxt As String = TextBox1.Text
        Dim strdrp As String = DropDownList6.Text
        Dim strbkgno As String

        Call GET_BKGNO(strdrp, strbkgno, "1")


        Label5.Text = ""
        Label6.Text = ""
        Label3.Text = ""
        Label4.Text = ""

        DropDownList6.Items.Clear()
        DropDownList6.DataBind()
        DropDownList6.Items.Insert(0, "Please Select")

        Page.ClientScript.RegisterStartupScript(Me.GetType, "確認", "<script language='JavaScript'>confirm('登録しました。');</script>", False)


    End Sub
    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Dim strdrp As String = DropDownList1.Text
        Dim strbkgno As String

        Call GET_BKGNO(strdrp, strbkgno, "2")

        DropDownList1.Items.Clear()
        DropDownList1.DataBind()
        DropDownList1.Items.Insert(0, "Please Select")


        Label5.Text = ""
        Label6.Text = ""
        Label3.Text = ""
        Label4.Text = ""


        Page.ClientScript.RegisterStartupScript(Me.GetType, "確認", "<script language='JavaScript'>confirm('削除しました。');</script>", False)



    End Sub
    Protected Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        Dim strtxt As String = DropDownList4.Text

        Call INS_ITKCUST(strtxt)



        DropDownList4.Items.Clear()
        DropDownList4.DataBind()
        DropDownList4.Items.Insert(0, "Please Select")


        Page.ClientScript.RegisterStartupScript(Me.GetType, "確認", "<script language='JavaScript'>confirm('登録しました。');</script>", False)


    End Sub

    Protected Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        Dim strdrp As String = DropDownList2.Text

        Call DEL_ITKCUST(strdrp)


        DropDownList2.Items.Clear()
        DropDownList2.DataBind()
        DropDownList2.Items.Insert(0, "Please Select")


        Page.ClientScript.RegisterStartupScript(Me.GetType, "確認", "<script language='JavaScript'>confirm('削除しました。');</script>", False)


    End Sub
    Protected Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click

        Dim strtxt As String = DropDownList5.Text

        Call INS_ITKKAIKA(strtxt)



        DropDownList5.Items.Clear()
        DropDownList5.DataBind()
        DropDownList5.Items.Insert(0, "Please Select")


        Page.ClientScript.RegisterStartupScript(Me.GetType, "確認", "<script language='JavaScript'>confirm('登録しました。');</script>", False)


    End Sub
    Protected Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click

        Dim strdrp As String = DropDownList3.Text

        Call DEL_ITKKAIKA(strdrp)

        DropDownList3.Items.Clear()
        DropDownList3.DataBind()
        DropDownList3.Items.Insert(0, "Please Select")



        Page.ClientScript.RegisterStartupScript(Me.GetType, "確認", "<script language='JavaScript'>confirm('削除しました。');</script>", False)


    End Sub

    Private Sub GET_BKGNO(strivno As String, strbkgno As String, flg01 As String)

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

        Dim dt1 As DateTime = DateTime.Now

        Dim ts1 As New TimeSpan(100, 0, 0, 0)
        Dim dt2 As DateTime = dt1 + ts1
        Dim dt3 As DateTime = dt1 - ts1

        cnn.Open()

        strSQL = "Select distinct T_INV_HD_TB.BOOKINGNO "
        strSQL = strSQL & "FROM T_INV_HD_TB LEFT JOIN T_INV_BD_TB On T_INV_HD_TB.INVOICENO = T_INV_BD_TB.INVOICENO "
        strSQL = strSQL & "WHERE T_INV_HD_TB.BLDATE > '" & dt3.ToShortDateString & "' "

        'INVOICENOが最大のものを取得
        strSQL = strSQL & "AND T_INV_HD_TB.INVOICENO = "
        strSQL = strSQL & "(SELECT MAX(T_INV_HD_TB.INVOICENO) AS IVNO "
        strSQL = strSQL & "FROM T_INV_HD_TB "

        strSQL = strSQL & "WHERE T_INV_HD_TB.HEADTITLE Like 'INVOICE%' "
        strSQL = strSQL & "AND T_INV_HD_TB.OLD_INVNO like '%" & strivno & "%' "
        strSQL = strSQL & "AND T_INV_HD_TB.BLDATE > '" & dt3.ToShortDateString & "' "
        strSQL = strSQL & "AND T_INV_HD_TB.BOOKINGNO IS NOT NULL "
        strSQL = strSQL & "AND T_INV_HD_TB.ORG_INVOICENO IS NULL "
        strSQL = strSQL & ") "

        strSQL = strSQL & "GROUP BY T_INV_HD_TB.BOOKINGNO, T_INV_HD_TB.OLD_INVNO, T_INV_HD_TB.SHIPPEDPER, T_INV_HD_TB.VOYAGENO, T_INV_HD_TB.IOPORTDATE, T_INV_HD_TB.CUTDATE "
        strSQL = strSQL & "HAVING T_INV_HD_TB.OLD_INVNO like '%" & strivno & "%' "

        strSQL = strSQL & "AND BOOKINGNO is not null "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        strDate = ""
        '結果を取り出す 
        While (dataread.Read())
            strbkgno = Trim(Convert.ToString(dataread("BOOKINGNO")))        'ETD(計上日)
        End While

        If strbkgno <> "" Then
            If flg01 = "1" Then
                Call GET_IVDATA(strbkgno, "1")
            ElseIf flg01 = "2" Then
                Call GET_IVDATA(strbkgno, "2")
            End If
        Else
        End If

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
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

        strSQL = strSQL & "order by T_INV_HD_TB.CUTDATE DESC "

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
            ElseIf flg = "2" Then
                Call DEL_ITKIVNO(strinv, bkgno)
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
            strSQL = strSQL & ",'" & Session("UsrId") & "_08" & "' "
            strSQL = strSQL & ")"

        End If

        Command.CommandText = strSQL
        ' SQLの実行
        Command.ExecuteNonQuery()

        cnn.Close()
        cnn.Dispose()

    End Sub

    Private Sub DEL_ITKIVNO(strinv As String, bkgno As String)

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
        strSQL = strSQL & "DELETE FROM T_EXL_WORKSTATUS00 "
        strSQL = strSQL & "WHERE T_EXL_WORKSTATUS00.ID = '001' "
        strSQL = strSQL & "AND T_EXL_WORKSTATUS00.INVNO ='" & strinv & "' "
        strSQL = strSQL & "AND T_EXL_WORKSTATUS00.BKGNO ='" & bkgno & "' "

        Command.CommandText = strSQL
        ' SQLの実行
        Command.ExecuteNonQuery()

        cnn.Close()
        cnn.Dispose()

    End Sub

    Private Sub INS_ITKCUST(strcust As String)
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
        strSQL = strSQL & "SELECT COUNT(*) AS RecCnt FROM T_EXL_ITAKU00 WHERE "
        strSQL = strSQL & "T_EXL_ITAKU00.ITKNAME = '" & strcust & "' "
        strSQL = strSQL & "AND T_EXL_ITAKU00.KBN = '002' "

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
        Else

            strSQL = ""
            strSQL = strSQL & "INSERT INTO T_EXL_ITAKU00 VALUES("
            strSQL = strSQL & "'002' "
            strSQL = strSQL & ",'" & strcust & "' "
            strSQL = strSQL & ")"

            Command.CommandText = strSQL
            ' SQLの実行
            Command.ExecuteNonQuery()

        End If

        cnn.Close()
        cnn.Dispose()

    End Sub
    Private Sub INS_ITKKAIKA(strkik As String)
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
        strSQL = strSQL & "SELECT COUNT(*) AS RecCnt FROM T_EXL_ITAKU00 WHERE "
        strSQL = strSQL & "T_EXL_ITAKU00.ITKNAME = '" & strkik & "' "
        strSQL = strSQL & "AND T_EXL_ITAKU00.KBN = '001' "

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
        Else
            strSQL = ""
            strSQL = strSQL & "INSERT INTO T_EXL_ITAKU00 VALUES("
            strSQL = strSQL & "'001' "
            strSQL = strSQL & ",'" & strkik & "' "
            strSQL = strSQL & ")"

            Command.CommandText = strSQL
            ' SQLの実行
            Command.ExecuteNonQuery()

        End If

        cnn.Close()
        cnn.Dispose()

    End Sub



    Private Sub DEL_ITKCUST(strcust As String)
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
        strSQL = strSQL & "DELETE FROM T_EXL_ITAKU00 "
        strSQL = strSQL & "WHERE T_EXL_ITAKU00.KBN = '002' "
        strSQL = strSQL & "AND T_EXL_ITAKU00.ITKNAME ='" & strcust & "' "



        Command.CommandText = strSQL
        ' SQLの実行
        Command.ExecuteNonQuery()

        cnn.Close()
        cnn.Dispose()

    End Sub

    Private Sub DEL_ITKKAIKA(strkik As String)
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
        strSQL = strSQL & "DELETE FROM T_EXL_ITAKU00 "
        strSQL = strSQL & "WHERE T_EXL_ITAKU00.KBN = '001' "
        strSQL = strSQL & "AND T_EXL_ITAKU00.ITKNAME ='" & strkik & "' "

        Command.CommandText = strSQL
        ' SQLの実行
        Command.ExecuteNonQuery()

        cnn.Close()
        cnn.Dispose()

    End Sub

End Class
