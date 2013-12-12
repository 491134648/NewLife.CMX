﻿using System;
using System.Collections.Generic;
using System.Text;
using NewLife.CMX.Config;
using NewLife.CMX.TemplateEngine;
using NewLife.CMX.Web;
using XCode;

namespace NewLife.CMX.Web
{
    public class ArticleModelContent : ModelContentBase
    {
        override public string Process()
        {
            try
            {
                Article.Meta.TableName = "";
                ArticleCategory.Meta.TableName = "";
                Article.Meta.TableName += Suffix;
                ArticleCategory.Meta.TableName += Suffix;

                var article = Article.FindByID(ID);
                Article.ChannelSuffix = Suffix;

                LeftMenu = LeftMenuContent.GetContent(Suffix, article.CategoryID);

                if (article == null) return "不存在该记录！";

                Dictionary<String, String> dic = new Dictionary<string, string>();
                dic.Add("Address", Address);
                dic.Add("ID", ID.ToString());
                //dic.Add("Suffix", Suffix);

                CMXEngine engine = new CMXEngine(TemplateConfig.Current, WebSettingConfig.Current);
                engine.ArgDic = dic;
                engine.Header = Header;
                engine.Foot = Foot;
                engine.LeftMenu = LeftMenu;
                engine.Suffix = Suffix;
                engine.Entity = article as IEntity;

                String content = engine.Render(Address + ".html");
                return content;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Article.Meta.TableName = "";
                ArticleCategory.Meta.TableName = "";
            }
        }
    }
}
