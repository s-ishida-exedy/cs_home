﻿Imports System.Data.SqlClient
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
        Dim strID As String = ""

        Label3.Text = ""

        Button2.Attributes.Add("onclick", "return confirm('削除しますがよろしいですか？');")

        If IsPostBack Then
            ' そうでない時処理
        Else
            'パラメータ取得
            strMode = Session("strMode")
            strID = Session("strID")

            '接続文字列の作成
            Dim ConnectionString As String = String.Empty
            'SQL Server認証
            ConnectionString = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
            'SqlConnectionクラスの新しいインスタンスを初期化
            Dim cnn = New SqlConnection(ConnectionString)

            'データベース接続を開く
            cnn.Open()

            If strMode = "0" Then
                '更新モード　DBから値取得し、セット
                strinv = Session("strinv")
                strbkg = Session("strbkg")

                strSQL = ""
                strSQL = strSQL & "SELECT  "
                strSQL = strSQL & "  CUSTCODE "
                strSQL = strSQL & "  , REV_ETD "
                strSQL = strSQL & "  , REV_ETA "
                strSQL = strSQL & "  , DATE_GETBL "
                strSQL = strSQL & "  , DATE_ONBL "
                strSQL = strSQL & "  , REV_SALESDATE "
                strSQL = strSQL & "  , REV_STATUS "
                strSQL = strSQL & "FROM T_EXL_SHIPPINGMEMOLIST "
                strSQL = strSQL & "WHERE FLG01 = '" & strID & "' "
                'strSQL = strSQL & "WHERE INVOICE_NO = '" & strinv & "' "
                'strSQL = strSQL & "AND BOOKING_NO = '" & strbkg & "' "


                'ＳＱＬコマンド作成 
                dbcmd = New SqlCommand(strSQL, cnn)
                'ＳＱＬ文実行 
                dataread = dbcmd.ExecuteReader()

                '結果を取り出す 
                While (dataread.Read())
                    Label1.Text = dataread("CUSTCODE")
                    Label2.Text = strinv
                    Label4.Text = strbkg
                    Label5.Text = strID
                    TextBox2.Text = dataread("REV_ETD")
                    TextBox3.Text = dataread("REV_ETA")
                    TextBox4.Text = dataread("DATE_GETBL")
                    TextBox5.Text = dataread("DATE_ONBL")
                    TextBox6.Text = dataread("REV_SALESDATE")


                    DropDownList2.SelectedValue = dataread("REV_STATUS")

                End While

                'クローズ処理 
                dataread.Close()
                dbcmd.Dispose()
                cnn.Close()
                cnn.Dispose()


            Else

            End If
        End If



    End Sub

    Private Sub DB_access(strExecMode As String)
        '画面入力情報をテーブルへ登録

        Dim strSQL As String = ""
        Dim strVal As String = ""
        Dim strkbn As String = ""
        Dim strref As String = ""



        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand

        'データベース接続を開く
        cnn.Open()

        Dim strinv As String = ""
        Dim strbkg As String = ""
        Dim REV_ETD As String = ""
        Dim REV_ETA As String = ""
        Dim DATE_GETBL As String = ""
        Dim DATE_ONBL As String = ""
        Dim REV_SALESDATE As String = ""
        Dim REV_STATUS As String = ""
        Dim strID As String = ""

        strinv = Label2.Text
        strbkg = Label4.Text
        strID = Label5.Text
        REV_ETD = TextBox2.Text
        REV_ETA = TextBox3.Text
        DATE_GETBL = TextBox4.Text
        DATE_ONBL = TextBox5.Text
        REV_SALESDATE = TextBox6.Text


        REV_STATUS = DropDownList2.SelectedValue

        '画面入力情報を変数に代入
        Dim strMail As String = TextBox2.Text
        Dim strCc As String = DropDownList2.Text

        If strExecMode = "01" Then
            '更新

            strSQL = ""
            strSQL = strSQL & "UPDATE T_EXL_SHIPPINGMEMOLIST SET"
            strSQL = strSQL & " REV_ETD = '" & REV_ETD & "' "
            strSQL = strSQL & ",REV_ETA = '" & REV_ETA & "' "
            strSQL = strSQL & ",DATE_GETBL = '" & DATE_GETBL & "' "
            strSQL = strSQL & ",DATE_ONBL = '" & DATE_ONBL & "' "
            strSQL = strSQL & ",REV_SALESDATE = '" & REV_SALESDATE & "' "
            strSQL = strSQL & ",REV_STATUS = '" & REV_STATUS & "' "
            strSQL = strSQL & "WHERE FLG01 = '" & strID & "' "
            'strSQL = strSQL & "WHERE INVOICE_NO = '" & strinv & "' "
            'strSQL = strSQL & "AND BOOKING_NO = '" & strbkg & "' "

        ElseIf strExecMode = "02" Then

            strSQL = ""
            strSQL = strSQL & "DELETE FROM T_EXL_SHIPPINGMEMOLIST "
            strSQL = strSQL & "WHERE FLG01 = '" & strID & "' "
            'strSQL = strSQL & "WHERE INVOICE_NO = '" & strinv & "' "
            'strSQL = strSQL & "AND BOOKING_NO = '" & strbkg & "' "


        ElseIf strExecMode = "03" Then

        End If

        Command.CommandText = strSQL
        ' SQLの実行
        Command.ExecuteNonQuery()


        cnn.Close()
        cnn.Dispose()

    End Sub

    Private Function chk_Nyuryoku() As Boolean
        chk_Nyuryoku = True

        Dim address As String = TextBox2.Text

        If address <> "" Then
            If System.Text.RegularExpressions.Regex.IsMatch(
                address,
                "\A[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}\z",
                System.Text.RegularExpressions.RegexOptions.IgnoreCase) Then
            Else
                Label3.Text = "メールアドレスの形式が間違っています。"
                chk_Nyuryoku = False
            End If
        Else
            Label3.Text = "メールアドレスは必須入力です。。"
            chk_Nyuryoku = False
        End If
        If DropDownList2.Text = "" Then
            Label3.Text = "宛先：1 CC：0を入力してください。"
            chk_Nyuryoku = False
        End If

    End Function

    Protected Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        '更新ボタンクリックイベント

        '更新
        Call DB_access("01")        '更新モード


        Session.Remove("strID")
        Session.Remove("strMode")
        Session.Remove("strinv")
        Session.Remove("strbkg")

        '元の画面に戻る
        Response.Redirect("shippingmemo.aspx")
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        '更新ボタンクリックイベント

        '更新
        Call DB_access("02")        '更新モード


        Session.Remove("strID")
        Session.Remove("strMode")
        Session.Remove("strinv")
        Session.Remove("strbkg")

        '元の画面に戻る
        Response.Redirect("shippingmemo.aspx")


    End Sub
End Class

