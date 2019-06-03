<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoomBookings.aspx.cs" Inherits="TestWebApp.RoomBookings" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div id="divFilters">
            Date: <asp:TextBox ID="txtBookDate" runat="server" Width="102px"></asp:TextBox>
            Slots: <asp:TextBox ID="txtSlots" runat="server" Width="102px"></asp:TextBox>
            Room: <asp:DropDownList ID="ddlRooms" runat="server" OnSelectedIndexChanged="ddlRooms_SelectedIndexChanged"></asp:DropDownList>
            <asp:Button ID="btnBook" runat="server" Text="Book" OnClick="btnBook_Click" />
        </div>
        <div id="divResults">
            <asp:GridView ID="gvResults" runat="server">
                <Columns>
                    <asp:BoundField HeaderText="Room" DataField="RoomName" />
                    <asp:BoundField HeaderText="Booked from" DataField="BookedFrom" />
                    <asp:BoundField HeaderText="Booked to" DataField="BookedFrom" />
                    <asp:BoundField HeaderText="Booked by" DataField="BookedFrom" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnBook" runat="server" CommandName="Book" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>

    </form>
</body>
</html>
