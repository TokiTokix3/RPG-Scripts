using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public PlayerCharacter selectedPlayer;
    public BattleManager battleManager;
    public Animator animator;
    public Mode mode;
    public enum Mode
    {
        battle, explore, dialogue
    }

    Rigidbody characterBody;

    private float moveSpeed = 400f;
    private float horizontalMove = 0f;
    //private float jumpHeight = 2.0;

    private Vector3 startValue;
    private Vector3 endValue;
    private Vector3 modifier;
    private int inputs;
    private AudioSource audioSource;
    private float timeBetweenSteps = 0.25f;
    private float timer;
    

    // Start is called before the first frame update
    void Start()
    {
        characterBody = GetComponent<Rigidbody>();
        if(battleManager != null)
            battleManager.forcedStart();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mode == Mode.battle)
        {
            BattleLogic();
        }
    }

    void FixedUpdate(){
        // horizontalMove = Input.GetAxisRaw("Horizontal") * moveSpeed;
        // if(animator != null)
        //     animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

         if(mode == Mode.explore)
        {
            modifier = Vector3.zero;
            animator.SetBool("Moving", false);
            inputs = 0;
            timer += Time.deltaTime;
            if (Input.GetKey("w")){
                modifier = modifier + new Vector3(0, 0, moveSpeed*Time.deltaTime);
                animator.SetBool("Moving", true);
                inputs++;
                if(timer > timeBetweenSteps) {
                    audioSource.Play();
                    timer = 0;
                }
            }
            if (Input.GetKey("a")){
                modifier = modifier + new Vector3(-1*moveSpeed*Time.deltaTime, 0, 0);
                inputs++;
                if(timer > timeBetweenSteps) {
                    audioSource.Play();
                    timer = 0;
                }
            }
            if (Input.GetKey("s")){
                modifier = modifier + new Vector3(0, 0, -1*moveSpeed*Time.deltaTime);
                animator.SetBool("Moving", true);
                inputs++;
                if(timer > timeBetweenSteps) {
                    audioSource.Play();
                    timer = 0;
                }
            }
            if (Input.GetKey("d")){
                modifier = modifier + new Vector3(moveSpeed*Time.deltaTime, 0, 0);
                inputs++;
                if(timer > timeBetweenSteps) {
                    audioSource.Play();
                    timer = 0;
                }
            }
            // if (Input.GetKey("space")) // jump
            // {
            //     //Debug.Log("jump time");
            //     jump();   
            // }
            if(inputs > 1){
                modifier.x *= 0.75f;
                modifier.z *= 0.75f;
            }
            animator.SetFloat("MoveDir", modifier.x);
            characterBody.velocity = modifier;
        }
    }

    void BattleLogic()
    {
        if (Input.GetKeyDown("a"))
        {
            battleManager.moveSelectedPlayer(4);
        }
        if (Input.GetKeyDown("d"))
        {
            battleManager.moveSelectedPlayer(6);
        }
        if (Input.GetKeyDown("w"))
        {
            battleManager.moveSelectedPlayer(8);
        }
        if (Input.GetKeyDown("s"))
        {
            battleManager.moveSelectedPlayer(2);
        }
        if (!Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKeyDown("up"))
            {
                battleManager.useAttack(0);
            }
            if (Input.GetKeyDown("down"))
            {
                battleManager.useAttack(1);
            }
            if (Input.GetKeyDown("left"))
            {
                battleManager.useAttack(2);
            }
            if (Input.GetKeyDown("right"))
            {
                battleManager.useAttack(3);
            }
        }
        else
        {
            if (Input.GetKeyDown("up"))
            {
                battleManager.useItem(0);
            }
            if (Input.GetKeyDown("down"))
            {
                battleManager.useItem(1);
            }
            if (Input.GetKeyDown("left"))
            {
                battleManager.useItem(2);
            }
            if (Input.GetKeyDown("right"))
            {
                battleManager.useItem(3);
            }
        }
        
    }

    // void jump()
    // {
    //     //modifier = modifier + new Vector3(0, 1, 0) //only jumps up but not down
    //     gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * Mathf.Sqrt(2f * -2f * Physics.gravity.y), ForceMode.VelocityChange);
    // }
}
