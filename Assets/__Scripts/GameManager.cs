using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public enum Difficulty {Easy, Medium, Hard };

    public Difficulty myDifficulty = Difficulty.Easy;

    public int Score;
    public int MaxEarthHealth = 5;
    [HideInInspector]
    public int CurrentEarthHealth;
    [HideInInspector]
    public bool lost = false;

    public GameObject HeroObject;
    public Slider EarthHealthSlider;

    public TextMeshProUGUI ScoreTxt;

    public GameObject MainMenuBtn;
    public GameObject RetryBtn;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else Destroy(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        Score = 0;
        AddScore(0);
        CurrentEarthHealth = MaxEarthHealth;

        //EarthHealthSlider = EarthHealthObject.GetComponent<Slider>();
        EarthHealthSlider.maxValue = MaxEarthHealth;
        EarthHealthSlider.value = CurrentEarthHealth;

    }

    public void AddScore(int amount)
    {
        Score += amount;
        ScoreTxt.text = Score.ToString();

    }

    public void EarthTakeDamage()
    {
        CurrentEarthHealth -= 1;
        EarthHealthSlider.value = CurrentEarthHealth;
        //print("Earth takes damage...");
        if (CurrentEarthHealth <= 0)
        {
            Lose();
            HeroObject.SetActive(false);
        }
    }

    public void Lose()
    {
        lost = true;
        MainMenuBtn.SetActive(true);
        RetryBtn.SetActive(true);
        ScoreTxt.alpha = 1;
    }
}
