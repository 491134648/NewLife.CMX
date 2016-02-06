﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace NewLife.CMX.Web
{
    /// <summary>视图助手</summary>
    public static class ViewHelper
    {
        public static string GenerateUrl(this UrlHelper url, string routeName)
        {
            return UrlHelper.GenerateUrl(routeName, null, null, null, url.RouteCollection, url.RequestContext, false);
        }

        /// <summary>获取IInfo的Url</summary>
        /// <param name="page"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static String GetUrl(this WebViewPage page, IInfo entity)
        {
            var cat = entity.Category;
            if (cat == null) return null;

            var values = page.Url.RequestContext.RouteData.Values;
            values["modelName"] = cat.Channel.Name;
            values["categoryCode"] = cat.Code;
            values["id"] = entity.ID;

            if (cat.Code.IsNullOrEmpty())
                return page.Url.GenerateUrl("CMX_Title");
            else
                return page.Url.GenerateUrl("CMX_Title2");
        }

        /// <summary>获取到分类页面的链接</summary>
        /// <param name="page"></param>
        /// <param name="cat"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public static String GetCategoryUrl(this WebViewPage page, ICategory cat, Int32 pageIndex = 1)
        {
            if (cat == null || cat.Channel == null) return null;

            var values = page.Url.RequestContext.RouteData.Values;
            values["modelName"] = cat.Channel.Name;
            values["categoryCode"] = cat.Code;
            values["categoryid"] = cat.ID;
            values["pageIndex"] = pageIndex;

            var url = page.Url;
            if (cat.Code.IsNullOrEmpty())
            {
                if (pageIndex <= 1)
                    return page.Url.GenerateUrl("CMX_Category");
                else
                    return page.Url.GenerateUrl("CMX_Category_Page");
            }
            else
            {
                if (pageIndex <= 1)
                    return page.Url.GenerateUrl("CMX_Category2");
                else
                    return page.Url.GenerateUrl("CMX_Category_Page2");
            }
        }

        /// <summary>获取分类的Url</summary>
        /// <param name="page"></param>
        /// <param name="modelName"></param>
        /// <param name="categoryName"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public static String GetCategoryUrl(this WebViewPage page, String modelName, String categoryName, Int32 pageIndex = 1)
        {
            if (modelName.IsNullOrEmpty()) modelName = "Article";

            var chn = Channel.FindByName(modelName);
            if (chn == null) return null;
            var cat = chn.FindCategory(categoryName);
            if (cat == null) return null;

            return page.GetCategoryUrl(cat, pageIndex);
        }

        public static void PushTitle(this WebViewPage page, String title)
        {
            var list = page.ViewData["PageTitle"] as List<String> ?? new List<string>();

            list.Add(title);
            page.ViewData["PageTitle"] = list;
        }

        public static String GetPageTitle(this WebViewPage page)
        {
            var list = page.ViewData["PageTitle"] as List<String> ?? new List<string> { };

            list.Reverse();
            list.Add(SiteConfig.Current.Title);
            return String.Join(" - ", list);
        }
    }
}