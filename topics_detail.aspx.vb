Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.Common
Imports System.Web.UI.WebControls
Imports System.Activities.Expressions

Partial Class cs_home
    Inherits System.Web.UI.Page

    Public strRow As String
    Public strProcess As String

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim strStatus As String = ""
        'パラメータ取得
        Dim strId As String = Request.QueryString("strId")
        Dim strMode As String = Request.QueryString("strMode")

        If IsPostBack Then
        Else
            Me.DropDownList1.Items.Insert(0, "") '先頭に空白行追加

            '表示モード(01)と更新/削除モード(02)の時のみ、データ取得
            If strMode = "01" Or strMode = "02" Then

                'タイトル存在する場合のみ遷移。
                If Mid(strId, 1, 4) <> "Link" Then
                    'トピックスの明細取得、表示
                    Call GET_TOPICS_DETAIL(strId, strMode)
                End If
            ElseIf strMode = "03" Then
                TextBox1.Text = Format(Now, "yyyy/MM/dd")
                TextBox2.Text = Format(Now, "HH:mm")
                TextBox1.Enabled = False
                TextBox2.Enabled = False
                Button1.Enabled = True
                Button2.Visible = False
                Button3.Visible = False
            End If
        End If

        Call GET_STATUS(strStatus)

        Select Case strStatus
            Case "OK"
                Me.OKNGimg.ImageUrl = "images/OKtouka.png"
            Case "NG"
                Me.OKNGimg.ImageUrl = "images/NGtouka.png"
        End Select

        'サイドのトピックス取得
        Call GET_TOPICS()

        Button1.Attributes.Add("onclick", "return confirm('更新します。よろしいですか？');")
        Button2.Attributes.Add("onclick", "return confirm('更新します。よろしいですか？');")
        Button3.Attributes.Add("onclick", "return confirm('削除します。よろしいですか？');")

    End Sub

    Private Sub GET_STATUS(ByRef strStatus As String)
        '概況のステータスを取得
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()

        strSQL = ""
        strSQL = strSQL & "SELECT *  "
        strSQL = strSQL & "FROM T_EXL_POR_STATUS "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        '初期値セット
        strStatus = "OK"

        '１件でもNGあればNGを返す
        While (dataread.Read())
            If Trim(dataread("DATA_OKNG")) = "NG" Then
                strStatus = "NG"
                Exit While
            End If
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()

    End Sub

    Private Sub GET_TOPICS()
        'トピックスを取得
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim intCnt As Integer = 1
        Dim Linkbtn As New LinkButton

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()

        strSQL = ""
        strSQL = strSQL & "SELECT AA.* "
        strSQL = strSQL & "FROM (SELECT TOP 5 *   "
        strSQL = strSQL & "FROM T_EXL_TOPICS  "
        strSQL = strSQL & "WHERE INFO_DATE > DATEADD(DAY, -30, GETDATE()) "
        strSQL = strSQL & "AND   FIN_FLG = '0' "
        strSQL = strSQL & "ORDER BY INFO_DATE DESC, INFO_TIME DESC) AA "
        strSQL = strSQL & "ORDER BY INFO_DATE , INFO_TIME "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        While (dataread.Read())
            Select Case intCnt
                Case 1
                    Me.Label1.Text = Format(DateTime.Parse(dataread("INFO_DATE")), "M/d")
                    Me.Label6.Text = dataread("CREATE_NM")
                    Me.LinkButton1.Text = dataread("INFO_HEADER")
                    Me.LinkButton1.ID = dataread("INFO_NO")
                Case 2
                    Me.Label2.Text = Format(DateTime.Parse(dataread("INFO_DATE")), "M/d")
                    Me.Label7.Text = dataread("CREATE_NM")
                    Me.LinkButton2.Text = dataread("INFO_HEADER")
                    Me.LinkButton2.ID = dataread("INFO_NO")
                Case 3
                    Me.Label3.Text = Format(DateTime.Parse(dataread("INFO_DATE")), "M/d")
                    Me.Label8.Text = dataread("CREATE_NM")
                    Me.LinkButton3.Text = dataread("INFO_HEADER")
                    Me.LinkButton3.ID = dataread("INFO_NO")
                Case 4
                    Me.Label4.Text = Format(DateTime.Parse(dataread("INFO_DATE")), "M/d")
                    Me.Label9.Text = dataread("CREATE_NM")
                    Me.LinkButton4.Text = dataread("INFO_HEADER")
                    Me.LinkButton4.ID = dataread("INFO_NO")
                Case 5
                    Me.Label5.Text = Format(DateTime.Parse(dataread("INFO_DATE")), "M/d")
                    Me.Label10.Text = dataread("CREATE_NM")
                    Me.LinkButton5.Text = dataread("INFO_HEADER")
                    Me.LinkButton5.ID = dataread("INFO_NO")
            End Select
            intCnt += 1
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()
    End Sub
    Private Sub GET_TOPICS_DETAIL(strINFO_NO As String, strMode As String)
        'トピックス詳細を取得
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim intCnt As Integer = 1

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()

        strSQL = ""
        strSQL = strSQL & "SELECT * "
        strSQL = strSQL & "FROM T_EXL_TOPICS  "
        strSQL = strSQL & "WHERE INFO_NO = '" & strINFO_NO & "' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        While (dataread.Read())
            TextBox1.Text = dataread("INFO_DATE")
            TextBox2.Text = dataread("INFO_TIME")
            DropDownList1.SelectedValue = dataread("CREATE_NM")
            TextBox3.Text = dataread("INFO_HEADER")
            TextBox4.Text = dataread("INFO_DETAIL")
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()

        '表示、更新モード時は、下記の3つは固定でEnabled=Flase
        TextBox1.Enabled = False
        TextBox2.Enabled = False
        DropDownList1.Enabled = False
        Button1.Visible = False

        '表示モードは、下記の4つは固定でEnabled=Flase
        If strMode = "01" Then
            TextBox3.Enabled = False
            TextBox4.Enabled = False
            Button2.Visible = False
            Button3.Visible = False
        End If
    End Sub
    Private Sub Redirect_Detail(strId As String)
        'トピックス詳細画面へ遷移
        '表示モード
        Response.Redirect("topics_detail.aspx?strId=" & strId & "&strMode=01")

    End Sub

    Private Sub LinkButton1_Click(sender As Object, e As EventArgs) Handles LinkButton1.Click
        'トピックス1番目クリック
        Dim lnkbutton = CType(sender, LinkButton)
        Call Redirect_Detail(lnkbutton.ID)
    End Sub

    Private Sub LinkButton2_Click(sender As Object, e As EventArgs) Handles LinkButton2.Click
        'トピックス2番目クリック
        Dim lnkbutton = CType(sender, LinkButton)
        Call Redirect_Detail(lnkbutton.ID)
    End Sub

    Private Sub LinkButton3_Click(sender As Object, e As EventArgs) Handles LinkButton3.Click
        'トピックス3番目クリック
        Dim lnkbutton = CType(sender, LinkButton)
        Call Redirect_Detail(lnkbutton.ID)
    End Sub

    Private Sub LinkButton4_Click(sender As Object, e As EventArgs) Handles LinkButton4.Click
        'トピックス4番目クリック
        Dim lnkbutton = CType(sender, LinkButton)
        Call Redirect_Detail(lnkbutton.ID)
    End Sub

    Private Sub LinkButton5_Click(sender As Object, e As EventArgs) Handles LinkButton5.Click
        'トピックス5番目クリック
        Dim lnkbutton = CType(sender, LinkButton)
        Call Redirect_Detail(lnkbutton.ID)
    End Sub

    Private Function chk_Nyuryoku() As Boolean
        '入力チェック
        Dim strMode As String = Request.QueryString("strMode")

        chk_Nyuryoku = True
        Label11.Text = ""

        '文字数チェック
        If Len(TextBox3.Text) > 25 Then
            Label11.Text = "タイトルは25文字以内で記載してください。"
            chk_Nyuryoku = False
        End If
        If Len(TextBox4.Text) > 512 Then
            Label11.Text = "内容は512文字以内で記載してください。"
            chk_Nyuryoku = False
        End If
        If TextBox3.Text = "" Then
            Label11.Text = "タイトルが空白です。"
            chk_Nyuryoku = False
        End If
        If TextBox4.Text = "" Then
            Label11.Text = "内容が空白です。"
            chk_Nyuryoku = False
        End If
        If DropDownList1.SelectedValue = "" Then
            Label11.Text = "投稿者が選択されていません。"
            chk_Nyuryoku = False
        End If

    End Function
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        '登録ボタン押下

        '入力チェック
        If chk_Nyuryoku() = False Then
            Return
        End If

        '登録
        Call DB_access("ins")

        '元の画面に戻る
        Response.Redirect("topics.aspx")
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        '更新ボタン押下

        '入力チェック
        If chk_Nyuryoku() = False Then
            Return
        End If

        '更新
        Call DB_access("upd")

        '元の画面に戻る
        Response.Redirect("topics.aspx")

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        '削除ボタン押下

        '削除
        Call DB_access("del")

        '元の画面に戻る
        Response.Redirect("topics.aspx")

    End Sub
    Private Sub DB_access(strUpdMode As String)
        '画面入力情報をテーブルへ登録
        Dim strSQL As String
        Dim strVal As String = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=KBHWPM02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand

        'データベース接続を開く
        cnn.Open()

        'パラメータ取得
        Dim strId As String = Request.QueryString("strId")
        Dim strMode As String = Request.QueryString("strMode")

        If strUpdMode = "upd" Then
            'データ更新
            strSQL = ""
            strSQL = strSQL & "UPDATE T_EXL_TOPICS SET"
            strSQL = strSQL & " INFO_HEADER = '" & TextBox3.Text & "' "
            strSQL = strSQL & ",INFO_DETAIL = '" & TextBox4.Text & "' "
            strSQL = strSQL & "WHERE INFO_NO = '" & strId & "' "

            Command.CommandText = strSQL
            ' SQLの実行
            Command.ExecuteNonQuery()

            cnn.Close()
        ElseIf strUpdMode = "del" Then
            'データ削除
            strSQL = ""
            strSQL = strSQL & "DELETE FROM T_EXL_TOPICS "
            strSQL = strSQL & "WHERE INFO_NO = '" & strId & "' "

            Command.CommandText = strSQL
            ' SQLの実行
            Command.ExecuteNonQuery()

            cnn.Close()
        ElseIf strUpdMode = "ins" Then
            'データ登録
            Dim strDetail As String = Replace(TextBox4.Text, "vbCrLf", "<BR>")

            strSQL = ""
            strSQL = strSQL & "INSERT INTO T_EXL_TOPICS "
            strSQL = strSQL & "VALUES( '" & TextBox1.Text & "' "
            strSQL = strSQL & ",       '" & TextBox2.Text & "' "
            strSQL = strSQL & ",       '" & DropDownList1.SelectedValue & "' "
            strSQL = strSQL & ",       '" & TextBox3.Text & "' "
            strSQL = strSQL & ",       '" & strDetail & "' "
            strSQL = strSQL & ",       '0' "
            strSQL = strSQL & ",       '" & Session("UsrId") & "' "
            strSQL = strSQL & ",       '0') "

            Command.CommandText = strSQL
            ' SQLの実行
            Command.ExecuteNonQuery()

            cnn.Close()
        End If

    End Sub

End Class
