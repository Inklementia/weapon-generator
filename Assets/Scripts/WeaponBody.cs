using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBody : WeaponPart
{
    public Transform BarrelSocketPos;
    public Transform ScopeSocketPos;
    public Transform MagazineSocketPos;
    public Transform HandleSocketPos;
    public Transform StockSocketPos;

    //for the whole wepon 
    private List<WeaponPart> _weaponParts = new List<WeaponPart>();
    private Dictionary<WeaponStatType, float> _weaponStats = new Dictionary<WeaponStatType, float>();
    private Raritylevel _weaponRaritylevel;
    private WeaponType _weaponType;
    private int _sumRarityParts;
    private int _sumTypeParts;

    // when weapon is generated, this is called
    public void Initialize(WeaponPart barrel, WeaponPart scope, WeaponPart stock, WeaponPart handle, WeaponPart magazine)
    {
        _weaponParts.Add(this);
        _weaponParts.Add(barrel);
        _weaponParts.Add(scope);
        _weaponParts.Add(stock);
        _weaponParts.Add(handle);
        _weaponParts.Add(magazine);

        CalculateStats();
        DetermineRarity();
        DetermineWeaponType();
        DisplayStatsInConsole();
    }

    //going though each part and calculation its stats
    private void CalculateStats()
    {
        foreach (WeaponPart weaponPart in _weaponParts)
        {
            _sumRarityParts += weaponPart.GetRarityLevel();
            _sumTypeParts += weaponPart.GetWeaponType();
            foreach (KeyValuePair<WeaponStatType, float> stat in weaponPart.GetStats())
            {
                if (_weaponStats.ContainsKey(stat.Key)) // f.e "barrel" and "stock" both have accuracy
                {
                    _weaponStats[stat.Key] += stat.Value;
                }
                else
                {
                    _weaponStats.Add(stat.Key, stat.Value);
                }
                
                //Debug.Log(stat.Key + " : " + stat.Value);
            }
        }
    }

    // not dry
    private void DetermineRarity()
    {
        int averageRarity = _sumRarityParts / _weaponParts.Count;
        averageRarity = Mathf.Clamp(averageRarity, 0, Enum.GetValues(typeof(Raritylevel)).Length); // to make sure our rarity is not out of index
        _weaponRaritylevel = (Raritylevel)averageRarity;

        Debug.Log(_weaponRaritylevel);
    }
    // not dry
    private void DetermineWeaponType()
    {
        int averageType = _sumTypeParts / _weaponParts.Count;
        averageType = Mathf.Clamp(averageType, 0, Enum.GetValues(typeof(WeaponType)).Length); // to make sure our rarity is not out of index
        _weaponType = (WeaponType)averageType;

        Debug.Log(_weaponType);
    }

    private void DisplayStatsInConsole()
    {
        foreach (KeyValuePair<WeaponStatType, float> stat in _weaponStats)
        {
            Debug.Log(stat.Key + " : " + stat.Value);
        }
    }
   
}
