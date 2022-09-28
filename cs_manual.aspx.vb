Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.Common
Imports System.IO
Imports ClosedXML.Excel

Imports System.Linq


Partial Class cs_home
    Inherits System.Web.UI.Page

    Public strRow As String
    Public strProcess As String
    Public strPath As String = "C:\exp\cs_home\files"


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If IsPostBack = True Then
        Else

            Dim strSQL As String = ""

            Dim dataread As SqlDataReader
            Dim dbcmd As SqlCommand

            Dim dataread2 As SqlDataReader
            Dim dbcmd2 As SqlCommand

            Dim intCnt As Integer
            Dim intCnt2 As Integer

            Dim a As String

            Dim strcode As String = ""
            Dim strsname As String = ""
            Dim strname As String = ""
            Dim strname_addres01 As String = ""
            Dim strname_addres02 As String = ""


            Dim dt1 As DateTime = DateTime.Now

            Dim ts1 As New TimeSpan(2000, 0, 0, 0)
            Dim ts2 As New TimeSpan(2000, 0, 0, 0)
            Dim dt2 As DateTime = dt1 + ts1
            Dim dt3 As DateTime = dt1 - ts1

            '接続文字列の作成
            Dim ConnectionString As String = String.Empty
            Dim ConnectionString2 As String = String.Empty
            Dim ConnectionString3 As String = String.Empty
            'SQL Server認証
            ConnectionString = "Data Source=svdpo051;Initial Catalog=BPTB001;User Id=ado_bptb001;Password=ado_bptb001"
            ConnectionString2 = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
            ConnectionString3 = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"

            'SqlConnectionクラスの新しいインスタンスを初期化
            Dim cnn = New SqlConnection(ConnectionString)
            Dim Command = cnn.CreateCommand

            'データベース接続を開く
            cnn.Open()

            Dim cnn2 = New SqlConnection(ConnectionString2)
            Dim Command2 = cnn2.CreateCommand

            Dim cnn3 = New SqlConnection(ConnectionString3)
            Dim Command3 = cnn3.CreateCommand

            'データベース接続を開く
            cnn3.Open()

            'データベース接続を開く
            cnn2.Open()


            strSQL = ""
            strSQL = strSQL & "DELETE FROM T_EXL_CSMANUAL_ADDCUST "


            Command2.CommandText = strSQL
            ' SQLの実行
            Command2.ExecuteNonQuery()

            strSQL = ""
            strSQL = strSQL & "SELECT M_CUST_TB.CUSTCODE,M_CUST_TB.CUSTNAME,M_CUST_TB.CUSTOMERNAME,M_CUST_TB.CUSTOMERADDRESS,M_CUST_TB.CONSIGNEENAME,M_CUST_TB.CONSIGNEEADDRESS "
            strSQL = strSQL & "FROM M_CUST_TB "
            strSQL = strSQL & "WHERE M_CUST_TB.STAMP > '" & dt3 & "' "
            strSQL = strSQL & "ORDER BY M_CUST_TB.CUSTCODE "

            'ＳＱＬコマンド作成 
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行 
            dataread = dbcmd.ExecuteReader()


            '結果を取り出す 
            While (dataread.Read())
                a = dataread("CUSTCODE")

                strSQL = ""
                strSQL = strSQL & "SELECT T_EXL_CSMANUAL.NEW_CODE "
                strSQL = strSQL & "FROM T_EXL_CSMANUAL "
                strSQL = strSQL & "WHERE T_EXL_CSMANUAL.NEW_CODE ='" & dataread("CUSTCODE") & "'"

                'ＳＱＬコマンド作成 
                dbcmd2 = New SqlCommand(strSQL, cnn2)
                'ＳＱＬ文実行 
                dataread2 = dbcmd2.ExecuteReader()

                intCnt2 = 0
                '結果を取り出す 
                While (dataread2.Read())
                    intCnt2 = 1
                End While

                If IsNumeric(dataread("CUSTCODE")) = True Then
                    intCnt2 = 1
                End If

                'クローズ処理 
                dataread2.Close()
                dbcmd2.Dispose()

                If intCnt2 = 0 Then



                    strcode = Trim(dataread("CUSTCODE"))
                    strsname = Trim(dataread("CUSTNAME"))
                    strname = Trim(Replace(dataread("CONSIGNEENAME"), "'", "''"))
                    strname_addres01 = Replace(Trim(dataread("CUSTOMERNAME")) & vbCr & Trim(dataread("CUSTOMERADDRESS")), "'", "''")
                    strname_addres02 = Replace(Trim(dataread("CONSIGNEENAME")) & vbCr & Trim(dataread("CONSIGNEEADDRESS")), "'", "''")

                    strSQL = ""
                    strSQL = strSQL & "INSERT INTO T_EXL_CSMANUAL_ADDCUST VALUES("
                    strSQL = strSQL & " '" & strcode & "', "
                    strSQL = strSQL & " '" & strsname & "', "
                    strSQL = strSQL & " '" & strname & "', "
                    strSQL = strSQL & " '" & strname_addres01 & "', "
                    strSQL = strSQL & " '" & strname_addres02 & "' "
                    strSQL = strSQL & ")"

                    Command3.CommandText = strSQL
                    ' SQLの実行
                    Command3.ExecuteNonQuery()

                End If


            End While

            'クローズ処理 
            dataread.Close()
            dbcmd.Dispose()

            cnn.Close()
            cnn.Dispose()

            cnn2.Close()
            cnn2.Dispose()

            cnn3.Close()
            cnn3.Dispose()

            If DropDownList2.SelectedValue = "" Then

                DropDownList2.Items.Clear()
                DropDownList2.DataSource = SqlDataSource2
                DropDownList2.DataTextField = "CUSTCODE"
                DropDownList2.DataValueField = "CUSTCODE"
                DropDownList2.DataBind()

                DropDownList2.Items.Insert(0, "")

            End If
        End If


    End Sub



    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        '表示ボタン押下処理
        Dim Dataobj As New DBAccess
        Dim strCUST As String = TextBox1.Text

        Dim ds As DataSet = Dataobj.GET_CS_RESULT(strCUST)
        If ds.Tables.Count > 0 Then
            GridView1.DataSourceID = ""
            GridView1.DataSource = ds
            Session("data") = ds
        End If

        'Grid再表示
        GridView1.DataBind()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        '詳細表示ボタン押下
        If Trim(TextBox1.Text) = "" Then
            Label12.Text = "表示する客先コードを入力してください。"
            Return
        End If

        '入力された客先コードをセッションへ
        Session("strCust") = Trim(TextBox1.Text)
        Session("strMode") = "01"       '更新モード

        '画面遷移
        Response.Redirect("cs_manual_detail.aspx")
    End Sub

    'Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
    '    '新規登録ボタン押下
    '    If Trim(DropDownList2.Text) = "" Then
    '        Label12.Text = "ベースとなる客先コードを選択してください。"
    '        Return
    '    End If

    '    '入力された客先コードをセッションへ
    '    Session("strCust") = Trim(DropDownList2.Text)
    '    Session("strMode") = "03"       '登録モード（選択式）

    '    '画面遷移
    '    Response.Redirect("cs_manual_detail.aspx")
    'End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        '新規登録ボタン押下
        'If Trim(TextBox1.Text) = "" Then
        '    Label12.Text = "ベースとなる客先コードを入力してください。"
        '    Return
        'End If

        If Trim(TextBox1.Text) = "" And Trim(DropDownList2.SelectedValue) <> "" Then
            Session("strMode") = "03"       '登録モード
            Session("strCust") = Trim(DropDownList2.Text)
        ElseIf Trim(TextBox1.Text) <> "" And Trim(DropDownList2.SelectedValue) = "" Then
            Session("strMode") = "02"       '登録モード
            Session("strCust") = Trim(TextBox1.Text)
        ElseIf Trim(TextBox1.Text) = "" And Trim(DropDownList2.SelectedValue) = "" Then

            Label12.Text = "客先CDは①、②のどちらかは入力してください。"
            Return
        ElseIf Trim(TextBox1.Text) <> "" And Trim(DropDownList2.SelectedValue) <> "" Then
            Label12.Text = "客先CDは①、②のどちらかのみの入力してください。"
            Return
        End If



        '画面遷移
        Response.Redirect("cs_manual_detail.aspx")
    End Sub

    Private Sub GridView1_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound


        e.Row.Cells(5).Visible = False
        e.Row.Cells(6).Visible = False
        e.Row.Cells(7).Visible = False
        e.Row.Cells(8).Visible = False
        e.Row.Cells(9).Visible = False
        e.Row.Cells(10).Visible = False
        e.Row.Cells(11).Visible = False
        e.Row.Cells(12).Visible = False
        e.Row.Cells(13).Visible = False
        e.Row.Cells(14).Visible = False
        e.Row.Cells(15).Visible = False
        e.Row.Cells(16).Visible = False
        e.Row.Cells(17).Visible = False
        e.Row.Cells(18).Visible = False
        e.Row.Cells(19).Visible = False
        e.Row.Cells(20).Visible = False
        e.Row.Cells(21).Visible = False
        e.Row.Cells(22).Visible = False
        e.Row.Cells(23).Visible = False
        e.Row.Cells(24).Visible = False




        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim dltButton As ImageButton = e.Row.FindControl("ImageButton1")
            'ボタンが存在する場合のみセット
            If Not (dltButton Is Nothing) Then
                dltButton.CommandArgument = e.Row.RowIndex.ToString()
            End If
        End If

    End Sub

    Private Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName = "edt" Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)


            '入力された客先コードをセッションへ
            Session("strCust") = Trim(Me.GridView1.Rows(index).Cells(1).Text)
            Session("strMode") = "01"       '更新モード

            Response.Redirect("cs_manual_detail.aspx")

        End If

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        'Dim strFile As String = Format(Now, "yyyyMMdd") & "_CS_MANUAL.xlsx"
        'Dim strPath As String = "C:\exp\cs_home\files\"
        'Dim strChanged As String    'サーバー上のフルパス
        'Dim strFileNm As String     'ファイル名

        'Dim a

        'Dim dt As New DataTable("CSMANUAL")
        'For Each cell As TableCell In GridView1.HeaderRow.Cells
        '    dt.Columns.Add(cell.Text)
        'Next

        'For Each row As GridViewRow In GridView1.Rows
        '    dt.Rows.Add()
        '    For i As Integer = 0 To row.Cells.Count - 1
        '        a = Replace(Replace(row.Cells(i).Text, "&nbsp;", ""), "&#215;", "×")
        '        If a = "" Then
        '            a = DBNull.Value
        '        End If
        '        dt.Rows(dt.Rows.Count - 1)(i) = a
        '    Next
        'Next
        'Using workbook As New XLWorkbook()
        '    Dim ws As IXLWorksheet = workbook.Worksheets.Add(dt)

        '    ws.Style.Font.FontName = "Meiryo UI"
        '    ws.Style.Alignment.WrapText = False
        '    ws.Columns.AdjustToContents()
        '    workbook.SaveAs(strPath & strFile)

        'End Using

        ''ファイル名を取得する
        'strChanged = strPath & Format(Now, "yyyyMMdd") & "_CS_MANUAL.xlsx"
        'strFileNm = Path.GetFileName(strChanged)

        ''Contentをクリア
        'Response.ClearContent()

        ''Contentを設定
        'Response.ContentEncoding = System.Text.Encoding.GetEncoding("shift-jis")
        'Response.ContentType = "application/vnd.ms-excel"

        ''表示ファイル名を指定
        'Dim fn As String = HttpUtility.UrlEncode(strFileNm)
        'Response.AddHeader("Content-Disposition", "attachment;filename=" + fn)

        ''ダウンロード対象ファイルを指定
        'Response.WriteFile(strChanged)

        ''ダウンロード実行
        'Response.Flush()
        'Response.End()

        '前月分ダウンロードボタン押下
        Dim strFile As String = Format(Now, "yyyyMMdd") & "_CS_MANUAL.xlsx"
        Dim strPath As String = "C:\exp\cs_home\files\"
        Dim strChanged As String    'サーバー上のフルパス
        Dim strFileNm As String     'ファイル名

        Dim dtToday As DateTime = DateTime.Today



        Dim dt = GetNorthwindProductTable()
        Dim workbook = New XLWorkbook()
        Dim worksheet = workbook.Worksheets.Add(dt)

        worksheet.Style.Font.FontName = "Meiryo UI"
        worksheet.Style.Alignment.WrapText = False
        worksheet.Columns.AdjustToContents()
        worksheet.SheetView.FreezeRows(1)

        workbook.SaveAs(strPath & strFile)


        'ファイル名を取得する
        Dim strTxtFiles() As String = IO.Directory.GetFiles(strPath, Format(Now, "yyyyMMdd") & "_CS_MANUAL.xlsx")

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

        Dim dt = New DataTable("T_EXL_CSMANUAL")

        Using conn = New SqlConnection(ConnectionString)
            Dim cmd = conn.CreateCommand()

            strSQL = strSQL & "SELECT NEW_CODE, OLD_CODE, CUST_NM,CUST_AB,BL_SEND,CUST_ADDRESS,CONSIGNEE,CONSIGNEE_OF_SI,CONSIGNEE_OF_SI_ADDRESS,FINAL_DES,FINAL_DES_ADDRESS,NOTIFY "
            strSQL = strSQL & ",WOOD_NECE,CO_NECE,DOC_OF_EGYPT,EPA_NECE,FTA,CERTIFICATE_OF_CONFORMITY,ERL_NECE,VESS_NECE,DOC_NECESSITY,CONTAINER_CLEANING,BEARING,IV_AUTO_CALC "
            strSQL = strSQL & "FROM T_EXL_CSMANUAL "

            cmd.CommandText = strSQL
            Dim sda = New SqlDataAdapter(cmd)
            sda.Fill(dt)
        End Using

        Return dt
    End Function
End Class
