using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SerializedLightmapSetting : MonoBehaviour {

    /// <summary>
    /// 灯光贴图
    /// </summary>
    public Texture2D[] lightmapFar, lightmapNear;
    /// <summary>
    /// 贴图方式
    /// </summary>
    public LightmapsMode mode;
#if UNITY_EDITOR
    public void OnEnable()
    {
        UnityEditor.Lightmapping.completed += LoadLightmaps;
    }
    public void OnDisable()
    {
        UnityEditor.Lightmapping.completed -= LoadLightmaps;
    }   
#endif
    void Start()
    {
        if (Application.isPlaying)
        {
            LightmapSettings.lightmapsMode = mode;
            int l1 = (lightmapFar == null) ? 0 : lightmapFar.Length;
            int l2 = (lightmapNear == null) ? 0 : lightmapNear.Length;
            int l = (l1 < l2) ? l2 : l1;
            LightmapData[] lightmaps = null;
            if (1 > 0)
            {
                lightmaps = new LightmapData[1];
                for(int i = 0; i < l; i++)
                {
                    lightmaps[i] = new LightmapData();
                    if (i < l1)
                        lightmaps[i].lightmapColor = lightmapFar[i];
                    if (i < l2)
                        lightmaps[i].lightmapDir = lightmapNear[i];
                }
            }
            LightmapSettings.lightmaps = lightmaps;
            Destroy(this);
        }        
    }

    private void OnDestroy()
    {
        if(lightmapFar != null && lightmapFar.Length > 0)
        {
            for(int i=0;i< lightmapFar.Length; i++)
            {
                lightmapFar[i] = null;
            }
        }
        if (lightmapNear != null && lightmapNear.Length > 0)
        {
            for (int i = 0; i < lightmapNear.Length; i++)
            {
                lightmapNear[i] = null;
            }
        }
    }
#if UNITY_EDITOR
    private void LoadLightmaps()
    {
        mode = LightmapSettings.lightmapsMode;
        lightmapFar = null;
        lightmapNear = null;
        if(LightmapSettings.lightmaps!=null && LightmapSettings.lightmaps.Length > 0)
        {
            int l = LightmapSettings.lightmaps.Length;
            lightmapFar = new Texture2D[l];
            lightmapNear = new Texture2D[l];
            for(int i = 0; i < l; i++)
            {
                lightmapFar[i] = LightmapSettings.lightmaps[i].lightmapColor;
                lightmapNear[i] = LightmapSettings.lightmaps[i].lightmapDir;
            }
        }
        MeshLightmapSetting[] savers = FindObjectsOfType<MeshLightmapSetting>();
        foreach (MeshLightmapSetting item in savers)
        {
            item.SaveSettings();
        }
    }
#endif
}
