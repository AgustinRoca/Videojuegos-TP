using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class EnemyFactory
{
    public enum eEnemy {None, Zombie, FastZombie, Tank, Boss};

    public static GameObject createEnemy(eEnemy eenemy, Transform transform){
        GameObject prefab = null;
        
        switch (eenemy)
        {
            case eEnemy.Zombie:
                prefab = Resources.Load<GameObject>("Prefabs/Zombie/Prefabs/Enemy");
                break;
            case eEnemy.FastZombie:
                prefab = Resources.Load<GameObject>("Prefabs/_GhoulZombie/Ghoul");
                break;
            case eEnemy.Tank:
                prefab = Resources.Load<GameObject>("Prefabs/SkinlessZombie/Prefabs/skinless zombie");
                break;
            case eEnemy.Boss:
                prefab = Resources.Load<GameObject>("Prefabs/Skeleton/Prefabs/DungeonSkeleton");
                break;
            default:
                Debug.Log($"{eenemy} not recongnized as a weapon in enum eEnemy");
                break;
        }
        return (GameObject.Instantiate(prefab, transform.position, transform.rotation)) as GameObject;
    }

}
