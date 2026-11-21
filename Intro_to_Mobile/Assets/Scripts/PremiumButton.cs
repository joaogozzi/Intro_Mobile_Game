using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PremiumButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI costText = null;
    [SerializeField] private Image upgradeImg = null;
    [SerializeField] private Button button = null;
    private PremiumStore.Premium premiumData = null;

    public void SetData(PremiumStore.Premium premium)
    {
        premiumData = premium;
        UpdateDisplay();
    }

    public void UpdateDisplay()
    {
        costText.text = premiumData.cost.ToString();
        upgradeImg.sprite = premiumData.sprite;
    }

    public void OnClick()
    {
        if (GameManager.Instance.PurchasePremium(premiumData))
        {
            UpdateDisplay();
        }
    }
}
