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



End Class
