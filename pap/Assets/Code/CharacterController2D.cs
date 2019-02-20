using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 400f;							//Quantidade de força utilizada quando o player salta
	[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;			//Velocidade máxima no crouch
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	//Quanto suaviza o movimento
	[SerializeField] private bool m_AirControl = true;							//Controlar o Player no ar
	[SerializeField] private LayerMask m_WhatIsGround;							//Determina o que e chao para o Player
	[SerializeField] private Transform m_GroundCheck;							//Posiçao que marca onde ver se o Player esta a tocar no chao ou nao
	[SerializeField] private Transform m_CeilingCheck;							//Posiçao que procura se existe algum teto
	[SerializeField] private Collider2D m_CrouchDisableCollider;				//Desativa colisao quando em crouch

	const float k_GroundedRadius = .2f; //Raio que determina se o Player esta a tocar no chao
	private bool m_Grounded;            //Booleano se o Player esta ou nao a tocar no chao
	const float k_CeilingRadius = .2f;  //Raio que determina se o Player se pode levantar
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  //Ver para qual lado o Player esta virado
	private Vector3 m_Velocity = Vector3.zero;

	[Header("Events")]
	[Space]

    //Cria evento para quando esta no chao 
	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

    //Cria evento booleano para o crouch
	public BoolEvent OnCrouchEvent;
	private bool m_wasCrouching = false;

    //Usa o componente rigidbody para o crouch
	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();
	}

	private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		//O Player deteta que esta no chao quando o elemento que faz contacto tiver no chao
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}
	}

	public void Move(float move, bool crouch, bool jump)
	{
		// Se estiver em crouch, verifica se o Player se pode levantar
		if (!crouch)
		{
			//Se existir um teto o player está impedido de se levantar
			if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
			{
				crouch = true;
			}
		}

		//So possibilita o controlo do player se ele tiver no chao ou com Air Control ativado
		if (m_Grounded || m_AirControl)
		{

			//Se em Crouch
			if (crouch)
			{
				if (!m_wasCrouching)
				{
					m_wasCrouching = true;
					OnCrouchEvent.Invoke(true);
				}

				//Reduz a velocidade
				move *= m_CrouchSpeed;

				//Desativa um dos colliders quando em crouch
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = false;
			} else
			{
				//Ativa o collider se nao tiver em crouch
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = true;

				if (m_wasCrouching)
				{
					m_wasCrouching = false;
					OnCrouchEvent.Invoke(false);
				}
			}

			// Move a personagem indentificando-o como alvo de velocidade
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			//E suaviza o movimento da personagem
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

			// Se o input for de movimento para a direita e o player estiver virado para a direita
			if (move > 0 && !m_FacingRight)
			{
				//Vira o player
				Flip();
			}
			// Se o input for para a esquerda e o player estiver para a direita
			else if (move < 0 && m_FacingRight)
			{
				//Vira o player
				Flip();
			}
		}
		//Se o player saltar
		if (m_Grounded && jump)
		{
			//adiciona uma força vertical
			m_Grounded = false;
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
		}
	}


	private void Flip()
	{
		//maneira como o player esta virado para os lados
		m_FacingRight = !m_FacingRight;

        transform.Rotate(0f, 180f, 0f);
	}
}
