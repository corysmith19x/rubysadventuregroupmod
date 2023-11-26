using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtParticleScript : MonoBehaviour
{
    public float timerDisplay = 3.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timerDisplay -= Time.deltaTime;
        if (timerDisplay < 0)
        {
            Object.Destroy(gameObject);
        }
    }
}
