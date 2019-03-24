using UnityEngine;

[System.Serializable]
public class Health
{
    public delegate void Die();
    Die OnDieCallback;

    float _maxHealth = 100;
    float _currentHealth;

    public bool IsDead => _currentHealth <= 0;

    public Health(float maxHealth, Die onDieCallback)
    {
        _maxHealth = maxHealth;
        ResetHealth();
        OnDieCallback = onDieCallback;
    }

    public void ApplyDamage(float damage)
    {
        if (IsDead) return;

        _currentHealth -= damage;

        if (IsDead && OnDieCallback != null)
            OnDieCallback();
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