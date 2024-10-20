using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private int attackPower;

    private int _currentHealth;

    private void Start()
    {
        _currentHealth = health;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        Debug.Log($"{gameObject.name} was attacked with {damage}. current health: {_currentHealth}");
    }

    public void Heal(int healAmount)
    {
        _currentHealth += healAmount;
        _currentHealth = Mathf.Min(_currentHealth, health);
        Debug.Log($"{gameObject.name} was healed {healAmount}. current health: {_currentHealth}");
    }

    public int GetCurrentAttackPower()
    {
        return attackPower;
    }
}