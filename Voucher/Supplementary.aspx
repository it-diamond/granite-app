<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Supplementary.aspx.vb" Inherits="CnfGranite.Supplementary" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>CNF - Granite Report Tree</title>.
    	<meta charset="utf-8">
	<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
	<meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0">
    <script src="https://use.fontawesome.com/releases/v5.0.8/js/all.js" type="text/javascript"></script>
	<!-- VENDOR CSS -->
	<link rel="stylesheet" href="../assets/vendor/bootstrap/css/bootstrap.min.css">
	<link rel="stylesheet" href="../assets/vendor/font-awesome/css/font-awesome.min.css">
	<link rel="stylesheet" href="../assets/vendor/linearicons/style.css">
	<link rel="stylesheet" href="../assets/vendor/chartist/css/chartist-custom.css">
	<link rel="stylesheet" href="../assets/css/main.css">
	<link href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,600,700" rel="stylesheet">
     <%-- <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>--%>
      <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
       <link rel="Shortcut Icon" type="image/png" href="../Images/ic.png" />
        <link href="../tcal.css" rel="stylesheet" type="text/css" />
    <script src="../tcal.js" type="text/javascript"></script>
    
        <style >
    .clsegm,.clscnf,.clsweight
    
    {
        display :none ;
    }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".autocalc").keyup(function () {
                var voucheramt, taxamt, grandamt, roundoff, total;
                voucheramt = $("#voucheramt").val();
                taxamt = $("#taxamt").val();
                grandamt = (+voucheramt) + (+taxamt);
                total = Math.round(grandamt)
                var getrnd = Math.round(grandamt, 2)
                roundoff = (+grandamt) - (+getrnd);
                $("#grandamt").val(grandamt);
                $("#totamt").val(total);
                $("#round").val(Math.round(roundoff, 2));

            });
        });
</script>
<script type="text/javascript">

    $(document).ready(function () {
        $(".divjob").hide();
        $(".divcus").hide();
        $(".common").show();

        $("#taxamt").hide();
        $("#grandamt").hide();
        $("#totamt").hide();
        $("#round").hide();
        var selectedvalue = $('#<%= vouchertype.ClientID %> input:checked').val()
        if (selectedvalue == "general") { gendiv(); }
        else if (selectedvalue == "partywise") { partydiv(); }
        else { jobdiv(); }

    });
    function gendiv() {
        $(".divjob").hide();
        $(".divcus").hide();
        $(".common").show();


        $("#taxamt").hide();
        $("#grandamt").hide();
        $("#totamt").hide();
        $("#round").hide();
    }
    function partydiv() {
        $(".divjob").hide();
        $(".divcus").show();
        $(".common").show();

        $("#taxamt").hide();
        $("#grandamt").hide();
        $("#totamt").hide();
        $("#round").hide();
    }
    function jobdiv() {
        $(".divjob").show();
        $(".divcus").hide();
        $(".common").show();

        $("#taxamt").show();
        $("#grandamt").show();
        $("#totamt").show();
        $("#round").show();
    }
</script>
  <script type="text/javascript">
      $(function () {

          $('.fillemp').typeahead({
              hint: true,
              highlight: true,
              minLength: 1
            , source: function (request, response) {
                $.ajax({
                    url: '<%=ResolveUrl("Supplementary.aspx/GetEmployee") %>',
                    data: "{ 'prefix': '" + request + "'}",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        items = [];
                        map = {};
                        $.each(data.d, function (i, item) {
                            var id = item.split('-')[1];
                            var name = item.split('-')[0];
                            map[name] = { id: id, name: name };
                            items.push(name);
                        });
                        response(items);
                        $(".dropdown-menu").css("height", "auto");
                    },
                    error: function (response) {
                        alert(response.responseText);
                    },
                    failure: function (response) {
                        alert(response.responseText);
                    }
                });
            },
              updater: function (item) {


                  return item;
              }
          });
      });
