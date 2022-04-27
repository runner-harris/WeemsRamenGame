
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float horizontalInput;
    private int count;
    
    [Header("SFX")]
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip pickupSound;
    [SerializeField] private AudioClip runSound;

    void Start()
    {
        count = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PickUp")
        {
            collision.gameObject.SetActive(false);
            count += 1;
            SoundManager.instance.PlaySound(pickupSound);
        }
    }
    private void Awake()
    {
        //Grab references for rigidBody and animator from object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        

        //Flip player when moving side to side
        if(horizontalInput > 0.01f)
        {
            transform.localScale = new Vector3(.007f, .007f, 3);
        }
        else if(horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-.007f, .007f, 3);
        }
        if(horizontalInput > 0 || horizontalInput < 0){
            if (Input.GetKeyDown(KeyCode.RightArrow) && isGrounded())
                SoundManager.instance.PlaySound(runSound);
            if (Input.GetKeyDown(KeyCode.LeftArrow) && isGrounded())
                SoundManager.instance.PlaySound(runSound);
        }
        
        //Set animator parameters, false means player is not running
        //Arrow keys are pressed means run is true, animation works
        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());

        //Wall jump logic
        if(wallJumpCooldown > 0.2f)
        {
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            if (onWall() && !isGrounded())
            {
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }
            else
                body.gravityScale = 7;

            if (Input.GetKey(KeyCode.Space))
            {
                Jump();
                if(Input.GetKeyDown(KeyCode.Space) && isGrounded())
                    SoundManager.instance.PlaySound(jumpSound);
            }
        }
        else
            wallJumpCooldown += Time.deltaTime;

        if(!Input.anyKey)
            SoundManager.instance.StopSound(runSound);

    }
    
    private void Jump()
    {
        if(isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            anim.SetTrigger("jump");
        }
        else if(onWall() && !isGrounded())
        {
            if(horizontalInput == 0)
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);

            }
            else
                wallJumpCooldown = 0;

            body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
        }

    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down,
            0.1f, groundLayer); 
        return raycastHit.collider != null;
    }
     private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0),
            0.1f, wallLayer); 
        return raycastHit.collider != null;
    }

    public bool canAttack()
    {
        return horizontalInput == 0 && !onWall();
    }

    void EndGame()
    {
        if(count >= 3)
            gameObject.SetActive(false);
    }
}
