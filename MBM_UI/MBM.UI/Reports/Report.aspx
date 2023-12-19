<%@ Page Title="Reports" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Report.aspx.cs" Inherits="Reports_Report" MaintainScrollPositionOnPostback="true" EnableEventValidation = "false" %>

<%@ Register Src="~/Controls/ErrorSuccessNotifier.ascx" TagPrefix="ucreport" TagName="ErrorSuccessNotifier" %>


<asp:Content ID="BodyContent1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <div class="main">
        <ucreport:ErrorSuccessNotifier runat="server" ID="ErrorSuccessNotifier" />
    </div>
    <div id="info"></div>
    <div id="dvReports" runat="server">
        <div class="jumbotron" style="background-color: #E48D0F; padding: 10px">
            <h4 style="color: floralwhite">Reports View</h4>
        </div>
    </div>

    <div id="dvReportTable" runat="server">
        <table class="tblInvoiceDetails" style="width: 100%">
            <tbody>
                <tr>
                    <th>
                        <label id="lblDataType">Data Type</label>
                    </th>
                    <th>
                        <label id="lblCustomer">Customer</label>
                    </th>                    
                        <th>
                            <label id="lblStartDate" runat="server">Start Date</label>                            
                            <label id="lblInvoices" runat="server" visible="false">Invoice Number</label>                            
                        </th>
                        <th id="lblCol4Up" runat="server" >
                            <label id="lblEndDate" runat="server">End Date</label>
                        </th>                                
                    <th >                     
                    </th>                                                          
                </tr>

                <tr>
                    <th>
                        <asp:DropDownList ID="ddlDataType" runat="server" class="buttonNewInvoice" OnSelectedIndexChanged="ddlDataType_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Text="MBM Input Data" Value="RawData"></asp:ListItem>                            
                            <asp:ListItem Text="MBM Enhanced" Value="UnEnhanced"></asp:ListItem>
                            <asp:ListItem Text="MBM Enhanced with Charges" Value="Enhanced"></asp:ListItem>
                            <asp:ListItem Text="SOO Fixed Charges" Value="SOOARReport"></asp:ListItem>
                            <asp:ListItem Text="SOO Monthly Report" Value="SOOAccsReport"></asp:ListItem>
                            <asp:ListItem Text="EDI" Value="EDI"></asp:ListItem>
                        </asp:DropDownList>
                    </th>   
                    <th>                                           
                        <asp:DropDownList ID="ddlInvoiceType" class="buttonNewInvoice" runat="server" Style="width: 180px" AutoPostBack="true" OnSelectedIndexChanged="ddlInvoiceType_SelectedIndexChanged" AppendDataBoundItems="true"/>
                    </th>                   
                    <th>
                        <asp:DropDownList ID="ddlInvoiceNumber" Visible="false"  class="buttonNewInvoice" runat="server" Style="width: 180px" AutoPostBack="true"/>
                        <asp:CheckBox ID="chkInvoiceNumber" runat="server" Checked="false" Visible="false" OnCheckedChanged="RestrictInvoiceDisplay" AutoPostBack="true"/>
                        <asp:TextBox ID="txtStartDate" runat="server" ></asp:TextBox>
                        <ajaxToolkit:CalendarExtender runat="server" TargetControlID="txtStartDate" Format="MM-dd-yyyy" PopupButtonID="Image1" />
                    </th>
                    <th id="lblCol4Down" runat="server" >
                        <asp:TextBox ID="txtEndDate" runat="server"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender runat="server" TargetControlID="txtEndDate" Format="MM-dd-yyyy" PopupButtonID="Image1" />
                    </th>
                    <th>
                        <asp:Button ID="btnSearch" runat="server" Text="Search" class="buttonNewInvoice" Width="180px"  OnClick="btnSearch_Click"/>
                    </th>
                </tr>
            </tbody>
        </table>
        <br />
        <div id="divEnhanceddata" runat="server" style="overflow-x: auto; width: 100%" visible="false">
            <div>
                <table style="float:right; width: 6%;">
                    <tr>
                        <td><asp:LinkButton ID="btnEnhancedDataExportToExcel" runat="server" OnClick="btnEnhancedDataExportToExcel_Click"><img src="../Images/export_excel.png" alt="Export to XLS" style="width: 30px; height: 30px;"/></asp:LinkButton></td>
                        <%--<td><asp:LinkButton ID="btnEnhancedDataExportToPDF" runat="server" OnClick="btnEnhancedDataExportToPDF_Click"><img src="../Images/export_pdf.png" alt="Export to PDF" style="width: 25px; height: 25px;" /></asp:LinkButton></td>--%>
                    </tr>
                </table>
            </div>
            <asp:GridView ID="grdEnhancedData" runat="server" AutoGenerateColumns="false" HeaderStyle-Font-Bold="true" Width="100%" CssClass="tblInvoiceDetails"
                            OnPageIndexChanging="grdEnhancedData_PageIndexChanging" AllowPaging="true" PageSize="10" RowStyle-Wrap="true">
                            <Columns>
                                <asp:BoundField HeaderText="Customer" DataField="Customer" />
                                <asp:BoundField HeaderText="Invoice Number" DataField="InvoiceNumber" />
                                <asp:BoundField HeaderText="Snapshot Id" DataField="SnapshotId" />
                                <asp:BoundField HeaderText="Sub Identifier" DataField="SubIdentifier" />
                                <asp:BoundField HeaderText="First Name" DataField="FirstName" />
                                <asp:BoundField HeaderText="Last Name" DataField="LastName" />
                                <asp:BoundField HeaderText="Primary Unique Identifier" DataField="PrimaryUniqueIdentifier" />
                                <asp:BoundField HeaderText="Secondary Unique Identifier" DataField="SecondaryUniqueIdentifier" />
                                <asp:BoundField HeaderText="Service Profile" DataField="ServiceProfile" />
                                <asp:BoundField HeaderText="Legal Entity" DataField="LegalEntity" />
                                <asp:BoundField HeaderText="Charge Code" DataField="ChargeCode" />
                                <asp:BoundField HeaderText="Charge" DataField="Charge" />
                                <asp:BoundField HeaderText="Start Date" DataField="StartDate" />
                                <asp:BoundField HeaderText="End Date" DataField="EndDate" />
                                <asp:BoundField HeaderText="Feature Code" DataField="FeatureCode" />
                                <asp:BoundField HeaderText="e164_mask" DataField="e164_mask" />
                                <asp:BoundField HeaderText="ActiveCharge" DataField="ActiveCharge" />
                                <asp:BoundField HeaderText="EffectiveBillingDate" DataField="EffectiveBillingDate" />
                            </Columns>

                        <HeaderStyle Font-Bold="True"></HeaderStyle>
                        <PagerSettings Position="TopAndBottom" />
                            <PagerStyle CssClass="GridPager" />
                        <RowStyle Wrap="True"></RowStyle>
                        </asp:GridView>
         </div>

        <div style="align-content: center">
            <asp:Label ID="lbldata" runat="server" ForeColor="Red" Visible="false"></asp:Label>
        </div>

        <div id="divUnEnhanceddata" runat="server" style="overflow-x: auto; width: 100%" visible="false">
            <div>
                <table style="float:right; width: 6%;">
                    <tr>
                        <td><asp:LinkButton ID="btnUnEnhancedDataExportToExcel" runat="server" OnClick="btnUnEnhancedDataExportToExcel_Click"><img src="../Images/export_excel.png" alt="Export to XLS" style="width: 30px; height: 30px;"/></asp:LinkButton></td>
                        <%--<td><asp:LinkButton ID="btnUnEnhancedDataExportToPDF" runat="server" OnClick="btnUnEnhancedDataExportToPDF_Click"><img src="../Images/export_pdf.png" alt="Export to PDF" style="width: 25px; height: 25px;" /></asp:LinkButton></td>--%>
                    </tr>
                </table>
            </div>
        <asp:GridView ID="grdUnEnhanceddata" runat="server" AutoGenerateColumns="false" HeaderStyle-Font-Bold="true" Width="100%" CssClass="tblInvoiceDetails"
                            OnPageIndexChanging="grdUnEnhanceddata_PageIndexChanging" AllowPaging="true" PageSize="10" RowStyle-Wrap="true">
                            <Columns>
                                <asp:BoundField HeaderText="Customer" DataField="Customer" />
                                <asp:BoundField HeaderText="Invoice Number" DataField="InvoiceNumber" />
                                <asp:BoundField HeaderText="Snapshot Id" DataField="SnapshotId" />
                                <asp:BoundField HeaderText="Sub Identifier" DataField="SubIdentifier" />
                                <asp:BoundField HeaderText="First Name" DataField="FirstName" />
                                <asp:BoundField HeaderText="Last Name" DataField="LastName" />
                                <asp:BoundField HeaderText="Primary Unique Identifier" DataField="PrimaryUniqueIdentifier" />
                                <asp:BoundField HeaderText="Secondary Unique Identifier" DataField="SecondaryUniqueIdentifier" />
                                <asp:BoundField HeaderText="Service Profile" DataField="ServiceProfile" />
                                <asp:BoundField HeaderText="Legal Entity" DataField="LegalEntity" />
                                <asp:BoundField HeaderText="Start Date" DataField="ServiceStartDate" />
                                <asp:BoundField HeaderText="End Date" DataField="ServiceEndDate" />
                                <asp:BoundField HeaderText="State" DataField="State" />
                                <asp:BoundField HeaderText="PostalCode" DataField="PostalCode" />
                                <asp:BoundField HeaderText="Country" DataField="Country" />
                                <asp:BoundField HeaderText="e164_mask" DataField="e164_mask" />
                                <asp:BoundField HeaderText="ActiveCharge" DataField="ActiveCharge" />
                                <asp:BoundField HeaderText="EffectiveBillingDate" DataField="EffectiveBillingDate" />
                            </Columns>

                        <HeaderStyle Font-Bold="True"></HeaderStyle>
                        <PagerSettings Position="TopAndBottom" />
                            <PagerStyle CssClass="GridPager" />
                        <RowStyle Wrap="True"></RowStyle>
                        </asp:GridView>
            </div>

        <div id="divRawData" runat="server" style="overflow-x: auto; width: 100%" visible="false">
            <div>
                <table style="float:right; width: 6%;">
                    <tr>
                        <td><asp:LinkButton ID="btnRawDataExportToExcel" runat="server" OnClick="btnRawDataExportToExcel_Click"><img src="../Images/export_excel.png" alt="Export to XLS" style="width: 30px; height: 30px;"/></asp:LinkButton></td>                        
                    </tr>
                </table>
            </div>
            <asp:GridView ID="grdRawdata" runat="server" AutoGenerateColumns="false" HeaderStyle-Font-Bold="true" Width="100%" CssClass="tblInvoiceDetails"
            OnPageIndexChanging="grdRawdata_PageIndexChanging" AllowPaging="true" PageSize="10" RowStyle-Wrap="true">
                <Columns>
                    <asp:BoundField HeaderText="SnapshotId" DataField="SnapshotId" />
                    <asp:BoundField HeaderText="SubIdentifier" DataField="SubIdentifier" />
                    <asp:BoundField HeaderText="FirstName" DataField="FirstName" />
                    <asp:BoundField HeaderText="LastName" DataField="LastName" />
                    <asp:BoundField HeaderText="PrimaryUniqueIdentifier" DataField="PrimaryUniqueIdentifier" />
                    <asp:BoundField HeaderText="ServiceProfileId" DataField="ServiceProfileId" />
                    <asp:BoundField HeaderText="Service StartDate" DataField="ServiceStartDate" />
                    <asp:BoundField HeaderText="Service EndDate" DataField="ServiceEndDate" />
                    <asp:BoundField HeaderText="Legal Entity" DataField="LegalEntity" />
                    <asp:BoundField HeaderText="State" DataField="State" />
                    <asp:BoundField HeaderText="PostalCode" DataField="PostalCode" />
                    <asp:BoundField HeaderText="Country" DataField="Country" />
                    <asp:BoundField HeaderText="DirectoryNumber" DataField="DirectoryNumber" />
                    <asp:BoundField HeaderText="ServiceProfileUid" DataField="ServiceProfileUid" /> 
                    <asp:BoundField HeaderText="e164_mask" DataField="e164_mask" />
                    <asp:BoundField HeaderText="ActiveCharge" DataField="ActiveCharge" />
                    <asp:BoundField HeaderText="EffectiveBillingDate" DataField="EffectiveBillingDate" />
                </Columns>
                <HeaderStyle Font-Bold="True"></HeaderStyle>
                <PagerSettings Position="TopAndBottom" />
                    <PagerStyle CssClass="GridPager" />
                <RowStyle Wrap="True"></RowStyle>
            </asp:GridView>
        </div>

        <div id="divSOOARReportData" runat="server" style="overflow-x: auto; width: 100%" visible="false">
            <div>
                <table style="float:right; width: 6%;">
                    <tr>
                        <td><asp:LinkButton ID="btnSOOReportDataExportToExcel" runat="server" OnClick="btnSOOReportDataExportToExcel_Click"><img src="../Images/export_excel.png" alt="Export to XLS" style="width: 30px; height: 30px;"/></asp:LinkButton></td>
                    </tr>
                </table>
            </div>
            <asp:GridView ID="grdSOOReportData" runat="server" AutoGenerateColumns="false" HeaderStyle-Font-Bold="true" Width="100%" CssClass="tblInvoiceDetails"
                            OnPageIndexChanging="grdSOOReportData_PageIndexChanging" AllowPaging="true" PageSize="10" RowStyle-Wrap="true">
                            <Columns>
                                <asp:BoundField HeaderText="CRM_ACCOUNT" DataField="CRM_ACCOUNT" />
                                <asp:BoundField HeaderText="TN" DataField="TN" />
                                <asp:BoundField HeaderText="PROFILE_TYPE" DataField="PROFILE_TYPE" />
                                <asp:BoundField HeaderText="AMOUNT" DataField="AMOUNT" />
                                <asp:BoundField HeaderText="START_DTT" DataField="START_DTT" />
                                <asp:BoundField HeaderText="END_DTT" DataField="END_DTT" />
                                <asp:BoundField HeaderText="CUSTOMER_DEPARTMENT" DataField="CUSTOMER_DEPARTMENT" />
                                <asp:BoundField HeaderText="CUSTOMER_COST_CENTER" DataField="CUSTOMER_COST_CENTER" />
                                <asp:BoundField HeaderText="CUSTOMER_ALI_CODE" DataField="CUSTOMER_ALI_CODE" />
                                <asp:BoundField HeaderText="SUPERVISOR_NAME" DataField="SUPERVISOR_NAME" />
                                <asp:BoundField HeaderText="FIRST_NAME" DataField="FIRST_NAME" />
                                <asp:BoundField HeaderText="LAST_NAME" DataField="LAST_NAME" />
                                <asp:BoundField HeaderText="EMAIL" DataField="EMAIL" />
                                <asp:BoundField HeaderText="QUANTITY" DataField="QUANTITY" />
                                <asp:BoundField HeaderText="UNIT_PRICE" DataField="UNIT_PRICE" />
                                <asp:BoundField HeaderText="DESCRIPTION" DataField="DESCRIPTION" />
                            </Columns>

                        <HeaderStyle Font-Bold="True"></HeaderStyle>
                        <PagerSettings Position="TopAndBottom" />
                            <PagerStyle CssClass="GridPager" />
                        <RowStyle Wrap="True"></RowStyle>
            </asp:GridView>
        </div>

        <%--cbe_11609--%>
        <div id="divSOOAccListData" runat="server" style="overflow-x: auto; width: 100%" visible="false">
            <div>
                <table style="float:right; width: 6%;">
                    <tr>
                        <td><asp:LinkButton ID="btnSOOAccountDataExportToExcel" runat="server" OnClick="btnSOOAccountDataExportToExcel_Click"><img src="../Images/export_excel.png" alt="Export to XLS" style="width: 30px; height: 30px;"/></asp:LinkButton></td>
                    </tr>
                </table>
            </div>
        <asp:GridView ID="grdSOOAccountdata" runat="server" AutoGenerateColumns="false" HeaderStyle-Font-Bold="true" Width="100%" CssClass="tblInvoiceDetails"
                            OnPageIndexChanging="grdSOOAccountdata_PageIndexChanging" AllowPaging="true" PageSize="10" RowStyle-Wrap="true">
                            <Columns>
                                <asp:BoundField HeaderText="Record Type" DataField="record_type" />
                                <asp:BoundField HeaderText="Billing Number" DataField="billing_account_number" />
                                <asp:BoundField HeaderText="Invoice Number" DataField="invoice_number" />
                                <asp:BoundField HeaderText="DID" DataField="did" />
                                <asp:BoundField HeaderText="Employee Id" DataField="employee_id" />
                                <asp:BoundField HeaderText="Charge Description" DataField="charge_description" />
                                <asp:BoundField HeaderText="Tax Percentage" DataField="tax_percentage" />
                                <asp:BoundField HeaderText="Service Type" DataField="service_type" />
                                <asp:BoundField HeaderText="Tax Charge" DataField="tax_charge" />
                                <asp:BoundField HeaderText="Vendor Name" DataField="vendor_name" />
                                <asp:BoundField HeaderText="Bill Date" DataField="bill_date" />
                                <asp:BoundField HeaderText="State" DataField="state" />
                                <asp:BoundField HeaderText="Legal Entity" DataField="legal_entity" />
                            </Columns>

                        <HeaderStyle Font-Bold="True"></HeaderStyle>
                        <PagerSettings Position="TopAndBottom" />
                            <PagerStyle CssClass="GridPager" />
                        <RowStyle Wrap="True"></RowStyle>
                        </asp:GridView>
            </div>
        <div id="divEDIReportData" runat="server" style="overflow-x: auto; width: 100%" visible="false">
            <div>
                <table style="float:right; width: 6%;">
                    <tr>
                        <td><asp:LinkButton ID="btnEDIReportDataExportToExcel" runat="server" OnClick="btnEDIReportDataExportToExcel_Click"><img src="../Images/export_excel.png" alt="Export to XLS" style="width: 30px; height: 30px;"/></asp:LinkButton></td>
                    </tr>
                </table>
            </div>
            <asp:GridView ID="grdEDIReportData" runat="server" AutoGenerateColumns="false" HeaderStyle-Font-Bold="true" Width="100%" CssClass="tblInvoiceDetails"
                            OnPageIndexChanging="grdEDIReportData_PageIndexChanging" AllowPaging="true" PageSize="10" RowStyle-Wrap="true">
                            <Columns>
                                <asp:BoundField HeaderText="AccountID" DataField="AccountNumber" />
                                <asp:BoundField HeaderText="UserName" DataField="UserName" />
                                <asp:BoundField HeaderText="Password" DataField="Password" />
                                <asp:BoundField HeaderText="FTPSite" DataField="FTPSite" />
                                <asp:BoundField HeaderText="EmailID" DataField="Email" />
                                <asp:BoundField HeaderText="EDI" DataField="EDI" />
                            </Columns>

                        <HeaderStyle Font-Bold="True"></HeaderStyle>
                        <PagerSettings Position="TopAndBottom" />
                            <PagerStyle CssClass="GridPager" />
                        <RowStyle Wrap="True"></RowStyle>
            </asp:GridView>
        </div>

    </div>
    
    <div id="divUnextractedData" runat="server">

        <div class="jumbotron" style="background-color: #E48D0F; padding: 10px">
            <h4 style="color: floralwhite">Unextracted View</h4>
        </div>
        <asp:Button ID="btnUnextractedData" runat="server" Width="180px" class="buttonNewInvoice" Text="View Unextracted Data" OnClick="btnUnextractedData_Click" />
        <br />
        <br />        
        <div style="overflow-x: auto; width: 100%" id="divUnExtractedGrid" runat="server" visible="false">            
            <table style="float:right; width: 6%;">
                <tr>
                    <td><asp:LinkButton ID="btnUnextractedDataExportToExcel" runat="server" OnClick="btnUnextractedDataExportToExcel_Click"><img src="../Images/export_excel.png" alt="Export to XLS" style="width: 30px; height: 30px;"/></asp:LinkButton></td>
                    <%--<td><asp:LinkButton ID="btnUnextractedDataExportToPDF" runat="server" OnClick="btnUnextractedDataExportToPDF_Click"><img src="../Images/export_pdf.png" alt="Export to PDF" style="width: 25px; height: 25px;" /></asp:LinkButton></td>--%>
                </tr>
            </table>        
            <table>
                <tr>
                    <td>
                        <asp:GridView ID="grdUnextractedData" runat="server" AutoGenerateColumns="false" HeaderStyle-Font-Bold="true" Width="100%" CssClass="tblInvoiceDetails"
                            OnPageIndexChanging="grdUnextractedData_PageIndexChanging" AllowPaging="true" PageSize="15" RowStyle-Wrap="true">
                            <Columns>
                                <asp:BoundField HeaderText="Id" DataField="Id" />
                                <asp:BoundField HeaderText="Snapshot Id" DataField="SnapshotId" />
                                <asp:BoundField HeaderText="Sub Identifier" DataField="SubIdentifier" />
                                <asp:BoundField HeaderText="First Name" DataField="FirstName" />
                                <asp:BoundField HeaderText="Last Name" DataField="LastName" />
                                <asp:BoundField HeaderText="Asset Search" DataField="AssetSearch" />
                                <asp:BoundField HeaderText="Service Profile" DataField="ServiceProfileID" />
                                <asp:BoundField HeaderText="Provisioning Identifier" DataField="LegalEntity" />
                                <asp:BoundField HeaderText="Directory Number" DataField="DirectoryNumber" />
                                <asp:BoundField HeaderText="Mac Address" DataField="MacAddress" />
                                <asp:BoundField HeaderText="Start Date" DataField="ServiceStartDate" />
                                <asp:BoundField HeaderText="End Date" DataField="ServiceEndDate" />
                                <asp:BoundField HeaderText="State" DataField="State" />
                                <asp:BoundField HeaderText="Postal Code" DataField="PostalCode" />
                                <asp:BoundField HeaderText="Country" DataField="Country" />
                            </Columns>

<HeaderStyle Font-Bold="True"></HeaderStyle>

                            <PagerSettings Position="TopAndBottom" />

                            <PagerStyle CssClass="GridPager" />

<RowStyle Wrap="True"></RowStyle>
                        </asp:GridView>
                    </td>
                </tr>                
            </table>
        </div>
        <div>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblUnextractedData" runat="server" ForeColor="Red" Visible="false"></asp:Label>                        
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
            <table style="width: 100%">
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblAccessDenied" Font-Bold="true" ForeColor="Red">Please contact the application administrator for access.</asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
