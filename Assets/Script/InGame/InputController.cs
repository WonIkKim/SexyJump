using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour {


    private CharController charController = null;
    private UIController uiController = null;
	// Use this for initialization
	void Start () {
        charController = GameObject.Find("CharacterRoot").GetComponent<CharController>();
        uiController = GetComponent<UIController>();
	}
	
	// Update is called once per frame
	void Update () {
        if (charController.GetCurrentJumpStatus() == CharacterJumpState.NORMAL)
        {
            if (Input.touchCount > 0)
            {
                for (int i = 0; i < Input.touches.Length; i++)
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
}
