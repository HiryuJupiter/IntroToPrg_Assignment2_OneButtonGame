using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Gun))]
public class GameplaySceneManager : MonoBehaviour
{
    public static GameplaySceneManager instance;

    //Consts
    const int SceneIndex_MainMenu = 0;
    const int MaxLives = 3;

    //Class reference
    TargetSpawner targetSpawn;
    UIManager ui;
    Gun gun;
    
    //Status
    int lives;
    int killCount;
    bool gameOver;

    #region MonoBehavior
    void Awake()
    {
        //Super lazy singleton
        instance = this;
    }
    void Start()
    {
        //Reference
        ui = UIManager.instance;
        targetSpawn = TargetSpawner.instance;
        gun = GetComponent<Gun>();

        //Initialize lives
        lives = MaxLives;
        UpdateLivesDisplay();

        //Start spawning targets
        targetSpawn.StartSpawning();
    }


    void Update()
    {
        if (PlayerClickedShoot())
        {
            gun.ShootBullet();
        }
    }
    #endregion

    #region Public methods
    public void Reload()
    {
        if (!gameOver)
            gun.Reload();
    }

    public void LoseHealth()
    {
        if (gameOver)
            return;

        //Reduce health. Check if game is over.
        if (--lives <= 0) 
        {
            GameOver();
        }

        //Update health display and make a red flash on screen to indicate damage was taken.
        UpdateLivesDisplay();
        ui.OnHurtFlashRedBorder();
    }

    public void BackToMainMenu()
    {
        //Load main menu scene
        SceneManager.LoadScene(SceneIndex_MainMenu);
    }

    public void IncreaseKillCount ()
    {
        //Increase kill count
        ui.UpdateKillCount(++killCount);
    }

    public void Replay()
    {
        //Load the current scene again
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    #endregion

    #region Private methods
    void UpdateLivesDisplay() => ui.UpdateLives(lives);
    bool PlayerClickedShoot() => !gameOver && Input.GetMouseButtonDown(0);

    void GameOver ()
    {
        //Use gameover boolean to prevent certain methods from running.
        gameOver = true; 

        //Display highscore
        ui.GameOver(killCount);
        targetSpawn.StopSpawning();
    }
    #endregion
}
