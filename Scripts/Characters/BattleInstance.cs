using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleInstance : MonoBehaviour
{
    public BattleCharacter character;
    public int currentHealth;
    public int currentEnergy;
    public int attackModifier;
    public int defenseModifier;
    public bool canAct = true;
    public HealthBar healthBar;
    SpriteRenderer sprite;
    Renderer renderer;

    // Start is called before the first frame update
    public void Start()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        sprite.sprite = character.sprite;

        currentHealth = character.health;
        currentEnergy = character.energy;

        healthBar.SetMaxHealth(currentHealth);
        healthBar.SetHealth(currentHealth);

        //sprite.color = new Color(0f, 0f, 0f, .4f);
        renderer = GetComponent<Renderer>();
        
    }

    public void takeDamage(int damage)
    {
        Debug.Log("current health:" + currentHealth + "-" + damage);
        currentHealth -= damage;
        if (currentHealth > character.health)
            currentHealth = character.health;
        if (damage > 0)
            StartCoroutine(setHitEffectTimer(.12f));
        healthBar.SetHealth(currentHealth);
    }

    public void loseEnergy(int energy)
    {
        Debug.Log("current energy:" + currentEnergy + "-" + energy);
        currentEnergy -= energy;
        if(currentEnergy > character.energy)
        {
            currentEnergy = character.energy;
        }
    }

    public IEnumerator setActTimer(float seconds)
    {
        canAct = false;
        yield return new WaitForSeconds(seconds);
        canAct = true;
    }

    public IEnumerator setHitEffectTimer(float seconds)
    {
        renderer.material.color = Color.red;
        yield return new WaitForSeconds(seconds);
        renderer.material.color = Color.white;
    }

    public bool isDead()
    {
        return currentHealth <= 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
