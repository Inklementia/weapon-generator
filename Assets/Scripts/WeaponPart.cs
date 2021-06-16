using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPart : MonoBehaviour
{
    [SerializeField] private WeaponType weaponPartType;
    [SerializeField] private Raritylevel partRarityLevel;

    [SerializeField] private WeaponStatPair[] rawStats;

    private Dictionary<WeaponStatType, float> _partStats = new Dictionary<WeaponStatType, float>();

    private void Awake()
    {
        // to through each weapon part and collect all stats 
        foreach (WeaponStatPair statPair in rawStats)
        {
            float weaponStatValue = Random.Range(statPair.minStatValue, statPair.maxStatValue);
            _partStats.Add(statPair.stat, weaponStatValue);
        }
    }

    // to avoid making above variables public
    public Dictionary<WeaponStatType, float>  GetStats()
    {
        return _partStats;
    }

    public int GetRarityLevel()
    {
        return (int)partRarityLevel;
    }
    public int GetWeaponType()
    {
        return (int)weaponPartType;
    }
}
