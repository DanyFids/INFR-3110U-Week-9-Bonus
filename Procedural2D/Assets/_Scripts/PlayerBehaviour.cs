using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    public bool isGrounded;
    private Rigidbody2D m_rigidBody2D;
    private SpriteRenderer m_spriteRenderer;
    private Animator m_animator;

	public float horizontalForce;
	public float verticalForce;

	private Vector2 Force;

    // Start is called before the first frame update
    void Start()
    {
        m_rigidBody2D = GetComponent<Rigidbody2D>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
		if (Force.magnitude > 0)
			m_rigidBody2D.AddForce(Force);
		else
			m_rigidBody2D.velocity = new Vector2(0.0f, m_rigidBody2D.velocity.y);

		if (Mathf.Approximately(m_rigidBody2D.velocity.x, 0.0f) || !isGrounded)
		{
			m_animator.SetInteger("AnimState", 0);
		}
		else if (isGrounded)
		{
			m_animator.SetInteger("AnimState", 1);
		}

    }

	public void OnMove(InputAction.CallbackContext context)
	{
		float dir = context.ReadValue<float>();

		if (dir > 0)
			m_spriteRenderer.flipX = false;
		else if (dir < 0)
			m_spriteRenderer.flipX = true;

		Force = Vector2.right * dir * horizontalForce;
	}

	public void OnJump(InputAction.CallbackContext context)
	{
		if (isGrounded)
		{
			m_rigidBody2D.AddForce(Vector2.up * verticalForce);
			isGrounded = false;
		}

		//Debug.Log("HOI!!!");
	}

	public void OnCollisionEnter2D(Collision2D other)
    {
        isGrounded = true;
    }

    public void OnCollisionExit2D(Collision2D other)
    {
        isGrounded = false;
    }
}
