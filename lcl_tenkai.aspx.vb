Imports System.Data.SqlClient


Partial Class cs_home
    Inherits System.Web.UI.Page

    Public strRow As String
    Public strProcess As String


    Private Sub GridView1_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound

        '最終更新年月日取得
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String
        Dim strinv As String
        Dim cno As Long
        Dim wno As Long
        Dim wday As String
        Dim wday2 As String

        Dim dt1 As DateTime = DateTime.Now.ToShortDateString





        If e.Row.RowType = DataControlRowType.DataRow Then



            If e.Row.Cells(1).Text = dt1.ToShortDateString Then

                e.Row.Cells(1).BackColor = Drawing.Color.Salmon
                e.Row.Cells(2).BackColor = Drawing.Color.Salmon

            End If




        End If





        e.Row.Cells(0).Width = 90
        e.Row.Cells(1).Width = 40
        e.Row.Cells(2).Width = 100
        e.Row.Cells(3).Width = 70
        e.Row.Cells(4).Width = 70
        e.Row.Cells(5).Width = 110
        e.Row.Cells(6).Width = 140
        e.Row.Cells(7).Width = 70
        e.Row.Cells(8).Width = 70
        e.Row.Cells(9).Width = 70
        e.Row.Cells(10).Width = 50
        e.Row.Cells(11).Width = 60
        e.Row.Cells(12).Width = 60
        e.Row.Cells(13).Width = 110
        e.Row.Cells(14).Width = 10
        e.Row.Cells(15).Width = 110
        e.Row.Cells(16).Width = 10
        e.Row.Cells(17).Width = 110


        e.Row.Cells(5).Visible = False
        e.Row.Cells(6).Visible = False
        e.Row.Cells(9).Visible = False
        'e.Row.Cells(12).Visible = False
        'e.Row.Cells(13).Visible = False
        'e.Row.Cells(14).Visible = False



    End Sub




End Class
