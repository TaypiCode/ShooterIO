using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    private static bool _gameEnded;
    private static float _playerScore;

    public static bool GameEnded { get => _gameEnded; set =>  _gameEnded = value; }
    public static float PlayerScore { get => _playerScore; set => _playerScore = value; }
}
