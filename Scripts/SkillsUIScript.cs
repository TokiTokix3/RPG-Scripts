using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillsUIScript : MonoBehaviour
{
    public PlayerCharacter emmett;
    public Action skill;
    private bool isRunning = false;
    private GameObject leftSprite;
    private GameObject rightSprite;
    private GameObject upSprite;
    private GameObject downSprite;
    public Sprite skillSprite;
    
    void Start(){
        leftSprite = GameObject.Find("Skill_Left_Sprite");
        rightSprite = GameObject.Find("Skill_Right_Sprite");
        upSprite = GameObject.Find("Skill_Up_Sprite");
        downSprite = GameObject.Find("Skill_Down_Sprite");

    }

    // Update is called once per frame
    void Update()
    {
        isRunning = false;
        this.gameObject.GetComponent<Button>().onClick.AddListener(keyBindWrapper);
    }

    private void keyBindWrapper(){
        if(!isRunning){
            StartCoroutine("waitForKeyBind");
        }
    }

    private IEnumerator waitForKeyBind(){
        isRunning = true;
        bool assigned = false;
        while(!assigned){
            if(Input.GetKey(KeyCode.UpArrow)){
                emmett.actions[0] = skill;
                upSprite.GetComponent<Image>().sprite = skillSprite;
                assigned = true;
            }
            if(Input.GetKey(KeyCode.DownArrow)){
                emmett.actions[1] = skill;
                downSprite.GetComponent<Image>().sprite = skillSprite;
                assigned = true;
            }
            if(Input.GetKey(KeyCode.LeftArrow)){
                emmett.actions[2] = skill;
                leftSprite.GetComponent<Image>().sprite = skillSprite;
                assigned = true;
            }
            if(Input.GetKey(KeyCode.RightArrow)){
                emmett.actions[3] = skill;
                rightSprite.GetComponent<Image>().sprite = skillSprite;
                assigned = true;
            }
            
            yield return null;
        }
    }
}
