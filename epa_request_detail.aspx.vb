Imports System.Data.SqlClient
Imports mod_function

Partial Class cs_home
    Inherits System.Web.UI.Page

    Public strRow As String
    Public strProcess As String

    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load
        Dim strEtd As String = ""
        Dim strIvno As String = ""
        Dim strCust As String = ""
        Dim strSQL As String = ""
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strValue As String = ""
        Dim strStatus As String = ""
        Dim strVal As String = ""

        Label3.Text = ""

        If IsPostBack Then
            ' そうでない時処理
        Else
            'パラメータ取得
            strEtd = Session("strEtd")
            strIvno = Session("strIvno")
            strCust = Session("strCust")
            strStatus = Session("strStatus")

            Select Case strStatus
                Case "未"
                    strVal = "01"
                Case "済"
                    strVal = "02"
                Case "対象ﾅｼ"
                    strVal = "03"
                Case "ｷｬﾝｾﾙ"
                    strVal = "04"
                Case "再発給"
                    strVal = "09"
            End Select

            'T_EXL_EPA_KANRIからデータ取得
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
            strSQL = strSQL & "    STATUS "
            strSQL = strSQL & "  , BLDATE "
            strSQL = strSQL & "  , INV "
            strSQL = strSQL & "  , CUSTNAME "
            strSQL = strSQL & "  , CUSTCODE "
            strSQL = strSQL & "  , CASE SALESFLG "
            strSQL = strSQL & "    		WHEN '1' THEN '済' "
            strSQL = strSQL & "    		ELSE '' "
            strSQL = strSQL & "    End As SALESFLG "
            strSQL = strSQL & "  , SHIPPEDPER "
            strSQL = strSQL & "  , ETA "
            strSQL = strSQL & "  , CUTDATE "
            strSQL = strSQL & "  , VOYAGENO "
            strSQL = strSQL & "  , IsNull(RECEIPT_NUMBER,'') AS RECEIPT_NUMBER "
            strSQL = strSQL & "  , IsNull(IVNO_FULL,'') AS IVNO_FULL "
            strSQL = strSQL & "  , IsNull(APPLICATION_DATE,'') AS APPLICATION_DATE "
            strSQL = strSQL & "  , IsNull(SENDING_REQ_DATE,'') AS SENDING_REQ_DATE "
            strSQL = strSQL & "  , IsNull(RECEIPT_DATE,'') AS RECEIPT_DATE "
            strSQL = strSQL & "  , IsNull(EPA_SEND_DATE,'') AS EPA_SEND_DATE "
            strSQL = strSQL & "  , IsNull(TRK_NO,'') AS TRK_NO "
            strSQL = strSQL & "FROM T_EXL_EPA_KANRI "
            strSQL = strSQL & "WHERE BLDATE = '" & strEtd & "' "
            strSQL = strSQL & "AND   INV = '" & strIvno & "' "
            strSQL = strSQL & "AND   CUSTCODE = '" & strCust & "' "
            strSQL = strSQL & "AND   STATUS = '" & strVal & "' "

            'ＳＱＬコマンド作成 
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行 
            dataread = dbcmd.ExecuteReader()

            '結果を取り出す 
            While (dataread.Read())
                strValue = dataread("STATUS")
                DropDownList1.SelectedValue = strValue
                Label1.Text = dataread("BLDATE")
                Label2.Text = dataread("INV")
                Label4.Text = dataread("CUSTNAME")
                Label5.Text = dataread("CUSTCODE")
                Label6.Text = dataread("SALESFLG")
                TextBox1.Text = dataread("SHIPPEDPER")
                TextBox2.Text = dataread("ETA")
                TextBox3.Text = dataread("CUTDATE")
                TextBox4.Text = dataread("VOYAGENO")
                TextBox5.Text = dataread("RECEIPT_NUMBER")
                TextBox6.Text = dataread("IVNO_FULL")
                TextBox7.Text = dataread("APPLICATION_DATE")
                TextBox8.Text = dataread("SENDING_REQ_DATE")
                TextBox9.Text = dataread("RECEIPT_DATE")
                TextBox10.Text = dataread("EPA_SEND_DATE")
                TextBox11.Text = dataread("TRK_NO")
            End While

            'クローズ処理 
            dataread.Close()
            dbcmd.Dispose()
            cnn.Close()
            cnn.Dispose()
        End If

        Button7.Attributes.Add("onclick", "return confirm('更新します。よろしいですか？');")
        Button8.Attributes.Add("onclick", "return confirm('削除します。よろしいですか？');")
    End Sub

    Private Sub DB_access(strMode As String)
        '画面入力情報をテーブルへ登録
        Dim strSQL As String
        Dim dbcmd As SqlCommand
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
        Dim strEtd = Session("strEtd")
        Dim strIv = Session("strIvno")
        Dim strCust = Session("strCust")
        Dim strStatus = Session("strStatus")


        Select Case strStatus
            Case "未"
                strVal = "01"
            Case "済"
                strVal = "02"
            Case "対象ﾅｼ"
                strVal = "03"
            Case "ｷｬﾝｾﾙ"
                strVal = "04"
            Case "再発給"
                strVal = "09"
        End Select

        If strMode = "01" Then
            'ステータスのドロップダウン
            Dim strDropDownList As String = DropDownList1.SelectedValue

            '画面入力情報を変数に代入
            Dim strSenmei As String = TextBox1.Text
            Dim strEta As String = TextBox2.Text
            Dim strCut As String = TextBox3.Text
            Dim strVoy As String = TextBox4.Text
            Dim strUketsuke As String = TextBox5.Text
            Dim strIvno As String = TextBox6.Text
            Dim strShinsei As String = TextBox7.Text
            Dim strSoufu As String = TextBox8.Text
            Dim strJuryo As String = TextBox9.Text
            Dim strSend As String = TextBox10.Text
            Dim strTrkno As String = TextBox11.Text

            'データ更新
            strSQL = ""
            strSQL = strSQL & "UPDATE T_EXL_EPA_KANRI SET"
            strSQL = strSQL & " STATUS = '" & strDropDownList & "' "
            strSQL = strSQL & ",SHIPPEDPER = '" & strSenmei & "' "
            strSQL = strSQL & ",ETA = '" & strEta & "' "
            strSQL = strSQL & ",CUTDATE = '" & strCut & "' "
            strSQL = strSQL & ",VOYAGENO = '" & strVoy & "' "
            strSQL = strSQL & ",RECEIPT_NUMBER = '" & strUketsuke & "' "
            strSQL = strSQL & ",IVNO_FULL = '" & strIvno & "' "
            strSQL = strSQL & ",APPLICATION_DATE = '" & strShinsei & "' "
            strSQL = strSQL & ",SENDING_REQ_DATE = '" & strSoufu & "' "
            strSQL = strSQL & ",RECEIPT_DATE = '" & strJuryo & "' "
            strSQL = strSQL & ",EPA_SEND_DATE = '" & strSend & "' "
            strSQL = strSQL & ",TRK_NO = '" & strTrkno & "' "
            strSQL = strSQL & "WHERE STATUS = '" & strVal & "' "
            strSQL = strSQL & "AND   INV = '" & strIv & "' "
            strSQL = strSQL & "AND   CUSTCODE = '" & strCust & "' "
            strSQL = strSQL & "AND   BLDATE = '" & strEtd & "' "

            Command.CommandText = strSQL
            ' SQLの実行
            Command.ExecuteNonQuery()

            cnn.Close()
        Else
            'データ削除
            strSQL = ""
            strSQL = strSQL & "DELETE FROM T_EXL_EPA_KANRI "
            strSQL = strSQL & "WHERE STATUS = '" & strVal & "' "
            strSQL = strSQL & "AND   INV = '" & strIv & "' "
            strSQL = strSQL & "AND   CUSTCODE = '" & strCust & "' "
            strSQL = strSQL & "AND   BLDATE = '" & strEtd & "' "

            Command.CommandText = strSQL
            ' SQLの実行
            Command.ExecuteNonQuery()

            cnn.Close()

        End If

    End Sub

    Private Function chk_Nyuryoku() As Boolean
        chk_Nyuryoku = True

        '全角入力チェック
        If HankakuEisuChk(TextBox1.Text) = False And Trim(TextBox1.Text) <> "" Then
            Label3.Text = "船名に全角文字が使用されています。"
            chk_Nyuryoku = False
        End If
        If HankakuEisuChk(TextBox4.Text) = False And Trim(TextBox4.Text) <> "" Then
            Label3.Text = "VoyNOに全角文字が使用されています。"
            chk_Nyuryoku = False
        End If
        If HankakuEisuChk(TextBox5.Text) = False And Trim(TextBox5.Text) <> "" Then
            Label3.Text = "受付番号に全角文字が使用されています。"
            chk_Nyuryoku = False
        End If
        If HankakuEisuChk(TextBox6.Text) = False And Trim(TextBox6.Text) <> "" Then
            Label3.Text = "IVNO(Full)に全角文字が使用されています。"
            chk_Nyuryoku = False
        End If
        If HankakuEisuChk(TextBox11.Text) = False And Trim(TextBox11.Text) <> "" Then
            Label3.Text = "TRK#に全角文字が使用されています。"
            chk_Nyuryoku = False
        End If

        '日付入力チェック
        If Chk_Hiduke(TextBox2.Text) = False And Trim(TextBox2.Text) <> "" Then
            Label3.Text = "ETAの日付形式が間違っています。"
            chk_Nyuryoku = False
        End If
        If Chk_Hiduke(TextBox3.Text) = False And Trim(TextBox3.Text) <> "" Then
            Label3.Text = "カット日の日付形式が間違っています。"
            chk_Nyuryoku = False
        End If
        If Chk_Hiduke(TextBox7.Text) = False And Trim(TextBox7.Text) <> "" Then
            Label3.Text = "申請日の日付形式が間違っています。"
            chk_Nyuryoku = False
        End If
        If Chk_Hiduke(TextBox8.Text) = False And Trim(TextBox8.Text) <> "" Then
            Label3.Text = "送付依頼日の日付形式が間違っています。"
            chk_Nyuryoku = False
        End If
        If Chk_Hiduke(TextBox9.Text) = False And Trim(TextBox9.Text) <> "" Then
            Label3.Text = "受領日の日付形式が間違っています。"
            chk_Nyuryoku = False
        End If
        If Chk_Hiduke(TextBox10.Text) = False And Trim(TextBox10.Text) <> "" Then
            Label3.Text = "EPA送付日の日付形式が間違っています。"
            chk_Nyuryoku = False
        End If
    End Function

    Protected Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        '更新ボタンクリックイベント

        '入力チェック
        If chk_Nyuryoku() = False Then
            Return 
        End If

        '更新
        Call DB_access("01")        '更新モード

        '元の画面に戻る
        Response.Redirect("epa_request.aspx")
    End Sub
    Protected Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        '削除ボタン押下

        '削除
        Call DB_access("02")        '削除モード

        '元の画面に戻る
        Response.Redirect("epa_request.aspx")
    End Sub
End Class

