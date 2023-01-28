using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SaveGame : MonoBehaviour
{
    private Save save = new Save();

#if UNITY_ANDROID && !UNITY_EDITOR
    private void OnApplicationPause(){
        SaveProgress();
    }
#endif
    private void OnApplicationQuit()
    {
        SaveProgress();
    }

    public void SaveProgress()
    {
        save.playerScore =  GameData.PlayerScore;
        LeaderboardScript.SetLeaderboardValue(LeaderboardScript.Names.Rating, GameData.PlayerScore);
        PlayerPrefs.SetString("SV", JsonUtility.ToJson(save));
        PlayerPrefs.Save();
    }
}
[Serializable]
public class Save
{
    public float playerScore;
}