using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PremiumStore", menuName = "Scriptable Objects/PremiumStore")]
public class PremiumStore : ScriptableObject
{
    [Serializable]
    public class Premium
    {
        public string name = "";
        public Sprite sprite = null;
        public long cost = 0;
        public long pointsAmount = 0;
    }

    public List<Premium> premiumList = new List<Premium>();
}
