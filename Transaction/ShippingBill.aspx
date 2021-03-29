<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ShippingBill.aspx.vb" Inherits="CnfGranite.ShippingBill" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Shipping Bill Details</title>
    	<meta charset="utf-8">
	<meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1">
	<meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0">
    <script src="https://use.fontawesome.com/releases/v5.0.8/js/all.js" type="text/javascript"></script>
	<!-- VENDOR CSS -->
	 <link rel="Shortcut Icon" href="../Lassets/images/favicon.png"  />
    <link href="../assets/css/bootstrap.css" rel="stylesheet" type="text/css" />
	<link rel="stylesheet" href="../assets/vendor/font-awesome/css/font-awesome.min.css" />
	<link rel="stylesheet" href="../assets/vendor/linearicons/style.css" />
	<link rel="stylesheet" href="../assets/vendor/chartist/css/chartist-custom.css" />
	<!-- MAIN CSS -->
	<link rel="stylesheet" href="../assets/css/main.css">	
	<!-- GOOGLE FONTS -->
	<link href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,600,700" rel="stylesheet" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
     <link href="../tcal.css" rel="stylesheet" type="text/css" />
    <script src="../tcal.js" type="text/javascript"></script>
    <style>
    .footer
    {
       height:40px; width:100%; color:Blue; position :fixed; bottom:0px; z-index:-100;   
        }
        .row{padding:10px;}
    
    
    </style>
    <script type="text/javascript" >
        $(document).ready(function () {
        })
        $(document).ready(function () { $("input").attr("autocomplete", "off"); }); 
    </script>
    <script type="text/javascript">
        $(function () {
            $('[id*=vessname]').typeahead({
                hint: true,
                highlight: true,
                minLength: 1
            , source: function (request, response) {

                $.ajax({
                    url: '<%=ResolveUrl("ShippingBill.aspx/Getvessel") %>',
                    data: "{'prefix':'" + $("#vessname").val() + "'}",
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
<script  type="text/javascript">
    function validation() 
        {
            var vessname = document.getElementById("vessname").value;
            var portname = document.getElementById("portname").value;
            if (vessname == "") 
             {
             alert("Enter the vessel Name");
            return false;
            }
            if (portname == "") 
            {
            alert("Enter the Port name");
            return false;
            }
            return true; 
            }
    </script>
 <script type="text/javascript">
     $(function () {
         $('[id*=portname]').typeahead({
             hint: true,
             highlight: true,
             minLength: 1
            , source: function (request, response) {

                $.ajax({
                    url: '<%=ResolveUrl("ShippingBill.aspx/Getport") %>',
                    data: "{'prefix':'" + $("#portname").val() + "'}",
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
							<a href="#" class="dropdown-toggle" data-toggle="dropdown"><img src="../Lassets/images/favicon.png" class="img-circle" alt="icon"><span>
                            <asp:Label ID="asplbluser" runat="server"></asp:Label><label  id="dpt"  /></span> <i class="icon-submenu lnr lnr-chevron-down"></i></a>
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
						<li ><a href="#subPages" data-toggle="collapse" class="collapsed"><i class="fas fa-database"></i> <span>Essentials</span> <i class="icon-submenu lnr lnr-chevron-left"></i></a>
							<div id="subPages" class="collapse">
								<ul class="nav">
                                    <li><a href="../Essentials/Vessel.aspx">Vessel</a></li>
									<li><a href="../Essentials/Shipper.aspx">Shipper</a></li>
									<li><a href="../Essentials/Consignee.aspx" >Consignee</a></li>									
                                    <li><a href="../Essentials/Liner.aspx">Liner</a></li>
                                    <li><a href="../Essentials/Port.aspx" >Port</a></li>
									<li><a href="../Essentials/Cf.aspx" >C & F Party</a></li>                                    									
								</ul>
							</div>
						</li>                        
                        <li>
							<a href="#subPages3" data-toggle="collapse" class="active"><i class="fas fa-globe"></i> <span>Transaction</span> <i class="icon-submenu lnr lnr-chevron-left"></i></a>
							<div id="subPages3" class="collapse in ">
								<ul class="nav">
									<li><a href="../Transaction/PackingListVessel.aspx">Packing List Vessel </a></li>
                                    <li><a href="../Transaction/JobNoDetails.aspx">JobNo Details</a></li>
									<li><a class="active" href="../Transaction/ShippingBill.aspx">SB Details</a></li>
                                    <li><a href="../Transaction/Covering Letter.aspx">Covering Letter</a></li>
                                    <%--<li><a href="../Transaction/Allocation.aspx">Allocate Job</a></li>
                                    <li><a href="../Transaction/Search.aspx">Search </a></li>    --%>                                
								</ul>
							</div>
						</li>
                        <%--<li>
							<a href="#subPages2" data-toggle="collapse" class="collapsed"><i class=" fa fa-file"></i> <span>Voucher</span> <i class="icon-submenu lnr lnr-chevron-left"></i></a>
							<div id="subPages2" class="collapse">
								<ul class="nav">
			                        <li><a href="../Voucher/Supplementary.aspx">Supplementary</a></li>            
								</ul>
							</div>
						</li>--%>
                        <li><a href="../Reports/ReportTree.aspx"><i class="fas fa-chart-pie"></i> <span>Reports</span></a></li>                        
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
                <div class="lead"><b>Shipping Bill Details</b></div>
					<div class="panel panel-profile">
                    <br />
                        <div class="row">
                        <div class="col-md-2">Vessel Name </div><div class="col-md-4"><asp:TextBox ID="vessname" class="form-control " autocomplete="off" runat="server"></asp:TextBox></div>
                        <div class="col-md-2">Port of Destination </div><div class="col-md-4"><asp:TextBox ID="portname" class="form-control " autocomplete="off" runat="server"></asp:TextBox></div>
                        </div>
                        <div class="row">
                        <div class="col-md-6 text-right"><asp:Button ID="showdetails" runat="server" Text="Show Details" class="btn btn-success"  OnClientClick="return validation()" /></div>  <div class="col-md-4"></div>                     
                        </div>
                        <div class="row">                       
                        <div class="col-md-12"> 
                        <asp:HiddenField
                           ID="hdid" runat="server" />                             
                        
                  <asp:GridView ID="gvDetails" runat="server" AutoGenerateColumns ="False"        
                   EmptyDataText="No Data Found" >       
                   <headerstyle backcolor="#df5015" />
       <columns>
      <asp:templatefield headertext="Refno" headerstyle-backcolor="#00bfff">
<itemtemplate>
<asp:label id="gvlblrefno" text='<%# eval("Refno") %>' runat="server"/>
</itemtemplate>
</asp:templatefield>
    <asp:templatefield headertext="packing no" headerstyle-backcolor="#00bfff">
<itemtemplate>
<asp:label id="gvlblpno" text='<%# eval("packing_listno") %>' runat="server"/>
</itemtemplate>
</asp:templatefield>
<asp:templatefield headertext="marks" headerstyle-backcolor="#00bfff">
<itemtemplate>
<asp:label id="gvlblmarks" text='<%# eval("packing_marks") %>' runat="server" />
</itemtemplate>
</asp:templatefield>
<asp:templatefield headertext="noofblocks" headerstyle-backcolor="#00bfff">
<itemtemplate>
<asp:label id="gvlblnblocks" text='<%# eval("blocknumber") %>' runat="server"/>
</itemtemplate>
</asp:templatefield>
<asp:templatefield headertext="description of goods"  headerstyle-backcolor="#00bfff">
<itemtemplate>
<asp:label id="gvlblgoods" text='<%# eval("packing_description") %>' runat="server"/>
</itemtemplate>
</asp:templatefield>
<asp:templatefield headertext="shipper"  headerstyle-backcolor="#00bfff">
<itemtemplate>
<asp:label id="gvlblshippername" text='<%# eval("packing_shippername ") %>' runat="server"/>
</itemtemplate>
</asp:templatefield>
<asp:templatefield headertext="consignee"  headerstyle-backcolor="#00bfff">
<itemtemplate>
<asp:label id="gvlblconname" text='<%# eval("packing_consigneename") %>' runat="server"/>
</itemtemplate>
</asp:templatefield>
<asp:templatefield headertext="cbm"  headerstyle-backcolor="#00bfff">
<itemtemplate>
<asp:label id="gvlblcbm" text='<%# eval("volumeofcbt" ) %>' runat="server"/>
</itemtemplate>
</asp:templatefield>
<asp:templatefield headertext="gross wt"  headerstyle-backcolor="#00bfff">
<itemtemplate>
<asp:label id="gvlblgrosswt" text='<%# eval("weightmt") %>' runat="server"/>
</itemtemplate>
</asp:templatefield>
<asp:templatefield headertext="edit" headerstyle-backcolor="#00bfff">
<itemtemplate>
       <asp:imagebutton id="imgedit" runat="server" commandname="Edit"  imageurl="~/assets/img/edit.png"  tooltip="Edit" commandargument='<%#eval("Refno")%>' causesvalidation="false"/>
       
       </itemtemplate>
       </asp:templatefield>                                                                                      
       </columns>
       </asp:Gridview>                                           
                            
   </div>
    </div>
  <div class="row">
  <div class="col-md-2">Shp Bill Number </div>
  <div class="col-md-2">Shp date </div>
  <div class="col-md-2">BL Number </div>
  <div class="col-md-2">BL date </div>
  <div class="col-md-2">Total B/L weight </div>
  <div class="col-md-2">Freight </div>
  </div>
  <div class="row">
  <div class="col-md-2"><asp:TextBox ID="shpblno" class="form-control " autocomplete="off" runat="server"></asp:TextBox></div>
  <div class="col-md-2"><asp:TextBox ID="shpdate" autocomplete="off" class="tcal" runat="server"></asp:TextBox></div>
  <div class="col-md-2"><asp:TextBox ID="blno" class="form-control " autocomplete="off" runat="server"></asp:TextBox></div>
  <div class="col-md-2"><asp:TextBox ID="bldate" autocomplete="off"  class="tcal"  runat="server"></asp:TextBox></div>
  <div class="col-md-2"><asp:TextBox ID="totalblwt" class="form-control " autocomplete="off" runat="server"></asp:TextBox></div>
  <div class="col-md-2"><asp:TextBox ID="freight2" class="form-control " autocomplete="off" runat="server"></asp:TextBox></div>
  </div>  
  <div class="row">
  <div class="col-md-6 text-right ">
  <asp:Button ID="save" runat="server" Text="Update" class="btn btn-success" />
  </div><div class="col-md-6"><asp:Button ID="exit" runat="server" class="btn btn-danger" Text="Exit" />
  </div>
  </div> 
  </div>                      
      
      <br />                        
						<div class="clearfix"></div>
					</div>

				</div>
			</div>
			<!-- END MAIN CONTENT -->
		</div>
		<!-- END MAIN -->
        
		<div class="clearfix"></div>
        <footer>
			<div class="container-fluid">
				<p class="copyright">&copy; 2019 <a href="http://diamondshipping.in" target="_blank">Designed & Maintained by IT dept</a>. All Rights Reserved.</p>
			</div>
		</footer>
	</div>
   <%--<asp:SqlDataSource ID="shippingdsi" runat="server" ConnectionString="<%$ConnectionStrings:MydbConn%>"
SelectCommand="select Refno,sb_no,format( sb_date,'dd/MM/yyyy')as sb_date,bl_number,format( bl_date,'dd/MM/yyyy')as bl_date,tot_bl_wt,freight from granite_packinglistheader " >
</asp:SqlDataSource>--%>
    </form>
</body>
<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
    <script src="../assets/vendor/jquery/jquery.min.js"></script>
	<script src="../assets/vendor/bootstrap/js/bootstrap.min.js"></script>
	<script src="../assets/vendor/jquery-slimscroll/jquery.slimscroll.min.js"></script>
	<script src="../assets/scripts/klorofil-common.js"></script>
    <script type="text/javascript" src="http://cdn.rawgit.com/bassjobsen/Bootstrap-3-Typeahead/master/bootstrap3-typeahead.min.js"></script>
</html>