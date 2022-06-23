using UnityEngine;

public class Collision : MonoBehaviour
{

    private PlayerControl pControl;
    [Header("Layers")]
    [SerializeField] private LayerMask groundLayer;

    [Space]

    public bool onGround;
    public bool onWall;
    public bool onRightWall;
    public bool onLeftWall;
    public int wallSide;

    [Space]

    [Header("Collision")]

    [SerializeField] private float collisionRadius = 0.25f;
    [SerializeField] private Vector2 bottomOffset, rightOffset, leftOffset;
    private Color debugCollisionColor = Color.red;

    private void Awake()
    {
        pControl = GetComponent<PlayerControl>();
    }

    void Update()
    {
        if (pControl.top)
        {
            onGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset * -1, collisionRadius, groundLayer);
        }
        else
        {
            onGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, groundLayer);
        }
        
        onWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer)
            || Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer);

        onRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer);
        onLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer);

        wallSide = onRightWall ? -1 : 1;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        var positions = new Vector2[] { bottomOffset, rightOffset, leftOffset };

        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, collisionRadius);
    }

    private void OnCollisionStay2D(Collision2D coll)
    {

    }

    private void OnCollisionEnter2D(Collision2D coll)
    {

    }

    private void OnCollisionExit2D(Collision2D coll)
    {

    }
}