using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndSceneScore : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI currentScore;

    [SerializeField]
    private TextMeshProUGUI bestScore;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentScore.text = ScoreManager.main.CurrentScore.ToString();
        bestScore.text = ScoreManager.main.PreviousScore.ToString();
    }
}
