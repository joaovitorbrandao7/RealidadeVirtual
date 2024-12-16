using UnityEngine;

public class Inimigo : MonoBehaviour
{
    private Animator animator;
    private Transform jogador; 
    public float alcanceDeVisao = 10f; 
    public float alcanceDeAtaque = 2f;
    public float velocidadeMovimento = 3f; 

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        animator = GetComponent<Animator>();

        jogador = GameObject.FindWithTag("Player").transform; 
    }

    void Update()
    {
        float distanciaParaOJogador = Vector3.Distance(transform.position, jogador.position);

        if (distanciaParaOJogador <= alcanceDeVisao)
        {
            MoveTowardsPlayer();

            if (distanciaParaOJogador <= alcanceDeAtaque)
            {
                LookAtPlayer();
                animator.SetTrigger("Attack"); 
            }
        }
        else
        {
            
        }
    }

    void MoveTowardsPlayer()
    {
        Vector3 direcao = (jogador.position - transform.position).normalized; 
        transform.Translate(direcao * velocidadeMovimento * Time.deltaTime, Space.World); 

        animator.SetTrigger("Run");
    }

    void LookAtPlayer()
    {
        Vector3 lookDirection = (jogador.position - transform.position).normalized; 
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(lookDirection.x, 0f, lookDirection.z)); 
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Espada"))
        {
            Morrer(); 
        }
    }

    public void Morrer()
    {
        animator.SetTrigger("Death");
        audioManager.PlaySFX(audioManager.caveiramorta);

        Destroy(gameObject, 3f); 
    }
}
