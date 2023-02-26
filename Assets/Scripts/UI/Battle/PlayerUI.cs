using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;
using UnityEngine.SceneManagement;
using System;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private float _battleTime;
    [SerializeField] private AdsScript _ads;
    [SerializeField] private SaveGame _save;
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private PlayerEvents _playerEvents;
    [SerializeField] private TextMeshProUGUI _battleTimerText;
    [SerializeField] private BattleLeaderboard _battleLeaderboard;
    [SerializeField] private Slider _hpSlider;
    [SerializeField] private TextMeshProUGUI _hpText;
    [SerializeField] private TextMeshProUGUI _ammoText;
    [Header("End game")]
    [SerializeField] private GameObject _deathCanvas;
    [SerializeField] private TextMeshProUGUI _playerAddScoreText;

    private Timer _sessionTimer;

    private static int _gamesCount;
    private void Start()
    {
        GameData.GameEnded = false;
        _sessionTimer = gameObject.AddComponent<Timer>();
        _sessionTimer.SetTimer(_battleTime);
        LockCursor();
    }
    private void Update()
    {
        
        if (_sessionTimer.GetTime() <= 0 && GameData.GameEnded == false)
        {
            
            EndGame();
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            System.DateTime date;
            date = new System.DateTime(Convert.ToInt64(_sessionTimer.GetTime()) * System.TimeSpan.TicksPerSecond);
            _battleTimerText.text = string.Format("{0:00}:{1:00}", date.Minute, date.Second);
        }
        if (GameData.GameEnded == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
               //SwitchCursorState();
            }
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                _battleLeaderboard.ShowLeaderboard();
                UnLockCursor();
            }
            if (Input.GetKeyUp(KeyCode.Tab))
            {
                _battleLeaderboard.CloseLeaderboard();
                LockCursor();
            }
        }
    }
    private void SwitchCursorState()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            UnLockCursor();
        }
        else
        {
            LockCursor();
        }
    }
    private void LockCursor()
    {
        if (GameData.GameEnded == false)
        {
            //Cursor.lockState = CursorLockMode.Locked;
            //_playerEvents.UnLockCamera();
        }
    }
    private void UnLockCursor()
    {
        //Cursor.lockState = CursorLockMode.None;
        //_playerEvents.LockCamera();

    }
    private void EndGame()
    {
        _gamesCount++;
        GameData.GameEnded = true;
        _battleLeaderboard.ShowLeaderboard();
        _deathCanvas.SetActive(true);
        float score = _battleLeaderboard.GetScore(_playerData.Stat);
        GameData.PlayerScore += score;
        _playerAddScoreText.text = "Вы получаете " + score + " рейтинга за " + _battleLeaderboard.GetPosition(_playerData.Stat) + " место";
        UnLockCursor();
        _save.SaveProgress();
        if (_gamesCount > 5)
        {
            RateUsScript.ShowRateUs();
            _gamesCount = 0;
        }
        _ads.ShowNonRewardAd();
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ToMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void SetMaxHP(float value)
    {
        _hpSlider.maxValue = value;
    }
    public void SetHP(float value)
    {
        _hpText.text = value.ToString();
        _hpSlider.value = value;
    }
    public void SetAmmoText(float value)
    {
        _ammoText.text = value.ToString();
    }
}
