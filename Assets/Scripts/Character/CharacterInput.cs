using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInput : MonoBehaviour
{
    private Animator animator;
    private Vector3 colliderSize = new Vector3(0.5f,1.5f,0.3f);
    private Vector3 colliderPos = new Vector3(0,1,0);
    public BoxCollider runningCollider;
    public float handling = 3;
    public float speed = 5f;
    public bool moving = true;
    private bool canBoost = true;
    private bool isBoosting = false;
    private float initSpeed;
    private float initHandling;

    void Start()
    {
        this.animator = GetComponent<Animator>();
        this.initHandling = this.handling;
        this.initSpeed = this.speed;
    }

    private IEnumerator boost(){
        this.speed *= 2;
        this.canBoost = false;
        this.isBoosting = true;
        yield return new WaitForSeconds(1.5f);
        this.speed = this.initSpeed;
        this.isBoosting = false;
        yield return new WaitForSeconds(2);
        this.canBoost = true;
    }

    private bool keyDown(string k){
        return Input.GetKeyDown(k);
    }
    
    private bool key(string k){
        return Input.GetKey(k);
    }

    private float getHorizontalMovement(){
        return Time.deltaTime * handling;
    }

    private float getMovement(){
        return Time.deltaTime * speed;
    }

    private void setIdleState(){
        animator.SetBool("isBeingHit",false);
        runningCollider.size = this.colliderSize;
        runningCollider.center = this.colliderPos;
        if( !this.isBoosting ){
            this.speed = this.initSpeed;
        }
    }

    private void setSlidingState(){
        runningCollider.size = new Vector3(0.5f,0.5f,0.3f);
        runningCollider.center = this.colliderPos - new Vector3(0,0.5f,0);
    }

    private void setJumpingState(){
        runningCollider.size = new Vector3(0.5f,0.5f,0.3f);
        runningCollider.center = this.colliderPos + new Vector3(0,0.7f,0);
    }

    private void setHitState(){
        animator.SetTrigger("HitTrigger");
        animator.SetBool("isBeingHit",true);
        this.speed *= 0.7f;
    }

    void Update()
    {
        if( moving ){
            transform.Translate(0,0,getMovement());
        }
        if (keyDown("space"))
        {
            if( animator.GetBool("isIdle") ){
                this.setJumpingState();
                animator.SetTrigger("JumpTrigger");
            }
        }
        else if(keyDown("s"))
        {
            if( animator.GetBool("isIdle") ){
                this.setSlidingState();
                animator.SetTrigger("SlideTrigger");
            }
        }
        else if(key("w") && canBoost)
        {
            StartCoroutine(boost());
        }
        else if(key("a"))
        {
            if( transform.position.x > -3.1 ){
                transform.Translate(-getHorizontalMovement(), 0,0);
            }
        }
        else if(key("d"))
        {
            if( transform.position.x < 3.1 ){
                transform.Translate(getHorizontalMovement(), 0,0);
            }
        } 
        else if( animator.GetBool("isIdle") )
        {
            this.setIdleState();
        }
    }

    void OnTriggerEnter(Collider c){
        if( c.tag == Constants.ObstacleTag && !animator.GetBool("isBeingHit") ){
            UIManager.Instance.DecreaseLives();
            this.setHitState();
        }
    }
}
