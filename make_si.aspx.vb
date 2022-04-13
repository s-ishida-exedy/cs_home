
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

        '最終更新年月日取得
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strinv As String = ""
        Dim strbkg As String = ""


        Dim wday As String = ""
        Dim wday2 As String = ""
        Dim wday3 As String = ""
        Dim dt1 As DateTime = DateTime.Now
        Dim Kaika00 As String = ""

        If e.Row.RowType = DataControlRowType.DataRow Then


            '接続文字列の作成
            Dim ConnectionString As String = String.Empty

            'SQL Server認証
            ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"

            'SqlConnectionクラスの新しいインスタンスを初期化
            Dim cnn = New SqlConnection(ConnectionString)
            Dim Command = cnn.CreateCommand

            'データベース接続を開く
            cnn.Open()

            'strSQL = "SELECT DOCFIN_BKGNO FROM [T_EXL_CSWORKSTATUS] WHERE [T_EXL_CSWORKSTATUS].DOCFIN_BKGNO = '" & Trim(e.Row.Cells(26).Text) & "' "
            strSQL = "SELECT BKGNO FROM [T_EXL_WORKSTATUS00] WHERE [T_EXL_WORKSTATUS00].BKGNO = '" & Trim(e.Row.Cells(26).Text) & "' "
            strSQL = strSQL & "AND [T_EXL_WORKSTATUS00].ID = '002' "

            'ＳＱＬコマンド作成
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行
            dataread = dbcmd.ExecuteReader()
            strbkg = ""
            '結果を取り出す
            While (dataread.Read())
                strbkg += dataread("BKGNO")
                '書類作成状況
                If Trim(e.Row.Cells(26).Text) = strbkg Then

                    If e.Row.Cells(1).Text = "EXDCUT" Then

                        e.Row.Cells(1).Text = e.Row.Cells(1).Text & " " & "書類済"

                    Else

                        e.Row.BackColor = Drawing.Color.Khaki
                        e.Row.Cells(1).Text = e.Row.Cells(1).Text & " " & "書類済"

                    End If




                End If
            End While

            'クローズ処理
            dataread.Close()
            dbcmd.Dispose()

            'strSQL = "SELECT DECFIN_BKGNO FROM [T_EXL_CSWORKSTATUS] WHERE [T_EXL_CSWORKSTATUS].DECFIN_BKGNO = '" & Trim(e.Row.Cells(26).Text) & "' "
            strSQL = "SELECT BKGNO FROM [T_EXL_WORKSTATUS00] WHERE [T_EXL_WORKSTATUS00].BKGNO = '" & Trim(e.Row.Cells(26).Text) & "' "
            strSQL = strSQL & "AND [T_EXL_WORKSTATUS00].ID = '003' "

            'ＳＱＬコマンド作成
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行
            dataread = dbcmd.ExecuteReader()

            strbkg = ""
            '結果を取り出す
            While (dataread.Read())
                strbkg += dataread("BKGNO")
                '書類作成状況
                If Trim(e.Row.Cells(26).Text) = Trim(strbkg) Then
                    e.Row.BackColor = Drawing.Color.DarkGray
                    e.Row.Cells(1).Text = "通関済"
                End If
            End While

            'クローズ処理
            dataread.Close()
            dbcmd.Dispose()



            'strSQL = "SELECT ITK_BKGNO FROM [T_EXL_CSWORKSTATUS] WHERE [T_EXL_CSWORKSTATUS].ITK_BKGNO = '" & Trim(e.Row.Cells(26).Text) & "' "
            strSQL = "SELECT BKGNO FROM [T_EXL_WORKSTATUS00] WHERE [T_EXL_WORKSTATUS00].BKGNO = '" & Trim(e.Row.Cells(26).Text) & "' "
            strSQL = strSQL & "AND [T_EXL_WORKSTATUS00].ID = '001' "

            'ＳＱＬコマンド作成
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行
            dataread = dbcmd.ExecuteReader()
            strbkg = ""
            '結果を取り出す
            While (dataread.Read())
                strbkg += dataread("BKGNO")
                '書類作成状況
                If Trim(e.Row.Cells(26).Text) = Trim(strbkg) Then
                    e.Row.BackColor = Drawing.Color.DarkGray
                    e.Row.Cells(1).Text = "通関委託"
                End If
            End While

            'クローズ処理
            dataread.Close()
            dbcmd.Dispose()
            cnn.Close()
            cnn.Dispose()

            If e.Row.Cells(1).Text Like "当日必須*" Then
                e.Row.BackColor = Drawing.Color.LightGreen
            End If

            If e.Row.Cells(1).Text Like "EXDCUT*" Then
                e.Row.BackColor = Drawing.Color.LightBlue
            End If

            If e.Row.Cells(12).Text = "LCL" Then
                e.Row.BackColor = Drawing.Color.DarkGray
                e.Row.Cells(13).Text = "LCL"
            End If

            If e.Row.Cells(36).Text = "1" Then
                e.Row.Cells(0).BackColor = Drawing.Color.YellowGreen
            End If

        End If


        '不要行非表示
        e.Row.Cells(10).Visible = False
        e.Row.Cells(11).Visible = False
        e.Row.Cells(12).Visible = False

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

        e.Row.Cells(27).Visible = False
        e.Row.Cells(30).Visible = False
        e.Row.Cells(32).Visible = False

        e.Row.Cells(34).Visible = False
        e.Row.Cells(35).Visible = False
        e.Row.Cells(36).Visible = False
        e.Row.Cells(37).Visible = False



    End Sub



    Protected Sub DropDownList1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList1.SelectedIndexChanged

        If DropDownList1.Text = "進捗状況" Then
            DropDownList2.Items.Clear()
            DropDownList2.DataSource = SqlDataSource2
            DropDownList2.DataTextField = "STATUS"
            DropDownList2.DataValueField = "STATUS"
            DropDownList2.DataBind()

            DropDownList2.Items.Insert(0, "--Select--")

        ElseIf DropDownList1.Text = "シート" Then
            DropDownList2.Items.Clear()
            DropDownList2.DataSource = SqlDataSource3
            DropDownList2.DataTextField = "FORWARDER"
            DropDownList2.DataValueField = "FORWARDER"
            DropDownList2.DataBind()

            DropDownList2.Items.Insert(0, "--Select--")

        ElseIf DropDownList1.Text = "海貨業者" Then
            DropDownList2.Items.Clear()
            DropDownList2.DataSource = SqlDataSource4
            DropDownList2.DataTextField = "FORWARDER02"
            DropDownList2.DataValueField = "FORWARDER02"
            DropDownList2.DataBind()

            DropDownList2.Items.Insert(0, "--Select--")

        ElseIf DropDownList1.Text = "客先コード" Then
            DropDownList2.Items.Clear()
            DropDownList2.DataSource = SqlDataSource8
            DropDownList2.DataTextField = "CUST"
            DropDownList2.DataValueField = "CUST"
            DropDownList2.DataBind()

            DropDownList2.Items.Insert(0, "--Select--")

        End If

    End Sub
    Protected Sub DropDownList2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList2.SelectedIndexChanged

        If DropDownList1.Text = "進捗状況" Then

            GridView1.DataSourceID = ""
            GridView1.DataSource = SqlDataSource1
            GridView1.DataBind()

            DropDownList2.Items.Clear()
            DropDownList2.DataSource = SqlDataSource2
            DropDownList2.DataTextField = "STATUS"
            DropDownList2.DataValueField = "STATUS"
            DropDownList2.DataBind()

            DropDownList2.Items.Insert(0, "--Select--")

        ElseIf DropDownList1.Text = "シート" Then

            GridView1.DataSourceID = ""
            GridView1.DataSource = SqlDataSource5
            GridView1.DataBind()

            DropDownList2.Items.Clear()
            DropDownList2.DataSource = SqlDataSource3
            DropDownList2.DataTextField = "FORWARDER"
            DropDownList2.DataValueField = "FORWARDER"
            DropDownList2.DataBind()

            DropDownList2.Items.Insert(0, "--Select--")

        ElseIf DropDownList1.Text = "海貨業者" Then

            GridView1.DataSourceID = ""
            GridView1.DataSource = SqlDataSource6
            GridView1.DataBind()

            DropDownList2.Items.Clear()
            DropDownList2.DataSource = SqlDataSource4
            DropDownList2.DataTextField = "FORWARDER02"
            DropDownList2.DataValueField = "FORWARDER02"
            DropDownList2.DataBind()

            DropDownList2.Items.Insert(0, "--Select--")

        ElseIf DropDownList1.Text = "客先コード" Then

            GridView1.DataSourceID = ""
            GridView1.DataSource = SqlDataSource9
            GridView1.DataBind()

            DropDownList2.Items.Clear()
            DropDownList2.DataSource = SqlDataSource8
            DropDownList2.DataTextField = "CUST"
            DropDownList2.DataValueField = "CUST"
            DropDownList2.DataBind()

            DropDownList2.Items.Insert(0, "--Select--")

        End If



    End Sub
    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        DropDownList1.Items.Clear()

        DropDownList1.Items.Insert(0, "--Select--")
        DropDownList1.Items.Insert(1, "進捗状況")
        DropDownList1.Items.Insert(2, "シート")
        DropDownList1.Items.Insert(3, "海貨業者")
        DropDownList1.Items.Insert(4, "客先コード")

        DropDownList2.Items.Clear()
        DropDownList2.Items.Insert(0, "--Select--")

        GridView1.DataSourceID = ""
        GridView1.DataSource = SqlDataSource7
        GridView1.DataBind()


    End Sub

    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load

        Dim Kaika00 As String = ""
        Dim deccnt As Long
        Dim ercnt As Long
        Dim lng14 As Long
        Dim lng15 As Long
        Dim WDAYNO00 As String = ""
        Dim WDAY00 As String = ""
        Dim WDAY01 As String = ""
        Dim strSQL As String = ""
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand

        Dim dt1 As DateTime = DateTime.Now


        'If IsPostBack = True Then
        'Else

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"

        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()

        '対象の日付以下の日付の最大値を取得

        strSQL = ""
        strSQL = "SELECT T_EXL_CSWORKDAY.WORKDAY_NO FROM T_EXL_CSWORKDAY WHERE T_EXL_CSWORKDAY.WORKDAY <= '" & dt1.ToShortDateString & "' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)

        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        '結果を取り出す 
        While (dataread.Read())
            WDAYNO00 = dataread("WORKDAY_NO")
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()

        strSQL = ""
        strSQL = "SELECT T_EXL_CSWORKDAY.WORKDAY FROM T_EXL_CSWORKDAY WHERE T_EXL_CSWORKDAY.WORKDAY_NO = '" & Val(WDAYNO00) + 1 & "' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        '結果を取り出す 
        While (dataread.Read())
            WDAY00 = dataread("WORKDAY")
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()

        strSQL = ""
        strSQL = "SELECT T_EXL_CSWORKDAY.WORKDAY FROM T_EXL_CSWORKDAY WHERE T_EXL_CSWORKDAY.WORKDAY_NO = '" & Val(WDAYNO00) + 2 & "' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)

        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        '結果を取り出す 
        While (dataread.Read())
            WDAY01 = dataread("WORKDAY")
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()



        For I = 0 To GridView1.Rows.Count - 1

            deccnt = 0
            deccnt = DEC_GET(Trim(GridView1.Rows(I).Cells(26).Text))

            If Left(GridView1.Rows(I).Cells(2).Text, 2) = "上野" Then
                If GridView1.Rows(I).Cells(7).Text <= WDAY00 Then
                    If deccnt > 0 Then
                        lng14 = lng14 + 1
                    Else
                    End If
                ElseIf GridView1.Rows(I).Cells(7).Text <= WDAY00 And GridView1.Rows(I).Cells(7).Text > WDAY00 Then   ' 1稼働日後がEXDCUT 
                    If deccnt > 0 Then
                        lng15 = lng15 + 1
                    Else
                    End If
                End If
            Else
                If GridView1.Rows(I).Cells(7).Text <= dt1.ToShortDateString Then
                    If deccnt > 0 Then
                        lng14 = lng14 + 1
                    Else
                    End If
                ElseIf GridView1.Rows(I).Cells(7).Text <= WDAY00 And GridView1.Rows(I).Cells(7).Text > dt1.ToShortDateString Then ' 1稼働日後がEXDCUT
                    If deccnt > 0 Then
                        lng15 = lng15 + 1
                    Else
                    End If
                End If
            End If

        Next




        Dim strupddate00 As Date
        Dim strupddate01 As Date



        strSQL = ""
        strSQL = strSQL & "SELECT T_EXL_DATA_UPD.DATA_UPD FROM T_EXL_DATA_UPD "
        strSQL = strSQL & "WHERE T_EXL_DATA_UPD.DATA_CD ='008' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        While (dataread.Read())
            strupddate00 = Trim(dataread("DATA_UPD"))
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()



        strSQL = ""
        strSQL = strSQL & "SELECT COUNT(*) AS RecCnt FROM T_BOOKING "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        While (dataread.Read())
            ercnt = Trim(dataread("RecCnt"))
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()



        Dim dt00 As String = dt1.ToShortDateString
        Dim dt01 As String = strupddate00.ToShortDateString

        If ercnt = 0 Then

            Panel1.Visible = False
            Panel3.Visible = True

        Else

            If dt00 = dt01 Then
                Panel3.Visible = False
            Else
                Panel1.Visible = False
                Panel3.Visible = True
            End If


        End If

        cnn.Close()
        cnn.Dispose()


    End Sub

    Private Function DEC_GET(STRBOOKING_NO As String) As Integer

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

        DEC_GET = 0

        Dim dt1 As DateTime = DateTime.Now

        Dim ts1 As New TimeSpan(100, 0, 0, 0)
        Dim ts2 As New TimeSpan(100, 0, 0, 0)
        Dim dt2 As DateTime = dt1 + ts1
        Dim dt3 As DateTime = dt1 - ts1

        'データベース接続を開く
        cnn.Open()

        strSQL = ""
        strSQL = strSQL & "SELECT COUNT(*) AS RecCnt FROM T_EXL_WORKSTATUS00 WHERE "
        strSQL = strSQL & "T_EXL_WORKSTATUS00.ID = '003' "
        strSQL = strSQL & "AND T_EXL_WORKSTATUS00.BKGNO = '" & STRBOOKING_NO & "' "
        strSQL = strSQL & "AND T_EXL_WORKSTATUS00.REGDATE  between '" & dt3 & "' AND '" & dt2 & "' "


        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        While (dataread.Read())
            DEC_GET = dataread("RecCnt")
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()

        cnn.Close()
        cnn.Dispose()

    End Function
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        Dim strPath00(3) As String      '依頼書、タイムスケジュールのパスと作成先のパス
        Dim strPath01(3) As String      '

        Dim MyStr As String = ""
        Dim strLog As String            '問題報告ログ
        Dim strFile0 As String = ""         'ファイル名(依頼書)
        Dim strFile1 As String = ""         'ファイル名(タイムスケジュール)
        Dim strFol As String = ""         'フォルダ名
        Dim iptbx As String = ""         'フォルダ名
        Dim strfol001 As String = ""
        Dim CODE1 As String = ""
        Dim CODE2 As String = ""
        Dim myPath As String = ""
        Dim F_dir As String = ""
        Dim itkflg As String = ""
        Dim Ccode As String = ""
        Dim Cname As String = ""
        Dim strirai As String = ""
        Dim hensyuuiraisyo As String = ""
        Dim madef00 As String = ""
        Dim madef01 As String = "処理履歴："



        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String = ""
        Dim strinv As String

        Dim FLG As Boolean
        Dim NO_CK As Boolean
        Dim MIN_NO, MAX_NO As Double
        Dim constr As String
        Dim RW As Integer
        Dim HANTEI As Boolean
        Dim CNT As Double
        Dim CASENUM As String

        Dim lngFlgELA As Long
        Dim strFlgELA As String

        Dim PackNum As String
        Dim PackQty As Double
        Dim QTYNum As String
        Dim QTY As Double
        Dim Leng As Long
        Dim LN As Long
        Dim VANDATE As String

        Dim striv As String = ""
        Dim strcd As String = ""
        Dim strbl As Date
        Dim strbl02 As String = ""

        Dim I As Integer

        For I = 0 To GridView1.Rows.Count - 1
            If CType(GridView1.Rows(I).Cells(0).Controls(1), CheckBox).Checked Then
                If GridView1.Rows(I).Cells(26).Text = "&nbsp;" Or GridView1.Rows(I).Cells(6).Text = "&nbsp;" Then
                    madef00 = "3"
                Else

                    strPath00(0) = "\\svnas201\exd06100\COMMON\生産管理本部\ＣＳチーム\案件抽出\TEST_SI\SI雛形\"
                    strPath00(1) = ""
                    strPath01(0) = "\\svnas201\exd06100\COMMON\生産管理本部\ＣＳチーム\案件抽出\TEST_SI\"
                    'strPath01(0) = "\\svnas201\exd06100\COMMON\生産管理本部\ＣＳチーム\案件抽出\"　\\svnas201\exd06100\COMMON\生産管理本部\ＣＳチーム\案件抽出\TEST_SI\SI雛形
                    strPath01(1) = "\\svnas201\EXD06101\DISC_COMMON\自社通関輸出書類\"

                    '問題報告ログ初期化
                    strLog = ""

                    'ファイル検索
                    strFile0 = Dir(strPath00(0) & "X000_YYMMDD_SI9999.xlsx", vbNormal)
                    If strFile0 = "" Then
                        madef00 = 0
                        GoTo Step00
                    End If

                    '接続文字列の作成
                    Dim ConnectionString As String = String.Empty
                    'SQL Server認証
                    'ConnectionString = "Data Source=KBHWPM02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
                    ConnectionString = "Data Source=svdpo051;Initial Catalog=BPTB001;User Id=ado_bptb001;Password=ado_bptb001"
                    'SqlConnectionクラスの新しいインスタンスを初期化
                    Dim cnn = New SqlConnection(ConnectionString)

                    'データベース接続を開く
                    cnn.Open()
                    Dim dt1 As DateTime = DateTime.Now

                    Dim ts1 As New TimeSpan(100, 0, 0, 0)
                    Dim ts2 As New TimeSpan(100, 0, 0, 0)
                    Dim dt2 As DateTime = dt1 + ts1
                    Dim dt3 As DateTime = dt1 - ts1

                    Dim flnam As String

                    '基本データ取得_________________________________________________

                    strSQL = "SELECT * FROM T_INSTRUCTION_VIEW_01 WHERE OLD_INVNO = " & Chr(39) & GridView1.Rows(I).Cells(6).Text & Chr(39) & " " & "AND BLDATE BETWEEN '" & Format(dt3, "yyyy/MM/dd") & "' AND '" & Format(dt2, "yyyy/MM/dd") & "' ORDER BY BLDATE DESC"

                    'ＳＱＬコマンド作成 
                    dbcmd = New SqlCommand(strSQL, cnn)
                    'ＳＱＬ文実行 
                    dataread = dbcmd.ExecuteReader()


                    '結果を取り出す 

                    If dataread.Read() Then
                        striv = dataread("OLD_INVNO")
                        strcd = dataread("CUSTCODE")
                        strbl = dataread("BLDATE")
                        strbl02 = strbl.ToString("yyMMdd")
                    Else

                        ' レコードが取得できなかった時の処理
                        'MessageBox.Show("レコードがありません")
                    End If

                    'クローズ処理 
                    dataread.Close()
                    dbcmd.Dispose()

                    '基本データ取得_________________________________________________

                    strirai = Dir(strPath00(0) & "X000_YYMMDD_SI9999.xlsx", vbNormal)

                    'BLNOかIVNOでデータを取得

                    MyStr = Replace(strirai, "9999", striv)
                    MyStr = Replace(MyStr, "YYMMDD", strbl02)
                    MyStr = Replace(MyStr, "X000", strcd)

                    flnam = MyStr

                    'ファイル検索
                    strFile0 = Dir(strPath01(0) & MyStr)
                    If strFile0 <> "" Then
                        My.Computer.FileSystem.RenameFile(strPath01(0) & MyStr, "旧_" & Format(Now, "yyMMddmm") & "_" & MyStr)
                    End If

                    'System.IO.File.Copy(strPath00(0) & strirai, strPath01(0) & MyStr)

                    hensyuuiraisyo = strPath00(0) & strirai

                    Dim workbook = New XLWorkbook(hensyuuiraisyo)
                    Dim ws1 As IXLWorksheet = workbook.Worksheet(1)
                    Dim ws2 As IXLWorksheet = workbook.Worksheet(2)

                    'シート名変更
                    ws1.Name = "SI-" & striv & "HD"
                    ws2.Name = "SI-" & striv & "BD"

                    'MAKE_HEADER_______________________________________

                    strSQL = "SELECT * FROM T_INSTRUCTION_VIEW_01 WHERE OLD_INVNO = " & Chr(39) & striv & Chr(39) & " ORDER BY BLDATE DESC"

                    'ＳＱＬコマンド作成 
                    dbcmd = New SqlCommand(strSQL, cnn)
                    'ＳＱＬ文実行 
                    dataread = dbcmd.ExecuteReader()

                    strinv = ""
                    '結果を取り出す 

                    If dataread.Read() Then
                        ' レコードが取得できた時の処理
                        'ｲﾝﾎﾞｲｽNo   10月1日　IVNO＋BLDATE（YYMMDD)に変更
                        ws1.Cell(1, 13).Value = "IV-" & dataread("OLD_INVNO") & "-" & Right(Replace(dataread("BLDATE"), "/", ""), 6)
                        ws1.Cell(59, 2).Value = "IV-" & dataread("OLD_INVNO") & "-" & Right(Replace(dataread("BLDATE"), "/", ""), 6)

                        ws1.Cell(8, 2).Value = "1-1-1 KIDAMOTOMIYA, NEYAGAWA-SHI," & vbCrLf & "OSAKA, 572-8570 JAPAN" & vbCrLf & "TEL : 072-822-1153 FAX : 072-821-9267"
                        '作成日
                        ws1.Cell(2, 13).Value = "'" & EigoDate(Now)
                        'Accountee Name
                        ws1.Cell(14, 2).Value = dataread("CUSTOMERNAME")
                        'Accountee Address
                        ws1.Cell(15, 2).Value = dataread("CUSTOMERADDRESS")
                        'Consignee Name
                        ws1.Cell(21, 2).Value = dataread("CONSIGNEESINAME")
                        'Consignee Address
                        ws1.Cell(22, 2).Value = dataread("CONSIGNEESIADDRESS")
                        'Final Destination
                        ws1.Cell(28, 2).Value = dataread("DELIVERTONAME")
                        'final Destination Address
                        ws1.Cell(29, 2).Value = dataread("DELIVERTOADDRESS")
                        ' Notify
                        ws1.Cell(35, 2).Value = dataread("NOTIFYADDRESS")
                        ' Place of Delivery by Carrier
                        ws1.Cell(41, 7).Value = dataread("PLACECARRIER")
                        ' Ocean Vessel
                        ws1.Cell(42, 5).Value = dataread("SHIPPEDPER")
                        ' Port Of Loading
                        ws1.Cell(43, 5).Value = dataread("INVFROM")
                        ' Port Of Discharge
                        ws1.Cell(44, 5).Value = dataread("VIA")
                        ' Place Of delivery
                        ws1.Cell(45, 5).Value = dataread("PLACEDELIVERSI")
                        'SAILING ON/ABOUT
                        ws1.Cell(46, 5).Value = "'" & EigoDate(dataread("BLDATE"))

                        'CUT DATE
                        ws1.Cell(47, 5).Value = EigoDate(dataread("CUTDATE"))

                        'Freight
                        If dataread("PRIPAID") = "P" Then
                            ws1.Cell(48, 5).Value = "PREPAID"
                        ElseIf dataread("PRIPAID") = "C" Then
                            ws1.Cell(48, 5).Value = "COLLECT"
                        Else
                            ws1.Cell(48, 5).Value = ""
                        End If
                        ' Forwarder
                        ws1.Cell(49, 5).Value = dataread("OTNK")
                        'SHIPPING COMPANY
                        ws1.Cell(50, 5).Value = dataread("SHIPPER")
                        'VoyageNO
                        ws1.Cell(51, 5).Value = "'" & dataread("VOYAGENO")
                        'BOOKINGNO
                        ws1.Cell(52, 5).Value = "'" & Trim(dataread("BOOKINGNO"))

                    Else
                        ' レコードが取得できなかった時の処理
                        'MessageBox.Show("レコードがありません")
                    End If

                    'クローズ処理 
                    dataread.Close()
                    dbcmd.Dispose()


                    strSQL = "" &
    "SELECT * FROM T_INV_HD_TB " &
    "WHERE OLD_INVNO = " & Chr(39) & striv & Chr(39) & " ORDER BY BLDATE DESC"

                    'ＳＱＬコマンド作成 
                    dbcmd = New SqlCommand(strSQL, cnn)
                    'ＳＱＬ文実行 
                    dataread = dbcmd.ExecuteReader()

                    '結果を取り出す 

                    If dataread.Read() Then
                        ws1.Cell(60, 2).Value = dataread("INVBODYTITLE")
                        If InStr(1, dataread("CUSTNAME"), "ELA") > 0 Then
                            If Len(Trim(dataread("PAYMENT"))) > 0 Then
                                lngFlgELA = 1
                                strFlgELA = "支払い条件注意!" & vbCrLf & Trim(dataread("PAYMENT"))
                            Else
                                lngFlgELA = 0
                                strFlgELA = ""
                            End If
                        End If

                    Else
                        ' レコードが取得できなかった時の処理
                        'MessageBox.Show("レコードがありません")
                    End If

                    'クローズ処理 
                    dataread.Close()
                    dbcmd.Dispose()

                    strSQL = "" &
    "SELECT count(b.CASENO) AS caseno_ttl, b.PACKNAME AS packname_ttl,b.PACKPLURAL AS packplural_ttl " &
    "FROM ( select CASENO, PACKNAME, PACKPLURAL from T_INV_PDF_VIEW where CATEGORY_KBN = '1' and old_invno = " & Chr(39) & striv & Chr(39) &
    "group by CASENO,PACKNAME,PACKPLURAL,PLNO) b " &
    "group by b.PACKNAME,b.PACKPLURAL "


                    'ＳＱＬコマンド作成 
                    dbcmd = New SqlCommand(strSQL, cnn)
                    'ＳＱＬ文実行 
                    dataread = dbcmd.ExecuteReader()

                    '結果を取り出す 
                    While (dataread.Read())
                        If dataread("caseno_ttl") > 1 Then
                            PackNum = PackNum & dataread("caseno_ttl") & Chr(32) & dataread("packplural_ttl") & Chr(10)
                        Else
                            PackNum = PackNum & dataread("caseno_ttl") & Chr(32) & dataread("packname_ttl") & Chr(10)
                        End If
                        PackQty = PackQty + dataread("caseno_ttl")
                    End While

                    PackNum = PackNum &
            "-------------------" & Chr(10) &
            "TOTAL " & PackQty & Chr(32) & "PACKAGES"

                    ws1.Cell(61, 9).Value = PackNum

                    'クローズ処理 
                    dataread.Close()
                    dbcmd.Dispose()


                    strSQL = "" &
    "SELECT  b.SINGULARNAME,b.PLURALNAME,sum(b.QTY) as QTY  " &
    "FROM ( select SUM(QTY) AS QTY, SINGULARNAME, PLURALNAME, CASENO, PACKNAME, PACKPLURAL from T_INV_PDF_VIEW where CATEGORY_KBN = '1' and old_invno = " & Chr(39) & striv & Chr(39) &
    "group by  SINGULARNAME, PLURALNAME,CASENO,PACKNAME,PACKPLURAL,PLNO) b  " &
    "group by   b.SINGULARNAME,b.PLURALNAME " &
    "order by   b.SINGULARNAME Desc "

                    'ＳＱＬコマンド作成 
                    dbcmd = New SqlCommand(strSQL, cnn)
                    'ＳＱＬ文実行 
                    dataread = dbcmd.ExecuteReader()

                    '結果を取り出す 
                    While (dataread.Read())
                        If dataread("QTY") > 1 Then
                            QTYNum = QTYNum & Format(dataread("QTY"), "#,###") & Chr(32) & Trim(dataread("PLURALNAME")) & " & "
                        Else
                            QTYNum = QTYNum & Format(dataread("QTY"), "#,###") & Chr(32) & Trim(dataread("SINGULARNAME")) & " & "
                        End If
                    End While
                    Leng = Len(QTYNum)

                    ws1.Cell(61, 2).Value = Mid(QTYNum, 1, Leng - 3)

                    'クローズ処理 
                    dataread.Close()
                    dbcmd.Dispose()

                    strSQL = " SELECT VANDATE FROM T_INSTRUCTION_VIEW_01 WHERE OLD_INVNO = " & Chr(39) & striv & Chr(39) &
             " GROUP BY VANDATE ORDER BY VANDATE "

                    'ＳＱＬコマンド作成 
                    dbcmd = New SqlCommand(strSQL, cnn)
                    'ＳＱＬ文実行 
                    dataread = dbcmd.ExecuteReader()

                    strinv = ""
                    '結果を取り出す 

                    VANDATE = ""
                    '結果を取り出す 
                    While (dataread.Read())
                        VANDATE = VANDATE & "   " & EigoDate(dataread("VANDATE")) & ","
                    End While

                    LN = Len(VANDATE)

                    ws1.Cell(54, 2).Value = Left(VANDATE, LN - 1)

                    'クローズ処理 
                    dataread.Close()
                    dbcmd.Dispose()

                    'MAKE_HEADER_______________________________________



                    'MAKE_CMARK_______________________________________

                    '2008年6月27日追加
                    strSQL = " SELECT " &
