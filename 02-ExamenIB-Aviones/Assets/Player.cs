using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Sprite explosionSprite;
    private SpriteRenderer sr;

    [SerializeField] private float speed = 5f;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    [Range(0.1f, 2f)]
    [SerializeField] private float fireRate = 0.5f;
    
    private Rigidbody2D rb;
    private float mx;
    private float my;
    
    private float fireTimer;

    private Vector2 mousePos;
        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            sr = GetComponent<SpriteRenderer>();
        }
        private void Update()
        {
            mx = Input.GetAxis("Horizontal");
            my = Input.GetAxis("Vertical");
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            float angle = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x) * Mathf.Rad2Deg - 90f;
            transform.localRotation = Quaternion.Euler(0f, 0f, angle);

            if (Input.GetMouseButton(0) && fireTimer <= 0f)
            {
                Shoot();
                fireTimer = fireRate;
            }
            else
            {
                fireTimer -= Time.deltaTime;
            }
        }
        private void FixedUpdate()
        {
            rb.velocity = new Vector2(mx, my).normalized * speed;
        }

        private void Shoot()
        {
            Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);
        }

        public void Explode()
        {
            sr.sprite = explosionSprite;
            rb.velocity = Vector2.zero; // Detener movimiento
            enabled = false; // Opcional: desactiva este script
            Time.timeScale = 0f; // Detiene el juego
        }
}