using System.Collections.Generic;
using UnityEngine;

public class Spawner : LoboMonoBehaviour
{
    
    [Header("Spawner")]
    [SerializeField] protected Transform holder;

    [SerializeField] protected int spawnedCount = 0;
    public int SpawnedCount => spawnedCount;

    [SerializeField] protected List<Transform> prefabs;
    [SerializeField] protected List<Transform> poolObjs = new List<Transform>();
    
    protected override void LoadComponents()
    {
        this.LoadPrefabs();
        this.LoadHolder();
    }
    
    protected virtual void LoadHolder()
    {
        if (this.holder != null) return;
        this.holder = transform.Find("Holder");
        Debug.Log(transform.name + ": LoadHolder", gameObject);
    }

    protected virtual void LoadPrefabs()
    {
        if (this.prefabs.Count > 0) return;

        Transform prefabObj = transform.Find("Prefabs");
        foreach (Transform prefab in prefabObj)
        {
            this.prefabs.Add(prefab);
        }

        this.Hideprefabs();

        Debug.Log(transform.name + ": LoadPrefabs", gameObject);
    }

    protected virtual void Hideprefabs()
    {
        foreach (Transform prefab in this.prefabs)
        {
            prefab.gameObject.SetActive(false);
        }
    }

    public virtual Transform Spawn(string prefabName, Vector3 spawnPos, Quaternion rotation)
    {
        Transform prefab = this.GetPrefabByName(prefabName);
        if (prefab == null)
        {
            Debug.LogWarning("Prefab not found:" + prefabName);
            return null;
        }

        return this.Spawn(prefab, spawnPos, rotation);
    }

    public virtual Transform Spawn(Transform prefab, Vector3 spawnPos, Quaternion rotation)
    {
        Transform newPrefab = this.GetObjectFromPoll(prefab);
        newPrefab.SetPositionAndRotation(spawnPos, rotation);

        this.SetParentNewPrefab(newPrefab);
        this.spawnedCount++;
        return newPrefab;
    }

    protected virtual void SetParentNewPrefab(Transform newPrefab)
    {
        newPrefab.parent = this.holder;
    }    

    protected virtual Transform GetObjectFromPoll(Transform prefab)
    {
        for (int i = this.poolObjs.Count - 1; i >= 0; i--)
        {
            Transform poolObj = this.poolObjs[i];
            if (poolObj.name == prefab.name)
            {
                this.poolObjs.RemoveAt(i);
                return poolObj;
            }
        }

        Transform newPrefab = Instantiate(prefab);
        newPrefab.name = prefab.name;
        return newPrefab;
    }

    public virtual void Despawn(Transform obj)
    {
        this.poolObjs.Add(obj);
        obj.gameObject.SetActive(false);
        this.spawnedCount--;
        obj.SetParent(this.holder, false);
    }

    public virtual Transform GetPrefabByName(string prefabName)
    {
        foreach (Transform prefab in this.prefabs)
        {
            if (prefab.name == prefabName) return prefab;
        }

        return null;
    }
    public virtual Transform RandomPrefab()
    {
        int rand = Random.Range(0, this.prefabs.Count);
        return this.prefabs[rand];
    }
}