" OLD_INVNO,                    " &
" INVOICENO,                    " &
" NOKI,                         " &
" CUSTCODE,                     " &
" SHIPPL,                       " &
" SHIPPINGMARK02,               " &
" SHIPPINGMARK03,               " &
" SHIPPINGMARK04,               " &
" SHIPPINGMARK05,               " &
" SHIPPINGMARK06,               " &
" SHIPPINGMARK07,               " &
" SHIPPINGMARK08,               " &
" SHIPPINGMARK09,               " &
" MIN(CASENO) AS MIN_NO,        " &
" MAX(CASENO) AS MAX_NO         " &
" FROM                          " &
" T_INSTRUCTION_VIEW_03         " &
" WHERE OLD_INVNO =             " & Chr(39) & striv & Chr(39) &
" GROUP BY                      " &
" OLD_INVNO, INVOICENO, NOKI, CUSTCODE, SHIPPL,   " &
" SHIPPINGMARK02, SHIPPINGMARK03,SHIPPINGMARK04,  " &
" SHIPPINGMARK05, SHIPPINGMARK06, SHIPPINGMARK07, " &
" SHIPPINGMARK08, SHIPPINGMARK09                  " &
" ORDER BY NOKI, CUSTCODE, SHIPPL                 "

                    'ＳＱＬコマンド作成 
                    dbcmd = New SqlCommand(strSQL, cnn)
                    'ＳＱＬ文実行 
                    dataread = dbcmd.ExecuteReader()

                    '結果を取り出す 
                    RW = 3
                    While (dataread.Read())
                        FLG = False
                        For IX = 5 To dataread.FieldCount - 3
                            ws2.Cell(RW, 2).Value = "'" & dataread(IX)
                            ws2.Range("B" & RW & ":" & "E" & RW).Merge()

                            If FLG = False Then
                                '  Debug.Print "'" & FLD("NOKI") & FLD("CUSTCODE") & FLD("SHIPPL")
                                ws2.Cell(RW, 7).Value = "'" & dataread("NOKI") & dataread("CUSTCODE") & dataread("SHIPPL") '2008年6月27日追加

                                ws2.Cell(RW, 8).Value = dataread("MIN_NO")
                                ws2.Cell(RW, 9).Value = dataread("MAX_NO")
                                FLG = True
                            End If

                            RW = RW + 1
                        Next
                        ws2.Cell(RW, 1).Value = ""
                        RW = RW + 1
                    End While
                    ws2.Cell(RW, 2).Value = "--"

                    'クローズ処理 
                    dataread.Close()
                    dbcmd.Dispose()
                    'MAKE_CMARK_______________________________________

                    'MAKE_CNO_______________________________________
                    RW = 3

                    Do While ws2.Cell(RW, 2).Value <> "--"

                        '2008年6月27日追加
                        CASENUM = STR_CNO2(striv, ws2.Cell(RW, 7).Value, ws2.Cell(RW, 2).Value, ws2.Cell(RW + 1, 2).Value, ws2.Cell(RW + 2, 2).Value, ws2.Cell(RW + 3, 2).Value _
                      , ws2.Cell(RW + 4, 2).Value, ws2.Cell(RW + 5, 2).Value, ws2.Cell(RW + 6, 2).Value, ws2.Cell(RW + 7, 2).Value)

                        ws2.Range("J" & RW & ":" & "S" & RW + 7).Merge()
                        ws2.Range("J" & RW & ":" & "S" & RW + 7).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top
                        ws2.Range("J" & RW & ":" & "S" & RW + 7).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left
                        ws2.Range("J" & RW & ":" & "S" & RW + 7).Style.Alignment.WrapText = True
                        ws2.Range("J" & RW & ":" & "S" & RW + 7).Style.Alignment.ShrinkToFit = True
                        ws2.Range("J" & RW & ":" & "S" & RW + 7).Style.Alignment.Indent = False

                        ws2.Cell(RW, 10).Value = CASENUM

                        ws2.Cell(RW, 8).Value = ""
                        ws2.Cell(RW, 9).Value = ""
                        CNT = 1

                        RW = RW + 9
                    Loop

                    'Columns("G:I").Select
                    'Selection.Delete Shift:=xlToLeft
                    ws2.Column("G").Delete()
                    ws2.Column("G").Delete()
                    ws2.Column("G").Delete()

                    'MAKE_CNO_______________________________________

                    strFile1 = Dir(strPath01(0) & flnam)
                    If strFile1 = "" Then
                    Else
                        System.IO.File.Delete(strPath01(0) & flnam)
                    End If
                    workbook.SaveAs(strPath01(0) & flnam)

                    Call reg_sm(striv, strbl02, strcd)

                    cnn.Close()
                    cnn.Dispose()
