using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RubyController : MonoBehaviour
{
    public float speed = 3.0f;

    public int maxHealth = 5;
    public float timeInvincible = 2.0f;

    bool isInvincible;
    float invincibleTimer;

    public int health { get { return currentHealth; } }
    int currentHealth;

    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;

    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);

    public GameObject projectilePrefab;

    AudioSource audioSource;
    public AudioClip footstepsSound;
    public AudioClip shootingSound;
    public AudioClip hitSound;

    public bool canMove = true;
    public bool isHurt;

    public GameObject hurtParticle;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        currentHealth = maxHealth;

        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();

            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(footstepsSound);

            }
        }

        

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        if (!canMove)
        {
            horizontal = 0;
            vertical = 0;
            animator.SetFloat("Look X", 0);
            animator.SetFloat("Look Y", 0);
            animator.SetFloat("Speed", 0);
        }

        if (canMove && isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                
                isInvincible = false;
                
        }

        if (Input.GetMouseButtonDown(0))
        {
            Launch();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
                if (character != null)
                {
                    character.DisplayDialog();
                }
            }
        }

        if (!canMove && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if(ScoreScript.scoreValue == 4)
        {
            Win();
        }
    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }
    public void ChangeHealth(int amount)
    {

        if (amount < 0)
        {

            animator.SetTrigger("Hit");

            if (isInvincible)
                return;

            isInvincible = true;
            invincibleTimer = timeInvincible;

            

            audioSource.PlayOneShot(hitSound);


        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);

        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);

        if (currentHealth == 0)
        {
            Death();
        }
    }
    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);

        audioSource.PlayOneShot(shootingSound);

        animator.SetTrigger("Launch");
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    void Death()
    {
        canMove = false;
        SceneManager.LoadScene("LoseScene");
        ScoreScript.scoreValue = 0;


    }
    public void Win()
    {
        SceneManager.LoadScene("WinScene");
        ScoreScript.scoreValue = 0;
    }

    public void ChangeSpeed()
    //CHANGE MADE BY: Gabriel Santana
    {
        speed += 1;
    }
}
