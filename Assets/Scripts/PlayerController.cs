using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Pause Game
    private bool isPause;

    private bool isFacingRight = true;
    private bool isClimbing = false;
    private bool doubleJump;
    private bool isAttack;

    private float speed = 4f;
    private float speedClimb = 5f;
    private float jumpPower = 14f;
    private float horizontal;
    private float vertical;
    private float gravity;
    //Attack
    private float timeShowAttack = 0.3f;
    private float attackInterval = 0.5f;
    //Attack weapon
    private bool isDragging = false;
    private Vector3 mousePosition;
    private float lastShootTime = 0f;
    private float shootInterval = 0.5f;
    //
    [SerializeField] private LayerMask ground;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator ani;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private GameObject shootLocation;
    [SerializeField] private GameObject playerAttack;

    void Start()
    {
        gravity = rb.gravityScale;
    }

    void Update()
    {
        if(isPause) return;
        
        Move();
        Animations();
        Attack();
        ShootWeapon();
    }

    private Vector3 GetMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.forward, 0);

        float distance;
        if (plane.Raycast(ray, out distance))
        {
            Vector3 worldPosition = ray.GetPoint(distance);
            return worldPosition;
        }

        return Vector3.zero; 
    }

    private void ShootWeapon()
    {
        if(isPause) return;
        if(DataManager.Instance.playerWeapon <= 0) return;
        if(Time.time - lastShootTime < shootInterval) return;
        
        mousePosition = GetMousePosition() ;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            SoundManager.Instance.PlaySFX(SoundManager.Instance.napDan);
            isDragging = true;
            shootLocation.gameObject.SetActive(true);
        }

        if (isDragging)
        {
            
            Vector2 direction = mousePosition - gameObject.transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            if (!isFacingRight)
            {
                angle = 180 + angle;
            }
            shootLocation.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {   

            SoundManager.Instance.PlaySFX(SoundManager.Instance.shoots[Random.Range(0, SoundManager.Instance.shoots.Count)]);
            isDragging = false;
            ani.SetTrigger("isAttack");
            lastShootTime = Time.time;
            PlayerWeaponController playerWeaponController = GetComponent<PlayerWeaponController>();
            playerWeaponController.ShootWeapon();
            shootLocation.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            shootLocation.gameObject.SetActive(false);
        }
    }

    private void Attack()
    {
        if(isPause) return;

        if (Input.GetMouseButtonDown(1) && !isAttack)
        {
            StartCoroutine(HandleAttack());
        }
    }
    private IEnumerator HandleAttack()
    {
        isAttack = true;
        ani.SetTrigger("isAttack");
        playerAttack.SetActive(true);
        SoundManager.Instance.PlaySFX(SoundManager.Instance.attack1);

        yield return new WaitForSeconds(timeShowAttack);

        playerAttack.SetActive(false);
        isAttack = false;

        yield return new WaitForSeconds(attackInterval - timeShowAttack);
    }


    public void Animations()
    {
        if(isPause) return;
        if (horizontal == 0)
        {
            ani.SetBool("isRun", false);
        }

        if (rb.velocity.y == 0)
        {
            ani.SetBool("isJump", false);
        }
        else 
        {
            //ani.SetBool("isJump", true);
        }

        if (Mathf.Abs(horizontal) > 0 && !Input.GetKeyDown(KeyCode.W))
        {
            ani.SetBool("isRun", true);
        }
        else
        {
            ani.SetBool("isRun", false);
        }
    }

    public void Move()
    {
        if(isPause) return;
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            Flip();
        }
        horizontal = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        
        if (isClimbing)
        {
            vertical = Input.GetAxis("Vertical");
            rb.velocity = new Vector2(rb.velocity.x, vertical * speedClimb);
           
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (IsGrounded() || doubleJump)
                {
                    doubleJump = !doubleJump;
                    rb.velocity = new Vector2(rb.velocity.x, jumpPower);
                    ani.SetBool("isJump", true);
                    SoundManager.Instance.PlaySFX(SoundManager.Instance.jump);
                }
            }

            if (Input.GetKeyUp(KeyCode.W) && rb.velocity.y > 0f)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            }
        }

        if (IsGrounded() && !Input.GetKey(KeyCode.W))
        {
            doubleJump = false;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, ground);
    }

    public void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isClimbing = true;
            rb.gravityScale = 0f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            isClimbing = false;
            rb.gravityScale = gravity;
        }
    }

    public bool IsFacingRight()
    {
        return isFacingRight;
    }

    public void SetPause(bool pause)
    {
        isPause = pause;
    }
    
}
