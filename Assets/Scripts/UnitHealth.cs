using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UnitHealth : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    public float maxHealth = 100f;
    public float currentHealth;
    public UnityEvent OnDeath;
    
    private GameObject healthBarInstance;

    void Awake()
    {
        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        
        healthBar.value = currentHealth;
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        OnDeath.Invoke();
        Destroy(gameObject);
    }
}