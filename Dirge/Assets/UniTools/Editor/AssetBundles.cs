using UnityEngine;
using System.Collections;
using UnityEditor;

public class AssetBundles : AssetPostprocessor  {

    [MenuItem("Unitools/Build AssetBundles")]
    static void BuildAllAssetBundles()
    {
        BuildPipeline.BuildAssetBundles("Assets/AssetBundles", BuildAssetBundleOptions.None, BuildTarget.StandaloneOSX);
    }

    [MenuItem("Unitools/Log AssetBundle names")]
    static void GetNames()
    {
        var names = AssetDatabase.GetAllAssetBundleNames();
        foreach (var name in names)
            Debug.Log("AssetBundle: " + name);
    }

    void OnPostprocessAssetbundleNameChanged(string path,
           string previous, string next)
    {
        Debug.Log("AB: " + path + " old: " + previous + " new: " + next);
    }
}
