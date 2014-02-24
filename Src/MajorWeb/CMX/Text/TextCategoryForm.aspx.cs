﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NewLife.Log;
using NewLife.Web;
using NewLife.CMX;

public partial class CMX_TextCategoryForm : MyModelEntityForm<TextCategory>
{
    protected override void OnInitComplete(EventArgs e)
    {
        base.OnInitComplete(e);

        //只有新添加数据才可以设置是否最终分类
        //任意一个分类的子分类不允许 既有最终分类 又有不是最终分类的情况
        if (!EntityForm.IsNew) frmIsEnd.Enabled = false;
        if ((EntityForm.IsNew && Entity.Parent != null && Entity.Parent.Childs.Count > 0 && !Entity.Parent.Childs[0].IsEnd)) frmIsEnd.Enabled = false;

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        ManagerPage.SetFormScript(true);
    }
}