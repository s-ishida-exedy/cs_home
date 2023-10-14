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



    End Sub

    Private Sub DB_access(strExecMode As String)
        '画面入力情報をテーブルへ登録

        Dim strSQL As String = ""
        Dim strVal As String = ""
        Dim strkbn As String = ""
        Dim strref As String = ""
        Dim strinv As String = ""
        Dim strbkg As String = ""


        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=svdpo051;Initial Catalog=BPTB001;User Id=ado_bptb001;Password=ado_bptb001"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand

        'データベース接続を開く
        cnn.Open()

        '接続文字列の作成
        Dim ConnectionString02 As String = String.Empty
        'SQL Server認証
        ConnectionString02 = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn02 = New SqlConnection(ConnectionString02)
        Dim Command02 = cnn02.CreateCommand

        'データベース接続を開く
        cnn02.Open()

        strinv = Trim(TextBox2.Text)
        strbkg = Trim(TextBox3.Text)

        Dim CUSTCODE As String = ""
        Dim CUSTNAME As String = ""
        Dim ETD As String = ""
        Dim SHIP_TYPE As String = ""
        Dim ETA As String = ""
        Dim VOY_NO As String = ""
        Dim VESSEL As String = ""
        Dim LOADING_PORT As String = ""
        Dim RECEIVED_PORT As String = ""
        Dim SHIP_PLACE As String = ""


        strSQL = ""
        strSQL = strSQL & "SELECT  "
        strSQL = strSQL & "  CUSTCODE "
        strSQL = strSQL & "  , CUSTNAME "
        strSQL = strSQL & "  , IOPORTDATE "
        strSQL = strSQL & "  , SHIPCD "
        strSQL = strSQL & "  , REACHDATE "
        strSQL = strSQL & "  , VOYAGENO "
        strSQL = strSQL & "  , SHIPPEDPER "
        strSQL = strSQL & "  , INVFROM "
        strSQL = strSQL & "  , INVON "
        strSQL = strSQL & "  , SHIPBASE "
        strSQL = strSQL & "FROM T_INV_HD_TB "
        strSQL = strSQL & "WHERE T_INV_HD_TB.OLD_INVNO = '" & strinv & "' "
        strSQL = strSQL & "AND T_INV_HD_TB.BOOKINGNO = '" & strbkg & "' "

        'INVOICENOが最大のものを取得
        strSQL = strSQL & "AND T_INV_HD_TB.INVOICENO = "
        strSQL = strSQL & "(SELECT MAX(T_INV_HD_TB.INVOICENO) AS IVNO "
        strSQL = strSQL & "FROM T_INV_HD_TB "

        strSQL = strSQL & "WHERE T_INV_HD_TB.HEADTITLE Like 'INVOICE%' "
        strSQL = strSQL & "AND T_INV_HD_TB.OLD_INVNO = '" & strinv & "' "
        strSQL = strSQL & "AND T_INV_HD_TB.BOOKINGNO　= '" & strbkg & "' "
        strSQL = strSQL & "AND T_INV_HD_TB.ORG_INVOICENO IS NULL "
        strSQL = strSQL & ") "




        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        '結果を取り出す 
        While (dataread.Read())




            CUSTCODE = Trim(dataread("CUSTCODE"))
            CUSTNAME = Trim(dataread("CUSTNAME"))
            ETD = dataread("IOPORTDATE")
            SHIP_TYPE = dataread("SHIPCD")
            ETA = dataread("REACHDATE")
            VOY_NO = dataread("VOYAGENO")
            VESSEL = dataread("SHIPPEDPER")
            LOADING_PORT = dataread("INVFROM")
            RECEIVED_PORT = dataread("INVON")
            SHIP_PLACE = dataread("SHIPBASE")


            If SHIP_TYPE = "01" Then
                SHIP_TYPE = "船舶"
            ElseIf SHIP_TYPE = "02" Then
                SHIP_TYPE = "船舶"
            ElseIf SHIP_TYPE = "03" Then
                SHIP_TYPE = "CFS"
            ElseIf SHIP_TYPE = "05" Then
                SHIP_TYPE = "AIR"
            ElseIf SHIP_TYPE = "06" Then
                SHIP_TYPE = "COURIER"
            Else
                SHIP_TYPE = "その他"
            End If

        End While


        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()

        cnn.Close()
        cnn.Dispose()

        If strExecMode = "01" Then
            '更新

            'strSQL = ""
            'strSQL = strSQL & "UPDATE T_EXL_SHIPPINGMEMOLIST SET"
            'strSQL = strSQL & " REV_ETD = '" & REV_ETD & "' "
            'strSQL = strSQL & ",REV_ETA = '" & REV_ETA & "' "
            'strSQL = strSQL & ",DATE_GETBL = '" & DATE_GETBL & "' "
            'strSQL = strSQL & ",DATE_ONBL = '" & DATE_ONBL & "' "
            'strSQL = strSQL & ",REV_SALESDATE = '" & REV_SALESDATE & "' "
            'strSQL = strSQL & ",REV_STATUS = '" & REV_STATUS & "' "
            'strSQL = strSQL & "WHERE INVOICE_NO = '" & strinv & "' "
            'strSQL = strSQL & "AND BOOKING_NO = '" & strbkg & "' "

        ElseIf strExecMode = "02" Then

            'strSQL = ""
            'strSQL = strSQL & "DELETE FROM T_EXL_SHIPPINGMEMOLIST "
            'strSQL = strSQL & "WHERE INVOICE_NO = '" & strinv & "' "
            'strSQL = strSQL & "AND BOOKING_NO = '" & strbkg & "' "

        ElseIf strExecMode = "03" Then


            '既存データが無いのでINSERTする
            strSQL = ""
            strSQL = strSQL & "INSERT INTO T_EXL_SHIPPINGMEMOLIST VALUES("

            strSQL = strSQL & " '" & CUSTCODE & "' "
            strSQL = strSQL & ",'" & CUSTNAME & "' "

            strSQL = strSQL & ",'" & strinv & "' "

            strSQL = strSQL & ",'" & ETD & "' "

            strSQL = strSQL & ",'○' "

            strSQL = strSQL & ",'○' "
            strSQL = strSQL & ",'" & "" & "' "
            strSQL = strSQL & ",'" & SHIP_TYPE & "' "

            strSQL = strSQL & ",'" & "" & "' "
            strSQL = strSQL & ",'" & ETA & "' "
            strSQL = strSQL & ",'" & "" & "' "

            strSQL = strSQL & ",'" & "" & "' "
            strSQL = strSQL & ",'" & strbkg & "' "
            strSQL = strSQL & ",'" & VOY_NO & "' "

            strSQL = strSQL & ",'" & "" & "' "
            strSQL = strSQL & ",'" & "" & "' "
            strSQL = strSQL & ",'" & "" & "' "

            strSQL = strSQL & ",'" & "" & "' "
            strSQL = strSQL & ",'" & VESSEL & "' "
            strSQL = strSQL & ",'" & LOADING_PORT & "' "

            strSQL = strSQL & ",'" & RECEIVED_PORT & "' "
            strSQL = strSQL & ",'" & SHIP_PLACE & "' "
            strSQL = strSQL & ",'" & "' "

            strSQL = strSQL & ",'" & "' "
            strSQL = strSQL & ",'" & "' "
            strSQL = strSQL & ",'" & "' "

            strSQL = strSQL & ",'" & "' "
            strSQL = strSQL & ",'" & "' "
            strSQL = strSQL & ",'" & "' "

            strSQL = strSQL & ")"



        End If

        Command02.CommandText = strSQL
        ' SQLの実行
        Command02.ExecuteNonQuery()


        cnn02.Close()
        cnn02.Dispose()


    End Sub


    Protected Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        '更新ボタンクリックイベント

        '更新
        Call DB_access("03")        '更新モード


        '元の画面に戻る
        Response.Redirect("shippingmemo.aspx")
    End Sub

End Class

