﻿<%@ Page Title="产品管理" Language="C#" MasterPageFile="~/Admin/ManagerPage.master" AutoEventWireup="true" CodeFile="ProductForm.aspx.cs" Inherits="CMX_ProductForm"%>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="C">
    <table border="0" class="m_table" cellspacing="1" cellpadding="0" align="Center">
        <tr>
            <th colspan="2">产品</th>
        </tr>
        <tr>
            <td align="right">主题：</td>
            <td><XCL:NumberBox ID="frmSubjectID" runat="server" Width="80px"></XCL:NumberBox></td>
        </tr>
<tr>
            <td align="right">标题：</td>
            <td><asp:TextBox ID="frmTitle" runat="server" Width="150px"></asp:TextBox></td>
        </tr>
    </table>
    <table border="0" align="Center" width="100%">
        <tr>
            <td align="center">
                <asp:Button ID="btnSave" runat="server" CausesValidation="True" Text='保存' />
                &nbsp;<asp:Button ID="btnCopy" runat="server" CausesValidation="True" Text='另存为新产品' />
                &nbsp;<asp:Button ID="btnReturn" runat="server" OnClientClick="parent.Dialog.CloseSelfDialog(frameElement);return false;" Text="返回" />
            </td>
        </tr>
    </table>
</asp:Content>