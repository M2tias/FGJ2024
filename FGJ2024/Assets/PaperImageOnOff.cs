using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PaperImageOnOff : MonoBehaviour
{
    int max_health = 3;
    [SerializeField]
    List<Image> imgs;
    void Update()
    {
        int health = GameManager.main.Health;
        for (int i = 0; i < max_health; i++)
        {
            bool isVisible = (health - 1) >= i;
            imgs[i].enabled = isVisible;

        }
    }
    



}
