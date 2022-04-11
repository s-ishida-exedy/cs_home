Imports System.Data.SqlClient
Imports mod_function

Partial Class cs_home
    Inherits System.Web.UI.Page

    Public strRow As String
    Public strProcess As String

    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load
        Dim strSQL As String = ""
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strValue As String = ""
        Dim strVal As String = ""

        If IsPostBack Then
            ' そうでない時処理
        Else
            'パラメータ取得
            Dim strCust As String = Session("strCust")
            Dim strMode As String = Session("strMode")

            '接続文字列の作成
            Dim ConnectionString As String = String.Empty
            'SQL Server認証
            ConnectionString = "Data Source=kbhwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
            'SqlConnectionクラスの新しいインスタンスを初期化
            Dim cnn = New SqlConnection(ConnectionString)

            'データベース接続を開く
            cnn.Open()

            strSQL = ""
            strSQL = strSQL & "SELECT * FROM T_EXL_CSMANUAL "
            strSQL = strSQL & "WHERE NEW_CODE = '" & strCust & "'"

            'ＳＱＬコマンド作成 
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行 
            dataread = dbcmd.ExecuteReader()

            '結果を取り出す 
            While (dataread.Read())
                DropDownList1.SelectedValue = dataread("IV_NECE").ToString
                DropDownList2.SelectedValue = dataread("PL_NECE").ToString
                DropDownList3.SelectedValue = dataread("BL_NECE").ToString
                DropDownList4.SelectedValue = dataread("CO_NECE").ToString
                DropDownList5.SelectedValue = dataread("EPA_NECE").ToString
                DropDownList6.SelectedValue = dataread("WOOD_NECE").ToString
                DropDownList7.SelectedValue = dataread("DELI_NECE").ToString
                DropDownList8.SelectedValue = dataread("INSP_NECE").ToString
                DropDownList9.SelectedValue = dataread("ERL_NECE").ToString
                DropDownList10.SelectedValue = dataread("VESS_NECE").ToString
                DropDownList11.SelectedValue = dataread("CONTAINER_CLEANING").ToString
                DropDownList12.SelectedValue = dataread("LC").ToString
                DropDownList13.SelectedValue = dataread("DOC_NECESSITY").ToString
                DropDownList14.SelectedValue = dataread("FTA").ToString
                DropDownList15.SelectedValue = dataread("CERTIFICATE_OF_CONFORMITY").ToString
                DropDownList16.SelectedValue = dataread("DOC_OF_EGYPT").ToString

                If strMode = "01" Then
                    '更新モード
                    TextBox1.Text = dataread("NEW_CODE").ToString
                    TextBox1.Enabled = False
                Else
                    '登録モード
                    TextBox1.Text = ""
                End If
                TextBox2.Text = dataread("OLD_CODE").ToString
                TextBox3.Text = dataread("CUST_NM").ToString
                TextBox4.Text = dataread("CUST_AB").ToString
                TextBox5.Text = dataread("INCOTEM").ToString
                TextBox6.Text = dataread("BL_TYPE").ToString
                TextBox7.Text = dataread("BL_SEND").ToString
                TextBox8.Text = dataread("CUST_ADDRESS").ToString
                TextBox55.Text = dataread("CONSIGNEE").ToString
                TextBox9.Text = dataread("CNEE_NM_SI").ToString
                TextBox10.Text = dataread("FIN_DESTINATION").ToString
                TextBox11.Text = dataread("NOTIFY").ToString
                TextBox12.Text = dataread("FORWARDER_INFO").ToString
                TextBox13.Text = dataread("CUST_REQ").ToString
                TextBox14.Text = dataread("SALES_STAFF").ToString
                TextBox15.Text = dataread("SALES_STAFF1").ToString
                TextBox16.Text = dataread("SALES_STAFF2").ToString
                TextBox17.Text = dataread("SALES_STAFF3").ToString
                TextBox18.Text = dataread("SALES_STAFF4").ToString
                TextBox19.Text = dataread("CUST_STAFF").ToString
                TextBox20.Text = dataread("CUST_STAFF1").ToString
                TextBox21.Text = dataread("CUST_STAFF2").ToString
                TextBox22.Text = dataread("CUST_STAFF3").ToString
                TextBox23.Text = dataread("CUST_STAFF4").ToString
                TextBox24.Text = dataread("SHIP_DAY_OF_WEEK").ToString
                TextBox25.Text = dataread("DESTINATION").ToString
                TextBox26.Text = dataread("SHIPMENT_KBN").ToString
                TextBox27.Text = dataread("LT").ToString
                TextBox28.Text = dataread("PORT").ToString
                TextBox29.Text = dataread("SHIP_AGREE").ToString
                TextBox30.Text = dataread("SHIPPING_COMPANY").ToString
                TextBox31.Text = dataread("LOCAL_AGENCY").ToString
                TextBox32.Text = dataread("CONTAINER_SIZE").ToString
                TextBox33.Text = dataread("REMARKS_ONBL").ToString
                TextBox34.Text = dataread("IV_ON_BL").ToString
                TextBox56.Text = dataread("HS_ON_BL").ToString
                TextBox35.Text = dataread("CNEE_STAFF").ToString
                TextBox36.Text = dataread("CNEE_STAFF_CONTACT1").ToString
                TextBox37.Text = dataread("CNEE_STAFF_CONTACT2").ToString
                TextBox38.Text = dataread("CNEE_STAFF2").ToString
                TextBox39.Text = dataread("CNEE_STAFF2_CONTACT").ToString
                TextBox40.Text = dataread("TAXID").ToString
                TextBox41.Text = dataread("CONSIGNEE_OF_SI").ToString
                TextBox42.Text = dataread("CONSIGNEE_OF_SI_ADDRESS").ToString
                TextBox43.Text = dataread("FINAL_DES").ToString
                TextBox44.Text = dataread("FINAL_DES_ADDRESS").ToString
                TextBox45.Text = dataread("BEARING").ToString
                TextBox46.Text = dataread("IV_AUTO_CALC").ToString
                TextBox47.Text = dataread("FORWARDER").ToString
                TextBox48.Text = dataread("FORWARDER_STAFF").ToString
                TextBox49.Text = dataread("TO1").ToString
                TextBox50.Text = dataread("CC1").ToString
                TextBox51.Text = dataread("CC2").ToString
                TextBox52.Text = dataread("CC3").ToString
                TextBox53.Text = dataread("FORWARDER_NM").ToString
                TextBox54.Text = dataread("FORWARDER_STAFF_NM").ToString
            End While

            'クローズ処理 
            dataread.Close()
            dbcmd.Dispose()
            cnn.Close()
            cnn.Dispose()

            'モードによりボタン名称を変更する
            If strMode = "01" Then
                Button7.Text = "更　　新"
                Button7.Attributes.Add("onclick", "return confirm('更新します。よろしいですか？');")
            Else
                Button7.Text = "登　　録"
                Button7.Attributes.Add("onclick", "return confirm('登録します。よろしいですか？');")
            End If

        End If

    End Sub

    Private Sub DB_access()
        '画面入力情報をテーブルへ登録
        Dim strSQL As String
        Dim strVal As String = ""

        'パラメータ取得
        Dim strCust As String = Session("strCust")
        Dim strMode As String = Session("strMode")

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=KBHWPM02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)
        Dim Command = cnn.CreateCommand

        'データベース接続を開く
        cnn.Open()

        If strMode = "01" Then
            'データ更新
            strSQL = ""
            strSQL = strSQL & "UPDATE T_EXL_CSMANUAL "
            strSQL = strSQL & "SET "
            strSQL = strSQL & "  OLD_CODE =  '" & Replace(LTrim(RTrim(TextBox2.Text)), "'", "''") & "' "
            strSQL = strSQL & "  , CUST_NM =  '" & Replace(LTrim(RTrim(TextBox3.Text)), "'", "''") & "' "
            strSQL = strSQL & "  , CUST_AB =  '" & Replace(LTrim(RTrim(TextBox4.Text)), "'", "''") & "' "
            strSQL = strSQL & "  , INCOTEM =  '" & Replace(LTrim(RTrim(TextBox5.Text)), "'", "''") & "' "
            strSQL = strSQL & "  , BL_TYPE =  '" & Replace(LTrim(RTrim(TextBox6.Text)), "'", "''") & "' "
            strSQL = strSQL & "  , BL_SEND =  '" & Replace(LTrim(RTrim(TextBox7.Text)), "'", "''") & "' "
            strSQL = strSQL & "  , CUST_ADDRESS =  '" & Replace(LTrim(RTrim(TextBox8.Text)), "'", "''") & "' "
            strSQL = strSQL & "  , CONSIGNEE =  '" & Replace(LTrim(RTrim(TextBox55.Text)), "'", "''") & "' "
            strSQL = strSQL & "  , CNEE_NM_SI =  '" & Replace(LTrim(RTrim(TextBox9.Text)), "'", "''") & "' "
            strSQL = strSQL & "  , FIN_DESTINATION =  '" & Replace(LTrim(RTrim(TextBox10.Text)), "'", "''") & "' "
            strSQL = strSQL & "  , NOTIFY =  '" & Replace(LTrim(RTrim(TextBox11.Text)), "'", "''") & "' "
            strSQL = strSQL & "  , FORWARDER_INFO =  '" & Replace(LTrim(RTrim(TextBox12.Text)), "'", "''") & "' "
            strSQL = strSQL & "  , CUST_REQ =  '" & Replace(LTrim(RTrim(TextBox13.Text)), "'", "''") & "' "
            strSQL = strSQL & "  , IV_NECE =  '" & DropDownList1.SelectedValue & "' "
            strSQL = strSQL & "  , PL_NECE =  '" & DropDownList2.SelectedValue & "' "
            strSQL = strSQL & "  , BL_NECE =  '" & DropDownList3.SelectedValue & "' "
            strSQL = strSQL & "  , CO_NECE =  '" & DropDownList4.SelectedValue & "' "
            strSQL = strSQL & "  , EPA_NECE =  '" & DropDownList5.SelectedValue & "' "
            strSQL = strSQL & "  , WOOD_NECE =  '" & DropDownList6.SelectedValue & "' "
            strSQL = strSQL & "  , DELI_NECE =  '" & DropDownList7.SelectedValue & "' "
            strSQL = strSQL & "  , INSP_NECE =  '" & DropDownList8.SelectedValue & "' "
            strSQL = strSQL & "  , ERL_NECE =  '" & DropDownList9.SelectedValue & "' "
            strSQL = strSQL & "  , VESS_NECE =  '" & DropDownList10.SelectedValue & "' "
            strSQL = strSQL & "  , SALES_STAFF =  '" & Replace(LTrim(RTrim(TextBox14.Text)), "'", "''") & "' "
            strSQL = strSQL & "  , SALES_STAFF1 =  '" & Replace(LTrim(RTrim(TextBox15.Text)), "'", "''") & "' "
            strSQL = strSQL & "  , SALES_STAFF2 =  '" & Replace(LTrim(RTrim(TextBox16.Text)), "'", "''") & "' "
            strSQL = strSQL & "  , SALES_STAFF3 =  '" & Replace(LTrim(RTrim(TextBox17.Text)), "'", "''") & "' "
            strSQL = strSQL & "  , SALES_STAFF4 =  '" & Replace(LTrim(RTrim(TextBox18.Text)), "'", "''") & "' "
            strSQL = strSQL & "  , CUST_STAFF =  '" & Replace(LTrim(RTrim(TextBox19.Text)), "'", "''") & "' "
            strSQL = strSQL & "  , CUST_STAFF1 =  '" & Replace(LTrim(RTrim(TextBox20.Text)), "'", "''") & "' "
            strSQL = strSQL & "  , CUST_STAFF2 =  '" & Replace(LTrim(RTrim(TextBox21.Text)), "'", "''") & "' "
            strSQL = strSQL & "  , CUST_STAFF3 =  '" & Replace(LTrim(RTrim(TextBox22.Text)), "'", "''") & "' "
            strSQL = strSQL & "  , CUST_STAFF4 =  '" & Replace(LTrim(RTrim(TextBox23.Text)), "'", "''") & "' "
            strSQL = strSQL & "  , SHIP_DAY_OF_WEEK =  '" & Replace(LTrim(RTrim(TextBox24.Text)), "'", "''") & "' "
            strSQL = strSQL & "  , DESTINATION =  '" & Replace(LTrim(RTrim(TextBox25.Text)), "'", "''") & "' "
            strSQL = strSQL & "  , SHIPMENT_KBN =  '" & Replace(LTrim(RTrim(TextBox26.Text)), "'", "''") & "' "
            strSQL = strSQL & "  , LT =  '" & Replace(LTrim(RTrim(TextBox27.Text)), "'", "''") & "' "
            strSQL = strSQL & "  , PORT =  '" & Replace(LTrim(RTrim(TextBox28.Text)), "'", "''") & "' "
            strSQL = strSQL & "  , SHIP_AGREE =  '" & Replace(LTrim(RTrim(TextBox29.Text)), "'", "''") & "' "
            strSQL = strSQL & "  , SHIPPING_COMPANY =  '" & Replace(LTrim(RTrim(TextBox30.Text)), "'", "''") & "' "
            strSQL = strSQL & "  , LOCAL_AGENCY =  '" & Replace(LTrim(RTrim(TextBox31.Text)), "'", "''") & "' "
            strSQL = strSQL & "  , CONTAINER_SIZE =  '" & Replace(LTrim(RTrim(TextBox32.Text)), "'", "''") & "' "
            strSQL = strSQL & "  , REMARKS_ONBL =  '" & Replace(LTrim(RTrim(TextBox33.Text)), "'", "''") & "' "
            strSQL = strSQL & "  , IV_ON_BL =  '" & Replace(LTrim(RTrim(TextBox34.Text)), "'", "''") & "' "
            strSQL = strSQL & "  , HS_ON_BL =  '" & Replace(LTrim(RTrim(TextBox56.Text)), "'", "''") & "' "
            strSQL = strSQL & "  , CNEE_STAFF =  '" & Replace(LTrim(RTrim(TextBox35.Text)), "'", "''") & "' "
            strSQL = strSQL & "  , CNEE_STAFF_CONTACT1 =  '" & Replace(LTrim(RTrim(TextBox36.Text)), "'", "''") & "' "
            strSQL = strSQL & "  , CNEE_STAFF_CONTACT2 =  '" & Replace(LTrim(RTrim(TextBox37.Text)), "'", "''") & "' "
            strSQL = strSQL & "  , CNEE_STAFF2 =  '" & Replace(LTrim(RTrim(TextBox38.Text)), "'", "''") & "' "
            strSQL = strSQL & "  , CNEE_STAFF2_CONTACT =  '" & Replace(LTrim(RTrim(TextBox39.Text)), "'", "''") & "' "
            strSQL = strSQL & "  , TAXID =  '" & Replace(LTrim(RTrim(TextBox40.Text)), "'", "''") & "' "
            strSQL = strSQL & "  , CONTAINER_CLEANING =  '" & DropDownList11.SelectedValue & "' "
            strSQL = strSQL & "  , LC =  '" & DropDownList12.SelectedValue & "' "
            strSQL = strSQL & "  , CONSIGNEE_OF_SI =  '" & Replace(LTrim(RTrim(TextBox41.Text)), "'", "''") & "' "
            strSQL = strSQL & "  , CONSIGNEE_OF_SI_ADDRESS =  '" & Replace(LTrim(RTrim(TextBox42.Text)), "'", "''") & "' "
            strSQL = strSQL & "  , FINAL_DES =  '" & Replace(LTrim(RTrim(TextBox43.Text)), "'", "''") & "' "
            strSQL = strSQL & "  , FINAL_DES_ADDRESS =  '" & Replace(LTrim(RTrim(TextBox44.Text)), "'", "''") & "' "
            strSQL = strSQL & "  , BEARING =  '" & Replace(LTrim(RTrim(TextBox45.Text)), "'", "''") & "' "
            strSQL = strSQL & "  , IV_AUTO_CALC =  '" & Replace(LTrim(RTrim(TextBox46.Text)), "'", "''") & "' "
            strSQL = strSQL & "  , FORWARDER =  '" & Replace(LTrim(RTrim(TextBox47.Text)), "'", "''") & "' "
            strSQL = strSQL & "  , FORWARDER_STAFF =  '" & Replace(LTrim(RTrim(TextBox48.Text)), "'", "''") & "' "
            strSQL = strSQL & "  , TO1 =  '" & Replace(LTrim(RTrim(TextBox49.Text)), "'", "''") & "' "
            strSQL = strSQL & "  , CC1 =  '" & Replace(LTrim(RTrim(TextBox50.Text)), "'", "''") & "' "
            strSQL = strSQL & "  , CC2 =  '" & Replace(LTrim(RTrim(TextBox51.Text)), "'", "''") & "' "
            strSQL = strSQL & "  , CC3 =  '" & Replace(LTrim(RTrim(TextBox52.Text)), "'", "''") & "' "
            strSQL = strSQL & "  , FORWARDER_NM =  '" & Replace(LTrim(RTrim(TextBox53.Text)), "'", "''") & "' "
            strSQL = strSQL & "  , FORWARDER_STAFF_NM =  '" & Replace(LTrim(RTrim(TextBox54.Text)), "'", "''") & "' "
            strSQL = strSQL & "  , DOC_NECESSITY =  '" & DropDownList13.SelectedValue & "' "
            strSQL = strSQL & "  , FTA =  '" & DropDownList14.SelectedValue & "' "
            strSQL = strSQL & "  , CERTIFICATE_OF_CONFORMITY =  '" & DropDownList15.SelectedValue & "' "
            strSQL = strSQL & "  , DOC_OF_EGYPT =  '" & DropDownList16.SelectedValue & "' "
            strSQL = strSQL & "WHERE "
            strSQL = strSQL & "  NEW_CODE =  '" & strCust & "' "
        Else
            strSQL = ""
            strSQL = strSQL & "INSERT INTO T_EXL_CSMANUAL "
            strSQL = strSQL & "VALUES ( "
            strSQL = strSQL & "    '" & LTrim(RTrim(TextBox1.Text)) & "' "
            strSQL = strSQL & "  , '" & LTrim(RTrim(TextBox2.Text)) & "' "
            strSQL = strSQL & "  , '" & LTrim(RTrim(TextBox3.Text)) & "' "
            strSQL = strSQL & "  , '" & LTrim(RTrim(TextBox4.Text)) & "' "
            strSQL = strSQL & "  , '" & LTrim(RTrim(TextBox5.Text)) & "' "
            strSQL = strSQL & "  , '" & LTrim(RTrim(TextBox6.Text)) & "' "
            strSQL = strSQL & "  , '" & LTrim(RTrim(TextBox7.Text)) & "' "
            strSQL = strSQL & "  , '" & LTrim(RTrim(TextBox8.Text)) & "' "
            strSQL = strSQL & "  , '" & LTrim(RTrim(TextBox55.Text)) & "' "
            strSQL = strSQL & "  , '" & LTrim(RTrim(TextBox9.Text)) & "' "
            strSQL = strSQL & "  , '" & LTrim(RTrim(TextBox10.Text)) & "' "
            strSQL = strSQL & "  , '" & LTrim(RTrim(TextBox11.Text)) & "' "
            strSQL = strSQL & "  , '" & LTrim(RTrim(TextBox12.Text)) & "' "
            strSQL = strSQL & "  , '" & LTrim(RTrim(TextBox13.Text)) & "' "
            strSQL = strSQL & "  , '" & DropDownList1.SelectedValue & "' "
            strSQL = strSQL & "  , '" & DropDownList2.SelectedValue & "' "
            strSQL = strSQL & "  , '" & DropDownList3.SelectedValue & "' "
            strSQL = strSQL & "  , '" & DropDownList4.SelectedValue & "' "
            strSQL = strSQL & "  , '" & DropDownList5.SelectedValue & "' "
            strSQL = strSQL & "  , '" & DropDownList6.SelectedValue & "' "
            strSQL = strSQL & "  , '" & DropDownList7.SelectedValue & "' "
            strSQL = strSQL & "  , '" & DropDownList8.SelectedValue & "' "
            strSQL = strSQL & "  , '" & DropDownList9.SelectedValue & "' "
            strSQL = strSQL & "  , '" & DropDownList10.SelectedValue & "' "
            strSQL = strSQL & "  , '" & LTrim(RTrim(TextBox14.Text)) & "' "
            strSQL = strSQL & "  , '" & LTrim(RTrim(TextBox15.Text)) & "' "
            strSQL = strSQL & "  , '" & LTrim(RTrim(TextBox16.Text)) & "' "
            strSQL = strSQL & "  , '" & LTrim(RTrim(TextBox17.Text)) & "' "
            strSQL = strSQL & "  , '" & LTrim(RTrim(TextBox18.Text)) & "' "
            strSQL = strSQL & "  , '" & LTrim(RTrim(TextBox19.Text)) & "' "
            strSQL = strSQL & "  , '" & LTrim(RTrim(TextBox20.Text)) & "' "
            strSQL = strSQL & "  , '" & LTrim(RTrim(TextBox21.Text)) & "' "
            strSQL = strSQL & "  , '" & LTrim(RTrim(TextBox22.Text)) & "' "
            strSQL = strSQL & "  , '" & LTrim(RTrim(TextBox23.Text)) & "' "
            strSQL = strSQL & "  , '" & LTrim(RTrim(TextBox24.Text)) & "' "
            strSQL = strSQL & "  , '" & LTrim(RTrim(TextBox25.Text)) & "' "
            strSQL = strSQL & "  , '" & LTrim(RTrim(TextBox26.Text)) & "' "
            strSQL = strSQL & "  , '" & LTrim(RTrim(TextBox27.Text)) & "' "
            strSQL = strSQL & "  , '" & LTrim(RTrim(TextBox28.Text)) & "' "
            strSQL = strSQL & "  , '" & LTrim(RTrim(TextBox29.Text)) & "' "
            strSQL = strSQL & "  , '" & LTrim(RTrim(TextBox30.Text)) & "' "
            strSQL = strSQL & "  , '" & LTrim(RTrim(TextBox31.Text)) & "' "
            strSQL = strSQL & "  , '" & LTrim(RTrim(TextBox32.Text)) & "' "
            strSQL = strSQL & "  , '" & LTrim(RTrim(TextBox33.Text)) & "' "
            strSQL = strSQL & "  , '" & LTrim(RTrim(TextBox34.Text)) & "' "
            strSQL = strSQL & "  , '" & LTrim(RTrim(TextBox56.Text)) & "' "
            strSQL = strSQL & "  , '" & LTrim(RTrim(TextBox35.Text)) & "' "
            strSQL = strSQL & "  , '" & LTrim(RTrim(TextBox36.Text)) & "' "
            strSQL = strSQL & "  , '" & LTrim(RTrim(TextBox37.Text)) & "' "
            strSQL = strSQL & "  , '" & LTrim(RTrim(TextBox38.Text)) & "' "
            strSQL = strSQL & "  , '" & LTrim(RTrim(TextBox39.Text)) & "' "
            strSQL = strSQL & "  , '" & LTrim(RTrim(TextBox40.Text)) & "' "
            strSQL = strSQL & "  , '" & DropDownList11.SelectedValue & "' "
            strSQL = strSQL & "  , '" & DropDownList12.SelectedValue & "' "
            strSQL = strSQL & "  , '" & LTrim(RTrim(TextBox41.Text)) & "' "
            strSQL = strSQL & "  , '" & LTrim(RTrim(TextBox42.Text)) & "' "
            strSQL = strSQL & "  , '" & LTrim(RTrim(TextBox43.Text)) & "' "
            strSQL = strSQL & "  , '" & LTrim(RTrim(TextBox44.Text)) & "' "
            strSQL = strSQL & "  , '" & LTrim(RTrim(TextBox45.Text)) & "' "
            strSQL = strSQL & "  , '" & LTrim(RTrim(TextBox46.Text)) & "' "
            strSQL = strSQL & "  , '" & LTrim(RTrim(TextBox47.Text)) & "' "
            strSQL = strSQL & "  , '" & LTrim(RTrim(TextBox48.Text)) & "' "
            strSQL = strSQL & "  , '" & LTrim(RTrim(TextBox49.Text)) & "' "
            strSQL = strSQL & "  , '" & LTrim(RTrim(TextBox50.Text)) & "' "
            strSQL = strSQL & "  , '" & LTrim(RTrim(TextBox51.Text)) & "' "
            strSQL = strSQL & "  , '" & LTrim(RTrim(TextBox52.Text)) & "' "
            strSQL = strSQL & "  , '" & LTrim(RTrim(TextBox53.Text)) & "' "
            strSQL = strSQL & "  , '" & LTrim(RTrim(TextBox54.Text)) & "' "
            strSQL = strSQL & "  , '" & DropDownList13.SelectedValue & "' "
            strSQL = strSQL & "  , '" & DropDownList14.SelectedValue & "' "
            strSQL = strSQL & "  , '" & DropDownList15.SelectedValue & "' "
            strSQL = strSQL & "  , '" & DropDownList16.SelectedValue & "' "
            strSQL = strSQL & ") "
        End If

        Command.CommandText = strSQL
        ' SQLの実行
        Command.ExecuteNonQuery()

        cnn.Close()

    End Sub

    Private Function chk_Nyuryoku() As Boolean
        chk_Nyuryoku = True

        '必須チェック
        If Trim(TextBox1.Text) = "" Then
            MsgBox("新コードは必須入力です。")
            chk_Nyuryoku = False
        End If

        '全角入力チェック
        If HankakuEisuChk(TextBox1.Text) = False And Trim(TextBox1.Text) <> "" Then
            MsgBox("新コードに全角文字が使用されています。")
            chk_Nyuryoku = False
        End If

        If Len(TextBox1.Text) <> 4 Then
            MsgBox("新コードは４桁で入力してください。")
            chk_Nyuryoku = False
        End If

    End Function

    Protected Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        '更新（登録）ボタンクリックイベント
        Dim dbcmd As SqlCommand
        Dim dataread As SqlDataReader
        Dim StrSQL As String = ""

        '入力チェック
        If chk_Nyuryoku() = False Then
            Return
        End If

        'パラメータ取得
        Dim strMode As String = Session("strMode")

        '登録時、存在チェック
        If strMode = "02" Then
            '接続文字列の作成
            Dim ConnectionString As String = String.Empty
            'SQL Server認証
            ConnectionString = "Data Source=KBHWPM02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
            'SqlConnectionクラスの新しいインスタンスを初期化
            Dim cnn = New SqlConnection(ConnectionString)
            Dim Command = cnn.CreateCommand

            'データベース接続を開く
            cnn.Open()

            StrSQL = ""
            StrSQL = StrSQL & "SELECT COUNT(*) AS RecCnt FROM T_EXL_CSMANUAL "
            StrSQL = StrSQL & "WHERE NEW_CODE = '" & Trim(TextBox1.Text) & "'"

            'ＳＱＬコマンド作成 
            dbcmd = New SqlCommand(StrSQL, cnn)
            'ＳＱＬ文実行 
            dataread = dbcmd.ExecuteReader()

            '結果を取り出す 
            Dim intCnt As Integer = 0
            While (dataread.Read())
                intCnt = dataread("RecCnt")
            End While

            'クローズ処理 
            dataread.Close()
            dbcmd.Dispose()
            cnn.Close()
            cnn.Dispose()

            If intCnt > 0 Then
                MsgBox("【" & Trim(TextBox1.Text) & "】は既に登録済みです。")
                Return
            End If
        End If

        '更新(登録)
        Call DB_access()

        '元の画面に戻る
        Response.Redirect("cs_manual.aspx")
    End Sub
End Class

