


Imports System.Data.SqlClient
Imports System.Data
Imports mod_function
Imports MailKit.Net.Smtp
Imports MailKit.Security
Imports MimeKit
Imports MimeKit.Text
Imports System.IO
Partial Class cs_home
    Inherits System.Web.UI.Page

    Public strRow As String
    Public strProcess As String

    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load

        'GridView2.Columns(16).ItemStyle.Wrap = True

    End Sub

    Private Sub GridView2_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView2.RowDataBound

        '最終更新年月日取得
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String
        Dim strinv As String
        Dim cno As Long
        Dim wno As Long
        Dim wday As String
        Dim wday2 As String
        Dim intval As Integer
        Dim intCnt As Integer

        Dim dt1 As DateTime = DateTime.Now.ToShortDateString

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim dltButton As ImageButton = e.Row.FindControl("ImageButton1")
            'ボタンが存在する場合のみセット
            If Not (dltButton Is Nothing) Then
                dltButton.CommandArgument = e.Row.RowIndex.ToString()
            End If
        End If

    End Sub

    Private Sub GridView2_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView2.RowCommand

        If e.CommandName = "edt" Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim data0 = Me.GridView2.Rows(index).Cells(1).Text
            Dim data1 = Me.GridView2.Rows(index).Cells(2).Text

            Session("strMode") = "0"    '更新モード
            Session("NO") = data0
            Session("NAME") = data1

            Response.Redirect("m_lcl_address_detail.aspx")

        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        '新規登録ボタン押下

        Session("strMode") = "1"    '登録モード
        Response.Redirect("m_lcl_address_detail.aspx")
    End Sub

End Class
