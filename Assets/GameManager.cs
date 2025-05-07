using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public ArrangingGameItems arrangingGameItems;
    public FindDifferenceGame findDifferenceGame;

    public void OnGameCardClicked(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}