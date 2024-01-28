using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager main;

    public int PreviousScore { get; private set; }
    public int CurrentScore { get; private set; }

    private void Awake()
    {
        main = this;
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Restart()
    {
        if (CurrentScore > PreviousScore)
        {
            PreviousScore = CurrentScore;
        }
    }

    public void GiveScore()
    {
        CurrentScore++;
    }
}
