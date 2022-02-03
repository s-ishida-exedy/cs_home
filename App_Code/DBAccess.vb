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
        'CSマニュアルデータ取得時
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

        StrSQL = StrSQL & "WHERE FORMAT(ETD,'yyyy/MM/dd') BETWEEN '" & strstart & "' AND '" & strend & "' "
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

    Public Function GET_RESULT_SALES(strDate1 As String, strDate2 As String, strCode As String) As DataSet
        'バンニングスケジュール絞り込み
        Conn = Me.Dbconnect2
        Cmd = Conn.CreateCommand

        StrSQL = StrSQL & ""
        StrSQL = StrSQL & "SELECT "
        StrSQL = StrSQL & "  FORMAT(T_INV_HD_TB.BLDATE,'yyyy/MM/dd') AS BLDATE "
        StrSQL = StrSQL & "  , T_INV_HD_TB.OLD_INVNO "
        StrSQL = StrSQL & "  , T_INV_HD_TB.CUSTNAME "
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
        StrSQL = StrSQL & "  , T_INV_HD_TB.REGPERSON "
        StrSQL = StrSQL & "  , T_INV_HD_TB.CUTDATE "
        StrSQL = StrSQL & "  , T_INV_HD_TB.ALLOUTSTAMP "
        StrSQL = StrSQL & "  , T_INV_HD_TB.SALESFLG  "
        StrSQL = StrSQL & "HAVING "
        StrSQL = StrSQL & "    T_INV_HD_TB.BLDATE >= '" & strDate1 & "' And T_INV_HD_TB.BLDATE <= '" & strDate2 & "' "
        StrSQL = StrSQL & "    AND T_INV_HD_TB.CUSTCODE <> '111' And T_INV_HD_TB.CUSTCODE <> 'A121' "
        StrSQL = StrSQL & "    AND T_INV_HD_TB.REGPERSON IN (" & strCode & ") "
        StrSQL = StrSQL & "    AND T_INV_HD_TB.SALESFLG Is Null "
        StrSQL = StrSQL & "ORDER BY  T_INV_HD_TB.BLDATE "

        Cmd.CommandText = StrSQL

        Da = Factroy.CreateDataAdapter()
        Da.SelectCommand = Cmd
        Ds = New DataSet
        Da.Fill(Ds)
        Return Ds
    End Function

End Class
