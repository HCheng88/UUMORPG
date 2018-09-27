using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Renderer))]
public class MeshLightmapSetting : MonoBehaviour
{

    public int lightmapIndex;
    public Vector4 lightmapScaleOffset;

    public void SaveSettings()
    {
        Renderer renderer = GetComponent<Renderer>();
        lightmapIndex = renderer.lightmapIndex;
        lightmapScaleOffset = renderer.lightmapScaleOffset;
    }
    public void loadSetting()
    {
        Renderer renderer = GetComponent<Renderer>();
        lightmapIndex = renderer.lightmapIndex;
        lightmapScaleOffset = renderer.lightmapScaleOffset;
    }

	void Start () {
        loadSetting();
        if (Application.isPlaying)
            Destroy(this);
	}
	
}
