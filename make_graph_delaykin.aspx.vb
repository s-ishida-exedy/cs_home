
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.Common

Imports System.Text
Imports System.Web.Services
Imports System.Configuration

Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls

Imports System.Console
Imports ClosedXML.Excel
Imports System.IO
Imports MailKit.Net.Smtp
Imports MailKit.Security
Imports MimeKit
Imports MimeKit.Text
Imports mod_function



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

        Public Property value10 As String
        Public Property value11 As String
        Public Property value12 As String
        Public Property value13 As String


        Public Property color As String
        Public Property hightlight As String

    End Class


    <WebMethod()>
    Public Shared Function getTrafficSourceData() As List(Of trafficSourceData)

        Dim t As List(Of trafficSourceData) = New List(Of trafficSourceData)()
        Dim arrColor As String() = New String() {"#Ffb6c1", "#ADD8E6", "#90ee90", "#16F27E", "#FC9775", "#5A69A6"}

        Using cn As SqlConnection = New SqlConnection("Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager")
            Dim myQuery As String = "select * from T_EXL_GRAPH_DELAYKIN "
            Dim cmd As SqlCommand = New SqlCommand()
            cmd.CommandText = myQuery
            cmd.CommandType = CommandType.Text
            cmd.Connection = cn
            cn.Open()
            Dim dr As SqlDataReader = cmd.ExecuteReader()

            If dr.HasRows Then
                Dim counter As Integer = 0

                While dr.Read()
                    Dim tsData As trafficSourceData = New trafficSourceData()
                    tsData.label = dr("MONTH01").ToString()



                    tsData.value = Double.Parse(dr("KIN_ODR_A").ToString() / 1000000).ToString("#,0")
                    tsData.value2 = Double.Parse(dr("KIN_DLY_A").ToString() / 1000000).ToString("#,0")

                    tsData.value3 = Double.Parse(dr("KIN_ODR_I").ToString() / 1000000).ToString("#,0")
                    tsData.value4 = Double.Parse(dr("KIN_DLY_I").ToString() / 1000000).ToString("#,0")

                    tsData.value5 = Double.Parse(dr("KIN_ODR_M").ToString() / 1000000).ToString("#,0")
                    tsData.value6 = Double.Parse(dr("KIN_DLY_M").ToString() / 1000000).ToString("#,0")

                    tsData.value7 = Double.Parse(dr("KIN_ODR_TOTAL").ToString() / 1000000).ToString("#,0")
                    tsData.value8 = Double.Parse(dr("KIN_DLY_TOTAL").ToString() / 1000000).ToString("#,0")


                    tsData.value10 = Double.Parse(dr("KIN_DLY_TOTAL").ToString() / dr("KIN_ODR_A").ToString() / 1000000).ToString("#,0")
                    tsData.value11 = Double.Parse(dr("KIN_DLY_TOTAL").ToString() / dr("KIN_ODR_I").ToString() / 1000000).ToString("#,0")
                    tsData.value12 = Double.Parse(dr("KIN_DLY_TOTAL").ToString() / dr("KIN_ODR_M").ToString() / 1000000).ToString("#,0")
                    tsData.value13 = (Double.Parse(dr("KIN_DLY_TOTAL")) / Double.Parse(dr("KIN_ODR_TOTAL"))).ToString("N") * 100



                    t.Add(tsData)
                    counter += 1
                End While
            End If
        End Using

        Return t
    End Function

    'Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

    '    Dim dt As New DataTable("GridView_Data")
    '    For Each cell As TableCell In GridView1.HeaderRow.Cells
    '        dt.Columns.Add(cell.Text)
    '    Next
    '    For Each row As GridViewRow In GridView1.Rows
    '        dt.Rows.Add()
    '        For i As Integer = 0 To row.Cells.Count - 1
    '            If i = 0 Then
    '                dt.Rows(dt.Rows.Count - 1)(i) = row.Cells(i).Text

    '            Else

    '                dt.Rows(dt.Rows.Count - 1)(i) = Double.Parse(Replace(row.Cells(i).Text, "&nbsp;", 0)).ToString("#,0")

    '            End If
    '        Next
    '    Next
    '    Using wb As New XLWorkbook()
    '        wb.Worksheets.Add(dt)
    '        wb.SaveAs("C:\Users\T43529\OneDrive - 株式会社エクセディ\デスクトップ\新ツール\未納_アフタ_" & Now.ToString(“yyyyMMddhh”) & ".xlsx")

    '    End Using


    'End Sub


    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load

        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand
        Dim strSQL As String = ""
        Dim ivno As String = ""
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand

        Dim wval00 As String = ""
        Dim wval01 As String = ""
        Dim wval02 As String = ""
        Dim wval03 As String = ""
        Dim wval04 As String = ""
        Dim wval05 As String = ""
        Dim wval06 As String = ""
        Dim wval07 As String = ""
        Dim wval08 As String = ""
        Dim wval09 As String = ""
        Dim wval10 As String = ""
        Dim wval11 As String = ""
        Dim wval12 As String = ""
        Dim wval13 As String = ""
        Dim wval14 As String = ""
        Dim wval15 As String = ""

        Dim sumval1 As Double
        Dim sumval2 As Double
        Dim sumval3 As Double
        Dim sumval4 As Double




        Dim dt1 As DateTime = DateTime.Now

        Dim ts1 As New TimeSpan(100, 0, 0, 0)
        Dim ts2 As New TimeSpan(100, 0, 0, 0)
        Dim dt2 As DateTime = dt1 + ts1
        Dim dt3 As DateTime = dt1 - ts1

        'データベース接続を開く
        cnn.Open()

        strSQL = ""
        strSQL = strSQL & "SELECT T_EXL_DELAY_FORCAST.KBN, Sum(T_EXL_DELAY_FORCAST.KIN) AS KIN01 "
        strSQL = strSQL & "FROM T_EXL_DELAY_FORCAST "
        strSQL = strSQL & "WHERE T_EXL_DELAY_FORCAST.KBN = 'MT' "
        strSQL = strSQL & "GROUP BY T_EXL_DELAY_FORCAST.KBN "


        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        While (dataread.Read())
            Label4.Text = Double.Parse(Double.Parse(dataread("KIN01"))).ToString("#,0")
            sumval1 = Double.Parse(Double.Parse(dataread("KIN01"))).ToString("#,0")
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()

        strSQL = ""
        strSQL = strSQL & "SELECT T_EXL_DELAY_FORCAST.KBN, Sum(T_EXL_DELAY_FORCAST.KIN) AS KIN01 "
        strSQL = strSQL & "FROM T_EXL_DELAY_FORCAST "
        strSQL = strSQL & "WHERE T_EXL_DELAY_FORCAST.KBN = 'TS' "
        strSQL = strSQL & "GROUP BY T_EXL_DELAY_FORCAST.KBN "


        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        While (dataread.Read())
            Label6.Text = Double.Parse(Double.Parse(dataread("KIN01"))).ToString("#,0")
            sumval2 = Double.Parse(Double.Parse(dataread("KIN01"))).ToString("#,0")

        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()


        strSQL = ""
        strSQL = strSQL & "SELECT T_EXL_DELAY_FORCAST.KBN, Sum(T_EXL_DELAY_FORCAST.KIN) AS KIN01 "
        strSQL = strSQL & "FROM T_EXL_DELAY_FORCAST "
        strSQL = strSQL & "WHERE T_EXL_DELAY_FORCAST.KBN = 'AT' "
        strSQL = strSQL & "GROUP BY T_EXL_DELAY_FORCAST.KBN "


        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        While (dataread.Read())
            Label8.Text = Double.Parse(Double.Parse(dataread("KIN01"))).ToString("#,0")
            sumval3 = Double.Parse(Double.Parse(dataread("KIN01"))).ToString("#,0")
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()

        Label10.Text = Double.Parse(Double.Parse(sumval1 + sumval2 + sumval3)).ToString("#,0")




    End Sub

    Private Sub GridView1_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound


        If e.Row.RowType = DataControlRowType.DataRow Then


            e.Row.Cells(1).Text = Double.Parse(e.Row.Cells(1).Text).ToString("#,0")
            e.Row.Cells(2).Text = Double.Parse(e.Row.Cells(2).Text).ToString("#,0")
            e.Row.Cells(3).Text = Double.Parse(e.Row.Cells(3).Text).ToString("#,0")
            e.Row.Cells(4).Text = Double.Parse(e.Row.Cells(4).Text).ToString("#,0")
            e.Row.Cells(5).Text = Double.Parse(e.Row.Cells(5).Text).ToString("#,0")
            e.Row.Cells(6).Text = Double.Parse(e.Row.Cells(6).Text).ToString("#,0")
            e.Row.Cells(7).Text = Double.Parse(e.Row.Cells(7).Text).ToString("#,0")
            e.Row.Cells(8).Text = Double.Parse(e.Row.Cells(8).Text).ToString("#,0")
            e.Row.Cells(10).Text = Double.Parse(e.Row.Cells(7).Text).ToString("#,0")
            e.Row.Cells(11).Text = Double.Parse(e.Row.Cells(8).Text).ToString("#,0")

            e.Row.Cells(3).Text = (e.Row.Cells(2).Text / e.Row.Cells(1).Text).ToString("0.00%")
            e.Row.Cells(6).Text = (e.Row.Cells(5).Text / e.Row.Cells(4).Text).ToString("0.00%")
            e.Row.Cells(9).Text = (e.Row.Cells(8).Text / e.Row.Cells(7).Text).ToString("0.00%")
            e.Row.Cells(12).Text = (e.Row.Cells(11).Text / e.Row.Cells(10).Text).ToString("0.00%")

            e.Row.Cells(1).BackColor = Drawing.Color.LightPink
            e.Row.Cells(2).BackColor = Drawing.Color.LightPink
            e.Row.Cells(3).BackColor = Drawing.Color.LightPink
            e.Row.Cells(3).ForeColor = Drawing.Color.Red
            e.Row.Cells(3).Font.Bold = True

            e.Row.Cells(4).BackColor = Drawing.Color.LightBlue
            e.Row.Cells(5).BackColor = Drawing.Color.LightBlue
            e.Row.Cells(6).BackColor = Drawing.Color.LightBlue
            e.Row.Cells(6).ForeColor = Drawing.Color.Red
            e.Row.Cells(6).Font.Bold = True

            e.Row.Cells(7).BackColor = Drawing.Color.LightGreen
            e.Row.Cells(8).BackColor = Drawing.Color.LightGreen
            e.Row.Cells(9).BackColor = Drawing.Color.LightGreen
            e.Row.Cells(9).ForeColor = Drawing.Color.Red
            e.Row.Cells(9).Font.Bold = True

            e.Row.Cells(12).ForeColor = Drawing.Color.Red
            e.Row.Cells(12).Font.Bold = True

        End If

    End Sub

    Private Sub GridView2_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView2.RowDataBound



        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand
        Dim strSQL As String = ""
        Dim ivno As String = ""
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand

        Dim cnt01 As Long
        cnt01 = 0



        If e.Row.RowType = DataControlRowType.DataRow Then


            'データベース接続を開く
            cnn.Open()

            strSQL = ""
            strSQL = strSQL & "SELECT COUNT(T_EXL_SHIPPINGMEMOLIST.REV_STATUS) AS CNT "
            strSQL = strSQL & "FROM T_EXL_SHIPPINGMEMOLIST "
            strSQL = strSQL & "WHERE T_EXL_SHIPPINGMEMOLIST.BOOKING_NO = '" & Trim(e.Row.Cells(10).Text) & "' "
            strSQL = strSQL & "AND T_EXL_SHIPPINGMEMOLIST.REV_STATUS IN ('月またぎ','月またぎP') "


            'ＳＱＬコマンド作成 
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行 
            dataread = dbcmd.ExecuteReader()

            While (dataread.Read())
                cnt01 = dataread("CNT")
            End While

            'クローズ処理 
            dataread.Close()
            dbcmd.Dispose()

            If cnt01 > 0 Then

                e.Row.Cells(0).BackColor = Drawing.Color.Red

            End If


            e.Row.Cells(9).Text = Double.Parse(e.Row.Cells(9).Text).ToString("#,0")

            If e.Row.Cells(0).Text = "MT" Then
                e.Row.BackColor = Drawing.Color.LightGreen
            ElseIf e.Row.Cells(0).Text = "AT" Then
                e.Row.BackColor = Drawing.Color.LightPink
            ElseIf e.Row.Cells(0).Text = "TS" Then
                e.Row.BackColor = Drawing.Color.LightBlue
            End If

        End If

    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        '前月分ダウンロードボタン押下
        Dim strFile As String = "月またぎ明細.xls"
        Dim strPath As String = "C:\exp\cs_home\files\"
        Dim strChanged As String    'サーバー上のフルパス
        Dim strFileNm As String     'ファイル名

        Dim dtToday As DateTime = DateTime.Today



        'ファイル名を取得する
        Dim strTxtFiles() As String = System.IO.Directory.GetFiles(strPath, strFile)

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


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        '前月分ダウンロードボタン押下
        Dim strFile As String = "遅延予測（実績）明細.xls"
        Dim strPath As String = "C:\exp\cs_home\files\"
        Dim strChanged As String    'サーバー上のフルパス
        Dim strFileNm As String     'ファイル名

        Dim dtToday As DateTime = DateTime.Today



        'ファイル名を取得する
        Dim strTxtFiles() As String = System.IO.Directory.GetFiles(strPath, strFile)

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
End Class
