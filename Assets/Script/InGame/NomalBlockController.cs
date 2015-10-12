using UnityEngine;
using System.Collections;

public class NomalBlockController : MonoBehaviour {

    CharController charController;

    public void setChar(GameObject obj)
    {
        charController = obj.GetComponent<CharController>();
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
