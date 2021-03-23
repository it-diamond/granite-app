
Imports System.Web.Services
Imports System.Data.SqlClient
Public Class Supplementary
    Inherits System.Web.UI.Page
    Dim obj As New ObjClass
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            Me.processdt.Text = Format(Now, "dd/MM/yyyy")
            Me.submit.Visible = True
            Me.update.Visible = False
        End If
        Dim scriptKey As String = "UniqueKeyForThisScript"
        Response.Write("<script>$('.imp').hide(); $('.exp').hide();$('.eximp').hide(); </script>")
        Dim javaScript As String = "<script type='text/javascript'>dpmtfn('" + Session("dept") + "');</script>"
        ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)
    End Sub

    Private Sub add_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles add.Click


        Dim table As New DataTable
        ' Create four typed columns in the DataTable.
        table.Columns.AddRange(New DataColumn() {New DataColumn("name", GetType(String)), _
                                              New DataColumn("voucheramt", GetType(String)), _
                                              New DataColumn("chargedesc", GetType(String)), _
                                              New DataColumn("remarks", GetType(String)), _
                                              New DataColumn("taxamt", GetType(String)), _
                                              New DataColumn("grandamt", GetType(String)), _
                                              New DataColumn("round", GetType(String)), _
                                              New DataColumn("totamt", GetType(String))})

        ' Add five rows with those columns filled in the DataTable.
        For i = 0 To Gridview1.Rows.Count - 1
            table.Rows.Add(Server.HtmlDecode(Gridview1.Rows(i).Cells(0).Text),
                           Server.HtmlDecode(Gridview1.Rows(i).Cells(1).Text),
                          Server.HtmlDecode(Gridview1.Rows(i).Cells(2).Text),
                           Server.HtmlDecode(Gridview1.Rows(i).Cells(3).Text),
                           Server.HtmlDecode(Gridview1.Rows(i).Cells(4).Text),
                           Server.HtmlDecode(Gridview1.Rows(i).Cells(5).Text),
                           Server.HtmlDecode(Gridview1.Rows(i).Cells(6).Text),
                           Server.HtmlDecode(Gridview1.Rows(i).Cells(7).Text))
        Next
        If Me.vouchertype.SelectedValue = "general" Then
            table.Rows.Add("", Me.voucheramt.Text, Me.chargedesc.Text, Me.remarks.Text, "0", Me.voucheramt.Text, "0", Me.voucheramt.Text)


        ElseIf Me.vouchertype.SelectedValue = "partywise" Then
            table.Rows.Add(Me.cusname.Text, Me.voucheramt.Text, Me.chargedesc.Text, Me.remarks.Text, "0", Me.voucheramt.Text, "0", Me.voucheramt.Text)
            Me.cusname.Text = ""
            Me.cusname.Visible = True

        Else
            If obj.FindDuplicate("select count(*) from doc_arrival where refno='" + Me.jobno.Text + "'") = 0 Then
            Else
                If obj.GetOneValueFromQuery("select job_flg from doc_arrival where RefNo='" + Me.jobno.Text + "'") = "1" Then
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('Softlink Job No is not entered for this CNF Ref No - " + Me.jobno.Text + "')", True)
                    Exit Sub
                End If
            End If
            table.Rows.Add(Me.jobno.Text, Me.voucheramt.Text, Me.chargedesc.Text, Me.remarks.Text,
                           obj.ISValidNumber(Me.taxamt.Text), obj.ISValidNumber(Me.grandamt.Text),
                          Math.Round(obj.ISValidNumber(Me.totamt.Text) - obj.ISValidNumber(Me.grandamt.Text), 2), obj.ISValidNumber(Me.totamt.Text))
            Me.jobno.Text = ""
            Me.jobno.Visible = True


        End If
        Me.voucheramt.Text = ""
        Me.chargedesc.Text = ""
        Me.remarks.Text = ""
        Me.taxamt.Text = ""
        Me.grandamt.Text = ""
        Me.round.Text = ""
        Me.totamt.Text = ""
        Gridview1.DataSource = table
        Gridview1.DataBind()
    End Sub

    <WebMethod()>
    Public Shared Function GetEmployee(ByVal prefix As String) As String()
        Dim Employee As New List(Of String)()
        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("MyDbConn").ConnectionString
            Using cmd As New SqlCommand()
                cmd.CommandText = "select name,dpmt from employee_master where name like '%'+@prefix+'%'"
                cmd.Parameters.AddWithValue("@prefix", prefix)
                cmd.Connection = conn
                conn.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        Employee.Add(String.Format("{0}-{1}", sdr("name"), sdr("dpmt")))
                    End While
                End Using
                conn.Close()
            End Using
        End Using
        Return Employee.ToArray()
    End Function

    <WebMethod()>
    Public Shared Function GetVno(ByVal prefix As String) As String()
        Dim Employee As New List(Of String)()
        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("MyDbConn").ConnectionString
            Using cmd As New SqlCommand()
                cmd.CommandText = "select  voucher_no + ' - ' + emp_name+ ' - ' +convert(nvarchar,voucher_date,106) + ' - ' + department,voucher_no from voucher_tbl where voucher_no like '%'+@prefix+'%'  and voucher_type = 'SUPPLEMENTARY' and department='GRANITE DEPARTMENT'"
                cmd.Parameters.AddWithValue("@prefix", prefix)
                cmd.Connection = conn
                conn.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        Employee.Add(String.Format("{0}|{1}", sdr(0), sdr(1)))

                    End While
                End Using
                conn.Close()
            End Using
        End Using
        Return Employee.ToArray()
    End Function
    <WebMethod()>
    Public Shared Function GetChargeDesc(ByVal prefix As String) As String()
        Dim Employee As New List(Of String)()
        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("MyDbConn").ConnectionString
            Using cmd As New SqlCommand()
                cmd.CommandText = "select charge_desc,rate from charge_desc_master where charge_desc like '%'+@prefix+'%'"
                cmd.Parameters.AddWithValue("@prefix", prefix)
                cmd.Connection = conn
                conn.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        Employee.Add(String.Format("{0}|{1}", sdr(0), sdr(1)))
                    End While
                End Using
                conn.Close()
            End Using
        End Using
        Return Employee.ToArray()
    End Function
    <WebMethod()>
    Public Shared Function GetShipper(ByVal prefix As String) As String()
        Dim Employee As New List(Of String)()
        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("MyDbConn").ConnectionString
            Using cmd As New SqlCommand()
                cmd.CommandText = "select shipper_name,'' from granite_shipper_master where shipper_name like '%'+@prefix+'%'"
                cmd.Parameters.AddWithValue("@prefix", prefix)
                cmd.Connection = conn
                conn.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        Employee.Add(String.Format("{0}|{1}", sdr(0), sdr(1)))
                    End While
                End Using
                conn.Close()
            End Using
        End Using
        Return Employee.ToArray()
    End Function
    <WebMethod()>
    Public Shared Function GetJobNo(ByVal prefix As String) As String()
        Dim Employee As New List(Of String)()
        Dim sql As String
        'Select Case SharedData.Dpmt
        '    Case Is = "Exports"
        sql = "select Refno,packing_shippername from granite_packinglistheader where Refno like '%'+@prefix+'%'"
        '    Case Else
        'sql = "select RefNo,cuscode from doc_arrival where RefNo like '%'+@prefix+'%'"

        'End Select 

        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("MyDbConn").ConnectionString
            Using cmd As New SqlCommand()
                cmd.CommandText = sql
                cmd.Parameters.AddWithValue("@prefix", prefix)
                cmd.Connection = conn
                conn.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        Employee.Add(String.Format("{0}|{1}", sdr(0), sdr(1)))
                    End While
                End Using
                conn.Close()
            End Using
        End Using
        Return Employee.ToArray()
    End Function


    <WebMethod()>
    Public Shared Function GetRemarks(ByVal prefix As String) As String()
        Dim Employee As New List(Of String)()
        Dim sql As String
        sql = "select distinct remarks from supplementary_dtl where remarks like '%'+@prefix+'%'"
        Using conn As New SqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("MyDbConn").ConnectionString
            Using cmd As New SqlCommand()
                cmd.CommandText = sql
                cmd.Parameters.AddWithValue("@prefix", prefix)
                cmd.Connection = conn
                conn.Open()
                Using sdr As SqlDataReader = cmd.ExecuteReader()
                    While sdr.Read()
                        Employee.Add(String.Format("{0}|{1}", sdr(0), ""))
                    End While
                End Using
                conn.Close()
            End Using
        End Using
        Return Employee.ToArray()
    End Function
    Private Sub submit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles submit.Click
        If obj.FindDuplicate("select COUNT(*) from employee_master where name='" + Me.empname.Text + "'") = 0 Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('Employee Name is Invalid')", True)
            Exit Sub
        End If
        
        GraniteInsertFunction()
    End Sub

    Private Sub update_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles update.Click
        If obj.FindDuplicate("select COUNT(*) from employee_master where name='" + Me.empname.Text + "'") = 0 Then
            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('Employee Name is Invalid')", True)
            Exit Sub
        End If
         
        GraniteUpdateFunction()
           

    End Sub

    Protected Sub RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Gridview1.RowCommand
        Dim RefId As String = e.CommandArgument

        Dim rowIndex As Integer = Convert.ToInt32(e.CommandArgument)

        Select Case e.CommandName
            Case Is = "change"
                Dim whattype
                If obj.FindDuplicate("select count(*) from voucher_tbl where voucher_no='" + Me.hdvno.Value + "'") = 0 Then
                    If (Me.vouchertype.SelectedValue = "general") Then
                        whattype = "general"
                    ElseIf (Me.vouchertype.SelectedValue = "partywise") Then
                        whattype = "partywise"
                    Else
                        whattype = "jobno"
                    End If
                Else
                    whattype = obj.GetOneValueFromQuery("select what_type from voucher_tbl where voucher_no='" + Me.vno.Text + "'")

                End If



                Me.chargedesc.Text = Server.HtmlDecode(Gridview1.Rows(rowIndex).Cells(2).Text)
                Me.voucheramt.Text = Server.HtmlDecode(Gridview1.Rows(rowIndex).Cells(1).Text)
                Me.remarks.Text = Server.HtmlDecode(Gridview1.Rows(rowIndex).Cells(3).Text)
                Me.taxamt.Text = Server.HtmlDecode(Gridview1.Rows(rowIndex).Cells(4).Text)
                Me.grandamt.Text = Server.HtmlDecode(Gridview1.Rows(rowIndex).Cells(5).Text)
                Me.round.Text = Server.HtmlDecode(Gridview1.Rows(rowIndex).Cells(6).Text)
                Me.totamt.Text = Server.HtmlDecode(Gridview1.Rows(rowIndex).Cells(7).Text)
                'Dim sqlgrid As String
                If whattype = "general" Then
                    Me.taxamt.Visible = False
                    Me.grandamt.Visible = False
                    Me.round.Visible = False
                    Me.totamt.Visible = False
                ElseIf whattype = "partywise" Then
                    Me.cusname.Text = Server.HtmlDecode(Gridview1.Rows(rowIndex).Cells(0).Text)
                    Me.taxamt.Visible = False
                    Me.grandamt.Visible = False
                    Me.round.Visible = False
                    Me.totamt.Visible = False
                Else
                    Me.jobno.Text = Server.HtmlDecode(Gridview1.Rows(rowIndex).Cells(0).Text)
                    Me.taxamt.Visible = True
                    Me.grandamt.Visible = True
                    Me.round.Visible = True
                    Me.totamt.Visible = True
                End If

                Dim table As New DataTable
                ' Create four typed columns in the DataTable.
                table.Columns.AddRange(New DataColumn() {New DataColumn("name", GetType(String)), _
                                                      New DataColumn("voucheramt", GetType(String)), _
                                                      New DataColumn("chargedesc", GetType(String)), _
                                                      New DataColumn("remarks", GetType(String)), _
                                              New DataColumn("taxamt", GetType(String)), _
                                              New DataColumn("grandamt", GetType(String)), _
                                              New DataColumn("round", GetType(String)), _
                                              New DataColumn("totamt", GetType(String))})

                ' Add five rows with those columns filled in the DataTable.
                For i = 0 To Gridview1.Rows.Count - 1
                    table.Rows.Add(Server.HtmlDecode(Gridview1.Rows(i).Cells(0).Text),
                                   Server.HtmlDecode(Gridview1.Rows(i).Cells(1).Text),
                                  Server.HtmlDecode(Gridview1.Rows(i).Cells(2).Text),
                                   Server.HtmlDecode(Gridview1.Rows(i).Cells(3).Text),
                                   Server.HtmlDecode(Gridview1.Rows(i).Cells(4).Text),
                                   Server.HtmlDecode(Gridview1.Rows(i).Cells(5).Text),
                                   Server.HtmlDecode(Gridview1.Rows(i).Cells(6).Text),
                                   Server.HtmlDecode(Gridview1.Rows(i).Cells(7).Text))
                Next
                table.Rows.RemoveAt(rowIndex)



                Gridview1.DataSource = table
                Gridview1.DataBind()
            Case Is = "remove"
                Dim whattype
                If obj.FindDuplicate("select count(*) from voucher_tbl where voucher_no='" + Me.hdvno.Value + "'") = 0 Then
                    If (Me.vouchertype.SelectedValue = "general") Then
                        whattype = "general"
                    ElseIf (Me.vouchertype.SelectedValue = "partywise") Then
                        whattype = "partywise"
                    Else
                        whattype = "jobno"
                    End If
                Else
                    whattype = obj.GetOneValueFromQuery("select what_type from voucher_tbl where voucher_no='" + Me.vno.Text + "'")

                End If
                'Dim sqlgrid As String
                If whattype = "general" Then
                    Me.taxamt.Visible = False
                    Me.grandamt.Visible = False
                    Me.round.Visible = False
                    Me.totamt.Visible = False
                ElseIf whattype = "partywise" Then
                    Me.taxamt.Visible = False
                    Me.grandamt.Visible = False
                    Me.round.Visible = False
                    Me.totamt.Visible = False
                Else
                    Me.taxamt.Visible = True
                    Me.grandamt.Visible = True
                    Me.round.Visible = True
                    Me.totamt.Visible = True
                End If

                Dim table As New DataTable
                ' Create four typed columns in the DataTable.
                table.Columns.AddRange(New DataColumn() {New DataColumn("name", GetType(String)), _
                                                      New DataColumn("voucheramt", GetType(String)), _
                                                      New DataColumn("chargedesc", GetType(String)), _
                                                      New DataColumn("remarks", GetType(String)), _
                                              New DataColumn("taxamt", GetType(String)), _
                                              New DataColumn("grandamt", GetType(String)), _
                                              New DataColumn("round", GetType(String)), _
                                              New DataColumn("totamt", GetType(String))})

                ' Add five rows with those columns filled in the DataTable.
                For i = 0 To Gridview1.Rows.Count - 1
                    table.Rows.Add(Server.HtmlDecode(Gridview1.Rows(i).Cells(0).Text),
                                   Server.HtmlDecode(Gridview1.Rows(i).Cells(1).Text),
                                  Server.HtmlDecode(Gridview1.Rows(i).Cells(2).Text),
                                  Server.HtmlDecode(Gridview1.Rows(i).Cells(3).Text),
                                   Server.HtmlDecode(Gridview1.Rows(i).Cells(4).Text),
                                   Server.HtmlDecode(Gridview1.Rows(i).Cells(5).Text),
                                   Server.HtmlDecode(Gridview1.Rows(i).Cells(6).Text),
                                   Server.HtmlDecode(Gridview1.Rows(i).Cells(7).Text))
                Next
                table.Rows.RemoveAt(rowIndex)
                Gridview1.DataSource = table
                Gridview1.DataBind()
        End Select




    End Sub

    Private Sub GraniteInsertFunction()
        Me.update.Visible = False
        Me.submit.Visible = True
        Dim querylist As New List(Of String)
        Dim job_nodtl, charge_desc, job_amt, remarksdtl, voucher_no As String
        Dim totjobamt As Double = 0
        voucher_no = obj.GetOneValueFromQuery("select convert(nvarchar,voucher_no)+voucher_format from control_mast")
        Dim tax_amt, grand_amt, round_off, tot_amt As String
        For i = 0 To Gridview1.Rows.Count - 1
            job_nodtl = Server.HtmlDecode(Gridview1.Rows(i).Cells(0).Text)
            charge_desc = Server.HtmlDecode(Gridview1.Rows(i).Cells(2).Text)
            job_amt = Server.HtmlDecode(Gridview1.Rows(i).Cells(1).Text)
            remarksdtl = Server.HtmlDecode(Gridview1.Rows(i).Cells(3).Text)
            tax_amt = Server.HtmlDecode(Gridview1.Rows(i).Cells(4).Text)
            grand_amt = Server.HtmlDecode(Gridview1.Rows(i).Cells(5).Text)
            round_off = Server.HtmlDecode(Gridview1.Rows(i).Cells(6).Text)
            tot_amt = Server.HtmlDecode(Gridview1.Rows(i).Cells(7).Text)

            querylist.Add("insert into  supplementary_dtl(voucher_no ,job_no ,charge_desc ,job_amt ," & _
                          "remarks,tax_amt,grand_amt,round_off,tot_amt  ) values" & _
                          "('" + voucher_no + "','" + job_nodtl + "','" + charge_desc + "'," & _
                          "'" + job_amt + "','" + remarksdtl + "','" + tax_amt + "','" + grand_amt + "'," & _
                          "'" + round_off + "','" + tot_amt + "')"
)
            totjobamt = totjobamt + CDbl(tot_amt)
        Next
        Dim voucher_date, what_type, emp_name,
       voucher_type, cus_name, remarks, voucher_amt, bank_name, payment_type,
       place_type, department, credit_in_bank, supplier_name, account_name, bill_type, cheque_serial_no, cheque_flg

        voucher_date = obj.ConvDtFrmt(Me.processdt.Text, "yyyy-MM-dd")
        If (Me.vouchertype.SelectedValue = "general") Then
            what_type = "general"
        ElseIf (Me.vouchertype.SelectedValue = "partywise") Then
            what_type = "partywise"
        Else
            what_type = "jobno"
        End If
        voucher_type = "SUPPLEMENTARY"
        emp_name = Me.empname.Text
        cus_name = ""
        remarks = ""
        voucher_amt = totjobamt
        bank_name = ""
        payment_type = ""
        place_type = ""
        department = "GRANITE DEPARTMENT"
        credit_in_bank = ""
        supplier_name = ""
        account_name = ""
        bill_type = ""
        cheque_serial_no = ""
        cheque_flg = "0"
        querylist.Add("insert into voucher_tbl (voucher_no ,voucher_date,what_type ," & _
        "voucher_type, cus_name, remarks, voucher_amt, bank_name, payment_type," & _
        "place_type, department, credit_in_bank, supplier_name, account_name,emp_name,bill_type,cheque_serial_no  ,cheque_flg,approve_flg) values('" + voucher_no + "','" + voucher_date + "'," & _
        "'" + what_type + "','" + voucher_type + "','" + cus_name + "','" + remarks + "'," & _
        "'" & voucher_amt & "','" + bank_name + "','" + payment_type + "','" + place_type + "','" + department + "','" + credit_in_bank + "'," & _
        "'" + supplier_name + "','" + account_name + "','" + emp_name + "','" + bill_type + "','" + cheque_serial_no + "','" + cheque_flg + "',0)"
)

        Select Case obj.TransactionInsert(querylist)
            Case Is = True
                'Dim pageurl = "../ExpReports/CashPayVoc.aspx?vouno=" + voucher_no
                'Response.Write("<script> window.open( '" + pageurl + "','_blank' ); </script>")
                Dim message As String = "Export Voucher Details was sent to DSA Account, After getting approved, you can print voucher " & voucher_no & "."
                Dim url As String = "Supplementary.aspx"
                Dim script As String = "window.onload = function(){ alert('"
                script += message
                script += "');"
                script += "window.location = '"
                script += url
                script += "'; }"
                ClientScript.RegisterStartupScript(Me.GetType(), "Redirect", script, True)
            Case Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('Something went wrong')", True)
        End Select

    End Sub
    

    Private Sub GraniteUpdateFunction()
        Me.update.Visible = False
        Me.submit.Visible = True
        Dim querylist As New List(Of String)
        Dim job_nodtl, charge_desc, job_amt, remarksdtl, voucher_no As String
        Dim totjobamt As Double = 0
        voucher_no = Me.hdvno.Value
        obj.QueryExecution("delete from supplementary_dtl where voucher_no='" + voucher_no + "'")
        Dim tax_amt, grand_amt, round_off, tot_amt As String
        For i = 0 To Gridview1.Rows.Count - 1
            job_nodtl = Server.HtmlDecode(Gridview1.Rows(i).Cells(0).Text)
            charge_desc = Server.HtmlDecode(Gridview1.Rows(i).Cells(2).Text)
            job_amt = Server.HtmlDecode(Gridview1.Rows(i).Cells(1).Text)
            remarksdtl = Server.HtmlDecode(Gridview1.Rows(i).Cells(3).Text)
            tax_amt = Server.HtmlDecode(Gridview1.Rows(i).Cells(4).Text)
            grand_amt = Server.HtmlDecode(Gridview1.Rows(i).Cells(5).Text)
            round_off = Server.HtmlDecode(Gridview1.Rows(i).Cells(6).Text)
            tot_amt = Server.HtmlDecode(Gridview1.Rows(i).Cells(7).Text)

            querylist.Add("insert into  supplementary_dtl(voucher_no ,job_no ,charge_desc ,job_amt ," & _
                          "remarks,tax_amt,grand_amt,round_off,tot_amt) values" & _
                          "('" + voucher_no + "','" + job_nodtl + "','" + charge_desc + "'," & _
                          "'" + job_amt + "','" + remarksdtl + "','" + tax_amt + "','" + grand_amt + "'," & _
                          "'" + round_off + "','" + tot_amt + "')"
)
            totjobamt = totjobamt + CDbl(tot_amt)
        Next
        Dim voucher_date, what_type, emp_name, voucher_amt


        voucher_date = obj.ConvDtFrmt(Me.processdt.Text, "yyyy-MM-dd")
        If (Me.vouchertype.SelectedValue = "general") Then
            what_type = "general"
        ElseIf (Me.vouchertype.SelectedValue = "partywise") Then
            what_type = "partywise"
        Else
            what_type = "jobno"
        End If
        emp_name = Me.empname.Text
        voucher_amt = totjobamt

        querylist.Add("update voucher_tbl set voucher_date='" + voucher_date + "',what_type='" + what_type + "' ," & _
        " voucher_amt='" & voucher_amt & "',emp_name='" + emp_name + "',approve_flg=0 where voucher_no='" + voucher_no + "'")

        Select Case obj.TransactionInsert(querylist)
            Case Is = True
                'Dim pageurl = "../ExpReports/CashPayVoc.aspx?vouno=" + voucher_no
                'Response.Write("<script> window.open( '" + pageurl + "','_blank' ); </script>")
                Dim message As String = "Export Voucher Details was sent to DSA Account, After getting approved, you can print voucher " & voucher_no & "."
                Dim url As String = "Supplementary.aspx"
                Dim script As String = "window.onload = function(){ alert('"
                script += message
                script += "');"
                script += "window.location = '"
                script += url
                script += "'; }"
                ClientScript.RegisterStartupScript(Me.GetType(), "Redirect", script, True)
            Case Else
                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "alertMessage", "alert('Something went wrong')", True)
        End Select

    End Sub

  

    Private Sub filldetails_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles filldetails.Click
        Me.submit.Visible = False
        Me.update.Visible = True
        Dim voucher_no As String = Me.hdvno.Value
        Dim listdata = obj.GetMoreValueFromQuery("select voucher_date,what_type,emp_name from voucher_tbl where voucher_no='" + voucher_no + "'", 3)

        Me.processdt.Text = obj.ConvDtFrmt(listdata(0), "dd/MM/yyyy")
        Me.vouchertype.SelectedValue = listdata(1)
        Me.empname.Text = listdata(2)

        Dim sql As String
        If listdata(1) = "general" Then
            sql = "select '' as name, charge_desc as chargedesc, job_amt as voucheramt, remarks,tax_amt as taxamt,grand_amt as grandamt,round_off as round,tot_amt as totamt  from supplementary_dtl where voucher_no='" + voucher_no + "'"
            Me.taxamt.Visible = False
            Me.grandamt.Visible = False
            Me.round.Visible = False
            Me.totamt.Visible = False
        ElseIf listdata(1) = "partywise" Then
            sql = "select job_no as name, charge_desc as chargedesc, job_amt as voucheramt, remarks,tax_amt as taxamt,grand_amt as grandamt,round_off as round,tot_amt as totamt  from supplementary_dtl where voucher_no='" + voucher_no + "'"
            Me.taxamt.Visible = False
            Me.grandamt.Visible = False
            Me.round.Visible = False
            Me.totamt.Visible = False
        Else
            Me.taxamt.Visible = True
            Me.grandamt.Visible = True
            Me.round.Visible = True
            Me.totamt.Visible = True
            sql = "select job_no as name, charge_desc as chargedesc, job_amt as voucheramt, remarks,tax_amt as taxamt,grand_amt as grandamt,round_off as round,tot_amt as totamt  from supplementary_dtl where voucher_no='" + voucher_no + "'"
        End If

        ' Add five rows with those columns filled in the DataTable.
        Dim dt = obj.getdatatable(sql)
        Gridview1.DataSource = dt
        Gridview1.DataBind()
    End Sub


   
    



End Class