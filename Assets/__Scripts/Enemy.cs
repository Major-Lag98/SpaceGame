using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoundsCheck))]
public class Enemy : MonoBehaviour
{
    [Header("inscribed")]
    public float speed = 10f;
    public float fireRate = 0.3f;
    public int MaxHealth = 1;
    public int health = 1;
    public int score = 100;
    public ParticleSystem burstCollision;

    public MeshRenderer[] myRenderers;

    private BoundsCheck bndCheck;
    public Vector3 pos
    {
        get
        {
            return this.transform.position;
        }
        set
        {
            this.transform.position = value;
        }
    }
    // Start is called before the first frame update
    void Awake()
    {
        burstCollision.Play();
        var em = burstCollision.emission;
        em.enabled = false;
        bndCheck = GetComponent<BoundsCheck>();
    }
    private void Start()
    {
        health = MaxHealth;
    }

    public void ChangeColor(Color color)
    {
        foreach (Renderer rend in myRenderers)
        {
            
            rend.material.color = color;
        }
    }

    public virtual void Move()
    {
        Vector3 tempPos = pos;
        tempPos.y -= speed * Time.deltaTime;
        pos = tempPos;
    }
    // Update is called once per frame
    void Update()
    {
        Move();
        if(!bndCheck.isOnScreen)
        {
            if(pos.y < bndCheck.camHeight - bndCheck.radius)
            {
                if (!GameManager.Instance.lost)
                {
                    GameManager.Instance.EarthTakeDamage();
                }
                
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject otherGO = collision.gameObject;
        if(otherGO.GetComponent<ProjectileHero>() != null)
        {
            Destroy(otherGO);

            //Debug.Log($"Health = {health}");
            health -= 1;
            //Debug.Log($"Health = {health}");
            if (health <= 0)
            {
                
                Invoke(nameof(DestroyEnemy), 0);
            }
            
        }
        else
        {
            //Debug.Log("Enemy Hit by Non Projectile Hero " + otherGO.name);
        }
    }

    public void DestroyEnemy()
    {
        //print("Destroy called...");

        GameManager.Instance.AddScore(MaxHealth);

        var em = burstCollision.emission;
        em.enabled = true;
        var dur = burstCollision.main.duration;
        burstCollision.Play();
        burstCollision.transform.parent = null;
        Destroy(burstCollision.gameObject, burstCollision.main.duration);
        Destroy(gameObject);
    }


}
