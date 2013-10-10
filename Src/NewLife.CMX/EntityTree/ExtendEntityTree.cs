﻿using System;
using System.Collections.Generic;
using System.Text;
using XCode;

namespace NewLife.CMX
{
    public class ExtendEntityTree<T> : EntityTree<T> where T : ExtendEntityTree<T>, new()
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentKey"></param>
        /// <returns></returns>
        public static Dictionary<String, String> FindChildNameAndIDByNoParent(Int32 parentKey)
        {
            var entity = Meta.Factory.Default as T;

            var list = new EntityList<T>();

            list = FindAllChildsNoParent(parentKey);

            Dictionary<String, String> dic = new Dictionary<String, String>();

            foreach (T item in list)
            {
                if (Convert.ToBoolean(item["IsEnd"]))
                    dic.Add(item["ID"].ToString(), item.TreeNodeName);
                else
                    dic.Add("-" + item["ID"].ToString(), item.TreeNodeName);
            }

            return dic;
        }
    }
}
