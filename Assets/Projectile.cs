using System;
using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10;
    private Rigidbody rb;
    private Vector3 lastVelocity;

    public int reflectCount = 0;
    public int maxReflectCount = 1;
    public int points = 0;
    private int sessionPoints = 0;

    private bool isIgnoringPlayer = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        if (PlayerPrefs.HasKey("Score: "))
        {
            points = PlayerPrefs.GetInt("Score: ");
        }

        if (PlayerPrefs.HasKey("SessionScore"))
        {
            sessionPoints = PlayerPrefs.GetInt("SessionScore");
        }

    }

    void Update()
    {
        lastVelocity = rb.velocity;

        var direction = rb.velocity;
        direction.y = 0; 

        if (direction.y > 0.01f || direction.y < -0.01f)
        {
            Destroy(gameObject);
        }

        if (direction.magnitude > 0.01f)
        {
            transform.rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 90, 0); ;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject collidedObject = collision.gameObject;
        string collidedTag = collidedObject.tag;

        switch (collidedTag)
        {
            case "Scarabee":
                Destroy(collidedObject);
                Destroy(this.gameObject);
                break;

            case "Player":
                if (isIgnoringPlayer)
                {
                    return;
                }
                DestroyPlayer(collidedObject);
                break;

            case "Enemy":
                PointSystem(collidedObject,collidedObject.gameObject.name);
                DestroyEnemy(collidedObject);
                break;

            default:
                if (reflectCount >= maxReflectCount)
                {
                    Destroy(gameObject);
                    AudioManager.instance.PlaySFX("Scarabee raakt muur");
                }
                else
                {
                    BounceProjectile(collision);
                    AudioManager.instance.PlaySFX("Scarabee raakt muur");
                }

                break;
        }
    }

    void DestroyEnemy(GameObject enemy)
    {
        GameObject enemyParent = enemy.transform.parent?.gameObject;
        Destroy(enemyParent ?? enemy);
        Destroy(gameObject);
        AudioManager.instance.PlaySFX("Standbeeld neer");
    }

    void DestroyPlayer(GameObject player)
    {
        GameObject playerParent = player.transform.parent?.gameObject;
        Destroy(playerParent ?? player);
        Destroy(gameObject);
        AudioManager.instance.PlaySFX("Standbeeld neer");

        GameObject laser = GameObject.Find("Line");
        if (laser != null)
        {
            Destroy(laser);
        }
    }

    void BounceProjectile(Collision collision)
    {
        reflectCount++;

        var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);

        rb.velocity = direction * speed;
    }
    void PointSystem(GameObject collidedObject,String enemy) {
        
        if (enemy.Contains("sfinx",StringComparison.OrdinalIgnoreCase)) 
        {
            points += 1;
            sessionPoints += 1;
        }
        else if (enemy.Contains("horus", StringComparison.OrdinalIgnoreCase))
        {
            points += 2;
            sessionPoints += 2;
        }
        else if (enemy.Contains("ra", StringComparison.OrdinalIgnoreCase))
        {
            points += 3;
            sessionPoints += 3;
        }
        else if (enemy.Contains("bastet", StringComparison.OrdinalIgnoreCase))
        {
            points += 3;
            sessionPoints += 3;
        }
        else if (enemy.Contains("anubis", StringComparison.OrdinalIgnoreCase))
        {
            points += 4;
            sessionPoints += 4;
        }
        else if (enemy.Contains("sekhmet",StringComparison.OrdinalIgnoreCase))
        {
            points += 4;
            sessionPoints += 4;
        }

        PlayerPrefs.SetInt("SessionScore", sessionPoints);
        PlayerPrefs.Save();

        PlayerPrefs.SetInt("Score: " , points);
        PlayerPrefs.Save();
    }
}