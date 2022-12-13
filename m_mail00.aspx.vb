Imports System.Data
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
            'Me.DropDownList1.Items.Insert(0, "") '先頭に空白行追加

        End If

        AddHandler GridView1.RowCommand, AddressOf GridView1_RowCommand

    End Sub

    Private Sub Make_Grid()
        'GRIDを作成する。

        Dim Dataobj As New DBAccess
        'Dim strkbn As String = DropDownList1.SelectedValue
        'Dim strkbn As String = DropDownList2.SelectedValue

        'Select Case strkbn
        '    Case "販促品"
        '        strkbn = "0"
        '    Case "LCL展開"
        '        strkbn = "1"
        '    Case "郵船委託"
        '        strkbn = "2"
        '    Case "近鉄委託"
        '        strkbn = "3"
        '    Case "日ト委託"
        '        strkbn = "4"
        '    Case "日通委託"
        '        strkbn = "5"
        '    Case "LCL準備_C258"
        '        strkbn = "6"
        '    Case "LCL準備_C6G0"
        '        strkbn = "7"
        '    Case "LCLBKG_C258"
        '        strkbn = "8"
        'End Select

        'データの取得
        'Dim ds As DataSet = Dataobj.GET_RESULT_DEC_LCL(strkbn, Me.TextBox1.Text)
        'If ds.Tables.Count > 0 Then
        '    GridView1.DataSourceID = ""
        '    GridView1.DataSource = ds
        '    GridView1.DataBind()
        'End If
    End Sub

    'Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
    '    '検索ボタン押下
    '    Call Make_Grid()
    'End Sub

    'Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
    '    'リセットボタン押下
    '    'DropDownList1.SelectedIndex = 0
    '    'TextBox1.Text = ""


    '    Call Make_Grid()
    'End Sub

    Private Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName = "edt" Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim data1 = Me.GridView1.Rows(index).Cells(3).Text
            Dim data2 = Me.GridView1.Rows(index).Cells(2).Text
            Dim data3 = Me.GridView1.Rows(index).Cells(1).Text

            Session("strMode") = "0"    '更新モード
            Session("strCode") = data1
            Session("strname") = data2
            Session("strcamp") = data3
            Response.Redirect("m_mail00_detail.aspx")
        End If

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        '新規登録ボタン押下

        Session("strMode") = "1"    '登録モード
        Response.Redirect("m_mail00_detail.aspx")
    End Sub
End Class
