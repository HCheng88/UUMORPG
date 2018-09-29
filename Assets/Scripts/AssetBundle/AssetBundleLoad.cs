using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 同步加载资源包
/// </summary>
public class AssetBundleLoad:IDisposable  {

    private AssetBundle bundle;
    public AssetBundleLoad(string assetBundlePath)
    {
        string fullPath = LocalFileMgr._instance.LocalFilePath + assetBundlePath;
        bundle = AssetBundle.LoadFromMemory(LocalFileMgr._instance.GetBuffer(fullPath));
    }
    public T LoadAsset<T>(string name)where T : UnityEngine.Object
    {
        if (bundle == null) return default(T);
        return bundle.LoadAsset(name) as T;
    }

    void IDisposable.Dispose()
    {
        if (bundle != null) bundle.Unload(false);
    }
}
