Imports System.Data.SqlClient
Imports System.Data
Imports ClosedXML.Excel
Imports System.Diagnostics


Partial Class cs_home
    Inherits System.Web.UI.Page

    'Public Const strChkPath As String = "C:\受注処理\受注処理ツール\自動化\"
    Public Const strChkPath As String = "\\svnas201\EXD06101\DISC_COMMON\オーダー受注処理\受注処理CK\"
    'Public Const strSavePath As String = "C:\exp\cs_home\files\"
    Public Const strSavePath As String = "\\svnas201\EXD06101\DISC_COMMON\CheckSheet海外受注処理シート\"

    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load

        If IsPostBack Then
            ' そうでない時処理
        Else
            '初期化
            TextBox1.Text = ""
        End If

        Button1.Attributes.Add("onclick", "return confirm('受注処理チェックします。よろしいですか？');")

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'チェック実行ボタン

        '選択された協定を取得
        Dim strDate As String = ""

        'エラーメッセージのラベルを初期化
        Label1.Text = ""

        If TextBox1.Text = "" Then
            Label1.Text = "インボイス番号が入力されていません。"
            Return
        Else
            strDate = Trim(TextBox1.Text)       '処理対象日
        End If

        'テンプレートファイルを開く
        'chk_yyyymmdd.xlsxのフルパスを指定
        Dim wb0 As New XLWorkbook(strSavePath & "chk_yyyymmdd.xlsx")
        Dim ws1 As IXLWorksheet = wb0.Worksheet("日毎処理履歴 ")

        'ヘッダ情報をセット
        ws1.Cell("B4").Value = Trim(TextBox1.Text)      '処理日
        ws1.Cell("M4").Value = Session("UsrId")         '確定者

        '明細情報を取得
        Dim dt = GetExcelData(Trim(TextBox1.Text))

        'データを貼り付ける
        ws1.Cell("A6").InsertTable(dt)

        'LS-2CHECKYYYYMMDD.xlsmを選択する
        Dim strFile As String = ""
        Dim posted As HttpPostedFile = File1.PostedFile

        If Not posted.FileName = "" Then
            'LS-2CHECKYYYYMMDD.xlsmのフルパスを変数に格納
            strFile = strChkPath & System.IO.Path.GetFileName(posted.FileName)
        Else
            Return
        End If

        'LS-2CHECKYYYYMMDD.xlsを開く
        Dim wb1 As New XLWorkbook(strFile)
        Dim ws2 As IXLWorksheet = wb1.Worksheet("data")

        '日毎処理履歴シートのPONOを上から順に、LS-2CHECKYYYYMMDD.xlsに存在するかチェックする。
        Dim i As Integer = 7
        Dim intRst As Integer = 0
        Dim blnJdg As Boolean = True
        'シートの最下行を取得
        Dim lastRow = ws1.LastRowUsed().RowNumber()

        For i = 7 To lastRow
            Dim resultCells As IXLCells = ws2.Search(Trim(ws1.Cell(i, 13).Value))
            For Each resultCell In resultCells
                '該当セルの行番号を返却
                intRst = resultCell.Address.RowNumber
            Next
            '対象が無ければフラグをFalseにする
            If intRst = 0 Then
                blnJdg = False
            Else
                '存在すればLS-2CHECKYYYYMMDD.xlsのPONOを日毎処理履歴シートにセット
                ws1.Cell(i, 17).Value = ws2.Cell(intRst, 7).Value
            End If
        Next

        'ファイル名、フォルダ名生成
        Dim dt1 As DateTime = DateTime.Now
        Dim strFileNM As String = "chk_" & Replace(strDate, "/", "") & ".xlsx"
        Dim strFldrNM As String = dt1.ToString("yyyy") & "年" & "\" & dt1.ToString("yyyyMM")

        '出力(chk_yyyymmdd.xlsxの保存)
        wb0.SaveAs(strSavePath & "\" & strFldrNM & "\" & strFileNM)

        '初期化
        TextBox1.Text = ""

        If blnJdg = False Then
            '完了メッセージ（エラーありの場合）
            Label1.Text = "処理は完了しましたが、エラーがあります。要確認！"
        Else
            '完了メッセージ（エラーなしの場合）
            Label1.Text = "処理が完了しました。"
        End If

    End Sub

    Private Shared Function GetExcelData(strDate As String) As DataTable
        'DataTableにEXCEL出力する内容を格納する

        Dim strSQL As String = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"

        Try
            Dim dt = New DataTable("INVHDSHEET")

            Using conn = New SqlConnection(ConnectionString)
                Dim cmd = conn.CreateCommand()

                strSQL = strSQL & "SELECT  "
                strSQL = strSQL & "    '○' AS '確定' "
                strSQL = strSQL & "  , '○' AS '一括出力' "
                strSQL = strSQL & "  , '○' AS 'SN送付' "
                strSQL = strSQL & "  , CUST_CD AS '新コード' "
                strSQL = strSQL & "  , CUST_CD_OLD AS '旧コード' "
                strSQL = strSQL & "  , CUST_NM AS '客先名' "
                strSQL = strSQL & "  , FRT AS 'POフォーマット' "
                strSQL = strSQL & "  , ETD "
                strSQL = strSQL & "  , LT_CONFIRM AS LT "
                strSQL = strSQL & "  , SAVE_DATA AS '保存' "
                strSQL = strSQL & "  , ERR "
                strSQL = strSQL & "  , KAKUTEISHA AS '確定者' "
                strSQL = strSQL & "  , PONO "
                strSQL = strSQL & "  , QTY "
                strSQL = strSQL & "  , MAKE_DATE AS '作成日' "
                strSQL = strSQL & "FROM  "
                strSQL = strSQL & "  T_EXL_ODR_LIST a   "
                strSQL = strSQL & "WHERE  "
                strSQL = strSQL & "  a.EXEC_DATE = '" & strDate & "' "

                cmd.CommandText = strSQL
                Dim sda = New SqlDataAdapter(cmd)
                sda.Fill(dt)
            End Using

            Return dt
        Catch ex As Exception
            Debug.Write(ex.Message)
        End Try

        Return Nothing
    End Function

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'クリアボタン
        TextBox1.Text = ""
        Label1.Text = ""
    End Sub
End Class
