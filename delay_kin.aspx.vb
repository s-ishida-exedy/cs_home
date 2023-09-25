Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing

Imports System.Data.Common
Imports System.Console
Imports ClosedXML.Excel
Imports System.IO
Imports System.Linq
Imports Oracle.DataAccess.Client

Imports mod_function

Partial Class cs_home
    Inherits System.Web.UI.Page

    Public strRow As String
    Public strProcess As String
    Public strPath As String = "C:\exp\cs_home\files"

    Private Sub GridView1_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound

        '最終更新年月日取得
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strinv As String = ""
        Dim strbkg As String = ""
        Dim strcname As String = ""
        Dim strddate As String = ""
        Dim strmax As String = ""

        Dim cnum01 As Long = 0
        Dim cnum02 As Long = 0
        Dim cnum03 As Long = 0

        Dim wday As String = ""
        Dim wday2 As String = ""
        Dim wday3 As String = ""
        Dim dt1 As DateTime = DateTime.Now
        Dim Kaika00 As String = ""

        Dim ts1 As New TimeSpan(150, 0, 0, 0)
        Dim ts2 As New TimeSpan(80, 0, 0, 0)
        Dim dt2 As DateTime = dt1 + ts1
        Dim dt3 As DateTime = dt1 - ts1
        Dim strfixetd03 As DateTime
        Dim strfixetd04 = New DateTime(dt1.Year, dt1.Month + 1, 1)

        If e.Row.RowType = DataControlRowType.DataRow Then


            strcname = GET_CUSTNAME(Trim(e.Row.Cells(0).Text))
            strmax = GET_MAXDATE(Trim(strcname))

            Dim strfixetd00 As DateTime = DateTime.Parse(e.Row.Cells(2).Text)
            If strmax < Format(dt3, "yyyy/MM") Then

                strddate = GET_DELATDATE(Trim(strcname), strmax)
                strfixetd03 = strfixetd00
            Else
                strddate = GET_DELATDATE(Trim(strcname), strmax)
                '遅延日数を日付けに加算

                Dim strfixetd01 As New TimeSpan(strddate, 0, 0, 0)
                strfixetd03 = strfixetd00 + strfixetd01

            End If

            If strfixetd03 < strfixetd04 Then
                e.Row.Visible = False
            Else


                'インボイス金額がない場合（書類作成未）
                If e.Row.Cells(8).Text = "&nbsp;" Or e.Row.Cells(8).Text = "" Then

                    If e.Row.Cells(6).Text Like "%M3%" Or IsNumeric(e.Row.Cells(6).Text) = True Then

                        'M3の処理

                    Else

                        If IsNumeric(e.Row.Cells(3).Text) = True Then
                            cnum01 = e.Row.Cells(3).Text
                        Else
                            cnum01 = 0
                        End If

                        If IsNumeric(e.Row.Cells(4).Text) = True Then
                            cnum02 = e.Row.Cells(4).Text
                        Else
                            cnum02 = 0
                        End If

                        If IsNumeric(e.Row.Cells(5).Text) = True Then
                            cnum03 = e.Row.Cells(5).Text
                        Else
                            cnum03 = 0
                        End If

                        e.Row.Cells(3).Text = cnum01 * 10000000 + cnum02 * 20000000 + cnum03 * 20000000
                        e.Row.Cells(4).Text = ""
                        e.Row.Cells(5).Text = ""


                    End If


                Else

                    e.Row.Cells(3).Text = GET_SNKIN(e.Row.Cells(8).Text)

                End If













































            End If
                e.Row.Cells(2).Text = strfixetd03
        End If




    End Sub

    Private Function GET_CUSTNAME(strcust As String) As String

        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String = ""
        Dim strinv As String = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=svdpo051;Initial Catalog=BPTB001;User Id=ado_bptb001;Password=ado_bptb001"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        Dim dt1 As DateTime = DateTime.Now

        Dim ts1 As New TimeSpan(100, 0, 0, 0)
        Dim ts2 As New TimeSpan(100, 0, 0, 0)
        Dim dt2 As DateTime = dt1 + ts1
        Dim dt3 As DateTime = dt1 - ts1

        'データベース接続を開く
        cnn.Open()

        strSQL = ""
        strSQL = strSQL & "SELECT * FROM M_CUST_TB "
        strSQL = strSQL & " WHERE CUSTCODE like '%" & strcust & "%' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        GET_CUSTNAME = ""
        '結果を取り出す 
        While (dataread.Read())
            GET_CUSTNAME = Convert.ToString(dataread("CUSTNAME"))
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()

    End Function
    Private Function GET_MAXDATE(strcust As String) As String

        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String = ""
        Dim strinv As String = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=KBHWPM02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        Dim dt1 As DateTime = DateTime.Now

        Dim ts1 As New TimeSpan(100, 0, 0, 0)
        Dim ts2 As New TimeSpan(100, 0, 0, 0)
        Dim dt2 As DateTime = dt1 + ts1
        Dim dt3 As DateTime = dt1 - ts1

        'データベース接続を開く
        cnn.Open()

        strSQL = ""
        strSQL = strSQL & "SELECT max(left(T_EXL_DELAYNOTICE.UPLOAD_DATE,7)) as ddd "
        strSQL = strSQL & "FROM T_EXL_DELAYNOTICE "
        strSQL = strSQL & "WHERE T_EXL_DELAYNOTICE.CUSTNAME like '%" & strcust & "%' "



        'CONVERT(DATETIME, '2020-02-14 00:00:00')   CONVERT(DATETIME, [T_EXL_DELAYNOTICE].[ETD_A])


        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        GET_MAXDATE = 0
        '結果を取り出す 
        While (dataread.Read())
            GET_MAXDATE = dataread("ddd")
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()

    End Function
    Private Function GET_DELATDATE(strcust As String, strETD As String) As Long

        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String = ""
        Dim strinv As String = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=KBHWPM02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        Dim dt1 As DateTime = DateTime.Now

        Dim ts1 As New TimeSpan(100, 0, 0, 0)
        Dim ts2 As New TimeSpan(100, 0, 0, 0)
        Dim dt2 As DateTime = dt1 + ts1
        Dim dt3 As DateTime = dt1 - ts1

        'データベース接続を開く
        cnn.Open()

        strSQL = ""
        strSQL = strSQL & "SELECT T_EXL_DELAYNOTICE.CUSTNAME, left(T_EXL_DELAYNOTICE.UPLOAD_DATE,7), Avg(CONVERT(decimal,CONVERT(datetime, [T_EXL_DELAYNOTICE].[ETD_A]))-CONVERT(decimal,CONVERT(datetime, [T_EXL_DELAYNOTICE].[ETD_B]))) as ddd "
        strSQL = strSQL & "FROM T_EXL_DELAYNOTICE "
        strSQL = strSQL & "WHERE T_EXL_DELAYNOTICE.CUSTNAME like '%" & strcust & "%' "
        strSQL = strSQL & "AND left(T_EXL_DELAYNOTICE.UPLOAD_DATE,7) like '%" & strETD & "%' "
        strSQL = strSQL & "GROUP BY T_EXL_DELAYNOTICE.CUSTNAME, left(T_EXL_DELAYNOTICE.UPLOAD_DATE,7) "


        'CONVERT(DATETIME, '2020-02-14 00:00:00')   CONVERT(DATETIME, [T_EXL_DELAYNOTICE].[ETD_A])


        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        GET_DELATDATE = 0
        '結果を取り出す 
        While (dataread.Read())
            GET_DELATDATE = dataread("ddd")
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()

    End Function

    Private Function GET_SNKIN(strpono As String) As String


        Dim strSQL As String = ""
        Dim strDate As String = ""
        Dim strinv As String = ""

        Dim dt1 As DateTime = DateTime.Now

        Dim ts1 As New TimeSpan(100, 0, 0, 0)
        Dim ts2 As New TimeSpan(100, 0, 0, 0)
        Dim dt2 As DateTime = dt1 + ts1
        Dim dt3 As DateTime = dt1 - ts1

        Dim conn As New OracleConnection
        conn.ConnectionString = "User Id=EXD043529;Password=EXD043529;Data Source=EXPJ"
        conn.Open()

        'データの取得
        Dim cmd As New OracleCommand
        cmd.Connection = conn


        strSQL = ""
        strSQL = strSQL & "SELECT EXPJ.T_ODR.DEL_FLG, Sum(EXPJ.T_ODR.ODR_AMOUNT) AS ODR_AMOUNTの合計 "
        strSQL = strSQL & "FROM EXPJ.T_ODR "
        strSQL = strSQL & "WHERE EXPJ.T_ODR.CUST_ODR_NO IN ('" & strpono & "') "
        strSQL = strSQL & "AND EXPJ.T_ODR.DEL_FLG = 0 "
        strSQL = strSQL & "GROUP BY EXPJ.T_ODR.DEL_FLG "


        GET_SNKIN = ""
        '結果を取り出す 
        Dim dataread As OracleDataReader = cmd.ExecuteReader()
        Dim strSize As String = ""
        Dim intMax As Integer = 0
        While (dataread.Read())
            GET_SNKIN = Convert.ToString(dataread("ODR_AMOUNTの合計"))
        End While


        'クローズ処理 
        dataread.Close()
        'dbcmd.Dispose()
        conn.Close()
        conn.Dispose()

    End Function
    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged
        Dim sch_ID As String = GridView1.SelectedValue.ToString
        '選択された行のSCH_ID
        strRow = sch_ID
    End Sub

    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load
        If IsPostBack Then
            ' そうでない時処理
        Else
            'Me.DropDownList1.Items.Insert(0, "-ｽﾃｰﾀｽ-") '先頭に空白行追加
            'Me.DropDownList2.Items.Insert(0, "-海貨業者-") '先頭に空白行追加
        End If

        'Me.Label2.Text = ""

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

        ''最終更新年月日を表示
        'Me.Label2.Text = strDate & " 更新"

    End Sub

    Private Sub Make_Grid()
        'GRIDを作成する。

        Dim Dataobj As New DBAccess
        'Dim strSts As String = DropDownList1.SelectedValue
        'Dim strFwd As String = DropDownList2.SelectedValue

        'If strSts = "-ｽﾃｰﾀｽ-" Then
        '    strSts = ""
        'End If
        'If strFwd = "-海貨業者-" Then
        '    strFwd = ""
        'End If

        ''データの取得
        'Dim ds As DataSet = Dataobj.GET_BOOKING_DATA(strSts, strFwd, Trim(Me.TextBox1.Text), Trim(Me.TextBox2.Text))
        'If ds.Tables.Count > 0 Then
        '    GridView1.DataSourceID = ""
        '    GridView1.DataSource = ds
        '    GridView1.DataBind()
        'End If
    End Sub

    'Private Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList1.SelectedIndexChanged
    '    'ステータス選択
    '    Call Make_Grid()
    'End Sub

    'Private Sub DropDownList2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList2.SelectedIndexChanged
    '    '海貨業者選択
    '    Call Make_Grid()
    'End Sub

    'Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
    '    '絞込ボタン押下
    '    Call Make_Grid()
    'End Sub

    'Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
    '    'リセットボタン押下
    '    DropDownList1.SelectedIndex = 0
    '    DropDownList2.SelectedIndex = 0
    '    TextBox1.Text = ""
    '    TextBox2.Text = ""

    '    Make_Grid()
    'End Sub

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
