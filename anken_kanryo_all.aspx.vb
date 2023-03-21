
Imports System.Data
Imports System.IO
Imports System.Data.SqlClient
Imports System.Data.Common
Imports System.Console
Imports ClosedXML.Excel
Imports MailKit.Net.Smtp
Imports MailKit.Security
Imports MimeKit
Imports MimeKit.Text
Imports System.Linq
Imports mod_function



Partial Class yuusen
    Inherits System.Web.UI.Page

    Public strRow As String
    Public strProcess As String
    Public strPath As String = "C:\exp\cs_home\files"


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

        Dim ts1 As New TimeSpan(80, 0, 0, 0)
        Dim ts2 As New TimeSpan(80, 0, 0, 0)
        Dim dt2 As DateTime = dt1 + ts1
        Dim dt3 As DateTime = dt1 - ts1
        Dim fflg2 As String = ""

        '接続文字列の作成
        Dim ConnectionString00 As String = String.Empty

        'SQL Server認証
        ConnectionString00 = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"

        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn00 = New SqlConnection(ConnectionString00)
        Dim Command00 = cnn00.CreateCommand

        'データベース接続を開く
        cnn00.Open()

        strSQL = "SELECT CODE "
        strSQL = strSQL & "FROM M_EXL_CS_MEMBER "
        strSQL = strSQL & "WHERE CODE = '" & Session("UsrId") & "' "

        'ＳＱＬコマンド作成
        dbcmd = New SqlCommand(strSQL, cnn00)
        'ＳＱＬ文実行
        dataread = dbcmd.ExecuteReader()
        strbkg = ""
        '結果を取り出す
        While (dataread.Read())
            fflg2 = dataread("CODE")
        End While

        'クローズ処理
        dataread.Close()
        dbcmd.Dispose()


        If e.Row.RowType = DataControlRowType.DataRow Then

            '仕向地
            If Len(e.Row.Cells(8).Text) > 7 Then
                e.Row.Cells(8).Font.Size = 7
            End If

            '仕向地
            If Len(e.Row.Cells(10).Text) > 20 Then
                e.Row.Cells(10).Font.Size = 7
            End If

            '仕向地
            If Len(e.Row.Cells(10).Text) >= 24 Then
                e.Row.Cells(10).Text = Left(e.Row.Cells(10).Text, 24)
            End If

            'IVNO
            If Len(e.Row.Cells(11).Text) > 19 Then
                e.Row.Cells(11).Font.Size = 7.5
            End If

            'IVNO
            If Len(e.Row.Cells(9).Text) > 8 Then
                e.Row.Cells(9).Font.Size = 7.5
            End If

            '船名
            If Len(e.Row.Cells(17).Text) > 19 Then
                e.Row.Cells(17).Font.Size = 7.5
            End If

            '荷受地
            If Len(e.Row.Cells(19).Text) > 5 Then
                e.Row.Cells(19).Font.Size = 7
            End If

            '積出港
            If Len(e.Row.Cells(20).Text) > 5 Then
                e.Row.Cells(20).Font.Size = 7
            End If

            '揚港
            If Len(e.Row.Cells(21).Text) > 16 Then
                e.Row.Cells(21).Font.Size = 7
            End If

            '進捗状況	
            If Len(e.Row.Cells(15).Text) > 7 Then
                e.Row.Cells(15).Font.Size = 7
            End If

            '配送先
            If Len(e.Row.Cells(22).Text) > 16 Then
                e.Row.Cells(22).Font.Size = 7
            End If

            '接続文字列の作成
            Dim ConnectionString As String = String.Empty

            'SQL Server認証
            ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"

            'SqlConnectionクラスの新しいインスタンスを初期化
            Dim cnn = New SqlConnection(ConnectionString)
            Dim Command = cnn.CreateCommand

            Dim dltlabel1 As Label = e.Row.FindControl("Label1")
            Dim dltlabel2 As Label = e.Row.FindControl("Label2")
            Dim btn01 As Button = e.Row.FindControl("Button1")
            Dim btn02 As Button = e.Row.FindControl("Button2")

            'データベース接続を開く
            cnn.Open()

            'strSQL = "SELECT ITK_BKGNO FROM [T_EXL_CSWORKSTATUS] WHERE [T_EXL_CSWORKSTATUS].ITK_BKGNO = '" & Trim(e.Row.Cells(26).Text) & "' "
            strSQL = "SELECT BKGNO FROM [T_EXL_WORKSTATUS00] WHERE [T_EXL_WORKSTATUS00].BKGNO = '" & Trim(Replace(e.Row.Cells(16).Text, vbLf, "")) & "' "
            strSQL = strSQL & "AND [T_EXL_WORKSTATUS00].ID = '006' "

            'ＳＱＬコマンド作成
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行
            dataread = dbcmd.ExecuteReader()
            strbkg = ""
            '結果を取り出す
            While (dataread.Read())
                strbkg += dataread("BKGNO")
                '書類作成状況
                If Trim(e.Row.Cells(16).Text) = Trim(strbkg) And strbkg <> "&nbsp;" Then
                    'e.Row.BackColor = Drawing.Color.DarkGray

                    Dim dltcb01 As CheckBox = e.Row.FindControl("cb01")

                    dltcb01.Checked = True
                    'dltcb02.Checked = True

                End If
            End While

            'クローズ処理
            dataread.Close()
            dbcmd.Dispose()


            'strSQL = "SELECT ITK_BKGNO FROM [T_EXL_CSWORKSTATUS] WHERE [T_EXL_CSWORKSTATUS].ITK_BKGNO = '" & Trim(e.Row.Cells(26).Text) & "' "
            strSQL = "SELECT BKGNO FROM [T_EXL_WORKSTATUS00] WHERE [T_EXL_WORKSTATUS00].BKGNO = '" & Trim(Replace(e.Row.Cells(16).Text, vbLf, "")) & "' "
            strSQL = strSQL & "AND [T_EXL_WORKSTATUS00].ID = '007' "

            'ＳＱＬコマンド作成
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行
            dataread = dbcmd.ExecuteReader()
            strbkg = ""
            '結果を取り出す
            While (dataread.Read())
                strbkg += dataread("BKGNO")
                '書類作成状況
                If Trim(e.Row.Cells(16).Text) = Trim(strbkg) And strbkg <> "&nbsp;" Then
                    'e.Row.BackColor = Drawing.Color.DarkGray


                    Dim dltcb02 As CheckBox = e.Row.FindControl("cb02")

                    'dltcb01.Checked = True
                    dltcb02.Checked = True

                End If
            End While

            'クローズ処理
            dataread.Close()
            dbcmd.Dispose()


            'strSQL = "SELECT ITK_BKGNO FROM [T_EXL_CSWORKSTATUS] WHERE [T_EXL_CSWORKSTATUS].ITK_BKGNO = '" & Trim(e.Row.Cells(26).Text) & "' "
            strSQL = "SELECT BKGNO FROM [T_EXL_WORKSTATUS00] WHERE [T_EXL_WORKSTATUS00].BKGNO = '" & Trim(Replace(e.Row.Cells(16).Text, vbLf, "")) & "' "
            strSQL = strSQL & "AND [T_EXL_WORKSTATUS00].ID = '008' "
            strSQL = strSQL & "AND [T_EXL_WORKSTATUS00].INVNO = '" & Trim(Replace(e.Row.Cells(30).Text, vbLf, "")) & "' "

            'ＳＱＬコマンド作成
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行
            dataread = dbcmd.ExecuteReader()
            strbkg = ""
            '結果を取り出す
            While (dataread.Read())
                strbkg += dataread("BKGNO")
                '書類作成状況
                If Trim(e.Row.Cells(16).Text) = Trim(strbkg) And strbkg <> "&nbsp;" Then

                    dltlabel1.Text = "〇"

                End If
            End While

            'クローズ処理
            dataread.Close()
            dbcmd.Dispose()


            'strSQL = "SELECT ITK_BKGNO FROM [T_EXL_CSWORKSTATUS] WHERE [T_EXL_CSWORKSTATUS].ITK_BKGNO = '" & Trim(e.Row.Cells(26).Text) & "' "
            strSQL = "SELECT BKGNO FROM [T_EXL_WORKSTATUS00] WHERE [T_EXL_WORKSTATUS00].BKGNO = '" & Trim(Replace(e.Row.Cells(16).Text, vbLf, "")) & "' "
            strSQL = strSQL & "AND [T_EXL_WORKSTATUS00].ID = '009' "
            strSQL = strSQL & "AND [T_EXL_WORKSTATUS00].INVNO = '" & Trim(Replace(e.Row.Cells(30).Text, vbLf, "")) & "' "


            'ＳＱＬコマンド作成
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行
            dataread = dbcmd.ExecuteReader()
            strbkg = ""
            '結果を取り出す
            While (dataread.Read())
                strbkg += dataread("BKGNO")
                '書類作成状況
                If Trim(e.Row.Cells(16).Text) = Trim(strbkg) And strbkg <> "&nbsp;" Then

                    dltlabel2.Text = "〇"

                End If
            End While

            'クローズ処理
            dataread.Close()
            dbcmd.Dispose()

            If Trim(e.Row.Cells(7).Text) = "LCL" Then
                e.Row.Cells(14).Text = e.Row.Cells(14).Text
            End If

            If Trim(e.Row.Cells(29).Text) = "1" Then
                e.Row.BackColor = Drawing.Color.Orange
            End If


            'strSQL = "SELECT ITK_BKGNO FROM [T_EXL_CSWORKSTATUS] WHERE [T_EXL_CSWORKSTATUS].ITK_BKGNO = '" & Trim(e.Row.Cells(26).Text) & "' "
            strSQL = "SELECT BKGNO FROM [T_EXL_WORKSTATUS00] WHERE [T_EXL_WORKSTATUS00].BKGNO = '" & Trim(Replace(e.Row.Cells(16).Text, vbLf, "")) & "' "
            strSQL = strSQL & "AND [T_EXL_WORKSTATUS00].ID = '010' "
            strSQL = strSQL & "AND [T_EXL_WORKSTATUS00].INVNO = '" & Trim(Replace(e.Row.Cells(30).Text, vbLf, "")) & "' "


            'ＳＱＬコマンド作成
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行
            dataread = dbcmd.ExecuteReader()
            strbkg = ""
            '結果を取り出す
            While (dataread.Read())
                strbkg += dataread("BKGNO")
                '書類作成状況
                If Trim(e.Row.Cells(16).Text) = Trim(strbkg) And strbkg <> "&nbsp;" Then


                    If dltlabel2.Text = "〇" Then
                        e.Row.ForeColor = Drawing.Color.White
                        e.Row.BackColor = Drawing.Color.Red
                        e.Row.Font.Bold = True
                    Else

                        e.Row.Font.Strikeout = True
                        e.Row.BackColor = Drawing.Color.LightGray
                        'btn01.Text = "戻す"


                    End If

                End If
            End While

            'クローズ処理
            dataread.Close()
            dbcmd.Dispose()

            cnn.Close()
            cnn.Dispose()

            dltlabel1.Dispose()
            dltlabel2.Dispose()

            Dim fll As String = "0"
            Call get_meisai(Trim(Replace(e.Row.Cells(16).Text, vbLf, "")), fll)

            If fll = "0" Then
                btn01.Enabled = False
                dltlabel1.Text = "明細未"

            End If

        End If


            'ボタンに行数をセット
            If e.Row.RowType = DataControlRowType.DataRow Then

            Dim fflg As String = ""
            Dim kflg01 As String = ""

            '接続文字列の作成
            Dim ConnectionString As String = String.Empty

            'SQL Server認証
            ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"

            'SqlConnectionクラスの新しいインスタンスを初期化
            Dim cnn = New SqlConnection(ConnectionString)
            Dim Command = cnn.CreateCommand

            'データベース接続を開く
            cnn.Open()

            strSQL = "SELECT T_EXL_CSKANRYO.FLG02 "
            strSQL = strSQL & "FROM T_EXL_CSKANRYO "
            strSQL = strSQL & "WHERE BOOKING_NO = '" & Trim(Replace(e.Row.Cells(16).Text, vbLf, "")) & "' "

            'ＳＱＬコマンド作成
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行
            dataread = dbcmd.ExecuteReader()
            strbkg = ""
            '結果を取り出す
            While (dataread.Read())
                fflg = dataread("FLG02")
            End While

            'クローズ処理
            dataread.Close()
            dbcmd.Dispose()


            'strSQL = "SELECT ITK_BKGNO FROM [T_EXL_CSWORKSTATUS] WHERE [T_EXL_CSWORKSTATUS].ITK_BKGNO = '" & Trim(e.Row.Cells(26).Text) & "' "
            strSQL = "SELECT BKGNO FROM [T_EXL_WORKSTATUS00] WHERE [T_EXL_WORKSTATUS00].BKGNO = '" & Trim(Replace(e.Row.Cells(16).Text, vbLf, "")) & "' "
            strSQL = strSQL & "AND [T_EXL_WORKSTATUS00].ID = '010' "
            strSQL = strSQL & "AND [T_EXL_WORKSTATUS00].INVNO = '" & Trim(Replace(e.Row.Cells(30).Text, vbLf, "")) & "' "


            'ＳＱＬコマンド作成
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行
            dataread = dbcmd.ExecuteReader()
            strbkg = ""
            '結果を取り出す
            While (dataread.Read())
                strbkg += dataread("BKGNO")
                '書類作成状況
                If Trim(e.Row.Cells(16).Text) = Trim(strbkg) And strbkg <> "&nbsp;" Then

                    kflg01 = 1


                End If
            End While

            'クローズ処理
            dataread.Close()
            dbcmd.Dispose()

            cnn.Close()
            cnn.Dispose()

            Dim dltButton As Button = e.Row.FindControl("Button1")
            Dim dltButton2 As Button = e.Row.FindControl("Button2")
            Dim dltButton3 As Button = e.Row.FindControl("Button3")
            Dim dltButton4 As Button = e.Row.FindControl("Button4")
            Dim dltButton5 As Button = e.Row.FindControl("Button5")
            Dim dltButton6 As Button = e.Row.FindControl("Button6")
            Dim dltlabel1 As Label = e.Row.FindControl("Label1")
            Dim dltlabel2 As Label = e.Row.FindControl("Label2")

            If dltlabel2.Text <> "〇" Then
                dltButton2.Visible = False
                ''e.Row.Cells(4).Text = "-"
            Else
                dltButton2.Visible = True
                ''e.Row.Cells(4).Text = ""
            End If

            If kflg01 = "1" Then
                dltButton.Text = "戻す"
            Else
                If Trim(e.Row.Cells(29).Text) = "1" Then
                    dltButton.Text = "最終"
                Else
                    dltButton.Text = "途中"
                End If
            End If

            'ボタンが存在する場合のみセット
            If Not (dltButton Is Nothing) Then
                    dltButton.CommandArgument = e.Row.RowIndex.ToString()
                End If

                'ボタンが存在する場合のみセット
                If Not (dltButton2 Is Nothing) Then
                    dltButton2.CommandArgument = e.Row.RowIndex.ToString()
                End If
            'ボタンが存在する場合のみセット
            If Not (dltButton3 Is Nothing) Then
                dltButton3.CommandArgument = e.Row.RowIndex.ToString()

                If dltlabel2.Text = "〇" Then
                Else
                    dltButton3.Visible = False
                End If

            End If
            'ボタンが存在する場合のみセット
            If Not (dltButton6 Is Nothing) Then
                dltButton6.CommandArgument = e.Row.RowIndex.ToString()
            End If
            'ボタンが存在する場合のみセット
            If Not (dltButton4 Is Nothing) Then
                    dltButton4.CommandArgument = e.Row.RowIndex.ToString()

                If fflg = "" Then
                    dltButton4.Text = "更新"
                Else
                    dltButton4.Text = "戻す"
                End If
            End If
            If Not (dltButton5 Is Nothing) Then
                dltButton5.CommandArgument = e.Row.RowIndex.ToString()
                If e.Row.Cells(28).Text = "1" Then
                Else
                    dltButton5.Visible = False
                End If

            End If


            Dim dltcb01 As CheckBox = e.Row.FindControl("cb01")
                Dim dltcb02 As CheckBox = e.Row.FindControl("cb02")

                'ボタンが存在する場合のみセット
                If Not (dltcb01 Is Nothing) Then
                    dltcb01.AutoPostBack = True
                    If e.Row.Cells(24).Text <> "1" Then
                        dltcb01.Visible = False
                    End If
                End If
                If Not (dltcb02 Is Nothing) Then
                    dltcb02.AutoPostBack = True
                    If e.Row.Cells(23).Text <> "1" Then
                        dltcb02.Visible = False
                    End If
                End If

                'ボタンが存在する場合のみセット
                If Not (dltcb01 Is Nothing) And Not (dltcb02 Is Nothing) Then
                    dltcb01.AutoPostBack = True
                    dltcb02.AutoPostBack = True
                    If e.Row.Cells(24).Text <> "1" And e.Row.Cells(23).Text <> "1" Then
                        dltcb01.Visible = True
                        dltcb02.Visible = True
                    End If
                End If

                Dim ckfl00 As String = ""
                Dim ckfl01 As String = ""

                'ボタンが存在する場合のみセット
                If dltcb01.Visible = True Then
                    If dltcb01.Checked = True Then
                        ckfl00 = ""
                    Else
                        ckfl00 = "KD"
                    End If
                End If
                'ボタンが存在する場合のみセット
                If dltcb02.Visible = True Then
                    If dltcb02.Checked = True Then
                        ckfl00 = ""
                    Else
                        ckfl01 = "AM"
                    End If
                End If

                If dltlabel1.Text = "〇" Then
                    dltButton.Attributes.Add("onclick", "return confirm('完了報告を解除しますか？');")
                Else
                    If ckfl00 = "KD" And ckfl01 = "AM" Then
                        dltButton.Attributes.Add("onclick", "return confirm('KD、ｱﾌﾀどちらもチェックがありませんが処理を進めますか？\r\n※ｱﾌﾀ、KD混載時は注意してください。');")
                    ElseIf ckfl00 = "KD" And ckfl01 = "" Then
                        dltButton.Attributes.Add("onclick", "return confirm('KDにチェックがありませんが処理を進めますか？\r\n※ｱﾌﾀ、KD混載時は注意してください。');")
                    ElseIf ckfl00 = "" And ckfl01 = "AM" Then
                        dltButton.Attributes.Add("onclick", "return confirm('ｱﾌﾀにチェックがありませんが処理を進めますか？\r\n※ｱﾌﾀ、KD混載時は注意してください。');")
                    Else
                        dltButton.Attributes.Add("onclick", "return confirm('チェックボックス確認\r\n※ｱﾌﾀ、KD混載時は注意してください。');")
                    End If
                End If


                dltButton.Dispose()
                dltButton2.Dispose()
                dltButton3.Dispose()
                dltButton4.Dispose()
                dltButton5.Dispose()
                dltlabel1.Dispose()
                dltlabel2.Dispose()
                dltcb01.Dispose()
                dltcb02.Dispose()


            End If


            If fflg2 = "" Then

            e.Row.Cells(4).Visible = False
            e.Row.Cells(5).Visible = False
            e.Row.Cells(10).Visible = False
            e.Row.Cells(15).Visible = False
            e.Row.Cells(18).Visible = False
            e.Row.Cells(19).Visible = False
            e.Row.Cells(17).Visible = False
            e.Row.Cells(20).Visible = False
            e.Row.Cells(21).Visible = False
            e.Row.Cells(22).Visible = False

            e.Row.Cells(25).Visible = False
            GridView1.Width = 1250

        End If



        '23 24 visible false
        e.Row.Cells(23).Visible = False
        e.Row.Cells(24).Visible = False
        e.Row.Cells(28).Visible = False

        e.Row.Cells(29).Visible = False
        e.Row.Cells(30).Visible = False
        e.Row.Cells(31).Visible = False

        cnn00.Close()
        cnn00.Dispose()



    End Sub
    Private Sub GridView2_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles GridView2.RowDataBound

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

        Dim ts1 As New TimeSpan(80, 0, 0, 0)
        Dim ts2 As New TimeSpan(80, 0, 0, 0)
        Dim dt2 As DateTime = dt1 + ts1
        Dim dt3 As DateTime = dt1 - ts1
        Dim fflg2 As String = ""

        '接続文字列の作成
        Dim ConnectionString00 As String = String.Empty

        'SQL Server認証
        ConnectionString00 = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"

        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn00 = New SqlConnection(ConnectionString00)
        Dim Command00 = cnn00.CreateCommand

        'データベース接続を開く
        cnn00.Open()

        strSQL = "SELECT CODE "
        strSQL = strSQL & "FROM M_EXL_CS_MEMBER "
        strSQL = strSQL & "WHERE CODE = '" & Session("UsrId") & "' "

        'ＳＱＬコマンド作成
        dbcmd = New SqlCommand(strSQL, cnn00)
        'ＳＱＬ文実行
        dataread = dbcmd.ExecuteReader()
        strbkg = ""
        '結果を取り出す
        While (dataread.Read())
            fflg2 = dataread("CODE")
        End While

        'クローズ処理
        dataread.Close()
        dbcmd.Dispose()


        If e.Row.RowType = DataControlRowType.DataRow Then

            '仕向地
            If Len(e.Row.Cells(8).Text) > 7 Then
                e.Row.Cells(8).Font.Size = 7
            End If

            '仕向地
            If Len(e.Row.Cells(10).Text) > 20 Then
                e.Row.Cells(10).Font.Size = 7
            End If

            'IVNO
            If Len(e.Row.Cells(11).Text) > 19 Then
                e.Row.Cells(11).Font.Size = 7.5
            End If

            '船名
            If Len(e.Row.Cells(17).Text) > 19 Then
                e.Row.Cells(17).Font.Size = 7.5
            End If

            '荷受地
            If Len(e.Row.Cells(19).Text) > 5 Then
                e.Row.Cells(19).Font.Size = 7
            End If

            '積出港
            If Len(e.Row.Cells(20).Text) > 5 Then
                e.Row.Cells(20).Font.Size = 7
            End If

            '揚港
            If Len(e.Row.Cells(21).Text) > 16 Then
                e.Row.Cells(21).Font.Size = 7
            End If

            '配送先
            If Len(e.Row.Cells(22).Text) > 16 Then
                e.Row.Cells(22).Font.Size = 7
            End If

            '接続文字列の作成
            Dim ConnectionString As String = String.Empty

            'SQL Server認証
            ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"

            'SqlConnectionクラスの新しいインスタンスを初期化
            Dim cnn = New SqlConnection(ConnectionString)
            Dim Command = cnn.CreateCommand

            Dim dltlabel1 As Label = e.Row.FindControl("Label1")
            Dim dltlabel2 As Label = e.Row.FindControl("Label2")
            Dim btn01 As Button = e.Row.FindControl("Button1")
            Dim btn02 As Button = e.Row.FindControl("Button2")

            'データベース接続を開く
            cnn.Open()

            ''strSQL = "SELECT ITK_BKGNO FROM [T_EXL_CSWORKSTATUS] WHERE [T_EXL_CSWORKSTATUS].ITK_BKGNO = '" & Trim(e.Row.Cells(26).Text) & "' "
            'strSQL = "SELECT BKGNO FROM [T_EXL_WORKSTATUS00] WHERE [T_EXL_WORKSTATUS00].BKGNO = '" & Trim(Replace(e.Row.Cells(16).Text, vbLf, "")) & "' "
            'strSQL = strSQL & "AND [T_EXL_WORKSTATUS00].ID = '006' "

            ''ＳＱＬコマンド作成
            'dbcmd = New SqlCommand(strSQL, cnn)
            ''ＳＱＬ文実行
            'dataread = dbcmd.ExecuteReader()
            'strbkg = ""
            ''結果を取り出す
            'While (dataread.Read())
            '    strbkg += dataread("BKGNO")
            '    '書類作成状況
            '    If Trim(e.Row.Cells(16).Text) = Trim(strbkg) And strbkg <> "&nbsp;" Then
            '        'e.Row.BackColor = Drawing.Color.DarkGray

            '        Dim dltcb01 As CheckBox = e.Row.FindControl("cb01")

            '        dltcb01.Checked = True
            '        'dltcb02.Checked = True

            '    End If
            'End While

            ''クローズ処理
            'dataread.Close()
            'dbcmd.Dispose()


            ''strSQL = "SELECT ITK_BKGNO FROM [T_EXL_CSWORKSTATUS] WHERE [T_EXL_CSWORKSTATUS].ITK_BKGNO = '" & Trim(e.Row.Cells(26).Text) & "' "
            'strSQL = "SELECT BKGNO FROM [T_EXL_WORKSTATUS00] WHERE [T_EXL_WORKSTATUS00].BKGNO = '" & Trim(Replace(e.Row.Cells(16).Text, vbLf, "")) & "' "
            'strSQL = strSQL & "AND [T_EXL_WORKSTATUS00].ID = '007' "

            ''ＳＱＬコマンド作成
            'dbcmd = New SqlCommand(strSQL, cnn)
            ''ＳＱＬ文実行
            'dataread = dbcmd.ExecuteReader()
            'strbkg = ""
            ''結果を取り出す
            'While (dataread.Read())
            '    strbkg += dataread("BKGNO")
            '    '書類作成状況
            '    If Trim(e.Row.Cells(16).Text) = Trim(strbkg) And strbkg <> "&nbsp;" Then
            '        'e.Row.BackColor = Drawing.Color.DarkGray


            '        Dim dltcb02 As CheckBox = e.Row.FindControl("cb02")

            '        'dltcb01.Checked = True
            '        dltcb02.Checked = True

            '    End If
            'End While

            ''クローズ処理
            'dataread.Close()
            'dbcmd.Dispose()


            ''strSQL = "SELECT ITK_BKGNO FROM [T_EXL_CSWORKSTATUS] WHERE [T_EXL_CSWORKSTATUS].ITK_BKGNO = '" & Trim(e.Row.Cells(26).Text) & "' "
            'strSQL = "SELECT BKGNO FROM [T_EXL_WORKSTATUS00] WHERE [T_EXL_WORKSTATUS00].BKGNO = '" & Trim(Replace(e.Row.Cells(16).Text, vbLf, "")) & "' "
            'strSQL = strSQL & "AND [T_EXL_WORKSTATUS00].ID = '008' "
            'strSQL = strSQL & "AND [T_EXL_WORKSTATUS00].INVNO = '" & Trim(Replace(e.Row.Cells(30).Text, vbLf, "")) & "' "

            ''ＳＱＬコマンド作成
            'dbcmd = New SqlCommand(strSQL, cnn)
            ''ＳＱＬ文実行
            'dataread = dbcmd.ExecuteReader()
            'strbkg = ""
            ''結果を取り出す
            'While (dataread.Read())
            '    strbkg += dataread("BKGNO")
            '    '書類作成状況
            '    If Trim(e.Row.Cells(16).Text) = Trim(strbkg) And strbkg <> "&nbsp;" Then

            '        dltlabel1.Text = "〇"

            '    End If
            'End While

            ''クローズ処理
            'dataread.Close()
            'dbcmd.Dispose()


            ''strSQL = "SELECT ITK_BKGNO FROM [T_EXL_CSWORKSTATUS] WHERE [T_EXL_CSWORKSTATUS].ITK_BKGNO = '" & Trim(e.Row.Cells(26).Text) & "' "
            'strSQL = "SELECT BKGNO FROM [T_EXL_WORKSTATUS00] WHERE [T_EXL_WORKSTATUS00].BKGNO = '" & Trim(Replace(e.Row.Cells(16).Text, vbLf, "")) & "' "
            'strSQL = strSQL & "AND [T_EXL_WORKSTATUS00].ID = '009' "
            'strSQL = strSQL & "AND [T_EXL_WORKSTATUS00].INVNO = '" & Trim(Replace(e.Row.Cells(30).Text, vbLf, "")) & "' "


            ''ＳＱＬコマンド作成
            'dbcmd = New SqlCommand(strSQL, cnn)
            ''ＳＱＬ文実行
            'dataread = dbcmd.ExecuteReader()
            'strbkg = ""
            ''結果を取り出す
            'While (dataread.Read())
            '    strbkg += dataread("BKGNO")
            '    '書類作成状況
            '    If Trim(e.Row.Cells(16).Text) = Trim(strbkg) And strbkg <> "&nbsp;" Then

            '        dltlabel2.Text = "〇"

            '    End If
            'End While

            ''クローズ処理
            'dataread.Close()
            'dbcmd.Dispose()

            'If Trim(e.Row.Cells(14).Text) = "LCL" Then
            '    e.Row.Cells(14).BackColor = Drawing.Color.Orange
            'End If

            'If Trim(e.Row.Cells(29).Text) = "1" Then
            '    e.Row.BackColor = Drawing.Color.Orange
            'End If


            'strSQL = "SELECT ITK_BKGNO FROM [T_EXL_CSWORKSTATUS] WHERE [T_EXL_CSWORKSTATUS].ITK_BKGNO = '" & Trim(e.Row.Cells(26).Text) & "' "
            strSQL = "SELECT BKGNO FROM [T_EXL_WORKSTATUS00] WHERE [T_EXL_WORKSTATUS00].BKGNO = '" & Trim(Replace(e.Row.Cells(16).Text, vbLf, "")) & "' "
            strSQL = strSQL & "AND [T_EXL_WORKSTATUS00].ID = '010' "
            strSQL = strSQL & "AND [T_EXL_WORKSTATUS00].INVNO = '" & Trim(Replace(e.Row.Cells(30).Text, vbLf, "")) & "' "


            'ＳＱＬコマンド作成
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行
            dataread = dbcmd.ExecuteReader()
            strbkg = ""
            '結果を取り出す
            While (dataread.Read())
                strbkg += dataread("BKGNO")
                '書類作成状況
                If Trim(e.Row.Cells(16).Text) = Trim(strbkg) And strbkg <> "&nbsp;" Then


                    If dltlabel2.Text = "〇" Then
                        e.Row.ForeColor = Drawing.Color.White
                        e.Row.BackColor = Drawing.Color.Red
                        e.Row.Font.Bold = True
                    Else

                        'e.Row.Font.Strikeout = True
                        e.Row.BackColor = Drawing.Color.LightGray
                        'btn01.Text = "戻す"


                    End If

                End If
            End While

            'クローズ処理
            dataread.Close()
            dbcmd.Dispose()

            cnn.Close()
            cnn.Dispose()

            dltlabel1.Dispose()
            dltlabel2.Dispose()

            'Dim fll As String = "0"
            'Call get_meisai(Trim(Replace(e.Row.Cells(16).Text, vbLf, "")), fll)

            'If fll = "0" Then
            '    btn01.Enabled = False
            '    dltlabel1.Text = "明細未"

            'End If

        End If


        'ボタンに行数をセット
        If e.Row.RowType = DataControlRowType.DataRow Then

            Dim fflg As String = ""
            Dim kflg01 As String = ""

            '接続文字列の作成
            Dim ConnectionString As String = String.Empty

            'SQL Server認証
            ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"

            'SqlConnectionクラスの新しいインスタンスを初期化
            Dim cnn = New SqlConnection(ConnectionString)
            Dim Command = cnn.CreateCommand

            'データベース接続を開く
            cnn.Open()

            'strSQL = "SELECT T_EXL_CSKANRYO.FLG02 "
            'strSQL = strSQL & "FROM T_EXL_CSKANRYO "
            'strSQL = strSQL & "WHERE BOOKING_NO = '" & Trim(Replace(e.Row.Cells(16).Text, vbLf, "")) & "' "

            ''ＳＱＬコマンド作成
            'dbcmd = New SqlCommand(strSQL, cnn)
            ''ＳＱＬ文実行
            'dataread = dbcmd.ExecuteReader()
            'strbkg = ""
            ''結果を取り出す
            'While (dataread.Read())
            '    fflg = dataread("FLG02")
            'End While

            ''クローズ処理
            'dataread.Close()
            'dbcmd.Dispose()


            ''strSQL = "SELECT ITK_BKGNO FROM [T_EXL_CSWORKSTATUS] WHERE [T_EXL_CSWORKSTATUS].ITK_BKGNO = '" & Trim(e.Row.Cells(26).Text) & "' "
            'strSQL = "SELECT BKGNO FROM [T_EXL_WORKSTATUS00] WHERE [T_EXL_WORKSTATUS00].BKGNO = '" & Trim(Replace(e.Row.Cells(16).Text, vbLf, "")) & "' "
            'strSQL = strSQL & "AND [T_EXL_WORKSTATUS00].ID = '010' "
            'strSQL = strSQL & "AND [T_EXL_WORKSTATUS00].INVNO = '" & Trim(Replace(e.Row.Cells(30).Text, vbLf, "")) & "' "


            ''ＳＱＬコマンド作成
            'dbcmd = New SqlCommand(strSQL, cnn)
            ''ＳＱＬ文実行
            'dataread = dbcmd.ExecuteReader()
            'strbkg = ""
            ''結果を取り出す
            'While (dataread.Read())
            '    strbkg += dataread("BKGNO")
            '    '書類作成状況
            '    If Trim(e.Row.Cells(16).Text) = Trim(strbkg) And strbkg <> "&nbsp;" Then

            '        kflg01 = 1


            '    End If
            'End While

            ''クローズ処理
            'dataread.Close()
            'dbcmd.Dispose()

            cnn.Close()
            cnn.Dispose()

            Dim dltButton As Button = e.Row.FindControl("Button1")
            Dim dltButton2 As Button = e.Row.FindControl("Button2")
            Dim dltButton3 As Button = e.Row.FindControl("Button3")
            Dim dltButton4 As Button = e.Row.FindControl("Button4")
            Dim dltButton5 As Button = e.Row.FindControl("Button5")
            Dim dltButton6 As Button = e.Row.FindControl("Button6")
            Dim dltlabel1 As Label = e.Row.FindControl("Label1")
            Dim dltlabel2 As Label = e.Row.FindControl("Label2")

            'If dltlabel2.Text <> "〇" Then
            '    dltButton2.Visible = False
            '    ''e.Row.Cells(4).Text = "-"
            'Else
            '    dltButton2.Visible = True
            '    ''e.Row.Cells(4).Text = ""
            'End If

            'If kflg01 = "1" Then
            '    dltButton.Text = "戻す"
            'Else
            '    If Trim(e.Row.Cells(29).Text) = "1" Then
            '        dltButton.Text = "最終"
            '    Else
            '        dltButton.Text = "途中"
            '    End If
            'End If

            ''ボタンが存在する場合のみセット
            'If Not (dltButton Is Nothing) Then
            '    dltButton.CommandArgument = e.Row.RowIndex.ToString()
            'End If

            ''ボタンが存在する場合のみセット
            'If Not (dltButton2 Is Nothing) Then
            '    dltButton2.CommandArgument = e.Row.RowIndex.ToString()
            'End If
            ''ボタンが存在する場合のみセット
            'If Not (dltButton3 Is Nothing) Then
            '    dltButton3.CommandArgument = e.Row.RowIndex.ToString()
            'End If
            ''ボタンが存在する場合のみセット
            'If Not (dltButton6 Is Nothing) Then
            '    dltButton6.CommandArgument = e.Row.RowIndex.ToString()
            'End If
            ''ボタンが存在する場合のみセット
            'If Not (dltButton4 Is Nothing) Then
            '    dltButton4.CommandArgument = e.Row.RowIndex.ToString()

            '    If fflg = "" Then
            '        dltButton4.Text = "更新"
            '    Else
            '        dltButton4.Text = "戻す"
            '    End If
            'End If
            'If Not (dltButton5 Is Nothing) Then
            '    dltButton5.CommandArgument = e.Row.RowIndex.ToString()
            '    If e.Row.Cells(28).Text = "1" Then
            '    Else
            '        dltButton5.Visible = False
            '    End If

            'End If


            Dim dltcb01 As CheckBox = e.Row.FindControl("cb01")
            Dim dltcb02 As CheckBox = e.Row.FindControl("cb02")

            ''ボタンが存在する場合のみセット
            'If Not (dltcb01 Is Nothing) Then
            '    dltcb01.AutoPostBack = True
            '    If e.Row.Cells(24).Text <> "1" Then
            '        dltcb01.Visible = False
            '    End If
            'End If
            'If Not (dltcb02 Is Nothing) Then
            '    dltcb02.AutoPostBack = True
            '    If e.Row.Cells(23).Text <> "1" Then
            '        dltcb02.Visible = False
            '    End If
            'End If

            ''ボタンが存在する場合のみセット
            'If Not (dltcb01 Is Nothing) And Not (dltcb02 Is Nothing) Then
            '    dltcb01.AutoPostBack = True
            '    dltcb02.AutoPostBack = True
            '    If e.Row.Cells(24).Text <> "1" And e.Row.Cells(23).Text <> "1" Then
            '        dltcb01.Visible = True
            '        dltcb02.Visible = True
            '    End If
            'End If

            Dim ckfl00 As String = ""
            Dim ckfl01 As String = ""

            ''ボタンが存在する場合のみセット
            'If dltcb01.Visible = True Then
            '    If dltcb01.Checked = True Then
            '        ckfl00 = ""
            '    Else
            '        ckfl00 = "KD"
            '    End If
            'End If
            ''ボタンが存在する場合のみセット
            'If dltcb02.Visible = True Then
            '    If dltcb02.Checked = True Then
            '        ckfl00 = ""
            '    Else
            '        ckfl01 = "AM"
            '    End If
            'End If

            'If dltlabel1.Text = "〇" Then
            '    dltButton.Attributes.Add("onclick", "return confirm('完了報告を解除しますか？');")
            'Else
            '    If ckfl00 = "KD" And ckfl01 = "AM" Then
            '        dltButton.Attributes.Add("onclick", "return confirm('KD、ｱﾌﾀどちらもチェックがありませんが処理を進めますか？\r\n※ｱﾌﾀ、KD混載時は注意してください。');")
            '    ElseIf ckfl00 = "KD" And ckfl01 = "" Then
            '        dltButton.Attributes.Add("onclick", "return confirm('KDにチェックがありませんが処理を進めますか？\r\n※ｱﾌﾀ、KD混載時は注意してください。');")
            '    ElseIf ckfl00 = "" And ckfl01 = "AM" Then
            '        dltButton.Attributes.Add("onclick", "return confirm('ｱﾌﾀにチェックがありませんが処理を進めますか？\r\n※ｱﾌﾀ、KD混載時は注意してください。');")
            '    Else
            '        dltButton.Attributes.Add("onclick", "return confirm('チェックボックス確認\r\n※ｱﾌﾀ、KD混載時は注意してください。');")
            '    End If
            'End If

            'If e.Row.Cells(29).Text = "1" Then
            '    dltlabel1.Text = "最終"
            'Else
            '    dltlabel1.Text = "途中"
            'End If


            dltButton.Dispose()
            dltButton2.Dispose()
            dltButton3.Dispose()
            dltButton4.Dispose()
            dltButton5.Dispose()
            dltlabel1.Dispose()
            dltlabel2.Dispose()
            dltcb01.Dispose()
            dltcb02.Dispose()


        End If


        If fflg2 = "" Then

            e.Row.Cells(0).Visible = False
            e.Row.Cells(1).Visible = False
            e.Row.Cells(2).Visible = False
            e.Row.Cells(3).Visible = False

            e.Row.Cells(4).Visible = False
            e.Row.Cells(5).Visible = False
            e.Row.Cells(10).Visible = False
            e.Row.Cells(15).Visible = False
            e.Row.Cells(18).Visible = False
            e.Row.Cells(19).Visible = False
            e.Row.Cells(17).Visible = False
            e.Row.Cells(20).Visible = False
            e.Row.Cells(21).Visible = False
            e.Row.Cells(22).Visible = False

            e.Row.Cells(25).Visible = False

            e.Row.Cells(27).Visible = False
            e.Row.Cells(26).Visible = False
            e.Row.Cells(32).Visible = False


            GridView2.Width = 900
        Else


            e.Row.Cells(0).Visible = False
            e.Row.Cells(1).Visible = False
            e.Row.Cells(2).Visible = False
            e.Row.Cells(3).Visible = False

            e.Row.Cells(4).Visible = False
            e.Row.Cells(5).Visible = False
            e.Row.Cells(10).Visible = False
            e.Row.Cells(15).Visible = False
            e.Row.Cells(18).Visible = False
            e.Row.Cells(19).Visible = False
            e.Row.Cells(17).Visible = False
            e.Row.Cells(20).Visible = False
            e.Row.Cells(21).Visible = False
            e.Row.Cells(22).Visible = False

            e.Row.Cells(25).Visible = False

            e.Row.Cells(27).Visible = False
            e.Row.Cells(26).Visible = False
            e.Row.Cells(32).Visible = False

            GridView2.Width = 900
        End If



        '23 24 visible false
        e.Row.Cells(23).Visible = False
        e.Row.Cells(24).Visible = False
        e.Row.Cells(28).Visible = False

        e.Row.Cells(29).Visible = False
        e.Row.Cells(30).Visible = False
        e.Row.Cells(31).Visible = False

        cnn00.Close()
        cnn00.Dispose()



    End Sub

    Private Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        'GridViewのボタン押下処理
        If e.CommandName = "edt" Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim data1 = Me.GridView1.Rows(index).Cells(16).Text

            Dim data2 = Me.GridView1.Rows(index).Cells(6).Text
            Dim data3 = Me.GridView1.Rows(index).Cells(7).Text
            Dim data4 = Me.GridView1.Rows(index).Cells(9).Text
            Dim data5 = Me.GridView1.Rows(index).Cells(11).Text
            Dim data6 = Me.GridView1.Rows(index).Cells(14).Text
            Dim data7 = Me.GridView1.Rows(index).Cells(13).Text
            Dim data8 = Me.GridView1.Rows(index).Cells(30).Text
            Dim data9 = Me.GridView1.Rows(index).Cells(29).Text
            Dim data10 = Me.GridView1.Rows(index).Cells(31).Text



            ''添付ファイルをサーバーにアップロード
            ''Dim posted As HttpPostedFile = Request.Files("userfile")
            'Dim posted As HttpPostedFile = userfile00.PostedFile

            'If Not posted.FileName = "" Then
            '    posted.SaveAs("C:\exp\cs_home\upload\" & System.IO.Path.GetFileName(posted.FileName))
            '    Session("strFile") = System.IO.Path.GetFileName(posted.FileName)
            'End If


            Dim dataread As SqlDataReader
            Dim dbcmd As SqlCommand
            Dim strSQL As String = ""
            Dim strDate As String = ""
            Dim strinv As String = ""
            Dim eflg As Long
            Dim strcst As String = ""
            Dim FLGval As Long
            Dim a As Long
            Dim dflg As Long

            Dim ck As String = ""

            Dim dt00 As DateTime = DateTime.Now
            'Dim dt01 As DateTime

            'If Me.GridView1.Rows(index).Cells(7).Text = "手動登録" Then
            'Else
            '    dt01 = Me.GridView1.Rows(index).Cells(6).Text + " " + Me.GridView1.Rows(index).Cells(7).Text

            '    If dt00 < dt01 Then
            '    End If
            'End If


            'データの削除 ｴﾗｰの有無

            Call DEL_KANRYOERROR()

            FLGval = 0

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

            Dim cflg As Long

            'データベース接続を開く
            cnn.Open()


            strSQL = "SELECT DISTINCT T_INV_HD_TB.OLD_INVNO, T_INV_HD_TB.CUSTCODE "
            strSQL = strSQL & "FROM T_INV_HD_TB LEFT JOIN T_INV_BD_TB ON T_INV_HD_TB.INVOICENO = T_INV_BD_TB.INVOICENO "
            strSQL = strSQL & "WHERE T_INV_HD_TB.BLDATE BETWEEN '" & dt3 & "' AND '" & dt2 & "' "

            strSQL = strSQL & "GROUP BY T_INV_HD_TB.BOOKINGNO, T_INV_HD_TB.OLD_INVNO,T_INV_HD_TB.CRYCD, T_INV_HD_TB.SHIPPEDPER, T_INV_HD_TB.VOYAGENO, T_INV_HD_TB.IOPORTDATE, T_INV_HD_TB.CUTDATE,T_INV_HD_TB.CUSTCODE,T_INV_HD_TB.HEADTITLE "
            strSQL = strSQL & "HAVING T_INV_HD_TB.BOOKINGNO like '%" & data1 & "%' "
            strSQL = strSQL & "AND Sum(T_INV_BD_TB.QTY) >= 1 "
            strSQL = strSQL & "AND Sum(T_INV_BD_TB.KIN) >= 1 "
            strSQL = strSQL & "AND T_INV_HD_TB.HEADTITLE like 'INVOICE%' "
            strSQL = strSQL & "Order by T_INV_HD_TB.OLD_INVNO "


            'ＳＱＬコマンド作成 
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行 
            dataread = dbcmd.ExecuteReader()


            '結果を取り出す 
            While (dataread.Read())
                'インボイス番号をキーにデータを更新
                If cflg = 0 Then
                    Call reg_check3(Trim(data1), 3, dflg, data8, data9)
                    Call check001(Trim(data1))
                    Call docexck(Trim(data1), Trim(dataread("OLD_INVNO")))
                    cflg = 1
                Else
                End If

                FLGval = 1

            End While

            'クローズ処理 
            dataread.Close()
            dbcmd.Dispose()
            cnn.Close()
            cnn.Dispose()


            If FLGval = 0 Then
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "確認", "<script language='JavaScript'>confirm('未登録です。');</script>", False)

            Else

                'エラーの抽出　エラーがあればエラー通知メールを送付 エラーの項目に追加・削除
                If dflg = 1 Then '取消時はチェックは無しで強制的にチェックの登録を解除


                Else
                    If FLGval = 0 Then 'データがなければ
                    Else


                        If data9 = 1 Then
                            Call erreg(a, data1, data8)
                            Call Mail01(data1, data2, data3, data4, data5, data6, data7, data10, a) 'kkkkkk
                        Else
                            Call Mail03(data1, data2, data3, data4, data5, data6, data7, data10)
                        End If


                    End If
                End If

                If a > 0 Then
                    If data9 = 1 Then
                        Call Mail02(data1, data2, data3, data4, data5, data6, data7, data10)
                    Else
                    End If

                Else
                    'ｴｸｾﾙ出力
                    If dflg = 1 Then '取消時はチェックは無しで強制的にチェックの登録を解除
                    Else
                        'バンニングのみの場合は処理しない＿＿＿X
                        If data9 = 1 Then
                            Call EXELE(data1, data2, data3, data4, data5, data6, data7)
                        Else
                        End If

                    End If
                End If

                If dflg = 1 Then '取消時はチェックは無しで強制的にチェックの登録を解除
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "確認", "<script language='JavaScript'>confirm('解除しました。');</script>", False)
                Else
                    'バンニングのみの場合は処理しない＿＿＿X
                    If data9 = 1 Then
                        Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "確認", "<script language='JavaScript'>confirm('＜最終バン＞処理が完了しました。');</script>", False)
                    Else
                        Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "確認", "<script language='JavaScript'>confirm('＜途中バン＞処理が完了しました。');</script>", False)
                    End If
                End If
            End If


        ElseIf e.CommandName = "edt2" Then



            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim data1 = Me.GridView1.Rows(index).Cells(16).Text
            Dim data2 = Me.GridView1.Rows(index).Cells(6).Text
            Dim data3 = Me.GridView1.Rows(index).Cells(7).Text
            Dim data4 = Me.GridView1.Rows(index).Cells(9).Text
            Dim data5 = Me.GridView1.Rows(index).Cells(11).Text
            Dim data6 = Me.GridView1.Rows(index).Cells(14).Text
            Dim data7 = Me.GridView1.Rows(index).Cells(13).Text
            Dim data8 = Me.GridView1.Rows(index).Cells(30).Text
            Dim data10 = Me.GridView1.Rows(index).Cells(31).Text

            Dim dataread As SqlDataReader
            Dim dbcmd As SqlCommand
            Dim strSQL As String = ""
            Dim strDate As String = ""
            Dim strinv As String = ""
            Dim eflg As Long
            Dim strcst As String = ""
            Dim FLGval As Long
            Dim a As Long
            Dim cflg As Long
            Dim ck As String = ""

            FLGval = 0

            'データの削除 
            Call DEL_KANRYOERROR()

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


            strSQL = "SELECT DISTINCT T_INV_HD_TB.OLD_INVNO, T_INV_HD_TB.CUSTCODE "
            strSQL = strSQL & "FROM T_INV_HD_TB LEFT JOIN T_INV_BD_TB ON T_INV_HD_TB.INVOICENO = T_INV_BD_TB.INVOICENO "
            strSQL = strSQL & "WHERE T_INV_HD_TB.BLDATE BETWEEN '" & dt3 & "' AND '" & dt2 & "' "

            strSQL = strSQL & "GROUP BY T_INV_HD_TB.BOOKINGNO, T_INV_HD_TB.OLD_INVNO,T_INV_HD_TB.CRYCD, T_INV_HD_TB.SHIPPEDPER, T_INV_HD_TB.VOYAGENO, T_INV_HD_TB.IOPORTDATE, T_INV_HD_TB.CUTDATE,T_INV_HD_TB.CUSTCODE,T_INV_HD_TB.HEADTITLE "
            strSQL = strSQL & "HAVING T_INV_HD_TB.BOOKINGNO like '%" & data1 & "%' "
            strSQL = strSQL & "AND Sum(T_INV_BD_TB.QTY) >= 1 "
            strSQL = strSQL & "AND Sum(T_INV_BD_TB.KIN) >= 1 "
            strSQL = strSQL & "AND T_INV_HD_TB.HEADTITLE like 'INVOICE%' "
            strSQL = strSQL & "Order by T_INV_HD_TB.OLD_INVNO "


            'ＳＱＬコマンド作成 
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行 
            dataread = dbcmd.ExecuteReader()


            '結果を取り出す 
            While (dataread.Read())
                'インボイス番号をキーにデータを更新
                If cflg = 0 Then
                    Call check001(Trim(data1))
                    cflg = 1
                Else
                End If

            End While

            'クローズ処理 
            dataread.Close()
            dbcmd.Dispose()
            cnn.Close()
            cnn.Dispose()

            'エラーの抽出　エラーがあればエラー通知メールを送付 エラーの項目に追加・削除
            Call erreg(a, data1, data8)


            If a > 0 Then
                Call Mail02(data1, data2, data3, data4, data5, data6, data7, data10)
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "確認", "<script language='JavaScript'>confirm('エラーが解消されていません。');</script>", False)
            Else
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "確認", "<script language='JavaScript'>confirm('エラーがなくなりました。');</script>", False)

                Call EXELE(data1, data2, data3, data4, data5, data6, data7)


            End If

            Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "確認", "<script language='JavaScript'>confirm('処理が完了しました。');</script>", False)



        ElseIf e.CommandName = "edt3" Then


            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim data1 = Me.GridView1.Rows(index).Cells(16).Text
            Dim data2 = Me.GridView1.Rows(index).Cells(6).Text
            Dim data3 = Me.GridView1.Rows(index).Cells(7).Text
            Dim data4 = Me.GridView1.Rows(index).Cells(9).Text
            Dim data5 = Me.GridView1.Rows(index).Cells(11).Text
            Dim data6 = Me.GridView1.Rows(index).Cells(14).Text
            Dim data7 = Me.GridView1.Rows(index).Cells(13).Text
            Dim data8 = Me.GridView1.Rows(index).Cells(30).Text
            Dim data10 = Me.GridView1.Rows(index).Cells(31).Text


            Dim dataread As SqlDataReader
            Dim dbcmd As SqlCommand
            Dim strSQL As String = ""
            Dim strDate As String = ""
            Dim strinv As String = ""
            Dim eflg As Long
            Dim strcst As String = ""
            Dim FLGval As Long
            Dim a As Long
            Dim cflg As Long
            Dim ck As String = ""

            FLGval = 0

            'データの削除 
            Call DEL_KANRYOERROR()

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


            strSQL = "SELECT DISTINCT T_INV_HD_TB.OLD_INVNO, T_INV_HD_TB.CUSTCODE "
            strSQL = strSQL & "FROM T_INV_HD_TB LEFT JOIN T_INV_BD_TB ON T_INV_HD_TB.INVOICENO = T_INV_BD_TB.INVOICENO "
            strSQL = strSQL & "WHERE T_INV_HD_TB.BLDATE BETWEEN '" & dt3 & "' AND '" & dt2 & "' "

            strSQL = strSQL & "GROUP BY T_INV_HD_TB.BOOKINGNO, T_INV_HD_TB.OLD_INVNO,T_INV_HD_TB.CRYCD, T_INV_HD_TB.SHIPPEDPER, T_INV_HD_TB.VOYAGENO, T_INV_HD_TB.IOPORTDATE, T_INV_HD_TB.CUTDATE,T_INV_HD_TB.CUSTCODE,T_INV_HD_TB.HEADTITLE "
            strSQL = strSQL & "HAVING T_INV_HD_TB.BOOKINGNO like '%" & data1 & "%' "
            strSQL = strSQL & "AND Sum(T_INV_BD_TB.QTY) >= 1 "
            strSQL = strSQL & "AND Sum(T_INV_BD_TB.KIN) >= 1 "
            strSQL = strSQL & "AND T_INV_HD_TB.HEADTITLE like 'INVOICE%' "
            strSQL = strSQL & "Order by T_INV_HD_TB.OLD_INVNO "


            'ＳＱＬコマンド作成 
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行 
            dataread = dbcmd.ExecuteReader()


            '結果を取り出す 
            While (dataread.Read())
                'インボイス番号をキーにデータを更新
                If cflg = 0 Then
                    'Call check001(Trim(data1))
                    cflg = 1
                Else
                End If

            End While

            'クローズ処理 
            dataread.Close()
            dbcmd.Dispose()
            cnn.Close()
            cnn.Dispose()

            'エラーの抽出　エラーがあればエラー通知メールを送付 エラーの項目に追加・削除
            Call erreg(a, data1, data8)


            If a > 0 Then
                Call Mail02(data1, data2, data3, data4, data5, data6, data7, data10)
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "確認", "<script language='JavaScript'>confirm('エラーが解消されていません。');</script>", False)
            Else
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "確認", "<script language='JavaScript'>confirm('エラーを強制的に解除します。。');</script>", False)

                Call EXELE(data1, data2, data3, data4, data5, data6, data7)


            End If


        ElseIf e.CommandName = "edt4" Then


            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim data1 = Me.GridView1.Rows(index).Cells(16).Text
            Dim data2 = Me.GridView1.Rows(index).Cells(6).Text
            Dim data3 = Me.GridView1.Rows(index).Cells(7).Text
            Dim data4 = Me.GridView1.Rows(index).Cells(9).Text
            Dim data5 = Me.GridView1.Rows(index).Cells(11).Text
            Dim data6 = Me.GridView1.Rows(index).Cells(14).Text
            Dim data7 = Me.GridView1.Rows(index).Cells(13).Text

            Dim fflg As Long
            Dim cflg As String = ""

            Dim dataread As SqlDataReader
            Dim dbcmd As SqlCommand

            fflg = 0


            Dim strSQL As String
            Dim dtNow As DateTime = DateTime.Now
            Dim strFlg As String = ""

            Dim dltcb01 As CheckBox = Me.GridView1.Rows(index).FindControl("cb01")
            Dim dltcb02 As CheckBox = Me.GridView1.Rows(index).FindControl("cb02")
            Dim dltButton4 As Button = Me.GridView1.Rows(index).FindControl("Button4")

            '接続文字列の作成
            Dim ConnectionString As String = String.Empty
            'SQL Server認証
            ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
            'SqlConnectionクラスの新しいインスタンスを初期化
            Dim cnn = New SqlConnection(ConnectionString)
            Dim Command = cnn.CreateCommand

            'データベース接続を開く
            cnn.Open()



            strSQL = "SELECT T_EXL_CSKANRYO.FLG02 "
            strSQL = strSQL & "FROM T_EXL_CSKANRYO "
            strSQL = strSQL & "WHERE BOOKING_NO = '" & data1 & "' "

            'ＳＱＬコマンド作成 
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行 
            dataread = dbcmd.ExecuteReader()


            '結果を取り出す 
            While (dataread.Read())
                'インボイス番号をキーにデータを更新
                cflg = dataread("FLG02")
            End While

            'クローズ処理 
            dataread.Close()
            dbcmd.Dispose()

            If cflg = "" Then

                If dltcb01.Visible = True And dltcb02.Visible = True Then
                    fflg = 1
                ElseIf dltcb01.Visible = False And dltcb02.Visible = True Then
                    fflg = 2
                ElseIf dltcb01.Visible = True And dltcb02.Visible = False Then
                    fflg = 3
                ElseIf dltcb01.Visible = False And dltcb02.Visible = False Then
                    fflg = 4
                End If

                strSQL = ""
                strSQL = strSQL & "UPDATE T_EXL_CSKANRYO SET "
                strSQL = strSQL & "FLG02 = '" & fflg & "' "
                strSQL = strSQL & "WHERE BOOKING_NO = '" & data1 & "' "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

                strSQL = ""
                strSQL = strSQL & "UPDATE T_EXL_CSKANRYO SET "
                strSQL = strSQL & "FLG04 = '1', "
                strSQL = strSQL & "FLG05 = '1' "
                strSQL = strSQL & "WHERE BOOKING_NO = '" & data1 & "' "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

                cnn.Close()
                cnn.Dispose()

                Me.GridView1.Rows(index).Cells(23).Text = "1"
                Me.GridView1.Rows(index).Cells(24).Text = "1"

                Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "確認", "<script language='JavaScript'>confirm('KD,ｱﾌﾀともにチェックボックスを表示しました。');</script>", False)


            Else

                If cflg = "1" Then

                    dltcb01.Visible = True
                    dltcb02.Visible = True

                    strSQL = ""
                    strSQL = strSQL & "UPDATE T_EXL_CSKANRYO SET "
                    strSQL = strSQL & "FLG02 = '' "
                    strSQL = strSQL & "WHERE BOOKING_NO = '" & data1 & "' "

                    Command.CommandText = strSQL
                    ' SQLの実行
                    Command.ExecuteNonQuery()

                    strSQL = ""
                    strSQL = strSQL & "UPDATE T_EXL_CSKANRYO SET "
                    strSQL = strSQL & "FLG04 = '1', "
                    strSQL = strSQL & "FLG05 = '1' "
                    strSQL = strSQL & "WHERE BOOKING_NO = '" & data1 & "' "

                    Command.CommandText = strSQL
                    ' SQLの実行
                    Command.ExecuteNonQuery()

                    cnn.Close()
                    cnn.Dispose()

                    Me.GridView1.Rows(index).Cells(23).Text = "1"
                    Me.GridView1.Rows(index).Cells(24).Text = "1"

                ElseIf cflg = "2" Then

                    dltcb01.Visible = True
                    dltcb02.Visible = False

                    strSQL = ""
                    strSQL = strSQL & "UPDATE T_EXL_CSKANRYO SET "
                    strSQL = strSQL & "FLG02 = '' "
                    strSQL = strSQL & "WHERE BOOKING_NO = '" & data1 & "' "

                    Command.CommandText = strSQL
                    ' SQLの実行
                    Command.ExecuteNonQuery()

                    strSQL = ""
                    strSQL = strSQL & "UPDATE T_EXL_CSKANRYO SET "
                    strSQL = strSQL & "FLG04 = '1', "
                    strSQL = strSQL & "FLG05 = '0' "
                    strSQL = strSQL & "WHERE BOOKING_NO = '" & data1 & "' "

                    Command.CommandText = strSQL
                    ' SQLの実行
                    Command.ExecuteNonQuery()

                    cnn.Close()
                    cnn.Dispose()

                    Me.GridView1.Rows(index).Cells(23).Text = "0"
                    Me.GridView1.Rows(index).Cells(24).Text = "1"

                ElseIf cflg = "3" Then

                    dltcb01.Visible = False
                    dltcb02.Visible = True

                    strSQL = ""
                    strSQL = strSQL & "UPDATE T_EXL_CSKANRYO SET "
                    strSQL = strSQL & "FLG02 = '' "
                    strSQL = strSQL & "WHERE BOOKING_NO = '" & data1 & "' "

                    Command.CommandText = strSQL
                    ' SQLの実行
                    Command.ExecuteNonQuery()

                    strSQL = ""
                    strSQL = strSQL & "UPDATE T_EXL_CSKANRYO SET "
                    strSQL = strSQL & "FLG04 = '0', "
                    strSQL = strSQL & "FLG05 = '1' "
                    strSQL = strSQL & "WHERE BOOKING_NO = '" & data1 & "' "

                    Command.CommandText = strSQL
                    ' SQLの実行
                    Command.ExecuteNonQuery()

                    cnn.Close()
                    cnn.Dispose()

                    Me.GridView1.Rows(index).Cells(23).Text = "1"
                    Me.GridView1.Rows(index).Cells(24).Text = "0"
                ElseIf cflg = "4" Then

                    dltcb01.Visible = False
                    dltcb02.Visible = False

                    strSQL = ""
                    strSQL = strSQL & "UPDATE T_EXL_CSKANRYO SET "
                    strSQL = strSQL & "FLG02 = '' "
                    strSQL = strSQL & "WHERE BOOKING_NO = '" & data1 & "' "

                    Command.CommandText = strSQL
                    ' SQLの実行
                    Command.ExecuteNonQuery()

                    strSQL = ""
                    strSQL = strSQL & "UPDATE T_EXL_CSKANRYO SET "
                    strSQL = strSQL & "FLG04 = '0', "
                    strSQL = strSQL & "FLG05 = '0' "
                    strSQL = strSQL & "WHERE BOOKING_NO = '" & data1 & "' "

                    Command.CommandText = strSQL
                    ' SQLの実行
                    Command.ExecuteNonQuery()

                    cnn.Close()
                    cnn.Dispose()

                    'GRIDVIEWの作成時になくなる？
                    Me.GridView1.Rows(index).Cells(23).Text = "0"
                    Me.GridView1.Rows(index).Cells(24).Text = "0"

                End If

                Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "確認", "<script language='JavaScript'>confirm('チェックボックスを初期状態に戻しました。');</script>", False)

                dltButton4.Dispose()
                dltcb01.Dispose()
                dltcb02.Dispose()

            End If
            Command.Dispose()

        ElseIf e.CommandName = "edt5" Then


            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim data1 = Me.GridView1.Rows(index).Cells(16).Text
            Dim data2 = Me.GridView1.Rows(index).Cells(6).Text
            Dim data3 = Me.GridView1.Rows(index).Cells(7).Text
            Dim data4 = Me.GridView1.Rows(index).Cells(9).Text
            Dim data5 = Me.GridView1.Rows(index).Cells(11).Text
            Dim data6 = Me.GridView1.Rows(index).Cells(14).Text
            Dim data7 = Me.GridView1.Rows(index).Cells(13).Text
            Dim data8 = Me.GridView1.Rows(index).Cells(30).Text
            Dim data9 = Me.GridView1.Rows(index).Cells(29).Text
            Dim data10 = Me.GridView1.Rows(index).Cells(31).Text

            Dim dataread As SqlDataReader
            Dim dbcmd As SqlCommand
            Dim strSQL As String = ""
            Dim strDate As String = ""
            Dim strinv As String = ""
            Dim eflg As Long
            Dim strcst As String = ""
            Dim FLGval As Long
            Dim a As Long
            Dim cflg As Long

            FLGval = 0


            '接続文字列の作成
            Dim ConnectionString As String = String.Empty
            'SQL Server認証
            ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
            'SqlConnectionクラスの新しいインスタンスを初期化
            Dim cnn = New SqlConnection(ConnectionString)

            Dim dt1 As DateTime = DateTime.Now

            Dim ts1 As New TimeSpan(100, 0, 0, 0)
            Dim ts2 As New TimeSpan(100, 0, 0, 0)
            Dim dt2 As DateTime = dt1 + ts1
            Dim dt3 As DateTime = dt1 - ts1

            'データベース接続を開く
            cnn.Open()
            Dim Command = cnn.CreateCommand

            strSQL = ""
            strSQL = strSQL & "DELETE FROM T_EXL_CSKANRYO "
            strSQL = strSQL & "WHERE T_EXL_CSKANRYO.INVOICE = '" & data5 & "' "
            strSQL = strSQL & "AND T_EXL_CSKANRYO.BOOKING_NO = '" & data1 & "' "
            strSQL = strSQL & "AND T_EXL_CSKANRYO.DAY09 = '" & data8 & "' "

            Command.CommandText = strSQL
            ' SQLの実行
            Command.ExecuteNonQuery()

            cnn.Close()
            cnn.Dispose()

            Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "確認", "<script language='JavaScript'>confirm('案件を削除しました。');</script>", False)
            Command.Dispose()

        ElseIf e.CommandName = "edt6" Then


            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim data1 = Me.GridView1.Rows(index).Cells(16).Text
            Dim data2 = Me.GridView1.Rows(index).Cells(6).Text
            Dim data3 = Me.GridView1.Rows(index).Cells(7).Text
            Dim data4 = Me.GridView1.Rows(index).Cells(9).Text
            Dim data5 = Me.GridView1.Rows(index).Cells(11).Text
            Dim data6 = Me.GridView1.Rows(index).Cells(14).Text
            Dim data7 = Me.GridView1.Rows(index).Cells(13).Text
            Dim data8 = Me.GridView1.Rows(index).Cells(30).Text
            Dim data9 = Me.GridView1.Rows(index).Cells(29).Text
            Dim data10 = Me.GridView1.Rows(index).Cells(31).Text



            Dim dataread As SqlDataReader
            Dim dbcmd As SqlCommand
            Dim strSQL As String = ""
            Dim strDate As String = ""
            Dim strinv As String = ""
            Dim eflg As Long
            Dim strcst As String = ""
            Dim FLGval As Long
            Dim a As Long
            Dim cflg As Long
            Dim cntnum As Long

            FLGval = 0


            '接続文字列の作成
            Dim ConnectionString As String = String.Empty
            'SQL Server認証
            ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
            'SqlConnectionクラスの新しいインスタンスを初期化
            Dim cnn = New SqlConnection(ConnectionString)


            Dim dt1 As DateTime = DateTime.Now

            Dim ts1 As New TimeSpan(100, 0, 0, 0)
            Dim ts2 As New TimeSpan(100, 0, 0, 0)
            Dim dt2 As DateTime = dt1 + ts1
            Dim dt3 As DateTime = dt1 - ts1

            'データベース接続を開く
            cnn.Open()
            Dim Command = cnn.CreateCommand

            If data9 = "1" Then
                data9 = "0"
            Else
                data9 = "1"

                strSQL = "SELECT count(T_EXL_CSKANRYO.BOOKING_NO) as cnt01 "
                strSQL = strSQL & "FROM T_EXL_CSKANRYO "
                strSQL = strSQL & "WHERE BOOKING_NO = '" & data1 & "' "
                strSQL = strSQL & "AND DAY10 = '1' "

                'ＳＱＬコマンド作成 
                dbcmd = New SqlCommand(strSQL, cnn)
                'ＳＱＬ文実行 
                dataread = dbcmd.ExecuteReader()


                '結果を取り出す 
                While (dataread.Read())
                    'インボイス番号をキーにデータを更新
                    cntnum = dataread("cnt01")
                End While

                'クローズ処理 
                dataread.Close()
                dbcmd.Dispose()

                If cntnum > 0 Then
                    data9 = "0"
                End If

            End If

            strSQL = ""
            strSQL = strSQL & "UPDATE T_EXL_CSKANRYO SET "
            strSQL = strSQL & "DAY10 = '" & data9 & "' "
            strSQL = strSQL & "WHERE DAY09 = '" & data8 & "' "
            strSQL = strSQL & "AND BOOKING_NO = '" & data1 & "' "

            Command.CommandText = strSQL
            ' SQLの実行
            Command.ExecuteNonQuery()

            cnn.Close()
                cnn.Dispose()

                If cntnum > 0 Then
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "確認", "<script language='JavaScript'>confirm('既に最終データが存在するため処理を中止します。');</script>", False)
                Else
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "確認", "<script language='JavaScript'>confirm('最終⇔途中を変更します。');</script>", False)
                End If

                Command.Dispose()

            End If

            'Grid再表示
            GridView1.DataBind()

    End Sub

    Private Sub GridView2_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView2.RowCommand
        'GridViewのボタン押下処理
        If e.CommandName = "edt" Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim data1 = Me.GridView2.Rows(index).Cells(16).Text

            Dim data2 = Me.GridView2.Rows(index).Cells(6).Text
            Dim data3 = Me.GridView2.Rows(index).Cells(7).Text
            Dim data4 = Me.GridView2.Rows(index).Cells(9).Text
            Dim data5 = Me.GridView2.Rows(index).Cells(11).Text
            Dim data6 = Me.GridView2.Rows(index).Cells(14).Text
            Dim data7 = Me.GridView2.Rows(index).Cells(13).Text
            Dim data8 = Me.GridView2.Rows(index).Cells(30).Text
            Dim data9 = Me.GridView2.Rows(index).Cells(29).Text
            Dim data10 = Me.GridView2.Rows(index).Cells(31).Text



            Dim dataread As SqlDataReader
            Dim dbcmd As SqlCommand
            Dim strSQL As String = ""
            Dim strDate As String = ""
            Dim strinv As String = ""
            Dim eflg As Long
            Dim strcst As String = ""
            Dim FLGval As Long
            Dim a As Long
            Dim dflg As Long
            Dim ck As String = ""

            Dim dt00 As DateTime = DateTime.Now
            'Dim dt01 As DateTime

            'If Me.GridView1.Rows(index).Cells(7).Text = "手動登録" Then
            'Else
            '    dt01 = Me.GridView1.Rows(index).Cells(6).Text + " " + Me.GridView1.Rows(index).Cells(7).Text

            '    If dt00 < dt01 Then
            '    End If
            'End If


            'データの削除 ｴﾗｰの有無

            Call DEL_KANRYOERROR()

            FLGval = 0

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

            Dim cflg As Long

            'データベース接続を開く
            cnn.Open()


            strSQL = "SELECT DISTINCT T_INV_HD_TB.OLD_INVNO, T_INV_HD_TB.CUSTCODE "
            strSQL = strSQL & "FROM T_INV_HD_TB LEFT JOIN T_INV_BD_TB ON T_INV_HD_TB.INVOICENO = T_INV_BD_TB.INVOICENO "
            strSQL = strSQL & "WHERE T_INV_HD_TB.BLDATE BETWEEN '" & dt3 & "' AND '" & dt2 & "' "

            strSQL = strSQL & "GROUP BY T_INV_HD_TB.BOOKINGNO, T_INV_HD_TB.OLD_INVNO,T_INV_HD_TB.CRYCD, T_INV_HD_TB.SHIPPEDPER, T_INV_HD_TB.VOYAGENO, T_INV_HD_TB.IOPORTDATE, T_INV_HD_TB.CUTDATE,T_INV_HD_TB.CUSTCODE,T_INV_HD_TB.HEADTITLE "
            strSQL = strSQL & "HAVING T_INV_HD_TB.BOOKINGNO like '%" & data1 & "%' "
            strSQL = strSQL & "AND Sum(T_INV_BD_TB.QTY) >= 1 "
            strSQL = strSQL & "AND Sum(T_INV_BD_TB.KIN) >= 1 "
            strSQL = strSQL & "AND T_INV_HD_TB.HEADTITLE like 'INVOICE%' "
            strSQL = strSQL & "Order by T_INV_HD_TB.OLD_INVNO "


            'ＳＱＬコマンド作成 
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行 
            dataread = dbcmd.ExecuteReader()


            '結果を取り出す 
            While (dataread.Read())
                'インボイス番号をキーにデータを更新
                If cflg = 0 Then
                    Call reg_check3(Trim(data1), 3, dflg, data8, data9)
                    Call check001(Trim(data1))
                    cflg = 1
                Else
                End If

                FLGval = 1

            End While

            'クローズ処理 
            dataread.Close()
            dbcmd.Dispose()
            cnn.Close()
            cnn.Dispose()


            If FLGval = 0 Then
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "確認", "<script language='JavaScript'>confirm('未登録です。');</script>", False)

            Else

                'エラーの抽出　エラーがあればエラー通知メールを送付 エラーの項目に追加・削除
                If dflg = 1 Then '取消時はチェックは無しで強制的にチェックの登録を解除


                Else
                    If FLGval = 0 Then 'データがなければ
                    Else


                        If data9 = 1 Then
                            Call erreg(a, data1, data8)
                            Call Mail01(data1, data2, data3, data4, data5, data6, data7, data10, a) 'Kkkkkkk
                        Else
                            Call Mail03(data1, data2, data3, data4, data5, data6, data7, data10)
                        End If


                    End If
                End If

                If a > 0 Then
                    If data9 = 1 Then
                        Call Mail02(data1, data2, data3, data4, data5, data6, data7, data10)
                    Else
                    End If

                Else
                    'ｴｸｾﾙ出力
                    If dflg = 1 Then '取消時はチェックは無しで強制的にチェックの登録を解除
                    Else

                        'バンニングのみの場合は処理しない＿＿＿X
                        If data9 = 1 Then

                            Call EXELE(data1, data2, data3, data4, data5, data6, data7)


                        Else
                        End If

                    End If
                End If

                If dflg = 1 Then '取消時はチェックは無しで強制的にチェックの登録を解除
                    Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "確認", "<script language='JavaScript'>confirm('解除しました。');</script>", False)
                Else
                    'バンニングのみの場合は処理しない＿＿＿X
                    If data9 = 1 Then
                        Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "確認", "<script language='JavaScript'>confirm('＜最終バン＞処理が完了しました。');</script>", False)
                    Else
                        Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "確認", "<script language='JavaScript'>confirm('＜途中バン＞処理が完了しました。');</script>", False)
                    End If
                End If
            End If


        ElseIf e.CommandName = "edt2" Then



            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim data1 = Me.GridView2.Rows(index).Cells(16).Text
            Dim data2 = Me.GridView2.Rows(index).Cells(6).Text
            Dim data3 = Me.GridView2.Rows(index).Cells(7).Text
            Dim data4 = Me.GridView2.Rows(index).Cells(9).Text
            Dim data5 = Me.GridView2.Rows(index).Cells(11).Text
            Dim data6 = Me.GridView2.Rows(index).Cells(14).Text
            Dim data7 = Me.GridView2.Rows(index).Cells(13).Text
            Dim data8 = Me.GridView2.Rows(index).Cells(30).Text
            Dim data10 = Me.GridView2.Rows(index).Cells(31).Text

            Dim dataread As SqlDataReader
            Dim dbcmd As SqlCommand
            Dim strSQL As String = ""
            Dim strDate As String = ""
            Dim strinv As String = ""
            Dim eflg As Long
            Dim strcst As String = ""
            Dim FLGval As Long
            Dim a As Long
            Dim cflg As Long
            Dim ck As String = ""

            FLGval = 0

            'データの削除 
            Call DEL_KANRYOERROR()

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


            strSQL = "SELECT DISTINCT T_INV_HD_TB.OLD_INVNO, T_INV_HD_TB.CUSTCODE "
            strSQL = strSQL & "FROM T_INV_HD_TB LEFT JOIN T_INV_BD_TB ON T_INV_HD_TB.INVOICENO = T_INV_BD_TB.INVOICENO "
            strSQL = strSQL & "WHERE T_INV_HD_TB.BLDATE BETWEEN '" & dt3 & "' AND '" & dt2 & "' "

            strSQL = strSQL & "GROUP BY T_INV_HD_TB.BOOKINGNO, T_INV_HD_TB.OLD_INVNO,T_INV_HD_TB.CRYCD, T_INV_HD_TB.SHIPPEDPER, T_INV_HD_TB.VOYAGENO, T_INV_HD_TB.IOPORTDATE, T_INV_HD_TB.CUTDATE,T_INV_HD_TB.CUSTCODE,T_INV_HD_TB.HEADTITLE "
            strSQL = strSQL & "HAVING T_INV_HD_TB.BOOKINGNO like '%" & data1 & "%' "
            strSQL = strSQL & "AND Sum(T_INV_BD_TB.QTY) >= 1 "
            strSQL = strSQL & "AND Sum(T_INV_BD_TB.KIN) >= 1 "
            strSQL = strSQL & "AND T_INV_HD_TB.HEADTITLE like 'INVOICE%' "
            strSQL = strSQL & "Order by T_INV_HD_TB.OLD_INVNO "


            'ＳＱＬコマンド作成 
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行 
            dataread = dbcmd.ExecuteReader()


            '結果を取り出す 
            While (dataread.Read())
                'インボイス番号をキーにデータを更新
                If cflg = 0 Then
                    Call check001(Trim(data1))
                    cflg = 1
                Else
                End If

            End While

            'クローズ処理 
            dataread.Close()
            dbcmd.Dispose()
            cnn.Close()
            cnn.Dispose()

            'エラーの抽出　エラーがあればエラー通知メールを送付 エラーの項目に追加・削除
            Call erreg(a, data1, data8)


            If a > 0 Then
                Call Mail02(data1, data2, data3, data4, data5, data6, data7, data10)
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "確認", "<script language='JavaScript'>confirm('エラーが解消されていません。');</script>", False)
            Else
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "確認", "<script language='JavaScript'>confirm('エラーがなくなりました。');</script>", False)


                Call EXELE(data1, data2, data3, data4, data5, data6, data7)


            End If

            Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "確認", "<script language='JavaScript'>confirm('処理が完了しました。');</script>", False)



        ElseIf e.CommandName = "edt3" Then


            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim data1 = Me.GridView2.Rows(index).Cells(16).Text
            Dim data2 = Me.GridView2.Rows(index).Cells(6).Text
            Dim data3 = Me.GridView2.Rows(index).Cells(7).Text
            Dim data4 = Me.GridView2.Rows(index).Cells(9).Text
            Dim data5 = Me.GridView2.Rows(index).Cells(11).Text
            Dim data6 = Me.GridView2.Rows(index).Cells(14).Text
            Dim data7 = Me.GridView2.Rows(index).Cells(13).Text
            Dim data8 = Me.GridView2.Rows(index).Cells(30).Text
            Dim data10 = Me.GridView2.Rows(index).Cells(31).Text


            Dim dataread As SqlDataReader
            Dim dbcmd As SqlCommand
            Dim strSQL As String = ""
            Dim strDate As String = ""
            Dim strinv As String = ""
            Dim eflg As Long
            Dim strcst As String = ""
            Dim FLGval As Long
            Dim a As Long
            Dim cflg As Long

            Dim ck As String = ""

            FLGval = 0

            'データの削除 
            Call DEL_KANRYOERROR()

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


            strSQL = "SELECT DISTINCT T_INV_HD_TB.OLD_INVNO, T_INV_HD_TB.CUSTCODE "
            strSQL = strSQL & "FROM T_INV_HD_TB LEFT JOIN T_INV_BD_TB ON T_INV_HD_TB.INVOICENO = T_INV_BD_TB.INVOICENO "
            strSQL = strSQL & "WHERE T_INV_HD_TB.BLDATE BETWEEN '" & dt3 & "' AND '" & dt2 & "' "

            strSQL = strSQL & "GROUP BY T_INV_HD_TB.BOOKINGNO, T_INV_HD_TB.OLD_INVNO,T_INV_HD_TB.CRYCD, T_INV_HD_TB.SHIPPEDPER, T_INV_HD_TB.VOYAGENO, T_INV_HD_TB.IOPORTDATE, T_INV_HD_TB.CUTDATE,T_INV_HD_TB.CUSTCODE,T_INV_HD_TB.HEADTITLE "
            strSQL = strSQL & "HAVING T_INV_HD_TB.BOOKINGNO like '%" & data1 & "%' "
            strSQL = strSQL & "AND Sum(T_INV_BD_TB.QTY) >= 1 "
            strSQL = strSQL & "AND Sum(T_INV_BD_TB.KIN) >= 1 "
            strSQL = strSQL & "AND T_INV_HD_TB.HEADTITLE like 'INVOICE%' "
            strSQL = strSQL & "Order by T_INV_HD_TB.OLD_INVNO "


            'ＳＱＬコマンド作成 
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行 
            dataread = dbcmd.ExecuteReader()


            '結果を取り出す 
            While (dataread.Read())
                'インボイス番号をキーにデータを更新
                If cflg = 0 Then
                    'Call check001(Trim(data1))
                    cflg = 1
                Else
                End If

            End While

            'クローズ処理 
            dataread.Close()
            dbcmd.Dispose()
            cnn.Close()
            cnn.Dispose()

            'エラーの抽出　エラーがあればエラー通知メールを送付 エラーの項目に追加・削除
            Call erreg(a, data1, data8)


            If a > 0 Then
                Call Mail02(data1, data2, data3, data4, data5, data6, data7, data10)
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "確認", "<script language='JavaScript'>confirm('エラーが解消されていません。');</script>", False)
            Else
                Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "確認", "<script language='JavaScript'>confirm('エラーを強制的に解除します。。');</script>", False)

                Call EXELE(data1, data2, data3, data4, data5, data6, data7)

            End If


        ElseIf e.CommandName = "edt4" Then


            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim data1 = Me.GridView2.Rows(index).Cells(16).Text
            Dim data2 = Me.GridView2.Rows(index).Cells(6).Text
            Dim data3 = Me.GridView2.Rows(index).Cells(7).Text
            Dim data4 = Me.GridView2.Rows(index).Cells(9).Text
            Dim data5 = Me.GridView2.Rows(index).Cells(11).Text
            Dim data6 = Me.GridView2.Rows(index).Cells(14).Text
            Dim data7 = Me.GridView2.Rows(index).Cells(13).Text

            Dim fflg As Long
            Dim cflg As String = ""

            Dim dataread As SqlDataReader
            Dim dbcmd As SqlCommand

            fflg = 0


            Dim strSQL As String
            Dim dtNow As DateTime = DateTime.Now
            Dim strFlg As String = ""

            Dim dltcb01 As CheckBox = Me.GridView2.Rows(index).FindControl("cb01")
            Dim dltcb02 As CheckBox = Me.GridView2.Rows(index).FindControl("cb02")
            Dim dltButton4 As Button = Me.GridView2.Rows(index).FindControl("Button4")

            '接続文字列の作成
            Dim ConnectionString As String = String.Empty
            'SQL Server認証
            ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
            'SqlConnectionクラスの新しいインスタンスを初期化
            Dim cnn = New SqlConnection(ConnectionString)
            Dim Command = cnn.CreateCommand

            'データベース接続を開く
            cnn.Open()



            strSQL = "SELECT T_EXL_CSKANRYO.FLG02 "
            strSQL = strSQL & "FROM T_EXL_CSKANRYO "
            strSQL = strSQL & "WHERE BOOKING_NO = '" & data1 & "' "

            'ＳＱＬコマンド作成 
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行 
            dataread = dbcmd.ExecuteReader()


            '結果を取り出す 
            While (dataread.Read())
                'インボイス番号をキーにデータを更新
                cflg = dataread("FLG02")
            End While

            'クローズ処理 
            dataread.Close()
            dbcmd.Dispose()

            If cflg = "" Then

                If dltcb01.Visible = True And dltcb02.Visible = True Then
                    fflg = 1
                ElseIf dltcb01.Visible = False And dltcb02.Visible = True Then
                    fflg = 2
                ElseIf dltcb01.Visible = True And dltcb02.Visible = False Then
                    fflg = 3
                ElseIf dltcb01.Visible = False And dltcb02.Visible = False Then
                    fflg = 4
                End If

                strSQL = ""
                strSQL = strSQL & "UPDATE T_EXL_CSKANRYO SET "
                strSQL = strSQL & "FLG02 = '" & fflg & "' "
                strSQL = strSQL & "WHERE BOOKING_NO = '" & data1 & "' "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

                strSQL = ""
                strSQL = strSQL & "UPDATE T_EXL_CSKANRYO SET "
                strSQL = strSQL & "FLG04 = '1', "
                strSQL = strSQL & "FLG05 = '1' "
                strSQL = strSQL & "WHERE BOOKING_NO = '" & data1 & "' "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

                cnn.Close()
                cnn.Dispose()

                Me.GridView2.Rows(index).Cells(23).Text = "1"
                Me.GridView2.Rows(index).Cells(24).Text = "1"

                Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "確認", "<script language='JavaScript'>confirm('KD,ｱﾌﾀともにチェックボックスを表示しました。');</script>", False)


            Else

                If cflg = "1" Then

                    dltcb01.Visible = True
                    dltcb02.Visible = True

                    strSQL = ""
                    strSQL = strSQL & "UPDATE T_EXL_CSKANRYO SET "
                    strSQL = strSQL & "FLG02 = '' "
                    strSQL = strSQL & "WHERE BOOKING_NO = '" & data1 & "' "

                    Command.CommandText = strSQL
                    ' SQLの実行
                    Command.ExecuteNonQuery()

                    strSQL = ""
                    strSQL = strSQL & "UPDATE T_EXL_CSKANRYO SET "
                    strSQL = strSQL & "FLG04 = '1', "
                    strSQL = strSQL & "FLG05 = '1' "
                    strSQL = strSQL & "WHERE BOOKING_NO = '" & data1 & "' "

                    Command.CommandText = strSQL
                    ' SQLの実行
                    Command.ExecuteNonQuery()

                    cnn.Close()
                    cnn.Dispose()

                    Me.GridView2.Rows(index).Cells(23).Text = "1"
                    Me.GridView2.Rows(index).Cells(24).Text = "1"

                ElseIf cflg = "2" Then

                    dltcb01.Visible = True
                    dltcb02.Visible = False

                    strSQL = ""
                    strSQL = strSQL & "UPDATE T_EXL_CSKANRYO SET "
                    strSQL = strSQL & "FLG02 = '' "
                    strSQL = strSQL & "WHERE BOOKING_NO = '" & data1 & "' "

                    Command.CommandText = strSQL
                    ' SQLの実行
                    Command.ExecuteNonQuery()

                    strSQL = ""
                    strSQL = strSQL & "UPDATE T_EXL_CSKANRYO SET "
                    strSQL = strSQL & "FLG04 = '1', "
                    strSQL = strSQL & "FLG05 = '0' "
                    strSQL = strSQL & "WHERE BOOKING_NO = '" & data1 & "' "

                    Command.CommandText = strSQL
                    ' SQLの実行
                    Command.ExecuteNonQuery()

                    cnn.Close()
                    cnn.Dispose()

                    Me.GridView2.Rows(index).Cells(23).Text = "0"
                    Me.GridView2.Rows(index).Cells(24).Text = "1"

                ElseIf cflg = "3" Then

                    dltcb01.Visible = False
                    dltcb02.Visible = True

                    strSQL = ""
                    strSQL = strSQL & "UPDATE T_EXL_CSKANRYO SET "
                    strSQL = strSQL & "FLG02 = '' "
                    strSQL = strSQL & "WHERE BOOKING_NO = '" & data1 & "' "

                    Command.CommandText = strSQL
                    ' SQLの実行
                    Command.ExecuteNonQuery()

                    strSQL = ""
                    strSQL = strSQL & "UPDATE T_EXL_CSKANRYO SET "
                    strSQL = strSQL & "FLG04 = '0', "
                    strSQL = strSQL & "FLG05 = '1' "
                    strSQL = strSQL & "WHERE BOOKING_NO = '" & data1 & "' "

                    Command.CommandText = strSQL
                    ' SQLの実行
                    Command.ExecuteNonQuery()

                    cnn.Close()
                    cnn.Dispose()

                    Me.GridView2.Rows(index).Cells(23).Text = "1"
                    Me.GridView2.Rows(index).Cells(24).Text = "0"
                ElseIf cflg = "4" Then

                    dltcb01.Visible = False
                    dltcb02.Visible = False

                    strSQL = ""
                    strSQL = strSQL & "UPDATE T_EXL_CSKANRYO SET "
                    strSQL = strSQL & "FLG02 = '' "
                    strSQL = strSQL & "WHERE BOOKING_NO = '" & data1 & "' "

                    Command.CommandText = strSQL
                    ' SQLの実行
                    Command.ExecuteNonQuery()

                    strSQL = ""
                    strSQL = strSQL & "UPDATE T_EXL_CSKANRYO SET "
                    strSQL = strSQL & "FLG04 = '0', "
                    strSQL = strSQL & "FLG05 = '0' "
                    strSQL = strSQL & "WHERE BOOKING_NO = '" & data1 & "' "

                    Command.CommandText = strSQL
                    ' SQLの実行
                    Command.ExecuteNonQuery()

                    cnn.Close()
                    cnn.Dispose()

                    'GRIDVIEWの作成時になくなる？
                    Me.GridView2.Rows(index).Cells(23).Text = "0"
                    Me.GridView2.Rows(index).Cells(24).Text = "0"

                End If

                Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "確認", "<script language='JavaScript'>confirm('チェックボックスを初期状態に戻しました。');</script>", False)

                dltButton4.Dispose()
                dltcb01.Dispose()
                dltcb02.Dispose()

            End If
            Command.Dispose()

        ElseIf e.CommandName = "edt5" Then


            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim data1 = Me.GridView2.Rows(index).Cells(16).Text
            Dim data2 = Me.GridView2.Rows(index).Cells(6).Text
            Dim data3 = Me.GridView2.Rows(index).Cells(7).Text
            Dim data4 = Me.GridView2.Rows(index).Cells(9).Text
            Dim data5 = Me.GridView2.Rows(index).Cells(11).Text
            Dim data6 = Me.GridView2.Rows(index).Cells(14).Text
            Dim data7 = Me.GridView2.Rows(index).Cells(13).Text
            Dim data8 = Me.GridView2.Rows(index).Cells(30).Text
            Dim data9 = Me.GridView2.Rows(index).Cells(29).Text
            Dim data10 = Me.GridView2.Rows(index).Cells(31).Text

            Dim dataread As SqlDataReader
            Dim dbcmd As SqlCommand
            Dim strSQL As String = ""
            Dim strDate As String = ""
            Dim strinv As String = ""
            Dim eflg As Long
            Dim strcst As String = ""
            Dim FLGval As Long
            Dim a As Long
            Dim cflg As Long

            FLGval = 0


            '接続文字列の作成
            Dim ConnectionString As String = String.Empty
            'SQL Server認証
            ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
            'SqlConnectionクラスの新しいインスタンスを初期化
            Dim cnn = New SqlConnection(ConnectionString)

            Dim dt1 As DateTime = DateTime.Now

            Dim ts1 As New TimeSpan(100, 0, 0, 0)
            Dim ts2 As New TimeSpan(100, 0, 0, 0)
            Dim dt2 As DateTime = dt1 + ts1
            Dim dt3 As DateTime = dt1 - ts1

            'データベース接続を開く
            cnn.Open()
            Dim Command = cnn.CreateCommand

            strSQL = ""
            strSQL = strSQL & "DELETE FROM T_EXL_CSKANRYO "
            strSQL = strSQL & "WHERE T_EXL_CSKANRYO.INVOICE = '" & data5 & "' "
            strSQL = strSQL & "AND T_EXL_CSKANRYO.BOOKING_NO = '" & data1 & "' "
            strSQL = strSQL & "AND T_EXL_CSKANRYO.DAY09 = '" & data8 & "' "

            Command.CommandText = strSQL
            ' SQLの実行
            Command.ExecuteNonQuery()

            cnn.Close()
            cnn.Dispose()

            Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "確認", "<script language='JavaScript'>confirm('案件を削除しました。');</script>", False)
            Command.Dispose()

        ElseIf e.CommandName = "edt6" Then


            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim data1 = Me.GridView2.Rows(index).Cells(16).Text
            Dim data2 = Me.GridView2.Rows(index).Cells(6).Text
            Dim data3 = Me.GridView2.Rows(index).Cells(7).Text
            Dim data4 = Me.GridView2.Rows(index).Cells(9).Text
            Dim data5 = Me.GridView2.Rows(index).Cells(11).Text
            Dim data6 = Me.GridView2.Rows(index).Cells(14).Text
            Dim data7 = Me.GridView2.Rows(index).Cells(13).Text
            Dim data8 = Me.GridView2.Rows(index).Cells(30).Text
            Dim data9 = Me.GridView2.Rows(index).Cells(29).Text
            Dim data10 = Me.GridView2.Rows(index).Cells(31).Text



            Dim dataread As SqlDataReader
            Dim dbcmd As SqlCommand
            Dim strSQL As String = ""
            Dim strDate As String = ""
            Dim strinv As String = ""
            Dim eflg As Long
            Dim strcst As String = ""
            Dim FLGval As Long
            Dim a As Long
            Dim cflg As Long

            FLGval = 0


            '接続文字列の作成
            Dim ConnectionString As String = String.Empty
            'SQL Server認証
            ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
            'SqlConnectionクラスの新しいインスタンスを初期化
            Dim cnn = New SqlConnection(ConnectionString)

            Dim dt1 As DateTime = DateTime.Now

            Dim ts1 As New TimeSpan(100, 0, 0, 0)
            Dim ts2 As New TimeSpan(100, 0, 0, 0)
            Dim dt2 As DateTime = dt1 + ts1
            Dim dt3 As DateTime = dt1 - ts1

            'データベース接続を開く
            cnn.Open()
            Dim Command = cnn.CreateCommand

            If data9 = "1" Then
                data9 = "0"
            Else
                data9 = "1"
            End If

            strSQL = ""
            strSQL = strSQL & "UPDATE T_EXL_CSKANRYO SET "
            strSQL = strSQL & "DAY10 = '" & data9 & "' "
            strSQL = strSQL & "WHERE DAY09 = '" & data8 & "' "

            Command.CommandText = strSQL
            ' SQLの実行
            Command.ExecuteNonQuery()

            cnn.Close()
            cnn.Dispose()

            Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "確認", "<script language='JavaScript'>confirm('最終⇔途中を変更します。');</script>", False)
            Command.Dispose()

        End If

        'Grid再表示
        GridView2.DataBind()

    End Sub

    Private Sub Update_FLG(strIVNO As String, strDate As String, strTime As String)
        'レコードのフラグを取得する。
        Dim strSQL As String
        Dim dtNow As DateTime = DateTime.Now
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strFlg As String = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand

        'データベース接続を開く
        cnn.Open()

        'フラグを確認し、１（作成済み）なら０（未作成）にUPDATEする。
        strSQL = ""
        strSQL = strSQL & "SELECT UPD_FLG FROM T_EXL_VAN_SCH_DETAIL "
        strSQL = strSQL & "WHERE IVNO = '" & strIVNO & "' "
        strSQL = strSQL & "AND VAN_DATE = '" & strDate & "' "
        strSQL = strSQL & "AND VAN_TIME = '" & strTime & "' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        '結果を取り出す 
        While (dataread.Read())
            strFlg = dataread("UPD_FLG")
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()

        'フラグをUPDATE
        strSQL = ""
        strSQL = strSQL & "UPDATE T_EXL_VAN_SCH_DETAIL  "
        If strFlg = "0" Then
            strSQL = strSQL & "SET UPD_FLG = '1', UPDATE_TIME = '" & dtNow & "', UPD_PERSON = '" & Session("UsrId") & "'  "
        ElseIf strFlg = "1" Then
            strSQL = strSQL & "SET UPD_FLG = '0', UPDATE_TIME = '" & dtNow & "', UPD_PERSON = '" & Session("UsrId") & "' "
        End If
        strSQL = strSQL & "WHERE IVNO = '" & strIVNO & "' "
        strSQL = strSQL & "AND VAN_DATE = '" & strDate & "' "
        strSQL = strSQL & "AND VAN_TIME = '" & strTime & "' "

        Command.CommandText = strSQL
        ' SQLの実行
        Command.ExecuteNonQuery()

        cnn.Close()
        cnn.Dispose()
        Command.Dispose()
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
        strSQL = strSQL & "WHERE BOOKING_NO = '" & Trim(bkgno) & "'"

        Command.CommandText = strSQL
        ' SQLの実行
        Command.ExecuteNonQuery()

        cnn.Close()
        cnn.Dispose()
        Command.Dispose()
    End Sub

    Private Sub syorui(bkgno As String)
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
        strSQL = strSQL & "UPDATE T_EXL_CSKANRYO SET DAY08='2' "
        strSQL = strSQL & "WHERE BOOKING_NO = '" & Trim(bkgno) & "'"

        Command.CommandText = strSQL
        ' SQLの実行
        Command.ExecuteNonQuery()

        cnn.Close()
        cnn.Dispose()
        Command.Dispose()
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


        Dim strinv As String = ""
        Dim strbkg As String = ""


        Dim wday As String = ""
        Dim wday2 As String = ""
        Dim wday3 As String = ""
        Dim dt1 As DateTime = DateTime.Now

        Dim ts1 As New TimeSpan(1, 0, 0, 0)
        Dim ts2 As New TimeSpan(2, 0, 0, 0)
        Dim dt2 As DateTime = dt1 - ts1
        Dim dt3 As DateTime = dt1 - ts2


        Dim a As String



        '接続文字列の作成
        Dim ConnectionString As String = String.Empty

        'SQL Server認証
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"

        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand

        'データベース接続を開く
        cnn.Open()


        '表示ボタン　FLG03は表示
        Dim I As Integer
        For I = 0 To GridView1.Rows.Count - 1


            Dim dltcb01 As CheckBox = GridView1.Rows(I).FindControl("cb01")
            Dim dltcb02 As CheckBox = GridView1.Rows(I).FindControl("cb02")
            Dim dltlabel1 As Label = GridView1.Rows(I).FindControl("Label1")
            Dim dltlabel2 As Label = GridView1.Rows(I).FindControl("Label2")

            If CType(GridView1.Rows(I).Cells(0).Controls(1), CheckBox).Checked Then
                Call reg_check(GridView1.Rows(I).Cells(16).Text, 1, GridView1.Rows(I).Cells(30).Text)
            Else
                Call reg_check2(GridView1.Rows(I).Cells(16).Text, 1, GridView1.Rows(I).Cells(30).Text)
            End If

            If CType(GridView1.Rows(I).Cells(1).Controls(1), CheckBox).Checked Then
                Call reg_check(GridView1.Rows(I).Cells(16).Text, 2, GridView1.Rows(I).Cells(30).Text)
            Else
                Call reg_check2(GridView1.Rows(I).Cells(16).Text, 2, GridView1.Rows(I).Cells(30).Text)
            End If


            'strSQL = "SELECT ITK_BKGNO FROM [T_EXL_CSWORKSTATUS] WHERE [T_EXL_CSWORKSTATUS].ITK_BKGNO = '" & Trim(e.Row.Cells(26).Text) & "' "
            strSQL = "SELECT BKGNO FROM [T_EXL_WORKSTATUS00] WHERE [T_EXL_WORKSTATUS00].BKGNO = '" & Trim(Replace(GridView1.Rows(I).Cells(16).Text, vbLf, "")) & "' "
            strSQL = strSQL & "AND [T_EXL_WORKSTATUS00].ID = '006' "

            'ＳＱＬコマンド作成
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行
            dataread = dbcmd.ExecuteReader()
            strbkg = ""
            '結果を取り出す
            While (dataread.Read())
                strbkg += dataread("BKGNO")
                '書類作成状況
                If Trim(GridView1.Rows(I).Cells(16).Text) = Trim(strbkg) And strbkg <> "&nbsp;" Then
                    dltcb01.Checked = True
                End If
            End While

            'クローズ処理
            dataread.Close()
            dbcmd.Dispose()


            'strSQL = "SELECT ITK_BKGNO FROM [T_EXL_CSWORKSTATUS] WHERE [T_EXL_CSWORKSTATUS].ITK_BKGNO = '" & Trim(e.Row.Cells(26).Text) & "' "
            strSQL = "SELECT BKGNO FROM [T_EXL_WORKSTATUS00] WHERE [T_EXL_WORKSTATUS00].BKGNO = '" & Trim(Replace(GridView1.Rows(I).Cells(16).Text, vbLf, "")) & "' "
            strSQL = strSQL & "AND [T_EXL_WORKSTATUS00].ID = '007' "

            'ＳＱＬコマンド作成
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行
            dataread = dbcmd.ExecuteReader()
            strbkg = ""
            '結果を取り出す
            While (dataread.Read())
                strbkg += dataread("BKGNO")
                '書類作成状況
                If Trim(GridView1.Rows(I).Cells(16).Text) = Trim(strbkg) And strbkg <> "&nbsp;" Then
                    dltcb02.Checked = True
                End If
            End While

            'クローズ処理
            dataread.Close()
            dbcmd.Dispose()


            'strSQL = "SELECT ITK_BKGNO FROM [T_EXL_CSWORKSTATUS] WHERE [T_EXL_CSWORKSTATUS].ITK_BKGNO = '" & Trim(e.Row.Cells(26).Text) & "' "
            strSQL = "SELECT BKGNO FROM [T_EXL_WORKSTATUS00] WHERE [T_EXL_WORKSTATUS00].BKGNO = '" & Trim(Replace(GridView1.Rows(I).Cells(16).Text, vbLf, "")) & "' "
            strSQL = strSQL & "AND [T_EXL_WORKSTATUS00].ID = '008' "
            strSQL = strSQL & "AND [T_EXL_WORKSTATUS00].INVNO = '" & Trim(Replace(GridView1.Rows(I).Cells(30).Text, vbLf, "")) & "' "


            'ＳＱＬコマンド作成
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行
            dataread = dbcmd.ExecuteReader()
            strbkg = ""
            '結果を取り出す
            While (dataread.Read())
                strbkg += dataread("BKGNO")
                '書類作成状況
                If Trim(GridView1.Rows(I).Cells(16).Text) = Trim(strbkg) And strbkg <> "&nbsp;" Then
                    dltlabel1.Text = "〇"
                End If
            End While

            'クローズ処理
            dataread.Close()
            dbcmd.Dispose()


            'strSQL = "SELECT ITK_BKGNO FROM [T_EXL_CSWORKSTATUS] WHERE [T_EXL_CSWORKSTATUS].ITK_BKGNO = '" & Trim(e.Row.Cells(26).Text) & "' "
            strSQL = "SELECT BKGNO FROM [T_EXL_WORKSTATUS00] WHERE [T_EXL_WORKSTATUS00].BKGNO = '" & Trim(Replace(GridView1.Rows(I).Cells(16).Text, vbLf, "")) & "' "
            strSQL = strSQL & "AND [T_EXL_WORKSTATUS00].ID = '009' "
            strSQL = strSQL & "AND [T_EXL_WORKSTATUS00].INVNO = '" & Trim(Replace(GridView1.Rows(I).Cells(30).Text, vbLf, "")) & "' "


            'ＳＱＬコマンド作成
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行
            dataread = dbcmd.ExecuteReader()
            strbkg = ""
            '結果を取り出す
            While (dataread.Read())
                strbkg += dataread("BKGNO")
                '書類作成状況
                If Trim(GridView1.Rows(I).Cells(16).Text) = Trim(strbkg) And strbkg <> "&nbsp;" Then
                    dltlabel2.Text = "〇"
                End If
            End While

            'クローズ処理
            dataread.Close()
            dbcmd.Dispose()

            'strSQL = "SELECT ITK_BKGNO FROM [T_EXL_CSWORKSTATUS] WHERE [T_EXL_CSWORKSTATUS].ITK_BKGNO = '" & Trim(e.Row.Cells(26).Text) & "' "
            strSQL = "SELECT BKGNO FROM [T_EXL_WORKSTATUS00] WHERE [T_EXL_WORKSTATUS00].BKGNO = '" & Trim(Replace(GridView1.Rows(I).Cells(16).Text, vbLf, "")) & "' "
            strSQL = strSQL & "AND [T_EXL_WORKSTATUS00].ID = '010' "
            strSQL = strSQL & "AND [T_EXL_WORKSTATUS00].INVNO = '" & Trim(Replace(GridView1.Rows(I).Cells(30).Text, vbLf, "")) & "' "


            'ＳＱＬコマンド作成
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行
            dataread = dbcmd.ExecuteReader()
            strbkg = ""
            '結果を取り出す
            While (dataread.Read())
                strbkg += dataread("BKGNO")
                '書類作成状況
                If Trim(GridView1.Rows(I).Cells(16).Text) = Trim(strbkg) And strbkg <> "&nbsp;" Then

                    If dltlabel2.Text = "〇" Then
                        GridView1.Rows(I).ForeColor = Drawing.Color.White
                        GridView1.Rows(I).BackColor = Drawing.Color.Red
                        GridView1.Rows(I).Font.Bold = True
                    Else
                        GridView1.Rows(I).Font.Strikeout = True
                        GridView1.Rows(I).BackColor = Drawing.Color.LightGray
                    End If


                End If
            End While

            'クローズ処理
            dataread.Close()
            dbcmd.Dispose()

            'strSQL = "SELECT ITK_BKGNO FROM [T_EXL_CSWORKSTATUS] WHERE [T_EXL_CSWORKSTATUS].ITK_BKGNO = '" & Trim(e.Row.Cells(26).Text) & "' "
            strSQL = "SELECT BKGNO FROM [T_EXL_WORKSTATUS00] WHERE [T_EXL_WORKSTATUS00].BKGNO = '" & Trim(Replace(GridView1.Rows(I).Cells(16).Text, vbLf, "")) & "' "
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
                If Trim(GridView1.Rows(I).Cells(16).Text) = Trim(strbkg) And strbkg <> "&nbsp;" Then
                    Call syorui(Trim(GridView1.Rows(I).Cells(16).Text))
                End If
            End While

            'クローズ処理
            dataread.Close()
            dbcmd.Dispose()

            'LABEL
            Dim la01 As String = "完了報告残 ： "

            strSQL = "SELECT Count(T_EXL_CSKANRYO.DAY08) AS C "
            strSQL = strSQL & "FROM T_EXL_CSKANRYO "
            strSQL = strSQL & "WHERE T_EXL_CSKANRYO.DAY08 IS NULL OR T_EXL_CSKANRYO.DAY08 ='' "
            strSQL = strSQL & "AND T_EXL_CSKANRYO.DAY11 = '" & Format(dt1, "yyyy/MM/dd") & "'"

            'ＳＱＬコマンド作成
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行
            dataread = dbcmd.ExecuteReader()

            '結果を取り出す
            While (dataread.Read())
                la01 = la01 & dataread("C")
            End While

            'クローズ処理
            dataread.Close()
            dbcmd.Dispose()

            strSQL = "SELECT Count(T_EXL_CSKANRYO.DAY09) AS C "
            strSQL = strSQL & "FROM T_EXL_CSKANRYO "
            strSQL = strSQL & "WHERE T_EXL_CSKANRYO.DAY09 IS NOT NULL "
            strSQL = strSQL & "AND T_EXL_CSKANRYO.DAY11 = '" & Format(dt1, "yyyy/MM/dd") & "'"



            'ＳＱＬコマンド作成
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行
            dataread = dbcmd.ExecuteReader()

            '結果を取り出す
            While (dataread.Read())
                la01 = la01 & " / " & dataread("C")
            End While

            'クローズ処理
            dataread.Close()
            dbcmd.Dispose()

            Label6.Text = la01 & " コンテナ"

            Dim dltButton As Button = GridView1.Rows(I).FindControl("Button1")
            Dim dltButton2 As Button = GridView1.Rows(I).FindControl("Button2")

            If dltlabel2.Text <> "〇" Then
                dltButton2.Visible = False
                ''GridView1.Rows(I).Cells(4).Text = "-"
            Else
                dltButton2.Visible = True
            End If


            Dim ckfl00 As String = ""
            Dim ckfl01 As String = ""
            Dim ckfl02 As String = ""
            Dim ckfl03 As String = ""

            'ボタンが存在する場合のみセット
            If dltcb01.Visible = True Then
                If dltcb01.Checked = True Then
                    ckfl00 = ""
                Else
                    ckfl00 = "KD"
                End If
            End If
            'ボタンが存在する場合のみセット
            If dltcb02.Visible = True Then
                If dltcb02.Checked = True Then
                    ckfl00 = ""
                Else
                    ckfl01 = "AM"
                End If
            End If

            If dltlabel1.Text = "〇" Then
                dltButton.Attributes.Add("onclick", "return confirm('完了報告を解除しますか？');")
            Else
                If ckfl00 = "KD" And ckfl01 = "AM" Then
                    dltButton.Attributes.Add("onclick", "return confirm('KD、ｱﾌﾀどちらもチェックがありませんが処理を進めますか？\r\n※ｱﾌﾀ、KD混載時は注意してください。');")
                ElseIf ckfl00 = "KD" And ckfl01 = "" Then
                    dltButton.Attributes.Add("onclick", "return confirm('KDにチェックがありませんが処理を進めますか？\r\n※ｱﾌﾀ、KD混載時は注意してください。');")
                ElseIf ckfl00 = "" And ckfl01 = "AM" Then
                    dltButton.Attributes.Add("onclick", "return confirm('ｱﾌﾀにチェックがありませんが処理を進めますか？\r\n※ｱﾌﾀ、KD混載時は注意してください。');")
                Else
                    dltButton.Attributes.Add("onclick", "return confirm('チェックボックス確認\r\n※ｱﾌﾀ、KD混載時は注意してください。');")
                End If
            End If

            'ボタンが存在する場合のみセット
            If Not (dltButton Is Nothing) Then
                dltButton.CommandArgument = GridView1.Rows(I).RowIndex.ToString()
                dltButton.Dispose()
            End If
            'ボタンが存在する場合のみセット
            If Not (dltButton2 Is Nothing) Then
                dltButton2.CommandArgument = GridView1.Rows(I).RowIndex.ToString()
                dltButton2.Dispose()
            End If



            dltlabel1.Dispose()
            dltlabel2.Dispose()

        Next




        For I = 0 To GridView2.Rows.Count - 1


            Dim dltcb01 As CheckBox = GridView2.Rows(I).FindControl("cb01")
            Dim dltcb02 As CheckBox = GridView2.Rows(I).FindControl("cb02")
            Dim dltlabel1 As Label = GridView2.Rows(I).FindControl("Label1")
            Dim dltlabel2 As Label = GridView2.Rows(I).FindControl("Label2")

            'If CType(GridView2.Rows(I).Cells(0).Controls(1), CheckBox).Checked Then
            '    Call reg_check(GridView2.Rows(I).Cells(16).Text, 1, GridView2.Rows(I).Cells(30).Text)
            'Else
            '    Call reg_check2(GridView2.Rows(I).Cells(16).Text, 1, GridView2.Rows(I).Cells(30).Text)
            'End If

            'If CType(GridView2.Rows(I).Cells(1).Controls(1), CheckBox).Checked Then
            '    Call reg_check(GridView2.Rows(I).Cells(16).Text, 2, GridView2.Rows(I).Cells(30).Text)
            'Else
            '    Call reg_check2(GridView2.Rows(I).Cells(16).Text, 2, GridView2.Rows(I).Cells(30).Text)
            'End If


            ''strSQL = "SELECT ITK_BKGNO FROM [T_EXL_CSWORKSTATUS] WHERE [T_EXL_CSWORKSTATUS].ITK_BKGNO = '" & Trim(e.Row.Cells(26).Text) & "' "
            'strSQL = "SELECT BKGNO FROM [T_EXL_WORKSTATUS00] WHERE [T_EXL_WORKSTATUS00].BKGNO = '" & Trim(Replace(GridView2.Rows(I).Cells(16).Text, vbLf, "")) & "' "
            'strSQL = strSQL & "AND [T_EXL_WORKSTATUS00].ID = '006' "

            ''ＳＱＬコマンド作成
            'dbcmd = New SqlCommand(strSQL, cnn)
            ''ＳＱＬ文実行
            'dataread = dbcmd.ExecuteReader()
            'strbkg = ""
            ''結果を取り出す
            'While (dataread.Read())
            '    strbkg += dataread("BKGNO")
            '    '書類作成状況
            '    If Trim(GridView2.Rows(I).Cells(16).Text) = Trim(strbkg) And strbkg <> "&nbsp;" Then
            '        dltcb01.Checked = True
            '    End If
            'End While

            ''クローズ処理
            'dataread.Close()
            'dbcmd.Dispose()


            ''strSQL = "SELECT ITK_BKGNO FROM [T_EXL_CSWORKSTATUS] WHERE [T_EXL_CSWORKSTATUS].ITK_BKGNO = '" & Trim(e.Row.Cells(26).Text) & "' "
            'strSQL = "SELECT BKGNO FROM [T_EXL_WORKSTATUS00] WHERE [T_EXL_WORKSTATUS00].BKGNO = '" & Trim(Replace(GridView2.Rows(I).Cells(16).Text, vbLf, "")) & "' "
            'strSQL = strSQL & "AND [T_EXL_WORKSTATUS00].ID = '007' "

            ''ＳＱＬコマンド作成
            'dbcmd = New SqlCommand(strSQL, cnn)
            ''ＳＱＬ文実行
            'dataread = dbcmd.ExecuteReader()
            'strbkg = ""
            ''結果を取り出す
            'While (dataread.Read())
            '    strbkg += dataread("BKGNO")
            '    '書類作成状況
            '    If Trim(GridView2.Rows(I).Cells(16).Text) = Trim(strbkg) And strbkg <> "&nbsp;" Then
            '        dltcb02.Checked = True
            '    End If
            'End While

            ''クローズ処理
            'dataread.Close()
            'dbcmd.Dispose()


            ''strSQL = "SELECT ITK_BKGNO FROM [T_EXL_CSWORKSTATUS] WHERE [T_EXL_CSWORKSTATUS].ITK_BKGNO = '" & Trim(e.Row.Cells(26).Text) & "' "
            'strSQL = "SELECT BKGNO FROM [T_EXL_WORKSTATUS00] WHERE [T_EXL_WORKSTATUS00].BKGNO = '" & Trim(Replace(GridView2.Rows(I).Cells(16).Text, vbLf, "")) & "' "
            'strSQL = strSQL & "AND [T_EXL_WORKSTATUS00].ID = '008' "
            'strSQL = strSQL & "AND [T_EXL_WORKSTATUS00].INVNO = '" & Trim(Replace(GridView2.Rows(I).Cells(30).Text, vbLf, "")) & "' "


            ''ＳＱＬコマンド作成
            'dbcmd = New SqlCommand(strSQL, cnn)
            ''ＳＱＬ文実行
            'dataread = dbcmd.ExecuteReader()
            'strbkg = ""
            ''結果を取り出す
            'While (dataread.Read())
            '    strbkg += dataread("BKGNO")
            '    '書類作成状況
            '    If Trim(GridView2.Rows(I).Cells(16).Text) = Trim(strbkg) And strbkg <> "&nbsp;" Then
            '        dltlabel1.Text = "〇"
            '    End If
            'End While

            ''クローズ処理
            'dataread.Close()
            'dbcmd.Dispose()


            ''strSQL = "SELECT ITK_BKGNO FROM [T_EXL_CSWORKSTATUS] WHERE [T_EXL_CSWORKSTATUS].ITK_BKGNO = '" & Trim(e.Row.Cells(26).Text) & "' "
            'strSQL = "SELECT BKGNO FROM [T_EXL_WORKSTATUS00] WHERE [T_EXL_WORKSTATUS00].BKGNO = '" & Trim(Replace(GridView2.Rows(I).Cells(16).Text, vbLf, "")) & "' "
            'strSQL = strSQL & "AND [T_EXL_WORKSTATUS00].ID = '009' "
            'strSQL = strSQL & "AND [T_EXL_WORKSTATUS00].INVNO = '" & Trim(Replace(GridView2.Rows(I).Cells(30).Text, vbLf, "")) & "' "


            ''ＳＱＬコマンド作成
            'dbcmd = New SqlCommand(strSQL, cnn)
            ''ＳＱＬ文実行
            'dataread = dbcmd.ExecuteReader()
            'strbkg = ""
            ''結果を取り出す
            'While (dataread.Read())
            '    strbkg += dataread("BKGNO")
            '    '書類作成状況
            '    If Trim(GridView2.Rows(I).Cells(16).Text) = Trim(strbkg) And strbkg <> "&nbsp;" Then
            '        dltlabel2.Text = "〇"
            '    End If
            'End While

            ''クローズ処理
            'dataread.Close()
            'dbcmd.Dispose()

            'strSQL = "SELECT ITK_BKGNO FROM [T_EXL_CSWORKSTATUS] WHERE [T_EXL_CSWORKSTATUS].ITK_BKGNO = '" & Trim(e.Row.Cells(26).Text) & "' "
            strSQL = "SELECT BKGNO FROM [T_EXL_WORKSTATUS00] WHERE [T_EXL_WORKSTATUS00].BKGNO = '" & Trim(Replace(GridView2.Rows(I).Cells(16).Text, vbLf, "")) & "' "
            strSQL = strSQL & "AND [T_EXL_WORKSTATUS00].ID = '010' "
            strSQL = strSQL & "AND [T_EXL_WORKSTATUS00].INVNO = '" & Trim(Replace(GridView2.Rows(I).Cells(30).Text, vbLf, "")) & "' "


            'ＳＱＬコマンド作成
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行
            dataread = dbcmd.ExecuteReader()
            strbkg = ""
            '結果を取り出す
            While (dataread.Read())
                strbkg += dataread("BKGNO")
                '書類作成状況
                If Trim(GridView2.Rows(I).Cells(16).Text) = Trim(strbkg) And strbkg <> "&nbsp;" Then

                    If dltlabel2.Text = "〇" Then
                        GridView2.Rows(I).ForeColor = Drawing.Color.White
                        GridView2.Rows(I).ForeColor = Drawing.Color.Red
                        GridView2.Rows(I).Font.Bold = True
                    Else
                        'GridView2.Rows(I).Font.Strikeout = True
                        GridView2.Rows(I).BackColor = Drawing.Color.LightGray
                    End If


                End If
            End While

            'クローズ処理
            dataread.Close()
            dbcmd.Dispose()

            ''strSQL = "SELECT ITK_BKGNO FROM [T_EXL_CSWORKSTATUS] WHERE [T_EXL_CSWORKSTATUS].ITK_BKGNO = '" & Trim(e.Row.Cells(26).Text) & "' "
            'strSQL = "SELECT BKGNO FROM [T_EXL_WORKSTATUS00] WHERE [T_EXL_WORKSTATUS00].BKGNO = '" & Trim(Replace(GridView2.Rows(I).Cells(16).Text, vbLf, "")) & "' "
            'strSQL = strSQL & "AND [T_EXL_WORKSTATUS00].ID = '002' "

            ''ＳＱＬコマンド作成
            'dbcmd = New SqlCommand(strSQL, cnn)
            ''ＳＱＬ文実行
            'dataread = dbcmd.ExecuteReader()
            'strbkg = ""
            ''結果を取り出す
            'While (dataread.Read())
            '    strbkg += dataread("BKGNO")
            '    '書類作成状況
            '    If Trim(GridView2.Rows(I).Cells(16).Text) = Trim(strbkg) And strbkg <> "&nbsp;" Then
            '        Call syorui(Trim(GridView2.Rows(I).Cells(16).Text))
            '    End If
            'End While

            ''クローズ処理
            'dataread.Close()
            'dbcmd.Dispose()




            Dim dltButton As Button = GridView2.Rows(I).FindControl("Button1")
            Dim dltButton2 As Button = GridView2.Rows(I).FindControl("Button2")

            'If dltlabel2.Text <> "〇" Then
            '    dltButton2.Visible = False
            'Else
            '    dltButton2.Visible = True
            'End If


            Dim ckfl00 As String = ""
            Dim ckfl01 As String = ""
            Dim ckfl02 As String = ""
            Dim ckfl03 As String = ""

            ''ボタンが存在する場合のみセット
            'If dltcb01.Visible = True Then
            '    If dltcb01.Checked = True Then
            '        ckfl00 = ""
            '    Else
            '        ckfl00 = "KD"
            '    End If
            'End If
            ''ボタンが存在する場合のみセット
            'If dltcb02.Visible = True Then
            '    If dltcb02.Checked = True Then
            '        ckfl00 = ""
            '    Else
            '        ckfl01 = "AM"
            '    End If
            'End If

            'If dltlabel1.Text = "〇" Then
            '    dltButton.Attributes.Add("onclick", "return confirm('完了報告を解除しますか？');")
            'Else
            '    If ckfl00 = "KD" And ckfl01 = "AM" Then
            '        dltButton.Attributes.Add("onclick", "return confirm('KD、ｱﾌﾀどちらもチェックがありませんが処理を進めますか？\r\n※ｱﾌﾀ、KD混載時は注意してください。');")
            '    ElseIf ckfl00 = "KD" And ckfl01 = "" Then
            '        dltButton.Attributes.Add("onclick", "return confirm('KDにチェックがありませんが処理を進めますか？\r\n※ｱﾌﾀ、KD混載時は注意してください。');")
            '    ElseIf ckfl00 = "" And ckfl01 = "AM" Then
            '        dltButton.Attributes.Add("onclick", "return confirm('ｱﾌﾀにチェックがありませんが処理を進めますか？\r\n※ｱﾌﾀ、KD混載時は注意してください。');")
            '    Else
            '        dltButton.Attributes.Add("onclick", "return confirm('チェックボックス確認\r\n※ｱﾌﾀ、KD混載時は注意してください。');")
            '    End If
            'End If

            'If GridView2.Rows(I).Cells(29).Text = "1" Then
            '    dltlabel1.Text = "最終"
            'Else
            '    dltlabel1.Text = "途中"
            'End If

            ''ボタンが存在する場合のみセット
            'If Not (dltButton Is Nothing) Then
            '    dltButton.CommandArgument = GridView2.Rows(I).RowIndex.ToString()
            '    dltButton.Dispose()
            'End If
            ''ボタンが存在する場合のみセット
            'If Not (dltButton2 Is Nothing) Then
            '    dltButton2.CommandArgument = GridView2.Rows(I).RowIndex.ToString()
            '    dltButton2.Dispose()
            'End If

            dltlabel1.Dispose()
            dltlabel2.Dispose()

        Next

        Dim strDate As String = ""

        strSQL = "SELECT DATA_UPD FROM T_EXL_DATA_UPD WHERE DATA_CD = '015'"
        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        strDate = ""
        '結果を取り出す 
        While (dataread.Read())
            strDate += dataread("DATA_UPD")
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()

        If strDate < dt3 Then

            '最終更新年月日を表示
            Me.Label10.Text = strDate & " 最終更新分(2日前以前)"
            Me.Label10.ForeColor = Drawing.Color.Red


        ElseIf strDate < dt2 Then

            '最終更新年月日を表示
            Me.Label10.Text = strDate & " 最終更新分(1日前以前)"
            Me.Label10.ForeColor = Drawing.Color.Red

        Else

            '最終更新年月日を表示
            Me.Label10.Text = strDate & " 最終更新分"
            Me.Label10.ForeColor = Drawing.Color.Black

        End If

        cnn.Close()
        cnn.Dispose()
    End Sub

    Private Sub reg_check(bkgno As String, A As String, b As String)

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


        strinv = b        'ETD(計上日)
        If A = "1" Then 'KD
            Call INS_kanryo(strinv, bkgno)
        ElseIf A = "2" Then 'ｱﾌﾀ
            Call INS_kanryo2(strinv, bkgno)
        End If

        cnn.Close()
        cnn.Dispose()

    End Sub

    Private Sub reg_check2(bkgno As String, A As String, b As String)

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

        strinv = b      'ETD(計上日)
        If A = "1" Then 'KD
            Call DEL_kanryo(strinv, bkgno)
        ElseIf A = "2" Then 'ｱﾌﾀ
            Call DEL_kanryo2(strinv, bkgno)
        End If

        cnn.Close()
        cnn.Dispose()

    End Sub

    Private Sub reg_check3(bkgno As String, A As String, ByRef dflg As Long, nom As String, kanflg As String)

        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String
        Dim strinv As String
        Dim tcnt As Long

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


        strinv = nom       'ETD(計上日)
        If A = "1" Then 'KD
            Call reg_kanryo(strinv, bkgno)
        ElseIf A = "2" Then 'KD
            Call reg_kanryo2(strinv, bkgno)
        ElseIf A = "3" Then 'KD

            Call get_day07(strinv, bkgno, dflg, tcnt) 'tcnt 

            If kanflg = 1 Then
                Call reg_kanryo3(strinv, bkgno, dflg, tcnt)
                Call reg_kanryo4(strinv, bkgno, dflg, tcnt)
            Else
                Call reg_kanryo5(strinv, bkgno, dflg, tcnt)
                Call reg_kanryo4(strinv, bkgno, dflg, tcnt)
            End If
        End If

            cnn.Close()
        cnn.Dispose()

    End Sub
    Private Sub INS_kanryo(strinv As String, bkgno As String)
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

        If bkgno = "" Or IsNothing(bkgno) = True Then

        Else

            strSQL = ""
            strSQL = strSQL & "SELECT COUNT(*) AS RecCnt FROM T_EXL_WORKSTATUS00 WHERE "
            strSQL = strSQL & "T_EXL_WORKSTATUS00.ID = '006' "
            '           strSQL = strSQL & "AND T_EXL_WORKSTATUS00.INVNO = '" & strinv & "' "
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
                '               strSQL = strSQL & "WHERE T_EXL_WORKSTATUS00.INVNO ='" & strinv & "' "
                strSQL = strSQL & "WHERE T_EXL_WORKSTATUS00.BKGNO = '" & bkgno & "' "
                strSQL = strSQL & "AND T_EXL_WORKSTATUS00.ID = '006' "
            Else
                strSQL = ""
                strSQL = strSQL & "INSERT INTO T_EXL_WORKSTATUS00 VALUES("
                strSQL = strSQL & " '006' "
                strSQL = strSQL & ",'" & strinv & "' "
                strSQL = strSQL & ",'" & bkgno & "' "
                strSQL = strSQL & ",'" & Format(Now(), "yyyy/MM/dd") & "' "
                strSQL = strSQL & ",'" & Session("UsrId") & "_07" & "' "
                strSQL = strSQL & ")"
            End If

            Command.CommandText = strSQL
            ' SQLの実行
            Command.ExecuteNonQuery()

        End If

        cnn.Close()
        cnn.Dispose()
        Command.Dispose()
    End Sub

    Private Sub INS_kanryo2(strinv As String, bkgno As String)
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

        If bkgno = "" Or IsNothing(bkgno) = True Then

        Else

            strSQL = ""
            strSQL = strSQL & "SELECT COUNT(*) AS RecCnt FROM T_EXL_WORKSTATUS00 WHERE "
            strSQL = strSQL & "T_EXL_WORKSTATUS00.ID = '007' "
            'strSQL = strSQL & "AND T_EXL_WORKSTATUS00.INVNO = '" & strinv & "' "
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
                'strSQL = strSQL & "WHERE T_EXL_WORKSTATUS00.INVNO ='" & strinv & "' "
                strSQL = strSQL & "WHERE T_EXL_WORKSTATUS00.BKGNO = '" & bkgno & "' "
                strSQL = strSQL & "AND T_EXL_WORKSTATUS00.ID = '007' "
            Else

                strSQL = ""
                strSQL = strSQL & "INSERT INTO T_EXL_WORKSTATUS00 VALUES("
                strSQL = strSQL & " '007' "
                strSQL = strSQL & ",'" & strinv & "' "
                strSQL = strSQL & ",'" & bkgno & "' "
                strSQL = strSQL & ",'" & Format(Now(), "yyyy/MM/dd") & "' "
                strSQL = strSQL & ",'" & Session("UsrId") & "_07" & "' "
                strSQL = strSQL & ")"
            End If

            Command.CommandText = strSQL
            ' SQLの実行
            Command.ExecuteNonQuery()

        End If

        cnn.Close()
        cnn.Dispose()
        Command.Dispose()
    End Sub

    Private Sub DEL_kanryo(strinv As String, bkgno As String)
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

        If bkgno = "" Or IsNothing(bkgno) = True Then

        Else

            strSQL = ""
            strSQL = strSQL & "SELECT COUNT(*) AS RecCnt FROM T_EXL_WORKSTATUS00 WHERE "
            strSQL = strSQL & "T_EXL_WORKSTATUS00.ID = '006' "
            'strSQL = strSQL & "AND T_EXL_WORKSTATUS00.INVNO = '" & strinv & "' "
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
                strSQL = strSQL & "WHERE T_EXL_WORKSTATUS00.ID = '006' "
                'strSQL = strSQL & "AND T_EXL_WORKSTATUS00.INVNO = '" & strinv & "' "
                strSQL = strSQL & "AND T_EXL_WORKSTATUS00.BKGNO = '" & bkgno & "' "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()
            Else
            End If
        End If

        cnn.Close()
        cnn.Dispose()
        Command.Dispose()
    End Sub

    Private Sub DEL_kanryo2(strinv As String, bkgno As String)
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

        If bkgno = "" Or IsNothing(bkgno) = True Then

        Else

            strSQL = ""
            strSQL = strSQL & "SELECT COUNT(*) AS RecCnt FROM T_EXL_WORKSTATUS00 WHERE "
            strSQL = strSQL & "T_EXL_WORKSTATUS00.ID = '007' "
            'strSQL = strSQL & "AND T_EXL_WORKSTATUS00.INVNO = '" & strinv & "' "
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
                strSQL = strSQL & "WHERE T_EXL_WORKSTATUS00.ID = '007' "
                'strSQL = strSQL & "AND T_EXL_WORKSTATUS00.INVNO = '" & strinv & "' "
                strSQL = strSQL & "AND T_EXL_WORKSTATUS00.BKGNO = '" & bkgno & "' "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()
            Else
            End If
        End If

        cnn.Close()
        cnn.Dispose()
        Command.Dispose()
    End Sub

    Private Sub reg_kanryo(strinv As String, bkgno As String)
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

        If bkgno = "" Or IsNothing(bkgno) = True Then

        Else

            strSQL = ""
            strSQL = strSQL & "SELECT COUNT(*) AS RecCnt FROM T_EXL_WORKSTATUS00 WHERE "
            strSQL = strSQL & "T_EXL_WORKSTATUS00.ID = '008' "
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
                strSQL = strSQL & "WHERE T_EXL_WORKSTATUS00.ID = '008' "
                'strSQL = strSQL & "AND T_EXL_WORKSTATUS00.INVNO = '" & strinv & "' "
                strSQL = strSQL & "AND T_EXL_WORKSTATUS00.BKGNO = '" & bkgno & "' "

                strSQL = ""
                strSQL = strSQL & "UPDATE T_EXL_WORKSTATUS00 SET "
                strSQL = strSQL & "T_EXL_WORKSTATUS00.INVNO = '" & strinv & "', "
                strSQL = strSQL & "T_EXL_WORKSTATUS00.REGDATE = '" & Format(Now(), "yyyy/MM/dd") & "', "
                strSQL = strSQL & "T_EXL_WORKSTATUS00.BKGNO = '" & bkgno & "' "
                'strSQL = strSQL & "WHERE T_EXL_WORKSTATUS00.INVNO ='" & strinv & "' "
                strSQL = strSQL & "WHERE T_EXL_WORKSTATUS00.BKGNO = '" & bkgno & "' "
                strSQL = strSQL & "AND T_EXL_WORKSTATUS00.ID = '008' "

            Else

                strSQL = ""
                strSQL = strSQL & "INSERT INTO T_EXL_WORKSTATUS00 VALUES("
                strSQL = strSQL & " '008' "
                strSQL = strSQL & ",'" & strinv & "' "
                strSQL = strSQL & ",'" & bkgno & "' "
                strSQL = strSQL & ",'" & Format(Now(), "yyyy/MM/dd") & "' "
                strSQL = strSQL & ",'" & Session("UsrId") & "_07" & "' "
                strSQL = strSQL & ")"
            End If

            Command.CommandText = strSQL
            ' SQLの実行
            Command.ExecuteNonQuery()

        End If

        cnn.Close()
        cnn.Dispose()
        Command.Dispose()
    End Sub

    Private Sub reg_kanryo2(strinv As String, bkgno As String)
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

        If bkgno = "" Or IsNothing(bkgno) = True Then

        Else

            strSQL = ""
            strSQL = strSQL & "SELECT COUNT(*) AS RecCnt FROM T_EXL_WORKSTATUS00 WHERE "
            strSQL = strSQL & "T_EXL_WORKSTATUS00.ID = '008' "
            'strSQL = strSQL & "AND T_EXL_WORKSTATUS00.INVNO = '" & strinv & "' "
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
                strSQL = strSQL & "WHERE T_EXL_WORKSTATUS00.ID = '008' "
                'strSQL = strSQL & "AND T_EXL_WORKSTATUS00.INVNO = '" & strinv & "' "
                strSQL = strSQL & "AND T_EXL_WORKSTATUS00.BKGNO = '" & bkgno & "' "

            End If

            Command.CommandText = strSQL
            ' SQLの実行
            Command.ExecuteNonQuery()

        End If

        cnn.Close()
        cnn.Dispose()
        Command.Dispose()
    End Sub

    Private Sub reg_kanryo3(strinv As String, bkgno As String, ByRef dflg As Long, tcnt As Long)
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

        If bkgno = "" Or IsNothing(bkgno) = True Then

        Else

            strSQL = ""
            strSQL = strSQL & "SELECT COUNT(*) AS RecCnt FROM T_EXL_WORKSTATUS00 WHERE "
            strSQL = strSQL & "T_EXL_WORKSTATUS00.ID = '008' "
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

            If tcnt = 1 Then
                strSQL = ""
                strSQL = strSQL & "DELETE FROM T_EXL_WORKSTATUS00 "
                strSQL = strSQL & "WHERE T_EXL_WORKSTATUS00.ID IN ('008','009') "
                strSQL = strSQL & "AND T_EXL_WORKSTATUS00.INVNO = '" & strinv & "' "
                strSQL = strSQL & "AND T_EXL_WORKSTATUS00.BKGNO = '" & bkgno & "' "



                dflg = 1

            Else

                strSQL = ""
                strSQL = strSQL & "INSERT INTO T_EXL_WORKSTATUS00 VALUES("
                strSQL = strSQL & " '008' "
                strSQL = strSQL & ",'" & strinv & "' "
                strSQL = strSQL & ",'" & bkgno & "' "
                strSQL = strSQL & ",'" & Format(Now(), "yyyy/MM/dd") & "' "
                strSQL = strSQL & ",'" & Session("UsrId") & "_07" & "' "
                strSQL = strSQL & ")"
            End If

            Command.CommandText = strSQL
            ' SQLの実行
            Command.ExecuteNonQuery()

        End If

        cnn.Close()
        cnn.Dispose()
        Command.Dispose()
    End Sub

    Private Sub reg_kanryo4(strinv As String, bkgno As String, ByRef dflg As Long, tcnt As Long)
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

        If bkgno = "" Or IsNothing(bkgno) = True Then

        Else

            strSQL = ""
            strSQL = strSQL & "SELECT COUNT(*) AS RecCnt FROM T_EXL_WORKSTATUS00 WHERE "
            strSQL = strSQL & "T_EXL_WORKSTATUS00.ID = '010' "
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

            If tcnt = 1 Then

                strSQL = ""
                strSQL = strSQL & "DELETE FROM T_EXL_WORKSTATUS00 "
                strSQL = strSQL & "WHERE T_EXL_WORKSTATUS00.ID IN ('010') "
                strSQL = strSQL & "AND T_EXL_WORKSTATUS00.INVNO = '" & strinv & "' "
                strSQL = strSQL & "AND T_EXL_WORKSTATUS00.BKGNO = '" & bkgno & "' "


                dflg = 1

            Else

                strSQL = ""
                strSQL = strSQL & "INSERT INTO T_EXL_WORKSTATUS00 VALUES("
                strSQL = strSQL & " '010' "
                strSQL = strSQL & ",'" & strinv & "' "
                strSQL = strSQL & ",'" & bkgno & "' "
                strSQL = strSQL & ",'" & Format(Now(), "yyyy/MM/dd") & "' "
                strSQL = strSQL & ",'" & Session("UsrId") & "_07" & "' "
                strSQL = strSQL & ")"
            End If

            Command.CommandText = strSQL
            ' SQLの実行
            Command.ExecuteNonQuery()

        End If

        cnn.Close()
        cnn.Dispose()
        Command.Dispose()
    End Sub

    Private Sub reg_kanryo5(strinv As String, bkgno As String, ByRef dflg As Long, tcnt As Long)
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

        If bkgno = "" Or IsNothing(bkgno) = True Then

        Else

            strSQL = ""
            strSQL = strSQL & "SELECT COUNT(*) AS RecCnt FROM T_EXL_WORKSTATUS00 WHERE "
            strSQL = strSQL & "T_EXL_WORKSTATUS00.ID = '008' "
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

            If tcnt = 1 Then
                strSQL = ""
                strSQL = strSQL & "DELETE FROM T_EXL_WORKSTATUS00 "
                strSQL = strSQL & "WHERE T_EXL_WORKSTATUS00.ID IN ('008','009') "
                strSQL = strSQL & "AND T_EXL_WORKSTATUS00.INVNO = '" & strinv & "' "
                strSQL = strSQL & "AND T_EXL_WORKSTATUS00.BKGNO = '" & bkgno & "' "



                dflg = 1

            Else

            End If

            Command.CommandText = strSQL
            ' SQLの実行
            Command.ExecuteNonQuery()

        End If

        cnn.Close()
        cnn.Dispose()
        Command.Dispose()
    End Sub
    Private Sub get_day07(strinv As String, bkgno As String, ByRef dflg As Long, ByRef intCnt As Long)
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


        Dim dt1 As DateTime = DateTime.Now

        'データベース接続を開く
        cnn.Open()

        If bkgno = "" Or IsNothing(bkgno) = True Then

        Else

            strSQL = ""
            strSQL = strSQL & "SELECT COUNT(*) AS RecCnt FROM T_EXL_CSKANRYO "
            strSQL = strSQL & "WHERE T_EXL_CSKANRYO.DAY09 = '" & strinv & "' "
            strSQL = strSQL & "AND T_EXL_CSKANRYO.BOOKING_NO = '" & bkgno & "' "
            strSQL = strSQL & "AND T_EXL_CSKANRYO.DAY07 = '1' "

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
                strSQL = strSQL & "UPDATE T_EXL_CSKANRYO SET "
                strSQL = strSQL & "T_EXL_CSKANRYO.DAY07 = '0' "
                strSQL = strSQL & "WHERE T_EXL_CSKANRYO.DAY09 = '" & strinv & "' "
                strSQL = strSQL & "AND T_EXL_CSKANRYO.BOOKING_NO = '" & bkgno & "' "

                dflg = 1

            Else

                strSQL = ""
                strSQL = strSQL & "UPDATE T_EXL_CSKANRYO SET "
                strSQL = strSQL & "T_EXL_CSKANRYO.DAY07 = '1' "
                strSQL = strSQL & "WHERE T_EXL_CSKANRYO.DAY09 = '" & strinv & "' "
                strSQL = strSQL & "AND T_EXL_CSKANRYO.BOOKING_NO = '" & bkgno & "' "

            End If

            Command.CommandText = strSQL
            ' SQLの実行
            Command.ExecuteNonQuery()

        End If

        cnn.Close()
        cnn.Dispose()
        Command.Dispose()
    End Sub
    Private Sub check001(bkgno As String)
        'データの取得


        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String = ""
        Dim strinv As String = ""
        Dim eflg As Long
        Dim strcst As String = ""

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


        strSQL = "SELECT DISTINCT T_INV_HD_TB.OLD_INVNO, T_INV_HD_TB.CUSTCODE "
        strSQL = strSQL & "FROM T_INV_HD_TB LEFT JOIN T_INV_BD_TB ON T_INV_HD_TB.INVOICENO = T_INV_BD_TB.INVOICENO "
        strSQL = strSQL & "WHERE T_INV_HD_TB.BLDATE BETWEEN '" & dt3 & "' AND '" & dt2 & "' "



        strSQL = strSQL & "GROUP BY T_INV_HD_TB.BOOKINGNO, T_INV_HD_TB.OLD_INVNO,T_INV_HD_TB.CRYCD, T_INV_HD_TB.SHIPPEDPER, T_INV_HD_TB.VOYAGENO, T_INV_HD_TB.IOPORTDATE, T_INV_HD_TB.CUTDATE,T_INV_HD_TB.CUSTCODE,T_INV_HD_TB.HEADTITLE "
        strSQL = strSQL & "HAVING T_INV_HD_TB.BOOKINGNO like '%" & bkgno & "%' "
        strSQL = strSQL & "AND Sum(T_INV_BD_TB.QTY) >= 1 "
        strSQL = strSQL & "AND Sum(T_INV_BD_TB.KIN) >= 1 "
        strSQL = strSQL & "AND T_INV_HD_TB.HEADTITLE like 'INVOICE%' "
        strSQL = strSQL & "Order by T_INV_HD_TB.OLD_INVNO "


        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()


        '結果を取り出す 
        While (dataread.Read())
            strinv = Trim(Convert.ToString(dataread("OLD_INVNO")))        '客先目
            strcst = Trim(Convert.ToString(dataread("CUSTCODE")))        '客先目
            Call check002(strinv, bkgno, eflg, strcst)
            Call check004(strinv, bkgno, eflg, strcst)
            Call check003(strinv, bkgno, eflg, strcst)
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()



    End Sub

    Private Sub get_meisai(bkgno As String, ByRef FLGval As String)
        'データの取得


        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String = ""
        Dim strinv As String = ""
        Dim eflg As Long
        Dim strcst As String = ""

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


        strSQL = "SELECT DISTINCT T_INV_HD_TB.OLD_INVNO, T_INV_HD_TB.CUSTCODE "
        strSQL = strSQL & "FROM T_INV_HD_TB LEFT JOIN T_INV_BD_TB ON T_INV_HD_TB.INVOICENO = T_INV_BD_TB.INVOICENO "
        strSQL = strSQL & "WHERE T_INV_HD_TB.BLDATE BETWEEN '" & dt3 & "' AND '" & dt2 & "' "

        strSQL = strSQL & "GROUP BY T_INV_HD_TB.BOOKINGNO, T_INV_HD_TB.OLD_INVNO,T_INV_HD_TB.CRYCD, T_INV_HD_TB.SHIPPEDPER, T_INV_HD_TB.VOYAGENO, T_INV_HD_TB.IOPORTDATE, T_INV_HD_TB.CUTDATE,T_INV_HD_TB.CUSTCODE,T_INV_HD_TB.HEADTITLE "
        strSQL = strSQL & "HAVING T_INV_HD_TB.BOOKINGNO like '%" & bkgno & "%' "
        strSQL = strSQL & "AND Sum(T_INV_BD_TB.QTY) >= 1 "
        strSQL = strSQL & "AND Sum(T_INV_BD_TB.KIN) >= 1 "
        strSQL = strSQL & "AND T_INV_HD_TB.HEADTITLE like 'INVOICE%' "
        strSQL = strSQL & "Order by T_INV_HD_TB.OLD_INVNO "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        FLGval = 0
        '結果を取り出す 
        While (dataread.Read())

            FLGval = 1

        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()



    End Sub


    Private Sub check002(strinv As String, bkgno As String, eflg As Long, strcst As String)
        'データの取得


        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String = ""
        Dim h01 As String = ""
        Dim h02 As String = ""
        Dim h03 As String = ""
        Dim Strcn As String = ""

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

        strSQL = "SELECT T_INV_HD_TB.CRYCD,IIf(T_INV_HD_TB.CRYCD=T_SN_HD_TB.CRYCD,'1','2') AS AAA,IIf(T_INV_HD_TB.TATENECD=T_SN_HD_TB.TATENECD,'1','2') AS BBB ,IIf(T_INV_HD_TB.RATE=T_SN_HD_TB.RATE,'1','2') AS CCC,T_INV_HD_TB.CONSIGNEENAME,T_INV_HD_TB.CONSIGNEEADDRESS "
        strSQL = strSQL & "FROM (T_INV_HD_TB LEFT JOIN T_INV_BD_TB ON T_INV_HD_TB.INVOICENO = T_INV_BD_TB.INVOICENO) LEFT JOIN T_SN_HD_TB ON T_INV_BD_TB.SNNO = T_SN_HD_TB.SALESNOTENO "
        strSQL = strSQL & "WHERE T_INV_BD_TB.EXDNO <> 'INTEREST' "


        'INVOICENOが最大のものを取得
        strSQL = strSQL & "AND T_INV_HD_TB.INVOICENO = "
        strSQL = strSQL & "(SELECT MAX(T_INV_HD_TB.INVOICENO) AS IVNO "
        strSQL = strSQL & "FROM T_INV_HD_TB "
        strSQL = strSQL & "WHERE T_INV_HD_TB.HEADTITLE Like 'INVOICE%' "
        strSQL = strSQL & "AND T_INV_HD_TB.OLD_INVNO = '" & strinv & "' "
        strSQL = strSQL & "AND T_INV_HD_TB.BOOKINGNO = '" & bkgno & "' "
        strSQL = strSQL & "AND T_INV_HD_TB.ORG_INVOICENO IS NULL "
        strSQL = strSQL & ") "

        strSQL = strSQL & "GROUP BY T_INV_HD_TB.CRYCD,IIf(T_INV_HD_TB.CRYCD=T_SN_HD_TB.CRYCD,'1','2'),IIf(T_INV_HD_TB.TATENECD=T_SN_HD_TB.TATENECD,'1','2'),IIf(T_INV_HD_TB.RATE=T_SN_HD_TB.RATE,'1','2') ,T_INV_HD_TB.CONSIGNEENAME,T_INV_HD_TB.CONSIGNEEADDRESS  "
        strSQL = strSQL & "ORDER BY IIf(T_INV_HD_TB.CRYCD=T_SN_HD_TB.CRYCD,'1','2'),IIf(T_INV_HD_TB.TATENECD=T_SN_HD_TB.TATENECD,'1','2'),IIf(T_INV_HD_TB.RATE=T_SN_HD_TB.RATE,'1','2') DESC  "



        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()


        '結果を取り出す 
        While (dataread.Read())

            h01 = Trim(Convert.ToString(dataread("AAA")))        '客先目
            h02 = Trim(Convert.ToString(dataread("BBB")))        '客先目
            h03 = Trim(Convert.ToString(dataread("CCC")))        '客先目

            'Strcn = Trim(Convert.ToString(dataread("CONSIGNEENAME"))) & Trim(Convert.ToString(dataread("CONSIGNEEADDRESS")))
            'Call check003(Strcn, strcst)

            If h01 <> "1" Then
                eflg = 1
            End If


            If h02 <> "1" Then
                eflg = 2
            End If


            If h03 <> "1" Then
                eflg = 3
            End If

        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()



    End Sub

    Private Sub check003(strinv As String, bkgno As String, eflg As Long, strcst As String)
        'データの取得


        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String = ""
        Dim h01 As String = ""
        Dim h02 As String = ""
        Dim h03 As String = ""
        Dim Strcn As String = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=svdpo051;Initial Catalog=BPTB001;User Id=ado_bptb001;Password=ado_bptb001"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        '接続文字列の作成
        Dim ConnectionString2 As String = String.Empty
        'SQL Server認証
        ConnectionString2 = "Data Source=KBHWPM02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn2 = New SqlConnection(ConnectionString2)
        Dim Command = cnn2.CreateCommand


        Dim errlrmsg01 As String

        Dim dt1 As DateTime = DateTime.Now

        Dim ts1 As New TimeSpan(100, 0, 0, 0)
        Dim ts2 As New TimeSpan(100, 0, 0, 0)
        Dim dt2 As DateTime = dt1 + ts1
        Dim dt3 As DateTime = dt1 - ts1

        'データベース接続を開く
        cnn.Open()
        cnn2.Open()

        strSQL = "SELECT COUNT(T_INV_HD_TB.INVOICENO) AS CNT "
        strSQL = strSQL & "FROM T_INV_HD_TB "
        strSQL = strSQL & "WHERE T_INV_HD_TB.INVOICENO = "
        strSQL = strSQL & "(SELECT MAX(T_INV_HD_TB.INVOICENO) AS IVNO "
        strSQL = strSQL & "FROM T_INV_HD_TB "
        strSQL = strSQL & "WHERE T_INV_HD_TB.HEADTITLE Like 'INVOICE%' "
        strSQL = strSQL & "AND T_INV_HD_TB.OLD_INVNO = '" & strinv & "' "
        strSQL = strSQL & "AND T_INV_HD_TB.BOOKINGNO = '" & bkgno & "' "
        strSQL = strSQL & "AND T_INV_HD_TB.ORG_INVOICENO IS NULL "
        strSQL = strSQL & ") "
        strSQL = strSQL & "AND T_INV_HD_TB.ALLOUTSTAMP IS NULL "



        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()


        '結果を取り出す 
        While (dataread.Read())
            h01 = Trim(Convert.ToString(dataread("CNT")))        '客先目
        End While


        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()

        If h01 = 0 Then

            errlrmsg01 = "一括処理解除と作成済み書類削除が必要"

            'エラー内容登録
            strSQL = ""
            strSQL = strSQL & "INSERT INTO T_EXL_KANRYOERROR VALUES("
            strSQL = strSQL & " '" & strinv & "', "
            strSQL = strSQL & " '" & bkgno & "', "
            strSQL = strSQL & " '" & errlrmsg01 & "', "
            strSQL = strSQL & " '書類作成済み01', "
            strSQL = strSQL & " '', "
            strSQL = strSQL & " '', "
            strSQL = strSQL & " '', "
            strSQL = strSQL & " '' "
            strSQL = strSQL & ") "

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


    Private Sub check004(strinv As String, bkgno As String, eflg As Long, strcst As String)
        'データの取得

        Dim str計上日01 As String = ""
        Dim str積出港01 As String = ""
        Dim str揚地01 As String = ""
        Dim str配送先01 As String = ""
        Dim str荷受地01 As String = ""
        Dim str配送先責任送り先01 As String = ""
        Dim strカット日01 As String = ""
        Dim str到着日01 As String = ""
        Dim str入出港日01 As String = ""
        Dim str出荷方法01 As String = ""
        Dim strVOYAGENo01 As String = ""
        Dim str船社01 As String = ""
        Dim strブッキングNo01 As String = ""
        Dim str船名01 As String = ""
        Dim getflg01 As Long

        Dim str計上日02 As String = ""
        Dim str積出港02 As String = ""
        Dim str揚地02 As String = ""
        Dim str配送先02 As String = ""
        Dim str荷受地02 As String = ""
        Dim str配送先責任送り先02 As String = ""
        Dim strカット日02 As String = ""
        Dim str到着日02 As String = ""
        Dim str入出港日02 As String = ""
        Dim str出荷方法02 As String = ""
        Dim strVOYAGENo02 As String = ""
        Dim str船社02 As String = ""
        Dim strブッキングNo02 As String = ""
        Dim str船名02 As String = ""
        Dim getflg02 As Long

        Dim str客コード02 As String = ""

        Dim i As Long
        Dim flg As Long
        Dim flg2 As Long

        Dim strhankai As String = ""


        Dim strFINALDN01 As String = ""
        Dim strFINALDA01 As String = ""
        Dim strCNEESIN01 As String = ""

        Dim strCNEESIA01 As String = ""
        Dim strPODSI01 As String = ""
        Dim strNOTYSI01 As String = ""

        Dim strFINALDN02 As String = ""
        Dim strFINALDA02 As String = ""
        Dim strCNEESIN02 As String = ""

        Dim strCNEESIA02 As String = ""
        Dim strPODSI02 As String = ""
        Dim strNOTYSI02 As String = ""

        Dim errlrmsg01 As String = ""
        Dim errlrmsg02 As String = ""
        Dim errlrmsg03 As String = ""
        Dim errlrmsg04 As String = ""
        Dim errlrmsg05 As String = ""
        Dim errlrmsg06 As String = ""
        Dim errlrmsg07 As String = ""
        Dim errlrmsg08 As String = ""



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


        Call check005(str計上日01, str積出港01, str揚地01, str配送先01, str荷受地01, str配送先責任送り先01, strカット日01, str到着日01, str入出港日01, str出荷方法01, strVOYAGENo01, str船社01, strブッキングNo01, str船名01, getflg01, strinv)
        Call check006(str計上日02, str積出港02, str揚地02, str配送先02, str荷受地02, str配送先責任送り先02, strカット日02, str到着日02, str入出港日02, str出荷方法02, strVOYAGENo02, str船社02, strブッキングNo02, str船名02, getflg02, strinv, str客コード02, strFINALDN02, strFINALDA02, strCNEESIN02, strCNEESIA02, strPODSI02, strNOTYSI02)
        Call check007(strFINALDN01, strFINALDA01, strCNEESIN01, strCNEESIA01, strPODSI01, strNOTYSI01, str客コード02)


        If getflg01 = 1 And getflg02 = 1 Then

            errlrmsg01 = "0" '"両方あり"

            If str計上日01 = str計上日02 Then
            Else
                errlrmsg02 = "相違：" & vbLf & "B:" & str計上日01 & vbLf & " vs " & vbLf & "I:" & str計上日02

                'エラー内容登録
                strSQL = ""
                strSQL = strSQL & "INSERT INTO T_EXL_KANRYOERROR VALUES("
                strSQL = strSQL & " '" & strinv & "', "
                strSQL = strSQL & " '" & bkgno & "', "
                strSQL = strSQL & " '" & errlrmsg02 & "', "
                strSQL = strSQL & " 'Sailing On/About(計上日)', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '' "
                strSQL = strSQL & ") "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

                flg = 1
            End If


            '全角半角、改行
            If InStr(str計上日01, vbCr) + InStr(str計上日01, vbLf) + InStr(str計上日01, vbCrLf) > 0 Then
                errlrmsg03 = "ブッキングシート_計上日_改行 "

                'エラー内容登録
                strSQL = ""
                strSQL = strSQL & "INSERT INTO T_EXL_KANRYOERROR VALUES("
                strSQL = strSQL & " '" & strinv & "', "
                strSQL = strSQL & " '" & bkgno & "', "
                strSQL = strSQL & " '" & errlrmsg03 & "', "
                strSQL = strSQL & " 'Sailing On/About(計上日)', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '' "
                strSQL = strSQL & ") "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

            End If

            If isOneByteChar(str計上日01) Then
            Else
                'If Len(str計上日01) <> Len(StrConv(str計上日01, VbStrConv.Wide)) Then
                errlrmsg04 = "ブッキングシート_計上日_全角 "

                'エラー内容登録
                strSQL = ""
                strSQL = strSQL & "INSERT INTO T_EXL_KANRYOERROR VALUES("
                strSQL = strSQL & " '" & strinv & "', "
                strSQL = strSQL & " '" & bkgno & "', "
                strSQL = strSQL & " '" & errlrmsg04 & "', "
                strSQL = strSQL & " 'Sailing On/About(計上日)', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '' "
                strSQL = strSQL & ") "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

            End If

            '全角半角、改行
            If InStr(str計上日02, vbCr) + InStr(str計上日02, vbLf) + InStr(str計上日02, vbCrLf) > 0 Then
                errlrmsg03 = "イントラ_計上日_改行 "

                'エラー内容登録
                strSQL = ""
                strSQL = strSQL & "INSERT INTO T_EXL_KANRYOERROR VALUES("
                strSQL = strSQL & " '" & strinv & "', "
                strSQL = strSQL & " '" & bkgno & "', "
                strSQL = strSQL & " '" & errlrmsg03 & "', "
                strSQL = strSQL & " 'Sailing On/About(計上日)', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '' "
                strSQL = strSQL & ") "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

            End If

            If isOneByteChar(str計上日02) Then
            Else
                'If Len(str計上日02) <> Len(StrConv(str計上日02, VbStrConv.Narrow)) Then
                errlrmsg04 = "イントラ_計上日_全角 "

                'エラー内容登録
                strSQL = ""
                strSQL = strSQL & "INSERT INTO T_EXL_KANRYOERROR VALUES("
                strSQL = strSQL & " '" & strinv & "', "
                strSQL = strSQL & " '" & bkgno & "', "
                strSQL = strSQL & " '" & errlrmsg04 & "', "
                strSQL = strSQL & " 'Sailing On/About(計上日)', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '' "
                strSQL = strSQL & ") "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

            End If


            If str積出港01 = str積出港02 Then
            Else
                errlrmsg02 = "相違：" & vbLf & "B:" & str積出港01 & vbLf & " vs " & vbLf & "I:" & str積出港02

                'エラー内容登録
                strSQL = ""
                strSQL = strSQL & "INSERT INTO T_EXL_KANRYOERROR VALUES("
                strSQL = strSQL & " '" & strinv & "', "
                strSQL = strSQL & " '" & bkgno & "', "
                strSQL = strSQL & " '" & errlrmsg02 & "', "
                strSQL = strSQL & " 'Port of Loading(積出港)', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '' "
                strSQL = strSQL & ") "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

                flg = 1
            End If


            '全角半角、改行
            If InStr(str積出港01, vbCr) + InStr(str積出港01, vbLf) + InStr(str積出港01, vbCrLf) > 0 Then
                errlrmsg03 = "ブッキングシート_積出港_改行 "

                'エラー内容登録
                strSQL = ""
                strSQL = strSQL & "INSERT INTO T_EXL_KANRYOERROR VALUES("
                strSQL = strSQL & " '" & strinv & "', "
                strSQL = strSQL & " '" & bkgno & "', "
                strSQL = strSQL & " '" & errlrmsg03 & "', "
                strSQL = strSQL & " 'Port of Loading(積出港)', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '' "
                strSQL = strSQL & ") "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

            End If

            If isOneByteChar(str積出港01) Then
            Else
                'If Len(str積出港01) <> Len(StrConv(str積出港01, VbStrConv.Narrow)) Then
                errlrmsg04 = "ブッキングシート_積出港_全角 "

                'エラー内容登録
                strSQL = ""
                strSQL = strSQL & "INSERT INTO T_EXL_KANRYOERROR VALUES("
                strSQL = strSQL & " '" & strinv & "', "
                strSQL = strSQL & " '" & bkgno & "', "
                strSQL = strSQL & " '" & errlrmsg04 & "', "
                strSQL = strSQL & " 'Port of Loading(積出港)', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '' "
                strSQL = strSQL & ") "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

            End If

            '全角半角、改行
            If InStr(str積出港02, vbCr) + InStr(str積出港02, vbLf) + InStr(str積出港02, vbCrLf) > 0 Then
                errlrmsg03 = "イントラ_積出港_改行 "

                'エラー内容登録
                strSQL = ""
                strSQL = strSQL & "INSERT INTO T_EXL_KANRYOERROR VALUES("
                strSQL = strSQL & " '" & strinv & "', "
                strSQL = strSQL & " '" & bkgno & "', "
                strSQL = strSQL & " '" & errlrmsg03 & "', "
                strSQL = strSQL & " 'Port of Loading(積出港)', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '' "
                strSQL = strSQL & ") "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

            End If

            If isOneByteChar(str積出港02) Then
            Else
                'If Len(str積出港02) <> Len(StrConv(str積出港02, VbStrConv.Narrow)) Then
                errlrmsg04 = "イントラ_積出港_全角 "

                'エラー内容登録
                strSQL = ""
                strSQL = strSQL & "INSERT INTO T_EXL_KANRYOERROR VALUES("
                strSQL = strSQL & " '" & strinv & "', "
                strSQL = strSQL & " '" & bkgno & "', "
                strSQL = strSQL & " '" & errlrmsg04 & "', "
                strSQL = strSQL & " 'Port of Loading(積出港)', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '' "
                strSQL = strSQL & ") "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

            End If



            If str揚地01 Like str揚地02 & "*" Then
            Else
                errlrmsg02 = "相違：" & vbLf & "B:" & str揚地01 & vbLf & " vs " & vbLf & "I:" & str揚地02

                'エラー内容登録
                strSQL = ""
                strSQL = strSQL & "INSERT INTO T_EXL_KANRYOERROR VALUES("
                strSQL = strSQL & " '" & strinv & "', "
                strSQL = strSQL & " '" & bkgno & "', "
                strSQL = strSQL & " '" & errlrmsg02 & "', "
                strSQL = strSQL & " 'Port of Discharge(揚地)', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '' "
                strSQL = strSQL & ") "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

                flg = 1
            End If



            '全角半角、改行
            If InStr(str揚地01, vbCr) + InStr(str揚地01, vbLf) + InStr(str揚地01, vbCrLf) > 0 Then
                errlrmsg03 = "ブッキングシート_揚地_改行 "

                'エラー内容登録
                strSQL = ""
                strSQL = strSQL & "INSERT INTO T_EXL_KANRYOERROR VALUES("
                strSQL = strSQL & " '" & strinv & "', "
                strSQL = strSQL & " '" & bkgno & "', "
                strSQL = strSQL & " '" & errlrmsg03 & "', "
                strSQL = strSQL & " 'Port of Discharge(揚地)', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '' "
                strSQL = strSQL & ") "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

            End If

            If isOneByteChar(str揚地01) Then
            Else
                'If Len(str揚地01) <> Len(StrConv(str揚地01, VbStrConv.Narrow)) Then
                errlrmsg04 = "ブッキングシート_揚地_全角 "

                'エラー内容登録
                strSQL = ""
                strSQL = strSQL & "INSERT INTO T_EXL_KANRYOERROR VALUES("
                strSQL = strSQL & " '" & strinv & "', "
                strSQL = strSQL & " '" & bkgno & "', "
                strSQL = strSQL & " '" & errlrmsg04 & "', "
                strSQL = strSQL & " 'Port of Discharge(揚地)', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '' "
                strSQL = strSQL & ") "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

            End If

            '全角半角、改行
            If InStr(str揚地02, vbCr) + InStr(str揚地02, vbLf) + InStr(str揚地02, vbCrLf) > 0 Then
                errlrmsg03 = "イントラ_揚地_改行 "

                'エラー内容登録
                strSQL = ""
                strSQL = strSQL & "INSERT INTO T_EXL_KANRYOERROR VALUES("
                strSQL = strSQL & " '" & strinv & "', "
                strSQL = strSQL & " '" & bkgno & "', "
                strSQL = strSQL & " '" & errlrmsg03 & "', "
                strSQL = strSQL & " 'Port of Discharge(揚地)', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '' "
                strSQL = strSQL & ") "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

            End If

            If isOneByteChar(str揚地02) Then
            Else
                'If Len(str揚地02) <> Len(StrConv(str揚地02, VbStrConv.Narrow)) Then
                errlrmsg04 = "イントラ_揚地_全角 "

                'エラー内容登録
                strSQL = ""
                strSQL = strSQL & "INSERT INTO T_EXL_KANRYOERROR VALUES("
                strSQL = strSQL & " '" & strinv & "', "
                strSQL = strSQL & " '" & bkgno & "', "
                strSQL = strSQL & " '" & errlrmsg04 & "', "
                strSQL = strSQL & " 'Port of Discharge(揚地)', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '' "
                strSQL = strSQL & ") "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

            End If


            If str配送先01 Like str配送先02 & "*" Then
            Else
                errlrmsg02 = "相違：" & vbLf & "B:" & str配送先01 & vbLf & " vs " & vbLf & "I:" & str配送先02

                'エラー内容登録
                strSQL = ""
                strSQL = strSQL & "INSERT INTO T_EXL_KANRYOERROR VALUES("
                strSQL = strSQL & " '" & strinv & "', "
                strSQL = strSQL & " '" & bkgno & "', "
                strSQL = strSQL & " '" & errlrmsg02 & "', "
                strSQL = strSQL & " 'Place of Delivery(配送先)', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '' "
                strSQL = strSQL & ") "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

                flg = 1
            End If


            '全角半角、改行
            If InStr(str配送先01, vbCr) + InStr(str配送先01, vbLf) + InStr(str配送先01, vbCrLf) > 0 Then
                errlrmsg03 = "ブッキングシート_配送先_改行 "

                'エラー内容登録
                strSQL = ""
                strSQL = strSQL & "INSERT INTO T_EXL_KANRYOERROR VALUES("
                strSQL = strSQL & " '" & strinv & "', "
                strSQL = strSQL & " '" & bkgno & "', "
                strSQL = strSQL & " '" & errlrmsg03 & "', "
                strSQL = strSQL & " 'Place of Delivery(配送先)', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '' "
                strSQL = strSQL & ") "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

            End If

            If isOneByteChar(str配送先01) Then
            Else
                'If Len(str配送先01) <> Len(StrConv(str配送先01, VbStrConv.Narrow)) Then
                errlrmsg04 = "ブッキングシート_配送先_全角 "

                'エラー内容登録
                strSQL = ""
                strSQL = strSQL & "INSERT INTO T_EXL_KANRYOERROR VALUES("
                strSQL = strSQL & " '" & strinv & "', "
                strSQL = strSQL & " '" & bkgno & "', "
                strSQL = strSQL & " '" & errlrmsg04 & "', "
                strSQL = strSQL & " 'Place of Delivery(配送先)', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '' "
                strSQL = strSQL & ") "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

            End If

            '全角半角、改行
            If InStr(str配送先02, vbCr) + InStr(str配送先02, vbLf) + InStr(str配送先02, vbCrLf) > 0 Then
                errlrmsg03 = "イントラ_配送先_改行 "

                'エラー内容登録
                strSQL = ""
                strSQL = strSQL & "INSERT INTO T_EXL_KANRYOERROR VALUES("
                strSQL = strSQL & " '" & strinv & "', "
                strSQL = strSQL & " '" & bkgno & "', "
                strSQL = strSQL & " '" & errlrmsg03 & "', "
                strSQL = strSQL & " 'Place of Delivery(配送先)', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '' "
                strSQL = strSQL & ") "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

            End If

            If isOneByteChar(str配送先02) Then
            Else
                'If Len(str配送先02) <> Len(StrConv(str配送先02, VbStrConv.Narrow)) Then
                errlrmsg04 = "イントラ_配送先_全角 "

                'エラー内容登録
                strSQL = ""
                strSQL = strSQL & "INSERT INTO T_EXL_KANRYOERROR VALUES("
                strSQL = strSQL & " '" & strinv & "', "
                strSQL = strSQL & " '" & bkgno & "', "
                strSQL = strSQL & " '" & errlrmsg04 & "', "
                strSQL = strSQL & " 'Place of Delivery(配送先)', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '' "
                strSQL = strSQL & ") "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

            End If


            If str荷受地01 = str荷受地02 Then
            Else
                errlrmsg02 = "相違：" & vbLf & "B:" & str荷受地01 & vbLf & " vs " & vbLf & "I:" & str荷受地02

                'エラー内容登録
                strSQL = ""
                strSQL = strSQL & "INSERT INTO T_EXL_KANRYOERROR VALUES("
                strSQL = strSQL & " '" & strinv & "', "
                strSQL = strSQL & " '" & bkgno & "', "
                strSQL = strSQL & " '" & errlrmsg02 & "', "
                strSQL = strSQL & " 'Place of Recipt(荷受地)', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '' "
                strSQL = strSQL & ") "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

                flg = 1
            End If


            '全角半角、改行
            If InStr(str荷受地01, vbCr) + InStr(str荷受地01, vbLf) + InStr(str荷受地01, vbCrLf) > 0 Then
                errlrmsg03 = "ブッキングシート_荷受地_改行 "

                'エラー内容登録
                strSQL = ""
                strSQL = strSQL & "INSERT INTO T_EXL_KANRYOERROR VALUES("
                strSQL = strSQL & " '" & strinv & "', "
                strSQL = strSQL & " '" & bkgno & "', "
                strSQL = strSQL & " '" & errlrmsg03 & "', "
                strSQL = strSQL & " 'Place of Recipt(荷受地)', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '' "
                strSQL = strSQL & ") "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

            End If

            If isOneByteChar(str荷受地01) Then
            Else
                'If Len(str荷受地01) <> Len(StrConv(str荷受地01, VbStrConv.Narrow)) Then
                errlrmsg04 = "ブッキングシート_荷受地_全角 "

                'エラー内容登録
                strSQL = ""
                strSQL = strSQL & "INSERT INTO T_EXL_KANRYOERROR VALUES("
                strSQL = strSQL & " '" & strinv & "', "
                strSQL = strSQL & " '" & bkgno & "', "
                strSQL = strSQL & " '" & errlrmsg04 & "', "
                strSQL = strSQL & " 'Place of Recipt(荷受地)', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '' "
                strSQL = strSQL & ") "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

            End If

            '全角半角、改行
            If InStr(str荷受地02, vbCr) + InStr(str荷受地02, vbLf) + InStr(str荷受地02, vbCrLf) > 0 Then
                errlrmsg03 = "イントラ_荷受地_改行 "

                'エラー内容登録
                strSQL = ""
                strSQL = strSQL & "INSERT INTO T_EXL_KANRYOERROR VALUES("
                strSQL = strSQL & " '" & strinv & "', "
                strSQL = strSQL & " '" & bkgno & "', "
                strSQL = strSQL & " '" & errlrmsg03 & "', "
                strSQL = strSQL & " 'Place of Recipt(荷受地)', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '' "
                strSQL = strSQL & ") "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

            End If

            If isOneByteChar(str荷受地02) Then
            Else
                'If Len(str荷受地02) <> Len(StrConv(str荷受地02, VbStrConv.Narrow)) Then
                errlrmsg04 = "イントラ_荷受地_全角 "

                'エラー内容登録
                strSQL = ""
                strSQL = strSQL & "INSERT INTO T_EXL_KANRYOERROR VALUES("
                strSQL = strSQL & " '" & strinv & "', "
                strSQL = strSQL & " '" & bkgno & "', "
                strSQL = strSQL & " '" & errlrmsg04 & "', "
                strSQL = strSQL & " 'Place of Recipt(荷受地)', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '' "
                strSQL = strSQL & ") "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

            End If


            If strカット日01 = strカット日02 Then
            Else
                errlrmsg02 = "相違：" & vbLf & "B:" & strカット日01 & vbLf & " vs " & vbLf & "I:" & strカット日02

                'エラー内容登録
                strSQL = ""
                strSQL = strSQL & "INSERT INTO T_EXL_KANRYOERROR VALUES("
                strSQL = strSQL & " '" & strinv & "', "
                strSQL = strSQL & " '" & bkgno & "', "
                strSQL = strSQL & " '" & errlrmsg02 & "', "
                strSQL = strSQL & " 'CUT日', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '' "
                strSQL = strSQL & ") "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

                flg = 1
            End If


            '全角半角、改行
            If InStr(strカット日01, vbCr) + InStr(strカット日01, vbLf) + InStr(strカット日01, vbCrLf) > 0 Then
                errlrmsg03 = "ブッキングシート_カット日_改行 "

                'エラー内容登録
                strSQL = ""
                strSQL = strSQL & "INSERT INTO T_EXL_KANRYOERROR VALUES("
                strSQL = strSQL & " '" & strinv & "', "
                strSQL = strSQL & " '" & bkgno & "', "
                strSQL = strSQL & " '" & errlrmsg03 & "', "
                strSQL = strSQL & " 'CUT日', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '' "
                strSQL = strSQL & ") "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

            End If

            If isOneByteChar(strカット日01) Then
            Else
                'If Len(strカット日01) <> Len(StrConv(strカット日01, VbStrConv.Narrow)) Then
                errlrmsg04 = "ブッキングシート_カット日_全角 "

                'エラー内容登録
                strSQL = ""
                strSQL = strSQL & "INSERT INTO T_EXL_KANRYOERROR VALUES("
                strSQL = strSQL & " '" & strinv & "', "
                strSQL = strSQL & " '" & bkgno & "', "
                strSQL = strSQL & " '" & errlrmsg04 & "', "
                strSQL = strSQL & " 'CUT日', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '' "
                strSQL = strSQL & ") "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

            End If

            '全角半角、改行
            If InStr(strカット日02, vbCr) + InStr(strカット日02, vbLf) + InStr(strカット日02, vbCrLf) > 0 Then
                errlrmsg03 = "イントラ_カット日_改行 "

                'エラー内容登録
                strSQL = ""
                strSQL = strSQL & "INSERT INTO T_EXL_KANRYOERROR VALUES("
                strSQL = strSQL & " '" & strinv & "', "
                strSQL = strSQL & " '" & bkgno & "', "
                strSQL = strSQL & " '" & errlrmsg03 & "', "
                strSQL = strSQL & " 'CUT日', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '' "
                strSQL = strSQL & ") "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

            End If

            If isOneByteChar(strカット日02) Then
            Else
                'If Len(strカット日02) <> Len(StrConv(strカット日02, VbStrConv.Narrow)) Then
                errlrmsg04 = "イントラ_カット日_全角 "

                'エラー内容登録
                strSQL = ""
                strSQL = strSQL & "INSERT INTO T_EXL_KANRYOERROR VALUES("
                strSQL = strSQL & " '" & strinv & "', "
                strSQL = strSQL & " '" & bkgno & "', "
                strSQL = strSQL & " '" & errlrmsg04 & "', "
                strSQL = strSQL & " 'CUT日', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '' "
                strSQL = strSQL & ") "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

            End If



            If str到着日01 = str到着日02 Then
            Else
                errlrmsg02 = "相違：" & vbLf & "B:" & str到着日01 & vbLf & " vs " & vbLf & "I:" & str到着日02

                'エラー内容登録
                strSQL = ""
                strSQL = strSQL & "INSERT INTO T_EXL_KANRYOERROR VALUES("
                strSQL = strSQL & " '" & strinv & "', "
                strSQL = strSQL & " '" & bkgno & "', "
                strSQL = strSQL & " '" & errlrmsg02 & "', "
                strSQL = strSQL & " '到着日', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '' "
                strSQL = strSQL & ") "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

                flg = 1
            End If


            '全角半角、改行
            If InStr(str到着日01, vbCr) + InStr(str到着日01, vbLf) + InStr(str到着日01, vbCrLf) > 0 Then
                errlrmsg03 = "ブッキングシート_到着日_改行 "

                'エラー内容登録
                strSQL = ""
                strSQL = strSQL & "INSERT INTO T_EXL_KANRYOERROR VALUES("
                strSQL = strSQL & " '" & strinv & "', "
                strSQL = strSQL & " '" & bkgno & "', "
                strSQL = strSQL & " '" & errlrmsg03 & "', "
                strSQL = strSQL & " '到着日', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '' "
                strSQL = strSQL & ") "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

            End If

            If isOneByteChar(str到着日01) Then
            Else
                'If Len(str到着日01) <> Len(StrConv(str到着日01, VbStrConv.Narrow)) Then
                errlrmsg04 = "ブッキングシート_到着日_全角 "

                'エラー内容登録
                strSQL = ""
                strSQL = strSQL & "INSERT INTO T_EXL_KANRYOERROR VALUES("
                strSQL = strSQL & " '" & strinv & "', "
                strSQL = strSQL & " '" & bkgno & "', "
                strSQL = strSQL & " '" & errlrmsg04 & "', "
                strSQL = strSQL & " '到着日', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '' "
                strSQL = strSQL & ") "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

            End If

            '全角半角、改行
            If InStr(str到着日02, vbCr) + InStr(str到着日02, vbLf) + InStr(str到着日02, vbCrLf) > 0 Then
                errlrmsg03 = "イントラ_到着日_改行 "

                'エラー内容登録
                strSQL = ""
                strSQL = strSQL & "INSERT INTO T_EXL_KANRYOERROR VALUES("
                strSQL = strSQL & " '" & strinv & "', "
                strSQL = strSQL & " '" & bkgno & "', "
                strSQL = strSQL & " '" & errlrmsg03 & "', "
                strSQL = strSQL & " '到着日', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '' "
                strSQL = strSQL & ") "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

            End If

            If isOneByteChar(str到着日02) Then
            Else
                'If Len(str到着日02) <> Len(StrConv(str到着日02, VbStrConv.Narrow)) Then
                errlrmsg04 = "イントラ_到着日_全角 "

                'エラー内容登録
                strSQL = ""
                strSQL = strSQL & "INSERT INTO T_EXL_KANRYOERROR VALUES("
                strSQL = strSQL & " '" & strinv & "', "
                strSQL = strSQL & " '" & bkgno & "', "
                strSQL = strSQL & " '" & errlrmsg04 & "', "
                strSQL = strSQL & " '到着日', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '' "
                strSQL = strSQL & ") "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

            End If


            If str入出港日01 = str入出港日02 Then
            Else
                errlrmsg02 = "相違：" & vbLf & "B:" & str入出港日01 & vbLf & " vs " & vbLf & "I:" & str入出港日02

                'エラー内容登録
                strSQL = ""
                strSQL = strSQL & "INSERT INTO T_EXL_KANRYOERROR VALUES("
                strSQL = strSQL & " '" & strinv & "', "
                strSQL = strSQL & " '" & bkgno & "', "
                strSQL = strSQL & " '" & errlrmsg02 & "', "
                strSQL = strSQL & " '入出港日', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '' "
                strSQL = strSQL & ") "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

                flg = 1
            End If


            '全角半角、改行
            If InStr(str入出港日01, vbCr) + InStr(str入出港日01, vbLf) + InStr(str入出港日01, vbCrLf) > 0 Then
                errlrmsg03 = "ブッキングシート_入出港日_改行 "

                'エラー内容登録
                strSQL = ""
                strSQL = strSQL & "INSERT INTO T_EXL_KANRYOERROR VALUES("
                strSQL = strSQL & " '" & strinv & "', "
                strSQL = strSQL & " '" & bkgno & "', "
                strSQL = strSQL & " '" & errlrmsg03 & "', "
                strSQL = strSQL & " '入出港日', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '' "
                strSQL = strSQL & ") "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

            End If

            If isOneByteChar(str入出港日01) Then
            Else
                'If Len(str入出港日01) <> Len(StrConv(str入出港日01, VbStrConv.Narrow)) Then
                errlrmsg04 = "ブッキングシート_入出港日_全角 "

                'エラー内容登録
                strSQL = ""
                strSQL = strSQL & "INSERT INTO T_EXL_KANRYOERROR VALUES("
                strSQL = strSQL & " '" & strinv & "', "
                strSQL = strSQL & " '" & bkgno & "', "
                strSQL = strSQL & " '" & errlrmsg04 & "', "
                strSQL = strSQL & " '入出港日', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '' "
                strSQL = strSQL & ") "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

            End If

            '全角半角、改行
            If InStr(str入出港日02, vbCr) + InStr(str入出港日02, vbLf) + InStr(str入出港日02, vbCrLf) > 0 Then
                errlrmsg03 = "イントラ_入出港日_改行 "

                'エラー内容登録
                strSQL = ""
                strSQL = strSQL & "INSERT INTO T_EXL_KANRYOERROR VALUES("
                strSQL = strSQL & " '" & strinv & "', "
                strSQL = strSQL & " '" & bkgno & "', "
                strSQL = strSQL & " '" & errlrmsg03 & "', "
                strSQL = strSQL & " '入出港日', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '' "
                strSQL = strSQL & ") "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

            End If

            If isOneByteChar(str入出港日02) Then
            Else
                'If Len(str入出港日02) <> Len(StrConv(str入出港日02, VbStrConv.Narrow)) Then
                errlrmsg04 = "イントラ_入出港日_全角 "

                'エラー内容登録
                strSQL = ""
                strSQL = strSQL & "INSERT INTO T_EXL_KANRYOERROR VALUES("
                strSQL = strSQL & " '" & strinv & "', "
                strSQL = strSQL & " '" & bkgno & "', "
                strSQL = strSQL & " '" & errlrmsg04 & "', "
                strSQL = strSQL & " '入出港日', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '' "
                strSQL = strSQL & ") "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

            End If

            If strVOYAGENo01 = strVOYAGENo02 Then
            Else
                errlrmsg02 = "相違：" & vbLf & "B:" & strVOYAGENo01 & vbLf & " vs " & vbLf & "I:" & strVOYAGENo02

                'エラー内容登録
                strSQL = ""
                strSQL = strSQL & "INSERT INTO T_EXL_KANRYOERROR VALUES("
                strSQL = strSQL & " '" & strinv & "', "
                strSQL = strSQL & " '" & bkgno & "', "
                strSQL = strSQL & " '" & errlrmsg02 & "', "
                strSQL = strSQL & " 'VoyageNo', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '' "
                strSQL = strSQL & ") "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

                flg = 1
            End If

            '全角半角、改行
            If InStr(strVOYAGENo01, vbCr) + InStr(strVOYAGENo01, vbLf) + InStr(strVOYAGENo01, vbCrLf) > 0 Then
                errlrmsg03 = "ブッキングシート_VOYAGENo_改行 "

                'エラー内容登録
                strSQL = ""
                strSQL = strSQL & "INSERT INTO T_EXL_KANRYOERROR VALUES("
                strSQL = strSQL & " '" & strinv & "', "
                strSQL = strSQL & " '" & bkgno & "', "
                strSQL = strSQL & " '" & errlrmsg03 & "', "
                strSQL = strSQL & " 'VoyageNo', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '' "
                strSQL = strSQL & ") "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

            End If

            If isOneByteChar(strVOYAGENo01) Then
            Else
                'If Len(strVOYAGENo01) <> Len(StrConv(strVOYAGENo01, VbStrConv.Narrow)) Then
                errlrmsg04 = "ブッキングシート_VOYAGENo_全角 "

                'エラー内容登録
                strSQL = ""
                strSQL = strSQL & "INSERT INTO T_EXL_KANRYOERROR VALUES("
                strSQL = strSQL & " '" & strinv & "', "
                strSQL = strSQL & " '" & bkgno & "', "
                strSQL = strSQL & " '" & errlrmsg04 & "', "
                strSQL = strSQL & " 'VoyageNo', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '' "
                strSQL = strSQL & ") "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

            End If

            '全角半角、改行
            If InStr(strVOYAGENo02, vbCr) + InStr(strVOYAGENo02, vbLf) + InStr(strVOYAGENo02, vbCrLf) > 0 Then
                errlrmsg03 = "イントラ_VOYAGENo_改行 "

                'エラー内容登録
                strSQL = ""
                strSQL = strSQL & "INSERT INTO T_EXL_KANRYOERROR VALUES("
                strSQL = strSQL & " '" & strinv & "', "
                strSQL = strSQL & " '" & bkgno & "', "
                strSQL = strSQL & " '" & errlrmsg03 & "', "
                strSQL = strSQL & " 'VoyageNo', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '' "
                strSQL = strSQL & ") "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

            End If

            If isOneByteChar(strVOYAGENo02) Then
            Else
                'If Len(strVOYAGENo02) <> Len(StrConv(strVOYAGENo02, VbStrConv.Narrow)) Then
                errlrmsg04 = "イントラ_VOYAGENo_全角 "

                'エラー内容登録
                strSQL = ""
                strSQL = strSQL & "INSERT INTO T_EXL_KANRYOERROR VALUES("
                strSQL = strSQL & " '" & strinv & "', "
                strSQL = strSQL & " '" & bkgno & "', "
                strSQL = strSQL & " '" & errlrmsg04 & "', "
                strSQL = strSQL & " 'VoyageNo', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '' "
                strSQL = strSQL & ") "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

            End If

            If str船社01 = str船社02 Then
            Else
                errlrmsg02 = "相違：" & vbLf & "B:" & str船社01 & vbLf & " vs " & vbLf & "I:" & str船社02

                'エラー内容登録
                strSQL = ""
                strSQL = strSQL & "INSERT INTO T_EXL_KANRYOERROR VALUES("
                strSQL = strSQL & " '" & strinv & "', "
                strSQL = strSQL & " '" & bkgno & "', "
                strSQL = strSQL & " '" & errlrmsg02 & "', "
                strSQL = strSQL & " '  船社 ', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '' "
                strSQL = strSQL & ") "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

                flg = 1
            End If


            '全角半角、改行
            If InStr(str船社01, vbCr) + InStr(str船社01, vbLf) + InStr(str船社01, vbCrLf) > 0 Then
                errlrmsg03 = "ブッキングシート_船社_改行 "

                'エラー内容登録
                strSQL = ""
                strSQL = strSQL & "INSERT INTO T_EXL_KANRYOERROR VALUES("
                strSQL = strSQL & " '" & strinv & "', "
                strSQL = strSQL & " '" & bkgno & "', "
                strSQL = strSQL & " '" & errlrmsg03 & "', "
                strSQL = strSQL & " '船社', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '' "
                strSQL = strSQL & ") "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

            End If


            '全角半角、改行
            If InStr(str船社02, vbCr) + InStr(str船社02, vbLf) + InStr(str船社02, vbCrLf) > 0 Then
                errlrmsg03 = "イントラ_船社_改行 "

                'エラー内容登録
                strSQL = ""
                strSQL = strSQL & "INSERT INTO T_EXL_KANRYOERROR VALUES("
                strSQL = strSQL & " '" & strinv & "', "
                strSQL = strSQL & " '" & bkgno & "', "
                strSQL = strSQL & " '" & errlrmsg03 & "', "
                strSQL = strSQL & " '船社', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '' "
                strSQL = strSQL & ") "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

            End If

            If strブッキングNo01 = strブッキングNo02 Then
            Else
                errlrmsg02 = "相違：" & vbLf & "B:" & strブッキングNo01 & vbLf & " vs " & vbLf & "I:" & strブッキングNo02

                'エラー内容登録
                strSQL = ""
                strSQL = strSQL & "INSERT INTO T_EXL_KANRYOERROR VALUES("
                strSQL = strSQL & " '" & strinv & "', "
                strSQL = strSQL & " '" & bkgno & "', "
                strSQL = strSQL & " '" & errlrmsg02 & "', "
                strSQL = strSQL & " 'BookingNo', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '' "
                strSQL = strSQL & ") "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

                flg = 1
            End If


            '全角半角、改行
            If InStr(strブッキングNo01, vbCr) + InStr(strブッキングNo01, vbLf) + InStr(strブッキングNo01, vbCrLf) > 0 Then
                errlrmsg03 = "ブッキングシート_ブッキングNo_改行 "

                'エラー内容登録
                strSQL = ""
                strSQL = strSQL & "INSERT INTO T_EXL_KANRYOERROR VALUES("
                strSQL = strSQL & " '" & strinv & "', "
                strSQL = strSQL & " '" & bkgno & "', "
                strSQL = strSQL & " '" & errlrmsg03 & "', "
                strSQL = strSQL & " 'BookingNo', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '' "
                strSQL = strSQL & ") "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

            End If

            If isOneByteChar(strブッキングNo01) Then
            Else
                'If Len(strブッキングNo01) <> Len(StrConv(strブッキングNo01, VbStrConv.Narrow)) Then
                errlrmsg04 = "ブッキングシート_ブッキングNo_全角 "

                'エラー内容登録
                strSQL = ""
                strSQL = strSQL & "INSERT INTO T_EXL_KANRYOERROR VALUES("
                strSQL = strSQL & " '" & strinv & "', "
                strSQL = strSQL & " '" & bkgno & "', "
                strSQL = strSQL & " '" & errlrmsg04 & "', "
                strSQL = strSQL & " 'BookingNo', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '' "
                strSQL = strSQL & ") "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

            End If

            '全角半角、改行
            If InStr(strブッキングNo02, vbCr) + InStr(strブッキングNo02, vbLf) + InStr(strブッキングNo02, vbCrLf) > 0 Then
                errlrmsg03 = "イントラ_ブッキングNo_改行 "

                'エラー内容登録
                strSQL = ""
                strSQL = strSQL & "INSERT INTO T_EXL_KANRYOERROR VALUES("
                strSQL = strSQL & " '" & strinv & "', "
                strSQL = strSQL & " '" & bkgno & "', "
                strSQL = strSQL & " '" & errlrmsg03 & "', "
                strSQL = strSQL & " 'BookingNo', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '' "
                strSQL = strSQL & ") "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

            End If

            If isOneByteChar(strブッキングNo02) Then
            Else
                'If Len(strブッキングNo02) <> Len(StrConv(strブッキングNo02, VbStrConv.Narrow)) Then
                errlrmsg04 = "イントラ_ブッキングNo_全角 "

                'エラー内容登録
                strSQL = ""
                strSQL = strSQL & "INSERT INTO T_EXL_KANRYOERROR VALUES("
                strSQL = strSQL & " '" & strinv & "', "
                strSQL = strSQL & " '" & bkgno & "', "
                strSQL = strSQL & " '" & errlrmsg04 & "', "
                strSQL = strSQL & " 'BookingNo', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '' "
                strSQL = strSQL & ") "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

            End If


            If str船名01 = str船名02 Then
            Else
                errlrmsg02 = "相違：" & vbLf & "B:" & str船名01 & vbLf & " vs " & vbLf & "I:" & str船名02

                'エラー内容登録
                strSQL = ""
                strSQL = strSQL & "INSERT INTO T_EXL_KANRYOERROR VALUES("
                strSQL = strSQL & " '" & strinv & "', "
                strSQL = strSQL & " '" & bkgno & "', "
                strSQL = strSQL & " '" & errlrmsg02 & "', "
                strSQL = strSQL & " '船名', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '' "
                strSQL = strSQL & ") "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

                flg = 1
            End If

            '全角半角、改行
            If InStr(str船名01, vbCr) + InStr(str船名01, vbLf) + InStr(str船名01, vbCrLf) > 0 Then
                errlrmsg03 = "ブッキングシート_船名_改行 "

                'エラー内容登録
                strSQL = ""
                strSQL = strSQL & "INSERT INTO T_EXL_KANRYOERROR VALUES("
                strSQL = strSQL & " '" & strinv & "', "
                strSQL = strSQL & " '" & bkgno & "', "
                strSQL = strSQL & " '" & errlrmsg03 & "', "
                strSQL = strSQL & " '船名', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '' "
                strSQL = strSQL & ") "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

            End If


            If isOneByteChar(str船名01) Then
            Else
                'If Len(str船名01) <> Len(StrConv(str船名01, VbStrConv.Narrow)) Then

                errlrmsg04 = "ブッキングシート_船名_全角 "

                'エラー内容登録
                strSQL = ""
                strSQL = strSQL & "INSERT INTO T_EXL_KANRYOERROR VALUES("
                strSQL = strSQL & " '" & strinv & "', "
                strSQL = strSQL & " '" & bkgno & "', "
                strSQL = strSQL & " '" & errlrmsg04 & "', "
                strSQL = strSQL & " '船名', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '' "
                strSQL = strSQL & ") "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

            End If

            '全角半角、改行
            If InStr(str船名02, vbCr) + InStr(str船名02, vbLf) + InStr(str船名02, vbCrLf) > 0 Then
                errlrmsg03 = "イントラ_船名_改行 "

                'エラー内容登録
                strSQL = ""
                strSQL = strSQL & "INSERT INTO T_EXL_KANRYOERROR VALUES("
                strSQL = strSQL & " '" & strinv & "', "
                strSQL = strSQL & " '" & bkgno & "', "
                strSQL = strSQL & " '" & errlrmsg03 & "', "
                strSQL = strSQL & " '船名', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '' "
                strSQL = strSQL & ") "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

            End If

            If isOneByteChar(str船名02) Then
            Else
                'If Len(str船名02) <> Len(StrConv(str船名02, VbStrConv.Narrow)) Then
                errlrmsg04 = "イントラ_船名_全角 "

                'エラー内容登録
                strSQL = ""
                strSQL = strSQL & "INSERT INTO T_EXL_KANRYOERROR VALUES("
                strSQL = strSQL & " '" & strinv & "', "
                strSQL = strSQL & " '" & bkgno & "', "
                strSQL = strSQL & " '" & errlrmsg04 & "', "
                strSQL = strSQL & " '船名', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '' "
                strSQL = strSQL & ") "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

            End If


            If Replace(Replace(strFINALDN01, vbCrLf, ""), vbLf, "") = Replace(Replace(strFINALDN02, vbCrLf, ""), vbLf, "") Then
            Else
                errlrmsg02 = "相違：" & vbLf & "M:" & strFINALDN01 & vbLf & " vs " & vbLf & "I:" & strFINALDN02

                'エラー内容登録
                strSQL = ""
                strSQL = strSQL & "INSERT INTO T_EXL_KANRYOERROR VALUES("
                strSQL = strSQL & " '" & strinv & "', "
                strSQL = strSQL & " '" & bkgno & "', "
                strSQL = strSQL & " '" & errlrmsg02 & "', "
                strSQL = strSQL & " 'Final Destination(届先名)', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '' "
                strSQL = strSQL & ") "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

                flg = 1
            End If


            If Replace(Replace(strFINALDA01, vbCrLf, ""), vbLf, "") = Replace(Replace(strFINALDA02, vbCrLf, ""), vbLf, "") Then
            Else
                errlrmsg02 = "相違：" & vbLf & "M:" & strFINALDA01 & vbLf & " vs " & vbLf & "I:" & strFINALDA02

                'エラー内容登録
                strSQL = ""
                strSQL = strSQL & "INSERT INTO T_EXL_KANRYOERROR VALUES("
                strSQL = strSQL & " '" & strinv & "', "
                strSQL = strSQL & " '" & bkgno & "', "
                strSQL = strSQL & " '" & errlrmsg02 & "', "
                strSQL = strSQL & " 'Final Destination Address(届先住所)', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '' "
                strSQL = strSQL & ") "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

                flg = 1
            End If


            If Replace(Replace(strCNEESIN01, vbCrLf, ""), vbLf, "") = Replace(Replace(strCNEESIN02, vbCrLf, ""), vbLf, "") Then
            Else
                errlrmsg02 = "相違：" & vbLf & "M:" & strCNEESIN01 & vbLf & " vs " & vbLf & "I:" & strCNEESIN02

                'エラー内容登録
                strSQL = ""
                strSQL = strSQL & "INSERT INTO T_EXL_KANRYOERROR VALUES("
                strSQL = strSQL & " '" & strinv & "', "
                strSQL = strSQL & " '" & bkgno & "', "
                strSQL = strSQL & " '" & errlrmsg02 & "', "
                strSQL = strSQL & " 'Consignee(荷受先名)', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '' "
                strSQL = strSQL & ") "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

                flg = 1
            End If


            If Replace(Replace(strCNEESIA01, vbCrLf, ""), vbLf, "") = Replace(Replace(strCNEESIA02, vbCrLf, ""), vbLf, "") Then
            Else
                errlrmsg02 = "相違：" & vbLf & "M:" & strCNEESIA01 & vbLf & " vs " & vbLf & "I:" & strCNEESIA02

                'エラー内容登録
                strSQL = ""
                strSQL = strSQL & "INSERT INTO T_EXL_KANRYOERROR VALUES("
                strSQL = strSQL & " '" & strinv & "', "
                strSQL = strSQL & " '" & bkgno & "', "
                strSQL = strSQL & " '" & errlrmsg02 & "', "
                strSQL = strSQL & " 'ConsigneeAddress(荷受先住所)', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '' "
                strSQL = strSQL & ") "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

                flg = 1
            End If

            If Replace(Replace(strNOTYSI01, vbCrLf, ""), vbLf, "") = Replace(Replace(strNOTYSI02, vbCrLf, ""), vbLf, "") Then
            Else
                errlrmsg02 = "相違：" & vbLf & "M:" & strNOTYSI01 & vbLf & " vs " & vbLf & "I:" & strNOTYSI02

                'エラー内容登録
                strSQL = ""
                strSQL = strSQL & "INSERT INTO T_EXL_KANRYOERROR VALUES("
                strSQL = strSQL & " '" & strinv & "', "
                strSQL = strSQL & " '" & bkgno & "', "
                strSQL = strSQL & " '" & errlrmsg02 & "', "
                strSQL = strSQL & " 'Notify Address', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '' "
                strSQL = strSQL & ") "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

                flg = 1
            End If


            If DateValue(strカット日01) < DateValue(str入出港日01) Or DateValue(strカット日01) < DateValue(str計上日01) Then
            Else

                errlrmsg06 = "出港日か売上計上日がCUT日以前にあります。（ブッキングシート）"

                'エラー内容登録
                strSQL = ""
                strSQL = strSQL & "INSERT INTO T_EXL_KANRYOERROR VALUES("
                strSQL = strSQL & " '" & strinv & "', "
                strSQL = strSQL & " '" & bkgno & "', "
                strSQL = strSQL & " '" & errlrmsg06 & "', "
                strSQL = strSQL & " 'CUT日', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '' "
                strSQL = strSQL & ") "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

            End If


            If DateValue(strカット日02) < DateValue(str入出港日02) Or DateValue(strカット日02) < DateValue(str計上日02) Then
            Else

                errlrmsg06 = "出港日か売上計上日がCUT日以前にあります。（イントラ）"

                'エラー内容登録
                strSQL = ""
                strSQL = strSQL & "INSERT INTO T_EXL_KANRYOERROR VALUES("
                strSQL = strSQL & " '" & strinv & "', "
                strSQL = strSQL & " '" & bkgno & "', "
                strSQL = strSQL & " '" & errlrmsg06 & "', "
                strSQL = strSQL & " '入出港日', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '', "
                strSQL = strSQL & " '' "
                strSQL = strSQL & ") "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

            End If



        ElseIf getflg01 = 1 And getflg02 = 0 Then
            errlrmsg01 = "イントラにデータなし" '"イントラなし"
        ElseIf getflg01 = 0 And getflg02 = 1 Then
            errlrmsg01 = "BookingSheetにデータなし" '"Bookingシートなし"
        ElseIf getflg01 = 0 And getflg02 = 0 Then
            errlrmsg01 = "両方にデータなし" '"両方なし"
        End If

        If errlrmsg01 <> "0" Then
            'エラー内容登録
            strSQL = ""
            strSQL = strSQL & "INSERT INTO T_EXL_KANRYOERROR VALUES("
            strSQL = strSQL & " '" & strinv & "', "
            strSQL = strSQL & " '" & bkgno & "', "
            strSQL = strSQL & " '" & errlrmsg01 & "', "
            strSQL = strSQL & " 'データの有無判定', "
            strSQL = strSQL & " '', "
            strSQL = strSQL & " '', "
            strSQL = strSQL & " '', "
            strSQL = strSQL & " '' "
            strSQL = strSQL & ") "

            Command.CommandText = strSQL
            ' SQLの実行
            Command.ExecuteNonQuery()

        Else
            ''エラー内容登録
            'strSQL = ""
            'strSQL = strSQL & "INSERT INTO T_EXL_KANRYOERROR VALUES("
            'strSQL = strSQL & " '" & strinv & "', "
            'strSQL = strSQL & " '" & bkgno & "', "
            'strSQL = strSQL & " '" & errlrmsg01 & "', "
            'strSQL = strSQL & " 'データの有無判定', "
            'strSQL = strSQL & " '', "
            'strSQL = strSQL & " '', "
            'strSQL = strSQL & " '', "
            'strSQL = strSQL & " '' "
            'strSQL = strSQL & ") "

            'Command.CommandText = strSQL
            '' SQLの実行
            'Command.ExecuteNonQuery()
        End If


        cnn.Close()
        cnn.Dispose()
        Command.Dispose()
    End Sub

    Private Function isOneByteChar(ByVal str As String) As Boolean
        Dim byte_data As Byte() = System.Text.Encoding.GetEncoding(932).GetBytes(str)

        If byte_data.Length = str.Length Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub check005(ByRef str計上日01 As String, ByRef str積出港01 As String, ByRef str揚地01 As String, ByRef str配送先01 As String, ByRef str荷受地01 As String, ByRef str配送先責任送り先01 As String, ByRef strカット日01 As String, ByRef str到着日01 As String, ByRef str入出港日01 As String, ByRef str出荷方法01 As String, ByRef strVOYAGENo01 As String, ByRef str船社01 As String, ByRef strブッキングNo01 As String, ByRef str船名01 As String, ByRef getflg01 As String, ByVal strinv As String)

        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String = ""

        Dim strEtd As String = ""
        Dim intEtd As Integer
        Dim strEtdCut As String = ""
        Dim intCnt2 As Integer


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
        strSQL = strSQL & "SELECT * FROM  T_BOOKING "
        strSQL = strSQL & "WHERE INVOICE_NO like '%" & strinv & "%' "
        strSQL = strSQL & "and INVOICE_NO <> '' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        strinv = ""
        '結果を取り出す 
        While (dataread.Read())


            str計上日01 = Trim(Convert.ToString(dataread("ETD")))
            str積出港01 = Trim(Convert.ToString(dataread("LOADING_PORT")))
            str揚地01 = Trim(Convert.ToString(dataread("DISCHARGING_PORT")))
            str配送先01 = Trim(Convert.ToString(dataread("PLACE_OF_DELIVERY")))
            str荷受地01 = Trim(Convert.ToString(dataread("PLACE_OF_RECEIPT")))
            str配送先責任送り先01 = ""
            strカット日01 = Trim(Convert.ToString(dataread("CUT_DATE")))
            str到着日01 = Trim(Convert.ToString(dataread("ETA")))
            str入出港日01 = Trim(Convert.ToString(dataread("ETD")))
            str出荷方法01 = ""
            strVOYAGENo01 = Trim(Convert.ToString(dataread("VOYAGE_NO")))
            str船社01 = Trim(Convert.ToString(dataread("BOOK_TO")))
            strブッキングNo01 = Trim(Convert.ToString(dataread("BOOKING_NO")))
            str船名01 = Trim(Convert.ToString(dataread("VESSEL_NAME")))






            '→の場所を変数へ
            '最後の→の場所を変数へ
            intCnt2 = 0
            strEtd = ""
            intCnt2 = InStr(1, str計上日01, "→")
            If intCnt2 > 0 Then
                '→があればこちら
                Do While intCnt2 > 0
                    intEtd = intCnt2
                    intCnt2 = InStr(intCnt2 + 1, str計上日01, "→")
                Loop
                '→から右側の文字列を変数へ
                str計上日01 = Mid(str計上日01, intEtd + 1, Len(str計上日01) - intEtd)

            Else

            End If


            '→の場所を変数へ
            '最後の→の場所を変数へ
            intCnt2 = 0
            strEtd = ""
            intCnt2 = InStr(1, str計上日01, "→")
            If intCnt2 > 0 Then
                '→があればこちら
                Do While intCnt2 > 0
                    intEtd = intCnt2
                    intCnt2 = InStr(intCnt2 + 1, str計上日01, "→")
                Loop
                '→から右側の文字列を変数へ
                strEtd = Mid(str計上日01, intEtd + 1, Len(str計上日01) - intEtd)
                str計上日01 = Date_Year(Format(strEtd, "yyyy/mm/dd"))
            Else
                '→がなければこちら
                If IsDate(str計上日01) = True Then
                    str計上日01 = str計上日01
                Else
                    str計上日01 = ""
                End If
            End If



            '日付関連
            '→の場所を変数へ
            '最後の→の場所を変数へ
            intCnt2 = 0
            strEtd = ""
            intCnt2 = InStr(1, str積出港01, "→")
            If intCnt2 > 0 Then
                '→があればこちら
                Do While intCnt2 > 0
                    intEtd = intCnt2
                    intCnt2 = InStr(intCnt2 + 1, str積出港01, "→")
                Loop
                '→から右側の文字列を変数へ
                str積出港01 = Mid(str積出港01, intEtd + 1, Len(str積出港01) - intEtd)

            Else

            End If


            '→の場所を変数へ
            '最後の→の場所を変数へ
            intCnt2 = 0
            strEtd = ""
            intCnt2 = InStr(1, str揚地01, "→")
            If intCnt2 > 0 Then
                '→があればこちら
                Do While intCnt2 > 0
                    intEtd = intCnt2
                    intCnt2 = InStr(intCnt2 + 1, str揚地01, "→")
                Loop
                '→から右側の文字列を変数へ
                str揚地01 = Mid(str揚地01, intEtd + 1, Len(str揚地01) - intEtd)

            Else

            End If

            '→の場所を変数へ
            '最後の→の場所を変数へ
            intCnt2 = 0
            strEtd = ""
            intCnt2 = InStr(1, str配送先01, "→")
            If intCnt2 > 0 Then
                '→があればこちら
                Do While intCnt2 > 0
                    intEtd = intCnt2
                    intCnt2 = InStr(intCnt2 + 1, str配送先01, "→")
                Loop
                '→から右側の文字列を変数へ
                str配送先01 = Mid(str配送先01, intEtd + 1, Len(str配送先01) - intEtd)

            Else

            End If

            '→の場所を変数へ
            '最後の→の場所を変数へ
            intCnt2 = 0
            strEtd = ""
            intCnt2 = InStr(1, str荷受地01, "→")
            If intCnt2 > 0 Then
                '→があればこちら
                Do While intCnt2 > 0
                    intEtd = intCnt2
                    intCnt2 = InStr(intCnt2 + 1, str荷受地01, "→")
                Loop
                '→から右側の文字列を変数へ
                str荷受地01 = Mid(str荷受地01, intEtd + 1, Len(str荷受地01) - intEtd)

            Else

            End If







            '→の場所を変数へ
            '最後の→の場所を変数へ
            intCnt2 = 0
            strEtd = ""
            intCnt2 = InStr(1, strカット日01, "→")
            If intCnt2 > 0 Then
                '→があればこちら
                Do While intCnt2 > 0
                    intEtd = intCnt2
                    intCnt2 = InStr(intCnt2 + 1, strカット日01, "→")
                Loop
                '→から右側の文字列を変数へ
                strEtd = Mid(strカット日01, intEtd + 1, Len(strカット日01) - intEtd)
                strカット日01 = Date_Year(Format(strEtd, "yyyy/mm/dd"))
            Else
                '→がなければこちら
                If IsDate(strカット日01) = True Then
                    strカット日01 = strカット日01
                Else
                    strカット日01 = ""
                End If
            End If





            '→の場所を変数へ
            '最後の→の場所を変数へ
            intCnt2 = 0
            strEtd = ""
            intCnt2 = InStr(1, str到着日01, "→")
            If intCnt2 > 0 Then
                '→があればこちら
                Do While intCnt2 > 0
                    intEtd = intCnt2
                    intCnt2 = InStr(intCnt2 + 1, str到着日01, "→")
                Loop
                '→から右側の文字列を変数へ
                strEtd = Mid(str到着日01, intEtd + 1, Len(str到着日01) - intEtd)
                str到着日01 = Date_Year(Format(strEtd, "yyyy/mm/dd"))
            Else
                '→がなければこちら
                If IsDate(str到着日01) = True Then
                    str到着日01 = str到着日01
                Else
                    str到着日01 = ""
                End If
            End If





            '→の場所を変数へ
            '最後の→の場所を変数へ
            intCnt2 = 0
            strEtd = ""
            intCnt2 = InStr(1, str入出港日01, "→")
            If intCnt2 > 0 Then
                '→があればこちら
                Do While intCnt2 > 0
                    intEtd = intCnt2
                    intCnt2 = InStr(intCnt2 + 1, str入出港日01, "→")
                Loop
                '→から右側の文字列を変数へ
                strEtd = Mid(str入出港日01, intEtd + 1, Len(str入出港日01) - intEtd)
                str入出港日01 = Date_Year(Format(strEtd, "yyyy/mm/dd"))
            Else
                '→がなければこちら
                If IsDate(str入出港日01) = True Then
                    str入出港日01 = str入出港日01
                Else
                    str入出港日01 = ""
                End If
            End If




            '→の場所を変数へ
            '最後の→の場所を変数へ
            intCnt2 = 0
            strEtd = ""
            intCnt2 = InStr(1, strVOYAGENo01, "→")
            If intCnt2 > 0 Then
                '→があればこちら
                Do While intCnt2 > 0
                    intEtd = intCnt2
                    intCnt2 = InStr(intCnt2 + 1, strVOYAGENo01, "→")
                Loop
                '→から右側の文字列を変数へ
                strVOYAGENo01 = Mid(strVOYAGENo01, intEtd + 1, Len(strVOYAGENo01) - intEtd)

            Else

            End If


            '→の場所を変数へ
            '最後の→の場所を変数へ
            intCnt2 = 0
            strEtd = ""
            intCnt2 = InStr(1, str船社01, "→")
            If intCnt2 > 0 Then
                '→があればこちら
                Do While intCnt2 > 0
                    intEtd = intCnt2
                    intCnt2 = InStr(intCnt2 + 1, str船社01, "→")
                Loop
                '→から右側の文字列を変数へ
                str船社01 = Mid(str船社01, intEtd + 1, Len(str船社01) - intEtd)

            Else

            End If


            '→の場所を変数へ
            '最後の→の場所を変数へ
            intCnt2 = 0
            strEtd = ""
            intCnt2 = InStr(1, strブッキングNo01, "→")
            If intCnt2 > 0 Then
                '→があればこちら
                Do While intCnt2 > 0
                    intEtd = intCnt2
                    intCnt2 = InStr(intCnt2 + 1, strブッキングNo01, "→")
                Loop
                '→から右側の文字列を変数へ
                strブッキングNo01 = Mid(strブッキングNo01, intEtd + 1, Len(strブッキングNo01) - intEtd)

            Else

            End If

            '→の場所を変数へ
            '最後の→の場所を変数へ
            intCnt2 = 0
            strEtd = ""
            intCnt2 = InStr(1, str船名01, "→")
            If intCnt2 > 0 Then
                '→があればこちら
                Do While intCnt2 > 0
                    intEtd = intCnt2
                    intCnt2 = InStr(intCnt2 + 1, str船名01, "→")
                Loop
                '→から右側の文字列を変数へ
                str船名01 = Mid(str船名01, intEtd + 1, Len(str船名01) - intEtd)

            Else

            End If


            getflg01 = 1

        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()





    End Sub

    Private Sub check006(ByRef str計上日02 As String, ByRef str積出港02 As String, ByRef str揚地02 As String, ByRef str配送先02 As String, ByRef str荷受地02 As String, ByRef str配送先責任送り先02 As String, ByRef strカット日02 As String, ByRef str到着日02 As String, ByRef str入出港日02 As String, ByRef str出荷方法02 As String, ByRef strVOYAGENo02 As String, ByRef str船社02 As String, ByRef strブッキングNo02 As String, ByRef str船名02 As String, ByRef getflg02 As String, ByVal strinv As String, ByRef str客コード02 As String, ByRef strFINALDN02 As String, ByRef strFINALDA02 As String, ByRef strCNEESIN02 As String, ByRef strCNEESIA02 As String, ByRef strPODSI02 As String, ByRef strNOTYSI02 As String)

        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String = ""


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


        ' テーブル名,条件を指定してレコードセットを取得する
        strSQL = "SELECT * FROM T_INV_HD_TB WHERE OLD_INVNO = " & Chr(39) & strinv & Chr(39) & " " & "AND BLDATE BETWEEN '" & dt3 & "' AND '" & dt2 & "' "



        'INVOICENOが最大のものを取得
        strSQL = strSQL & "AND T_INV_HD_TB.INVOICENO = "
        strSQL = strSQL & "(SELECT MAX(T_INV_HD_TB.INVOICENO) AS IVNO "
        strSQL = strSQL & "FROM T_INV_HD_TB "

        strSQL = strSQL & "WHERE T_INV_HD_TB.HEADTITLE Like 'INVOICE%' "
        strSQL = strSQL & "AND T_INV_HD_TB.OLD_INVNO = '" & strinv & "' "
        strSQL = strSQL & "AND T_INV_HD_TB.BLDATE between '" & dt3 & "' AND '" & dt2 & "' "
        strSQL = strSQL & "AND T_INV_HD_TB.BOOKINGNO IS NOT NULL "
        strSQL = strSQL & "AND T_INV_HD_TB.ORG_INVOICENO IS NULL "
        strSQL = strSQL & ") "

        strSQL = strSQL & "ORDER BY BLDATE DESC"

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        strinv = ""
        '結果を取り出す 
        While (dataread.Read())



            str計上日02 = Trim(Left(Convert.ToString(dataread("BLDATE")), 10))

            str積出港02 = Trim(Convert.ToString(dataread("INVFROM")))
            str揚地02 = Trim(Convert.ToString(dataread("VIA")))
            str配送先02 = Trim(Convert.ToString(dataread("INVTO")))
            str荷受地02 = Trim(Convert.ToString(dataread("INVON")))
            str配送先責任送り先02 = ""
            strカット日02 = Trim(Left(Convert.ToString(dataread("CUTDATE")), 10))
            str到着日02 = Trim(Left(Convert.ToString(dataread("REACHDATE")), 10))
            str入出港日02 = Trim(Left(Convert.ToString(dataread("IOPORTDATE")), 10))
            str出荷方法02 = ""
            strVOYAGENo02 = Trim(Convert.ToString(dataread("VOYAGENO")))
            str船社02 = Trim(Convert.ToString(dataread("SHIPPER")))
            strブッキングNo02 = Trim(Convert.ToString(dataread("BOOKINGNO")))
            str船名02 = Trim(Convert.ToString(dataread("SHIPPEDPER")))
            str客コード02 = Trim(Convert.ToString(dataread("CUSTCODE")))

            strFINALDN02 = Trim(Convert.ToString(dataread("DELIVERTONAME")))
            strFINALDA02 = Trim(Convert.ToString(dataread("DELIVERTOADDRESS")))
            strCNEESIN02 = Trim(Convert.ToString(dataread("CONSIGNEESINAME")))
            strCNEESIA02 = Trim(Convert.ToString(dataread("CONSIGNEESIADDRESS")))
            '    strPODSI02 = Trim(fld("PLACEDELIVERSI"))
            strNOTYSI02 = Trim(Convert.ToString(dataread("NOTIFYADDRESS")))


            getflg02 = 1

            If str船社02 = "-" Then

                str船社02 = ""

            End If




        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()


    End Sub


    Private Sub check007(ByRef strFINALDN01 As String, ByRef strFINALDA01 As String, ByRef strCNEESIN01 As String, ByRef strCNEESIA01 As String, ByRef strPODSI01 As String, ByRef strNOTYSI01 As String, ByRef str客コード02 As String)

        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String = ""


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

        Dim del As String = ""
        'データベース接続を開く
        cnn.Open()


        strSQL = ""
        strSQL = strSQL & "SELECT * FROM T_EXL_CSMANUAL "
        strSQL = strSQL & "WHERE NEW_CODE = '" & str客コード02 & "' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()


        '結果を取り出す 
        While (dataread.Read())



            strCNEESIN01 = Trim(Convert.ToString(dataread("CONSIGNEE_OF_SI")))
            strCNEESIA01 = Trim(Convert.ToString(dataread("CONSIGNEE_OF_SI_ADDRESS")))
            strFINALDN01 = Trim(Convert.ToString(dataread("FINAL_DES")))
            strFINALDA01 = Trim(Convert.ToString(dataread("FINAL_DES_ADDRESS")))
            strNOTYSI01 = Trim(Convert.ToString(dataread("NOTIFY")))

            del = """" ' 半角スペース

            If InStr(strCNEESIN01, del) > 0 Then

                strCNEESIN01 = Left(strCNEESIN01, InStr(strCNEESIN01, del) - 1)
                strFINALDN01 = Left(strFINALDN01, InStr(strFINALDN01, del) - 1)

            End If

        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()






    End Sub

    Private Function Date_Year(ByVal Target As String) As String


        Dim dt1 As DateTime = DateTime.Now

        Dim ts1 As New TimeSpan(100, 0, 0, 0)
        Dim ts2 As New TimeSpan(100, 0, 0, 0)
        Dim dt2 As DateTime = dt1 + ts1
        Dim dt3 As DateTime = dt1 - ts1

        '過去の日付を一年足してい返す

        If IsDate(Target) = False Then
            Exit Function
        End If

        If Target < DateAdd("m", -6, dt1) Then
            Date_Year = DateAdd("yyyy", 1, Target)
        Else
            Date_Year = Format(Target, "yyyy/mm/dd")
        End If


    End Function

    Private Sub erreg(ByRef a As Long, ByRef bkgno As String, b As String)

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



        Dim dt1 As DateTime = DateTime.Now

        Dim ts1 As New TimeSpan(100, 0, 0, 0)
        Dim ts2 As New TimeSpan(100, 0, 0, 0)
        Dim dt2 As DateTime = dt1 + ts1
        Dim dt3 As DateTime = dt1 - ts1

        'データベース接続を開く
        cnn.Open()

        strSQL = ""
        strSQL = strSQL & "Select COUNT(*) As RecCnt FROM T_EXL_KANRYOERROR WHERE T_EXL_KANRYOERROR.ERDETAIL <>'0' "
        'strSQL = strSQL & "SELECT COUNT(*) AS RecCnt FROM T_EXL_KANRYOERROR "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        While (dataread.Read())
            a = dataread("RecCnt")
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()

        If a > 0 Then
            strSQL = ""
            strSQL = strSQL & "INSERT INTO T_EXL_WORKSTATUS00 VALUES("
            strSQL = strSQL & " '009' "
            strSQL = strSQL & ",'" & b & "' "
            strSQL = strSQL & ",'" & bkgno & "' "
            strSQL = strSQL & ",'" & Format(Now(), "yyyy/MM/dd") & "' "
            strSQL = strSQL & ",'" & Session("UsrId") & "_07" & "' "
            strSQL = strSQL & ")"

            Command.CommandText = strSQL
            ' SQLの実行
            Command.ExecuteNonQuery()


        Else
            strSQL = ""
            strSQL = strSQL & "DELETE FROM T_EXL_WORKSTATUS00 "
            strSQL = strSQL & "WHERE T_EXL_WORKSTATUS00.ID = '009' "
            strSQL = strSQL & "AND T_EXL_WORKSTATUS00.BKGNO = '" & bkgno & "' "

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



    Private Sub DEL_KANRYOERROR()

        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=KBHWPM02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand
        Dim strSQL As String = ""
        Dim ivno As String = ""


        Dim a As String

        'データベース接続を開く
        cnn.Open()

        strSQL = ""
        strSQL = strSQL & "DELETE "
        strSQL = strSQL & "FROM T_EXL_KANRYOERROR "


        Command.CommandText = strSQL
        ' SQLの実行
        Command.ExecuteNonQuery()

        cnn.Close()
        cnn.Dispose()


    End Sub

    Sub Mail01(ByRef bkgno As String, ByRef a As String, ByRef b As String, ByRef c2 As String, ByRef d As String, ByRef e As String, ByRef f As String, ByRef g As String, ByRef er As Long)

        'メールの送信に必要な情報
        Dim smtpHostName As String = "svsmtp01.exedy.co.jp"
        Dim smtpPort As Integer = 25
        Dim strcon As String
        Dim stritk As String

        Dim ls7 As String = ""

        Dim plt As Long
        Dim qty As Long

        ' メールの内容
        Dim struid As String = Session("UsrId")

        Dim strfrom As String = GET_from(struid)

        Dim strto As String = GET_ToAddress("10", 1)

        'Dim strFilePath As String = "C:\exp\cs_home\upload\" & Session("strFile")

        If strto = "" Then
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "確認", "<script language='JavaScript'>confirm('宛先が設定されていないためメール送信に失敗しました。');</script>", False)
            Exit Sub
        End If

        strto = Left(strto, Len(strto) - 1)



        Dim strcc As String = GET_ToAddress("10", 2) + GET_from(struid)


        Dim strsyomei As String = GET_syomei(struid)

        strcon = GET_CONNO(bkgno)
        stritk = GET_ITK(bkgno)

        'メールの件名
        Dim subject As String = " 【ご報告】＜最終＞完了報告 " & "客先：" & c2 & " / IV-" & d & " / BKG#" & bkgno & " / コンテナ：" & strcon & "/" & e & " 本"

        If e Like "*M3*" Then
            subject = " 【ご報告】＜最終＞ＬＣＬ 完了報告 " & "客先：" & c2 & " / IV-" & d & " / BKG#" & bkgno & " / 荷量：" & e
        End If

        If er > 0 Then
            subject = Replace(subject, "ご報告", "ご報告_ｴﾗｰあり")
        End If

        'message.Subject = ConvertBase64Subject(System.Text.Encoding.GetEncoding("csISO2022JP"), _MailTitle)


        'メールの本文
        Dim body As String = ""
        Dim bodyitk As String = ""


        If strcon > e Then
            body = "＜実績バン本数とブッキング本数に相違があります＞"
            body = "<font style=" & Chr(34) & "background-color: Red" & Chr(34) & ">" & body & "</font>"
            body = "<font style=" & Chr(34) & "color: white" & Chr(34) & ">" & body & "</font>"
            body = "<b><font size=" & Chr(34) & "3" & Chr(34) & ">" & body & "</font></b><br/>"
        Else


        End If

        If e Like "*M3*" Then

            bodyitk = "＜委託案件です＞"
            bodyitk = "<font style=" & Chr(34) & "background-color: Yellow" & Chr(34) & ">" & bodyitk & "</font>"
            bodyitk = "<font style=" & Chr(34) & "color: Black" & Chr(34) & ">" & bodyitk & "</font>"
            bodyitk = "<b><font size=" & Chr(34) & "3" & Chr(34) & ">" & bodyitk & "</font></b><br/>"
            body = body + bodyitk

            stritk = "X"

        Else
        End If

        If stritk = "0" Then
            stritk = ""
        ElseIf stritk = "X" Then
        Else
            bodyitk = "＜委託案件です＞"
            bodyitk = "<font style=" & Chr(34) & "background-color: Yellow" & Chr(34) & ">" & bodyitk & "</font>"
            bodyitk = "<font style=" & Chr(34) & "color: Black" & Chr(34) & ">" & bodyitk & "</font>"
            bodyitk = "<b><font size=" & Chr(34) & "3" & Chr(34) & ">" & bodyitk & "</font></b><br/>"
            body = body + bodyitk

            stritk = "X"

        End If

        body = body + "－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－<br/>"
        body = body + "このメールはシステムから送信されています。<br/>"
        body = body + "心当たりが無い場合、エクセディ　CSチーム担当者までご連絡ください。<br/>"
        body = body + "－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－<br/>"

        body = body & "<html><body>各位<br>お疲れ様です。<br><br>主題の件<br>以下にて完了報告実施しましたので、ご確認宜しくお願いいたします。<br></body></html>" ' UriBodyC()

        '       Dim subject As String = " 【ご報告】＜最終＞完了報告" & "　最終バン：" & a & " " & b & "　客先：" & c2 & "　IV-" & d & "　コンテナ：" & e & " 本" & "　ETD：" & f
        Dim t2 As String = ""
        t2 = "<html><body><Table border='1' style='Font-Size:13px;font-family:Meiryo UI;'><tr style='background-color: #D3D3D3;'><td>客先</td><td>IVNO</td><td>ETD</td><td>最終バン</td><td>BKG#</td></tr>"
        t2 = t2 & "<tr><td>" & c2 & "</td><td>" & d & "</td><td>" & f & "</td><td>" & g & " " & b & "</td><td>" & bkgno & "</td></tr>"
        t2 = t2 & "</Table></body></html><br/>"

        body = body & t2

        Dim t As String = ""




        If e Like "*M3*" Then
            body = body & "＜ 荷量" & " " & e & " ＞<br/>"
        Else
            body = body & "＜ コンテナ" & strcon & "/" & e & "本目(ブッキングシートがただしければ) ＞<br/>"
        End If


        t = "<html><body><Table border='1' style='Font-Size:13px;font-family:Meiryo UI;text-align: center;'><tr style='background-color: #6fbfd1;'><td>IVNO</td><td>通貨</td><td>レート</td><td>客先</td><td>LS7.9</td><td>LS7.9品名</td><td>木材</td><td>パッケージ数</td><td>数量</td></tr>"

        Call IVINFO(bkgno, t, plt, qty, ls7)

        If ls7 = "1" Then
            If stritk <> "X" Then
                subject = "★委託漏れしていませんか？★" & subject
            End If
        End If

        t = t & "<tr style ='background-color: #6fbfd1;'><td colspan='7' style='text-align: center;'>合計</td><td>" & plt & "</td><td>" & Format(qty, "#,0") & "</td></tr>"

        t = t & "</Table></body></html>"
        t = t & "<html><body><Table style='Font-Size:12px;font-family:Meiryo UI;'>※パッケージ数はパレット、カートン等の総合計個数です。</Table></body></html>"

        body = "<font size=" & Chr(34) & "2" & Chr(34) & ">" & body & "</font>"

        body = body & t


        body = "<font size= '3' face=" & Chr(34) & "Meiryo UI" & Chr(34) & ">" & body & "</font>"

        Dim body2 As String = "<br><br>備考－－－－－－－－－－－－－－</p>" & TextBox1.Text.Replace(vbCrLf, "<br/>") & "</p>" ' UriBodyC()

        body2 = "<font face=" & Chr(34) & "Meiryo UI" & Chr(34) & ">" & body2 & "</font>"
        body2 = "<font size=" & Chr(34) & "2" & Chr(34) & ">" & body2 & "</font>"

        body = body & body2

        TextBox1.Text = ""

        ' MailKit におけるメールの情報
        Dim message = New MimeKit.MimeMessage()

        ' 送り元情報  
        message.From.Add(MailboxAddress.Parse(strfrom))

        ' 宛先情報  
        If strto <> "" Then
            'カンマ区切りをSPLIT
            Dim strVal() As String = strto.Split(",")
            For Each c In strVal
                message.To.Add(New MailboxAddress("", c))
            Next
        End If

        If strcc <> "" Then
            'カンマ区切りをSPLIT
            Dim strVal() As String = strcc.Split(",")
            For Each c In strVal
                message.Cc.Add(New MailboxAddress("", c))
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


        ''添付ファイル
        'If Session("strFile") <> "" Then
        '    Dim path = strFilePath     '添付したいファイル
        '    Dim mimeType = MimeKit.MimeTypes.GetMimeType(path)
        '    Dim attachment = New MimeKit.MimePart(mimeType) _
        '    With {
        '        .Content = New MimeKit.MimeContent(System.IO.File.OpenRead(path)),
        '        .ContentDisposition = New MimeKit.ContentDisposition(),
        '        .ContentTransferEncoding = MimeKit.ContentEncoding.Base64,
        '        .FileName = System.IO.Path.GetFileName(path)
        '    }
        '    multipart.Add(attachment)
        'End If

        Using client As New MailKit.Net.Smtp.SmtpClient()
            client.Connect(smtpHostName, smtpPort, MailKit.Security.SecureSocketOptions.Auto)
            client.Send(message)
            client.Disconnect(True)
            client.Dispose()
            message.Dispose()
        End Using

    End Sub

    Sub Mail03(ByRef bkgno As String, ByRef a As String, ByRef b As String, ByRef c2 As String, ByRef d As String, ByRef e As String, ByRef f As String, ByRef g As String)

        'メールの送信に必要な情報
        Dim smtpHostName As String = "svsmtp01.exedy.co.jp"
        Dim smtpPort As Integer = 25
        Dim strcon As String
        Dim stritk As String

        Dim ls7 As String = ""

        Dim plt As Long
        Dim qty As Long

        ' メールの内容
        Dim struid As String = Session("UsrId")

        Dim strfrom As String = GET_from(struid)

        Dim strto As String = GET_ToAddress("10", 1)

        If strto = "" Then
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "確認", "<script language='JavaScript'>confirm('宛先が設定されていないためメール送信に失敗しました。');</script>", False)
            Exit Sub
        End If

        strto = Left(strto, Len(strto) - 1)

        Dim strcc As String = GET_ToAddress("10", 2) + GET_from(struid)
        Dim strsyomei As String = GET_syomei(struid)

        strcon = GET_CONNO(bkgno)
        stritk = GET_ITK(bkgno)

        'メールの件名
        Dim subject As String = " 【ご報告】＜" & strcon & "本目＞完了報告 " & "客先：" & c2 & " / IV-" & d & " / BKG#" & bkgno & " / コンテナ：" & strcon & "/" & e & " 本"
        'message.Subject = ConvertBase64Subject(System.Text.Encoding.GetEncoding("csISO2022JP"), _MailTitle)


        'メールの本文
        Dim body As String = ""
        Dim bodyitk As String = ""


        If strcon > e Then
            body = "＜実績バン本数とブッキング本数に相違があります＞"
            body = "<font style=" & Chr(34) & "background-color: Red" & Chr(34) & ">" & body & "</font>"
            body = "<font style=" & Chr(34) & "color: white" & Chr(34) & ">" & body & "</font>"
            body = "<b><font size=" & Chr(34) & "3" & Chr(34) & ">" & body & "</font></b><br/>"

            stritk = "X"
        Else
        End If


        If stritk = "0" Then
            stritk = ""
        ElseIf stritk = "X" Then
        Else
            bodyitk = "＜委託案件です＞"
            bodyitk = "<font style=" & Chr(34) & "background-color: Yellow" & Chr(34) & ">" & bodyitk & "</font>"
            bodyitk = "<font style=" & Chr(34) & "color: Black" & Chr(34) & ">" & bodyitk & "</font>"
            bodyitk = "<b><font size=" & Chr(34) & "3" & Chr(34) & ">" & bodyitk & "</font></b><br/>"
            body = body + bodyitk

            stritk = "X"

        End If




        body = body + "－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－<br/>"
        body = body + "このメールはシステムから送信されています。<br/>"
        body = body + "心当たりが無い場合、エクセディ　CSチーム担当者までご連絡ください。<br/>"
        body = body + "－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－<br/>"

        body = body & "<html><body>各位<br>お疲れ様です。<br><br>主題の件<br>以下にて完了報告実施しましたので、ご確認宜しくお願いいたします。<br></body></html>" ' UriBodyC()

        Dim t2 As String = ""
        t2 = "<html><body><Table border='1' style='Font-Size:13px;font-family:Meiryo UI;text-align: center;'><tr style='background-color: #D3D3D3;'><td>客先</td><td>IVNO</td><td>ETD</td><td>最終バン</td><td>BKG#</td></tr>"
        t2 = t2 & "<tr><td>" & c2 & "</td><td>" & d & "</td><td>" & f & "</td><td>" & g & " " & b & "</td><td>" & bkgno & "</td></tr>"
        t2 = t2 & "</Table></body></html><br/>"

        body = body & t2

        Dim t As String = ""


        If e Like "*M3*" Then
            body = body & "＜ 荷量" & " " & e & " ＞<br/>"
        Else
            body = body & "＜ コンテナ" & strcon & "/" & e & "本目(ブッキングシートがただしければ) ＞<br/>"
        End If


        t = "<html><body><Table border='1' style='Font-Size:13px;font-family:Meiryo UI;'><tr style='background-color: #6fbfd1;'><td>IVNO</td><td>通貨</td><td>レート</td><td>客先</td><td>LS7.9</td><td>LS7.9品名</td><td>木材</td><td>パッケージ数</td><td>数量</td></tr>"

        Call IVINFO(bkgno, t, plt, qty, ls7)


        If ls7 = "1" Then
            If stritk <> "X" Then
                subject = "★委託漏れしていませんか？★" & subject
            End If
        End If

        t = t & "<tr style ='background-color: #6fbfd1;'><td colspan='7' style='text-align: center;'>合計</td><td>" & plt & "</td><td>" & Format(qty, "#,0") & "</td></tr>"

        t = t & "</Table></body></html>"

        t = t & "<html><body><Table style='Font-Size:12px;font-family:Meiryo UI;'>※パッケージ数はパレット、カートン等の総合計個数です。</Table></body></html>"


        't = "<font style=" & Chr(34) & "background-color: yellow" & Chr(34) & ">" & t & "</font>"
        't = "<font size=" & Chr(34) & "3" & Chr(34) & ">" & t & "</font>"
        body = "<font size=" & Chr(34) & "2" & Chr(34) & ">" & body & "</font>"

        body = body & t



        body = "<font face=" & Chr(34) & "Meiryo UI" & Chr(34) & ">" & body & "</font>"

        Dim body2 As String = "<br><br>備考－－－－－－－－－－－－－－</p>" & TextBox1.Text.Replace(vbCrLf, "<br/>") & "</p>" ' UriBodyC()

        body2 = "<font face=" & Chr(34) & "Meiryo UI" & Chr(34) & ">" & body2 & "</font>"

        body = body & body2

        ' MailKit におけるメールの情報
        Dim message = New MimeKit.MimeMessage()

        ' 送り元情報  
        message.From.Add(MailboxAddress.Parse(strfrom))

        ' 宛先情報  
        If strto <> "" Then
            'カンマ区切りをSPLIT
            Dim strVal() As String = strto.Split(",")
            For Each c In strVal
                message.To.Add(New MailboxAddress("", c))
            Next
        End If

        If strcc <> "" Then
            'カンマ区切りをSPLIT
            Dim strVal() As String = strcc.Split(",")
            For Each c In strVal
                message.Cc.Add(New MailboxAddress("", c))
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

    End Sub

    Sub Mail02(ByRef bkgno As String, ByRef a As String, ByRef b As String, ByRef c2 As String, ByRef d As String, ByRef e As String, ByRef f As String, ByRef g As String)

        'メールの送信に必要な情報
        Dim smtpHostName As String = "svsmtp01.exedy.co.jp"
        Dim smtpPort As Integer = 25

        ' メールの内容
        Dim struid As String = Session("UsrId")



        Dim strfrom As String = GET_from(struid)

        Dim strto As String = GET_ToAddress("11", 1)
        strto = Left(strto, Len(strto) - 1)

        If strto = "" Then
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "確認", "<script language='JavaScript'>confirm('宛先が設定されていないためメール送信に失敗しました。');</script>", False)
            Exit Sub
        End If

        Dim strcc As String = GET_ToAddress("11", 2) + GET_from(struid)


        Dim strsyomei As String = GET_syomei(struid)

        Dim cnt As Long
        Call GETERROR2(bkgno, cnt)

        'メールの件名
        Dim subject As String = " 【通知】＜エラー " & cnt & " 件＞" & " 客先：" & c2 & " / IV-" & d & " / BKG#" & bkgno

        'message.Subject = ConvertBase64Subject(System.Text.Encoding.GetEncoding("csISO2022JP"), _MailTitle)


        'メールの本文
        Dim body As String = ""

        body = body + "－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－<br/>"
        body = body + "このメールはシステムから送信されています。<br/>"
        body = body + "心当たりが無い場合、エクセディ　CSチーム担当者までご連絡ください。<br/>"
        body = body + "－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－<br/>"

        body = body & "<html><body>CS各位<br>お疲れ様です。<br><br>エラーが発生しているため、ご確認ください。<br></body></html>" ' UriBodyC()


        Dim t2 As String = ""
        t2 = "<html><body><Table border='1' style='Font-Size:13px;font-family:Meiryo UI;'><tr style='background-color: #D3D3D3;'><td>客先</td><td>IVNO</td><td>ETD</td><td>最終バン</td><td>BKG#</td></tr>"
        t2 = t2 & "<tr><td>" & c2 & "</td><td>" & d & "</td><td>" & f & "</td><td>" & g & " " & b & "</td><td>" & bkgno & "</td></tr>"
        t2 = t2 & "</Table></body></html><br/>"

        body = body & t2

        Dim t As String = ""


        t = "<html><body><Table border='1' style='Font-Size:13px;font-family:Meiryo UI;'><tr style='background-color: #6fbfd1;'><td>IVNO</td><td>ｴﾗｰ項目</td><td>ｴﾗｰ内容</td></tr>"

        Call GETERROR(bkgno, t)

        t = t & "</Table></body></html>"


        t = "<font color=" & Chr(34) & "RED" & Chr(34) & ">" & t & "</font>"
        t = "<font size=" & Chr(34) & "3" & Chr(34) & ">" & t & "</font>"
        body = "<font size=" & Chr(34) & "2" & Chr(34) & ">" & body & "</font>"
        body = body & t


        body = body & "<html><body><Table style='Font-Size:13px;font-family:Meiryo UI;'>・B：Booking sheet<br>・I：Intra-mart<br>・M：CSﾏｽﾀ<br></Table></body></html>" ' UriBodyC()

        body = "<font face=" & Chr(34) & "Meiryo UI" & Chr(34) & ">" & body & "</font>"

        ' MailKit におけるメールの情報
        Dim message = New MimeKit.MimeMessage()

        ' 送り元情報  
        message.From.Add(MailboxAddress.Parse(strfrom))

        ' 宛先情報  
        If strto <> "" Then
            'カンマ区切りをSPLIT
            Dim strVal() As String = strto.Split(",")
            For Each c In strVal
                message.To.Add(New MailboxAddress("", c))
            Next
        End If

        If strcc <> "" Then
            'カンマ区切りをSPLIT
            Dim strVal() As String = strcc.Split(",")
            For Each c In strVal
                message.Cc.Add(New MailboxAddress("", c))
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

    End Sub

    Sub Mail04(ByRef bkgno As String, ByRef a As String, ByRef b As String, ByRef c2 As String, ByRef d As String, ByRef e As String, ByRef f As String, ByRef g As String)

        'メールの送信に必要な情報
        Dim smtpHostName As String = "svsmtp01.exedy.co.jp"
        Dim smtpPort As Integer = 25

        ' メールの内容
        Dim struid As String = Session("UsrId")

        '宛先はｴﾗｰと一緒

        Dim strfrom As String = GET_from(struid)

        Dim strto As String = GET_ToAddress("11", 1)
        strto = Left(strto, Len(strto) - 1)

        If strto = "" Then
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "確認", "<script language='JavaScript'>confirm('宛先が設定されていないためメール送信に失敗しました。');</script>", False)
            Exit Sub
        End If

        Dim strcc As String = GET_ToAddress("11", 2) + GET_from(struid)


        Dim strsyomei As String = GET_syomei(struid)

        Dim cnt As Long
        Call GETERROR2(bkgno, cnt)

        'メールの件名
        Dim subject As String = " 【通知】！！書類を作成し直してください！！ " & " 客先：" & c2 & " / IV-" & d & " / BKG#" & bkgno

        'message.Subject = ConvertBase64Subject(System.Text.Encoding.GetEncoding("csISO2022JP"), _MailTitle)


        'メールの本文
        Dim body As String = ""

        body = body + "－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－<br/>"
        body = body + "このメールはシステムから送信されています。<br/>"
        body = body + "心当たりが無い場合、エクセディ　CSチーム担当者までご連絡ください。<br/>"
        body = body + "－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－－<br/>"

        body = body & "<html><body>CS各位<br>お疲れ様です。<br><br>書類の再作成が必要です。<br><br>既存の書類を削除し一括処理解除後に、<br>ＲＰＡを使用し書類を再作成してください。<br></body></html>" ' UriBodyC()

        body = "<font face=" & Chr(34) & "Meiryo UI" & Chr(34) & ">" & body & "</font>"

        ' MailKit におけるメールの情報
        Dim message = New MimeKit.MimeMessage()

        ' 送り元情報  
        message.From.Add(MailboxAddress.Parse(strfrom))

        ' 宛先情報  
        If strto <> "" Then
            'カンマ区切りをSPLIT
            Dim strVal() As String = strto.Split(",")
            For Each c In strVal
                message.To.Add(New MailboxAddress("", c))
            Next
        End If

        If strcc <> "" Then
            'カンマ区切りをSPLIT
            Dim strVal() As String = strcc.Split(",")
            For Each c In strVal
                message.Cc.Add(New MailboxAddress("", c))
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

        strSQL = strSQL & "Select MAIL_ADD FROM M_EXL_MAIL01 "
        strSQL = strSQL & "WHERE TASK_CD = '" & strkbn & "' "
        strSQL = strSQL & "AND FLG = '" & strtocc & "' "

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

    Private Sub IVINFO(bkgno As String, ByRef t As String, ByRef plt As Long, ByRef qty As Long, ByRef ls7 As String)
        'データの取得


        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String = ""
        Dim strinv As String = ""
        Dim eflg As Long
        Dim strcst As String = ""
        Dim strrate As Double
        Dim strcry As String = ""
        Dim y As String = ""

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


        strSQL = "SELECT DISTINCT T_INV_HD_TB.OLD_INVNO, T_INV_HD_TB.CUSTCODE, T_INV_HD_TB.RATE,M_CRY_TB.EXPJCD "
        strSQL = strSQL & "FROM (T_INV_HD_TB LEFT JOIN T_INV_BD_TB ON T_INV_HD_TB.INVOICENO = T_INV_BD_TB.INVOICENO) LEFT JOIN M_CRY_TB ON T_INV_HD_TB.CRYCD = M_CRY_TB.CRYCD "
        strSQL = strSQL & "WHERE T_INV_HD_TB.BLDATE BETWEEN '" & dt3 & "' AND '" & dt2 & "' "



        strSQL = strSQL & "GROUP BY T_INV_HD_TB.BOOKINGNO, T_INV_HD_TB.OLD_INVNO,T_INV_HD_TB.CRYCD, T_INV_HD_TB.SHIPPEDPER, T_INV_HD_TB.VOYAGENO, T_INV_HD_TB.IOPORTDATE, T_INV_HD_TB.CUTDATE,T_INV_HD_TB.CUSTCODE,T_INV_HD_TB.HEADTITLE, T_INV_HD_TB.RATE,M_CRY_TB.EXPJCD "
        strSQL = strSQL & "HAVING T_INV_HD_TB.BOOKINGNO like '%" & bkgno & "%' "
        strSQL = strSQL & "AND Sum(T_INV_BD_TB.QTY) >= 1 "
        strSQL = strSQL & "AND Sum(T_INV_BD_TB.KIN) >= 1 "
        strSQL = strSQL & "AND T_INV_HD_TB.HEADTITLE like 'INVOICE%' "
        strSQL = strSQL & "Order by T_INV_HD_TB.OLD_INVNO "


        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()



        '結果を取り出す 
        While (dataread.Read())
            strinv = Trim(Convert.ToString(dataread("OLD_INVNO")))        '客先目
            Call IVINFO2(bkgno, strinv, y, ls7)
            Call IVINFO3(bkgno, strinv, y, plt)
            Call IVINFO4(bkgno, strinv, y, qty)
            strcst = Trim(Convert.ToString(dataread("CUSTCODE")))        '客先目
            strrate = Trim(dataread("RATE"))        '客先目
            strcry = Trim(Convert.ToString(dataread("EXPJCD")))        '客先目

            t = t & "<tr><td>" & strinv & "</td><td>" & strcry & "</td><td>" & strrate & "</td><td>" & strcst & "</td>" & y & "</tr>"

            y = ""
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()



    End Sub

    Private Sub IVINFO2(bkgno As String, IVNO As String, ByRef t As String, ByRef ls7 As String)
        'データの取得


        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String = ""
        Dim strinv As String = ""
        Dim eflg As Long
        Dim strcst As String = ""
        Dim strrate As Double
        Dim strcry As String = ""

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


        Dim tt As Long

        tt = 0

        'データベース接続を開く
        cnn.Open()


        strSQL = "SELECT DISTINCT T_INV_BD_TB.LS, T_INV_BD_TB.PRODNAME,T_INV_BD_TB.REMARK "
        strSQL = strSQL & "FROM T_INV_HD_TB LEFT JOIN T_INV_BD_TB ON T_INV_HD_TB.INVOICENO = T_INV_BD_TB.INVOICENO "
        strSQL = strSQL & "WHERE T_INV_BD_TB.LS <> '2' "
        strSQL = strSQL & "AND T_INV_HD_TB.BLDATE BETWEEN '" & dt3 & "' AND '" & dt2 & "' "
        strSQL = strSQL & "AND T_INV_HD_TB.BOOKINGNO like '%" & bkgno & "%' "
        strSQL = strSQL & "AND T_INV_HD_TB.OLD_INVNO like '%" & IVNO & "%' "
        'strSQL = strSQL & "AND Sum(T_INV_BD_TB.QTY) >= 1 "
        'strSQL = strSQL & "AND Sum(T_INV_BD_TB.KIN) >= 1 "
        strSQL = strSQL & "AND T_INV_HD_TB.HEADTITLE like 'INVOICE%' "

        'INVOICENOが最大のものを取得
        strSQL = strSQL & "AND T_INV_HD_TB.INVOICENO = "
        strSQL = strSQL & "(SELECT MAX(T_INV_HD_TB.INVOICENO) AS IVNO "
        strSQL = strSQL & "FROM T_INV_HD_TB "
        strSQL = strSQL & "WHERE T_INV_HD_TB.HEADTITLE Like 'INVOICE%' "
        strSQL = strSQL & "AND T_INV_HD_TB.OLD_INVNO like '%" & IVNO & "%' "
        strSQL = strSQL & "AND T_INV_HD_TB.BOOKINGNO like '%" & bkgno & "%' "
        strSQL = strSQL & "AND T_INV_HD_TB.BLDATE BETWEEN '" & dt3 & "' AND '" & dt2 & "' "
        strSQL = strSQL & "AND T_INV_HD_TB.ORG_INVOICENO IS NULL "
        strSQL = strSQL & ") "


        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()


        '結果を取り出す 
        While (dataread.Read())

            If tt = 0 Then
                strinv = Trim(Convert.ToString(dataread("LS")))        '客先目
                If Trim(Convert.ToString(dataread("LS"))) = "7" Or Trim(Convert.ToString(dataread("LS"))) = "9" Then
                    ls7 = "1"
                End If

                strcst = Trim(Convert.ToString(dataread("PRODNAME"))) & " / " & Trim(Convert.ToString(dataread("REMARK")))      '客先目
            Else
                strinv = strinv & "<br>" & Trim(Convert.ToString(dataread("LS")))        '客先目
                If Trim(Convert.ToString(dataread("LS"))) = "7" Or Trim(Convert.ToString(dataread("LS"))) = "9" Then
                    ls7 = "1"
                End If

                strcst = strcst & "<br>" & Trim(Convert.ToString(dataread("PRODNAME"))) & " / " & Trim(Convert.ToString(dataread("REMARK")))      '客先目
            End If


            tt = 1
        End While

        '結果を取り出す 
        If tt = 1 Then
            t = t & "<td>" & strinv & "</td><td>" & strcst & "</td><td>" & "不明" & "</td>"
        End If


        If t = "" Then
            t = "<td>" & "なし" & "</td><td>" & "なし" & "</td><td>" & "不明" & "</td>"
        End If


        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()



    End Sub

    Private Sub IVINFO3(bkgno As String, IVNO As String, ByRef t As String, ByRef plt As Long)
        'データの取得


        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String = ""
        Dim strinv As String = ""
        Dim eflg As Long
        Dim strcst As String = ""
        Dim strrate As Double
        Dim strcry As String = ""

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


        strSQL = "SELECT count(b.CASENO) AS caseno_ttl "
        strSQL = strSQL & "FROM "
        strSQL = strSQL & "(select CASENO, PACKNAME, PACKPLURAL "
        strSQL = strSQL & "From T_INV_PDF_VIEW "
        strSQL = strSQL & "where HEADTITLE like 'INVOICE' "
        strSQL = strSQL & "AND CATEGORY_KBN = '1' "
        strSQL = strSQL & "AND old_invno = '" & IVNO & "' "
        strSQL = strSQL & "AND BOOKINGNO like '%" & bkgno & "%' "
        strSQL = strSQL & "AND BLDATE BETWEEN '" & dt3 & "' AND '" & dt2 & "' "


        'INVOICENOが最大のものを取得
        strSQL = strSQL & "AND INVOICENO = "
        strSQL = strSQL & "(SELECT MAX(INVOICENO) AS IVNO "
        strSQL = strSQL & "FROM T_INV_PDF_VIEW "
        strSQL = strSQL & "WHERE HEADTITLE Like 'INVOICE%' "
        strSQL = strSQL & "AND old_invno = '" & IVNO & "' "
        strSQL = strSQL & "AND BOOKINGNO like '%" & bkgno & "%' "
        strSQL = strSQL & ") "

        strSQL = strSQL & "group by CASENO,PACKNAME,PACKPLURAL,PLNO) b "











        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()


        '結果を取り出す 
        While (dataread.Read())
            strinv = Trim(Convert.ToString(dataread("caseno_ttl")))        '客先目

            plt = plt + dataread("caseno_ttl")

            t = t & "<td>" & strinv & "</td>"
        End While

        If t = "" Then
            t = "<td>" & "なし" & "</td>"
        End If


        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()



    End Sub

    Private Sub IVINFO4(bkgno As String, IVNO As String, ByRef t As String, ByRef qty As Long)
        'データの取得


        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String = ""
        Dim strinv As String = ""
        Dim eflg As Long
        Dim strcst As String = ""
        Dim strrate As Double
        Dim strcry As String = ""

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


        strSQL = "SELECT  b.SINGULARNAME,b.PLURALNAME,sum(b.QTY) as QTY  "
        strSQL = strSQL & "FROM "
        strSQL = strSQL & "( select SUM(QTY) AS QTY, SINGULARNAME, PLURALNAME, CASENO, PACKNAME, PACKPLURAL "
        strSQL = strSQL & "From T_INV_PDF_VIEW "
        strSQL = strSQL & "where HEADTITLE like 'INVOICE' "
        strSQL = strSQL & "AND CATEGORY_KBN = '1' "
        strSQL = strSQL & "AND old_invno = '" & IVNO & "' "
        strSQL = strSQL & "AND BOOKINGNO like '%" & bkgno & "%' "
        strSQL = strSQL & "AND BLDATE BETWEEN '" & dt3 & "' AND '" & dt2 & "' "


        'INVOICENOが最大のものを取得
        strSQL = strSQL & "AND INVOICENO = "
        strSQL = strSQL & "(SELECT MAX(INVOICENO) AS IVNO "
        strSQL = strSQL & "FROM T_INV_PDF_VIEW "
        strSQL = strSQL & "WHERE HEADTITLE Like 'INVOICE%' "
        strSQL = strSQL & "AND old_invno = '" & IVNO & "' "
        strSQL = strSQL & "AND BOOKINGNO like '%" & bkgno & "%' "
        strSQL = strSQL & ") "


        strSQL = strSQL & "group by SINGULARNAME, PLURALNAME,CASENO,PACKNAME,PACKPLURAL,PLNO) b  "
        strSQL = strSQL & "group by b.SINGULARNAME,b.PLURALNAME "
        strSQL = strSQL & "order by b.SINGULARNAME Desc "







        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()


        '結果を取り出す 
        While (dataread.Read())
            strinv = Trim(Convert.ToString(dataread("QTY")))        '客先目
            qty = qty + Int(dataread("QTY"))

            t = t & "<td>" & Format(Int(strinv), "#,0") & "</td>"
        End While

        If t = "" Then
            t = "<td>" & "なし" & "</td>"
        End If


        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()



    End Sub
    Private Sub GETERROR(bkgno As String, ByRef t As String)
        'データの取得


        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String = ""
        Dim strinv As String = ""
        Dim eflg As Long
        Dim strcst As String = ""
        Dim strrate As String = ""
        Dim strrate2 As String = ""

        Dim a00 As String = ""
        Dim a01 As String = ""
        Dim a02 As String = ""
        Dim a03 As String = ""
        Dim a000 As String = ""


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


        strSQL = "Select T_EXL_KANRYOERROR.IVNO, T_EXL_KANRYOERROR.BKGNO, T_EXL_KANRYOERROR.ERDETAIL, T_EXL_KANRYOERROR.REF01 "
        strSQL = strSQL & "FROM T_EXL_KANRYOERROR "
        strSQL = strSQL & "WHERE T_EXL_KANRYOERROR.BKGNO Like '%" & bkgno & "%' "




        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()


        '結果を取り出す 
        While (dataread.Read())
            strinv = Trim(Convert.ToString(dataread("IVNO")))        '客先目
            strcst = Trim(Convert.ToString(dataread("BKGNO")))        '客先目
            strrate = Trim(Convert.ToString(dataread("ERDETAIL")))        'REF01
            strrate2 = Trim(Convert.ToString(dataread("REF01")))        '
            t = t & "<tr><td>" & strinv & "</td><td>" & strrate2 & "</td><td>" & Replace(Replace(strrate, vbCrLf, "<br>"), vbLf, "<br>") & "</td></tr>" '<br>

            'a000 = Replace(Replace(Replace(Replace(strrate, vbCrLf, "<br>"), vbLf, "<br>"), "相違：", ""), "vs ", "★")

            'a00 = Left(a000, 2)
            'a01 = Mid(a000, 2, InStr(a000, "★") - 2)
            'a02 = Mid(a000, InStr(a000, "★"), 2)
            'a03 = Mid(a000, InStr(a000, "★") + 2, Len(a000))


            't = t & "<tr><td rowspan='2'>" & strinv & "</td><td rowspan='2'>" & strrate2 & "</td><td>" & Replace(Replace(strrate, vbCrLf, "<br>"), vbLf, "<br>") & "</td><td>" & Replace(Replace(strrate, vbCrLf, "<br>"), vbLf, "<br>") & "</td></tr>" '<br>



        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()



    End Sub
    Private Sub GETERROR2(bkgno As String, ByRef t As String)
        'データの取得


        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String = ""
        Dim strinv As String = ""
        Dim eflg As Long
        Dim strcst As String = ""
        Dim strrate As String = ""
        Dim strrate2 As String = ""


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


        strSQL = "Select count(T_EXL_KANRYOERROR.IVNO) As cnt "
        strSQL = strSQL & "FROM T_EXL_KANRYOERROR "
        strSQL = strSQL & "WHERE T_EXL_KANRYOERROR.BKGNO Like '%" & bkgno & "%' "




        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()


        '結果を取り出す 
        While (dataread.Read())
            t = dataread("cnt")

        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()



    End Sub
    Private Function GET_CONNO(bkgno As String) As String
        'データの取得


        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String = ""
        Dim strinv As String = ""
        Dim eflg As Long
        Dim strcst As String = ""
        Dim strrate As String = ""
        Dim strrate2 As String = ""
        GET_CONNO = ""

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
        strSQL = strSQL & "SELECT COUNT(*) AS RecCnt FROM T_EXL_WORKSTATUS00 WHERE "
        strSQL = strSQL & "T_EXL_WORKSTATUS00.ID = '010' "
        strSQL = strSQL & "AND T_EXL_WORKSTATUS00.BKGNO = '" & bkgno & "' "
        strSQL = strSQL & "AND T_EXL_WORKSTATUS00.REGDATE BETWEEN '" & dt3 & "' AND '" & dt2 & "' "


        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()


        '結果を取り出す 
        While (dataread.Read())
            GET_CONNO = dataread("RecCnt")      '客先目

        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()


    End Function

    Private Function GET_ITK(bkgno As String) As String
        'データの取得


        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String = ""
        Dim strinv As String = ""
        Dim eflg As Long
        Dim strcst As String = ""
        Dim strrate As String = ""
        Dim strrate2 As String = ""
        GET_ITK = ""

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
        strSQL = strSQL & "SELECT COUNT(*) AS RecCnt FROM T_EXL_WORKSTATUS00 WHERE "
        strSQL = strSQL & "T_EXL_WORKSTATUS00.ID = '001' "
        strSQL = strSQL & "AND T_EXL_WORKSTATUS00.BKGNO = '" & bkgno & "' "
        strSQL = strSQL & "AND T_EXL_WORKSTATUS00.REGDATE BETWEEN '" & dt3 & "' AND '" & dt2 & "' "


        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()


        '結果を取り出す 
        While (dataread.Read())
            GET_ITK = dataread("RecCnt")      '客先目
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()


    End Function
    Private Sub EXELE(ByRef bkgno As String, ByRef a As String, ByRef b As String, ByRef c As String, ByRef d As String, ByRef e As String, ByRef f As String)

        ' Dim sw As New System.IO.StreamWriter("\\svnas201\EXD06101\DISC_COMMON\WEB出力\RPA書類作成専用\" & Left(d, 4) & ".txt", True)
        ' sw.Close()


    End Sub
    Private Sub docexck(ByRef bkgno As String, ByRef IVNO As String)

        Dim strFile0 As String = ""

        Dim path01 As String = "\\K3HWPA83\InvPack送信前フォルダ\"
        Dim path02 As String = "\\K3HWPA83\InvPack送信前フォルダ\送付待ち\"
        Dim path03 As String = "\\svnas201\EXD06101\DISC_COMMON\_按分INVOICE\"
        Dim path04 As String = "\\svnas201\EXD06101\DISC_COMMON\輸出書類保管（新）\"
        Dim path05 As String = "\\svnas201\EXD06101\DISC_COMMON\WEB出力\RPA書類作成専用\"
        Dim path06 As String = ""
        Dim Cname As String = ""
        Dim Ccode As String = ""
        Dim ck01 As String = ""


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
        Dim errlrmsg01 As String

        Dim dt1 As DateTime = DateTime.Now

        'データベース接続を開く
        cnn.Open()


        'Call copy_custfile(bkgno, Cname, Ccode)
        'path05 = path04 & Dir("\\svnas201\EXD06101\DISC_COMMON\輸出書類保管（新）\*" & strCust & "*", vbDirectory) & "\"

        strFile0 = Dir(path01 & "*_IV" & IVNO & ".pdf")

        If strFile0 <> "" Then
            ck01 = "1"
        End If

        strFile0 = Dir(path01 & "*_PL" & IVNO & ".pdf")

        If strFile0 <> "" Then
            ck01 = "1"
        End If
        'strFile0 = Dir(strPath & "*_IV" & a & "*.xls")
        'strFile0 = Dir(strPath & "*_SI" & a & "*.xls")
        strFile0 = Dir(path03 & "*IV-" & IVNO & "DATA.xls")

        If strFile0 <> "" Then
            ck01 = "1"
        End If

        Call Get_allinv_ck(bkgno, ck01)
        Call get_ikkatsu(bkgno, ck01)

        If ck01 <> "" Then

            errlrmsg01 = "一括処理解除と作成済み書類削除後に書類を作成"

            'エラー内容登録
            strSQL = ""
            strSQL = strSQL & "INSERT INTO T_EXL_KANRYOERROR VALUES("
            strSQL = strSQL & " '" & IVNO & "', "
            strSQL = strSQL & " '" & bkgno & "', "
            strSQL = strSQL & " '" & errlrmsg01 & "', "
            strSQL = strSQL & " '書類再作成必須02', "
            strSQL = strSQL & " '', "
            strSQL = strSQL & " '', "
            strSQL = strSQL & " '', "
            strSQL = strSQL & " '' "
            strSQL = strSQL & ") "

            Command.CommandText = strSQL
            ' SQLの実行
            Command.ExecuteNonQuery()

        End If

        cnn.Close()
        cnn.Dispose()

    End Sub

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


        strSQL = "SELECT DISTINCT T_INV_HD_TB.OLD_INVNO, T_INV_HD_TB.CUSTCODE "
        strSQL = strSQL & "FROM T_INV_HD_TB LEFT JOIN T_INV_BD_TB ON T_INV_HD_TB.INVOICENO = T_INV_BD_TB.INVOICENO "
        strSQL = strSQL & "AND T_INV_HD_TB.BLDATE BETWEEN '" & dt3 & "' AND '" & dt2 & "' "


        strSQL = strSQL & "GROUP BY T_INV_HD_TB.BOOKINGNO, T_INV_HD_TB.OLD_INVNO,T_INV_HD_TB.CRYCD, T_INV_HD_TB.SHIPPEDPER, T_INV_HD_TB.VOYAGENO, T_INV_HD_TB.IOPORTDATE, T_INV_HD_TB.CUTDATE,T_INV_HD_TB.CUSTCODE,T_INV_HD_TB.HEADTITLE "
        strSQL = strSQL & "HAVING T_INV_HD_TB.BOOKINGNO like '%" & iptbx & "%' "
        strSQL = strSQL & "AND Sum(T_INV_BD_TB.QTY) >= 1 "
        strSQL = strSQL & "AND Sum(T_INV_BD_TB.KIN) >= 1 "
        strSQL = strSQL & "AND T_INV_HD_TB.HEADTITLE like 'INVOICE%' "
        strSQL = strSQL & "Order by T_INV_HD_TB.OLD_INVNO "


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
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click


        Response.Redirect("anken_kanryo_detail.aspx")

    End Sub


    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click

        '前月分ダウンロードボタン押下
        Dim strFile As String = Format(Now, "yyyyMMdd") & "_完了報告.xlsx"
        Dim strPath As String = "C:\exp\cs_home\files\"
        Dim strChanged As String    'サーバー上のフルパス
        Dim strFileNm As String     'ファイル名

        Dim dtToday As DateTime = DateTime.Today

        Dim strFile0 As String = ""
        'ファイル検索
        strFile0 = Dir(strPath & "*_完了報告.xlsx")
        Do While strFile0 <> ""

            If strFile0 = Format(Now, "yyyyMMdd") & "_完了報告.xlsx" Then
            Else
                System.IO.File.Delete(strPath & strFile0)
            End If

            strFile0 = Dir()
        Loop

        Dim dt = GetNorthwindProductTable()
        Dim workbook = New XLWorkbook()
        Dim worksheet = workbook.Worksheets.Add(dt)

        worksheet.Style.Font.FontName = "Meiryo UI"
        worksheet.Style.Alignment.WrapText = False
        worksheet.Columns.AdjustToContents()
        worksheet.SheetView.FreezeRows(1)

        workbook.SaveAs(strPath & strFile)


        'ファイル名を取得する
        Dim strTxtFiles() As String = System.IO.Directory.GetFiles(strPath, Format(Now, "yyyyMMdd") & "_完了報告.xlsx")

        strChanged = strTxtFiles(0)
        strFileNm = Path.GetFileName(strChanged)

        'Contentをクリア
        Response.ClearContent()

        'Contentを設定
        'Response.ContentEncoding = System.Text.Encoding.GetEncoding("shift-jis")
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

        Dim dt = New DataTable("T_EXL_CSKANRYO")

        Using conn = New SqlConnection(ConnectionString)
            Dim cmd = conn.CreateCommand()

            strSQL = strSQL & "SELECT * "
            strSQL = strSQL & "FROM T_EXL_CSKANRYO "

            cmd.CommandText = strSQL
            Dim sda = New SqlDataAdapter(cmd)
            sda.Fill(dt)
        End Using

        Return dt
    End Function

    Private Sub Get_allinv_ck(strbkg As String, ByRef ck01 As String)

        Dim strSQL As String = ""
        Dim ivno As String = ""
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand

        Dim path05 As String = "\\svnas201\EXD06101\DISC_COMMON\WEB出力\RPA書類作成専用\"

        Dim dt1 As DateTime = DateTime.Now

        Dim ts1 As New TimeSpan(100, 0, 0, 0)
        Dim ts2 As New TimeSpan(100, 0, 0, 0)
        Dim dt2 As DateTime = dt1 + ts1
        Dim dt3 As DateTime = dt1 - ts1

        Dim strFile0 As String = ""

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
            '全てのインボスNOをループ
            strFile0 = Dir(path05 & "" & dataread("OLD_INVNO") & ".txt")
            If strFile0 <> "" Then
                ck01 = "1"
            End If
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()


    End Sub

    Private Sub get_ikkatsu(bkgno As String, ByRef ck01 As String)

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


        strSQL = "SELECT T_INV_HD_TB.BOOKINGNO "
        strSQL = strSQL & "FROM T_INV_HD_TB "
        strSQL = strSQL & "WHERE T_INV_HD_TB.BLDATE BETWEEN '" & dt3 & "' AND '" & dt2 & "' "
        strSQL = strSQL & "AND T_INV_HD_TB.ALLOUTSTAMP IS NOT NULL "
        'INVOICENOが最大のものを取得
        strSQL = strSQL & "AND T_INV_HD_TB.INVOICENO = "
        strSQL = strSQL & "(SELECT MAX(T_INV_HD_TB.INVOICENO) AS IVNO "
        strSQL = strSQL & "FROM T_INV_HD_TB "

        strSQL = strSQL & "WHERE T_INV_HD_TB.HEADTITLE Like 'INVOICE%' "
        strSQL = strSQL & "AND T_INV_HD_TB.BLDATE BETWEEN '" & dt3 & "' AND '" & dt2 & "' "
        strSQL = strSQL & "AND T_INV_HD_TB.BOOKINGNO like '%" & bkgno & "%' "
        strSQL = strSQL & "AND T_INV_HD_TB.ORG_INVOICENO IS NULL "
        strSQL = strSQL & ") "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        strDate = ""
        '結果を取り出す 
        While (dataread.Read())
            ck01 = "1"
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()

    End Sub
End Class


