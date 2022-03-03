
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.Common
Partial Class yuusen
    Inherits System.Web.UI.Page

    Public strRow As String
    Public strProcess As String




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

        strSQL = "SELECT distinct T_INV_HD_TB.BOOKINGNO "
        strSQL = strSQL & "FROM T_INV_HD_TB LEFT JOIN T_INV_BD_TB ON T_INV_HD_TB.INVOICENO = T_INV_BD_TB.INVOICENO "

        strSQL = strSQL & "WHERE T_INV_HD_TB.BLDATE > '" & dt3.ToShortDateString & "' "

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

            strbkgno = Convert.ToString(dataread("BOOKINGNO"))        'ETD(計上日)


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

        'データベース接続を開く
        cnn.Open()


        strSQL = "SELECT T_INV_HD_TB.OLD_INVNO "
        strSQL = strSQL & "FROM T_INV_HD_TB LEFT JOIN T_INV_BD_TB ON T_INV_HD_TB.INVOICENO = T_INV_BD_TB.INVOICENO "
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

            ElseIf flg = "2"

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

    Private Sub DEL_ITKIVNO(strinv As String, bkgno As String)

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
        'strSQL = strSQL & "UPDATE T_EXL_CSWORKSTATUS SET "
        'strSQL = strSQL & "T_EXL_CSWORKSTATUS.ITK_INVNO = '', "
        'strSQL = strSQL & "T_EXL_CSWORKSTATUS.ITK_REGDATE = '', "
        'strSQL = strSQL & "T_EXL_CSWORKSTATUS.ITK_BKGNO = '' "
        'strSQL = strSQL & "WHERE T_EXL_CSWORKSTATUS.ITK_INVNO ='" & strinv & "' "
        'strSQL = strSQL & "AND T_EXL_CSWORKSTATUS.ITK_BKGNO ='" & bkgno & "' "

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
        strSQL = strSQL & "SELECT COUNT(*) AS RecCnt FROM T_EXL_ITAKU WHERE "
        strSQL = strSQL & "T_EXL_ITAKU.CUST_CD = '" & strcust & "' "


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
            strSQL = strSQL & "INSERT INTO T_EXL_ITAKU VALUES("

            strSQL = strSQL & "'" & strcust & "' "
            strSQL = strSQL & ",'" & " ' "

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
        strSQL = strSQL & "SELECT COUNT(*) AS RecCnt FROM T_EXL_ITAKU WHERE "
        strSQL = strSQL & "T_EXL_ITAKU.FORWARDER = '" & strkik & "' "


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
            strSQL = strSQL & "INSERT INTO T_EXL_ITAKU VALUES("

            strSQL = strSQL & "'" & " ' "

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
        strSQL = strSQL & "UPDATE T_EXL_ITAKU SET "
        strSQL = strSQL & "T_EXL_ITAKU.CUST_CD = '' "
        strSQL = strSQL & "WHERE T_EXL_ITAKU.CUST_CD ='" & strcust & "' "

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
        strSQL = strSQL & "UPDATE T_EXL_ITAKU SET "
        strSQL = strSQL & "T_EXL_ITAKU.FORWARDER = '' "
        strSQL = strSQL & "WHERE T_EXL_ITAKU.FORWARDER ='" & strkik & "' "

        Command.CommandText = strSQL
        ' SQLの実行
        Command.ExecuteNonQuery()



        cnn.Close()
        cnn.Dispose()

    End Sub

    Protected Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList1.SelectedIndexChanged

        Label3.Text = DropDownList1.Text

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim adress As String = ""

        adress = TextBox1.Text & "__" & TextBox2.Text & "__" & TextBox3.Text

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
        strSQL = strSQL & "INSERT INTO T_EXL_LCLADDRESS VALUES("
        strSQL = strSQL & "'" & adress & "' "
        strSQL = strSQL & ",'' "
        strSQL = strSQL & ",'' "
        strSQL = strSQL & ")"



        Command.CommandText = strSQL
        ' SQLの実行
        Command.ExecuteNonQuery()



        cnn.Close()
        cnn.Dispose()


        DropDownList1.Items.Clear()
        'DropDownList1.DataSource = SqlDataSource8
        DropDownList1.DataBind()
        DropDownList1.Items.Insert(0, "--Select--")

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click


        Dim adress As String = ""

        adress = DropDownList2.Text

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
        strSQL = strSQL & "INSERT INTO T_EXL_LCLADDRESS VALUES("
        strSQL = strSQL & "'" & adress & "' "
        strSQL = strSQL & ",'' "
        strSQL = strSQL & ",'' "
        strSQL = strSQL & ")"


        strSQL = ""
        strSQL = strSQL & "DELETE FROM T_EXL_LCLADDRESS "
        strSQL = strSQL & "WHERE T_EXL_LCLADDRESS.ADDRESS = '" & adress & "' "




        Command.CommandText = strSQL
        ' SQLの実行
        Command.ExecuteNonQuery()



        cnn.Close()
        cnn.Dispose()


        DropDownList1.Items.Clear()
        'DropDownList1.DataSource = SqlDataSource8
        DropDownList1.DataBind()
        DropDownList1.Items.Insert(0, "--Select--")

        DropDownList2.Items.Clear()
        'DropDownList1.DataSource = SqlDataSource8
        DropDownList2.DataBind()
        DropDownList2.Items.Insert(0, "--Select--")


    End Sub
End Class
