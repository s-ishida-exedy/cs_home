﻿Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.Common
Imports System.IO

Partial Class cs_home
    Inherits System.Web.UI.Page

    Public strRow As String
    Public strProcess As String

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If IsPostBack Then
            ' そうでない時処理
        Else
            Me.DropDownList2.Items.Insert(0, "-出荷方法-") '先頭に空白行追加

            'テキストボックスに月初と月末をセット
            Dim dtToday As DateTime = DateTime.Today
            Dim dtFDM As New DateTime(dtToday.Year, dtToday.Month, 1)
            Dim dtLDM As New DateTime(dtToday.Year, dtToday.Month,
            DateTime.DaysInMonth(dtToday.Year, dtToday.Month))

            TextBox1.Text = dtFDM
            TextBox2.Text = dtLDM
        End If

        Label2.Text = ""

        '初期データ取得
        Call GET_DATA()

    End Sub

    Private Sub Make_Grid(strCode As String)
        'GRIDを作成する。

        Dim Dataobj As New DBAccess
        Dim strShukka As String = ""
        Dim strChk As String = CheckBox1.Checked.ToString

        Select Case DropDownList2.SelectedValue
            Case "20ft"
                strShukka = "01"
            Case "40ft"
                strShukka = "02"
            Case "CFS"
                strShukka = "03"
            Case "AIR"
                strShukka = "05"
            Case "COURIER"
                strShukka = "06"
        End Select

        'データの取得
        Dim ds As DataSet = Dataobj.GET_RESULT_SALES(Me.TextBox1.Text, Me.TextBox2.Text, strCode, strShukka, strChk)
        Dim dtCnt As Integer = ds.Tables("dt_Product").Rows.Count

        If dtCnt = 0 Then
            Label2.Text = "売上未確定のものはありません。"
        End If

        If ds.Tables.Count > 0 Then
            GridView1.DataSourceID = ""
            GridView1.DataSource = ds
            GridView1.DataBind()
        End If
    End Sub

    Private Sub GridView1_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowDataBound
        If e.Row.RowType = DataControlRowType.DataRow Then
            '登録者CD(社員番号)を取得
            Dim drv As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim strCode As String = Trim(drv("REGPERSON").ToString())

            '取得したコードの登録者名を取得する
            Dim dataread As SqlDataReader
            Dim dbcmd As SqlCommand
            Dim strSQL As String
            Dim strPlace = DropDownList1.SelectedValue

            '接続文字列の作成
            Dim ConnectionString As String = String.Empty
            'SQL Server認証
            ConnectionString = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
            'SqlConnectionクラスの新しいインスタンスを初期化
            Dim cnn = New SqlConnection(ConnectionString)

            'データベース接続を開く
            cnn.Open()

            strSQL = ""
            strSQL = strSQL & "SELECT NAME_AB FROM M_EXL_CS_MEMBER "
            strSQL = strSQL & "WHERE CODE = '" & strCode & "' "
            'strSQL = strSQL & "AND PLACE like '%" & strPlace & "%'"

            'ＳＱＬコマンド作成 
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行 
            dataread = dbcmd.ExecuteReader()

            strCode = ""
            '結果を取り出す 
            While (dataread.Read())
                strCode = Trim(dataread("NAME_AB"))
            End While

            'クローズ処理 
            dataread.Close()
            dbcmd.Dispose()
            cnn.Close()
            cnn.Dispose()

            '名前列に値セット
            e.Row.Cells(6).Text = strCode
        End If
    End Sub

    Private Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList1.SelectedIndexChanged
        '場所変更時処理
        Call GET_DATA()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Call GET_DATA()
    End Sub

    Private Sub GET_DATA()
        'データ取得
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String
        Dim strCode As String

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()

        Dim strPlace = DropDownList1.SelectedValue

        strSQL = ""
        strSQL = strSQL & "SELECT CODE FROM M_EXL_CS_MEMBER "
        strSQL = strSQL & "WHERE PLACE like '%" & strPlace & "%'"

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        strCode = ""
        '結果を取り出す 
        While (dataread.Read())
            strCode += "'" & Trim(dataread("CODE")) & "',"
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()

        '最後のカンマを削除する
        strCode = Mid(strCode, 1, Len(strCode) - 1)

        'グリッド表示
        Call Make_Grid(strCode)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'クリアボタン押下

        'テキストボックスに月初と月末をセット
        Dim dtToday As DateTime = DateTime.Today
        Dim dtFDM As New DateTime(dtToday.Year, dtToday.Month, 1)
        Dim dtLDM As New DateTime(dtToday.Year, dtToday.Month,
            DateTime.DaysInMonth(dtToday.Year, dtToday.Month))

        TextBox1.Text = dtFDM
        TextBox2.Text = dtLDM

        DropDownList1.SelectedIndex = 0
        DropDownList2.SelectedIndex = 0

        CheckBox1.Checked = False

        Call GET_DATA()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        '前月ボタン押下

        'テキストボックスに月初と月末をセット
        Dim dtToday As DateTime = DateTime.Today

        Dim dtFDM = New DateTime(dtToday.Year, dtToday.Month, 1).AddMonths(-1)
        Dim dtLDM = New DateTime(dtToday.Year, dtToday.Month, 1).AddDays(-1)

        TextBox1.Text = dtFDM
        TextBox2.Text = dtLDM

        Call GET_DATA()

    End Sub
End Class
