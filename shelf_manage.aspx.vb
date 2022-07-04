
Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.Common
Partial Class yuusen
    Inherits System.Web.UI.Page

    Public strRow As String
    Public strProcess As String


    Private Sub form1_Load(sender As Object, e As EventArgs) Handles form1.Load

        Dim dataread As SqlDataReader
        Dim dbcmd As SqlCommand
        Dim strSQL As String = ""
        Dim strDate As String
        Dim strinv As String

        Dim strtext00(50) As String

        Dim strtext01(50) As String

        Dim strtext02(50) As String

        Dim cnt As Long = 0

        '接続文字列の作成
        Dim ConnectionString As String = String.Empty
        'SQL Server認証
        ConnectionString = "Data Source=KBHWPM02;Initial Catalog=EXPDB;User Id=sa;Password=expdb-manager"
        'SqlConnectionクラスの新しいインスタンスを初期化
        Dim cnn = New SqlConnection(ConnectionString)

        Dim dt1 As DateTime = DateTime.Now

        'データベース接続を開く
        cnn.Open()

        strSQL = ""
        strSQL = strSQL & "SELECT SPACE,BOXNO,BOXNO_EX "
        strSQL = strSQL & "FROM T_EXL_DOC_SHELF "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        strinv = ""
        '結果を取り出す 
        While (dataread.Read())
            strtext00(cnt) = Convert.ToString(dataread("SPACE"))
            strtext01(cnt) = Convert.ToString(dataread("BOXNO"))
            strtext02(cnt) = Convert.ToString(dataread("BOXNO_EX"))
            cnt = cnt + 1
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()

        cnt = 0

        Label158.Text = strtext00(cnt) 'A0001
        Label159.Text = strtext01(cnt)
        Label242.Text = strtext02(cnt)

        cnt = cnt + 1

        Label161.Text = strtext00(cnt) 'A0002
        Label162.Text = strtext01(cnt)
        Label243.Text = strtext02(cnt)

        cnt = cnt + 1

        Label164.Text = strtext00(cnt) 'A0003
        Label165.Text = strtext01(cnt)
        Label244.Text = strtext02(cnt)

        cnt = cnt + 17 'ここまでにB000を入れる

        Label2.Text = strtext00(cnt) 'C0001
        Label3.Text = strtext01(cnt)
        Label190.Text = strtext02(cnt)

        cnt = cnt + 1

        Label5.Text = strtext00(cnt)
        Label6.Text = strtext01(cnt)
        Label191.Text = strtext02(cnt)

        cnt = cnt + 1

        Label8.Text = strtext00(cnt)
        Label9.Text = strtext01(cnt)
        Label192.Text = strtext02(cnt)

        cnt = cnt + 1

        Label62.Text = strtext00(cnt)
        Label63.Text = strtext01(cnt)
        Label210.Text = strtext02(cnt)

        cnt = cnt + 1

        Label65.Text = strtext00(cnt)
        Label66.Text = strtext01(cnt)
        Label211.Text = strtext02(cnt)

        cnt = cnt + 1

        Label68.Text = strtext00(cnt)
        Label69.Text = strtext01(cnt)
        Label212.Text = strtext02(cnt)

        cnt = cnt + 1

        Label71.Text = strtext00(cnt)
        Label72.Text = strtext01(cnt)
        Label213.Text = strtext02(cnt)

        cnt = cnt + 1

        Label14.Text = strtext00(cnt)
        Label15.Text = strtext01(cnt)
        Label194.Text = strtext02(cnt)

        cnt = cnt + 1

        Label17.Text = strtext00(cnt)
        Label18.Text = strtext01(cnt)
        Label195.Text = strtext02(cnt)

        cnt = cnt + 1

        Label20.Text = strtext00(cnt)
        Label21.Text = strtext01(cnt)
        Label196.Text = strtext02(cnt)

        cnt = cnt + 1

        Label74.Text = strtext00(cnt) 'C0011
        Label75.Text = strtext01(cnt)
        Label214.Text = strtext02(cnt)

        cnt = cnt + 1

        Label80.Text = strtext00(cnt) 'C0012
        Label81.Text = strtext01(cnt)
        Label216.Text = strtext02(cnt)

        cnt = cnt + 1

        Label134.Text = strtext00(cnt)
        Label135.Text = strtext01(cnt)
        Label234.Text = strtext02(cnt)

        cnt = cnt + 1

        Label140.Text = strtext00(cnt) 'C0014
        Label141.Text = strtext01(cnt)
        Label236.Text = strtext02(cnt)

        cnt = cnt + 1

        Label146.Text = strtext00(cnt) 'C0001
        Label147.Text = strtext01(cnt)
        Label238.Text = strtext02(cnt)

        cnt = cnt + 1

        Label149.Text = strtext00(cnt)
        Label150.Text = strtext01(cnt)
        Label239.Text = strtext02(cnt)

        cnt = cnt + 1

        Label152.Text = strtext00(cnt)
        Label153.Text = strtext01(cnt)
        Label240.Text = strtext02(cnt)

        cnt = cnt + 1

        'Label155.Text = strtext00(cnt)
        'Label156.Text = strtext01(cnt)
        'Label241.Text = strtext02(cnt)

        cnt = cnt + 1

        Label86.Text = strtext00(cnt)
        Label87.Text = strtext01(cnt)
        Label218.Text = strtext02(cnt)

        cnt = cnt + 1

        Label11.Text = strtext00(cnt)
        Label12.Text = strtext01(cnt)
        Label22.Text = strtext02(cnt)

        cnt = cnt + 1

        'Label92.Text = strtext00(cnt)
        'Label93.Text = strtext01(cnt)
        'Label220.Text = strtext02(cnt)

        cnt = cnt + 1

        'Label26.Text = strtext00(cnt)
        'Label27.Text = strtext01(cnt)
        'Label198.Text = strtext02(cnt)

        cnt = cnt + 1

        Label29.Text = strtext00(cnt)
        Label30.Text = strtext01(cnt)
        Label199.Text = strtext02(cnt)

        cnt = cnt + 1

        'Label32.Text = strtext00(cnt)
        'Label33.Text = strtext01(cnt)
        'Label200.Text = strtext02(cnt)

        cnt = cnt + 1

        Label38.Text = strtext00(cnt) 'D0011
        Label39.Text = strtext01(cnt)
        Label202.Text = strtext02(cnt)

        cnt = cnt + 1

        Label41.Text = strtext00(cnt)
        Label42.Text = strtext01(cnt)
        Label203.Text = strtext02(cnt)

        cnt = cnt + 1

        Label44.Text = strtext00(cnt)
        Label45.Text = strtext01(cnt)
        Label204.Text = strtext02(cnt)

        cnt = cnt + 1

        'Label47.Text = strtext00(cnt)
        'Label48.Text = strtext01(cnt)
        'Label205.Text = strtext02(cnt)

        cnt = cnt + 1

        Label98.Text = strtext00(cnt)
        Label99.Text = strtext01(cnt)
        Label222.Text = strtext02(cnt)

        cnt = cnt + 1

        Label101.Text = strtext00(cnt)
        Label102.Text = strtext01(cnt)
        Label223.Text = strtext02(cnt)

        cnt = cnt + 1

        Label104.Text = strtext00(cnt)
        Label105.Text = strtext01(cnt)
        Label224.Text = strtext02(cnt)

        cnt = cnt + 1

        'Label107.Text = strtext00(cnt)
        'Label108.Text = strtext01(cnt)
        'Label225.Text = strtext02(cnt)



        Dim val01 As Long = 0
        Dim val02 As Long = 0
        Dim val03 As Long = 0



        strSQL = ""
        strSQL = strSQL & "SELECT SUM(SPACE) AS A01 "
        strSQL = strSQL & "FROM T_EXL_DOC_SHELF "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        strinv = ""
        '結果を取り出す 
        While (dataread.Read())
            Label23.Text = Convert.ToString(dataread("A01"))
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()


        strSQL = ""
        strSQL = strSQL & "SELECT SUM(BOXNO) AS A01 "
        strSQL = strSQL & "FROM T_EXL_DOC_SHELF "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        strinv = ""
        '結果を取り出す 
        While (dataread.Read())
            Label24.Text = Convert.ToString(dataread("A01"))
        End While

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()


        strSQL = ""
        strSQL = strSQL & "SELECT SUM(BOXNO_EX) AS A01 "
        strSQL = strSQL & "FROM T_EXL_DOC_SHELF "

        'ＳＱＬコマンド作成 
        dbcmd = New SqlCommand(strSQL, cnn)
        'ＳＱＬ文実行 
        dataread = dbcmd.ExecuteReader()

        strinv = ""
        '結果を取り出す 
        While (dataread.Read())
            Label34.Text = Convert.ToString(dataread("A01"))
        End While


        Label35.Text = Label23.Text - Label24.Text

        'クローズ処理 
        dataread.Close()
        dbcmd.Dispose()


        cnn.Close()
        cnn.Dispose()


    End Sub
End Class
