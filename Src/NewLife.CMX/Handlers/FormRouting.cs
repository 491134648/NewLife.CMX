﻿using System;
using System.Web;
using NewLife.Web;
using XCode.Membership;

namespace NewLife.CMX.Handlers
{
    public class FormRouting : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            var user = ManageProvider.Provider.Current;
            if (user == null) context.Response.Redirect("Default.aspx");

            var cid = WebHelper.RequestInt("ChannelID");
            var chn = Channel.FindByID(cid);
            //Channel chn = Channel.FindBySuffixOrModel(context.Request["Channel"], WebHelper.RequestInt("ModelID"));

            if (chn != null)
            {
                //ChannelRole cr = ChannelRole.FindChannelIDAndRoleID(chn.ID, Admin.Current.RoleID);
                //if (cr == null) context.Response.Redirect("Default.aspx");
                if (!chn.HasRole(user)) context.Response.Redirect("Default.aspx");

                var url = chn.Model.TitleUrl;
                if (!String.IsNullOrEmpty(url))
                {
                    url += "?" + context.Request.QueryString.ToString();
                    context.Response.Redirect(url);
                }
                else
                {
                    context.Response.Write("路径地址无法解析");
                }
            }
            context.Response.StatusCode = 404;
            context.Response.Write("未知地址！");
            context.Response.End();
        }

        public bool IsReusable { get { return false; } }
    }
}