
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




Partial Class yuusen
    Inherits System.Web.UI.Page

    Public strRow As String
    Public strProcess As String


    Public Class trafficSourceData
        Public Property label As String
        Public Property label2 As String
        Public Property label3 As String

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
        Public Property value23 As String
        Public Property value24 As String
        Public Property value25 As String


        Public Property color As String
        Public Property hightlight As String

    End Class




    <WebMethod()>
    Public Shared Function getTrafficSourceData() As List(Of trafficSourceData)
        '    Public Shared Function getTrafficSourceData(ByVal gData As List(Of String)) As List(Of trafficSourceData)

        Dim t As List(Of trafficSourceData) = New List(Of trafficSourceData)()
        Dim arrColor As String() = New String() {"#231F20", "#FFC200", "#F44937", "#16F27E", "#FC9775", "#5A69A6"}

        Using cn As SqlConnection = New SqlConnection("Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager")

            Dim myQuery As String = "select CUSTCODE,MINOU,RED,DELAY,ADJ,ACM,FLG01 from T_EXL_GRAPH_MINOU_AM WHERE CUSTCODE IN('K60C','K53Y','K51A') "
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
                    tsData.value = Double.Parse(dr("MINOU")) / 1000000
                    tsData.label = dr("CUSTCODE").ToString()
                    tsData.value2 = Double.Parse(dr("RED")) / 1000000
                    tsData.value3 = Double.Parse(dr("DELAY")) / 1000000
                    tsData.value4 = Double.Parse(dr("ADJ")) / 1000000
                    tsData.value5 = Double.Parse(dr("ACM")) / 1000000
                    tsData.value6 = dr("FLG01")
                    '                   tsData.color = arrColor(counter)
                    t.Add(tsData)
                    counter += 1
                End While
            End If




        End Using



        Return t
    End Function

    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load

        'テーブルから週ごとの金額を取得
        'If IsPostBack = True Then

        '    TextBox1.Text = "aaaaaaa2"
        'Else

        '    TextBox1.Text = "aaaaaaa"

        'End If






    End Sub
End Class