Step00:
                End If
            Else
            End If
        Next
        GridView1.DataBind()

        'RegisterClientScriptBlock　ページ描写前

        'RegisterStartupScript べージ描写後

        Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "確認", "<script language='JavaScript'>confirm('フォルダ作成完了しました。フィルタがクリアされ全件表示します。\n\n" & madef01 & "');</script>", False)

    End Sub

    Function EigoDate(argDate) As String
        Dim arryMonth
        Dim strYear As String, strDay As String
        Dim strMonth As Long

        If argDate Is Nothing Then
            EigoDate = ""
            Exit Function
        End If

        arryMonth = New String() {"JAN.", "FEB.", "MAR.", "APR.", "MAY", "JUN.",
                  "JUL.", "AUG.", "SEP.", "OCT.", "NOV.", "DEC."}

        strYear = DatePart("yyyy", argDate)
        strDay = DatePart("d", argDate)
        strMonth = Int(DatePart("m", argDate)) - 1

        EigoDate = arryMonth(strMonth) & " " & strDay & ", " & strYear


    End Function
    Public Function STR_CNO2(INV As String, SN_KEY As String, CMRK2 As String _
, CMRK3 As String, CMRK4 As String, CMRK5 As String, CMRK6 As String _
, CMRK7 As String, CMRK8 As String, CMRK9 As String) As String

        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String = ""
        Dim strinv As String

        Dim CNO As String
        Dim LN_CNO As Double
        Dim strNOKI As String
        Dim strCUSTCODE As String
        Dim strSHIPPL As String

        strNOKI = Mid(SN_KEY, 1, 7)
        strCUSTCODE = Mid(SN_KEY, 8, 4)
        strSHIPPL = Right(SN_KEY, 2)

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

        CNO = "'"

        strSQL = "" &
