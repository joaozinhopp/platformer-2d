using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemManager : MonoBehaviour
{
    public SOInt coins;
    public static ItemManager Instance;
    public TextMeshProUGUI uiTextCoins;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

    }
    private void Start()
    {
        Reset();
    }

    private void Reset()
    {
        coins.Value = 0;
    }

    public void AddCoins(int amount = 1)
    {
        coins.Value += amount;
        UpdateUI();
    }

    private void UpdateUI()
    {
        uiTextCoins.text = coins.Value.ToString();
    }
}
