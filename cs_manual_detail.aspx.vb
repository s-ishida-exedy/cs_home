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

        Label3.Text = ""
        Label3.ForeColor = Drawing.Color.Red
        Label3.Font.Bold = True

        If IsPostBack Then
            ' そうでない時処理
        Else
            'パラメータ取得
            Dim strCust As String = Session("strCust")
            Dim strMode As String = Session("strMode")

            '接続文字列の作成
            Dim ConnectionString As String = String.Empty
            'SQL Server認証
            ConnectionString = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
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

                    DropDownList100.Items.Clear()
                    DropDownList100.DataSource = SqlDataSource1
                    DropDownList100.DataTextField = "NEW_CODE"
                    DropDownList100.DataValueField = "NEW_CODE"
                    DropDownList100.DataBind()

                    DropDownList100.Items.Insert(0, "")
                    DropDownList100.SelectedValue = strCust
                    'DropDownList100.Enabled = False

                    DropDownList200.Visible = False
                    CheckBox100.Visible = False
                    Button100.Visible = False
                    Label4.Visible = False
                Else



                End If
                TextBox1.Text = dataread("SALES_STAFF").ToString & " " & dataread("SALES_STAFF1").ToString
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

                DropDownList17.SelectedValue = dataread("BEARING").ToString
                DropDownList18.SelectedValue = dataread("IV_AUTO_CALC").ToString

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


            If strMode = "03" Then

                strSQL = ""
                strSQL = strSQL & "SELECT * FROM T_EXL_CSMANUAL_ADDCUST "
                strSQL = strSQL & "WHERE CUSTCODE = '" & strCust & "'"

                'ＳＱＬコマンド作成 
                dbcmd = New SqlCommand(strSQL, cnn)
                'ＳＱＬ文実行 
                dataread = dbcmd.ExecuteReader()

                '結果を取り出す 
                While (dataread.Read())

                    DropDownList100.Text = dataread("CUSTCODE").ToString
                    TextBox3.Text = dataread("CUST_NAME").ToString
                    TextBox4.Text = dataread("CUST_SNAME").ToString
                    TextBox8.Text = dataread("CUST_NAME_ADDRESS").ToString
                    TextBox55.Text = dataread("CNEE_NAME_ADDRESS").ToString
                End While



            End If


            If strMode = "02" Then

                TextBox2.Text = ""
                TextBox3.Text = ""
                TextBox4.Text = ""
                TextBox5.Text = ""
                TextBox6.Text = ""
                TextBox7.Text = ""
                TextBox8.Text = ""
                TextBox55.Text = ""
                TextBox9.Text = ""
                TextBox10.Text = ""
                TextBox11.Text = ""
                TextBox12.Text = ""
                TextBox13.Text = ""
                TextBox14.Text = ""
                TextBox15.Text = ""
                TextBox16.Text = ""
                TextBox17.Text = ""
                TextBox18.Text = ""
                TextBox19.Text = ""
                TextBox20.Text = ""
                TextBox21.Text = ""
                TextBox22.Text = ""
                TextBox23.Text = ""
                TextBox24.Text = ""
                TextBox25.Text = ""
                TextBox26.Text = ""
                TextBox27.Text = ""
                TextBox28.Text = ""
                TextBox29.Text = ""
                TextBox30.Text = ""
                TextBox31.Text = ""
                TextBox32.Text = ""
                TextBox33.Text = ""
                TextBox34.Text = ""
                TextBox56.Text = ""
                TextBox35.Text = ""
                TextBox36.Text = ""
                TextBox37.Text = ""
                TextBox38.Text = ""
                TextBox39.Text = ""
                TextBox40.Text = ""
                TextBox41.Text = ""
                TextBox42.Text = ""
                TextBox43.Text = ""
                TextBox44.Text = ""
                TextBox47.Text = ""
                TextBox48.Text = ""
                TextBox49.Text = ""
                TextBox50.Text = ""
                TextBox51.Text = ""
                TextBox52.Text = ""
                TextBox53.Text = ""
                TextBox54.Text = ""

                DropDownList4.SelectedValue = "×"
                DropDownList5.SelectedValue = "×"
                DropDownList6.SelectedValue = "×"
                DropDownList9.SelectedValue = "×"
                DropDownList10.SelectedValue = "×"
                DropDownList11.SelectedValue = "×"
                DropDownList13.SelectedValue = "×"
                DropDownList14.SelectedValue = "×"
                DropDownList15.SelectedValue = "×"
                DropDownList16.SelectedValue = "×"
                DropDownList17.SelectedValue = "無し"
                DropDownList18.SelectedValue = "無し"

                '登録モード
                DropDownList100.Items.Clear()
                DropDownList100.DataSource = SqlDataSource2
                DropDownList100.DataTextField = "CUSTCODE"
                DropDownList100.DataValueField = "CUSTCODE"
                DropDownList100.DataBind()

                DropDownList100.Items.Insert(0, "")
                DropDownList100.SelectedValue = ""

            End If

            'クローズ処理 
            dataread.Close()
            dbcmd.Dispose()
            cnn.Close()
            cnn.Dispose()

            '登録モード
            DropDownList200.Items.Clear()
            DropDownList200.DataSource = SqlDataSource1
            DropDownList200.DataTextField = "NEW_CODE"
            DropDownList200.DataValueField = "NEW_CODE"
            DropDownList200.DataBind()
            DropDownList200.Items.Insert(0, "")
            DropDownList200.SelectedValue = ""


            'モードによりボタン名称を変更する
            If strMode = "01" Then
                Button7.Text = "更　　新"
                Button7.Attributes.Add("onclick", "return confirm('更新します。よろしいですか？');")
            ElseIf strMode = "02" Then
                Button7.Text = "登　　録"
                Button7.Attributes.Add("onclick", "return confirm('登録します。よろしいですか？');")
            End If

            Session("strCust") = DropDownList100.SelectedValue

        End If

    End Sub

    Private Sub CheckBox100_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox100.CheckedChanged

        If CheckBox100.Checked = True Then
            DropDownList200.AutoPostBack() = True
        Else
            DropDownList200.AutoPostBack() = False
        End If

    End Sub

    Protected Sub DropDownList200_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList200.SelectedIndexChanged


        Dim strSQL As String = ""
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strValue As String = ""
        Dim strVal As String = ""

        'パラメータ取得
        Dim strCust As String = DropDownList200.SelectedValue

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)



        If CheckBox100.Checked = True Then



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
                DropDownList100.SelectedValue = ""
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

                DropDownList17.SelectedValue = dataread("BEARING").ToString
                DropDownList18.SelectedValue = dataread("IV_AUTO_CALC").ToString

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

            TextBox3.ForeColor = Drawing.Color.Black
            TextBox4.ForeColor = Drawing.Color.Black
            TextBox8.ForeColor = Drawing.Color.Black
            TextBox55.ForeColor = Drawing.Color.Black


        Else

            Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "確認", "<script language='JavaScript'>confirm('チェックを入れてから選択してください。');</script>", False)


        End If


    End Sub

    Protected Sub DropDownList100_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DropDownList100.SelectedIndexChanged


        Dim strSQL As String = ""
        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strValue As String = ""
        Dim strVal As String = ""

        'パラメータ取得
        Dim strCust As String = DropDownList100.SelectedValue

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        'データベース接続を開く
        cnn.Open()

        Dim strMode As String = Session("strMode")

        If strMode = "01" Then


            strSQL = ""
            strSQL = strSQL & "SELECT * FROM T_EXL_CSMANUAL "
            strSQL = strSQL & "WHERE NEW_CODE = '" & strCust & "'"

            'ＳＱＬコマンド作成 
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行 
            dataread = dbcmd.ExecuteReader()

            '結果を取り出す 
            While (dataread.Read())
                'DropDownList100.SelectedValue = ""
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

                DropDownList17.SelectedValue = dataread("BEARING").ToString
                DropDownList18.SelectedValue = dataread("IV_AUTO_CALC").ToString

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

            TextBox3.ForeColor = Drawing.Color.Black
            TextBox4.ForeColor = Drawing.Color.Black
            TextBox8.ForeColor = Drawing.Color.Black
            TextBox55.ForeColor = Drawing.Color.Black

        Else


            strSQL = ""
            strSQL = strSQL & "SELECT * FROM T_EXL_CSMANUAL_ADDCUST "
            strSQL = strSQL & "WHERE CUSTCODE = '" & strCust & "'"

            'ＳＱＬコマンド作成 
            dbcmd = New SqlCommand(strSQL, cnn)
            'ＳＱＬ文実行 
            dataread = dbcmd.ExecuteReader()

            '結果を取り出す 
            While (dataread.Read())

                DropDownList100.Text = dataread("CUSTCODE").ToString
                TextBox3.Text = dataread("CUST_NAME").ToString
                TextBox4.Text = dataread("CUST_SNAME").ToString
                TextBox8.Text = dataread("CUST_NAME_ADDRESS").ToString
                TextBox55.Text = dataread("CNEE_NAME_ADDRESS").ToString

                TextBox3.ReadOnly = True
                TextBox4.ReadOnly = True
                TextBox8.ReadOnly = True
                TextBox55.ReadOnly = True
                TextBox3.ForeColor = Drawing.Color.Red
                TextBox4.ForeColor = Drawing.Color.Red
                TextBox8.ForeColor = Drawing.Color.Red
                TextBox55.ForeColor = Drawing.Color.Red


            End While



            'クローズ処理 
            dataread.Close()
            dbcmd.Dispose()

        End If

        cnn.Close()
        cnn.Dispose()


    End Sub
    Private Sub DB_access()
        '画面入力情報をテーブルへ登録
        Dim strSQL As String
        Dim strVal As String = ""
        Dim dt1 As DateTime = DateTime.Now
        'パラメータ取得
        Dim strCust As String = Session("strCust")
        Dim strMode As String = Session("strMode")

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
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
            strSQL = strSQL & "  , SALES_STAFF =  '" & Session("UsrId") & "' "
            strSQL = strSQL & "  , SALES_STAFF1 =  '" & dt1.ToString("yyyy/MM/dd") & "' "
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
            strSQL = strSQL & "  , BEARING =  '" & DropDownList17.SelectedValue & "' "
            strSQL = strSQL & "  , IV_AUTO_CALC =  '" & DropDownList18.SelectedValue & "' "
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
            strSQL = strSQL & "  NEW_CODE =  '" & LTrim(RTrim(DropDownList100.Text)) & "' "
        Else
            strSQL = ""
            strSQL = strSQL & "INSERT INTO T_EXL_CSMANUAL "
            strSQL = strSQL & "VALUES ( "
            strSQL = strSQL & "    '" & LTrim(RTrim(DropDownList100.Text)) & "' "
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
            strSQL = strSQL & "  , '" & Session("UsrId") & "' "
            strSQL = strSQL & "  , '" & dt1.ToString("yyyy/MM/dd") & "' "
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
            strSQL = strSQL & "  , '" & DropDownList17.SelectedValue & "' "
            strSQL = strSQL & "  , '" & DropDownList18.SelectedValue & "' "
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
        cnn.Dispose()

    End Sub

    Private Function chk_Nyuryoku() As Boolean
        chk_Nyuryoku = True

        '必須チェック
        If Trim(DropDownList100.Text) = "" Then
            Label3.Text = "新コードは必須入力です。"
            Page.ClientScript.RegisterClientScriptBlock(Me.GetType, "確認", "<script language='JavaScript'>confirm('新コードは必須入力です。');</script>", False)

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
            ConnectionString = "Data Source=k3hwpm02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
            'SqlConnectionクラスの新しいインスタンスを初期化
            Dim cnn = New SqlConnection(ConnectionString)
            Dim Command = cnn.CreateCommand

            'データベース接続を開く
            cnn.Open()

            StrSQL = ""
            StrSQL = StrSQL & "SELECT COUNT(*) AS RecCnt FROM T_EXL_CSMANUAL "
            StrSQL = StrSQL & "WHERE NEW_CODE = '" & Trim(DropDownList100.Text) & "'"

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
                MsgBox("【" & Trim(DropDownList100.Text) & "】は既に登録済みです。")
                Return
            End If
        End If

        '更新(登録)
        Call DB_access()

        '元の画面に戻る
        Response.Redirect("cs_manual.aspx")
    End Sub

    Private Sub Button100_Click(sender As Object, e As EventArgs) Handles Button100.Click


        TextBox2.Text = ""
        TextBox3.Text = ""
        TextBox4.Text = ""
        TextBox5.Text = ""
        TextBox6.Text = ""
        TextBox7.Text = ""
        TextBox8.Text = ""
        TextBox55.Text = ""
        TextBox9.Text = ""
        TextBox10.Text = ""
        TextBox11.Text = ""
        TextBox12.Text = ""
        TextBox13.Text = ""
        TextBox14.Text = ""
        TextBox15.Text = ""
        TextBox16.Text = ""
        TextBox17.Text = ""
        TextBox18.Text = ""
        TextBox19.Text = ""
        TextBox20.Text = ""
        TextBox21.Text = ""
        TextBox22.Text = ""
        TextBox23.Text = ""
        TextBox24.Text = ""
        TextBox25.Text = ""
        TextBox26.Text = ""
        TextBox27.Text = ""
        TextBox28.Text = ""
        TextBox29.Text = ""
        TextBox30.Text = ""
        TextBox31.Text = ""
        TextBox32.Text = ""
        TextBox33.Text = ""
        TextBox34.Text = ""
        TextBox56.Text = ""
        TextBox35.Text = ""
        TextBox36.Text = ""
        TextBox37.Text = ""
        TextBox38.Text = ""
        TextBox39.Text = ""
        TextBox40.Text = ""
        TextBox41.Text = ""
        TextBox42.Text = ""
        TextBox43.Text = ""
        TextBox44.Text = ""
        TextBox47.Text = ""
        TextBox48.Text = ""
        TextBox49.Text = ""
        TextBox50.Text = ""
        TextBox51.Text = ""
        TextBox52.Text = ""
        TextBox53.Text = ""
        TextBox54.Text = ""

        DropDownList4.SelectedValue = "×"
        DropDownList5.SelectedValue = "×"
        DropDownList6.SelectedValue = "×"
        DropDownList9.SelectedValue = "×"
        DropDownList10.SelectedValue = "×"
        DropDownList11.SelectedValue = "×"
        DropDownList13.SelectedValue = "×"
        DropDownList14.SelectedValue = "×"
        DropDownList15.SelectedValue = "×"
        DropDownList16.SelectedValue = "×"
        DropDownList17.SelectedValue = "無し"
        DropDownList18.SelectedValue = "無し"

        '登録モード
        DropDownList200.Items.Clear()
        DropDownList200.DataSource = SqlDataSource1
        DropDownList200.DataTextField = "NEW_CODE"
        DropDownList200.DataValueField = "NEW_CODE"
        DropDownList200.DataBind()
        DropDownList200.Items.Insert(0, "")
        DropDownList200.SelectedValue = ""

        CheckBox100.Checked = False

        '登録モード
        DropDownList100.Items.Clear()
        DropDownList100.DataSource = SqlDataSource2
        DropDownList100.DataTextField = "CUSTCODE"
        DropDownList100.DataValueField = "CUSTCODE"
        DropDownList100.DataBind()

        DropDownList100.Items.Insert(0, "")
        DropDownList100.SelectedValue = ""

        TextBox3.ForeColor = Drawing.Color.Black
        TextBox4.ForeColor = Drawing.Color.Black
        TextBox8.ForeColor = Drawing.Color.Black
        TextBox55.ForeColor = Drawing.Color.Black


    End Sub
End Class

