using UnityEngine;
using System.Collections;

public enum CharacterJumpState
{
    NORMAL,
    START_JUMP,
    JUMPING_UP,
    JUMPING_DOWN,
    LANDING
} 

public class CharController : MonoBehaviour {

    private Transform CharTransform;
    protected Animator animator;

    //Character Status
    private CharacterJumpState jumpStatus;
    private bool isLeft = true;
    [SerializeField]
    private float jumpForce = 3000.0f;
    private float jumpAddForce = 0f;

    private float beforePosY = 0;
    // Use this for initialization

    private MainController mainController;

    void Start()
    {
        mainController = GameObject.Find("GameControll").GetComponent<MainController>();

        CharTransform = transform.FindChild("CharSprite");
        animator = CharTransform.GetComponent<Animator>();
        animator.SetFloat("DirectX", -1);

        jumpStatus = CharacterJumpState.NORMAL;

    }

	// Update is called once per frame
    void Update()
    {

        if (isLeft)
        {
            animator.SetFloat("DirectX", -1);
            transform.Translate(new Vector3(-1, 0, 0) * Time.deltaTime);
        }
        else
        {
            animator.SetFloat("DirectX", 1);
            transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime);
        }

        // 올라가는 중이면
        if (beforePosY != 0 && GetCurrentJumpStatus() == CharacterJumpState.JUMPING_UP)
        {
            // 이전 위치(Y 좌표)보다 밑이면 떨어지는....
            float CharPosY = CharTransform.position.y;
            if (beforePosY > CharPosY)
            {
                ChangeJumpStatus(CharacterJumpState.JUMPING_DOWN);
                mainController.SetBlockTrigger(false);
            }
        }
        beforePosY = CharTransform.position.y;

    }

    void FixedUpdate()
    {
        

        if (GetCurrentJumpStatus() == CharacterJumpState.START_JUMP)
        {
            ChangeJumpStatus(CharacterJumpState.JUMPING_UP);
            mainController.SetBlockTrigger(true);
            rigidbody2D.AddForce(new Vector3(0f, jumpForce * jumpAddForce, 0f));
        }
    }

    void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Ground" || coll.gameObject.tag == "Block")
        {
            // 점프 중이면 랜딩처리
            if (GetCurrentJumpStatus() == CharacterJumpState.JUMPING_DOWN)
            {
                ChangeJumpStatus(CharacterJumpState.NORMAL);
                mainController.SetUIGage(0);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "WallLeft")
        {
            isLeft = false;
        }
        else if (coll.gameObject.tag == "WallRight")
        {
            isLeft = true;
        }
    }

    public void JumpingStart(float jumpAddPos )
    {
        ChangeJumpStatus(CharacterJumpState.START_JUMP);
        this.jumpAddForce = (jumpAddPos * 1.0f) * 0.8f;
    }

    void ChangeJumpStatus(CharacterJumpState cs)
    {
        jumpStatus = cs;
    }

    public CharacterJumpState GetCurrentJumpStatus()
    {
        return jumpStatus;
    }
}
