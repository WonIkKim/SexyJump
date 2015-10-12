using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum GameState
{
    READY,
    COUNT,
    START,
    INGAME,
    END
}

public class MainController : MonoBehaviour {

    
    // Controll Script
    private CharController charController = null;
    private BlockController blockController = null;
    private UIController uiController = null;

    private GameState gameState = GameState.READY;

    // Object 생성 시작 -------------------------
    void MakeCharacter(string charactor)
    {
        GameObject CharacterRoot = GameObject.Find("CharacterRoot");
        charController = CharacterRoot.GetComponent<CharController>();

        //GameObject CharacterRoot = (GameObject)GameObject.Instantiate(Resources.Load("Prefebs/Character/pfCharacter")) as GameObject;
        //CharacterRoot.name = "CharacterRoot";
        //CharacterRoot.transform.parent = transform;

        GameObject CharacterSprite = (GameObject)GameObject.Instantiate(Resources.Load("Prefebs/Character/pfChar_" + charactor)) as GameObject;
        //GameObject CharacterSprite = GameObject.Find("CharSprite");
        CharacterSprite.name = "CharSprite";
        CharacterSprite.transform.position = CharacterRoot.transform.position; 
        CharacterSprite.transform.parent = CharacterRoot.transform;
        

        Animator animator = CharacterSprite.transform.GetComponent<Animator>();
        charController.setInitAni(animator);
        //charController.setAni(animator);
        //GameObject.Find("GameCamera").GetComponent<GameCamera>().SetTarget(CharacterRoot.transform);
    }

    void MakeBlock()
    {
        GameObject BlockRoot = (GameObject)GameObject.Instantiate(Resources.Load("Prefebs/Blocks/pfBlock_Root")) as GameObject;
        BlockRoot.name = "BlockRoot";
        BlockRoot.transform.parent = transform;
        BlockRoot.transform.position = transform.position;

        blockController = BlockRoot.GetComponent<BlockController>();

    }

    void MakeGround()
    {
        GameObject pfGround = (GameObject)GameObject.Instantiate(Resources.Load("Prefebs/Background/pfGround")) as GameObject;
        pfGround.transform.parent = transform;
        pfGround.transform.position = transform.position;
    }

    // Object 생성 끝 ----------------------------


	// Use this for initialization
    void Start()
    {
        InitGame();
        StartCoroutine(goNextStep());
    }

    IEnumerator goNextStep()
    {
        yield return new WaitForSeconds(2);
        ReadyGame();
    }

	// Update is called once per frame
	void Update () {
       
	}

    void InitGame()
    {
        gameState = GameState.READY;
        MakeGround();
        MakeCharacter("1");
        uiController = GameObject.Find("UIRoot").GetComponent<UIController>();
    }

    void ReadyGame()
    {
        StopCoroutine(goNextStep());
        MakeBlock();
        Animator animator = GameObject.Find("CharSprite").transform.GetComponent<Animator>();
        charController.setAni(animator);
        charController.setGravity(1);
        GameObject.Find("GameCamera").GetComponent<GameCamera>().SetTarget(GameObject.Find("CharacterRoot").transform);
    }

    void StartGame()
    {
    }
    
    // Character 관련 Method

    // Block 관련 Method
    public void SetBlockTrigger(bool t)
    {
        if(blockController != null)
            blockController.SetIsTrigger(t);
    }

    public void SetUIGage(float g)
    {
        if (uiController != null)
        {
            uiController.SetGage(g);
            uiController.StopGageAnimation();
        }
    }
}
