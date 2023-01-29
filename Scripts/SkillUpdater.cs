using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUpdater : MonoBehaviour
{
    public PlayerCharacter player;
    public Image skill1;
    public Image skill2;
    public Image skill3;
    public Image skill4;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(player.actions[0] != null)
            skill1.sprite = player.actions[0].emblem;
        if (player.actions[1] != null)
            skill2.sprite = player.actions[1].emblem;
        if (player.actions[2] != null)
            skill3.sprite = player.actions[2].emblem;
        if (player.actions[3] != null)
            skill4.sprite = player.actions[0].emblem;
    }
}
