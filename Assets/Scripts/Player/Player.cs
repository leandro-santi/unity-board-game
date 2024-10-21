using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int playerIndex;
    [SerializeField] private int health;
    [SerializeField] private int attackPower;

    private int _currentHealth;
    private int _maxAttackPower;

    private void Start()
    {
        _currentHealth = health;
        _maxAttackPower = attackPower;

        UpdateHpText();
        UpdatePowerText();
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        _currentHealth = Mathf.Max(0, _currentHealth);

        UpdateHpText();

        if (_currentHealth <= 0)
        {
            GameController.Instance.onPlayerWin?.Invoke((playerIndex) % 2);
        }
    }

    public void Heal(int healAmount)
    {
        _currentHealth += healAmount;
        _currentHealth = Mathf.Min(_currentHealth, health);

        UpdateHpText();
    }

    public void GainAttackPower(int attack)
    {
        attackPower += attack;

        UpdatePowerText();
    }

    public void ResetAttackPower()
    {
        attackPower = _maxAttackPower;

        UpdatePowerText();
    }

    public void AddExtraTurn(int turn)
    {
        TurnController.Instance.AddExtraTurn();
    }

    public int GetCurrentAttackPower()
    {
        return attackPower;
    }

    public int GetPlayerIndex()
    {
        return playerIndex;
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