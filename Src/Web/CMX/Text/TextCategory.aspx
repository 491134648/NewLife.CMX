﻿<%@ Page Title="文本分类管理" Language="C#" MasterPageFile="~/Admin/ManagerPage.master" AutoEventWireup="true" CodeFile="TextCategory.aspx.cs" Inherits="CMX_TextCategory" %>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="H">
    <title>文本分类管理</title>
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="C">
    <div class="tools_box">
        <div class="tools_bar">
            <a href="TextCategoryForm.aspx" class="tools_btn listpage"><span><b class="add">添加文本分类</b></span></a>
            <div class="search_box">
                关键字：<asp:TextBox ID="txtKey" runat="server"></asp:TextBox><asp:Button ID="btnSearch" runat="server" Text="查询" />
            </div>
        </div>
    </div>
    <asp:GridView ID="gv" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="ods" AllowPaging="True" AllowSorting="True" CssClass="msgtable" PageSize="10" CellPadding="0" GridLines="None" EnableModelValidation="True">
        <Columns>
            <%--<asp:TemplateField>
                <ItemTemplate>
                    <asp:CheckBox ID="cb" runat="server" />
                </ItemTemplate>
                <HeaderStyle Width="20px" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>--%>
            <asp:BoundField DataField="ID" HeaderText="编号" SortExpression="ID" InsertVisible="False" ReadOnly="True" >
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="Ikey" />
            </asp:BoundField>
            <asp:BoundField DataField="Name" HeaderText="名称" SortExpression="Name" />
            <asp:BoundField DataField="ParentID" HeaderText="父类" SortExpression="ParentID" DataFormatString="{0:n0}">
                <ItemStyle HorizontalAlign="Right" Font-Bold="True" />
            </asp:BoundField>
            <asp:BoundField DataField="Sort" HeaderText="排序" SortExpression="Sort" DataFormatString="{0:n0}">
                <ItemStyle HorizontalAlign="Right" Font-Bold="True" />
            </asp:BoundField>
            <asp:BoundField DataField="Remark" HeaderText="备注" SortExpression="Remark" />
                <asp:TemplateField HeaderText="编辑" SortExpression="Name">
                    <ItemTemplate>
                        <asp:HyperLink ID="HyperManager" runat="server" Text='编辑文本分类' NavigateUrl='<%# "TextCategoryForm.aspx?ID="+Eval("ID")%>'></asp:HyperLink>
                    </ItemTemplate>
                      <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            <asp:TemplateField ShowHeader="False" HeaderText="删除">
                <ItemTemplate>
                    <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="False" CommandName="Delete" OnClientClick='return confirm("确定删除吗？")' Text="删除"></asp:LinkButton>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            没有符合条件的数据！
        </EmptyDataTemplate>
    </asp:GridView>
    <asp:ObjectDataSource ID="ods" runat="server" EnablePaging="True" SelectCountMethod="SearchCount" SelectMethod="Search" SortParameterName="orderClause" EnableViewState="false">
        <SelectParameters>
            <asp:ControlParameter ControlID="txtKey" Name="key" PropertyName="Text" Type="String" />
            <asp:Parameter Name="orderClause" Type="String" />
            <asp:Parameter Name="startRowIndex" Type="Int32" />
            <asp:Parameter Name="maximumRows" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <XCL:GridViewExtender ID="gvExt" runat="server">
    </XCL:GridViewExtender>
    <div class="line10"></div>
</asp:Content>