﻿using UnityEngine;

public class Enemi_patrol : MonoBehaviour
{
    public float speed;
    public Transform[] waypoints;
    private Transform target;
    private int destPoint = 0;

    public SpriteRenderer graphics;

    // Start is called before the first frame update
    void Start()
    {
        target = waypoints[0];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        
        if(Vector3.Distance(transform.position, target.position) < 0.3f)
        {
            destPoint = (destPoint + 1) % waypoints.Length;
            target = waypoints[destPoint];
            graphics.flipX = !graphics.flipX;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            playerHealth playerhealth = collision.transform.GetComponent<playerHealth>();
            playerhealth.takeDamage(20);
        }
    }
}
