using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ICharMov : MonoBehaviour
{
    internal Rigidbody2D rb2D;
    protected Transform tr;
    protected CanvasMod canvasMod;
    internal Animator animator;

    public delegate void OnDeathCharacter();
    public event OnDeathCharacter eventDeath;

    protected float Speed = 5.5f;
    [HideInInspector]
    public int Condition =-1;
    [HideInInspector]
    public float clampVelocity = 12f;
    internal bool isMoveX = true;
    protected float positionZ = 0;
    protected bool isFall = false;
    protected bool isPause = false;
    [HideInInspector]
    public float scores;

    private bool isStay=false;
    private float ifStayCount = 0;

    private bool isDeath = false;
    private void Awake()
    {
        CanvasMod.charMov = this;
    }
    private void Start()
    {
        Application.targetFrameRate = 60;
        tr = transform;
        rb2D = GetComponent<Rigidbody2D>();
        canvasMod = GameObject.FindGameObjectWithTag("Canvas").GetComponent<CanvasMod>();
        animator = GetComponent<Animator>();

        CanvasMod.eventContinue += Continue;

        CanvasMod.eventPause += Pause;
    }

    public void Jump(float force)
    {
        if (Condition !=-1)
        {
            animator.SetBool("isJump", true);
            animator.Play("Jump");
            rb2D.velocity = Vector3.zero;
            rb2D.AddForce(new Vector2(0, 1) * force);
        }
    }
    internal virtual void FixedUpdate()
    {
        //Debug.Log("POints1 - " + SaveLoad.savePoints.points1 + "BUILD INDX - " + UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);

        if (isMoveX) MoveX(); // Нужно что бы перемещать влево - вправо
        if (Condition == 1)
            OnCondition_1();
        if (Input.GetMouseButton(0) && Condition == 0)
            StartJump();
    }
    void MoveX()
    {
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        /*if (Mathf.Abs(objPosition.x - tr.position.x) > 0.1)
        {
            if (Mathf.Abs(objPosition.x - tr.position.x) > 2)
                tr.Translate(new Vector2(objPosition.x - tr.position.x, 0) * Time.fixedDeltaTime * Speed);
            else
                tr.Translate(new Vector2(objPosition.x - tr.position.x, 0) * Time.fixedDeltaTime * Speed * 2.2f);
            //rb2D.velocity = new Vector2(objPosition.x - tr.position.x, 0) * Speed;
        }*/
        transform.position = Vector3.Slerp(tr.position, new Vector3(objPosition.x ,tr.position.y, positionZ), Time.fixedDeltaTime*8f);
    }
    void CheckPlatform()
    {
        RaycastHit2D hitUp = Physics2D.Raycast(new Vector2(tr.position.x + 0.15f, tr.position.y + 1.2f), tr.up, 1f);
        RaycastHit2D hitUp1 = Physics2D.Raycast(new Vector2(tr.position.x - 0.15f, tr.position.y + 1.2f), tr.up, 1f);
        if (hitUp.collider != null)
        {
            if (!hitUp.collider.CompareTag("Player") && hitUp.collider.CompareTag("Platforms")) hitUp.collider.isTrigger = true;
        }
        if (hitUp1.collider != null)
        {
            if (!hitUp1.collider.CompareTag("Player") && hitUp1.collider.CompareTag("Platforms")) hitUp1.collider.isTrigger = true;
        }
        RaycastHit2D hitDown = Physics2D.Raycast(new Vector2(tr.position.x + 0.15f, tr.position.y), -tr.up, 1f);
        RaycastHit2D hitDown1 = Physics2D.Raycast(new Vector2(tr.position.x - 0.15f, tr.position.y), -tr.up, 1f);
        RaycastHit2D hitDown2 = Physics2D.Raycast(new Vector2(tr.position.x, tr.position.y), -tr.up, 1f);
        if (hitDown.collider != null)
        {
            if (!hitDown.collider.CompareTag("Player") && hitDown.collider.CompareTag("Platforms")) hitDown.collider.isTrigger = false;
        }
        if (hitDown1.collider != null)
        {
            if (!hitDown1.collider.CompareTag("Player") && hitDown1.collider.CompareTag("Platforms")) hitDown1.collider.isTrigger = false;
        }
        if (hitDown2.collider != null)
        {
            if (!hitDown2.collider.CompareTag("Player") && hitDown2.collider.CompareTag("Platforms")) hitDown2.collider.isTrigger = false;
        }

       // Debug.DrawRay(new Vector2(tr.position.x + 0.15f, tr.position.y + 1.2f), tr.up, Color.red, 5f);
       // Debug.DrawRay(new Vector2(tr.position.x - 0.15f, tr.position.y + 1.2f), tr.up, Color.red , 5f);
    }
    internal virtual void OnCondition_1()
    {
        CheckPlatform(); // Нужно бы чекнуть платформы
        if (rb2D.velocity.y > clampVelocity)
        {
            rb2D.velocity = Vector3.Lerp(rb2D.velocity, new Vector3(rb2D.velocity.x, clampVelocity, 0), rb2D.velocity.y - clampVelocity);
        }
        if (scores < tr.position.y)
            scores = tr.position.y;
        if (rb2D.velocity.y < 0f)
        {  isFall = true; }
        else
            isFall = false;
        animator.SetBool("Fall", isFall);
        WhenPlayLandCharacter();
    }
    void StartJump()
    {
        Condition = 1;
        Jump(1000f);
        CameraMovement.isWithDampningMotion = 0;
    }
    /*private void OnCollisionStay2D(Collision2D collision)
    {
        if (rb2D.velocity.y == 0 && collision.gameObject.CompareTag("Platforms"))            //костыль для бага застывания на платформе
        {
            Jump(1000f);
        }
    }*/
   
    private void OnCollisionStay2D(Collision2D collision)
    {
        //Debug.Log(isStay + "   "+ ifStayCount);
        if(collision.gameObject.CompareTag("Platforms"))
        {
            isStay = true;
            ifStayCount += 1;
            if(ifStayCount >=8)
            {
                Jump(1000f);
                clampVelocity = 10f;            // прыгает на 2 юнита
                collision.gameObject.GetComponent<IPlatforms>().OnCollision(gameObject);
            }
        }
        else
        {
            isStay = false;
            ifStayCount = 0;
        }
    }
    internal virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if ((Condition == 1 || Condition == 0) && collision.gameObject.CompareTag("BasePlatform"))
        {
            animator.SetBool("isJump", false);
            Condition = 0;
            if (scores > 5)
                OnDeath("BasePlatform so fell!");
        }
        if (collision.gameObject.CompareTag("Platforms") && rb2D.velocity.y <=0)  // Спавн частиц под нами
        {
            Jump(1000f);
            clampVelocity = 10f;            // прыгает на 2 юнита
            collision.gameObject.GetComponent<IPlatforms>().OnCollision(gameObject);
            ifStayCount = 0;
        }
    }   //чек смерти пока что такой
    internal void OnDeath(string fromDanger)//здесь менюшка вылазиит
    {
        if (!isDeath)
        {
           //print("Loos due to: "+fromDanger);
            isDeath = true;
            Condition = -1;
            isMoveX = false;
            CameraMovement.isWithDampningMotion = 2;
            animator.SetBool("Loos", true);
            animator.Play("Loos");
            Save();
            eventDeath?.Invoke();
            rb2D.simulated = false;
        }
    }
    internal abstract void Save();
    public virtual void Pause()
    {
        /* rb2D.gravityScale = 0 ;
         rb2D.velocity = Vector2.zero;*/
        rb2D.simulated = false;
        isMoveX = false;
        isPause = true;
    }
    public virtual void Continue()
    {
        rb2D.simulated = true;
        isMoveX = true;
        isPause = false;
    }
    public virtual void ResetValueBeforeStart()
    {
        tr.position = new Vector3(0, 1, positionZ);
        isPause = false;
        Condition = -1;
        scores = 0;
        CameraMovement.cameraMovement.gameObject.transform.position = new Vector3(0, -3.5f, -10);
        CameraMovement.isWithDampningMotion = 2;
        rb2D.simulated = true;
        animator.SetBool("Loos", false);
        animator.SetBool("isJump", false);
        animator.Play("Stand");
        isMoveX = true;
        isDeath = false;
    }
    void WhenPlayLandCharacter()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(tr.position.x, tr.position.y - 0.1f), -tr.up, 1f);

        if (hit.collider != null)
        {
            float value = Mathf.Abs(Mathf.Abs( tr.position.y) - Mathf.Abs(hit.collider.transform.position.y));
            if (rb2D.velocity.y<0.2f && value < 0.5f)
            {
                animator.SetBool("Land", true);
            }
            else
            {
                animator.SetBool("Land", false);
            }
        }
    }
    
}
