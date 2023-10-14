Imports System.Data.SqlClient
'Protected System.Web.UI.HtmlControls.HtmlTableCell tdchg01

Partial Class cs_home
    Inherits System.Web.UI.Page

    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load
        Dim ivno As String = ""
        Dim strComf As String = ""
        Dim strComfD As String = ""

        If IsPostBack Then
            ' そうでない時処理
            If Not Request("Button1") Is Nothing Then
                ' ボタン押下により発生した PostBack
            End If
        Else
            'パラメータ取得
            If Request.QueryString("id") IsNot Nothing Then
                ivno = Request.QueryString("id").ToString
                Label1.Text = "I/V:" & ivno
            Else
                Exit Sub
            End If

            'ブッキングデータ取得
            Call GET_DATA_Booking(ivno)

            'インボイスヘッダー取得
            Call GET_DATA_Header(ivno)

            '相違部の背景色変更処理
            Call COLORING_LABEL(ivno)

            '確認済み状況を取得する
            Call GET_COMFIRM_STATUS(ivno, strComf, strComfD)

            '確認者セット
            If strComf <> "" Then
                TextBox1.Text = strComf
                Label47.Text = strComfD
                Button1.Enabled = False
            Else
                '初期化
                TextBox1.Text = ""
                Label47.Text = ""
                Button1.Enabled = True
            End If

            Label46.Text = ""
        End If
    End Sub

    Private Sub GET_DATA_Booking(ivno As String)
        'ブッキングシートのデータを取り出す
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()

        strSQL = "SELECT * FROM T_BOOKING WHERE INVOICE_NO like '%" & ivno & "%'"
        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        strDate = ""
        '結果を取り出す 
        While (dataread.Read())
            Label7.Text = dataread("ETD")                   'ETD(計上日)
            Label9.Text = dataread("LOADING_PORT")          '積出港
            Label6.Text = dataread("DISCHARGING_PORT")      '揚地
            Label13.Text = dataread("PLACE_OF_DELIVERY")     '配送先
            Label16.Text = dataread("PLACE_OF_RECEIPT")      '荷受地
            Label19.Text = dataread("PLACE_OF_DELIVERY")     '配送先責任送り先
            Label22.Text = dataread("CUT_DATE")              'カット日
            Label25.Text = dataread("ETA")                   '到着日
            Label28.Text = dataread("ETD")                   '入出港日

            Label31.Text = "40ft : " & dataread("FOURTY_FEET") & ", 20ft : " & dataread("TWENTY_FEET")

            'If dataread("FOURTY_FEET") <> "" And dataread("FOURTY_FEET") <> "使用禁止" Then
            '    If Right(dataread("FOURTY_FEET"), 1) > 0 Then    '出荷方法
            '        Label31.Text = "02:40ft"
            '    End If
            'ElseIf dataread("TWENTY_FEET") <> "" And dataread("TWENTY_FEET") <> "使用禁止" Then
            '    If Right(dataread("TWENTY_FEET"), 1) > 0 Then
            '        Label31.Text = "01:20ft"
            '    End If
            'Else
            '    Label31.Text = "03:LCL"
            'End If

            Label34.Text = dataread("VOYAGE_NO")              'VoyageNo
            Label37.Text = dataread("BOOK_TO")                '船社
            Label40.Text = Replace(Convert.ToString(dataread("BOOKING_NO")), "", "-")             'ブッキングNo
            Label43.Text = dataread("VESSEL_NAME")            '船名
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()

    End Sub

    Private Sub GET_DATA_Header(ivno As String)
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=svdpo051;Initial Catalog=BPTB001;User Id=ado_bptb001;Password=ado_bptb001"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()

        strSQL = ""
        strSQL = strSQL & "SELECT CONVERT(NVARCHAR, BLDATE2, 111) AS BLDATE2, INVON, VIA, INVTO, INVFROM, PLACECARRIER, "
        strSQL = strSQL & "CONVERT(NVARCHAR, CUTDATE, 111) AS CUTDATE, CONVERT(NVARCHAR, REACHDATE, 111) AS REACHDATE, "
        strSQL = strSQL & "CONVERT(NVARCHAR, IOPORTDATE, 111) AS IOPORTDATE, SHIPCD, VOYAGENO, SHIPPER, BOOKINGNO, SHIPPEDPER "
        strSQL = strSQL & "FROM T_INV_HD_TB WHERE OLD_INVNO = '" & ivno & "' "
        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        strDate = ""
        '結果を取り出す 
        While (dataread.Read())
            Label8.Text = Convert.ToString(dataread("BLDATE2"))        'ETD(計上日)
            Label10.Text = Convert.ToString(dataread("INVFROM"))         '積出港
            Label11.Text = Convert.ToString(dataread("VIA"))           '揚地
            Label14.Text = Convert.ToString(dataread("INVTO"))         '配送先
            Label17.Text = Convert.ToString(dataread("INVON"))       '荷受地
            Label20.Text = Convert.ToString(dataread("PLACECARRIER"))  '配送先責任送り先
            Label23.Text = Convert.ToString(dataread("CUTDATE"))       'カット日
            Label26.Text = Convert.ToString(dataread("REACHDATE"))     '到着日
            Label29.Text = Convert.ToString(dataread("IOPORTDATE"))    '入出港日

            If Convert.ToString(dataread("SHIPCD")) = "01" Then        '出荷方法
                Label32.Text = "01:20ft"
            ElseIf Convert.ToString(dataread("SHIPCD")) = "02" Then
                Label32.Text = "02:40ft"
            ElseIf Convert.ToString(dataread("SHIPCD")) = "03" Then
                Label32.Text = "03:LCL"
            End If
            'Label32.Text = Convert.ToString(dataread("SHIPCD"))        

            Label35.Text = Convert.ToString(dataread("VOYAGENO"))      'VoyageNo
            Label38.Text = Convert.ToString(dataread("SHIPPER"))       '船社
            Label41.Text = Convert.ToString(dataread("BOOKINGNO"))     'ブッキングNo
            Label44.Text = Convert.ToString(dataread("SHIPPEDPER"))    '船名
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()

    End Sub

    Private Sub COLORING_LABEL(ivno As String)
        '比較結果で×の場合、背景色を黄色にする
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()

        strSQL = ""
        strSQL = strSQL & "SELECT * FROM T_COMPARE_INV_HD "
        strSQL = strSQL & "WHERE INVOICE_NO = '" & ivno & "' "
        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        '結果を取り出す 
        While (dataread.Read())
            If dataread("ETD") = "1" Then   '計上日
                Label7.BackColor = System.Drawing.ColorTranslator.FromHtml("#fffa00")
                Label8.BackColor = System.Drawing.ColorTranslator.FromHtml("#fffa00")
            End If
            If dataread("LOADING_PORT") = "1" Then  '積出港
                Label9.BackColor = System.Drawing.ColorTranslator.FromHtml("#fffa00")
                Label10.BackColor = System.Drawing.ColorTranslator.FromHtml("#fffa00")
            End If
            If dataread("DISCHARGING_PORT") = "1" Then  '揚地
                Label6.BackColor = System.Drawing.ColorTranslator.FromHtml("#fffa00")
                Label11.BackColor = System.Drawing.ColorTranslator.FromHtml("#fffa00")
            End If
            If dataread("PLACE_OF_DELIVERY") = "1" Then '配送先
                Label13.BackColor = System.Drawing.ColorTranslator.FromHtml("#fffa00")
                Label14.BackColor = System.Drawing.ColorTranslator.FromHtml("#fffa00")
            End If
            If dataread("PLACE_OF_RECEIPT") = "1" Then  '荷受地
                Label16.BackColor = System.Drawing.ColorTranslator.FromHtml("#fffa00")
                Label17.BackColor = System.Drawing.ColorTranslator.FromHtml("#fffa00")
            End If
            If dataread("PLACE_CARRIER") = "1" Then     '配送先責任送り先
                Label19.BackColor = System.Drawing.ColorTranslator.FromHtml("#fffa00")
                Label20.BackColor = System.Drawing.ColorTranslator.FromHtml("#fffa00")
            End If
            If dataread("CUT_DATE") = "1" Then          'カット日
                Label22.BackColor = System.Drawing.ColorTranslator.FromHtml("#fffa00")
                Label23.BackColor = System.Drawing.ColorTranslator.FromHtml("#fffa00")
            End If
            If dataread("ETA") = "1" Then               '到着日
                Label25.BackColor = System.Drawing.ColorTranslator.FromHtml("#fffa00")
                Label26.BackColor = System.Drawing.ColorTranslator.FromHtml("#fffa00")
            End If
            If dataread("IOPORTDATE") = "1" Then        '入出港日
                Label28.BackColor = System.Drawing.ColorTranslator.FromHtml("#fffa00")
                Label29.BackColor = System.Drawing.ColorTranslator.FromHtml("#fffa00")
            End If
            If dataread("SHIP_METHOD") = "1" Then       '出荷方法
                Label31.BackColor = System.Drawing.ColorTranslator.FromHtml("#fffa00")
                Label32.BackColor = System.Drawing.ColorTranslator.FromHtml("#fffa00")
            End If
            If dataread("VOYAGE_NO") = "1" Then         'VOYAGENo
                Label34.BackColor = System.Drawing.ColorTranslator.FromHtml("#fffa00")
                Label35.BackColor = System.Drawing.ColorTranslator.FromHtml("#fffa00")
            End If
            If dataread("BOOK_TO") = "1" Then           '船社
                Label37.BackColor = System.Drawing.ColorTranslator.FromHtml("#fffa00")
                Label38.BackColor = System.Drawing.ColorTranslator.FromHtml("#fffa00")
            End If
            If dataread("BOOKING_NO") = "1" Then        'ﾌﾞｯｷﾝｸﾞNo
                Label40.BackColor = System.Drawing.ColorTranslator.FromHtml("#fffa00")
                Label41.BackColor = System.Drawing.ColorTranslator.FromHtml("#fffa00")
            End If
            If dataread("VESSEL_NAME") = "1" Then       '船名
                Label43.BackColor = System.Drawing.ColorTranslator.FromHtml("#fffa00")
                Label44.BackColor = System.Drawing.ColorTranslator.FromHtml("#fffa00")
            End If
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        '確認完了ボタン押下時

        '入力チェック
        If TextBox1.Text = "" Then
            Label46.Text = "確認者が入力されていません。"
            Exit Sub
        End If

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand
        Dim strSQL As String = ""
        Dim ivno As String = ""

        'パラメータ取得
        If Request.QueryString("id") IsNot Nothing Then
            ivno = Request.QueryString("id").ToString
        Else
            Exit Sub
        End If

        'データベース接続を開く
        cnn.Open()

        'FIN_FLGを更新
        strSQL = ""
        strSQL = strSQL & "UPDATE T_COMPARE_INV_HD SET FIN_FLG ='1' "
        strSQL = strSQL & ",COMFIRM = '" & TextBox1.Text & "' "
        strSQL = strSQL & ",COMFIRM_DATE = '" & DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") & "' "
        strSQL = strSQL & "WHERE INVOICE_NO = '" & ivno & "'"

        Command.CommandText = strSQL
        ' SQLの実行
        Command.ExecuteNonQuery()

        Response.Redirect("iv_booking.aspx")

    End Sub

    Private Sub GET_COMFIRM_STATUS(ivno As String, ByRef strComf As String, ByRef strComfD As String)
        '確認状況の取得

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String

        'データベース接続を開く
        cnn.Open()

        strSQL = ""
        strSQL = strSQL & "SELECT COMFIRM, COMFIRM_DATE FROM T_COMPARE_INV_HD WHERE INVOICE_NO = '" & ivno & "' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()
        '結果を取り出す 
        While (dataread.Read())
            strComf = dataread("COMFIRM")
            strComfD = "確認日時： " & dataread("COMFIRM_DATE")
        End While
        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()

    End Sub

End Class
