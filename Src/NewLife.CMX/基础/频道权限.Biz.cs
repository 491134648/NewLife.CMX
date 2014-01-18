﻿/*
 * XCoder v5.1.5000.39465
 * 作者：XYF/XYF-PC
 * 时间：2013-09-16 17:45:40
 * 版权：版权所有 (C) 新生命开发团队 2002~2013
*/
﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml.Serialization;
using NewLife.CommonEntity;
using NewLife.Log;
using NewLife.Web;
using XCode;
using XCode.Configuration;

namespace NewLife.CMX
{
    /// <summary>频道权限</summary>
    public partial class ChannelRole : Entity<ChannelRole>
    {
        #region 对象操作﻿

        /// <summary>验证数据，通过抛出异常的方式提示验证失败。</summary>
        /// <param name="isNew"></param>
        public override void Valid(Boolean isNew)
        {
            // 这里验证参数范围，建议抛出参数异常，指定参数名，前端用户界面可以捕获参数异常并聚焦到对应的参数输入框
            //if (String.IsNullOrEmpty(Name)) throw new ArgumentNullException(__.Name, _.Name.DisplayName + "无效！");
            //if (!isNew && ID < 1) throw new ArgumentOutOfRangeException(__.ID, _.ID.DisplayName + "必须大于0！");

            // 建议先调用基类方法，基类方法会对唯一索引的数据进行验证
            base.Valid(isNew);

            // 在新插入数据或者修改了指定字段时进行唯一性验证，CheckExist内部抛出参数异常
            if (isNew || Dirtys[__.ChannelID] || Dirtys[__.RoleID]) CheckExist(new String[] { __.ChannelID, __.RoleID });

        }

        /// <summary>首次连接数据库时初始化数据，仅用于实体类重载，用户不应该调用该方法</summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        protected override void InitData()
        {
            base.InitData();

            // InitData一般用于当数据表没有数据时添加一些默认数据，该实体类的任何第一次数据库操作都会触发该方法，默认异步调用
            // Meta.Count是快速取得表记录数
            if (Meta.Count > 0) return;

            // 需要注意的是，如果该方法调用了其它实体类的首次数据库操作，目标实体类的数据初始化将会在同一个线程完成
            if (XTrace.Debug) XTrace.WriteLine("开始初始化{0}[{1}]数据……", typeof(ChannelRole).Name, Meta.Table.DataTable.DisplayName);

            Channel.Meta.WaitForInitData(3000);
            foreach (var item in Channel.Meta.Cache.Entities)
            {
                var entity = new ChannelRole();
                entity.RoleID = ManageProvider.Provider.Current.ID;
                entity.ChannelID = item.ID;
                entity.Save();
            }

            if (XTrace.Debug) XTrace.WriteLine("完成初始化{0}[{1}]数据！", typeof(ChannelRole).Name, Meta.Table.DataTable.DisplayName);
        }
        
        ///// <summary>已重载。基类先调用Valid(true)验证数据，然后在事务保护内调用OnInsert</summary>
        ///// <returns></returns>
        //public override Int32 Insert()
        //{
        //    return base.Insert();
        //}

        ///// <summary>已重载。在事务保护范围内处理业务，位于Valid之后</summary>
        ///// <returns></returns>
        //protected override Int32 OnInsert()
        //{
        //    return base.OnInsert();
        //}
        #endregion

        #region 扩展属性﻿
        private Role _Role;
        /// <summary>角色</summary>
        public Role Role
        {
            get
            {
                if (_Role == null && RoleID > 0 && !Dirtys.ContainsKey("Role"))
                {
                    _Role = Role.FindByID(RoleID);
                    Dirtys["Role"] = true;
                }
                return _Role;
            }
            set { _Role = value; }
        }

        /// <summary>角色名</summary>
        public String RoleName { get { return Role != null ? Role.Name : "未知角色"; } }

        private Channel _Channel;
        /// <summary>频道</summary>
        public Channel Channel
        {
            get
            {
                if (_Channel == null && ChannelID > 0 && !Dirtys.ContainsKey("Channel"))
                {
                    _Channel = Channel.FindByID(ChannelID);
                    Dirtys["Channel"] = true;
                }
                return _Channel;
            }
            set { _Channel = value; }
        }

        /// <summary>频道名</summary>
        public String ChannelName { get { return Channel != null ? Channel.Name : "未知频道"; } }
        #endregion

        #region 扩展查询﻿
        /// <summary>
        /// 根据角色ID查询所有
        /// </summary>
        /// <param name="RoleID"></param>
        /// <returns></returns>
        public static List<ChannelRole> FindAllByRoleID(Int32 RoleID)
        {
            if (Meta.Count >= 1000)
                return FindAll(__.RoleID, RoleID);
            else
                return Meta.Cache.Entities.FindAll(__.RoleID, RoleID);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ChannelID"></param>
        /// <param name="RoleID"></param>
        public static ChannelRole FindChannelIDAndRoleID(int ChannelID, int RoleID)
        {
            if (Meta.Count >= 1000)
                return Find(new String[] { _.ChannelID, _.RoleID }, new object[] { ChannelID, RoleID });
            else
                return Meta.Cache.Entities.Find(e => e.ChannelID == ChannelID && e.RoleID == RoleID);
        }
        #endregion

        #region 高级查询
        // 以下为自定义高级查询的例子

        ///// <summary>
        ///// 查询满足条件的记录集，分页、排序
        ///// </summary>
        ///// <param name="key">关键字</param>
        ///// <param name="orderClause">排序，不带Order By</param>
        ///// <param name="startRowIndex">开始行，0表示第一行</param>
        ///// <param name="maximumRows">最大返回行数，0表示所有行</param>
        ///// <returns>实体集</returns>
        //[DataObjectMethod(DataObjectMethodType.Select, true)]
        //public static EntityList<ChennalRole> Search(String key, String orderClause, Int32 startRowIndex, Int32 maximumRows)
        //{
        //    return FindAll(SearchWhere(key), orderClause, null, startRowIndex, maximumRows);
        //}

        ///// <summary>
        ///// 查询满足条件的记录总数，分页和排序无效，带参数是因为ObjectDataSource要求它跟Search统一
        ///// </summary>
        ///// <param name="key">关键字</param>
        ///// <param name="orderClause">排序，不带Order By</param>
        ///// <param name="startRowIndex">开始行，0表示第一行</param>
        ///// <param name="maximumRows">最大返回行数，0表示所有行</param>
        ///// <returns>记录数</returns>
        //public static Int32 SearchCount(String key, String orderClause, Int32 startRowIndex, Int32 maximumRows)
        //{
        //    return FindCount(SearchWhere(key), null, null, 0, 0);
        //}

        /// <summary>构造搜索条件</summary>
        /// <param name="key">关键字</param>
        /// <returns></returns>
        private static String SearchWhere(String key)
        {
            // WhereExpression重载&和|运算符，作为And和Or的替代
            // SearchWhereByKeys系列方法用于构建针对字符串字段的模糊搜索
            var exp = SearchWhereByKeys(key, null);

            // 以下仅为演示，Field（继承自FieldItem）重载了==、!=、>、<、>=、<=等运算符（第4行）
            //if (userid > 0) exp &= _.OperatorID == userid;
            //if (isSign != null) exp &= _.IsSign == isSign.Value;
            //if (start > DateTime.MinValue) exp &= _.OccurTime >= start;
            //if (end > DateTime.MinValue) exp &= _.OccurTime < end.AddDays(1).Date;

            return exp;
        }
        #endregion

        #region 扩展操作
        #endregion

        #region 业务

        #endregion


    }
}