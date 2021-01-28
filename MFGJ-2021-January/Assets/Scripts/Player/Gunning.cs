﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunning : MonoBehaviour
{
    GameManager gameManager;
    public float offset;

    public GameObject bulletPrefab;
    public GameObject rocketPrefab;
    public GameObject javelinPrefab;

    public GameObject rocketsUI;
    public GameObject javelinUI;

    public int rocketsAmmo = 0;
    public int javelinAmmo = 0;

    string selectedSpecial = "Rocket";

    public float bulletForce = 20f;
    public float specialForce = 800f;
    public Transform shotPoint;

    float timeBtwShots;
    float rocketCooldown;
    float javelinCooldown;

    public float startTimeBtwShots;
    public float startRocketCooldown;
    public float startJavelinCooldown;

    public PlayerController playerController;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        rocketCooldown = startRocketCooldown;
        javelinCooldown = startJavelinCooldown;
    }
    void Update()
    {
        if (!gameManager.IsGameOver && Time.timeScale != 0)
        {
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - shotPoint.position;
            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            UpdateShotPoint();
            shotPoint.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

            LeftClickListener();
            RightClickListener();

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                ChangeSpecial();
                //change ui here?
            }
        }
    }
    private void FixedUpdate()
    {
        if (timeBtwShots > 0)
        {
            timeBtwShots -= Time.deltaTime;
        }
        if (rocketCooldown > 0)
        {
            rocketCooldown -= Time.deltaTime;
        }
        if (javelinCooldown > 0)
        {
            javelinCooldown -= Time.deltaTime;
        }

        UpdateAmmoUI();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("RocketAmmo"))
        {
            rocketsAmmo += collision.GetComponent<Healing>().amount;
            Destroy(collision.gameObject);
            //play +RocketAmmo SFX
        }
        if (collision.CompareTag("JavelinAmmo"))
        {
            javelinAmmo += collision.GetComponent<Healing>().amount;
            Destroy(collision.gameObject);
            //play +JavelinAmmo SFX
        }
    }
    private void UpdateAmmoUI()
    {
        javelinUI.GetComponentInChildren<UnityEngine.UI.Text>().text = javelinAmmo.ToString();
        rocketsUI.GetComponentInChildren<UnityEngine.UI.Text>().text = rocketsAmmo.ToString();
    }

    private void RightClickListener()
    {
        switch (selectedSpecial)
        {
            case "Rocket":
                if (rocketCooldown <= 0 && Input.GetMouseButtonDown(1))
                {
                    if (rocketsAmmo >= 1)
                    {
                        RocketShoot();
                        rocketCooldown = startRocketCooldown;
                    }
                }
                break;

            case "Javelin":
                if (javelinCooldown <= 0 && Input.GetMouseButtonDown(1))
                {
                    if (javelinAmmo >= 1)
                    {
                        JavelinShoot();
                        javelinCooldown = startJavelinCooldown;
                    }
                }
                break;
            default:
                Debug.LogError("Gunning.cs Line 114: Switch case not exists");
                break;
        }
    }
    private void LeftClickListener()
    {
        if (timeBtwShots <= 0 && Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }
    public void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, shotPoint.position, shotPoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(shotPoint.up * bulletForce, ForceMode2D.Impulse);
    }
    public void RocketShoot()
    {
        GameObject special = Instantiate(rocketPrefab, shotPoint.position, shotPoint.rotation);
        Rigidbody2D rb = special.GetComponent<Rigidbody2D>();
        rb.AddForce(shotPoint.up * specialForce, ForceMode2D.Force); //for a bazooka rocket propeller.
        rocketsAmmo--;
    }
    public void JavelinShoot()
    {
        if (javelinAmmo >= 1)
        {
            GameObject special = Instantiate(javelinPrefab, shotPoint.position, shotPoint.rotation);
            Rigidbody2D rb = special.GetComponent<Rigidbody2D>();
            rb.AddForce(shotPoint.up * specialForce, ForceMode2D.Force);
            javelinAmmo--;
        }
    }
    private void ChangeSpecial()
    {
        //when change button was pressed:
        switch (selectedSpecial)
        {
            case "Rocket":
                selectedSpecial = "Javelin";

                javelinUI.SetActive(true);
                rocketsUI.SetActive(false);

                break;
            case "Javelin":
                selectedSpecial = "Rocket";

                javelinUI.SetActive(false);
                rocketsUI.SetActive(true);

                break;
            default:
                Debug.LogError("Gunning.cs Line 111: Switch case not exists");
                break;
        }
    }

    public void UpdateShotPoint()
    {
        if (playerController.isFacingRight)
        {
            if (shotPoint.localPosition.x < 0)
            {
                Vector3 pos = shotPoint.localPosition;
                pos.x *= -1;
                shotPoint.localPosition = pos;
            }
            if (shotPoint.localScale.x < 0)
            {
                Vector3 scale = shotPoint.localScale;
                scale.x *= -1;
                shotPoint.localScale = scale;
            }
        }
        else if (playerController.isFacingLeft)
        {
            if (shotPoint.localPosition.x > 0)
            {
                Vector3 pos = shotPoint.localPosition;
                pos.x *= -1;
                shotPoint.localPosition = pos;
            }
            if (shotPoint.localScale.x > 0)
            {
                Vector3 scale = shotPoint.localScale;
                scale.x *= -1;
                shotPoint.localScale = scale;
            }
        }
    }

}
