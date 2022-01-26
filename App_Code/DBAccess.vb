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
End Class
