using UnityEngine;

public class Gravity : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D body;
    private PlayerControl player;
    private bool top;

    private void Awake()
    {
        player = GetComponent<PlayerControl>();
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //if (Input.GetKeyDown())
        {
            body.gravityScale *= -1;
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
