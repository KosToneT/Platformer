using UnityEngine;

public class Gravity : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D body;
    private PlayerControl player;

    [Header("Movement Variables")]
    public KeyCode gravityButton = KeyCode.V;
    private bool top;

    void Start()
    {
        player = GetComponent<PlayerControl>();
        body = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        if (Input.GetKeyDown(gravityButton))
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
