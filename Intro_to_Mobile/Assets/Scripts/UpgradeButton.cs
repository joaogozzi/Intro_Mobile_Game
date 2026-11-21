using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI costText = null;
    [SerializeField] private Image upgradeImg = null;
    [SerializeField] private Button button = null;
    private Upgrades.Upgrade upgradeData = null;

    private long prevPoints = 0;

    public void SetData(Upgrades.Upgrade upgrade)
    {
        upgradeData = upgrade;
        prevPoints = GameManager.Instance.GetPoints();
        UpdateDisplay();
    }

    private void Update()
    {
        if(prevPoints != GameManager.Instance.GetPoints())
        {
            UpdateDisplay();
            prevPoints = GameManager.Instance.GetPoints();
        }
    }

    public void UpdateDisplay()
    {
        costText.text = upgradeData.cost.ToString();
        upgradeImg.sprite = upgradeData.sprite;
        button.interactable = upgradeData.cost < GameManager.Instance.GetPoints();
    }
    
    public void OnClick()
    {
        if(GameManager.Instance.PurchaseUpgrade(upgradeData))
        {
            UpdateDisplay();
        }
    }
}
