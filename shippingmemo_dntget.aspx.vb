
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.Common
Partial Class yuusen
    Inherits System.Web.UI.Page

    Public strRow As String
    Public strProcess As String

    Private Sub GridView1_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowDataBound

        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String

        Dim strkaika As String

        Dim str01 As String = ""
        Dim str02 As String = ""
        Dim str03 As String = ""
        Dim str04 As String = ""
        Dim str05 As String = ""

        If e.Row.RowType = DataControlRowType.DataRow Then
            If e.Row.Cells(11).Text = "月またぎ" Then
                e.Row.BackColor = Drawing.Color.DarkSalmon
            ElseIf e.Row.Cells(11).Text = "月またぎP" Then
                e.Row.BackColor = Drawing.Color.DarkOrange
            ElseIf e.Row.Cells(11).Text = "出港済み" Then
                e.Row.BackColor = Drawing.Color.LightBlue
            ElseIf e.Row.Cells(11).Text = "月またぎ前月" Then
                e.Row.BackColor = Drawing.Color.LightGreen
            End If

            If e.Row.Cells(8).Text = "" Or e.Row.Cells(8).Text = "&nbsp;" Then
                Call GET_IVDATA(Trim(e.Row.Cells(12).Text), Trim(e.Row.Cells(2).Text), e.Row.Cells(3).Text)
            End If

            Dim dt0 As DateTime = DateTime.Parse(e.Row.Cells(3).Text)

            If Replace(e.Row.Cells(8).Text, "&nbsp;", "") <> "" And Replace(e.Row.Cells(11).Text, "&nbsp;", "") = "" Then
                If Replace(e.Row.Cells(3).Text, "&nbsp;", "") <> "" And Replace(e.Row.Cells(9).Text, "&nbsp;", "") <> "" Then
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

            'If e.Row.Cells(4).Text <> "&nbsp;" Then
            '    If Trim(e.Row.Cells(4).Text) <> "" Then

            '        Dim dt3 As DateTime = DateTime.Parse(e.Row.Cells(4).Text)

            '        If dt0.ToString("MM") = dt3.ToString("MM") Then
            '            str04 = "○"
            '            Call UPD_MEMO03(Trim(e.Row.Cells(12).Text), str04)
            '        Else
            '            str04 = "×"
            '            Call UPD_MEMO03(Trim(e.Row.Cells(12).Text), str04)
            '        End If
            '    End If
            'End If

            Dim dt00 As DateTime = DateTime.Now
            Dim ts1 As New TimeSpan(7, 0, 0, 0)
            Dim dt01 As DateTime = dt00 + ts1

            If e.Row.Cells(8).Text = "&nbsp;" Or Trim(e.Row.Cells(8).Text) = "" Then
                '    If e.Row.Cells(6).Text = "&nbsp;" Or Trim(e.Row.Cells(6).Text) = "" Then

                Dim dt4 As DateTime = DateTime.Parse(e.Row.Cells(5).Text)

                If dt4 <= dt01 Then
                    e.Row.BackColor = Drawing.Color.Red
                    e.Row.Cells(23).Text = "E"
                Else
                End If
                '    Else

                '        Dim dt3 As DateTime = DateTime.Parse(e.Row.Cells(6).Text)

                '        If dt3 <= dt01 Then
                '            e.Row.BackColor = Drawing.Color.Red
                '            e.Row.Cells(25).Text = "E"
                '        Else
                '        End If
                '    End If
            End If

            '接続文字列の作成
            Dim ConnectionString As String = String.Empty
            'SQL Server認証
            ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
            'SqlConnectionクラスの新しいインスタンスを初期化
            Dim cnn = New SqlConnection(ConnectionString)
            Dim Command = cnn.CreateCommand

            'データベース接続を開く
            cnn.Open()

            strSQL = "SELECT T_BOOKING.Forwarder, T_BOOKING.BOOKING_NO FROM T_BOOKING WHERE T_BOOKING.BOOKING_NO = '" & Trim(e.Row.Cells(12).Text) & "' AND STATUS <>'キャンセル' "


            'ＳＱＬコマンド作成
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行
            dataread = dbcmd.ExecuteReader()
            strkaika = ""
            '結果を取り出す
            While (dataread.Read())
                strkaika = dataread("Forwarder")
                e.Row.Cells(1).Text = strkaika
            End While

            'クローズ処理
            dataread.Close()
            dbcmd.Dispose()

            cnn.Close()
            cnn.Dispose()


        End If

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
        strSQL = strSQL & "AND T_INV_HD_TB.OLD_INVNO = " & Chr(39) & ivno & Chr(39) & " "
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
        'strSQL = strSQL & "T_EXL_SHIPPINGMEMOLIST.BOOKING_NO ='" & str01 & "', "
        'strSQL = strSQL & "T_EXL_SHIPPINGMEMOLIST.VOY_NO ='" & str02 & "', "
        strSQL = strSQL & "T_EXL_SHIPPINGMEMOLIST.IV_BLDATE ='" & str03 & "', "
        strSQL = strSQL & "T_EXL_SHIPPINGMEMOLIST.KIN_GAIKA ='" & str04 & "', "
        strSQL = strSQL & "T_EXL_SHIPPINGMEMOLIST.RATE ='" & str05 & "', "
        strSQL = strSQL & "T_EXL_SHIPPINGMEMOLIST.KIN_JPY ='" & str06 & "' "
        'strSQL = strSQL & "T_EXL_SHIPPINGMEMOLIST.VESSEL ='" & str07 & "', "
        'strSQL = strSQL & "T_EXL_SHIPPINGMEMOLIST.LOADING_PORT ='" & str08 & "', "
        'strSQL = strSQL & "T_EXL_SHIPPINGMEMOLIST.RECEIVED_PORT ='" & str09 & "', "
        'strSQL = strSQL & "T_EXL_SHIPPINGMEMOLIST.SHIP_PLACE ='" & str10 & "' "
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

End Class
