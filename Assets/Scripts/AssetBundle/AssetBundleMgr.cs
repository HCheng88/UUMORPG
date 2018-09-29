using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetBundleMgr : Singleton<AssetBundleMgr> {

    #region 同步加载
    /// <summary>
    /// 加载assetBundle到缓存
    /// </summary>
    /// <param name="path"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public GameObject Load(string path,string name)
    {
        using (AssetBundleLoad loader = new AssetBundleLoad(path))
        {
           return loader.LoadAsset<GameObject>(name);
        }
    }
    /// <summary>
    /// 加载并克隆
    /// </summary>
    /// <param name="path"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    public GameObject LoadClone(string path, string name)
    {
        using (AssetBundleLoad loader = new AssetBundleLoad(path))
        {
            GameObject go = loader.LoadAsset<GameObject>(name);
            return Object.Instantiate(go);
        }
    }
    #endregion

    #region 异步加载
    public AssetBundleLoadAsync LoadAsync(string path, string name)
    {
        GameObject obj = new GameObject("AssetBundleLoadAsync");
        AssetBundleLoadAsync async = obj.AddComponent<AssetBundleLoadAsync>();
        async.Init(path, name);
        return async;
    }
    #endregion
}
