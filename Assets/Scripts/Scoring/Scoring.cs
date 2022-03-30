using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scoring : MonoBehaviour
{

    //handles all scoring and life count
    public int Score { get; private set; }
    public int Lives { get; private set; }

    public static Scoring Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void AddScore(int amount)
    {
        Score += amount;
    }
    public void IncreaseLives(int amount)
    {
        Lives += amount;
    }

    public void DecreaseLives(int amount)
    {
        Lives -= amount;
        if(Lives < 0)
        {
            SceneManager.LoadScene(0);
        }
    }
}
