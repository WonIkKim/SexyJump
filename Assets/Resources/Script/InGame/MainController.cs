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
        GameObject CharacterRoot = GameObject.Find("CharacterRoot");
        charController = CharacterRoot.GetComponent<CharController>();

        //GameObject CharacterRoot = (GameObject)GameObject.Instantiate(Resources.Load("Prefebs/Character/pfCharacter")) as GameObject;
        //CharacterRoot.name = "CharacterRoot";
        //CharacterRoot.transform.parent = transform;

        GameObject CharacterSprite = (GameObject)GameObject.Instantiate(Resources.Load("Prefebs/Character/pfChar_1")) as GameObject;
        CharacterSprite.name = "CharSprite";
        CharacterSprite.transform.parent = CharacterRoot.transform;
        Animator animator = CharacterSprite.transform.GetComponent<Animator>();
        
        
        charController.setAni(animator);

        GameObject.Find("GameCamera").GetComponent<GameCamera>().SetTarget(CharacterRoot.transform);
    }

    void MakeBlock()
    {
        GameObject BlockRoot = (GameObject)GameObject.Instantiate(Resources.Load("Prefebs/Blocks/pfBlock_Root")) as GameObject;
        BlockRoot.name = "BlockRoot";
        BlockRoot.transform.parent = transform;

        blockController = BlockRoot.GetComponent<BlockController>();

    }

    void MakeGround()
    {
        GameObject pfGround = (GameObject)GameObject.Instantiate(Resources.Load("Prefebs/Background/pfGround")) as GameObject;
        pfGround.transform.parent = transform;
    }

    // Object 생성 끝 ----------------------------


	// Use this for initialization
    void Start()
    {
        MakeGround();
        MakeBlock();
        MakeCharacter();

        uiController = GameObject.Find("UIRoot").GetComponent<UIController>();

    }
	
	// Update is called once per frame
	void Update () {

        if (charController.GetCurrentJumpStatus() == CharacterJumpState.NORMAL)
        {
            if (Input.touchCount > 0)
            {
                for (int i = 0 ; i < Input.touches.Length ; i++)
                //foreach (Touch touch in Input.touches)
                {
                    Touch touch = (Touch)Input.touches.GetValue(i);
                    HandleTouch(touch.fingerId, Camera.main.ScreenToWorldPoint(touch.position), touch.phase);
                }
            }
            else if (Input.touchCount == 0)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    HandleTouch(10, Camera.main.ScreenToWorldPoint(Input.mousePosition), TouchPhase.Began);
                }
                if (Input.GetMouseButton(0))
                {
                    HandleTouch(10, Camera.main.ScreenToWorldPoint(Input.mousePosition), TouchPhase.Moved);
                }
                if (Input.GetMouseButtonUp(0))
                {
                    HandleTouch(10, Camera.main.ScreenToWorldPoint(Input.mousePosition), TouchPhase.Ended);
                }
            }
        }
	}

    // Touch 
    private void HandleTouch(int touchFingerId, Vector3 touchPosition, TouchPhase touchPhase)
    {
        switch (touchPhase)
        {
            case TouchPhase.Began:
                uiController.StartGageAnimation();
                break;
            case TouchPhase.Moved:
                break;
            case TouchPhase.Ended:
                uiController.StopGageAnimation();
                charController.JumpingStart(uiController.GetGage());
                break;
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
        uiController.StopGageAnimation();
    }
}
