Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.Common
Imports System.IO
Imports ClosedXML.Excel

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

    Private Sub GridView1_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound


        e.Row.Cells(5).Visible = False
        e.Row.Cells(6).Visible = False
        e.Row.Cells(7).Visible = False
        e.Row.Cells(8).Visible = False
        e.Row.Cells(9).Visible = False
        e.Row.Cells(10).Visible = False
        e.Row.Cells(11).Visible = False
        e.Row.Cells(12).Visible = False
        e.Row.Cells(13).Visible = False
        e.Row.Cells(14).Visible = False
        e.Row.Cells(15).Visible = False
        e.Row.Cells(16).Visible = False
        e.Row.Cells(17).Visible = False
        e.Row.Cells(18).Visible = False
        e.Row.Cells(19).Visible = False
        e.Row.Cells(20).Visible = False
        e.Row.Cells(21).Visible = False
        e.Row.Cells(22).Visible = False
        e.Row.Cells(23).Visible = False
        e.Row.Cells(24).Visible = False
        e.Row.Cells(25).Visible = False
        e.Row.Cells(26).Visible = False
        e.Row.Cells(27).Visible = False
        e.Row.Cells(28).Visible = False
        e.Row.Cells(29).Visible = False
        e.Row.Cells(30).Visible = False
        e.Row.Cells(31).Visible = False
        e.Row.Cells(32).Visible = False
        e.Row.Cells(33).Visible = False
        e.Row.Cells(34).Visible = False
        e.Row.Cells(35).Visible = False
        e.Row.Cells(36).Visible = False
        e.Row.Cells(37).Visible = False
        e.Row.Cells(38).Visible = False
        e.Row.Cells(39).Visible = False
        e.Row.Cells(40).Visible = False

        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim dltButton As ImageButton = e.Row.FindControl("ImageButton1")
            'ボタンが存在する場合のみセット
            If Not (dltButton Is Nothing) Then
                dltButton.CommandArgument = e.Row.RowIndex.ToString()
            End If
        End If

    End Sub

    Private Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName = "edt" Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)


            '入力された客先コードをセッションへ
            Session("strCust") = Trim(Me.GridView1.Rows(index).Cells(1).Text)
            Session("strMode") = "01"       '更新モード

            Response.Redirect("cs_manual_detail.aspx")

        End If

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click






        Dim t As Integer
        t = 1
        Dim cnt As Integer = 0

        Dim val01 As String = ""

        Using wb As XLWorkbook = New XLWorkbook()
            Dim ws As IXLWorksheet = wb.AddWorksheet("CSマニュアル")
            For Each cell As TableCell In GridView1.HeaderRow.Cells

                If cnt = 0 Then
                    cnt = 1
                Else
                    val01 = Trim(Replace(cell.Text, "&nbsp;", ""))
                    ws.Cell(1, t).Value = val01
                    t = t + 1
                End If
            Next

            cnt = 0
            t = 2
            For Each row As GridViewRow In GridView1.Rows

                If cnt = 0 Then
                    cnt = 1
                Else
                    For i As Integer = 1 To row.Cells.Count - 1
                        val01 = Trim(Replace(row.Cells(i).Text, "&nbsp;", ""))
                        Select Case i
                            Case 15 To 24, 29 To 30, 37 To 41
                                val01 = Trim(Replace(val01, "&#215;", "×"))
                                If IsDate(val01) = True Then
                                    ws.Cell(t, i).SetValue(DateValue(val01))
                                Else
                                    ws.Cell(t, i).SetValue(val01)
                                End If

                            Case Else
                                If IsDate(val01) = True Then
                                    ws.Cell(t, i).SetValue(DateValue(val01))
                                Else
                                    ws.Cell(t, i).SetValue(val01)
                                End If

                        End Select
                    Next
                    t = t + 1
                End If
            Next

            ws.Style.Font.FontName = "Meiryo UI"
            ws.Style.Alignment.WrapText = False
            ws.Columns.AdjustToContents()
            ws.SheetView.FreezeRows(1)

            Dim struid As String = Session("UsrId")
            wb.SaveAs("\\svnas201\EXD06101\DISC_COMMON\WEB出力\CSマニュアル" & Now.ToString(“yyyyMMddhhmmss”) & "_PIC_" & struid & ".xlsx")

        End Using


        Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "確認", "<script language='JavaScript'>confirm('出力が完了しました。\n出力先：\\\svnas201\\EXD06101\\DISC_COMMON\\WEB出力');</script>", False)





    End Sub
End Class
