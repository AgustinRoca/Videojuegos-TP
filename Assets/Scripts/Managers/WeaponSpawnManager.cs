using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawnManager : MonoBehaviour
{
    private WeaponSpawnPoint[] _spawnPoints;
    [SerializeField] private GameObject _weaponBoxPrefab;
    [SerializeField] private Gun[] _availableGuns;

    
    void Start()
    {
        EventManager.instance.OnWeaponSpawn += OnWeaponSpawn;
        _spawnPoints = FindObjectsOfType<WeaponSpawnPoint>();
    }

    public void OnWeaponSpawn(WeaponFactory.eWeapon weapon)
    {
        int score = GlobalData.instance.Score;
        int spawnPointIndex = Random.Range(0, _spawnPoints.Length);
        Transform transform = _spawnPoints[spawnPointIndex].transform;
        GameObject go = Instantiate(
            _weaponBoxPrefab, 
            transform.position, 
            transform.rotation
        ) as GameObject;
        WeaponBox wb = go.GetComponent<WeaponBox>();
        wb.SetOwner(weapon);
    }
}
