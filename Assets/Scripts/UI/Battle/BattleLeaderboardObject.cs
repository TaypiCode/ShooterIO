using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleLeaderboardObject : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _idText;   
    [SerializeField] private TextMeshProUGUI _nameText;   
    [SerializeField] private TextMeshProUGUI _scoreText;   
    public void SetData(int id, string objName, int score)
    {
        _idText.text = id.ToString();
        _nameText.text = objName.ToString();
        _scoreText.text = score.ToString();
    }
}
