using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TowerGameScript : MonoBehaviour
{
    public Transform groundPlane;       // The lower plane (used for game over detection)
    public Text scoreText;              // UI Text to display the score
    public GameObject gameOverPanel;    // UI Panel to show game over screen

    private int score = -1;              // Tracks the score based on stack height

    void Start()
    {
        UpdateScore();
        gameOverPanel.SetActive(false);
    }

    void Update()
    {
        CheckGameOver(); // Continuously check if an object falls off the stacking platform
    }

    // Updates the score based on the highest stacked object
    public void UpdateScore()
    {
        score++;
        scoreText.text = "Score: " + score;
    }

    // Checks if any object has fallen onto the ground plane
    private void CheckGameOver()
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Stackable"))
        {
            if (obj.transform.position.y < groundPlane.position.y) // If it falls below ground level
            {
                GameOver();
                break;
            }
        }
    }

    // Ends the game when an object falls to the ground
    private void GameOver()
    {
        gameOverPanel.SetActive(true);
        Debug.Log("Game Over!");
        
    }

    // Restart the game by reloading the scene
    public void RestartGame()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }
}
