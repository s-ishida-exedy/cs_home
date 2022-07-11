Imports System.Data
Imports System.Data.SqlClient
Imports System.Linq
Imports ClosedXML.Excel
Imports System.IO

Partial Class cs_home
    Inherits System.Web.UI.Page

    Public strRow As String
    Public strProcess As String

    Private Sub GridView1_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowCreated
        'ボタンに行数をセット
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim dltButton As Button = e.Row.FindControl("Button1")
            Dim dltButton2 As Button = e.Row.FindControl("Button2")
            Dim dltButton3 As Button = e.Row.FindControl("Button3")
            'ボタンが存在する場合のみセット
            If Not (dltButton Is Nothing) Then
                dltButton.CommandArgument = e.Row.RowIndex.ToString()
                dltButton2.CommandArgument = e.Row.RowIndex.ToString()
                dltButton3.CommandArgument = e.Row.RowIndex.ToString()
            End If
        End If

    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged
        Dim sch_ID As String = GridView1.SelectedValue.ToString
        '選択された行のSCH_ID
        strRow = sch_ID
    End Sub

    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load
        If IsPostBack Then
            ' そうでない時処理
        Else
            Me.DropDownList1.Items.Insert(0, "-ETD-") '先頭にタイトル行追加
            Me.DropDownList2.Items.Insert(0, "-客先-") '先頭にタイトル行追加
            Me.DropDownList3.Items.Insert(0, "-依頼者-") '先頭にタイトル行追加
            Me.DropDownList4.Items.Insert(0, "-IVNO-") '先頭にタイトル行追加
            Me.DropDownList5.Items.Insert(0, "-ETD-") '先頭にタイトル行追加
            Me.DropDownList6.Items.Insert(0, "-客先-") '先頭にタイトル行追加
            Me.DropDownList7.Items.Insert(0, "-依頼者-") '先頭にタイトル行追加
            Me.DropDownList8.Items.Insert(0, "-IVNO-") '先頭にタイトル行追加
            DropDownList5.Visible = False
            DropDownList6.Visible = False
            DropDownList7.Visible = False
            DropDownList8.Visible = False
        End If

    End Sub

    Private Sub Make_Grid()
        'GRIDを作成する。
        Dim strValue(3) As String
        Dim Dataobj As New DBAccess
        Dim strEtd As String = ""
        Dim strCUST As String = ""
        Dim strReq As String = ""
        Dim strIVNO As String = ""
        Dim strChk As String = "0"

        If CheckBox1.Checked = True Then    '集荷済みも表示
            strEtd = DropDownList5.SelectedValue
            strCUST = DropDownList6.SelectedValue
            strReq = DropDownList7.SelectedValue
            strIVNO = DropDownList8.SelectedValue
            strChk = "1"
        Else
            strEtd = DropDownList1.SelectedValue
            strCUST = DropDownList2.SelectedValue
            strReq = DropDownList3.SelectedValue
            strIVNO = DropDownList4.SelectedValue
            strChk = "0"
        End If

        If strEtd = "-ETD-" Then strEtd = ""
        If strCUST = "-客先-" Then strCUST = ""
        If strReq = "-依頼者-" Then strReq = ""
        If strIVNO = "-IVNO-" Then strIVNO = ""

        '引数の配列へセット
        strValue = {strEtd, strCUST, strReq, strIVNO}

        'データの取得
        Dim ds As DataSet = Dataobj.GET_AIR_DATA(strValue, strChk, "0")
        If ds.Tables.Count > 0 Then
            GridView1.DataSourceID = ""
            GridView1.DataSource = ds
            GridView1.DataBind()

            Dim ds2 As DataSet
            'ドロップダウンリストのデータバインド
            If CheckBox1.Checked = True Then    '集荷済みも表示
                ds2 = Dataobj.GET_AIR_DATA(strValue, strChk, "1")
                Call DropDown_Ctrl(DropDownList5, ds2, strEtd, "-ETD-", "ETD")
                ds2 = Dataobj.GET_AIR_DATA(strValue, strChk, "2")
                Call DropDown_Ctrl(DropDownList6, ds2, strCUST, "-客先-", "CUST_CD")
                ds2 = Dataobj.GET_AIR_DATA(strValue, strChk, "3")
                Call DropDown_Ctrl(DropDownList7, ds2, strReq, "-依頼者-", "REQUESTER")
                ds2 = Dataobj.GET_AIR_DATA(strValue, strChk, "4")
                Call DropDown_Ctrl(DropDownList8, ds2, strIVNO, "-IVNO-", "IVNO")
            Else
                ds2 = Dataobj.GET_AIR_DATA(strValue, strChk, "1")
                Call DropDown_Ctrl(DropDownList1, ds2, strEtd, "-ETD-", "ETD")
                ds2 = Dataobj.GET_AIR_DATA(strValue, strChk, "2")
                Call DropDown_Ctrl(DropDownList2, ds2, strCUST, "-客先-", "CUST_CD")
                ds2 = Dataobj.GET_AIR_DATA(strValue, strChk, "3")
                Call DropDown_Ctrl(DropDownList3, ds2, strReq, "-依頼者-", "REQUESTER")
                ds2 = Dataobj.GET_AIR_DATA(strValue, strChk, "4")
                Call DropDown_Ctrl(DropDownList4, ds2, strIVNO, "-IVNO-", "IVNO")
            End If

        End If
    End Sub

    Private Sub Make_Grid2()
        'GRIDを作成する。
        Dim strValue As String
        Dim Dataobj As New DBAccess

        If TextBox3.Text = "" Then
            Exit Sub
        Else
            strValue = TextBox3.Text
        End If

        'データの取得
        Dim ds As DataSet = Dataobj.GET_AIR_DATA2(strValue)
        If ds.Tables.Count > 0 Then
            GridView1.DataSourceID = ""
            GridView1.DataSource = ds
            GridView1.DataBind()
        End If
    End Sub

    Private Sub DropDown_Ctrl(objDL As DropDownList, ds As DataSet, strValue As String,
                              strTitle As String, strKey As String)
        '検索時のドロップダウンリストのデータセット
        objDL.Items.Clear()
        objDL.Items.Insert(0, strTitle) '先頭にタイトル行追加
        objDL.DataSourceID = ""
        objDL.DataSource = ds
        objDL.DataTextField = strKey
        objDL.DataValueField = strKey
        objDL.DataBind()

        '選択値の存在チェック後、セット
        If objDL.Items.FindByValue(strValue) IsNot Nothing And strValue <> "" Then
            '存在する
            objDL.SelectedValue = strValue
        Else
            objDL.SelectedValue = strTitle
        End If
    End Sub

    Private Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList1.SelectedIndexChanged
        Call Make_Grid()
    End Sub

    Private Sub DropDownList2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList2.SelectedIndexChanged
        Call Make_Grid()
    End Sub

    Private Sub DropDownList3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList3.SelectedIndexChanged
        Call Make_Grid()
    End Sub

    Private Sub DropDownList4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList4.SelectedIndexChanged
        Call Make_Grid()
    End Sub

    Private Sub DropDownList5_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList5.SelectedIndexChanged
        Call Make_Grid()
    End Sub

    Private Sub DropDownList6_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList6.SelectedIndexChanged
        Call Make_Grid()
    End Sub

    Private Sub DropDownList7_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList7.SelectedIndexChanged
        Call Make_Grid()
    End Sub

    Private Sub DropDownList8_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList8.SelectedIndexChanged
        Call Make_Grid()
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'リセットボタン押下
        DropDownList1.SelectedIndex = 0
        DropDownList2.SelectedIndex = 0
        DropDownList3.SelectedIndex = 0
        DropDownList4.SelectedIndex = 0
        DropDownList5.SelectedIndex = 0
        DropDownList6.SelectedIndex = 0
        DropDownList7.SelectedIndex = 0
        DropDownList8.SelectedIndex = 0
        DropDownList1.Visible = True
        DropDownList2.Visible = True
        DropDownList3.Visible = True
        DropDownList4.Visible = True

        DropDownList5.Visible = False
        DropDownList6.Visible = False
        DropDownList7.Visible = False
        DropDownList8.Visible = False
        CheckBox1.Checked = False

        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""

        Call Make_Grid()
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        'チェックボックスON/OFF
        If CheckBox1.Checked = True Then
            DropDownList1.Visible = False
            DropDownList2.Visible = False
            DropDownList3.Visible = False
            DropDownList4.Visible = False

            DropDownList5.Visible = True
            DropDownList6.Visible = True
            DropDownList7.Visible = True
            DropDownList8.Visible = True
        Else
            DropDownList1.Visible = True
            DropDownList2.Visible = True
            DropDownList3.Visible = True
            DropDownList4.Visible = True

            DropDownList5.Visible = False
            DropDownList6.Visible = False
            DropDownList7.Visible = False
            DropDownList8.Visible = False
        End If

        '入力されたIVNOを初期化
        TextBox3.Text = ""

        '再表示
        Call Make_Grid()

    End Sub

    Private Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        'グリッド内のボタン押下処理
        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim data1 = Me.GridView1.Rows(index).Cells(5).Text
        Dim data2 = Me.GridView1.Rows(index).Cells(7).Text

        '更新
        If e.CommandName = "doc_fin" Then
            Call Update_FLG("doc_fin", data1, data2)
        ElseIf e.CommandName = "pic_fin" Then
            Call Update_FLG("pic_fin", data1, data2)
        ElseIf e.CommandName = "update" Then
            '入力された客先コードをセッションへ
            Session("strEtd") = data1
            Session("strIvno") = data2
            Session("strMode") = "01"       '更新モード

            '画面遷移
            Response.Redirect("air_mng_detail.aspx")
            Return
        End If

        '画面再表示
        Call Make_Grid()
    End Sub

    Private Sub Update_FLG(strFlg As String, strEtd As String, strIvno As String)
        'レコードのフラグを取得する。
        Dim strSQL As String
        Dim dtNow As DateTime = DateTime.Now

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand
        Dim i As Integer = 0

        'データベース接続を開く
        cnn.Open()

        strSQL = ""
        'フラグをUPDATE
        If strFlg = "doc_fin" Then
            strSQL = strSQL & "UPDATE T_EXL_AIR_MANAGE SET DOC_FIN = '作成済み' "
            strSQL = strSQL & ", UPD_DATE = '" & dtNow & "' "
            strSQL = strSQL & ", UPD_PERSON = '" & Session("UsrId") & "' "
            strSQL = strSQL & "WHERE ETD = '" & strEtd & "' AND IVNO = '" & strIvno & "' "
        ElseIf strFlg = "pic_fin" Then
            strSQL = strSQL & "UPDATE T_EXL_AIR_MANAGE SET PICKUP = '集荷済み' "
            strSQL = strSQL & ", UPD_DATE = '" & dtNow & "' "
            strSQL = strSQL & ", UPD_PERSON = '" & Session("UsrId") & "' "
            strSQL = strSQL & "WHERE ETD = '" & strEtd & "' AND IVNO = '" & strIvno & "' "
        End If

        Command.CommandText = strSQL
        ' SQLの実行
        Command.ExecuteNonQuery()

        cnn.Close()
        cnn.Dispose()

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        '新規登録ボタン押下
        Session("strMode") = "02"       '更新モード

        '画面遷移
        Response.Redirect("air_mng_detail.aspx")

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        '前月分ダウンロードボタン押下
        Dim strFile As String = Format(Now, "yyyyMMdd") & "_air_previous.xlsx"
        Dim strPath As String = "C:\exp\cs_home\files\"
        Dim strChanged As String    'サーバー上のフルパス
        Dim strFileNm As String     'ファイル名

        Dim dtToday As DateTime = DateTime.Today

        Dim dtFDM As Date = New DateTime(dtToday.Year, dtToday.Month, 1).AddMonths(-1)      '前月初日
        Dim dtTDM As Date = New DateTime(dtToday.Year, dtToday.Month, 1).AddDays(-1)        '前月末日

        Dim dt = GetNorthwindProductTable(dtFDM, dtTDM)
        Dim workbook = New XLWorkbook()
        Dim worksheet = workbook.Worksheets.Add(dt)
        workbook.SaveAs(strPath & strFile)

        'ファイル名を取得する
        Dim strTxtFiles() As String = IO.Directory.GetFiles(strPath, "*_air_previous.xlsx")

        strChanged = strTxtFiles(0)
        strFileNm = Path.GetFileName(strChanged)

        'Contentをクリア
        Response.ClearContent()

        'Contentを設定
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("shift-jis")
        Response.ContentType = "application/vnd.ms-excel"

        '表示ファイル名を指定
        Dim fn As String = HttpUtility.UrlEncode(strFileNm)
        Response.AddHeader("Content-Disposition", "attachment;filename=" + fn)

        'ダウンロード対象ファイルを指定
        Response.WriteFile(strChanged)

        'ダウンロード実行
        Response.Flush()
        Response.End()

    End Sub

    Private Shared Function GetNorthwindProductTable(dtFDM As Date, dtTDM As Date) As DataTable
        'EXCELファイル出力
        Dim strSQL As String = ""
        Dim strSDate As String = ""
        Dim strEDate As String = ""

        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"

        Dim dt = New DataTable("T_EXL_AIR_MANAGE")

        Using conn = New SqlConnection(ConnectionString)
            Dim cmd = conn.CreateCommand()

            strSQL = strSQL & "SELECT * FROM T_EXL_AIR_MANAGE "
            strSQL = strSQL & "WHERE ETD BETWEEN '" & Format(dtFDM, "yyyy/MM/dd") & "' "
            strSQL = strSQL & "AND '" & Format(dtTDM, "yyyy/MM/dd") & "' "
            strSQL = strSQL & "ORDER BY ETD "

            cmd.CommandText = strSQL
            Dim sda = New SqlDataAdapter(cmd)
            sda.Fill(dt)
        End Using

        Return dt
    End Function

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        '期間指定してﾀﾞｳﾝﾛｰﾄﾞボタン押下

        Dim strFile As String = Format(Now, "yyyyMMdd") & "_air_previous.xlsx"
        Dim strPath As String = "C:\exp\cs_home\files\"
        Dim strChanged As String    'サーバー上のフルパス
        Dim strFileNm As String     'ファイル名

        Dim dt = GetNorthwindProductTable(TextBox1.Text, TextBox2.Text)
        Dim workbook = New XLWorkbook()
        Dim worksheet = workbook.Worksheets.Add(dt)
        workbook.SaveAs(strPath & strFile)

        'ファイル名を取得する
        Dim strTxtFiles() As String = IO.Directory.GetFiles(strPath, "*_air_previous.xlsx")

        strChanged = strTxtFiles(0)
        strFileNm = Path.GetFileName(strChanged)

        'Contentをクリア
        Response.ClearContent()

        'Contentを設定
        Response.ContentEncoding = System.Text.Encoding.GetEncoding("shift-jis")
        Response.ContentType = "application/vnd.ms-excel"

        '表示ファイル名を指定
        Dim fn As String = HttpUtility.UrlEncode(strFileNm)
        Response.AddHeader("Content-Disposition", "attachment;filename=" + fn)

        'ダウンロード対象ファイルを指定
        Response.WriteFile(strChanged)

        'ダウンロード実行
        Response.Flush()
        Response.End()

    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        '検索ボタン押下
        Call Make_Grid2()
    End Sub
End Class
