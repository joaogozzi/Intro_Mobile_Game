using System.Collections.Generic;
using UnityEngine;
using static Upgrades;

public class StoreScreen : MonoBehaviour
{
    [SerializeField] private RectTransform content = null;
    [SerializeField] private GameObject upgradeButtonPrefab = null;
    [SerializeField] private GameObject premiumButtonPrefab = null;

    private void OnEnable()
    {
        SetupUpgrades();
    }

    void SetupUpgrades()
    {
        List<Upgrades.Upgrade> upgrades = GameManager.Instance.GetUpgrades();

        for (int i = 0; i < upgrades.Count; ++i)
        {
            GameObject button = Instantiate(upgradeButtonPrefab, content);
            UpgradeButton buttonData = button.GetComponent<UpgradeButton>();
            buttonData.SetData(upgrades[i]);
        }
    }

    void DestroyBtns()
    {
        for (int i = 0; i < content.childCount; ++i)
        {
            Destroy(content.GetChild(i).gameObject);
        }
    }

    void SetupPointsStore()
    {
        List<PremiumStore.Premium> premium = GameManager.Instance.GetPremium();

        for (int i = 0; i < premium.Count; ++i)
        {
            GameObject button = Instantiate(premiumButtonPrefab, content);
            PremiumButton buttonData = button.GetComponent<PremiumButton>();
            buttonData.SetData(premium[i]);
        }
    }

    public void OpenUpgrades()
    {
        DestroyBtns();
        SetupUpgrades();
    }

    public void OpenPremium()
    {
        DestroyBtns();
        SetupPointsStore();
    }

    private void OnDisable()
    {
        DestroyBtns();
    }

    public void OnCloseScreen()
    {
        ScreenManager.Instance.CloseScreen();
    }
}