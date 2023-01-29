using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopkeeperScript : MonoBehaviour
{
    public GameObject shopUi;
    private GameObject emmett;
    private PlayerCharacter player;
    private MovementController moveScript;
    public Dialogue dialogue;
    public Text moneyText;
    public ItemSlot buyButton;
    public Button button;

    void Start()
    {
        emmett = GameObject.FindWithTag("Player");
        player = emmett.GetComponent<MovementController>().selectedPlayer;
        if (shopUi == null)
            shopUi = GameObject.Find("Shop-UI");
        
    }

    public void buyItem()
    {
        if(player.money > buyButton.item.purchaseCost)
        {
            player.money -= buyButton.item.purchaseCost;
            buyButton.item.uses++;
            updateMoney();
        }
    }

    public void updateMoney()
    {
        moneyText.text = player.money.ToString();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player"){
            shopUi.SetActive(true);
            emmett.GetComponent<Rigidbody>().velocity = Vector3.zero;
            emmett.GetComponent<MovementController>().enabled = false;
            emmett.GetComponent<Animator>().SetFloat("MoveDir", 0);
            emmett.GetComponent<Animator>().SetBool("Moving", false);
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
            Debug.Log(player.money);
            updateMoney();
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(buyItem);
        }
        //SceneManager.LoadScene("Overworld-UI");
    }

}
