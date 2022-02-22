
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

            Dim myQuery As String = "select TITLE,ZENSEN,TOUTYOU,TOUORI,ZAN,SENYOKU,TYOUYOKU from T_EXL_SINTYOKU "
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
                    tsData.value = Double.Parse(dr("ZENSEN"))
                    tsData.label = dr("TITLE").ToString()
                    tsData.value2 = Double.Parse(dr("TOUTYOU"))
                    tsData.value3 = Double.Parse(dr("TOUORI"))
                    tsData.value4 = Double.Parse(dr("ZAN"))
                    tsData.value5 = Double.Parse(dr("SENYOKU"))
                    tsData.value6 = Double.Parse(dr("TYOUYOKU"))
                    '                   tsData.color = arrColor(counter)
                    t.Add(tsData)
                    counter += 1
                End While
            End If




        End Using



        Return t
    End Function

    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load


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

        Dim dt1 As DateTime = DateTime.Now

        Dim ts1 As New TimeSpan(100, 0, 0, 0)
        Dim ts2 As New TimeSpan(100, 0, 0, 0)
        Dim dt2 As DateTime = dt1 + ts1
        Dim dt3 As DateTime = dt1 - ts1

        'データベース接続を開く
        cnn.Open()

        strSQL = ""
        strSQL = strSQL & "SELECT * FROM T_EXL_KINGAKU WHERE "
        strSQL = strSQL & "KBN = 'アフター' "


        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        While (dataread.Read())

            wval00 = dataread("Week1") / 1000
            wval01 = (dataread("Week2") / 1000) + wval00
            wval02 = (dataread("Week3") / 1000) + wval01
            wval03 = (dataread("Week4") / 1000) + wval02
            wval12 = (dataread("FLG01") / 1000) + wval03

        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()


        strSQL = ""
        strSQL = strSQL & "SELECT * FROM T_EXL_KINGAKU WHERE "
        strSQL = strSQL & "KBN = 'KD' "


        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        While (dataread.Read())


            wval04 = dataread("Week1") / 1000
            wval05 = (dataread("Week2") / 1000) + wval04
            wval06 = (dataread("Week3") / 1000) + wval05
            wval07 = (dataread("Week4") / 1000) + wval06
            wval13 = (dataread("FLG01") / 1000) + wval07

        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()




        strSQL = ""
        strSQL = strSQL & "SELECT * FROM T_EXL_KINGAKU WHERE "
        strSQL = strSQL & "KBN = '上野' "


        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        While (dataread.Read())


            wval08 = dataread("Week1") / 1000
            wval09 = (dataread("Week2") / 1000) + wval08
            wval10 = (dataread("Week3") / 1000) + wval09
            wval11 = (dataread("Week4") / 1000) + wval10
            wval14 = (dataread("FLG01") / 1000) + wval11

        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()

        cnn.Close()
        cnn.Dispose()
        TextBox1.Text = wval00
        TextBox2.Text = wval01
        TextBox3.Text = wval02
        TextBox4.Text = wval03
        TextBox5.Text = wval04
        TextBox6.Text = wval05
        TextBox7.Text = wval06
        TextBox8.Text = wval07
        TextBox9.Text = wval08
        TextBox10.Text = wval09
        TextBox11.Text = wval10
        TextBox12.Text = wval11
        TextBox13.Text = wval12
        TextBox14.Text = wval13
        TextBox15.Text = wval14



    End Sub
End Class
