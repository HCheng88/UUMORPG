using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;
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

        for(int i = 0; i < mList.Count; i++)
        {
            AssetBundleEntity entity = mList[i];
            GUILayout.BeginHorizontal("box");       
            mDic[entity.Key] = GUILayout.Toggle(mDic[entity.Key], "", GUILayout.Width(20));
            GUILayout.Label(entity.Name);
            GUILayout.Label(entity.Tag, GUILayout.Width(100));
            GUILayout.Label(entity.ToPath, GUILayout.Width(200));
            GUILayout.Label(entity.Version.ToString(), GUILayout.Width(100));
            GUILayout.Label(entity.Size.ToString(), GUILayout.Width(100));
            GUILayout.EndHorizontal();
            foreach (string path in entity.PathList)
            {
                GUILayout.BeginHorizontal("box");
                GUILayout.Space(40);
                GUILayout.Label(path);
                GUILayout.EndHorizontal();
            }
        }

        EditorGUILayout.EndScrollView();

        GUILayout.EndVertical();
    }



    /// <summary>
    /// 选定Tag回调（unity的打包标签）
    /// </summary>
    private void OnSelectTagCallBack()
    {
        switch (tagIndex)
        {
            case 0://All
                foreach (AssetBundleEntity entity in mList)
                {
                    mDic[entity.Key] = true;
                }
                break;
            case 1://Scence
                foreach (AssetBundleEntity entity in mList)
                {
                    mDic[entity.Key] = entity.Tag.Equals("Scence", StringComparison.CurrentCultureIgnoreCase);
                }
                break;
            case 2://Role
                foreach (AssetBundleEntity entity in mList)
                {
                    mDic[entity.Key] = entity.Tag.Equals("Role", StringComparison.CurrentCultureIgnoreCase);
                }
                break;
            case 3://Effect
                foreach (AssetBundleEntity entity in mList)
                {
                    mDic[entity.Key] = entity.Tag.Equals("Effect", StringComparison.CurrentCultureIgnoreCase);
                }
                break;
            case 4://Audio
                foreach (AssetBundleEntity entity in mList)
                {
                    mDic[entity.Key] = entity.Tag.Equals("Audio", StringComparison.CurrentCultureIgnoreCase);
                }
                break;
            case 5://None
                foreach (AssetBundleEntity entity in mList)
                {
                    mDic[entity.Key] = false;
                }
                break;

            default:
                break;
        }
        Debug.LogFormat("当前选择的Tag：{0}", arrTag[tagIndex]);
    }
    /// <summary>
    /// 选定Target回调（打包平台）
    /// </summary>
    private void OnSelectTargetCallBack()
    {
        switch (buildTargetIndex)
        {
            case 0://Windows
                target = BuildTarget.StandaloneWindows;
                break;
            case 1://Android
                target = BuildTarget.Android;
                break;
            case 2://IOS
                target = BuildTarget.iOS;
                break;
            default:
                break;
        }
        Debug.LogFormat("当前选择的target：{0}", arrBuidTarget[buildTargetIndex]);
    }
    /// <summary>
    /// 打包回调
    /// </summary>
    private void OnAssetBundleCallBack()
    {
        List<AssetBundleEntity> listNeed = new List<AssetBundleEntity>();
        foreach (AssetBundleEntity entity in mList)
        {
            if (mDic[entity.Key])
                listNeed.Add(entity);
        }
        for(int i = 0; i < listNeed.Count; i++)
        {
            Debug.LogFormat("正在打包{0}/{1}", i + 1, listNeed.Count);
            BuildAssetBundle(listNeed[i]);
        }
        Debug.Log("打包完成");
    }
    /// <summary>
    /// 打包方法
    /// </summary>
    /// <param name="entity"></param>
    private void BuildAssetBundle(AssetBundleEntity entity)
    {
        AssetBundleBuild[] arrBuild = new AssetBundleBuild[1];
        AssetBundleBuild build = new AssetBundleBuild();
        //包名及后缀
        build.assetBundleName = string.Format("{0}. {1}",entity.Name, (entity.Tag.Equals("Scence", StringComparison.CurrentCultureIgnoreCase) ? "unity3d" : "assetbundle"));
        //AssetBundle包的后缀名...写成下面两行代码会报错，放到上面就不会报错
        //string variant = (entity.Tag.Equals("Scence", StringComparison.CurrentCultureIgnoreCase) ? "unity3d" : "assetbundle");
        //build.assetBundleVariant = variant;


        //资源路径
        build.assetNames = entity.PathList.ToArray();

        arrBuild[0] = build;
        //资源打包保存路径
        string toPath = Application.dataPath + "/../AssetBundle/" + arrBuidTarget[buildTargetIndex] + entity.ToPath;
        //如果目标不存在，则创建文件夹
        if (!Directory.Exists(toPath))
        {
            Directory.CreateDirectory(toPath);
        }
        //四个参数是按照第二个参数的格式来打包，第二个参数保存的是AssetBundle打包的名字、后缀、源文件地址等
        BuildPipeline.BuildAssetBundles(toPath,arrBuild,BuildAssetBundleOptions.None,target);
    }
    /// <summary>
    /// 清空AssetBundle包回调(删除打包后的资源)
    /// </summary>
    private void OnClearAssetBundleCallBack()
    {
        string Path = Application.dataPath + "/../AssetBundle/" + arrBuidTarget[buildTargetIndex];
        if (Directory.Exists(Path))
        {
            Directory.Delete(Path,true);
        }

        Debug.Log("删除成功");
    }
}
