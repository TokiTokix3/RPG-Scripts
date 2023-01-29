using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjectScript : MonoBehaviour
{
    private GameObject emmett;
    private PlayerCharacter player;
    public Dialogue dialogue;
    public SpriteRenderer sprite;
    public bool isAutomaticActivation;
    public IEnumerator corutine;
    private bool corutineRunning;

    // Start is called before the first frame update
    void Start()
    {
        corutineRunning = false;
        emmett = GameObject.FindWithTag("Player");
        player = emmett.GetComponent<MovementController>().selectedPlayer;
        corutine = startPrompt();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {

            if(isAutomaticActivation)
                startDialogue();
            else 
            {
                StartCoroutine(corutine);
            }
            
        }
        //SceneManager.LoadScene("Overworld-UI");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            stopPrompt();
        }
    }

    private IEnumerator startPrompt()
    {
        corutineRunning = true;
        sprite.enabled = true;
        Debug.Log("startPrompt");
        while (true)
        {
            if (Input.GetKeyDown("f"))
            {
                startDialogue();
            }
            yield return null;
        }
    }

    private void stopPrompt()
    {
        Debug.Log("stopPrompt");
        corutineRunning = false;
        sprite.enabled = false;
        StopCoroutine(corutine);
    }

    public void startDialogue()
    {
        emmett.GetComponent<Rigidbody>().velocity = Vector3.zero;
        emmett.GetComponent<MovementController>().enabled = false;
        emmett.GetComponent<Animator>().SetFloat("MoveDir", 0);
        emmett.GetComponent<Animator>().SetBool("Moving", false);
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        stopPrompt();
    }

    public void givePlayerMovement()
    {
        emmett.GetComponent<MovementController>().enabled = true;
    }
}
