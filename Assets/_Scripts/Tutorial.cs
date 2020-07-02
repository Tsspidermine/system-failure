using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Tutorial : MonoBehaviour
{
    public Animator animator;

    public GameObject keyboardMove;
    public GameObject gamepadMove;

    private bool gamepadConnected = false;
    // Start is called before the first frame update
    void Start()
    {
        animator.SetBool("IsOpen", false);
    }

    private int tutorialCompletion = 0;
    float x, y;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetJoystickNames().Length > 0)
        {
            gamepadConnected = true;
        }

        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

        if (x > 0 || x < 0 || y > 0 || y < 0 && tutorialCompletion <= 100)
        {
            tutorialCompletion++;
        }

        if (tutorialCompletion >= 100)
        {
            keyboardMove.SetActive(false);
            gamepadMove.SetActive(false);
            animator.SetBool("IsOpen", true);
        } else
        {
            if(gamepadConnected == false)
            {
                keyboardMove.SetActive(true);
                gamepadMove.SetActive(false);
            } else
            {
                keyboardMove.SetActive(false);
                gamepadMove.SetActive(true);
            }
        }
    }
}
