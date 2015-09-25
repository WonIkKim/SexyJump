using UnityEngine;
using System.Collections;
using UnityEditor;
using SimpleJSON;

public class MakeBlockJsonData {

    [MenuItem("Assets/Make Block Data")]
    static void ExportResource()
    {
        int StageNum = 1;
        string url = Application.persistentDataPath + "/Stage" + StageNum + ".json";

        Transform tf = GameObject.Find("BlockRoot").transform;

        string seedString = "{\"blocks\":[]}";

        JSONNode node = JSON.Parse(seedString);
        
        Transform[] arTF = tf.GetComponentsInChildren<Transform>();
        for (int i = 0; i < arTF.Length; i++)
        {
            Transform subtf = arTF[i];

            
            JSONClass subNode = new JSONClass();

            subNode.Add("type", "Normal");
            subNode.Add("posX", subtf.position.x + "");
            subNode.Add("posY", subtf.position.y + "");

            node["blocks"][-1] = subNode;
        }

        Debug.Log(node.ToString());
        System.IO.File.WriteAllText(url, node.ToString());
        //node.SaveToFile(url);
    }
}
