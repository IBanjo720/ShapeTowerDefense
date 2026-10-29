
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    [Header("References")]
    public Transform startPoint;
    public Transform[] path;

    [Header("Currency")]
    [SerializeField] private int startingCurrency = 100;
    public int currency { get; private set; }

    private void Awake()
    {
        main = this;
        currency = startingCurrency;
    }

    public void IncreaseCurrency(int amount)
    {
        if (amount <= 0)
            return;

        currency += amount;
    }

    public bool SpendCurrency(int amount)
    {
        // Reject invalid costs.
        if (amount < 0)
        {
            Debug.LogError("Cannot spend a negative amount!");
            return false;
        }

        // Stop the purchase if there isn't enough currency.
        if (currency < amount)
        {
            Debug.Log("Not enough money!");
            return false;
        }

        // Deduct only when the player can afford it.
        currency -= amount;

        // Safety check: currency can never be negative.
        currency = Mathf.Max(0, currency);

        return true;
    }
}
