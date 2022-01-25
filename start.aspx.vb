Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.Common
Imports System.Web.UI.WebControls
Imports System.Activities.Expressions

Partial Class cs_home
    Inherits System.Web.UI.Page

    Public strRow As String
    Public strProcess As String

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Call GET_VAN_DATA()
        Dim strStatus As String = ""

        Call GET_STATUS(strStatus)

        Select Case strStatus
            Case "OK"
                Me.OKNGimg.ImageUrl = "images/OKtouka.png"
            Case "NG"
                Me.OKNGimg.ImageUrl = "images/NGtouka.png"

        End Select
    End Sub

    Private Sub GET_STATUS(ByRef strStatus As String)
        '概況のステータスを取得
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()

        strSQL = ""
        strSQL = strSQL & "SELECT CONTAINER, VANNING, FORTH_FLOOR, EED  "
        strSQL = strSQL & "FROM T_EXL_PORTAL_STATUS "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        While (dataread.Read())
            If Trim(dataread("CONTAINER")) = "OK" And Trim(dataread("VANNING")) = "OK" And
                Trim(dataread("FORTH_FLOOR")) = "OK" And Trim(dataread("EED")) = "OK" Then
                strStatus = "OK"
            Else
                strStatus = "NG"
            End If
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()


    End Sub
    Private Sub GET_VAN_DATA()
        'バンニングスケジュール情報を取得する
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""

        Dim lngHAll As Long = 0
        Dim lngHAf As Long = 0
        Dim lngHKd As Long = 0
        Dim lngHAll2 As Long = 0
        Dim lngHAf2 As Long = 0
        Dim lngHKd2 As Long = 0
        Dim lngU As Long = 0
        Dim lngU2 As Long = 0
        Dim lngAir As Long = 0
        Dim lngAir2 As Long = 0

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()

        strSQL = ""
        strSQL = strSQL & "SELECT * FROM T_EXL_POR_CNT ORDER BY DATA_CD "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        While (dataread.Read())
            Select Case dataread("DATA_CD")
                Case "001"
                    lngHAll = dataread("DATA_CNT")
                Case "002"
                    lngHAf = dataread("DATA_CNT")
                Case "003"
                    lngHKd = dataread("DATA_CNT")
                Case "004"
                    lngHAll2 = dataread("DATA_CNT")
                Case "005"
                    lngHAf2 = dataread("DATA_CNT")
                Case "006"
                    lngHKd2 = dataread("DATA_CNT")
                Case "007"
                    lngU = dataread("DATA_CNT")
                Case "008"
                    lngU2 = dataread("DATA_CNT")
                Case "009"
                    lngAir = dataread("DATA_CNT")
                Case "010"
                    lngAir2 = dataread("DATA_CNT")
            End Select
        End While

        Literal1.Text = StrConv(lngHAll, VbStrConv.Wide) + "件（ＡＦ" +
                        StrConv(lngHAf, VbStrConv.Wide) + "件、ＫＤ" +
                        StrConv(lngHKd, VbStrConv.Wide) + "件）／　" +
                        StrConv(lngHAll2, VbStrConv.Wide) + "件（ＡＦ" +
                        StrConv(lngHAf2, VbStrConv.Wide) + "件、ＫＤ" +
                        StrConv(lngHKd2, VbStrConv.Wide) + "件）"
        Literal2.Text = StrConv(lngU, VbStrConv.Wide) + "件　／　" +
                        StrConv(lngU2, VbStrConv.Wide) + "件"
        Literal3.Text = StrConv(lngAir, VbStrConv.Wide) + "件　／　" +
                        StrConv(lngAir2, VbStrConv.Wide) + "件"

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()

    End Sub
End Class
