<%@ Page Language="VB" AutoEventWireup="false" CodeFile="login.aspx.vb" Inherits="Default2" %>

<html>
<head>
    <title>ログイン</title>
    <script src="https://code.jquery.com/jquery-2.1.0.min.js" ></script>
    <style type="text/css">
    /* Fonts */
    @import url(https://fonts.googleapis.com/css?family=Open+Sans:400);

    /* fontawesome */
    @import url(http://weloveiconfonts.com/api/?family=fontawesome);
    [class*="fontawesome-"]:before {
      font-family: 'FontAwesome', sans-serif;
    }

    /* Simple Reset */
    * { margin: 0; padding: 0; box-sizing: border-box; }

    /* body */
    body {
      background: #e9e9e9;
      color: #5e5e5e;
      font: 400 87.5%/1.5em 'Open Sans', sans-serif;
    }

    /* Form Layout */
    .form-wrapper {
      background: #fafafa;
      margin: 3em auto;
      padding: 0 1em;
      max-width: 370px;
    }

    h1 {
      text-align: center;
      padding: 1em 0;
    }

    form {
      padding: 0 1.5em;
    }

    .form-item {
      margin-bottom: 0.75em;
      width: 100%;
    }

    .form-item input {
      background: #fafafa;
      border: none;
      border-bottom: 2px solid #e9e9e9;
      color: #666;
      font-family: 'Open Sans', sans-serif;
      font-size: 1em;
      height: 50px;
      transition: border-color 0.3s;
      width: 100%;
    }

    .form-item input:focus {
      border-bottom: 2px solid #c0c0c0;
      outline: none;
    }

    .button-panel {
      margin: 2em 0 0;
      width: 100%;
    }

    .button-panel .button {
      background: #f16272;
      border: none;
      color: #fff;
      cursor: pointer;
      height: 50px;
      font-family: 'Open Sans', sans-serif;
      font-size: 1.2em;
      letter-spacing: 0.05em;
      text-align: center;
      text-transform: uppercase;
      transition: background 0.3s ease-in-out;
      width: 100%;
    }

    .button:hover {
      background: #ee3e52;
    }

    .form-footer {
      font-size: 1em;
      padding: 2em 0;
      text-align: center;
    }

    .form-footer a {
      color: #8c8c8c;
      text-decoration: none;
      transition: border-color 0.3s;
    }

    .form-footer a:hover {
      border-bottom: 1px dotted #8c8c8c;
    }
    </style>
</head>
<body>
<form runat="Server" class="form">

<div class="form-wrapper">
  <h1>Sign In</h1>
    <div class="form-item">
      <label for="email"></label>
      <asp:TextBox id="txtUsr" runat="Server" Columns="12" placeholder="User ID" />
    </div>
    <div class="form-item">
      <label for="password"></label>
      <asp:TextBox id="txtPass" runat="Server" Columns="11" TextMode="Password" placeholder="Password" />
    </div>
    <div class="button-panel">
       <asp:Button id="objBtn" class="button" runat="Server" Text="SIGN IN" /><br />
       <asp:Label id="objLbl" runat="Server" ForeColor="Red" />
    </div>
  <div class="form-footer">
    <p><a href="../UsrCtrl/create_acc.aspx">ユーザー登録</a></p>
      <br/  >
    <p><a href="../UsrCtrl/re_pass.aspx">パスワードの再設定</a></p>
  </div>
</div>
</form>
</body>
</html>






<%--<html>
<head>
<title>フォーム認証ログイン</title>
</head>
<body>
<form runat="Server">
<h1>ログイン</h1>
<hr />
<b>ユーザーID：</b>
<asp:TextBox id="txtUsr" runat="Server" Columns="12" /><br />
<b>パスワード：</b>
<asp:TextBox id="txtPass" runat="Server" Columns="11" TextMode="Password" />
<br />
<asp:Button id="objBtn" runat="Server" Text="ログイン" /><br />
<asp:Label id="objLbl" runat="Server" ForeColor="Red" />
</form>
</body>
</html>--%>


