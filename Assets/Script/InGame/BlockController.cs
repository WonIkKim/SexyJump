﻿using UnityEngine;
using System.Collections;
using SimpleJSON;

public class BlockController : MonoBehaviour {

	// Use this for initialization

    [SerializeField]
    private int StageNum = 1;

	void Start () {
        TextAsset bindata = Resources.Load("Data/Stage"+StageNum) as TextAsset;
        string jsonString = bindata.ToString();
        LoadBlockJson(jsonString);
	}

    void LoadBlockJson(string strJson)
    {
        
        JSONNode N = JSON.Parse(strJson);

        JSONArray ArBlock = N["blocks"].AsArray;

        GameObject normalBlock = (GameObject)GameObject.Instantiate(Resources.Load("Prefebs/Blocks/pfBlock_Normal")) as GameObject;

        for (int i = 0; i < ArBlock.Count; i++)
        {
            string type = ArBlock[i]["type"];
            float posX = ArBlock[i]["posX"].AsFloat;
            float posY = ArBlock[i]["posY"].AsFloat;

            GameObject gameBlock = (GameObject)GameObject.Instantiate(normalBlock) as GameObject;

            gameBlock.transform.parent = transform;
            gameBlock.name = type;
            Vector3 pos = new Vector3(transform.position.x + posX,transform.position.y + posY,0.5f);
            gameBlock.transform.position = pos;

        }

        Destroy(normalBlock);

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
