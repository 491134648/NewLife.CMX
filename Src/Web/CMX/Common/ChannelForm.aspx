﻿<%@ Page Title="频道管理" Language="C#" MasterPageFile="~/Admin/ManagerPage.master" AutoEventWireup="true" CodeFile="ChannelForm.aspx.cs" Inherits="CMX_ChannelForm" %>

<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="H">
    <title>频道管理</title>
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="C">
    <div id="contentTab">
        <ul class="tab_nav">
            <li class="selected"><a onclick="tabs('#contentTab',0);" href="javascript:;">编辑频道</a></li>
        </ul>
        <div class="tab_con" style="display: block;">
            <table class="form_table">
                <col width="180px" />
                <col />
                <tbody>
                    <tr>
                        <th>名称：</th>
                        <td>
                            <asp:TextBox ID="frmName" runat="server" Width="150px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <th>模型：</th>
                        <td>
                            <XCL:DropDownList ID="frmModelID" runat="server" DataTextField="Name" DataValueField="ID"></XCL:DropDownList></td>
                    </tr>
                    <tr>
                        <th>后缀：</th>
                        <td>
                            <asp:TextBox ID="frmSuffix" runat="server" Width="150px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <th>启用：</th>
                        <td>
                            <asp:CheckBox ID="frmEnable" runat="server" Text="启用" /></td>
                    </tr>
                    <tr>
                        <th>创建人：</th>
                        <td>
                            <asp:Label ID="frmCreateUserName" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <th>创建时间：</th>
                        <td>
                            <asp:Label ID="frmCreateTime" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <th>更新人：</th>
                        <td>
                            <asp:Label ID="frmUpdateUserName" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <th>更新时间：</th>
                        <td>
                            <asp:Label ID="frmUpdateTime" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <th>备注：</th>
                        <td>
                            <asp:TextBox ID="frmRemark" runat="server" Width="300px"></asp:TextBox></td>
                    </tr>

                    <tr>
                        <th></th>
                        <td>
                            <asp:Button ID="btnSave" runat="server" CausesValidation="True" Text='保存' />
                            &nbsp;<asp:Button ID="btnCopy" runat="server" CausesValidation="True" Text='另存为新频道' />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
