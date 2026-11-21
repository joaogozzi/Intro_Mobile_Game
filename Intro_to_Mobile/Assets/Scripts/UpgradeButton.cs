using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText = null;
    [SerializeField] private TextMeshProUGUI levelText = null;
    [SerializeField] private TextMeshProUGUI costText = null;
    [SerializeField] private Image upgradeImg = null;
    [SerializeField] private Button button = null;
    private Upgrades.Upgrade upgradeData = null;

    private long prevPoints = 0;

    private string effectLabel = "";

    public void SetData(Upgrades.Upgrade upgrade, string effectLabel = "")
    {
        upgradeData = upgrade;
        this.effectLabel = effectLabel;
        prevPoints = GameManager.Instance.GetPoints();
        UpdateDisplay();
    }

    private void Update()
    {
        if (prevPoints != GameManager.Instance.GetPoints())
        {
            UpdateDisplay();
            prevPoints = GameManager.Instance.GetPoints();
        }
    }

    public void UpdateDisplay()
    {
        int level = GameManager.Instance.GetLevel(upgradeData);
        long cost = GameManager.Instance.GetCost(upgradeData);

        costText.text = cost.ToString();
        upgradeImg.sprite = upgradeData.sprite;
        button.interactable = cost <= GameManager.Instance.GetPoints();

        if (nameText != null)
            nameText.text = upgradeData.name;

        if (levelText != null)
        {
            levelText.text = $"Lv {level}  (+{upgradeData.amountPerLevel} {effectLabel})";
        }
    }

    public void OnClick()
    {
        if (GameManager.Instance.PurchaseUpgrade(upgradeData))
        {
            UpdateDisplay();
        }
    }
}