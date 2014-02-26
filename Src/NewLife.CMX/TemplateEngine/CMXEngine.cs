﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using NewLife.CMX.Config;
using XCode;
using XTemplate.Templating;

namespace NewLife.CMX.TemplateEngine
{
    public class CMXEngine
    {
        #region 属性
        private TemplateConfig _Config;
        /// <summary>模板配置</summary>
        public TemplateConfig Config { get { return _Config; } set { _Config = value; } }

        private WebSettingConfig _WebSettingConfig;
        /// <summary>网站配置信息</summary>
        public WebSettingConfig WebSettingConfig { get { return _WebSettingConfig; } set { _WebSettingConfig = value; } }

        //private String _LeftMenu;
        ///// <summary>左侧导航菜单</summary>
        //public String LeftMenu { get { return _LeftMenu; } set { _LeftMenu = value; } }

        //private String _Header;
        ///// <summary>页头</summary>
        //public String Header { get { return _Header; } set { _Header = value; } }

        //private String _Foot;
        ///// <summary>页脚</summary>
        //public String Foot { get { return _Foot; } set { _Foot = value; } }

        private Dictionary<String, String> _ArgDic;
        /// <summary>参数字典</summary>
        public Dictionary<String, String> ArgDic { get { return _ArgDic; } set { _ArgDic = value; } }

        private IEntityList _ListEntity;
        /// <summary>数据列表</summary>
        public IEntityList ListEntity { get { return _ListEntity; } set { _ListEntity = value; } }

        private List<IEntityTree> _ListCategory;
        /// <summary>分类列表</summary>
        public List<IEntityTree> ListCategory { get { return _ListCategory; } set { _ListCategory = value; } }

        private IEntity _Entity;
        /// <summary>实体数据</summary>
        public IEntity Entity { get { return _Entity; } set { _Entity = value; } }

        private IEntityTree _RootEntityTree;
        /// <summary>实体树根节点</summary>
        public IEntityTree RootEntityTree { get { return _RootEntityTree; } set { _RootEntityTree = value; } }

        private String _Suffix;
        /// <summary>扩展名称</summary>
        public String Suffix { get { return _Suffix; } set { _Suffix = value; } }

        private String _ModelShortName;
        /// <summary>模型缩写</summary>
        public String ModelShortName { get { return _ModelShortName; } set { _ModelShortName = value; } }

        #endregion

        #region 构造
        static CMXEngine()
        {
            Template.BaseClassName = typeof(CMXTemplateBase).FullName;
        }

        public CMXEngine(TemplateConfig config, WebSettingConfig setting)
        {
            Config = config;
            WebSettingConfig = setting;
        }
        #endregion

        #region 生成
        /// <summary>
        /// 生成模板
        /// </summary>
        /// <param name="TemplateName"></param>
        /// <returns></returns>
        public String Render(String TemplateName)
        {
            var data = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            data["Config"] = Config;
            data["WebSettingConfig"] = WebSettingConfig;
            //data["Header"] = Header;
            //data["Foot"] = Foot;
            //data["LeftMenu"] = LeftMenu;
            data["ArgDic"] = ArgDic;
            data["ListEntity"] = ListEntity;
            data["ListCategory"] = ListCategory;
            data["Entity"] = Entity;
            data["RootEntityTree"] = RootEntityTree;
            data["Suffix"] = Suffix;
            data["ModelShortName"] = ModelShortName;

            #region 获取模板资源文件
            Template.Debug = TemplateConfig.Current.Debug;
            Dictionary<String, String> tempdic = new Dictionary<String, String>();

            String WebPath = HttpRuntime.AppDomainAppPath;
            String Templatepath = WebPath.CombinePath(Config.Root, Config.Style);

            //校验模板目录
            if (!Directory.Exists(Templatepath)) throw new Exception("指定样式模板不存在！");
            //校验请求文件
            var RequestFile = Templatepath.CombinePath(TemplateName);
            if (!File.Exists(RequestFile)) throw new FileNotFoundException("请求地址不存在！", RequestFile);
            #endregion

            #region 过滤忽略文件
            //获取忽略文件类型数组
            //String[] IgnoreExtentList = String.IsNullOrEmpty(Config.IgnoreExtendName) ? new string[] { } : Config.IgnoreExtendName.Split(',');
            //如果是忽略列表中的文件直接返回
            String TempContent = File.ReadAllText(RequestFile, Encoding.UTF8);

            //if (IgnoreExtentList.Contains(Path.GetExtension(RequestFile).TrimStart('.'))) return TempContent;

            tempdic.Add(TemplateName, TempContent);
            #endregion

            #region 生成文件
            //List<String> imports = Config.ImportsAssembly.Split(",").ToList();
            ////添加程序集引用
            //Template.Imports.AddRange(imports);

            Template tt = Template.Create(tempdic);
            //编译文件
            tt.Compile();

            String ResultContent = tt.Render(TemplateName, data);

            //if (Config.Debug)
            //{
            //    String Outputpath = WebPath.CombinePath(Config.OutputPath, Config.Style);
            //    if (!Directory.Exists(Outputpath)) Directory.CreateDirectory(Outputpath);
            //    Outputpath = Outputpath.CombinePath(TemplateName);
            //    File.WriteAllText(Outputpath, ResultContent, Encoding.UTF8);
            //}
            return ResultContent;
            #endregion
        }
        #endregion
    }
}