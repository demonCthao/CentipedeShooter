using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private Blaster _blaster;
    private Centipede _centipede;
    private MushroomField _mushroomField;

    public Text scoreText;
    public Text livesText;
    public GameObject GameObject;
    
    private int score;
    private int lives;

    private void Awake()
    {
        if (Instance == null) { Instance = this; }
        else{ Destroy(gameObject); }
    }
    private void OnDestroy()
    {
        if (Instance == this) { Instance = null; }
    }

    private void Start()
    {
        _blaster = FindObjectOfType<Blaster>();
        _centipede = FindObjectOfType<Centipede>();
        _mushroomField = FindObjectOfType<MushroomField>();
        NewGame();
    }

    private void Update()
    {
        if(lives <=0 && Input.anyKeyDown){NewGame();}
    }

    private void NewGame()
    {
        SetScore(0);
        SetLives(3);
        
        _centipede.Respawn();
        _blaster.Respawn();
        _mushroomField.Clear();
        _mushroomField.Generate();
        GameObject.SetActive(false);
        
    }

    private void GameOver()
    {
        _blaster.gameObject.SetActive(false);
        GameObject.SetActive(true);
    }

    public void ResetRound()
    {
        SetLives(lives - 1);
        if (lives <=0)
        {
            GameOver();
            return;
        }
        _blaster.Respawn();
        _centipede.Respawn();
        _mushroomField.Heal();
    }

    public void NextLevel()
    {
        _centipede.speed *= 1.1f;
        _centipede.Respawn();
    }

    public void IncreaseScore(int amount)
    {
       SetScore(score + amount);
    }

    private void SetScore(int value)
    {
        score = value;
        scoreText.text = score.ToString();
    }
    private void SetLives(int value)
    {
        lives = value;
        livesText.text = lives.ToString();
    }
}