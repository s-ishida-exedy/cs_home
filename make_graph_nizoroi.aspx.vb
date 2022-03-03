
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
        Public Property color As String
        Public Property hightlight As String

    End Class


    <WebMethod()>
    Public Shared Function getTrafficSourceData() As List(Of trafficSourceData)
        '    Public Shared Function getTrafficSourceData(ByVal gData As List(Of String)) As List(Of trafficSourceData)

        Dim t As List(Of trafficSourceData) = New List(Of trafficSourceData)()
        Dim arrColor As String() = New String() {"#231F20", "#FFC200", "#F44937", "#16F27E", "#FC9775"}

        Using cn As SqlConnection = New SqlConnection("Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager")
            Dim myQuery As String = "select TITLE01,RDY02,RDY_BO02,VAN02,VAN_BO02,INVTRY,NON_INVTRY,RDY01,RDY_BO01,VAN01,VAN_BO01,NO_ARI01,NO_NASI01 from T_EXL_GRAPH_NIZOROI "
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
                    tsData.value = (Double.Parse(dr("RDY02")) + Double.Parse(dr("RDY_BO02"))) * 100
                    tsData.label = dr("TITLE01").ToString()
                    tsData.value2 = (Double.Parse(dr("VAN02")) + Double.Parse(dr("VAN_BO02"))) * 100
                    tsData.value3 = Double.Parse(dr("INVTRY")) * 100
                    tsData.value4 = Double.Parse(dr("NON_INVTRY")) * 100


                    tsData.value5 = Integer.Parse(dr("RDY01")) + Integer.Parse(dr("RDY_BO01"))
                    tsData.value6 = Integer.Parse(dr("VAN01")) + Integer.Parse(dr("VAN_BO01"))
                    tsData.value7 = Integer.Parse(dr("NO_ARI01"))
                    tsData.value8 = Integer.Parse(dr("NO_NASI01"))

                    '                   tsData.color = arrColor(counter)
                    t.Add(tsData)
                    counter += 1
                End While
            End If
        End Using

        Return t
    End Function



End Class
