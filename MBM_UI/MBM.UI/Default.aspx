<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" MaintainScrollPositionOnPostback="true" %>

<%@ Register Src="~/Controls/ErrorSuccessNotifier.ascx" TagPrefix="uc1" TagName="ErrorSuccessNotifier" %>


<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>--%>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script src="Scripts/jquery-1.10.2.min.js"></script>
    <link href="Content/progress-bar.min.css" rel="stylesheet" />
    <script src="Scripts/ErrorSuccessNotifier.js"></script>

    <link href="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.6-rc.0/css/select2.min.css" rel="stylesheet" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/select2/4.0.6-rc.0/js/select2.min.js"></script>

    <style>
        ul {
            margin: 0;
            padding: 0.4em;
            list-style-type: square;
        }

        li {
            padding-left: 0.5em;
            line-height: 2.4em;
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
        }

        .select2-container--default .select2-selection--single .select2-selection__rendered {
            color: white;
            line-height: 28px;
            border-radius: 5px;
        }

        #MainContent_btnPreBillOverride {
            margin-left: 10px;
        }

        .select2-container--default .select2-selection--single .select2-selection__arrow b {
            border-color: white transparent transparent transparent;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            var acc = $('.accordion');
            var i;
            for (i = 0; i < acc.length; i++) {
                acc[i].onclick = function (e) {
                    this.classList.toggle("active");
                    this.nextElementSibling.classList.toggle("show");
                }
            }
        });

        function HidePopupInvoice() {
            $find("mdlNewInvoice").hide();
            return false;
        }
        function HidePopupUpload() {
            $find("mdlUploadFile").hide();
            return false;
        }
        function HidePopupViewChanges() {
            $find("mdlViewChanges").hide();
            return false;
        }

        //SERO-1582

        function HidePopupViewChangesCSG() {
            $find("mdlViewChangesCSG").hide();
            return false;
        }



        function HidePreBillOverride() {
            $find("mdlPreBillOverride").hide();
            return false;
        }

        function CheckOne(obj) {
            var grid = obj.parentNode.parentNode.parentNode;
            var inputs = grid.getElementsByTagName("input");
            for (var i = 0; i < inputs.length; i++) {
                if (inputs[i].type == "checkbox") {
                    if (obj.checked && inputs[i] != obj && inputs[i].checked) {
                        inputs[i].checked = false;
                    }
                }
            }
        }

        function fileUploadChange(control) {
            var selectedText = $(control).find("option:selected").text();

            if (selectedText == "EHCS Production Database" || selectedText == "Select FileType") {
                document.getElementById("rowlblNewFileuploadFilePath").style.display = 'none';

            } else {
                document.getElementById("rowlblNewFileuploadFilePath").style.display = '';

            }
        }

        //var submit = 0;
        //function CheckIsRepeat() {
        //    if (++submit > 1) {
        //        alert('Wait till present process gets completed');
        //        return false;
        //    }
        //}        
        $(document).ready(function () {
            var uri = window.location.toString();
            if (uri.indexOf("?") > 0) {
                var clean_uri = uri.substring(0, uri.indexOf("?"));
                window.history.replaceState({}, document.title, clean_uri);
            }
        });

        $(document).ready(function () {
            $('.ddlTypeSearch').select2();

        });
    </script>
    <br />
    <div class="main">
        <uc1:ErrorSuccessNotifier runat="server" ID="ErrorSuccessNotifier" />
    </div>
    <div id="info"></div>
    <div id="dvProcessInvoice" runat="server">
        <div class="jumbotron" style="background-color: #E48D0F; padding: 10px">
            <h4 style="color: floralwhite">View/Process Invoice</h4>
        </div>
        <div id="divInvoiceInfo">
            <table style="width: 1100px">
                <tr>
                    <td style="width: 15%">
                        <label>Customer:</label></td>
                    <td style="width: 20%">
                        <asp:DropDownList ID="ddlInvoiceType" class="ddlTypeSearch" runat="server" Style="width: 500px" AutoPostBack="true" OnSelectedIndexChanged="ddlInvoiceType_SelectedIndexChanged" AppendDataBoundItems="true" /></td>
                    <td style="width: 20%"></td>
                    <td style="width: 15%">
                        <asp:Button ID="btnNewInvoice" runat="server" Text="New Invoice" class="buttonNewInvoice" Style="width: 100px" OnClick="btnNewInvoice_Click" CausesValidation="false" />
                        <asp:HiddenField ID="hfNewInvoice" runat="server" />
                        <ajaxToolkit:ModalPopupExtender ID="mdlNewInvoice" BehaviorID="mdlNewInvoice" runat="server" PopupControlID="pnlNewInvoicePopup" TargetControlID="hfNewInvoice" BackgroundCssClass="modalBackground">
                        </ajaxToolkit:ModalPopupExtender>
                    </td>
                    <td style="width: 10%"></td>
                    <td style="width: 20%">&nbsp;</td>
                </tr>

                <tr>
                    <td>
                        <label>Invoice Number:</label></td>
                    <td>
                        <asp:DropDownList ID="ddlInvoiceNumber" class="buttonNewInvoice" runat="server" Style="width: 500px" AutoPostBack="true" OnSelectedIndexChanged="ddlInvoiceNumber_SelectedIndexChanged" Enabled="false" /></td>
                    <td>
                        <asp:CheckBox ID="chkInvoiceNumber" runat="server" Checked="false" OnCheckedChanged="RestrictInvoiceDisplay" AutoPostBack="true" /></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>

            </table>
        </div>
        <br>

        <div id="divInvoiceDetails">
            <table class="tblInvoiceDetails">
                <tr>
                    <th style="width: 15%"></th>
                    <th style="width: 20%"></th>
                    <th style="width: 20%"></th>
                    <th style="width: 15%">Record Type</th>
                    <th style="width: 10%">Status</th>
                    <th style="width: 20%">Date/Time</th>
                </tr>
                <tr>
                    <td>
                        <label>Created Date:</label></td>
                    <td>
                        <asp:Label runat="server" ID="lblInvoiceCreatedDate" /></td>
                    <td></td>
                    <td>
                        <label>Long Distance:</label></td>
                    <td>
                        <asp:Label runat="server" ID="lblRT1Status" /></td>
                    <td>
                        <asp:Label runat="server" ID="lblRT1TimeStamp" /></td>
                </tr>
                <tr>
                    <td>
                        <label>Status:</label></td>
                    <td>
                        <asp:Label runat="server" ID="lblInvoiceStatus" /></td>
                    <td></td>
                    <td>
                        <label>MRC/NRC:</label></td>
                    <td>
                        <asp:Label runat="server" ID="lblRT2Status" /></td>
                    <td>
                        <asp:Label runat="server" ID="lblRT2TimeStamp" /></td>
                </tr>
                <tr>
                    <td>
                        <label>Last Action:</label></td>
                    <td>
                        <asp:Label runat="server" ID="lblInvoiceLastAction" /></td>
                    <td></td>
                    <td>
                        <label>Taxes:</label></td>
                    <td>
                        <asp:Label runat="server" ID="lblRT3Status" /></td>
                    <td>
                        <asp:Label runat="server" ID="lblRT3TimeStamp" /></td>
                </tr>
                <tr>
                    <td>
                        <label>Billing Date:</label></td>
                    <td>
                        <asp:Label runat="server" ID="lblInvoiceBillingDate" /></td>
                    <td></td>
                    <td>
                        <label>Summary:</label></td>
                    <td>
                        <asp:Label runat="server" ID="lblRT4Status" /></td>
                    <td>
                        <asp:Label runat="server" ID="lblRT4TimeStamp" /></td>
                </tr>
                <tr>
                    <td>
                        <label>Export Status:</label></td>
                    <td>
                        <asp:Label runat="server" ID="lblInvoiceExportStatus" /></td>
                    <td></td>
                    <td>
                        <label>Totals:</label></td>
                    <td>
                        <asp:Label runat="server" ID="lblRT5Status" /></td>
                    <td>
                        <asp:Label runat="server" ID="lblRT5TimeStamp" /></td>
                </tr>

                <tr>
                    <td>
                        <label>Billing System:</label></td>
                    <td>
                        <asp:Label runat="server" ID="lblInvoiceBillingSystem" /></td>
                    <td></td>

                    <td>
                        <label></label>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="Label2" /></td>
                    <td>
                        <asp:Label runat="server" ID="Label3" /></td>

                </tr>

            </table>
        </div>
        <br />


        <%--automation--%>
        <div id="divAutomationStatus" runat="server" visible="false">
            <table class="tblInvoiceDetails">
                <tr>
                    <th colspan="2">Auto Invoice Process</th>
                </tr>
                <tr>
                    <td style="width: 35%;">
                        <label>Status:</label></td>
                    <td>
                        <asp:Label runat="server" ID="lblAutomationStatus" /></td>
                </tr>
                <tr>
                    <td style="width: 35%;">
                        <label>Pre-Billing Manual Override:</label></td>
                    <td>
                        <asp:CheckBox ID="chkPreBillOverride" runat="server" ClientIDMode="Static" onclick="return false" />

                        <asp:Button ID="btnPreBillOverride" class="buttonNewInvoice" runat="server" Text="Edit" ValidationGroup="validaiton" OnClick="btnEdit_Click" />
                        <asp:HiddenField ID="hfPreBillOverridee" runat="server" />
                        <ajaxToolkit:ModalPopupExtender ID="mdlPreBillOverride" BehaviorID="mdlPreBillOverride" runat="server" PopupControlID="pnlPreBillOverride" TargetControlID="hfPreBillOverridee"
                            BackgroundCssClass="modalBackground">
                        </ajaxToolkit:ModalPopupExtender>
                    </td>
                </tr>
            </table>
            <br />
        </div>

        <asp:Panel ID="pnlPreBillOverride" ClientIDMode="Static" runat="server" CssClass="modalPopup" Style="display: none">
            <a onclick="return HidePreBillOverride()" href="#close"
                title="Close" class="close">X</a>
            <header class="header">
                <h4>Update Pre Billing Override</h4>
            </header>
            <div class="body">
                <div>
                    <%--<asp:Label ID="lblPreBillingManual" runat="server" Visible="false" Font-Bold="true" ForeColor="Red"></asp:Label>--%>
                    <table class="tblmodel">
                        <tr>
                            <td style="width: 50%">
                                <asp:Label ID="lblPreBillingOverride" runat="server" Text="Pre Billing Override:"></asp:Label>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkPreBillOverrideEditable" runat="server" ClientIDMode="Static" />
                            </td>
                        </tr>
                    </table>
                </div>
                <br />
                <asp:Button ID="btnPreBillOverride2" runat="server" Text="Submit" CssClass="yes" ClientIDMode="Static" OnClick="btnSubmit_Click" />
            </div>
        </asp:Panel>


        <a id="achUploadFile" runat="server" class="accordion">Upload Files</a>
        <div id="divUploadFile" runat="server" class="panel">
            <br />
            <asp:GridView ID="grdUploadFile" DataKeyNames="UploadedFileId" runat="server"
                AutoGenerateColumns="false" HeaderStyle-Font-Bold="true" Width="100%" CssClass="tblInvoiceDetails"
                OnRowDeleting="grdUploadFile_RowDeleting">
                <%--onrowupdating="gridView_RowUpdating"
            onrowcommand="gridView_RowCommand"
            OnRowDataBound="gridView_RowDataBound"--%>
                <Columns>
                    <asp:TemplateField HeaderText="File Type">
                        <ItemTemplate>
                            <asp:Label ID="lblUploadId" runat="server" Visible="false" Text='<%#Eval("UploadedFileId") %>' />
                        </ItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblFileType" runat="server" Text='<%#Eval("FileType") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="File Path">
                        <ItemTemplate>
                            <asp:Label ID="lblFileName" runat="server" Text='<%#Eval("FilePath") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="File ID">
                        <ItemTemplate>
                            <asp:Label ID="lblFileId" runat="server" Text='<%#Eval("UploadedFileId") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Snapshot ID">
                        <ItemTemplate>
                            <asp:Label ID="lblSnapshotId" runat="server" Text='<%#Eval("SnapshotId") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("UploadedStatus") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Date">
                        <ItemTemplate>
                            <asp:Label ID="lblLastUpdatedDate" runat="server" Text='<%#Eval("LastUpdatedDate") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Uploaded By">
                        <ItemTemplate>
                            <asp:Label ID="lblLastUpdatedBy" runat="server" Text='<%#Eval("LastUpdatedBy") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Select">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkSelectedFile" runat="server" onClick="CheckOne(this)" OnCheckedChanged="chkSelectedFile" AutoPostBack="true" />
                        </ItemTemplate>

                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnDeleteUploadFile" class="buttonNewInvoice" runat="server" CommandName="Delete" Text="Delete" OnClientClick="return confirm('Are you sure you want to delete the file?');" />
                        </ItemTemplate>

                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <br />
            <asp:Button ID="btnUploadFile" class="buttonNewInvoice" runat="server" Text="Upload New File" ValidationGroup="validaiton" Enabled="false" OnClick="btnUploadFile_Click" />
            <ajaxToolkit:ModalPopupExtender ID="mdlUploadFile" BehaviorID="mdlUploadFile" runat="server" PopupControlID="pnlNewFileUpload" TargetControlID="btnUploadFile"
                BackgroundCssClass="modalBackground">
            </ajaxToolkit:ModalPopupExtender>
            <br />
            <br />
        </div>

        <%--Automation--%>
        <%--  <a id="achAutomationStatus" runat="server" class="accordion">Automation Process Status</a>
        <div id="divAutomationStatus" runat="server" class="panel">
            <br />
            <asp:GridView ID="grdAutomationProcess" class="tblInvoiceDetails" runat="server" AutoGenerateColumns="false">
                <Columns>
                    <asp:BoundField HeaderText="File Uploaded" DataField="IsFileTypeUploaded" />
                    <asp:BoundField HeaderText="Compared To CRM?" DataField="IsComparedToCRM" />
                    <asp:BoundField HeaderText="Changes Viewed?" DataField="IsViwedChanged" />
                    <asp:BoundField HeaderText="Approval Mail Sent" DataField="IsApprovalMailSent" />
                    <asp:BoundField HeaderText="Approved by Mail?" DataField="IsApprovedByMail" />
                    <asp:BoundField HeaderText="Approved CRM Changes" DataField="IsApprovedChange" />
                    <asp:BoundField HeaderText="Synced CRM?" DataField="IsSyncedCRM" />
                    <asp:BoundField HeaderText="Imported CRM?" DataField="IsImportCRM" />
                    <asp:BoundField HeaderText="Processed Invoice?" DataField="IsProcessInvoice" />
                    <asp:BoundField HeaderText="Exported Invoice?" DataField="IsExportInvoice" />                    
                </Columns>
            </asp:GridView>
            <br />
        </div>--%>

        <%--automation end--%>

        <a id="achProcessWorkFlow" runat="server" class="accordion active">Process Workflow</a>
        <div id="divProcessWorkFlow" runat="server" class="panel show">
            <br />
            <table class="tblInvoiceDetails" style="width: 1070px;">
                <tr>
                    <th colspan="5" style="width: 570px; background-color: #dddddd;">
                        <label>Pre-Billing Activities</label></th>
                    <th colspan="2" style="width: 430px; background-color: #cccccc;">
                        <label>Post-Billing Activities </label>
                        <asp:Label runat="server" ID="lblBillDate" /></th>
                </tr>
                <tr>
                    <td colspan="7">
                        <ul class="progress-indicator" style="width: 1060px">
                            <li id="liCompareToCRM" runat="server" style="width: 145px">
                                <span class="bubble"></span>
                                <asp:Button ID="btnCompareToCRM" Enabled="false" runat="server" Text="Compare to Biller" class="buttonNewInvoice"
                                    UseSubmitBehavior="false" OnClientClick="this.disabled = 'True'; this.value = 'Please Wait..'" Style="width: 145px; margin: 5px" OnClick="btnCompareToCRM_Click" />
                            </li>
                            <li id="liViewChanges" runat="server" style="width: 145px">
                                <span class="bubble"></span>
                                <asp:Button ID="btnViewChanges" Enabled="false" runat="server" Text="View Changes" class="buttonNewInvoice" Style="width: 145px; margin: 5px" OnClick="btnViewChanges_Click" />

                                <ajaxToolkit:ModalPopupExtender ID="mdlViewChanges" BehaviorID="mdlViewChanges" runat="server" PopupControlID="pnlComparisonViewChanges" TargetControlID="hdnViewChanges"
                                    BackgroundCssClass="modalBackground">
                                </ajaxToolkit:ModalPopupExtender>

                                <%-- SERO-1582 --%>
                                <ajaxToolkit:ModalPopupExtender ID="mdlViewChangesCSG" BehaviorID="mdlViewChangesCSG" runat="server" PopupControlID="pnlComparisonViewChangesCSG" TargetControlID="hdnViewChanges"
                                    BackgroundCssClass="modalBackground">
                                </ajaxToolkit:ModalPopupExtender>
                            </li>
                            <li id="liApproveChanges" runat="server" style="width: 145px">
                                <span class="bubble"></span>
                                <asp:Button ID="btnApproveChanges" Enabled="false" runat="server" Text="Approve Changes" class="buttonNewInvoice" Style="width: 145px; margin: 5px" OnClick="btnApproveChanges_Click" />
                            </li>
                            <li id="liSyncCRM" runat="server" style="width: 145px">
                                <span class="bubble"></span>
                                <asp:Button ID="btnSyncCRM" Enabled="false" runat="server" Text="Sync Biller" class="buttonNewInvoice"
                                    UseSubmitBehavior="false" OnClientClick="this.disabled = 'True'; this.value = 'Please Wait..'" Style="width: 145px; margin: 5px" OnClick="btnSyncCRM_Click" />
                            </li>

                            <li id="liImportCRMData" runat="server" style="width: 145px">
                                <span class="bubble"></span>
                                <asp:Button ID="btnImportCRMData" Enabled="false" runat="server" Text="Import Biller Data" class="buttonNewInvoice" ToolTip="This button will be enabled only after the bill cycle has run"
                                    UseSubmitBehavior="false" OnClientClick="this.disabled = 'True'; this.value = 'Please Wait..'" Style="width: 145px; margin: 5px" OnClick="btnImportCRMData_Click" />
                            </li>
                            <li id="liProcessInvoice" runat="server" style="width: 145px">
                                <span class="bubble"></span>
                                <asp:Button ID="btnProcessInvoice" Enabled="false" runat="server" Text="Process Invoice" class="buttonNewInvoice"
                                    UseSubmitBehavior="false" OnClientClick="this.disabled = 'True'; this.value = 'Please Wait..'" Style="width: 145px; margin: 5px" OnClick="btnProcessInvoice_Click" />
                            </li>
                            <li id="liExportInvoiceFiles" runat="server" style="width: 145px">
                                <span class="bubble"></span>
                                <asp:Button ID="btnExportInvoiceFiles" Enabled="false" runat="server" Text="Export Invoice File" class="buttonNewInvoice"
                                    UseSubmitBehavior="false" OnClientClick="this.disabled = 'True'; this.value = 'Please Wait..'" Style="width: 145px; margin: 5px" OnClick="btnExportInvoiceFiles_Click" />
                                <span>
                                    <asp:CheckBox runat="server" ID="chkExportProcess" Enabled="false" Visible="false" /></span>
                                <span runat="server" id="spnExportText" style="color: black" visible="false">Generate Invoice Files by Legal Entity</span>
                            </li>
                        </ul>
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="hdnViewChanges" runat="server" />
        </div>

        <a id="achExportFile" runat="server" class="accordion">Exported Files</a>
        <div id="divExportFile" runat="server" class="panel">
            <br />
            <br />
            <asp:GridView ID="grdExportFile" runat="server" AutoGenerateColumns="false" HeaderStyle-Font-Bold="true" class="tblInvoiceDetails" Width="1100px">
                <Columns>
                    <asp:TemplateField HeaderText="Invoice Number">
                        <ItemTemplate>
                            <asp:Label ID="lblExportFileInvoiceNumber" runat="server" Text='<%#Eval("InvoiceNumber") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ExportFile Name">
                        <ItemTemplate>
                            <asp:Label ID="lblExportFileName" runat="server" Text='<%#Eval("ExportedFileName") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ExportFile Path">
                        <ItemTemplate>
                            <asp:Label ID="lblExportFilePath" runat="server" Text='<%#Eval("ExportedFilePath") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ExportFile Date">
                        <ItemTemplate>
                            <asp:Label ID="lblExportFileDate" runat="server" Text='<%#Eval("ExportedFileDate") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ExportFile By">
                        <ItemTemplate>
                            <asp:Label ID="lblExportFileBy" runat="server" Text='<%#Eval("ExportedFileBy") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>
            <br />
            <br />
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

    <asp:Panel ID="pnlNewInvoicePopup" runat="server" CssClass="modalPopup" Style="display: none">
        <a onclick="return HidePopupInvoice()" href="#close" title="Close" class="close">X</a>
        <header class="header">
            <h4>Create New Invoice </h4>
        </header>
        <asp:Label ID="lblNewInvoiceErrorMessage" runat="server" Visible="false" Font-Bold="true" ForeColor="Red"></asp:Label>
        <div class="body">
            <div>
                <table class="tblmodel">
                    <tr>
                        <td>
                            <asp:Label ID="lblNewBillType" runat="server" Text="Customer:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlNewInvoiceType" Width="150px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlNewInvoiceType_SelectedIndexChanged"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblNewBillingDate" runat="server" Text="Billing Date:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlNewInvoiceMonth" runat="server"></asp:DropDownList>
                            <asp:DropDownList ID="ddlNewInvoiceYear" runat="server"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblNewImportCurrency" runat="server" Text="Import Currency:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlNewImportCurrency" runat="server"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblNewExportCurrency" runat="server" Text="Export Currency:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlNewExportCurrency" runat="server"></asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <asp:Button ID="btnCreateInvoice" runat="server" Text="Create" CssClass="yes" OnClick="btnCreateInvoice_Click" UseSubmitBehavior="false" OnClientClick="this.disabled = 'True'; this.value = 'Please Wait..'" Style="width: 120px; margin: 5px" />
        </div>
    </asp:Panel>

    <asp:Panel ID="pnlNewFileUpload" runat="server" CssClass="modalPopup" Style="display: none">
        <a onclick="return HidePopupUpload()" href="#close"
            title="Close" class="close">X</a>
        <header class="header">
            <h4>Upload Files </h4>
        </header>
        <div class="body">
            <div>
                <asp:Label ID="lblNewFileUploadExeception" runat="server" Visible="false" Font-Bold="true" ForeColor="Red"></asp:Label>
                <table class="tblmodel">
                    <tr>
                        <td>
                            <asp:Label ID="lblNewFileTypeUpload" runat="server" Text="File Type:"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlNewFileTypeUpload" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlNewFileTypeUpload_SelectedIndexChanged"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr id="rowlblNewFileuploadFilePath">
                        <td>
                            <asp:Label ID="lblNewFileuploadFilePath" runat="server" Text="File Path:" Visible="false"></asp:Label>
                        </td>
                        <td>
                            <asp:FileUpload ID="fpNewFileuploadFilePath" runat="server" Visible="false" />
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <asp:Button ID="btnProceessUploadFile" runat="server" Text="Upload" CssClass="yes" OnClick="btnProceessUploadFile_Click" />
        </div>
    </asp:Panel>

    <asp:Panel ID="pnlComparisonViewChanges" runat="server" CssClass="modalPopup" Style="display: none">
        <a onclick="return HidePopupViewChanges()" href="#close" title="Close" class="close">X</a>
        <header class="header">
            <h4>View Changes</h4>
        </header>
        <div id="dvComparisonView" style="overflow: auto; height: 450px; width: 775px;">
            <div>
                <table style="float: right; width: 6%;">
                    <tr>
                        <td>
                            <asp:LinkButton ID="btnInsertsExportToExcel" runat="server" OnClick="btnInsertsExportToExcel_Click"><img src="../Images/export_excel.png" alt="Export to XLS" style="width: 30px; height: 30px;"/></asp:LinkButton></td>
                    </tr>
                </table>
            </div>
            <br />
            <asp:Label ID="lblActiveTelephoneListCount" runat="server" Font-Bold="true"></asp:Label>
            <asp:GridView ID="grdActiveTelephoneList" runat="server"
                class="tblmodel" AutoGenerateColumns="false" HeaderStyle-Font-Bold="true" AllowPaging="true" EmptyDataText="No Records Found For Insert."
                ShowHeaderWhenEmpty="True" PageSize="5" AllowSorting="false" OnPageIndexChanging="grdActiveTelephoneList_PageIndexChanging">
                <Columns>
                    <asp:BoundField HeaderText="Invoice Id" DataField="InvoiceId" SortExpression="InvoiceId"></asp:BoundField>
                    <asp:BoundField HeaderText="CRM Account Number" DataField="CRMAccountNumber" SortExpression="CRMAccountNumber"></asp:BoundField>
                    <asp:BoundField HeaderText="UserId" DataField="AssetSearchCode1" SortExpression="AssetSearchCode1"></asp:BoundField>
                    <asp:BoundField HeaderText="Telephone Number" DataField="AssetSearchCode2" SortExpression="AssetSearchCode2"></asp:BoundField>
                    <asp:BoundField HeaderText="Feature Code" DataField="FeatureCode" SortExpression="FeatureCode"></asp:BoundField>
                    <asp:BoundField HeaderText="Profile Name" DataField="ProfileName" SortExpression="ProfileName"></asp:BoundField>
                    <asp:BoundField HeaderText="Charge" DataField="Charge" SortExpression="Charge"></asp:BoundField>
                    <asp:BoundField HeaderText="LoadId" DataField="LoadId" SortExpression="LoadId"></asp:BoundField>
                    <asp:BoundField HeaderText="Processed Flag" DataField="Processed" SortExpression="Processed"></asp:BoundField>
                    <asp:BoundField HeaderText="Action Type" DataField="ActionType" SortExpression="ActionType"></asp:BoundField>
                </Columns>
            </asp:GridView>
            <br>
            <div>
                <table style="float: right; width: 6%;">
                    <tr>
                        <td>
                            <asp:LinkButton ID="btnCancelsExportToExcel" runat="server" OnClick="btnCancelsExportToExcel_Click"><img src="../Images/export_excel.png" alt="Export to XLS" style="width: 30px; height: 30px;"/></asp:LinkButton></td>
                    </tr>
                </table>
            </div>
            <br />
            <asp:Label ID="lblDeactiveTelephoneListCount" runat="server" Font-Bold="true"></asp:Label>
            <asp:GridView ID="grdDeactiveTelephoneList" runat="server"
                class="tblmodel" AutoGenerateColumns="false" HeaderStyle-Font-Bold="true" AllowPaging="true" EmptyDataText="No Records Found For Cancel."
                ShowHeaderWhenEmpty="True" PageSize="5" OnPageIndexChanging="grdDeactiveTelephoneList_PageIndexChanging">
                <Columns>
                    <asp:BoundField HeaderText="Invoice Id" DataField="InvoiceId" SortExpression="InvoiceId"></asp:BoundField>
                    <asp:BoundField HeaderText="CRM Account Number" DataField="CRMAccountNumber" SortExpression="CRMAccountNumber"></asp:BoundField>
                    <asp:BoundField HeaderText="UserId" DataField="AssetSearchCode1" SortExpression="AssetSearchCode1"></asp:BoundField>
                    <asp:BoundField HeaderText="Telephone Number" DataField="AssetSearchCode2" SortExpression="AssetSearchCode2"></asp:BoundField>
                    <asp:BoundField HeaderText="Feature Code" DataField="FeatureCode" SortExpression="FeatureCode"></asp:BoundField>
                    <asp:BoundField HeaderText="Profile Name" DataField="ProfileName" SortExpression="ProfileName"></asp:BoundField>
                    <asp:BoundField HeaderText="Charge" DataField="Charge" SortExpression="Charge"></asp:BoundField>
                    <asp:BoundField HeaderText="LoadId" DataField="LoadId" SortExpression="LoadId"></asp:BoundField>
                    <asp:BoundField HeaderText="Processed Flag" DataField="Processed" SortExpression="Processed"></asp:BoundField>
                    <asp:BoundField HeaderText="Action Type" DataField="ActionType" SortExpression="ActionType"></asp:BoundField>
                </Columns>
            </asp:GridView>
        </div>
    </asp:Panel>

    <%-- SERO-1582--%>

    <asp:Panel ID="pnlComparisonViewChangesCSG" runat="server" CssClass="modalPopup" Style="display: none">
        <a onclick="return HidePopupViewChangesCSG()" href="#close" title="Close" class="close">X</a>
        <header class="header">
            <h4>View Changes</h4>
        </header>
        <div id="Div1" style="overflow: auto; height: 450px; width: 775px;">
            <div>
                <table style="float: right; width: 6%;">
                    <tr>
                        <td>
                            <asp:LinkButton ID="btnInsertsExportToExcelCSG" runat="server" OnClick="btnInsertsExportToExcelCSG_Click"><img src="../Images/export_excel.png" alt="Export to XLS" style="width: 30px; height: 30px;"/></asp:LinkButton></td>
                    </tr>
                </table>
            </div>

            <br />
            <asp:Label ID="lblActiveTelephoneListCountCSG" runat="server" Font-Bold="true"></asp:Label>
            <asp:GridView ID="grdActiveTelephoneListCSG" runat="server"
                class="tblmodel" AutoGenerateColumns="false" HeaderStyle-Font-Bold="true" AllowPaging="true" EmptyDataText="No Records Found For Insert."
                ShowHeaderWhenEmpty="True" PageSize="5" AllowSorting="false" OnPageIndexChanging="grdActiveTelephoneListCSG_PageIndexChanging">
                <Columns>
                    <asp:BoundField HeaderText="Invoice Id" DataField="InvoiceId" SortExpression="InvoiceId"></asp:BoundField>
                    <asp:BoundField HeaderText="Subcriber Id" DataField="SubscriberId" SortExpression="SubscriberId"></asp:BoundField>
                    <asp:BoundField HeaderText="UserId" DataField="AssetSearchCode1" SortExpression="AssetSearchCode1"></asp:BoundField>
                    <asp:BoundField HeaderText="Service Identifier" DataField="MBMUniqueID" SortExpression="MBMUniqueID"></asp:BoundField>
                    <%-- <asp:BoundField HeaderText="Product ID" DataField="FeatureCode" SortExpression="FeatureCode"></asp:BoundField>--%>
                    <asp:BoundField HeaderText="Profile Name" DataField="ProfileName" SortExpression="ProfileName"></asp:BoundField>

                    <asp:BoundField HeaderText="Offer Name" DataField="OfferExternalRef" SortExpression="OfferExternalRef"></asp:BoundField>
                    <asp:BoundField HeaderText="Product Name" DataField="ProductExternalRef" SortExpression="ProductExternalRef"></asp:BoundField>
                    <asp:BoundField HeaderText="Pricing Name" DataField="PricingPlanExternalRef" SortExpression="PricingPlanExternalRef"></asp:BoundField>

                    <asp:BoundField HeaderText="LoadId" DataField="LoadId" SortExpression="LoadId"></asp:BoundField>
                    <asp:BoundField HeaderText="Processed Flag" DataField="Processed" SortExpression="Processed"></asp:BoundField>
                    <asp:BoundField HeaderText="Action Type" DataField="ActionType" SortExpression="ActionType"></asp:BoundField>
                </Columns>
            </asp:GridView>

            <br>
            <div>
                <table style="float: right; width: 6%;">
                    <tr>
                        <td>
                            <asp:LinkButton ID="btnCancelsExportToExcelCSG" runat="server" OnClick="btnCancelsExportToExcelCSG_Click"><img src="../Images/export_excel.png" alt="Export to XLS" style="width: 30px; height: 30px;"/></asp:LinkButton></td>
                    </tr>
                </table>
            </div>
            <br />
            <asp:Label ID="lblDeactiveTelephoneListCountCSG" runat="server" Font-Bold="true"></asp:Label>
            <asp:GridView ID="grdDeactiveTelephoneListCSG" runat="server"
                class="tblmodel" AutoGenerateColumns="false" HeaderStyle-Font-Bold="true" AllowPaging="true" EmptyDataText="No Records Found For Cancel."
                ShowHeaderWhenEmpty="True" PageSize="5" OnPageIndexChanging="grdDeactiveTelephoneListCSG_PageIndexChanging">
                <Columns>
                    <asp:BoundField HeaderText="Invoice Id" DataField="InvoiceId" SortExpression="InvoiceId"></asp:BoundField>
                    <asp:BoundField HeaderText="Subcriber Id" DataField="SubscriberId" SortExpression="SubscriberId"></asp:BoundField>
                    <asp:BoundField HeaderText="UserId" DataField="AssetSearchCode1" SortExpression="AssetSearchCode1"></asp:BoundField>
                    <asp:BoundField HeaderText="Service Identifier" DataField="MBMUniqueID" SortExpression="MBMUniqueID"></asp:BoundField>
                    <%--<asp:BoundField HeaderText="Product ID" DataField="FeatureCode" SortExpression="FeatureCode"></asp:BoundField>--%>
                    <asp:BoundField HeaderText="Profile Name" DataField="ProfileName" SortExpression="ProfileName"></asp:BoundField>

                    <asp:BoundField HeaderText="Offer Name" DataField="OfferExternalRef" SortExpression="OfferExternalRef"></asp:BoundField>
                    <asp:BoundField HeaderText="Product Name" DataField="ProductExternalRef" SortExpression="ProductExternalRef"></asp:BoundField>
                    <asp:BoundField HeaderText="Pricing Name" DataField="PricingPlanExternalRef" SortExpression="PricingPlanExternalRef"></asp:BoundField>

                    <asp:BoundField HeaderText="LoadId" DataField="LoadId" SortExpression="LoadId"></asp:BoundField>
                    <asp:BoundField HeaderText="Processed Flag" DataField="Processed" SortExpression="Processed"></asp:BoundField>
                    <asp:BoundField HeaderText="Action Type" DataField="ActionType" SortExpression="ActionType"></asp:BoundField>
                </Columns>
            </asp:GridView>
        </div>
    </asp:Panel>


</asp:Content>
