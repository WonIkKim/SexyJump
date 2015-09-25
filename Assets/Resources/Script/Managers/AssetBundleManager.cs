using UnityEngine;
using System.Collections;

public class AssetBundleManager : MonoBehaviour {

    private static AssetBundleManager _instance;
    public static AssetBundleManager Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = GameObject.FindObjectOfType(typeof(AssetBundleManager)) as AssetBundleManager;
                if (!_instance)
                {
                    GameObject container = new GameObject();
                    container.name = "AssetBundleContainer";
                    _instance = container.AddComponent(typeof(AssetBundleManager)) as AssetBundleManager;
                }
            }

            return _instance;
        }
    }
    
    public AssetBundle GetAssetBundle(string strAssetBundle)
    {
        
        return null;
    }

    public GameObject GetGameObject(string strObj)
    {

        return null;
    }
}
