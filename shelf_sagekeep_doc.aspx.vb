
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.Common
Imports System.Console
Imports ClosedXML.Excel
Imports System.IO

Partial Class yuusen
    Inherits System.Web.UI.Page

    Public strRow As String
    Public strProcess As String

    Protected Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList1.SelectedIndexChanged

        If DropDownList1.Text = "棚番号" Then
            DropDownList2.Items.Clear()
            DropDownList2.DataSource = SqlDataSource2
            DropDownList2.DataTextField = "SHELFNO"
            DropDownList2.DataValueField = "SHELFNO"
            DropDownList2.DataBind()

            DropDownList2.Items.Insert(0, "--Select--")

        ElseIf DropDownList1.Text = "業務区分" Then
            DropDownList2.Items.Clear()
            DropDownList2.DataSource = SqlDataSource3
            DropDownList2.DataTextField = "KBN02"
            DropDownList2.DataValueField = "KBN02"
            DropDownList2.DataBind()

            DropDownList2.Items.Insert(0, "--Select--")

        ElseIf DropDownList1.Text = "チーム" Then
            DropDownList2.Items.Clear()
            DropDownList2.DataSource = SqlDataSource4
            DropDownList2.DataTextField = "TEAM"
            DropDownList2.DataValueField = "TEAM"
            DropDownList2.DataBind()

            DropDownList2.Items.Insert(0, "--Select--")

        ElseIf DropDownList1.Text = "担当者" Then
            DropDownList2.Items.Clear()
            DropDownList2.DataSource = SqlDataSource8
            DropDownList2.DataTextField = "PIC02"
            DropDownList2.DataValueField = "PIC02"
            DropDownList2.DataBind()

            DropDownList2.Items.Insert(0, "--Select--")

        ElseIf DropDownList1.Text = "書類名" Then
            DropDownList2.Items.Clear()
            DropDownList2.DataSource = SqlDataSource12
            DropDownList2.DataTextField = "DOCNAME"
            DropDownList2.DataValueField = "DOCNAME"
            DropDownList2.DataBind()

            DropDownList2.Items.Insert(0, "--Select--")

        End If

    End Sub
    Protected Sub DropDownList2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList2.SelectedIndexChanged

        If DropDownList1.Text = "棚番号" Then

            GridView1.DataSourceID = ""
            GridView1.DataSource = SqlDataSource1
            GridView1.DataBind()

            DropDownList2.Items.Clear()
            DropDownList2.DataSource = SqlDataSource2
            DropDownList2.DataTextField = "SHELFNO"
            DropDownList2.DataValueField = "SHELFNO"
            DropDownList2.DataBind()

            DropDownList2.Items.Insert(0, "--Select--")

        ElseIf DropDownList1.Text = "業務区分" Then

            GridView1.DataSourceID = ""
            GridView1.DataSource = SqlDataSource5
            GridView1.DataBind()

            DropDownList2.Items.Clear()
            DropDownList2.DataSource = SqlDataSource3
            DropDownList2.DataTextField = "KBN02"
            DropDownList2.DataValueField = "KBN02"
            DropDownList2.DataBind()

            DropDownList2.Items.Insert(0, "--Select--")

        ElseIf DropDownList1.Text = "チーム" Then

            GridView1.DataSourceID = ""
            GridView1.DataSource = SqlDataSource6
            GridView1.DataBind()

            DropDownList2.Items.Clear()
            DropDownList2.DataSource = SqlDataSource4
            DropDownList2.DataTextField = "TEAM"
            DropDownList2.DataValueField = "TEAM"
            DropDownList2.DataBind()

            DropDownList2.Items.Insert(0, "--Select--")

        ElseIf DropDownList1.Text = "担当者" Then

            GridView1.DataSourceID = ""
            GridView1.DataSource = SqlDataSource9
            GridView1.DataBind()

            DropDownList2.Items.Clear()
            DropDownList2.DataSource = SqlDataSource8
            DropDownList2.DataTextField = "PIC02"
            DropDownList2.DataValueField = "PIC02"
            DropDownList2.DataBind()

            DropDownList2.Items.Insert(0, "--Select--")

        ElseIf DropDownList1.Text = "書類名" Then

            GridView1.DataSourceID = ""
            GridView1.DataSource = SqlDataSource10
            GridView1.DataBind()

            DropDownList2.Items.Clear()
            DropDownList2.DataSource = SqlDataSource12
            DropDownList2.DataTextField = "DOCNAME"
            DropDownList2.DataValueField = "DOCNAME"
            DropDownList2.DataBind()

            DropDownList2.Items.Insert(0, "--Select--")


        End If



    End Sub
    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        DropDownList1.Items.Clear()

        DropDownList1.Items.Insert(0, "--Select--")
        DropDownList1.Items.Insert(1, "棚番号")
        DropDownList1.Items.Insert(2, "業務区分")
        DropDownList1.Items.Insert(3, "チーム")
        DropDownList1.Items.Insert(4, "担当者")
        DropDownList1.Items.Insert(5, "書類名")


        DropDownList2.Items.Clear()
        DropDownList2.Items.Insert(0, "--Select--")

        GridView1.DataSourceID = ""
        GridView1.DataSource = SqlDataSource7
        GridView1.DataBind()


    End Sub


    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load




    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click


        '前月分ダウンロードボタン押下
        Dim strFile As String = Format(Now, "yyyyMMdd") & "_書庫管理.xlsx"
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
        Dim strTxtFiles() As String = IO.Directory.GetFiles(strPath, Format(Now, "yyyyMMdd") & "_書庫管理.xlsx")

        Dim strFile0 As String = ""
        'ファイル検索
        strFile0 = Dir(strPath & "*書庫管理.xlsx")
        Do While strFile0 <> ""

            If strFile0 = Format(Now, "yyyyMMdd") & "_書庫管理.xlsx" Then
            Else
                System.IO.File.Delete(strPath & strFile0)
            End If

            strFile0 = Dir()
        Loop



        strChanged = strTxtFiles(0)
        strFileNm = Path.GetFileName(strChanged)

        'Contentをクリア
        Response.ClearContent()

        'Contentを設定
        'Response.ContentEncoding = System.Text.Encoding.GetEncoding("shift-jis")
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

        Dim dt = New DataTable("T_EXL_DOC_BOX")

        Using conn = New SqlConnection(ConnectionString)
            Dim cmd = conn.CreateCommand()

            strSQL = strSQL & "SELECT * "
            strSQL = strSQL & "FROM T_EXL_DOC_BOX "

            cmd.CommandText = strSQL
            Dim sda = New SqlDataAdapter(cmd)
            sda.Fill(dt)
        End Using

        Return dt
    End Function


End Class
