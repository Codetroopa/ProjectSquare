using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovement : MonoBehaviour {

	public float Speed = 15f;
	public float JumpForce = 3000f;
	public LayerMask Layers;				// What is considered ground
	public Transform TopLeft;				// The top left of the player
	public Transform BottomRight;			// The bottom right of the player

	public bool grounded;
	private bool doubleJump;
	private bool canDoubleJump;
	private bool jump;
	private bool falling;
	private bool checkMaxVel;
	public float yVel;
	public float maxYVel;
	public float jumpScale = 0.5f;

	private Rigidbody2D playerRB;
	private Animator anim;
	private Obstacle obs;

	void Awake () {
		playerRB = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
	}

	
	void Start () {
		//Obstacle.SpawnPillar(transform);
		obs = Obstacle.obs;
	}


	void Update () {
		// Velocities
		playerRB.velocity = new Vector2 (Speed, playerRB.velocity.y);
		yVel = playerRB.velocity.y;
		jumpScale = 0.5f + ((((23.019f - yVel) / 2f) / 23.019f) * 1.25f);


		// Is the Player falling?
		falling = playerRB.velocity.y < -0.1f;

		// Grounded?
		grounded = Physics2D.OverlapArea(TopLeft.position, BottomRight.position, Layers);

		if (!jump && grounded) {
			jump = CrossPlatformInputManager.GetButtonDown("Jump");
		}
		if (canDoubleJump && !grounded) {
			doubleJump = CrossPlatformInputManager.GetButtonDown("Jump");
		}

		// Animator
		if (grounded) {
			anim.SetBool("Grounded", true);
		} else {
			anim.SetBool("Grounded", false);
		}
		if (falling) {
			anim.SetBool("Falling", true);
		} else {
			anim.SetBool("Falling", false);
		}

		// Moves the character
		Move();
	}


	void Move (){
		// Max Velocity after jumping
		if (checkMaxVel) {
			maxYVel = playerRB.velocity.y;
			checkMaxVel = false;
		}

		// Jump Force
		if (jump && grounded) {
			canDoubleJump = true;
			playerRB.AddForce(new Vector2(0f, JumpForce));
			checkMaxVel = true;
			jump = false;
		}
		// Double Jump
		if (!grounded && doubleJump) {
			playerRB.AddForce(new Vector2(0f, JumpForce * jumpScale));
			canDoubleJump = false;
			doubleJump = false;
		}
	}
}
