
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.Common
Imports System.Console
Imports ClosedXML.Excel
Imports MailKit.Net.Smtp
Imports MailKit.Security
Imports MimeKit
Imports MimeKit.Text
Imports System.IO
Partial Class yuusen
    Inherits System.Web.UI.Page

    Public strRow As String
    Public strProcess As String


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim Dataobj As New DBAccess
        Dim strtana As String

        strtana = tana02.Text

        Dim ds As DataSet = Dataobj.GET_CS_RESULT_SHELF(strtana)

        If ds.Tables.Count > 0 Then
            GridView1.DataSourceID = ""
            GridView1.DataSource = ds
            Session("data") = ds
        End If

        'Grid再表示
        GridView1.DataBind()

    End Sub

End Class