</script>
<script type="text/javascript">
    $(function () {
        $('.fillship').typeahead({
            hint: true,
            highlight: true,
            minLength: 1
            , source: function (request, response) {
                $.ajax({
                    url: '<%=ResolveUrl("Supplementary.aspx/GetShipper") %>',
                    data: "{ 'prefix': '" + request + "'}",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        items = [];
                        map = {};
                        $.each(data.d, function (i, item) {
                            var id = item.split('-')[1];
                            var name = item.split('-')[0];
                            map[name] = { id: id, name: name };
                            items.push(name);
                        });
                        response(items);
                        $(".dropdown-menu").css("height", "auto");
                    },
                    error: function (response) {
                        alert(response.responseText);
                    },
                    failure: function (response) {
                        alert(response.responseText);
                    }
                });
            },
            updater: function (item) {
                //$('[id*=hfResult]').val(map[item].id);
                return item;
            }
        });
    });
</script>
<script type="text/javascript">
    $(function () {
        $('.filldesc').typeahead({
            hint: true,
            highlight: true,
            minLength: 1
            , source: function (request, response) {
                $.ajax({
                    url: '<%=ResolveUrl("Supplementary.aspx/GetChargeDesc") %>',
                    data: "{ 'prefix': '" + request + "'}",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        items = [];
                        map = {};
                        $.each(data.d, function (i, item) {
                            var id = item.split('|')[1];
                            var name = item.split('|')[0];
                            map[name] = { id: id, name: name };
                            items.push(name);
                        });
                        response(items);
                        $(".dropdown-menu").css("height", "auto");
                    },
                    error: function (response) {
                        alert(response.responseText);
                    },
                    failure: function (response) {
                        alert(response.responseText);
                    }
                });
            },
            updater: function (item) {
                $('[id*=voucheramt]').val(map[item].id);
                return item;
            }
        });

       
    });
</script>

<script type="text/javascript">
    $(function () {
        $('.fillremarks').typeahead({
            hint: true,
            highlight: true,
            minLength: 1
            , source: function (request, response) {
                $.ajax({
                    url: '<%=ResolveUrl("Supplementary.aspx/GetRemarks") %>',
                    data: "{ 'prefix': '" + request + "'}",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        items = [];
                        map = {};
                        $.each(data.d, function (i, item) {
                            var id = item.split('|')[1];
                            var name = item.split('|')[0];
                            map[name] = { id: id, name: name };
                            items.push(name);
                        });
                        response(items);
                        $(".dropdown-menu").css("height", "auto");
                    },
                    error: function (response) {
                        //alert(response.responseText);
                    },
                    failure: function (response) {
                        // alert(response.responseText);
                    }
                });
            },
            updater: function (item) {
                return item;
            }
        });

    });
</script>


<script type="text/javascript">
    $(function () {
        $('.filljob').typeahead({
            hint: true,
            highlight: true,
            minLength: 1
            , source: function (request, response) {
                $.ajax({
                    url: '<%=ResolveUrl("Supplementary.aspx/GetJobNo") %>',
                    data: "{ 'prefix': '" + request + "'}",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        items = [];
                        map = {};
                        $.each(data.d, function (i, item) {
                            var id = item.split('|')[1];
                            var name = item.split('|')[0];
                            map[name] = { id: id, name: name };
                            items.push(name);
                        });
                        response(items);
                        $(".dropdown-menu").css("height", "auto");
                    },
                    error: function (response) {
                        //  alert(response.responseText);
                    },
                    failure: function (response) {
                        // alert(response.responseText);
                    }
                });
            },
            updater: function (item) {
                $('#lblcusname').text(map[item].id);
                return item;
            }
        });
    });
</script>
<script type="text/javascript">
    $(function () {
        $('.fillvno').typeahead({
            hint: true,
            highlight: true,
            minLength: 1
            , source: function (request, response) {
                $.ajax({
                    url: '<%=ResolveUrl("Supplementary.aspx/GetVno") %>',
                    data: "{ 'prefix': '" + request + "'}",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        items = [];
                        map = {};
                        $.each(data.d, function (i, item) {
                            var id = item.split('|')[1];
                            var name = item.split('|')[0];
                            map[name] = { id: id, name: name };
                            items.push(name);
                        });
                        response(items);
                        $(".dropdown-menu").css("height", "auto");



                    },
                    error: function (response) {
                        alert(response.responseText);
                    },
                    failure: function (response) {
                        alert(response.responseText);
                    }
                });
            },
            updater: function (item) {
                //alert(map[item].id);
                $("#<%= hdvno.ClientID %>").val(map[item].id);
                return item;

            }
        });
    });
    </script>
