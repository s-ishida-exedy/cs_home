Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.Common
Imports System.IO
Imports ClosedXML.Excel

Partial Class cs_home
    Inherits System.Web.UI.Page

    Public strRow As String
    Public strProcess As String
    Private Const strPath As String = "C:\express\cs_home\files\"       'サーバー上ファイルのフルパス

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
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
            If CType(GridView1.Rows(I).Cells(0).Controls(1), CheckBox).Checked Then
                Call GET_IVDATA(Trim(GridView1.Rows(I).Cells(9).Text), strtype)
            Else
            End If
        Next

        'Grid再表示
        GridView1.DataBind()
        GridView2.DataBind()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=KBHWPM02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand
        Dim strSQL As String = ""
        Dim ivno As String = ""
        Dim strtype As String = "2"

        'データベース接続を開く
        cnn.Open()

        '非表示ボタン　FLG03は非表示
        Dim I As Integer
        For I = 0 To GridView1.Rows.Count - 1
            If CType(GridView1.Rows(I).Cells(0).Controls(1), CheckBox).Checked Then
                Call GET_IVDATA(Trim(GridView1.Rows(I).Cells(9).Text), strtype)
            Else
            End If
        Next

        'Grid再表示
        GridView1.DataBind()
        GridView2.DataBind()

    End Sub

    Private Sub GET_IVDATA(bkgno As String, strtype As String)

        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String
        Dim strinv As String

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=svdpo051;Initial Catalog=BPTB001;User Id=ado_bptb001;Password=ado_bptb001"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        Dim dt1 As DateTime = DateTime.Now

        Dim ts1 As New TimeSpan(100, 0, 0, 0)
        Dim ts2 As New TimeSpan(100, 0, 0, 0)
        Dim dt2 As DateTime = dt1 + ts1
        Dim dt3 As DateTime = dt1 - ts1


        'データベース接続を開く
        cnn.Open()

        strSQL = "SELECT T_INV_HD_TB.OLD_INVNO "
        strSQL = strSQL & "FROM T_INV_HD_TB LEFT JOIN T_INV_BD_TB ON T_INV_HD_TB.INVOICENO = T_INV_BD_TB.INVOICENO "

        strSQL = strSQL & "WHERE T_INV_HD_TB.BLDATE BETWEEN '" & dt3 & "' AND '" & dt2 & "' "
        strSQL = strSQL & "GROUP BY T_INV_HD_TB.BOOKINGNO, T_INV_HD_TB.OLD_INVNO, T_INV_HD_TB.SHIPPEDPER, T_INV_HD_TB.VOYAGENO, T_INV_HD_TB.IOPORTDATE, T_INV_HD_TB.CUTDATE "
        strSQL = strSQL & "HAVING T_INV_HD_TB.BOOKINGNO = '" & bkgno & "' "
        'strSQL = strSQL & "AND Sum(T_INV_BD_TB.QTY) >= 1 "
        'strSQL = strSQL & "AND Sum(T_INV_BD_TB.KIN) >= 1 "
        'strSQL = strSQL & "order by T_INV_HD_TB.CUTDATE Decs "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        strDate = ""
        '結果を取り出す 
        While (dataread.Read())
            strinv = Convert.ToString(dataread("OLD_INVNO"))        'ETD(計上日)
            If strtype = "1" Then
                Call INS_ITK(strinv, bkgno)
                Call UPD_ITK(strinv, bkgno)
            ElseIf strtype = "2" Then
                Call UPD_CNL(strinv, bkgno)
            End If
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()

    End Sub
    Private Sub INS_ITK(strinv As String, bkgno As String)
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
        strSQL = strSQL & "SELECT COUNT(*) AS RecCnt FROM T_EXL_WORKSTATUS00 WHERE "
        strSQL = strSQL & "T_EXL_WORKSTATUS00.ID = '001' "
        strSQL = strSQL & "AND T_EXL_WORKSTATUS00.INVNO = '" & strinv & "' "
        strSQL = strSQL & "AND T_EXL_WORKSTATUS00.BKGNO = '" & bkgno & "' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        While (dataread.Read())
            intCnt = dataread("RecCnt")
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()

        If intCnt > 0 Then

            strSQL = ""
            strSQL = strSQL & "UPDATE T_EXL_WORKSTATUS00 SET "
            strSQL = strSQL & "T_EXL_WORKSTATUS00.INVNO = '" & strinv & "', "
            strSQL = strSQL & "T_EXL_WORKSTATUS00.REGDATE = '" & Format(Now(), "yyyy/MM/dd") & "', "
            strSQL = strSQL & "T_EXL_WORKSTATUS00.BKGNO = '" & bkgno & "' "
            strSQL = strSQL & "WHERE T_EXL_WORKSTATUS00.INVNO ='" & strinv & "' "
            strSQL = strSQL & "AND T_EXL_WORKSTATUS00.BKGNO = '" & bkgno & "' "
            strSQL = strSQL & "AND T_EXL_WORKSTATUS00.ID = '001' "

        Else

            strSQL = ""
            strSQL = strSQL & "INSERT INTO T_EXL_WORKSTATUS00 VALUES("
            strSQL = strSQL & " '001' "
            strSQL = strSQL & ",'" & strinv & "' "
            strSQL = strSQL & ",'" & bkgno & "' "
            strSQL = strSQL & ",'" & Format(Now(), "yyyy/MM/dd") & "' "
            strSQL = strSQL & ",'" & Session("UsrId") & "_03" & "' "
            strSQL = strSQL & ")"

        End If

        Command.CommandText = strSQL
        ' SQLの実行
        Command.ExecuteNonQuery()

        cnn.Close()
        cnn.Dispose()

    End Sub


    Private Sub UPD_ITK(strinv As String, bkgno As String)
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
        strSQL = strSQL & "UPDATE T_EXL_DECKANRIHYO SET "
        strSQL = strSQL & "T_EXL_DECKANRIHYO.IFLG = '1' "
        strSQL = strSQL & "WHERE T_EXL_DECKANRIHYO.BOOKING_NO ='" & bkgno & "' "

        Command.CommandText = strSQL
        ' SQLの実行
        Command.ExecuteNonQuery()

        cnn.Close()
        cnn.Dispose()

    End Sub


    Private Sub UPD_CNL(strinv As String, bkgno As String)
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
        strSQL = strSQL & "UPDATE T_EXL_DECKANRIHYO SET "
        strSQL = strSQL & "T_EXL_DECKANRIHYO.IFLG = '2' "
        strSQL = strSQL & "WHERE T_EXL_DECKANRIHYO.BOOKING_NO ='" & bkgno & "' "

        Command.CommandText = strSQL
        ' SQLの実行
        Command.ExecuteNonQuery()

        cnn.Close()
        cnn.Dispose()

    End Sub



    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click


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
        For I = 0 To GridView2.Rows.Count - 1
            If CType(GridView2.Rows(I).Cells(0).Controls(1), CheckBox).Checked Then
                Call GET_IVDATA2(Trim(GridView2.Rows(I).Cells(9).Text), strtype)
            Else
            End If
        Next


        'Grid再表示
        GridView1.DataBind()
        GridView2.DataBind()

    End Sub



    Private Sub GET_IVDATA2(bkgno As String, strtype As String)

        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String
        Dim strinv As String

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=svdpo051;Initial Catalog=BPTB001;User Id=ado_bptb001;Password=ado_bptb001"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        Dim dt1 As DateTime = DateTime.Now

        Dim ts1 As New TimeSpan(100, 0, 0, 0)
        Dim ts2 As New TimeSpan(100, 0, 0, 0)
        Dim dt2 As DateTime = dt1 + ts1
        Dim dt3 As DateTime = dt1 - ts1

        'データベース接続を開く
        cnn.Open()

        strSQL = "SELECT T_INV_HD_TB.OLD_INVNO "
        strSQL = strSQL & "FROM T_INV_HD_TB LEFT JOIN T_INV_BD_TB ON T_INV_HD_TB.INVOICENO = T_INV_BD_TB.INVOICENO "

        strSQL = strSQL & "WHERE T_INV_HD_TB.BLDATE BETWEEN '" & dt3 & "' AND '" & dt2 & "' "
        strSQL = strSQL & "GROUP BY T_INV_HD_TB.BOOKINGNO, T_INV_HD_TB.OLD_INVNO, T_INV_HD_TB.SHIPPEDPER, T_INV_HD_TB.VOYAGENO, T_INV_HD_TB.IOPORTDATE, T_INV_HD_TB.CUTDATE "
        strSQL = strSQL & "HAVING T_INV_HD_TB.BOOKINGNO = '" & bkgno & "' "
        'strSQL = strSQL & "AND Sum(T_INV_BD_TB.QTY) >= 1 "
        'strSQL = strSQL & "AND Sum(T_INV_BD_TB.KIN) >= 1 "
        'strSQL = strSQL & "order by T_INV_HD_TB.CUTDATE Decs "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        strDate = ""
        '結果を取り出す 
        While (dataread.Read())

            strinv = Convert.ToString(dataread("OLD_INVNO"))        'ETD(計上日)

            If strtype = "1" Then
                Call DEL_ITK(strinv, bkgno)
                Call DEL_ITK2(strinv, bkgno)
            ElseIf strtype = "2" Then
            End If
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()

    End Sub


    Private Sub DEL_ITK(strinv As String, bkgno As String)
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
        strSQL = strSQL & "UPDATE T_EXL_DECKANRIHYO SET "
        strSQL = strSQL & "T_EXL_DECKANRIHYO.IFLG = '0' "
        strSQL = strSQL & "WHERE T_EXL_DECKANRIHYO.BOOKING_NO ='" & bkgno & "' "

        Command.CommandText = strSQL
        ' SQLの実行
        Command.ExecuteNonQuery()

        cnn.Close()
        cnn.Dispose()

    End Sub


    Private Sub DEL_ITK2(strinv As String, bkgno As String)
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
        strSQL = strSQL & "SELECT COUNT(*) AS RecCnt FROM T_EXL_WORKSTATUS00 WHERE "
        strSQL = strSQL & "T_EXL_WORKSTATUS00.ID = '001' "
        strSQL = strSQL & "AND T_EXL_WORKSTATUS00.INVNO = '" & strinv & "' "
        strSQL = strSQL & "AND T_EXL_WORKSTATUS00.BKGNO = '" & bkgno & "' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        While (dataread.Read())
            intCnt = dataread("RecCnt")
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()

        If intCnt > 0 Then

            strSQL = ""
            strSQL = strSQL & "DELETE FROM T_EXL_WORKSTATUS00 "
            strSQL = strSQL & "WHERE T_EXL_WORKSTATUS00.ID = '001' "
            strSQL = strSQL & "AND T_EXL_WORKSTATUS00.INVNO ='" & strinv & "' "
            strSQL = strSQL & "AND T_EXL_WORKSTATUS00.BKGNO ='" & bkgno & "' "

        Else


        End If

        Command.CommandText = strSQL
        ' SQLの実行
        Command.ExecuteNonQuery()

        cnn.Close()
        cnn.Dispose()


    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click


        If Panel1.Visible = True Then

            Panel1.Visible = False
            Panel2.Visible = True
            Me.Label5.Text = "申告済等"

        ElseIf Panel2.Visible = True Then

            Panel1.Visible = True
            Panel2.Visible = False
            Me.Label5.Text = "申告未"

        End If

        'Grid再表示
        GridView1.DataBind()
        GridView2.DataBind()

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click


        Dim t As Integer
        t = 1
        Dim cnt As Integer = 0

        Dim val01 As String = ""

        Using wb As XLWorkbook = New XLWorkbook()
            Dim ws As IXLWorksheet = wb.AddWorksheet("管理表")
            For Each cell As TableCell In GridView2.HeaderRow.Cells

                If cnt < 2 Then
                    cnt = cnt + 1
                Else
                    val01 = Trim(Replace(cell.Text, "&nbsp;", ""))
                    ws.Cell(1, t).Value = val01
                    t = t + 1
                End If
            Next

            cnt = 0
            t = 2
            For Each row As GridViewRow In GridView2.Rows


                For i As Integer = 2 To row.Cells.Count - 1

                    val01 = Trim(Replace(row.Cells(i).Text, "&nbsp;", ""))
                    Select Case i

                        Case 1 To 2

                            If IsDate(val01) = True Then
                                ws.Cell(t, i - 1).SetValue(DateValue(val01))
                            Else
                                ws.Cell(t, i - 1).SetValue(val01)
                            End If

                        Case Else
                            ws.Cell(t, i - 1).SetValue(val01)
                    End Select

                Next
                t = t + 1

            Next

            ws.Style.Font.FontName = "Meiryo UI"
            ws.Columns.AdjustToContents()
            ws.SheetView.FreezeRows(1)

            Dim struid As String = Session("UsrId")
            wb.SaveAs("\\svnas201\EXD06101\DISC_COMMON\WEB出力\特定輸出管理表_" & Now.ToString(“yyyyMMddhhmmss”) & "_PIC_" & struid & ".xlsx")

        End Using

        Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "確認", "<script language='JavaScript'>confirm('出力が完了しました。\n出力先：\\\svnas201\\EXD06101\\DISC_COMMON\\WEB出力');</script>", False)


    End Sub
End Class


