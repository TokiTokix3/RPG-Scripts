using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OverworldEnemy : MonoBehaviour
{
    public BoxCollider box;
    public Encounter[] encounter;
    public TransitionScript transitionObject;
    // Start is called before the first frame update
    void Start()
    {
        transitionObject = GameObject.FindObjectOfType<TransitionScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void enterBattle()
    {
        int random = Random.Range(0, encounter.Length);
        transitionObject.encounter = encounter[random];
        GameObject player = GameObject.FindWithTag("Player");
        transitionObject.worldPosition = player.transform.position + (player.transform.position - gameObject.transform.position).normalized * .4f;
        transitionObject.worldPosition.y = player.transform.position.y;
        SceneManager.LoadScene("BattleScene");
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            int random = Random.Range(0, encounter.Length);
            transitionObject.encounter = encounter[random];
            transitionObject.worldPosition = collision.gameObject.transform.position + (collision.gameObject.transform.position - gameObject.transform.position).normalized * .4f;
            transitionObject.worldPosition.y = collision.gameObject.transform.position.y;
            SceneManager.LoadScene("BattleScene");
        }
    }
}
