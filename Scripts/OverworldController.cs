using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class FlagContainer
{
    public string flag;
    public UnityEvent events;
}

public class OverworldController : MonoBehaviour
{
    public GameObject player;
    public GameObject follower;
    [SerializeField]
    public FlagContainer[] flags;
    TransitionScript transition;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        transition = GameObject.FindObjectOfType<TransitionScript>();

        if (transition.worldPosition != new Vector3(0, 0, 0))
        {
            player.transform.position = transition.worldPosition;
            follower.transform.position = player.transform.position - new Vector3(.3f, 0, .3f);
        }

        foreach(FlagContainer flagContainer in flags)
        {
            Debug.Log(flagContainer.flag + " " + transition.checkFlag(flagContainer.flag));
            Debug.Log(flagContainer.events);
            if (transition.checkFlag(flagContainer.flag))
            {

                flagContainer.events.Invoke();
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
