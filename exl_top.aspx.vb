Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.Common
Partial Class cs_home
    Inherits System.Web.UI.Page

    Public strRow As String
    Public strProcess As String



    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim strStatus As String = ""

        Call GET_PORTAL_DATA()

        Call GET_STATUS(strStatus)

        Select Case strStatus
            Case "OK"
                Me.OKNGimg.ImageUrl = "images/OKtouka.png"
            Case "NG"
                Me.OKNGimg.ImageUrl = "images/NGtouka.png"
        End Select
    End Sub

    Private Sub GET_PORTAL_DATA()
        '概況ステータス情報を取得する
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()

        strSQL = ""
        strSQL = strSQL & "SELECT * FROM T_EXL_POR_STATUS "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        While (dataread.Read())
            Select Case dataread("DATA_CD")
                Case "001"
                    'Literal1.Text = Trim(StrConv(dataread("DATA_OKNG"), VbStrConv.Wide))
                    'Literal2.Text = "更新日時：" + dataread("DATA_UPD")
                Case "002"
                    Literal3.Text = Trim(StrConv(dataread("DATA_OKNG"), VbStrConv.Wide))
                    Literal4.Text = "更新日時：" + dataread("DATA_UPD")
                Case "003"
                    Literal5.Text = Trim(StrConv(dataread("DATA_OKNG"), VbStrConv.Wide))
                    Literal6.Text = "更新日時：" + dataread("DATA_UPD")
                Case "004"
                    Literal7.Text = Trim(StrConv(dataread("DATA_OKNG"), VbStrConv.Wide))
                    Literal8.Text = "更新日時：" + dataread("DATA_UPD")
                Case "005"
                    Literal9.Text = Trim(StrConv(dataread("DATA_OKNG"), VbStrConv.Wide))
                    Literal10.Text = "更新日時：" + dataread("DATA_UPD")
            End Select
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()

    End Sub

    Private Sub GET_STATUS(ByRef strStatus As String)
        '概況のステータスを取得
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()

        strSQL = ""
        strSQL = strSQL & "SELECT *  "
        strSQL = strSQL & "FROM T_EXL_POR_STATUS "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        '初期値セット
        strStatus = "OK"

        '１件でもNGあればNGを返す
        While (dataread.Read())
            If Trim(dataread("DATA_OKNG")) = "NG" Then
                strStatus = "NG"
                Exit While
            End If
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()


    End Sub
End Class
