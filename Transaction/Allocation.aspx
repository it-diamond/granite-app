<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Allocation.aspx.vb" Inherits="CnfGranite.Allocation" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Allocate Job</title>
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
						<li class="hidden"><a href="../DashBoard.aspx" class="active"><i class="fas fa-tachometer-alt"></i> <span>Dashboard</span></a></li>
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
							<a href="#subPages3" data-toggle="collapse" class="collapsed"><i class="fas fa-globe"></i> <span>Transaction</span> <i class="icon-submenu lnr lnr-chevron-left"></i></a>
							<div id="subPages3" class="collapse in ">
								<ul class="nav">
									<li><a href="../Transaction/PackingListVessel.aspx" >Packing List Vessel </a></li>
                                    <li><a href="../Transaction/JobNoDetails.aspx" >JobNo Details </a></li>
                                     <li><a href="../Transaction/PackingListContainer.aspx" >Covering Letter</a></li>
									<li><a href="../Transaction/ShippingBill.aspx">SB Details</a></li>
                                    <li><a class="active" href="../Transaction/Allocation.aspx">Allocate Job</a></li>
                                    <li><a href="../Transaction/Search.aspx" >Search </a></li>                                    
								</ul>
							</div>
						</li>
                        <li>
							<a href="#subPages2" data-toggle="collapse" class="collapsed"><i class=" fa fa-file"></i> <span>Voucher</span> <i class="icon-submenu lnr lnr-chevron-left"></i></a>
							<div id="subPages2" class="collapse">
								<ul class="nav">
			                        <li><a href="../Voucher/Supplementary.aspx">Supplementary</a></li>            
								</ul>
							</div>
						</li>
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
                <div class="lead"><b>Job Allocation</b></div>
					<div class="panel panel-profile">
                    <br />
                        <div class="row">
                        <div class="col-md-2">Job No: </div><div class="col-md-4"><p class="text-danger ">Will be added shortly</p></div>
                        <div class="col-md-2">Type</div>  <div class="col-md-4">
                            <asp:RadioButtonList ID="RadioButtonList1" RepeatDirection="Horizontal" Width ="200px"  runat="server">
                           <asp:ListItem Selected ="true" > Vessel</asp:ListItem>
                            <asp:ListItem  > Container</asp:ListItem>
                             
                            </asp:RadioButtonList>
                        </div>                     
                        </div>
                        <div class="row">
                        <div class="col-md-2">Vessel Name</div><div class="col-md-4"><asp:TextBox ID="vesno" class="form-control " autocomplete="off" runat="server"></asp:TextBox></div>
                        <div class="col-md-2"><asp:Button ID="Button2" runat="server" Text="View" class="btn btn-success" /></div><div class="col-md-4"></div>
                        </div>
                          <br />                        
						<div class="clearfix"></div>
					</div>



                    <div class="panel panel-profile">
                    <div class="vessel">
                  <div class="row">
                        <div class="col-md-2">Invoice No</div><div class="col-md-4"><asp:TextBox ID="TextBox1" class="form-control " autocomplete="off" runat="server"></asp:TextBox></div>
                        <div class="col-md-2">Invoice Date</div><div class="col-md-4"><asp:TextBox ID="TextBox2" class="form-control " autocomplete="off" runat="server"></asp:TextBox></div>
                        </div>
                        <div class="row">
                        <div class="col-md-2">Total CBM</div><div class="col-md-4"><asp:TextBox ID="TextBox3" class="form-control " autocomplete="off" runat="server"></asp:TextBox></div>
                        <div class="col-md-2">Total Weight</div><div class="col-md-4"><asp:TextBox ID="TextBox4" class="form-control " autocomplete="off" runat="server"></asp:TextBox></div>
                        </div>
                        <div class="row">
                        <div class="col-md-2">No of Blocks</div><div class="col-md-4"><asp:TextBox ID="TextBox5" class="form-control " autocomplete="off" runat="server"></asp:TextBox></div>
                        <div class="col-md-2">Stuffing Date</div><div class="col-md-4"><asp:TextBox ID="TextBox6" class="form-control " autocomplete="off" runat="server"></asp:TextBox></div>
                        </div>
                        </div>
                        <br /><br />
                        <div class="container">
                       Container grid
                        
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
    </form>
</body>
    <script src="../assets/vendor/jquery/jquery.min.js"></script>
	<script src="../assets/vendor/bootstrap/js/bootstrap.min.js"></script>
	<script src="../assets/vendor/jquery-slimscroll/jquery.slimscroll.min.js"></script>
	<script src="../assets/scripts/klorofil-common.js"></script>
</html>