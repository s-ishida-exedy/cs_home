Imports System.Data.SqlClient
Imports System.Data

Partial Class cs_home
    Inherits System.Web.UI.Page

    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load

        If IsPostBack Then
            ' そうでない時処理
        Else
            '初期化
            DropDownList1.SelectedIndex = 0
            RadioButton1.Checked = False
            RadioButton2.Checked = True
            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
            TextBox4.Text = ""
            TextBox2.Enabled = False
            TextBox3.Enabled = False
            TextBox4.Enabled = False

        End If

        Button1.Attributes.Add("onclick", "return confirm('TSV出力します。よろしいですか？');")

    End Sub

    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        '第三国インボイス有無　有り
        Dim strSQL As String = ""
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strCst As String = ""

        If RadioButton1.Checked = True Then

            If TextBox1.Text = "" Then
                Label1.Text = "インボイス番号が入力されていません。"
                RadioButton1.Checked = False
                RadioButton2.Checked = True
                TextBox2.Enabled = False
                TextBox3.Enabled = False
                TextBox2.Text = ""
                TextBox3.Text = ""
                TextBox4.Text = ""
                Return
            End If

            'インボイスNOから客先を特定する。

            '接続文字列の作成
            Dim ConnectionString As String = String.Empty
            'SQL Server認証
            ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
            'SqlConnectionクラスの新しいインスタンスを初期化
            Dim cnn = New SqlConnection(ConnectionString)

            'データベース接続を開く
            cnn.Open()

            strSQL = ""
            strSQL = strSQL & "SELECT CUST_CD FROM M_EXL_CUST a "
            strSQL = strSQL & "INNER JOIN V_T_INV_HD_TB b "
            strSQL = strSQL & "ON a.CUST_ANAME = b.CUSTCODE "
            strSQL = strSQL & "WHERE b.INVOICENO = ( "
            strSQL = strSQL & "SELECT MAX(a.INVOICENO) AS IVNO  "
            strSQL = strSQL & "FROM  "
            strSQL = strSQL & "  V_T_INV_HD_TB b  "
            strSQL = strSQL & "INNER JOIN V_T_INV_BD_TB a  "
            strSQL = strSQL & "ON a.INVOICENO = b.INVOICENO  "
            strSQL = strSQL & "WHERE "
            strSQL = strSQL & "  b.OLD_INVNO = '" & Trim(TextBox1.Text) & "') "

            'ＳＱＬコマンド作成 
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行 
            dataread = dbcmd.ExecuteReader()

            '結果を取り出す 
            While (dataread.Read())
                strCst = Trim(dataread("CUST_CD"))
            End While

            'クローズ処理 
            dataread.Close()
            dbcmd.Dispose()
            cnn.Close()
            cnn.Dispose()

            '対象客先がESPの場合のみ選択可能
            If strCst = "ESP" Then
                RadioButton2.Checked = False
                TextBox2.Enabled = True
                TextBox3.Enabled = True
                TextBox4.Enabled = True
                TextBox2.Text = "EXEDY SINGAPORE PTE. LTD."
                TextBox3.Text = "45 UBI ROAD 1, #02-01, SINGAPORE 408696"
                TextBox4.Text = "SG"
                Label1.Text = ""
            Else
                Label1.Text = "第三国インボイスはESPのみ選択可能です。"
                RadioButton1.Checked = False
                RadioButton2.Checked = True
                TextBox2.Enabled = False
                TextBox3.Enabled = False
                TextBox4.Enabled = False
                TextBox2.Text = ""
                TextBox3.Text = ""
                TextBox4.Text = ""
            End If
        End If
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        '第三国インボイス有無　無し
        If RadioButton2.Checked = True Then
            RadioButton1.Checked = False
            TextBox2.Enabled = False
            TextBox3.Enabled = False
            TextBox4.Enabled = False
            TextBox2.Text = ""
            TextBox3.Text = ""
            TextBox4.Text = ""
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'TSV出力ボタン

        '選択された協定を取得
        Dim strIVno As String = ""
        Dim strAgree As String = DropDownList1.SelectedValue
        Dim strCase As String = ""

        'エラーメッセージのラベルを初期化
        Label1.Text = ""

        'インボイス(V_T_INV_HD_TB,V_T_INV_BD_TB)と判定番号マスタ(M_EXL_ORIGIN_ITM)の紐づきを取得

        If TextBox1.Text = "" Then
            Label1.Text = "インボイス番号が入力されていません。"
            Return
        Else
            strIVno = Trim(TextBox1.Text)
        End If

        'ケースマーク文字列の作成
        strCase = Get_CaseMark(strIVno, strAgree)

        If strCase = "" Then
            Label1.Text = "対象データがありません。"
            Return
        End If

        '荷姿文字列の作成
        Dim strNi As String = Get_Nisugata(strIVno, strAgree)

        'ヘッダ,明細情報取得、TSVﾌｧｲﾙ書き込み
        If strAgree <> "05" Then
            'インドネシア以外
            Call Make_Tsv(strIVno, strNi, strCase, strAgree)
        Else
            'インドネシア用
            Call Make_Tsv_for_Indonesia(strIVno, strNi, strCase, strAgree)
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'クリアボタン
        DropDownList1.SelectedIndex = 0
        RadioButton1.Checked = True
        RadioButton2.Checked = False
        TextBox1.Text = ""
        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox2.Enabled = False
        TextBox3.Enabled = False
        TextBox4.Enabled = False

    End Sub

    Private Function Get_CaseMark(strIVno As String, strCountry As String) As String
        'ケースマーク情報の取得
        Dim strSQL As String = ""
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strAgree As String = ""

        Get_CaseMark = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()

        strSQL = ""
        strSQL = strSQL & "SELECT DISTINCT "
        strSQL = strSQL & "  SEL.INVOICENO  "
        strSQL = strSQL & "  , SEL.SHIPPINGMARK02 "
        strSQL = strSQL & "  , SEL.ORDERNO "
        strSQL = strSQL & "  , SEL.SHIPPINGMARK04 "
        strSQL = strSQL & "  , SEL.SHIPPINGMARK05 "
        strSQL = strSQL & "  , SEL.SHIPPINGMARK06 "
        strSQL = strSQL & "  , SEL.PLNO "
        strSQL = strSQL & "  , STUFF((  "
        strSQL = strSQL & "    SELECT DISTINCT "
        strSQL = strSQL & "      ',' + LTRIM(STR(IVB2.CASENO)) "
        strSQL = strSQL & "    FROM "
        strSQL = strSQL & "      V_T_INV_PDF_VIEW01 IVB2  "

        'strSQL = strSQL & "    LEFT JOIN M_EXL_ORIGIN_ITM ORI2  "
        'strSQL = strSQL & "    ON IVB2.CUSTMPN = ORI2.CST_ITM_CODE  "

        'JOINする時の条件　01:品番、02:品名、03：両方
        Select Case Get_Search_Method(strCountry)
            Case "01"
                strSQL = strSQL & "      LEFT JOIN M_EXL_ORIGIN_ITM ORI2  "
                strSQL = strSQL & "        On IVB2.CUSTMPN = ORI2.CST_ITM_CODE  "
            Case "02"
                strSQL = strSQL & "      LEFT JOIN M_EXL_ORIGIN_ITM ORI2  "
                strSQL = strSQL & "        On IVB2.PRODNAME = ORI2.ITM_NAME  "
            Case "03"
                strSQL = strSQL & "      LEFT JOIN M_EXL_ORIGIN_ITM ORI2  "
                strSQL = strSQL & "        On IVB2.CUSTMPN = ORI2.CST_ITM_CODE OR IVB2.PRODNAME = ORI2.ITM_NAME "
        End Select

        strSQL = strSQL & "    WHERE "
        strSQL = strSQL & "      IVB2.INVOICENO = (  "
        strSQL = strSQL & "        SELECT MAX(b.INVOICENO) AS IVNO  "
        strSQL = strSQL & "        FROM V_T_INV_PDF_VIEW01 b  "
        strSQL = strSQL & "        WHERE b.OLD_INVNO = '" & strIVno & "' AND b.QTY > 0)  "
        strSQL = strSQL & "      AND ORI2.CODE = '" & strCountry & "'  "
        strSQL = strSQL & "      AND IVB2.MIX_FLG = 'N'       "
        strSQL = strSQL & "      AND SEL.INVOICENO = IVB2.INVOICENO  "
        strSQL = strSQL & "      AND SEL.SHIPPINGMARK02 = IVB2.SHIPPINGMARK02  "
        strSQL = strSQL & "      AND SEL.ORDERNO = IVB2.ORDERNO  "
        strSQL = strSQL & "      AND SEL.SHIPPINGMARK04 = IVB2.SHIPPINGMARK04  "
        strSQL = strSQL & "      AND SEL.SHIPPINGMARK05 = IVB2.SHIPPINGMARK05  "
        strSQL = strSQL & "      AND SEL.SHIPPINGMARK06 = IVB2.SHIPPINGMARK06  "
        strSQL = strSQL & "      AND SEL.PLNO = IVB2.PLNO "
        strSQL = strSQL & "    ORDER BY ',' + LTRIM(STR(IVB2.CASENO)) "
        strSQL = strSQL & "    FOR XML PATH ('')), 1, 1, '' "
        strSQL = strSQL & "  ) AS CASENO  "
        strSQL = strSQL & "FROM "
        strSQL = strSQL & "(SELECT DISTINCT "
        strSQL = strSQL & "  IVB.INVOICENO  "
        strSQL = strSQL & "  , IVB.SHIPPINGMARK02 "
        strSQL = strSQL & "  , IVB.ORDERNO "
        strSQL = strSQL & "  , IVB.SHIPPINGMARK04 "
        strSQL = strSQL & "  , IVB.SHIPPINGMARK05 "
        strSQL = strSQL & "  , IVB.SHIPPINGMARK06 "
        strSQL = strSQL & "  , IVB.PLNO "
        strSQL = strSQL & "  , IVB.CASENO "
        strSQL = strSQL & "  FROM "
        strSQL = strSQL & "  V_T_INV_PDF_VIEW01 IVB  "

        'strSQL = strSQL & "  LEFT JOIN M_EXL_ORIGIN_ITM ORI  "
        'strSQL = strSQL & "    ON IVB.CUSTMPN = ORI.CST_ITM_CODE  "

        'JOINする時の条件　01:品番、02:品名、03：両方
        Select Case Get_Search_Method(strCountry)
            Case "01"
                strSQL = strSQL & "      LEFT JOIN M_EXL_ORIGIN_ITM ORI  "
                strSQL = strSQL & "        On IVB.CUSTMPN = ORI.CST_ITM_CODE  "
            Case "02"
                strSQL = strSQL & "      LEFT JOIN M_EXL_ORIGIN_ITM ORI  "
                strSQL = strSQL & "        On IVB.PRODNAME = ORI.ITM_NAME  "
            Case "03"
                strSQL = strSQL & "      LEFT JOIN M_EXL_ORIGIN_ITM ORI  "
                strSQL = strSQL & "        On IVB.CUSTMPN = ORI.CST_ITM_CODE OR IVB.PRODNAME = ORI.ITM_NAME "
        End Select

        strSQL = strSQL & "WHERE "
        strSQL = strSQL & "  IVB.INVOICENO = (  "
        strSQL = strSQL & "    SELECT MAX(b.INVOICENO) AS IVNO  "
        strSQL = strSQL & "    FROM V_T_INV_PDF_VIEW01 b  "
        strSQL = strSQL & "    WHERE b.OLD_INVNO = '" & strIVno & "' AND b.QTY > 0)  "
        strSQL = strSQL & "  AND ORI.CODE = '" & strCountry & "'  "
        strSQL = strSQL & "  AND IVB.MIX_FLG = 'N')SEL "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        Dim i As Integer = 0
        Dim intCnt As Integer = 0
        Dim strArry As String = ""

        '結果を取り出す 
        While (dataread.Read())
            strArry = ""
            Dim strAry() As String = Split(dataread("CASENO"), ",")
            Dim intAry(999) As Integer

            'まずは数値型に変換
            For Each str0 As Integer In strAry
                intAry(intCnt) = Integer.Parse(str0)
                intCnt += 1
            Next

            '並び替える
            Array.Sort(intAry)

            '取り出して文字列に変換し、変数にセット
            Dim strArry2(0) As String
            intCnt = 0
            For Each int1 As Integer In intAry
                If int1 <> 0 Then
                    ReDim Preserve strArry2(intCnt)
                    strArry2(intCnt) = int1.ToString

                    intCnt += 1

                    'strArry += int1.ToString & ","
                End If
            Next

            '最後のカンマを削除
            'strArry = Mid(strArry, 1, Len(strArry) - 1)

            strArry = MargeNumber(strArry2)

            '１回目のみ客先、仕向地、MADE IN を出力
            If i = 0 Then
                '客先によって仕向地が入っていないパターンあり
                If Trim(dataread("SHIPPINGMARK06")) = "NO." Then
                    Get_CaseMark += Trim(dataread("SHIPPINGMARK02")) & " " & Trim(dataread("ORDERNO")) & " " & " " & Trim(dataread("SHIPPINGMARK04")) &
                " " & Trim(dataread("SHIPPINGMARK05")) & " " & Trim(dataread("SHIPPINGMARK06")) & " " & strArry & "/"
                Else
                    Get_CaseMark += Trim(dataread("SHIPPINGMARK02")) & " " & Trim(dataread("ORDERNO")) & " " & " " & Trim(dataread("SHIPPINGMARK04")) &
                " " & Trim(dataread("SHIPPINGMARK05")) & " No." & strArry & "/"

                End If
            Else
                Get_CaseMark += Trim(dataread("ORDERNO")) & " No." & strArry & "/"
            End If

            i += 1
        End While

        '値がある時のみ最後のスラッシュを削除
        If Get_CaseMark <> "" Then
            Get_CaseMark = Mid(Get_CaseMark, 1, Len(Get_CaseMark) - 1)
        End If

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()
    End Function

    Private Function Get_Nisugata(strIVno As String, strCountry As String) As String
        '荷姿情報の取得
        Dim strSQL As String = ""
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strAgree As String = ""

        Get_Nisugata = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()

        strSQL = ""
        strSQL = strSQL & "SELECT "
        strSQL = strSQL & " COUNT(SEL.PACKNAMES) as qty "
        strSQL = strSQL & " , SEL.PACKNAMES "
        strSQL = strSQL & " , SEL.PACKPLURAL "
        strSQL = strSQL & "FROM "
        strSQL = strSQL & "  (Select DISTINCT "
        strSQL = strSQL & "      PA.PACKNAMES "
        strSQL = strSQL & "      , PA.PACKPLURAL "
        strSQL = strSQL & "      , AAA.PACKINGCD "
        strSQL = strSQL & "      , AAA.CASENO "
        strSQL = strSQL & "      , KON.SHIPPL  "
        strSQL = strSQL & "    FROM "
        strSQL = strSQL & "      (Select DISTINCT "
        strSQL = strSQL & "          AA.*  "
        strSQL = strSQL & "        FROM "
        strSQL = strSQL & "          (Select "
        strSQL = strSQL & "              MAX(a.INVOICENO) As IVNO "
        strSQL = strSQL & "              , a.SERIALNO "
        strSQL = strSQL & "              , a.CUSTMPN "
        strSQL = strSQL & "              , a.PACKINGCD "
        strSQL = strSQL & "              , a.CASENO "
        strSQL = strSQL & "              , a.ORDNO "
        strSQL = strSQL & "              , a.PRODNAME "
        strSQL = strSQL & "              , a.PACKAGENO  "
        strSQL = strSQL & "            FROM "
        strSQL = strSQL & "              V_T_INV_HD_TB b  "
        strSQL = strSQL & "              INNER JOIN V_T_INV_BD_TB a  "
        strSQL = strSQL & "                On a.INVOICENO = b.INVOICENO  "
        strSQL = strSQL & "            WHERE "
        strSQL = strSQL & "              b.OLD_INVNO = '" & strIVno & "'  "
        strSQL = strSQL & "              AND a.QTY > 0  "
        strSQL = strSQL & "            GROUP BY "
        strSQL = strSQL & "              a.SERIALNO "
        strSQL = strSQL & "              , a.CUSTMPN "
        strSQL = strSQL & "              , a.PACKINGCD "
        strSQL = strSQL & "              , a.CASENO "
        strSQL = strSQL & "              , a.ORDNO "
        strSQL = strSQL & "              , a.PRODNAME "
        strSQL = strSQL & "              , a.PACKAGENO) AA  "

        'strSQL = strSQL & "          LEFT JOIN M_EXL_ORIGIN_ITM c  "
        'strSQL = strSQL & "            ON AA.CUSTMPN = c.CST_ITM_CODE  "

        'JOINする時の条件　01:品番、02:品名、03：両方
        Select Case Get_Search_Method(strCountry)
            Case "01"
                strSQL = strSQL & "      LEFT JOIN M_EXL_ORIGIN_ITM c  "
                strSQL = strSQL & "        On AA.CUSTMPN = c.CST_ITM_CODE  "
            Case "02"
                strSQL = strSQL & "      LEFT JOIN M_EXL_ORIGIN_ITM c  "
                strSQL = strSQL & "        On AA.PRODNAME = c.ITM_NAME  "
            Case "03"
                strSQL = strSQL & "      LEFT JOIN M_EXL_ORIGIN_ITM c  "
                strSQL = strSQL & "        On AA.CUSTMPN = c.CST_ITM_CODE OR AA.PRODNAME = c.ITM_NAME "
        End Select

        strSQL = strSQL & "        WHERE "
        strSQL = strSQL & "          c.CODE = '" & strCountry & "') AAA  "
        strSQL = strSQL & "      INNER JOIN V_M_PACK_TB PA  "
        strSQL = strSQL & "        ON AAA.PACKINGCD = PA.PACKINGCD  "
        strSQL = strSQL & "      INNER JOIN V_T_KONPO_TB KON  "
        strSQL = strSQL & "        ON AAA.PACKAGENO = KON.KONPONO) SEL  "
        strSQL = strSQL & "GROUP BY "
        strSQL = strSQL & "  SEL.PACKNAMES, SEL.PACKPLURAL "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        '結果を取り出す 
        While (dataread.Read())
            If dataread("qty") >= 2 Then
                Get_Nisugata += Trim(dataread("qty")) & " " & Trim(dataread("PACKPLURAL")) & " & "
            Else
                Get_Nisugata += Trim(dataread("qty")) & " " & Trim(dataread("PACKNAMES")) & " & "
            End If
        End While

        Get_Nisugata = Mid(Get_Nisugata, 1, Len(Get_Nisugata) - 2)

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()
    End Function

    Private Sub Make_Tsv(strIVNO As String, strNisu As String, strCase As String, strAgree As String)
        'ヘッダ,明細情報の取得と書き出し
        Dim strSQL As String = ""
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand

        Dim strCneeNM As String = ""
        Dim strCneeAD As String = ""
        Dim strBLDATE As String = ""
        Dim strINVFrom As String = ""
        Dim strINVTo As String = ""
        Dim strShip As String = ""
        Dim strCust As String = ""
        Dim strPrt As String = ""
        Dim strPrt2 As String = ""
        Dim strVoyNo As String = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()

        strSQL = ""
        strSQL = strSQL & "SELECT "
        strSQL = strSQL & "  IVH.CONSIGNEENAME "
        strSQL = strSQL & "  , IVH.CONSIGNEEADDRESS "
        strSQL = strSQL & "  , IVH.BLDATE2 "
        strSQL = strSQL & "  , IVH.INVFROM "
        strSQL = strSQL & "  , IVH.INVTO "
        strSQL = strSQL & "  , IVH.SHIPPEDPER  "
        strSQL = strSQL & "  , IVH.CUSTCODE  "
        strSQL = strSQL & "  , IVH.ALLOUTSTAMP　"
        strSQL = strSQL & "  , IVH.INVPRTDATE　"
        strSQL = strSQL & "  , IVH.VOYAGENO　"
        strSQL = strSQL & "FROM "
        strSQL = strSQL & "  V_T_INV_HD_TB IVH  "
        strSQL = strSQL & "WHERE "
        strSQL = strSQL & "  IVH.INVOICENO =   "
        strSQL = strSQL & "    (Select "
        strSQL = strSQL & "      MAX(a.INVOICENO) As IVNO  "
        strSQL = strSQL & "    FROM "
        strSQL = strSQL & "      V_T_INV_HD_TB a  "
        strSQL = strSQL & "    WHERE "
        strSQL = strSQL & "      a.OLD_INVNO = '" & strIVNO & "') "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        '結果を取り出す 
        While (dataread.Read())
            If dataread("ALLOUTSTAMP") Is DBNull.Value Then
                Label1.Text = "一括出力されていません。処理を中止します。"
                Return
            End If

            strCneeNM = Trim(dataread("CONSIGNEENAME"))
            strCneeAD = StrConv(Replace(Replace(Trim(dataread("CONSIGNEEADDRESS")), vbCrLf, " "), "　", " "), VbStrConv.Narrow)
            strBLDATE = Trim(dataread("BLDATE2"))
            strINVFrom = StrConv(Trim(dataread("INVFROM")), VbStrConv.Narrow)
            strINVTo = Trim(dataread("INVTO"))
            strShip = StrConv(Trim(dataread("SHIPPEDPER")), VbStrConv.Narrow)
            strCust = Trim(dataread("CUSTCODE"))
            strPrt = Replace(Left(Trim(dataread("ALLOUTSTAMP")), 10), "/", "")
            strPrt2 = Replace(Left(Trim(dataread("INVPRTDATE")), 10), "/", "")
            strVoyNo = StrConv(Trim(dataread("VOYAGENO")), VbStrConv.Narrow)
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()

        '客先名を取得
        Dim strCstNm As String = GET_CUST_INFO(strCust)

        '客先名から住所取得
        Select Case strCstNm
            Case "EDM", "EXM", "EXT", "EMI", "EDS", "EXC"
                strCneeAD = Get_CST_ADD("", strCstNm)
            Case "ELA", "ESP"
                If Get_CST_ADD(strCust, "") <> "" Then
                    strCneeAD = Get_CST_ADD(strCust, "")
                End If
        End Select

        'インドネシアの場合、本船名＋VoｙNo
        If strAgree = "05" Then
            strShip = strShip & " " & strVoyNo
        End If

        'TSVファイル（ヘッダ）
        Dim str As StringBuilder = New StringBuilder
        If strAgree = "15" Then
            'RCEP
            str.Append("1")         'No1
            str.Append(vbTab)
            str.Append("")          'No2
            str.Append(vbTab)
            str.Append("2701")      'No3
            str.Append(vbTab)
            str.Append("A00004143") 'No4
            str.Append(vbTab)
            str.Append("A00004143") 'No5
            str.Append(vbTab)
            str.Append(strCneeNM)   'No6
            str.Append(vbTab)
            str.Append(strCneeAD)   'No7
            str.Append(vbTab)
            str.Append("")          'No8
            str.Append(vbTab)
            str.Append("")          'No9
            str.Append(vbTab)
            str.Append(strBLDATE)   'No10
            str.Append(vbTab)
            str.Append(strINVTo)    'No11
            str.Append(vbTab)
            str.Append(strShip)     'No12
            str.Append(vbTab)
            str.Append("1")         'No13
            str.Append(vbTab)
            str.Append("1")         'No14
            str.Append(vbTab)
            str.Append("1")         'No15
            str.Append(vbTab)
            str.Append("")          'No16
            str.Append(vbTab)
            str.Append("")          'No17
            str.Append(vbTab)
            str.Append(strCase)     'No18
            str.Append(vbTab)
            str.Append(strNisu)     'No19
            str.Append(vbTab)
            str.Append("E")         'No20
            str.Append(vbCrLf)
        Else
            str.Append("1")         'No1
            str.Append(vbTab)
            str.Append("")          'No2
            str.Append(vbTab)
            str.Append("A00004143") 'No3
            str.Append(vbTab)
            str.Append("")          'No4
            str.Append(vbTab)
            str.Append("")          'No5
            str.Append(vbTab)
            str.Append("")          'No6
            str.Append(vbTab)
            str.Append("")          'No7
            str.Append(vbTab)
            str.Append("")          'No8
            str.Append(vbTab)
            str.Append("")          'No9
            str.Append(vbTab)
            str.Append("")          'No10
            str.Append(vbTab)
            str.Append("")          'No11
            str.Append(vbTab)
            str.Append("")          'No12
            str.Append(vbTab)
            str.Append("")          'No13
            str.Append(vbTab)
            str.Append("")          'No14
            str.Append(vbTab)
            str.Append("")          'No15
            str.Append(vbTab)
            str.Append("")          'No16
            str.Append(vbTab)
            str.Append("")          'No17
            str.Append(vbTab)
            str.Append("")          'No18
            str.Append(vbTab)
            str.Append("")          'No19
            str.Append(vbTab)

            If strAgree = "01" Then
                'メキシコ
                str.Append(strCneeNM)   'No20
                str.Append(vbTab)
                str.Append(strCneeAD)   'No21
                str.Append(vbTab)
                str.Append("0")         'No22
                str.Append(vbTab)
                str.Append("")          'No23
                str.Append(vbTab)
                str.Append("")          'No24
                str.Append(vbTab)
                str.Append("")          'No25
                str.Append(vbTab)
                str.Append("")          'No26
                str.Append(vbTab)
                str.Append(strINVFrom)  'No27
                str.Append(vbTab)
                str.Append(strINVTo)    'No28
                str.Append(vbTab)
                str.Append("")          'No29
                str.Append(vbTab)
                str.Append(strShip)     'No30
                str.Append(vbTab)
                str.Append("")          'No31
                str.Append(vbTab)
                str.Append(strBLDATE)   'No32
                str.Append(vbTab)
                str.Append("E")         'No33
                str.Append(vbCrLf)
            Else
                'メキシコ、RCEP以外

                str.Append("")          'No20
                str.Append(vbTab)
                str.Append(strCneeNM)   'No21
                str.Append(vbTab)
                str.Append(strCneeAD)   'No22
                str.Append(vbTab)
                str.Append("")          'No23
                str.Append(vbTab)
                str.Append("")          'No24
                str.Append(vbTab)
                str.Append(strBLDATE)   'No25
                str.Append(vbTab)
                str.Append(strINVFrom)  'No26
                str.Append(vbTab)
                str.Append("")          'No27
                str.Append(vbTab)
                str.Append(strINVTo)    'No28
                str.Append(vbTab)
                str.Append(strShip)     'No29
                str.Append(vbTab)
                str.Append("")          'No30
                str.Append(vbTab)
                str.Append("")          'No31
                str.Append(vbTab)

                '第三国インボイス有無
                If RadioButton1.Checked = True Then
                    str.Append(TextBox2.Text)          'No32
                    str.Append(vbTab)
                    str.Append(TextBox3.Text)          'No33
                    str.Append(vbTab)
                Else
                    str.Append("")          'No32
                    str.Append(vbTab)
                    str.Append("")          'No33
                    str.Append(vbTab)
                End If

                str.Append(strCase)     'No34
                str.Append(vbTab)
                str.Append(strNisu)     'No35
                str.Append(vbTab)
                str.Append("")          'No36
                str.Append(vbTab)
                str.Append("2701")      'No37
                str.Append(vbTab)
                str.Append("E")         'No38
                str.Append(vbCrLf)
            End If
        End If

        'TSVファイル（明細）

        '明細取得用のＳＱＬ作成
        If Get_Search_Method(strAgree) <> "03" Then
            '品番　ＯＲ　品名
            Call Make_SQL(strIVNO, strSQL, strAgree)
        Else
            '品番　＆　品名
            Call Make_SQL_Name(strIVNO, strSQL, strAgree)
        End If

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        Dim strIVNOFull As String = Mid(strBLDATE, 3, 2) & Mid(strBLDATE, 6, 2) & Mid(strBLDATE, 9, 2)
        Dim strETD As String = Replace(strBLDATE, "/", "")


        '結果を取り出す 
        While (dataread.Read())
            If strAgree = "15" Then
                'RCEP
                str.Append("3")         'No1
                str.Append(vbTab)
                str.Append("")          'No2
                str.Append(vbTab)
                str.Append(Trim(dataread("ORI_JDG_NO")))    'No3
                str.Append(vbTab)
                str.Append(Trim(dataread("ITM_QTY")))       'No4
                str.Append(vbTab)
                str.Append(Trim(dataread("UNIT")))          'No5
                str.Append(vbTab)
                str.Append("IV-" & strIVNO & "-" & strIVNOFull)  'No6
                str.Append(vbTab)
                str.Append(strPrt2)                          'No7
                str.Append(vbTab)
                str.Append("")          'No8
                str.Append(vbTab)
                str.Append("")          'No9
                str.Append(vbTab)
                str.Append(Trim(dataread("ORI_ITM_NAME")))  'No10
                str.Append(vbTab)
                str.Append("E")                             'No11
                str.Append(vbCrLf)
            Else
                str.Append("3")         'No1
                str.Append(vbTab)
                str.Append("")          'No2
                str.Append(vbTab)
                str.Append(Trim(dataread("ORI_JDG_NO")))    'No3
                str.Append(vbTab)
                str.Append("")                              'No4
                str.Append(vbTab)
                str.Append(Trim(dataread("ITM_QTY")))       'No5
                str.Append(vbTab)
                str.Append(Trim(dataread("UNIT")))          'No6
                str.Append(vbTab)
                str.Append("IV-" & strIVNO & "-" & strIVNOFull)  'No7
                str.Append(vbTab)

                If strAgree = "01" Then
                    'メキシコ
                    str.Append("")                              'No8
                    str.Append(vbTab)
                    str.Append(Trim(dataread("ORI_ITM_NAME")))  'No9
                    str.Append(vbTab)
                    str.Append("E")                             'No10
                    str.Append(vbCrLf)
                Else
                    '上記以外
                    str.Append(strPrt2)                          'No8
                    str.Append(vbTab)
                    str.Append("")                              'No9
                    str.Append(vbTab)
                    str.Append(Trim(dataread("ORI_ITM_NAME")))  'No10
                    str.Append(vbTab)
                    str.Append("E")                             'No11
                    str.Append(vbCrLf)
                End If
            End If
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()

        'Contentをクリア
        Response.ClearContent()

        'Contentを設定
        'Response.ContentEncoding = System.Text.Encoding.GetEncoding("shift-jis")  'Shift-JISで出力したい場合
        Response.ContentEncoding = System.Text.Encoding.UTF8  'UTF-8で出力したい場合
        'Response.ContentType = "text/csv"
        Response.ContentType = "application/octet-stream"

        Dim strFile As String = strCust & "_" & strBLDATE & "_" & strIVNO

        '表示ファイル名を指定
        Dim viewFileName As String = HttpUtility.UrlEncode(strFile & ".tsv")
        Response.AddHeader("Content-Disposition", "attachment;filename=" + viewFileName)

        'BOMを送信
        Dim bom As Byte() = System.Text.Encoding.UTF8.GetPreamble()
        Response.BinaryWrite(bom)

        'CSVデータを書き込み
        Response.BinaryWrite(Encoding.UTF8.GetBytes(str.ToString))
        'Response.Write(str.ToString)

        'ダウンロード実行
        Response.Flush()
        Response.End()

    End Sub

    Private Sub Make_SQL(strIVNO As String, ByRef strSQL As String, strAgree As String)
        '各国用明細のSQL作成（検索条件が品番または品名）

        strSQL = ""
        strSQL = strSQL & "SELECT "
        strSQL = strSQL & "  SEL.ORI_JDG_NO "
        strSQL = strSQL & "  , SUM(SEL.QTY) AS ITM_QTY "
        strSQL = strSQL & "  , SEL.UNIT "
        strSQL = strSQL & "  , SEL.ORI_ITM_NAME  "
        strSQL = strSQL & "FROM "
        strSQL = strSQL & "  (  "
        strSQL = strSQL & "    SELECT "
        strSQL = strSQL & "      ORI.ORI_JDG_NO "
        strSQL = strSQL & "      , IVB.QTY "
        strSQL = strSQL & "      , CASE  "
        strSQL = strSQL & "        WHEN IVB.QTY = 1  "
        strSQL = strSQL & "          THEN UNI.SINGULARNAME  "
        strSQL = strSQL & "        ELSE UNI.PLURALNAME  "
        strSQL = strSQL & "        END AS UNIT "
        strSQL = strSQL & "      , ORI.ORI_ITM_NAME  "
        strSQL = strSQL & "    FROM "
        strSQL = strSQL & "      V_T_INV_BD_TB IVB  "

        'JOINする時の条件　01:品番、02:品名、03：両方
        Select Case Get_Search_Method(strAgree)
            Case "01"
                strSQL = strSQL & "      LEFT JOIN M_EXL_ORIGIN_ITM ORI  "
                strSQL = strSQL & "        On IVB.CUSTMPN = ORI.CST_ITM_CODE  "
            Case "02"
                strSQL = strSQL & "      LEFT JOIN M_EXL_ORIGIN_ITM ORI  "
                strSQL = strSQL & "        On IVB.PRODNAME = ORI.ITM_NAME  "
        End Select

        strSQL = strSQL & "      LEFT JOIN V_M_UNIT_TB UNI  "
        strSQL = strSQL & "        ON IVB.UNITCD = UNI.UNITCD  "
        strSQL = strSQL & "    WHERE "
        strSQL = strSQL & "      IVB.INVOICENO = (  "
        strSQL = strSQL & "        SELECT "
        strSQL = strSQL & "          MAX(a.INVOICENO) AS IVNO  "
        strSQL = strSQL & "        FROM "
        strSQL = strSQL & "          V_T_INV_HD_TB b  "
        strSQL = strSQL & "          INNER JOIN V_T_INV_BD_TB a  "
        strSQL = strSQL & "            ON a.INVOICENO = b.INVOICENO  "
        strSQL = strSQL & "        WHERE "
        strSQL = strSQL & "          b.OLD_INVNO = '" & strIVNO & "' "
        strSQL = strSQL & "          AND a.QTY > 0 "
        strSQL = strSQL & "      )  "
        strSQL = strSQL & "      AND ORI.CODE = '" & strAgree & "' "
        strSQL = strSQL & "  ) SEL  "
        strSQL = strSQL & "GROUP BY "
        strSQL = strSQL & "  SEL.ORI_JDG_NO "
        strSQL = strSQL & "  , SEL.UNIT "
        strSQL = strSQL & "  , SEL.ORI_ITM_NAME "

    End Sub

    Private Sub Make_SQL_Name(strIVNO As String, ByRef strSQL As String, strAgree As String)
        '各国用明細のSQL作成（検索条件が品番と品名）

        strSQL = ""
        strSQL = strSQL & "SELECT "
        strSQL = strSQL & "  SEL.ORI_JDG_NO "
        strSQL = strSQL & "  , SUM(SEL.QTY) As ITM_QTY "
        strSQL = strSQL & "  , Case  "
        strSQL = strSQL & "    When SUM(SEL.QTY) = 1  "
        strSQL = strSQL & "      Then d.SINGULARNAME  "
        strSQL = strSQL & "    Else d.PLURALNAME  "
        strSQL = strSQL & "    End As UNIT "
        strSQL = strSQL & "  , SEL.ORI_ITM_NAME  "
        strSQL = strSQL & "FROM "
        strSQL = strSQL & "  (  "
        strSQL = strSQL & "    SELECT "
        strSQL = strSQL & "      INVOICENO "
        strSQL = strSQL & "      , IVB.SERIALNO "
        strSQL = strSQL & "      , RTRIM(IVB.CUSTMPN) As CUSTMPN "
        strSQL = strSQL & "      , IVB.UNITCD "
        strSQL = strSQL & "      , IVB.QTY "
        strSQL = strSQL & "      , IVB.PRODNAME "
        strSQL = strSQL & "      , ORI.ITM_NAME "
        strSQL = strSQL & "      , ORI.ORI_ITM_NAME "
        strSQL = strSQL & "      , ORI.ORI_JDG_NO  "
        strSQL = strSQL & "    FROM "
        strSQL = strSQL & "      V_T_INV_BD_TB IVB  "
        strSQL = strSQL & "      LEFT JOIN M_EXL_ORIGIN_ITM ORI  "
        strSQL = strSQL & "        ON IVB.CUSTMPN = ORI.CST_ITM_CODE  "
        strSQL = strSQL & "    WHERE "
        strSQL = strSQL & "      IVB.INVOICENO = (  "
        strSQL = strSQL & "        SELECT "
        strSQL = strSQL & "          MAX(a.INVOICENO) As IVNO  "
        strSQL = strSQL & "        FROM "
        strSQL = strSQL & "          V_T_INV_HD_TB b  "
        strSQL = strSQL & "          INNER JOIN V_T_INV_BD_TB a  "
        strSQL = strSQL & "            ON a.INVOICENO = b.INVOICENO  "
        strSQL = strSQL & "        WHERE "
        strSQL = strSQL & "          b.OLD_INVNO = '" & strIVNO & "' "
        strSQL = strSQL & "          AND a.QTY > 0 "
        strSQL = strSQL & "      )  "
        strSQL = strSQL & "      AND ORI.CODE = '" & strAgree & "' "
        strSQL = strSQL & "  ) SEL  "
        strSQL = strSQL & "  LEFT JOIN V_M_UNIT_TB d  "
        strSQL = strSQL & "    ON SEL.UNITCD = d.UNITCD  "
        strSQL = strSQL & "GROUP BY "
        strSQL = strSQL & "  SEL.ORI_ITM_NAME "
        strSQL = strSQL & "  , SEL.ORI_JDG_NO "
        strSQL = strSQL & "  , d.SINGULARNAME "
        strSQL = strSQL & "  , d.PLURALNAME "
        strSQL = strSQL & "  , SEL.ORI_ITM_NAME "
        strSQL = strSQL & "UNION  "
        strSQL = strSQL & "SELECT "
        strSQL = strSQL & "  DISTINCT ORI2.ORI_JDG_NO "
        strSQL = strSQL & "  , SUM(SEL.QTY) AS QTY "
        strSQL = strSQL & "  , CASE  "
        strSQL = strSQL & "    WHEN SUM(SEL.QTY) = 1  "
        strSQL = strSQL & "      THEN d.SINGULARNAME  "
        strSQL = strSQL & "    ELSE d.PLURALNAME  "
        strSQL = strSQL & "    END AS UNIT "
        strSQL = strSQL & "  , ORI2.ORI_ITM_NAME  "
        strSQL = strSQL & "FROM "
        strSQL = strSQL & "  (  "
        strSQL = strSQL & "    SELECT DISTINCT "
        strSQL = strSQL & "      IVB.*  "
        strSQL = strSQL & "    FROM "
        strSQL = strSQL & "      V_T_INV_BD_TB IVB  "
        strSQL = strSQL & "      LEFT JOIN M_EXL_ORIGIN_ITM ORI  "
        strSQL = strSQL & "        ON IVB.CUSTMPN = ORI.CST_ITM_CODE  "
        strSQL = strSQL & "    WHERE "
        strSQL = strSQL & "      IVB.INVOICENO = (  "
        strSQL = strSQL & "        SELECT "
        strSQL = strSQL & "          MAX(a.INVOICENO) AS IVNO  "
        strSQL = strSQL & "        FROM "
        strSQL = strSQL & "          V_T_INV_HD_TB b  "
        strSQL = strSQL & "          INNER JOIN V_T_INV_BD_TB a  "
        strSQL = strSQL & "            ON a.INVOICENO = b.INVOICENO  "
        strSQL = strSQL & "        WHERE "
        strSQL = strSQL & "          b.OLD_INVNO = '" & strIVNO & "' "
        strSQL = strSQL & "          AND a.QTY > 0 "
        strSQL = strSQL & "      ) "
        strSQL = strSQL & "	  AND NOT EXISTS ( "
        strSQL = strSQL & "	  SELECT "
        strSQL = strSQL & "        RTRIM(IVB.CUSTMPN) As CUSTMPN "
        strSQL = strSQL & "      FROM "
        strSQL = strSQL & "        V_T_INV_BD_TB IVB_E  "
        strSQL = strSQL & "        LEFT JOIN M_EXL_ORIGIN_ITM ORI  "
        strSQL = strSQL & "        ON IVB.CUSTMPN = ORI.CST_ITM_CODE  "
        strSQL = strSQL & "      WHERE "
        strSQL = strSQL & "        IVB.INVOICENO = (  "
        strSQL = strSQL & "        SELECT "
        strSQL = strSQL & "          MAX(a.INVOICENO) As IVNO  "
        strSQL = strSQL & "        FROM "
        strSQL = strSQL & "          V_T_INV_HD_TB b  "
        strSQL = strSQL & "          INNER JOIN V_T_INV_BD_TB a  "
        strSQL = strSQL & "          ON a.INVOICENO = b.INVOICENO  "
        strSQL = strSQL & "        WHERE "
        strSQL = strSQL & "          b.OLD_INVNO = '" & strIVNO & "' "
        strSQL = strSQL & "          AND a.QTY > 0)  "
        strSQL = strSQL & "      AND ORI.CODE = '" & strAgree & "' "
        strSQL = strSQL & "	  AND IVB.CUSTMPN = IVB_E.CUSTMPN "
        strSQL = strSQL & "	  ) "
        strSQL = strSQL & "  ) SEL  "
        strSQL = strSQL & "  INNER JOIN M_EXL_ORIGIN_ITM ORI2   "
        strSQL = strSQL & "    ON SEL.PRODNAME = ORI2.ITM_NAME   "
        strSQL = strSQL & "  LEFT JOIN V_M_UNIT_TB d  "
        strSQL = strSQL & "    ON SEL.UNITCD = d.UNITCD  "
        strSQL = strSQL & "WHERE "
        strSQL = strSQL & "  ORI2.CODE = '" & strAgree & "' "
        strSQL = strSQL & "  AND ORI2.REMARKS = '01'  "
        strSQL = strSQL & "GROUP BY "
        strSQL = strSQL & "  ORI2.ORI_ITM_NAME "
        strSQL = strSQL & "  , ORI2.ORI_JDG_NO "
        strSQL = strSQL & "  , d.SINGULARNAME "
        strSQL = strSQL & "  , d.PLURALNAME "
        strSQL = strSQL & "  , ORI2.ORI_ITM_NAME "

    End Sub

    Private Function GET_CUST_INFO(strCstCd As String) As String
        '客先略称を取得

        Dim strSQL As String = ""
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand

        GET_CUST_INFO = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()

        strSQL = ""
        strSQL = strSQL & "SELECT CUST_CD FROM M_EXL_CUST a "
        strSQL = strSQL & "WHERE CUST_ANAME = '" & strCstCd & "' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        '結果を取り出す 
        While (dataread.Read())
            GET_CUST_INFO = Trim(dataread("CUST_CD"))
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()
    End Function

    Private Function Get_Search_Method(strAgree As String) As String
        '各国のEPA対象取得の方法を取得

        Dim strSQL As String = ""
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand

        Get_Search_Method = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()

        strSQL = ""
        strSQL = strSQL & "SELECT JDG_METHOD FROM M_EXL_EPA_COUNTRY "
        strSQL = strSQL & "WHERE CODE = '" & strAgree & "' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        '結果を取り出す 
        While (dataread.Read())
            Get_Search_Method = dataread("JDG_METHOD")
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()
    End Function

    Private Function Get_CST_ADD(strCstCd As String, strCust As String) As String
        '客先住所を取得

        Dim strSQL As String = ""
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand

        Get_CST_ADD = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()

        strSQL = ""
        strSQL = strSQL & "SELECT CUST_ADD FROM M_EXL_EPA_ADD "
        If strCust = "" Then
            '客先名が空白の場合、EXDG以外
            strSQL = strSQL & "WHERE CUST_CD = '" & strCstCd & "' "

        ElseIf strCust <> "" Then
            '客先名が空白で無い場合、EXDG
            strSQL = strSQL & "WHERE CUST = '" & strCust & "' "
        End If

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        '結果を取り出す 
        While (dataread.Read())
            Get_CST_ADD = dataread("CUST_ADD")
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()
    End Function

    Private Function MargeNumber(target_range() As String) As String
        '連続するケースマークの数値をハイフンでつなげる
        Dim iMin As Long : iMin = LBound(target_range)
        Dim iMax As Long : iMax = UBound(target_range)
        Dim TempCode As String
        MargeNumber = ""

        Dim arr() As String
        ReDim arr(0 To iMax)
        Dim arr2() As String
        ReDim arr2(0 To iMax)
        For i = iMin To iMax
            arr(i) = target_range(i)
            arr2(i) = target_range(i)
        Next

        Select Case UBound(target_range)
            Case 0
                MargeNumber = target_range(iMin)
            Case 1
                ' 要素が２個しかない場合は、「,」でつなぐ。
                MargeNumber = target_range(iMin) & ", " & arr(iMax)

            Case Else
                ' 要素が３個以上の場合。

                ' ２個前の要素との差が２の場合、３つの値は連続している。
                ' 従って、一つ前の要素を空白に置き換えて省略とする。
                ' 例）数字が１，２，３と並んでいる場合、１と３の差は２。
                ' 　　従って、１，２，３は必ず連続しているといえるため、
                '　　 ２を空白に置き換えて１，，３とする。
                ' ※この時、元の配列を置き換えてしまうと、その後の数字を
                ' 　確認の際、支障をきたす。そのため、元データ配列「arr」
                ' 　に加え、置き換え後の配列「arr2」を準備している。
                For i = iMin + 2 To iMax
                    If CLng(arr(i - 2)) + 2 = CLng(arr(i)) Then
                        arr2(i - 1) = vbNullString
                    End If
                Next

                TempCode = Join(arr2, ",")

                ' ２個以上「,」が連続しているということは、その前後の数が
                ' 連続していることを表す。従って、２個以上連続する「,」を
                ' まとめて「～」に置き換えている。
                Dim myReg As Object
                myReg = CreateObject("VBScript.RegExp")
                myReg.Pattern = ",{2,}"
                myReg.Global = True
                If myReg.test(TempCode) Then
                    MargeNumber = myReg.Replace(TempCode, "-")
                Else
                    MargeNumber = TempCode
                End If
        End Select

    End Function

    Private Sub Make_Tsv_for_Indonesia(strIVNO As String, strNisu As String, strCase As String, strAgree As String)
        'インドネシア用のTSV作成(2023/06/26～用)ヘッダ,明細情報の取得と書き出し
        Dim strSQL As String = ""
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand

        Dim strCneeNM As String = ""
        Dim strCneeAD As String = ""
        Dim strBLDATE As String = ""
        Dim strINVFrom As String = ""
        Dim strINVTo As String = ""
        Dim strShip As String = ""
        Dim strCust As String = ""
        Dim strPrt As String = ""
        Dim strVoyNo As String = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()

        strSQL = ""
        strSQL = strSQL & "SELECT "
        strSQL = strSQL & "  IVH.CONSIGNEENAME "
        strSQL = strSQL & "  , IVH.CONSIGNEEADDRESS "
        strSQL = strSQL & "  , IVH.BLDATE2 "
        strSQL = strSQL & "  , IVH.INVFROM "
        strSQL = strSQL & "  , IVH.INVTO "
        strSQL = strSQL & "  , IVH.SHIPPEDPER  "
        strSQL = strSQL & "  , IVH.CUSTCODE  "
        strSQL = strSQL & "  , IVH.ALLOUTSTAMP　"
        strSQL = strSQL & "  , IVH.VOYAGENO　"
        strSQL = strSQL & "FROM "
        strSQL = strSQL & "  V_T_INV_HD_TB IVH  "
        strSQL = strSQL & "WHERE "
        strSQL = strSQL & "  IVH.INVOICENO =   "
        strSQL = strSQL & "    (Select "
        strSQL = strSQL & "      MAX(a.INVOICENO) As IVNO  "
        strSQL = strSQL & "    FROM "
        strSQL = strSQL & "      V_T_INV_HD_TB a  "
        strSQL = strSQL & "    WHERE "
        strSQL = strSQL & "      a.OLD_INVNO = '" & strIVNO & "') "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        '結果を取り出す 
        While (dataread.Read())
            If dataread("ALLOUTSTAMP") Is DBNull.Value Then
                Label1.Text = "一括出力されていません。処理を中止します。"
                Return
            End If

            strCneeNM = Trim(dataread("CONSIGNEENAME"))
            strCneeAD = StrConv(Replace(Replace(Trim(dataread("CONSIGNEEADDRESS")), vbCrLf, " "), "　", " "), VbStrConv.Narrow)
            strBLDATE = Trim(dataread("BLDATE2"))
            strINVFrom = StrConv(Trim(dataread("INVFROM")), VbStrConv.Narrow)
            strINVTo = Trim(dataread("INVTO"))
            strShip = StrConv(Trim(dataread("SHIPPEDPER")), VbStrConv.Narrow)
            strCust = Trim(dataread("CUSTCODE"))
            strPrt = Replace(Left(Trim(dataread("ALLOUTSTAMP")), 10), "/", "")
            strVoyNo = StrConv(Trim(dataread("VOYAGENO")), VbStrConv.Narrow)
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()

        '客先名を取得
        Dim strCstNm As String = GET_CUST_INFO(strCust)

        '客先名から住所取得
        Select Case strCstNm
            Case "EDM", "EXM", "EXT", "EMI", "EDS", "EXC"
                strCneeAD = Get_CST_ADD("", strCstNm)
            Case "ELA", "ESP"
                If Get_CST_ADD(strCust, "") <> "" Then
                    strCneeAD = Get_CST_ADD(strCust, "")
                End If
        End Select

        'インドネシアの場合、本船名＋VoｙNo
        If strAgree = "05" Then
            strShip = strShip & " " & strVoyNo
        End If

        'TSVファイル（ヘッダ）
        Dim str As StringBuilder = New StringBuilder

        str.Append("1")         'No1
        str.Append(vbTab)
        str.Append("")          'No2
        str.Append(vbTab)
        str.Append("A00004143") 'No3
        str.Append(vbTab)
        str.Append(strCneeNM)   'No4
        str.Append(vbTab)
        str.Append("")          'No5
        str.Append(vbTab)
        str.Append(strCneeAD)   'No6
        str.Append(vbTab)
        str.Append("")          'No7
        str.Append(vbTab)
        str.Append("")          'No8
        str.Append(vbTab)
        str.Append(strBLDATE)   'No9
        str.Append(vbTab)
        str.Append("")          'No10
        str.Append(vbTab)
        str.Append(strINVFrom)  'No11
        str.Append(vbTab)
        str.Append("")          'No12
        str.Append(vbTab)
        str.Append("")          'No13
        str.Append(vbTab)
        str.Append("IDJKT")     'No14
        str.Append(vbTab)
        str.Append(strINVTo)    'No15
        str.Append(vbTab)
        str.Append(strShip)     'No16
        str.Append(vbTab)
        str.Append("")          'No17
        str.Append(vbTab)
        str.Append("")          'No18
        str.Append(vbTab)

        '第三国インボイス有無
        If RadioButton1.Checked = True Then
            str.Append(TextBox2.Text)          'No19
            str.Append(vbTab)
            str.Append(TextBox4.Text)          'No20
            str.Append(vbTab)
            str.Append(TextBox3.Text)          'No21
            str.Append(vbTab)
        Else
            str.Append("")          'No19
            str.Append(vbTab)
            str.Append("")          'No20
            str.Append(vbTab)
            str.Append("")          'No21
            str.Append(vbTab)
        End If

        str.Append(strCase)     'No22
        str.Append(vbTab)
        str.Append(Get_Nisugata_for_Indonesia(strIVNO, strAgree))     'No23
        str.Append(vbTab)
        str.Append("D97")       'No24
        str.Append(vbTab)
        str.Append("PB")        'No25
        str.Append(vbTab)
        str.Append("")          'No26
        str.Append(vbTab)
        str.Append("2701")      'No27
        str.Append(vbTab)
        str.Append("E")         'No28
        str.Append(vbCrLf)

        'TSVファイル（明細）

        '明細取得用のＳＱＬ作成
        If Get_Search_Method(strAgree) <> "03" Then
            '品番　ＯＲ　品名
            Call Make_SQL(strIVNO, strSQL, strAgree)
        Else
            '品番　＆　品名
            Call Make_SQL_Name(strIVNO, strSQL, strAgree)
        End If

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        Dim strIVNOFull As String = Mid(strBLDATE, 3, 2) & Mid(strBLDATE, 6, 2) & Mid(strBLDATE, 9, 2)
        Dim strETD As String = Replace(strBLDATE, "/", "")


        '結果を取り出す 
        While (dataread.Read())

            str.Append("3")         'No1
            str.Append(vbTab)
            str.Append("")          'No2
            str.Append(vbTab)
            str.Append(Trim(dataread("ORI_JDG_NO")))    'No3
            str.Append(vbTab)
            str.Append("1")                             'No4
            str.Append(vbTab)
            str.Append(Trim(dataread("ITM_QTY")))       'No5
            str.Append(vbTab)
            str.Append("H87")                           'No6
            str.Append(vbTab)
            str.Append("IV-" & strIVNO & "-" & strIVNOFull)  'No7
            str.Append(vbTab)
            str.Append(strPrt)                          'No8
            str.Append(vbTab)
            str.Append("")                              'No9
            str.Append(vbTab)
            str.Append("")                              'No10
            str.Append(vbTab)
            str.Append("")                              'No11
            str.Append(vbTab)
            str.Append("")                              'No12
            str.Append(vbTab)
            str.Append("")                              'No13
            str.Append(vbTab)
            str.Append("")                              'No14
            str.Append(vbTab)
            str.Append("")                              'No15
            str.Append(vbTab)
            str.Append("")                              'No16
            str.Append(vbTab)
            str.Append("")                              'No17
            str.Append(vbTab)
            str.Append("")                              'No18
            str.Append(vbTab)
            str.Append("")                              'No19
            str.Append(vbTab)
            str.Append("")                              'No20
            str.Append(vbTab)
            str.Append("")                              'No21
            str.Append(vbTab)
            str.Append(Trim(dataread("ORI_ITM_NAME")))  'No22
            str.Append(vbTab)
            str.Append("E")                             'No23
            str.Append(vbCrLf)

        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()

        'Contentをクリア
        Response.ClearContent()

        'Contentを設定
        'Response.ContentEncoding = System.Text.Encoding.GetEncoding("shift-jis")  'Shift-JISで出力したい場合
        Response.ContentEncoding = System.Text.Encoding.UTF8  'UTF-8で出力したい場合
        'Response.ContentType = "text/csv"
        Response.ContentType = "application/octet-stream"

        Dim strFile As String = strCust & "_" & strBLDATE & "_" & strIVNO

        '表示ファイル名を指定
        Dim viewFileName As String = HttpUtility.UrlEncode(strFile & ".tsv")
        Response.AddHeader("Content-Disposition", "attachment;filename=" + viewFileName)

        'BOMを送信
        Dim bom As Byte() = System.Text.Encoding.UTF8.GetPreamble()
        Response.BinaryWrite(bom)

        'CSVデータを書き込み
        Response.BinaryWrite(Encoding.UTF8.GetBytes(str.ToString))
        'Response.Write(str.ToString)

        'ダウンロード実行
        Response.Flush()
        Response.End()

    End Sub

    Private Function Get_Nisugata_for_Indonesia(strIVno As String, strCountry As String) As String
        '荷姿情報の取得
        Dim strSQL As String = ""
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strAgree As String = ""
        Dim intCnt As Integer = 0

        Get_Nisugata_for_Indonesia = ""

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()

        strSQL = ""
        strSQL = strSQL & "SELECT "
        strSQL = strSQL & " COUNT(SEL.PACKNAMES) as qty "
        strSQL = strSQL & "FROM "
        strSQL = strSQL & "  (Select DISTINCT "
        strSQL = strSQL & "      PA.PACKNAMES "
        strSQL = strSQL & "      , PA.PACKPLURAL "
        strSQL = strSQL & "      , AAA.PACKINGCD "
        strSQL = strSQL & "      , AAA.CASENO "
        strSQL = strSQL & "      , KON.SHIPPL  "
        strSQL = strSQL & "    FROM "
        strSQL = strSQL & "      (Select DISTINCT "
        strSQL = strSQL & "          AA.*  "
        strSQL = strSQL & "        FROM "
        strSQL = strSQL & "          (Select "
        strSQL = strSQL & "              MAX(a.INVOICENO) As IVNO "
        strSQL = strSQL & "              , a.SERIALNO "
        strSQL = strSQL & "              , a.CUSTMPN "
        strSQL = strSQL & "              , a.PACKINGCD "
        strSQL = strSQL & "              , a.CASENO "
        strSQL = strSQL & "              , a.ORDNO "
        strSQL = strSQL & "              , a.PRODNAME "
        strSQL = strSQL & "              , a.PACKAGENO  "
        strSQL = strSQL & "            FROM "
        strSQL = strSQL & "              V_T_INV_HD_TB b  "
        strSQL = strSQL & "              INNER JOIN V_T_INV_BD_TB a  "
        strSQL = strSQL & "                On a.INVOICENO = b.INVOICENO  "
        strSQL = strSQL & "            WHERE "
        strSQL = strSQL & "              b.OLD_INVNO = '" & strIVno & "'  "
        strSQL = strSQL & "              AND a.QTY > 0  "
        strSQL = strSQL & "            GROUP BY "
        strSQL = strSQL & "              a.SERIALNO "
        strSQL = strSQL & "              , a.CUSTMPN "
        strSQL = strSQL & "              , a.PACKINGCD "
        strSQL = strSQL & "              , a.CASENO "
        strSQL = strSQL & "              , a.ORDNO "
        strSQL = strSQL & "              , a.PRODNAME "
        strSQL = strSQL & "              , a.PACKAGENO) AA  "

        'JOINする時の条件　01:品番、02:品名、03：両方
        Select Case Get_Search_Method(strCountry)
            Case "01"
                strSQL = strSQL & "      LEFT JOIN M_EXL_ORIGIN_ITM c  "
                strSQL = strSQL & "        On AA.CUSTMPN = c.CST_ITM_CODE  "
            Case "02"
                strSQL = strSQL & "      LEFT JOIN M_EXL_ORIGIN_ITM c  "
                strSQL = strSQL & "        On AA.PRODNAME = c.ITM_NAME  "
            Case "03"
                strSQL = strSQL & "      LEFT JOIN M_EXL_ORIGIN_ITM c  "
                strSQL = strSQL & "        On AA.CUSTMPN = c.CST_ITM_CODE OR AA.PRODNAME = c.ITM_NAME "
        End Select

        strSQL = strSQL & "        WHERE "
        strSQL = strSQL & "          c.CODE = '" & strCountry & "') AAA  "
        strSQL = strSQL & "      INNER JOIN V_M_PACK_TB PA  "
        strSQL = strSQL & "        ON AAA.PACKINGCD = PA.PACKINGCD  "
        strSQL = strSQL & "      INNER JOIN V_T_KONPO_TB KON  "
        strSQL = strSQL & "        ON AAA.PACKAGENO = KON.KONPONO) SEL  "
        strSQL = strSQL & "GROUP BY "
        strSQL = strSQL & "  SEL.PACKNAMES, SEL.PACKPLURAL "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        '結果を取り出す 
        While (dataread.Read())
            intCnt += Integer.Parse(Trim(dataread("qty")))
        End While

        Get_Nisugata_for_Indonesia = intCnt

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()
    End Function
End Class
