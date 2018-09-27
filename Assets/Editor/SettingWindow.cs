using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SettingWindow : EditorWindow
{
    //宏的信息list
    private List<MacorItem> MacorItemList = new List<MacorItem>();
    //保存宏的名称和是否开启
    private Dictionary<string, bool> mDic = new Dictionary<string, bool>();
    //保存unity中的宏信息
    private string mMacor = null;
    /// <summary>
    ///获取宏内容，True为需要获取，false为已经获取
    /// </summary>
    private bool IsInit;

    public SettingWindow()
    {
        MacorItemList.Clear();
        //添加宏信息
        MacorItemList.Add(new MacorItem() { Name = "DEBUG_MODEL", DisplauName = "调试模式", IsDebug = true, IsRelease = false });
        MacorItemList.Add(new MacorItem() { Name = "DEBUG_LOG", DisplauName = "打印日志", IsDebug = true, IsRelease = false });
        MacorItemList.Add(new MacorItem() { Name = "STAT_TD", DisplauName = "开启统计", IsDebug = false, IsRelease = true });
        IsInit = true;
        //添加到字典中
        for (int i = 0; i < MacorItemList.Count; i++)
        {
            mDic[MacorItemList[i].Name] = false;
        }
    }

    private void OnGUI()
    {
        if (IsInit)
        {
            //获取宏内容，参数是哪个平台，Standalone是mc、pc（注意不能在构造函数中获取）
            mMacor = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone);
            //判断宏中是否存在list数组中的宏，存在togger选中，不存在不选中
            for (int i = 0; i < MacorItemList.Count; i++)
            {
                //获取的宏（mMacor）不为空，list中的宏在mMacor中的索引不为-1即存在
                if (!string.IsNullOrEmpty(mMacor) && mMacor.IndexOf(MacorItemList[i].Name) != -1)
                {
                    mDic[MacorItemList[i].Name] = true;
                }
                else
                {
                    mDic[MacorItemList[i].Name] = false;
                }
            }
            IsInit = false;
        }
        for (int i = 0; i < MacorItemList.Count; i++)
        {
            //开启一个水平行(仅仅是一行)
            EditorGUILayout.BeginHorizontal("Box");
            //开启一个文本
            //GUILayout.Label("强啊这玩意！");//添加一个label显示字体类似ugui中text
            //将字典中的内容建成Toggle（bool值，要显示的名字）
            mDic[MacorItemList[i].Name] = GUILayout.Toggle(mDic[MacorItemList[i].Name], MacorItemList[i].DisplauName);
            //关闭行，必须成对出现
            EditorGUILayout.EndHorizontal();
        }
        //启动新的一行新三个按钮
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("保存", GUILayout.Width(100)))
        {
            SaveMacor();
        }
        if (GUILayout.Button("调试模式", GUILayout.Width(100)))
        {
            for (int i = 0; i < MacorItemList.Count; i++)
            {
                //字典中状态根据预设改变Togger跟着改变
                mDic[MacorItemList[i].Name] = MacorItemList[i].IsDebug;
            }
            SaveMacor();
        }
        if (GUILayout.Button("发布模式", GUILayout.Width(100)))
        {
            for (int i = 0; i < MacorItemList.Count; i++)
            {
                //字典中状态根据预设改变Togger跟着改变
                mDic[MacorItemList[i].Name] = MacorItemList[i].IsRelease;
            }
            SaveMacor();
        }
        EditorGUILayout.EndHorizontal();
    }
    /// <summary>
    /// 保存
    /// </summary>
    private void SaveMacor()
    {
        mMacor = string.Empty;
        foreach (var item in mDic)
        {
            if (item.Value)
            {
                //要加分号
                mMacor += string.Format("{0};", item.Key);
            }
        }
        //保存三个平台
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, mMacor);
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS, mMacor);
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, mMacor);
    }
    /// <summary>
    /// 宏的属性
    /// </summary>
    public class MacorItem
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name;
        /// <summary>
        /// 显示的名称
        /// </summary>
        public string DisplauName;
        /// <summary>
        /// 是否调试
        /// </summary>
        public bool IsDebug;
        /// <summary>
        /// 是否发布项
        /// </summary>
        public bool IsRelease;
    }
}
