
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


    Private Sub GridView1_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound

        Dim str01 As String = ""
        Dim str02 As String = ""
        Dim str03 As String = ""
        Dim str04 As String = ""
        Dim str05 As String = ""

        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.Cells(12).Text = "月またぎ" Then
                e.Row.BackColor = Drawing.Color.DarkSalmon
            ElseIf e.Row.Cells(12).Text = "月またぎP" Then
                e.Row.BackColor = Drawing.Color.DarkOrange
            ElseIf e.Row.Cells(12).Text = "出港済み" Then
                e.Row.BackColor = Drawing.Color.LightBlue
            ElseIf e.Row.Cells(12).Text = "月またぎ前月" Then
                e.Row.BackColor = Drawing.Color.LightGreen
            End If

            If IsPostBack = True Then

                If e.Row.Cells(9).Text = "" Or e.Row.Cells(9).Text = "&nbsp;" Then
                    Call GET_IVDATA(Trim(e.Row.Cells(13).Text), Trim(e.Row.Cells(3).Text), e.Row.Cells(4).Text)
                End If

                Dim dt0 As DateTime = DateTime.Parse(e.Row.Cells(4).Text)

                If Replace(e.Row.Cells(9).Text, "&nbsp;", "") <> "" And Replace(e.Row.Cells(12).Text, "&nbsp;", "") = "" Then
                    If Replace(e.Row.Cells(4).Text, "&nbsp;", "") <> "" And Replace(e.Row.Cells(10).Text, "&nbsp;", "") <> "" Then
                        Dim dt1 As DateTime = DateTime.Parse(e.Row.Cells(10).Text)
                        If dt0.ToString("MM") = dt1.ToString("MM") Then
                            str02 = "不要"
                            str01 = "-"
                            Call UPD_MEMO02(Trim(e.Row.Cells(13).Text), str01, str02)
                        Else
                            str02 = "要"
                            str01 = "確認要"
                            Call UPD_MEMO02(Trim(e.Row.Cells(13).Text), str01, str02)
                        End If
                    End If
                End If


                If e.Row.Cells(15).Text <> "&nbsp;" Then
                    If Trim(e.Row.Cells(15).Text) <> "" Then
                        Dim dt2 As DateTime = DateTime.Parse(e.Row.Cells(15).Text)
                        If dt0.ToString("MM") = dt2.ToString("MM") Then
                            str03 = "○"
                            Call UPD_MEMO03(Trim(e.Row.Cells(13).Text), str03)
                        Else
                            str03 = "Ｘ"
                            Call UPD_MEMO03(Trim(e.Row.Cells(13).Text), str03)
                        End If
                    End If
                End If

                If e.Row.Cells(5).Text <> "&nbsp;" Then
                    If Trim(e.Row.Cells(5).Text) <> "" Then
                        Dim dt3 As DateTime = DateTime.Parse(e.Row.Cells(5).Text)
                        If dt0.ToString("MM") = dt3.ToString("MM") Then
                            str04 = "○"
                            Call UPD_MEMO03(Trim(e.Row.Cells(13).Text), str04)
                        Else
                            str04 = "Ｘ"
                            Call UPD_MEMO03(Trim(e.Row.Cells(13).Text), str04)
                        End If
                    End If
                End If

            End If

            Dim dt00 As DateTime = DateTime.Now
            Dim ts1 As New TimeSpan(7, 0, 0, 0)
            Dim dt01 As DateTime = dt00 + ts1

            If e.Row.Cells(9).Text = "&nbsp;" Or Trim(e.Row.Cells(9).Text) = "" Then
                If e.Row.Cells(7).Text = "&nbsp;" Or Trim(e.Row.Cells(7).Text) = "" Then
                    Dim dt4 As DateTime = DateTime.Parse(e.Row.Cells(6).Text)
                    If dt4 <= dt01 Then
                        e.Row.BackColor = Drawing.Color.Red
                    Else
                    End If
                Else

                    Dim dt3 As DateTime = DateTime.Parse(e.Row.Cells(7).Text)

                    If dt3 <= dt01 Then
                        e.Row.BackColor = Drawing.Color.Red
                    Else
                    End If
                End If
            End If
        End If


        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim dltButton As ImageButton = e.Row.FindControl("ImageButton1")
            'ボタンが存在する場合のみセット
            If Not (dltButton Is Nothing) Then
                dltButton.CommandArgument = e.Row.RowIndex.ToString()
            End If
        End If



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
            Call GET_IVDATA(Trim(GridView1.Rows(I).Cells(13).Text), Trim(GridView1.Rows(I).Cells(3).Text), GridView1.Rows(I).Cells(4).Text)
        Next

        'Grid再表示
        GridView1.DataBind()

        DropDownList1.Items.Clear()

        DropDownList1.Items.Insert(0, "--Select--")
        DropDownList1.Items.Insert(1, "未回収")
        DropDownList1.Items.Insert(2, "修正状況")


        DropDownList2.Items.Clear()
        DropDownList2.Items.Insert(0, "--Select--")

    End Sub

    Private Sub GET_IVDATA(bkgno As String, ivno As String, strETD As Date)

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


        Dim ts1 As New TimeSpan(400, 0, 0, 0)
        Dim ts2 As New TimeSpan(100, 0, 0, 0)
        Dim ts3 As New TimeSpan(30, 0, 0, 0)

        Dim dt2 As DateTime = dt1 + ts1
        Dim dt3 As DateTime = dt1 - ts1

        Dim dt4 As DateTime = strETD + ts3
        Dim dt5 As DateTime = strETD - ts3


        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=svdpo051;Initial Catalog=BPTB001;User Id=ado_bptb001;Password=ado_bptb001"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()


        strSQL = "SELECT T_INV_HD_TB.OLD_INVNO,T_INV_HD_TB.INVFROM,T_INV_HD_TB.INVON, T_INV_HD_TB.BLDATE, Sum(T_INV_BD_TB.KIN) AS KINの合計,T_INV_HD_TB.INVOICENO,T_INV_HD_TB.STAMP,T_INV_HD_TB.RATE,(Sum(T_INV_BD_TB.KIN) * T_INV_HD_TB.RATE) as JPY,T_INV_HD_TB.BOOKINGNO,T_INV_HD_TB.SHIPPEDPER,T_INV_HD_TB.SHIPBASE,T_INV_HD_TB.VOYAGENO "
        strSQL = strSQL & "FROM T_INV_BD_TB RIGHT JOIN T_INV_HD_TB ON T_INV_BD_TB.INVOICENO = T_INV_HD_TB.INVOICENO "
        strSQL = strSQL & "WHERE "
        '    strSQL = strSQL & "T_INV_HD_TB.SALESFLG = '1' "
        '    strSQL = strSQL & "AND T_INV_HD_TB.BOOKINGNO is not null "
        strSQL = strSQL & " T_INV_HD_TB.BOOKINGNO is not null "
        strSQL = strSQL & " AND T_INV_HD_TB.BLDATE BETWEEN '" & dt5 & "' AND '" & dt4 & "' "


        'INVOICENOが最大のものを取得
        strSQL = strSQL & "AND T_INV_HD_TB.INVOICENO = "
        strSQL = strSQL & "(SELECT MAX(T_INV_HD_TB.INVOICENO) AS IVNO "
        strSQL = strSQL & "FROM T_INV_HD_TB "

        strSQL = strSQL & "WHERE T_INV_HD_TB.HEADTITLE Like 'INVOICE%' "
        strSQL = strSQL & "AND T_INV_HD_TB.OLD_INVNO  = " & Chr(39) & ivno & Chr(39) & " "
        strSQL = strSQL & "AND T_INV_HD_TB.BLDATE BETWEEN '" & dt5 & "' AND '" & dt4 & "' "
        strSQL = strSQL & "AND T_INV_HD_TB.BOOKINGNO IS NOT NULL "
        strSQL = strSQL & "AND T_INV_HD_TB.ORG_INVOICENO IS NULL "
        strSQL = strSQL & ") "


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
        'strSQL = strSQL & "WHERE T_EXL_SHIPPINGMEMOLIST.BOOKING_NO ='" & str01 & "' "
        'strSQL = strSQL & "AND T_EXL_SHIPPINGMEMOLIST.INVOICE_NO ='" & ivno & "' "

        strSQL = strSQL & "WHERE T_EXL_SHIPPINGMEMOLIST.BOOKING_NO ='" & str01 & "' "
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
                    GridView1.DataSourceID = ""
                    GridView1.DataSource = ds
                    Session("data") = ds
                End If

                'Grid再表示
                GridView1.DataBind()

                DropDownList2.Items.Clear()
                DropDownList2.Items.Insert(0, "--Select--")

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

                GridView1.DataSourceID = ""
                GridView1.DataSource = SqlDataSource2
                GridView1.DataBind()

                DropDownList2.Items.Clear()
                DropDownList2.Items.Insert(0, "--Select--")


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
                    GridView1.DataSourceID = ""
                    GridView1.DataSource = ds
                    Session("data") = ds
                End If

                'Grid再表示
                GridView1.DataBind()

                'DropDownList2.Items.Clear()
                'DropDownList2.Items.Insert(0, "--Select--")



                DropDownList2.Items.Clear()
                DropDownList2.DataSource = SqlDataSource5
                DropDownList2.DataTextField = "REV_STATUS"
                DropDownList2.DataValueField = "REV_STATUS"
                DropDownList2.DataBind()

                DropDownList2.Items.Insert(0, "--Select--")

            Else
                GridView1.DataSourceID = ""
                GridView1.DataSource = SqlDataSource3
                GridView1.DataBind()

                'DropDownList2.Items.Clear()
                'DropDownList2.Items.Insert(0, "--Select--")

                DropDownList2.Items.Clear()
                DropDownList2.DataSource = SqlDataSource5
                DropDownList2.DataTextField = "REV_STATUS"
                DropDownList2.DataValueField = "REV_STATUS"
                DropDownList2.DataBind()

                DropDownList2.Items.Insert(0, "--Select--")

            End If
        Else

            GridView1.DataSourceID = ""
            GridView1.DataSource = SqlDataSource3
            GridView1.DataBind()

            'DropDownList2.Items.Clear()
            'DropDownList2.Items.Insert(0, "--Select--")

            DropDownList2.Items.Clear()
            DropDownList2.DataSource = SqlDataSource5
            DropDownList2.DataTextField = "REV_STATUS"
            DropDownList2.DataValueField = "REV_STATUS"
            DropDownList2.DataBind()

            DropDownList2.Items.Insert(0, "--Select--")

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
                GridView1.DataSourceID = ""
                GridView1.DataSource = ds
                Session("data") = ds
            End If

            'Grid再表示
            GridView1.DataBind()

        Else

            GridView1.DataSourceID = ""
            GridView1.DataSource = SqlDataSource1

            GridView1.DataBind()

        End If

        DropDownList1.Items.Clear()

        DropDownList1.Items.Insert(0, "--Select--")
        DropDownList1.Items.Insert(1, "未回収")
        DropDownList1.Items.Insert(2, "修正状況")


        DropDownList2.Items.Clear()
        DropDownList2.Items.Insert(0, "--Select--")

    End Sub



    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click


        'Dim strFile As String = Format(Now, "yyyyMMdd") & "_SHIPPINGMEMO.xlsx"
        'Dim strPath As String = "C:\exp\cs_home\files\"
        'Dim strChanged As String    'サーバー上のフルパス
        'Dim strFileNm As String     'ファイル名
        'Dim a

        'Dim dt As New DataTable("SHIPPINGMEMO")
        'For Each cell As TableCell In GridView1.HeaderRow.Cells
        '    If cell.Text = "最新ETD" Or cell.Text = "遅延前ETD" Or cell.Text = "最新ETA" Or cell.Text = "遅延前ETA" Or cell.Text = "BL回収" Or cell.Text = "BL上の日付" Or cell.Text = "IV_BLDATE" Then
        '        dt.Columns.Add(cell.Text, Type.GetType("System.DateTime"))
        '    ElseIf cell.Text = "金額（外貨）" Or cell.Text = "レート" Or cell.Text = "金額（JPY）" Then
        '        dt.Columns.Add(cell.Text, Type.GetType("System.Decimal"))
        '    Else
        '        dt.Columns.Add(cell.Text)
        '    End If
        'Next
        'For Each row As GridViewRow In GridView1.Rows
        '    dt.Rows.Add()
        '    For i As Integer = 0 To row.Cells.Count - 1
        '        a = Replace(row.Cells(i).Text, "&nbsp;", "")
        '        If a = "" Then
        '            a = DBNull.Value
        '        End If
        '        dt.Rows(dt.Rows.Count - 1)(i) = a
        '    Next
        'Next
        'Using workbook As New XLWorkbook()
        '    workbook.Worksheets.Add(dt)
        '    workbook.SaveAs(strPath & strFile)

        'End Using

        ''ファイル名を取得する
        'strChanged = strPath & Format(Now, "yyyyMMdd") & "_SHIPPINGMEMO.xlsx"
        'strFileNm = Path.GetFileName(strChanged)

        ''Contentをクリア
        'Response.ClearContent()

        ''Contentを設定
        'Response.ContentEncoding = System.Text.Encoding.GetEncoding("shift-jis")
        'Response.ContentType = "application/vnd.ms-excel"

        ''表示ファイル名を指定
        'Dim fn As String = HttpUtility.UrlEncode(strFileNm)
        'Response.AddHeader("Content-Disposition", "attachment;filename=" + fn)

        ''ダウンロード対象ファイルを指定
        'Response.WriteFile(strChanged)

        ''ダウンロード実行
        'Response.Flush()
        'Response.End()


        '前月分ダウンロードボタン押下
        Dim strFile As String = Format(Now, "yyyyMMdd") & "_SHIPPINGMEMO.xlsx"
        Dim strPath As String = "C:\exp\cs_home\files\"
        Dim strChanged As String    'サーバー上のフルパス
        Dim strFileNm As String     'ファイル名

        Dim dtToday As DateTime = DateTime.Today



        Dim dt = GetNorthwindProductTable()


        Dim a

        Dim dt2 As New DataTable("SHIPPINGMEMO")
        For Each cell As TableCell In GridView1.HeaderRow.Cells
            If cell.Text = "&nbsp;" Then
            ElseIf cell.Text = "最新ETD" Or cell.Text = "遅延前ETD" Or cell.Text = "最新ETA" Or cell.Text = "遅延前ETA" Or cell.Text = "BL回収" Or cell.Text = "BL上の日付" Or cell.Text = "IV_BLDATE" Then
                dt2.Columns.Add(cell.Text, Type.GetType("System.DateTime"))
            ElseIf cell.Text = "金額（外貨）" Or cell.Text = "レート" Or cell.Text = "金額（JPY）" Then
                dt2.Columns.Add(cell.Text, Type.GetType("System.Decimal"))
            Else
                dt2.Columns.Add(cell.Text)
            End If


        Next

        For Each row As DataRow In dt.Rows
            dt2.Rows.Add()
            For i As Integer = 0 To dt.Columns.Count - 1
                a = row(i)
                If a = "" Then
                    a = DBNull.Value
                End If
                dt2.Rows(dt2.Rows.Count - 1)(i) = a
            Next
        Next





        Dim workbook = New XLWorkbook()
        Dim worksheet = workbook.Worksheets.Add(dt2)

        worksheet.Style.Font.FontName = "Meiryo UI"
        worksheet.Style.Alignment.WrapText = False
        worksheet.Columns.AdjustToContents()
        worksheet.SheetView.FreezeRows(1)

        workbook.SaveAs(strPath & strFile)


        'ファイル名を取得する
        Dim strTxtFiles() As String = IO.Directory.GetFiles(strPath, Format(Now, "yyyyMMdd") & "_SHIPPINGMEMO.xlsx")

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
    Private Shared Function GetNorthwindProductTable() As DataTable
        'EXCELファイル出力
        Dim strSQL As String = ""
        Dim strSDate As String = ""
        Dim strEDate As String = ""

        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"

        Dim dt = New DataTable("T_EXL_SHIPPINGMEMOLIST")

        Using conn = New SqlConnection(ConnectionString)
            Dim cmd = conn.CreateCommand()

            strSQL = strSQL & "SELECT CUSTCODE,CUSTNAME,INVOICE_NO,iif(len(REV_ETD)=10,REV_ETD,ETD) AS ETD02,iif(len(REV_ETD)=10,ETD,'') AS ETD03,iif(len(REV_ETA)=10,REV_ETA,ETA) AS ETA02,iif(len(REV_ETA)=10,ETA,'') AS ETA03,SHIP_TYPE,DATE_GETBL,DATE_ONBL,REV_SALESDATE,REV_STATUS,BOOKING_NO,VOY_NO,IV_BLDATE,KIN_GAIKA,RATE,KIN_JPY,VESSEL,LOADING_PORT,RECEIVED_PORT,SHIP_PLACE,CHECKFLG FROM [T_EXL_SHIPPINGMEMOLIST] WHERE CUSTCODE not in ('B494','B490','B491','B492','B520','A063','A064','A060','A061','A062','B530') AND ETD > DATEADD(day,1,EOMONTH(GETDATE()-90, -1)) ORDER BY ETD02,INVOICE_NO "


            cmd.CommandText = strSQL
            Dim sda = New SqlDataAdapter(cmd)
            sda.Fill(dt)
        End Using

        Return dt
    End Function

    Public Overrides Sub VerifyRenderingInServerForm(ByVal control As Control)
        ' このOverridesは以下のエラーを回避するために必要です。
        ' 「GridViewのコントロールGridView1は、runat=server を含む
        '  form タグの内側に置かなければ成りません」    
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged

        If CheckBox1.Checked = True Then
            If TextBox1.Text = "" Or TextBox2.Text = "" Then
                Page.ClientScript.RegisterStartupScript(Me.GetType, "確認", "<script language='JavaScript'>confirm('日付を指定してください。');</script>", False)
                CheckBox1.Checked = False
                Label1.Text = "フィルタ 全案件"
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
    Private Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName = "edt" Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim data0 = Me.GridView1.Rows(index).Cells(3).Text
            Dim data1 = Me.GridView1.Rows(index).Cells(13).Text
            Dim data2 = Me.GridView1.Rows(index).Cells(24).Text

            Session("strMode") = "0"    '更新モード
            Session("strinv") = data0
            Session("strbkg") = data1
            Session("strID") = data2

            'Dim clientScript As String = "<script language='JavaScript'> window.open('shippingmemo_detail.aspx', '', 'width=1500,height=450','scrollbars=no','status=no','toolbar=no','location=no','menubar=no','resizable=no') <" + "/script>"
            'Dim startupScript As String = "<script language='JavaScript'>  window.open('shippingmemo_detail.aspx') <" + "/script>"

            'RegisterClientScriptBlock("client", clientScript)

            Response.Redirect("shippingmemo_detail.aspx")

        End If

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        Response.Redirect("shippingmemo_detail02.aspx")

    End Sub
End Class
