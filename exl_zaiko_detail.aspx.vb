Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.Common
Imports System.IO
Imports ClosedXML.Excel

Imports System.Linq


Partial Class cs_home
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If IsPostBack = True Then
        Else
            Dim strPlace As String = Request.QueryString("place").ToString
            Dim strShubetsu As String = Request.QueryString("shubetsu").ToString

            Dim Dataobj As New DBAccess

            'データの取得
            Dim ds As DataSet = Dataobj.GET_RESULT_ZAIKO(strPlace, strShubetsu)
            If ds.Tables.Count > 0 Then
                GridView1.DataSourceID = ""
                GridView1.DataSource = ds
                GridView1.DataBind()
            End If

        End If


    End Sub


End Class
