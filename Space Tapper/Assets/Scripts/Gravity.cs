using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerControl player;
    public KeyCode gravityButton = KeyCode.Z;
    private bool top;

    void Start()
    {
        player = GetComponent<PlayerControl>();
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        if (Input.GetKeyDown(gravityButton))
        {
            rb.gravityScale *= -1;
            Ratate();
        }
    }

    void Ratate()
    {
        if (top == false)
        {
            transform.eulerAngles = new Vector3(0, 0, 180);
        }
        else
        {
            transform.eulerAngles = Vector3.zero;
        }

        player.isFacingRight = !player.isFacingRight;
        top = !top;
    }

}
