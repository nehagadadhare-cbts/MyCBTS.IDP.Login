<%@ Page Title="Admin" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="InvoiceType.aspx.cs" Inherits="InvoiceType" MaintainScrollPositionOnPostback="true" %>

<%@ Register Src="~/Controls/ErrorSuccessNotifier.ascx" TagPrefix="ucadmin" TagName="ErrorSuccessNotifier" %>

<asp:Content ID="BodyContent1" ContentPlaceHolderID="MainContent" runat="server">

    <script src="../Scripts/jquery-1.10.2.min.js"></script>
    <script src="../Scripts/ErrorSuccessNotifier.js"></script>
    <script src="../Scripts/chargevalidation.js"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />

    <link href="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.6-rc.0/css/select2.min.css" rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.6-rc.0/js/select2.min.js"></script>
    

    <script type="text/javascript">
        $(document).ready(function () {
            var acc = $('.accordion');
            var i;
            for (i = 0; i < acc.length; i++) {
                acc[i].onclick = function (e) {
                    this.classList.toggle("active");
                    this.nextElementSibling.classList.toggle("show");
                    var hiddenfield = this.previousElementSibling;
                    if (hiddenfield.value == "true") {
                    hiddenfield.value = "false";
                    }
                    else {
                    hiddenfield.value = "true";
                    }
                }
                //on page load
                if (acc[i].previousElementSibling.value == "true") {
                    acc[i].classList.toggle("active");
                    acc[i].nextElementSibling.classList.toggle("show");
                }
            }
           
            //$("#txtCSGprofileChargeEffectiveStartDate").datepicker();         
           
        });

        function HidePopupInvoice() {
            $find("mpeNewInvoiceType").hide();
            return false;
        }

        function HidePopupSearch() {
            $find("mdlSearchUserResult").hide();
            return false;
        }

        function HidePopupAddAcount() {
            $find("mpeAddAccount").hide();
            return false;
        }

        function HidePopupEditAcount() {
            $find("mpeEditAccount").hide();
            return false;
        }

        function HidePopupAddProfile() {
            $find("mpeAddProfile").hide();
            return false;
        }

        function HidePopupAddProfileCharge() {
            $find("mpeCreateProfileCharge").hide();
            return false;
        }

        <%-- SERO-1582--%>

        function HidePopupAddProfileChargeCSG() {
            $find("mpeCreateProfileChargeCSG").hide();
            return false;
        }

        function HidePopupUpload() {
            $find("mpeManualUpload").hide();
            return false;
    }

        window.onload = function () { ToggleDDLBeforeBillcycle();  FixedScrollPosition(); };
        //ToggleDDLAfterBillcycle();
    </script>
    <style>
        .ajax__calendar_day {
            cursor: pointer;
            height: 22px;
            padding: 0 2px;
            text-align: center;
            width: 18px;
        }

        .tblInvoiceDetails th {
            padding: 17px;
        }
    </style>

    <script type="text/javascript">

       function SetButtonStatus(sender, target) {
            var first = document.getElementById('<%=txtProfileChargeGLCode.ClientID %>');
            if ((sender.value.length < 1 && first.value.length < 1)) {
                document.getElementById('<%= this.BtnSaveProfileCharge.ClientID %>').disabled = true;
                return false;
            }
            else {
                document.getElementById('<%= this.BtnSaveProfileCharge.ClientID %>').disabled = false;
                $('#' + '<%= this.BtnSaveProfileCharge.ClientID %>').attr('title', '');
                return false;
            }
       }

        function ToggleDDLBeforeBillcycle() {
            if ((document.getElementById('chkEnableAutoPreBilling') !== null) && (document.getElementById('chkEnableAutoPreBilling').checked)) {
                document.getElementById('rowDaysBeforeBillcycle').style.display = '';
            } else {
                document.getElementById('rowDaysBeforeBillcycle').style.display = 'none';
            }

        }

        //function ToggleDDLAfterBillcycle() {

        //    if ((document.getElementById('chkEnableAutoPostBilling') !== null) && (document.getElementById('chkEnableAutoPostBilling').checked)) {                
        //        document.getElementById('rowDaysAfterBillcycle').style.display = '';      
        //    } else {
        //        document.getElementById('rowDaysAfterBillcycle').style.display = 'none';
        //    }
        //}

        function FTPPathCheck() {
            debugger;
            if (document.getElementById('chkFTPCheckBox').checked) {
                document.getElementById('txtDefaultftp').disabled = '';
                document.getElementById('txtFTPUsername').disabled = '';
                document.getElementById('txtFTPPassword').disabled = '';

            } else {

                document.getElementById('txtDefaultftp').disabled = 'disabled';
                document.getElementById('txtFTPUsername').disabled = 'disabled';
                document.getElementById('txtFTPPassword').disabled = 'disabled';

            }

        }

        function ChildPaysCheck() {
            debugger;
            if (document.getElementById('chkChildPays').checked) {
                document.getElementById('lblEditUserName').disabled = '';
                document.getElementById('txtPassword').disabled = '';
                document.getElementById('txtEmail').disabled = '';
                document.getElementById('chkEDI').disabled = '';
                document.getElementById('txtFTP').disabled = '';

            } else {

                document.getElementById('lblEditUserName').disabled = 'disabled';
                document.getElementById('txtPassword').disabled = 'disabled';
                document.getElementById('txtEmail').disabled = 'disabled';
                document.getElementById('chkEDI').disabled = 'disabled';
                document.getElementById('txtFTP').disabled = 'disabled';

            }

        }

        function CheckChildPays() {
            debugger;
            if (document.getElementById('checkChildPays').checked) {
                document.getElementById('textUserName').disabled = '';
                document.getElementById('textPassword').disabled = '';
                document.getElementById('textEmail').disabled = '';
                document.getElementById('checkEDI').disabled = '';
                document.getElementById('textFTP').disabled = '';

            } else {

                document.getElementById('textUserName').disabled = 'disabled';
                document.getElementById('textPassword').disabled = 'disabled';
                document.getElementById('textEmail').disabled = 'disabled';
                document.getElementById('checkEDI').disabled = 'disabled';
                document.getElementById('textFTP').disabled = 'disabled';
                document.getElementById('checkEDI').Checked = 'false';
            }

        }

        function RadioCheck(rb) {
            var gv = document.getElementById("<%=grdSearchUserResult.ClientID%>");
            var rbs = gv.getElementsByTagName("input");

            <%--var addUser = document.getElementById("<%=btnAddUser.ClientID%>");
            addUser.setAttribute("disabled", "false");--%>
            //var btnAddUser = addUser.getElementsByTagName("button");
            document.getElementById('btnAddUser').style.display = 'inherit';
            var row = rb.parentNode.parentNode;
            for (var i = 0; i < rbs.length; i++) {
                if (rbs[i].type == "radio") {
                    if (rbs[i].checked && rbs[i] != rb) {
                        rbs[i].checked = false;
                        break;
                    }
                }   
            }
        }

        $(document).ready(function () {
            $('.ddlTypeSearch').select2();
            
              $(".ddlCSGChargeStringSearch").select2({
                dropdownParent: $("#divProfileChargepopup")
            });
            });
         
        function FixedScrollPosition() {
            var div = document.getElementById("divScroll");
            var div_position = document.getElementById("div_position");
            var position = parseInt('<%=Request.Form["div_position"] %>');
            if (isNaN(position)) {
                position = 0;
            }
            div.scrollTop = position;
            div.onscroll = function () {
                div_position.value = div.scrollTop;
            };
        }

        function ScrollBottom() {
            window.scrollTo(0, 20000)
        }

    </script>

    <style>

        /*.customerbody{
    overflow-y: initial !important
}*/

   .customerbody{
        height: 500px;
        overflow-y: auto;
        overflow-x:hidden;
    }
    
    .customerhr { 
      display: block;
      margin-top: 0em;
      margin-bottom: 0em;
      margin-left: auto;
      margin-right: auto;
      border-style: inset;
      border-width: 1px;
    } 


    .select2-selection__rendered {   
    background: #E48D0F;    
    }

    .js-example-basic-single:hover {
        border-top-color: #28597a;
        background: black;
        color: #ccc;
    }

    .js-example-basic-single:active {
        border-top-color: black;
        background: black;
    }

    .select2-results__option {
    background: black;
    color: white;
    text-align:left;
    }

    .select2-container--default .select2-selection--single .select2-selection__rendered {
    color: white; 
    line-height: 28px;
    border-radius: 5px;
    }

    .select2-container--default .select2-selection--single .select2-selection__arrow b {
    border-color: white transparent transparent transparent;    
    }
   
    </style>

    <br />
    <div class="main">
        <ucadmin:ErrorSuccessNotifier runat="server" ID="ErrorSuccessNotifier" />
    </div>
    <div id="info"></div>
    <%-- Invoice Type Section--%>
    <div id="divInvoiceSelection" runat="server" >
        <div class="jumbotron" style="background-color: #E48D0F; padding: 10px">
            <h4 style="color: floralwhite">Customer</h4>
        </div>
        <div>
            <table style="width: 1100px">
                <tr>
                    <td style="padding-left: 20px; width: 20%">
                        <%--<label>Invoice Type:</label>--%>
                        <label>Customer:</label>
                    </td>
                    <td style="width: 60%">
                        <asp:DropDownList ID="ddlInvoiceType" runat="server" Width="500px" class="ddlTypeSearch" AutoPostBack="True" OnSelectedIndexChanged="ddlInvoiceType_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="btnNewInvoiceType" runat="server" Width="150px" class="buttonNewInvoice" Text="New Customer" OnClick="btnNewInvoiceType_Click" CausesValidation="False" />
                        <asp:HiddenField ID="hfNewInvoiceType" runat="server" />
                        <ajaxToolkit:ModalPopupExtender ID="mpeNewInvoiceType" BehaviorID="mpeNewInvoiceType" runat="server" PopupControlID="pnlNewInvoiceTypePopup"
                            TargetControlID="hfNewInvoiceType" BackgroundCssClass="modalBackground">
                        </ajaxToolkit:ModalPopupExtender>
                    </td>
                </tr>

            </table>
            <asp:Panel ID="pnlNewInvoiceTypePopup" runat="server" CssClass="modalPopup" Style="display: none; position: relative; top: 10px">
                <a onclick="return HidePopupInvoice()" href="#close"
                    title="Close" class="close">X</a>
                <div class="header">
                    <h4>
                        <asp:Label ID="lblheaderInvoiceType" runat="server" Text="New Customer"></asp:Label></h4>
                </div>
                <div class="body">
                    <div class="customerbody"> 
                        <asp:Label ID="lblNewInvoiceTypeExeception" runat="server" Visible="false" Font-Bold="true" ForeColor="Red"></asp:Label>
                        <table class="tblmodel customerbody">
                            <tr>
                                <td>
                                    <label id="lblNewInvoiceType">Customer:</label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNewInvoiceType" runat="server" ValidationGroup="Create" Height="32px" CausesValidation="true" TabIndex="1"></asp:TextBox>
                                    <span style="color:red">*</span>
                                    <asp:RequiredFieldValidator ID="rfvtxtNewInvoiceType" runat="server" ControlToValidate="txtNewInvoiceType" ErrorMessage="Please enter InvoiceType!" ForeColor="Red" ValidationGroup="Create" Text="Please enter InvoiceType!" Visible="True"></asp:RequiredFieldValidator>
                                    <%--<asp:RegularExpressionValidator ID="revtxtNewInvoiceType" runat="server" ValidationExpression="^([A-z][A-Za-z]*\s*[A-Za-z]*\s*[A-Za-z]*\s*)$" ControlToValidate="txtNewInvoiceType" ValidationGroup="Create"
                                        ErrorMessage="Only letters are allowed"></asp:RegularExpressionValidator>--%>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label id="Label1">Default BAN:</label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDefaultBan" runat="server" ValidationGroup="Create" Height="32px" CausesValidation="true" TabIndex="2"></asp:TextBox>
                                    <span style="color:red">*</span>
                                    <asp:RequiredFieldValidator ID="rfvtxtDefaultBan" runat="server" ControlToValidate="txtDefaultBan" ErrorMessage="Please enter BAN!" ForeColor="Red" ValidationGroup="Create" Text="Please enter BAN!" Visible="True"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revtxtDefaultBan" runat="server" ValidationExpression="^([ A-Za-z0-9_@./#&+-]*)$" ControlToValidate="txtDefaultBan" ValidationGroup="Create"
                                        ErrorMessage="Only alphanumerics are allowed"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label id="lblPrefixCode">Prefix Code:</label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPrefixCode" runat="server" ValidationGroup="Create" Height="32px" CausesValidation="true" TabIndex="3" MaxLength="50"></asp:TextBox>
                                    <span style="color:red">*</span>
                                    <asp:RequiredFieldValidator ID="rfvtxtPrefixCode" runat="server" ControlToValidate="txtPrefixCode" ErrorMessage="Please enter PrefixCode!" ForeColor="Red" ValidationGroup="Create" Text="Please enter PrefixCode!" Visible="True"></asp:RequiredFieldValidator>
<%--                                    <asp:RegularExpressionValidator ID="revtxtPrefixCode" runat="server" ValidationExpression="^([A-z][A-Za-z]*\s*[A-Za-z]*)$" ControlToValidate="txtPrefixCode" ValidationGroup="Create"
                                        ErrorMessage="Only letters are allowed"></asp:RegularExpressionValidator>--%>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label id="lblVendorName">Vendor Name:</label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtVendorName" runat="server" ValidationGroup="Create" Height="32px" CausesValidation="true" TabIndex="4"></asp:TextBox>
                                    <span style="color:red">*</span>
                                    <asp:RequiredFieldValidator ID="rfvtxtVendorName" runat="server" ControlToValidate="txtVendorName" ErrorMessage="Please enter VendorName!" ForeColor="Red" ValidationGroup="Create" Text="Please enter VendorName!" Visible="True"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="revtxtVendorName" runat="server" ValidationExpression="^([A-z][A-Za-z]*\s*[A-Za-z]*)$" ControlToValidate="txtVendorName" ValidationGroup="Create"
                                        ErrorMessage="Only letters are allowed"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label id="lblImportCurrency">Import Currency:</label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlImportCurrency" Width="153px" runat="server" TabIndex="5">
                                    </asp:DropDownList>                                   
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="New" ControlToValidate="ddlImportCurrency" ErrorMessage="Please enter VendorName!" ForeColor="Red" Text="Please enter VendorName!" Visible="True"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationGroup="New" ValidationExpression="^([A-z][A-Za-z]*\s*[A-Za-z]*)$" ControlToValidate="ddlImportCurrency"
                                        ErrorMessage=""></asp:RegularExpressionValidator>
                                </td>

                            </tr>
                            <tr>
                                <td>
                                    <label id="lblExportCurrency">Export Currency:</label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlExportCurrency" Width="153px" runat="server" TabIndex="6">
                                    </asp:DropDownList>                                
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="New" ControlToValidate="ddlImportCurrency" ErrorMessage="Please enter VendorName!" ForeColor="Red" Text="Please enter VendorName!" Visible="True"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ValidationGroup="New" ValidationExpression="^([A-z][A-Za-z]*\s*[A-Za-z]*)$" ControlToValidate="ddlImportCurrency"
                                        ErrorMessage=""></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label id="lblFTPCheckExist">Do you have FTP Path?</label>
                                </td>
                                <td>
                                    <%--<input type="checkbox" ID="lblFTPCheckBox"  onclick="FTPPathCheck()" />--%>
                                    <asp:CheckBox ID="chkFTPCheckBox" runat="server" Checked="false" ClientIDMode="Static" onclick="FTPPathCheck()" />

                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label id="lblDefaultftp">Default FTP:</label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDefaultftp" runat="server" Height="32px" TabIndex="7" Enabled="false" ClientIDMode="Static"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="New" ControlToValidate="ddlImportCurrency" ErrorMessage="Please enter VendorName!" ForeColor="Red" Text="Please enter VendorName!" Visible="True"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ValidationGroup="New" ValidationExpression="^([A-z][A-Za-z]*\s*[A-Za-z]*)$" ControlToValidate="ddlImportCurrency"
                                        ErrorMessage=""></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label id="lblFTPUsername">FTP UserName:</label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFTPUsername" runat="server" Height="32px" TabIndex="8" Enabled="false" ClientIDMode="Static"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label id="lblFTPPassword">FTP Password:</label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtFTPPassword" TextMode="Password" runat="server" Height="32px" TabIndex="9" Enabled="false" ClientIDMode="Static"></asp:TextBox>
                                </td>
                            </tr>

                            <%--Automation--%>
                            <tr>
                                <td>
                                    <label id="lblEnableAutoPreBilling">Enable Auto Pre Billing?</label>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkEnableAutoPreBilling" runat="server" ClientIDMode="Static" Checked="true"  onclick="ToggleDDLBeforeBillcycle()"  />
                                </td>
                            </tr>
                            
                            <tr ID="rowDaysBeforeBillcycle" >
                                <td>
                                    <label id="Label2">Days Before Billcycle:</label>
                                </td>
                                <td>
                                    <asp:DropDownList ClientIDMode="Static" ID="ddlDaysBeforeBillcycle" Width="153px" runat="server" TabIndex="6">
                                        <asp:ListItem Text="1" Value="1"></asp:ListItem> 
                                        <asp:ListItem Text="2" Value="2"></asp:ListItem> 
                                        <asp:ListItem Text="3" Value="3"></asp:ListItem> 
                                        <asp:ListItem Text="4" Value="4"></asp:ListItem> 
                                        <asp:ListItem Text="5" Value="5"></asp:ListItem> 
                                        <asp:ListItem Text="6" Value="6" Selected="True"></asp:ListItem> 
                                        <asp:ListItem Text="7" Value="7"></asp:ListItem> 
                                        <asp:ListItem Text="8" Value="8"></asp:ListItem> 
                                        <asp:ListItem Text="9" Value="9"></asp:ListItem> 
                                        <asp:ListItem Text="10" Value="10"></asp:ListItem>                                                                                                                                                   
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            
                            <tr>
                                <td>
                                    <label id="lblEnableAutoPostBilling">Enable Auto Post Billing?</label>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkEnableAutoPostBilling" runat="server"  ClientIDMode="Static" Checked="true"  /> <%--onclick="ToggleDDLAfterBillcycle()"--%>
                                </td>
                            </tr>

                             <%--CBE_8967--%>
                            <tr ID="lblOutputFileFormat" >
                                <td>
                                    <label id="Label3">Output Format:</label>
                                </td>
                                <td>
                                    <asp:DropDownList ClientIDMode="Static" ID="ddlOutputFileFormat" Width="153px" runat="server" TabIndex="6">
                                        <asp:ListItem Text="1 Combined File" Value="1"></asp:ListItem> 
                                        <asp:ListItem Text="5 Individual Files" Value="5" Selected="True"></asp:ListItem>                                                                                                                                                                                          
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label id="lblEmailAddressNew">Email Address:</label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEmailAddress" runat="server" ValidationGroup="Create" Height="32px" CausesValidation="true" TabIndex="1"></asp:TextBox>
                                    <span style="color:red">*</span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtEmailAddress" ErrorMessage="Please enter Email Id" ForeColor="Red" ValidationGroup="Create" Text="Please enter Email Id" Visible="True"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ValidationExpression="^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtEmailAddress" ValidationGroup="Create"
                                        ErrorMessage="Email Address is not valid"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label id="lblEDINew">EDI:</label>
                                </td>
                                <td>
                                    <asp:CheckBox ID="chkEDINew" runat="server"  ClientIDMode="Static" Checked="false"  />
                                </td>
                            </tr>

                            <%--CBE_11609--%>
                            <tr ID="lblIsSOO" >
                                <td>
                                    <label id="Label4">Is SOO:</label>
                                </td>
                                <td>
                                    <asp:DropDownList ClientIDMode="Static" ID="ddlIsSOO" Width="153px" runat="server" TabIndex="6">
                                        <asp:ListItem Text="True" Value="1"></asp:ListItem> 
                                        <asp:ListItem Text="False" Value="0" Selected="True"></asp:ListItem>                                                                                                                                                                                          
                                    </asp:DropDownList>
                                </td>
                            </tr>


                            <%--SERO-1582--%>
                           <tr ID="lblBillingSystem" >
                                <td>
                                    <label id="Label4">Billing System:</label>
                                </td>
                                <td>
                                    <asp:DropDownList ClientIDMode="Static" ID="ddlBillingSystem" Width="153px" runat="server" AutoPostBack="true"  TabIndex="6" OnSelectedIndexChanged="ddlBillingSystem_SelectedIndexChanged">
                                        <asp:ListItem Text="Select" Value="Select" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="CRM" Value="CRM" ></asp:ListItem> 
                                        <asp:ListItem Text="CSG" Value="CSG" ></asp:ListItem>                                                                                                                                                                                          
                                    </asp:DropDownList>
                                    <span style="color:red">*</span>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlBillingSystem" ErrorMessage="Please Select Billing System" InitialValue="Select" ForeColor="Red" ValidationGroup="Create" Text="Please Select Billing System" Visible="True"></asp:RequiredFieldValidator>
                                </td>
                            </tr>

                            <tr ID="lblautomationFrequency" >
                                <td>
                                    <label id="Lable6">Automation Frequency:</label>
                                </td>
                                <td>
                                    <asp:DropDownList ClientIDMode="Static" ID="ddlautomationFrequency" Width="153px" runat="server" TabIndex="6">
                                      <%--  <asp:ListItem Text="Select" Value="Select" Selected="True"></asp:ListItem> --%>
                                        <asp:ListItem Text="Weekly" Value="Weekly" ></asp:ListItem> 
                                        <asp:ListItem Text="Fortnightly" Value="Fortnightly" ></asp:ListItem>
                                        <asp:ListItem Text="Monthly" Value="Monthly" Selected="True"></asp:ListItem>                                                                                                                                                                                            
                                    </asp:DropDownList>
                                </td>
                            </tr>

                            <%--Ser0#3511 Start--%>
                           <div id="divGL" runat="server" visible="false"   >
                            <tr>
                                <td>
                                     <asp:label id="lblContractNumber" runat="server">Contract Number:</asp:label>
                                </td>
                                <td>
                                     <asp:TextBox ID="txtContractNumber" runat="server" Height="32px"  CausesValidation="true" TabIndex="2"></asp:TextBox>
                                </td>
                            </tr>

                             <tr>
                                <td>
                                     <asp:label id="lblContractStartDate" runat="server">Contract Start Date:</asp:label>
                                </td>
                                <td>
                                        <asp:TextBox ID="txtContractStartDate" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender runat="server" TargetControlID="txtContractStartDate" Format="MM-dd-yyyy hh:mm:ss" />
                                        <%--<asp:RequiredFieldValidator ID="rfvProfileChargeAmount" runat="server" ControlToValidate="txtprofileChargeAmount" ErrorMessage="Please enter positive value!" ForeColor="Red" ValidationGroup="AddProfileCharge" Text="*" Visible="True"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revDecimals" runat="server" ErrorMessage="Only Decimals With Precision Less Than 2" ControlToValidate="txtprofileChargeAmount" ValidationExpression="^\d+(\.\d{1,2})?$"></asp:RegularExpressionValidator>
                                        <asp:RangeValidator ID="rvprofilechargAmount" runat="server" MinimumValue="0.01" ErrorMessage="provide minimum value" ValidationGroup="AddProfileCharge"></asp:RangeValidator>--%>
                                </td>
                            </tr>  

                            <tr>
                                <td>
                                     <asp:label id="lblContractEndDate" runat="server" >Contract End Date:</asp:label>
                                </td>
                                <td>
                                        <asp:TextBox ID="txtContractEndDate" runat="server" AutoCompleteType="Disabled" ></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender runat="server" TargetControlID="txtContractEndDate" Format="MM-dd-yyyy hh:mm:ss" />
                                        <%--<asp:RequiredFieldValidator ID="rfvProfileChargeAmount" runat="server" ControlToValidate="txtprofileChargeAmount" ErrorMessage="Please enter positive value!" ForeColor="Red" ValidationGroup="AddProfileCharge" Text="*" Visible="True"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="revDecimals" runat="server" ErrorMessage="Only Decimals With Precision Less Than 2" ControlToValidate="txtprofileChargeAmount" ValidationExpression="^\d+(\.\d{1,2})?$"></asp:RegularExpressionValidator>
                                        <asp:RangeValidator ID="rvprofilechargAmount" runat="server" MinimumValue="0.01" ErrorMessage="provide minimum value" ValidationGroup="AddProfileCharge"></asp:RangeValidator>--%>
                                </td>
                            </tr>                                                                            

                            <tr>
                                <td>
                                     <asp:label id="lblIndirectPartnerOrRepCode" runat="server" >Indirect Partner Or RepCode:</asp:label>
                                </td>
                                <td>
                                     <asp:TextBox ID="txtIndirectPartnerOrRepCode" runat="server" Height="32px" CausesValidation="true" TabIndex="2"></asp:TextBox>
                                </td>
                            </tr>

                           <%-- <tr>
                                <td>
                                     <label id="lblGLDepartmentCode">GL Department Code:</label>
                                </td>
                                <td>
                                     <asp:TextBox ID="txtGLDepartmentCode" runat="server" ValidationGroup="Create" Height="32px" CausesValidation="true" TabIndex="2"></asp:TextBox>
                                    <span style="color:red">*</span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtVendorName" ErrorMessage="Please enter GL Department Code!" ForeColor="Red" ValidationGroup="Create" Text="Please enter GL Department Code!" Visible="True"></asp:RequiredFieldValidator>
                                </td>
                            </tr>--%>

                         <%--   <tr ID="lblGLDepartmentCode" >
                                <td>
                                    <label id="lblGLDepartmentCodes">GL Department Code:</label>
                                </td>
                                <td>
                                    <asp:DropDownList ClientIDMode="Static" ID="ddlGLDepartmentCode" Width="153px" runat="server" TabIndex="6">
                                        <asp:ListItem Text="Select" Value="Select" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="CRM" Value="CRM" ></asp:ListItem> 
                                        <asp:ListItem Text="CSG" Value="CSG" ></asp:ListItem>                                                                                                                                                                                          
                                    </asp:DropDownList>
                                </td>
                            </tr>--%>

                            <tr>
                                <td>
                                    <asp:label id="lblGLDepartmentCode"  runat="server">GL Department Code:</asp:label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlGLDepartmentCode" runat="server"  Width="275px"  AutoPostBack="true" OnSelectedIndexChanged="ddlGLDepartmentCode_SelectedIndexChanged" ></asp:DropDownList>
                                
                                     <span style="color:red">*</span>
                                     <asp:RequiredFieldValidator ID="reqddlGLDepartmentCode" runat="server" ControlToValidate="ddlGLDepartmentCode" ErrorMessage="Please Select GL Department Code" InitialValue="0" ForeColor="Red" ValidationGroup="Create" Text="Please Select GL Department Code" Visible="True"></asp:RequiredFieldValidator>
                                
                                </td>
                            </tr>

                              <tr>
                                <td>
                                    <asp:label id="lblIndirectAgentRegion" runat="server" >Indirect Agent Region:</asp:label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlIndirectAgentRegion" runat="server" Width="275px" AutoPostBack="true" OnSelectedIndexChanged="ddlIndirectAgentRegion_SelectedIndexChanged"></asp:DropDownList>
                                
                                  <%--    <span style="color:red">*</span>--%>
                                     <asp:RequiredFieldValidator ID="reqddlIndirectAgentRegion" Enabled="false" runat="server" ControlToValidate="ddlIndirectAgentRegion" ErrorMessage="Please Select Indirect Agent Region" InitialValue="0" ForeColor="Red" ValidationGroup="Create" Text="Please Select Indirect Agent Region" Visible="True"></asp:RequiredFieldValidator>
                                
                                </td>
                            </tr>

                          </div>
                            <%-- <tr ID="lblIndirectAgentRegion" >
                                <td>
                                    <label id="lblIndirectAgentRegion">Indirect Agent Region:</label>
                                </td>
                                <td>
                                    <asp:DropDownList ClientIDMode="Static" ID="ddlIndirectAgentRegion" Width="153px" runat="server" TabIndex="6">
                                        <asp:ListItem Text="Select" Value="Select" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="CRM" Value="CRM" ></asp:ListItem> 
                                        <asp:ListItem Text="CSG" Value="CSG" ></asp:ListItem>                                                                                                                                                                                          
                                    </asp:DropDownList>
                                </td>
                            </tr>--%>


                          <%--  <tr>
                                <td>
                                     <label id="lblIndirectAgentRegion">Indirect Agent Region</label>
                                </td>
                                <td>
                                     <asp:TextBox ID="txtIndirectAgentRegion" runat="server" Height="32px" CausesValidation="true" TabIndex="2"></asp:TextBox>
                                </td>
                            </tr>--%>

                            <%--Ser0#3511 END--%>

                            <%--SERO-1582--%>
                            <%--<tr ID="rowDaysAfterBillcycle" >
                                <td>
                                    <label id="Label3">Days After Billcycle:</label>
                                </td>
                                <td>
                                    <asp:DropDownList ClientIDMode="Static" ID="ddlDaysAfterBillcycle" Width="153px" runat="server" TabIndex="6">
                                        <asp:ListItem Text="1" Value="1"></asp:ListItem> 
                                        <asp:ListItem Text="2" Value="2" Selected="True"></asp:ListItem> 
                                        <asp:ListItem Text="3" Value="3"></asp:ListItem> 
                                        <asp:ListItem Text="4" Value="4"></asp:ListItem> 
                                        <asp:ListItem Text="5" Value="5"></asp:ListItem> 
                                        <asp:ListItem Text="6" Value="6"></asp:ListItem> 
                                        <asp:ListItem Text="7" Value="7"></asp:ListItem> 
                                        <asp:ListItem Text="8" Value="8"></asp:ListItem> 
                                        <asp:ListItem Text="9" Value="9"></asp:ListItem> 
                                        <asp:ListItem Text="10" Value="10"></asp:ListItem>     
                                    </asp:DropDownList>                                    
                                </td>
                            </tr>--%>                                                                                                              
                        </table>
                    </div>
                    <%--<br />--%>
                    <hr class="customerhr">
                    <asp:Button ID="btnCreate" runat="server" Text="Create" CssClass="yes" OnClick="btnCreate_Click" ValidationGroup="Create" CausesValidation="true" TabIndex="10" />
                </div>
            </asp:Panel>
        </div>
        <br />
    </div>

    <%--<div id="divInvoiceType" runat="server">
        <div>
            <asp:GridView ID="grdInvoiceType" class="tblInvoiceDetails" runat="server" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField HeaderText="Customer" DataField="Name" />
                    <asp:BoundField HeaderText="Default BAN" DataField="BAN" />
                    <asp:BoundField HeaderText="Prefix Code" DataField="Prefix" />
                    <asp:BoundField HeaderText="Vendor Name" DataField="VendorName" />
                    <asp:BoundField HeaderText="Import $" DataField="ImportCurrencyDefault" />
                    <asp:BoundField HeaderText="Export $" DataField="ExportCurrencyDefault" />
                    <asp:BoundField HeaderText="Defaultftp" DataField="DefaultFTP" />

                    <asp:BoundField HeaderText="Auto Pre Biling" DataField="IsAutoPreBilling" />
                    <asp:BoundField HeaderText="Days Before Bill Cycle" DataField="DaysBeforeBillCycle" />
                    <asp:BoundField HeaderText="Auto Post Biling" DataField="IsAutoPostBilling" />
                    <asp:BoundField HeaderText="Days After Bill Cycle" DataField="DaysAfterBillCycle" />
                </Columns>
            </asp:GridView>
            <br />
            <asp:Button ID="btnEdit" class="buttonNewInvoice" runat="server" Text="Edit" Visible="false" OnClick="btnEdit_Click" CausesValidation="False" />
        </div>
    </div>--%>

    <div id="divInvoiceType" runat="server">
            <table class="tblInvoiceDetails">
                <tr>
                    <th style="width: 20%"></th>
                    <th style="width: 25%"></th>
                    <th style="width: 10%"></th>
                    <th style="width: 20%"></th>
                    <th style="width: 25%"></th>                    
                </tr>
                <tr>
                    <td>
                        <label>Customer:</label></td>
                    <td>
                        <asp:Label runat="server" ID="lblCustomer" /></td>
                    <td></td>
                    <td>
                        <label>Default BAN:</label></td>
                    <td>
                        <asp:Label runat="server" ID="lblDefaultBAN" /></td>                    
                </tr>
                <tr>
                    <td>
                        <label>Prefix Code:</label></td>
                    <td>
                        <asp:Label runat="server" ID="lblPrefix" /></td>
                    <td></td>
                    <td>
                        <label>Vendor Name:</label></td>
                    <td>
                        <asp:Label runat="server" ID="lblVendor" /></td>                    
                </tr>
                <tr>
                    <td>
                        <label>Import $:</label></td>
                    <td>
                        <asp:Label runat="server" ID="lblImport" /></td>
                    <td></td>
                    <td>
                        <label>Export $:</label></td>
                    <td>
                        <asp:Label runat="server" ID="lblExport" /></td>                   
                </tr>
                <tr>
                    <td>
                        <label>Default ftp:</label></td>
                    <td>
                        <asp:Label runat="server" ID="lblDefaultftpPath" /></td>
                    <td></td>
                    <%--CBE_8967--%>
                    <td>
                        <label>Output File Format:</label></td>
                    <td>
                        <asp:Label runat="server" ID="lblOutputFormat" /></td>                    
                </tr> 
                
                <tr>
                    <td>
                        <label>Auto Pre Biling:</label></td>
                    <td>
                        <asp:Label runat="server" ID="lblAutoPreBiling" /></td>
                    <td></td>
                    <td>

                         <label>Auto Post Biling:</label></td>
                       
                    <td>
                        <asp:Label runat="server" ID="lblAutoPostBiling" /></td>                    
                </tr>  
                <tr>
                    <td>
                        <label>Days Before Bill Cycle:</label></td>
                    <td>
                        <asp:Label runat="server" ID="lblDaysBeforeBillCycle" /></td>
                    <td></td>
                    <td>
                        <label>Days After Bill Cycle:</label></td>
                    <td>
                        <asp:Label runat="server" ID="lblDaysAfterBillCycle" /></td>   
                                     
                </tr>  
                <tr>
                    <td>
                        <label>Email Address:</label></td>
                    <td>
                        <asp:Label runat="server" ID="lblEmailAddress" /></td>
                    <td></td>
                    <td>
                        <label>EDI:</label></td>
                    <td>
                        <asp:Label runat="server" ID="lblEDI" /></td>                    
                </tr>            
                <%--cbe_11609--%>
                <tr>
                    <td>
                        <label>Is SOO:</label></td>
                    <td>
                        <asp:Label runat="server" ID="IsSOOlabel" /></td>
                    <td></td>                   
                </tr>  
                <%--SERO-1582--%>
                 <td>
                       <label>Automation Frequency:</label>

                 </td>   
                 <td>
                        <asp:Label runat="server" ID="lblautomationFrequency" />

                 </td> 
                 <td>
                 </td>
                 <td>
                        <label>Billing System:</label>
                 </td>
                 <td>
                        <asp:Label runat="server" ID="lblBillingSystem" />
                 </td>   
                                     
                </tr>  
         <%-- Sero 3511 start --%>   
                 <div id="csgAdminDiv" runat="server" visible="false">
                <tr>
                    <td>
                        <label>Contract Number:</label></td>
                    <td>
                        <asp:Label runat="server" ID="lblAdminContractNumber" /></td>  
                    <td></td>    
                    <td>
                        <label>GL Department Code:</label></td>
                    <td>
                        <asp:Label runat="server" ID="lblAdminGLDepartmentCode" /></td>
                    <td></td>                                  
                </tr>       
                <tr>
                    <td>
                        <label>Contract Start Date:</label></td>
                    <td>
                        <asp:Label runat="server" ID="lblAdminContractStartDate" /></td>
                    <td></td>
                    <td>
                        <label>Contract End Date:</label></td>
                    <td>
                        <asp:Label runat="server" ID="lblAdminContractEndDate" /></td>                    
                </tr>   
                <tr>
                    <td>
                        <label>Indirect Partner Or RepCode:</label></td>
                    <td>
                        <asp:Label runat="server" ID="lblAdminIndirectPartnerOrRepCode" /></td>
                    <td></td>
                    <td>
                        <label>Indirect Agent Region</label></td>
                    <td>
                        <asp:Label runat="server" ID="lblAdminIndirectAgentRegion" /></td>                    
                </tr>   
                </div>
               <%-- Sero 3511 End --%>            

            </table>        
        </div>
    <br />
    <asp:Button ID="btnEdit" class="buttonNewInvoice" runat="server" Text="Edit" Visible="false" OnClick="btnEdit_Click" CausesValidation="False" />
    <br />
    
    <br />
    
    <div id="divAssociates" runat="server" visible="false">
        <%--Associated Users Section--%>
        <%--<div class="jumbotron" style="background-color: #E48D0F; padding: 10px">
            <h4 style="color: floralwhite">Associated Users</h4>
        </div>--%>
        <asp:HiddenField ID="hdnAssociatedUsers" runat="server" Value="false" />
        <a id="accAssociatedUsers" runat="server" class="accordion"  >Associated Users</a>
        <div id="divAssociatedUsers" runat="server" class="panel"  >
            <br />
            <asp:Panel DefaultButton="btnSearchUser" ID="pnlUsers" runat="server">
            <div>
                <asp:GridView ID="grdAssociateUsers" runat="server" DataKeyNames="AssociatedUserId" class="tblInvoiceDetails" AutoGenerateColumns="false" OnRowDeleting="grdAssociateUsers_OnRowDeleting" OnRowDataBound="grdAssociatedUsers_RowDataBound">
                    <Columns>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblAssociatedUserId" runat="server" Text='<%#Eval("AssociatedUserId") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="User Id" DataField="UserId" />
                        <asp:BoundField HeaderText="First Name" DataField="UserFirstName" />
                        <asp:BoundField HeaderText="Last Name" DataField="UserLastName" />
                        <asp:BoundField HeaderText="User Role" DataField="UserRole" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnDeleteUser" class="buttonNewInvoice" runat="server" CommandName="Delete" Text="Delete User" OnClientClick="return confirm('Are you sure you want to delete the User?');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <br />
                <table style="width: 1100px">
                    <tr>
                        <td style="padding-left: 20px; width: 20%">
                            <asp:TextBox ID="txtSearchUser" runat="server"></asp:TextBox>
                        </td>
                        <td style="width: 20%">
                            <asp:Button ID="btnSearchUser" class="buttonNewInvoice" Width="175px" runat="server" Text="Search User" OnClick="btnSearchUser_Click" CausesValidation="false" />
                            <ajaxToolkit:ModalPopupExtender ID="mdlSearchUserResult" BehaviorID="mdlSearchUserResult" runat="server" PopupControlID="pnlSearchUserResult"
                                TargetControlID="dummyHidden" BackgroundCssClass="modalBackground">
                            </ajaxToolkit:ModalPopupExtender>
                            <asp:HiddenField ID="dummyHidden" runat="server" />
                        </td>
                        <td></td>
                    </tr>
                </table>
            </div>
            </asp:Panel>
        </div>
        <%--Associated Accounts Section--%>

        <%--<div class="jumbotron" style="background-color: #E48D0F; padding: 10px">
            <h4 style="color: floralwhite">Associated Accounts</h4>
        </div>--%>
        <asp:HiddenField ID="hdnAssociatedAccounts" runat="server" Value="false" />
        <a id="achAssociateAccounts" runat="server" class="accordion"  >Associated Accounts</a>
        <div id="divAssociateAccounts" runat="server" class="panel"  >
            <br />
            <div>
            <asp:GridView ID="grdAssociateAccounts" runat="server" DataKeyNames="AccountNumber" AutoGenerateColumns="false" class="tblInvoiceDetails" OnRowDeleting="grdAssociateAccounts_RowDeleting" OnRowEditing="grdAssociateAccounts_RowEditing" OnSelectedIndexChanged="grdAssociateAccounts_SelectedIndexChanged">
                <Columns>
                    <asp:TemplateField HeaderText="AccountNumber">
                        <ItemTemplate>
                            <asp:Label ID="lblAccount" runat="server" Text='<%#Eval("AccountNumber") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="AccountName">
                        <ItemTemplate>
                            <asp:Label ID="lblAccountName" runat="server" Text='<%#Eval("AccountName") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ChildPays">
                        <ItemTemplate>
                            <asp:Label ID="lblChildPays" runat="server" Text='<%#Eval("ChildPays") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Parent">
                        <ItemTemplate>
                            <asp:Label ID="lblParent" runat="server" Text='<%#Eval("ParentAccountNumber") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ParentAccountName">
                        <ItemTemplate>
                            <asp:Label ID="lblParentAccountName" runat="server" Text='<%#Eval("ParentAccountName") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="BillCycle" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblBillCycle" runat="server" Text='<%#Eval("AccountBillCycle") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Provisioning Identifier">
                        <ItemTemplate>
                            <asp:Label ID="lblStringIdentifier" Width="170px" runat="server" Style="word-wrap: normal; word-break: break-all" Text='<%#Eval("StringIdentifier") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                        <asp:TemplateField HeaderText="Effective Billing Date">
                            <ItemTemplate>
                                <asp:Label ID="lblDateIdentifier" runat="server" Text='<%#Eval("EffectiveBillingDate") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    <asp:TemplateField HeaderText="User Name" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblUserName" runat="server" Text='<%#Eval("UserName") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Password" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblPassword" runat="server" Text='<%#Eval("Password") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="FTP" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblFTP" runat="server" Text='<%#Eval("FTP") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="EmailId" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblEmailId" runat="server" Text='<%#Eval("EmailId") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="EDI" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblEDI" runat="server" Text='<%#Eval("EDI") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CreateAccount" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblCreateAccount" runat="server" Text='<%#Eval("CreateAccount") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnAssoicateAccountEdit" runat="server" Text="Edit" class="buttonNewInvoice" CommandName="Edit" CausesValidation="false" OnClick="btnAssoicateAccountEdit_Click" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnDeleteAssoicateAccount" class="buttonNewInvoice" runat="server" CausesValidation="false" CommandName="Delete" Text="Delete" OnClientClick="return confirm('Are you sure want to delete Association of the Account?');" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <br />
            <table style="width: 1100px">
                <tr>
                    <td>
                        <asp:Button ID="btnAddAccount" class="buttonNewInvoice" Width="150px" runat="server" Text="Associate Account" OnClick="btnAddAccount_Click" CausesValidation="false" />
                        <asp:HiddenField ID="hfAddAccount" runat="server" OnValueChanged="hfAddAccount_ValueChanged" />
                        <ajaxToolkit:ModalPopupExtender ID="mpeAddAccount" BehaviorID="mpeAddAccount" runat="server" PopupControlID="pnlAddAcount"
                            TargetControlID="hfAddAccount" BackgroundCssClass="modalBackground">
                        </ajaxToolkit:ModalPopupExtender>
                        <asp:HiddenField ID="hfEditAccount" runat="server" />
                        <ajaxToolkit:ModalPopupExtender ID="mpeEditAccount" BehaviorID="mpeEditAccount" runat="server" PopupControlID="pnlEditAcount"
                            TargetControlID="hfEditAccount" BackgroundCssClass="modalBackground">
                        </ajaxToolkit:ModalPopupExtender>
                    </td>
                </tr>
            </table>
        </div>
        </div>
        <%--Associated Files Section--%>

        <%--<div class="jumbotron" style="background-color: #E48D0F; padding: 10px">
            <h4 style="color: floralwhite">Associated Files</h4>
        </div>--%>
        <asp:HiddenField ID="hdnAssociatedFiles" runat="server" Value="false" />
        <a id="accAssociatedFiles" runat="server" class="accordion"  >Associated Files</a>
        <div id="divAssociatedFiles" runat="server" class="panel"  >
            <br />
            <div>
                <asp:GridView ID="grdFileType" runat="server" DataKeyNames="AssociatedFileId" AutoGenerateColumns="false" 
                    class="tblInvoiceDetails" OnRowDeleting="grdFileType_OnRowDeleting">
                    <Columns>
                        <asp:TemplateField HeaderText="AssociatedFileId" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblAssociatedFileId" runat="server" Text='<%#Eval("AssociatedFileId") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="File">
                            <ItemTemplate>
                                <asp:Label ID="lblFileType" runat="server" Text='<%#Eval("FileType") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnDeleteAssoicateFile" class="buttonNewInvoice" runat="server" CausesValidation="false" CommandName="Delete" Text="Delete" OnClientClick="return confirm('Are you sure want to delete the FileType?');" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <br />
            <div>
            <table style="width: 1100px">
                <tr>
                    <td style="padding-left: 20px; width: 20%">
                        <asp:Label ID="lbldropdownFileType" runat="server" Text="FileType:" Font-Bold="True"></asp:Label>
                    </td>
                    <td style="width: 20%">
                        <asp:DropDownList ID="ddlFileType" class="buttonNewInvoice" Width="250px" runat="server"></asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="btnAddFileType" class="buttonNewInvoice" Width="150px" runat="server" Text="Add File Type" OnClick="btnAddFileType_Click" CausesValidation="False" />
                    </td>
                </tr>
            </table>
        </div>
        </div>

        <%-- Profile Section --%>
        <asp:HiddenField ID="hdnProfiles" runat="server" Value="false" />
        <a id="accProfiles" runat="server" class="accordion"  >Profiles</a>
        <div id="divProfiles" runat="server" class="panel"  >
            <br />
            <div>
            <asp:GridView ID="grdProfiles" runat="server" DataKeyNames="profileId" AutoGenerateColumns="false" 
                class="tblInvoiceDetails" OnRowDeleting="grdProfiles_RowDeleting" OnRowEditing="grdProfiles_RowEditing">
                <Columns>
                    <asp:TemplateField HeaderText="Profile Name">
                        <ItemTemplate>
                            <asp:Label ID="lblProfileName" runat="server" Text='<%#Eval("ProfileName") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Profile Description">
                        <ItemTemplate>
                            <asp:Label ID="lblDescription" runat="server" Text='<%#Eval("Description") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnProfileEdit" runat="server" Text="Edit" class="buttonNewInvoice" CommandName="Edit" CausesValidation="false" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnDeleteProfile" class="buttonNewInvoice" runat="server" CausesValidation="false" CommandName="Delete" Text="Delete" OnClientClick="return confirm('Are you sure to delete the Profile and associated Profile Charges from MBM?');" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <br />
            <table style="width: 1100px">
                <tr>
                    <td>
                        <asp:Button ID="btnAddProfileDailog" class="buttonNewInvoice" Width="150px" runat="server" Text="Add Profile" CausesValidation="false" OnClick="btnAddProfileDailog_Click"/>
                        <asp:HiddenField ID="hfAddProfile" runat="server" />
                        <ajaxToolkit:ModalPopupExtender ID="mpeAddProfile" BehaviorID="mpeAddProfile" runat="server" PopupControlID="pnlAddProfile"
                            TargetControlID="hfAddProfile" BackgroundCssClass="modalBackground">
                        </ajaxToolkit:ModalPopupExtender>
                    </td>
                </tr>
            </table>
            </div>
        </div>

        <%-- Profile Charges Section --%>
        <asp:HiddenField ID="hdnProfileCharges" runat="server" Value="false" ClientIDMode="Static" />
        <a id="accProfileCharges" runat="server" class="accordion" >Profiles Charges</a>
        <div id="divprofileCharges" runat="server" class="panel" >
           <br />
            <div>            
            <div id="divScroll" style="width: auto; max-height:450px; overflow-x:hidden; overflow-y:scroll;" > <%--onscroll="$get('hdnScrollTop').value = this.scrollTop;"--%>
            <asp:GridView ID="grdProfileCharges" ClientIDMode="Static" runat="server" DataKeyNames="ChargeId" AutoGenerateColumns="false" class="tblInvoiceDetails" OnRowDeleting="grdProfileCharges_RowDeleting" OnRowEditing="grdProfileCharges_RowEditing">
                <Columns>  
                    
                                 
                  <asp:TemplateField HeaderText="ProfileName">
                        <ItemTemplate>
                            <asp:Label ID="lblProfileName" runat="server" Text='<%#Eval("ProfileName") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>  
                    <asp:TemplateField HeaderText="GL Code">
                        <ItemTemplate>
                            <asp:Label ID="lblChargeCode" runat="server" Text='<%#Eval("GLCode") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Feature Code">
                        <ItemTemplate>
                            <asp:Label ID="lblFeature" runat="server" Text='<%#Eval("Feature") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Charge Description">
                        <ItemTemplate>
                            <asp:Label ID="lblChargeType" runat="server" Text='<%#Eval("ChargeDescription") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Charge">
                        <ItemTemplate>
                            <asp:Label ID="lblChargeAmount" runat="server" Text='<%#Eval("ChargeAmount") %>' />
                        </ItemTemplate>
                    </asp:TemplateField> 
                  
                   
                     <%-- <asp:TemplateField HeaderText="Profile Charge Identifier">
                        <ItemTemplate>
                            <asp:Label ID="lblProfileIdentifier" runat="server" Text='<%#Eval("ProfileIdentifier") %>' />
                        </ItemTemplate>
                    </asp:TemplateField> --%>

                   
                    
                    <asp:TemplateField>
                        <ItemTemplate>
                            <input type="hidden" id="div_position" name="div_position" />
                            <asp:Button ID="btnProfileChargeEdit" runat="server" Text="Edit" class="buttonNewInvoice" CommandName="Edit" CausesValidation="false" OnClick="btnProfileChargeEdit_Click" />                            
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnProfileChargeDelete" class="buttonNewInvoice" runat="server" CausesValidation="false" CommandName="Delete" Text="Delete" OnClientClick="return confirm('Are you sure want to delete Association of the Account?');" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>                
            </asp:GridView>
            <br />
             <table style="width: 1100px">
                <tr>
                    <td>
                        <asp:Button ID="btnProfileChargePopDailog" class="buttonNewInvoice" Width="150px" runat="server" Text="Add Profile Charge" CausesValidation="false" OnClick="btnProfileChargePopDailog_Click"/>
                        <asp:HiddenField ID="hfAddprofileChagreId" runat="server" />
                        <ajaxToolkit:ModalPopupExtender ID="mpeCreateProfileCharge" BehaviorID="mpeCreateProfileCharge" runat="server" PopupControlID="pnlAddProfileCharge"
                            TargetControlID="hfAddprofileChagreId" BackgroundCssClass="modalBackground">
                        </ajaxToolkit:ModalPopupExtender>
                    </td>
                </tr>
            </table>

            </div>
            </div>
        </div>


         <%-- Profile Charges for CSG Section SERO-1582 starts --%> 
        <asp:HiddenField ID="HiddenField1" runat="server" Value="false" ClientIDMode="Static" />
        <a id="accProfileChargesCSG" runat="server" class="accordion" >Profiles Charges</a>
        <div id="divprofileChargesCSG" runat="server" class="panel" >
           <br />
            <div>            
            <div id="div2" style="width: auto; max-height:450px; overflow-x:hidden; overflow-y:scroll;" > <%--onscroll="$get('hdnScrollTop').value = this.scrollTop;"--%>
            <asp:GridView ID="grdProfileChargesCSG" ClientIDMode="Static" runat="server" DataKeyNames="ChargeId" AutoGenerateColumns="false" class="tblInvoiceDetails" OnRowDeleting="grdProfileCharges_RowDeletingCSG" OnRowEditing="grdProfileCharges_RowEditing">
                <Columns>  
                    
                    <%--<asp:TemplateField HeaderText="ID">
                        <ItemTemplate>
                            <asp:Label ID="lblChargeType" runat="server" Text='<%#Eval("Id") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                                 
                <%--  <asp:TemplateField HeaderText="Product Id">
                        <ItemTemplate>
                            <asp:Label ID="lblProfileName" runat="server" Text='<%#Eval("ProductId") %>' />
                        </ItemTemplate>
                    </asp:TemplateField> --%> 

                     <asp:TemplateField HeaderText="ProfileName">
                        <ItemTemplate>
                            <asp:Label ID="lblProfileName" runat="server" Text='<%#Eval("ProfileName") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>  

                     <asp:TemplateField HeaderText="Charge Description">
                        <ItemTemplate>
                            <asp:Label ID="lblChargeType" runat="server" Text='<%#Eval("ChargeDescription") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Charge">
                        <ItemTemplate>
                            <asp:Label ID="lblChargeAmount" runat="server" Text='<%#Eval("ChargeAmount") %>' />
                        </ItemTemplate>
                    </asp:TemplateField> 

                        <%--<asp:TemplateField HeaderText="Effective Start Date">
                        <ItemTemplate>
                            <asp:Label ID="lblEffectiveStartDate" runat="server" Text='<%#Eval("EffectiveStartDate") %>' />
                        </ItemTemplate>
                    </asp:TemplateField> --%>

                          <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnProfileChargeEdit" class="buttonNewInvoice" runat="server" CausesValidation="false" CommandName="Edit" Text="Edit" OnClientClick="btnProfileChargeEdit_Click" />
                        </ItemTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField>
                        <ItemTemplate>
                            <%--<asp:HiddenField ID="HiddenFields4" Text='<%# Eval(Session["grdEditedRow"], "") %>' runat="server" />--%>
                            <asp:Button ID="btnProfileChargeDelete" class="buttonNewInvoice" runat="server" CausesValidation="false" CommandName="Delete" Text="Delete" OnClientClick="return confirm('Are you sure want to delete Association of the Account?');" />
                        </ItemTemplate>
                    </asp:TemplateField>

                  

                   <%-- <asp:TemplateField HeaderText="BillerRuleConfigurationId">
                        <ItemTemplate>
                            <asp:Label ID="lblChargeCode" runat="server" Text='<%#Eval("BillerRuleConfigurationId") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Pricing Plan Id">
                        <ItemTemplate>
                            <asp:Label ID="lblFeature" runat="server" Text='<%#Eval("PricingPlanId") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                <asp:TemplateField HeaderText="Price">
                        <ItemTemplate>
                            <asp:Label ID="lblChargeAmount" runat="server" Text='<%#Eval("Price") %>' />
                        </ItemTemplate>
                    </asp:TemplateField> --%>
                  
                   
                     <%-- <asp:TemplateField HeaderText="Profile Charge Identifier">
                        <ItemTemplate>
                            <asp:Label ID="lblProfileIdentifier" runat="server" Text='<%#Eval("ProfileIdentifier") %>' />
                        </ItemTemplate>
                    </asp:TemplateField> --%>

                   
                    
<%-- <asp:TemplateField>
                        <ItemTemplate>
                            <input type="hidden" id="div_position" name="div_position" />
                            <asp:Button ID="btnProfileChargeEdit" runat="server" Text="Edit" class="buttonNewInvoice" CommandName="Edit" CausesValidation="false" OnClick="btnProfileChargeEdit_Click" />                            
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnProfileChargeDelete" class="buttonNewInvoice" runat="server" CausesValidation="false" CommandName="Delete" Text="Delete" OnClientClick="return confirm('Are you sure want to delete Association of the Account?');" />
                        </ItemTemplate>
                    </asp:TemplateField>
    --%>
                </Columns>                
            </asp:GridView>
            <br />
             <table style="width: 1100px">
                <tr>
                    <td>
                        <asp:Button ID="Button1" class="buttonNewInvoice" Width="150px" runat="server" Text="Add Profile Charge" CausesValidation="false" OnClick="btnProfileChargePopDailog_Click"/>
                        <asp:HiddenField ID="HiddenField2" runat="server" />
                        <ajaxToolkit:ModalPopupExtender ID="mpeCreateProfileChargeCSG" BehaviorID="mpeCreateProfileChargeCSG" runat="server" PopupControlID="pnlAddProfileChargeCSG"
                            TargetControlID="hfAddprofileChagreId" BackgroundCssClass="modalBackground">
                        </ajaxToolkit:ModalPopupExtender>
                    </td>
                </tr>
            </table>

            </div>
            </div>
        </div>

        <%-- Manual Charges Section --%>
        <asp:HiddenField ID="hdnManualUpload" runat="server" Value="false" />
         <a id="accManualUpload" runat="server" class="accordion">Manual Upload</a>
        <div id="divManualUpload" runat="server" class="panel">
            <br />
            <div>
            <asp:GridView ID="grdManualChargeFileInfo" runat="server" AutoGenerateColumns="false" 
                DataKeyNames="MCharge_FileId" class="tblInvoiceDetails" OnRowDeleting="grdManualChargeFileInfo_RowDeleting" OnRowDataBound="grdManualChargeFileInfo_RowDataBound">
                <Columns>
                    <asp:TemplateField HeaderText="File Name">
                        <ItemTemplate>
                            <asp:Label ID="lblMChargeFileName" runat="server" Text='<%#Eval("MCharge_FileName") %>'/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="FileID">
                        <ItemTemplate>
                            <asp:Label ID="lblMChargeFiledId" runat="server" Text='<%#Eval("MCharge_FileId") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Number of Rows">
                        <ItemTemplate>
                            <asp:Label ID="lblMChargeRecordCount" runat="server" Text='<%#Eval("MCharge_file_RecordCount") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:Label ID="lblMChargeStatus" runat="server" Text='<%#Eval("MCharge_FileStatus") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Uploaded On">
                        <ItemTemplate>
                            <asp:Label ID="lblMchargeUploadDate" runat="server" Text='<%#Eval("MCharge_Imported") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Uploaded By">
                        <ItemTemplate>
                            <asp:Label ID="lblMchanrgeFileuploaddBy" runat="server" Text='<%#Eval("MCharge_ImportedBy") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Download Process Log">
                        <ItemTemplate>
                            <asp:ImageButton ID="BtnMChargeProcesslog" runat="server"  Width="25px" 
                                ImageUrl="~/Images/export_excel.png"  OnClick="BtnMChargeProcesslog_Click" ToolTip="Process Log"
                                CommandArgument='<%#Bind("MCharge_FileId")%>' CommandName='<%#Bind("MCharge_FileName")%>'/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:TemplateField HeaderText="Select">
                        <ItemTemplate>
                           <asp:Label ID="lblDirectoryNumber" runat="server" Text='<%#Eval("DirectoryNumber") %>' />-
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnDeleteManualUpload" class="buttonManualChargeFile" runat="server" CausesValidation="false" CommandName="Delete" Text="Delete" OnClientClick="return confirm('Are you sure to delete the Manual Charges file from MBM?');" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <br />
            <table style="width: 1100px">
                <tr>
                    <td>
                        <asp:Button ID="btnManualUploadDailog" class="buttonNewInvoice" Width="150px" runat="server" Text="Upload New File" CausesValidation="false" />
                        <asp:HiddenField ID="hfManualUpload" runat="server" />
                        <ajaxToolkit:ModalPopupExtender ID="mpeManualUpload" BehaviorID="mpeManualUpload" runat="server" PopupControlID="pnlManualUpload"
                            TargetControlID="btnManualUploadDailog" BackgroundCssClass="modalBackground">
                        </ajaxToolkit:ModalPopupExtender>
                        <asp:Button ID="btnManualSuccessData" class="buttonNewInvoice" Width="250px" runat="server" Text="Download Existing Data" CausesValidation="false" OnClick="btnManualSuccessData_Click" />
                    </td>
                </tr>
            </table>
            </div>
        </div>


    <div id="dvAccessDenied" runat="server">
        <div class="jumbotron" style="background-color: #E48D0F; padding: 10px">
            <h4 style="color: floralwhite">Access Denied</h4>
        </div>
        <div>
            <table style="width: 1100px">
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblAccessDenied" Font-Bold="true" ForeColor="Red">Please contact the application administrator for access.</asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </div>

    <asp:Panel ID="pnlSearchUserResult" runat="server" CssClass="modalPopup" Style="display: none" DefaultButton="btnAddUser">
        <a onclick="return HidePopupSearch()" href="#close" title="Close" class="close">X</a>
        <header class="header">
            <h4>Search Result </h4>
        </header>
        <asp:Label ID="lblAssociatedUserMessage" runat="server" Visible="false" Font-Bold="true" ForeColor="Green"></asp:Label>
        <asp:GridView ID="grdSearchUserResult" DataKeyNames="UserId" runat="server"
            class="tblmodel" AutoGenerateColumns="false" HeaderStyle-Font-Bold="true">
            <%--   onrowdeleting="gridView_RowDeleting" onrowupdating="gridView_RowUpdating"
        onrowcommand="gridView_RowCommand"
        OnRowDataBound="gridView_RowDataBound">--%>
            <Columns>
                <asp:TemplateField HeaderText="Select User">
                    <ItemTemplate>
                        <asp:RadioButton ID="rbtnSelectedUser" runat="server" OnClick="javascript:RadioCheck(this)" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="User Id">
                    <ItemTemplate>
                        <asp:Label ID="lblUserId" runat="server" Text='<%#Eval("UserId") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="First Name">
                    <ItemTemplate>
                        <asp:Label ID="lblUserFirstName" runat="server" Text='<%#Eval("UserFirstName") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Last Name">
                    <ItemTemplate>
                        <asp:Label ID="lblUserLastName" runat="server" Text='<%#Eval("UserLastName") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="User Role">
                    <ItemTemplate>
                        <asp:Label ID="lblUserRole" runat="server" Text='<%#Eval("UserRole") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>
        <asp:Label ID="lblSearchResultNull" runat="server" ForeColor="Red" Visible="false"></asp:Label>
        <br />
        <br />
        <asp:Button ID="btnAddUser" CssClass="yes" runat="server" Text="Add User" Style="display: none;" ClientIDMode="Static" OnClick="btnAddUser_Click" CausesValidation="False" />
    </asp:Panel>

    <asp:Panel ID="pnlAddAcount" runat="server" CssClass="modalPopup" Style="display: none" DefaultButton="btnAddCRMAccount">
        <a onclick="return HidePopupAddAcount()" href="#close"
            title="Close" class="close">X</a>
        <header class="header">
            <h4>Add Billing Account </h4> <%--CBE-1582--%>
        </header>
        <asp:Label ID="lblAddAccountErrorMessage" runat="server" Visible="false" Font-Bold="true" ForeColor="Red"></asp:Label>
        <div class="body" style ="width: auto; max-height:470px; overflow-x:auto; overflow-y:auto">
            <div>
                <table class="tblmodel">
                    <tr>
                        <td>
                                <label id="lblCrmAccountNumber">Billing Account Number:</label> <%--CBE-1582--%>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCrmAccountNumber" runat="server" ValidationGroup="AddAccount" Height="32px" CausesValidation="true"></asp:TextBox>
                            <asp:Button ID="btnValidate" runat="server" Text="Validate" CausesValidation="false" CssClass="yes" OnClick="btnValidate_Click" />
                            <asp:RequiredFieldValidator ID="rfvtxtCrmAccountNumber" runat="server" ControlToValidate="txtCrmAccountNumber" ErrorMessage="Please enter CRMAccountNumber!" ForeColor="Red" ValidationGroup="AddAccount" Text="*" Visible="True"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revtxtCrmAccountNumber" runat="server" ValidationExpression="^([0-9]*)$" ControlToValidate="txtCrmAccountNumber" ValidationGroup="AddAccount"
                                ErrorMessage="Only digits are allowed"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                                <label id="lblCRMAccountName">Account Name:</label>
                        </td>
                        <td>
                            <asp:Label ID="lblCRMAccountNameDisplay" runat="server"></asp:Label>
                        </td>
                    </tr>
                        
                    <tr>
                        <td>
                                <label id="lblParentAccountNumber">Parent Account Number:</label>
                        </td>
                        <td>
                            <asp:Label ID="lblParentAccountNumberDisplay" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                                <label id="lblParentAccountName">Parent Account Name:</label>
                        </td>
                        <td>
                            <asp:Label ID="lblParentAccountNameDisplay" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                                <label id="lblBillCycle">Bill Cycle:</label>
                        </td>
                        <td>
                            <asp:Label ID="lblBillCycleDisplay" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                                <label id="lblStringIdentifier">Provisioning Identifier:</label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtStringIdentifier" runat="server" Enabled="false"></asp:TextBox>
                        </td>

                    </tr>

                        <tr>
                            <td>
                                <label id="lblDateIdentifier">Effective Billing Date:</label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDateIdentifier" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender runat="server" TargetControlID="txtDateIdentifier" Format="MM-dd-yyyy" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label id="lblChildPays">Child Pays:</label>
                            </td>
                            <td>
                                <asp:CheckBox ID="checkChildPays" runat="server" ClientIDMode="Static" onclick="CheckChildPays()"/>
                            </td>
                        </tr>
<tr>
                        <td>
                                <label id="lblUserName">User Name:</label>
                        </td>
                        <td>
                                <asp:TextBox ID="textUserName" runat="server"  ClientIDMode="Static"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td>
                                <label id="lblPassword">Password:</label>
                        </td>
                        <td>
                                <asp:TextBox ID="textPassword" runat="server"  ClientIDMode="Static"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                                <label id="lblFTP">FTP:</label>
                        </td>
                        <td>
                                <asp:TextBox ID="textFTP" runat="server"  ClientIDMode="Static"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                                <label id="lblEmail">Email Id:</label>
                        </td>
                        <td>
                                <asp:TextBox ID="textEmail" runat="server"  ClientIDMode="Static"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                                <label id="lblEdi">EDI:</label>
                        </td>
                        <td>
                                <asp:CheckBox ID="checkEDI" runat="server"  ClientIDMode="Static"></asp:CheckBox>
                        </td>
                    </tr>
                    <tr style="visibility:hidden;">
                        <td>
                                <label id="lblCreateAccount">Create CRM Account:</label>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkCrmAccount" runat="server" />
                        </td>
                    </tr>
                </table>
            </div>
            <br />
             <asp:HiddenField ID="hdnSubscriberId" Visible="false" runat="server" />
            <asp:Button ID="btnAddCRMAccount" CssClass="yes" runat="server" Text="Add Account" Width="100px" ValidationGroup="AddAccount" CausesValidation="true" OnClick="btnAddCRMAccount_Click" />
        </div>
    </asp:Panel>

    <asp:Panel ID="pnlEditAcount" runat="server" CssClass="modalPopup" Style="display: none" DefaultButton="btnUpdateAccount">
        <a onclick="return HidePopupEditAcount()" href="#close" title="Close" class="close">X</a>
        <header class="header">
            <h4>Update Billing Account </h4>
        </header>
        <asp:Label ID="lblEditAccountErrorMessage" runat="server" Visible="false" Font-Bold="true" ForeColor="Red"></asp:Label>
        <div class="body">
            <div>
                <table class="tblmodel">
                    <tr>
                        <td>
                                <label id="lblEditCrmAccountNumber">Billing Account Number:</label>
                        </td>
                        <td>
                            <asp:Label ID="lblEditCrmAccountNumberDisplay" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                                <label id="lblEditAccountName">Account Name:</label>
                        </td>
                        <td>
                            <asp:Label ID="lblEditAccountNameDisplay" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                                <label id="lblEditParentAccountNumber">Parent Account Number:</label>
                        </td>
                        <td>
                            <asp:Label ID="lblEditParentAccountNumberDisplay" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                                <label id="lblEditParentAccountName">Parent Account Name:</label>
                        </td>
                        <td>
                            <asp:Label ID="lblEditParentAccountNameDisplay" runat="server"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td>
                                <label id="lblEditBillCycle">Bill Cycle:</label>
                        </td>
                        <td>
                            <asp:Label ID="lblEditBillCycleDisplay" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                                <label id="lblEditStringIdentifier">Provisioning Identifier:</label>
                        </td>
                        <td>
                                <asp:TextBox ID="txtEditStringIdentifier" runat="server" Enabled="false"></asp:TextBox>
                        </td>

                    </tr>

                        <tr>
                            <td>
                                <label id="lblEditDateIdentifier">Effective Billing Date:</label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtEditDateIdentifier" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender runat="server" TargetControlID="txtEditDateIdentifier" Format="MM-dd-yyyy" />
                            </td>

                        </tr>
                    <tr>
                        <td>
                                <label id="labelChildPays">Child Pays:</label>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkChildPays" runat="server" ClientIDMode="Static" onclick="ChildPaysCheck()"/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                                <label id="lblUserName">User Name:</label>
                        </td>
                        <td>
                                <asp:TextBox ID="lblEditUserName" runat="server"  ClientIDMode="Static" Enabled="false"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td>
                                <label id="lblPassword">Password:</label>
                        </td>
                        <td>
                                <asp:TextBox ID="txtPassword" runat="server"  ClientIDMode="Static" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                                <label id="lblFTP">FTP:</label>
                        </td>
                        <td>
                                <asp:TextBox ID="txtFTP" runat="server"  ClientIDMode="Static" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                                <label id="lblEmail">Email Id:</label>
                        </td>
                        <td>
                                <asp:TextBox ID="txtEmail" runat="server"  ClientIDMode="Static" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                                <label id="lblEdi">EDI:</label>
                        </td>
                        <td>
                                <asp:CheckBox ID="chkEDI" runat="server"  ClientIDMode="Static" Enabled="false"></asp:CheckBox>
                        </td>
                    </tr>
                    <tr style="visibility:hidden;">
                        <td>
                                <label id="lblEditCreateCRMAccount">Create CRM Account:</label>
                        </td>
                        <td>
                            <asp:CheckBox ID="chkEditCreateCRMAccount" runat="server" />
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <asp:Button ID="btnUpdateAccount" CssClass="yes" runat="server" Text="Update Account" Width="130px" OnClick="btnUpdateAccount_Click" />
        </div>
    </asp:Panel>

    <asp:Panel ID="pnlAddProfile" runat="server" CssClass="modalPopup" Style="display: none" DefaultButton="btnAddProfile">
        <a onclick="return HidePopupAddProfile()" href="#close"
            title="Close" class="close">X</a>
        <header class="header">
            <h4 id="hAddProfile" runat="server">Add New Profile </h4>
            <h4 id="hEditProfile" runat="server" visible="false">Update Profile </h4>
        </header>
        <asp:Label ID="lblAddProfileErrorMessage" runat="server" Visible="false" Font-Bold="true" ForeColor="Red"></asp:Label>
        <asp:HiddenField ID="hdnProfileId" runat="server" />
        <div class="body">
            <div>
                <table class="tblmodel">
                    <tr>
                        <td>
                                <label id="lblProfileName">Profile Name:</label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtProfileName" runat="server" ValidationGroup="AddProfile" Height="32px" CausesValidation="true"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rdvProfileName" ControlToValidate="txtProfileName" ErrorMessage="Please enter profile name" runat="server" ValidationGroup="AddProfile" Text="*" Visible="True"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                                <label id="lblProfileDescription">Profile Description:</label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtProfileDescription" runat="server" ValidationGroup="AddProfile" Height="32px" CausesValidation="true"></asp:TextBox>
                        </td>

                    </tr>
                </table>
            </div>
            <asp:Button ID="btnAddProfile" CssClass="yes" runat="server" Text="Save" Width="100px" ValidationGroup="AddProfile" CausesValidation="true" OnClick="btnAddProfile_Click" />
            <asp:Button ID="btnUpdateProfile" CssClass="yes" Visible="false" runat="server" Text="Update Profile" Width="130px" ValidationGroup="AddProfile" CausesValidation="true" OnClick="btnUpdateProfile_Click" />
        </div>
    </asp:Panel>

     <asp:Panel ID="pnlAddProfileCharge" runat="server" CssClass="modalPopup" Style="display: none" >
        <a onclick="return HidePopupAddProfileCharge()" href="#close"
            title="Close" class="close">X</a>
        <header class="header">
            <h4 id="hAddProfileCharge" runat="server">Add New ProfileCharge </h4>
            <h4 id="hEditProfileCharge" runat="server" visible="false">Update ProfileCharge </h4>
        </header>
        <asp:Label ID="lblprofileChargesErrorMessage" runat="server" Visible="false" Font-Bold="true" ForeColor="Red"></asp:Label>
        <div class="body">
            <div>
                <table class="tblmodel">
                    <tr>
                        <td>
                                <label id="lblProfileChargeName">Profile Name:</label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlProfileName" runat="server" Width="175px" class="buttonNewInvoice" AutoPostBack="false" ValidationGroup="AddProfileCharge" ></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                                <label id="lblProfileChargeCode">GL Code<span style="color: red">*</span>:</label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtProfileChargeGLCode" runat="server" onkeyup="SetButtonStatus(this,'BtnSaveProfileCharge')" ValidationGroup="AddProfileCharge" Height="32px" CausesValidation="true" AutoCompleteType="Disabled"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td>
                                <label id="lblProfileChargeFeature">Feature Code(Auto Populated):</label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtprofileChargeFeature" runat="server" ValidationGroup="AddProfileCharge" Height="32px" CausesValidation="true" Enabled="false" ></asp:TextBox>
                        </td>

                    </tr>
                     <tr>
                        <td>
                                <label id="lblProfileChargeType">Charge Description(Auto Populated):</label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtProfileChargeDesc" runat="server" ValidationGroup="AddProfileCharge" Height="32px" CausesValidation="true" Enabled="false"  ></asp:TextBox><%--ReadOnly="True" BackColor="Gray"--%>
                        </td>

                    <tr>
                        <td>
                                    <label id="lblProfileChargeAmount">Charge:</label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtprofileChargeAmount" runat="server" class="maskedExt" ValidationGroup="AddProfileCharge" Height="32px" CausesValidation="true" ></asp:TextBox>
                            <%--<asp:RequiredFieldValidator ID="rfvProfileChargeAmount" runat="server" ControlToValidate="txtprofileChargeAmount" ErrorMessage="Please enter positive value!" ForeColor="Red" ValidationGroup="AddProfileCharge" Text="*" Visible="True"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revDecimals" runat="server" ErrorMessage="Only Decimals With Precision Less Than 2" ControlToValidate="txtprofileChargeAmount" ValidationExpression="^\d+(\.\d{1,2})?$"></asp:RegularExpressionValidator>
                            <asp:RangeValidator ID="rvprofilechargAmount" runat="server" MinimumValue="0.01" ErrorMessage="provide minimum value" ValidationGroup="AddProfileCharge"></asp:RangeValidator>--%>
                        </td>
                    </tr>
                </table>
            </div>
            <asp:Button ID="BtnSaveProfileCharge"  Enabled="false" CssClass="yes" runat="server" Text="Validate" CommandName="Validate" Width="100px" ValidationGroup="AddProfileCharge" CausesValidation="true" OnClick="BtnSaveProfileCharge_Click"  /><%--OnClientClick="return profilechargeValidation();"--%>
            <asp:HiddenField ID="hdnChargeId" Visible="false" runat="server" />
            <asp:Button ID="BtnUpdateProfileCharge" Visible="false" CssClass="yes" runat="server" CommandName="UpdateCharge" Text="Update Charge" Width="130px" ValidationGroup="AddProfileCharge" CausesValidation="true" OnClick="BtnUpdateProfileCharge_Click" />
        </div>
    </asp:Panel>

        
        <%-- SERO -1582--%>

       
        <asp:Panel ID="pnlAddProfileChargeCSG" runat="server" CssClass="modalPopup" Style="display: none" >
        <a onclick="return HidePopupAddProfileChargeCSG()" href="#close"
            title="Close" class="close">X</a>
        <header class="header">
            <h4 id="hAddProfileChargeCSG" runat="server">Add New ProfileCharge </h4>
            <h4 id="hEditProfileChargeCSG" runat="server" visible="false">Update ProfileCharge </h4>
        </header>

        <asp:Label ID="lblCSGprofileChargesErrorMessage" runat="server" Visible="false" Font-Bold="true" ForeColor="Red"></asp:Label>
        <div class="body" id="divProfileChargepopup">
            <div>
                <table class="tblmodel">
                    <tr>
                        <td>
                                <label id="Label6">Profile Name:</label>
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownList1" runat="server" Width="175px" class="buttonNewInvoice" AutoPostBack="false" ValidationGroup="AddProfileCharge" ></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                                <label id="Label7">Charge String Identifier<span style="color: red">*</span>:</label>
                        </td>
                        <td>
                            <%--<asp:DropDownList ID="DropDownList2" runat="server"  CausesValidation="true" AutoCompleteType="Disabled" Width="175px" class="buttonNewInvoice" AutoPostBack="false" ValidationGroup="AddProfileCharge" ></asp:DropDownList>--%>
                       
                            <asp:DropDownList ID="DropDownList2" runat="server" Width="550px" class="ddlCSGChargeStringSearch" CausesValidation="true" AutoCompleteType="Disabled" AutoPostBack="false" ValidationGroup="AddProfileCharge"></asp:DropDownList>
                             </td>
                    </tr>

                         <tr>
                        <td>
                                    <label id="lblCSGprofileChargeAmount">Charge <span style="color: red">*</span>:</label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCSGprofileChargeAmount" runat="server" class="maskedExt" ValidationGroup="AddProfileCharge" Height="32px" CausesValidation="true" ></asp:TextBox>
                            <%--<asp:RequiredFieldValidator ID="rfvProfileChargeAmount" runat="server" ControlToValidate="txtprofileChargeAmount" ErrorMessage="Please enter positive value!" ForeColor="Red" ValidationGroup="AddProfileCharge" Text="*" Visible="True"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revDecimals" runat="server" ErrorMessage="Only Decimals With Precision Less Than 2" ControlToValidate="txtprofileChargeAmount" ValidationExpression="^\d+(\.\d{1,2})?$"></asp:RegularExpressionValidator>
                            <asp:RangeValidator ID="rvprofilechargAmount" runat="server" MinimumValue="0.01" ErrorMessage="provide minimum value" ValidationGroup="AddProfileCharge"></asp:RangeValidator>--%>
                        </td>
                    </tr>
                    
                  <%--  <tr>
                        <td>
                                <label id="Label8">Pricing Plan Id:</label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox2" runat="server"  Height="32px" CausesValidation="true" Enabled="false" ></asp:TextBox>
                        </td>

                    </tr>
                     <tr>
                        <td>
                                <label id="Label9">Offer Id:</label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox3" runat="server"  Height="32px" CausesValidation="true" Enabled="false"  ></asp:TextBox>
                        </td>

                    <tr>
                        <td>
                                    <label id="Label10">Price:</label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBox4" runat="server" class="maskedExt"  Height="32px" CausesValidation="true" ></asp:TextBox>
                  --%>          <%--<asp:RequiredFieldValidator ID="rfvProfileChargeAmount" runat="server" ControlToValidate="txtprofileChargeAmount" ErrorMessage="Please enter positive value!" ForeColor="Red" ValidationGroup="AddProfileCharge" Text="*" Visible="True"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revDecimals" runat="server" ErrorMessage="Only Decimals With Precision Less Than 2" ControlToValidate="txtprofileChargeAmount" ValidationExpression="^\d+(\.\d{1,2})?$"></asp:RegularExpressionValidator>
                            <asp:RangeValidator ID="rvprofilechargAmount" runat="server" MinimumValue="0.01" ErrorMessage="provide minimum value" ValidationGroup="AddProfileCharge"></asp:RangeValidator>--%>
                      <%--  </td>
                    </tr>--%>
                </table>
            </div>
             <asp:Button ID="BtnSaveProfileChargeCSG"  CssClass="yes" runat="server" Text="Save" CommandName="Save" Width="100px"  CausesValidation="true" OnClick="BtnSaveProfileChargeCSG_Click"  /><%--OnClientClick="return profilechargeValidation();"--%>
            <asp:HiddenField ID="HiddenField3" Visible="false" runat="server" />
            <asp:Button ID="BtnUpdateProfileChargeCSG" Visible="false" CssClass="yes" runat="server" CommandName="UpdateCharge" Text="Update Charge" Width="130px" ValidationGroup="AddProfileCharge" CausesValidation="true" OnClick="BtnUpdateProfileChargeCSG_Click" />
        </div>
    </asp:Panel>

    <asp:Panel ID="pnlManualUpload" runat="server" CssClass="modalPopup" Style="display: none" >
       <a onclick="return HidePopupUpload()" href="#close"
            title="Close" class="close">X</a>
        <header class="header">
            <h4>Upload Files </h4>
        </header>
        <div class="body">
            <div>
                <asp:Label ID="lblManualFileUploadExeception" runat="server" Visible="false" Font-Bold="true" ForeColor="Red"></asp:Label>
                <table class="tblmodel">
                    <tr>
                        <td>
                            <asp:Label ID="lblManualFileuploadFilePath" runat="server" Text="File Path:" Visible="true"></asp:Label>
                        </td>
                        <td>
                            <asp:FileUpload ID="fpManualFileuploadFilePath" runat="server" Visible="true" />
                        </td>
                    </tr>
                </table>
            </div>
            <br />
                <asp:Button ID="btnProceessManualUploadFile" runat="server" Text="Upload" CssClass="yes" OnClick="btnProceessManualUploadFile_Click" />
        </div>
    </asp:Panel>
   
    </div>
    <asp:HiddenField ID="hdnShowAccordin"  runat="server" />
</asp:Content>