" SELECT CASENO " &
" FROM T_INSTRUCTION_VIEW_03 AS T_MARK " &
" WHERE T_MARK.OLD_INVNO = " & Chr(39) & INV & Chr(39) & " AND " &
" T_MARK.SHIPPINGMARK02 = " & Chr(39) & CMRK2 & Chr(39) & " AND " &
" T_MARK.SHIPPINGMARK03 = " & Chr(39) & CMRK3 & Chr(39) & " AND " &
" T_MARK.SHIPPINGMARK04 = " & Chr(39) & CMRK4 & Chr(39) & " AND " &
" T_MARK.SHIPPINGMARK05 = " & Chr(39) & CMRK5 & Chr(39) & " AND " &
" T_MARK.SHIPPINGMARK06 = " & Chr(39) & CMRK6 & Chr(39) & " AND " &
" T_MARK.SHIPPINGMARK07 = " & Chr(39) & CMRK7 & Chr(39) & " AND " &
" T_MARK.SHIPPINGMARK08 = " & Chr(39) & CMRK8 & Chr(39) & " AND " &
" T_MARK.SHIPPINGMARK09 = " & Chr(39) & CMRK9 & Chr(39) & " AND " &
" T_MARK.NOKI           = " & Chr(39) & strNOKI & Chr(39) & " AND " &
" T_MARK.CUSTCODE       = " & Chr(39) & strCUSTCODE & Chr(39) & " AND " &
" T_MARK.SHIPPL       = " & Chr(39) & strSHIPPL & Chr(39)


        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()


        '結果を取り出す 
        While (dataread.Read())
            CNO = CNO & dataread("CASENO") & ","
        End While

        LN_CNO = Len(CNO)

        CNO = Left(CNO, LN_CNO - 1)

        STR_CNO2 = CNO

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()

    End Function


    Private Sub reg_sm(strKoumoku2 As String, strKoumoku3 As String, strcd As String)

        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String = ""
        Dim strinv As String = ""
        Dim intCnt As Integer

        strKoumoku3 = "20" & Left(strKoumoku3, 2) & "/" & Mid(strKoumoku3, 3, 2) & "/" & Right(strKoumoku3, 2)

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=KBHWPM02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand

        Dim dt1 As DateTime = DateTime.Now
        Dim dtbl01 As DateTime = strKoumoku3

        Dim ts1 As New TimeSpan(100, 0, 0, 0)
        Dim ts2 As New TimeSpan(100, 0, 0, 0)
        Dim dt2 As DateTime = dt1 + ts1
        Dim dt3 As DateTime = dt1 - ts1

        Dim dtbl02 As DateTime = dt1 + ts1
        Dim dtbl03 As DateTime = dt1 - ts1

        'データベース接続を開く
        cnn.Open()

        strSQL = ""
        strSQL = strSQL & "SELECT COUNT(*) AS RecCnt FROM T_EXL_SHIPPINGMEMOLIST WHERE "
        strSQL = strSQL & "T_EXL_SHIPPINGMEMOLIST.INVOICE_NO = '" & strKoumoku2 & "' "
        strSQL = strSQL & "AND T_EXL_SHIPPINGMEMOLIST.CUSTCODE = '" & strcd & "' "
        strSQL = strSQL & "AND T_EXL_SHIPPINGMEMOLIST.ETD BETWEEN '" & dtbl03 & "' AND '" & dtbl02 & "' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        strinv = ""
        '結果を取り出す 
        While (dataread.Read())
            intCnt = dataread!RecCnt
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()

        '既存データの有無を判定
        If intCnt > 0 Then

        Else
            '既存データが無いのでINSERTする
            strSQL = ""
            strSQL = strSQL & "INSERT INTO T_EXL_SHIPPINGMEMOLIST VALUES("

            strSQL = strSQL & " '" & "' "
            strSQL = strSQL & ",'" & "' "

            strSQL = strSQL & ",'" & strKoumoku2 & "' "
            strSQL = strSQL & ",'" & strKoumoku3 & "' "

            strSQL = strSQL & ",'" & "' "



            strSQL = strSQL & ",'○' "

            strSQL = strSQL & ",'" & "' "
            strSQL = strSQL & ",'" & "' "
            strSQL = strSQL & ",'" & "' "



            strSQL = strSQL & ",'" & "' "
            strSQL = strSQL & ",'" & "' "
            strSQL = strSQL & ",'" & "' "

            strSQL = strSQL & ",'" & "' "
            strSQL = strSQL & ",'" & "' "
            strSQL = strSQL & ",'" & "' "

            strSQL = strSQL & ",'" & "' "
            strSQL = strSQL & ",'" & "' "
            strSQL = strSQL & ",'" & "' "

            strSQL = strSQL & ",'" & "' "
            strSQL = strSQL & ",'" & "' "
            strSQL = strSQL & ",'" & "' "

            strSQL = strSQL & ",'" & "' "
            strSQL = strSQL & ",'" & "' "
            strSQL = strSQL & ",'" & "' "

            strSQL = strSQL & ",'" & "' "
            strSQL = strSQL & ",'" & "' "
            strSQL = strSQL & ",'" & "' "

            strSQL = strSQL & ",'" & "' "
            strSQL = strSQL & ",'" & "' "
            strSQL = strSQL & ",'" & "' "

            strSQL = strSQL & ")"

            Command.CommandText = strSQL
            ' SQLの実行
            Command.ExecuteNonQuery()

        End If




        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()

    End Sub


    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        Dim strPath00(3) As String      '依頼書、タイムスケジュールのパスと作成先のパス
        Dim strPath01(3) As String      '

        Dim MyStr As String = ""
        Dim strLog As String            '問題報告ログ
        Dim strFile0 As String = ""         'ファイル名(依頼書)
        Dim strFile1 As String = ""         'ファイル名(タイムスケジュール)
        Dim strFol As String = ""         'フォルダ名
        Dim iptbx As String = ""         'フォルダ名
        Dim strfol001 As String = ""
        Dim CODE1 As String = ""
        Dim CODE2 As String = ""
        Dim myPath As String = ""
        Dim F_dir As String = ""
        Dim itkflg As String = ""
        Dim Ccode As String = ""
        Dim Cname As String = ""
        Dim strirai As String = ""
        Dim hensyuuiraisyo As String = ""
        Dim madef00 As String = ""
        Dim madef01 As String = "処理履歴："


        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String = ""
        Dim strinv As String

        Dim FLG As Boolean
        Dim NO_CK As Boolean
        Dim MIN_NO, MAX_NO As Double
        Dim constr As String
        Dim RW As Integer
        Dim HANTEI As Boolean
        Dim CNT As Double
        Dim CASENUM As String

        Dim lngFlgELA As Long
        Dim strFlgELA As String

        Dim PackNum As String
        Dim PackQty As Double
        Dim QTYNum As String
        Dim QTY As Double
        Dim Leng As Long
        Dim LN As Long
        Dim VANDATE As String

        Dim striv As String = ""
        Dim strcd As String = ""
        Dim strbl As Date
        Dim strbl02 As String = ""

        Dim I As Integer

        For I = 0 To GridView1.Rows.Count - 1
            If CType(GridView1.Rows(I).Cells(0).Controls(1), CheckBox).Checked Then
                If GridView1.Rows(I).Cells(26).Text = "&nbsp;" Or GridView1.Rows(I).Cells(6).Text = "&nbsp;" Then
                    madef00 = "3"
                Else

                    strPath00(0) = "\\svnas201\exd06100\COMMON\生産管理本部\ＣＳチーム\案件抽出\TEST_SI\SI雛形\"
                    strPath00(1) = ""
                    strPath01(0) = "\\svnas201\exd06100\COMMON\生産管理本部\ＣＳチーム\案件抽出\TEST_SI\"
                    'strPath01(0) = "\\svnas201\exd06100\COMMON\生産管理本部\ＣＳチーム\案件抽出\"　\\svnas201\exd06100\COMMON\生産管理本部\ＣＳチーム\案件抽出\TEST_SI\SI雛形
                    strPath01(1) = "\\svnas201\EXD06101\DISC_COMMON\自社通関輸出書類\"

                    '問題報告ログ初期化
                    strLog = ""

                    'ファイル検索
                    strFile0 = Dir(strPath00(0) & "MEMO.xlsx", vbNormal)
                    If strFile0 = "" Then
                        madef00 = 0
                        GoTo Step00
                    End If

                    '接続文字列の作成
                    Dim ConnectionString As String = String.Empty
                    'SQL Server認証
                    'ConnectionString = "Data Source=KBHWPM02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
                    ConnectionString = "Data Source=svdpo051;Initial Catalog=BPTB001;User Id=ado_bptb001;Password=ado_bptb001"
                    'SqlConnectionクラスの新しいインスタンスを初期化
                    Dim cnn = New SqlConnection(ConnectionString)

                    'データベース接続を開く
                    cnn.Open()
                    Dim dt1 As DateTime = DateTime.Now

                    Dim ts1 As New TimeSpan(100, 0, 0, 0)
                    Dim ts2 As New TimeSpan(100, 0, 0, 0)
                    Dim dt2 As DateTime = dt1 + ts1
                    Dim dt3 As DateTime = dt1 - ts1

                    '基本データ取得_________________________________________________

                    strSQL = "SELECT * FROM T_INSTRUCTION_VIEW_01 WHERE OLD_INVNO = " & Chr(39) & GridView1.Rows(I).Cells(6).Text & Chr(39) & " " & "AND BLDATE BETWEEN '" & Format(dt3, "yyyy/MM/dd") & "' AND '" & Format(dt2, "yyyy/MM/dd") & "' ORDER BY BLDATE DESC"

                    'ＳＱＬコマンド作成 
                    dbcmd = New SqlCommand(strSQL, cnn)
                    'ＳＱＬ文実行 
                    dataread = dbcmd.ExecuteReader()


                    '結果を取り出す 

                    If dataread.Read() Then
                        striv = dataread("OLD_INVNO")
                        strcd = dataread("CUSTCODE")
                        strbl = dataread("BLDATE")
                        strbl02 = strbl.ToString("yyMMdd")
                    Else

                        ' レコードが取得できなかった時の処理
                        'MessageBox.Show("レコードがありません")
                    End If

                    'クローズ処理 
                    dataread.Close()
                    dbcmd.Dispose()

                    '基本データ取得_________________________________________________

                    strirai = Dir(strPath00(0) & "MEMO.xlsx", vbNormal)
                    MyStr = strirai

                    'ファイル検索
                    strFile0 = Dir(strPath01(0) & MyStr)


                    'System.IO.File.Copy(strPath00(0) & strirai, strPath01(0) & MyStr)

                    hensyuuiraisyo = strPath00(0) & MyStr
                    'hensyuuiraisyo = strPath01(0) & MyStr

                    Dim workbook = New XLWorkbook(hensyuuiraisyo)
                    Dim ws1 As IXLWorksheet = workbook.Worksheet(1)
                    '                    Dim ws2 As IXLWorksheet = workbook.Worksheet(2)

                    'シート名変更
                    ws1.Name = "MEMO-" & striv


                    'MAKE_HEADER_______________________________________

                    strSQL = "SELECT * FROM T_MEMO_VIEW_01 WHERE OLD_INVNO = " & Chr(39) & striv & Chr(39) & " ORDER BY BLDATE DESC"

                    'ＳＱＬコマンド作成 
                    dbcmd = New SqlCommand(strSQL, cnn)
                    'ＳＱＬ文実行 
                    dataread = dbcmd.ExecuteReader()

                    strinv = ""
                    '結果を取り出す 

                    If dataread.Read() Then
                        ' レコードが取得できた時の処理
                        ' ヘッダー部分の書き込み
                        ws1.Cell(5, 2).Value = "'" & dataread("OLD_INVNO") 'ｲﾝﾎﾞｲｽNO
                        ws1.Cell(5, 4).Value = dataread("CUSTCODE") '客先コード
                        If dataread("BASENAME") Is Nothing Then
                            ws1.Cell(5, 6).Value = "未入力"
                        Else
                            ws1.Cell(5, 6).Value = dataread("BASENAME") '出荷拠点
                        End If

                        'コンテナ清掃追加（2013/12/17）
                        Select Case ws1.Cell(5, 4).Value
                            Case "6642"
                                ws1.Cell(26, 11).Value = "有"
                            Case "6645"
                                ws1.Cell(26, 11).Value = "有"
                        End Select


                        '通関パターン
                        If dataread("CUSTOMS") = "0" Then
                            ws1.Cell(5, 8).Value = "事前"
                        ElseIf dataread("CUSTOMS") = "1" Then
                            ws1.Cell(5, 8).Value = "包括"
                        Else
                            ws1.Cell(5, 8).Value = "未入力"
                        End If

                        ws1.Cell(5, 10).Value = Trim(dataread("CUSTNAME")) '請求先
                        ws1.Cell(8, 2).Value = Trim(dataread("DELIVERTONAME")) '届先
                        ws1.Cell(8, 6).Value = Trim(dataread("CONSIGNEENAME")) '荷受人
                        ws1.Cell(8, 10).Value = Trim(dataread("NOTIFYADDRESS")) '連絡先
                        ws1.Cell(11, 2).Value = Trim(dataread("SHIPPER")) '船社
                        ws1.Cell(11, 6).Value = Trim(dataread("OTNK")) '乙仲
                        ws1.Cell(11, 10).Value = "'" & Trim(dataread("BOOKINGNO")) 'BOOKINGNO
                        ws1.Cell(14, 2).Value = Trim(dataread("INVFROM")) '積出港
                        ws1.Cell(14, 6).Value = Trim(dataread("INVTO")) '配送先
                        ws1.Cell(14, 10).Value = Trim(dataread("VIA")) '揚港
                        ws1.Cell(17, 2).Value = Trim(dataread("SHIPPEDPER")) '船名
                        ws1.Cell(17, 6).Value = "'" & Trim(dataread("VOYAGENO")) 'V.NO
                        ws1.Cell(17, 10).Value = Trim(dataread("INVON")) '荷受地

                        ws1.Cell(20, 2).Value = dataread("BLDATE") 'BLDATE
                        ws1.Cell(20, 5).Value = dataread("REACHDATE") ' 到着日
                        ws1.Cell(20, 8).Value = dataread("CUTDATE") 'カット日
                        ws1.Cell(20, 11).Value = dataread("CARRYDATE") '搬入日

                        ws1.Cell(22, 5).Value = dataread("OTNKSENDDATE") '乙仲書類提出日
                        ws1.Cell(23, 5).Value = dataread("SHIPINSTDATE") 'SI作成日
                        ws1.Cell(24, 5).Value = dataread("CUSTSENDDATE") '客先送付日
                        ws1.Cell(25, 5).Value = dataread("EXPZAISENDDATE") '港在庫入力日
                        ws1.Cell(26, 5).Value = dataread("SALESDATE") '売上日
                        ws1.Cell(27, 5).Value = dataread("LOADINPUTDATE") 'EPA手配　（変更2009/12/17）
                        ws1.Cell(28, 5).Value = dataread("INSPECTDATE") '検査手配
                        ws1.Cell(22, 11).Value = dataread("CERTOFORIGINDATE") '原産地証明
                        ws1.Cell(23, 11).Value = dataread("MARININSURDATE") '保険手配
                        ws1.Cell(24, 11).Value = dataread("BLCONFIMDATE") 'BL確認
                        ws1.Cell(2, 11).Value = Now() '出力日


                    Else
                        ' レコードが取得できなかった時の処理
                        'MessageBox.Show("レコードがありません")
                    End If

                    'クローズ処理 
                    dataread.Close()
                    dbcmd.Dispose()


                    strSQL = "SELECT VANDATE, VANTIME_ST , SHIPNAME, STATUS , QTY , M3 , WEIGHT  " &
              " FROM T_MEMO_VIEW_01 WHERE OLD_INVNO = " & Chr(39) & striv & Chr(39) &
              " AND VANDATE Is Not Null " &
              " AND CUSTCODE = " & Chr(39) & ws1.Cell(5, 4).Value & Chr(39) &
              " AND BLDATE = '" & ws1.Cell(20, 2).Value & "'" &
              " ORDER BY VANDATE, VANTIME_ST "

                    'ＳＱＬコマンド作成 
                    dbcmd = New SqlCommand(strSQL, cnn)
                    'ＳＱＬ文実行 
                    dataread = dbcmd.ExecuteReader()

                    '結果を取り出す 

                    RW = 35
                    While (dataread.Read())

                        ws1.Cell(RW, 2).Value = dataread("VANDATE")
                        ws1.Cell(RW, 4).Value = Format(dataread("VANTIME_ST"), "HH:MM")
                        ws1.Cell(RW, 6).Value = dataread("SHIPNAME")

                        If dataread("STATUS") = "01" Then
                            ws1.Cell(RW, 10).Value = "確定"
                        Else
                            ws1.Cell(RW, 10).Value = "未定"
                        End If
                        ws1.Cell(RW, 11).Value = dataread("QTY")
                        ws1.Cell(RW, 12).Value = dataread("M3")
                        ws1.Cell(RW, 13).Value = dataread("WEIGHT")

                        RW = RW + 1

                    End While

                    'クローズ処理 
                    dataread.Close()
                    dbcmd.Dispose()

                    ws1.Cell(23, 5).Value = AddSCAC(ws1.Cell(5, 4).Value)
                    ws1.Cell(27, 5).Value = AddMokuzai(ws1.Cell(5, 4).Value)
                    ws1.Cell(26, 5).Value = AddKyakusaki(ws1.Cell(5, 4).Value)
                    ws1.Cell(31, 2).Value = AddBL(ws1.Cell(5, 4).Value)
                    ws1.Cell(31, 2).Value = ws1.Cell(31, 2).Value & Chr(10) & AddBLType(ws1.Cell(5, 4).Value)
                    ws1.Cell(28, 5).Value = AddEPA(ws1.Cell(5, 4).Value)
                    ws1.Cell(22, 11).Value = AddInsp(ws1.Cell(5, 4).Value)
                    ws1.Cell(23, 11).Value = AddCERL(ws1.Cell(5, 4).Value)
                    ws1.Cell(25, 11).Value = AddCClean(ws1.Cell(5, 4).Value)
                    ws1.Cell(26, 11).Value = AddLC(ws1.Cell(5, 4).Value)
                    ws1.Cell(24, 11).Value = AddBL01(ws1.Cell(5, 4).Value)



                    hensyuuiraisyo = strPath01(0) & ws1.Cell(5, 4).Value & "(" & ws1.Cell(5, 10).Value & ")" & "_IV-" & ws1.Cell(5, 2).Value & "_" & Format(ws1.Cell(20, 2).Value, "yyMMdd") & "_" & Replace(ws1.Cell(8, 6).Value, "/", "-") & ".xlsx"


                    strFile1 = Dir(hensyuuiraisyo)
                    If strFile1 = "" Then
                    Else
                        My.Computer.FileSystem.RenameFile(strPath01(0) & ws1.Cell(5, 4).Value & "(" & ws1.Cell(5, 10).Value & ")" & "_IV-" & ws1.Cell(5, 2).Value & "_" & Format(ws1.Cell(20, 2).Value, "yyMMdd") & "_" & Replace(ws1.Cell(8, 6).Value, "/", "-") & ".xlsx", "旧_" & Format(Now, "yyMMddmm") & "_" & ws1.Cell(5, 4).Value & "(" & ws1.Cell(5, 10).Value & ")" & "_IV-" & ws1.Cell(5, 2).Value & "_" & Format(ws1.Cell(20, 2).Value, "yyMMdd") & "_" & Replace(ws1.Cell(8, 6).Value, "/", "-") & ".xlsx")
                    End If
                    workbook.SaveAs(hensyuuiraisyo)
                    'My.Computer.FileSystem.RenameFile(hensyuuiraisyo, "E_" & MyStr)

                    Call reg_sm2(ws1.Cell(5, 4).Value, ws1.Cell(5, 10).Value, ws1.Cell(5, 2).Value, ws1.Cell(20, 2).Value, ws1.Cell(35, 6).Value, ws1.Cell(20, 5).Value)


                    cnn.Close()
                    cnn.Dispose()
