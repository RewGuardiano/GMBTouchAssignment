using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    public void PlayAgain()
    {

        SceneManager.LoadScene("StackingGameScene"); // Reload the current scene
    }
}
