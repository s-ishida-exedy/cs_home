
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


        Dim code As String = ""
        Dim intval As Integer
        Dim intCnt As Integer

        Dim byteLength As Integer

        Dim dt1 As DateTime = DateTime.Now

        Dim ts1 As New TimeSpan(180, 0, 0, 0)
        Dim ts2 As New TimeSpan(180, 0, 0, 0)
        Dim dt2 As DateTime = dt1 + ts1
        Dim dt3 As DateTime = dt1 - ts1



        Dim dt00 As String = dt3.ToShortDateString


        If e.Row.RowType = DataControlRowType.DataRow Then

            '請求書コード
            e.Row.Cells(0).Text = Trim(Left(e.Row.Cells(0).Text, 4))

            'PORTOF LOADING(積み出し港)	4
            intval = 0
            intCnt = InStr(intCnt + 1, e.Row.Cells(4).Text, "→")
            Do While intCnt > 0
                intval = intCnt
                intCnt = InStr(intCnt + 1, e.Row.Cells(4).Text, "→")
            Loop
            e.Row.Cells(4).Text = Trim(Mid(e.Row.Cells(4).Text, intval + 1, Len(e.Row.Cells(4).Text) - intval))
            intCnt = 0

            If Len(e.Row.Cells(4).Text) <> System.Text.Encoding.GetEncoding("shift_jis").GetByteCount(e.Row.Cells(4).Text) Then
                e.Row.BackColor = Drawing.Color.Green
                e.Row.Cells(4).Text = e.Row.Cells(4).Text & "_全角"
            End If

            If InStr(e.Row.Cells(4).Text, vbCr) + InStr(e.Row.Cells(4).Text, vbLf) + InStr(e.Row.Cells(4).Text, vbCrLf) > 0 Then
                e.Row.BackColor = Drawing.Color.Blue
                e.Row.Cells(4).Text = e.Row.Cells(4).Text & "_改行"
            End If

            'PORTOF DEISCHARGE(揚地)	5
            intval = 0
                intCnt = InStr(intCnt + 1, e.Row.Cells(5).Text, "→")
                Do While intCnt > 0
                    intval = intCnt
                    intCnt = InStr(intCnt + 1, e.Row.Cells(5).Text, "→")
                Loop
                e.Row.Cells(5).Text = Trim(Mid(e.Row.Cells(5).Text, intval + 1, Len(e.Row.Cells(5).Text) - intval))
            intCnt = 0

            If Len(e.Row.Cells(5).Text) <> System.Text.Encoding.GetEncoding("shift_jis").GetByteCount(e.Row.Cells(5).Text) Then
                e.Row.BackColor = Drawing.Color.Green
                e.Row.Cells(5).Text = e.Row.Cells(5).Text & "_全角"
            End If

            If InStr(e.Row.Cells(5).Text, vbCr) + InStr(e.Row.Cells(5).Text, vbLf) + InStr(e.Row.Cells(5).Text, vbCrLf) > 0 Then
                e.Row.BackColor = Drawing.Color.Blue
                e.Row.Cells(5).Text = e.Row.Cells(5).Text & "_改行"
            End If

            'PALECE OF DELIVERY(配送先)	6
            intval = 0
                intCnt = InStr(intCnt + 1, e.Row.Cells(6).Text, "→")
                Do While intCnt > 0
                    intval = intCnt
                    intCnt = InStr(intCnt + 1, e.Row.Cells(6).Text, "→")
                Loop
                e.Row.Cells(6).Text = Trim(Mid(e.Row.Cells(6).Text, intval + 1, Len(e.Row.Cells(6).Text) - intval))
            intCnt = 0

            If Len(e.Row.Cells(6).Text) <> System.Text.Encoding.GetEncoding("shift_jis").GetByteCount(e.Row.Cells(6).Text) Then
                e.Row.BackColor = Drawing.Color.Green
                e.Row.Cells(6).Text = e.Row.Cells(6).Text & "_全角"
            End If

            If InStr(e.Row.Cells(6).Text, vbCr) + InStr(e.Row.Cells(6).Text, vbLf) + InStr(e.Row.Cells(6).Text, vbCrLf) > 0 Then
                e.Row.BackColor = Drawing.Color.Blue
                e.Row.Cells(6).Text = e.Row.Cells(6).Text & "_改行"
            End If

            '荷受地	7
            intval = 0
                intCnt = InStr(intCnt + 1, e.Row.Cells(7).Text, "→")
                Do While intCnt > 0
                    intval = intCnt
                    intCnt = InStr(intCnt + 1, e.Row.Cells(7).Text, "→")
                Loop
                e.Row.Cells(7).Text = Trim(Mid(e.Row.Cells(7).Text, intval + 1, Len(e.Row.Cells(7).Text) - intval))
            intCnt = 0

            If Len(e.Row.Cells(7).Text) <> System.Text.Encoding.GetEncoding("shift_jis").GetByteCount(e.Row.Cells(7).Text) Then
                e.Row.BackColor = Drawing.Color.Green
                e.Row.Cells(7).Text = e.Row.Cells(7).Text & "_全角"
            End If

            If InStr(e.Row.Cells(7).Text, vbCr) + InStr(e.Row.Cells(7).Text, vbLf) + InStr(e.Row.Cells(7).Text, vbCrLf) > 0 Then
                e.Row.BackColor = Drawing.Color.Blue
                e.Row.Cells(7).Text = e.Row.Cells(7).Text & "_改行"
            End If

            'PLACE OF DELIVERY BY CARRIER(配送者責任送り先)	8
            intval = 0
                intCnt = InStr(intCnt + 1, e.Row.Cells(8).Text, "→")
                Do While intCnt > 0
                    intval = intCnt
                    intCnt = InStr(intCnt + 1, e.Row.Cells(8).Text, "→")
                Loop
                e.Row.Cells(8).Text = Trim(Mid(e.Row.Cells(8).Text, intval + 1, Len(e.Row.Cells(8).Text) - intval))
                intCnt = 0

            If Len(e.Row.Cells(8).Text) <> System.Text.Encoding.GetEncoding("shift_jis").GetByteCount(e.Row.Cells(8).Text) Then
                e.Row.BackColor = Drawing.Color.Green
                e.Row.Cells(8).Text = e.Row.Cells(8).Text & "_全角"
            End If

            If InStr(e.Row.Cells(8).Text, vbCr) + InStr(e.Row.Cells(8).Text, vbLf) + InStr(e.Row.Cells(8).Text, vbCrLf) > 0 Then
                e.Row.BackColor = Drawing.Color.Blue
                e.Row.Cells(8).Text = e.Row.Cells(8).Text & "_改行"
            End If

            'voyage 16
            intval = 0
                intCnt = InStr(intCnt + 1, e.Row.Cells(16).Text, "→")
                Do While intCnt > 0
                    intval = intCnt
                    intCnt = InStr(intCnt + 1, e.Row.Cells(16).Text, "→")
                Loop
                e.Row.Cells(16).Text = Trim(Mid(e.Row.Cells(16).Text, intval + 1, Len(e.Row.Cells(16).Text) - intval))
                intCnt = 0

            If Len(e.Row.Cells(16).Text) <> System.Text.Encoding.GetEncoding("shift_jis").GetByteCount(e.Row.Cells(16).Text) Then
                e.Row.BackColor = Drawing.Color.Green
                e.Row.Cells(16).Text = e.Row.Cells(16).Text & "_全角"
            End If

            If InStr(e.Row.Cells(16).Text, vbCr) + InStr(e.Row.Cells(16).Text, vbLf) + InStr(e.Row.Cells(16).Text, vbCrLf) > 0 Then
                e.Row.BackColor = Drawing.Color.Blue
                e.Row.Cells(16).Text = e.Row.Cells(16).Text & "_改行"
            End If

            '船社	17
            intval = 0
                intCnt = InStr(intCnt + 1, e.Row.Cells(17).Text, "→")
                Do While intCnt > 0
                    intval = intCnt
                    intCnt = InStr(intCnt + 1, e.Row.Cells(17).Text, "→")
                Loop
                e.Row.Cells(17).Text = Trim(Mid(e.Row.Cells(17).Text, intval + 1, Len(e.Row.Cells(17).Text) - intval))
            intCnt = 0

            'If Len(e.Row.Cells(17).Text) <> System.Text.Encoding.GetEncoding("shift_jis").GetByteCount(e.Row.Cells(17).Text) Then
            '    e.Row.BackColor = Drawing.Color.Green
            '    e.Row.Cells(17).Text = e.Row.Cells(17).Text & "_全角"
            'End If

            If InStr(e.Row.Cells(17).Text, vbCr) + InStr(e.Row.Cells(17).Text, vbLf) + InStr(e.Row.Cells(17).Text, vbCrLf) > 0 Then
                e.Row.BackColor = Drawing.Color.Blue
                e.Row.Cells(17).Text = e.Row.Cells(17).Text & "_改行"
            End If


            e.Row.Cells(19).Text = Replace(e.Row.Cells(19).Text, "AT_", "")

            e.Row.Cells(19).Text = get_kaika(Trim(e.Row.Cells(19).Text))

            If e.Row.Cells(19).Text = "" Then

                e.Row.Cells(0).Text = e.Row.Cells(0).Text & "海貨御者英名変換マスタ登録必要"

            End If


            'BOOKING_NO	    21
            intval = 0
                intCnt = InStr(intCnt + 1, e.Row.Cells(21).Text, "→")
                Do While intCnt > 0
                    intval = intCnt
                    intCnt = InStr(intCnt + 1, e.Row.Cells(21).Text, "→")
                Loop
                e.Row.Cells(21).Text = Trim(Replace(Mid(e.Row.Cells(21).Text, intval + 1, Len(e.Row.Cells(21).Text) - intval), vbLf, ""))
                intCnt = 0

            If Len(e.Row.Cells(21).Text) <> System.Text.Encoding.GetEncoding("shift_jis").GetByteCount(e.Row.Cells(21).Text) Then
                e.Row.BackColor = Drawing.Color.Green
                e.Row.Cells(21).Text = e.Row.Cells(21).Text & "_全角"
            End If

            If InStr(e.Row.Cells(21).Text, vbCr) + InStr(e.Row.Cells(21).Text, vbLf) + InStr(e.Row.Cells(21).Text, vbCrLf) > 0 Then
                e.Row.BackColor = Drawing.Color.Blue
                e.Row.Cells(21).Text = e.Row.Cells(21).Text & "_改行"
            End If

            If e.Row.Cells(0).Text = "C255" Then

                e.Row.Cells(21).Text = "C255" & Format(DateValue(e.Row.Cells(11).Text), "yyyyMMdd")          'booking no

                e.Row.Cells(16).Text = "-"
                e.Row.Cells(22).Text = "-"

            ElseIf e.Row.Cells(0).Text = "C258" Then

                e.Row.Cells(21).Text = "C258" & Format(DateValue(e.Row.Cells(11).Text), "yyyyMMdd")          'booking no

                e.Row.Cells(16).Text = "-"
                e.Row.Cells(22).Text = "-"



            End If

                '船名	22
                intval = 0
                intCnt = InStr(intCnt + 1, e.Row.Cells(22).Text, "→")
                Do While intCnt > 0
                    intval = intCnt
                    intCnt = InStr(intCnt + 1, e.Row.Cells(22).Text, "→")
                Loop
                e.Row.Cells(22).Text = Trim(Mid(e.Row.Cells(22).Text, intval + 1, Len(e.Row.Cells(22).Text) - intval))
            intCnt = 0

            If Len(e.Row.Cells(22).Text) <> System.Text.Encoding.GetEncoding("shift_jis").GetByteCount(e.Row.Cells(22).Text) Then
                e.Row.BackColor = Drawing.Color.Green
                e.Row.Cells(22).Text = e.Row.Cells(22).Text & "_全角"
            End If

            If InStr(e.Row.Cells(22).Text, vbCr) + InStr(e.Row.Cells(22).Text, vbLf) + InStr(e.Row.Cells(22).Text, vbCrLf) > 0 Then
                e.Row.BackColor = Drawing.Color.Blue
                e.Row.Cells(22).Text = e.Row.Cells(22).Text & "_改行"
            End If

            'place of delivery SI	25
            intval = 0
                intCnt = InStr(intCnt + 1, e.Row.Cells(25).Text, "→")
                Do While intCnt > 0
                    intval = intCnt
                    intCnt = InStr(intCnt + 1, e.Row.Cells(25).Text, "→")
                Loop
                e.Row.Cells(25).Text = Trim(Mid(e.Row.Cells(25).Text, intval + 1, Len(e.Row.Cells(25).Text) - intval))
                intCnt = 0

                '20Ft	
                intval = 0
                intCnt = InStr(intCnt + 1, e.Row.Cells(33).Text, "→")
                Do While intCnt > 0
                    intval = intCnt
                    intCnt = InStr(intCnt + 1, e.Row.Cells(33).Text, "→")
                Loop
                e.Row.Cells(33).Text = Mid(e.Row.Cells(33).Text, intval + 1, Len(e.Row.Cells(33).Text) - intval)
                intCnt = 0
                e.Row.Cells(33).Text = Trim(Left(Trim(e.Row.Cells(33).Text), 1))
                If IsNumeric(e.Row.Cells(33).Text) = True Then
                Else
                    e.Row.Cells(33).Text = 0
                End If

                '40Ft	
                intval = 0
                intCnt = InStr(intCnt + 1, e.Row.Cells(34).Text, "→")
                Do While intCnt > 0
                    intval = intCnt
                    intCnt = InStr(intCnt + 1, e.Row.Cells(34).Text, "→")
                Loop
                e.Row.Cells(34).Text = Mid(e.Row.Cells(34).Text, intval + 1, Len(e.Row.Cells(34).Text) - intval)
                intCnt = 0
                e.Row.Cells(34).Text = Trim(Left(Trim(e.Row.Cells(34).Text), 1))
                If IsNumeric(e.Row.Cells(34).Text) = True Then
                Else
                    e.Row.Cells(34).Text = 0
                End If

                'LCL/40Ft
                intval = 0
                intCnt = InStr(intCnt + 1, e.Row.Cells(35).Text, "→")
                Do While intCnt > 0
                    intval = intCnt
                    intCnt = InStr(intCnt + 1, e.Row.Cells(35).Text, "→")
                Loop
                e.Row.Cells(35).Text = Mid(e.Row.Cells(35).Text, intval + 1, Len(e.Row.Cells(35).Text) - intval)
                intCnt = 0

                'M3以外は数量を算出
                If e.Row.Cells(35).Text Like "*M3*" Then
                Else
                    e.Row.Cells(35).Text = Trim(Left(Trim(e.Row.Cells(35).Text), 1))
                    If IsNumeric(e.Row.Cells(35).Text) = True Then
                    Else
                        e.Row.Cells(35).Text = 0
                    End If
                End If

                'M3以外は40FTに追加する
                If e.Row.Cells(35).Text Like "*M3*" Then
                Else
                    e.Row.Cells(34).Text = Integer.Parse(e.Row.Cells(35).Text) + Integer.Parse(e.Row.Cells(34).Text)
                    e.Row.Cells(35).Text = 0
                End If

                '出荷元ストアコード	　13
                code = e.Row.Cells(0).Text
                Select Case code

                    Case "E170", "E232", "E250"                     '上野出荷元ストアコード
                        e.Row.Cells(13).Text = "0LNF"

                    Case "E134"
                        e.Row.Cells(13).Text = "0LNR"

                    Case Else
                        e.Row.Cells(13).Text = "0BNA"                           '出荷元ストアコード
                End Select

                '出荷方法		　14
                Select Case code
                    Case "E134", "E170", "E232", "E250"                             '上野出荷拠点
                        e.Row.Cells(14).Text = "U"
                    Case Else
                        e.Row.Cells(14).Text = "O"                               '出荷拠点
                End Select


                '「M3」がN列に含まれている場合、出荷方法 CFS
                If e.Row.Cells(35).Text Like "*M3*" Then       'NULLチェック
                    e.Row.Cells(14).Text = "03"
                    '上記以外で、M列に1以上の数字が入っている場合、出荷方法 40ft
                ElseIf e.Row.Cells(34).Text > 0 Then
                    e.Row.Cells(14).Text = "02"

                    '上記以外で、L列に1以上の数字が入っている場合、出荷方法 20ft
                ElseIf e.Row.Cells(33).Text > 0 Then
                    e.Row.Cells(14).Text = "01"

                Else
                    e.Row.Cells(14).Text = "-"
                End If


                '出荷拠点		　15
                Select Case code
                    Case "E134", "E170", "E232", "E250"                             '上野出荷拠点
                        e.Row.Cells(15).Text = "U"
                    Case Else
                        e.Row.Cells(15).Text = "O"                               '出荷拠点
                End Select

                '船社担当者			　18
                e.Row.Cells(18).Text = "-"

                '通関方法				　27
                e.Row.Cells(27).Text = "包括"

                '船積スケジュール登録					　29 マニュアル
                e.Row.Cells(29).Text = "有り"

                'コンテナ情報登録					　30
                e.Row.Cells(30).Text = "有り"


                '接続文字列の作成
                Dim ConnectionString As String = String.Empty

                'SQL Server認証
                ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"

                'SqlConnectionクラスの新しいインスタンスを初期化
                Dim cnn = New SqlConnection(ConnectionString)
                Dim Command = cnn.CreateCommand

                'データベース接続を開く
                cnn.Open()

                strSQL = "SELECT T_EXL_CSMANUAL.* "
                strSQL = strSQL & "FROM T_EXL_CSMANUAL "
                strSQL = strSQL & "WHERE T_EXL_CSMANUAL.NEW_CODE = '" & code & "' "

                'ＳＱＬコマンド作成
                dbcmd = New SqlCommand(strSQL, cnn)
                'ＳＱＬ文実行
                dataread = dbcmd.ExecuteReader()

                '結果を取り出す
                While (dataread.Read())
                    strbkg += dataread(56)

                    'Finaldestination(届け先名)	2
                    e.Row.Cells(2).Text += dataread(56)
                    'Finaldestination ADDRESS(届け先住所)	3
                    e.Row.Cells(3).Text += dataread(57)
                '乙仲名	 19
                e.Row.Cells(32).Text += dataread(60)
                '乙仲担当者	 20
                If dataread(61) = "" Then
                        e.Row.Cells(20).Text += "-"
                    Else
                        e.Row.Cells(20).Text += dataread(61)

                    End If


                    'consinerr name of SI		 23
                    e.Row.Cells(23).Text += dataread(54)
                    'consiner address of SI		 24
                    e.Row.Cells(24).Text += dataread(55)
                    'Nortify address			 26
                    e.Row.Cells(26).Text += dataread(11)
                    'ベアリング帳票出力					　28
                    e.Row.Cells(28).Text += dataread(58)
                    'INVOICE内訳自動計算						　31　マニュアル
                    e.Row.Cells(31).Text += dataread(59)

                End While

                'クローズ処理
                dataread.Close()
                dbcmd.Dispose()




                If IsDate(e.Row.Cells(1).Text) = False Then

                    If e.Row.Cells(0).Text = "C255" Or e.Row.Cells(0).Text = "C258" Then

                    Else

                    e.Row.Cells(0).Text = "日付エラー02行目_" & e.Row.Cells(0).Text
                    e.Row.BackColor = Drawing.Color.Red

                    End If

                Else
                    '半年移行前の日付の場合は翌年にする
                    If DateValue(e.Row.Cells(1).Text) < dt00 Then
                        e.Row.Cells(1).Text = DateValue(Format(DateValue(DateAdd("yyyy", 1, e.Row.Cells(1).Text)), "yyyy") & Format(DateValue(e.Row.Cells(1).Text), "/mm/dd"))
                    End If
                End If

                If IsDate(e.Row.Cells(9).Text) = False Then
                    If e.Row.Cells(0).Text = "C255" Or e.Row.Cells(0).Text = "C258" Then

                    Else

                    e.Row.Cells(0).Text = "日付エラー10行目_" & e.Row.Cells(0).Text
                    e.Row.BackColor = Drawing.Color.Red

                    End If
                Else
                    '半年移行前の日付の場合は翌年にする
                    If DateValue(e.Row.Cells(9).Text) < dt00 Then
                        e.Row.Cells(9).Text = DateValue(Format(DateValue(DateAdd("yyyy", 1, e.Row.Cells(9).Text)), "yyyy") & Format(DateValue(e.Row.Cells(9).Text), "/mm/dd"))
                    End If
                End If

                If IsDate(e.Row.Cells(10).Text) = False Then
                    If e.Row.Cells(0).Text = "C255" Or e.Row.Cells(0).Text = "C258" Then

                    Else

                    e.Row.Cells(0).Text = "日付エラー11行目_" & e.Row.Cells(0).Text
                    e.Row.BackColor = Drawing.Color.Red

                    End If
                Else
                    '半年移行前の日付の場合は翌年にする
                    If DateValue(e.Row.Cells(10).Text) < dt00 Then
                        e.Row.Cells(10).Text = DateValue(Format(DateValue(DateAdd("yyyy", 1, e.Row.Cells(10).Text)), "yyyy") & Format(DateValue(e.Row.Cells(10).Text), "/mm/dd"))
                    End If
                End If

                If IsDate(e.Row.Cells(11).Text) = False Then
                    If e.Row.Cells(0).Text = "C255" Or e.Row.Cells(0).Text = "C258" Then

                    Else

                    e.Row.Cells(0).Text = "日付エラー12行目_" & e.Row.Cells(0).Text
                    e.Row.BackColor = Drawing.Color.Red

                    End If
                Else
                    '半年移行前の日付の場合は翌年にする
                    If DateValue(e.Row.Cells(11).Text) < dt00 Then
                        e.Row.Cells(11).Text = DateValue(Format(DateValue(DateAdd("yyyy", 1, e.Row.Cells(11).Text)), "yyyy") & Format(DateValue(e.Row.Cells(11).Text), "/mm/dd"))
                    End If
                End If

                If e.Row.Cells(12).Text <> "&nbsp;" Then

                    '対象の日付以下の日付の最大値を取得
                    strSQL = "SELECT MAX(WORKDAY) AS WDAY01 FROM [T_EXL_CSWORKDAY] WHERE [T_EXL_CSWORKDAY].WORKDAY < '" & e.Row.Cells(12).Text & "' "

                    'ＳＱＬコマンド作成 
                    dbcmd = New SqlCommand(strSQL, cnn)
                    'ＳＱＬ文実行 
                    dataread = dbcmd.ExecuteReader()

                    '結果を取り出す 
                    While (dataread.Read())
                        e.Row.Cells(12).Text = dataread("WDAY01")
                    End While

                    dataread.Close()
                    dbcmd.Dispose()

                End If

                cnn.Close()
                cnn.Dispose()


                If IsDate(e.Row.Cells(12).Text) = False Then
                    e.Row.Cells(0).Text = "日付エラー13行目"
                    e.Row.BackColor = Drawing.Color.Red
                Else
                    '半年移行前の日付の場合は翌年にする
                    If DateValue(e.Row.Cells(12).Text) < dt00 Then
                        e.Row.Cells(12).Text = DateValue(Format(DateValue(DateAdd("yyyy", 1, e.Row.Cells(12).Text)), "yyyy") & Format(DateValue(e.Row.Cells(12).Text), "/mm/dd"))
                    End If
                End If

            End If



        '不要行非表示


        'e.Row.Cells(33).Visible = False
        'e.Row.Cells(34).Visible = False
        'e.Row.Cells(35).Visible = False
        'e.Row.Cells(36).Visible = False



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

        Button1.Attributes.Add("onclick", "return confirm('ファイルを出力します。よろしいですか？');")

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click


        Dim t As Integer
        t = 1
        Dim cnt As Integer = 0

        Dim lastRow As Integer
        Dim lastCol As Integer

        Dim lastRow2 As Integer
        Dim ro As Integer
        Dim co As Integer

        Dim val01 As String = ""

        GridView1.DataBind()

        If GridView1.Rows.Count >= 1 Then

            Using wb As XLWorkbook = New XLWorkbook()
                Dim ws As IXLWorksheet = wb.AddWorksheet("INVHDSHEET")



                For Each cell As TableCell In GridView1.HeaderRow.Cells

                    If cnt > 31 Then


                    Else
                        ws.Cell(1, t).Value = cell.Text
                        t = t + 1
                        cnt = cnt + 1
                    End If
                Next

                cnt = 0
                t = 2
                For Each row As GridViewRow In GridView1.Rows

                    If row.Cells(0).Text Like "*日付エラー*" Then

                    Else

                        For i As Integer = 0 To row.Cells.Count - 5
                        val01 = Trim(Replace(row.Cells(i).Text, "&nbsp;", ""))
                        Select Case i

                            Case 1, 9 To 12
                                If IsDate(val01) = True Then
                                    ws.Cell(t, i + 1).SetValue(DateValue(val01))
                                Else

                                    If row.Cells(0).Text = "C258" Or row.Cells(0).Text = "C255" Then
                                        ws.Cell(t, i + 1).SetValue(DateValue(row.Cells(11).Text))
                                    Else

                                        ws.Cell(t, i + 1).SetValue("日付エラー")
                                        ws.Cell(t, 1).Style.Fill.BackgroundColor = XLColor.Red
                                    End If
                                End If

                            Case 2 To 8, 16, 18 To 26
                                If Len(row.Cells(i).Text) <> System.Text.Encoding.GetEncoding("shift_jis").GetByteCount(row.Cells(i).Text) Then
                                    ws.Cell(t, 1).Style.Fill.BackgroundColor = XLColor.Green
                                    ws.Cell(t, i + 1).Style.Fill.BackgroundColor = XLColor.Green
                                    ws.Cell(t, i + 1).SetValue("全角エラー")
                                    ws.Cell(t, 1).SetValue("全角エラー")
                                Else
                                    ws.Cell(t, i + 1).SetValue(val01)
                                End If

                            Case 0, 2, 4 To 8, 16 To 23, 25

                                If InStr(row.Cells(i).Text, vbCr) + InStr(row.Cells(i).Text, vbLf) + InStr(row.Cells(i).Text, vbCrLf) > 0 Then
                                    ws.Cell(t, 1).Style.Fill.BackgroundColor = XLColor.Blue
                                    ws.Cell(t, i + 1).Style.Fill.BackgroundColor = XLColor.Blue
                                    ws.Cell(t, i + 1).SetValue(val01)
                                    ws.Cell(t, i + 1).SetValue("改行エラー")
                                    ws.Cell(t, 1).SetValue("改行エラー")
                                Else
                                    ws.Cell(t, i + 1).SetValue(val01)
                                End If

                            Case Else
                                ws.Cell(t, i + 1).SetValue(val01)
                        End Select

                    Next
                        t = t + 1
                    End If
                Next

                ws.Style.Font.FontName = "Meiryo UI"
                ws.Style.Alignment.WrapText = False
                ws.Columns.AdjustToContents()
                ws.SheetView.FreezeRows(1)



                lastRow = ws.LastRowUsed().RowNumber()
                lastCol = ws.LastColumnUsed().ColumnNumber()

                For ro = 2 To lastRow


                    Select Case Left(ws.Cell(ro, 1).Value, 4)

                        Case "E211"
                            lastRow2 = ws.LastRowUsed().RowNumber()
                            For co = 1 To lastCol
                                If co = 1 Then
                                    ws.Cell(lastRow2 + 1, co).SetValue("E213")
                                    ws.Cell(lastRow2 + 2, co).SetValue("E214")
                                    ws.Cell(lastRow2 + 3, co).SetValue("E215")
                                    ws.Cell(lastRow2 + 4, co).SetValue("K52C")
                                Else
                                    ws.Cell(lastRow2 + 1, co).SetValue(ws.Cell(ro, co).Value)
                                    ws.Cell(lastRow2 + 2, co).SetValue(ws.Cell(ro, co).Value)
                                    ws.Cell(lastRow2 + 3, co).SetValue(ws.Cell(ro, co).Value)
                                    ws.Cell(lastRow2 + 4, co).SetValue(ws.Cell(ro, co).Value)
                                End If
                            Next

                        Case "E153"
                            lastRow2 = ws.LastRowUsed().RowNumber()
                            For co = 1 To lastCol
                                If co = 1 Then
                                    ws.Cell(lastRow2 + 1, co).SetValue("E156")
                                    ws.Cell(lastRow2 + 2, co).SetValue("K501")
                                    ws.Cell(lastRow2 + 3, co).SetValue("E15A")
                                Else
                                    ws.Cell(lastRow2 + 1, co).SetValue(ws.Cell(ro, co).Value)
                                    ws.Cell(lastRow2 + 2, co).SetValue(ws.Cell(ro, co).Value)
                                    ws.Cell(lastRow2 + 3, co).SetValue(ws.Cell(ro, co).Value)
                                End If
                            Next

                        Case "E242"
                            lastRow2 = ws.LastRowUsed().RowNumber()
                            For co = 1 To lastCol
                                If co = 1 Then
                                    ws.Cell(lastRow2 + 1, co).SetValue("E243")
                                    ws.Cell(lastRow2 + 2, co).SetValue("E244")
                                    ws.Cell(lastRow2 + 3, co).SetValue("E248")
                                Else
                                    ws.Cell(lastRow2 + 1, co).SetValue(ws.Cell(ro, co).Value)
                                    ws.Cell(lastRow2 + 2, co).SetValue(ws.Cell(ro, co).Value)
                                    ws.Cell(lastRow2 + 3, co).SetValue(ws.Cell(ro, co).Value)
                                End If
                            Next

                        Case "E290"
                            lastRow2 = ws.LastRowUsed().RowNumber()
                            For co = 1 To lastCol
                                If co = 1 Then
                                    ws.Cell(lastRow2 + 1, co).SetValue("K51R")
                                Else
                                    ws.Cell(lastRow2 + 1, co).SetValue(ws.Cell(ro, co).Value)
                                End If
                            Next

                        Case "B261"
                            lastRow2 = ws.LastRowUsed().RowNumber()
                            For co = 1 To lastCol
                                If co = 1 Then
                                    ws.Cell(lastRow2 + 1, co).SetValue("K561")
                                Else
                                    ws.Cell(lastRow2 + 1, co).SetValue(ws.Cell(ro, co).Value)
                                End If
                            Next

                        Case "E410"
                            lastRow2 = ws.LastRowUsed().RowNumber()
                            For co = 1 To lastCol
                                If co = 1 Then
                                    ws.Cell(lastRow2 + 1, co).SetValue("K580")
                                Else
                                    ws.Cell(lastRow2 + 1, co).SetValue(ws.Cell(ro, co).Value)
                                End If
                            Next

                        Case "E231"
                            lastRow2 = ws.LastRowUsed().RowNumber()
                            For co = 1 To lastCol
                                If co = 1 Then
                                    ws.Cell(lastRow2 + 1, co).SetValue("E230")
                                Else
                                    ws.Cell(lastRow2 + 1, co).SetValue(ws.Cell(ro, co).Value)
                                End If
                            Next
                    End Select
                Next


                Dim struid As String = Session("UsrId")
                wb.SaveAs("\\svnas201\EXD06101\DISC_COMMON\WEB出力\Mak_ivhd_" & Now.ToString(“yyMMdd”) & "_P_" & struid & ".xlsx")

            End Using

            Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "確認", "<script language='JavaScript'>confirm('出力が完了しました。\n出力先：\\\svnas201\\EXD06101\\DISC_COMMON\\WEB出力');</script>", False)


        Else

            Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "確認", "<script language='JavaScript'>confirm('対象無し');</script>", False)

        End If



    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Dim strcust As String = "'C255','C258','C253'"
        Dim Dataobj As New DBAccess

        Dim ds As DataSet = Dataobj.GET_CS_RESULT_MAKE_IV(strcust)
        If ds.Tables.Count > 0 Then
            GridView1.DataSourceID = ""
            GridView1.DataSource = ds
            GridView1.DataBind()
        End If

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        GridView1.DataSourceID = ""
        GridView1.DataSource = SqlDataSource1
        GridView1.DataBind()

    End Sub


    Private Function get_kaika(strkaika As String) As String

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

        get_kaika = ""

        'データベース接続を開く
        cnn.Open()

        strSQL = ""
        strSQL = strSQL & "SELECT DISTINCT NAME_EG "
        strSQL = strSQL & "FROM M_EXL_KAIKA_CHANGE "
        strSQL = strSQL & "WHERE NAME_JPN = '" & strkaika & "' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        While (dataread.Read())
            get_kaika = dataread("NAME_EG")
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()

        cnn.Close()
        cnn.Dispose()

    End Function

End Class
