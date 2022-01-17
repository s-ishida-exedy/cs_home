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

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()

        strSQL = ""
        strSQL = strSQL & "SELECT * FROM T_EXL_VAN_SCH "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        While (dataread.Read())
            Literal1.Text = StrConv(dataread("H_T_ALL"), VbStrConv.Wide) + "件（ＡＦ" +
                            StrConv(dataread("H_T_AF"), VbStrConv.Wide) + "件、ＫＤ" +
                            StrConv(dataread("H_T_KD"), VbStrConv.Wide) + "件）／　" +
                            StrConv(dataread("H_N_ALL"), VbStrConv.Wide) + "件（ＡＦ" +
                            StrConv(dataread("H_N_AF"), VbStrConv.Wide) + "件、ＫＤ" +
                            StrConv(dataread("H_N_KD"), VbStrConv.Wide) + "件）"
            Literal2.Text = StrConv(dataread("U_T_ALL"), VbStrConv.Wide) + "件　／　" +
                            StrConv(dataread("U_N_ALL"), VbStrConv.Wide) + "件"
            Literal3.Text = StrConv(dataread("A_T_ALL"), VbStrConv.Wide) + "件　／　" +
                            StrConv(dataread("A_N_ALL"), VbStrConv.Wide) + "件"
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()

    End Sub
End Class
