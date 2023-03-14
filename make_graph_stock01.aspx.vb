
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.Common
Imports ClosedXML.Excel
Imports System.Text
Imports System.Web.Services
Imports System.Configuration
Imports System.IO

Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls




Partial Class yuusen
    Inherits System.Web.UI.Page

    Public strRow As String
    Public strProcess As String




    Public Class trafficSourceData
        Public Property label As String
        Public Property value As String
        Public Property value2 As String
        Public Property value3 As String
        Public Property value4 As String
        Public Property value5 As String
        Public Property value6 As String
        Public Property value7 As String
        Public Property value8 As String
        Public Property value9 As String
        Public Property value10 As String
        Public Property value11 As String
        Public Property value12 As String
        Public Property value13 As String
        Public Property value14 As String
        Public Property value15 As String
        Public Property value16 As String
        Public Property value17 As String
        Public Property value18 As String
        Public Property value19 As String
        Public Property value20 As String
        Public Property value21 As String
        Public Property value22 As String

        Public Property color As String
        Public Property hightlight As String

    End Class


    <WebMethod()>
    Public Shared Function getTrafficSourceData() As List(Of trafficSourceData)
        '    Public Shared Function getTrafficSourceData(ByVal gData As List(Of String)) As List(Of trafficSourceData)

        Dim t As List(Of trafficSourceData) = New List(Of trafficSourceData)()
        Dim arrColor As String() = New String() {"#231F20", "#FFC200", "#F44937", "#16F27E", "#FC9775"}

        Using cn As SqlConnection = New SqlConnection("Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager")
            Dim myQuery As String = "select * from T_EXL_GRAPH_STOCK WHERE DATE01 >= '2022/12/01' ORDER BY DATE01 "
            Dim cmd As SqlCommand = New SqlCommand()
            cmd.CommandText = myQuery
            cmd.CommandType = CommandType.Text
            '            cmd.Parameters.AddWithValue("@year", gData(0))
            '            cmd.Parameters.AddWithValue("@month", gData(1))
            cmd.Connection = cn
            cn.Open()
            Dim dr As SqlDataReader = cmd.ExecuteReader()

            If dr.HasRows Then
                Dim counter As Integer = 0

                While dr.Read()
                    Dim tsData As trafficSourceData = New trafficSourceData()

                    tsData.label = dr("DATE01").ToString()
                    'tsData.value = Math.Round(Double.Parse(dr("firfloor")) / 1000)
                    'tsData.value2 = Math.Round(Double.Parse(dr("trdfloor")) / 1000)
                    'tsData.value3 = Math.Round(Double.Parse(dr("fourfloor")) / 1000)
                    'tsData.value4 = Math.Round(Double.Parse(dr("ew")) / 1000)
                    'tsData.value5 = Math.Round(Double.Parse(dr("cap")) / 1000)
                    'tsData.value6 = Math.Round(Double.Parse(0) / 1000)
                    'tsData.value7 = Math.Round(Double.Parse(dr("ovrpallet")))



                    tsData.value = Math.Round(Double.Parse(dr("firfloor")) / 1000)
                    tsData.value2 = Math.Round(Double.Parse(dr("trdfloor")) / 1000)
                    tsData.value3 = Math.Round(Double.Parse(dr("fourfloor")) / 1000)
                    tsData.value4 = Math.Round(Double.Parse(dr("EW_FIRFR")) / 1000)
                    tsData.value5 = Math.Round(Double.Parse(dr("EW_THRFR")) / 1000)
                    tsData.value6 = Math.Round(Double.Parse(dr("EW_FOUFR")) / 1000)
                    tsData.value7 = Math.Round(Double.Parse(dr("CP_FIRFR")) / 1000)
                    tsData.value8 = Math.Round(Double.Parse(dr("CP_THRFR")) / 1000)
                    tsData.value9 = Math.Round(Double.Parse(dr("CP_FOUFR")) / 1000)
                    tsData.value10 = Math.Round(Double.Parse(dr("OK_FIRFR")) / 1000)
                    tsData.value11 = Math.Round(Double.Parse(dr("OK_THRFR")) / 1000)
                    tsData.value12 = Math.Round(Double.Parse(dr("OK_FOUFR")) / 1000)
                    tsData.value13 = Math.Round(Double.Parse(dr("OVRPALLET")))

                    tsData.value14 = Math.Round(Double.Parse(dr("EW_FIRFR")) / 1000) + Math.Round(Double.Parse(dr("EW_THRFR")) / 1000) + Math.Round(Double.Parse(dr("EW_FOUFR")) / 1000)
                    tsData.value15 = Math.Round(Double.Parse(dr("CP_FIRFR")) / 1000) + Math.Round(Double.Parse(dr("CP_THRFR")) / 1000) + Math.Round(Double.Parse(dr("CP_FOUFR")) / 1000)
                    tsData.value16 = Math.Round(Double.Parse(dr("OK_FIRFR")) / 1000) + Math.Round(Double.Parse(dr("OK_THRFR")) / 1000) + Math.Round(Double.Parse(dr("OK_FOUFR")) / 1000)
                    tsData.value16 = Math.Round(Double.Parse(dr("OK_FIRFR")) / 1000) + Math.Round(Double.Parse(dr("OK_THRFR")) / 1000) + Math.Round(Double.Parse(dr("OK_FOUFR")) / 1000)
                    tsData.value17 = Math.Round(Double.Parse(0) / 1000)
                    tsData.value18 = Math.Round(Double.Parse(dr("firfloor")) / 1000) + Math.Round(Double.Parse(dr("trdfloor")) / 1000) + Math.Round(Double.Parse(dr("fourfloor")) / 1000) + Math.Round(Double.Parse(dr("EW_FIRFR")) / 1000) + Math.Round(Double.Parse(dr("EW_THRFR")) / 1000) + Math.Round(Double.Parse(dr("EW_FOUFR")) / 1000)
                    tsData.value19 = Math.Round(Double.Parse(dr("firfloor")) / 1000) + Math.Round(Double.Parse(dr("EW_FIRFR")) / 1000)
                    tsData.value20 = Math.Round(Double.Parse(dr("trdfloor")) / 1000) + Math.Round(Double.Parse(dr("EW_THRFR")) / 1000)
                    tsData.value21 = Math.Round(Double.Parse(dr("fourfloor")) / 1000) + Math.Round(Double.Parse(dr("EW_FOUFR")) / 1000)





                    t.Add(tsData)
                    counter += 1
                End While
            End If
        End Using

        Return t
    End Function

    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load

        Dim c01 As String = ""
        Dim c02 As String = ""
        Dim c03 As String = ""
        Dim c04 As String = ""

        Dim workbook = New XLWorkbook("\\Svnas201\EXD06101\COMMON\輸出日次報告\16_INV在庫\INV在庫推移.xlsm")
        Dim ws1 As IXLWorksheet = workbook.Worksheet("コメント")

        '転記
        Label1.Text = ws1.Cell("D2").Value
        Label2.Text = ws1.Cell("D3").Value
        Label3.Text = ws1.Cell("D4").Value
        Label4.Text = ws1.Cell("D5").Value

        If Label1.Text <> "" Then
            Label1.Text = "未達要因 " & Label1.Text
        End If

        If Label2.Text <> "" Then
            Label2.Text = "未達要因 " & Label2.Text
        End If

        If Label3.Text <> "" Then
            Label3.Text = "未達要因 " & Label3.Text
        End If

        If Label4.Text <> "" Then
            Label4.Text = "未達要因 " & Label4.Text
        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click


        '前月分ダウンロードボタン押下
        Dim strFile As String = Format(Now, "yyyyMMdd") & "_インベントリ在庫推移.xlsx"
        Dim strPath As String = "C:\exp\cs_home\files\"
        Dim strChanged As String    'サーバー上のフルパス
        Dim strFileNm As String     'ファイル名

        Dim dtToday As DateTime = DateTime.Today

        Dim strFile0 As String = ""
        'ファイル検索
        strFile0 = Dir(strPath & "*_インベントリ在庫推移.xlsx")
        Do While strFile0 <> ""

            If strFile0 = Format(Now, "yyyyMMdd") & "_インベントリ在庫推移.xlsx" Then
            Else
                System.IO.File.Delete(strPath & strFile0)
            End If

            strFile0 = Dir()
        Loop

        Dim dt = GetNorthwindProductTable()
        Dim workbook = New XLWorkbook()
        Dim worksheet = workbook.Worksheets.Add(dt)

        worksheet.Style.Font.FontName = "Meiryo UI"
        worksheet.Style.Alignment.WrapText = False
        worksheet.Columns.AdjustToContents()
        worksheet.SheetView.FreezeRows(1)

        worksheet.Cell("A1").Value = "日付"
        worksheet.Cell("B1").Value = "１Ｆ在庫"
        worksheet.Cell("C1").Value = "３Ｆ在庫"
        worksheet.Cell("D1").Value = "４Ｆ在庫"
        worksheet.Cell("E1").Value = "１Ｆ外部在庫"
        worksheet.Cell("F1").Value = "３Ｆ外部在庫"
        worksheet.Cell("G1").Value = "４Ｆ外部在庫"
        worksheet.Cell("H1").Value = "１Ｆ上限"
        worksheet.Cell("I1").Value = "３Ｆ上限"
        worksheet.Cell("J1").Value = "４Ｆ上限"
        worksheet.Cell("K1").Value = "１Ｆ許容"
        worksheet.Cell("L1").Value = "３Ｆ許容"
        worksheet.Cell("M1").Value = "４Ｆ許容"
        worksheet.Cell("N1").Value = "溢れパレット"

        workbook.SaveAs(strPath & strFile)


        'ファイル名を取得する
        Dim strTxtFiles() As String = IO.Directory.GetFiles(strPath, Format(Now, "yyyyMMdd") & "_インベントリ在庫推移.xlsx")

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

        Dim dt = New DataTable("T_EXL_GRAPH_STOCK")

        Using conn = New SqlConnection(ConnectionString)
            Dim cmd = conn.CreateCommand()

            strSQL = strSQL & "SELECT * FROM [T_EXL_GRAPH_STOCK] ORDER BY DATE01 "


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


    '<WebMethod()>
    'Public Shared Function getTrafficSourceData2() As List(Of trafficSourceData)
    '    '    Public Shared Function getTrafficSourceData(ByVal gData As List(Of String)) As List(Of trafficSourceData)

    '    Dim t As List(Of trafficSourceData) = New List(Of trafficSourceData)()
    '    Dim arrColor As String() = New String() {"#231F20", "#FFC200", "#F44937", "#16F27E", "#FC9775"}

    '    Using cn As SqlConnection = New SqlConnection("Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager")
    '        Dim myQuery As String = "Select DATE01, firfloor, trdfloor, fourfloor, ew, cap, ovrpallet from T_EXL_GRAPH_STOCK "
    '        Dim cmd As SqlCommand = New SqlCommand()
    '        cmd.CommandText = myQuery
    '        cmd.CommandType = CommandType.Text
    '        '            cmd.Parameters.AddWithValue("@year", gData(0))
    '        '            cmd.Parameters.AddWithValue("@month", gData(1))
    '        cmd.Connection = cn
    '        cn.Open()
    '        Dim dr As SqlDataReader = cmd.ExecuteReader()

    '        If dr.HasRows Then
    '            Dim counter As Integer = 0

    '            While dr.Read()
    '                Dim tsData As trafficSourceData = New trafficSourceData()

    '                tsData.label = dr("DATE01").ToString()
    '                tsData.value = Math.Round(Double.Parse(dr("firfloor")) / 1000)
    '                tsData.value2 = Math.Round(Double.Parse(dr("trdfloor")) / 1000)
    '                tsData.value3 = Math.Round(Double.Parse(dr("fourfloor")) / 1000)
    '                tsData.value4 = Math.Round(Double.Parse(dr("ew")) / 1000)


    '                tsData.value5 = Math.Round(Double.Parse(dr("cap")) / 1000)
    '                tsData.value6 = Math.Round(Double.Parse(0) / 1000)
    '                tsData.value7 = Math.Round(Double.Parse(dr("ovrpallet")))

    '                '                   tsData.color = arrColor(counter)
    '                t.Add(tsData)
    '                counter += 1
    '            End While
    '        End If
    '    End Using

    '    Return t
    'End Function


End Class
