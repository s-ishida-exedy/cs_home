
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
        Public Property color As String
        Public Property hightlight As String

    End Class


    <WebMethod()>
    Public Shared Function getTrafficSourceData() As List(Of trafficSourceData)
        '    Public Shared Function getTrafficSourceData(ByVal gData As List(Of String)) As List(Of trafficSourceData)

        Dim t As List(Of trafficSourceData) = New List(Of trafficSourceData)()
        Dim arrColor As String() = New String() {"#231F20", "#FFC200", "#F44937", "#16F27E", "#FC9775", "#5A69A6"}

        Using cn As SqlConnection = New SqlConnection("Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager")
            Dim myQuery As String = "select CUSTCODE,MINOU,RED,DELAY,ADJ,ACM from T_EXL_GRAPH_MINOU_KD WHERE CUSTCODE <>'' AND CUSTCODE <>'合計' "
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
                    tsData.value = dr("MINOU").ToString() / 1000000
                    tsData.label = dr("CUSTCODE").ToString()
                    tsData.value2 = dr("RED").ToString() / 1000000
                    tsData.value3 = dr("DELAY").ToString() / 1000000
                    tsData.value4 = dr("ADJ").ToString() / 1000000
                    tsData.value5 = dr("ACM").ToString() / 1000000
                    '                   tsData.color = arrColor(counter)
                    t.Add(tsData)
                    counter += 1
                End While
            End If
        End Using

        Return t
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim dt As New DataTable("GridView_Data")
        For Each cell As TableCell In GridView1.HeaderRow.Cells
            dt.Columns.Add(cell.Text)
        Next
        For Each row As GridViewRow In GridView1.Rows
            dt.Rows.Add()
            For i As Integer = 0 To row.Cells.Count - 1

                dt.Rows(dt.Rows.Count - 1)(i) = row.Cells(i).Text



            Next
        Next
        Using wb As New XLWorkbook()
            wb.Worksheets.Add(dt)
            wb.SaveAs("C:\Users\T43529\OneDrive - 株式会社エクセディ\デスクトップ\新ツール\未納_KD_" & Now.ToString(“yyyyMMddhh”) & ".xlsx")

        End Using


    End Sub


    Private Sub GridView1_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound


        If e.Row.RowType = DataControlRowType.DataRow Then

            If e.Row.Cells(1).Text <> "&nbsp;" Then



                e.Row.Cells(1).Text = Double.Parse(e.Row.Cells(1).Text).ToString("#,0.0")
                e.Row.Cells(2).Text = Double.Parse(e.Row.Cells(2).Text).ToString("#,0.0")
                e.Row.Cells(3).Text = Double.Parse(e.Row.Cells(3).Text).ToString("#,0.0")
                e.Row.Cells(4).Text = Double.Parse(e.Row.Cells(4).Text).ToString("#,0.0")
                e.Row.Cells(5).Text = Double.Parse(e.Row.Cells(5).Text).ToString("#,0.0")





            End If

        End If

    End Sub

End Class
