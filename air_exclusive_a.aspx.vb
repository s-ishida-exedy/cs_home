Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.Common
Imports System.IO

Partial Class cs_home
    Inherits System.Web.UI.Page

    Public strOdrCtrl As String

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        Label12.Text = ""

        If IsPostBack Then
            ' そうでない時処理
            Dim i As String = ""
        Else
            '宛先の初期値
            TextBox12.Text = "r-uchida@exedy.com"

            'AIR専用客先のセールスノート情報取得
            Call Get_SN_Data()

            '表示データ取得
            Call Make_Grid()
        End If

        Button1.Attributes.Add("onclick", "return confirm('登録します。よろしいですか？');")

    End Sub

    Private Sub Make_Grid()
        'GRIDを作成する。

        Dim strMode As String = ""
        Dim Dataobj As New DBAccess

        strMode = "1"       'IVNO未

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

        'k3hwpm02用
        Dim ConnectionString2 As String = String.Empty
        'SQL Server認証
        ConnectionString2 = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
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
            strSQL = strSQL & "    , CONVERT(nvarchar,IIf(  "
            strSQL = strSQL & "    [adjusted_dlv_date] Is Null "
            strSQL = strSQL & "    , [desinated_dlv_date] "
            strSQL = strSQL & "    , [adjusted_dlv_date] "
            strSQL = strSQL & "  ),111) AS NOUKI "
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
        ConnectionString = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
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

    Private Sub GridView1_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridView1.RowCommand

        If e.CommandName = "edt" Then
            'GridView内のボタン押下処理
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)
            Dim data1 = Me.GridView1.Rows(index).Cells(6).Text
            Dim data2 = Me.GridView1.Rows(index).Cells(7).Text

            Dim strCst = Me.GridView1.Rows(index).Cells(1).Text
            Dim strEtd = Me.GridView1.Rows(index).Cells(2).Text
            Me.GridView1.Rows(index).BackColor = Drawing.Color.Aqua

            Dim strCst2 = ""
            Dim strEtd2 = ""

            Dim rows As GridViewRowCollection = Me.GridView1.Rows

            TextBox13.Text = ""

            'ボタンを押した行と同じ客先、ETDの背景色を塗る
            For i As Integer = 0 To rows.Count - 1
                strCst2 = Me.GridView1.Rows(i).Cells(1).Text
                strEtd2 = Me.GridView1.Rows(i).Cells(2).Text
                If strCst = strCst2 And strEtd = strEtd2 Then
                    Me.GridView1.Rows(i).BackColor = Drawing.Color.Aqua
                    '選択された受注管理番号を変数へ入れる
                    strOdrCtrl += "'" & Me.GridView1.Rows(i).Cells(6).Text & "',"
                End If
            Next

            If data2 = "&nbsp;" Then
                data2 = ""
            End If

            Label3.Text = strCst
            Label5.Text = strEtd
            TextBox13.Text = Mid(strOdrCtrl, 1, Len(strOdrCtrl) - 1)
        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        '登録ボタン

        '入力チェック
        If Chk_IVNO() = False Then
            Return
        End If

        If Label2.Text <> "" Then
            Call INS_DATA(TextBox2.Text)       '登録
        End If

        'EXL担当者にメール送信
        Session("strOdrCtrl") = TextBox13.Text
        Session("strTO") = TextBox12.Text
        Response.Redirect("./air_exc_comfirm.aspx")

    End Sub

    Private Sub INS_DATA(strIVNO As String)
        'データ登録
        Dim strSQL As String = ""
        Dim intRec As Integer = 0

        'SQL Server認証
        Dim ConnectionString As String = String.Empty
        ConnectionString = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand

        cnn.Open()

        strOdrCtrl = TextBox13.Text
        Dim arr1() As String = strOdrCtrl.Split(",")
        For Each c In arr1
            strSQL = ""
            strSQL = strSQL & "INSERT INTO T_EXL_AIR_EXCLUSIVE "
            strSQL = strSQL & "VALUES(" & c & " "
            strSQL = strSQL & ",'" & strIVNO & "' "
            strSQL = strSQL & ")"
            Command.CommandText = strSQL
            ' SQLの実行
            Command.ExecuteNonQuery()
        Next

        'クローズ処理 
        cnn.Close()
        cnn.Dispose()
    End Sub

    Private Function Chk_IVNO() As Boolean
        '入力チェック

        Chk_IVNO = True

        If TextBox2.Text = "" Then
            Label12.Text = "インボイスNOが未入力です。"
            Chk_IVNO = False
        End If
        If Label3.Text = "" Or Label5.Text = "" Then
            Label12.Text = "対象が選択されていません。。"
            Chk_IVNO = False
        End If

    End Function

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click
        'リセットボタン押下
        TextBox2.Text = ""

        Call Make_Grid()
    End Sub
End Class
