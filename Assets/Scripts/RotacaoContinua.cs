using UnityEngine;

public class RotacaoComInteracao : MonoBehaviour
{
    public float velocidadeRotacao = 50f; // Velocidade da rotação em graus por segundo
    public Vector3 eixoRotacao = Vector3.up; // Eixo de rotação (padrão: eixo Y)

    void Update()
    {
        // Aplica a rotação contínua ao retângulo
        transform.Rotate(eixoRotacao * velocidadeRotacao * Time.deltaTime);
    }

    // Quando o personagem entra em contato, ele é "fixado"
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform); // Torna o personagem filho do retângulo
        }
    }

    // Quando o personagem sai do contato, ele é "solto"
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null); // Remove o vínculo com o retângulo
        }
    }
}
