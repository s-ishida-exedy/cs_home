<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default2.aspx.vb" Inherits="Default2" %>

<!DOCTYPE html>

<html>
<head>
<title>暗号化（MD5/SHA1）</title>
</head>
<body>
<form runat="server">
<h1>暗号化（MD5/SHA1）</h1>
<hr />
<asp:TextBox id="txtPass" runat="server" />
<asp:Button id="btnOk" runat="server" Text="暗号化" />
<p />
<table bgcolor="#ffffc0" border="1">
<tr>
  <th>MD5</th>
  <td><asp:Literal id="ltrMd5" runat="server" /><br /></td>
</tr><tr>
  <th>SHA1</th>
  <td><asp:Literal id="ltrSha1" runat="server" /><br /></td>
</tr>
</table>
</form>
</body>
</html>
