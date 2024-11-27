using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    float time;
    const float cooldown = 2;

    [SerializeField] GameObject[] obstaclePrefab;

    private void Update()
    {
        spanw();
    }
    public void spanw()
    {
        if (time <=0)
        {
            float randomT = Random.Range(0, 1f);
            GameObject towersellect = null;

            switch (randomT)
            {
                case <= 0.5f:
                    towersellect = obstaclePrefab[0];
                    break;
                case >=0.5f:
                    towersellect= obstaclePrefab[1];
                    break;
            }
            Instantiate(towersellect);
            time = cooldown;
        }
        else
        {
            time-= Time.deltaTime;
        }
    }
}
