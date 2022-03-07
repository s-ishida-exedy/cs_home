Imports System.Data
Imports System.Data.SqlClient
Imports System.Console
Imports System.IO
Imports System.Security.Cryptography
Imports System.Text
Imports System.Threading.Tasks

Partial Class Default2
    Inherits System.Web.UI.Page

    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        ltrMd5.Text =
   FormsAuthentication.HashPasswordForStoringInConfigFile(txtPass.Text, "MD5")
        '     ltrSha1.Text =
        'FormsAuthentication.HashPasswordForStoringInConfigFile(txtPass.Text, "SHA1")


        ltrSha1.Text = CryptoSample(txtPass.Text)


    End Sub

    Private Function CryptoSample(plainText As String) As String
        ' KeyとIV（一例）
        Dim key() As Byte _
    = {
        &HED, &HB, &H56, &HAF, &H61, &HA2, &H71, &H39,
        &HE0, &H4B, &HDC, &HC9, &H23, &H69, &H8C, &HBD,
        &HB9, &H86, &H98, &H28, &HC8, &H3E, &H62, &HA7,
        &HFA, &H17, &HC1, &H33, &H64, &HBF, &H96, &H24
      }
        Dim iv() As Byte _
    = {
        &H6F, &HDF, &H98, &H0, &H67, &H36, &H7D, &H3B,
        &HFF, &HC9, &H3B, &H79, &H4D, &HD4, &H81, &H72
      }

        ' 暗号化
        Dim encrypted As String = EncryptToBase64(plainText, key, iv)

        Return encrypted
    End Function


    ' 入力文字列をAES暗号化してBase64形式で返すメソッド
    Private Function EncryptToBase64(plainText As String, key As Byte(), iv As Byte()) As String
        ' 入力文字列をバイト型配列に変換
        Dim src() As Byte = Encoding.Unicode.GetBytes(plainText)
        ' 出力例：平文のバイト型配列の長さ=60

        ' Encryptor（暗号化器）を用意する
        Using am = New AesManaged()
            Using encryptor = am.CreateEncryptor(key, iv)
                ' ファイルを入力とするなら、ここでファイルを開く
                'Using inStream = New FileStream(FilePath, ……省略……
                ' 出力ストリームを用意する
                Using outStream = New MemoryStream()
                    ' 暗号化して書き出す
                    Using cs = New CryptoStream(outStream, encryptor, CryptoStreamMode.Write)
                        cs.WriteAsync(src, 0, src.Length)
                        ' 入力がファイルなら、inStreamから一定量ずつバイトバッファーに読み込んで
                        ' cse.Writeで書き込む処理を繰り返す（復号のサンプルコードを参照）
                    End Using
                    ' 出力がファイルなら、以上で完了

                    ' Base64文字列に変換して返す
                    Dim result() As Byte = outStream.ToArray()
                    ' 出力例：暗号のバイト型配列の長さ=64
                    ' 出力サイズはBlockSize（既定値16バイト）の倍数になる
                    Return Convert.ToBase64String(result)
                End Using
            End Using
        End Using
    End Function
End Class
