using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Linq;

public class AssetBundleDAL  {
    /// <summary>
    /// xml路径
    /// </summary>
    private string mPath;
    /// <summary>
    /// 返回的数据集合
    /// </summary>
    private List<AssetBundleEntity> mList = null;

    public AssetBundleDAL(string path)
    {
        mPath = path;
        mList = new List<AssetBundleEntity>();
    }
    /// <summary>
    /// 返回XML数据
    /// </summary>
    /// <returns></returns>
    public List<AssetBundleEntity> GetList()
    {
        mList.Clear();

        //读取XML
        XDocument XDoc = XDocument.Load(mPath);

        XElement root = XDoc.Root;

        XElement assetBundleNode = root.Element("AssetBundle");

        IEnumerable<XElement> lst = assetBundleNode.Elements("Item");

        int index = 0;

        foreach (XElement item in lst)
        {
            AssetBundleEntity entity = new AssetBundleEntity();
            entity.Key = "key"+ ++ index;
            entity.Name = item.Attribute("Name").Value;
            entity.Tag = item.Attribute("Tag").Value;
            entity.Version = item.Attribute("Version").Value.ToInt();
            entity.Size = item.Attribute("Size").Value.ToLong();
            entity.ToPath = item.Attribute("ToPath").Value;

            IEnumerable<XElement> pathList = item.Elements("Path");

            foreach (XElement path in pathList)
            {
                entity.PathList.Add(string.Format("Asset/{0}", path.Attribute("Value").Value));
            }
            mList.Add(entity);
        }        
        return mList;
    }

}
