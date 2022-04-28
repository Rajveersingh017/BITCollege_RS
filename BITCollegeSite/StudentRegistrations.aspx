<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StudentRegistrations.aspx.cs" Inherits="BITCollegeSite.StudentRegistrations" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        <br />
        <asp:Label ID="LblUserName" runat="server" Text="Label"></asp:Label>
    </p>
    <p>
        <asp:GridView ID="GVRegistrations" runat="server" AutoGenerateSelectButton="True" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" Width="708px">
            <Columns>
                <asp:BoundField HeaderText="Course" DataField="Course.CourseNumber" />
                <asp:BoundField HeaderText="Title" DataField="Course.Title" />
                <asp:BoundField HeaderText="Course Type" DataField="Course.courseType" />
                <asp:BoundField DataField="Course.TuitionAmount" HeaderText="Tuition">
                <HeaderStyle HorizontalAlign="Right" />
                <ItemStyle Wrap="True" HorizontalAlign="Right" />
                </asp:BoundField>
                <asp:BoundField HeaderText="Grade" DataField="Grade" DataFormatString="{0:P2}">
                <ItemStyle HorizontalAlign="Right" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
    </p>
    <p>
        <asp:Label ID="Label1" runat="server" Text="Click the Select Link beside a registration (above) to View or Drop the course"></asp:Label>
    </p>
    <p>
        <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">Click Here to Register for a Course</asp:LinkButton>
    </p>
    <p>
        <asp:Label ID="lblExceptions" runat="server" Text="Error/Message" ForeColor="Red" Visible="False"></asp:Label>
    </p>
    <p>
        &nbsp;</p>
</asp:Content>
