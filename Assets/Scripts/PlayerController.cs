using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Animator head;
    public Animator chest;
    public Animator legs;
	
	public GameObject player;

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
    void FixedUpdate() {}
	
	public void assignAnimators(Animator pHead, Animator pBody, Animator pLegs) {
		head = pHead;
		chest = pBody;
		legs = pLegs;
	}
	
	public void changeState(int state)
    {
        if (_currentAnimationState == state)
            return;
		
		head.SetInteger("State", state);
		chest.SetInteger("State", state);
		legs.SetInteger("State", state);
    
        _currentAnimationState = state;
    }
}
