﻿using System;
using NewLife.Reflection;
using System.Collections.Generic;
using System.Linq;
using NewLife.CMX.Config;
using NewLife.CMX.TemplateEngine;
using XCode;

namespace NewLife.CMX.Web
{
    public class LeftMenuContent
    {
        /// <summary>获取菜单内容</summary>
        /// <returns></returns>
        public static String GetContent(Channel channel, Int32 CategoryID)
        {

            //var channel = Channel.FindBySuffix(Suffix);
            var dic = new Dictionary<String, String>();
            //var classname = channel.Model.ClassName;
            var id = 0;

            //var eop = EntityFactory.CreateOperate(classname);
            var eop = EntityFactory.CreateOperate(channel.Model.Provider.CategoryType);
            //var root = eop.Default.GetValue("Root") as IEntityTree;
            var root = eop.Create().GetValue("Root") as IEntityTree;
            try
            {
                // 使用上级分类
                if (CategoryID != 0)
                {
                    //var entity = eop.Find("ID", CategoryID) as IEntityTree;
                    //if (entity != null && entity.Parent != null) id = (Int32)entity.Parent["ID"];
                    var entity = root.AllChilds.Find("ID", CategoryID) as IEntityTree;
                    //对于自身为最终分类的节点，前台选中打开节点即自身
                    if (entity != null)
                    {
                        id = (Int32)entity["ParentID"];

                        if ((Boolean)entity.GetValue("IsEnd") && id == 0) id = (Int32)entity.GetValue("ID");
                    }
                }

                dic.Add("ModelListAddress", channel.ListTemplate);
                dic.Add("ModelFormTemplate", channel.FormTemplate);
                dic.Add("SelectedCategory", id + "");
                dic.Add("MenuTitle", channel.Name);


                var engine = new CMXEngine(TemplateConfig.Current, WebSettingConfig.Current);
                engine.ListCategory = root.Childs.ToList().OrderBy(e => e["ID"]).ToList().ConvertAll<IEntityTree>(e => e as IEntityTree);
                engine.ArgDic = dic;
                engine.Suffix = String.IsNullOrEmpty(channel.Suffix) ? "$" : channel.Suffix;
                engine.ModelShortName = String.IsNullOrEmpty(channel.Model.ShortName) ? "$" : channel.Model.ShortName;

                return engine.Render(TemplateConfig.Current.LeftAddress);
            }
            finally
            {
                eop.Create().SetItem("Root", null);
            }
        }
    }
}