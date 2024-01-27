using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager main { get; private set; }
    public bool CanMove { get; private set; }

    [SerializeField]
    private TestSpawner spawner;

    private float lastMove = 0f;
    private float waitTime = 1f;

    void Awake()
    {
        main = this;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log($"Spawner: {spawner.GetFollowers().All(x => x.HasMoved)}, {CanMove}");
        if (!CanMove && (Time.time - lastMove > waitTime))
        {
            Debug.Log("Can move again");
            CanMove = true;
        }
        else if (CanMove)
        {
            bool allMoved = spawner.GetFollowers().All(x => x.HasMoved);
            if (allMoved)
            {
                Debug.Log("All moved");
                CanMove = false;
                lastMove = Time.time;
                Debug.Log(lastMove);

                spawner.GetFollowers().ForEach(x => x.HasMoved = false);
            }
        }
    }
}
