
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

        ''接続文字列の作成
        'Dim ConnectionString As String = String.Empty
        ''SQL Server認証
        'ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        ''SqlConnectionクラスの新しいインスタンスを初期化
        'Dim cnn = New SqlConnection(ConnectionString)
        'Dim Command = cnn.CreateCommand

        ''データベース接続を開く
        'cnn.Open()
        'If e.Row.RowType = DataControlRowType.DataRow Then

        'End If


    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click


        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String
        Dim strinv As String
        Dim strcust As String = ""
        Dim strPath00(3) As String      '依頼書、タイムスケジュールのパスと作成先のパス
        Dim strPath01(3) As String      '


        strPath00(0) = "\\svnas201\EXD06101\DISC_COMMON\輸出書類保管（新）\原産地証明書_雛形\"
        strPath00(1) = "\\svnas201\EXD06101\DISC_COMMON\輸出書類保管（新）\"

        Dim strPath As String = ""

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

        Dim I As Integer
        For I = 0 To GridView1.Rows.Count - 1




            strSQL = "SELECT distinct T_INV_HD_TB.CUSTCODE, T_INV_HD_TB.CUSTNAME, T_INV_HD_TB.BLDATE, T_INV_HD_TB.ISSUEDATE, T_INV_HD_TB.OLD_INVNO "
            strSQL = strSQL & "FROM T_INV_HD_TB "
            strSQL = strSQL & "WHERE T_INV_HD_TB.OLD_INVNO = '" & GridView1.Rows(I).Cells(2).Text & "' "
            strSQL = strSQL & "AND T_INV_HD_TB.BLDATE BETWEEN '" & dt3 & "' AND '" & dt2 & "' "


            'ＳＱＬコマンド作成 
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行 
            dataread = dbcmd.ExecuteReader()

            strDate = ""
            '結果を取り出す 
            While (dataread.Read())
                strcust = Convert.ToString(dataread("CUSTCODE"))        'ETD(計上日)

            End While

            'クローズ処理 
            dataread.Close()
            dbcmd.Dispose()

            strPath = strPath00(1) & Dir(strPath00(1) & "*" & strcust & "*」送信済み", vbDirectory) & "\"


            Call make_co(GridView1.Rows(I).Cells(2).Text, strcust, strPath)





        Next

        cnn.Close()
        cnn.Dispose()

    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click



    End Sub

    Sub make_co(strinv As String, strcust As String, strpath As String)

        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String
        Dim strPath00(3) As String      '依頼書、タイムスケジュールのパスと作成先のパス
        Dim strPath01(3) As String      '

        Dim CUSTNAME As String = ""
        Dim BLDATE As String = ""
        Dim ISSUEDATE As String = ""
        Dim STAMP As String = ""

        Dim strFile0 As String = ""

        Dim CONSIGNEENAME As String = ""
        Dim CONSIGNEEADDRESS As String = ""
        Dim INVBODYTITLE As String = ""
        Dim HEADTITLE As String = ""
        Dim SHIPPEDPER As String = ""
        Dim INVFROM As String = ""
        Dim INVTO As String = ""

        strPath00(0) = "\\svnas201\EXD06101\DISC_COMMON\輸出書類保管（新）\原産地証明書_雛形\"
        strPath00(1) = "\\svnas201\EXD06101\DISC_COMMON\輸出書類保管（新）\"

        strPath01(0) = ""
        strPath01(1) = ""

        Dim QTYNUM As String = ""
        Dim PackNum As String = ""
        Dim PCS As String = ""
        Dim PackQty As Integer = 0
        Dim Leng As Long = 0

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


        '1______________________________________________________________________________________________________________________________

        strSQL = "SELECT distinct T_INV_HD_TB.CUSTNAME, T_INV_HD_TB.BLDATE, T_INV_HD_TB.ISSUEDATE,  T_INV_HD_TB.STAMP "
        strSQL = strSQL & "FROM T_INV_HD_TB "
        strSQL = strSQL & "WHERE T_INV_HD_TB.OLD_INVNO='" & strinv & "' "
        strSQL = strSQL & "AND T_INV_HD_TB.BLDATE BETWEEN '" & dt3 & "' AND '" & dt2 & "' "
        strSQL = strSQL & "order by T_INV_HD_TB.STAMP DESC "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()


        '結果を取り出す 
        While (dataread.Read())

            CUSTNAME = dataread("CUSTNAME")
            BLDATE = dataread("BLDATE")
            ISSUEDATE = dataread("ISSUEDATE")
            STAMP = dataread("STAMP")

        End While


        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()

        '2______________________________________________________________________________________________________________________________

        strSQL = "SELECT  b.SINGULARNAME,b.PLURALNAME,sum(b.QTY) as QTY  "
        strSQL = strSQL & "FROM ( "
        strSQL = strSQL & "Select Case SUM(QTY) AS QTY, SINGULARNAME, PLURALNAME, CASENO, PACKNAME, PACKPLURAL "
        strSQL = strSQL & "from T_INV_PDF_VIEW where CATEGORY_KBN = '1' and old_invno = " & Chr(39) & strinv & Chr(39) & " " & "AND BLDATE BETWEEN '" & dt3 & "' AND '" & dt2 & "' "
        strSQL = strSQL & "group by  SINGULARNAME, PLURALNAME,CASENO,PACKNAME,PACKPLURAL,PLNO) b  "
        strSQL = strSQL & "group by   b.SINGULARNAME,b.PLURALNAME "
        strSQL = strSQL & "order by   b.SINGULARNAME Desc "

        strSQL = "" &
    "SELECT  b.SINGULARNAME,b.PLURALNAME,sum(b.QTY) as QTY  " &
    "FROM ( select SUM(QTY) AS QTY, SINGULARNAME, PLURALNAME, CASENO, PACKNAME, PACKPLURAL from T_INV_PDF_VIEW where CATEGORY_KBN = '1' and old_invno = " & Chr(39) & strinv & Chr(39) & " " & "AND BLDATE BETWEEN '" & dt3 & "' AND '" & dt2 & "' " &
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
                QTYNUM = QTYNUM & Format(dataread("QTY"), "#,###") & Chr(32) & Trim(dataread("PLURALNAME")) & " & "
            Else
                QTYNUM = QTYNUM & Format(dataread("QTY"), "#,###") & Chr(32) & Trim(dataread("SINGULARNAME")) & " & "
            End If


            Leng = Len(QTYNUM)

            PCS = Mid(QTYNUM, 1, Leng - 3)


        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        '3______________________________________________________________________________________________________________________________


        strSQL = "SELECT count(b.CASENO) AS caseno_ttl, b.PACKNAME AS packname_ttl,b.PACKPLURAL AS packplural_ttl "
        strSQL = strSQL & "FROM ( select CASENO, PACKNAME, PACKPLURAL from T_INV_PDF_VIEW where HEADTITLE like 'INVOICE' and CATEGORY_KBN = '1' and old_invno = " & Chr(39) & strinv & Chr(39) & " " & "AND BLDATE BETWEEN '" & dt3 & "' AND '" & dt2 & "' "
        strSQL = strSQL & "group by CASENO,PACKNAME,PACKPLURAL,PLNO) b "
        strSQL = strSQL & "group by b.PACKNAME,b.PACKPLURAL "

        strSQL = "" &
    "SELECT count(b.CASENO) AS caseno_ttl, b.PACKNAME AS packname_ttl,b.PACKPLURAL AS packplural_ttl " &
    "FROM ( select CASENO, PACKNAME, PACKPLURAL from T_INV_PDF_VIEW where HEADTITLE like 'INVOICE' and CATEGORY_KBN = '1' and old_invno = " & Chr(39) & strinv & Chr(39) & " " & "AND BLDATE BETWEEN '" & dt3 & "' AND '" & dt2 & "' " &
    "group by CASENO,PACKNAME,PACKPLURAL,PLNO) b " &
    "group by b.PACKNAME,b.PACKPLURAL "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()


        '結果を取り出す 
        While (dataread.Read())


            If dataread("caseno_ttl") > 1 Then
                PackNum = Trim(PackNum) & dataread("caseno_ttl") & Chr(32) & Trim(dataread("packplural_ttl")) & Chr(13) & Chr(10)
            Else
                PackNum = Trim(PackNum) & dataread("caseno_ttl") & Chr(32) & Trim(dataread("packplural_ttl")) & Chr(13) & Chr(10)
            End If

            PackQty = PackQty + dataread("caseno_ttl")


        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        '4______________________________________________________________________________________________________________________________


        strSQL = "SELECT distinct T_INV_HD_TB.CONSIGNEENAME, T_INV_HD_TB.CONSIGNEEADDRESS, T_INV_HD_TB.SALESFLG, T_INV_HD_TB.OLD_INVNO,T_INV_HD_TB.STAMP,T_INV_HD_TB.SHIPPEDPER,T_INV_HD_TB.INVBODYTITLE,T_INV_HD_TB.HEADTITLE,T_INV_HD_TB.INVFROM,T_INV_HD_TB.INVTO "
        strSQL = strSQL & "FROM T_INV_HD_TB "
        strSQL = strSQL & "WHERE T_INV_HD_TB.OLD_INVNO = " & Chr(39) & strinv & Chr(39) & " "
        strSQL = strSQL & "AND T_INV_HD_TB.BLDATE BETWEEN '" & dt3 & "' AND '" & dt2 & "' "
        strSQL = strSQL & "AND T_INV_HD_TB.HEADTITLE like 'INVOICE%'"
        strSQL = strSQL & " order by T_INV_HD_TB.STAMP DESC "


        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()


        '結果を取り出す 
        While (dataread.Read())

            CONSIGNEENAME = dataread("CONSIGNEENAME")
            CONSIGNEEADDRESS = dataread("CONSIGNEEADDRESS")
            INVBODYTITLE = dataread("INVBODYTITLE")
            HEADTITLE = dataread("HEADTITLE")
            SHIPPEDPER = dataread("SHIPPEDPER")
            INVFROM = dataread("INVFROM")
            INVTO = dataread("INVTO")



        End While


        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()


        strFile0 = Dir(strPath00(0) & "*" & strcust & "*", vbNormal) '"\\svnas201\EXD06101\DISC_COMMON\輸出書類保管（新）\原産地証明書_雛形\"
        If strFile0 = "" Then

        End If

        Dim strSI As String = ""
        Dim strFile1 As String = ""

        strSI = Dir(strpath & "*_SI" & strinv & ".xls")
        strFile1 = Replace(strSI, "_SI", "_CO")


        System.IO.File.Copy(strPath00(0) & "\" & strFile0, strpath & strFile1)


        Dim hensyuuiraisyo As String = ""
        Dim hensyuuiraisyo02 As String = ""

        hensyuuiraisyo = strpath & strFile1
        hensyuuiraisyo02 = strpath & strSI

        Dim workbook = New XLWorkbook(hensyuuiraisyo)
        Dim ws1 As IXLWorksheet = workbook.Worksheet(1)

        Dim workbook2 = New XLWorkbook(hensyuuiraisyo02)
        Dim ws2 As IXLWorksheet = workbook.Worksheet(1)
        Dim ws3 As IXLWorksheet = workbook.Worksheet(2)

        'ws1.Cell(4, 1).Value

        ws1.Cell(2, 9).Value = CONSIGNEENAME
        ws1.Cell(2, 10).Value = CONSIGNEEADDRESS
        ws1.Cell(7, 8).Value = "ORIGINAL"
        ws1.Cell(10, 8).Value = ws2.Cell(1, 13)                                     'IVNO
        ws1.Cell(11, 8).Value = ISSUEDATE

        ws1.Cell(22, 2).Value = SHIPPEDPER                                     'sennmei
        ws1.Cell(22, 4).Value = BLDATE
        'Rng.Offset(1, 2).NumberFormatLocal = "mmm.dd,yyyy"


        ws1.Cell(24, 9).Value = "from" & INVFROM & " PORT,  JAPAN" & "  確認用" '　　　　　　　港
        ws1.Cell(25, 9).Value = "to " & INVTO & "  確認用"  'テスト用 '　　　　　　　港


        ws1.Cell(32, 10).Value = PCS



        Dim Mystr As String = Trim(PackNum)
        ws1.Cell(33, 10).Value = Mystr


        '品名
        ws1.Cell(32, 2).Value = Trim(INVBODYTITLE)        '　　　　　　　　　　　　　　　　　　　　　　　　　　　　品名
        'Ws0.Range("B32").Font.Underline = xlUnderlineStyleSingle


        Dim lastPossibleAddress = ws3.LastCellUsed.Address


        Dim k As Long = 0
        Dim lngCosu As Long = 0
        Dim upblk As Long = 0
        Dim i As Long = 0



        'k = Ws2.Range("B65535").End(xlUp).Row
        lngCosu = 0

        'upblk = Ws2.Range("B65535").End(xlUp).Row
        'i = 2

        'Do Until i = upblk

        '    If Ws2.Range("G" & i) <> "" Then

        '        If Trim(Ws2.Range("B" & i)) = "" Then

        '            Ws2.Range("B" & i) = "仮入力-消してください"

        '        End If

        '    End If

        '    i = i + 1

        'Loop

        'If Trim(Ws2.Range("B3")) = "" Then

        '    Ws2.Range("B3") = "仮入力-消してください"

        'End If

        'strStart = Ws2.Range("B3")
        'i = Ws2.Range("B3").Row
        'j = i + 1

        'Do Until Trim(Ws2.Range("B" & j)) = ""
        '    j = j + 1
        'Loop

        'lngGyou = (j - i) - 1


        'i = 3
        'Do Until i >= k

        '    If Ws2.Range("G" & i) <> "" Then '        If Ws2.Range("B" & i) = strStart Then  〇〇

        '        If lngCosu >= 15 Then
        '            FLG = "3"
        '            '                MsgBox "SMが１２種類を超えています" & vbCrLf & "手動で対応して下さい"  '１２種類を超えた場合＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿＿
        '            Exit Sub
        '        End If


        '        '行数取得___________________________________________________________________　〇〇
        '        lngGyou = 0
        '        t = i
        '        Do Until Trim(Ws2.Range("B" & t)) = ""

        '            lngGyou = lngGyou + 1
        '            t = t + 1

        '        Loop

        '        lngGyou = lngGyou - 1

        '        '____________________________________________________________________________　〇〇

        '        Ws2.Range("B" & i & ":B" & i + lngGyou).Copy

        '        If lngCosu <= 3 Then
        '            Mystr = "B" & 34 + (lngCosu * (3 + 2)) + lngCosu
        '            Ws0.Range(Mystr).PasteSpecial xlPasteValues
        '        Mystr = "B" & 34 + (lngCosu * (3 + 2)) + lngCosu + lngGyou
        '            Ws0.Range(Mystr) = Trim(Ws0.Range(Mystr)) & SMNO(Ws2.Range("G" & i))
        '            lngCosu = lngCosu + 1
        '        ElseIf (lngCosu > 3) And (lngCosu <= 7) Then
        '            Mystr = "D" & 34 + ((lngCosu - 4) * (3 + 2)) + (lngCosu - 4)
        '            Ws0.Range(Mystr).PasteSpecial xlPasteValues
        '        Mystr = "D" & 34 + ((lngCosu - 4) * (3 + 2)) + (lngCosu - 4) + lngGyou
        '            Ws0.Range(Mystr) = Trim(Ws0.Range(Mystr)) & SMNO(Ws2.Range("G" & i))
        '            lngCosu = lngCosu + 1
        '        ElseIf (lngCosu > 7) And (lngCosu <= 11) Then
        '            Mystr = "F" & 34 + ((lngCosu - 8) * (3 + 2)) + (lngCosu - 8)
        '            Ws0.Range(Mystr).PasteSpecial xlPasteValues
        '        Mystr = "F" & 34 + ((lngCosu - 8) * (3 + 2)) + (lngCosu - 8) + lngGyou
        '            Ws0.Range(Mystr) = Trim(Ws0.Range(Mystr)) & SMNO(Ws2.Range("G" & i))
        '            lngCosu = lngCosu + 1
        '        Else
        '            Mystr = "H" & 34 + ((lngCosu - 12) * (3 + 2)) + (lngCosu - 12)
        '            Ws0.Range(Mystr).PasteSpecial xlPasteValues
        '        Mystr = "H" & 34 + ((lngCosu - 12) * (3 + 2)) + (lngCosu - 12) + lngGyou
        '            Ws0.Range(Mystr) = Trim(Ws0.Range(Mystr)) & SMNO(Ws2.Range("G" & i))
        '            lngCosu = lngCosu + 1

        '        End If

        '    End If

        '    i = i + 1

        'Loop







        cnn.Close()
        cnn.Dispose()


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



    End Sub



End Class
