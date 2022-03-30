using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballScore : MonoBehaviour
{
    public int ScoreAddition;

    //specific score of a ball
    private void OnDisable()
    {
        Scoring.Instance.AddScore(ScoreAddition);
    }
}
