using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    public int maxHealth = 100;
    public int currentHealth;

    //add in health bar object
    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        //adjust healthbar
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame

    //take 10 damage on space bar press
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10);
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        //adjust healthbar
        healthBar.SetHealth(currentHealth);

    }
}
