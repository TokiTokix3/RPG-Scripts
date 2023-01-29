using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTrigger : MonoBehaviour
{
    private GameObject emmett;
    public Dialogue dialogue;
    private TransitionScript transition;
    private MonoBehaviour thisGameObject;
    public bool hasUsed = false;

    public void updateTrigger(MonoBehaviour mono)
    {
        CollisionTrigger trigger = mono as CollisionTrigger;
        dialogue = trigger.dialogue;
        transition = trigger.transition;
        hasUsed = trigger.hasUsed;
    }

    void Start()
    {
        emmett = GameObject.FindWithTag("Player");
        transition = GameObject.FindObjectOfType<TransitionScript>();
        thisGameObject = this;
        thisGameObject = transition.checkDictionary(this.name, this);
        if(!thisGameObject.Equals(this))
        {
            updateTrigger(thisGameObject);
        }
        if (hasUsed)
        {
            GetComponent<Collider>().enabled = false;
        }
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            if(emmett == null)
                emmett = GameObject.FindWithTag("Player");
            emmett.GetComponent<Rigidbody>().velocity = Vector3.zero;
            emmett.GetComponent<MovementController>().enabled = false;
            emmett.GetComponent<Animator>().SetFloat("MoveDir", 0);
            emmett.GetComponent<Animator>().SetBool("Moving", false);
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.tag == "Player")
        {
            GetComponent<Collider>().enabled = false;
            hasUsed = true;
            transition.updateDictionary(this.name, this);
        }
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
