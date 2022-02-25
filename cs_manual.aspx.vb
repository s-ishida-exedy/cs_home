Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.Common
Imports System.IO

Partial Class cs_home
    Inherits System.Web.UI.Page

    Public strRow As String
    Public strProcess As String

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        '表示ボタン押下処理
        Dim Dataobj As New DBAccess
        Dim strCUST As String = TextBox1.Text

        Dim ds As DataSet = Dataobj.GET_CS_RESULT(strCUST)
        If ds.Tables.Count > 0 Then
            GridView1.DataSourceID = ""
            GridView1.DataSource = ds
            Session("data") = ds
        End If

        'Grid再表示
        GridView1.DataBind()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        '詳細表示ボタン押下
        If Trim(TextBox1.Text) = "" Then
            Label12.Text = "表示する客先コードを入力してください。"
            Return
        End If

        '入力された客先コードをセッションへ
        Session("strCust") = Trim(TextBox1.Text)
        Session("strMode") = "01"       '更新モード

        '画面遷移
        Response.Redirect("cs_manual_detail.aspx")
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        '新規登録ボタン押下
        If Trim(TextBox1.Text) = "" Then
            Label12.Text = "ベースとなる客先コードを入力してください。"
            Return
        End If

        '入力された客先コードをセッションへ
        Session("strCust") = Trim(TextBox1.Text)
        Session("strMode") = "02"       '登録モード

        '画面遷移
        Response.Redirect("cs_manual_detail.aspx")
    End Sub
End Class
