using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainController : MonoBehaviour {

    
    // Controll Script
    private CharController charController;
    private BlockController blockController;
    private UIController uiController;

    // Object 생성 시작 -------------------------
    void MakeCharacter()
    {
        GameObject CharacterRoot = (GameObject)GameObject.Instantiate(Resources.Load("Prefebs/Character/pfCharacter")) as GameObject;
        CharacterRoot.name = "CharacterRoot";
        CharacterRoot.transform.parent = transform;

        GameObject CharacterSprite = (GameObject)GameObject.Instantiate(Resources.Load("Prefebs/Character/pfChar_1")) as GameObject;
        CharacterSprite.name = "CharSprite";
        CharacterSprite.transform.parent = CharacterRoot.transform;

        charController = CharacterRoot.GetComponent<CharController>();

        GameObject.Find("GameCamera").GetComponent<GameCamera>().SetTarget(CharacterRoot.transform);
    }

    void MakeBlock()
    {
        GameObject BlockRoot = (GameObject)GameObject.Instantiate(Resources.Load("Prefebs/Blocks/pfBlock_Root")) as GameObject;
        BlockRoot.name = "BlockRoot";
        BlockRoot.transform.parent = transform;

        blockController = BlockRoot.GetComponent<BlockController>();

    }

    // Object 생성 끝 ----------------------------


	// Use this for initialization
    void Start()
    {
        MakeBlock();
        MakeCharacter();

        uiController = GameObject.Find("UIRoot").GetComponent<UIController>();

    }
	
	// Update is called once per frame
	void Update () {

        if (charController.GetCurrentJumpStatus() == CharacterJumpState.NORMAL)
        {
            if (Input.GetKeyDown(KeyCode.Space) )
            {
                uiController.StartGageAnimation();
            }

            if (Input.GetKeyUp(KeyCode.Space) )
            {
                uiController.StopGageAnimation();
                charController.JumpingStart( uiController.GetGage() );
            }

        }
	}

    // Character 관련 Method

    // Block 관련 Method
    public void SetBlockTrigger(bool t)
    {
        blockController.SetIsTrigger(t);
    }

    public void SetUIGage(float g)
    {
        uiController.SetGage(g);
    }
}
