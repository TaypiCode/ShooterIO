using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _playerScoreText;
    private void Start()
    {
        _playerScoreText.text = "Рейтинг: " + GameData.PlayerScore;
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
