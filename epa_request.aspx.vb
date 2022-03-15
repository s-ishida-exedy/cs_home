Imports System.Data.SqlClient
Imports System.Data

Partial Class cs_home
    Inherits System.Web.UI.Page

    Private Sub GridView1_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowCreated

        '列非表示処理
        If e.Row.RowType = DataControlRowType.DataRow OrElse e.Row.RowType = DataControlRowType.Header Then
            'キー項目のETDをテーブルのデータのままセットし、その項目は非表示にする。
            e.Row.Cells(18).Visible = False
        End If

        'ボタンに行数をセット
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim dltButton As Button = e.Row.FindControl("Button1")
            'ボタンが存在する場合のみセット
            If Not (dltButton Is Nothing) Then
                dltButton.CommandArgument = e.Row.RowIndex.ToString()
            End If
        End If
    End Sub

    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load

        AddHandler GridView1.RowCommand, AddressOf GridView1_RowCommand
    End Sub

    Private Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        If e.CommandName = "edt" Then
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim data1 = Me.GridView1.Rows(index).Cells(18).Text     '非表示としたキー項目
            Dim data2 = Me.GridView1.Rows(index).Cells(3).Text
            Dim data3 = Me.GridView1.Rows(index).Cells(5).Text
            Dim data4 = Me.GridView1.Rows(index).Cells(1).Text

            Session("strEtd") = data1
            Session("strIvno") = data2
            Session("strCust") = data3
            Session("strStatus") = data4
            Response.Redirect("epa_request_detail.aspx")
        End If
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        '追加登録ボタン押下
        Response.Redirect("epa_request_detail_ins.aspx")
    End Sub

    Private Sub CheckBox1_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox1.CheckedChanged
        'チェックボックスの制御
        Dim strValue As String = ""

        If CheckBox1.Checked = True Then
            '「未」以外も表示する。
            strValue = "True"
        Else
            strValue = "False"
        End If

        Dim Dataobj As New DBAccess

        'データの取得
        Dim ds As DataSet = Dataobj.GET_RESULT_EPA(strValue)
        If ds.Tables.Count > 0 Then
            GridView1.DataSourceID = ""
            GridView1.DataSource = ds
            GridView1.DataBind()
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        '最新情報取込ボタン押下

        'インボイスヘッダ情報取得
        If Get_IV_Data() = "False" Then
            Label1.Text = "データ更新に失敗しました。"
            Return
        End If

        '最新のEPA対象データを登録
        If Ins_EPA_Data() = "False" Then
            Label1.Text = "データ更新に失敗しました。"
            Return
        End If

        'Grid再表示
        GridView1.DataBind()

        Label1.Text = "データ更新完了しました。"

    End Sub

    Private Function Get_IV_Data() As String
        'インボイスヘッダ情報取得 イントラから取得したデータをT_EXL_IV_HD_EPAにINSERT
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strCST As String = ""
        Dim tran As System.Data.SqlClient.SqlTransaction = Nothing

        'イントラ用
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=KBHWPA85;Initial Catalog=BPTB001;User Id=ado_bptb001;Password=ado_bptb001"
        Dim cnn = New SqlConnection(ConnectionString)

        'KBHWPM02用
        Dim ConnectionString2 As String = String.Empty
        'SQL Server認証
        ConnectionString2 = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        Dim cnn2 = New SqlConnection(ConnectionString2)
        Dim Command = cnn2.CreateCommand

        Get_IV_Data = "True"

        Try
            'データベース接続を開く
            cnn.Open()

            '既存データ削除
            cnn2.Open()
            strSQL = ""
            strSQL = strSQL & "DELETE FROM T_EXL_IV_HD_EPA "
            Command.CommandText = strSQL
            ' SQLの実行
            Command.ExecuteNonQuery()
            cnn2.Close()

            Dim dt As DateTime = DateTime.Now
            dt = dt.AddDays(-7)
            Dim strBefore As String = dt.ToString("yyyy/MM/dd")
            dt = DateTime.Now
            dt = dt.AddDays(15)
            Dim strAfter As String = dt.ToString("yyyy/MM/dd")

            'イントラから前後２～３週間のインボイスデータを取得する
            strSQL = ""
            strSQL = strSQL & "SELECT "
            strSQL = strSQL & "  '01' AS STATUS "
            strSQL = strSQL & "  , T_INV_HD_TB.BLDATE "
            strSQL = strSQL & "  , T_INV_HD_TB.OLD_INVNO AS INV "
            strSQL = strSQL & "  , T_INV_HD_TB.CUSTNAME "
            strSQL = strSQL & "  , T_INV_HD_TB.CUSTCODE "
            strSQL = strSQL & "  , IsNull(T_INV_HD_TB.SALESFLG,'') AS SALESFLG  "
            strSQL = strSQL & "  , IsNull(T_INV_HD_TB.SHIPPEDPER,'') AS SHIPPEDPER "
            strSQL = strSQL & "  , IsNull(T_INV_HD_TB.BOOKINGNO,'') AS BOOKINGNO "
            strSQL = strSQL & "  , IsNull(T_INV_HD_TB.REACHDATE,'') AS  ETA "
            strSQL = strSQL & "  , IsNull(T_INV_HD_TB.CUTDATE,'') AS  CUTDATE "
            strSQL = strSQL & "  , IsNull(T_INV_HD_TB.VOYAGENO,'') AS VOYAGENO  "
            strSQL = strSQL & "FROM "
            strSQL = strSQL & "  T_INV_HD_TB  "
            strSQL = strSQL & "WHERE "
            strSQL = strSQL & "  T_INV_HD_TB.BLDATE BETWEEN '" & strBefore & "' AND '" & strAfter & "' "
            strSQL = strSQL & "  AND T_INV_HD_TB.SHIPPEDPER NOT IN ('NA', 'FedEx', 'DHL', 'by AirCraft')  "
            strSQL = strSQL & "  AND (T_INV_HD_TB.SHIPPEDPER IS NOT NULL  "
            strSQL = strSQL & "    AND T_INV_HD_TB.SHIPPEDPER <> '')  "
            strSQL = strSQL & "GROUP BY "
            strSQL = strSQL & "  T_INV_HD_TB.BLDATE "
            strSQL = strSQL & "  , T_INV_HD_TB.OLD_INVNO "
            strSQL = strSQL & "  , T_INV_HD_TB.CUSTNAME "
            strSQL = strSQL & "  , T_INV_HD_TB.CUSTCODE "
            strSQL = strSQL & "  , T_INV_HD_TB.SALESFLG "
            strSQL = strSQL & "  , T_INV_HD_TB.SHIPPEDPER "
            strSQL = strSQL & "  , T_INV_HD_TB.BOOKINGNO "
            strSQL = strSQL & "  , T_INV_HD_TB.REACHDATE "
            strSQL = strSQL & "  , T_INV_HD_TB.CUTDATE "
            strSQL = strSQL & "  , T_INV_HD_TB.VOYAGENO  "
            strSQL = strSQL & "ORDER BY "
            strSQL = strSQL & "  T_INV_HD_TB.BLDATE "
            strSQL = strSQL & "  , T_INV_HD_TB.CUSTCODE "
            strSQL = strSQL & "  , T_INV_HD_TB.OLD_INVNO "
            'ＳＱＬコマンド作成 
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行 
            dataread = dbcmd.ExecuteReader()

            Dim strSTATUS, strBLDATE, strINVP, strCUSTNAME, strCUSTCODE, strSALESFLG, strSHIPPEDPER,
                strBOOKINGNO, strETA, strCUTDATE, strVOYAGENO As String

            cnn2.Open()

            'トランザクションの開始
            tran = cnn2.BeginTransaction
            ' トランザクションをすることを明示する
            Command.Transaction = tran

            '結果を取り出す 
            While (dataread.Read())

                strSTATUS = Trim(dataread("STATUS"))
                strBLDATE = Trim(dataread("BLDATE"))
                strINVP = Trim(dataread("INV"))
                strCUSTNAME = Trim(dataread("CUSTNAME"))
                strCUSTCODE = Trim(dataread("CUSTCODE"))
                strSALESFLG = Trim(dataread("SALESFLG"))
                strSHIPPEDPER = Trim(dataread("SHIPPEDPER"))
                strBOOKINGNO = Trim(dataread("BOOKINGNO"))
                strETA = Trim(dataread("ETA"))
                strCUTDATE = Trim(dataread("CUTDATE"))
                strVOYAGENO = Trim(dataread("VOYAGENO"))

                'T_EXL_AIR_EXCLUSIVEにINSERT
                strSQL = ""
                strSQL = strSQL & "INSERT INTO T_EXL_IV_HD_EPA "
                strSQL = strSQL & "(STATUS , BLDATE , INV , CUSTNAME , CUSTCODE , SALESFLG , SHIPPEDPER , BOOKINGNO , ETA , CUTDATE , VOYAGENO ) "
                strSQL = strSQL & "VALUES(  "
                strSQL = strSQL & "'" & strSTATUS & "' "
                strSQL = strSQL & ",'" & strBLDATE & "' "
                strSQL = strSQL & ",'" & strINVP & "' "
                strSQL = strSQL & ",'" & strCUSTNAME & "' "
                strSQL = strSQL & ",'" & strCUSTCODE & "' "
                strSQL = strSQL & ",'" & strSALESFLG & "' "
                strSQL = strSQL & ",'" & strSHIPPEDPER & "' "
                strSQL = strSQL & ",'" & strBOOKINGNO & "' "
                strSQL = strSQL & ",'" & strETA & "' "
                strSQL = strSQL & ",'" & strCUTDATE & "' "
                strSQL = strSQL & ",'" & strVOYAGENO & "' "
                strSQL = strSQL & ") "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

            End While

            ' コミット
            tran.Commit()


        Catch ex As Exception
            Console.WriteLine("Error! {0}", ex.Message)
            Get_IV_Data = "False"

            ' ロールバック
            tran.Rollback()
        Finally
            ' コネクションが閉じられていないとき閉じる
            If Not cnn.State = ConnectionState.Closed Then
                cnn.Close()
            End If
            If Not cnn2.State = ConnectionState.Closed Then
                cnn2.Close()
            End If

            'クローズ処理 
            cnn.Close()
            cnn.Dispose()
            cnn2.Close()
            cnn2.Dispose()
        End Try
    End Function

    Private Function Ins_EPA_Data() As String
        '最新のEPA取得対象データをINSERT
        Dim strSQL As String = ""
        Dim tran As System.Data.SqlClient.SqlTransaction = Nothing

        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand

        Ins_EPA_Data = "True"

        Try
            'データベース接続を開く
            cnn.Open()

            'トランザクションの開始
            tran = cnn.BeginTransaction
            ' トランザクションをすることを明示する
            Command.Transaction = tran

            strSQL = strSQL & "INSERT "
            strSQL = strSQL & "INTO T_EXL_EPA_KANRI(  "
            strSQL = strSQL & "  STATUS "
            strSQL = strSQL & "  , BLDATE "
            strSQL = strSQL & "  , INV "
            strSQL = strSQL & "  , CUSTNAME "
            strSQL = strSQL & "  , CUSTCODE "
            strSQL = strSQL & "  , SALESFLG "
            strSQL = strSQL & "  , SHIPPEDPER "
            strSQL = strSQL & "  , BOOKINGNO "
            strSQL = strSQL & "  , ETA "
            strSQL = strSQL & "  , CUTDATE "
            strSQL = strSQL & "  , VOYAGENO "
            strSQL = strSQL & ")  "
            strSQL = strSQL & "SELECT "
            strSQL = strSQL & "  '01' AS STATUS "
            strSQL = strSQL & "  , T_EXL_IV_HD_EPA.BLDATE "
            strSQL = strSQL & "  , T_EXL_IV_HD_EPA.INV "
            strSQL = strSQL & "  , T_EXL_IV_HD_EPA.CUSTNAME "
            strSQL = strSQL & "  , T_EXL_IV_HD_EPA.CUSTCODE "
            strSQL = strSQL & "  , T_EXL_IV_HD_EPA.SALESFLG "
            strSQL = strSQL & "  , T_EXL_IV_HD_EPA.SHIPPEDPER "
            strSQL = strSQL & "  , T_EXL_IV_HD_EPA.BOOKINGNO "
            strSQL = strSQL & "  , T_EXL_IV_HD_EPA.ETA "
            strSQL = strSQL & "  , T_EXL_IV_HD_EPA.CUTDATE "
            strSQL = strSQL & "  , T_EXL_IV_HD_EPA.VOYAGENO  "
            strSQL = strSQL & "FROM "
            strSQL = strSQL & "  T_EXL_IV_HD_EPA  "
            strSQL = strSQL & "  INNER JOIN T_EXL_CSMANUAL  "
            strSQL = strSQL & "    ON T_EXL_IV_HD_EPA.CUSTCODE = T_EXL_CSMANUAL.NEW_CODE  "
            strSQL = strSQL & "WHERE "
            strSQL = strSQL & "  T_EXL_CSMANUAL.EPA_NECE = '○'  "
            strSQL = strSQL & "  AND NOT EXISTS (  "
            strSQL = strSQL & "    SELECT "
            strSQL = strSQL & "      *  "
            strSQL = strSQL & "    FROM "
            strSQL = strSQL & "      T_EXL_EPA_KANRI  "
            strSQL = strSQL & "    WHERE "
            strSQL = strSQL & "     T_EXL_IV_HD_EPA.BLDATE = T_EXL_EPA_KANRI.BLDATE "
            strSQL = strSQL & "      AND T_EXL_IV_HD_EPA.INV = T_EXL_EPA_KANRI.INV "
            strSQL = strSQL & "  )  "
            strSQL = strSQL & "GROUP BY "
            strSQL = strSQL & "  T_EXL_IV_HD_EPA.BLDATE "
            strSQL = strSQL & "  , T_EXL_IV_HD_EPA.INV "
            strSQL = strSQL & "  , T_EXL_IV_HD_EPA.CUSTNAME "
            strSQL = strSQL & "  , T_EXL_IV_HD_EPA.CUSTCODE "
            strSQL = strSQL & "  , T_EXL_IV_HD_EPA.SALESFLG "
            strSQL = strSQL & "  , T_EXL_IV_HD_EPA.SHIPPEDPER "
            strSQL = strSQL & "  , T_EXL_IV_HD_EPA.BOOKINGNO "
            strSQL = strSQL & "  , T_EXL_IV_HD_EPA.ETA "
            strSQL = strSQL & "  , T_EXL_IV_HD_EPA.CUTDATE "
            strSQL = strSQL & "  , T_EXL_IV_HD_EPA.VOYAGENO  "

            Command.CommandText = strSQL
            ' SQLの実行
            Command.ExecuteNonQuery()

            ' コミット
            tran.Commit()

        Catch ex As Exception
            Console.WriteLine("Error! {0}", ex.Message)
            Ins_EPA_Data = "False"

            ' ロールバック
            tran.Rollback()
        Finally
            ' コネクションが閉じられていないとき閉じる
            If Not cnn.State = ConnectionState.Closed Then
                cnn.Close()
            End If
            If Not cnn.State = ConnectionState.Closed Then
                cnn.Close()
            End If

            'クローズ処理 
            cnn.Close()
            cnn.Dispose()
        End Try
    End Function
End Class