Step00:
                End If
            Else
            End If
        Next
        GridView1.DataBind()

        'RegisterClientScriptBlock　ページ描写前

        'RegisterStartupScript べージ描写後

        Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "確認", "<script language='JavaScript'>confirm('ファイル作成完了しました。フィルタがクリアされ全件表示します。\n\n" & madef01 & "');</script>", False)

    End Sub


    Function AddSCAC(IVNO As String) As String

        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String = ""
        Dim strinv As String = ""
        Dim intCnt As Integer

        Dim strrng As String = ""

        If strrng = "" Then
            AddSCAC = ""
            Exit Function
        End If
        Select Case strrng
            Case 0
            Case 1
                AddSCAC = "有"
            Case 2
                AddSCAC = "EROIｺｰﾄﾞ有"
            Case 3
                AddSCAC = "要確認"
        End Select

    End Function

    Function AddMokuzai(IVNO As String) As String

        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String = ""
        Dim strinv As String = ""
        Dim intCnt As Integer

        Dim strrng As String = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=KBHWPM02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand

        'データベース接続を開く
        cnn.Open()

        strSQL = "SELECT * FROM T_EXL_CSMANUAL "
        strSQL = strSQL & "WHERE T_EXL_CSMANUAL.NEW_CODE ='" & IVNO & "' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        '結果を取り出す 

        While (dataread.Read())
            strrng = dataread("WOOD_NECE")
        End While


        If strrng = "" Then
            AddMokuzai = ""
            Exit Function
        End If
        If strrng = "○" Then
            AddMokuzai = "要"
        ElseIf strrng = "×" Then
            AddMokuzai = "不要"

        Else
            AddMokuzai = "情報無し"

        End If
    End Function

    Function AddEPA(IVNO As String) As String

        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String = ""
        Dim strinv As String = ""
        Dim intCnt As Integer

        Dim strrng As String = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=KBHWPM02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand

        'データベース接続を開く
        cnn.Open()


        strSQL = "SELECT * FROM T_EXL_CSMANUAL "
        strSQL = strSQL & "WHERE T_EXL_CSMANUAL.NEW_CODE ='" & IVNO & "' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        '結果を取り出す 


        While (dataread.Read())

            strrng = dataread("EPA_NECE")

        End While


        If strrng = "" Then
            AddEPA = ""
            Exit Function
        End If
        If strrng = "○" Then
            AddEPA = "要"
        ElseIf strrng = "×" Then
            AddEPA = "不要"

        Else
            AddEPA = "情報無し"

        End If
    End Function


    Function AddInsp(IVNO As String) As String

        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String = ""
        Dim strinv As String = ""
        Dim intCnt As Integer

        Dim strrng As String = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=KBHWPM02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand

        'データベース接続を開く
        cnn.Open()


        strSQL = "SELECT * FROM T_EXL_CSMANUAL "
        strSQL = strSQL & "WHERE T_EXL_CSMANUAL.NEW_CODE ='" & IVNO & "' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        '結果を取り出す 


        While (dataread.Read())

            strrng = dataread("INSP_NECE")

        End While


        If strrng = "" Then
            AddInsp = ""
            Exit Function
        End If
        If strrng = "○" Then
            AddInsp = "要"
        ElseIf strrng = "×" Then
            AddInsp = "不要"

        Else
            AddInsp = "情報無し"

        End If
    End Function

    Function AddCERL(IVNO As String) As String

        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String = ""
        Dim strinv As String = ""
        Dim intCnt As Integer

        Dim strrng As String = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=KBHWPM02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand

        'データベース接続を開く
        cnn.Open()


        strSQL = "SELECT * FROM T_EXL_CSMANUAL "
        strSQL = strSQL & "WHERE T_EXL_CSMANUAL.NEW_CODE ='" & IVNO & "' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        '結果を取り出す 


        While (dataread.Read())

            strrng = dataread("ERL_NECE")

        End While


        If strrng = "" Then
            AddCERL = ""
            Exit Function
        End If
        If strrng = "○" Then
            AddCERL = "要"
        ElseIf strrng = "×" Then
            AddCERL = "不要"

        Else
            AddCERL = "情報無し"

        End If


    End Function

    Function AddCClean(IVNO As String) As String

        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String = ""
        Dim strinv As String = ""
        Dim intCnt As Integer

        Dim strrng As String = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=KBHWPM02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand

        'データベース接続を開く
        cnn.Open()


        strSQL = "SELECT * FROM T_EXL_CSMANUAL "
        strSQL = strSQL & "WHERE T_EXL_CSMANUAL.NEW_CODE ='" & IVNO & "' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        '結果を取り出す 


        While (dataread.Read())

            strrng = dataread("CONTAINER_CLEANING")

        End While


        If strrng = "" Then
            AddCClean = ""
            Exit Function
        End If
        If strrng = "○" Then
            AddCClean = "要"
        ElseIf strrng = "×" Then
            AddCClean = "不要"

        Else
            AddCClean = "情報無し"

        End If


    End Function

    Function AddLC(IVNO As String) As String


        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String = ""
        Dim strinv As String = ""
        Dim intCnt As Integer

        Dim strrng As String = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=KBHWPM02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand

        'データベース接続を開く
        cnn.Open()


        strSQL = "SELECT * FROM T_EXL_CSMANUAL "
        strSQL = strSQL & "WHERE T_EXL_CSMANUAL.NEW_CODE ='" & IVNO & "' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        '結果を取り出す 


        While (dataread.Read())

            strrng = dataread("LC")

        End While


        If strrng = "" Then
            AddLC = ""
            Exit Function
        End If
        If strrng = "○" Then
            AddLC = "要"
        ElseIf strrng = "×" Then
            AddLC = "不要"

        Else
            AddLC = "情報無し"

        End If


    End Function
    Function AddBL01(IVNO As String) As String


        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String = ""
        Dim strinv As String = ""
        Dim intCnt As Integer

        Dim strrng As String = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=KBHWPM02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand

        'データベース接続を開く
        cnn.Open()


        strSQL = "SELECT * FROM T_EXL_CSMANUAL "
        strSQL = strSQL & "WHERE T_EXL_CSMANUAL.NEW_CODE ='" & IVNO & "' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        '結果を取り出す 


        While (dataread.Read())

            strrng = dataread("BL_TYPE")

        End While


        If strrng Is Nothing Then
            AddBL01 = ""
            Exit Function
        End If
        If strrng = "ORIGINAL B/L" Then
            AddBL01 = "-"
        Else

        End If



    End Function
    Function AddKyakusaki(IVNO As String) As String


        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String = ""
        Dim strinv As String = ""
        Dim intCnt As Integer

        Dim strrng As String = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=KBHWPM02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand

        'データベース接続を開く
        cnn.Open()


        strSQL = "SELECT * FROM T_EXL_CSMANUAL "
        strSQL = strSQL & "WHERE T_EXL_CSMANUAL.NEW_CODE ='" & IVNO & "' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        '結果を取り出す 


        While (dataread.Read())

            strrng = dataread("CO_NECE")

        End While


        If strrng Is Nothing Then
            AddKyakusaki = ""
            Exit Function
        End If
        If strrng = "○" Then
            AddKyakusaki = "要"
        ElseIf strrng = "×" Then
            AddKyakusaki = "不要"

        Else
            AddKyakusaki = "情報無し"

        End If

    End Function

    Function AddBL(IVNO As String) As String


        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String = ""
        Dim strinv As String = ""
        Dim intCnt As Integer

        Dim strrng As String = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=KBHWPM02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand

        'データベース接続を開く
        cnn.Open()


        strSQL = "SELECT * FROM T_EXL_CSMANUAL "
        strSQL = strSQL & "WHERE T_EXL_CSMANUAL.NEW_CODE ='" & IVNO & "' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        '結果を取り出す 


        While (dataread.Read())

            strrng = dataread("BL_SEND")

        End While


        If strrng Is Nothing Then
            AddBL = ""
            Exit Function
        End If



        AddBL = strrng

    End Function

    Function AddBLType(IVNO As String) As String


        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String = ""
        Dim strinv As String = ""
        Dim intCnt As Integer

        Dim strrng As String = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=KBHWPM02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand

        'データベース接続を開く
        cnn.Open()


        strSQL = "SELECT * FROM T_EXL_CSMANUAL "
        strSQL = strSQL & "WHERE T_EXL_CSMANUAL.NEW_CODE ='" & IVNO & "' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        '結果を取り出す 


        While (dataread.Read())

            strrng = dataread("BL_TYPE")

        End While


        If strrng Is Nothing Then
            AddBLType = ""
            Exit Function
        End If
        AddBLType = strrng

    End Function


    Private Sub reg_sm2(strKoumoku0 As String, strKoumoku1 As String, strKoumoku2 As String, strKoumoku3 As String, strKoumoku4 As String, strKoumoku5 As String)

        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String = ""
        Dim strinv As String = ""
        Dim intCnt As Integer
        Dim A As String = ""
        Dim B As String = ""
        Dim C As String = ""
        Dim D As String = ""
        Dim E As String = ""
        Dim F As String = ""
        Dim G As String = ""
        Dim H As String = ""
        Dim J As String = ""


        Call IVDATA(strKoumoku0, strKoumoku2, strKoumoku3, A, B, C, D, E, F, G, H, J)

        If InStr(1, strKoumoku4, "コンテナ") > 0 Then

            strKoumoku4 = "船舶"
        ElseIf InStr(1, strKoumoku4, "CFS") > 0 Then
            strKoumoku4 = "CFS"

        ElseIf InStr(1, strKoumoku4, "AIR") > 0 Then
            strKoumoku4 = "AIR"

        ElseIf InStr(1, strKoumoku4, "COURIER SERVICE") > 0 Then
            strKoumoku4 = "COURIER SERVICE"

        Else
            strKoumoku4 = "その他"

        End If

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=KBHWPM02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand

        Dim dt1 As DateTime = DateTime.Now
        Dim dtbl01 As DateTime = strKoumoku3

        Dim ts1 As New TimeSpan(100, 0, 0, 0)
        Dim ts2 As New TimeSpan(100, 0, 0, 0)
        Dim dt2 As DateTime = dt1 + ts1
        Dim dt3 As DateTime = dt1 - ts1

        Dim dtbl02 As DateTime = dt1 + ts1
        Dim dtbl03 As DateTime = dt1 - ts1

        'データベース接続を開く
        cnn.Open()

        strSQL = ""
        strSQL = strSQL & "SELECT COUNT(*) AS RecCnt FROM T_EXL_SHIPPINGMEMOLIST WHERE "
        strSQL = strSQL & "T_EXL_SHIPPINGMEMOLIST.INVOICE_NO = '" & strKoumoku2 & "' "
        strSQL = strSQL & "AND T_EXL_SHIPPINGMEMOLIST.CUSTCODE = '" & strKoumoku0 & "' "
        strSQL = strSQL & "AND T_EXL_SHIPPINGMEMOLIST.ETD BETWEEN '" & dtbl03 & "' AND '" & dtbl02 & "' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        strinv = ""
        '結果を取り出す 
        While (dataread.Read())
            intCnt = dataread!RecCnt
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()

        '既存データの有無を判定
        If intCnt > 0 Then
            '既存データありの場合、UPDATE
            strSQL = ""
            strSQL = strSQL & "UPDATE T_EXL_SHIPPINGMEMOLIST SET "

            strSQL = strSQL & "T_EXL_SHIPPINGMEMOLIST.CUSTCODE = '" & strKoumoku0 & "', "
            strSQL = strSQL & "T_EXL_SHIPPINGMEMOLIST.CUSTNAME = '" & strKoumoku1 & "', "
            strSQL = strSQL & "T_EXL_SHIPPINGMEMOLIST.INVOICE_NO = '" & strKoumoku2 & "', "
            strSQL = strSQL & "T_EXL_SHIPPINGMEMOLIST.ETD = '" & strKoumoku3 & "', " ' DateValue(strKoumoku3)
            strSQL = strSQL & "T_EXL_SHIPPINGMEMOLIST.MEMOFLG = '〇', "

            strSQL = strSQL & "T_EXL_SHIPPINGMEMOLIST.SHIP_TYPE = '" & strKoumoku4 & "', "

            strSQL = strSQL & "T_EXL_SHIPPINGMEMOLIST.ETA = '" & strKoumoku5 & "', " ' DateValue(strKoumoku5)

            strSQL = strSQL & "T_EXL_SHIPPINGMEMOLIST.BOOKING_NO = '" & A & "', "
            strSQL = strSQL & "T_EXL_SHIPPINGMEMOLIST.VOY_NO = '" & J & "', "

            strSQL = strSQL & "T_EXL_SHIPPINGMEMOLIST.KIN_GAIKA = '" & B & "', "
            strSQL = strSQL & "T_EXL_SHIPPINGMEMOLIST.RATE = '" & C & "', "
            strSQL = strSQL & "T_EXL_SHIPPINGMEMOLIST.KIN_JPY = '" & D & "', "
            strSQL = strSQL & "T_EXL_SHIPPINGMEMOLIST.VESSEL = '" & E & "', "
            strSQL = strSQL & "T_EXL_SHIPPINGMEMOLIST.LOADING_PORT = '" & F & "', "
            strSQL = strSQL & "T_EXL_SHIPPINGMEMOLIST.RECEIVED_PORT = '" & G & "', "
            strSQL = strSQL & "T_EXL_SHIPPINGMEMOLIST.SHIP_PLACE = '" & H & "' "

            strSQL = strSQL & "WHERE T_EXL_SHIPPINGMEMOLIST.INVOICE_NO ='" & strKoumoku2 & "' "
            strSQL = strSQL & "AND T_EXL_SHIPPINGMEMOLIST.CUSTCODE = '" & strKoumoku0 & "' "
            strSQL = strSQL & "AND T_EXL_SHIPPINGMEMOLIST.ETD BETWEEN '" & dtbl03 & "' AND '" & dtbl02 & "' "
            strSQL = strSQL & "AND T_EXL_SHIPPINGMEMOLIST.MEMOFLG IS NULL or T_EXL_SHIPPINGMEMOLIST.MEMOFLG ='' "

            Command.CommandText = strSQL
            ' SQLの実行
            Command.ExecuteNonQuery()

        Else


        End If



        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()

    End Sub


    Private Sub IVDATA(ByRef strKoumoku0 As String, ByRef strKoumoku2 As String, ByRef strKoumoku3 As String, ByRef A As String,
                       ByRef B As String, ByRef C As String, ByRef D As String, ByRef E As String, ByRef F As String, ByRef G As String,
                       ByRef H As String, ByRef J As String)

        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=svdpo051;Initial Catalog=BPTB001;User Id=ado_bptb001;Password=ado_bptb001"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand
        Dim strSQL As String = ""
        Dim ivno As String = ""
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand


        Dim dt1 As DateTime = DateTime.Now

        Dim ts1 As New TimeSpan(100, 0, 0, 0)
        Dim ts2 As New TimeSpan(100, 0, 0, 0)
        Dim dt2 As DateTime = dt1 + ts1
        Dim dt3 As DateTime = dt1 - ts1

        'データベース接続を開く
        cnn.Open()

        strSQL = "SELECT T_INV_HD_TB.OLD_INVNO,T_INV_HD_TB.INVFROM,T_INV_HD_TB.INVON, T_INV_HD_TB.BLDATE, Sum(T_INV_BD_TB.KIN) AS KINの合計,T_INV_HD_TB.INVOICENO,T_INV_HD_TB.STAMP,T_INV_HD_TB.RATE,(Sum(T_INV_BD_TB.KIN) * T_INV_HD_TB.RATE) as JPY,T_INV_HD_TB.BOOKINGNO,T_INV_HD_TB.SHIPPEDPER,T_INV_HD_TB.SHIPBASE,T_INV_HD_TB.VOYAGENO "
        strSQL = strSQL & "FROM T_INV_BD_TB RIGHT JOIN T_INV_HD_TB ON T_INV_BD_TB.INVOICENO = T_INV_HD_TB.INVOICENO "
        strSQL = strSQL & "WHERE "
        '    strSQL = strSQL & "T_INV_HD_TB.SALESFLG = '1' "
        '    strSQL = strSQL & "AND T_INV_HD_TB.BOOKINGNO is not null "
        strSQL = strSQL & " T_INV_HD_TB.BOOKINGNO is not null "
        strSQL = strSQL & " AND T_INV_HD_TB.BLDATE between '" & dt3 & "' and '" & dt2 & "' "

        strSQL = strSQL & "GROUP BY T_INV_HD_TB.OLD_INVNO, T_INV_HD_TB.BLDATE,T_INV_HD_TB.INVOICENO,T_INV_HD_TB.STAMP,T_INV_HD_TB.RATE,T_INV_HD_TB.BOOKINGNO,T_INV_HD_TB.SHIPPEDPER,T_INV_HD_TB.SHIPBASE,T_INV_HD_TB.INVFROM,T_INV_HD_TB.INVON,T_INV_HD_TB.VOYAGENO "


        strSQL = strSQL & "HAVING (((T_INV_HD_TB.OLD_INVNO) = " & Chr(39) & strKoumoku2 & Chr(39) & ")) "
        strSQL = strSQL & "AND ((Sum(T_INV_BD_TB.KIN))>0 ) "
        strSQL = strSQL & "AND T_INV_HD_TB.STAMP = (SELECT MAX(T_INV_HD_TB.STAMP) T_INV_HD_TB WHERE T_INV_HD_TB.OLD_INVNO = " & Chr(39) & strKoumoku2 & Chr(39) & ") "
        strSQL = strSQL & "order by T_INV_HD_TB.STAMP DESC "


        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        If (dataread.Read()) Then

            A = Trim(dataread!BOOKINGNO)
            B = Trim(dataread!KINの合計)
            C = Trim(dataread!Rate)
            D = Trim(dataread!JPY)
            E = Trim(dataread!SHIPPEDPER)
            F = Trim(dataread!INVFROM)
            G = Trim(dataread!INVON)
            H = Trim(dataread!SHIPBASE)
            J = Trim(dataread!VOYAGENO)


        End If

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()

        cnn.Close()
        cnn.Dispose()

    End Sub

End Class
