using System;
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
    public GameObject YouWinPanel;
    public GameObject cubePrefab;
    public Transform spawnPoint;
    private bool isGameOver = false;

    private int score = 0;              // Tracks the score based on stack height
    private int winScore = 10; 

    // Getter
    public bool IsGameOver()
    {
        return isGameOver;
    }

    void Start()
    {
        UpdateScore();
        gameOverPanel.SetActive(false);
        YouWinPanel.SetActive(false);
        SpawnNewCube();
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

        if (score >= winScore)
        {
            WinGame();
        }
    }

    private void WinGame()
    {
        isGameOver = true;
        YouWinPanel.SetActive(true);
    }

    // Checks if any object has fallen onto the ground plane
    private void CheckGameOver()
    {
        if (isGameOver) return; // Prevent game over check if the game is already won

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
        isGameOver = true;
    }

    // Restart the game by reloading the scene
    public void RestartGame()
    {
     
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }



    public void SpawnNewCube()
    {
        GameObject newCube = Instantiate(cubePrefab, spawnPoint.position, Quaternion.identity);
        CubeScript cubeScript = newCube.GetComponent<CubeScript>();
        if (cubeScript != null)
        {
            cubeScript.SetLandingZoneZ(spawnPoint.position.z); // Pass the landing zone Z-position
        }
    }
}