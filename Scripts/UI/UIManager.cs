using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Bars and variables
    public HealthBar healthBar;
    private int currentHealth;

    public EnergyBar energyBar;
    private int currentEnergy;

    // Character Stats
    public BattleCharacter mainGuy;

    // Cooldown properties
    [Header("Attack")]
    public Image attackImageCD;
    bool isCooldown = false;
    public int cooldown = 3;
    public KeyCode attackButton;


    // Start is called before the first frame update
    void Start()
    {
        // Initiate Health
        currentHealth = mainGuy.health;
        healthBar.SetMaxHealth(mainGuy.health);

        // Initiate Energy
        currentEnergy = mainGuy.energy;
        energyBar.SetMaxEnergy(mainGuy.energy);

        attackImageCD.fillAmount = 0;
    }

    public void setEnergy(int currentEnergy)
    {
        energyBar.SetEnergy(currentEnergy);
    }

    public void setHealth(int currentHealth)
    {
        healthBar.SetHealth(currentHealth);
    }

    // Update is called once per frame
    void Update()
    {
        // Update Cooldowns with input values
        if (Input.GetKey(attackButton) && isCooldown == false)
        {
            isCooldown = true;
            attackImageCD.fillAmount = 1;
        }

        if (isCooldown)
        {
            attackImageCD.fillAmount -= Time.deltaTime;

            if (attackImageCD.fillAmount <= 0)
            {
                attackImageCD.fillAmount = 0;
                isCooldown = false;
            }
        }
    }

}
