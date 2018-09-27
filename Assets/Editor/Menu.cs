using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;

public class Menu  {
    [MenuItem("MyTools/Setting")]
    public static void Setting()
    {
        //实例化
        SettingWindow win = (SettingWindow)EditorWindow.GetWindow(typeof(SettingWindow));
        //设置窗口名称
        win.titleContent = new GUIContent("全局设置");
        //窗口显示
        win.Show();
    }
    [MenuItem("MyTools/AssetBundleCreate")]
    public static void AssetBundleCreate()
    {        
        // 实例化
        AssetBundleWindow win = EditorWindow.GetWindow<AssetBundleWindow>();
        //设置窗口名称
        win.titleContent = new GUIContent("AssetBundle");
        //窗口显示
        win.Show();
    }
}
