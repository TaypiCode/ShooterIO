using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private PlayerEvents _playerEvents;
    [SerializeField] private BattleLeaderboard _battleLeaderboard;
    [SerializeField] private GameObject _deathCanvas;
    [SerializeField] private Slider _hpSlider;
    [SerializeField] private TextMeshProUGUI _hpText;
    [SerializeField] private TextMeshProUGUI _ammoText;
    private void Start()
    {
        LockCursor();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SwitchCursorState();
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
    private void SwitchCursorState()
    {
        if(Cursor.lockState== CursorLockMode.Locked)
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
        Cursor.lockState = CursorLockMode.Locked;
        _playerEvents.UnLockCamera();
    }
    private void UnLockCursor()
    {
        if (_deathCanvas.activeSelf == false)
        {
            Cursor.lockState = CursorLockMode.None;
            _playerEvents.LockCamera();
        }
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
