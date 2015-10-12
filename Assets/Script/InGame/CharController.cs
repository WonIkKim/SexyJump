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

public enum BlockType
{
    NORMAL_BLOCK,
    SPRING_BLOCK,
    SPU_LEFT_BLOCK,
    SPU_RIGHT_BLOCK,
    ONCE_BLOCK
}

public class CharController : MonoBehaviour {

    //private Transform CharTransform = null;
    protected Animator animator = null;

    //Character Status
    private CharacterJumpState jumpStatus;
    private bool isLeft = true;

    [SerializeField]
    private float jumpForce = 20000;
    private float speed = 1;
    private float jumpAddForce = 0f;

    private float beforePosY = 0;
    // Use this for initialization

    private MainController mainController;

    public void setAni(Animator ani)
    {
        animator = ani;
        // 애니 초기화
        animator.SetFloat("DirectX", -1);
    }

    public void setInitAni(Animator ani)
    {
        ani.SetFloat("DirectX", 0);
        ani.SetFloat("DirectY", 0);
    }

    public void setGravity(float g)
    {
        rigidbody2D.gravityScale = g;
    }
    void Start()
    {
        mainController = GameObject.Find("GameControll").GetComponent<MainController>();

        ChangeJumpStatus(CharacterJumpState.NORMAL);

    }

	// Update is called once per frame
    void Update()
    {

        // 올라가는 중이면
        if (beforePosY != 0 && (GetCurrentJumpStatus() == CharacterJumpState.JUMPING_UP || GetCurrentJumpStatus() == CharacterJumpState.NORMAL))
        {
            // 이전 위치(Y 좌표)보다 밑이면 떨어지는....
            float CharPosY = transform.position.y;
            if (beforePosY > CharPosY)
            {
                ChangeJumpStatus(CharacterJumpState.JUMPING_DOWN);
                mainController.SetBlockTrigger(false);
                mainController.SetUIGage(0);
            }
        }
        beforePosY = transform.position.y;
        
    }

    void FixedUpdate()
    {
        if (animator != null)
        {
            if (isLeft)
            {
                animator.SetFloat("DirectX", -1);
                transform.Translate(Vector3.left * speed * Time.fixedDeltaTime);
            }
            else
            {
                animator.SetFloat("DirectX", 1);
                transform.Translate(Vector3.right * speed * Time.fixedDeltaTime);
            }
        }

        if (GetCurrentJumpStatus() == CharacterJumpState.START_JUMP)
        {
            ChangeJumpStatus(CharacterJumpState.JUMPING_UP);
            rigidbody2D.AddForce(Vector3.up * jumpForce * jumpAddForce * Time.fixedDeltaTime );
            mainController.SetBlockTrigger(true);
        }
    }

    /**
     * 
     */
    void OnCollisionEnter2D(Collision2D coll)
    {
        // 벽에 부딛치면 처리
        if (coll.gameObject.CompareTag("WallLeft") )
        {
            isLeft = false;
        }
        else if (coll.gameObject.CompareTag("WallRight") )
        {
            isLeft = true;
        }

        
        
    }

    void OnCollisionStay2D(Collision2D coll)
    {
        // 하강 중에 발판 등을 밟으면 처리
        OnBlockAtDown(coll);
    }

    public void JumpingStart(float jumpAddPos )
    {
        if (jumpAddPos == 0)
            return;
        ChangeJumpStatus(CharacterJumpState.START_JUMP);
        this.jumpAddForce = (jumpAddPos * 5.0f) + 1;
    }

    void ChangeJumpStatus(CharacterJumpState cs)
    {
        jumpStatus = cs;
    }

    public CharacterJumpState GetCurrentJumpStatus()
    {
        return jumpStatus;
    }

    /**
     * 하강 중 처리
     */
    private void OnBlockAtDown(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Ground") || coll.gameObject.CompareTag("Block"))
        {
            // 점프 중이면 랜딩처리
            if (GetCurrentJumpStatus() == CharacterJumpState.JUMPING_DOWN)
            {
                ChangeJumpStatus(CharacterJumpState.NORMAL);
                mainController.SetUIGage(0);
                
            }
        }
    }

    private void OverCameraOut(Collision2D coll)
    {
        if(coll.gameObject.CompareTag("MainCamera") )
        {
            if (isLeft)
            {
            }
            else
            {
            }
        }
    }

    /***********************
     * 캐릭터 능력치 변경  *
     ***********************/

    /**
     * SetMass : 캐릭터 질량 변경
     *  - 질량 수치가 높을 수록 높이 뛰지 못한다.
     */
    public void SetMass(float m)
    {
        rigidbody2D.mass = 5;
    }

    /**
     * SetSpeed : 캐릭터 이동 속도 변경
     * 수치가 높을수록 이속이 빠르다.
     */
    public void SetSpeed(float s)
    {
        this.speed = s;
    }

    /**
     * SetJumpForce : 점프시 뛰는 힘 변경
     * 수치가 높을 수록 뛰는 힘이 커서 높이 뛴다.
     */
    public void SetJumpForce(float j)
    {
        this.jumpForce = j;
    }
}
