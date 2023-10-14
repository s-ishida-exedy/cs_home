
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

        Public Property labelZ1 As String
        Public Property labelZ2 As String

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

        Public Property valueZ1 As String
        Public Property valueZ2 As String


        Public Property color As String
        Public Property color2 As String
        Public Property hightlight As String

    End Class




    <WebMethod()>
    Public Shared Function getTrafficSourceData() As List(Of trafficSourceData)
        '    Public Shared Function getTrafficSourceData(ByVal gData As List(Of String)) As List(Of trafficSourceData)

        Dim t As List(Of trafficSourceData) = New List(Of trafficSourceData)()
        Dim arrColor As String() = New String() {"#231F20", "#FFC200", "#F44937", "#16F27E", "#FC9775", "#5A69A6"}

        Using cn As SqlConnection = New SqlConnection("Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager")

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
                    tsData.value = Math.Round(Double.Parse(dr("ZENSEN")), MidpointRounding.AwayFromZero)
                    tsData.label = Replace(dr("TITLE").ToString(), "ー", "")
                    tsData.value2 = Math.Round(Double.Parse(dr("TOUTYOU")), MidpointRounding.AwayFromZero)
                    tsData.value3 = Math.Round(Double.Parse(dr("TOUORI")), MidpointRounding.AwayFromZero)
                    tsData.value4 = Math.Round(Double.Parse(dr("ZAN")), MidpointRounding.AwayFromZero)
                    tsData.value5 = Math.Round(Double.Parse(dr("SENYOKU")), MidpointRounding.AwayFromZero)
                    tsData.value6 = Math.Round(Double.Parse(dr("TYOUYOKU")), MidpointRounding.AwayFromZero)
                    '                   tsData.color = arrColor(counter)
                    t.Add(tsData)
                    counter += 1
                End While
            End If




        End Using



        Return t
    End Function

    <WebMethod()>
    Public Shared Function getTrafficSourceData2() As List(Of trafficSourceData)
        '    Public Shared Function getTrafficSourceData(ByVal gData As List(Of String)) As List(Of trafficSourceData)

        Dim tt As List(Of trafficSourceData) = New List(Of trafficSourceData)()

        Dim arrColor As String() = New String() {"#0068B7", "#F39800", "#FFF100", "#009944", "#E60012", "#1D2088", "#920783", "#16F27E", "#FC9775", "#5A69A6"} '
        Using cn2 As SqlConnection = New SqlConnection("Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager")

            Dim myQuery2 As String = "select KBN,ZANKIN from T_EXL_GRAPH_KDSINTYOKU "
            Dim cmd2 As SqlCommand = New SqlCommand()
            cmd2.CommandText = myQuery2
            cmd2.CommandType = CommandType.Text
            cmd2.Connection = cn2
            cn2.Open()
            Dim dr2 As SqlDataReader = cmd2.ExecuteReader()

            If dr2.HasRows Then
                Dim counter2 As Integer = 0

                While dr2.Read()
                    Dim tsData2 As trafficSourceData = New trafficSourceData()
                    tsData2.valueZ1 = Double.Parse(dr2("ZANKIN"))
                    tsData2.labelZ1 = dr2("KBN").ToString()

                    tsData2.color = arrColor(counter2)
                    tt.Add(tsData2)
                    counter2 += 1
                End While
            End If




        End Using



        Return tt
    End Function

    <WebMethod()>
    Public Shared Function getTrafficSourceData3() As List(Of trafficSourceData)
        '    Public Shared Function getTrafficSourceData(ByVal gData As List(Of String)) As List(Of trafficSourceData)

        Dim ttt As List(Of trafficSourceData) = New List(Of trafficSourceData)()

        Dim arrColor2 As String() = New String() {"#0068B7", "#F39800", "#FFF100", "#009944", "#E60012", "#1D2088", "#920783", "#16F27E", "#FC9775", "#5A69A6"} '
        Using cn3 As SqlConnection = New SqlConnection("Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager")

            Dim myQuery3 As String = "select KBN,ZANKIN from T_EXL_GRAPH_AMSINTYOKU "
            Dim cmd3 As SqlCommand = New SqlCommand()
            cmd3.CommandText = myQuery3
            cmd3.CommandType = CommandType.Text
            cmd3.Connection = cn3
            cn3.Open()
            Dim dr3 As SqlDataReader = cmd3.ExecuteReader()

            If dr3.HasRows Then
                Dim counter3 As Integer = 0

                While dr3.Read()
                    Dim tsData3 As trafficSourceData = New trafficSourceData()
                    tsData3.valueZ2 = Double.Parse(dr3("ZANKIN"))
                    tsData3.labelZ2 = dr3("KBN").ToString()

                    tsData3.color2 = arrColor2(counter3)
                    ttt.Add(tsData3)
                    counter3 += 1
                End While
            End If




        End Using



        Return ttt
    End Function
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
        Dim sumval5 As Integer
        Dim sumval6 As Integer
        Dim sumval7 As Integer
        Dim sumval8 As Integer
        Dim sumval9 As Integer
        Dim sumval10 As Integer
        Dim sumval11 As Integer
        Dim sumval12 As Integer
        Dim sumval13 As Integer
        Dim sumval14 As Integer
        Dim sumval15 As Integer
        Dim sumval16 As Integer
        Dim sumval17 As Integer

        Dim vala As Integer = 0
        Dim valk As Integer = 0
        Dim valk01 As Integer = 0
        Dim valk02 As Integer = 0
        Dim valu As Integer = 0
        Dim valu01 As Integer = 0
        Dim valu02 As Integer = 0


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



        TextBox1.Text = Int(wval00)
        TextBox2.Text = Int(wval01)
        TextBox3.Text = Int(wval02)
        TextBox4.Text = Int(wval03)
        TextBox5.Text = Int(wval04)
        TextBox6.Text = Int(wval05)
        TextBox7.Text = Int(wval06)
        TextBox8.Text = Int(wval07)
        TextBox9.Text = Int(wval08)
        TextBox10.Text = Int(wval09)
        TextBox11.Text = Int(wval10)
        TextBox12.Text = Int(wval11)
        TextBox13.Text = Int(wval12)
        TextBox14.Text = Int(wval13)
        TextBox15.Text = Int(wval14)





        strSQL = ""
        strSQL = strSQL & "SELECT * FROM T_EXL_HYOU_SINTYOKU WHERE "
        strSQL = strSQL & "KBN = 'アフタ' "


        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        While (dataread.Read())

            Label1.Text = dataread("KBN")
            Label2.Text = Double.Parse(dataread("BO")).ToString("#,0")
            Label3.Text = Double.Parse(dataread("ODR_T_T")).ToString("#,0")
            Label4.Text = Double.Parse(dataread("ODR_T_O")).ToString("#,0")
            Label5.Text = Double.Parse(dataread("ODR_T_S")).ToString("#,0")
            Label6.Text = Double.Parse(dataread("ODR_T_T_N")).ToString("#,0")
            Label7.Text = Double.Parse(dataread("SHIP_SEN")).ToString("#,0")
            Label8.Text = Double.Parse(dataread("SHIP_T")).ToString("#,0")
            Label9.Text = Double.Parse(dataread("SHIP_O")).ToString("#,0")
            Label10.Text = Double.Parse(dataread("SHIP_S")).ToString("#,0")
            Label11.Text = Double.Parse(dataread("SHIP_SEN_N")).ToString("#,0")
            Label12.Text = Double.Parse(dataread("SHIP_SUM")).ToString("#,0")
            Label13.Text = Double.Parse(dataread("ZAN_T")).ToString("#,0")
            Label14.Text = Double.Parse(dataread("ZAN_T_T_N")).ToString("#,0")
            Label15.Text = Double.Parse(dataread("ZAN_SUM")).ToString("#,0")

            sumval1 = Double.Parse(dataread("BO")).ToString("N")
            sumval2 = Double.Parse(dataread("ODR_T_T")).ToString("N")
            sumval3 = Double.Parse(dataread("ODR_T_O")).ToString("N")
            sumval4 = Double.Parse(dataread("ODR_T_S")).ToString("N")
            sumval5 = Double.Parse(dataread("ODR_T_T_N")).ToString("N")
            sumval6 = Double.Parse(dataread("SHIP_SEN")).ToString("N")
            sumval7 = Double.Parse(dataread("SHIP_T")).ToString("N")
            sumval8 = Double.Parse(dataread("SHIP_O")).ToString("N")
            sumval9 = Double.Parse(dataread("SHIP_S")).ToString("N")
            sumval10 = Double.Parse(dataread("SHIP_SEN_N")).ToString("N")
            sumval11 = Double.Parse(dataread("SHIP_SUM")).ToString("N")
            sumval12 = Double.Parse(dataread("ZAN_T")).ToString("N")
            sumval13 = Double.Parse(dataread("ZAN_T_T_N")).ToString("N")
            sumval14 = Double.Parse(dataread("ZAN_SUM")).ToString("N")


        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()



        vala = sumval4 + sumval5


        strSQL = ""
        strSQL = strSQL & "SELECT * FROM T_EXL_HYOU_SINTYOKU WHERE "
        strSQL = strSQL & "KBN = 'KD' "


        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        While (dataread.Read())

            Label16.Text = dataread("KBN")
            Label17.Text = Double.Parse(dataread("BO")).ToString("#,0")
            Label18.Text = Double.Parse(dataread("ODR_T_T")).ToString("#,0")
            Label19.Text = Double.Parse(dataread("ODR_T_O")).ToString("#,0")
            Label20.Text = Double.Parse(dataread("ODR_T_S")).ToString("#,0")
            Label21.Text = Double.Parse(dataread("ODR_T_T_N")).ToString("#,0")
            Label22.Text = Double.Parse(dataread("SHIP_SEN")).ToString("#,0")
            Label23.Text = Double.Parse(dataread("SHIP_T")).ToString("#,0")
            Label24.Text = Double.Parse(dataread("SHIP_O")).ToString("#,0")
            Label25.Text = Double.Parse(dataread("SHIP_S")).ToString("#,0")
            Label26.Text = Double.Parse(dataread("SHIP_SEN_N")).ToString("#,0")
            Label27.Text = Double.Parse(dataread("SHIP_SUM")).ToString("#,0")
            Label28.Text = Double.Parse(dataread("ZAN_T")).ToString("#,0")
            Label29.Text = Double.Parse(dataread("ZAN_T_T_N")).ToString("#,0")
            Label30.Text = Double.Parse(dataread("ZAN_SUM")).ToString("#,0")

            sumval1 = sumval1 + Double.Parse(dataread("BO")).ToString("N")
            sumval2 = sumval2 + Double.Parse(dataread("ODR_T_T")).ToString("N")
            sumval3 = sumval3 + Double.Parse(dataread("ODR_T_O")).ToString("N")
            sumval4 = sumval4 + Double.Parse(dataread("ODR_T_S")).ToString("N")
            sumval5 = sumval5 + Double.Parse(dataread("ODR_T_T_N")).ToString("N")
            sumval6 = sumval6 + Double.Parse(dataread("SHIP_SEN")).ToString("N")
            sumval7 = sumval7 + Double.Parse(dataread("SHIP_T")).ToString("N")
            sumval8 = sumval8 + Double.Parse(dataread("SHIP_O")).ToString("N")
            sumval9 = sumval9 + Double.Parse(dataread("SHIP_S")).ToString("N")
            sumval10 = sumval10 + Double.Parse(dataread("SHIP_SEN_N")).ToString("N")
            sumval11 = sumval11 + Double.Parse(dataread("SHIP_SUM")).ToString("N")
            sumval12 = sumval12 + Double.Parse(dataread("ZAN_T")).ToString("N")
            sumval13 = sumval13 + Double.Parse(dataread("ZAN_T_T_N")).ToString("N")
            sumval14 = sumval14 + Double.Parse(dataread("ZAN_SUM")).ToString("N")

            valk01 = Double.Parse(dataread("ODR_T_S")).ToString("N")
            valk02 = Double.Parse(dataread("ODR_T_T_N")).ToString("N")

        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()

        valk = valk01 + valk02

        strSQL = ""
        strSQL = strSQL & "SELECT * FROM T_EXL_HYOU_SINTYOKU WHERE "
        strSQL = strSQL & "KBN = '上野' "


        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        While (dataread.Read())

            Label31.Text = dataread("KBN")
            Label32.Text = Double.Parse(dataread("BO")).ToString("#,0")
            Label33.Text = Double.Parse(dataread("ODR_T_T")).ToString("#,0")
            Label34.Text = Double.Parse(dataread("ODR_T_O")).ToString("#,0")
            Label35.Text = Double.Parse(dataread("ODR_T_S")).ToString("#,0")
            Label36.Text = Double.Parse(dataread("ODR_T_T_N")).ToString("#,0")
            Label37.Text = Double.Parse(dataread("SHIP_SEN")).ToString("#,0")
            Label38.Text = Double.Parse(dataread("SHIP_T")).ToString("#,0")
            Label39.Text = Double.Parse(dataread("SHIP_O")).ToString("#,0")
            Label40.Text = Double.Parse(dataread("SHIP_S")).ToString("#,0")
            Label41.Text = Double.Parse(dataread("SHIP_SEN_N")).ToString("#,0")
            Label42.Text = Double.Parse(dataread("SHIP_SUM")).ToString("#,0")
            Label43.Text = Double.Parse(dataread("ZAN_T")).ToString("#,0")
            Label44.Text = Double.Parse(dataread("ZAN_T_T_N")).ToString("#,0")
            Label45.Text = Double.Parse(dataread("ZAN_SUM")).ToString("#,0")

            sumval1 = sumval1 + Double.Parse(dataread("BO")).ToString("N")
            sumval2 = sumval2 + Double.Parse(dataread("ODR_T_T")).ToString("N")
            sumval3 = sumval3 + Double.Parse(dataread("ODR_T_O")).ToString("N")
            sumval4 = sumval4 + Double.Parse(dataread("ODR_T_S")).ToString("N")
            sumval5 = sumval5 + Double.Parse(dataread("ODR_T_T_N")).ToString("N")
            sumval6 = sumval6 + Double.Parse(dataread("SHIP_SEN")).ToString("N")
            sumval7 = sumval7 + Double.Parse(dataread("SHIP_T")).ToString("N")
            sumval8 = sumval8 + Double.Parse(dataread("SHIP_O")).ToString("N")
            sumval9 = sumval9 + Double.Parse(dataread("SHIP_S")).ToString("N")
            sumval10 = sumval10 + Double.Parse(dataread("SHIP_SEN_N")).ToString("N")
            sumval11 = sumval11 + Double.Parse(dataread("SHIP_SUM")).ToString("N")
            sumval12 = sumval12 + Double.Parse(dataread("ZAN_T")).ToString("N")
            sumval13 = sumval13 + Double.Parse(dataread("ZAN_T_T_N")).ToString("N")
            sumval14 = sumval14 + Double.Parse(dataread("ZAN_SUM")).ToString("N")

            valu01 = Double.Parse(dataread("ODR_T_S")).ToString("N")
            valu02 = Double.Parse(dataread("ODR_T_T_N")).ToString("N")

        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()

        valu = valu01 + valu02

        Label65.Text = sumval1.ToString("#,0")
        Label66.Text = sumval2.ToString("#,0")
        Label67.Text = sumval3.ToString("#,0")
        Label68.Text = sumval4.ToString("#,0")
        Label69.Text = sumval5.ToString("#,0")
        Label70.Text = sumval6.ToString("#,0")
        Label71.Text = sumval7.ToString("#,0")
        Label72.Text = sumval8.ToString("#,0")
        Label73.Text = sumval9.ToString("#,0")
        Label74.Text = sumval10.ToString("#,0")
        Label75.Text = sumval11.ToString("#,0")
        Label76.Text = sumval12.ToString("#,0")
        Label77.Text = sumval13.ToString("#,0")
        Label78.Text = sumval14.ToString("#,0")




        If Double.Parse(vala) > Double.Parse(valk) Then

            sumval17 = Double.Parse(vala)

        Else
            sumval17 = Double.Parse(valk)

        End If

        If Double.Parse(valu) > Double.Parse(sumval17) Then

            sumval17 = Double.Parse(valu)

        Else


        End If

        Dim a As Integer

        a = sumval17 / 100000
        a = (a * 100) + 100

        TextBox16.Text = a



        cnn.Close()
        cnn.Dispose()


    End Sub
End Class