<style type="text/css">
     .radioboxlist radioboxlistStyle
{
font-size:x-large;
padding-right: 20px;
}
.radioboxlist label {

white-space: nowrap;
clear: left;
margin-right: 10px;
margin-left: 10px;
}
.radioboxlist label:hover
{
color: #0097FF;

}
input:checked + label {
color: #0097FF;
}
</style>
<style type="text/css">
    .pading
    {
        padding :6px;
    }
    </style>
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
    <div id="wrapper">
		<!-- NAVBAR -->
		<nav class="navbar navbar-default navbar-fixed-top">
			<div  style ="padding :20px 30px; float:left;">
				<a href="#"><img src="../Lassets/images/logo.png" style="height:40px;" alt="Logo" class="img-responsive logo" /></a>                               
			</div>
			<div class="container-fluid">
				
				<div id="navbar-menu">
					<ul class="nav navbar-nav navbar-right">						
						<li >
							<a href="#"  style="color:#6d6d6d ;font-size:20px;"><b>Diamond CNF Granite</b> <span></span> </a>							
						</li>
						<li class="dropdown">
							<a href="#" class="dropdown-toggle" data-toggle="dropdown"><img src="../Lassets/images/favicon.png" class="img-circle" alt="icon"> <span><label  id="lbluser"  /><label  id="dpt"  /></span> <i class="icon-submenu lnr lnr-chevron-down"></i></a>
							<ul class="dropdown-menu">								
								<li><a href="../login.aspx"><i class="lnr lnr-exit"></i> <span>Logout</span></a></li>
							</ul>
						</li>						
					</ul>
				</div>
			</div>
		</nav>
		<!-- END NAVBAR -->
		<!-- LEFT SIDEBAR -->
		<div id="sidebar-nav" class="sidebar">
			<div class="sidebar-scroll">
				<nav>
					<ul class="nav">
						<li class="hidden"><a href="../DashBoard/DashBoard.aspx"><i class="fas fa-tachometer-alt"></i> <span>Dashboard</span></a></li>
						<li ><a href="#subPages" data-toggle="collapse"><i class="fas fa-database"></i><span>Essentials</span> <i class="icon-submenu lnr lnr-chevron-left"></i></a>
							<div id="subPages" class="collapse in">
								<ul class="nav">
                                    <li><a  href="../Essentials/Vessel.aspx">Vessel</a></li>
									<li><a  href="../Essentials/Shipper.aspx">Shipper</a></li>
									<li><a href="../Essentials/Consignee.aspx" >Consignee</a></li>									
                                    <li><a href="../Essentials/Liner.aspx">Liner</a></li>
                                    <li><a href="../Essentials/Port.aspx" >Port</a></li>
									<li><a href="../Essentials/Cf.aspx" >C & F Party</a></li>                                    									
								</ul>
							</div>
						</li>                        
                        <li>
							<a href="#subPages3" data-toggle="collapse" class="collapsed"><i class="fas fa-globe"></i> <span>Transaction</span> <i class="icon-submenu lnr lnr-chevron-left"></i></a>
							<div id="subPages3" class="collapse ">
								<ul class="nav">
									<li><a href="../Transaction/PackingListVessel.aspx" >Packing List Vessel </a></li>
                                    <li><a href="../Transaction/JobNoDetails.aspx">JobNo Details </a></li>
									<li><a href="../Transaction/ShippingBill.aspx">SB Details</a></li>
                                    <li><a href="../Transaction/Covering Letter.aspx" >Covering Letter</a></li>
                                    <%--<li><a href="../Transaction/Allocation.aspx">Allocate Job</a></li>
                                    <li><a href="../Transaction/Search.aspx" >Search </a></li>         --%>                           
								</ul>
							</div>
						</li>
                        <li>
							<a href="#subPages2" data-toggle="collapse" class="collapsed"><i class=" fa fa-file"></i> <span>Voucher</span> 
                            <i class="icon-submenu lnr lnr-chevron-left"></i></a>
							<div id="subPages2" class="collapse">
								<ul class="nav">
			                        <li><a href="../Voucher/Supplementary.aspx" class="active">Supplementary</a></li>            
								</ul>
							</div>
						</li>
                        <li><a href="../Reports/ReportTree.aspx"><i class="fas fa-chart-pie"></i><span>Reports</span></a></li>                        
					</ul>
				</nav>
			</div>
		</div>
		<!-- END LEFT SIDEBAR -->
		<!-- MAIN -->
		<div class="main">
			<!-- MAIN CONTENT -->
			<div class="main-content">
				<div class="container-fluid">
						<div class="clearfix">
                      <div class="clearfix">
                      
                        <div class="panel panel-default" >
