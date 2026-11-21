using System.Collections.Generic;
using UnityEngine;

public class StoreScreen : MonoBehaviour
{
    [SerializeField] private RectTransform content = null;
    [SerializeField] private GameObject upgradeButtonPrefab = null;
    [SerializeField] private GameObject premiumButtonPrefab = null;

    private void OnEnable()
    {
        OpenClickUpgrades();
    }

    void SetupUpgrades(List<Upgrades.Upgrade> upgrades, string effectLabel)
    {
        for (int i = 0; i < upgrades.Count; ++i)
        {
            GameObject button = Instantiate(upgradeButtonPrefab, content);
            UpgradeButton buttonData = button.GetComponent<UpgradeButton>();
            buttonData.SetData(upgrades[i], effectLabel);
        }
    }

    void DestroyBtns()
    {
        for (int i = content.childCount - 1; i >= 0; --i)
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

    public void OpenClickUpgrades()
    {
        DestroyBtns();
        SetupUpgrades(GameManager.Instance.GetClickUpgrades(), "/ click");
    }

    public void OpenIdleUpgrades()
    {
        DestroyBtns();
        SetupUpgrades(GameManager.Instance.GetIdleUpgrades(), "/ sec");
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