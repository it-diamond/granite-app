
Imports System.Web.Services

Public Class DashBoard
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        asplbluser.Text = Session("username")
    End Sub
    Public Function GetUser() As String
        Return Session("username")
    End Function
    Sub clearsession()
        HttpContext.Current.Session.Clear()
    End Sub
End Class