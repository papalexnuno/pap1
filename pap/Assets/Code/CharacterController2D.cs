using UnityEngine;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
    //Quantidade de força utilizada quando o player salta
    [SerializeField] private float ForcaSalto = 400f;
    //Quanto suaviza o movimento
    [Range(0, .3f)] [SerializeField] private float MoveSuave = .05f;
    //Controlar o Player no ar
    [SerializeField] private bool m_AirControl = true;
    //Determina o que e chao para o Player
    [SerializeField] private LayerMask m_WhatIsGround;
    //Posiçao que marca onde ver se o Player esta a tocar no chao ou nao
    [SerializeField] private Transform m_GroundCheck;
    //Posiçao que procura se existe algum teto
    [SerializeField] private Transform m_CeilingCheck;
    //Raio que determina se o Player esta a tocar no chao
    const float k_GroundedRadius = .2f;
    //Booleano se o Player esta ou nao a tocar no chao
    private bool NoChao;
    //Raio que determina se o Player se pode levantar
    const float k_CeilingRadius = .2f;  
	private Rigidbody2D m_Rigidbody2D;
    //Ver para qual lado o Player esta virado
    private bool m_FacingRight = true; 
	private Vector3 m_Velocity = Vector3.zero;

	[Header("Events")]
	[Space]

    //Cria evento para quando esta no chao 
	public UnityEvent OnLandEvent;

    //Usa o componente rigidbody quando está no chao
	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();

		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();
	}

	private void FixedUpdate()
	{
		bool wasGrounded = NoChao;
        NoChao = false;

		//O Player deteta que esta no chao quando o elemento que faz contacto tiver no chao
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
                NoChao = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}
	}

	public void Move(float move, bool x, bool jump)
	{

		//So possibilita o controlo do player se ele tiver no chao ou com Air Control ativado
		if (NoChao || m_AirControl)
		{
			// Move a personagem indentificando-o como alvo de velocidade
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			//E suaviza o movimento da personagem
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, MoveSuave);

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
		if (NoChao && jump)
		{
            //adiciona uma força vertical
            NoChao = false;
			m_Rigidbody2D.AddForce(new Vector2(0f, ForcaSalto));
		}
	}

	private void Flip()
	{
		//maneira como o player esta virado para os lados
		m_FacingRight = !m_FacingRight;

        transform.Rotate(0f, 180f, 0f);
	}
}