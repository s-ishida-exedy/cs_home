Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.Common
Imports System.IO

Partial Class cs_home
    Inherits System.Web.UI.Page

    Public strRow As String
    Public strProcess As String

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim TableRow As TableRow
        Dim TableCell As TableCell

        If IsPostBack Then
            ' そうでない時処理
            Dim i As String = ""
        Else
            'AIR専用客先のセールスノート情報取得
            Call Get_SN_Data()

            TableRow = New TableRow()
            TableCell = New TableCell()
            TableCell.Text = "セル"
            TableRow.Cells.Add(TableCell)
            TableCell = New TableCell()
            TableCell.Text = "セル1"
            TableRow.Cells.Add(TableCell)
            Table1.Rows.Add(TableRow)
        End If

        '表示データ取得
        Call Make_Grid()
    End Sub

    Private Sub Make_Grid()
        'GRIDを作成する。

        Dim strMode As String = ""
        Dim Dataobj As New DBAccess

        If CheckBox2.Checked = True Then
            strMode = "1"       'IVNO未
        Else
            strMode = "0"       '全件
        End If

        'データの取得
        Dim ds As DataSet = Dataobj.GET_RESULT_AIR_EXC(strMode)
        If ds.Tables.Count > 0 Then
            GridView1.DataSourceID = ""
            GridView1.DataSource = ds
            GridView1.DataBind()
        End If
    End Sub

    Private Sub Get_SN_Data()
        'AIR専用客先のセールスノート情報取得
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

        Try
            'データベース接続を開く
            cnn.Open()

            '既存データ削除
            cnn2.Open()
            strSQL = ""
            strSQL = strSQL & "DELETE FROM T_EXL_AIR_EXC_ODR "
            Command.CommandText = strSQL
            ' SQLの実行
            Command.ExecuteNonQuery()
            cnn2.Close()

            'AIR専用客先取得
            Call GET_EXCLUSIVE_CST(strCST)

            'イントラからAIR専用客先のオーダーを取得する
            strSQL = ""
            strSQL = strSQL & "SELECT DISTINCT "
            strSQL = strSQL & "  b.CUST_CD "
            strSQL = strSQL & "    , IIf(  "
            strSQL = strSQL & "    [adjusted_dlv_date] Is Null "
            strSQL = strSQL & "    , [desinated_dlv_date] "
            strSQL = strSQL & "    , [adjusted_dlv_date] "
            strSQL = strSQL & "  ) AS NOUKI "
            strSQL = strSQL & "  , b.LS_TYP "
            strSQL = strSQL & "  , b.cust_odr_no "
            strSQL = strSQL & "  , b.odr_ctl_no "
            strSQL = strSQL & "  , a.SALESNOTENO  "
            strSQL = strSQL & "FROM "
            strSQL = strSQL & "  dbo.T_SN_VIEW a "
            strSQL = strSQL & "  INNER JOIN dbo.T_ODR_VIEW b "
            strSQL = strSQL & "  ON a.CUSTCODE=b.CUST_CD "
            strSQL = strSQL & "  AND a.ORDERKEY = b.odr_ctl_no  "
            strSQL = strSQL & "WHERE "
            strSQL = strSQL & "  b.CUST_CD IN (" & strCST & ") "

            'ＳＱＬコマンド作成 
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行 
            dataread = dbcmd.ExecuteReader()

            Dim strCUST_CD, strNOUKI, strLS_TYP, strCUST_ODR_NO, strOdrCtrl, strSALESNOTENO As String

            cnn2.Open()

            'トランザクションの開始
            tran = cnn2.BeginTransaction
            ' トランザクションをすることを明示する
            Command.Transaction = tran

            '結果を取り出す 
            While (dataread.Read())

                strCUST_CD = Trim(dataread("CUST_CD"))
                strNOUKI = Trim(dataread("NOUKI"))
                strLS_TYP = Trim(dataread("LS_TYP"))
                strCUST_ODR_NO = Trim(dataread("cust_odr_no"))
                strOdrCtrl = Trim(dataread("odr_ctl_no"))
                strSALESNOTENO = Trim(dataread("SALESNOTENO"))

                'T_EXL_AIR_EXCLUSIVEにINSERT
                strSQL = ""
                strSQL = strSQL & "INSERT INTO T_EXL_AIR_EXC_ODR "
                strSQL = strSQL & "(CUST_CD, NOUKI, LS_TYP, CUST_ODR_NO, SALESNOTENO, ODR_CTL_NO, IVNO) "
                strSQL = strSQL & "VALUES(  "
                strSQL = strSQL & "'" & strCUST_CD & "' "
                strSQL = strSQL & ",'" & strNOUKI & "' "
                strSQL = strSQL & ",'" & strLS_TYP & "' "
                strSQL = strSQL & ",'" & strCUST_ODR_NO & "' "
                strSQL = strSQL & ",'" & strSALESNOTENO & "' "
                strSQL = strSQL & ",'" & strOdrCtrl & "' "
                strSQL = strSQL & ",'') "

                Command.CommandText = strSQL
                ' SQLの実行
                Command.ExecuteNonQuery()

            End While

            ' コミット
            tran.Commit()


        Catch ex As Exception
            Console.WriteLine("Error! {0}", ex.Message)

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

    End Sub

    Private Sub GET_EXCLUSIVE_CST(ByRef strCUST As String)
        'AIR専用客先コード取得
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String
        Dim strCode As String

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()

        strSQL = ""
        strSQL = strSQL & "SELECT CUST_CD FROM M_EXL_AIR_EXC_CST "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        strCode = ""
        '結果を取り出す 
        While (dataread.Read())
            strCUST += "'" & Trim(dataread("CUST_CD")) & "',"
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()
        cnn.Close()
        cnn.Dispose()

        '最後のカンマを削除する
        strCUST = Mid(strCUST, 1, Len(strCUST) - 1)
    End Sub

    Private Sub GridView1_RowCreated(sender As Object, e As GridViewRowEventArgs) Handles GridView1.RowCreated
        'ボタンに行数をセット
        If e.Row.RowType = DataControlRowType.DataRow Then
            Dim dltButton As Button = e.Row.FindControl("Button1")
            'ボタンが存在する場合のみセット
            If Not (dltButton Is Nothing) Then
                dltButton.CommandArgument = e.Row.RowIndex.ToString()
            End If
        End If

    End Sub

    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged

        Call Make_Grid()
    End Sub

    Private Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand

        If e.CommandName = "edt" Then
            'GridView内のボタン押下処理
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim data1 = Me.GridView1.Rows(index).Cells(6).Text
            Dim data2 = Me.GridView1.Rows(index).Cells(7).Text



        End If

    End Sub
End Class
