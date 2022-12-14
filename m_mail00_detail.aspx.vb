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


            ''接続文字列の作成
            'Dim ConnectionString As String = String.Empty

            ''SQL Server認証
            'ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"

            ''SqlConnectionクラスの新しいインスタンスを初期化
            'Dim cnn = New SqlConnection(ConnectionString)
            'Dim Command = cnn.CreateCommand

            ''データベース接続を開く
            'cnn.Open()

            ''strSQL = "SELECT ITK_BKGNO FROM [T_EXL_CSWORKSTATUS] WHERE [T_EXL_CSWORKSTATUS].ITK_BKGNO = '" & Trim(e.Row.Cells(26).Text) & "' "
            'strSQL = "SELECT BKGNO FROM [T_EXL_WORKSTATUS00] WHERE [T_EXL_WORKSTATUS00].BKGNO = '" & Trim(Replace(e.Row.Cells(16).Text, vbLf, "")) & "' "
            'strSQL = strSQL & "AND [T_EXL_WORKSTATUS00].ID = '006' "

            ''ＳＱＬコマンド作成
            'dbcmd = New SqlCommand(strSQL, cnn)
            ''ＳＱＬ文実行
            'dataread = dbcmd.ExecuteReader()
            'strbkg = ""
            ''結果を取り出す
            'While (dataread.Read())
            '    strbkg += dataread("BKGNO")
            '    '書類作成状況
            '    If Trim(e.Row.Cells(16).Text) = Trim(strbkg) And strbkg <> "&nbsp;" Then
            '        'e.Row.BackColor = Drawing.Color.DarkGray

            '        Dim dltcb01 As CheckBox = e.Row.FindControl("cb01")

            '        dltcb01.Checked = True
            '        'dltcb02.Checked = True

            '    End If
            'End While

            ''クローズ処理
            'dataread.Close()
            'dbcmd.Dispose()


            ''strSQL = "SELECT ITK_BKGNO FROM [T_EXL_CSWORKSTATUS] WHERE [T_EXL_CSWORKSTATUS].ITK_BKGNO = '" & Trim(e.Row.Cells(26).Text) & "' "
            'strSQL = "SELECT BKGNO FROM [T_EXL_WORKSTATUS00] WHERE [T_EXL_WORKSTATUS00].BKGNO = '" & Trim(Replace(e.Row.Cells(16).Text, vbLf, "")) & "' "
            'strSQL = strSQL & "AND [T_EXL_WORKSTATUS00].ID = '007' "

            ''ＳＱＬコマンド作成
            'dbcmd = New SqlCommand(strSQL, cnn)
            ''ＳＱＬ文実行
            'dataread = dbcmd.ExecuteReader()
            'strbkg = ""
            ''結果を取り出す
            'While (dataread.Read())
            '    strbkg += dataread("BKGNO")
            '    '書類作成状況
            '    If Trim(e.Row.Cells(16).Text) = Trim(strbkg) And strbkg <> "&nbsp;" Then
            '        'e.Row.BackColor = Drawing.Color.DarkGray


            '        Dim dltcb02 As CheckBox = e.Row.FindControl("cb02")

            '        'dltcb01.Checked = True
            '        dltcb02.Checked = True

            '    End If
            'End While

            ''クローズ処理
            'dataread.Close()
            'dbcmd.Dispose()


            ''strSQL = "SELECT ITK_BKGNO FROM [T_EXL_CSWORKSTATUS] WHERE [T_EXL_CSWORKSTATUS].ITK_BKGNO = '" & Trim(e.Row.Cells(26).Text) & "' "
            'strSQL = "SELECT BKGNO FROM [T_EXL_WORKSTATUS00] WHERE [T_EXL_WORKSTATUS00].BKGNO = '" & Trim(Replace(e.Row.Cells(16).Text, vbLf, "")) & "' "
            'strSQL = strSQL & "AND [T_EXL_WORKSTATUS00].ID = '008' "

            ''ＳＱＬコマンド作成
            'dbcmd = New SqlCommand(strSQL, cnn)
            ''ＳＱＬ文実行
            'dataread = dbcmd.ExecuteReader()
            'strbkg = ""
            ''結果を取り出す
            'While (dataread.Read())
            '    strbkg += dataread("BKGNO")
            '    '書類作成状況
            '    If Trim(e.Row.Cells(16).Text) = Trim(strbkg) And strbkg <> "&nbsp;" Then

            '        e.Row.Cells(3).Text = "〇"

            '    End If
            'End While

            ''クローズ処理
            'dataread.Close()
            'dbcmd.Dispose()


            ''strSQL = "SELECT ITK_BKGNO FROM [T_EXL_CSWORKSTATUS] WHERE [T_EXL_CSWORKSTATUS].ITK_BKGNO = '" & Trim(e.Row.Cells(26).Text) & "' "
            'strSQL = "SELECT BKGNO FROM [T_EXL_WORKSTATUS00] WHERE [T_EXL_WORKSTATUS00].BKGNO = '" & Trim(Replace(e.Row.Cells(16).Text, vbLf, "")) & "' "
            'strSQL = strSQL & "AND [T_EXL_WORKSTATUS00].ID = '009' "

            ''ＳＱＬコマンド作成
            'dbcmd = New SqlCommand(strSQL, cnn)
            ''ＳＱＬ文実行
            'dataread = dbcmd.ExecuteReader()
            'strbkg = ""
            ''結果を取り出す
            'While (dataread.Read())
            '    strbkg += dataread("BKGNO")
            '    '書類作成状況
            '    If Trim(e.Row.Cells(16).Text) = Trim(strbkg) And strbkg <> "&nbsp;" Then

            '        e.Row.Cells(5).Text = "〇"

            '    End If
            'End While

            ''クローズ処理
            'dataread.Close()
            'dbcmd.Dispose()


            'cnn.Close()
            'cnn.Dispose()



            'If Trim(e.Row.Cells(14).Text) = "LCL" Then
            '    e.Row.Cells(14).BackColor = Drawing.Color.Orange
            'End If

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
                ddl01.SelectedIndex = e.Row.Cells(4).Text
            End If

            If e.Row.Cells(4).Text = 1 Then
                e.Row.Cells(4).Text = "宛先"
            ElseIf e.Row.Cells(4).Text = 2 Then
                e.Row.Cells(4).Text = "CC"
            ElseIf e.Row.Cells(4).Text = 3 Then
                e.Row.Cells(4).Text = "なし"
                e.Row.BackColor = Drawing.Color.LightGray
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
            ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
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
                cnn.Close()
                cnn.Dispose()

                '登録ボタンを非表示
                Button1.Visible = False
            Else
                'CODEの現在値を取得する
                'strSQL = ""
                'strSQL = strSQL & "SELECT DISTINCT "
                'strSQL = strSQL & "  IDENT_CURRENT('M_EXL_LCL_DEC_MAIL') AS CODE "
                'strSQL = strSQL & "FROM M_EXL_LCL_DEC_MAIL "

                ''ＳＱＬコマンド作成 
                'dbcmd = New SqlCommand(strSQL, cnn)
                ''ＳＱＬ文実行 
                'dataread = dbcmd.ExecuteReader()

                ''結果を取り出す 
                'While (dataread.Read())
                '    '現在値＋１をセットする。
                '    'Label1.Text = dataread("CODE") + 1
                'End While

                '更新ボタンを非表示
                Button7.Visible = False
                Button8.Visible = False
            End If




            'Dim tableStyle As TableItemStyle = New TableItemStyle()
            'tableStyle.HorizontalAlign = HorizontalAlign.Center
            'tableStyle.VerticalAlign = VerticalAlign.Middle
            'tableStyle.Width = Unit.Pixel(190)
            ''tableStyle.BackColor = 
            'tableStyle.BackColor = Drawing.ColorTranslator.FromHtml("#e6e6fa")


            'Dim tableStyle2 As TableItemStyle = New TableItemStyle()
            'tableStyle2.HorizontalAlign = HorizontalAlign.Center
            'tableStyle2.VerticalAlign = VerticalAlign.Middle
            'tableStyle2.Width = Unit.Pixel(60)
            ''tableStyle.BackColor = 



            'Dim lis01 = New ListItem

            'Dim TableRow As TableRow
            'Dim TableCell As TableCell
            'Dim TableCell02 As TableCell
            'Dim Linkbtn As New LinkButton
            'Dim txtbox As New TextBox
            'Dim lbl01 As New Label

            'Dim dataread2 As SqlDataReader
            'Dim dbcmd2 As SqlCommand
            ''手順書マスタからデータ取得し、その数の<TD>を作成する。

            'Dim intCnt As Integer = 1
            'Dim intAll As Integer = 0

            'Dim ddlvval01 As Long
            'Dim ddlvval02 As String = ""

            ''接続文字列の作成
            'Dim ConnectionString2 As String = String.Empty
            ''SQL Server認証
            'ConnectionString2 = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
            ''SqlConnectionクラスの新しいインスタンスを初期化
            'Dim cnn2 = New SqlConnection(ConnectionString2)

            ''データベース接続を開く
            'cnn2.Open()

            'strSQL = "SELECT TASK_CD, TASK_NM, REF FROM M_EXL_MAIL00 "
            'strSQL = strSQL & "ORDER BY TASK_CD ASC "

            ''ＳＱＬコマンド作成 
            'dbcmd2 = New SqlCommand(strSQL, cnn2)
            ''ＳＱＬ文実行 
            'dataread2 = dbcmd2.ExecuteReader()

            ''結果を取り出す 
            'While (dataread2.Read())
            '    If dataread2("TASK_CD") = 0 Then     'サーバーパス
            '        strMainPath = dataread2("REF")
            '    ElseIf dataread2("TASK_CD") <> 0 Then '各ファイル
            '        If intCnt = 1 Or intCnt Mod 4 = 1 Then
            '            '次の行を追加
            '            TableRow = New TableRow()
            '        End If



            '        TableCell = New TableCell()
            '        TableRow.Cells.Add(TableCell)
            '        TableCell.ApplyStyle(tableStyle)
            '        lbl01 = New Label
            '        lbl01.Text = dataread2("TASK_NM")
            '        lbl01.ID = dataread2("REF")

            '        TableCell.Controls.Add(lbl01)



            '        TableCell02 = New TableCell()
            '        TableRow.Cells.Add(TableCell02)
            '        TableCell02.ApplyStyle(tableStyle2)
            '        Dim ddl01 As DropDownList = New DropDownList
            '        'ddl01.AutoPostBack = True
            '        'AddHandler ddl01.SelectedIndexChanged, AddressOf ddl01_change
            '        ddl01.Text = dataread2("TASK_NM")
            '        ddl01.ID = dataread2("TASK_CD")
            '        TableCell.Controls.Add(ddl01)
            '        lis01 = New ListItem("なし")
            '        ddl01.Items.Add(lis01)
            '        lis01 = New ListItem("宛先")
            '        ddl01.Items.Add(lis01)
            '        lis01 = New ListItem("CC")
            '        ddl01.Items.Add(lis01)



            '        Call GET_DDLVAL(strCode, dataread2("TASK_CD"), ddlvval01)

            '        If ddlvval01 = 0 Then
            '            ddlvval02 = "なし"
            '        ElseIf ddlvval01 = 1 Then
            '            ddlvval02 = "宛先"
            '        ElseIf ddlvval01 = 2 Then
            '            ddlvval02 = "CC"
            '        End If

            '        ddl01.SelectedValue = ddlvval02 'アドレスとコードで値を取得

            '        TableCell02.Controls.Add(ddl01)



            '        If intCnt Mod 4 = 0 Then
            '            '次の行を追加
            '            Table1.Rows.Add(TableRow)
            '        End If
            '        intCnt += 1
            '    End If
            'End While

            ''３列に収まらなかった場合、残りの列を追加
            'If intCnt - 1 Mod 3 <> 0 Then
            '    Table1.Rows.Add(TableRow)
            'End If

            ''クローズ処理 
            'dataread2.Close()
            'dbcmd2.Dispose()
            'cnn2.Close()
            'cnn2.Dispose()


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
        ConnectionString = "Data Source=KBHWPM02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
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
        ConnectionString = "Data Source=KBHWPM02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        cnn.Open()

        Dim Command = cnn.CreateCommand

        Dim a As String = ""


        If strExecMode = "01" Then

            Dim I As Integer
            For I = 0 To GridView1.Rows.Count - 1
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


            ''登録
            ''strCode = Label1.Text

        End If



        cnn.Close()

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
        ConnectionString = "Data Source=KBHWPM02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
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
        ConnectionString = "Data Source=KBHWPM02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        cnn.Open()

        Dim Command = cnn.CreateCommand

        'データベース接続を開く


        strSQL = ""
        strSQL = strSQL & "INSERT INTO M_EXL_MAIL01 VALUES("
        strSQL = strSQL & "'" & strMail & "' "
        strSQL = strSQL & ",'" & strtask & "' "
        strSQL = strSQL & ",3 "
        strSQL = strSQL & ",'" & strcompany & "' "
        strSQL = strSQL & ",'" & strpsn & "' ) "


        Command.CommandText = strSQL
        ' SQLの実行
        Command.ExecuteNonQuery()



        cnn.Close()

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

        '入力チェック
        'If chk_Nyuryoku() = False Then
        '    Return
        'End If

        '更新
        Call DB_access("01")        '更新モード


        Session.Remove("strMode")
        Session.Remove("strCode")

        '元の画面に戻る
        Response.Redirect("m_mail00.aspx")
    End Sub
    Protected Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        '削除ボタン押下

        '削除
        Call DB_access("02")        '削除モード


        Session.Remove("strMode")
        Session.Remove("strCode")

        '元の画面に戻る
        Response.Redirect("m_mail00.aspx")
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        '登録ボタン押下
        Dim strSQL As String = ""
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand

        '入力チェック
        If chk_Nyuryoku() = False Then
            Return
        End If

        '登録
        Call DB_access("03")        '登録モード


        Session.Remove("strMode")

        '元の画面に戻る
        Response.Redirect("m_mail00.aspx")

    End Sub








End Class

