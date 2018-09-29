using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 异步加载AssetBundle
/// </summary>
public class AssetBundleLoadAsync : MonoBehaviour {

    private string mFullPath;
    private string mName;

    private AssetBundleCreateRequest request;
    private AssetBundle bundle;
    public Action<UnityEngine.Object> OnLoadComplete;

    public void Init(string path, string name)
    {
        mFullPath = LocalFileMgr._instance.LocalFilePath + path;
        mName = name;
    }

    void Start () {
        StartCoroutine(Load());
	}
    IEnumerator Load()
    {
        request = AssetBundle.LoadFromMemoryAsync(LocalFileMgr._instance.GetBuffer(mFullPath));
        yield return request;
        bundle = request.assetBundle;
        if (OnLoadComplete != null)
        {
            OnLoadComplete(bundle.LoadAsset(mName));
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        if (bundle != null) bundle.Unload(false);
        mFullPath = null;
        mName = null;
    }
}
