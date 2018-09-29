using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAB : MonoBehaviour {	
	void Start () {
        //异步加载
        AssetBundleLoadAsync async = AssetBundleMgr._instance.LoadAsync(@"Role\role_mainplayer. assetbundle", "Role_mainplayer");
        async.OnLoadComplete += OnLoadComplete;
        //同步加载
        //GameObject go = AssetBundleMgr._instance.LoadClone(@"Role\role_mainplayer. assetbundle", "Role_mainplayer");
               
    }
    /// <summary>
    /// 异步加载回调
    /// </summary>
    /// <param name="obj"></param>
    private void OnLoadComplete(UnityEngine.Object obj)
    {
        Instantiate(obj);
    }
}
