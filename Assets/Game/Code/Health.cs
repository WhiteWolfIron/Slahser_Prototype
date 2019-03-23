using UnityEngine;

[System.Serializable]
public class Health
{
    [SerializeField]
    float _maxHealth = 100;

    float _currentHealth;

    public bool IsDead => _currentHealth <= 0;

    public void ApplyDamage(float damage)
    {
        _currentHealth -= damage;
    }

    public void ResetHealth()
    {
        _currentHealth = _maxHealth;
    }

    public void ApplyHeal(float amount)
    {
        _currentHealth += amount;
        _currentHealth = Mathf.Min(_currentHealth, _maxHealth);
    }
}