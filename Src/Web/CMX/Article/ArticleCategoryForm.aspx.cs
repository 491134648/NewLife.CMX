﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NewLife.Log;
using NewLife.Web;
using NewLife.CMX;

public partial class CMX_ArticleCategoryForm : MyModelEntityForm<ArticleCategory>
{
    //private String _suffix;
    ///// <summary>分类扩展名</summary>
    //public String Suffix
    //{
    //    get
    //    {
    //        if (_suffix == null)
    //            _suffix = Request["Channel"];
    //        return _suffix;
    //    }
    //}

    protected override void OnInit(EventArgs e)
    {
        ////防止重复修改
        //ArticleCategory.Meta.TableName = "";
        //ArticleCategory.Meta.TableName += Suffix;

        base.OnInit(e);

        if (!EntityForm.IsNew) frmIsEnd.Enabled = false;
    }

    //protected override void OnSaveStateComplete(EventArgs e)
    //{
    //    base.OnSaveStateComplete(e);

    //    ArticleCategory.Meta.TableName += "";
    //}

    //protected override void OnUnload(EventArgs e)
    //{
    //    //防止用户保存失败或者其他的一些异常操作时候关闭页面的时候将页面的
    //    ArticleCategory.Meta.TableName = "";

    //    base.OnUnload(e);
    //}

    protected void Page_Load(object sender, EventArgs e)
    {
        ManagerPage.SetFormScript(true);
    }
}