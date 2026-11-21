using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreTxt = null;
    [SerializeField] private TextMeshProUGUI perSecondTxt = null;
    [SerializeField] private Image cookieImg = null;
    [SerializeField] private Upgrades clickUpgrades = null;
    [SerializeField] private Upgrades idleUpgrades = null;
    [SerializeField] private PremiumStore premium = null;

    private long points = 0;
    private float idleTimer = 0f;

    private readonly Dictionary<Upgrades.Upgrade, int> levels = new Dictionary<Upgrades.Upgrade, int>();

    static GameManager instance = null;
    public static GameManager Instance { get { return instance; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            UpdateScore();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        idleTimer += Time.deltaTime;
        while (idleTimer >= 1f)
        {
            idleTimer -= 1f;
            long perSecond = GetPointsPerSecond();
            if (perSecond > 0)
                BuyScore(perSecond);
        }
    }

    void UpdateScore()
    {
        scoreTxt.text = $"Score: {points}";
        if (perSecondTxt != null)
            perSecondTxt.text = $"{GetPointsPerSecond()} / s";
    }

    public void AddScore()
    {
        points += GetClickPower();
        UpdateScore();
    }

    public void BuyScore(long score)
    {
        points += score;
        UpdateScore();
    }

    public bool UsePoints(long amount)
    {
        if (amount <= points)
        {
            points -= amount;
            return true;
        }

        return false;
    }

    public long GetPoints()
    {
        return points;
    }

    public int GetLevel(Upgrades.Upgrade upgrade)
    {
        return levels.TryGetValue(upgrade, out int level) ? level : 0;
    }

    public long GetCost(Upgrades.Upgrade upgrade)
    {
        return (long)Math.Ceiling(upgrade.baseCost * Math.Pow(upgrade.costMultiplier, GetLevel(upgrade)));
    }

    public long GetClickPower()
    {
        long total = 1;
        foreach (Upgrades.Upgrade up in clickUpgrades.upgradeList)
            total += GetLevel(up) * up.amountPerLevel;
        return total;
    }

    public long GetPointsPerSecond()
    {
        long total = 0;
        foreach (Upgrades.Upgrade up in idleUpgrades.upgradeList)
            total += GetLevel(up) * up.amountPerLevel;
        return total;
    }

    public bool PurchaseUpgrade(Upgrades.Upgrade upgrade)
    {
        if (UsePoints(GetCost(upgrade)))
        {
            levels[upgrade] = GetLevel(upgrade) + 1;
            UpdateScore();
            return true;
        }

        return false;
    }

    public List<Upgrades.Upgrade> GetClickUpgrades()
    {
        return clickUpgrades.upgradeList;
    }

    public List<Upgrades.Upgrade> GetIdleUpgrades()
    {
        return idleUpgrades.upgradeList;
    }

    public List<PremiumStore.Premium> GetPremium()
    {
        return premium.premiumList;
    }

    public bool PurchasePremium(PremiumStore.Premium premium)
    {
        BuyScore(premium.pointsAmount);
        return true;
    }
}