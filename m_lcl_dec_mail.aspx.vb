﻿Imports System.Data
Imports System.Data.SqlClient

Partial Class cs_home
    Inherits System.Web.UI.Page

    Public strRow As String
    Public strProcess As String

    Private Sub GridView1_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowCreated

        'ボタンに行数をセット
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim dltButton As ImageButton = e.Row.FindControl("ImageButton1")
            'ボタンが存在する場合のみセット
            If Not (dltButton Is Nothing) Then
                dltButton.CommandArgument = e.Row.RowIndex.ToString()
            End If
        End If

    End Sub

    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load
        If IsPostBack Then
            ' そうでない時処理
        Else
            Me.DropDownList1.Items.Insert(0, "") '先頭に空白行追加
            Me.DropDownList2.Items.Insert(0, "") '先頭に空白行追加
        End If

        AddHandler GridView1.RowCommand, AddressOf GridView1_RowCommand

    End Sub

    Private Sub Make_Grid()
        'GRIDを作成する。

        Dim Dataobj As New DBAccess
        Dim strkbn As String = DropDownList1.SelectedValue
        'Dim strkbn As String = DropDownList2.SelectedValue

        'Select Case strVal
        '    Case "本社"

        '    Case "上野"

        'End Select

        'データの取得
        Dim ds As DataSet = Dataobj.GET_RESULT_DEC_LCL(strkbn, Me.TextBox1.Text)
        If ds.Tables.Count > 0 Then
            GridView1.DataSourceID = ""
            GridView1.DataSource = ds
            GridView1.DataBind()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        '検索ボタン押下
        Call Make_Grid()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'リセットボタン押下
        DropDownList1.SelectedIndex = 0
        TextBox1.Text = ""
        DropDownList2.SelectedIndex = 0

        Call Make_Grid()
    End Sub

    Private Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName = "edt" Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim data1 = Me.GridView1.Rows(index).Cells(1).Text

            Session("strMode") = "0"    '更新モード
            Session("strCode") = data1
            Response.Redirect("m_lcl_dec_mail_detail.aspx")
        End If

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        '新規登録ボタン押下

        Session("strMode") = "1"    '登録モード
        Response.Redirect("m_lcl_dec_mail_detail.aspx")
    End Sub
End Class
