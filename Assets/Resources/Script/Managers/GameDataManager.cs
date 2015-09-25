using UnityEngine;
using System.Collections;

public class GameDataManager 
{
    private string jsonData;

    private static GameDataManager _instance;
    public static GameDataManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameDataManager();
                  
            }

            return _instance;
        }
    }

    public void SetBlocks(string strJson)
    {
        jsonData = strJson;
    }
}
