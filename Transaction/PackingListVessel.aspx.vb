Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Web.Services

Public Class PackingList
    Inherits System.Web.UI.Page
    Dim obj As New ObjClass


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        asplbluser.Text = Session("username")
    End Sub
    <WebMethod()>
    Public Shared Function Getcnfparty(prefix As String) As String()
        Dim Liner As New List(Of String)()
        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("MyDbConn").ConnectionString
            Using cmd As New SqlCommand()
                cmd.CommandText = "SELECT cnf_name FROM granite_cnfdetails_master where cnf_name LIKE '%'+@pre+'%'"
                cmd.Parameters.AddWithValue("@pre", prefix)
                cmd.Connection = conn
                conn.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        Liner.Add(String.Format("{0}{1}", sdr("cnf_name"), ""))
                    End While
                End Using
                conn.Close()
            End Using
        End Using
        Return Liner.ToArray()
    End Function
    <WebMethod()>
    Public Shared Function Getvessel(prefix As String) As String()
        Dim Liner As New List(Of String)()
        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("MyDbConn").ConnectionString
            Using cmd As New SqlCommand()
                cmd.CommandText = "SELECT vessel_name FROM granite_vessel_master where vessel_name LIKE '%'+@pre+'%' and job_completion_flag=0"
                cmd.Parameters.AddWithValue("@pre", prefix)
                cmd.Connection = conn
                conn.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        Liner.Add(String.Format("{0}-{1}", sdr("vessel_name"), ""))
                    End While
                End Using
                conn.Close()
            End Using
        End Using
        Return Liner.ToArray()
    End Function
    <WebMethod()>
    Public Shared Function Getconsignee(prefix As String) As String()
        Dim Liner As New List(Of String)()
        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("MyDbConn").ConnectionString
            Using cmd As New SqlCommand()
                cmd.CommandText = "SELECT consignee_name FROM granite_consigneedetails_master where consignee_name LIKE '%'+@pre+'%'"
                cmd.Parameters.AddWithValue("@pre", prefix)
                cmd.Connection = conn
                conn.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        Liner.Add(String.Format("{0}-{1}", sdr("consignee_name"), ""))
                    End While
                End Using
                conn.Close()
            End Using
        End Using
        Return Liner.ToArray()
    End Function
    <WebMethod()>
    Public Shared Function Getshipper(prefix As String) As String()
        Dim Liner As New List(Of String)()
        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("MyDbConn").ConnectionString
            Using cmd As New SqlCommand()
                cmd.CommandText = "SELECT shipper_name FROM granite_shipper_master where shipper_name LIKE '%'+@pre+'%'"
                cmd.Parameters.AddWithValue("@pre", prefix)
                cmd.Connection = conn
                conn.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        Liner.Add(String.Format("{0}-{1}", sdr("shipper_name"), ""))
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
    Protected Sub save_Click(sender As Object, e As EventArgs) Handles save.Click
        Dim Refno, packing_date, packing_vesselname, packing_listno, packing_shippername, packingid,
        packing_consigneename, packing_marks, packing_cnfagent, packing_portname, packing_description, blocknumber, volumeofCBT, weightMT, status, message, sql, no_of_pcs, hatch_nbr, autonumber As String
        packing_date = obj.ConvDtFrmt(Me.date.Text, "yyyy-MM-dd")
        'Dim autonumber As Integer = 1
        Dim pack_vesselname = Me.vesno.Text
        packing_vesselname = Me.vesno.Text
        packing_listno = Me.packinglist.Text
        packing_shippername = Me.shippername.Text
        packing_consigneename = Me.conname.Text
        packing_marks = Me.packingmarks.Text
        packing_cnfagent = Me.packingcnfagent.Text
        packing_portname = Me.pportname.Text
        packing_description = Me.pdesc.Text
        no_of_pcs = Me.noofpcs.Text
        hatch_nbr = Me.hatchnbr.Text
        autonumber = Val(Me.number.Text)
        'crane_wt_bup1 = Me.cranew1.Text
        'crane_wt_bup2 = Me.cranew2.Text
        'crane_wt_bup3 = Me.cranew3.Text


        'packing_blocknumber = Me.pblocknumber.Text
        'packing_volumeofcbm = Me.volumeofcbm.Text
        'packing_weightMT = Me.weightMT.Text
        'packing_status = Me.packingstatus.Text
        Dim querylist As New List(Of String)
        message = ""

        'If obj.FindDuplicate("SELECT count(*) FROM granite_vessel_master where vessel_name='" + packing_vesselname + "'") = 0 Then
        '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('invalid vessel name')", True)
        '    Exit Sub
        'End If

        'If obj.FindDuplicate("SELECT count(*) FROM granite_cnfdetails_master where cnf_name='" + packing_cnfagent + "'") = 0 Then
        '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('invalid cnf Name')", True)
        '    Exit Sub
        'End If

        'If obj.FindDuplicate("SELECT count(*) FROM granite_shipper_master where shipper_name='" + packing_shippername + "'") = 0 Then
        '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('invalid Shipper Name')", True)
        '    Exit Sub
        'End If
        If obj.FindDuplicate("SELECT count(*) FROM granite_portdetails_master where port_name='" + packing_portname + "'") = 0 Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('invalid Port Name')", True)
            Exit Sub
        End If

        If obj.FindDuplicate("SELECT count(*) FROM granite_consigneedetails_master where consignee_name='" + packing_consigneename + "'") = 0 Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('invalid consignee Name')", True)
            Exit Sub
        End If

        'If (Gridview1.Rows.Count = 0) Then
        '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('Please Enter block details')", True)
        '    Exit Sub
        'End If


        Select Case Me.save.Text
            Case Is = "Submit"
                'Refno = getreferenceno()
                Dim querycont As New List(Of String)
                If (TextBox1.Text = "") Then
                    Refno = getreferenceno()
                    'Me.blockhidden.Value = autonumber
                    'autonumber = Me.blockhidden.Value
                    sql = "insert into granite_packinglistheader(Refno,packing_date,packing_vesselname ,packing_listno,packing_shippername," & _
                                "packing_consigneename, packing_marks, packing_cnfagent , packing_portname, packing_description,job_completion_flag,packing_list_type,no_of_pcs,hatch_nbr,autonumber)values('" + Refno + "','" + packing_date + "'," & _
                                 "'" + packing_vesselname + "','" + packing_listno + "','" + packing_shippername + "','" + packing_consigneename + "','" + packing_marks + "'," & _
                                 "'" + packing_cnfagent + "','" + packing_portname + "','" + packing_description + "','0','V','" + no_of_pcs + "','" + hatch_nbr + "','" + autonumber + "')"
                    For i = 0 To Gridview1.Rows.Count - 1
                        blocknumber = Server.HtmlDecode(Gridview1.Rows(i).Cells(0).Text)
                        volumeofCBT = Server.HtmlDecode(Gridview1.Rows(i).Cells(1).Text)
                        weightMT = Server.HtmlDecode(Gridview1.Rows(i).Cells(2).Text)
                        status = Server.HtmlDecode(Gridview1.Rows(i).Cells(3).Text)

                        querylist.Add("insert into granite_packinglistdetails(Refno,blocknumber,volumeofCBT,weightMT,status,pack_vesselname,autonumber)values('" + Refno + "','" + blocknumber + "','" + volumeofCBT + "','" + weightMT + "','" + status + "','" + pack_vesselname + "','" + autonumber + "')")

                    Next
                    autonumber = autonumber + 1
                    querylist.Add(sql)
                Else
                    Refno = TextBox1.Text
                    'TextBox1.Text = Refno
                    sql = "insert into granite_packinglistheader(Refno,packing_date,packing_vesselname ,packing_listno,packing_shippername," & _
                                "packing_consigneename, packing_marks, packing_cnfagent , packing_portname, packing_description,job_completion_flag,packing_list_type,no_of_pcs,hatch_nbr,autonumber)values('" + Refno + "','" + packing_date + "'," & _
                                 "'" + packing_vesselname + "','" + packing_listno + "','" + packing_shippername + "','" + packing_consigneename + "','" + packing_marks + "'," & _
                                 "'" + packing_cnfagent + "','" + packing_portname + "','" + packing_description + "','0','V','" + no_of_pcs + "','" + hatch_nbr + "','" + autonumber + "')"
                    For i = 0 To Gridview1.Rows.Count - 1
                        blocknumber = Server.HtmlDecode(Gridview1.Rows(i).Cells(0).Text)
                        volumeofCBT = Server.HtmlDecode(Gridview1.Rows(i).Cells(1).Text)
                        weightMT = Server.HtmlDecode(Gridview1.Rows(i).Cells(2).Text)
                        status = Server.HtmlDecode(Gridview1.Rows(i).Cells(3).Text)
                        autonumber = Server.HtmlDecode(Gridview1.Rows(i).Cells(4).Text)
                        querylist.Add("insert into granite_packinglistdetails(Refno,blocknumber,volumeofCBT,weightMT,status,pack_vesselname,autonumber) values('" + Refno + "','" + blocknumber + "','" + volumeofCBT + "','" + weightMT + "','" + status + "','" + pack_vesselname + "','" + autonumber + "')")
                    Next
                    querylist.Add(sql)
                End If
                querylist.Add("update control_mast set granite_ref_no=granite_ref_no+1")
                message = "Successfully added data"
            Case Is = "Update"
                Refno = Me.Refno.Text
                obj.QueryExecution("delete from granite_packinglistdetails where Refno='" + Refno + "'")
                packingid = Me.hdid.Value
                sql = "update granite_packinglistheader set packing_date='" + packing_date + "',packing_vesselname='" + packing_vesselname + "' ,packing_listno='" + packing_listno +
                    "',packing_shippername ='" + packing_shippername + "', packing_consigneename ='" + packing_consigneename + "' ,packing_marks='" + packing_marks + "',packing_cnfagent ='" + packing_cnfagent +
                    "',packing_portname='" + packing_portname + "' , packing_description='" + packing_description + "' where autoid = " & packingid

                querylist.Add(sql)
                For i = 0 To Gridview1.Rows.Count - 1
                    blocknumber = Server.HtmlDecode(Gridview1.Rows(i).Cells(0).Text)
                    volumeofCBT = Server.HtmlDecode(Gridview1.Rows(i).Cells(1).Text)
                    weightMT = Server.HtmlDecode(Gridview1.Rows(i).Cells(2).Text)
                    status = Server.HtmlDecode(Gridview1.Rows(i).Cells(3).Text)
                    autonumber = Server.HtmlDecode(Gridview1.Rows(i).Cells(4).Text)
                    querylist.Add("insert into granite_packinglistdetails(Refno, blocknumber,volumeofCBT,weightMT,status,pack_vesselname,autonumber)values('" + Refno + "','" + blocknumber + "','" + volumeofCBT + "','" + weightMT + "','" + status + "','" + pack_vesselname + "','" + autonumber + "')")
                Next
                Me.save.Text = "Submit"
                message = "Successfully Update data"
                'Gridview1.selectcommand = " Select Refno,blocknumber,volumeofCBT,weightMT,status from granite_packinglistdetails"
            Case Else
                message = "Not Inserted data"

        End Select
        Select Case obj.TransactionInsert(querylist)
            Case Is = True
                Dim url As String = "PackingListVessel.aspx"
                Dim script As String = "window.onload = function(){ alert('"
                script += message
                script += "');"
                script += "window.location = '"
                script += url
                script += "'; }"
                ClientScript.RegisterStartupScript(Me.GetType(), "Redirect", script, True)
                'bindgridview(packing_vesselname, packing_portname)
                bindgridview(packing_vesselname)
        End Select

    End Sub

    Private Sub gvvessel_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gvvessel.RowCommand
        Dim packingid As String
        Dim Refno As String
        packingid = e.CommandArgument

        Select Case e.CommandName
            Case Is = "Edit"

                Dim strcon As String = ConfigurationManager.ConnectionStrings("MydbConn").ConnectionString
                Dim sql As String

                sql = "Select Refno,packing_date,packing_vesselname,packing_listno, packing_shippername,packing_consigneename ,packing_marks,packing_cnfagent,packing_portname,packing_description,autonumber from granite_packinglistheader where autoid='" + packingid + "'"
                Dim getdata As New List(Of String)
                getdata = obj.GetMoreValueFromQuery(sql, 11)

                Me.refno.Text = getdata.Item(0)
                Me.date.Text = IIf(getdata.Item(1) = "01/01/1900", "", getdata.Item(1))
                Me.vesno.Text = getdata.Item(2)
                Me.packinglist.Text = getdata.Item(3)
                Me.shippername.Text = getdata.Item(4)
                Me.conname.Text = getdata.Item(5)
                Me.packingmarks.Text = getdata.Item(6)
                Me.packingcnfagent.Text = getdata.Item(7)
                Me.pportname.Text = getdata.Item(8)
                Me.pdesc.Text = getdata.Item(9)
                Me.number.Text = getdata.Item(10)
                Me.hdid.Value = packingid

                'If (Me.date.Text = getdata.Item(1)) Then
                '    {

                '    }
                'End If
                Dim table, DD As New DataTable
                DD = obj.getdatatable("select blocknumber,volumeofCBT AS volumeofcbm,weightMT,status from granite_packinglistdetails WHERE autonumber='" + getdata.Item(10) + "'")
                'DD = obj.getdatatable("select blocknumber,volumeofCBT AS volumeofcbm,weightMT,status from granite_packinglistdetails WHERE autonumber='" + autonumber + "'")
                Gridview1.DataSource = DD
                Gridview1.DataBind()
                Me.save.Text = "Update"
            Case Is = "Remove"
                Refno = Me.Refno.Text
                obj.QueryExecution("delete from granite_packinglistheader where Refno='" + Refno + "'")
                obj.QueryExecution("delete from granite_packinglistdetails  where Refno='" + Refno + "'")

        End Select
        bindgridview(Me.vesno.Text)
    End Sub


    Private Function getreferenceno() As String
        Dim RefNo As String = Convert.ToInt32(obj.GetOneValueFromQuery("select granite_ref_no from control_mast")).ToString("D4")
        Dim RefNoFormat As String = obj.GetOneValueFromQuery("select granite_ref_format from control_mast")
        Return RefNoFormat + RefNo
    End Function

    Sub Viewdetails()
        Dim Volumeofcbm As Double = 0
        Dim weightMT As Double = 0
        For i = 0 To Gridview1.Rows.Count - 1


            Volumeofcbm = Volumeofcbm + CDbl(Server.HtmlDecode(Gridview1.Rows(i).Cells(1).Text))
            weightMT = weightMT + CDbl(Server.HtmlDecode(Gridview1.Rows(i).Cells(2).Text))
        Next
        totalblocknumber.Text = "Total number of Blocknumber = " & Gridview1.Rows.Count
        totalvolumeofCBM.Text = "Total number of Volumeofcbm =  " & Volumeofcbm
        totalweightMT.Text = "Total number of weightMT =  " & weightMT


    End Sub
    Sub bindgridview(ByVal v As String)
        'Sub bindgridview(ByVal v As String, ByVal p As String)
        listdsi.SelectCommand = "Select autoid, Refno,format( packing_date,'dd/MM/yyyy')as packing_date,packing_vesselname," & _
            "packing_listno, packing_shippername,packing_consigneename ,packing_marks,packing_cnfagent," & _
            "packing_portname,packing_description from granite_packinglistheader where packing_vesselname='" + v + "' and job_completion_flag='0'and packing_list_type='V'"
        Viewdetails()
    End Sub

    Private Sub add_Click(sender As Object, e As System.EventArgs) Handles add.Click
        Dim table As New DataTable
        ' Create four typed columns in the DataTable.
        table.Columns.AddRange(New DataColumn() {New DataColumn("Blocknumber", GetType(String)), _
                                              New DataColumn("Volumeofcbm", GetType(String)), _
                                              New DataColumn("weightMT", GetType(String)), _
                                              New DataColumn("status", GetType(String))})

        ' Add five rows with those columns filled in the DataTable.
        For i = 0 To Gridview1.Rows.Count - 1
            table.Rows.Add(Server.HtmlDecode(Gridview1.Rows(i).Cells(0).Text),
                           Server.HtmlDecode(Gridview1.Rows(i).Cells(1).Text),
                          Server.HtmlDecode(Gridview1.Rows(i).Cells(2).Text),
                           Server.HtmlDecode(Gridview1.Rows(i).Cells(3).Text))
        Next
      
        table.Rows.Add(Me.pblocknumber.Text, obj.ISValidNumber(Me.volumeofcbm.Text), obj.ISValidNumber(Me.weightMT.Text), "A")
           
        Me.pblocknumber.Text = ""
        Me.volumeofcbm.Text = ""
        Me.weightMT.Text = ""
        Me.packingstatus.Text = ""
        'Me.autonumber.Text = ""
        Gridview1.DataSource = table
        Gridview1.DataBind()
        Viewdetails()
        'If ((pblocknumber.Text = String.Empty) And (volumeofcbm.Text = String.Empty)) Then
        '    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('You Need To Enter Atleast One Field')", True)
        'End If

    End Sub

    Private Sub RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Gridview1.RowCommand
        Dim RefId As String = e.CommandArgument

        Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)

        Select Case e.CommandName
            Case Is = "change"
                Me.pblocknumber.Text = Server.HtmlDecode(Gridview1.Rows(rowIndex).Cells(0).Text)
                Me.volumeofcbm.Text = Server.HtmlDecode(Gridview1.Rows(rowIndex).Cells(1).Text)
                Me.weightMT.Text = Server.HtmlDecode(Gridview1.Rows(rowIndex).Cells(2).Text)
                Me.packingstatus.Text = Server.HtmlDecode(Gridview1.Rows(rowIndex).Cells(3).Text)
                'Me.autonumber.Text = Server.HtmlDecode(Gridview1.Rows(rowIndex).Cells(4).Text)
                'Case Is = "remove"
        End Select
        Dim table As New DataTable
        ' Create four typed columns in the DataTable.
        table.Columns.AddRange(New DataColumn() {New DataColumn("Blocknumber", GetType(String)), _
                                         New DataColumn("Volumeofcbm", GetType(String)), _
                                         New DataColumn("weightMT", GetType(String)), _
                                         New DataColumn("status", GetType(String))})

        ' Add five rows with those columns filled in the DataTable.
        For i = 0 To Gridview1.Rows.Count - 1
            table.Rows.Add(Server.HtmlDecode(Gridview1.Rows(i).Cells(0).Text),
                           Server.HtmlDecode(Gridview1.Rows(i).Cells(1).Text),
                           Server.HtmlDecode(Gridview1.Rows(i).Cells(2).Text),
                           Server.HtmlDecode(Gridview1.Rows(i).Cells(3).Text)
)

        Next

        table.Rows.RemoveAt(rowIndex)
        Gridview1.DataSource = table
        Gridview1.DataBind()

        Viewdetails()
    End Sub

    Protected Sub Gridview1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Gridview1.SelectedIndexChanged

    End Sub

    Private Sub viewgrid_Click(sender As Object, e As System.EventArgs) Handles viewgrid.Click
        'bindgridview(Me.vesno.Text, Me.pportname.Text)
        bindgridview(Me.vesno.Text)
    End Sub

    Private Sub clear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles clear.Click
        vesno.Text = ""
        packinglist.Text = ""
        shippername.Text = ""
        conname.Text = ""
        packingmarks.Text = ""
        packingcnfagent.Text = ""
        pportname.Text = ""
        pdesc.Text = ""
        pblocknumber.Text = ""
        volumeofcbm.Text = ""
        weightMT.Text = ""
        packingstatus.Text = ""

    End Sub

    Private Sub jobno_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles jobno.Click
        Dim Refno As String
        Refno = obj.GetOneValueFromQuery("select Refno from granite_packinglistheader where packing_vesselname = '" & vesno.Text & "' and packing_portname='" & pportname.Text & "' and job_completion_flag=0")
        TextBox1.Text = Refno
    End Sub
End Class