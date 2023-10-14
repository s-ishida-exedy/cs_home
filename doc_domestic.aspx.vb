Imports System.Data.SqlClient
Imports System.IO
Imports System.Console

Partial Class cs_home
    Inherits System.Web.UI.Page

    Public strMainPath As String = ""       'サーバーのパス用

    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load
        Dim TableRow As TableRow
        Dim TableCell As TableCell
        Dim Linkbtn As New LinkButton

        '手順書マスタからデータ取得し、その数の<TD>を作成する。
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim intCnt As Integer = 1
        Dim intAll As Integer = 0

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()

        strSQL = strSQL & "SELECT CODE, MANUAL_NM, MANUAL_FILE, PLACE FROM M_EXL_MANUAL_CS "
        strSQL = strSQL & "WHERE PLACE IN ('0','9') ORDER BY CODE "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        '結果を取り出す 
        While (dataread.Read())
            If dataread("PLACE") = "9" Then     'サーバーパス
                strMainPath = dataread("MANUAL_FILE")
            ElseIf dataread("PLACE") = "0" Then '各ファイル
                If intCnt = 1 Or intCnt Mod 3 = 1 Then
                    '次の行を追加
                    TableRow = New TableRow()
                End If

                TableCell = New TableCell()
                TableRow.Cells.Add(TableCell)
                Linkbtn = New LinkButton
                Linkbtn.Text = dataread("MANUAL_NM")
                Linkbtn.ID = dataread("MANUAL_FILE")
                AddHandler Linkbtn.Click, AddressOf LBtn_Click
                TableCell.Controls.Add(Linkbtn)

                '３列ごとに行追加
                If intCnt Mod 3 = 0 Then
                    '次の行を追加
                    Table1.Rows.Add(TableRow)
                End If
                intCnt += 1
            End If
        End While

        '３列に収まらなかった場合、残りの列を追加
        If intCnt - 1 Mod 3 <> 0 Then
            Table1.Rows.Add(TableRow)
        End If

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()
    End Sub

    Private Sub LBtn_Click(sender As Object, e As EventArgs)
        'クリックされたリンクボタンを取得
        Dim lnkbutton = CType(sender, LinkButton)

        'WEBサーバーの「\\k3hwpm01\exp\cs_home\manual」に配置されたファイルのみ起動可能
        Response.Redirect("./manual/" & lnkbutton.ID)       'フルパス指定

        'ID（ファイル名）を引数にファイルオープン処理
        'Dim p As New System.Diagnostics.Process
        'p.StartInfo.FileName = strMainPath & lnkbutton.ID   'フルパス指定
        'p.Start()

        'Dim attributes = File.GetAttributes(strMainPath & lnkbutton.ID)

        'File.SetAttributes(strMainPath & lnkbutton.ID, attributes Or FileAttributes.ReadOnly)

        'System.Diagnostics.Process.Start(strMainPath & lnkbutton.ID)

        'File.SetAttributes(strMainPath & lnkbutton.ID, attributes)

        'Response.Redirect("./manual/手順書_【国内】A042」LS1 帳票出力.xlsx")


        'Dim workbook
        '        Dim ExcelFilePath As String = "\\svnas201\exd06100\COMMON\生産管理本部\ＣＳチーム\手順書\新規_手順書\2019年度手順書整備\02.承認待ち\手順書_【国内】A042」 LS1集荷指示書出力.xlsx"
        'Dim ExcelFilePath As String = "C:\temp\Book1.xlsx"
        'Dim ExcelFilePath As String = "\\KBHWPA83\InvPack送信前フォルダ\Book1.xlsx"

        'Dim workbook = New XLWorkbook(ExcelFilePath)
        '' Excelファイルを開く
        ''Using workbook As New ClosedXML.Excel.XLWorkbook(ExcelFilePath)
        ''End Using
        'workbook.Worksheet(1).Range("A1").Value = "wsssss"
        'workbook.SaveAs(ExcelFilePath)
        '        Dim workbook = New XLWorkbook

        'ワークブックを保存するには最低１つのシートが必要
        '        workbook.AddWorksheet("Sheet1")
        'workbook.Worksheet(1).Range("B1").Value = "test"
        'workbook.SaveAs(ExcelFilePath)
        '    Dim file As String = ExcelFilePath
        '    Using fs As New FileStream(
        'file,
        'FileMode.Open,
        'FileAccess.Read,
        'FileShare.ReadWrite)
        '        Dim book As New ClosedXML.Excel.XLWorkbook(fs, ClosedXML.Excel.XLEventTracking.Disabled)
        '        Dim sheet As ClosedXML.Excel.IXLWorksheet
        '        ' 開くシート名を指定する
        '        sheet = book.Worksheet("sheet1")
        '        ' セルの位置を指定する(左右方向がx,上下方向がy)
        '        Dim x As Integer = 1
        '        Dim y As Integer = 1
        '        ' セルの値をRead
        '        Dim read_data As String = sheet.Cell(y, x).Value.ToString
        '    End Using

        'Dim excelApp As Excel.Application = Nothing
        'Dim wkbk As Excel.Workbook
        'Dim sheet As Excel.Worksheet
        'Dim filePath As String = "\\KBHWPA83\InvPack送信前フォルダ\Book1.xlsx"

        'Try
        '    ' Start Excel and create a workbook and worksheet.
        '    excelApp = New Excel.Application
        '    wkbk = excelApp.Workbooks.Open(filePath)
        '    excelApp.Visible = True
        'Catch

        'Finally
        '    sheet = Nothing
        '    wkbk = Nothing

        '    ' Close Excel.
        '    'excelApp.Quit()
        '    excelApp = Nothing
        'End Try

    End Sub

End Class
