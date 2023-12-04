using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflySpawner : MonoBehaviour
{
    public GameObject game_area;
    public GameObject butterfly_prefab;

    public int butterfly_count = 0;
    public int butterfly_limit = 12;
    public int butterfly_per_frame = 1;

    public float spawn_circle_radius = 150.0f;
    public float death_circle_radius = 160.0f;

    public float fastest_speed = 8.0f;
    public float slowest_speed = 3.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MaintainPopulation()
    {
        if(butterfly_count < butterfly_limit)
        {
            for(int i=0; i<butterfly_per_frame; i++)
            {
                Vector3 position = GetRandomPosition();
            }
        }
    }

    Vector3 GetRandomPosition()
    {
        Vector3 position = Random.insideUnitCircle.normalized;

        position *= spawn_circle_radius;
        position += game_area.transform.position;

        return position;

    }

    //SCR_Butterfly AddButterfly(Vector3 position)
}
