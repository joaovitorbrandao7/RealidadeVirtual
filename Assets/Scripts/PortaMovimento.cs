using UnityEngine;

public class PortaMovimento : MonoBehaviour
{
    public float posicaoCima = 35f; // Posi��o m�xima no eixo Y (em cima)
    public float posicaoBaixo = 20f; // Posi��o m�nima no eixo Y (em baixo)
    public float velocidade = 2f; // Velocidade do movimento

    private bool subindo = true; // Indica se a porta est� subindo

    void Update()
    {
        // Movimento para cima
        if (subindo)
        {
            transform.position += Vector3.up * velocidade * Time.deltaTime;

            // Verifica se atingiu a posi��o superior
            if (transform.position.y >= posicaoCima)
            {
                subindo = false; // Inverte para descer
            }
        }
        // Movimento para baixo
        else
        {
            transform.position += Vector3.down * velocidade * Time.deltaTime;

            // Verifica se atingiu a posi��o inferior
            if (transform.position.y <= posicaoBaixo)
            {
                subindo = true; // Inverte para subir
            }
        }
    }
}
