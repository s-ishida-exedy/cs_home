
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.Common
Imports System.Console
Imports ClosedXML.Excel
Partial Class yuusen
    Inherits System.Web.UI.Page

    Public strRow As String
    Public strProcess As String

    Private Sub GridView1_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound

        Dim str01 As String = ""
        Dim str02 As String = ""
        Dim str03 As String = ""
        Dim str04 As String = ""
        Dim str05 As String = ""


        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.Cells(11).Text = "月またぎ" Then
                e.Row.BackColor = Drawing.Color.DarkSalmon
            ElseIf e.Row.Cells(11).Text = "出港済み" Then
                e.Row.BackColor = Drawing.Color.LightBlue
            End If

            If e.Row.Cells(8).Text = "" Or e.Row.Cells(8).Text = "&nbsp;" Then
                Call GET_IVDATA(Trim(e.Row.Cells(12).Text), Trim(e.Row.Cells(2).Text))
            End If

            Dim dt0 As DateTime = DateTime.Parse(e.Row.Cells(3).Text)

            If e.Row.Cells(8).Text <> "&nbsp;" And e.Row.Cells(11).Text = "&nbsp;" Then
                If e.Row.Cells(3).Text <> "&nbsp;" And e.Row.Cells(9).Text <> "&nbsp;" Then

                    Dim dt1 As DateTime = DateTime.Parse(e.Row.Cells(9).Text)

                    If dt0.ToString("MM") = dt1.ToString("MM") Then
                        str02 = "不要"
                        str01 = "-"
                        Call UPD_MEMO02(Trim(e.Row.Cells(12).Text), str01, str02)
                    Else
                        str02 = "要"
                        str01 = "確認要"
                        Call UPD_MEMO02(Trim(e.Row.Cells(12).Text), str01, str02)
                    End If
                End If
            End If



            If e.Row.Cells(14).Text <> "&nbsp;" Then

                If Trim(e.Row.Cells(14).Text) <> "" Then
                    Dim dt2 As DateTime = DateTime.Parse(e.Row.Cells(14).Text)

                    If dt0.ToString("MM") = dt2.ToString("MM") Then
                        str03 = "○"
                        Call UPD_MEMO03(Trim(e.Row.Cells(12).Text), str03)
                    Else
                        str03 = "×"
                        Call UPD_MEMO03(Trim(e.Row.Cells(12).Text), str03)
                    End If
                End If
            End If

            If e.Row.Cells(4).Text <> "&nbsp;" Then
                If Trim(e.Row.Cells(4).Text) <> "" Then

                    Dim dt3 As DateTime = DateTime.Parse(e.Row.Cells(4).Text)

                    If dt0.ToString("MM") = dt3.ToString("MM") Then
                        str04 = "○"
                        Call UPD_MEMO03(Trim(e.Row.Cells(12).Text), str04)
                    Else
                        str04 = "×"
                        Call UPD_MEMO03(Trim(e.Row.Cells(12).Text), str04)
                    End If
                End If
            End If

            Dim dt00 As DateTime = DateTime.Now
            Dim ts1 As New TimeSpan(7, 0, 0, 0)
            Dim dt01 As DateTime = dt00 + ts1

            If e.Row.Cells(8).Text = "&nbsp;" Or Trim(e.Row.Cells(8).Text) = "" Then
                If e.Row.Cells(6).Text = "&nbsp;" Or Trim(e.Row.Cells(6).Text) = "" Then

                    Dim dt4 As DateTime = DateTime.Parse(e.Row.Cells(5).Text)

                    If dt4 <= dt01 Then
                        e.Row.BackColor = Drawing.Color.Red
                    Else
                    End If
                Else

                    Dim dt3 As DateTime = DateTime.Parse(e.Row.Cells(6).Text)

                    If dt3 <= dt01 Then
                        e.Row.BackColor = Drawing.Color.Red
                    Else
                    End If
                End If
            End If
        End If

        Panel1.Visible = True
        Panel2.Visible = False
        Panel3.Visible = False


    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click


        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=KBHWPM02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand
        Dim strSQL As String = ""
        Dim strtype As String = "1"

        'データベース接続を開く
        cnn.Open()

        '非表示ボタン　FLG03は非表示
        Dim I As Integer
        For I = 0 To GridView1.Rows.Count - 1
            Call GET_IVDATA(Trim(GridView1.Rows(I).Cells(12).Text), Trim(GridView1.Rows(I).Cells(2).Text))
        Next

        'Grid再表示
        GridView1.DataBind()

        Panel1.Visible = True
        Panel2.Visible = False
        Panel3.Visible = False


        DropDownList1.Items.Clear()

        DropDownList1.Items.Insert(0, "--Select--")
        DropDownList1.Items.Insert(1, "未回収")
        DropDownList1.Items.Insert(2, "修正状況")


        DropDownList2.Items.Clear()
        DropDownList2.Items.Insert(0, "--Select--")



    End Sub

    Private Sub GET_IVDATA(bkgno As String, ivno As String)

        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String
        Dim str01 As String
        Dim str02 As String
        Dim str03 As String
        Dim str04 As String
        Dim str05 As String
        Dim str06 As String
        Dim str07 As String
        Dim str08 As String
        Dim str09 As String
        Dim str10 As String

        Dim dt1 As DateTime = DateTime.Now

        Dim ts1 As New TimeSpan(100, 0, 0, 0)
        Dim ts2 As New TimeSpan(100, 0, 0, 0)
        Dim dt2 As DateTime = dt1 + ts1
        Dim dt3 As DateTime = dt1 - ts1

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=svdpo051;Initial Catalog=BPTB001;User Id=ado_bptb001;Password=ado_bptb001"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()

        'strSQL = "SELECT T_INV_HD_TB.OLD_INVNO,T_INV_HD_TB.INVFROM,T_INV_HD_TB.INVON, T_INV_HD_TB.BLDATE, Sum(T_INV_BD_TB.KIN) AS KINの合計,T_INV_HD_TB.INVOICENO,T_INV_HD_TB.STAMP,T_INV_HD_TB.RATE,(Sum(T_INV_BD_TB.KIN) * T_INV_HD_TB.RATE) as JPY,T_INV_HD_TB.BOOKINGNO,T_INV_HD_TB.SHIPPEDPER,T_INV_HD_TB.SHIPBASE,T_INV_HD_TB.VOYAGENO "
        'strSQL = strSQL & "FROM T_INV_BD_TB RIGHT JOIN T_INV_HD_TB ON T_INV_BD_TB.INVOICENO = T_INV_HD_TB.INVOICENO "
        'strSQL = strSQL & "WHERE "
        'strSQL = strSQL & " T_INV_HD_TB.BOOKINGNO is not null "
        'strSQL = strSQL & "GROUP BY T_INV_HD_TB.OLD_INVNO, T_INV_HD_TB.BLDATE,T_INV_HD_TB.INVOICENO,T_INV_HD_TB.STAMP,T_INV_HD_TB.RATE,T_INV_HD_TB.BOOKINGNO,T_INV_HD_TB.SHIPPEDPER,T_INV_HD_TB.SHIPBASE,T_INV_HD_TB.INVFROM,T_INV_HD_TB.INVON,T_INV_HD_TB.VOYAGENO "
        'strSQL = strSQL & "HAVING (((T_INV_HD_TB.BOOKINGNO) = '" & bkgno & "')) "
        'strSQL = strSQL & "AND (((T_INV_HD_TB.OLD_INVNO) = '" & ivno & "')) "
        'strSQL = strSQL & "AND ((Sum(T_INV_BD_TB.KIN))>0 ) "
        'strSQL = strSQL & "AND T_INV_HD_TB.STAMP = (SELECT MAX(T_INV_HD_TB.STAMP) T_INV_HD_TB WHERE T_INV_HD_TB.BOOKINGNO = '" & bkgno & "') "
        'strSQL = strSQL & "order by T_INV_HD_TB.STAMP DESC "



        strSQL = "SELECT T_INV_HD_TB.OLD_INVNO,T_INV_HD_TB.INVFROM,T_INV_HD_TB.INVON, T_INV_HD_TB.BLDATE, Sum(T_INV_BD_TB.KIN) AS KINの合計,T_INV_HD_TB.INVOICENO,T_INV_HD_TB.STAMP,T_INV_HD_TB.RATE,(Sum(T_INV_BD_TB.KIN) * T_INV_HD_TB.RATE) as JPY,T_INV_HD_TB.BOOKINGNO,T_INV_HD_TB.SHIPPEDPER,T_INV_HD_TB.SHIPBASE,T_INV_HD_TB.VOYAGENO "
        strSQL = strSQL & "FROM T_INV_BD_TB RIGHT JOIN T_INV_HD_TB ON T_INV_BD_TB.INVOICENO = T_INV_HD_TB.INVOICENO "
        strSQL = strSQL & "WHERE "
        '    strSQL = strSQL & "T_INV_HD_TB.SALESFLG = '1' "
        '    strSQL = strSQL & "AND T_INV_HD_TB.BOOKINGNO is not null "
        strSQL = strSQL & " T_INV_HD_TB.BOOKINGNO is not null "
        strSQL = strSQL & " AND T_INV_HD_TB.BLDATE BETWEEN '" & dt3 & "' AND '" & dt2 & "' "

        strSQL = strSQL & "GROUP BY T_INV_HD_TB.OLD_INVNO, T_INV_HD_TB.BLDATE,T_INV_HD_TB.INVOICENO,T_INV_HD_TB.STAMP,T_INV_HD_TB.RATE,T_INV_HD_TB.BOOKINGNO,T_INV_HD_TB.SHIPPEDPER,T_INV_HD_TB.SHIPBASE,T_INV_HD_TB.INVFROM,T_INV_HD_TB.INVON,T_INV_HD_TB.VOYAGENO "


        strSQL = strSQL & "HAVING (((T_INV_HD_TB.OLD_INVNO) = " & Chr(39) & ivno & Chr(39) & ")) "
        strSQL = strSQL & "AND ((Sum(T_INV_BD_TB.KIN))>0 ) "
        strSQL = strSQL & "AND T_INV_HD_TB.STAMP = (SELECT MAX(T_INV_HD_TB.STAMP) T_INV_HD_TB WHERE T_INV_HD_TB.OLD_INVNO = " & Chr(39) & ivno & Chr(39) & ") "
        strSQL = strSQL & "order by T_INV_HD_TB.STAMP DESC "



        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        strDate = ""
        '結果を取り出す 
        While (dataread.Read())

            str01 = Convert.ToString(dataread("BOOKINGNO"))        'ETD(計上日)
            str02 = Convert.ToString(dataread("VOYAGENO"))        'ETD(計上日)
            str03 = Convert.ToString(dataread("BLDATE"))        'ETD(計上日)
            str04 = Convert.ToString(dataread("KINの合計"))        'ETD(計上日)
            str05 = Convert.ToString(dataread("Rate"))        'ETD(計上日)
            str06 = Convert.ToString(dataread("JPY"))        'ETD(計上日)
            str07 = Convert.ToString(dataread("SHIPPEDPER"))        'ETD(計上日)
            str08 = Convert.ToString(dataread("INVFROM"))        'ETD(計上日)
            str09 = Convert.ToString(dataread("INVON"))        'ETD(計上日)
            str10 = Convert.ToString(dataread("SHIPBASE"))        'ETD(計上日)

            Call UPD_MEMO(bkgno, str01, str02, str03, str04, str05, str06, str07, str08, str09, str10, ivno)

        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()

    End Sub

    Private Sub UPD_MEMO(bkgno As String, str01 As String, str02 As String, str03 As String, str04 As String, str05 As String, str06 As String, str07 As String, str08 As String, str09 As String, str10 As String, ivno As String)
        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=KBHWPM02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand
        Dim strSQL As String = ""

        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand

        Dim dataread2 As SqlDataReader
        Dim dbcmd2 As SqlCommand

        Dim intCnt As Long

        Dim dt1 As DateTime = DateTime.Now

        'データベース接続を開く
        cnn.Open()

        strSQL = ""
        strSQL = strSQL & "UPDATE T_EXL_SHIPPINGMEMOLIST SET "
        strSQL = strSQL & "T_EXL_SHIPPINGMEMOLIST.BOOKING_NO ='" & str01 & "', "
        strSQL = strSQL & "T_EXL_SHIPPINGMEMOLIST.VOY_NO ='" & str02 & "', "
        strSQL = strSQL & "T_EXL_SHIPPINGMEMOLIST.IV_BLDATE ='" & str03 & "', "
        strSQL = strSQL & "T_EXL_SHIPPINGMEMOLIST.KIN_GAIKA ='" & str04 & "', "
        strSQL = strSQL & "T_EXL_SHIPPINGMEMOLIST.RATE ='" & str05 & "', "
        strSQL = strSQL & "T_EXL_SHIPPINGMEMOLIST.KIN_JPY ='" & str06 & "', "
        strSQL = strSQL & "T_EXL_SHIPPINGMEMOLIST.VESSEL ='" & str07 & "', "
        strSQL = strSQL & "T_EXL_SHIPPINGMEMOLIST.LOADING_PORT ='" & str08 & "', "
        strSQL = strSQL & "T_EXL_SHIPPINGMEMOLIST.RECEIVED_PORT ='" & str09 & "', "
        strSQL = strSQL & "T_EXL_SHIPPINGMEMOLIST.SHIP_PLACE ='" & str10 & "' "
        strSQL = strSQL & "WHERE T_EXL_SHIPPINGMEMOLIST.BOOKING_NO ='" & bkgno & "' "
        strSQL = strSQL & "AND T_EXL_SHIPPINGMEMOLIST.INVOICE_NO ='" & ivno & "' "

        Command.CommandText = strSQL
        ' SQLの実行
        Command.ExecuteNonQuery()

        cnn.Close()
        cnn.Dispose()

    End Sub

    Private Sub UPD_MEMO02(bkgno As String, str01 As String, str02 As String)
        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=KBHWPM02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand
        Dim strSQL As String = ""
        Dim ivno As String = ""
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim dataread2 As SqlDataReader
        Dim dbcmd2 As SqlCommand
        Dim intCnt As Long
        Dim dt1 As DateTime = DateTime.Now

        'データベース接続を開く
        cnn.Open()

        strSQL = ""
        strSQL = strSQL & "UPDATE T_EXL_SHIPPINGMEMOLIST SET "
        strSQL = strSQL & "T_EXL_SHIPPINGMEMOLIST.REV_SALESDATE ='" & str01 & "', "
        strSQL = strSQL & "T_EXL_SHIPPINGMEMOLIST.REV_STATUS ='" & str02 & "' "
        strSQL = strSQL & "WHERE T_EXL_SHIPPINGMEMOLIST.BOOKING_NO ='" & bkgno & "' "

        Command.CommandText = strSQL
        ' SQLの実行
        Command.ExecuteNonQuery()

        cnn.Close()
        cnn.Dispose()

    End Sub

    Private Sub UPD_MEMO03(bkgno As String, str01 As String)
        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=KBHWPM02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand
        Dim strSQL As String = ""
        Dim ivno As String = ""
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim dataread2 As SqlDataReader
        Dim dbcmd2 As SqlCommand
        Dim intCnt As Long
        Dim dt1 As DateTime = DateTime.Now

        'データベース接続を開く
        cnn.Open()

        strSQL = ""
        strSQL = strSQL & "UPDATE T_EXL_SHIPPINGMEMOLIST SET "
        strSQL = strSQL & "T_EXL_SHIPPINGMEMOLIST.CHECKFLG ='" & str01 & "' "
        strSQL = strSQL & "WHERE T_EXL_SHIPPINGMEMOLIST.BOOKING_NO ='" & bkgno & "' "

        Command.CommandText = strSQL
        ' SQLの実行
        Command.ExecuteNonQuery()

        cnn.Close()
        cnn.Dispose()
    End Sub

    Protected Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList1.SelectedIndexChanged

        Dim strstart As Date
        Dim strend As Date
        Dim strstart2 As String
        Dim strend2 As String
        Dim Dataobj As New DBAccess
        Dim strd1 As String
        Dim strd2 As String

        If CheckBox1.Checked = True Then
            If DropDownList1.Text = "未回収" Then
                strd1 = DropDownList1.Text
                strd2 = DropDownList2.Text

                strstart = TextBox1.Text
                strstart2 = strstart.ToString("yyyy/MM/dd")

                strend = TextBox2.Text
                strend2 = strend.ToString("yyyy/MM/dd")

                Dim ds As DataSet = Dataobj.GET_CS_RESULT_SHIPPINGMEMO(strstart2, strend2, strd1, strd2)
                If ds.Tables.Count > 0 Then
                    GridView2.DataSourceID = ""
                    GridView2.DataSource = ds
                    Session("data") = ds
                End If

                'Grid再表示
                GridView2.DataBind()

                DropDownList2.Items.Clear()
                DropDownList2.Items.Insert(0, "--Select--")

                Panel1.Visible = False
                Panel2.Visible = True
                Panel3.Visible = False

            ElseIf DropDownList1.Text = "修正状況" Then
                DropDownList2.Items.Clear()
                DropDownList2.DataSource = SqlDataSource5
                DropDownList2.DataTextField = "REV_STATUS"
                DropDownList2.DataValueField = "REV_STATUS"
                DropDownList2.DataBind()

                DropDownList2.Items.Insert(0, "--Select--")

            ElseIf DropDownList1.Text = "--Select--" Then

                DropDownList2.Items.Clear()
                DropDownList2.Items.Insert(0, "--Select--")

            End If
        Else

            If DropDownList1.Text = "未回収" Then

                GridView2.DataSource = SqlDataSource2
                GridView2.DataBind()

                DropDownList2.Items.Clear()
                DropDownList2.Items.Insert(0, "--Select--")

                Panel1.Visible = False
                Panel2.Visible = True
                Panel3.Visible = False

            ElseIf DropDownList1.Text = "修正状況" Then

                DropDownList2.Items.Clear()
                DropDownList2.DataSource = SqlDataSource5
                DropDownList2.DataTextField = "REV_STATUS"
                DropDownList2.DataValueField = "REV_STATUS"
                DropDownList2.DataBind()

                DropDownList2.Items.Insert(0, "--Select--")

            ElseIf DropDownList1.Text = "--Select--" Then

                DropDownList2.Items.Clear()
                DropDownList2.Items.Insert(0, "--Select--")

            End If
        End If

    End Sub
    Protected Sub DropDownList2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList2.SelectedIndexChanged

        Dim Dataobj As New DBAccess
        Dim strstart As Date
        Dim strend As Date
        Dim strstart2 As String
        Dim strend2 As String
        Dim strd1 As String
        Dim strd2 As String

        If CheckBox1.Checked = True Then
            If DropDownList1.Text = "修正状況" Then

                strd1 = DropDownList1.Text
                strd2 = DropDownList2.Text

                strstart = TextBox1.Text
                strstart2 = strstart.ToString("yyyy/MM/dd")

                strend = TextBox2.Text
                strend2 = strend.ToString("yyyy/MM/dd")

                Dim ds As DataSet = Dataobj.GET_CS_RESULT_SHIPPINGMEMO(strstart2, strend2, strd1, strd2)
                If ds.Tables.Count > 0 Then
                    GridView2.DataSourceID = ""
                    GridView2.DataSource = ds
                    Session("data") = ds
                End If

                'Grid再表示
                GridView2.DataBind()

                DropDownList2.Items.Clear()
                DropDownList2.Items.Insert(0, "--Select--")

            Else

                GridView2.DataSource = SqlDataSource3
                GridView2.DataBind()

                DropDownList2.Items.Clear()
                DropDownList2.Items.Insert(0, "--Select--")

            End If

            Panel1.Visible = False
            Panel2.Visible = True
            Panel3.Visible = False


        Else



            GridView2.DataSource = SqlDataSource3
            GridView2.DataBind()

            DropDownList2.Items.Clear()
            DropDownList2.Items.Insert(0, "--Select--")

            Panel1.Visible = False
            Panel2.Visible = True
            Panel3.Visible = False

        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        If CheckBox1.Checked = True Then

            Dim Dataobj As New DBAccess
            Dim strCUST As String = TextBox1.Text
            Dim strstart As Date
            Dim strend As Date
            Dim strstart2 As String
            Dim strend2 As String

            Dim strd1 As String
            Dim strd2 As String

            strd1 = ""
            strd2 = ""

            strstart = TextBox1.Text
            strstart2 = strstart.ToString("yyyy/MM/dd")

            strend = TextBox2.Text
            strend2 = strend.ToString("yyyy/MM/dd")

            Dim ds As DataSet = Dataobj.GET_CS_RESULT_SHIPPINGMEMO(strstart2, strend2, strd1, strd2)
            If ds.Tables.Count > 0 Then
                GridView2.DataSourceID = ""
                GridView2.DataSource = ds
                Session("data") = ds
            End If

            'Grid再表示
            GridView2.DataBind()
            Panel1.Visible = False
            Panel2.Visible = True
            Panel3.Visible = False

        Else

            Panel1.Visible = True
            Panel2.Visible = False
            Panel3.Visible = False

        End If

        DropDownList1.Items.Clear()

        DropDownList1.Items.Insert(0, "--Select--")
        DropDownList1.Items.Insert(1, "未回収")
        DropDownList1.Items.Insert(2, "修正状況")


        DropDownList2.Items.Clear()
        DropDownList2.Items.Insert(0, "--Select--")

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        If Panel3.Visible = True Then

            Panel1.Visible = True
            Panel2.Visible = True
            Panel3.Visible = False

        Else

            Panel1.Visible = False
            Panel2.Visible = False
            Panel3.Visible = True

        End If

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        'Dim i As Integer
        ''フォーマットを指定する場合
        'For i = 0 To GridView1.Rows.Count - 1
        '    Me.GridView1.Rows(i).Cells(4).Style.Add(“mso-number-format”, “yyyy\\/mm\\/dd”)
        '    Me.GridView1.Rows(i).Cells(5).Style.Add(“mso-number-format”, “yyyy\\/mm\\/dd”)
        '    Me.GridView1.Rows(i).Cells(6).Style.Add(“mso-number-format”, “yyyy\\/mm\\/dd”)
        '    Me.GridView1.Rows(i).Cells(3).Style.Add(“mso-number-format”, “yyyy\\/mm\\/dd”)
        '    Me.GridView1.Rows(i).Cells(8).Style.Add(“mso-number-format”, “yyyy\\/mm\\/dd”)
        '    Me.GridView1.Rows(i).Cells(9).Style.Add(“mso-number-format”, “yyyy\\/mm\\/dd”)

        'Next

        '' Set the content type to Excel.
        'Response.AddHeader(“content-disposition”, “attachment; filename=SHIPPINGMEMO_” & Now.ToString(“yyyyMMddhh”) & “.xls”)
        'Response.ContentType = “application/vnd.ms-excel”
        'Response.Charset = “”
        '' Turn off the view state.
        'Me.EnableViewState = False
        'Dim tw As New System.IO.StringWriter()
        'Dim hw As New System.Web.UI.HtmlTextWriter(tw)
        '' Get the HTML for the control.
        'Me.GridView1.RenderControl(hw)
        '' Write the HTML back to the browser.
        'Response.Write(tw.ToString())
        '' End the response.
        'Response.End()

        Dim dt As New DataTable("GridView_Data")
        For Each cell As TableCell In GridView1.HeaderRow.Cells
            dt.Columns.Add(cell.Text)
        Next
        For Each row As GridViewRow In GridView1.Rows
            dt.Rows.Add()
            For i As Integer = 0 To row.Cells.Count - 1
                dt.Rows(dt.Rows.Count - 1)(i) = Replace(row.Cells(i).Text, "&nbsp;", "")
            Next
        Next
        Using wb As New XLWorkbook()
            wb.Worksheets.Add(dt)
            wb.SaveAs("C:\Users\T43529\OneDrive - 株式会社エクセディ\デスクトップ\新ツール\SHIPPINGMEMO_" & Now.ToString(“yyyyMMddhh”) & ".xlsx")

        End Using



    End Sub


    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        ' このOverridesは以下のエラーを回避するために必要です。
        ' 「GridViewのコントロールGridView1は、runat=server を含む
        '  form タグの内側に置かなければ成りません」    
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged

        If CheckBox1.Checked = True Then
            If TextBox1.Text = "" Or TextBox2.Text = "" Then
                CheckBox1.Checked = False
                MsgBox("日付を指定してください。")
            Else
                Label1.Text = "フィルタ 期間指定"
            End If
        Else
            Label1.Text = "フィルタ 全案件"
            TextBox1.text = ""
            TextBox2.Text = ""
        End If

    End Sub

    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load

        If CheckBox1.Checked = True Then
            Label1.Text = "フィルタ 期間指定"
        Else
            Label1.Text = "フィルタ 全案件"
        End If

    End Sub

    Private Sub GridView2_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView2.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.Cells(11).Text = "月またぎ" Then
                e.Row.BackColor = Drawing.Color.DarkSalmon
            ElseIf e.Row.Cells(11).Text = "出港済み" Then
                e.Row.BackColor = Drawing.Color.LightBlue
            End If

            Dim dt00 As DateTime = DateTime.Now
            Dim ts1 As New TimeSpan(7, 0, 0, 0)
            Dim dt01 As DateTime = dt00 + ts1

            If e.Row.Cells(8).Text = "&nbsp;" Or Trim(e.Row.Cells(8).Text) = "" Then
                If e.Row.Cells(6).Text = "&nbsp;" Or Trim(e.Row.Cells(6).Text) = "" Then
                    Dim dt4 As DateTime = DateTime.Parse(e.Row.Cells(5).Text)
                    If dt4 <= dt01 Then
                        e.Row.BackColor = Drawing.Color.Red
                    Else
                    End If
                Else
                    Dim dt3 As DateTime = DateTime.Parse(e.Row.Cells(6).Text)
                    If dt3 <= dt01 Then
                        e.Row.BackColor = Drawing.Color.Red
                    Else
                    End If
                End If
            End If
        End If

    End Sub

    Private Sub GridView3_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridView3.RowDataBound

        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.Cells(11).Text = "月またぎ" Then
                e.Row.BackColor = Drawing.Color.DarkSalmon
            ElseIf e.Row.Cells(11).Text = "出港済み" Then
                e.Row.BackColor = Drawing.Color.LightBlue
            End If

            Dim dt00 As DateTime = DateTime.Now
            Dim ts1 As New TimeSpan(7, 0, 0, 0)
            Dim dt01 As DateTime = dt00 + ts1

            If e.Row.Cells(8).Text = "&nbsp;" Or Trim(e.Row.Cells(8).Text) = "" Then
                If e.Row.Cells(6).Text = "&nbsp;" Or Trim(e.Row.Cells(6).Text) = "" Then
                    Dim dt4 As DateTime = DateTime.Parse(e.Row.Cells(5).Text)
                    If dt4 <= dt01 Then
                        e.Row.BackColor = Drawing.Color.Red
                    Else
                    End If
                Else
                    Dim dt3 As DateTime = DateTime.Parse(e.Row.Cells(6).Text)
                    If dt3 <= dt01 Then
                        e.Row.BackColor = Drawing.Color.Red
                    Else
                    End If
                End If
            End If
        End If

    End Sub
End Class
