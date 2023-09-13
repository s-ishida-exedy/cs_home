
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.Common
Imports System.Console
Imports ClosedXML.Excel
Imports System.IO
Imports System.Linq
Partial Class yuusen
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


        Dim ano As Long
        Dim cno As Long
        Dim wday3 As String = ""
        Dim dt1 As DateTime = DateTime.Now
        Dim Kaika00 As String = ""

        Dim ts1 As New TimeSpan(80, 0, 0, 0)
        Dim ts2 As New TimeSpan(80, 0, 0, 0)
        Dim dt2 As DateTime = dt1 + ts1
        Dim dt3 As DateTime = dt1 - ts1

        Dim dubkin As Long = 0
        Dim str02 As String = ""
        Dim str03 As String = ""
        Dim str04 As String = ""
        Dim str05 As String = ""


        If e.Row.RowType = DataControlRowType.DataRow Then

            '接続文字列の作成
            Dim ConnectionString As String = String.Empty

            'SQL Server認証
            ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"

            'SqlConnectionクラスの新しいインスタンスを初期化
            Dim cnn = New SqlConnection(ConnectionString)
            Dim Command = cnn.CreateCommand

            e.Row.Cells(7).Text = ""
            dubkin = GET_IV_AMOUNT(Trim(Replace(e.Row.Cells(5).Text, vbLf, "")))
            e.Row.Cells(7).Text = Double.Parse(dubkin).ToString("#,0")

            If dubkin = 0 And e.Row.Cells(6).Text <> 0 Then
                dubkin = GET_CON_AMOUNT(Trim(Replace(e.Row.Cells(5).Text, vbLf, "")))
                e.Row.Cells(7).Text = Double.Parse(dubkin).ToString("#,0")
            End If


            If dubkin = 0 And e.Row.Cells(6).Text = 0 Then
                dubkin = GET_LCL_AMOUNT(Trim(Replace(e.Row.Cells(5).Text, vbLf, "")))
                e.Row.Cells(7).Text = Double.Parse(dubkin).ToString("#,0")
            End If


            Label05.Text = Double.Parse(Label05.Text) + Double.Parse(e.Row.Cells(6).Text)
            Label06.Text = Double.Parse(Label06.Text) + dubkin



            If e.Row.Cells(9).Text = "1" Then
                e.Row.Cells(9).Text = "○"
            End If


            'データベース接続を開く
            cnn.Open()

            strSQL = "SELECT BKGNO FROM [T_EXL_WORKSTATUS00] WHERE [T_EXL_WORKSTATUS00].BKGNO = '" & Trim(Replace(e.Row.Cells(5).Text, vbLf, "")) & "' "
            strSQL = strSQL & "AND [T_EXL_WORKSTATUS00].ID = '005' "

            'ＳＱＬコマンド作成
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行
            dataread = dbcmd.ExecuteReader()
            strbkg = ""
            '結果を取り出す
            While (dataread.Read())
                '書類作成状況
                e.Row.Cells(11).BackColor = Drawing.Color.DarkGray
                e.Row.Cells(9).Text = "○"

            End While

            'クローズ処理
            dataread.Close()
            dbcmd.Dispose()


            strSQL = "SELECT BKGNO FROM [T_EXL_WORKSTATUS00] WHERE [T_EXL_WORKSTATUS00].BKGNO = '" & Trim(Replace(e.Row.Cells(5).Text, vbLf, "")) & "' "
            strSQL = strSQL & "AND [T_EXL_WORKSTATUS00].ID = '002' "

            'ＳＱＬコマンド作成
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行
            dataread = dbcmd.ExecuteReader()
            strbkg = ""
            '結果を取り出す
            While (dataread.Read())
                '書類作成状況
                'e.Row.BackColor = Drawing.Color.Khaki
                e.Row.Cells(10).Text = "済"


            End While

            'クローズ処理
            dataread.Close()
            dbcmd.Dispose()

            strSQL = "SELECT BKGNO FROM [T_EXL_WORKSTATUS00] WHERE [T_EXL_WORKSTATUS00].BKGNO = '" & Trim(Replace(e.Row.Cells(5).Text, vbLf, "")) & "' "
            strSQL = strSQL & "AND [T_EXL_WORKSTATUS00].ID = '003' "

            'ＳＱＬコマンド作成
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行
            dataread = dbcmd.ExecuteReader()
            strbkg = ""
            '結果を取り出す
            While (dataread.Read())
                '書類作成状況
                'e.Row.BackColor = Drawing.Color.Khaki
                e.Row.Cells(11).Text = "済"
                e.Row.Cells(9).BackColor = Drawing.Color.DarkGray
            End While

            'クローズ処理
            dataread.Close()
            dbcmd.Dispose()



            strSQL = "SELECT LOADING_PORT FROM [T_PORT_BCP]  "
            strSQL = strSQL & "WHERE [T_PORT_BCP].CUSTCODE = '" & Trim(Replace(e.Row.Cells(2).Text, vbLf, "")) & "' "

            'ＳＱＬコマンド作成
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行
            dataread = dbcmd.ExecuteReader()
            strbkg = ""
            '結果を取り出す
            While (dataread.Read())
                '書類作成状況
                e.Row.Cells(12).Text = dataread("LOADING_PORT")
            End While

            If e.Row.Cells(12).Text = "" Then
                'e.Row.Cells(12).Text = "港振替不可"
            End If

            'クローズ処理
            dataread.Close()
            dbcmd.Dispose()

            strSQL = "SELECT FORWAEDER FROM [T_FORWARDER_BCP]  "
            strSQL = strSQL & "WHERE [T_FORWARDER_BCP].CUSTCODE = '" & Trim(Replace(e.Row.Cells(2).Text, vbLf, "")) & "' "

            'ＳＱＬコマンド作成
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行
            dataread = dbcmd.ExecuteReader()
            strbkg = ""
            '結果を取り出す
            While (dataread.Read())
                '書類作成状況
                If e.Row.Cells(13).Text = "" Or e.Row.Cells(13).Text = "&nbsp;" Then
                    If e.Row.Cells(1).Text = dataread("FORWAEDER") Then
                    Else
                        e.Row.Cells(13).Text = dataread("FORWAEDER")
                    End If
                Else
                    If e.Row.Cells(1).Text = dataread("FORWAEDER") Then

                    Else
                        e.Row.Cells(13).Text = e.Row.Cells(13).Text & "," & dataread("FORWAEDER")
                    End If

                End If

            End While

            If e.Row.Cells(12).Text = "&nbsp;" Then
                e.Row.Cells(0).BackColor = Drawing.Color.Red
            End If

            If e.Row.Cells(13).Text = "&nbsp;" Then
                e.Row.Cells(0).BackColor = Drawing.Color.Red
            End If

            'クローズ処理
            dataread.Close()
                dbcmd.Dispose()

        End If


            Label05.Text = Double.Parse(Label05.Text).ToString("#,0")
        Label06.Text = Double.Parse(Label06.Text).ToString("#,0")


    End Sub


    Function GET_IV_AMOUNT(bkgno As String) As Double

        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""

        Dim dt1 As DateTime = DateTime.Now


        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=svdpo051;Initial Catalog=BPTB001;User Id=ado_bptb001;Password=ado_bptb001"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()


        strSQL = "SELECT T_INV_HD_TB.BOOKINGNO, Sum(T_INV_BD_TB.KIN)*T_INV_HD_TB.RATE AS KIN  "
        strSQL = strSQL & "FROM T_INV_HD_TB LEFT JOIN T_INV_BD_TB ON T_INV_HD_TB.INVOICENO = T_INV_BD_TB.INVOICENO "
        strSQL = strSQL & "WHERE T_INV_HD_TB.HEADTITLE like 'INVOICE%' "
        strSQL = strSQL & "AND T_INV_HD_TB.BOOKINGNO = '" & bkgno & "' "
        strSQL = strSQL & "AND T_INV_BD_TB.CMCVFLG = '0' "
        strSQL = strSQL & "AND T_INV_BD_TB.EXDMPN <> 'INTEREST' "
        strSQL = strSQL & "GROUP BY T_INV_HD_TB.BOOKINGNO,T_INV_HD_TB.RATE "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()



        GET_IV_AMOUNT = 0
        '結果を取り出す 
        While (dataread.Read())
            GET_IV_AMOUNT = Convert.ToString(dataread("KIN"))        'ETD(計上日)
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()

    End Function

    Function GET_CON_AMOUNT(bkgno As String) As Double

        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strcon01 As Long
        Dim strcon02 As Long
        Dim strcon03 As Long
        Dim dt1 As DateTime = DateTime.Now


        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=KBHWPM02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()

        strSQL = ""
        strSQL = strSQL & "SELECT T_EXL_CSANKEN.* "
        strSQL = strSQL & "FROM T_EXL_CSANKEN "
        strSQL = strSQL & "WHERE T_EXL_CSANKEN.BOOKING_NO = '" & bkgno & "' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()




        GET_CON_AMOUNT = 0
        '結果を取り出す 
        While (dataread.Read())

            strcon01 = dataread("TWENTY_FEET")
            strcon02 = dataread("FOURTY_FEET")
            strcon03 = dataread("LCL_QTY")

            strcon01 = strcon01 * 10000000
            strcon02 = strcon02 * 20000000
            strcon03 = strcon03 * 20000000


        End While


        GET_CON_AMOUNT = strcon01 + strcon02 + strcon03

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()

    End Function

    Function GET_LCL_AMOUNT(bkgno As String) As Double

        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strcon01 As Long
        Dim strcon00 As String = ""
        Dim dt1 As DateTime = DateTime.Now


        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=KBHWPM02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()

        strSQL = ""
        strSQL = strSQL & "SELECT T_BOOKING.* "
        strSQL = strSQL & "FROM T_BOOKING "
        strSQL = strSQL & "WHERE T_BOOKING.BOOKING_NO = '" & bkgno & "' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()




        GET_LCL_AMOUNT = 0
        '結果を取り出す 
        While (dataread.Read())

            strcon00 = Replace(Replace(Replace(dataread("LCL_QTY"), "M3", ""), "M3未満", ""), "ﾊﾟﾚｯﾄ", "")

            If IsNumeric(strcon00) Then
                strcon01 = strcon00
            Else
                strcon01 = Left(strcon00, 2)
                If IsNumeric(strcon00) Then
                    strcon01 = strcon00
                Else
                    strcon01 = Left(strcon00, 1)
                    If IsNumeric(strcon00) Then
                        strcon01 = strcon00
                    Else
                        strcon01 = 0
                    End If
                End If
            End If
            strcon01 = strcon01 * 500000
        End While


        GET_LCL_AMOUNT = strcon01

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()

    End Function

    Private Sub UPD_MEMO02(bkgno As String, str01 As String, str02 As String)
        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=KBHWPM02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand
        Dim strSQL As String = ""
        Dim ivno As String = ""
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim dataread2 As SqlDataReader
        Dim dbcmd2 As SqlCommand
        Dim intCnt As Long
        Dim dt1 As DateTime = DateTime.Now

        'データベース接続を開く
        cnn.Open()

        strSQL = ""
        strSQL = strSQL & "UPDATE T_EXL_SHIPPINGMEMOLIST SET "
        strSQL = strSQL & "T_EXL_SHIPPINGMEMOLIST.REV_SALESDATE ='" & str01 & "', "
        strSQL = strSQL & "T_EXL_SHIPPINGMEMOLIST.REV_STATUS ='" & str02 & "' "
        strSQL = strSQL & "WHERE T_EXL_SHIPPINGMEMOLIST.BOOKING_NO ='" & bkgno & "' "

        Command.CommandText = strSQL
        ' SQLの実行
        Command.ExecuteNonQuery()

        cnn.Close()
        cnn.Dispose()

    End Sub

    Private Sub UPD_MEMO03(bkgno As String, str01 As String)
        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=KBHWPM02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand
        Dim strSQL As String = ""
        Dim ivno As String = ""
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim dataread2 As SqlDataReader
        Dim dbcmd2 As SqlCommand
        Dim intCnt As Long
        Dim dt1 As DateTime = DateTime.Now

        'データベース接続を開く
        cnn.Open()

        strSQL = ""
        strSQL = strSQL & "UPDATE T_EXL_SHIPPINGMEMOLIST SET "
        strSQL = strSQL & "T_EXL_SHIPPINGMEMOLIST.CHECKFLG ='" & str01 & "' "
        strSQL = strSQL & "WHERE T_EXL_SHIPPINGMEMOLIST.BOOKING_NO ='" & bkgno & "' "

        Command.CommandText = strSQL
        ' SQLの実行
        Command.ExecuteNonQuery()

        cnn.Close()
        cnn.Dispose()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        '前月分ダウンロードボタン押下
        Dim strFile As String = Format(Now, "yyyyMMdd") & "_BCP.xlsx"
        Dim strPath As String = "C:\exp\cs_home\files\"
        Dim strChanged As String    'サーバー上のフルパス
        Dim strFileNm As String     'ファイル名

        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strinv As String = ""
        Dim strbkg As String = ""


        Dim dubkin As Long = 0

        Dim dtToday As DateTime = DateTime.Today

        Dim dt = GetNorthwindProductTable(Replace(TextBox1.Text, "-", "/"), Replace(TextBox2.Text, "-", "/"), DropDownList2.SelectedValue)


        Dim strFile0 As String = ""
        'ファイル検索
        strFile0 = Dir(strPath & "*BCP.xlsx")
        Do While strFile0 <> ""

            If strFile0 = Format(Now, "yyyyMMdd") & "_BCP.xlsx" Then
            Else
                System.IO.File.Delete(strPath & strFile0)
            End If

            strFile0 = Dir()
        Loop

        Dim a

        Dim dt2 As New DataTable("BCP")
        For Each cell As TableCell In GridView1.HeaderRow.Cells
            dt2.Columns.Add(cell.Text)
        Next



        For Each row As DataRow In dt.Rows
            dt2.Rows.Add()
            For i As Integer = 0 To dt.Columns.Count - 1

                If i = 0 Then

                    '接続文字列の作成
                    Dim ConnectionString As String = String.Empty

                    'SQL Server認証
                    ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"

                    'SqlConnectionクラスの新しいインスタンスを初期化
                    Dim cnn = New SqlConnection(ConnectionString)
                    Dim Command = cnn.CreateCommand

                    row(7) = ""
                    dubkin = GET_IV_AMOUNT(Trim(Replace(row(5), vbLf, "")))
                    row(7) = Double.Parse(dubkin).ToString("#,0")

                    If dubkin = 0 And row(6) <> 0 Then
                        dubkin = GET_CON_AMOUNT(Trim(Replace(row(5), vbLf, "")))
                        row(7) = Double.Parse(dubkin).ToString("#,0")
                    End If


                    If dubkin = 0 And row(6) = 0 Then
                        dubkin = GET_LCL_AMOUNT(Trim(Replace(row(5), vbLf, "")))
                        row(7) = Double.Parse(dubkin).ToString("#,0")
                    End If



                    If row(9) = "1" Then
                        row(9) = "○"
                    End If


                    'データベース接続を開く
                    cnn.Open()

                    strSQL = "SELECT BKGNO FROM [T_EXL_WORKSTATUS00] WHERE [T_EXL_WORKSTATUS00].BKGNO = '" & Trim(Replace(row(5), vbLf, "")) & "' "
                    strSQL = strSQL & "AND [T_EXL_WORKSTATUS00].ID = '005' "

                    'ＳＱＬコマンド作成
                    dbcmd = New SqlCommand(strSQL, cnn)
                    'ＳＱＬ文実行
                    dataread = dbcmd.ExecuteReader()
                    strbkg = ""
                    '結果を取り出す
                    While (dataread.Read())
                        '書類作成状況
                        row(9) = "○"

                    End While

                    'クローズ処理
                    dataread.Close()
                    dbcmd.Dispose()


                    strSQL = "SELECT BKGNO FROM [T_EXL_WORKSTATUS00] WHERE [T_EXL_WORKSTATUS00].BKGNO = '" & Trim(Replace(row(5), vbLf, "")) & "' "
                    strSQL = strSQL & "AND [T_EXL_WORKSTATUS00].ID = '002' "

                    'ＳＱＬコマンド作成
                    dbcmd = New SqlCommand(strSQL, cnn)
                    'ＳＱＬ文実行
                    dataread = dbcmd.ExecuteReader()
                    strbkg = ""
                    '結果を取り出す
                    While (dataread.Read())
                        '書類作成状況

                        row(10) = "済"


                    End While

                    'クローズ処理
                    dataread.Close()
                    dbcmd.Dispose()

                    strSQL = "SELECT BKGNO FROM [T_EXL_WORKSTATUS00] WHERE [T_EXL_WORKSTATUS00].BKGNO = '" & Trim(Replace(row(5), vbLf, "")) & "' "
                    strSQL = strSQL & "AND [T_EXL_WORKSTATUS00].ID = '003' "

                    'ＳＱＬコマンド作成
                    dbcmd = New SqlCommand(strSQL, cnn)
                    'ＳＱＬ文実行
                    dataread = dbcmd.ExecuteReader()
                    strbkg = ""
                    '結果を取り出す
                    While (dataread.Read())
                        '書類作成状況
                        'e.Row.BackColor = Drawing.Color.Khaki
                        row(11) = "済"
                    End While

                    'クローズ処理
                    dataread.Close()
                    dbcmd.Dispose()

                    Dim a2 As String = row(12)
                    a2 = a2

                    strSQL = "SELECT LOADING_PORT FROM [T_PORT_BCP]  "
                    strSQL = strSQL & "WHERE [T_PORT_BCP].CUSTCODE = '" & Trim(Replace(row(2), vbLf, "")) & "' "

                    'ＳＱＬコマンド作成
                    dbcmd = New SqlCommand(strSQL, cnn)
                    'ＳＱＬ文実行
                    dataread = dbcmd.ExecuteReader()
                    strbkg = ""
                    '結果を取り出す
                    While (dataread.Read())
                        '書類作成状況
                        row(12) = dataread("LOADING_PORT")
                    End While

                    a2 = a2
                    If Len(row(12)) <= 1 Then
                        'row(12) = "港振替不可"
                    End If

                    'クローズ処理
                    dataread.Close()
                    dbcmd.Dispose()

                    strSQL = "SELECT FORWAEDER FROM [T_FORWARDER_BCP]  "
                    strSQL = strSQL & "WHERE [T_FORWARDER_BCP].CUSTCODE = '" & Trim(Replace(row(2), vbLf, "")) & "' "

                    'ＳＱＬコマンド作成
                    dbcmd = New SqlCommand(strSQL, cnn)
                    'ＳＱＬ文実行
                    dataread = dbcmd.ExecuteReader()
                    strbkg = ""
                    '結果を取り出す
                    While (dataread.Read())
                        '書類作成状況
                        a = row(13)
                        If row(13) = "1" Or row(13) = "" Then
                            If row(1) = dataread("FORWAEDER") Then
                                row(13) = Replace(row(13), "1", "")
                                If IsDBNull(row(13)) Then
                                    row(13) = ""
                                Else
                                End If
                            Else
                                row(13) = dataread("FORWAEDER")
                            End If

                        Else
                            If row(13) = "1" Or row(1) = dataread("FORWAEDER") Then
                            Else
                                row(13) = row(13) & "," & dataread("FORWAEDER")
                            End If
                        End If

                    End While

                    If row(12) = "" Then
                        'row(12) = "港振替不可"
                    End If

                    'クローズ処理
                    dataread.Close()
                    dbcmd.Dispose()


                End If

                a = row(i)
                If IsDBNull(a) Then
                Else
                    If a = "" Then
                        a = DBNull.Value
                    End If
                End If

                dt2.Rows(dt2.Rows.Count - 1)(i) = a
            Next
        Next


        Dim workbook = New XLWorkbook()
        Dim worksheet = workbook.Worksheets.Add(dt2)

        worksheet.Style.Font.FontName = "Meiryo UI"
        worksheet.Style.Alignment.WrapText = False
        worksheet.Columns.AdjustToContents()
        worksheet.SheetView.FreezeRows(1)

        workbook.SaveAs(strPath & strFile)


        'ファイル名を取得する
        Dim strTxtFiles() As String = IO.Directory.GetFiles(strPath, Format(Now, "yyyyMMdd") & "_BCP.xlsx")

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
    Private Shared Function GetNorthwindProductTable(a As String, b As String, c As String) As DataTable
        'EXCELファイル出力
        Dim strSQL As String = ""
        Dim strSDate As String = ""
        Dim strEDate As String = ""

        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"

        Dim dt = New DataTable("T_EXL_CSANKEN")

        Using conn = New SqlConnection(ConnectionString)
            Dim cmd = conn.CreateCommand()



            strSQL = strSQL & ""
            strSQL = strSQL & "SELECT "
            strSQL = strSQL & "  PLACE_OF_RECEIPT,FORWARDER02,CUST,INVOICE,ETD,BOOKING_NO,CONTAINER,FIN_FLG,PLACE_OF_DELIVERY,FLG01,FLG02,FLG03,FLG01,FLG01 "
            strSQL = strSQL & "FROM "
            strSQL = strSQL & "  [T_EXL_CSANKEN] "

            strSQL = strSQL & "WHERE ETD BETWEEN '" & a & "' AND '" & b & "' "
            strSQL = strSQL & "AND PLACE_OF_RECEIPT = '" & c & "' "
            strSQL = strSQL & "ORDER BY ETD "

            cmd.CommandText = strSQL
            Dim sda = New SqlDataAdapter(cmd)
            sda.Fill(dt)
        End Using

        Return dt
    End Function

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        ' このOverridesは以下のエラーを回避するために必要です。
        ' 「GridViewのコントロールGridView1は、runat=server を含む
        '  form タグの内側に置かなければ成りません」    
    End Sub

    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load


    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim Dataobj As New DBAccess
        'Dim strst As Date = Session("stdate")
        'Dim stred As Date = Session("eddate")
        Dim strst As Date
        Dim stred As Date
        Dim strpt As String

        If Session("stdate") = "" And Session("eddate") = "" And TextBox1.Text = "" And TextBox2.Text = "" Then
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "確認", "<script language='JavaScript'>confirm('日付けが未選択です。');</script>", False)
        Else

            If Session("stdate") = "" And Session("eddate") = "" Then
                strst = DateTime.Parse(TextBox1.Text) 'テキストボックスを作成
                stred = DateTime.Parse(TextBox2.Text)
                strpt = DropDownList2.SelectedValue
                Label04.Text = DropDownList2.SelectedValue
            Else
                TextBox1.Text = Session("stdate")
                TextBox2.Text = Session("eddate")
                DropDownList2.SelectedValue = Session("portcd")

                strst = Session("stdate")
                stred = Session("eddate")
                strpt = Session("portcd")
                Label04.Text = Session("portcd")

                Session("stdate") = ""
                Session("eddate") = ""
                Session("portcd") = ""
            End If


            strst = strst.ToString("yyyy/MM/dd")
            stred = stred.ToString("yyyy/MM/dd")


            Dim ds As DataSet = Dataobj.GET_CS_RESULT_BCP_ANKEN(strst, stred, strpt)

            If ds.Tables.Count > 0 Then
                GridView1.DataSourceID = ""
                GridView1.DataSource = ds
                Session("data") = ds
            End If

            'Grid再表示
            GridView1.DataBind()


        End If

    End Sub

End Class
