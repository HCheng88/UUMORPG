using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
/// <summary>
/// AssetBundle管理窗口
/// </summary>
public class AssetBundleWindow : EditorWindow {
    AssetBundleDAL dal;
    List<AssetBundleEntity> mList;

    private Dictionary<string, bool> mDic;
    private string[] arrTag = { "All", "Scence", "Role", "Effect", "Audio", "None" };
    private int tagIndex = 0;
    private string[] arrBuidTarget = { "Windows", "Android", "IOS" };

    private bool IsRun = true;

    private Vector2 pos;

#if UNITY_STANDALONE_WIN
    private BuildTarget target = BuildTarget.StandaloneWindows;
    private int buildTargetIndex = 0;
#elif UNITY_ANDROID
     private BuildTarget target = BuildTarget.Android;
    private int buildTargetIndex = 1;
#elif UNITY_IPHONE
     private BuildTarget target = BuildTarget.iOS;
    private int buildTargetIndex = 2;
#endif
    /// <summary>
    /// 构造函数
    /// </summary>
    public AssetBundleWindow()
    {
        
    }
    /// <summary>
    /// 绘制窗口
    /// </summary>
    public void OnGUI()
    {
        while (IsRun)
        {
            string path = Application.dataPath + "/Editor/AssetBundle/AssetBundleConfig.xml";
            dal = new AssetBundleDAL(path);
            mList = dal.GetList();
            mDic = new Dictionary<string, bool>();
            for (int i = 0; i < mList.Count; i++)
            {
                mDic[mList[i].Key] = true;
            }
            IsRun = false;
            break;
        }
        if (mList == null) return;
        #region 按钮行
        EditorGUILayout.BeginHorizontal("box");
        //新建一个下拉列表
        tagIndex = EditorGUILayout.Popup(tagIndex, arrTag, GUILayout.Width(100));
        //创建一个按钮
        if (GUILayout.Button("选定Tag", GUILayout.Width(100)))
        {
            EditorApplication.delayCall = OnSelectTagCallBack;
        }
        //新建一个下拉列表
        buildTargetIndex = EditorGUILayout.Popup(buildTargetIndex, arrBuidTarget, GUILayout.Width(100));
        if (GUILayout.Button("选定Target", GUILayout.Width(100)))
        {
            EditorApplication.delayCall = OnSelectTargetCallBack;
        }
        if (GUILayout.Button("打AssetBundle包", GUILayout.Width(200)))
        {
            EditorApplication.delayCall = OnAssetBundleCallBack;
        }
        if (GUILayout.Button("清空AssetBundle包", GUILayout.Width(200)))
        {
            EditorApplication.delayCall = OnClearAssetBundleCallBack;
        }
        EditorGUILayout.Space();//用空白自动填充剩余的部分
        
        EditorGUILayout.EndHorizontal();
        #endregion
        #region 显示包内容
        GUILayout.BeginHorizontal("BOX");
        GUILayout.Label("包名");
        GUILayout.Label("标记", GUILayout.Width(100));
        GUILayout.Label("保存路径", GUILayout.Width(200));
        GUILayout.Label("版本", GUILayout.Width(100));
        GUILayout.Label("大小", GUILayout.Width(100));
        GUILayout.EndHorizontal();
        #endregion

        GUILayout.BeginVertical();

        pos = EditorGUILayout.BeginScrollView(pos);

        EditorGUILayout.EndScrollView();

        GUILayout.EndVertical();
    }



    /// <summary>
    /// 选定Tag回调（unity的打包标签）
    /// </summary>
    private void OnSelectTagCallBack()
    {
        Debug.Log(1);
    }
    /// <summary>
    /// 选定Target回调（打包平台）
    /// </summary>
    private void OnSelectTargetCallBack()
    {
        Debug.Log(2);
    }
    /// <summary>
    /// 打包回调
    /// </summary>
    private void OnAssetBundleCallBack()
    {
        Debug.Log(3);
    }
    /// <summary>
    /// 清空AssetBundle包回调(删除打包后的资源)
    /// </summary>
    private void OnClearAssetBundleCallBack()
    {
        Debug.Log(4);
    }
}
