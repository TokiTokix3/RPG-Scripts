using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuStats : MonoBehaviour
{
    public Text healthStat;
    public Text energyStat;
    public Text levelText;

    public BattleCharacter mainGuy;

    // Start is called before the first frame update
    void Start()
    { 
        healthStat.text = "Health: " + mainGuy.health.ToString() + " / 10";
        energyStat.text = "Energy: " + mainGuy.energy.ToString() + " / 100";
        levelText.text = "Level " + mainGuy.level.ToString();
    }
}
