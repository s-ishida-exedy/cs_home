
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.Common
Imports System.Console
Imports ClosedXML.Excel
Imports System.IO
Imports System.Linq
Partial Class yuusen
    Inherits System.Web.UI.Page

    Public strRow As String
    Public strProcess As String
    Public strPath As String = "C:\exp\cs_home\files"

    'Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
    '    '名古屋

    '    Session("stdate") = TextBox1.Text
    '    Session("eddate") = TextBox2.Text
    '    Session("portcd") = "NAGOYA"


    '    '入力チェック
    '    If chk_Nyuryoku() = False Then
    '        Return
    '    End If


    '    Response.Redirect("bcp_export_data.aspx")

    'End Sub

    Private Function chk_Nyuryoku() As Boolean
        chk_Nyuryoku = True

        Dim strst As String = TextBox1.Text
        Dim stred As String = TextBox2.Text

        '登録時のみチェック
        If strst = "" Or stred = "" Then
            Label3.Text = "日付けを設定してください。"
            chk_Nyuryoku = False
        End If

        '登録時のみチェック
        If strst > stred Then
            Label3.Text = "開始日が終了日を超えています。"
            chk_Nyuryoku = False
        End If


    End Function

    'Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
    '    '四日市

    '    Session("stdate") = TextBox1.Text
    '    Session("eddate") = TextBox2.Text
    '    Session("portcd") = "YOKKAICHI"

    '    '入力チェック
    '    If chk_Nyuryoku() = False Then
    '        Return
    '    End If

    '    Response.Redirect("bcp_export_data.aspx")

    'End Sub

    'Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
    '    '大阪

    '    Session("stdate") = TextBox1.Text
    '    Session("eddate") = TextBox2.Text
    '    Session("portcd") = "OSAKA"

    '    '入力チェック
    '    If chk_Nyuryoku() = False Then
    '        Return
    '    End If

    '    Response.Redirect("bcp_export_data.aspx")

    'End Sub

    'Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
    '    '神戸

    '    Session("stdate") = TextBox1.Text
    '    Session("eddate") = TextBox2.Text
    '    Session("portcd") = "KOBE"

    '    '入力チェック
    '    If chk_Nyuryoku() = False Then
    '        Return
    '    End If

    '    Response.Redirect("bcp_export_data.aspx")


    'End Sub
End Class
