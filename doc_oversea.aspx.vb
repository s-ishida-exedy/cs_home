Imports System.Data.SqlClient
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
        strSQL = strSQL & "WHERE PLACE IN ('1','9') ORDER BY CODE "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        '結果を取り出す 
        While (dataread.Read())
            If dataread("PLACE") = "9" Then     'サーバーパス
                strMainPath = dataread("MANUAL_FILE")
            ElseIf dataread("PLACE") = "1" Then '各ファイル
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

        ''ID（ファイル名）を引数にファイルオープン処理
        'Dim p As New System.Diagnostics.Process
        'p.StartInfo.FileName = strMainPath & lnkbutton.ID   'フルパス指定
        'p.Start()

        'WEBサーバーの「\\k3hwpm01\exp\cs_home\manual」に配置されたファイルのみ起動可能
        Response.Redirect("./manual/" & lnkbutton.ID)       'フルパス指定

    End Sub

End Class
