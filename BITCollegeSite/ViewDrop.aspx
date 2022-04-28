<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ViewDrop.aspx.cs" Inherits="BITCollegeSite.ViewDrop" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        <br />
    </p>
    <p>
        <asp:DetailsView ID="dvDropViewDetails" runat="server" AutoGenerateRows="False" BackColor="#CCCCCC" BorderColor="#999999" BorderStyle="Solid" BorderWidth="3px" CellPadding="4" CellSpacing="2" ForeColor="Black" Height="50px" Width="295px" AllowPaging="True" OnPageIndexChanging="dvDropViewDetails_PageIndexChanging">
            <EditRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
            <Fields>
                <asp:BoundField DataField="RegistrationId" HeaderText="Registration" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Student.FullName" HeaderText="Student" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Course.Title" HeaderText="Course" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="RegistrationDate" DataFormatString="{0:d}" HeaderText="Date" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="Grade" DataFormatString="{0:p}" HeaderText="Grade" >
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
            </Fields>
            <FooterStyle BackColor="#CCCCCC" />
            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
            <RowStyle BackColor="White" />
        </asp:DetailsView>
    </p>
    <p>
&nbsp;<asp:LinkButton ID="lbDrop" runat="server" OnClick="lbDrop_Click" Enabled="False">Drop Course</asp:LinkButton>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:LinkButton ID="lbGoBack" runat="server" OnClick="LinkButton2_Click">Return to Registration Listing</asp:LinkButton>
    </p>
    <p>
        <asp:Label ID="lblViewDropExceptions" runat="server" Text="Error/Message" ForeColor="Red" Visible="False"></asp:Label>
    </p>
</asp:Content>
