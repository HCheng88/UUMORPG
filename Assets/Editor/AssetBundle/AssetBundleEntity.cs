using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// AssetBundle实体
/// </summary>
public class AssetBundleEntity {

    /// <summary>
    /// 打包的时候的Key 唯一
    /// </summary>
    public string Key;
    /// <summary>
    /// 名字
    /// </summary>
    public string Name;
    /// <summary>
    /// 标记
    /// </summary>
    public string Tag;
    /// <summary>
    /// 版本号
    /// </summary>
    public int Version;
    /// <summary>
    /// 大小（k）
    /// </summary>
    public long Size;
    /// <summary>
    /// 打包保存的路径
    /// </summary>
    public string ToPath;
    private List<string> mPathList = new List<string>();
    /// <summary>
    /// 路径集合
    /// </summary>
    public List<string> PathList
    {
        get { return mPathList; }
    }
}
