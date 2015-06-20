﻿using System;
using System.Web.Mvc;
using NewLife.Web;

namespace NewLife.CMX.Web.Controllers
{
    [AllowAnonymous]
    public class ContentController : Controller
    {
        private Channel _channel;
        public Channel Channel { get { return _channel; } }

        protected virtual Int32 PageSize { get { return 10; } }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //拦截频道名称
            if (filterContext.RouteData.Values.ContainsKey("channelName"))
            {
                var name = filterContext.RouteData.Values["channelName"] + "";
                SetChannel(name);
            }
            if (_channel == null)
            {
                filterContext.Result = new HttpNotFoundResult();
            }
            base.OnActionExecuting(filterContext);
        }

        protected void SetChannel(String name)
        {
            _channel = Channel.FindByName(name);
        }

        public ActionResult Index()
        {
            // 选择模版
            var viewName = Channel.IndexTemplate;
            if (viewName.IsNullOrEmpty()) viewName = "Index";
            viewName = "../{0}/{1}".F(Channel.Name, viewName);

            ViewBag.Channel = Channel;

            return View(viewName, Channel);
        }

        public ActionResult List(Int32 categoryid, Int32? pageindex)
        {
            var cat = Channel.FindCategory(categoryid);
            if (cat == null) return HttpNotFound();

            // 选择模版
            var viewName = cat.GetCategoryTemplate();
            if (viewName.IsNullOrEmpty()) viewName = "Category";
            viewName = "../{0}/{1}".F(Channel.Name, viewName);

            ViewBag.Channel = Channel;
            //ViewBag.Category = cat;
            //ViewBag.PageIndex = pageindex ?? 1;
            //ViewBag.PageSize = PageSize;

            var pager = new Pager { PageIndex = pageindex ?? 1, PageSize = PageSize };
            var list = cat.GetTitles(pager);

            ViewBag.Titles = list;
            ViewBag.Pager = pager;

            return View(viewName, cat);
        }

        /// <summary>主题详细页</summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Detail(Int32 id)
        {
            var title = Channel.FindTitle(id);
            if (title == null) return HttpNotFound();
            var cat = title.Category;

            // 选择模版
            var viewName = cat.GetTitleTemplate();
            if (viewName.IsNullOrEmpty()) viewName = "Title";
            viewName = "../{0}/{1}".F(Channel.Name, viewName);

            // 增加浏览数
            title.Views++;
            title.Statistics.Increment(null);

            ViewBag.Channel = Channel;
            ViewBag.Category = cat;

            return View(viewName, title);
        }
    }
}