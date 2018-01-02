using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Animator head;
    public Animator chest;
    public Animator legs;

    public float walkSpeed = 1;

    const int IDLE = 0;
    const int WALK_RIGHT = 1;
    const int WALK_LEFT = 2;
    private int _currentAnimationState = IDLE;
    // Use this for initialization
    void Start()
    {
        head.GetComponent<Animator>();
        chest.GetComponent<Animator>();
        legs.GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            changeState(WALK_RIGHT);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            changeState(WALK_LEFT);
        }
        else
        {
            changeState(IDLE);
        }

        // Debug.Log("current state: " + _currentAnimationState);
    }

    void changeState(int state)
    {
        if (_currentAnimationState == state)
            return;

        if (_currentAnimationState == WALK_RIGHT)
        {
            head.SetInteger("State", WALK_RIGHT);
            chest.SetInteger("State", WALK_RIGHT);
            legs.SetInteger("State", WALK_RIGHT);
        }

        if (_currentAnimationState == WALK_LEFT)
        {
            head.SetInteger("State", WALK_LEFT);
            chest.SetInteger("State", WALK_LEFT);
            legs.SetInteger("State", WALK_LEFT);
        }

        if (_currentAnimationState == IDLE)
        {
            head.SetInteger("State", IDLE);
            chest.SetInteger("State", IDLE);
            chest.SetInteger("State", IDLE);
        }

        _currentAnimationState = state;
    }
}
