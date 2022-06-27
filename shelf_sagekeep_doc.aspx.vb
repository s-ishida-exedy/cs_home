
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

    Private Sub GridView1_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound



    End Sub



    Protected Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList1.SelectedIndexChanged

        If DropDownList1.Text = "棚番号" Then
            DropDownList2.Items.Clear()
            DropDownList2.DataSource = SqlDataSource2
            DropDownList2.DataTextField = "SHELFNO"
            DropDownList2.DataValueField = "SHELFNO"
            DropDownList2.DataBind()

            DropDownList2.Items.Insert(0, "--Select--")

        ElseIf DropDownList1.Text = "業務区分" Then
            DropDownList2.Items.Clear()
            DropDownList2.DataSource = SqlDataSource3
            DropDownList2.DataTextField = "KBN02"
            DropDownList2.DataValueField = "KBN02"
            DropDownList2.DataBind()

            DropDownList2.Items.Insert(0, "--Select--")

        ElseIf DropDownList1.Text = "チーム" Then
            DropDownList2.Items.Clear()
            DropDownList2.DataSource = SqlDataSource4
            DropDownList2.DataTextField = "TEAM"
            DropDownList2.DataValueField = "TEAM"
            DropDownList2.DataBind()

            DropDownList2.Items.Insert(0, "--Select--")

        ElseIf DropDownList1.Text = "担当者" Then
            DropDownList2.Items.Clear()
            DropDownList2.DataSource = SqlDataSource8
            DropDownList2.DataTextField = "PIC02"
            DropDownList2.DataValueField = "PIC02"
            DropDownList2.DataBind()

            DropDownList2.Items.Insert(0, "--Select--")

        ElseIf DropDownList1.Text = "書類名" Then
            DropDownList2.Items.Clear()
            DropDownList2.DataSource = SqlDataSource12
            DropDownList2.DataTextField = "DOCNAME"
            DropDownList2.DataValueField = "DOCNAME"
            DropDownList2.DataBind()

            DropDownList2.Items.Insert(0, "--Select--")

        End If

    End Sub
    Protected Sub DropDownList2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList2.SelectedIndexChanged

        If DropDownList1.Text = "棚番号" Then

            GridView1.DataSourceID = ""
            GridView1.DataSource = SqlDataSource1
            GridView1.DataBind()

            DropDownList2.Items.Clear()
            DropDownList2.DataSource = SqlDataSource2
            DropDownList2.DataTextField = "SHELFNO"
            DropDownList2.DataValueField = "SHELFNO"
            DropDownList2.DataBind()

            DropDownList2.Items.Insert(0, "--Select--")

        ElseIf DropDownList1.Text = "業務区分" Then

            GridView1.DataSourceID = ""
            GridView1.DataSource = SqlDataSource5
            GridView1.DataBind()

            DropDownList2.Items.Clear()
            DropDownList2.DataSource = SqlDataSource3
            DropDownList2.DataTextField = "KBN02"
            DropDownList2.DataValueField = "KBN02"
            DropDownList2.DataBind()

            DropDownList2.Items.Insert(0, "--Select--")

        ElseIf DropDownList1.Text = "チーム" Then

            GridView1.DataSourceID = ""
            GridView1.DataSource = SqlDataSource6
            GridView1.DataBind()

            DropDownList2.Items.Clear()
            DropDownList2.DataSource = SqlDataSource4
            DropDownList2.DataTextField = "TEAM"
            DropDownList2.DataValueField = "TEAM"
            DropDownList2.DataBind()

            DropDownList2.Items.Insert(0, "--Select--")

        ElseIf DropDownList1.Text = "担当者" Then

            GridView1.DataSourceID = ""
            GridView1.DataSource = SqlDataSource9
            GridView1.DataBind()

            DropDownList2.Items.Clear()
            DropDownList2.DataSource = SqlDataSource8
            DropDownList2.DataTextField = "PIC02"
            DropDownList2.DataValueField = "PIC02"
            DropDownList2.DataBind()

            DropDownList2.Items.Insert(0, "--Select--")

        ElseIf DropDownList1.Text = "書類名" Then

            GridView1.DataSourceID = ""
            GridView1.DataSource = SqlDataSource10
            GridView1.DataBind()

            DropDownList2.Items.Clear()
            DropDownList2.DataSource = SqlDataSource12
            DropDownList2.DataTextField = "DOCNAME"
            DropDownList2.DataValueField = "DOCNAME"
            DropDownList2.DataBind()

            DropDownList2.Items.Insert(0, "--Select--")


        End If



    End Sub
    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        DropDownList1.Items.Clear()

        DropDownList1.Items.Insert(0, "--Select--")
        DropDownList1.Items.Insert(1, "棚番号")
        DropDownList1.Items.Insert(2, "業務区分")
        DropDownList1.Items.Insert(3, "チーム")
        DropDownList1.Items.Insert(4, "担当者")
        DropDownList1.Items.Insert(5, "書類名")


        DropDownList2.Items.Clear()
        DropDownList2.Items.Insert(0, "--Select--")

        GridView1.DataSourceID = ""
        GridView1.DataSource = SqlDataSource7
        GridView1.DataBind()


    End Sub


    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load




    End Sub


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Dim t As Integer
        t = 1
        Dim cnt As Integer = 0

        Dim val01 As String = ""

        Using wb As XLWorkbook = New XLWorkbook()
            Dim ws As IXLWorksheet = wb.AddWorksheet("保管書類")
            For Each cell As TableCell In GridView1.HeaderRow.Cells

                val01 = Trim(Replace(cell.Text, "&nbsp;", ""))
                ws.Cell(1, t).Value = val01
                t = t + 1
            Next


            t = 2
            For Each row As GridViewRow In GridView1.Rows
                For i As Integer = 0 To row.Cells.Count - 1
                    val01 = Trim(Replace(row.Cells(i).Text, "&nbsp;", ""))

                    If IsDate(val01) = True Then
                        ws.Cell(t, i + 1).SetValue(DateValue(val01))
                    Else
                        ws.Cell(t, i + 1).SetValue(val01)
                    End If



                Next
                t = t + 1

            Next

            ws.Style.Font.FontName = "Meiryo UI"
            ws.Style.Alignment.WrapText = False
            ws.Columns.AdjustToContents()
            ws.SheetView.FreezeRows(1)


            Dim struid As String = Session("UsrId")
            wb.SaveAs("\\svnas201\EXD06101\DISC_COMMON\WEB出力\書庫保管処理" & Now.ToString(“yyyyMMddhhmmss”) & "_PIC_" & struid & ".xlsx")

        End Using

        Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "確認", "<script language='JavaScript'>confirm('出力が完了しました。\n出力先：\\\svnas201\\EXD06101\\DISC_COMMON\\WEB出力');</script>", False)


    End Sub
End Class
