using UnityEngine;
using UnityEngine.Serialization;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "Upgrades", menuName = "Scriptable Objects/Upgrades")]
public class Upgrades : ScriptableObject
{
    public enum UpgradeType
    {
        Click,
        Idle
    }

    [Serializable]
    public class Upgrade
    {
        public string name = "";
        public Sprite sprite = null;
        public UpgradeType type = UpgradeType.Click;

        public long baseCost = 10;
        public float costMultiplier = 1.15f;
        public long amountPerLevel = 1;
    }

    public List<Upgrade> upgradeList = new List<Upgrade>();
}
