Imports System.Data.SqlClient
Imports mod_function
Imports System.Data
Imports System.Data.Common
Imports System.Console
Imports ClosedXML.Excel
Imports System.IO
Imports System.Linq

Partial Class cs_home
    Inherits System.Web.UI.Page
    Public strMainPath As String = ""       'サーバーのパス用

    Private Sub GridView1_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound

        '最終更新年月日取得
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strinv As String = ""
        Dim strbkg As String = ""

        Dim wday As String = ""
        Dim wday2 As String = ""
        Dim wday3 As String = ""
        Dim dt1 As DateTime = DateTime.Now
        Dim Kaika00 As String = ""

        Dim ts1 As New TimeSpan(80, 0, 0, 0)
        Dim ts2 As New TimeSpan(80, 0, 0, 0)
        Dim dt2 As DateTime = dt1 + ts1
        Dim dt3 As DateTime = dt1 - ts1

        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.Cells(5).Text = 0 Then
                e.Row.Cells(5).Text = "無効"
                e.Row.ForeColor = Drawing.Color.DarkGray

                e.Row.BackColor = Drawing.Color.LightGray
            ElseIf e.Row.Cells(5).Text = 1 Then
                e.Row.Cells(5).Text = "有効"
            End If
        End If

        Dim a As String
        'ボタンに行数をセット
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim ddl01 As DropDownList = e.Row.FindControl("DropDownList1")




            'ボタンが存在する場合のみセット
            If Not (ddl01 Is Nothing) Then
                'AddHandler ddl01.SelectedIndexChanged, AddressOf ddl01_change
            End If



            If e.Row.Cells(2).Text = "本社AIR" Or e.Row.Cells(2).Text = "上野AIR" Then

                Dim li As ListItem = New ListItem("宛先", 1)
                ddl01.Items.Add(li)
                Dim li2 As ListItem = New ListItem("CC", 2)
                ddl01.Items.Add(li2)
                Dim li3 As ListItem = New ListItem("BCC", 3)
                ddl01.Items.Add(li3)
                Dim li4 As ListItem = New ListItem("なし", 4)
                ddl01.Items.Add(li4)

            Else


                Dim li As ListItem = New ListItem("宛先", 1)
                ddl01.Items.Add(li)
                Dim li2 As ListItem = New ListItem("CC", 2)
                ddl01.Items.Add(li2)
                Dim li4 As ListItem = New ListItem("なし", 4)
                ddl01.Items.Add(li4)

            End If

            If e.Row.Cells(4).Text = 1 Then
                e.Row.Cells(4).Text = "宛先"
                ddl01.SelectedValue = 1
            ElseIf e.Row.Cells(4).Text = 2 Then
                e.Row.Cells(4).Text = "CC"
                ddl01.SelectedValue = 2
            ElseIf e.Row.Cells(4).Text = 3 Then
                e.Row.Cells(4).Text = "BCC"
                ddl01.SelectedValue = 3
            ElseIf e.Row.Cells(4).Text = 4 Then
                e.Row.Cells(4).Text = "なし"
                ddl01.SelectedValue = 4
                'e.Row.BackColor = Drawing.Color.LightGray
                e.Row.ForeColor = Drawing.Color.DarkGray
            End If

            If e.Row.Cells(5).Text = "無効" Then
                'ddl01.Visible = False
                ddl01.Enabled = Not ddl01.Enabled

            End If




        End If

        e.Row.Cells(4).Visible = False

    End Sub

    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load
        Dim strCode As String = ""
        Dim strSQL As String = ""
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strMode As String = ""
        Dim strname As String = ""
        Dim strcamp As String = ""
        Dim txt As String = 0
        Dim txt1 As String = 0

        Label3.Text = ""

        If IsPostBack Then

            ' そうでない時処理
        Else
            'パラメータ取得
            strMode = Session("strMode")

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
                strCode = Session("strCode")
                strname = Session("strname")
                strcamp = Session("strcamp")

                TextBox0.Text = strCode
                TextBox1.Text = strcamp
                TextBox2.Text = strname
                TextBox0.ReadOnly = True
                'TextBox1.ReadOnly = True
                'TextBox2.ReadOnly = True

                strSQL = ""
                strSQL = strSQL & "SELECT  "
                strSQL = strSQL & "  TASK_CD "
                strSQL = strSQL & "  , FLG "
                strSQL = strSQL & "FROM M_EXL_MAIL01 "
                strSQL = strSQL & "WHERE MAIL_ADD = '" & strCode & "' "

                'ＳＱＬコマンド作成 
                dbcmd = New SqlCommand(strSQL, cnn)
                'ＳＱＬ文実行 
                dataread = dbcmd.ExecuteReader()

                '結果を取り出す 
                While (dataread.Read())
                End While

                'クローズ処理 
                dataread.Close()
                dbcmd.Dispose()


                '登録ボタンを非表示
                Button1.Visible = False
            Else

                '更新ボタンを非表示
                Button7.Visible = False
                Button8.Visible = False
            End If

            cnn.Close()
            cnn.Dispose()

            Dim Dataobj As New DBAccess

            Dim ds As DataSet = Dataobj.GET_CS_RESULT_MAIL(strCode)
            If ds.Tables.Count > 0 Then
                GridView1.DataSourceID = ""
                GridView1.DataSource = ds
                Session("data") = ds
            End If

            'Grid再表示
            GridView1.DataBind()


        End If


        Button1.Attributes.Add("onclick", "return confirm('登録します。よろしいですか？');")
        Button7.Attributes.Add("onclick", "return confirm('更新します。よろしいですか？');")
        Button8.Attributes.Add("onclick", "return confirm('削除します。よろしいですか？');")

    End Sub

    Private Sub GET_DDLVAL(ByRef mail01 As String, ByRef strid As Long, ByRef ddlval01 As Long)
        '画面入力情報をテーブルへ登録

        Dim strSQL As String = ""
        Dim strVal As String = ""
        Dim strkbn As String = ""
        Dim strref As String = ""
        Dim strbkg As String = ""
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand

        Dim strMail As String = TextBox0.Text
        Dim strcompany As String = TextBox1.Text
        Dim strpsn As String = TextBox2.Text

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        cnn.Open()

        strSQL = "SELECT FLG, MAIL_ADD, TASK_CD FROM M_EXL_MAIL01 "
        strSQL = strSQL & "WHERE MAIL_ADD = '" & mail01 & "' "
        strSQL = strSQL & "AND TASK_CD =  " & strid & " "

        'ＳＱＬコマンド作成
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行
        dataread = dbcmd.ExecuteReader()
        '結果を取り出す
        While (dataread.Read())
            ddlval01 = dataread("FLG")
        End While
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
        Dim cnt As Long

        Dim strMail As String = TextBox0.Text
        Dim strcompany As String = TextBox1.Text
        Dim strpsn As String = TextBox2.Text

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        cnn.Open()

        Dim Command = cnn.CreateCommand

        Dim a As String = ""

        If strExecMode = "01" Then

            Dim I As Integer
            For I = 0 To GridView1.Rows.Count - 1

                a = CType(GridView1.Rows(I).FindControl("DropDownList1"), DropDownList).SelectedValue

                '更新
                strSQL = ""
                strSQL = strSQL & "UPDATE M_EXL_MAIL01 SET"
                strSQL = strSQL & " FLG = '" & CType(GridView1.Rows(I).FindControl("DropDownList1"), DropDownList).SelectedValue & "' "
                strSQL = strSQL & "WHERE MAIL_ADD = '" & TextBox0.Text & "' "
                strSQL = strSQL & "AND TASK_CD = '" & GridView1.Rows(I).Cells(1).Text & "' "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()
            Next

            '更新
            strSQL = ""
            strSQL = strSQL & "UPDATE M_EXL_MAIL01 SET"
            strSQL = strSQL & " COMPANY = '" & TextBox1.Text & "' "
            strSQL = strSQL & " ,IN_CHARGE_NAME = '" & TextBox2.Text & "' "
            strSQL = strSQL & "WHERE MAIL_ADD = '" & TextBox0.Text & "' "

            Command.CommandText = strSQL
            ' SQLの実行
            Command.ExecuteNonQuery()

        ElseIf strExecMode = "02" Then

            'データ削除
            strSQL = ""
            strSQL = strSQL & "DELETE FROM M_EXL_MAIL01 "
            strSQL = strSQL & "WHERE MAIL_ADD = '" & TextBox0.Text & "' "

            Command.CommandText = strSQL
            ' SQLの実行
            Command.ExecuteNonQuery()

        ElseIf strExecMode = "03" Then

            strSQL = "SELECT TASK_CD FROM [M_EXL_MAIL00] " ' "

            'ＳＱＬコマンド作成
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行
            dataread = dbcmd.ExecuteReader()
            '結果を取り出す
            While (dataread.Read())

                Call DB_INSERT(dataread("TASK_CD"), strMail, strcompany, strpsn)
            End While

            'クローズ処理
            dataread.Close()
            dbcmd.Dispose()

        End If

        cnn.Close()
        cnn.Dispose()

    End Sub

    Private Sub Mail_check(ByRef cnt As Long)
        '画面入力情報をテーブルへ登録

        Dim strSQL As String = ""
        Dim strVal As String = ""
        Dim strkbn As String = ""
        Dim strref As String = ""
        Dim strbkg As String = ""
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand

        Dim strMail As String = TextBox0.Text
        Dim strcompany As String = TextBox1.Text
        Dim strpsn As String = TextBox2.Text

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        cnn.Open()

        strSQL = "SELECT Count(TASK_CD) as cnt FROM [M_EXL_MAIL01] "
        strSQL = strSQL & "WHERE MAIL_ADD = '" & strMail & "' "

        'ＳＱＬコマンド作成
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行
        dataread = dbcmd.ExecuteReader()
        '結果を取り出す
        While (dataread.Read())
            cnt = dataread("cnt")
        End While

        cnn.Close()
        cnn.Dispose()

    End Sub
    Private Sub DB_INSERT(ByRef strtask As String, ByRef strMail As String, ByRef strcompany As String, ByRef strpsn As String)
        '画面入力情報をテーブルへ登録

        Dim strSQL As String = ""
        Dim strVal As String = ""
        Dim strkbn As String = ""
        Dim strref As String = ""
        Dim strbkg As String = ""


        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        cnn.Open()

        Dim Command = cnn.CreateCommand

        'データベース接続を開く

        strSQL = ""
        strSQL = strSQL & "INSERT INTO M_EXL_MAIL01 VALUES("
        strSQL = strSQL & "'" & strMail & "' "
        strSQL = strSQL & ",'" & strtask & "' "
        strSQL = strSQL & ",4 "
        strSQL = strSQL & ",'" & strcompany & "' "
        strSQL = strSQL & ",'" & strpsn & "' ) "

        Command.CommandText = strSQL
        ' SQLの実行
        Command.ExecuteNonQuery()

        cnn.Close()
        cnn.Dispose()

    End Sub
    Private Function chk_Nyuryoku() As Boolean
        chk_Nyuryoku = True

        Dim address As String = TextBox0.Text
        Dim cnt As Long


        If address <> "" Then
            If System.Text.RegularExpressions.Regex.IsMatch(
                address,
                "\A[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}\z",
                System.Text.RegularExpressions.RegexOptions.IgnoreCase) Then


                Call Mail_check(cnt)
                If cnt > 0 Then
                    Label3.Text = "登録済みです。"
                    chk_Nyuryoku = False
                End If

            Else
                Label3.Text = "メールアドレスの形式が間違っています。"
                chk_Nyuryoku = False
            End If
        Else
            Label3.Text = "メールアドレスは必須入力です。。"
            chk_Nyuryoku = False
        End If
        If TextBox1.Text = "" Then
            Label3.Text = "会社名が未入力です。"
            chk_Nyuryoku = False
        End If
        If TextBox2.Text = "" Then
            Label3.Text = "担当者名が未入力です。"
            chk_Nyuryoku = False
        End If

    End Function

    Protected Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        '更新ボタンクリックイベント

        '更新
        Call DB_access("01")        '更新モード

        Session.Remove("strMode")
        Session.Remove("strCode")
        Session.Remove("strname")
        Session.Remove("strcamp")

        '元の画面に戻る
        Response.Redirect("m_mail00.aspx")
    End Sub
    Protected Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        '削除ボタン押下

        '削除
        Call DB_access("02")        '削除モード

        Session.Remove("strMode")
        Session.Remove("strCode")
        Session.Remove("strname")
        Session.Remove("strcamp")

        '元の画面に戻る
        Response.Redirect("m_mail00.aspx")
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        '登録ボタン押下
        Dim strSQL As String = ""

        '入力チェック
        If chk_Nyuryoku() = False Then
            Return
        End If

        '登録
        Call DB_access("03")        '登録モード

        Session.Remove("strMode")
        Session.Remove("strCode")
        Session.Remove("strname")
        Session.Remove("strcamp")

        '元の画面に戻る
        Response.Redirect("m_mail00.aspx")

    End Sub








End Class

