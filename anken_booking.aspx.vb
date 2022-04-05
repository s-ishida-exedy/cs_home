
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
                If Trim(e.Row.Cells(26).Text) = strbkg Then
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
                If Trim(e.Row.Cells(26).Text) = strbkg Then
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


    Private Sub itaku(bkgno As String)
        '確認完了ボタン押下時

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=KBHWPM02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand
        Dim strSQL As String = ""
        Dim ivno As String = ""

        'データベース接続を開く
        cnn.Open()

        'FIN_FLGを更新
        strSQL = ""
        strSQL = strSQL & "UPDATE T_EXL_CSANKEN SET FLG02 ='1' "
        strSQL = strSQL & "WHERE BOOKING_NO = '" & bkgno & "'"

        Command.CommandText = strSQL
        ' SQLの実行
        Command.ExecuteNonQuery()

    End Sub

    'Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

    '    '接続文字列の作成
    '    Dim ConnectionString As String = String.Empty
    '    'SQL Server認証
    '    ConnectionString = "Data Source=KBHWPM02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
    '    'SqlConnectionクラスの新しいインスタンスを初期化
    '    Dim cnn = New SqlConnection(ConnectionString)
    '    Dim Command = cnn.CreateCommand
    '    Dim strSQL As String = ""
    '    Dim ivno As String = ""

    '    'データベース接続を開く
    '    cnn.Open()

    '    Dim I As Integer

    '    For I = 0 To GridView1.Rows.Count - 1
    '            If CType(GridView1.Rows(I).Cells(0).Controls(1), CheckBox).Checked Then
    '                If GridView1.Rows(I).Cells(26).Text = "&nbsp;" Or GridView1.Rows(I).Cells(6).Text = "&nbsp;" Then
    '                    MsgBox("未確定のためフォルダを作成できません。" & vbCrLf & "客先:" & GridView1.Rows(I).Cells(4).Text & vbCrLf & "ETD:" & vbCrLf & GridView1.Rows(I).Cells(8).Text)
    '                Else

    '                    'FIN_FLGを更新
    '                    strSQL = ""
    '                    strSQL = strSQL & "UPDATE T_EXL_CSANKEN SET FLG03 ='1' "
    '                    strSQL = strSQL & "WHERE BOOKING_NO = '" & GridView1.Rows(I).Cells(26).Text & "'"

    '                    Command.CommandText = strSQL
    '                    ' SQLの実行
    '                    Command.ExecuteNonQuery()

    '                End If
    '            Else
    '            End If
    '        Next

    '        GridView1.DataBind()


    '    If DropDownList1.Text = "進捗状況" Then

    '        GridView1.DataSource = SqlDataSource1
    '        GridView1.DataBind()

    '    ElseIf DropDownList1.Text = "シート" Then

    '        GridView1.DataSource = SqlDataSource5
    '        GridView1.DataBind()

    '    ElseIf DropDownList1.Text = "海貨業者" Then

    '        GridView1.DataSource = SqlDataSource6
    '        GridView1.DataBind()

    '    ElseIf DropDownList1.Text = "客先コード" Then

    '        GridView1.DataSource = SqlDataSource9
    '        GridView1.DataBind()

    '    End If


    'End Sub

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
            deccnt = DEC_GET(GridView1.Rows(I).Cells(26).Text)

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


        Dim I As Integer

        For I = 0 To GridView1.Rows.Count - 1
            If CType(GridView1.Rows(I).Cells(0).Controls(1), CheckBox).Checked Then
                If GridView1.Rows(I).Cells(26).Text = "&nbsp;" Or GridView1.Rows(I).Cells(6).Text = "&nbsp;" Then
                    madef00 = "3"
                Else

                    strPath00(0) = "\\svnas201\exd06100\COMMON\生産管理本部\ＣＳチーム\案件抽出\a)自社通関依頼書（客先別）WEB\"
                    strPath00(1) = "\\svnas201\exd06100\COMMON\生産管理本部\ＣＳチーム\案件抽出\b)タイムスケジュール（客先別）\"
                    strPath01(0) = "\\svnas201\exd06100\COMMON\生産管理本部\ＣＳチーム\案件抽出\WEB_test\"
                    'strPath01(0) = "\\svnas201\exd06100\COMMON\生産管理本部\ＣＳチーム\案件抽出\"
                    strPath01(1) = "\\svnas201\EXD06101\DISC_COMMON\自社通関輸出書類\"

                    '問題報告ログ初期化
                    strLog = ""

                    'ファイル検索
                    strFile0 = Dir(strPath00(0) & "自社通関依頼書　EXEDY *(" & Replace(GridView1.Rows(I).Cells(4).Text, "/", "-") & ")*.xlsx", vbNormal)
                    If strFile0 = "" Then
                        strLog = strLog & Right("0000" & I, 5) & ",原紙なし" & Chr(10)
                        madef00 = 0
                        GoTo Step00
                    End If

                    '委託検索
                    itkflg = ""
                    itkflg = get_itakuhanntei(GridView1.Rows(I).Cells(6).Text)

                    If itkflg = "1" Then
                        madef00 = 1
                        GoTo Step00
                    End If

                    '1_________________________________________________

                    strfol001 = Dir(strPath01(0) & "*(" & Replace(GridView1.Rows(I).Cells(4).Text, "/", "-") & ")*" & Replace(GridView1.Rows(I).Cells(6).Text, "/", "-"), vbDirectory)

                    'If strfol001 <> "" Then
                    'madef00 = 2
                    '    GoTo Step00

                    'End If

                    'strfol001 = Dir(strPath01(1) & "*(" & Replace(GridView1.Rows(I).Cells(4).Text, "/", "-") & ")*" & Replace(GridView1.Rows(I).Cells(5).Text, "/", "-") & "*", vbDirectory)


                    'If strfol001 <> "" Then
                    'madef00 = 2
                    '    GoTo Step00

                    'End If


                    'strfol001 = Dir(strPath01(1) & Format(DateAdd("m", -1, Now()), "yyyyMM") & "\" & "*(" & Replace(GridView1.Rows(I).Cells(4).Text, "/", "-") & ")*" & Replace(GridView1.Rows(I).Cells(5).Text, "/", "-") & "*", vbDirectory)


                    'If strfol001 <> "" Then
                    'madef00 = 2
                    '    GoTo Step00

                    'End If


                    'strfol001 = Dir(strPath01(1) & Format(DateAdd("m", 0, Now()), "yyyyMM") & "\" & "*(" & Replace(GridView1.Rows(I).Cells(4).Text, "/", "-") & ")*" & Replace(GridView1.Rows(I).Cells(5).Text, "/", "-") & "*", vbDirectory)


                    'If strfol001 <> "" Then
                    'madef00 = 2
                    '    GoTo Step00

                    'End If

                    'strfol001 = Dir(strPath01(1) & Format(DateAdd("m", 1, Now()), "yyyyMM") & "\" & "*(" & Replace(GridView1.Rows(I).Cells(4).Text, "/", "-") & ")*" & Replace(GridView1.Rows(I).Cells(5).Text, "/", "-") & "*", vbDirectory)


                    'If strfol001 <> "" Then
                    'madef00 = 2
                    '    GoTo Step00

                    'End If

                    '2_________________________________________________

                    'フォルダ作成(既にあればスキップ)
                    '検索したファイル名から作成
                    strFol = Replace(strFile0, "自社通関依頼書　EXEDY ", "-")
                    strFol = Replace(strFol, "IV-0000", "IV-" & GridView1.Rows(I).Cells(6).Text)
                    strFol = Replace(strFol, "/", "-")

                    'ここを帰るとフォルダ作成先が変わる
                    Dim dt1 As DateTime = DateTime.Parse(GridView1.Rows(I).Cells(7).Text)
                    strFol = strPath01(0) & Format(dt1, "yyMMdd") & strFol ' Wb0.Path & "\" & Format(Ws0.Cells(i, 1), "yymmdd") & strFol

                    strFol = Left(strFol, Len(strFol) - 4)
                    MyStr = Dir(strFol, vbDirectory)
                    If MyStr <> "" Then
                        strLog = strLog & Right("0000" & I, 5) & ",同一フォルダ有り" & Chr(10)
                        madef00 = 2
                        GoTo Step00
                    End If

                    'MkDir strFol                                                                                   '格納先
                    My.Computer.FileSystem.CreateDirectory(strFol)


                    '3_________________________________________________



                    '追加 住所ファイル

                    myPath = "\\svnas201\exd06100\COMMON\生産管理本部\ＣＳチーム\案件抽出\q_住所" '--- フォルダを作成した場所のパス

                    iptbx = Left(Replace(GridView1.Rows(I).Cells(6).Text, "IV-", ""), 4)
                    Call copy_custfile(iptbx, Cname, Ccode)

                    F_dir = Dir(myPath & "\" & Ccode & "*", vbDirectory)

                    If F_dir <> "" Then
                        '処理
                        'objFSO.CopyFile myPath & "\" & F_dir, strFol & "\" & F_dir, True 
                        System.IO.File.Copy(myPath & "\" & F_dir, strFol & "\" & F_dir)
                    Else

                        F_dir = Dir(myPath & "\*" & Cname & "*", vbDirectory)

                        If F_dir <> "" Then
                            '処理
                            'objFSO.CopyFile myPath & "\" & F_dir, strFol & "\" & F_dir, True
                            System.IO.File.Copy(myPath & "\" & F_dir, strFol & "\" & F_dir)
                        Else
                        End If
                    End If


                    '4_________________________________________________


                    strirai = Dir(strPath00(0) & "*自社通関依頼書*" & Ccode & "*xlsx")
                    MyStr = Replace(strFile0, "IV-0000", "IV-" & GridView1.Rows(I).Cells(6).Text)

                    System.IO.File.Copy(strPath00(0) & strirai, strFol & "\" & MyStr)

                    hensyuuiraisyo = strFol & "\" & MyStr

                    Dim workbook = New XLWorkbook(hensyuuiraisyo)
                    Dim ws1 As IXLWorksheet = workbook.Worksheet(1)


                    '転記
                    ws1.Cell(4, 1).Value = GridView1.Rows(I).Cells(7).Text   '通関予定日
                    '        ws2.Range("B1") = Ws0.Cells(i, 2)   '通関予定日

                    ws1.Cell(11, 6).Value = GridView1.Rows(I).Cells(7).Text  'カット日 
                    '        ws2.Range("B20") = Ws0.Cells(i, 2)   '通関予定日

                    ws1.Cell(11, 7).Value = GridView1.Rows(I).Cells(8).Text  'POSITION(ETD)
                    '        ws2.Range("B7") = Ws0.Cells(i, 9)   'POSITION(ETD)
                    '        ws2.Range("B7").NumberFormatLocal = "yyyy/m/d"

                    ws1.Cell(11, 8).Value = GridView1.Rows(I).Cells(9).Text 'ETA 
                    '        ws2.Range("B21") = Ws0.Cells(i, 10)  'ETA

                    ws1.Cell(14, 2).Value = "'" & GridView1.Rows(I).Cells(26).Text  'BOOKING NO.
                    '        ws2.Range("B8") = "'" & Ws0.Cells(i, 13)  'BOOKING NO.

                    ws1.Cell(11, 9).Value = GridView1.Rows(I).Cells(27).Text  'CARRIER(船社)
                    '        ws2.Range("B22") = Ws0.Cells(i, 14)  'CARRIER(船社)

                    ws1.Cell(11, 1).Value = GridView1.Rows(I).Cells(28).Text & " ()" 'VESSEL(船舶コード） '船舶コード課題
                    '        ws2.Range("B5") = Ws0.Cells(i, 11) 'VESSEL(船舶コード） '船舶コード課題

                    ws1.Cell(11, 4).Value = GridView1.Rows(I).Cells(29).Text  'VOY.NO.(航海番号)
                    '        ws2.Range("B6") = Ws0.Cells(i, 12)  'VOY.NO.(航海番号)

                    'MyStr = "確認要"           'REF番号
                    'MyStr = Left(MyStr, InStr(1, MyStr, "-"))
                    'ws1.Cell(4, 5).Value = MyStr
                    '        ws2.Range("C1") = MyStr

                    Dim MyStr2 As String = "---" 'REF番号
                    ws1.Cell(4, 6).Value = MyStr
                    '        ws2.Range("D1") = MyStr

                    '港チェック(現段階では相違があれば、色を付ける→後々は訂正をする方向で)
                    Dim erflg As Long = 0
                    'PLACE OF RECEIPT(荷受地)
                    If ws1.Cell(16, 1).Value <> GridView1.Rows(I).Cells(30).Text Then

                        ws1.Cell(16, 1).Style.Fill.BackgroundColor = XLColor.Red
                        erflg = 1
                    End If
                    'PORT OF LOADING(積出港)
                    If ws1.Cell(16, 3).Value <> GridView1.Rows(I).Cells(31).Text Then
                        ws1.Cell(16, 3).Style.Fill.BackgroundColor = XLColor.Red
                        erflg = 1
                    End If
                    'PORT OF DISCHARGE(揚地)
                    If ws1.Cell(16, 5).Value <> GridView1.Rows(I).Cells(32).Text Then
                        ws1.Cell(16, 5).Style.Fill.BackgroundColor = XLColor.Red
                        erflg = 1
                    End If
                    'PLACE OF DELIVERY(配送先)
                    If ws1.Cell(16, 7).Value <> GridView1.Rows(I).Cells(33).Text Then
                        ws1.Cell(16, 7).Style.Fill.BackgroundColor = XLColor.Red
                        erflg = 1
                    End If


                    '------------　18/04追記  港コードも色付け　--------------
                    If ws1.Cell(16, 3).Style.Fill.BackgroundColor = XLColor.Red Then
                        ws1.Cell(25, 1).Style.Fill.BackgroundColor = XLColor.Red
                        erflg = 1
                    End If

                    If ws1.Cell(16, 5).Style.Fill.BackgroundColor = XLColor.Red Then
                        ws1.Cell(25, 6).Style.Fill.BackgroundColor = XLColor.Red
                        erflg = 1
                    End If

                    If ws1.Cell(16, 7).Style.Fill.BackgroundColor = XLColor.Red Then
                        ws1.Cell(30, 1).Style.Fill.BackgroundColor = XLColor.Red
                        erflg = 1
                    End If
                    '---------------------------------------------------------

                    '------------　21/03追記  Bookingsheetからデータ取得　--------------
                    If erflg = 1 Then

                        Dim niuke As String = ""
                        Dim tsumi As String = ""
                        Dim ageti As String = ""
                        Dim haisou As String = ""

                        Call get_bookingdata(I, niuke, tsumi, ageti, haisou, GridView1.Rows(I).Cells(6).Text)

                        ws1.Cell(2, 12).Value = niuke
                        ws1.Cell(3, 12).Value = tsumi
                        ws1.Cell(4, 12).Value = ageti
                        ws1.Cell(5, 12).Value = haisou

                        ws1.Cell(2, 11).Value = "荷受地"
                        ws1.Cell(3, 11).Value = "積出港"
                        ws1.Cell(4, 11).Value = "揚げ港"
                        ws1.Cell(5, 11).Value = "配送先"

                        'Call Minatocode01(ageti, CODE1)
                        'Call Minatocode02(haisou, CODE2)

                        'ws1.Cell(4, 13).Value = CODE1
                        'ws1.Cell(5, 13).Value = CODE2
                        'ws1.Cell(1, 13).Value = "過去実績"

                    End If

                    workbook.SaveAs(hensyuuiraisyo)

                    If erflg = 1 Then
                        My.Computer.FileSystem.RenameFile(hensyuuiraisyo, "E_" & MyStr)
                    End If

                    Call Get_allinv_k(Trim(GridView1.Rows(I).Cells(6).Text), Trim(GridView1.Rows(I).Cells(26).Text))

Step00:


                End If

                If madef00 = "" Then
                    madef01 = madef01 & "\n" & "＜作成無し＞作成済み　　　　 客先：" & GridView1.Rows(I).Cells(4).Text & " IVNO：" & GridView1.Rows(I).Cells(6).Text
                ElseIf madef00 = "0" Then
                    madef01 = madef01 & "\n" & "＜作成無し＞依頼書なし　　　 客先：" & GridView1.Rows(I).Cells(4).Text & " IVNO：" & GridView1.Rows(I).Cells(6).Text
                ElseIf madef00 = "1" Then
                    madef01 = madef01 & "\n" & "＜作成無し＞委託案件　　　　 客先：" & GridView1.Rows(I).Cells(4).Text & " IVNO：" & GridView1.Rows(I).Cells(6).Text
                ElseIf madef00 = "2" Then
                    madef01 = madef01 & "\n" & "＜作成無し＞同一フォルダあり 客先：" & GridView1.Rows(I).Cells(4).Text & " IVNO：" & GridView1.Rows(I).Cells(6).Text
                ElseIf madef00 = "3" Then
                    madef01 = madef01 & "\n" & "＜作成無し＞Booking未 客先：" & GridView1.Rows(I).Cells(4).Text & " IVNO：" & GridView1.Rows(I).Cells(6).Text
                End If
                madef00 = ""

            Else
            End If



        Next

            GridView1.DataBind()


        'RegisterClientScriptBlock　ページ描写前

        'RegisterStartupScript べージ描写後

        Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "確認", "<script language='JavaScript'>confirm('フォルダ作成完了しました。フィルタがクリアされ全件表示します。\n\n" & madef01 & "');</script>", False)

    End Sub


    Private Function get_itakuhanntei(ivno As String) As String

        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String = ""
        Dim strinv As String

        get_itakuhanntei = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=KBHWPM02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        Dim dt1 As DateTime = DateTime.Now

        Dim ts1 As New TimeSpan(100, 0, 0, 0)
        Dim ts2 As New TimeSpan(100, 0, 0, 0)
        Dim dt2 As DateTime = dt1 + ts1
        Dim dt3 As DateTime = dt1 - ts1

        'データベース接続を開く
        cnn.Open()

        strSQL = ""
        strSQL = strSQL & "SELECT INVNO FROM T_EXL_WORKSTATUS00 WHERE "
        strSQL = strSQL & "T_EXL_WORKSTATUS00.ID = '001' "
        strSQL = strSQL & "AND T_EXL_WORKSTATUS00.INVNO = '" & ivno & "' "
        strSQL = strSQL & "AND T_EXL_WORKSTATUS00.REGDATE > '" & dt3 & "' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        strinv = ""
        '結果を取り出す 
        While (dataread.Read())
            strinv = Convert.ToString(dataread("INVNO"))        'ETD(計上日)
            If strinv = "" Then
            Else
                get_itakuhanntei = 1
            End If
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()

    End Function

    Private Sub copy_custfile(iptbx As String, ByRef Cname As String, ByRef Ccode As String)

        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String = ""
        Dim strinv As String = ""

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

        strSQL = "SELECT distinct T_INV_HD_TB.CUSTCODE,T_INV_HD_TB.CUSTNAME "
        strSQL = strSQL & "FROM T_INV_HD_TB "
        strSQL = strSQL & "WHERE T_INV_HD_TB.OLD_INVNO like '%" & iptbx & "%' "
        strSQL = strSQL & "AND T_INV_HD_TB.BLDATE BETWEEN '" & dt3 & "' AND '" & dt2 & "' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        Cname = ""
        Ccode = ""
        '結果を取り出す 
        While (dataread.Read())
            Cname = Trim(Convert.ToString(dataread("CUSTNAME")))        '客先目
            Ccode = Trim(Convert.ToString(dataread("CUSTCODE")))        '客先コード
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()

    End Sub

    Private Sub get_bookingdata(i As String, ByRef niuke As String, ByRef tsumi As String, ByRef ageti As String, ByRef haisou As String, ivno As String)

        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String = ""
        Dim strinv As String = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=KBHWPM02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        Dim dt1 As DateTime = DateTime.Now

        Dim ts1 As New TimeSpan(100, 0, 0, 0)
        Dim ts2 As New TimeSpan(100, 0, 0, 0)
        Dim dt2 As DateTime = dt1 + ts1
        Dim dt3 As DateTime = dt1 - ts1

        'データベース接続を開く
        cnn.Open()

        strSQL = ""
        strSQL = strSQL & "SELECT * FROM T_BOOKING "
        strSQL = strSQL & " WHERE INVOICE_NO like '%" & ivno & "%' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        strinv = ""
        '結果を取り出す 
        While (dataread.Read())
            niuke = Convert.ToString(dataread("PLACE_OF_RECEIPT"))
            tsumi = Convert.ToString(dataread("LOADING_PORT"))
            ageti = Convert.ToString(dataread("DISCHARGING_PORT"))
            haisou = Convert.ToString(dataread("PLACE_OF_DELIVERY"))
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()

    End Sub


    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click



        Dim struid As String = Session("UsrId")
        Dim strfrom As String = GET_from(struid)
        '        Dim strto As String = "r-fukao@exedy.com,s-ishida@exedy.com"
        Dim strto As String = "r-fukao@exedy.com,r-fukao@exedy.com"

        Dim strsyomei As String = GET_syomei(struid)

        'メールの送信に必要な情報
        Dim smtpHostName As String = "svsmtp01.exedy.co.jp"
        Dim smtpPort As Integer = 25

        ' メールの内容


        'メールの件名
        Dim subject As String = "【異常報告】Bookingシート未更新"

        'メールの本文
        Dim body As String = "<html><body>Bookingシート未更新です。</body></html>" ' UriBodyC()

        body = "<font size=" & Chr(34) & " 3" & Chr(34) & ">" & body & "</font>"
        body = "<font face=" & Chr(34) & " Meiryo UI" & Chr(34) & ">" & body & "</font>"

        ' MailKit におけるメールの情報
        Dim message = New MimeKit.MimeMessage()

        ' 送り元情報  
        message.From.Add(MailboxAddress.Parse(strfrom))


        If strto <> "" Then
            'カンマ区切りをSPLIT
            Dim strVal() As String = strto.Split(",")
            For Each c In strVal
                message.To.Add(New MailboxAddress("", c))
            Next
        End If

        ' 表題  
        message.Subject = subject

        ' 本文
        Dim textPart = New MimeKit.TextPart(MimeKit.Text.TextFormat.Html)
        textPart.Text = body
        message.Body = textPart

        Dim multipart = New MimeKit.Multipart("mixed")

        multipart.Add(textPart)

        message.Body = multipart

        Using client As New MailKit.Net.Smtp.SmtpClient()
            client.Connect(smtpHostName, smtpPort, MailKit.Security.SecureSocketOptions.Auto)
            client.Send(message)
            client.Disconnect(True)
            client.Dispose()
            message.Dispose()
        End Using

        Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "確認", "<script language='JavaScript'>confirm('メールを送信しました。');</script>", False)

    End Sub



    Private Function GET_ToAddress(strkbn As String, strtocc As String) As String
        'BCCメールアドレス情報を取得
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String

        GET_ToAddress = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()

        strSQL = strSQL & "SELECT MAIL_ADD FROM M_EXL_LCL_DEC_MAIL "
        strSQL = strSQL & "WHERE kbn = '" & strkbn & "' "
        strSQL = strSQL & "AND TO_CC = '" & strtocc & "' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        strDate = ""
        '結果を取り出す 
        While (dataread.Read())
            GET_ToAddress += dataread("MAIL_ADD") + ","
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()

    End Function


    Private Function GET_from(struid As String) As String
        'BCCメールアドレス情報を取得
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String

        GET_from = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()

        strSQL = strSQL & "SELECT e_mail FROM M_EXL_USR "
        strSQL = strSQL & "WHERE uid = '" & struid & "' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        strDate = ""
        '結果を取り出す 
        While (dataread.Read())
            GET_from += dataread("e_mail")
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()

    End Function


    Private Function GET_syomei(struid As String) As String
        'BCCメールアドレス情報を取得
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String

        GET_syomei = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()

        strSQL = strSQL & "SELECT MEMBER_NAME,COMPANY,TEAM,TEL_NO,E_MAIL FROM M_EXL_CS_MEMBER "
        strSQL = strSQL & "WHERE code = '" & struid & "' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        strDate = ""
        '結果を取り出す 
        While (dataread.Read())
            GET_syomei += "<html><body>******************************<p></p>" + "" + dataread("MEMBER_NAME") + "<p></p>" + dataread("COMPANY") + "<p></p>" + dataread("TEL_NO") + "<p></p>" + dataread("E_MAIL") + "<p></p>" + "******************************</body></html>"
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()

    End Function

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Dim t As Integer
        t = 1
        Dim cnt As Integer = 0

        Dim val01 As String = ""

        Using wb As XLWorkbook = New XLWorkbook()
            Dim ws As IXLWorksheet = wb.AddWorksheet("管理表")
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

                            Case 7 To 9, 14 To 25

                                If IsDate(val01) = True Then
                                    ws.Cell(t, i).SetValue(DateValue(val01))
                                Else
                                    ws.Cell(t, i).SetValue(val01)
                                End If

                            Case 10 To 13
                                If IsNumeric(val01) = True Then
                                    ws.Cell(t, i).SetValue(Val(val01))
                                Else
                                    ws.Cell(t, i).SetValue(val01)
                                End If
                            Case Else
                                ws.Cell(t, i).SetValue(val01)
                        End Select
                    Next
                    t = t + 1
                End If
            Next

            ws.Style.Font.FontName = "Meiryo UI"
            ws.Columns.AdjustToContents()
            ws.SheetView.FreezeRows(1)


            Dim struid As String = Session("UsrId")
            wb.SaveAs("\\svnas201\EXD06101\DISC_COMMON\WEB出力\出荷案件管理表" & Now.ToString(“yyyyMMddhhmmss”) & "_PIC_" & struid & ".xlsx")

        End Using


    End Sub

    Private Sub Get_allinv_k(strInv As String, strbkg As String)

        Dim strSQL As String = ""
        Dim ivno As String = ""
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand

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

        strSQL = "SELECT T_INV_HD_TB.OLD_INVNO "
        strSQL = strSQL & "FROM T_INV_HD_TB LEFT JOIN T_INV_BD_TB ON T_INV_HD_TB.INVOICENO = T_INV_BD_TB.INVOICENO "
        strSQL = strSQL & "WHERE T_INV_HD_TB.BLDATE BETWEEN '" & dt3 & "' AND '" & dt2 & "' "
        strSQL = strSQL & "GROUP BY T_INV_HD_TB.BOOKINGNO, T_INV_HD_TB.OLD_INVNO, T_INV_HD_TB.SHIPPEDPER, T_INV_HD_TB.VOYAGENO, T_INV_HD_TB.IOPORTDATE, T_INV_HD_TB.CUTDATE "
        strSQL = strSQL & "HAVING T_INV_HD_TB.BOOKINGNO like '%" & strbkg & "%' "

        strSQL = strSQL & "order by T_INV_HD_TB.CUTDATE Desc "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()


        '結果を取り出す 
        While (dataread.Read())
            Call addRecord_K(dataread("OLD_INVNO"))
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()


    End Sub

    Private Sub addRecord_K(strIVNO As String)

        Dim strSQL As String = ""
        Dim ivno As String = ""
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand

        Dim TDATE As String = ""
        Dim CUT As String = ""
        Dim CUST As String = ""
        Dim SUMMARY_INVO As String = ""
        Dim INVOICE_NO As String = ""
        Dim LOADING_PORT As String = ""
        Dim DESTINATION As String = ""
        Dim KANNRINO As String = ""
        Dim BOOKING_NO As String = ""
        Dim IFLG As String = ""
        Dim IV_COUNT As String = ""
        Dim CONTAINER As String = ""
        Dim REF01 As String = ""
        Dim REF02 As String = ""
        Dim REV_KANNRINO As String = ""
        Dim SALES As String = ""
        Dim CHECK01 As String = ""
        Dim itkcnt As String = ""
        Dim LCLF As String = ""

        Dim intCnt As Integer

        Dim dt1 As DateTime = DateTime.Now

        Dim ts1 As New TimeSpan(100, 0, 0, 0)
        Dim ts2 As New TimeSpan(100, 0, 0, 0)
        Dim dt2 As DateTime = dt1 + ts1
        Dim dt3 As DateTime = dt1 - ts1

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand

        'データベース接続を開く
        cnn.Open()

        strSQL = ""
        strSQL = strSQL & "SELECT * FROM T_EXL_DECKANRIHYO WHERE "
        strSQL = strSQL & "T_EXL_DECKANRIHYO.INVOICE_NO = '" & strIVNO & "' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()


        '結果を取り出す 
        While (dataread.Read())

            TDATE = dataread("TDATE")
            CUT = dataread("CUT")
            CUST = dataread("CUST")
            SUMMARY_INVO = dataread("SUMMARY_INVO")
            INVOICE_NO = dataread("INVOICE_NO")
            LOADING_PORT = dataread("LOADING_PORT")
            DESTINATION = dataread("DESTINATION")
            KANNRINO = dataread("KANNRINO")
            BOOKING_NO = dataread("BOOKING_NO")
            IFLG = dataread("IFLG")
            IV_COUNT = dataread("IV_COUNT")
            CONTAINER = dataread("CONTAINER")
            REF01 = dataread("REF01")
            REF02 = dataread("REF02")
            REV_KANNRINO = dataread("REV_KANNRINO")
            SALES = dataread("SALES")
            CHECK01 = dataread("CHECK01")

            intCnt = 1

        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()



        strSQL = ""
        strSQL = strSQL & "SELECT * FROM T_EXL_CSANKEN WHERE "
        strSQL = strSQL & "T_EXL_CSANKEN.INVOICE like '%" & strIVNO & "%' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()


        '結果を取り出す 
        While (dataread.Read())

            TDATE = Format(Now(), "yyyy/MM/dd")
            CUT = dataread("CUT_DATE")
            CUST = dataread("CUST")
            SUMMARY_INVO = dataread("INVOICE")
            INVOICE_NO = strIVNO
            LOADING_PORT = dataread("LOADING_PORT")
            DESTINATION = dataread("DESTINATION")
            KANNRINO = ""
            BOOKING_NO = dataread("BOOKING_NO")
            IFLG = ""
            IV_COUNT = ""
            CONTAINER = dataread("CONTAINER")
            REF01 = ""
            REF02 = ""
            REV_KANNRINO = ""
            SALES = ""
            CHECK01 = ""
            LCLF = dataread("LCL_QTY")

        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()




        '既存データの検索
        strSQL = ""
        strSQL = strSQL & "SELECT ITK_BKGNO FROM T_EXL_CSWORKSTATUS WHERE "
        strSQL = strSQL & "T_EXL_CSWORKSTATUS.ITK_BKGNO like '%" & BOOKING_NO & "%' "

        strSQL = ""
        strSQL = strSQL & "SELECT BKGNO FROM T_EXL_WORKSTATUS00 WHERE "
        strSQL = strSQL & "T_EXL_WORKSTATUS00.ID = '001' "
        strSQL = strSQL & "AND T_EXL_WORKSTATUS00.BKGNO = '" & BOOKING_NO & "' "
        strSQL = strSQL & "AND T_EXL_WORKSTATUS00.REGDATE > '" & Format(dt3, "yyyy/MM/dd") & "' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()


        '結果を取り出す 
        While (dataread.Read())

            itkcnt = dataread("bkgno")

        End While


        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()

        If itkcnt <> "" Then

            IFLG = "1"

        End If

        If LCLF = "LCL" Then

            IFLG = "1"

        End If



        '既存データの有無を判定
        If intCnt > 0 Then

            '既存データありの場合、UPDATE
            strSQL = ""
            strSQL = strSQL & "UPDATE T_EXL_DECKANRIHYO SET "
            strSQL = strSQL & "T_EXL_DECKANRIHYO.TDATE = '" & TDATE & "', "
            strSQL = strSQL & "T_EXL_DECKANRIHYO.CUT = '" & CUT & " ', "
            strSQL = strSQL & "T_EXL_DECKANRIHYO.CUST = '" & CUST & " ', "
            strSQL = strSQL & "T_EXL_DECKANRIHYO.SUMMARY_INVO = '" & SUMMARY_INVO & " ', "
            strSQL = strSQL & "T_EXL_DECKANRIHYO.INVOICE_NO = '" & Trim(strIVNO) & " ', "
            strSQL = strSQL & "T_EXL_DECKANRIHYO.LOADING_PORT = '" & LOADING_PORT & " ', "
            strSQL = strSQL & "T_EXL_DECKANRIHYO.DESTINATION = '" & DESTINATION & " ', "

            strSQL = strSQL & "T_EXL_DECKANRIHYO.BOOKING_NO = '" & BOOKING_NO & " ', "
            strSQL = strSQL & "T_EXL_DECKANRIHYO.IFLG = '" & IIf(IFLG = "", 0, IFLG) & "', "
            strSQL = strSQL & "T_EXL_DECKANRIHYO.IV_COUNT = '" & IV_COUNT & " ', "
            strSQL = strSQL & "T_EXL_DECKANRIHYO.CONTAINER = '" & CONTAINER & " ', "
            strSQL = strSQL & "T_EXL_DECKANRIHYO.REF01 = '" & REF01 & " ', "
            strSQL = strSQL & "T_EXL_DECKANRIHYO.Ref02 = '" & "" & " ', "
            strSQL = strSQL & "T_EXL_DECKANRIHYO.REV_KANNRINO = '" & "" & " ' "
            strSQL = strSQL & "WHERE T_EXL_DECKANRIHYO.INVOICE_NO = '" & Trim(strIVNO) & "' "

            Command.CommandText = strSQL
            ' SQLの実行
            Command.ExecuteNonQuery()




        Else


            '既存データが無いのでINSERTする
            strSQL = ""
            strSQL = strSQL & "INSERT INTO T_EXL_DECKANRIHYO VALUES("
            strSQL = strSQL & " '" & TDATE & "', "
            strSQL = strSQL & " '" & IIf(CUT = "", "", CUT) & "', "
            strSQL = strSQL & " '" & IIf(CUST = "", "", CUST) & "', "
            strSQL = strSQL & " '" & IIf(SUMMARY_INVO = "", "", SUMMARY_INVO) & "', "
            strSQL = strSQL & " '" & Trim(strIVNO) & "', "
            strSQL = strSQL & " '" & IIf(LOADING_PORT = "", "", LOADING_PORT) & "', "
            strSQL = strSQL & " '" & IIf(DESTINATION = "", "", DESTINATION) & "', "
            strSQL = strSQL & " '" & IIf(KANNRINO = "", "", KANNRINO) & "', "
            strSQL = strSQL & " '" & IIf(BOOKING_NO = "", "", BOOKING_NO) & "', "
            strSQL = strSQL & " '" & IIf(IFLG = "", "0", IFLG) & "', "
            strSQL = strSQL & " '" & IIf(IV_COUNT = "", "", IV_COUNT) & "', "
            strSQL = strSQL & " '" & IIf(CONTAINER = "", "", CONTAINER) & "', "
            strSQL = strSQL & " '" & IIf(REF01 = "", "", REF01) & "', "
            strSQL = strSQL & " '" & "" & "', "
            strSQL = strSQL & " '" & "" & "', "
            strSQL = strSQL & " '" & "" & "', "
            strSQL = strSQL & " '" & "" & "' "
            strSQL = strSQL & ")"


            Command.CommandText = strSQL
            ' SQLの実行
            Command.ExecuteNonQuery()


        End If





        cnn.Close()
        cnn.Dispose()


    End Sub

End Class
