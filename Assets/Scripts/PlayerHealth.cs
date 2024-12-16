using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    int maxVidas = 5; 
    private int vidasAtuais; 
    private bool isInvulnerable = false;
    public float tempoDeInvulnerabilidade = 2f; 
    public Text placarTexto; 
    public Image telaDano;
    public GameObject painelImortalidade;
    public Text imortalidadeTexto; 
    public Text cronometroTexto; 

    private Animator animator; 
    private Rigidbody rb; 

    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        vidasAtuais = 3; 
        AtualizarPlacar();

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        if (telaDano != null)
        {
            telaDano.color = new Color(1, 0, 0, 0); 
        }

        
        if (painelImortalidade != null)
        {
            painelImortalidade.SetActive(false); 
        }

        if (imortalidadeTexto != null)
        {
            imortalidadeTexto.text = ""; 
        }

        if (cronometroTexto != null)
        {
            cronometroTexto.text = ""; 
        }

        if (animator == null)
        {
            Debug.LogWarning("Animator não encontrado no objeto Player!");
        }
    }

    void Update()
    {
        if (animator != null)
        {
            if (rb.velocity.magnitude > 0.1f) 
            {
                animator.SetBool("IsMoving", true); 
            }
            else
            {
                animator.SetBool("IsMoving", false); 
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!isInvulnerable &&
            (collision.gameObject.CompareTag("Bola") ||
             collision.gameObject.CompareTag("Porta") ||
             collision.gameObject.CompareTag("Inimigo") ||
             collision.gameObject.CompareTag("Rotate")))
        {
            PerderVida(); 
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GemaVermelha"))
        {
            Debug.Log("PEGOU A GEMA VERMELHA.");
            GanharVida();
            Destroy(other.gameObject); 
        }
        else if (other.CompareTag("GemaAzul"))
        {
            audioManager.PlaySFX(audioManager.gemaazul);

            Debug.Log("PEGOU A GEMA AZUL. +15s de imortalidade.");
            StartCoroutine(AtivarImortalidade(15f)); 
            Destroy(other.gameObject); 
        }
        else if (other.CompareTag("GemaRoxa"))
        {
            GanharJogo();
            Destroy(other.gameObject); 
        }
    }

    void GanharVida()
    {
        if (vidasAtuais < maxVidas) 
        {
            vidasAtuais++; 
            AtualizarPlacar(); 
            Debug.Log("Ganhou 1 vida! Vidas atuais: " + vidasAtuais);
            audioManager.PlaySFX(audioManager.gemavermelha);
        }
        else
        {
            Debug.Log("Já está com o número máximo de vidas (" + maxVidas + ").");
        }
    }

    IEnumerator AtivarImortalidade(float tempoDeImortalidade)
    {
        Debug.Log("Imortalidade ativada por " + tempoDeImortalidade + " segundos.");

        // Ativa o painel e o texto de imortalidade
        if (painelImortalidade != null)
            painelImortalidade.SetActive(true);

        if (imortalidadeTexto != null)
        {
            imortalidadeTexto.text = "Imortalidade Ativada!";
            Debug.Log("Texto de imortalidade exibido.");

        }
        else
        {
            Debug.LogError("imortalidadeTexto não está atribuído no Inspector!");
        }

        if (cronometroTexto != null)
        {
            StartCoroutine(AtualizarCronometro(tempoDeImortalidade));
        }
        else
        {
            Debug.LogError("cronometroTexto não está atribuído no Inspector!");
        }

        isInvulnerable = true; 
        yield return new WaitForSeconds(tempoDeImortalidade); 
        isInvulnerable = false; 
        Debug.Log("Imortalidade desativada.");

        if (painelImortalidade != null)
            painelImortalidade.SetActive(false);

        if (imortalidadeTexto != null)
            imortalidadeTexto.text = "";

        if (cronometroTexto != null)
            cronometroTexto.text = "";
    }

    IEnumerator AtualizarCronometro(float duracao)
    {
        float tempoRestante = duracao;

        while (tempoRestante > 0)
        {
            if (cronometroTexto != null)
            {
                cronometroTexto.text = tempoRestante.ToString("F1") + "s"; 
                Debug.Log("Cronômetro atualizado: " + tempoRestante.ToString("F1") + "s");
            }

            yield return new WaitForSeconds(0.1f); 
            tempoRestante -= 0.1f;
        }

        if (cronometroTexto != null)
        {
            cronometroTexto.text = "";
            Debug.Log("Cronômetro finalizado.");
        }
    }

    void GanharJogo()
    {
        Debug.Log("Você ganhou o jogo!");
        SceneManager.LoadSceneAsync("TelaDeVitoria"); 
    }

    void PerderVida()
    {

        audioManager.PlaySFX(audioManager.morreu);

        vidasAtuais--;
        Debug.Log("Vidas restantes: " + vidasAtuais);

        if (vidasAtuais < 0)
        {
            vidasAtuais = 0;
        }

        AtualizarPlacar();
        StartCoroutine(EfeitoDano());

        if (vidasAtuais <= 0)
        {

            SceneManager.LoadSceneAsync(2); 
        }
        else
        {
            StartCoroutine(AtivarImortalidade(tempoDeInvulnerabilidade)); 
        }
    }

    IEnumerator EfeitoDano()
    {
        if (telaDano == null)
        {
            Debug.LogError("O Panel Dano não foi atribuído no Inspector!");
            yield break; 
        }

        telaDano.color = new Color(1, 0, 0, 0.5f); 

        yield return new WaitForSeconds(0.5f);

        telaDano.color = new Color(1, 0, 0, 0); 
    }

    void AtualizarPlacar()
    {
        if (vidasAtuais >= 0)
        {
            string novoPlacar = new string('O', vidasAtuais); 
            placarTexto.text = novoPlacar;
        }
        else
        {

            placarTexto.text = "Game Over"; 
        }
    }
}
