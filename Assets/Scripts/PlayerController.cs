using UnityEngine;

public class PlayerController : MonoBehaviour {
	[SerializeField] private float m_JumpForce = 400f;                          // Amount of force added when the player jumps.
	[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;          // Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
	[SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings
	[SerializeField] private Collider2D m_CrouchDisableCollider;                // A collider that will be disabled when crouching

	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.
	const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 velocity = Vector3.zero;

	public Vector3 SpawnPoint = new Vector3(1, 1, 0);
	public TimerUI timer;

	private void Awake() {
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
	}


	private void FixedUpdate() {
		m_Grounded = false;

		// Check stick to wall
		// Gitanada Porque no se puede desactivar el flag del collider autodetectandose por el point and click al crear mapa
		// REVISAR
		RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, Vector2.left);
		WallJump(hit);
		Debug.Log(m_Grounded);
		hit = Physics2D.RaycastAll(transform.position, Vector2.right);
		WallJump(hit);

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		foreach(Collider2D objCol in colliders ) {
			if (objCol.gameObject != gameObject)
				m_Grounded = true;
		}

		if(ScoreScript.scoreValue < this.transform.position.x) {
			ScoreScript.scoreValue = this.transform.position.x;
		}

		if(this.transform.position.y < -15) {
			Die();
		}

	}

	public void WallJump(RaycastHit2D[] hits) {
		if(hits.Length > 0) {
			foreach (RaycastHit2D hit in hits) {
				if(hit.collider != null) {
					if(hit.collider.gameObject.tag == "Wall") {

						float distance = Mathf.Abs(hit.point.x - transform.position.x);
						if(distance < .5f) m_Grounded = true;
					}
				}
			}
		}
	}

	public void Move(float move, bool crouch, bool jump) {

		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl) {

			// If crouching
			if (crouch) {
				// Reduce the speed by the crouchSpeed multiplier
				move *= m_CrouchSpeed;

				// Disable one of the colliders when crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = false;
			} else {
				// Enable the collider when not crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = true;
			}

			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref velocity, m_MovementSmoothing);

			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !m_FacingRight) {
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight) {
				// ... flip the player.
				Flip();
			}
		}
		// If the player should jump...
		if (m_Grounded && jump) {
			// Add a vertical force to the player.
			m_Grounded = false;
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
		}
	}


	private void Flip() {
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	public void Die() {
		timer.RestartTimer();
		ScoreScript.scoreValue = 0;
		ScoreScript.deathsValue++;
		m_Grounded = false;
		
		m_Rigidbody2D.velocity = Vector3.zero; //Get Rigidbody and set velocity to (0f, 0f, 0f)
		Respawn();
	}

	public void SetSpawnPoint(Vector3 point) {
		SpawnPoint = point;
	}
	
	public void Respawn() {
		this.transform.position = SpawnPoint;
	}

	public void Win() {
		timer.StopTimer();
	}
}