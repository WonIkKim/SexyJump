using UnityEngine;
using System.Collections;

public class InitGround : MonoBehaviour {

	// Use this for initialization
    SpriteRenderer spriteRenderer;
	void Start () {

        spriteRenderer = GetComponent<SpriteRenderer>();

        GameObject goWall = (GameObject)GameObject.Instantiate(Resources.Load("Prefebs/Background/pfWall")) as GameObject;
        SpriteRenderer wallSR = goWall.GetComponent<SpriteRenderer>();

        float GroundWidth = spriteRenderer.sprite.rect.width;
        float GroundHeight = spriteRenderer.sprite.rect.height;
        float GroundX = GroundWidth * 0.5f * 0.01f * -1.0f;
        float GroundY = GroundHeight * 0.5f * 0.01f ;
        float WallHeight = wallSR.sprite.rect.height;
        

        for (int i = 0; i < 50; i++)
        {
            GameObject goWallCopy = (GameObject)GameObject.Instantiate(goWall, Vector3.zero, Quaternion.identity) as GameObject;
            goWallCopy.name = "WallLeft";
            goWallCopy.tag = "WallLeft";
            goWallCopy.transform.parent = transform;

            goWallCopy.transform.localPosition = new Vector3(GroundX , GroundY + (WallHeight * i * 0.01f));
            
        }
        for (int i = 0; i < 50; i++)
        {
            GameObject goWallCopy = (GameObject)GameObject.Instantiate(goWall, Vector3.zero, Quaternion.identity) as GameObject;
            goWallCopy.name = "WallRight";
            goWallCopy.tag = "WallRight";
            goWallCopy.transform.parent = transform;

            goWallCopy.transform.localPosition = new Vector3(GroundX * -1.0f, GroundY + (WallHeight * i * 0.01f));
            
        }
        Destroy(goWall);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
