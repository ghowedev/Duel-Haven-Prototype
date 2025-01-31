using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    private int currentHealth;
    public int CurrentHealth => currentHealth;

    public int maxHealth = 200;

    public event System.Action<int> OnHealthChanged;

    public void TakeDamage(int damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        OnHealthChanged?.Invoke(CurrentHealth);

        if (currentHealth == 0) Die();
    }

    public void Heal(int heal)
    {
        currentHealth = Mathf.Clamp(currentHealth + heal, 0, maxHealth);
        OnHealthChanged?.Invoke(CurrentHealth);
    }

    public void Die()
    {
        Debug.Log("Dead");
    }
}
