
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.Common
Partial Class yuusen
    Inherits System.Web.UI.Page

    Public strRow As String
    Public strProcess As String
    Private Sub GridView1_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound

        '最終更新年月日取得
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String
        Dim strinv As String
        Dim cno As Long
        Dim wno As Long
        Dim wno2 As Long
        Dim wno3 As Long
        Dim wday As String
        Dim wday2 As String
        Dim wday3 As String
        Dim wday4 As String
        Dim strbkg As String

        Dim dt1 As DateTime = DateTime.Now

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand

        'データベース接続を開く
        cnn.Open()
        If e.Row.RowType = DataControlRowType.DataRow Then
            'strSQL = "SELECT ITK_BKGNO FROM [T_EXL_CSWORKSTATUS] WHERE [T_EXL_CSWORKSTATUS].ITK_BKGNO = '" & Trim(e.Row.Cells(9).Text) & "' "

            strSQL = "SELECT BKGNO FROM [T_EXL_WORKSTATUS00] WHERE [T_EXL_WORKSTATUS00].BKGNO = '" & Trim(e.Row.Cells(9).Text) & "' "
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
                If Trim(e.Row.Cells(9).Text) = strbkg Then
                    Call itaku(e.Row.Cells(9).Text)
                End If

            End While

            'クローズ処理
            dataread.Close()
            dbcmd.Dispose()

        End If

        If e.Row.RowType = DataControlRowType.DataRow Then
            strSQL = "SELECT FLG02 FROM [T_EXL_CSANKEN] WHERE [T_EXL_CSANKEN].BOOKING_NO = '" & Trim(e.Row.Cells(9).Text) & "' "
            'ＳＱＬコマンド作成
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行
            dataread = dbcmd.ExecuteReader()

            strbkg = ""
            '結果を取り出す
            While (dataread.Read())
                strbkg += dataread("FLG02")
            End While

            If strbkg = "1" Then
                e.Row.Cells(1).Text = "委託登録済"
                e.Row.BackColor = Drawing.Color.DarkSalmon
            End If
        End If

        cnn.Close()
        cnn.Dispose()

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

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        'Dim p As New System.Diagnostics.Process
        'p.StartInfo.FileName = “\\kbhwpm01\exp\cs_home\通関フォルダ作成_委託メール作成.xls”
        'p.Start()

        Response.Redirect("通関フォルダ作成_委託メール作成.xls")

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
        Dim ivno As String = ""

        'データベース接続を開く
        cnn.Open()

        Dim I As Integer
        For I = 0 To GridView1.Rows.Count - 1
            If CType(GridView1.Rows(I).Cells(0).Controls(1), CheckBox).Checked Then
                'FIN_FLGを更新
                strSQL = ""
                strSQL = strSQL & "UPDATE T_EXL_CSANKEN SET FLG02 ='1' "
                strSQL = strSQL & "WHERE BOOKING_NO = '" & GridView1.Rows(I).Cells(9).Text & "'"

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

                Call GET_IVDATA(GridView1.Rows(I).Cells(9).Text, "1")
            Else
            End If
        Next
        GridView1.DataBind()

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

        'データベース接続を開く
        cnn.Open()

        Dim I As Integer
        For I = 0 To GridView1.Rows.Count - 1
            If CType(GridView1.Rows(I).Cells(0).Controls(1), CheckBox).Checked Then

                'FIN_FLGを更新
                strSQL = ""
                strSQL = strSQL & "UPDATE T_EXL_CSANKEN SET FLG02 ='' "
                strSQL = strSQL & "WHERE BOOKING_NO = '" & GridView1.Rows(I).Cells(9).Text & "'"

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

                Call GET_IVDATA(GridView1.Rows(I).Cells(9).Text, "2")
            Else
            End If
        Next
        GridView1.DataBind()

    End Sub

    Private Sub GET_IVDATA(bkgno As String, flg As String)

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

        'データベース接続を開く
        cnn.Open()

        strSQL = "SELECT T_INV_HD_TB.OLD_INVNO "
        strSQL = strSQL & "FROM T_INV_HD_TB LEFT JOIN T_INV_BD_TB ON T_INV_HD_TB.INVOICENO = T_INV_BD_TB.INVOICENO "
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
            If flg = "1" Then
                Call INS_ITK(strinv, bkgno)
            ElseIf flg = "2"
                Call DEL_ITK(strinv, bkgno)
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

        'strSQL = ""
        'strSQL = strSQL & "SELECT COUNT(*) AS RecCnt FROM T_EXL_CSWORKSTATUS WHERE "
        'strSQL = strSQL & "T_EXL_CSWORKSTATUS.ITK_INVNO = '" & strinv & "' "
        'strSQL = strSQL & "AND T_EXL_CSWORKSTATUS.ITK_BKGNO = '" & bkgno & "' "

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

            'strSQL = ""
            'strSQL = strSQL & "UPDATE T_EXL_CSWORKSTATUS SET "
            'strSQL = strSQL & "T_EXL_CSWORKSTATUS.ITK_INVNO = '" & strinv & "', "
            'strSQL = strSQL & "T_EXL_CSWORKSTATUS.ITK_REGDATE = '" & Format(Now(), "yyyy/MM/dd") & "', "
            'strSQL = strSQL & "T_EXL_CSWORKSTATUS.ITK_BKGNO = '" & bkgno & "' "
            'strSQL = strSQL & "WHERE T_EXL_CSWORKSTATUS.ITK_INVNO ='" & strinv & "' "

            strSQL = ""
            strSQL = strSQL & "UPDATE T_EXL_WORKSTATUS00 SET "
            strSQL = strSQL & "T_EXL_WORKSTATUS00.ID = '001', "
            strSQL = strSQL & "T_EXL_WORKSTATUS00.INVNO = '" & strinv & "', "
            strSQL = strSQL & "T_EXL_WORKSTATUS00.REGDATE = '" & Format(Now(), "yyyy/MM/dd") & "', "
            strSQL = strSQL & "T_EXL_WORKSTATUS00.BKGNO = '" & bkgno & "' "
            strSQL = strSQL & "WHERE T_EXL_WORKSTATUS00.INVNO ='" & strinv & "' "
            strSQL = strSQL & "AND T_EXL_WORKSTATUS00.BKGNO = '" & bkgno & "' "


        Else
            'strSQL = ""
            'strSQL = strSQL & "INSERT INTO T_EXL_CSWORKSTATUS VALUES("

            'strSQL = strSQL & " '" & "' "
            'strSQL = strSQL & ",'" & " ' "
            'strSQL = strSQL & ",'" & " ' "

            'strSQL = strSQL & ",'" & " ' "
            'strSQL = strSQL & ",'" & " ' "
            'strSQL = strSQL & ",'" & " ' "

            'strSQL = strSQL & ",'" & " ' "
            'strSQL = strSQL & ",'" & " ' "
            'strSQL = strSQL & ",'" & " ' "

            'strSQL = strSQL & ",'" & " ' "
            'strSQL = strSQL & ",'" & " ' "
            'strSQL = strSQL & ",'" & " ' "

            'strSQL = strSQL & ",'" & strinv & "' "
            'strSQL = strSQL & ",'" & Format(Now(), "yyyy/MM/dd") & "' "
            'strSQL = strSQL & ",'" & bkgno & "' "

            'strSQL = strSQL & ")"

            strSQL = ""
            strSQL = strSQL & "INSERT INTO T_EXL_WORKSTATUS00 VALUES("
            strSQL = strSQL & " '001' "
            strSQL = strSQL & ",'" & strinv & "' "
            strSQL = strSQL & ",'" & bkgno & "' "
            strSQL = strSQL & ",'" & Format(Now(), "yyyy/MM/dd") & "' "
            strSQL = strSQL & ")"



        End If

        Command.CommandText = strSQL
        ' SQLの実行
        Command.ExecuteNonQuery()

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

        'strSQL = ""
        'strSQL = strSQL & "SELECT COUNT(*) AS RecCnt FROM T_EXL_CSWORKSTATUS WHERE "
        'strSQL = strSQL & "T_EXL_CSWORKSTATUS.ITK_INVNO = '" & strinv & "' "
        'strSQL = strSQL & "AND T_EXL_CSWORKSTATUS.ITK_BKGNO = '" & bkgno & "' "

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
            'strSQL = ""
            'strSQL = strSQL & "DELETE FROM T_EXL_CSWORKSTATUS "
            'strSQL = strSQL & "WHERE T_EXL_CSWORKSTATUS.ITK_INVNO ='" & strinv & "' "
            'strSQL = strSQL & "AND T_EXL_CSWORKSTATUS.ITK_BKGNO ='" & bkgno & "' "

            strSQL = ""
            strSQL = strSQL & "DELETE FROM T_EXL_WORKSTATUS00 "
            strSQL = strSQL & "WHERE T_EXL_WORKSTATUS00.ID = '001' "
            strSQL = strSQL & "AND T_EXL_WORKSTATUS00.INVNO = '" & strinv & "' "
            strSQL = strSQL & "AND T_EXL_WORKSTATUS00.BKGNO ='" & bkgno & "' "


            Command.CommandText = strSQL
            ' SQLの実行
            Command.ExecuteNonQuery()
        Else
        End If

        cnn.Close()
        cnn.Dispose()

    End Sub

    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load

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
        Dim strkd As String
        Dim stram As String

        Dim dt1 As DateTime = DateTime.Now

        'データベース接続を開く
        cnn.Open()

        strSQL = ""
        strSQL = strSQL & "SELECT T_EXL_ANKENCK.FLG01,T_EXL_ANKENCK.FLG02 FROM T_EXL_ANKENCK "
        strSQL = strSQL & "WHERE T_EXL_ANKENCK.FLG03 ='1' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        While (dataread.Read())
            strkd = Trim(dataread("FLG01"))
            stram = Trim(dataread("FLG02"))
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()

        cnn.Close()
        cnn.Dispose()

        If Label3.Text = "" Then
            If strkd = "1" Then
                CheckBox1.Checked = True
                Label3.Text = "済"
            Else
                CheckBox1.Checked = False
                Label3.Text = "未"
            End If
            If stram = "1" Then
                CheckBox2.Checked = True
                Label4.Text = "済"
            Else
                CheckBox2.Checked = False
                Label4.Text = "未"
            End If

        Else
            If strkd = "1" Then
                If CheckBox1.Checked = True Then
                    CheckBox1.Checked = True
                    Label3.Text = "済"
                End If
            Else
                If CheckBox1.Checked = False Then
                    CheckBox1.Checked = False
                    Label3.Text = "未"
                End If
            End If

            If stram = "1" Then
                If CheckBox2.Checked = True Then
                    CheckBox2.Checked = True
                    Label4.Text = "済"
                End If
            Else
                If CheckBox2.Checked = False Then
                    CheckBox2.Checked = False
                    Label4.Text = "未"
                End If
            End If
        End If

        If CheckBox1.Checked = True And CheckBox2.Checked = True Then
            Label7.Text = "〇"
        Else
            Label7.Text = "×"
        End If

    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged

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

        If CheckBox1.Checked = True Then
            strSQL = ""
            strSQL = strSQL & "UPDATE T_EXL_ANKENCK SET "
            strSQL = strSQL & "T_EXL_ANKENCK.FLG01 = '1' "
            strSQL = strSQL & "WHERE T_EXL_ANKENCK.FLG03 ='1' "

            Command.CommandText = strSQL
            ' SQLの実行
            Command.ExecuteNonQuery()
        Else
            strSQL = ""
            strSQL = strSQL & "UPDATE T_EXL_ANKENCK SET "
            strSQL = strSQL & "T_EXL_ANKENCK.FLG01 = '0' "
            strSQL = strSQL & "WHERE T_EXL_ANKENCK.FLG03 ='1' "

            Command.CommandText = strSQL
            ' SQLの実行

            Command.ExecuteNonQuery()
        End If

        'クローズ処理 

        cnn.Close()
        cnn.Dispose()

        If CheckBox1.Checked = True Then
            Label3.Text = "済"
        Else
            Label3.Text = "未"
        End If

    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged

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

        If CheckBox2.Checked = True Then
            strSQL = ""
            strSQL = strSQL & "UPDATE T_EXL_ANKENCK SET "
            strSQL = strSQL & "T_EXL_ANKENCK.FLG02 = '1' "
            strSQL = strSQL & "WHERE T_EXL_ANKENCK.FLG03 ='1' "

            Command.CommandText = strSQL
            ' SQLの実行
            Command.ExecuteNonQuery()
        Else
            strSQL = ""
            strSQL = strSQL & "UPDATE T_EXL_ANKENCK SET "
            strSQL = strSQL & "T_EXL_ANKENCK.FLG02 = '0' "
            strSQL = strSQL & "WHERE T_EXL_ANKENCK.FLG03 ='1' "

            Command.CommandText = strSQL
            ' SQLの実行

            Command.ExecuteNonQuery()
        End If
        'クローズ処理 

        cnn.Close()
        cnn.Dispose()

        If CheckBox2.Checked = True Then
            Label4.Text = "済"
        Else
            Label4.Text = "未"
        End If

    End Sub


End Class
