﻿Imports System.Data
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
            Me.DropDownList1.Items.Insert(0, "-select-") '先頭にタイトル行追加

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

            If data2 = "&nbsp;" Then
                data2 = ""
            End If

            If Label2.Text = "" Then
                Label2.Text = data1
                TextBox2.Text = data2
            ElseIf Label3.Text = "" Then
                Label3.Text = data1
                TextBox3.Text = data2
            ElseIf Label4.Text = "" Then
                Label4.Text = data1
                TextBox4.Text = data2
            ElseIf Label5.Text = "" Then
                Label5.Text = data1
                TextBox5.Text = data2
            ElseIf Label6.Text = "" Then
                Label6.Text = data1
                TextBox6.Text = data2
            ElseIf Label7.Text = "" Then
                Label7.Text = data1
                TextBox7.Text = data2
            ElseIf Label8.Text = "" Then
                Label8.Text = data1
                TextBox8.Text = data2
            ElseIf Label9.Text = "" Then
                Label9.Text = data1
                TextBox9.Text = data2
            ElseIf Label10.Text = "" Then
                Label10.Text = data1
                TextBox10.Text = data2
            ElseIf Label11.Text = "" Then
                Label11.Text = data1
                TextBox11.Text = data2
            End If
        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        '更新ボタン

        '入力チェック
        If Chk_IVNO() = False Then
            Return
        End If

        strOdrCtrl = ""

        If Label2.Text <> "" Then
            Call INS_DATA(Label2.Text, TextBox2.Text)       '登録または更新
        End If
        If Label3.Text <> "" Then
            Call INS_DATA(Label3.Text, TextBox3.Text)
        End If
        If Label4.Text <> "" Then
            Call INS_DATA(Label4.Text, TextBox4.Text)
        End If
        If Label5.Text <> "" Then
            Call INS_DATA(Label5.Text, TextBox5.Text)
        End If
        If Label6.Text <> "" Then
            Call INS_DATA(Label6.Text, TextBox6.Text)
        End If
        If Label7.Text <> "" Then
            Call INS_DATA(Label7.Text, TextBox7.Text)
        End If
        If Label8.Text <> "" Then
            Call INS_DATA(Label8.Text, TextBox8.Text)
        End If
        If Label9.Text <> "" Then
            Call INS_DATA(Label9.Text, TextBox9.Text)
        End If
        If Label10.Text <> "" Then
            Call INS_DATA(Label10.Text, TextBox10.Text)
        End If
        If Label11.Text <> "" Then
            Call INS_DATA(Label11.Text, TextBox11.Text)
        End If

        If strOdrCtrl <> "" Then
            '新規登録があった場合、EXL担当者にメール送信
            Session("strOdrCtrl") = Mid(strOdrCtrl, 1, Len(strOdrCtrl) - 1)
            Session("strTan") = DropDownList1.SelectedValue
            Response.Redirect("./air_exc_comfirm.aspx")
        Else
            '新規登録以外は、再表示
            Label12.Text = "更新しました。"

            Label2.Text = ""
            Label3.Text = ""
            Label4.Text = ""
            Label5.Text = ""
            Label6.Text = ""
            Label7.Text = ""
            Label8.Text = ""
            Label9.Text = ""
            Label10.Text = ""
            Label11.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
            TextBox4.Text = ""
            TextBox5.Text = ""
            TextBox6.Text = ""
            TextBox7.Text = ""
            TextBox8.Text = ""
            TextBox9.Text = ""
            TextBox10.Text = ""
            TextBox11.Text = ""

            '表示データ取得
            Call Make_Grid()
        End If

    End Sub

    Private Sub INS_DATA(strCtrl As String, strIVNO As String)
        '既存データの削除
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim intRec As Integer = 0

        'SQL Server認証
        Dim ConnectionString As String = String.Empty
        ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand

        cnn.Open()

        strSQL = ""
        strSQL = strSQL & "SELECT COUNT(*) AS RecCnt FROM T_EXL_AIR_EXCLUSIVE "
        strSQL = strSQL & "WHERE ODR_CTL_NO = '" & strCtrl & "' "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        '結果を取り出す 
        While (dataread.Read())
            intRec = dataread("RecCnt")
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()

        If intRec > 0 Then
            '既に存在している場合
            If strIVNO <> "" Then
                'IVNOが入っている場合、UPDATE
                strSQL = ""
                strSQL = strSQL & "UPDATE T_EXL_AIR_EXCLUSIVE "
                strSQL = strSQL & "SET IVNO = '" & strIVNO & "' "
                strSQL = strSQL & "WHERE ODR_CTL_NO = '" & strCtrl & "' "
            Else
                strSQL = ""
                strSQL = strSQL & "DELETE FROM T_EXL_AIR_EXCLUSIVE "
                strSQL = strSQL & "WHERE ODR_CTL_NO = '" & strCtrl & "' "
            End If

        Else
            '存在し無い場合、INSERT
            strSQL = ""
            strSQL = strSQL & "INSERT INTO T_EXL_AIR_EXCLUSIVE "
            strSQL = strSQL & "VALUES('" & strCtrl & "' "
            strSQL = strSQL & ",'" & strIVNO & "' "
            strSQL = strSQL & ")"

            strOdrCtrl += "'" & strCtrl & "',"
        End If

        Command.CommandText = strSQL
        ' SQLの実行
        Command.ExecuteNonQuery()

        'クローズ処理 
        cnn.Close()
        cnn.Dispose()
    End Sub

    Private Function Chk_IVNO() As Boolean
        '入力チェック

        Chk_IVNO = True

        If Label2.Text = "" And Label3.Text = "" And Label4.Text = "" And Label5.Text = "" And
           Label6.Text = "" And Label7.Text = "" And Label8.Text = "" And Label9.Text = "" And
           Label10.Text = "" And Label11.Text = "" Then
            Label12.Text = "更新対象がありません。"
            Chk_IVNO = False
        End If

        If DropDownList1.SelectedValue = "" Then
            Label12.Text = "更新者が選択されていません。"
            Chk_IVNO = False
        End If

    End Function

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Label2.Text = ""
        TextBox2.Text = ""
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Label3.Text = ""
        TextBox3.Text = ""
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Label4.Text = ""
        TextBox4.Text = ""
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Label5.Text = ""
        TextBox5.Text = ""
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Label6.Text = ""
        TextBox6.Text = ""
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Label7.Text = ""
        TextBox7.Text = ""
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Label8.Text = ""
        TextBox8.Text = ""
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Label9.Text = ""
        TextBox9.Text = ""
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Label10.Text = ""
        TextBox10.Text = ""
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        Label11.Text = ""
        TextBox11.Text = ""
    End Sub

End Class