<div class="panel-heading" style ="background-color:#ffd182;">
<font style="font-weight:bold;font-size:medium;">Cash Voucher</font>
</div>
    <div class="panel-body">
     <div class="row">
        <div class="col-sm-8" >
    <div class="row pading">
    <div class="col-sm-6" >Voucher No</div>
     <div class="col-sm-3" > <asp:TextBox ID="vno" runat="server" class="form-control  fillvno" AutoComplete="off"></asp:TextBox></div>
    <asp:HiddenField ID="hdvno" runat="server"  ></asp:HiddenField> 
     <div class="col-sm-3" ><asp:Button ID="filldetails"  class="btn btn-primary" runat="server" Text="Fill" /></div>
     <b style ="font-size:small">VOUCHER NO WILL BE CREATED IN NEW FORMAT LIKE    833/2021-2022  </b> 
          </div>
  <div class="row pading">
    <div class="col-sm-6" >Employee Name</div>
    <div class="col-sm-6" > <asp:TextBox ID="empname" runat="server" class="form-control  fillemp" AutoComplete="off"></asp:TextBox> 
         <asp:HiddenField ID="hdid" runat="server"  ></asp:HiddenField>  <asp:HiddenField ID="hfbtn" runat="server"   ></asp:HiddenField> </div> 
    </div>
       
           <div class="row pading">
            <div class="col-sm-6">Process Date</div>
      
    <div class="col-sm-6"><asp:TextBox ID="processdt" runat="server" class="form-control  tcal" AutoComplete="off"></asp:TextBox></div>
       <div class="col-sm-6" >&nbsp;</div>
        <div class="col-sm-6" >&nbsp;</div>
        </div>
          <div class="row pading">
        <div class="col-sm-6">Voucher Type</div>
        <div class="col-sm-6" >
            <asp:RadioButtonList ID="vouchertype" runat="server" RepeatDirection="Horizontal" CssClass="radioboxlist">
            <asp:ListItem Value="general" onclick="gendiv()" Selected="True">General</asp:ListItem>
              <asp:ListItem Value="partywise"  onclick="partydiv()">Party Wise</asp:ListItem>
                <asp:ListItem Value="jobno"  onclick="jobdiv()">Job No</asp:ListItem>
            </asp:RadioButtonList>
   
 
  
    </div>
    </div>
    
    </div>
      
      </div>
      <div class="row">
  <div class="col-sm-3 common"><asp:TextBox ID="chargedesc" runat="server" class="form-control  filldesc" AutoComplete="off" placeholder="Charge Desc"></asp:TextBox></div>
                 <div class="col-sm-3 divjob"><asp:TextBox ID="jobno" runat="server" class="form-control  filljob" AutoComplete="off" placeholder="Job No"></asp:TextBox><label id="lblcusname"  ></label></div> 
               <div class="col-sm-3 divcus"><asp:TextBox ID="cusname" runat="server" class="form-control  fillship" AutoComplete="off" placeholder="Cus Name"></asp:TextBox></div>
       <div class="col-sm-3 common"><asp:TextBox ID="voucheramt" runat="server" class="form-control autocalc" AutoComplete="off" placeholder="Voucher Amount"></asp:TextBox></div> 
         <div class="col-sm-3 common"><asp:TextBox ID="remarks" runat="server" class="form-control fillremarks" AutoComplete="off" placeholder="Remarks"></asp:TextBox></div> 
          
          <div class="col-sm-3 common"><asp:TextBox ID="taxamt" runat="server" class="form-control autocalc" AutoComplete="off" placeholder="Tax"></asp:TextBox></div> 
         <div class="col-sm-3 common"><asp:TextBox ID="grandamt" runat="server" class="form-control" AutoComplete="off" placeholder="Grand Amt"></asp:TextBox></div> 
         <div class="col-sm-3 common"><asp:TextBox ID="round" runat="server" class="form-control" AutoComplete="off" placeholder="Round Off"></asp:TextBox></div> 
          <div class="col-sm-3 common"><asp:TextBox ID="totamt" runat="server" class="form-control" AutoComplete="off" placeholder="Total"></asp:TextBox></div> 
          
          
           
   </div>
   <br />
   <div class="row text-center ">

           <div class="col-sm-12"><asp:Button ID="add"  class="btn btn-primary" runat="server" Text="Add" /></div>
         
   </div>
      <br />
   <div class="row">

           <asp:Gridview ID="Gridview1" runat="server" AutoGenerateColumns="false" Width="100%" OnRowCommand="RowCommand" class="table-bordered ">

        <Columns>
        <asp:BoundField DataField="name" HeaderText="No/Name" ItemStyle-Width="30" />
        <asp:BoundField DataField="voucheramt" HeaderText="Voucher Amount" ItemStyle-Width="150" />
        <asp:BoundField DataField="chargedesc" HeaderText="Charge Description" ItemStyle-Width="150" />
         <asp:BoundField DataField="remarks" HeaderText="Remarks" ItemStyle-Width="150" />

          <asp:BoundField DataField="taxamt" HeaderText="Tax" ItemStyle-Width="150" />
           <asp:BoundField DataField="grandamt" HeaderText="Grand Amt" ItemStyle-Width="150" />
            <asp:BoundField DataField="round" HeaderText="Round" ItemStyle-Width="150" />
             <asp:BoundField DataField="totamt" HeaderText="Total" ItemStyle-Width="150" />
       

               <asp:TemplateField headertext="Edit" HeaderStyle-Width ="5">
             <ItemTemplate >              
              <asp:ImageButton ID="Editcargo" runat="server" CommandName="change" ImageUrl="../Images/writing.png" CommandArgument="<%# Container.DataItemIndex%>"  ToolTip="Edit"  CausesValidation="false" />
                  </ItemTemplate>
              </asp:TemplateField>
                <asp:TemplateField headertext="delete" HeaderStyle-Width ="5">
              <ItemTemplate>
              <asp:ImageButton id="deletecargo"  ImageUrl="../Images/delete.png" CommandName="remove" runat="server" CommandArgument="<%# Container.DataItemIndex%>" ToolTip="Remove" CausesValidation="false"  />
            </ItemTemplate>
              </asp:TemplateField>
        </Columns>

