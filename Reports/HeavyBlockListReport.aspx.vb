Public Class HeavyBlockListReport
    Inherits System.Web.UI.Page
    Dim obj As New ObjClass

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim vesselname As String
        vesselname = Request.QueryString("vesname1")
        Dim grosstonnage As String = obj.GetOneValueFromQuery("select gross_tonnage from granite_vessel_master where vessel_name ='" + vesselname + "'")
        Dim entrydate As String = obj.ConvDtFrmt(Now, "yyyy-MM-dd")
        Me.vesselname.Text = vesselname
        Me.cranewt1.Text = grosstonnage
        Me.docdate.Text = obj.ConvDtFrmt(entrydate, "dd/MM/yyyy")
            Dim dd As New DataTable
        dd = obj.getdatatable("select a.bl_number,a.packing_marks,a.no_of_pcs,a.hatch_nbr,b.weightMT,b.blocknumber from granite_packinglistheader a," & _
            "granite_packinglistdetails b  where a.packing_vesselname= '" + vesselname + "'and b.weightMT>=28  group  by a.packing_marks,b.weightMT,a.bl_number, " & _
            "a.no_of_pcs,a.hatch_nbr,b.blocknumber")
            gvDetails.DataSource = dd
            gvDetails.DataBind()
            
    End Sub

End Class