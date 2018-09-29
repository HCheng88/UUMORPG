using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LocalFileMgr : Singleton<LocalFileMgr>
{
#if UNITY_EDITOR
#if UNITY_STANDALONE_WIN
    public readonly string LocalFilePath = Application.dataPath + "/../AssetBundle/Windows/";
#elif UNITY_ANDROID
        public readonly string LocalFilePath = Application.dataPath + "/../AssetBundle/Android/";
#elif UNITY_IPHONE
        public readonly string LocalFilePath = Application.dataPath + "/../AssetBundle/IOS/";
#endif
#elif UNITY_STANDALONE_WIN || UNITY_ANDROID || UNITY_IPHONE
        public readonly string LocalFilePath = Application.persistentDataPath + "/";
#endif

    /// <summary>
    /// 将文件转换成字节数组
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public byte[] GetBuffer(string path)
    {
        byte[] buffer = null;
        using (FileStream fs = new FileStream(path, FileMode.Open))
        {
            buffer = new byte[fs.Length];
            fs.Read(buffer, 0, buffer.Length);
        }
        return buffer;
    }
}
