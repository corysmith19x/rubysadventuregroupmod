using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoManiacScript : MonoBehaviour
{
    public float moveSpeed;
    public bool vertical;
    public float changeTime = 4.0f;

    Animator animator;

    new Rigidbody2D rigidbody2D;
    float timer;
    int direction = 1;

    public ParticleSystem smokeEffect;

    public AudioSource audioSourceEnemy;
    public AudioClip enemyFixSound;
    public AudioClip robotWalkSound;

    bool broken;

    public static int automaniacHealth = 3;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
        audioSourceEnemy = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }

        if (!broken)
        {
            return;
        }
    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2D.position;

        if (!audioSourceEnemy.isPlaying)
        {
            audioSourceEnemy.PlayOneShot(robotWalkSound);

        }
        if (vertical)
        {
            //animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
            position.y = position.y + Time.deltaTime * moveSpeed * direction; ;
        }
        else
        {
            //animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
            //position.x = position.x + Time.deltaTime * moveSpeed * direction; ;
        }

        rigidbody2D.MovePosition(position);

        if (!broken)
        {
            return;
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();

        if (player != null)
        {
            player.ChangeHealth(-2);
        }
    }
    public void AMFix()
    {
        if (automaniacHealth == 0)
        {
            ScoreScript.scoreValue += 1;
            audioSourceEnemy.PlayOneShot(enemyFixSound);
            broken = false;
            rigidbody2D.simulated = false;
            animator.SetTrigger("Fixed");
            smokeEffect.Stop();
        }

    }
    public void PlaySound(AudioClip clip)
    {
        audioSourceEnemy.PlayOneShot(clip);
    }
}
