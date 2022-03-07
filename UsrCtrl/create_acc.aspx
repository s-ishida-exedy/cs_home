<%@ Page Language="VB" AutoEventWireup="false" CodeFile="create_acc.aspx.vb" Inherits="create_acc" %>

<!DOCTYPE html>
<html lang="jp">
<head runat="server">
<title>ユーザー登録</title>
<meta charset="UTF-8">
<meta name="viewport" content="width=device-width, initial-scale=1.0">
<link rel="stylesheet" href="css/style.css">
<style type="text/css">    
    * {
      font-family: 'Maven Pro', sans-serif;
      box-sizing: border-box;
    }
    body,html {
      height: 100%;
      width: 100%;
      margin: 0;
      padding: 0;
      text-align: center;
      font-family: 'Segoe UI';
    }
    form {
      width: 40%;
      margin-left: 30%;
      padding-top: 1%;
    }
    input {
      width: 100%;
      background: transparent;
      border-bottom: solid 1px #387df8;
      border-top: none;
      border-left: none;
      border-right: none;
      font-size: 1rem;
      padding: 0.5em 0.4em;
      transition: all 0.4s;
      color: #BDBDBD;
      margin: 0.7rem 0;
    }
    input:focus {
      background: #ffffff;
      transform: scale3d(1.06,1.06,1.06);
    }
    input:required{background:#fbd1f6;}
    input:valid{background: transparent;} /* 入力内容が正しかった場合の指定 */
    .button {
      background: transparent;
      width: 50%;
      margin-top: 2.5rem;
      font-size: 1rem;
      border: solid 1px #387df8;
      padding: 1em 0;
      color: #5e4c4c;
      transition: all 0.6s;
    }
    .button:hover {
      cursor:pointer;
      background: #c1eafa;
    }
    h1 {
      color: #5e4c4c;
      border-bottom: solid 1px #387df8;
      padding: 0 0 0.8em 0;
      width: 50%;
      margin-left: 25%;
      margin-bottom: 1em;
    }
    @media (max-width: 550px) {
      form {
      width: 90%;
      margin-left: 3%;
      padding-top: 5%;
    }
      input {
        font-size: 1em;
      }
    }
</style>

</head>
<body>
<form id="form1" runat="server" autocomplete="off">
    <h1>ユーザー登録</h1>
    <asp:Label id="objLbl" runat="Server" ForeColor="Red" Text ="全て必須入力です。"/>
    <asp:TextBox ID="TextBox1" placeholder="社員番号" runat="server" required></asp:TextBox>
    <asp:TextBox ID="TextBox2" type="password" placeholder="Password（半角英数 8文字以上16文字以内）" runat="server" required></asp:TextBox>
    <asp:TextBox ID="TextBox3" type="password" placeholder="Password（再入力）" runat="server" required></asp:TextBox>
    <asp:TextBox ID="TextBox4" placeholder="名前" runat="server" required></asp:TextBox>
    <asp:TextBox ID="TextBox5" type="email" placeholder="メールアドレス" runat="server" required></asp:TextBox>
    <asp:Button ID="Button1" class="button" runat="server" Text="登　　録" /><br />
    <br />
    <a href="../login.aspx">ログイン画面へ戻る</a>
</form>
</body>
</html>