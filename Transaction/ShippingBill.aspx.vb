Imports System.Data.SqlClient
Imports System.Web.Services
Imports System.Configuration
Imports System.Data

Public Class ShippingBill

    Inherits System.Web.UI.Page
    Dim obj As New ObjClass
    Dim message, sb_no, sb_date, bl_number, bl_date, tot_bl_wt, freight, shippingid, sql As String
    'Dim connection As New SqlConnection("Data Source=IISSERVER\SQLEXPRESS;Initial Catalog=gdn;User ID=godown;Password=dsagdn@1")

    <WebMethod()>
    Public Shared Function Getvessel(prefix As String) As String()
        Dim Liner As New List(Of String)()
        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("MyDbConn").ConnectionString
            Using cmd As New SqlCommand()
                'cmd.CommandText = "SELECT vessel_name FROM granite_vessel_master where vessel_name LIKE '%'+@pre+'%'"
                cmd.CommandText = "SELECT packing_vesselname FROM granite_packinglistheader where packing_vesselname LIKE '%'+@pre+'%' and job_completion_flag=0"
                cmd.Parameters.AddWithValue("@pre", prefix)
                cmd.Connection = conn
                conn.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        Liner.Add(String.Format("{0}-{1}", sdr("packing_vesselname"), ""))
                    End While
                End Using
                conn.Close()
            End Using
        End Using
        Return Liner.ToArray()
    End Function
    <WebMethod()>
    Public Shared Function Getport(prefix As String) As String()
        Dim Liner As New List(Of String)()
        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("MyDbConn").ConnectionString
            Using cmd As New SqlCommand()
                cmd.CommandText = "SELECT port_name FROM granite_portdetails_master where port_name LIKE '%'+@pre+'%'"
                cmd.Parameters.AddWithValue("@pre", prefix)
                cmd.Connection = conn
                conn.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        Liner.Add(String.Format("{0}-{1}", sdr("port_name"), ""))
                    End While
                End Using
                conn.Close()
            End Using
        End Using
        Return Liner.ToArray()
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'BindGridView()

        End If
        asplbluser.Text = Session("username")
    End Sub
    Sub BindGridView()

        Dim sql As String = "select a.Refno,a.blocknumber,a.volumeofCBT,a.weightMT,b.packing_listno,b.packing_description,b.packing_shippername,b.packing_consigneename,b.packing_marks  from granite_packinglistdetails a,granite_packinglistheader b where a.Refno =b.Refno  and b.packing_list_type='V'"
        'Dim sql As String = "select a.Refno,a.blocknumber,a.volumeofCBT,a.weightMT,b.packing_listno,b.packing_description,b.packing_shippername,b.packing_consigneename,b.packing_marks  from granite_packinglistdetails a,granite_packinglistheader b where a.Refno =b.Refno  "
        Dim ds As New DataSet()
        ds = obj.getdataset(Sql)
        gvDetails.DataSource = ds
        gvDetails.DataBind()
    End Sub


    
  
    Private Sub gvDetails_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvDetails.RowCommand
        Dim shippingid As String
        shippingid = e.CommandArgument

        Select e.CommandName
            Case Is = "Edit"

                Dim strcon As String = ConfigurationManager.ConnectionStrings("MydbConn").ConnectionString
                Dim sql As String
                sql = "select sb_no,format(sb_date,'dd/MM/yyyy')as sb_date,bl_number,format(bl_date,'dd/MM/yyyy')as bl_date,tot_bl_wt,freight from granite_packinglistheader where Refno='" + shippingid + "'"
                Dim getdata As New List(Of String)
                getdata = obj.GetMoreValueFromQuery(sql, 6)
                Me.shpblno.Text = getdata.Item(0)
                Me.shpdate.Text = getdata.Item(1)
                Me.blno.Text = getdata.Item(2)
                Me.bldate.Text = getdata.Item(3)
                Me.totalblwt.Text = getdata.Item(4)
                Me.freight2.Text = getdata.Item(5)
                Me.hdid.Value = shippingid
        End Select

    End Sub

    

    Protected Sub save_Click(sender As Object, e As EventArgs) Handles save.Click
        Dim successid As Integer
        sb_no = Me.shpblno.Text
        sb_date = obj.ConvDtFrmt(Me.shpdate.Text, "yyyy-MM-dd")
        bl_date = obj.ConvDtFrmt(Me.bldate.Text, "yyyy-MM-dd")
        bl_number = Me.blno.Text
        tot_bl_wt = Me.totalblwt.Text
        freight = Me.freight2.Text
        message = ""
        Select Case Me.save.Text
            Case Is = "Update"

                shippingid = Me.hdid.Value
                sql = "update granite_packinglistheader set sb_no ='" + sb_no + "' ,sb_date='" + sb_date + "', bl_date ='" + bl_date + "' ,bl_number='" + bl_number +
                    "', tot_bl_wt ='" + tot_bl_wt + "',freight  ='" + freight + "' where Refno='" + shippingid + "'"
                successid = obj.QueryExecution(Sql)
                If (successid) Then
                    message = "Record Updated Successfully"
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('something went wrong')", True)
                    Exit Sub

                End If
        End Select

        Dim url As String = "ShippingBill.aspx"
        Dim script As String = "window.onload = function(){ alert('"
        script += message
        script += "');"
        script += "window.location = '"
        script += url
        script += "'; }"
        ClientScript.RegisterStartupScript(Me.GetType(), "Redirect", script, True)
        'shippingdsi.SelectCommand = "select sb_no,format( sb_date,'dd/MM/yyyy')as sb_date,bl_number,format( bl_date,'dd/MM/yyyy')as bl_date,tot_bl_wt,freight from granite_packinglistheader"
    End Sub

    Private Sub gvDetails_RowEditing(sender As Object, e As System.Web.UI.WebControls.GridViewEditEventArgs) Handles gvDetails.RowEditing

    End Sub

    Private Sub showdetails_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles showdetails.Click
        Dim constr As String = ConfigurationManager.ConnectionStrings("MydbConn").ConnectionString
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand()
                'cmd.CommandText = "select b.Refno,count(a.blocknumber)as blocknumber,sum(convert(float,a.volumeofCBT))as volumeofCBT,sum(convert(float,a.weightMT))as weightMT,b.packing_listno,b.packing_description,b.packing_shippername, b.packing_consigneename, b.packing_marks from granite_packinglistdetails a,granite_packinglistheader b where a.Refno =b.Refno and packing_vesselname='" & vessname.Text & "' and packing_portname='" & portname.Text & "'  and b.packing_list_type='V' Group by b.Refno,b.packing_listno,b.packing_description,b.packing_shippername,b.packing_consigneename,b.packing_marks"
                cmd.CommandText = "select b.Refno,count(a.blocknumber)as blocknumber,sum(convert(float,a.volumeofCBT))as volumeofCBT,sum(convert(float,a.weightMT))as weightMT,b.packing_listno,b.packing_description,b.packing_shippername, b.packing_consigneename, b.packing_marks from granite_packinglistdetails a,granite_packinglistheader b where a.Refno =b.Refno and packing_vesselname='" & vessname.Text & "' and packing_portname='" & portname.Text & "' Group by b.Refno,b.packing_listno,b.packing_description,b.packing_shippername,b.packing_consigneename,b.packing_marks"
                cmd.Connection = con
                Dim dt As New DataTable()
                Using sda As New SqlDataAdapter(cmd)
                    sda.Fill(dt)
                    gvDetails.DataSource = dt
                    gvDetails.DataBind()
                End Using
            End Using
        End Using
    End Sub

    Private Sub exit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles [exit].Click


    End Sub
End Class