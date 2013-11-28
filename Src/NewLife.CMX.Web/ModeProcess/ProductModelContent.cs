﻿using System;
using System.Collections.Generic;
using System.Text;
using NewLife.CMX.TemplateEngine;
using NewLife.CMX.Config;
using XCode;

namespace NewLife.CMX.Web
{
    public class ProductModelContent : ModelContentBase
    {
        override public string Process()
        {
            try
            {
                Product.Meta.TableName += Suffix;
                Product product = Product.FindByKey(ID);
                Product.ChannelSuffix = Suffix;

                LeftMenu = LeftMenuContent.GetContent(Suffix, product.CategoryID);

                if (product == null) return "不存在该记录！";

                Dictionary<String, String> dic = new Dictionary<string, string>();
                dic.Add("Address", Address);
                dic.Add("ID", ID.ToString());
                dic.Add("Suffix", Suffix);
                dic.Add("Header", Header);
                dic.Add("Foot", Foot);
                dic.Add("LeftMenu", LeftMenu);

                CMXEngine engine = new CMXEngine(TemplateConfig.Current);
                engine.ArgDic = dic;
                engine.Entity = product as IEntity;

                String content = engine.Render(Address + ".html");
                return content;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Product.Meta.TableName = "";
            }
        }
    }
}
