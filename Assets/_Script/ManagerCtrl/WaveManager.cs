using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{

    [SerializeField] int _currentWave = 1;
    [SerializeField] private int _totalWave = 7;
    [SerializeField] private int _spawnCountLimiteInWave = 10;

    public int GetSpawnCountLimiteInWave()
    {
        return _spawnCountLimiteInWave;
    }    

    public void NextWave()
    {
        if (_currentWave == _totalWave) this.WinGame();
        else this.UpdateNewWave();
    }

    private void UpdateNewWave()
    {
        this._currentWave++;
        this._spawnCountLimiteInWave *= 2;
        this.ShowWaveText();
    }

    private void ShowWaveText()
    {
        UICtrl.Instance.GameplayScreen.TopScreen.WaveText.UpdateWaveText(_currentWave);
    }


    private void WinGame()
    {
        Debug.Log("WinGame");
    }    

}