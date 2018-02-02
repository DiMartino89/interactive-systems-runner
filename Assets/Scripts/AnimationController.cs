using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    /** Variables for the Animator Components*/
    public Animator head;
    public Animator chest;
    public Animator legs;
	
	public GameObject player;

    /** State varialbes for the Aninmations, Would be cleaner to have an Enum (Couldn't get it to Work with it)*/
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
    void FixedUpdate() {
		
		// Player-Controls change animation-states
		if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            ChangeState(WALK_RIGHT);
        }
        else if (Input.GetKey(KeyCode.LeftArrow)|| Input.GetKey(KeyCode.A))
        {
            ChangeState(WALK_LEFT);
        }
        else
        {
            ChangeState(IDLE);
        }
	}
	
	// Assign the single-animators
	public void AssignAnimators(Animator pHead, Animator pBody, Animator pLegs) {
		head = pHead;
		chest = pBody;
		legs = pLegs;
	}
	
	// Method to change animation-states
	public void ChangeState(int state)
    {
        if (_currentAnimationState == state)
            return;
		
		head.SetInteger("State", state);
		chest.SetInteger("State", state);
		legs.SetInteger("State", state);
    
        _currentAnimationState = state;
    }
}
