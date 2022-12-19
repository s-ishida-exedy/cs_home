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

        str00 = Trim(DropDownList1.SelectedValue)
        str01 = Trim(TextBox1.Text)
        str02 = Trim(TextBox2.Text)
        str03 = Trim(TextBox3.Text)
        str04 = Trim(TextBox4.Text)
        str05 = Trim(TextBox5.Text)
        str06 = Trim(TextBox6.Text)


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

        Dim strvessle As String = ""
        Dim strvy As String = ""
        Dim strrp As String = ""
        Dim strlp As String = ""
        Dim strdp As String = ""
        Dim strpd As String = ""
        Dim val01 As Long
        val01 = 0



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
        strSQL = strSQL & " count(*) AS C "
        strSQL = strSQL & "FROM T_EXL_CSKANRYO "
        strSQL = strSQL & "WHERE T_EXL_CSKANRYO.INVOICE = '" & str00 & "' "
        strSQL = strSQL & "AND T_EXL_CSKANRYO.BOOKING_NO = '" & str05 & "' "



        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        '結果を取り出す 
        While (dataread.Read())
            val01 = Trim(dataread("C"))
        End While


        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()


        strSQL = ""
        strSQL = strSQL & "SELECT Max(T_SHIPSCH_VIEW_02.VANTIME_ST) AS A, T_SHIPSCH_VIEW_02.BOOKINGNO "
        strSQL = strSQL & "FROM T_SHIPSCH_VIEW_02 "
        strSQL = strSQL & "WHERE T_SHIPSCH_VIEW_02.BOOKINGNO = '" & strbkg & "' "
        strSQL = strSQL & "GROUP BY T_SHIPSCH_VIEW_02.BOOKINGNO "


        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn02)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        '結果を取り出す 
        While (dataread.Read())
            strtime = Right(Trim(dataread("A")), 8)
        End While


        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()


        If val01 > 0 Then
            '更新


            strSQL = ""
            strSQL = strSQL & "UPDATE T_EXL_CSKANRYO SET"
            strSQL = strSQL & " STATUS = '" & strsta & "' "
            strSQL = strSQL & ",FORWARDER02 = '" & strfwd & "' "
            strSQL = strSQL & ",CUST = '" & strcus & "' "
            strSQL = strSQL & ",DESTINATION = '" & strdes & "' "
            strSQL = strSQL & ",INVOICE = '" & strinv & "' "
            strSQL = strSQL & ",CUT_DATE = '" & strcut & "' "
            strSQL = strSQL & ",ETD = '" & stretd & "' "
            strSQL = strSQL & ",CONTAINER = '" & strcon & "' "
            strSQL = strSQL & ",FINALVANDATE = '" & strvan & "' "
            strSQL = strSQL & ",BOOKING_NO = '" & strbkg & "' "
            strSQL = strSQL & ",VESSEL_NAME = '" & strvessle & "' "
            strSQL = strSQL & ",VOYAGE_NO = '" & strvy & "' "
            strSQL = strSQL & ",PLACE_OF_RECEIPT = '" & strrp & "' "
            strSQL = strSQL & ",LOADING_PORT = '" & strlp & "' "
            strSQL = strSQL & ",DISCHARGING_PORT = '" & strdp & "' "
            strSQL = strSQL & ",PLACE_OF_DELIVERY = '" & strpd & "' "
            strSQL = strSQL & ",FLG01 = '" & strtime & "' "
            strSQL = strSQL & ",FLG03 = '1' "
            strSQL = strSQL & "WHERE T_EXL_CSKANRYO.INVOICE = '" & str00 & "' "
            strSQL = strSQL & "AND T_EXL_CSKANRYO.BOOKING_NO = '" & str05 & "' "

        Else


            strSQL = ""
            strSQL = strSQL & "INSERT INTO T_EXL_CSKANRYO VALUES("
            strSQL = strSQL & " '" & strsta & "' "
            strSQL = strSQL & ",'" & "" & "' "
            strSQL = strSQL & ",'" & strfwd & "' "
            strSQL = strSQL & ",'" & strcus & "' "
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
            strSQL = strSQL & ",'" & "" & "' "
            strSQL = strSQL & ",'" & "" & "' "
            strSQL = strSQL & ",'" & "" & "' "
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
            strSQL = strSQL & ",'' "
            strSQL = strSQL & ",'' "
            strSQL = strSQL & ",'' "

            strSQL = strSQL & ")"



        End If

        Command.CommandText = strSQL
        ' SQLの実行
        Command.ExecuteNonQuery()


        cnn.Close()
        cnn.Dispose()
        cnn02.Close()
        cnn02.Dispose()


    End Sub


    Protected Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        '更新ボタンクリックイベント

        '更新
        Call DB_access("03")        '更新モード


        '元の画面に戻る
        Response.Redirect("anken_kanryo.aspx")
    End Sub

End Class

