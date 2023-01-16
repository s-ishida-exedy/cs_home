Imports System.Data.SqlClient
Imports mod_function

Partial Class cs_home
    Inherits System.Web.UI.Page

    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load
        Dim strinv As String = ""
        Dim strbkg As String = ""
        Dim strSQL As String = ""
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strMode As String = ""

        TextBox1.ReadOnly = True
        TextBox2.ReadOnly = True
        TextBox3.ReadOnly = True
        TextBox4.ReadOnly = True
        TextBox5.ReadOnly = True
        TextBox6.ReadOnly = True


        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=KBHWPM02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand
        Dim ivno As String = ""

        Dim intCnt As Long

        Dim dt1 As DateTime = DateTime.Now

        'データベース接続を開く
        cnn.Open()

        strSQL = ""
        strSQL = strSQL & "SELECT T_EXL_CSANKEN.* "
        strSQL = strSQL & "FROM T_EXL_CSANKEN "
        strSQL = strSQL & "WHERE "
        strSQL = strSQL & "T_EXL_CSANKEN.INVOICE = '" & DropDownList1.SelectedValue & "' "
        strSQL = strSQL & "AND (T_EXL_CSANKEN.INVOICE IS NOT NULL or T_EXL_CSANKEN.INVOICE <>'') "

        If DropDownList1.SelectedValue = "" Then
        Else
            'ＳＱＬコマンド作成 
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行 
            dataread = dbcmd.ExecuteReader()

            While (dataread.Read())
                TextBox1.Text = dataread("CUST")
                TextBox2.Text = dataread("DESTINATION")
                TextBox3.Text = dataread("CUT_DATE")
                TextBox4.Text = dataread("ETD")
                TextBox5.Text = dataread("BOOKING_NO")
                TextBox6.Text = dataread("FORWARDER02")
            End While

            'クローズ処理 
            dataread.Close()
            dbcmd.Dispose()


        End If



        cnn.Close()
        cnn.Dispose()



    End Sub

    Private Sub DB_access(strExecMode As String)
        '画面入力情報をテーブルへ登録

        Dim strSQL As String = ""
        Dim strVal As String = ""
        Dim strkbn As String = ""
        Dim strref As String = ""
        Dim strbkg As String = ""


        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand

        'データベース接続を開く
        cnn.Open()

        '接続文字列の作成
        Dim ConnectionString02 As String = String.Empty
        'SQL Server認証
        ConnectionString02 = "Data Source=svdpo051;Initial Catalog=BPTB001;User Id=ado_bptb001;Password=ado_bptb001"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn02 = New SqlConnection(ConnectionString02)
        Dim Command02 = cnn02.CreateCommand

        'データベース接続を開く
        cnn02.Open()

        Dim str00 As String = ""
        Dim str01 As String = ""
        Dim str02 As String = ""
        Dim str03 As String = ""
        Dim str04 As String = ""
        Dim str05 As String = ""
        Dim str06 As String = ""
        Dim str07 As String = ""

        str00 = Trim(DropDownList1.SelectedValue)
        str01 = Trim(TextBox1.Text)
        str02 = Trim(TextBox2.Text)
        str03 = Trim(TextBox3.Text)
        str04 = Trim(TextBox4.Text)
        str05 = Trim(TextBox5.Text)
        str06 = Trim(TextBox6.Text)
        str07 = Trim(DropDownList3.SelectedValue)

        If str07 = "0" Then
            str07 = "0"
        End If

        Dim strvan As String = ""
        Dim strtime As String = ""
        Dim strfwd As String = ""
        Dim strcus As String = ""
        Dim strdes As String = ""
        Dim strinv As String = ""
        Dim strcut As String = ""
        Dim stretd As String = ""
        Dim strcon As String = ""
        Dim strsta As String = ""
        Dim strd As String = ""

        Dim strvessle As String = ""
        Dim strvy As String = ""
        Dim strrp As String = ""
        Dim strlp As String = ""
        Dim strdp As String = ""
        Dim strpd As String = ""
        Dim val01 As String = ""
        Dim flgka00 As String = "0"
        Dim flgka01 As String = "0"
        Dim flgka02 As String = ""


        strSQL = ""
        strSQL = strSQL & "SELECT  "
        strSQL = strSQL & "  * "
        strSQL = strSQL & "FROM T_EXL_CSANKEN "
        strSQL = strSQL & "WHERE T_EXL_CSANKEN.INVOICE = '" & str00 & "' "
        strSQL = strSQL & "AND T_EXL_CSANKEN.BOOKING_NO = '" & str05 & "' "



        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        '結果を取り出す 
        While (dataread.Read())


            strvan = Trim(dataread("FINALVANDATE"))
            strfwd = Trim(dataread("FORWARDER02"))
            strcus = dataread("CUST")
            strdes = dataread("DESTINATION")
            strinv = dataread("INVOICE")
            strcut = dataread("CUT_DATE")
            stretd = dataread("ETD")
            strcon = dataread("CONTAINER")
            strsta = dataread("STATUS")
            strbkg = dataread("BOOKING_NO")
            strvessle = dataread("VESSEL_NAME")
            strvy = dataread("VOYAGE_NO")
            strrp = dataread("PLACE_OF_RECEIPT")
            strlp = dataread("LOADING_PORT")
            strdp = dataread("DISCHARGING_PORT")
            strpd = dataread("PLACE_OF_DELIVERY")

        End While


        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()




        strSQL = ""
        strSQL = strSQL & "SELECT  "
        strSQL = strSQL & " count(*)+1  AS C "
        strSQL = strSQL & "FROM T_EXL_CSKANRYO "



        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        '結果を取り出す 
        While (dataread.Read())
            val01 = Trim(dataread("C"))
        End While

        If val01 < 10 Then
            val01 = "0" & val01
        End If

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()


        strtime = "手動登録"
        strvan = "手動登録"


        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()

        flgka02 = ""
        flgka00 = "0"
        flgka01 = "0"

        Dim strcus2 As String = strcus
        strcus = get_cust(strcus)

        strSQL = ""
        strSQL = strSQL & "SELECT IIf(T_SN_HD_TB.CUSTCODE = 'E230','A',IIf(left(T_SN_HD_TB.CUSTCODE,1) = 'K','A','K')) AS 式1 "
        strSQL = strSQL & "FROM (T_INV_HD_TB LEFT JOIN T_INV_BD_TB ON T_INV_HD_TB.INVOICENO = T_INV_BD_TB.INVOICENO) LEFT JOIN T_SN_HD_TB ON T_INV_BD_TB.SNNO = T_SN_HD_TB.SALESNOTENO "
        strSQL = strSQL & "WHERE T_INV_HD_TB.CUSTCODE IN ('" & strcus & "') "
        strSQL = strSQL & "AND T_SN_HD_TB.CUSTCODE Is Not Null "



        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn02)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        '結果を取り出す 
        While (dataread.Read())


            If IsDBNull(dataread("式1")) = True Then
                flgka02 = "XXX"
            Else
                flgka02 = Trim(dataread("式1"))
            End If

            If flgka02 = "A" Then
                flgka00 = "1"
            ElseIf flgka02 = "K" Then
                flgka01 = "1"
            End If

        End While


        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()

        If flgka00 = "0" And flgka01 = "0" Then
            flgka00 = "1"
            flgka01 = "1"
        End If



        strSQL = ""
        strSQL = strSQL & "INSERT INTO T_EXL_CSKANRYO VALUES("
        strSQL = strSQL & " '" & strsta & "' "
        strSQL = strSQL & ",'" & "" & "' "
        strSQL = strSQL & ",'" & strfwd & "' "

        strSQL = strSQL & ",'" & strcus2 & "' "
        strSQL = strSQL & ",'" & strdes & "' "
        strSQL = strSQL & ",'" & strinv & "' "
        strSQL = strSQL & ",'" & strcut & "' "
        strSQL = strSQL & ",'" & stretd & "' "
        strSQL = strSQL & ",'" & "" & "' "
        strSQL = strSQL & ",'" & "" & "' "
        strSQL = strSQL & ",'" & "" & "' "
        strSQL = strSQL & ",'" & "" & "' "
        strSQL = strSQL & ",'" & strcon & "' "
        strSQL = strSQL & ",'" & "" & "' "
        strSQL = strSQL & ",'" & "" & "' "
        strSQL = strSQL & ",'" & "" & "' "
        strSQL = strSQL & ",'" & "" & "' "
        strSQL = strSQL & ",'" & "" & "' "
        strSQL = strSQL & ",'" & "" & "' "
        strSQL = strSQL & ",'" & "" & "' "
        strSQL = strSQL & ",'" & "" & "' "
        strSQL = strSQL & ",'" & val01 & "' "
        strSQL = strSQL & ",'" & str07 & "' "
        strSQL = strSQL & ",'" & Format(Now(), "yyyy/MM/dd") & "' "
        strSQL = strSQL & ",'" & strvan & "' "
        strSQL = strSQL & ",'" & strbkg & "' "
        strSQL = strSQL & ",'" & "" & "' "
        strSQL = strSQL & ",'" & strvessle & "' "
        strSQL = strSQL & ",'" & strvy & "' "
        strSQL = strSQL & ",'" & strrp & "' "
        strSQL = strSQL & ",'" & strlp & "' "

        strSQL = strSQL & ",'" & strdp & "' "
        strSQL = strSQL & ",'" & strpd & "' "
        strSQL = strSQL & ",'" & strtime & "' "
        strSQL = strSQL & ",'' "
        strSQL = strSQL & ",'1' "
        strSQL = strSQL & ",'" & flgka00 & "' "
        strSQL = strSQL & ",'" & flgka01 & "' "
        strSQL = strSQL & ",'' "

        strSQL = strSQL & ")"



        Command.CommandText = strSQL
        ' SQLの実行
        Command.ExecuteNonQuery()


        cnn.Close()
        cnn.Dispose()
        cnn02.Close()
        cnn02.Dispose()


    End Sub
    Private Function get_cust(custcode As String) As String

        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String
        Dim strcust As String = ""

        get_cust = ""

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
        strSQL = strSQL & "SELECT M_EXL_CUST.CUST_GRP_CD "
        strSQL = strSQL & "FROM M_EXL_CUST "
        strSQL = strSQL & "WHERE M_EXL_CUST.CUST_ANAME = '" & custcode & "' "


        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        strcust = ""
        '結果を取り出す 
        While (dataread.Read())
            strcust = Convert.ToString(dataread("CUST_GRP_CD"))        'ETD(計上日)
            If strcust = "" Then
            Else
                custcode = strcust
            End If
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()


        strSQL = ""
        strSQL = strSQL & "SELECT M_EXL_CUST.CUST_ANAME "
        strSQL = strSQL & "FROM M_EXL_CUST "
        strSQL = strSQL & "WHERE M_EXL_CUST.CUST_GRP_CD = '" & custcode & "' "


        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        strcust = ""
        '結果を取り出す 
        While (dataread.Read())
            If strcust = "" Then
                strcust = Convert.ToString(dataread("CUST_ANAME"))
            Else
                strcust = strcust & "','" & Convert.ToString(dataread("CUST_ANAME"))
            End If

        End While

        If strcust = "" Then
        Else
            get_cust = strcust
        End If

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()

        cnn.Close()
        cnn.Dispose()

    End Function

    Protected Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        '更新ボタンクリックイベント

        '更新
        Call DB_access("03")        '更新モード


        '元の画面に戻る
        Response.Redirect("anken_kanryo_all.aspx")
    End Sub

End Class

