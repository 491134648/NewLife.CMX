﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using XTemplate.Templating;

namespace NewLife.CMX.Templates
{
    /// <summary>首页模版基类。首页模版生成类继承于此类</summary>
    public class Index : PageBase
    {
        public static void Process()
        {
            var tt = Template.Create("index.html");
            //编译文件
            tt.Compile();

            var html = tt.Render(null, null);
            HttpContext.Current.Response.Write(html);
        }
    }
}