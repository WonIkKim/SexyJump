using UnityEngine;
using System.Collections;
using UnityEditor;
using SimpleJSON;
using System.IO;

public class MakeBlockJsonData {

    [MenuItem("Assets/Make Block Data")]
    static void ExportResource()
    {
        int StageNum = 1;
        string fileName = "Stage" + StageNum + ".json";

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
        writeStringToFile(node.ToString(), fileName);
    }

    private static void writeStringToFile(string str, string filename)
    {
 #if !WEB_BUILD
     string path = pathForDocumentsFile( filename );
     Debug.Log(path);
     FileStream file = new FileStream (path, FileMode.Create, FileAccess.Write);
 
     StreamWriter sw = new StreamWriter( file );
     sw.WriteLine( str );
 
     sw.Close();
     file.Close();
 #endif 
    }


    private static string readStringFromFile(string filename)//, int lineIndex )
    {
#if !WEB_BUILD
        string path = pathForDocumentsFile(filename);

        if (File.Exists(path))
        {
            FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(file);

            string str = null;
            str = sr.ReadLine();

            sr.Close();
            file.Close();

            return str;
        }
        else
        {
            return null;
        }
#else
     return null;
#endif
    }


    private static string pathForDocumentsFile(string filename)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            string path = Application.dataPath.Substring(0, Application.dataPath.Length - 5);
            path = path.Substring(0, path.LastIndexOf('/'));
            return Path.Combine(Path.Combine(path, "Documents"), filename);
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            string path = Application.persistentDataPath;
            path = path.Substring(0, path.LastIndexOf('/'));
            return Path.Combine(path, filename);
        }
        else
        {
            string path = Application.persistentDataPath;
            path = path.Substring(0, path.LastIndexOf('/'));
            return Path.Combine(path, filename);
        }
    }

}
