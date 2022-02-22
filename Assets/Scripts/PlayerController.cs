using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movespeed = 130f;
    public float rotationspeed = 50f;
    public Rigidbody2D rb;

    public GameObject bullet;
    public GameModel model;

    private Vector2 movement;
    private int gaze = 0;

    private bool isShooted = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!model.isPlayerAlive)
        {
            return;
        }
        if (Input.GetKey("w"))
        {
            movement.y = 1;
        }
        else
        {
            if (Input.GetKey("s"))
            {
                movement.y = -1;
            }
            else
            {
                movement.y = 0;
            }
        }
        if (Input.GetKey("d"))
        {
            movement.x = 1;
        }
        else
        {
            if (Input.GetKey("a"))
            {
                movement.x = -1;
            }
            else
            {
                movement.x = 0;
            }
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            gaze = 1;
        }
        else
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                gaze = -1;
            }
            else
            {
                gaze = 0;
            }
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (!isShooted)
            {
                shoot();
                isShooted = true;
            }
        }
        else
        {
            if (isShooted)
            {
                isShooted = false;
            }
        }
    }

    private void shoot()
    {
        var bulletItem = Instantiate(bullet, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
        bulletItem.GetComponent<Rigidbody2D>().rotation = rb.rotation;
        bulletItem.GetComponent<BulletController>().SetModel(model, gameObject);
    }

    private void FixedUpdate()
    {
        if (!model.isPlayerAlive)
        {
            return;
        }
        //movement
        rb.MovePosition(rb.position + movement * movespeed * Time.fixedDeltaTime);
        rb.MoveRotation(rb.rotation + gaze * rotationspeed * Time.fixedDeltaTime);
    }
}
