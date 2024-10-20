using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int playerIndex;
    [SerializeField] private int health;
    [SerializeField] private int attackPower;

    private int _currentHealth;

    private void Start()
    {
        _currentHealth = health;

        UpdateHpText();
        UpdatePowerText();
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;

        UpdateHpText();
    }

    public void Heal(int healAmount)
    {
        _currentHealth += healAmount;
        _currentHealth = Mathf.Min(_currentHealth, health);

        UpdateHpText();
    }

    public int GetCurrentAttackPower()
    {
        return attackPower;
    }

    private void UpdateHpText()
    {
        UIController.Instance.UpdatePlayerHp(playerIndex, _currentHealth);
    }

    private void UpdatePowerText()
    {
        UIController.Instance.UpdatePlayerPower(playerIndex, attackPower);
    }
}