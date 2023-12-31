using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    public GameObject projectileParticle;

    // Start is called before the first frame update
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (transform.position.magnitude > 1000.0f)
        {
            Destroy(gameObject);
        }
    }

    public void Launch(Vector2 direction, float force)
    {
        rigidbody2d.AddForce(direction * force);
    }
    void OnCollisionEnter2D(Collision2D other)
    {

        GameObject projectileEffect = Instantiate(projectileParticle, transform.position, transform.rotation *= Quaternion.Euler(-90, 0, 0));
        EnemyController e = other.collider.GetComponent<EnemyController>();
        AutoManiacScript am = other.collider.GetComponent<AutoManiacScript>(); //CHANGE MADE BY: Cory Smith

        if (e != null)
        {
            e.Fix();
        }

        if (am != null) //CHANGE MADE BY: Cory Smith
        {
            AutoManiacScript.automaniacHealth -= 1;

            if(AutoManiacScript.automaniacHealth == 0)
            {
                am.AMFix();
            }
        }

        Destroy(gameObject);
    }
}
