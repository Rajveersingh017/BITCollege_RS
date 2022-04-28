<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CourseRegistration.aspx.cs" Inherits="BITCollegeSite.CourseRegistration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        <br />
    </p>
    <p>
        <asp:Label ID="lblRegStudenName" runat="server" Text="Label"></asp:Label>
    </p>
    <p>
        <asp:Label ID="Label2" runat="server" Text="Course Selector"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:DropDownList ID="ddlCourseSelector" runat="server" Width="237px">
        </asp:DropDownList>
    </p>
    <p>
        <asp:Label ID="Label3" runat="server" Text="Registration Notes"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="txtNotes" runat="server" Width="373px"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvNotes" runat="server" ControlToValidate="txtNotes" Enabled="False" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>
    </p>
    <p>
        <asp:LinkButton ID="lbRegister" runat="server" OnClick="lbRegister_Click">Register</asp:LinkButton>
&nbsp;&nbsp;&nbsp;
        <asp:LinkButton ID="lbRegList" runat="server" OnClick="lbRegList_Click">Return to Registration Listing</asp:LinkButton>
    </p>
    <p>
        <asp:Label ID="lblRegErrors" runat="server" Text="Error/Message" Visible="False" ForeColor="Red"></asp:Label>
    </p>
    <p>
    </p>
</asp:Content>
