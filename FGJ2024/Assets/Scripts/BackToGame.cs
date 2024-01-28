using UnityEngine.SceneManagement;
using UnityEngine;

public class BackToGame : MonoBehaviour
{
    AudioSource audioData;

    public void moi()
    {
        audioData = GetComponent<AudioSource>();
        audioData.Play(0);
    }
    public void getback()
    {
        ScoreManager.main.Restart();
        SceneManager.LoadScene(0);
    }

}
