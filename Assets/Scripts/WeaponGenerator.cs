using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public  class WeaponGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] bodyParts;
    [SerializeField] private GameObject[] barrelParts;
    [SerializeField] private GameObject[] scopeParts;
    [SerializeField] private GameObject[] stockParts;
    [SerializeField] private GameObject[] handleParts;
    [SerializeField] private GameObject[] magazineParts;

    private GameObject _prevWeapon;
    private Transform _weaponSpawnPoint;

    private void Start()
    {
        // temporary spot to spawn a weapon
        // point where to spawn a weapon (i.e. chests etc)
        _weaponSpawnPoint = new GameObject("WeaponSpawnPoint").transform;
    }

    private void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ClearLog();
            GenerateWeapon(_weaponSpawnPoint);
        }
    }

    private void GenerateWeapon(Transform place)
    {
        // if weapon already was generated -> destroy it
        if(_prevWeapon != null)
        {
            Destroy(_prevWeapon);
        }
        // without object pool it is easier -> but performance and stuff
        GameObject randomBody = GetRandomPart(bodyParts);
        GameObject generatedBody = Instantiate(randomBody, place.position, Quaternion.identity);
        WeaponBody weaponBody = generatedBody.GetComponent<WeaponBody>();

        WeaponPart barrel = GenerateWeaponPart(barrelParts, weaponBody.BarrelSocketPos);
        WeaponPart scope = GenerateWeaponPart(scopeParts, weaponBody.ScopeSocketPos);
        WeaponPart stock = GenerateWeaponPart(stockParts, weaponBody.StockSocketPos);
        WeaponPart handle = GenerateWeaponPart(handleParts, weaponBody.HandleSocketPos);
        WeaponPart magazine = GenerateWeaponPart(magazineParts, weaponBody.MagazineSocketPos);

        // order doesn matter, as each part has certain stats, which only matters
        weaponBody.Initialize(barrel, scope, stock, handle, magazine);

        _prevWeapon = generatedBody;
    }

    private WeaponPart GenerateWeaponPart(GameObject[] parts, Transform socket)
    {
        GameObject randomPart = GetRandomPart(parts);
        WeaponPart generatedPart = Instantiate(randomPart, socket.position, socket.rotation, socket).GetComponent<WeaponPart>();
        return generatedPart;
    }

    // get part from array (that filled in In Inspector)
    private GameObject GetRandomPart(GameObject[] parts)
    {
        int randomIndex = Random.Range(0, parts.Length);
        return parts[randomIndex];
    }


    //** temporary **//
    // to clear console each time you generate weapon
    public void ClearLog() 
    {
        var assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
        var type = assembly.GetType("UnityEditor.LogEntries");
        var method = type.GetMethod("Clear");
        method.Invoke(new object(), null);
    }
}