</asp:Gridview>
           
   </div>      
    </div>   
    <div class="panel-footer">
    <div class="row">
 <div class="col-md-4">

  <asp:Button ID="submit"  class="btn btn-primary" runat="server" Text="Submit" />
      </div>
         <div class="col-md-4">
   <asp:Button ID="update"  class="btn btn-primary" runat="server" Text="Update" />
</div>
  <div class="col-md-4">
 
 <asp:Button ID="cancel" class="btn btn-primary" runat="server" Text="Cancel" />
  </div>
   
 </div>
    </div>                  
                       <%-- <div class="panel panel-default">
<div class="panel-heading" style ="background-color:#ffd182;">
<font style="font-weight:bold;font-size:medium;">Reports</font>
</div>
    <div class="panel-body" style="height:70%;">
      <div class="row">
     <div class="col-sm-4">
      <div class="container" style ="width :85%;margin-left:10px;">
  <ul class="file-tree">
    <li><a href="#">General</a>
      <ul>
        <li><a href="#" id="aegm">Export General Manifest</a></li>
        <li><a href="#" id="acnf">CNF Partywise Booking List Report</a></li>
        <li><a href="#" id="aweight">Weightwise Booking List Report</a></li>
      </ul>
      </li>
  </ul>
</div>
</div>
 <div class="col-sm-8">
               <div class="clsegm">
                <div class="row">
                <div class="col-sm-12 text-danger text-center" style="font-size:16px;"><u>Export General Manifest</u></div>
