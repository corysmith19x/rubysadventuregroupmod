using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyMovement : MonoBehaviour
{
    [SerializeField]
    public float speed;
    [SerializeField]
    public float range;
    [SerializeField]
    public float maxDistance;

    new Rigidbody2D rigidbody2D;
    public SpriteRenderer spriteRenderer;

    public bool flip;

    Vector2 wayPoint;

    // Start is called before the first frame update
    void Start()
    {
        //rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetNewDestination();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, wayPoint, speed * Time.deltaTime);
        float x = 0 + transform.position.x;


        if (Vector2.Distance(transform.position, wayPoint) < range)
        {
            SetNewDestination();
        }
        if(x < 0f)
        {
            transform.localScale = new Vector3( 0.5f, 0.5f, 0.5f);

        }
        if (x > 0f)
        {
            transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);

        }


    }

    void SetNewDestination()
    {
        wayPoint = new Vector2(transform.position.x + Random.Range(-maxDistance, maxDistance), transform.position.y + Random.Range(-maxDistance, maxDistance));
    }

    
}
