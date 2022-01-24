Imports Microsoft.VisualBasic


Public Class mod_function

    Public Shared Function HankakuEisuChk(strValue As String) As Boolean
        '半角英数チェック

        HankakuEisuChk = True

        '正規表現パターンを指定(英字a-z,A-Z,数値0-9)
        '        Dim r As New System.Text.RegularExpressions.Regex(“^[a-zA-Z0-9[ ]]+$”)
        Dim r As New System.Text.RegularExpressions.Regex(“^[a-zA-Z0-9\uFF61-\uFF9F\s]+$”)

        '半角英数字に一致しているかチェック
        If r.IsMatch(strValue) = False Then
            HankakuEisuChk = False
        End If
    End Function

    Public Shared Function Chk_Hiduke(strDate As String) As Boolean
        '引数が日付になっているかチェック
        Dim dt As DateTime
        'DateTimeに変換できるかチェック
        If DateTime.TryParse(strDate, dt) Then
            Chk_Hiduke = True
        Else
            Chk_Hiduke = False
        End If
    End Function


End Class
