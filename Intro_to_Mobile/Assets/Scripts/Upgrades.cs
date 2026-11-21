using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "Upgrades", menuName = "Scriptable Objects/Upgrades")]
public class Upgrades : ScriptableObject
{
    [Serializable]
    public class Upgrade
    {
        public string name = "";
        public Sprite sprite = null;
        public long cost = 0;
        public long clickAmount = 0;
    }

    public List<Upgrade> upgradeList = new List<Upgrade>();
}
