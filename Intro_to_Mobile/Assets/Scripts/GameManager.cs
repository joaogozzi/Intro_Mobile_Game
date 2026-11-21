using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Upgrades;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreTxt = null;
    [SerializeField] private Image cookieImg = null;
    [SerializeField] private Upgrades upgrades = null;
    [SerializeField] private PremiumStore premium = null;

    private Upgrades.Upgrade currentUpgrade = null;
    private long points = 0;

    static GameManager instance = null;
    public static GameManager Instance { get { return instance; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            UpdateScore();
            currentUpgrade = upgrades.upgradeList[0];
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void UpdateScore()
    {
        scoreTxt.text = $"Score: {points}";
    }

    public void AddScore()
    {
        points += currentUpgrade.clickAmount;
        UpdateScore();
    }

    public void BuyScore(long score)
    {
        points += score;
        UpdateScore();
    }

    public bool UsePoints(long amount)
    {
        if (amount < points)
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

    public List<Upgrades.Upgrade> GetUpgrades()
    {
        return upgrades.upgradeList;
    }

    public List<PremiumStore.Premium> GetPremium()
    {
        return premium.premiumList;
    }

    public bool PurchaseUpgrade(Upgrades.Upgrade upgrade)
    {
        if (UsePoints(upgrade.cost))
        {
            currentUpgrade = upgrade;
            cookieImg.sprite = upgrade.sprite;
            UpdateScore();
            return true;
        }

        return false;
    }

    public bool PurchasePremium(PremiumStore.Premium premium)
    {
        BuyScore(premium.pointsAmount);
        UpdateScore();
        return true;
    }
}