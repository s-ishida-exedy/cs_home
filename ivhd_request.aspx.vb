Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.Common
Imports System.IO
Imports mod_function
Imports MimeKit

Partial Class cs_home
    Inherits System.Web.UI.Page

    '依頼時
    Const strToAddressI As String = "s-ishida@exedy.com,r-fukao@exedy.com,y-nishiura@exedy.com,sa-sakamoto@exedy.com,k-tagawa@exedy.com"
    Const strCcAddressI As String = "y-tanabe@exedy.com,j-amasaki@exedy.com,r-uchida@exedy.com,te-fujimoto@exedy.com,mt-hamada@exedy.com,d-fujikawa@exedy.com"
    '回答時
    Const strToAddressK As String = "y-tanabe@exedy.com,j-amasaki@exedy.com,r-uchida@exedy.com,te-fujimoto@exedy.com,mt-hamada@exedy.com,d-fujikawa@exedy.com"
    Const strCcAddressK As String = "s-ishida@exedy.com,r-fukao@exedy.com,y-nishiura@exedy.com,sa-sakamoto@exedy.com,k-tagawa@exedy.com"


    'Const strToAddressI As String = "s-ishida@exedy.com"
    'Const strCcAddressI As String = "s-ishida@exedy.com,order-cs@exedy.com"
    ''回答時
    'Const strToAddressK As String = "s-ishida@exedy.com"
    'Const strCcAddressK As String = "s-ishida@exedy.com,order-cs@exedy.com"


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        Label12.Text = ""

        If IsPostBack Then
            ' そうでない時処理
            Dim i As String = ""
        Else

        End If

        '新規IVNO　テキストボックス制御
        'ＣＳチームの場合のみテキストボックスを活性。それ以外は非活性
        If GET_USR_ID() = "FALSE" Then
            'CS以外
            txtMoto.Visible = True
            lblMoto.Visible = False

            lblSN01.Visible = False    '明細のSNNNO
            txtIV01.Enabled = False    '明細のIVNO
            lblSN02.Visible = False    '明細のSNNNO
            txtIV02.Enabled = False    '明細のIVNO
            lblSN03.Visible = False    '明細のSNNNO
            txtIV03.Enabled = False    '明細のIVNO
            lblSN04.Visible = False    '明細のSNNNO
            txtIV04.Enabled = False    '明細のIVNO
            lblSN05.Visible = False    '明細のSNNNO
            txtIV05.Enabled = False    '明細のIVNO
            lblSN06.Visible = False    '明細のSNNNO
            txtIV06.Enabled = False    '明細のIVNO
            lblSN07.Visible = False    '明細のSNNNO
            txtIV07.Enabled = False    '明細のIVNO

            btnBack.Visible = False    '差し戻しボタン

        Else
            'CSチームメンバー
            txtMoto.Visible = False
            lblMoto.Visible = True
            btnRate.Visible = False    'ﾚｰﾄ表示ボタン

            txtSN01.Visible = False    '明細のSNNNO
            txtSN02.Visible = False    '明細のSNNNO
            txtSN03.Visible = False    '明細のSNNNO
            txtSN04.Visible = False    '明細のSNNNO
            txtSN05.Visible = False    '明細のSNNNO
            txtSN06.Visible = False    '明細のSNNNO
            txtSN07.Visible = False    '明細のSNNNO
            btnHyouji01.Enabled = False
            btnHyouji02.Enabled = False
            btnHyouji03.Enabled = False
            btnHyouji04.Enabled = False
            btnHyouji05.Enabled = False
            btnHyouji06.Enabled = False
            btnHyouji07.Enabled = False

        End If

        btnIns.Attributes.Add("onclick", "return confirm('登録します。よろしいですか？');")
        btnBack.Attributes.Add("onclick", "return confirm('差し戻しします。よろしいですか？');")

    End Sub

    Private Function GET_USR_ID() As String
        'ログインユーザーIDがＣＳチームメンバーかどうか判定する

        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String
        Dim strCode As String

        GET_USR_ID = "FALSE"

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()

        strSQL = ""
        strSQL = strSQL & "SELECT COUNT(*) AS RECCNT FROM M_EXL_CS_MEMBER "
        strSQL = strSQL & "WHERE CODE = '" & Trim(Session("UsrId")) & "'"
        strSQL = strSQL & "AND PLACE LIKE 'H%'"

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        strCode = ""
        '結果を取り出す 
        While (dataread.Read())
            If dataread("RECCNT") > 0 Then
                GET_USR_ID = "TRUE"
            End If
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()

    End Function

    Private Sub btnRate_Click(sender As Object, e As EventArgs) Handles btnRate.Click
        'インボイスのレート表示ボタン押下

        If txtMoto.Text = "" Then
            Label12.Text = "元のIVNOを入力してください。"
            Return
        End If

        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String
        Dim strCode As String

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()

        strSQL = ""
        strSQL = strSQL & "SELECT RATE FROM V_T_INV_HD_TB "
        strSQL = strSQL & "WHERE OLD_INVNO = '" & Trim(txtMoto.Text) & "'"

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        strCode = ""
        '結果を取り出す 
        While (dataread.Read())
            Label2.Text = Trim(dataread("RATE"))
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()

    End Sub

    Private Sub GridView1_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowCreated
        'ボタンに行数をセット
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim dltButton As Button = e.Row.FindControl("Button1")
            Dim dltButton2 As Button = e.Row.FindControl("Button2")
            Dim dltButton3 As Button = e.Row.FindControl("Button3")
            Dim dltButton4 As Button = e.Row.FindControl("Button4")
            'ボタンが存在する場合のみセット
            If Not (dltButton Is Nothing) Then
                dltButton.CommandArgument = e.Row.RowIndex.ToString()
                dltButton2.CommandArgument = e.Row.RowIndex.ToString()
                dltButton3.CommandArgument = e.Row.RowIndex.ToString()
                dltButton4.CommandArgument = e.Row.RowIndex.ToString()
            End If
        End If

        'コード列非表示処理
        If e.Row.RowType = DataControlRowType.DataRow OrElse e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(0).Visible = False

            'ログインユーザーによって、ボタン表示制御する。
            If GET_USR_ID() = "FALSE" Then
                'CS以外
                e.Row.Cells(7).Visible = False     '対応ボタン
            Else
                'CSメンバー
                e.Row.Cells(8).Visible = False     '更新ボタン
                e.Row.Cells(9).Visible = False     '完了ボタン
            End If

        End If

    End Sub

    Private Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound

        '回答済みの場合、ボタンの名称を変更する
        For Each control As Control In e.Row.Cells(8).Controls
            If TypeOf control Is Button Then
                If CHK_STATUS(e.Row.Cells(2).Text) = "1" Then
                    CType(control, Button).Text = "確認"
                Else
                    CType(control, Button).Text = "編集"
                End If
            End If
        Next
        For Each control As Control In e.Row.Cells(7).Controls
            If TypeOf control Is Button Then
                If CHK_STATUS(e.Row.Cells(2).Text) = "1" Then
                    CType(control, Button).Text = "確認"
                Else
                    CType(control, Button).Text = "対応"
                End If
            End If
        Next
    End Sub

    Private Function GET_SN_RATE(strSNNO As String) As String
        'SNのレートを取得する。

        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String
        Dim strCode As String

        GET_SN_RATE = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()

        strSQL = ""
        strSQL = strSQL & "SELECT RATE FROM V_T_SN_HD_TB "
        strSQL = strSQL & "WHERE SALESNOTENO = '" & strSNNO & "'"

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        strCode = ""
        '結果を取り出す 
        While (dataread.Read())
            GET_SN_RATE = dataread("RATE")
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()
    End Function

    Private Function GET_CUST_CD(strIVNO As String) As String
        'SNのレートを取得する。

        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String
        Dim strCode As String

        GET_CUST_CD = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()

        strSQL = ""
        strSQL = strSQL & "SELECT CUSTCODE FROM V_T_INV_HD_TB "
        strSQL = strSQL & "WHERE OLD_INVNO = '" & strIVNO & "'"

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        strCode = ""
        '結果を取り出す 
        While (dataread.Read())
            GET_CUST_CD = dataread("CUSTCODE")
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()
    End Function

    Private Sub btnHyouji01_Click(sender As Object, e As EventArgs) Handles btnHyouji01.Click
        '一行目の表示ボタン

        If txtSN01.Text = "" Then
            Label12.Text = "SNNOが入力されていません。"
            Return
        End If

        lblRate01.Text = GET_SN_RATE(txtSN01.Text)
    End Sub

    Private Sub btnHyouji02_Click(sender As Object, e As EventArgs) Handles btnHyouji02.Click
        If txtSN02.Text = "" Then
            Label12.Text = "SNNOが入力されていません。"
            Return
        End If

        lblRate02.Text = GET_SN_RATE(txtSN02.Text)
    End Sub

    Private Sub btnHyouji03_Click(sender As Object, e As EventArgs) Handles btnHyouji03.Click
        If txtSN03.Text = "" Then
            Label12.Text = "SNNOが入力されていません。"
            Return
        End If

        lblRate03.Text = GET_SN_RATE(txtSN03.Text)
    End Sub

    Private Sub btnHyouji04_Click(sender As Object, e As EventArgs) Handles btnHyouji04.Click
        If txtSN04.Text = "" Then
            Label12.Text = "SNNOが入力されていません。"
            Return
        End If

        lblRate04.Text = GET_SN_RATE(txtSN04.Text)
    End Sub

    Private Sub btnHyouji05_Click(sender As Object, e As EventArgs) Handles btnHyouji05.Click
        If txtSN05.Text = "" Then
            Label12.Text = "SNNOが入力されていません。"
            Return
        End If

        lblRate05.Text = GET_SN_RATE(txtSN05.Text)
    End Sub

    Private Sub btnHyouji06_Click(sender As Object, e As EventArgs) Handles btnHyouji06.Click
        If txtSN06.Text = "" Then
            Label12.Text = "SNNOが入力されていません。"
            Return
        End If

        lblRate06.Text = GET_SN_RATE(txtSN06.Text)
    End Sub

    Private Sub btnHyouji07_Click(sender As Object, e As EventArgs) Handles btnHyouji07.Click
        If txtSN07.Text = "" Then
            Label12.Text = "SNNOが入力されていません。"
            Return
        End If

        lblRate07.Text = GET_SN_RATE(txtSN07.Text)
    End Sub

    Private Sub btnIns_Click(sender As Object, e As EventArgs) Handles btnIns.Click
        '登録（更新）ボタン押下
        Dim tran As System.Data.SqlClient.SqlTransaction = Nothing
        Dim strSQL As String = ""
        Dim strVal As String = ""
        Dim strMode As String = ""

        'イントラ用
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand

        If GET_USR_ID() = "FALSE" Then
            'CS以外

            '入力データの既存チェック
            Select Case CHK_KIZON_DATA(Trim(txtMoto.Text), Trim(txtSN01.Text))
                Case "0"
                    strMode = "SHINKI"            '新規登録
                Case "1"
                    Label12.Text = "このIVNOは依頼中です。"
                    Return
                Case "2"
                    strMode = "KOUSIN"            '更新
            End Select

            '入力チェック
            If Chk_Nyuryoku("01") = False Then
                Return
            End If

            'テーブルへINSERT(更新はDELETE-INSERT)
            cnn.Open()

            If strMode = "KOUSIN" Then
                '既存データ削除
                strSQL = ""
                strSQL = strSQL & "DELETE FROM T_EXL_IVHD_REQ "
                strSQL = strSQL & "WHERE IVNO = '" & Trim(txtMoto.Text) & "' AND STATUS <> '9' "
                Command.CommandText = strSQL
                Command.ExecuteNonQuery()
            End If

            strSQL = ""
            strSQL = strSQL & "INSERT INTO T_EXL_IVHD_REQ "
            strSQL = strSQL & "(CUST_CD,IVNO,IV_RATE,SNNO,SN_RATE,NEW_IV,REQUESTER,REQ_DATE,STATUS) VALUES "
            If Trim(txtSN01.Text) <> "" Then strSQL = strSQL & MAKE_INSERT_SQL(Trim(txtSN01.Text), lblRate01.Text) '1行目
            If Trim(txtSN02.Text) <> "" Then strSQL = strSQL & "," & MAKE_INSERT_SQL(Trim(txtSN02.Text), lblRate02.Text) '2行目
            If Trim(txtSN03.Text) <> "" Then strSQL = strSQL & "," & MAKE_INSERT_SQL(Trim(txtSN03.Text), lblRate03.Text) '3行目
            If Trim(txtSN04.Text) <> "" Then strSQL = strSQL & "," & MAKE_INSERT_SQL(Trim(txtSN04.Text), lblRate04.Text) '4行目
            If Trim(txtSN05.Text) <> "" Then strSQL = strSQL & "," & MAKE_INSERT_SQL(Trim(txtSN05.Text), lblRate05.Text) '5行目
            If Trim(txtSN06.Text) <> "" Then strSQL = strSQL & "," & MAKE_INSERT_SQL(Trim(txtSN06.Text), lblRate06.Text) '6行目
            If Trim(txtSN07.Text) <> "" Then strSQL = strSQL & "," & MAKE_INSERT_SQL(Trim(txtSN07.Text), lblRate07.Text) '7行目

            Command.CommandText = strSQL
            Command.ExecuteNonQuery()

            'クローズ処理 
            cnn.Close()
            cnn.Dispose()

            Call Send_Mail("IRAI")
        Else
            'CSメンバー

            '更新（CSの場合は、対象のIVNO登録）
            '入力チェック
            If Chk_Nyuryoku("02") = False Then
                Return
            End If

            'テーブルへINSERT(更新はDELETE-INSERT)
            cnn.Open()

            Dim intCnt As Integer = 1

            '１行ずつ更新
            For intCnt = 1 To 7
                strSQL = ""
                strSQL = strSQL & "UPDATE T_EXL_IVHD_REQ SET "
                Select Case intCnt
                    Case 1
                        strSQL = strSQL & "NEW_IV = '" & Trim(txtIV01.Text) & "', STATUS = '1' "
                        strSQL = strSQL & "WHERE IVNO = '" & lblMoto.Text & "' AND SNNO = '" & lblSN01.Text & "' "
                    Case 2
                        strSQL = strSQL & "NEW_IV = '" & Trim(txtIV02.Text) & "', STATUS = '1' "
                        strSQL = strSQL & "WHERE IVNO = '" & lblMoto.Text & "' AND SNNO = '" & lblSN02.Text & "' "
                    Case 3
                        strSQL = strSQL & "NEW_IV = '" & Trim(txtIV03.Text) & "', STATUS = '1' "
                        strSQL = strSQL & "WHERE IVNO = '" & lblMoto.Text & "' AND SNNO = '" & lblSN03.Text & "' "
                    Case 4
                        strSQL = strSQL & "NEW_IV = '" & Trim(txtIV04.Text) & "', STATUS = '1' "
                        strSQL = strSQL & "WHERE IVNO = '" & lblMoto.Text & "' AND SNNO = '" & lblSN04.Text & "' "
                    Case 5
                        strSQL = strSQL & "NEW_IV = '" & Trim(txtIV05.Text) & "', STATUS = '1' "
                        strSQL = strSQL & "WHERE IVNO = '" & lblMoto.Text & "' AND SNNO = '" & lblSN05.Text & "' "
                    Case 6
                        strSQL = strSQL & "NEW_IV = '" & Trim(txtIV06.Text) & "', STATUS = '1' "
                        strSQL = strSQL & "WHERE IVNO = '" & lblMoto.Text & "' AND SNNO = '" & lblSN06.Text & "' "
                    Case 7
                        strSQL = strSQL & "NEW_IV = '" & Trim(txtIV07.Text) & "', STATUS = '1' "
                        strSQL = strSQL & "WHERE IVNO = '" & lblMoto.Text & "' AND SNNO = '" & lblSN07.Text & "' "
                End Select
                Command.CommandText = strSQL
                Command.ExecuteNonQuery()
            Next

            'クローズ処理 
            cnn.Close()
            cnn.Dispose()

            Call Send_Mail("KAITO")
        End If

        'リセット
        Call EXEC_RESERT()

        'グリッド再表示
        GridView1.DataBind()
    End Sub

    Private Function CHK_KIZON_DATA(strIV As String, strSN As String) As String
        '既存データチェック
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String
        Dim intCnt As Integer = 0
        CHK_KIZON_DATA = "0"

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()

        strSQL = ""
        strSQL = strSQL & "SELECT COUNT(*) AS RECCNT "
        strSQL = strSQL & "FROM T_EXL_IVHD_REQ "
        strSQL = strSQL & "WHERE IVNO = '" & strIV & "' "
        strSQL = strSQL & "AND STATUS <> '9' "

        dbcmd = New SqlCommand(strSQL, cnn)
        dataread = dbcmd.ExecuteReader()

        '結果を取り出す 
        While (dataread.Read())
            intCnt = dataread("RECCNT")
        End While

        dataread.Close()
        dbcmd.Dispose()

        If intCnt > 0 Then CHK_KIZON_DATA = "1"     '登録済み状態

        strSQL = ""
        strSQL = strSQL & "SELECT COUNT(*) AS RECCNT "
        strSQL = strSQL & "FROM T_EXL_IVHD_REQ "
        strSQL = strSQL & "WHERE IVNO = '" & strIV & "' "
        strSQL = strSQL & "AND SNNO = '" & strSN & "' "
        strSQL = strSQL & "AND STATUS IN ('0','2') "

        dbcmd = New SqlCommand(strSQL, cnn)
        dataread = dbcmd.ExecuteReader()

        '結果を取り出す 
        While (dataread.Read())
            intCnt = dataread("RECCNT")
        End While

        If intCnt > 0 Then CHK_KIZON_DATA = "2"     '更新可能

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()
    End Function

    Private Function MAKE_INSERT_SQL(strSN As String, strRate As String) As String
        Dim strSQL As String = ""
        Dim dt1 As DateTime = DateTime.Now
        Dim strDate As String = dt1.ToString("yyyy/MM/dd HH:mm:ss")

        MAKE_INSERT_SQL = ""

        '元のIVNOに対する客先コード取得
        Dim strCust = GET_CUST_CD(Trim(txtMoto.Text))

        strSQL = strSQL & "(  "
        strSQL = strSQL & "'" & strCust & "' "
        strSQL = strSQL & ",'" & Trim(txtMoto.Text) & "' "
        strSQL = strSQL & ",'" & Trim(Label2.Text) & "' "
        strSQL = strSQL & ",'" & strSN & "' "
        strSQL = strSQL & "," & Double.Parse(strRate) & " "
        strSQL = strSQL & ",'' "
        strSQL = strSQL & ",'" & Session("UsrId") & "' "
        strSQL = strSQL & ",'" & strDate & "' "
        strSQL = strSQL & ",'0') "
        MAKE_INSERT_SQL = strSQL
    End Function

    Private Function Chk_Nyuryoku(strMode As String) As Boolean
        '入力チェック

        Chk_Nyuryoku = True

        If strMode = "01" Then          '新規登録
            If (txtSN01.Text <> "" And lblRate01.Text = "-----") Or (txtSN02.Text <> "" And lblRate02.Text = "-----") Or
                (txtSN03.Text <> "" And lblRate03.Text = "-----") Or (txtSN04.Text <> "" And lblRate04.Text = "-----") Or
                (txtSN05.Text <> "" And lblRate05.Text = "-----") Or (txtSN06.Text <> "" And lblRate06.Text = "-----") Or
                (txtSN07.Text <> "" And lblRate07.Text = "-----") Then
                Label12.Text = "明細のレート表示を押してください。"
                Chk_Nyuryoku = False
            End If
            If txtSN01.Text = "" And txtSN02.Text = "" And txtSN03.Text = "" And txtSN04.Text = "" And
                txtSN05.Text = "" And txtSN06.Text = "" And txtSN07.Text = "" Then
                Label12.Text = "登録対象がありません。"
                Chk_Nyuryoku = False
            End If
            If Label2.Text = "" Then
                Label12.Text = "元のレートが空白です。"
                Chk_Nyuryoku = False
            End If
            If txtMoto.Text = "" Then
                Label12.Text = "元のIVNOが空白です。"
                Chk_Nyuryoku = False
            End If
        ElseIf strMode = "02" Then      'CS側の登録ボタン押下
            If txtIV01.Text = "" And txtIV02.Text = "" And txtIV03.Text = "" And txtIV04.Text = "" And
               txtIV05.Text = "" And txtIV06.Text = "" And txtIV07.Text = "" Then
                Label12.Text = "新規インボイスNOが空白です。"
                Chk_Nyuryoku = False
            End If
            If (Trim(txtIV01.Text) <> "" And HankakuNumChk(txtIV01.Text) = False) Or
               (Trim(txtIV02.Text) <> "" And HankakuNumChk(txtIV02.Text) = False) Or
               (Trim(txtIV03.Text) <> "" And HankakuNumChk(txtIV03.Text) = False) Or
               (Trim(txtIV04.Text) <> "" And HankakuNumChk(txtIV04.Text) = False) Or
               (Trim(txtIV05.Text) <> "" And HankakuNumChk(txtIV05.Text) = False) Or
               (Trim(txtIV06.Text) <> "" And HankakuNumChk(txtIV06.Text) = False) Or
               (Trim(txtIV07.Text) <> "" And HankakuNumChk(txtIV07.Text) = False) Then
                Label12.Text = "新規インボイスNOに半角数字以外が使われています。"
                Chk_Nyuryoku = False
            End If
            If (Trim(txtIV01.Text) <> "" And Len(Trim(txtIV01.Text)) <> 4) Or
               (Trim(txtIV02.Text) <> "" And Len(Trim(txtIV02.Text)) <> 4) Or
               (Trim(txtIV03.Text) <> "" And Len(Trim(txtIV03.Text)) <> 4) Or
               (Trim(txtIV04.Text) <> "" And Len(Trim(txtIV04.Text)) <> 4) Or
               (Trim(txtIV05.Text) <> "" And Len(Trim(txtIV05.Text)) <> 4) Or
               (Trim(txtIV06.Text) <> "" And Len(Trim(txtIV06.Text)) <> 4) Or
               (Trim(txtIV07.Text) <> "" And Len(Trim(txtIV07.Text)) <> 4) Then
                Label12.Text = "新規インボイスNOは半角数字４桁で入力してください。"
                Chk_Nyuryoku = False
            End If
        End If
    End Function

    Private Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        'グリッド内のボタン押下処理
        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim data1 = Me.GridView1.Rows(index).Cells(2).Text

        '更新
        If e.CommandName = "sup" Then
            '対応ボタン
            'ステータスが0：依頼中
            If CHK_STATUS(data1) = "0" Then
                Call SET_DATA(data1, "0")            '明細を右側にセット
            ElseIf CHK_STATUS(data1) = "1" Then
                '回答済みの場合、ボタン名は確認。右側を非活性にしてセットする。
                Call SET_DATA(data1, "0")            '明細を右側にセット

                txtIV01.Enabled = False
                txtIV02.Enabled = False
                txtIV03.Enabled = False
                txtIV04.Enabled = False
                txtIV05.Enabled = False
                txtIV06.Enabled = False
                txtIV07.Enabled = False
            Else
                Label12.Text = "ｽﾃｰﾀｽが依頼中のみ押下可能です。"
                Return
            End If
        ElseIf e.CommandName = "upd" Then
            '更新ボタン
            'ステータスが2：差し戻しのみＯＫ
            If CHK_STATUS(data1) = "2" Then
                Call SET_DATA(data1, "1")            '明細を右側にセット

                txtSN01.Enabled = True
                txtSN02.Enabled = True
                txtSN03.Enabled = True
                txtSN04.Enabled = True
                txtSN05.Enabled = True
                txtSN06.Enabled = True
                txtSN07.Enabled = True
            ElseIf CHK_STATUS(data1) = "1" Then
                '回答済みの場合、ボタン名は確認。右側を非活性にしてセットする。
                Call SET_DATA(data1, "1")            '明細を右側にセット

                txtSN01.Enabled = False
                txtSN02.Enabled = False
                txtSN03.Enabled = False
                txtSN04.Enabled = False
                txtSN05.Enabled = False
                txtSN06.Enabled = False
                txtSN07.Enabled = False
            Else
                Label12.Text = "ｽﾃｰﾀｽが差戻しのみ押下可能です。"
                Return
            End If
        ElseIf e.CommandName = "fin" Then
            '完了ボタン
            'ステータスが1：回答済みのみＯＫ
            If CHK_STATUS(data1) = "1" Then
                'ステータスをUPDATE
                Call UPD_STATUS(data1, "FIN")
            Else
                Label12.Text = "ｽﾃｰﾀｽが回答済みのみ押下可能です。"
                Return
            End If
            Call EXEC_RESERT()
        ElseIf e.CommandName = "del" Then
            '削除ボタン
            'ステータスを確認し、回答済みの場合エラー
            If CHK_STATUS(data1) = "1" Then
                Label12.Text = "既に回答済みの場合、削除できません。"
                Return
            Else
                'ステータスをUPDATE
                Call UPD_STATUS(data1, "DEL")
            End If
        End If

        GridView1.DataBind()
    End Sub

    Private Function CHK_STATUS(strIV As String) As String
        'ステータスチェック
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String
        Dim intCnt As Integer = 0
        CHK_STATUS = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()

        strSQL = ""
        strSQL = strSQL & "SELECT STATUS "
        strSQL = strSQL & "FROM T_EXL_IVHD_REQ "
        strSQL = strSQL & "WHERE IVNO = '" & strIV & "' "
        strSQL = strSQL & "AND STATUS <> '9' "

        dbcmd = New SqlCommand(strSQL, cnn)
        dataread = dbcmd.ExecuteReader()

        '結果を取り出す 
        While (dataread.Read())
            CHK_STATUS = dataread("STATUS")
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()
    End Function

    Private Sub UPD_STATUS(strIV As String, strMode As String)
        'ステータス更新
        Dim strSQL As String

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand

        'データベース接続を開く
        cnn.Open()

        strSQL = ""
        If strMode = "FIN" Then
            strSQL = strSQL & "UPDATE T_EXL_IVHD_REQ SET STATUS = '9' "
            strSQL = strSQL & "WHERE IVNO = '" & strIV & "' "
            strSQL = strSQL & "AND STATUS = '1' "
        ElseIf strMode = "DEL" Then
            strSQL = strSQL & "DELETE FROM T_EXL_IVHD_REQ "
            strSQL = strSQL & "WHERE IVNO = '" & strIV & "' "
            strSQL = strSQL & "AND STATUS <> '9' "
        ElseIf strMode = "MODOSI" Then
            strSQL = strSQL & "UPDATE T_EXL_IVHD_REQ SET STATUS = '2' "
            strSQL = strSQL & "WHERE IVNO = '" & strIV & "' "
            strSQL = strSQL & "AND STATUS = '0' "
        End If

        Command.CommandText = strSQL
        Command.ExecuteNonQuery()

        'クローズ処理 
        cnn.Close()
        cnn.Dispose()
    End Sub

    Private Sub SET_DATA(strIV As String, strMode As String)
        'GRID VIEWで選択したデータを右側のテキストボックスにセットする
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String
        Dim strCode As String
        Dim intCnt As Integer = 1

        '一旦リセット
        Call EXEC_RESERT()

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()

        strSQL = ""
        strSQL = strSQL & "SELECT "
        strSQL = strSQL & "  IVNO "
        strSQL = strSQL & "  , IV_RATE "
        strSQL = strSQL & "  , SNNO "
        strSQL = strSQL & "  , SN_RATE "
        strSQL = strSQL & "  , ISNULL(NEW_IV,'')AS NEW_IV "
        strSQL = strSQL & "FROM "
        strSQL = strSQL & "  T_EXL_IVHD_REQ A "
        strSQL = strSQL & "WHERE IVNO = '" & strIV & "' "
        strSQL = strSQL & "AND STATUS IN ('0','1','2') "
        strSQL = strSQL & "ORDER BY A.IV_CODE "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        strCode = ""
        '結果を取り出す 
        While (dataread.Read())

            If GET_USR_ID() = "FALSE" Then
                'CS以外
                txtMoto.Text = dataread("IVNO")
            Else
                lblMoto.Text = dataread("IVNO")
            End If

            Label2.Text = dataread("IV_RATE")

            Select Case intCnt
                Case 1
                    If strMode = "0" Then
                        lblSN01.Text = dataread("SNNO")
                    Else
                        txtSN01.Text = dataread("SNNO")
                    End If
                    lblRate01.Text = dataread("SN_RATE")
                    txtIV01.Text = dataread("NEW_IV")
                Case 2
                    If strMode = "0" Then
                        lblSN02.Text = dataread("SNNO")
                    Else
                        txtSN02.Text = dataread("SNNO")
                    End If
                    lblRate02.Text = dataread("SN_RATE")
                    txtIV02.Text = dataread("NEW_IV")
                Case 3
                    If strMode = "0" Then
                        lblSN03.Text = dataread("SNNO")
                    Else
                        txtSN03.Text = dataread("SNNO")
                    End If
                    lblRate03.Text = dataread("SN_RATE")
                    txtIV03.Text = dataread("NEW_IV")
                Case 4
                    If strMode = "0" Then
                        lblSN04.Text = dataread("SNNO")
                    Else
                        txtSN04.Text = dataread("SNNO")
                    End If
                    lblRate04.Text = dataread("SN_RATE")
                    txtIV04.Text = dataread("NEW_IV")
                Case 5
                    If strMode = "0" Then
                        lblSN05.Text = dataread("SNNO")
                    Else
                        txtSN05.Text = dataread("SNNO")
                    End If
                    lblRate05.Text = dataread("SN_RATE")
                    txtIV05.Text = dataread("NEW_IV")
                Case 6
                    If strMode = "0" Then
                        lblSN06.Text = dataread("SNNO")
                    Else
                        txtSN06.Text = dataread("SNNO")
                    End If
                    lblRate06.Text = dataread("SN_RATE")
                    txtIV06.Text = dataread("NEW_IV")
                Case 7
                    If strMode = "0" Then
                        lblSN07.Text = dataread("SNNO")
                    Else
                        txtSN07.Text = dataread("SNNO")
                    End If
                    lblRate07.Text = dataread("SN_RATE")
                    txtIV07.Text = dataread("NEW_IV")
            End Select

            intCnt += 1
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()
    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        'リセットボタン押下
        Call EXEC_RESERT()

        GridView1.DataBind()
    End Sub

    Private Sub EXEC_RESERT()
        txtMoto.Text = ""
        lblMoto.Text = ""
        Label2.Text = ""

        txtSN01.Text = ""
        lblSN01.Text = ""
        lblRate01.Text = "-----"
        txtIV01.Text = ""
        txtSN02.Text = ""
        lblSN02.Text = ""
        lblRate02.Text = "-----"
        txtIV02.Text = ""
        txtSN03.Text = ""
        lblSN03.Text = ""
        lblRate03.Text = "-----"
        txtIV03.Text = ""
        txtSN04.Text = ""
        lblSN04.Text = ""
        lblRate04.Text = "-----"
        txtIV04.Text = ""
        txtSN05.Text = ""
        lblSN05.Text = ""
        lblRate05.Text = "-----"
        txtIV05.Text = ""
        txtSN06.Text = ""
        lblSN06.Text = ""
        lblRate06.Text = "-----"
        txtIV06.Text = ""
        txtSN07.Text = ""
        lblSN07.Text = ""
        lblRate07.Text = "-----"
        txtIV07.Text = ""

        txtMsg.Text = ""
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        '差戻ボタン押下
        Call UPD_STATUS(lblMoto.Text, "MODOSI")

        'メール送信
        Call Send_Mail("BACK")

        Call EXEC_RESERT()
        GridView1.DataBind()
    End Sub

    Private Sub Send_Mail(strmode As String)
        'メールの送信に必要な情報
        Dim smtpHostName As String = "svsmtp01.exedy.co.jp"
        Dim smtpPort As Integer = 25
        Dim strfrom As String = ""
        Dim subject As String = ""
        Dim body As String = ""
        Dim strCust As String = ””
        ' MailKit におけるメールの情報
        Dim message = New MimeKit.MimeMessage()

        '元のインボイスNOから客先コードを取得
        If Trim(txtMoto.Text) <> "" Then
            strCust = CHK_CUSTCD(txtMoto.Text)
        Else
            strCust = CHK_CUSTCD(lblMoto.Text)
        End If

        If strmode = "IRAI" Then
            '依頼時
            strfrom = GET_USR_DATA()        ' 送信者
            message.From.Add(MailboxAddress.Parse(strfrom))        ' 送り元情報 
            subject = "【ご依頼・自動配信】インボイス追加登録" & strCust & "向け"     'メールの件名
            body = UriBodyC(strmode)               'メールの本文
            'Toのメールアドレスを取得
            If strToAddressI <> "" Then
                'カンマ区切りをSPLIT
                Dim strVal() As String = strToAddressI.Split(",")
                For Each c In strVal
                    message.To.Add(New MailboxAddress("", c))
                Next
            End If
            'Ccのメールアドレスを取得
            If strCcAddressI <> "" Then
                'カンマ区切りをSPLIT
                Dim strVal() As String = strCcAddressI.Split(",")
                For Each c In strVal
                    message.Cc.Add(New MailboxAddress("", c))
                Next
            End If
        ElseIf strmode = "KAITO" Then
            '回答時
            strfrom = GET_USR_DATA()        ' 送信者
            message.From.Add(MailboxAddress.Parse(strfrom))        ' 送り元情報 
            subject = "【ご回答・自動配信】インボイス追加登録" & strCust & "向け"     'メールの件名
            body = UriBodyC(strmode)              'メールの本文
            'Toのメールアドレスを取得
            If strToAddressK <> "" Then
                'カンマ区切りをSPLIT
                Dim strVal() As String = strToAddressK.Split(",")
                For Each c In strVal
                    message.To.Add(New MailboxAddress("", c))
                Next
            End If
            'Ccのメールアドレスを取得
            If strCcAddressK <> "" Then
                'カンマ区切りをSPLIT
                Dim strVal() As String = strCcAddressK.Split(",")
                For Each c In strVal
                    message.Cc.Add(New MailboxAddress("", c))
                Next
            End If
        ElseIf strmode = "BACK" Then
            '差し戻し時
            strfrom = GET_USR_DATA()        ' 送信者
            message.From.Add(MailboxAddress.Parse(strfrom))        ' 送り元情報 
            subject = "【差戻し・自動配信】インボイス追加登録" & strCust & "向け"    'メールの件名
            body = UriBodyC(strmode)              'メールの本文
            'Toのメールアドレスを取得
            If strToAddressK <> "" Then
                'カンマ区切りをSPLIT
                Dim strVal() As String = strToAddressK.Split(",")
                For Each c In strVal
                    message.To.Add(New MailboxAddress("", c))
                Next
            End If
            'Ccのメールアドレスを取得
            If strCcAddressK <> "" Then
                'カンマ区切りをSPLIT
                Dim strVal() As String = strCcAddressK.Split(",")
                For Each c In strVal
                    message.Cc.Add(New MailboxAddress("", c))
                Next
            End If
        End If

        ' 表題  
        message.Subject = subject

        ' メール作成、送信
        Dim bodyBuilder As New BodyBuilder
        bodyBuilder.HtmlBody = body
        message.Body = bodyBuilder.ToMessageBody

        Using client As New MailKit.Net.Smtp.SmtpClient()
            client.Connect(smtpHostName, smtpPort, MailKit.Security.SecureSocketOptions.Auto)
            client.Send(message)
            client.Disconnect(True)
            client.Dispose()
            message.Dispose()
        End Using

    End Sub

    Public Function UriBodyC(strmode As String) As String
        'メール本文の作成（メール用）

        Dim Bdy As String = ""
        Dim strBdy As String = ""

        Bdy = Bdy + "－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－<br/>"
        Bdy = Bdy + "このメールはシステムから送信されています。<br/>"
        Bdy = Bdy + "心当たりが無い場合、ポータルサイト管理者までご連絡ください。<br/>"
        Bdy = Bdy + "－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－<br/>"

        If strmode = "IRAI" Then
            Call Make_Mail_TABLE(strBdy)
            Bdy = Bdy + "ＣＳチーム　担当者殿<BR>"
            Bdy = Bdy + "お世話になります。<BR>"
            Bdy = Bdy + "レート違いの出荷がある為インボイスの追加登録お願いします。<BR>"
            Bdy = Bdy + "<br/>"
            Bdy = Bdy + "元のIVNO:" + txtMoto.Text + "<br/>"
            Bdy = Bdy + "元のﾚｰﾄ:" + Label2.Text + "<br/>"
            Bdy = Bdy + strBdy
            Bdy = Bdy + "メッセージ：<br/>"
            Bdy = Bdy + txtMsg.Text + "<br/>"
            Bdy = Bdy + "<br/>"
            Bdy = Bdy + "以上、よろしくお願いします。"
            Bdy = Bdy + "<br/><br/>"
            Bdy = Bdy + "【ＣＳ担当者はＣＳポータルにログインし、対応してください。】"
        ElseIf strmode = "KAITO" Then
            Call Make_Mail_TABLE(strBdy)
            Bdy = Bdy + "ＥＸＬ　出荷担当者殿<BR>"
            Bdy = Bdy + "お世話になります。<BR>"
            Bdy = Bdy + "インボイスの追加登録完了しましたので、確認お願いします。<BR>"
            Bdy = Bdy + "<br/>"
            Bdy = Bdy + "元のIVNO:" + lblMoto.Text + "<br/>"
            Bdy = Bdy + "元のﾚｰﾄ:" + Label2.Text + "<br/>"
            Bdy = Bdy + strBdy
            Bdy = Bdy + "メッセージ：<br/>"
            Bdy = Bdy + txtMsg.Text + "<br/>"
            Bdy = Bdy + "<br/>"
            Bdy = Bdy + "以上、よろしくお願いします。"
            Bdy = Bdy + "<br/><br/>"
            Bdy = Bdy + "【ＥＸＬ担当者はＣＳポータルにログインし、確認してください。】"
        ElseIf strmode = "BACK" Then
            Call Make_Mail_TABLE(strBdy)
            Bdy = Bdy + "ＥＸＬ　出荷担当者殿<BR>"
            Bdy = Bdy + "お世話になります。<BR>"
            Bdy = Bdy + "インボイスの追加登録が差し戻しとなりました。<BR>"
            Bdy = Bdy + "確認し、再度依頼してください。<BR>"
            Bdy = Bdy + "<br/>"
            Bdy = Bdy + "元のIVNO:" + lblMoto.Text + "<br/>"
            Bdy = Bdy + "元のﾚｰﾄ:" + Label2.Text + "<br/>"
            Bdy = Bdy + strBdy
            Bdy = Bdy + "メッセージ：<br/>"
            Bdy = Bdy + txtMsg.Text + "<br/>"
            Bdy = Bdy + "<br/>"
            Bdy = Bdy + "以上、よろしくお願いします。"
            Bdy = Bdy + "<br/><br/>"
            Bdy = Bdy + "【ＥＸＬ担当者はＣＳポータルにログインし、確認してください。】"
        End If

        Return Bdy
    End Function

    Private Function GET_USR_DATA() As String
        '送信者のメールアドレス情報を取得
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String

        GET_USR_DATA = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()

        strSQL = strSQL & "Select * FROM M_EXL_USR "
        strSQL = strSQL & "WHERE uid = '" & Session("UsrId") & "' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        strDate = ""
        '結果を取り出す 
        While (dataread.Read())
            GET_USR_DATA = dataread("e_mail")
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()

    End Function

    Private Function GET_CS_Member(intMode As Integer) As String
        'CSメンバー情報を取得
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String

        GET_CS_Member = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()

        strSQL = strSQL & "SELECT * FROM M_EXL_CS_MEMBER "
        strSQL = strSQL & "WHERE CODE = '" & Session("UsrId") & "' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        strDate = ""
        '結果を取り出す 
        While (dataread.Read())
            Select Case intMode
                Case 1
                    GET_CS_Member = dataread("COMPANY")
                Case 2
                    GET_CS_Member = dataread("TEAM")
                Case 3
                    GET_CS_Member = dataread("MEMBER_NAME")
                Case 4
                    GET_CS_Member = dataread("TEL_NO")
                Case 5
                    GET_CS_Member = dataread("FAX_NO")
                Case 6
                    GET_CS_Member = dataread("E_MAIL")
            End Select
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()

    End Function

    Private Function CHK_CUSTCD(strIV As String) As String
        'インボイスNOから客先コードを取得する。
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String
        Dim intCnt As Integer = 0
        CHK_CUSTCD = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()

        strSQL = ""
        strSQL = strSQL & "SELECT CUSTCODE "
        strSQL = strSQL & "FROM V_T_INV_HD_TB "
        strSQL = strSQL & "WHERE OLD_INVNO = '" & Trim(strIV) & "' "

        dbcmd = New SqlCommand(strSQL, cnn)
        dataread = dbcmd.ExecuteReader()

        '結果を取り出す 
        While (dataread.Read())
            CHK_CUSTCD = dataread("CUSTCODE")
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()
    End Function

    Private Sub Make_Mail_TABLE(ByRef Bdy As String)
        'メールに記載する表の作成
        Bdy = Bdy + "<table width=75% border=""1"" style=""border-collapse: collapse"">"
        Bdy = Bdy + "    <tr>"
        Bdy = Bdy + "        <td width=""25%"" align=""center""  bgcolor=""#87E7AD"" style =""font-weight:bold"">SNNO</td>"
        Bdy = Bdy + "        <td width=""54px"" align=""center"" bgcolor=""#87E7AD"" style =""font-weight:bold"">レート</td>"
        Bdy = Bdy + "        <td width=""54px"" align=""center"" bgcolor=""#87E7AD"" style =""font-weight:bold"">IVNO</td>"
        Bdy = Bdy + "    </tr>"
        If txtSN01.Text <> "" Or lblSN01.Text <> "" Then
            Bdy = Bdy + "    <tr>"
            If txtSN01.Text <> "" Then
                Bdy = Bdy + "        <td width=""25%"" align=""center""  bgcolor=""#ffffff"">" & Trim(txtSN01.Text) & "</td>"
            ElseIf lblSN01.Text <> "" Then
                Bdy = Bdy + "        <td width=""25%"" align=""center""  bgcolor=""#ffffff"">" & lblSN01.Text & "</td>"
            End If
            Bdy = Bdy + "        <td width=""54px"" align=""center"" bgcolor=""#ffffff"">" & lblRate01.Text & "</td>"
            Bdy = Bdy + "        <td width=""54px"" align=""center"" bgcolor=""#ffffff"">" & Trim(txtIV01.Text) & "</td>"
            Bdy = Bdy + "    </tr>"
        End If
        If txtSN02.Text <> "" Or lblSN02.Text <> "" Then
            Bdy = Bdy + "    <tr>"
            If txtSN02.Text <> "" Then
                Bdy = Bdy + "        <td width=""25%"" align=""center""  bgcolor=""#ffffff"">" & Trim(txtSN02.Text) & "</td>"
            ElseIf lblSN02.Text <> "" Then
                Bdy = Bdy + "        <td width=""25%"" align=""center""  bgcolor=""#ffffff"">" & lblSN02.Text & "</td>"
            End If
            Bdy = Bdy + "        <td width=""54px"" align=""center"" bgcolor=""#ffffff"">" & lblRate02.Text & "</td>"
            Bdy = Bdy + "        <td width=""54px"" align=""center"" bgcolor=""#ffffff"">" & Trim(txtIV02.Text) & "</td>"
            Bdy = Bdy + "    </tr>"
        End If
        If txtSN03.Text <> "" Or lblSN03.Text <> "" Then
            Bdy = Bdy + "    <tr>"
            If txtSN03.Text <> "" Then
                Bdy = Bdy + "        <td width=""25%"" align=""center""  bgcolor=""#ffffff"">" & Trim(txtSN03.Text) & "</td>"
            ElseIf lblSN03.Text <> "" Then
                Bdy = Bdy + "        <td width=""25%"" align=""center""  bgcolor=""#ffffff"">" & lblSN03.Text & "</td>"
            End If
            Bdy = Bdy + "        <td width=""54px"" align=""center"" bgcolor=""#ffffff"">" & lblRate03.Text & "</td>"
            Bdy = Bdy + "        <td width=""54px"" align=""center"" bgcolor=""#ffffff"">" & Trim(txtIV03.Text) & "</td>"
            Bdy = Bdy + "    </tr>"
        End If
        If txtSN04.Text <> "" Or lblSN04.Text <> "" Then
            Bdy = Bdy + "    <tr>"
            If txtSN04.Text <> "" Then
                Bdy = Bdy + "        <td width=""25%"" align=""center""  bgcolor=""#ffffff"">" & Trim(txtSN04.Text) & "</td>"
            ElseIf lblSN04.Text <> "" Then
                Bdy = Bdy + "        <td width=""25%"" align=""center""  bgcolor=""#ffffff"">" & lblSN04.Text & "</td>"
            End If
            Bdy = Bdy + "        <td width=""54px"" align=""center"" bgcolor=""#ffffff"">" & lblRate04.Text & "</td>"
            Bdy = Bdy + "        <td width=""54px"" align=""center"" bgcolor=""#ffffff"">" & Trim(txtIV04.Text) & "</td>"
            Bdy = Bdy + "    </tr>"
        End If
        If txtSN05.Text <> "" Or lblSN05.Text <> "" Then
            Bdy = Bdy + "    <tr>"
            If txtSN05.Text <> "" Then
                Bdy = Bdy + "        <td width=""25%"" align=""center""  bgcolor=""#ffffff"">" & Trim(txtSN05.Text) & "</td>"
            ElseIf lblSN05.Text <> "" Then
                Bdy = Bdy + "        <td width=""25%"" align=""center""  bgcolor=""#ffffff"">" & lblSN05.Text & "</td>"
            End If
            Bdy = Bdy + "        <td width=""54px"" align=""center"" bgcolor=""#ffffff"">" & lblRate05.Text & "</td>"
            Bdy = Bdy + "        <td width=""54px"" align=""center"" bgcolor=""#ffffff"">" & Trim(txtIV05.Text) & "</td>"
            Bdy = Bdy + "    </tr>"
        End If
        If txtSN06.Text <> "" Or lblSN06.Text <> "" Then
            Bdy = Bdy + "    <tr>"
            If txtSN06.Text <> "" Then
                Bdy = Bdy + "        <td width=""25%"" align=""center""  bgcolor=""#ffffff"">" & Trim(txtSN06.Text) & "</td>"
            ElseIf lblSN06.Text <> "" Then
                Bdy = Bdy + "        <td width=""25%"" align=""center""  bgcolor=""#ffffff"">" & lblSN06.Text & "</td>"
            End If
            Bdy = Bdy + "        <td width=""54px"" align=""center"" bgcolor=""#ffffff"">" & lblRate06.Text & "</td>"
            Bdy = Bdy + "        <td width=""54px"" align=""center"" bgcolor=""#ffffff"">" & Trim(txtIV06.Text) & "</td>"
            Bdy = Bdy + "    </tr>"
        End If
        If txtSN07.Text <> "" Or lblSN07.Text <> "" Then
            Bdy = Bdy + "    <tr>"
            If txtSN07.Text <> "" Then
                Bdy = Bdy + "        <td width=""25%"" align=""center""  bgcolor=""#ffffff"">" & Trim(txtSN07.Text) & "</td>"
            ElseIf lblSN07.Text <> "" Then
                Bdy = Bdy + "        <td width=""25%"" align=""center""  bgcolor=""#ffffff"">" & lblSN07.Text & "</td>"
            End If
            Bdy = Bdy + "        <td width=""54px"" align=""center"" bgcolor=""#ffffff"">" & lblRate07.Text & "</td>"
            Bdy = Bdy + "        <td width=""54px"" align=""center"" bgcolor=""#ffffff"">" & Trim(txtIV07.Text) & "</td>"
            Bdy = Bdy + "    </tr>"
        End If
        Bdy = Bdy + "</table><br/>"
    End Sub


End Class
