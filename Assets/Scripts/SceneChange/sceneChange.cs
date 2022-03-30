using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneChange : MonoBehaviour
{
    // a simple code to change from scene to scene
    public void ChangeScene(int number)
    {
        SceneManager.LoadScene(number);
    }
}
