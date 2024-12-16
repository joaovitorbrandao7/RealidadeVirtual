using UnityEngine;

public class RotacaoComInteracao : MonoBehaviour
{
    public float velocidadeRotacao = 50f; // Velocidade da rota��o em graus por segundo
    public Vector3 eixoRotacao = Vector3.up; // Eixo de rota��o (padr�o: eixo Y)

    void Update()
    {
        // Aplica a rota��o cont�nua ao ret�ngulo
        transform.Rotate(eixoRotacao * velocidadeRotacao * Time.deltaTime);
    }

    // Quando o personagem entra em contato, ele � "fixado"
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform); // Torna o personagem filho do ret�ngulo
        }
    }

    // Quando o personagem sai do contato, ele � "solto"
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null); // Remove o v�nculo com o ret�ngulo
        }
    }
}
