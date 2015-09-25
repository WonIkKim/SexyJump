using UnityEngine;
using System.Collections;
using SimpleJSON;

public class BlockController : MonoBehaviour {

	// Use this for initialization

    [SerializeField]
    private int StageNum;

    private string url;
    private string FieldData;
	void Start () {

        url = "file:///" + Application.persistentDataPath + "/Stage" + StageNum + ".json";
        
        StartCoroutine(DownData());

	}

    private IEnumerator DownData()
    {
        WWW www = new WWW(url);
        yield return www;

        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.Log("error : " + www.error);
            yield break;
        }

        string jsonString = www.text;
        LoadBlockJson(jsonString);

    }

    void LoadBlockJson(string strJson)
    {
        
        JSONNode N = JSON.Parse(strJson);

        JSONArray ArBlock = N["blocks"].AsArray;

        for (int i = 0; i < ArBlock.Count; i++)
        {
            string type = ArBlock[i]["type"];
            float posX = ArBlock[i]["posX"].AsFloat;
            float posY = ArBlock[i]["posY"].AsFloat;

            GameObject gameBlock = (GameObject)GameObject.Instantiate(Resources.Load("Prefebs/Blocks/pfBlock_" + type)) as GameObject;

            gameBlock.transform.parent = transform;
            gameBlock.name = "Normal";
            Vector3 pos = new Vector3(posX,posY,0.5f);
            gameBlock.transform.position = pos;

        }

    }

    public void SetIsTrigger(bool isTrigger)
    {
        Transform[] ts = gameObject.GetComponentsInChildren<Transform>();
        for (int i = 0; i < ts.Length ; i++)
        {
            ts[i].GetComponentInChildren<BoxCollider2D>().isTrigger = isTrigger;
        }

    }
	
}
