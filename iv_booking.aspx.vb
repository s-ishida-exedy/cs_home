Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.Common
Partial Class cs_home
    Inherits System.Web.UI.Page

    Public strRow As String
    Public strProcess As String

    Private Sub GridView1_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridView1.RowCreated

        'コード列非表示処理
        If e.Row.RowType = DataControlRowType.DataRow OrElse e.Row.RowType = DataControlRowType.Header Then
            e.Row.Cells(1).Visible = False
        End If

    End Sub

    Protected Sub GridView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridView1.SelectedIndexChanged
        Dim sch_ID As String = GridView1.SelectedValue.ToString
        '選択された行のSCH_ID
        strRow = sch_ID
    End Sub

    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load

        If IsPostBack Then
            ' そうでない時処理
            Dim i As String = ""
        Else
            '表示データ取得
            Call Make_Grid()
        End If
    End Sub

    Private Sub Make_Grid()
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strIVNO() As String
        Dim tran As System.Data.SqlClient.SqlTransaction = Nothing

        '項目のリセット
        Call Reset_Item()

        'k3hwpm02用
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand
        cnn.Open()

        'ブッキングシートからIVNOを取得
        strSQL = ""
        strSQL = strSQL & "SELECT INVOICE_NO "
        strSQL = strSQL & "FROM T_BOOKING a "
        strSQL = strSQL & "WHERE a.INVOICE_NO <> '' "
        strSQL = strSQL & "AND a.ETD > CONVERT(VARCHAR,GETDATE(),111) "
        strSQL = strSQL & "AND a.INVOICE_NO <> 'ヒョウコウへ送付' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        'トランザクションの開始
        Dim cnn2 = New SqlConnection(ConnectionString)
        Dim Command2 = cnn2.CreateCommand

        '既存データ削除
        cnn2.Open()
        strSQL = ""
        strSQL = strSQL & "DELETE FROM T_EXL_BKGIV "
        Command2.CommandText = strSQL
        ' SQLの実行
        Command2.ExecuteNonQuery()
        cnn2.Close()

        cnn2.Open()

        tran = cnn2.BeginTransaction
        ' トランザクションをすることを明示する
        Command2.Transaction = tran
        While (dataread.Read())
            'SPLIT（カンマとハイフンあり）
            strIVNO = Trim(dataread("INVOICE_NO")).Split(New Char() {",", "-"})
            For Each c As String In strIVNO
                'IVNOごとにインボイスヘッダと比較SQL実行。実行結果をテーブルへ登録
                strSQL = ""
                strSQL = strSQL & "INSERT INTO T_EXL_BKGIV "
                strSQL = strSQL & "SELECT "
                strSQL = strSQL & "   IV.CUSTCODE AS CUST_CD,   "
                strSQL = strSQL & "   IV.OLD_INVNO AS INVOICE_NO,   "
                strSQL = strSQL & "   CASE WHEN BK.ETD = CONVERT(VARCHAR,IV.BLDATE2,111) THEN '-' ELSE 'ＮＧ' END AS BLDATE,  "
                strSQL = strSQL & "   CASE WHEN BK.LOADING_PORT = IV.INVFROM THEN '-' ELSE 'ＮＧ' END AS INVFROM,  "
                strSQL = strSQL & "   CASE WHEN BK.PLACE_OF_RECEIPT = IV.INVON THEN '-' ELSE 'ＮＧ' END AS INVON,  "
                strSQL = strSQL & "   CASE WHEN BK.CUT_DATE = CONVERT(VARCHAR,IV.CUTDATE,111) THEN '-' ELSE 'ＮＧ' END AS CUTDATE,  "
                strSQL = strSQL & "   CASE WHEN BK.ETD = CONVERT(VARCHAR,IV.IOPORTDATE,111) THEN '-' ELSE 'ＮＧ' END AS IOPORTDATE,  "
                strSQL = strSQL & "   CASE WHEN BK.ETA = CONVERT(VARCHAR,IV.REACHDATE,111) THEN '-' ELSE 'ＮＧ' END AS REACHDATE,  "
                strSQL = strSQL & "   CASE WHEN BK.VOYAGE_NO = IV.VOYAGENO THEN '-' ELSE 'ＮＧ' END AS VOYAGENO,  "
                strSQL = strSQL & "   CASE WHEN BK.BOOK_TO = IV.SHIPPER or (BK.BOOK_TO ='' AND IV.SHIPPER ='-') THEN '-' ELSE 'ＮＧ' END AS SHIPPER,  "
                strSQL = strSQL & "   CASE WHEN BK.BOOKING_NO = IV.BOOKINGNO THEN '-' ELSE 'ＮＧ' END AS BOOKINGNO,  "
                strSQL = strSQL & "   CASE WHEN BK.VESSEL_NAME = IV.SHIPPEDPER THEN '-' ELSE 'ＮＧ' END AS SHIPPEDPER,  "
                strSQL = strSQL & "   BK.ETD, CONVERT(VARCHAR,IV.BLDATE2,111) AS BLDATE_01,   "
                strSQL = strSQL & "   BK.LOADING_PORT, IV.INVFROM AS INVFROM_01,  "
                strSQL = strSQL & "   BK.PLACE_OF_RECEIPT, IV.INVON AS INVON_01,  "
                strSQL = strSQL & "   BK.CUT_DATE, CONVERT(VARCHAR,IV.CUTDATE,111) AS CUTDATE_01,   "
                strSQL = strSQL & "   BK.ETD AS ETD_1,  CONVERT(VARCHAR,IV.IOPORTDATE,111) AS IOPORTDATE_01,  "
                strSQL = strSQL & "   BK.ETA, CONVERT(VARCHAR,IV.REACHDATE,111) AS REACHDATE_01,  "
                strSQL = strSQL & "   BK.VOYAGE_NO, IV.VOYAGENO AS VOYAGENO_01,  "
                strSQL = strSQL & "   BK.BOOK_TO, IV.SHIPPER AS SHIPPER_01,  "
                strSQL = strSQL & "   BK.BOOKING_NO, IV.BOOKINGNO AS BOOKINGNO_01,  "
                strSQL = strSQL & "   BK.VESSEL_NAME, IV.SHIPPEDPER AS SHIPPEDPER_01  "
                strSQL = strSQL & "FROM "
                strSQL = strSQL & "   (SELECT * "
                strSQL = strSQL & "      FROM T_BOOKING a  "
                strSQL = strSQL & "      WHERE INVOICE_NO = '" & Trim(dataread("INVOICE_NO")) & "') BK, "
                strSQL = strSQL & "   (SELECT * "
                strSQL = strSQL & "      FROM V_T_INV_HD_TB INV "
                strSQL = strSQL & "      WHERE INV.INVOICENO = "
                strSQL = strSQL & "         (SELECT MAX(INVOICENO) AS IVNO "
                strSQL = strSQL & "            FROM V_T_INV_HD_TB b "
                strSQL = strSQL & "         WHERE b.OLD_INVNO = '" & c & "' "
                strSQL = strSQL & "         AND b.ORG_INVOICENO IS NULL  "
                'strSQL = strSQL & "         AND b.ALLOUTSTAMP IS NULL "        '2024/07/08 深尾さんの依頼でコメント化
                strSQL = strSQL & ")) IV "

                'strSQL = strSQL & "SELECT "
                'strSQL = strSQL & "   b.CUSTCODE AS CUST_CD,  "
                'strSQL = strSQL & "   b.OLD_INVNO AS INVOICE_NO,  "
                'strSQL = strSQL & "   CASE WHEN a.ETD = CONVERT(VARCHAR,b.BLDATE2,111) THEN '-' ELSE 'ＮＧ' END AS BLDATE, "
                'strSQL = strSQL & "   CASE WHEN a.LOADING_PORT = b.INVFROM THEN '-' ELSE 'ＮＧ' END AS INVFROM, "
                'strSQL = strSQL & "   CASE WHEN a.PLACE_OF_RECEIPT = b.INVON THEN '-' ELSE 'ＮＧ' END AS INVON, "
                'strSQL = strSQL & "   CASE WHEN a.CUT_DATE = CONVERT(VARCHAR,b.CUTDATE,111) THEN '-' ELSE 'ＮＧ' END AS CUTDATE, "
                'strSQL = strSQL & "   CASE WHEN a.ETD = CONVERT(VARCHAR,b.IOPORTDATE,111) THEN '-' ELSE 'ＮＧ' END AS IOPORTDATE, "
                'strSQL = strSQL & "   CASE WHEN a.ETA = CONVERT(VARCHAR,b.REACHDATE,111) THEN '-' ELSE 'ＮＧ' END AS REACHDATE, "
                'strSQL = strSQL & "   CASE WHEN a.VOYAGE_NO = b.VOYAGENO THEN '-' ELSE 'ＮＧ' END AS VOYAGENO, "
                'strSQL = strSQL & "   CASE WHEN a.BOOK_TO = b.SHIPPER THEN '-' ELSE 'ＮＧ' END AS SHIPPER, "
                'strSQL = strSQL & "   CASE WHEN a.BOOKING_NO = b.BOOKINGNO THEN '-' ELSE 'ＮＧ' END AS BOOKINGNO, "
                'strSQL = strSQL & "   CASE WHEN a.VESSEL_NAME = b.SHIPPEDPER THEN '-' ELSE 'ＮＧ' END AS SHIPPEDPER, "
                'strSQL = strSQL & "   a.ETD, CONVERT(VARCHAR,b.BLDATE2,111) AS BLDATE_01,  "
                'strSQL = strSQL & "   a.LOADING_PORT, b.INVFROM AS INVFROM_01, "
                'strSQL = strSQL & "   a.PLACE_OF_RECEIPT, b.INVON AS INVON_01, "
                'strSQL = strSQL & "   a.CUT_DATE, CONVERT(VARCHAR,b.CUTDATE,111) AS CUTDATE_01,  "
                'strSQL = strSQL & "   a.ETD AS ETD_1,  CONVERT(VARCHAR,b.IOPORTDATE,111) AS IOPORTDATE_01, "
                'strSQL = strSQL & "   a.ETA, CONVERT(VARCHAR,b.REACHDATE,111) AS REACHDATE_01, "
                'strSQL = strSQL & "   a.VOYAGE_NO, b.VOYAGENO AS VOYAGENO_01, "
                'strSQL = strSQL & "   a.BOOK_TO, b.SHIPPER AS SHIPPER_01, "
                'strSQL = strSQL & "   a.BOOKING_NO, b.BOOKINGNO AS BOOKINGNO_01, "
                'strSQL = strSQL & "   a.VESSEL_NAME, b.SHIPPEDPER AS SHIPPEDPER_01 "
                'strSQL = strSQL & "FROM T_BOOKING a "
                'strSQL = strSQL & "INNER JOIN V_T_INV_HD_TB b "
                'strSQL = strSQL & "ON LEFT(a.CUST_CD,4) = b.CUSTCODE "
                'strSQL = strSQL & "AND a.INVOICE_NO = b.OLD_INVNO "
                'strSQL = strSQL & "WHERE b.OLD_INVNO = '" & c & "' "
                'strSQL = strSQL & "AND b.ORG_INVOICENO IS NULL "
                'strSQL = strSQL & "AND b.ALLOUTSTAMP IS NULL "

                Command2.CommandText = strSQL
                ' SQLの実行
                Command2.ExecuteNonQuery()
            Next

        End While

        ' コミット
        tran.Commit()

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()
        cnn2.Close()
        cnn2.Dispose()

        '再描画
        GridView1.DataBind()

    End Sub

    Private Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""

        If e.CommandName = "edt" Then
            Dim rows As GridViewRowCollection = Me.GridView1.Rows

            'データ項目表示リセット
            For i As Integer = 0 To rows.Count - 1
                Me.GridView1.Rows(i).BackColor = Drawing.Color.White
            Next

            Call Reset_Item()

            'GridView内のボタン押下処理
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim data1 = Me.GridView1.Rows(index).Cells(0).Text      '客先     
            Dim data2 = Me.GridView1.Rows(index).Cells(1).Text      'インボイスNO

            '背景色設定
            Me.GridView1.Rows(index).BackColor = Drawing.Color.Aqua

            '客先とインボイスNO
            Label2.Text = data1
            Label4.Text = data2

            'キーを使ってT_EXL_BKGIVからデータ取得し、ラベルにセット
            '接続文字列の作成
            Dim ConnectionString As String = String.Empty
            'SQL Server認証
            ConnectionString = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
            'SqlConnectionクラスの新しいインスタンスを初期化
            Dim cnn = New SqlConnection(ConnectionString)

            'データベース接続を開く
            cnn.Open()

            strSQL = ""
            strSQL = strSQL & "SELECT * FROM T_EXL_BKGIV "
            strSQL = strSQL & "WHERE CUST_CD = '" & data1 & "' "
            strSQL = strSQL & "AND INVOICE_NO = '" & data2 & "' "

            'ＳＱＬコマンド作成 
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行 
            dataread = dbcmd.ExecuteReader()

            '結果を取り出す 
            While (dataread.Read())
                If dataread("BLDATE") = "ＮＧ" Then   '計上日
                    Label5.Text = dataread("ETD")
                    Label6.Text = dataread("BLDATE_01")
                    Label5.BackColor = System.Drawing.ColorTranslator.FromHtml("#fffa00")
                    Label6.BackColor = System.Drawing.ColorTranslator.FromHtml("#fffa00")
                End If
                If dataread("INVFROM") = "ＮＧ" Then   '積出港
                    Label7.Text = dataread("LOADING_PORT")
                    Label8.Text = dataread("INVFROM_01")
                    Label7.BackColor = System.Drawing.ColorTranslator.FromHtml("#fffa00")
                    Label8.BackColor = System.Drawing.ColorTranslator.FromHtml("#fffa00")
                End If
                If dataread("INVON") = "ＮＧ" Then   '荷受地
                    Label9.Text = dataread("PLACE_OF_RECEIPT")
                    Label10.Text = dataread("INVON_01")
                    Label9.BackColor = System.Drawing.ColorTranslator.FromHtml("#fffa00")
                    Label10.BackColor = System.Drawing.ColorTranslator.FromHtml("#fffa00")
                End If
                If dataread("CUTDATE") = "ＮＧ" Then   'CUT日
                    Label11.Text = dataread("CUT_DATE")
                    Label12.Text = dataread("CUTDATE_01")
                    Label11.BackColor = System.Drawing.ColorTranslator.FromHtml("#fffa00")
                    Label12.BackColor = System.Drawing.ColorTranslator.FromHtml("#fffa00")
                End If
                If dataread("IOPORTDATE") = "ＮＧ" Then   'ETD
                    Label13.Text = dataread("ETD_1")
                    Label14.Text = dataread("IOPORTDATE_01")
                    Label13.BackColor = System.Drawing.ColorTranslator.FromHtml("#fffa00")
                    Label14.BackColor = System.Drawing.ColorTranslator.FromHtml("#fffa00")
                End If
                If dataread("REACHDATE") = "ＮＧ" Then   'ETA
                    Label15.Text = dataread("ETA")
                    Label16.Text = dataread("REACHDATE_01")
                    Label15.BackColor = System.Drawing.ColorTranslator.FromHtml("#fffa00")
                    Label16.BackColor = System.Drawing.ColorTranslator.FromHtml("#fffa00")
                End If
                If dataread("VOYAGENO") = "ＮＧ" Then   'VOY#
                    Label17.Text = dataread("VOYAGE_NO")
                    Label18.Text = dataread("VOYAGENO_01")
                    Label17.BackColor = System.Drawing.ColorTranslator.FromHtml("#fffa00")
                    Label18.BackColor = System.Drawing.ColorTranslator.FromHtml("#fffa00")
                End If
                If dataread("SHIPPER") = "ＮＧ" Then   '船社
                    Label19.Text = dataread("BOOK_TO")
                    Label20.Text = dataread("SHIPPER_01")
                    Label19.BackColor = System.Drawing.ColorTranslator.FromHtml("#fffa00")
                    Label20.BackColor = System.Drawing.ColorTranslator.FromHtml("#fffa00")
                End If
                If dataread("BOOKINGNO") = "ＮＧ" Then   'BKG#
                    Label21.Text = dataread("BOOKING_NO")
                    Label22.Text = dataread("BOOKINGNO_01")
                    Label21.BackColor = System.Drawing.ColorTranslator.FromHtml("#fffa00")
                    Label22.BackColor = System.Drawing.ColorTranslator.FromHtml("#fffa00")
                End If
                If dataread("SHIPPEDPER") = "ＮＧ" Then   '船名
                    Label23.Text = dataread("VESSEL_NAME")
                    Label24.Text = dataread("SHIPPEDPER_01")
                    Label23.BackColor = System.Drawing.ColorTranslator.FromHtml("#fffa00")
                    Label24.BackColor = System.Drawing.ColorTranslator.FromHtml("#fffa00")
                End If
            End While

            'クローズ処理 
            dataread.Close()
            dbcmd.Dispose()
            cnn.Close()
            cnn.Dispose()
        End If
    End Sub

    Private Sub Reset_Item()
        '各項目をリセット GridViewの背景色はRow_Commandで実施

        Label2.Text = ""
        Label4.Text = ""
        Label5.Text = ""
        Label6.Text = ""
        Label7.Text = ""
        Label8.Text = ""
        Label9.Text = ""
        Label10.Text = ""
        Label11.Text = ""
        Label12.Text = ""
        Label13.Text = ""
        Label14.Text = ""
        Label15.Text = ""
        Label16.Text = ""
        Label17.Text = ""
        Label18.Text = ""
        Label19.Text = ""
        Label20.Text = ""
        Label21.Text = ""
        Label22.Text = ""
        Label23.Text = ""
        Label24.Text = ""


    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        '表示データ取得
        Call Make_Grid()
    End Sub
End Class
