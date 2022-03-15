Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.Common
Imports System.IO

Partial Class cs_home
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If IsPostBack Then
            ' そうでない時処理
        Else
            Me.DropDownList2.Items.Insert(0, "-VAN日-") '先頭に空白行追加（日付）
            Me.DropDownList1.Items.Insert(0, "-場所-") '先頭に空白行追加（場所）
        End If
    End Sub

    Private Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList1.SelectedIndexChanged
        Dim Dataobj As New DBAccess
        Dim strDate As String = ""
        Dim strPlace As String = ""

        If DropDownList2.SelectedValue <> "-VAN日-" Then
            strDate = DropDownList2.SelectedValue
        ElseIf DropDownList2.SelectedValue = "-VAN日-" Then
            strDate = ""
        End If
        If DropDownList1.SelectedValue <> "-場所-" Then
            strPlace = DropDownList1.SelectedValue
        ElseIf DropDownList1.SelectedValue = "-場所-" Then
            strPlace = ""
        End If

        Dim ds As DataSet = Dataobj.GET_FINAL_DOC_DATA(strDate, strPlace)
        If ds.Tables.Count > 0 Then
            GridView1.DataSourceID = ""
            GridView1.DataSource = ds
            Session("data") = ds
        End If

        'Grid再表示
        GridView1.DataBind()

    End Sub

    Private Sub DropDownList2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList2.SelectedIndexChanged
        Dim Dataobj As New DBAccess
        Dim strDate As String = ""
        Dim strPlace As String = ""

        If DropDownList2.SelectedValue <> "-VAN日-" Then
            strDate = DropDownList2.SelectedValue
        ElseIf DropDownList2.SelectedValue = "-VAN日-" Then
            strDate = ""
        End If
        If DropDownList1.SelectedValue <> "-場所-" Then
            strPlace = DropDownList1.SelectedValue
        ElseIf DropDownList1.SelectedValue = "-場所-" Then
            strPlace = ""
        End If

        Dim ds As DataSet = Dataobj.GET_FINAL_DOC_DATA(strDate, strPlace)
        If ds.Tables.Count > 0 Then
            GridView1.DataSourceID = ""
            GridView1.DataSource = ds
            Session("data") = ds
        End If

        'Grid再表示
        GridView1.DataBind()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'リストの選択状態をリセット
        DropDownList1.SelectedIndex = 0     '場所
        DropDownList2.SelectedIndex = 0     '日付

        'データ再取得
        Dim Dataobj As New DBAccess
        Dim ds As DataSet = Dataobj.GET_FINAL_DOC_DATA("", "")
        If ds.Tables.Count > 0 Then
            GridView1.DataSourceID = ""
            GridView1.DataSource = ds
            Session("data") = ds
        End If

        'Grid再表示
        GridView1.DataBind()

    End Sub
End Class