<br />
<br />
                 <div class="col-sm-2">Vessel Name</div> <div class="col-sm-4"><asp:TextBox ID="vname" runat="server" CssClass="clsautovessel" Width="200px" /></div>
                  <div class="col-sm-2">Port Name</div> <div class="col-sm-4"><asp:TextBox ID="pname" CssClass="clsautoport" runat="server" Width="200px"/></div>
             <br />
                <br />
                   <br />
                   <div class="col-sm-3"> </div>
                   <div class="col-sm-3"> </div>
                   <div class="col-sm-3"> <asp:Button ID="report" runat="server" Text="Report" class="btn btn-primary"/></div>
                <div class="col-sm-3"><asp:Button ID="cancel" runat="server" Text="Cancel" class="btn btn-secondary"/></div>
                </div>
               </div>
               <div class="clscnf">
                <div class="row" >
                <div class="col-sm-12 text-danger text-center" style="font-size:16px;"><u>CNF Partywise Booking List Report</u></div>
   
                  <br />
<br />
                 <div class="col-sm-2">Vessel Name</div> <div class="col-sm-4"><asp:TextBox ID="vesname" runat="server" CssClass="clsautovessel" Width="200px" /></div>
                  <div class="col-sm-2">Port Name</div> <div class="col-sm-4"><asp:TextBox ID="portname" CssClass="clsautoport" runat="server" Width="200px"/></div>
                  <br />
                  <div class="col-sm-2">CNF Party</div> <div class="col-sm-4"><asp:TextBox ID="cnfparty"  CssClass="clsautocnf" runat="server" Width="200px"/></div>
            
             <br />
                <br />
                   <br />
                   <div class="col-sm-3"> <asp:Button ID="tim2report" runat="server" Text="Report" class="btn btn-primary"/></div>
                <div class="col-sm-3"><asp:Button ID="tim2cancel" runat="server" Text="Cancel" class="btn btn-secondary"/></div>
                </div>
               </div>           
               <div class="clsweight">
                <div class="row" >
                <div class="col-sm-12 text-danger text-center" style="font-size:16px;"><u>Weightwise Booking List Report</u></div>
                   <br />
                <br />
                 <div class="col-sm-2">Vessel Name</div> <div class="col-sm-4"><asp:TextBox ID="TextBox1" CssClass="clsautovessel" runat="server" Width="200px" /></div>
                  <div class="col-sm-2">Port Name</div> <div class="col-sm-4"><asp:TextBox ID="TextBox2" CssClass="clsautoport" runat="server" Width="200px"/></div>
             <br />
             <br />

             <div class="col-sm-3"> <asp:Button ID="Button1" runat="server" Text="Report" class="btn btn-primary"/></div>
                <div class="col-sm-3"><asp:Button ID="Button2" runat="server" Text="Cancel" class="btn btn-secondary"/></div>
            </div>
            </div>
 </div>
            
     </div>
    </div>--%>

    
    <%--</div>
      
     </div>
     </div>--%>
      </div>
		</div>
			</div>
			<!-- END MAIN CONTENT -->
		</div>
		<!-- END MAIN -->
		<div class="clearfix"></div>

	</div>
    </form>
</body>

	<%--<script src="../assets/vendor/jquery/jquery.min.js"></script>--%>
	<script src="../assets/vendor/bootstrap/js/bootstrap.min.js"></script>
	<script src="../assets/vendor/jquery-slimscroll/jquery.slimscroll.min.js"></script>
	<script src="../assets/scripts/klorofil-common.js"></script>
    <script type="text/javascript" src="http://cdn.rawgit.com/bassjobsen/Bootstrap-3-Typeahead/master/bootstrap3-typeahead.min.js"></script>
        <script src="../assets/scripts/file-explore.js" type="text/javascript"></script>
    <link href="../assets/css/jquerysctiptop.css" rel="stylesheet" type="text/css" />
    <link href="../assets/css/file-explore.css" rel="stylesheet" type="text/css" />
</html>
