using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    TransitionScript transition;
    // Start is called before the first frame update
    void Start()
    {
        transition = GameObject.FindObjectOfType<TransitionScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player"){
            transition.isFinalBattle = true;
            SceneManager.LoadScene("Anthill scene");
        }
    }
}
