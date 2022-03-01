Imports Microsoft.VisualBasic
Imports System.Data.Common
Imports System.Data
Public Class DBAccess
    Dim Factroy As DbProviderFactory
    Dim Conn As DbConnection
    Dim Cmd As DbCommand
    Dim Da As DbDataAdapter
    Dim Ds As DataSet
    Dim StrSQL As String

    Public Function Dbconnect() As DbConnection
        Dim settings As ConnectionStringSettings

        Factroy =
        DbProviderFactories.GetFactory("System.Data.SqlClient")

        Conn = Factroy.CreateConnection()
        settings = ConfigurationManager.
                    ConnectionStrings("EXPDBConnectionString")
        ' 接続文字列の設定
        Conn.ConnectionString = settings.ConnectionString

        Conn.Open()

        Return Conn

    End Function

    Public Function Dbconnect2() As DbConnection
        Dim settings As ConnectionStringSettings

        Factroy =
        DbProviderFactories.GetFactory("System.Data.SqlClient")

        Conn = Factroy.CreateConnection()
        settings = ConfigurationManager.
                    ConnectionStrings("KBHWPA85ConnectionString")
        ' 接続文字列の設定
        Conn.ConnectionString = settings.ConnectionString

        Conn.Open()

        Return Conn

    End Function

    Public Function GET_CS_RESULT(strCUST As String) As DataSet
        'CSマニュアルデータ取得時
        Conn = Me.Dbconnect
        Cmd = Conn.CreateCommand

        StrSQL = StrSQL & ""
        StrSQL = StrSQL & "SELECT "
        StrSQL = StrSQL & "  NEW_CODE "
        StrSQL = StrSQL & ", OLD_CODE "
        StrSQL = StrSQL & ", CUST_NM "
        StrSQL = StrSQL & ", CUST_AB "
        StrSQL = StrSQL & ", INCOTEM "
        StrSQL = StrSQL & ", BL_TYPE "
        StrSQL = StrSQL & ", BL_SEND "
        StrSQL = StrSQL & ", CUST_ADDRESS "
        StrSQL = StrSQL & ", CONSIGNEE "
        StrSQL = StrSQL & ", CNEE_NM_SI "
        StrSQL = StrSQL & ", FIN_DESTINATION "
        StrSQL = StrSQL & ", NOTIFY "
        StrSQL = StrSQL & ", FORWARDER_INFO "
        StrSQL = StrSQL & ", CUST_REQ "
        StrSQL = StrSQL & ", IV_NECE "
        StrSQL = StrSQL & ", PL_NECE "
        StrSQL = StrSQL & ", BL_NECE "
        StrSQL = StrSQL & ", CO_NECE "
        StrSQL = StrSQL & ", EPA_NECE "
        StrSQL = StrSQL & ", WOOD_NECE "
        StrSQL = StrSQL & ", DELI_NECE "
        StrSQL = StrSQL & ", INSP_NECE "
        StrSQL = StrSQL & ", ERL_NECE "
        StrSQL = StrSQL & ", VESS_NECE "
        StrSQL = StrSQL & ", SHIP_DAY_OF_WEEK "
        StrSQL = StrSQL & ", DESTINATION "
        StrSQL = StrSQL & ", SHIPMENT_KBN "
        StrSQL = StrSQL & ", LT "
        StrSQL = StrSQL & ", CONTAINER_CLEANING "
        StrSQL = StrSQL & ", LC "
        StrSQL = StrSQL & ", CONSIGNEE_OF_SI "
        StrSQL = StrSQL & ", CONSIGNEE_OF_SI_ADDRESS "
        StrSQL = StrSQL & ", FINAL_DES "
        StrSQL = StrSQL & ", FINAL_DES_ADDRESS "
        StrSQL = StrSQL & ", FORWARDER_NM "
        StrSQL = StrSQL & ", FORWARDER_STAFF_NM "
        StrSQL = StrSQL & ", DOC_NECESSITY "
        StrSQL = StrSQL & ", FTA "
        StrSQL = StrSQL & ", CERTIFICATE_OF_CONFORMITY "
        StrSQL = StrSQL & ", DOC_OF_EGYPT "
        StrSQL = StrSQL & "FROM "
        StrSQL = StrSQL & "  T_EXL_CSMANUAL "
        If strCUST <> "" Then
            StrSQL = StrSQL & "WHERE NEW_CODE = '" & strCUST & "' "
        End If
        StrSQL = StrSQL & "ORDER BY NEW_CODE "

        Cmd.CommandText = StrSQL

        Da = Factroy.CreateDataAdapter()
        Da.SelectCommand = Cmd
        Ds = New DataSet
        Da.Fill(Ds)
        Return Ds
    End Function

    Public Function GET_RESULT_IV_HEADER() As DataSet
        '全データ取得時(BookingvsIVﾍｯﾀﾞ比較用)
        Conn = Me.Dbconnect
        Cmd = Conn.CreateCommand

        StrSQL = StrSQL & ""
        StrSQL = StrSQL & "SELECT "
        StrSQL = StrSQL & "  CUST_CD AS 客先コード "
        StrSQL = StrSQL & ", INVOICE_NO AS IVNo "
        StrSQL = StrSQL & ", CASE ETD WHEN '1' THEN 'ＮＧ' ELSE '-' END AS 計上日 "
        StrSQL = StrSQL & ", CASE LOADING_PORT WHEN '1' THEN 'ＮＧ' ELSE '-' END AS 積出港 "
        StrSQL = StrSQL & ", CASE DISCHARGING_PORT WHEN '1' THEN 'ＮＧ' ELSE '-' END AS 揚地 "
        StrSQL = StrSQL & ", CASE PLACE_OF_DELIVERY WHEN '1' THEN 'ＮＧ' ELSE '-' END AS 配送先 "
        StrSQL = StrSQL & ", CASE PLACE_OF_RECEIPT WHEN '1' THEN 'ＮＧ' ELSE '-' END AS 荷受地 "
        StrSQL = StrSQL & ", CASE PLACE_CARRIER WHEN '1' THEN 'ＮＧ' ELSE '-' END AS 配送先責任送り先 "
        StrSQL = StrSQL & ", CASE CUT_DATE WHEN '1' THEN 'ＮＧ' ELSE '-' END AS カット日 "
        StrSQL = StrSQL & ", CASE ETA WHEN '1' THEN 'ＮＧ' ELSE '-' END AS 到着日 "
        StrSQL = StrSQL & ", CASE IOPORTDATE WHEN '1' THEN 'ＮＧ' ELSE '-' END AS 入出港日 "
        StrSQL = StrSQL & ", CASE SHIP_METHOD WHEN '1' THEN 'ＮＧ' ELSE '-' END AS 出荷方法 "
        StrSQL = StrSQL & ", CASE VOYAGE_NO WHEN '1' THEN 'ＮＧ' ELSE '-' END AS VOYAGENo "
        StrSQL = StrSQL & ", CASE BOOK_TO WHEN '1' THEN 'ＮＧ' ELSE '-' END AS 船社 "
        StrSQL = StrSQL & ", CASE BOOKING_NO WHEN '1' THEN 'ＮＧ' ELSE '-' END AS ﾌﾞｯｷﾝｸﾞNo "
        StrSQL = StrSQL & ", CASE VESSEL_NAME WHEN '1' THEN 'ＮＧ' ELSE '-' END AS 船名 "
        StrSQL = StrSQL & ", CASE FIN_FLG WHEN '1' THEN '確認完' ELSE '未' END AS FIN_FLG  "
        StrSQL = StrSQL & "FROM T_COMPARE_INV_HD "
        StrSQL = StrSQL & "ORDER BY INVOICE_NO  "

        Cmd.CommandText = StrSQL

        Da = Factroy.CreateDataAdapter()
        Da.SelectCommand = Cmd
        Ds = New DataSet
        Da.Fill(Ds)
        Return Ds
    End Function

    Public Function GET_RESULT_IV_HEADER_02() As DataSet
        '確認済み非表示時のデータ取得(BookingvsIVﾍｯﾀﾞ比較用)
        Conn = Me.Dbconnect
        Cmd = Conn.CreateCommand

        StrSQL = StrSQL & ""
        StrSQL = StrSQL & "SELECT "
        StrSQL = StrSQL & "  CUST_CD AS 客先コード "
        StrSQL = StrSQL & ", INVOICE_NO AS IVNo "
        StrSQL = StrSQL & ", CASE ETD WHEN '1' THEN 'ＮＧ' ELSE '-' END AS 計上日 "
        StrSQL = StrSQL & ", CASE LOADING_PORT WHEN '1' THEN 'ＮＧ' ELSE '-' END AS 積出港 "
        StrSQL = StrSQL & ", CASE DISCHARGING_PORT WHEN '1' THEN 'ＮＧ' ELSE '-' END AS 揚地 "
        StrSQL = StrSQL & ", CASE PLACE_OF_DELIVERY WHEN '1' THEN 'ＮＧ' ELSE '-' END AS 配送先 "
        StrSQL = StrSQL & ", CASE PLACE_OF_RECEIPT WHEN '1' THEN 'ＮＧ' ELSE '-' END AS 荷受地 "
        StrSQL = StrSQL & ", CASE PLACE_CARRIER WHEN '1' THEN 'ＮＧ' ELSE '-' END AS 配送先責任送り先 "
        StrSQL = StrSQL & ", CASE CUT_DATE WHEN '1' THEN 'ＮＧ' ELSE '-' END AS カット日 "
        StrSQL = StrSQL & ", CASE ETA WHEN '1' THEN 'ＮＧ' ELSE '-' END AS 到着日 "
        StrSQL = StrSQL & ", CASE IOPORTDATE WHEN '1' THEN 'ＮＧ' ELSE '-' END AS 入出港日 "
        StrSQL = StrSQL & ", CASE SHIP_METHOD WHEN '1' THEN 'ＮＧ' ELSE '-' END AS 出荷方法 "
        StrSQL = StrSQL & ", CASE VOYAGE_NO WHEN '1' THEN 'ＮＧ' ELSE '-' END AS VOYAGENo "
        StrSQL = StrSQL & ", CASE BOOK_TO WHEN '1' THEN 'ＮＧ' ELSE '-' END AS 船社 "
        StrSQL = StrSQL & ", CASE BOOKING_NO WHEN '1' THEN 'ＮＧ' ELSE '-' END AS ﾌﾞｯｷﾝｸﾞNo "
        StrSQL = StrSQL & ", CASE VESSEL_NAME WHEN '1' THEN 'ＮＧ' ELSE '-' END AS 船名 "
        StrSQL = StrSQL & ", CASE FIN_FLG WHEN '1' THEN '確認完' ELSE '未' END AS FIN_FLG  "
        StrSQL = StrSQL & "FROM T_COMPARE_INV_HD "
        StrSQL = StrSQL & "WHERE FIN_FLG = '0' "
        StrSQL = StrSQL & "ORDER BY INVOICE_NO  "

        Cmd.CommandText = StrSQL

        Da = Factroy.CreateDataAdapter()
        Da.SelectCommand = Cmd
        Ds = New DataSet
        Da.Fill(Ds)
        Return Ds
    End Function

    Public Function GET_RESULT_VAN_SCH(strDate As String, strPlace As String) As DataSet
        'バンニングスケジュール絞り込み
        Conn = Me.Dbconnect
        Cmd = Conn.CreateCommand

        StrSQL = StrSQL & ""
        StrSQL = StrSQL & "SELECT "
        StrSQL = StrSQL & "  CASE PLACE  "
        StrSQL = StrSQL & "    WHEN '0H' THEN '本社'  "
        StrSQL = StrSQL & "    WHEN '1U' THEN '上野'  "
        StrSQL = StrSQL & "    WHEN '2A' THEN 'AIR'  "
        StrSQL = StrSQL & "    END AS 場所 "
        StrSQL = StrSQL & "  , CUST_NM AS 客先名 "
        StrSQL = StrSQL & "  , VAN_DATE AS VAN日 "
        StrSQL = StrSQL & "  , VAN_TIME AS ｽﾀｰﾄ "
        StrSQL = StrSQL & "  , CON_SIZE AS コンテナサイズ "
        StrSQL = StrSQL & "  , IVNO AS インボイスNO "
        StrSQL = StrSQL & "  , CUT_DATE AS カット日 "
        StrSQL = StrSQL & "  , ETD AS ＥＴＤ  "
        StrSQL = StrSQL & "  , '' AS 最終  "
        StrSQL = StrSQL & "FROM "
        StrSQL = StrSQL & "  T_EXL_VAN_SCH_DETAIL  "
        If strDate <> "" And strPlace <> "" Then
            StrSQL = StrSQL & "WHERE "
            StrSQL = StrSQL & " PLACE = '" & strPlace & "' "
            StrSQL = StrSQL & " AND VAN_DATE = '" & strDate & "' "
        ElseIf strDate <> "" And strPlace = "" Then
            StrSQL = StrSQL & "WHERE "
            StrSQL = StrSQL & " VAN_DATE = '" & strDate & "' "
        ElseIf strDate = "" And strPlace <> "" Then
            StrSQL = StrSQL & "WHERE "
            StrSQL = StrSQL & " PLACE = '" & strPlace & "' "
        End If
        StrSQL = StrSQL & "ORDER BY "
        StrSQL = StrSQL & "  PLACE "

        Cmd.CommandText = StrSQL

        Da = Factroy.CreateDataAdapter()
        Da.SelectCommand = Cmd
        Ds = New DataSet
        Da.Fill(Ds)
        Return Ds
    End Function

    Public Function GET_CS_RESULT_SHIPPINGMEMO(strstart As String, strend As String, strd1 As String, strd2 As String) As DataSet
        'シッピングメモデータ取得時
        Conn = Me.Dbconnect
        Cmd = Conn.CreateCommand

        StrSQL = StrSQL & ""
        StrSQL = StrSQL & "SELECT "
        StrSQL = StrSQL & "  CUSTCODE "
        StrSQL = StrSQL & ", CUSTNAME "
        StrSQL = StrSQL & ", INVOICE_NO "
        StrSQL = StrSQL & ", ETD "
        StrSQL = StrSQL & ", MEMOFLG "
        StrSQL = StrSQL & ", SIFLG "
        StrSQL = StrSQL & ", DATE_GETBL "
        StrSQL = StrSQL & ", SHIP_TYPE "
        StrSQL = StrSQL & ", DATE_ONBL "
        StrSQL = StrSQL & ", ETA "
        StrSQL = StrSQL & ", REV_SALESDATE "
        StrSQL = StrSQL & ", REV_STATUS "
        StrSQL = StrSQL & ", BOOKING_NO "
        StrSQL = StrSQL & ", VOY_NO "
        StrSQL = StrSQL & ", IV_BLDATE "
        StrSQL = StrSQL & ", KIN_GAIKA "
        StrSQL = StrSQL & ", RATE "
        StrSQL = StrSQL & ", KIN_JPY "
        StrSQL = StrSQL & ", VESSEL "
        StrSQL = StrSQL & ", LOADING_PORT "
        StrSQL = StrSQL & ", RECEIVED_PORT "
        StrSQL = StrSQL & ", SHIP_PLACE "
        StrSQL = StrSQL & ", CHECKFLG "
        StrSQL = StrSQL & ", REV_ETD "
        StrSQL = StrSQL & ", REV_ETA "
        StrSQL = StrSQL & ", FLG01 "
        StrSQL = StrSQL & ", FLG02 "
        StrSQL = StrSQL & ", FLG03 "
        StrSQL = StrSQL & ", FLG04 "
        StrSQL = StrSQL & ", FLG05 "

        StrSQL = StrSQL & "FROM "
        StrSQL = StrSQL & "  [T_EXL_SHIPPINGMEMOLIST] "

        StrSQL = StrSQL & "WHERE ETD BETWEEN '" & strstart & "' AND '" & strend & "' "
        StrSQL = StrSQL & "AND CUSTCODE Not In ('B494','B490','B491','B492','B520','A063','A064','A060','A061','A062','B530') "

        If strd1 = "未回収" Then
            StrSQL = StrSQL & "AND DATE_GETBL ='' "
        End If

        If strd2 <> "" And strd2 <> "--Select--" Then
            StrSQL = StrSQL & "AND REV_STATUS = '" & strd2 & "' "
        End If

        StrSQL = StrSQL & "ORDER BY ETD "

        Cmd.CommandText = StrSQL

        Da = Factroy.CreateDataAdapter()
        Da.SelectCommand = Cmd
        Ds = New DataSet
        Da.Fill(Ds)
        Return Ds
    End Function

    Public Function GET_RESULT_SALES(strDate1 As String, strDate2 As String, strCode As String,
                                     strShukka As String, strChk As String) As DataSet
        '海外売上確定チェック絞り込み
        Conn = Me.Dbconnect2
        Cmd = Conn.CreateCommand

        StrSQL = StrSQL & ""
        StrSQL = StrSQL & "SELECT "
        StrSQL = StrSQL & "  FORMAT(T_INV_HD_TB.BLDATE,'yyyy/MM/dd') AS BLDATE "
        StrSQL = StrSQL & "  , T_INV_HD_TB.OLD_INVNO "
        StrSQL = StrSQL & "  , T_INV_HD_TB.CUSTCODE "
        StrSQL = StrSQL & "  , T_INV_HD_TB.CUSTNAME "
        StrSQL = StrSQL & "  , CASE T_INV_HD_TB.SHIPCD  "
        StrSQL = StrSQL & "      WHEN '01' THEN '20ft' "
        StrSQL = StrSQL & "      WHEN '02' THEN '40ft' "
        StrSQL = StrSQL & "      WHEN '03' THEN 'CFS' "
        StrSQL = StrSQL & "      WHEN '05' THEN 'AIR' "
        StrSQL = StrSQL & "      WHEN '06' THEN 'COURIER' "
        StrSQL = StrSQL & "    END AS SHIPCD "
        StrSQL = StrSQL & "  , T_INV_HD_TB.REGPERSON "
        StrSQL = StrSQL & "  , '' AS REGNAME "
        StrSQL = StrSQL & "  , FORMAT(T_INV_HD_TB.CUTDATE,'yyyy/MM/dd') AS CUTDATE "
        StrSQL = StrSQL & "  , T_INV_HD_TB.ALLOUTSTAMP "
        StrSQL = StrSQL & "FROM "
        StrSQL = StrSQL & "    T_INV_HD_TB  "
        StrSQL = StrSQL & "    INNER JOIN T_INV_BD_TB  "
        StrSQL = StrSQL & "    ON T_INV_HD_TB.INVOICENO = T_INV_BD_TB.INVOICENO "
        StrSQL = StrSQL & "GROUP BY "
        StrSQL = StrSQL & "  T_INV_HD_TB.BLDATE "
        StrSQL = StrSQL & "  , T_INV_HD_TB.OLD_INVNO "
        StrSQL = StrSQL & "  , T_INV_HD_TB.CUSTCODE "
        StrSQL = StrSQL & "  , T_INV_HD_TB.CUSTNAME "
        StrSQL = StrSQL & "  , T_INV_HD_TB.SHIPCD "
        StrSQL = StrSQL & "  , T_INV_HD_TB.REGPERSON "
        StrSQL = StrSQL & "  , T_INV_HD_TB.CUTDATE "
        StrSQL = StrSQL & "  , T_INV_HD_TB.ALLOUTSTAMP "
        StrSQL = StrSQL & "  , T_INV_HD_TB.SALESFLG  "
        StrSQL = StrSQL & "HAVING "
        StrSQL = StrSQL & "    T_INV_HD_TB.CUSTCODE <> '111' And T_INV_HD_TB.CUSTCODE <> 'A121' "
        StrSQL = StrSQL & "    AND T_INV_HD_TB.REGPERSON IN (" & strCode & ") "
        StrSQL = StrSQL & "    AND T_INV_HD_TB.SALESFLG Is Null "
        If strDate1 <> "" And strDate2 <> "" Then
            StrSQL = StrSQL & "    AND T_INV_HD_TB.BLDATE >= '" & strDate1 & "' And T_INV_HD_TB.BLDATE <= '" & strDate2 & "' "
        End If
        If strShukka <> "" Then
            StrSQL = StrSQL & "    AND T_INV_HD_TB.SHIPCD = '" & strShukka & "' "
        End If
        If strChk = "True" Then
            StrSQL = StrSQL & "    AND T_INV_HD_TB.ALLOUTSTAMP IS NOT NULL "
        End If
        StrSQL = StrSQL & "ORDER BY  T_INV_HD_TB.BLDATE "

        Cmd.CommandText = StrSQL

        Da = Factroy.CreateDataAdapter()
        Da.SelectCommand = Cmd
        Ds = New DataSet
        Da.Fill(Ds)
        Return Ds
    End Function

    Public Function GET_BOOKING_DATA(strFwd As String, strCust As String, strIV As String) As DataSet
        'ブッキングデータ絞り込み
        Conn = Me.Dbconnect
        Cmd = Conn.CreateCommand

        StrSQL = StrSQL & ""
        StrSQL = StrSQL & "SELECT * "
        StrSQL = StrSQL & "FROM T_BOOKING "

        If strFwd <> "" And strCust = "" And strIV = "" Then
            StrSQL = StrSQL & "WHERE Forwarder = '" & strFwd & "' "
        ElseIf strFwd <> "" And strCust <> "" And strIV = "" Then
            StrSQL = StrSQL & "WHERE Forwarder = '" & strFwd & "' "
            StrSQL = StrSQL & "    AND CUST_CD like '%" & strCust & "%' "
        ElseIf strFwd <> "" And strCust = "" And strIV <> "" Then
            StrSQL = StrSQL & "WHERE Forwarder = '" & strFwd & "' "
            StrSQL = StrSQL & "    AND INVOICE_NO like '%" & strIV & "%' "
        ElseIf strFwd <> "" And strCust <> "" And strIV <> "" Then
            StrSQL = StrSQL & "WHERE Forwarder = '" & strFwd & "' "
            StrSQL = StrSQL & "    AND CUST_CD like '%" & strCust & "%' "
            StrSQL = StrSQL & "    AND INVOICE_NO like '%" & strIV & "%' "
        ElseIf strFwd = "" And strCust <> "" And strIV = "" Then
            StrSQL = StrSQL & "WHERE CUST_CD like '%" & strCust & "%' "
        ElseIf strFwd = "" And strCust <> "" And strIV <> "" Then
            StrSQL = StrSQL & "WHERE CUST_CD like '%" & strCust & "%' "
            StrSQL = StrSQL & "    AND INVOICE_NO like '%" & strIV & "%' "
        ElseIf strFwd = "" And strCust = "" And strIV <> "" Then
            StrSQL = StrSQL & "WHERE INVOICE_NO like '%" & strIV & "%' "
        End If

        Cmd.CommandText = StrSQL

        Da = Factroy.CreateDataAdapter()
        Da.SelectCommand = Cmd
        Ds = New DataSet
        Da.Fill(Ds)
        Return Ds
    End Function

    Public Function GET_AIR_DATA(strValue() As String, strChk As String, strMode As String) As DataSet
        'AIR管理表絞り込み
        'strMode 0:GridView 1:ETD 2:客先 3:依頼者 4:IVNO

        Dim i As Integer = 0
        Dim intCnt As Integer = 0
        Dim intCnt2 As Integer = 0

        For i = 0 To 3
            If strValue(i) <> "" Then
                intCnt += 1
            End If
        Next

        Conn = Me.Dbconnect
        Cmd = Conn.CreateCommand

        StrSQL = ""
        Select Case strMode
            Case 0
                StrSQL = StrSQL & "SELECT  "
                StrSQL = StrSQL & " REQUESTED_DATE  , CREATED_DATE, ETD, CUST_CD "
                StrSQL = StrSQL & ", IVNO  , REQUESTER  , DEPARTMENT  , AUTHOR "
                StrSQL = StrSQL & ", CASE DOC_FIN WHEN '作成済み' THEN '済' END AS DOC_FIN "
                StrSQL = StrSQL & ", SHIPPING_COMPANY "
                StrSQL = StrSQL & ", CASE PICKUP WHEN '集荷済み' THEN '済' END AS PICKUP "
                StrSQL = StrSQL & ", PLACE  , REMARKS  "
            Case 1
                StrSQL = StrSQL & "Select DISTINCT ETD "
            Case 2
                StrSQL = StrSQL & "Select DISTINCT CUST_CD "
            Case 3
                StrSQL = StrSQL & "Select DISTINCT REQUESTER "
            Case 4
                StrSQL = StrSQL & "Select DISTINCT IVNO "
        End Select
        StrSQL = StrSQL & "FROM T_EXL_AIR_MANAGE "
        If intCnt = 0 And strChk = "0" Then    '全件で無い場合(デフォルト)         
            StrSQL = StrSQL & "WHERE PICKUP = '' "
        Else
            If intCnt > 0 And strChk = "1" Then  '1件でも条件がある場合
                StrSQL = StrSQL & "WHERE "
            ElseIf intCnt > 0 And strChk = "0" Then  '1件でも条件がある 且つ　全件で無い場合
                StrSQL = StrSQL & "WHERE PICKUP = '' AND "
            End If

            For i = 0 To 3
                If (intCnt2 > 0 And intCnt > intCnt2 And strValue(i) <> "") Then
                    StrSQL = StrSQL & " AND "
                End If
                If strValue(i) <> "" Then
                    Select Case i
                        Case 0
                            StrSQL = StrSQL & " ETD = '" & strValue(i) & "' "
                        Case 1
                            StrSQL = StrSQL & " CUST_CD = '" & strValue(i) & "' "
                        Case 2
                            StrSQL = StrSQL & " REQUESTER = '" & strValue(i) & "' "
                        Case 3
                            StrSQL = StrSQL & " IVNO = '" & strValue(i) & "' "
                    End Select
                    intCnt2 += 1
                End If
            Next
        End If

        Select Case strMode
            Case 0, 1
                StrSQL = StrSQL & "ORDER BY ETD "
            Case 2
                StrSQL = StrSQL & "ORDER BY CUST_CD "
            Case 3
                StrSQL = StrSQL & "ORDER BY REQUESTER "
            Case 4
                StrSQL = StrSQL & "ORDER BY IVNO "
        End Select


        Cmd.CommandText = StrSQL

        Da = Factroy.CreateDataAdapter()
        Da.SelectCommand = Cmd
        Ds = New DataSet
        Da.Fill(Ds)
        Return Ds
    End Function

    Public Function GET_RESULT_AIR_EXC(strMode As String) As DataSet
        'AIR専用オーダー取得
        Conn = Me.Dbconnect
        Cmd = Conn.CreateCommand

        StrSQL = StrSQL & ""
        StrSQL = StrSQL & "SELECT DISTINCT "
        StrSQL = StrSQL & "  CUST_CD  "
        StrSQL = StrSQL & "  , NOUKI "
        StrSQL = StrSQL & "  , LS_TYP "
        StrSQL = StrSQL & "  , CUST_ODR_NO "
        StrSQL = StrSQL & "  , SALESNOTENO "
        StrSQL = StrSQL & "  , a.ODR_CTL_NO "
        StrSQL = StrSQL & "  , b.IVNO "
        StrSQL = StrSQL & "FROM "
        StrSQL = StrSQL & "  T_EXL_AIR_EXC_ODR a "
        StrSQL = StrSQL & "  LEFT JOIN T_EXL_AIR_EXCLUSIVE b "
        StrSQL = StrSQL & "    ON a.ODR_CTL_NO = b.ODR_CTL_NO "
        If strMode = "1" Then
            StrSQL = StrSQL & "WHERE b.IVNO IS NULL "
        End If
        StrSQL = StrSQL & "ORDER BY NOUKI "

        Cmd.CommandText = StrSQL

        Da = Factroy.CreateDataAdapter()
        Da.SelectCommand = Cmd
        Ds = New DataSet
        Da.Fill(Ds)
        Return Ds
    End Function

    Public Function GET_RESULT_CS(strCode As String, strPlace As String, strName As String) As DataSet
        'CSメンバーマスタ取得
        Conn = Me.Dbconnect
        Cmd = Conn.CreateCommand

        StrSQL = StrSQL & ""
        StrSQL = StrSQL & "SELECT "
        StrSQL = StrSQL & "  CODE "
        StrSQL = StrSQL & "  , MEMBER_NAME "
        StrSQL = StrSQL & "  , NAME_AB "
        StrSQL = StrSQL & "  , CASE PLACE "
        StrSQL = StrSQL & "    WHEN 'H' THEN '本社'  "
        StrSQL = StrSQL & "    WHEN 'U' THEN '上野'  "
        StrSQL = StrSQL & "    WHEN 'HU' THEN '本社'  "
        StrSQL = StrSQL & "    END AS PLACE "
        StrSQL = StrSQL & "  , COMPANY "
        StrSQL = StrSQL & "  , TEAM "
        StrSQL = StrSQL & "  , TEL_NO "
        StrSQL = StrSQL & "  , FAX_NO "
        StrSQL = StrSQL & "  , E_MAIL  "
        StrSQL = StrSQL & "FROM "
        StrSQL = StrSQL & "  (SELECT *  "
        StrSQL = StrSQL & "    FROM  M_EXL_CS_MEMBER  "
        StrSQL = StrSQL & "    WHERE "
        StrSQL = StrSQL & "      CODE LIKE 'T%'  "
        StrSQL = StrSQL & "    UNION  "
        StrSQL = StrSQL & "   SELECT *  "
        StrSQL = StrSQL & "    FROM  M_EXL_CS_MEMBER  "
        StrSQL = StrSQL & "    WHERE "
        StrSQL = StrSQL & "      CODE LIKE 'E%'  ) A  "
        StrSQL = StrSQL & "WHERE "
        StrSQL = StrSQL & "  CODE LIKE '%" & strCode & "%'  "
        If strPlace <> "" Then
            StrSQL = StrSQL & "  AND PLACE LIKE '%" & strPlace & "%'  "
        End If
        If strName <> "" Then
            StrSQL = StrSQL & "  AND MEMBER_NAME LIKE '%" & strName & "%'  "
        End If

        Cmd.CommandText = StrSQL

        Da = Factroy.CreateDataAdapter()
        Da.SelectCommand = Cmd
        Ds = New DataSet
        Da.Fill(Ds)
        Return Ds
    End Function

    Public Function GET_RESULT_FWD(strFwd As String, strMail As String, strPlace As String) As DataSet
        'AIR用海貨業者アドレスマスタ取得
        Conn = Me.Dbconnect
        Cmd = Conn.CreateCommand

        StrSQL = StrSQL & ""
        StrSQL = StrSQL & "SELECT "
        StrSQL = StrSQL & "  CODE "
        StrSQL = StrSQL & "  , MAIL_ADD "
        StrSQL = StrSQL & "  , Case PLACE "
        StrSQL = StrSQL & "     WHEN '0' THEN '本社' "
        StrSQL = StrSQL & "     WHEN '1' THEN '上野' "
        StrSQL = StrSQL & "    End As PLACE "
        StrSQL = StrSQL & "  , PLACE "
        StrSQL = StrSQL & "  , GYOSHA  "
        StrSQL = StrSQL & "FROM M_EXL_AIR_MAIL "

        If strFwd <> "" Or strMail <> "" Or strPlace <> "" Then
            StrSQL = StrSQL & "WHERE "
            If strFwd <> "" And strMail = "" And strPlace = "" Then
                StrSQL = StrSQL & "  GYOSHA LIKE '%" & strFwd & "%'  "
            ElseIf strFwd <> "" And strMail <> "" And strPlace = "" Then
                StrSQL = StrSQL & "  GYOSHA LIKE '%" & strFwd & "%'  "
                StrSQL = StrSQL & "  AND MAIL_ADD LIKE '%" & strMail & "%'  "
            ElseIf strFwd <> "" And strMail <> "" And strPlace <> "" Then
                StrSQL = StrSQL & "  GYOSHA LIKE '%" & strFwd & "%'  "
                StrSQL = StrSQL & "  AND MAIL_ADD LIKE '%" & strMail & "%'  "
                StrSQL = StrSQL & "  AND PLACE LIKE '%" & strPlace & "%'  "
            ElseIf strFwd <> "" And strMail = "" And strPlace <> "" Then
                StrSQL = StrSQL & "  GYOSHA LIKE '%" & strFwd & "%'  "
                StrSQL = StrSQL & "  AND PLACE LIKE '%" & strPlace & "%'  "
            ElseIf strFwd = "" And strMail <> "" And strPlace = "" Then
                StrSQL = StrSQL & "  MAIL_ADD LIKE '%" & strMail & "%'  "
            ElseIf strFwd = "" And strMail <> "" And strPlace <> "" Then
                StrSQL = StrSQL & "  MAIL_ADD LIKE '%" & strMail & "%'  "
                StrSQL = StrSQL & "  AND PLACE LIKE '%" & strPlace & "%'  "
            ElseIf strFwd = "" And strMail = "" And strPlace <> "" Then
                StrSQL = StrSQL & "  PLACE LIKE '%" & strPlace & "%'  "
            End If
        End If

        Cmd.CommandText = StrSQL

        Da = Factroy.CreateDataAdapter()
        Da.SelectCommand = Cmd
        Ds = New DataSet
        Da.Fill(Ds)
        Return Ds
    End Function

    Public Function GET_RESULT_EPA(strValue As String) As DataSet
        'CSメンバーマスタ取得
        Conn = Me.Dbconnect
        Cmd = Conn.CreateCommand

        StrSQL = StrSQL & ""
        StrSQL = StrSQL & "SELECT "
        StrSQL = StrSQL & "  CASE STATUS "
        StrSQL = StrSQL & "  	WHEN '01' THEN '未' "
        StrSQL = StrSQL & "  	WHEN '02' THEN '済' "
        StrSQL = StrSQL & "  	WHEN '03' THEN '対象ﾅｼ' "
        StrSQL = StrSQL & "  	WHEN '04' THEN 'ｷｬﾝｾﾙ' "
        StrSQL = StrSQL & "  	WHEN '09' THEN '再発給' "
        StrSQL = StrSQL & "  END AS STATUS "
        StrSQL = StrSQL & "  , CASE BLDATE WHEN '' THEN '' ELSE FORMAT(CONVERT(DATETIME,BLDATE ),'MM/dd') END  AS BLDATE "
        StrSQL = StrSQL & "  , INV  "
        StrSQL = StrSQL & "  , CUSTNAME  "
        StrSQL = StrSQL & "  , CUSTCODE  "
        StrSQL = StrSQL & "  , CASE SALESFLG "
        StrSQL = StrSQL & "  		WHEN '1' THEN '済' "
        StrSQL = StrSQL & "        ELSE '' "
        StrSQL = StrSQL & "	END AS SALESFLG "
        StrSQL = StrSQL & "  , SHIPPEDPER  "
        StrSQL = StrSQL & "  , CASE ETA WHEN '' THEN '' ELSE FORMAT(CONVERT(DATETIME,ETA ),'MM/dd') END  AS ETA "
        StrSQL = StrSQL & "  , CASE CUTDATE WHEN '' THEN '' ELSE FORMAT(CONVERT(DATETIME,CUTDATE ),'MM/dd') END  AS CUTDATE "
        StrSQL = StrSQL & "  , VOYAGENO  "
        StrSQL = StrSQL & "  , RECEIPT_NUMBER  "
        StrSQL = StrSQL & "  , IVNO_FULL  "
        StrSQL = StrSQL & "  , BLDATE AS BLDATE_FULL "
        StrSQL = StrSQL & "  , CASE APPLICATION_DATE WHEN '' THEN '' ELSE FORMAT(CONVERT(DATETIME,APPLICATION_DATE ),'MM/dd') END  AS APPLICATION_DATE "
        StrSQL = StrSQL & "  , CASE SENDING_REQ_DATE WHEN '' THEN '' ELSE FORMAT(CONVERT(DATETIME,SENDING_REQ_DATE ),'MM/dd') END  AS SENDING_REQ_DATE "
        StrSQL = StrSQL & "  , CASE RECEIPT_DATE WHEN '' THEN '' ELSE FORMAT(CONVERT(DATETIME,RECEIPT_DATE ),'MM/dd') END  AS RECEIPT_DATE "
        StrSQL = StrSQL & "  , CASE EPA_SEND_DATE WHEN '' THEN '' ELSE FORMAT(CONVERT(DATETIME,EPA_SEND_DATE ),'MM/dd') END  AS EPA_SEND_DATE "
        StrSQL = StrSQL & "  , TRK_NO  "
        StrSQL = StrSQL & "FROM T_EXL_EPA_KANRI "
        If strValue = "False" Then
            StrSQL = StrSQL & "WHERE STATUS = '01' "
        End If
        StrSQL = StrSQL & "ORDER BY BLDATE, INV"

        Cmd.CommandText = StrSQL

        Da = Factroy.CreateDataAdapter()
        Da.SelectCommand = Cmd
        Ds = New DataSet
        Da.Fill(Ds)
        Return Ds
    End Function
End Class
