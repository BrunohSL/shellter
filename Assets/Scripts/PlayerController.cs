using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Animator anim;

    private bool grounded;
    private bool doubleJumpPowerUpGot;
    private bool doubleJumped;
    private bool isOverTrashCan;
    private float chargeTimeCounter;
    private float moveSpeed;
    private float moveVelocity;

    public Transform groundCheck; //verificador de chao
    public LayerMask whatIsGround;
    public Transform firePoint;
    public GameObject ninjaStar;
    public EnemyController enemy;

    public float moveSpeedFixo;
    public float jumpHeight;
    public float groundCheckRadius;
    public float chargeTime;
    public float knockbackForce;
    public float knockbackDuration;
    public float knockbackCount { get; set; } //isso é um negócio loko que implementa uma property pública com uma variável privada "subentendida"
    public int damageToGive;
    public bool isLoaded;
    public bool playerPressingDown;
    public bool facingRight;
    public bool knockFromRight { get; set; } //ídem

    void Start () {
        anim = GetComponent<Animator>();
        facingRight = true;
        isLoaded = false;
        playerPressingDown = false;
        moveSpeed = moveSpeedFixo;
    }

    void FixedUpdate () {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

	void Update () {
        anim.SetBool("Grounded", grounded);
        // jump action
        if (Input.GetKeyDown(KeyCode.Space) && grounded) {
            jump();
        }


        moveVelocity = 0f;
        // right move
        if (Input.GetKey(KeyCode.RightArrow)) {
            // GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
            moveVelocity = moveSpeed;
        }
        // left move
        if (Input.GetKey(KeyCode.LeftArrow)) {
           // GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
            moveVelocity = -moveSpeed;
        }

        //tratamento de knockback: em cada quadro verifica o contador para tomar knockback apenas quando o player não estiver no meio de um knockback
        //a partir do primeiro else ele controla o acontecimendo e a direção do knockback
        // knoockback action
        if (knockbackCount <= 0) {
            GetComponent<Rigidbody2D>().velocity = new Vector2(moveVelocity, GetComponent<Rigidbody2D>().velocity.y);
        } else {
            if (knockFromRight) {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-knockbackForce, knockbackForce);
            } else {
                GetComponent<Rigidbody2D>().velocity = new Vector2(knockbackForce, knockbackForce);
            }
            knockbackCount -= Time.deltaTime;
        }

        anim.SetFloat("Speed", Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));

        if (GetComponent<Rigidbody2D>().velocity.x > 0) {
            transform.localScale = new Vector3(1f, 1f, 1f);
            facingRight = true;
        } else if (GetComponent<Rigidbody2D>().velocity.x < 0) {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            facingRight = false;
        }

        // attack action
        if (Input.GetKeyDown(KeyCode.Z) && isLoaded) {
            anim.SetBool("Spiting", true);
            moveSpeed = 0;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            playerPressingDown = true;
            anim.SetBool("Hidden", true);
            moveSpeed = 0;
        }

        if (Input.GetKeyUp(KeyCode.DownArrow)) {
            playerPressingDown = false;
            anim.SetBool("Hidden", false);
            moveSpeed = moveSpeedFixo;
        }

        if (isOverTrashCan && Input.GetKeyDown(KeyCode.Z)) {
            isLoaded = true;
        }


        if (Input.GetKeyDown(KeyCode.Z)) {
            anim.SetBool("Biting", true);
            moveSpeed = 0;

            if (isOverTrashCan) {
                isLoaded = true;
            }
        }
    }

    void endOfBite(string message) {
        if (message.Equals("attack end")) {
            if (isLoaded) {
                anim.SetBool("Charged", true);
            }

            anim.SetBool("Biting", false);
            moveSpeed = moveSpeedFixo;
        }
    }

    void endOfSpit(string message) {
        if (message.Equals("spit end")) {
            if (isLoaded) {
                GameObject ninjaStarInstance = Instantiate(ninjaStar, firePoint.position, firePoint.rotation);
                AudioSource.PlayClipAtPoint(ninjaStarInstance.GetComponent<AudioSource>().clip, ninjaStarInstance.GetComponent<Transform>().position);
                isLoaded = false;
                anim.SetBool("Charged", false);
                anim.SetBool("Spiting", false);
            }

            moveSpeed = moveSpeedFixo;
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.name == "Enemy") {
            var player = other.GetComponent<PlayerController>(); //tratamento do knockback começa aqui
            player.knockbackCount = player.knockbackDuration;//inicia contador

            //verifica se o player está a direita ou a esquerda
            if (other.transform.position.x < transform.position.x)
                player.knockFromRight = true;
            else
                player.knockFromRight = false;
        }

        if (other.tag == "TrashPile") {
            isOverTrashCan = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "TrashPile") {
            isOverTrashCan = false;
        }
    }

    public void jump() {
        if (!playerPressingDown) {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
            GetComponents<AudioSource>()[1].Play();
        }

    }
}
