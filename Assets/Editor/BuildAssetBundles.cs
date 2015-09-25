using UnityEngine;
using System.Collections;
using UnityEditor;

public class BuildAssetBundles {

    [MenuItem("Assets/Build AssetBundle_1")]
    static void ExportResource()
    {
        BuildPipeline.PushAssetDependencies();
        string path = "Assets/AssetBundle/GameAsset.unity3d";
        BuildAssetBundleOptions options = BuildAssetBundleOptions.CollectDependencies |
            BuildAssetBundleOptions.CompleteAssets;
        Object[] selection = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
        BuildPipeline.BuildAssetBundle(Selection.activeObject, selection, path, options);
        BuildPipeline.PopAssetDependencies();
    }
    /*
    [MenuItem("Assets/AutoBuildImageFile")]

    public static void buildImage()
    {
        BuildPipeline.PushAssetDependencies();

        BuildAssetBundleOptions options = BuildAssetBundleOptions.CollectDependencies |
            BuildAssetBundleOptions.CompleteAssets;

        Object[] asset = AssetDatabase.LoadAllAssetsAtPath("Assets/Resources/");

        BuildPipeline.BuildAssetBundle(null, asset, "Game.unity3d", options);
        BuildPipeline.PopAssetDependencies();
    }
     */

}
