﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Web.UI;
using NewLife.CMX;
using NewLife.CMX.Config;
using NewLife.CMX.Web;
using NewLife.Log;
using NewLife.Reflection;
using NewLife.Web;

public partial class Template_Info : Page
{
    /// <summary>类编码</summary>
    private String Suffix { get { return Request["Suffix"]; } }

    /// <summary>模型缩写</summary>
    private String ModelShortName { get { return Request["ModelSN"]; } }

    /// <summary>请求地址</summary>
    private String Address
    {
        get
        {
            String ad = Request["Address"];

            if (string.IsNullOrEmpty(ad)) ad = C.FormTemplate;
            //ad = ad.Substring(0, ad.IndexOf('.'));
            return ad;
        }
    }

    private Int32 ID
    {
        get
        {
            Int32 i = WebHelper.RequestInt("ID");
            if (i <= 0) Err("未知的参数！");
            return i;
        }
    }

    private Channel _C;
    /// <summary>频道</summary>
    private Channel C
    {
        get
        {
            if (_C == null & !String.IsNullOrEmpty(Suffix))
                _C = Channel.FindBySuffixOrModelShortName(Suffix, ModelShortName);

            if (_C == null)
                Err("未确定的频道！");
            else if (String.IsNullOrEmpty(_C.FormTemplate))
                Err("未绑定模版！");

            return _C;
        }
        set { _C = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //String content = "";
        //try
        //{
        TypeX type = TypeX.GetType("NewLife.CMX.Web." + C.FormTemplate);
        IModelContent iml = type.CreateInstance() as IModelContent;

        iml.Suffix = Suffix;
        iml.ModelShortName = ModelShortName;
        iml.Address = Address;
        iml.ID = ID;
        String content = iml.Process();
        //}
        //catch (ThreadAbortException)
        //{
        //    Response.Redirect(CMXConfigBase.Current.CurrentRootPath + "/Index.html");
        //}
        //catch (Exception err)
        //{
        //    XTrace.WriteException(err);

        //    Err("编译出错！");
        //}

        Response.Write(content);
    }

    private void Err(String Msg)
    {
        Response.Clear();
        Response.Write(Msg);
        Response.End();
    }
}