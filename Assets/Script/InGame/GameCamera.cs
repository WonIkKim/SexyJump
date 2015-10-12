using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour {

    private Transform target;
    private float trackSpeed = 100;

    public void  SetTarget(Transform t)
    {
        target = t;
    }

    public void removeTarget()
    {
        target = null;
    }

    void LateUpdate()
    {
        if (target)
        {
            float x = IncrementTowards(transform.position.x, target.position.x, trackSpeed);
            float y = IncrementTowards(transform.position.y, target.position.y, trackSpeed);
            transform.position = new Vector3(x, y, transform.position.z);
        }
    }

    /*
    void OnCollisionStay2D(Collision2D coll)
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
    */
    private float IncrementTowards(float n, float target, float a)
    {
        if (n == target)
        {
            return n;
        }
        else
        {
            float dir = Mathf.Sign(target - n); // must n be increased or decreased to get closer to target
            n += a * Time.deltaTime * dir;
            return (dir == Mathf.Sign(target - n)) ? n : target; // if n has now passed target then return target, otherwise return n
        }
    }
	
}
