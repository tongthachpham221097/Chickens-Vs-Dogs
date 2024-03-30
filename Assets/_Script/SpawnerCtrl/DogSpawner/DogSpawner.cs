using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogSpawner : Spawner
{
    [Header("Dog Spawner")]
    [SerializeField] private SpawnPosition _spawnPosition;
    public SpawnPosition SpawnPosition => _spawnPosition;

    [SerializeField] private float _timer;
    [SerializeField] private float _timeDelay = 5f;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private bool _isSpawn;

    [SerializeField] private int _spawnCount;
    [SerializeField] private int _spawnCountLimiteInWave;

    protected void Start()
    {
        this.UpdateSpawnCountLimiteInWave();
    }

    private void UpdateSpawnCountLimiteInWave()
    {
        this._spawnCountLimiteInWave = ManagerCtrl.Instance.Wave.GetSpawnCountLimiteInWave();
        this._spawnCount = 0;
        this._isSpawn = true;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadSpawnPosition();
    }

    protected virtual void LoadSpawnPosition()
    {
        if (this._spawnPosition != null) return;
        this._spawnPosition = GetComponentInChildren<SpawnPosition>();
        Debug.LogWarning(transform.name + ": LoadSpawnPosition", gameObject);
    }

    private void FixedUpdate()
    {
        this.Spawning();
    }

    void Spawning()
    {
        if (!this._isSpawn) return;
        if (this.CheckTimeDelay()) return;
        this.Spawn();
        this._spawnCount++;
        this.CheckSpawnedEnemiesCount();
    }

    private void Spawn()
    {
        Transform prefab = this.RandomPrefab();
        Transform spawnPoint = this._spawnPosition.RamdomSpawnPoint();
        this._spawnPoint = spawnPoint;
        Vector3 spawnPos = spawnPoint.position;
        Transform obj = this.Spawn(prefab, spawnPos, Quaternion.identity);
        obj.gameObject.SetActive(true);
    }    

    private void CheckSpawnedEnemiesCount()
    {
        if (this._spawnCount < _spawnCountLimiteInWave) return;
        this._isSpawn = false;
        this.NextWave();
        this.UpdateSpawnCountLimiteInWave();
    }    

    private void NextWave()
    {
        ManagerCtrl.Instance.Wave.NextWave();
    }    

    protected override void SetParentNewPrefab(Transform newPrefab)
    {
        newPrefab.parent = this._spawnPoint;
    }

    bool CheckTimeDelay()
    {
        if(this._timer < this._timeDelay)
        {
            this._timer += Time.fixedDeltaTime;
            return true;
        }
        this._timer = 0;
        return false;
    }

}
