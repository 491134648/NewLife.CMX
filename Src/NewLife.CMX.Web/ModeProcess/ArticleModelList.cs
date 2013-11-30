﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using NewLife.CMX.Config;
using NewLife.CMX.TemplateEngine;
using NewLife.CMX.Web;
using NewLife.Web;
using XCode;

namespace NewLife.CMX.Web
{
    /// <summary></summary>
    public class ArticleModelList : ModelListBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dic"></param>
        override public String Process()
        {
            try
            {
                Article.Meta.TableName = "";
                ArticleCategory.Meta.TableName = "";
                Article.Meta.TableName += Suffix;
                ArticleCategory.Meta.TableName += Suffix;

                EntityList<Article> Articles;
                Int32 CountNum = 0;
                EntityList<ArticleCategory> Categories;

                //Channel channel = Channel.FindBySuffix(Suffix);
                ArticleCategory ac = ArticleCategory.FindByID(CategoryID);
                if (ac != null && ac.IsEnd)
                {
                    Articles = Article.Search(null, CategoryID, null, Pageindex * RecordNum, RecordNum);
                    Categories = ArticleCategory.FindAllChildsNoParent(ac.ParentID);
                    CountNum = Article.SearchCount(new int[] { CategoryID }, null, 0, 0);
                }
                else
                {
                    Categories = ArticleCategory.FindAllChildsNoParent(CategoryID).FindAll(delegate(ArticleCategory art)
                    {
                        return art.IsEnd == true;
                    });
                    ArticleCategory first = Categories[0];
                    Articles = Article.Search(null, first.ID, null, Pageindex * RecordNum, RecordNum);
                    CountNum = Article.SearchCount(new int[] { first.ID }, null, 0, 0);
                }

                CountNum = CountNum / 10 + 1;

                Dictionary<String, String> dic = new Dictionary<string, string>();
                dic.Add("Address", Address);
                dic.Add("CategoryID", CategoryID.ToString());
                dic.Add("Pageindex", Pageindex.ToString());
                dic.Add("RecordNum", RecordNum.ToString());
                dic.Add("ContentAddress", channel.FormTemplate);
                dic.Add("ChannelName", ChannelName);
                dic.Add("CountNum", CountNum.ToString());

                CMXEngine engine = new CMXEngine(TemplateConfig.Current, WebSettingConfig.Current);
                engine.ArgDic = dic;
                engine.Header = Header;
                engine.Foot = Foot;
                engine.LeftMenu = LeftMenu;
                engine.Suffix = Suffix;
                //engine.ListEntity = Articles.ConvertAll<IEntity>(e => e as IEntity);
                engine.ListEntity = Articles as IEntityList;
                engine.ListCategory = Categories.ConvertAll<IEntityTree>(e => e as IEntityTree);
                //engine.ListCategory = Categories;
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
