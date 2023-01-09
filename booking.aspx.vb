Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing

Imports System.Data.Common
Imports System.Console
Imports ClosedXML.Excel
Imports System.IO
Imports System.Linq

Partial Class cs_home
    Inherits System.Web.UI.Page

    Public strRow As String
    Public strProcess As String
    Public strPath As String = "C:\exp\cs_home\files"

    Private Sub GridView1_RowCreated2(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound


        'ヘッダー以外に処理
        If e.Row.RowType = DataControlRowType.DataRow Then

            'e.Row.Cells(10).Visible = False
            If e.Row.Cells(18).Text = "" Then
            Else
                Dim s As Integer = Left(e.Row.Cells(18).Text, Len(e.Row.Cells(18).Text) - 1)
                e.Row.Cells(0).BackColor = ColorTranslator.FromWin32(s)

                If Right(e.Row.Cells(18).Text, 1) = "●" Then
                    'e.Row.BorderStyle = BorderStyle.Dashed
                    e.Row.Font.Strikeout = True
                End If

            End If

        End If
        e.Row.Cells(18).Visible = False

    End Sub


    Private Sub GridView1_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowCreated

        'コード列非表示処理
        If e.Row.RowType = DataControlRowType.DataRow OrElse e.Row.RowType = DataControlRowType.Header Then
            'e.Row.Cells(10).Visible = False

        End If




        Dim row As GridViewRow = e.Row

        ' データ行である場合に、onmouseover／onmouseout属性を追加（1）
        If row.RowType = DataControlRowType.DataRow Then





            ' onmouseover属性を設定
            row.Attributes("onmouseover") = "setBg(this, '#CC99FF')"

                ' データ行が通常行／代替行であるかで処理を分岐（2）
                If row.RowState = DataControlRowState.Normal Then
                row.Attributes("onmouseout") =
                  String.Format("setBg(this, '{0}')",
                    ColorTranslator.ToHtml(GridView1.RowStyle.BackColor))
            Else
                row.Attributes("onmouseout") =
                  String.Format("setBg(this, '{0}')",
                    ColorTranslator.ToHtml(
                      GridView1.AlternatingRowStyle.BackColor))
            End If


        End If

    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged
        Dim sch_ID As String = GridView1.SelectedValue.ToString
        '選択された行のSCH_ID
        strRow = sch_ID
    End Sub

    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load
        If IsPostBack Then
            ' そうでない時処理
        Else
            Me.DropDownList1.Items.Insert(0, "-ｽﾃｰﾀｽ-") '先頭に空白行追加
            Me.DropDownList2.Items.Insert(0, "-海貨業者-") '先頭に空白行追加
        End If

        Me.Label2.Text = ""

        '最終更新年月日取得
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String
        Dim strDate As String

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()

        strSQL = "SELECT DATA_UPD FROM T_EXL_DATA_UPD WHERE DATA_CD = '008'"
        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        strDate = ""
        '結果を取り出す 
        While (dataread.Read())
            strDate += dataread("DATA_UPD")
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()

        '最終更新年月日を表示
        Me.Label2.Text = strDate & " 更新"

    End Sub

    Private Sub Make_Grid()
        'GRIDを作成する。

        Dim Dataobj As New DBAccess
        Dim strSts As String = DropDownList1.SelectedValue
        Dim strFwd As String = DropDownList2.SelectedValue

        If strSts = "-ｽﾃｰﾀｽ-" Then
            strSts = ""
        End If
        If strFwd = "-海貨業者-" Then
            strFwd = ""
        End If

        'データの取得
        Dim ds As DataSet = Dataobj.GET_BOOKING_DATA(strSts, strFwd, Trim(Me.TextBox1.Text), Trim(Me.TextBox2.Text))
        If ds.Tables.Count > 0 Then
            GridView1.DataSourceID = ""
            GridView1.DataSource = ds
            GridView1.DataBind()
        End If
    End Sub

    Private Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList1.SelectedIndexChanged
        'ステータス選択
        Call Make_Grid()
    End Sub

    Private Sub DropDownList2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList2.SelectedIndexChanged
        '海貨業者選択
        Call Make_Grid()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        '絞込ボタン押下
        Call Make_Grid()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'リセットボタン押下
        DropDownList1.SelectedIndex = 0
        DropDownList2.SelectedIndex = 0
        TextBox1.Text = ""
        TextBox2.Text = ""

        Make_Grid()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click




        '前月分ダウンロードボタン押下
        Dim strFile As String = Format(Now, "yyyyMMdd") & "_BOOKING.xlsx"
        Dim strPath As String = "C:\exp\cs_home\files\"
        Dim strChanged As String    'サーバー上のフルパス
        Dim strFileNm As String     'ファイル名

        Dim dtToday As DateTime = DateTime.Today

        Dim strFile0 As String = ""
        'ファイル検索
        strFile0 = Dir(strPath & "*BOOKING.xlsx")
        Do While strFile0 <> ""

            If strFile0 = Format(Now, "yyyyMMdd") & "_BOOKING.xlsx" Then
            Else
                System.IO.File.Delete(strPath & strFile0)
            End If

            strFile0 = Dir()
        Loop

        Dim dt = GetNorthwindProductTable()

        Dim a

        Dim dt2 As New DataTable("BOOKINGSHEET")

        For Each Col As DataColumn In dt.Columns
            dt2.Columns.Add(Col.ColumnName)
        Next




        For Each row As DataRow In dt.Rows
            dt2.Rows.Add()
            For i As Integer = 0 To dt.Columns.Count - 1
                a = row(i)
                'If a = "" Then
                '    a = DBNull.Value
                'End If
                dt2.Rows(dt2.Rows.Count - 1)(i) = a
            Next
        Next





        Dim workbook = New XLWorkbook()
        Dim worksheet = workbook.Worksheets.Add(dt2)

        worksheet.Style.Font.FontName = "Meiryo UI"
        worksheet.Style.Alignment.WrapText = False
        worksheet.Columns.AdjustToContents()
        worksheet.SheetView.FreezeRows(1)

        'Dim i As Long
        'i = 1
        'j = 1
        'Do Until worksheet.Cell(i, j).text = ""
        '    worksheet.Cell(i, j).Value


        '    i = i + 1
        'Loop



        'For i As Integer = 0 To dt2.Columns.Count() - 1
        For j As Integer = 1 To dt2.Rows.Count()
            Dim s As String = Left(dt2.Rows(j - 1)(49), Len(dt2.Rows(j - 1)(49)) - 1)
            Dim l As String = Right(dt2.Rows(j - 1)(49), 1)
            Dim color1 As String = ColorTranslator.FromWin32(s).ToArgb
            Dim cell = worksheet.Cell(j + 1, 1)
            Dim cell2 = worksheet.Cell(j + 1, 50)
            Dim cell3 = worksheet.Range(cell, cell2)


            cell3.Style.Fill.BackgroundColor = XLColor.FromArgb(color1)

            If l = "●" Then
                cell3.Style.Font.SetStrikethrough(True)
            End If

        Next
        'Next



        workbook.SaveAs(strPath & strFile)


        'ファイル名を取得する
        Dim strTxtFiles() As String = IO.Directory.GetFiles(strPath, Format(Now, "yyyyMMdd") & "_BOOKING.xlsx")

        strChanged = strTxtFiles(0)
        strFileNm = Path.GetFileName(strChanged)

        'Contentをクリア
        Response.ClearContent()

        'Contentを設定
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("shift-jis")
        Response.ContentType = "application/vnd.ms-excel"

        '表示ファイル名を指定
        Dim fn As String = HttpUtility.UrlEncode(strFileNm)
        Response.AddHeader("Content-Disposition", "attachment;filename=" + fn)

        'ダウンロード対象ファイルを指定
        Response.WriteFile(strChanged)

        'ダウンロード実行
        Response.Flush()
        Response.End()


    End Sub
    Private Shared Function GetNorthwindProductTable() As DataTable
        'EXCELファイル出力
        Dim strSQL As String = ""
        Dim strSDate As String = ""
        Dim strEDate As String = ""

        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"

        Dim dt = New DataTable("T_BOOKING")

        Using conn = New SqlConnection(ConnectionString)
            Dim cmd = conn.CreateCommand()

            strSQL = strSQL & "Select T_BOOKING.STATUS, T_BOOKING.Forwarder, T_BOOKING.SEQ_NO01, T_BOOKING.SEQ_NO02, T_BOOKING.CUST_CD, T_BOOKING.CONSIGNEE, T_BOOKING.DESTINATION, T_BOOKING.INVOICE_NO, T_BOOKING.OFFICIAL_QUOT, T_BOOKING.CUT_DATE, T_BOOKING.ETD, T_BOOKING.ETA, T_BOOKING.TWENTY_FEET, T_BOOKING.FOURTY_FEET, T_BOOKING.LCL_QTY, T_BOOKING.DAY01, T_BOOKING.PACKAGE01, T_BOOKING.DAY02, T_BOOKING.PACKAGE02, T_BOOKING.DAY03, T_BOOKING.PACKAGE03, T_BOOKING.DAY04, T_BOOKING.PACKAGE04, T_BOOKING.DAY05, T_BOOKING.PACKAGE05, T_BOOKING.DAY06, T_BOOKING.PACKAGE06, T_BOOKING.DAY07, T_BOOKING.PACKAGE07, T_BOOKING.DAY08, T_BOOKING.PACKAGE08, T_BOOKING.DAY09, T_BOOKING.PACKAGE09, T_BOOKING.DAY10, T_BOOKING.PACKAGE10, T_BOOKING.DAY11, T_BOOKING.PACKAGE11, T_BOOKING.BOOKING_NO, T_BOOKING.BOOK_TO, T_BOOKING.VESSEL_NAME, T_BOOKING.VOYAGE_NO, T_BOOKING.PLACE_OF_RECEIPT, T_BOOKING.LOADING_PORT, T_BOOKING.DISCHARGING_PORT, T_BOOKING.PLACE_OF_DELIVERY, T_BOOKING.ETA_AFTER_TS, T_BOOKING.REMARKS, T_BOOKING.PODATE, T_BOOKING.PONO,T_BOOKING.ROW_KBN From T_BOOKING "

            cmd.CommandText = strSQL
            Dim sda = New SqlDataAdapter(cmd)
            sda.Fill(dt)
        End Using

        Return dt
    End Function
End Class
