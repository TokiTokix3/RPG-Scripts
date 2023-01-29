using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionBoundary : MonoBehaviour
{
    private GameObject emmett;
    public Dialogue dialogue;

    void Start()
    {
        emmett = GameObject.FindWithTag("Player");
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            emmett.GetComponent<Rigidbody>().velocity = Vector3.zero;
            emmett.GetComponent<MovementController>().enabled = false;
            emmett.GetComponent<Animator>().SetFloat("MoveDir", 0);
            emmett.GetComponent<Animator>().SetBool("Moving", false);
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        }
    }
}
