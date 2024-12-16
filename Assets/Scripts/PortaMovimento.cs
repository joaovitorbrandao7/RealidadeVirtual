using UnityEngine;

public class PortaMovimento : MonoBehaviour
{
    public float posicaoCima = 35f; // Posição máxima no eixo Y (em cima)
    public float posicaoBaixo = 20f; // Posição mínima no eixo Y (em baixo)
    public float velocidade = 2f; // Velocidade do movimento

    private bool subindo = true; // Indica se a porta está subindo

    void Update()
    {
        // Movimento para cima
        if (subindo)
        {
            transform.position += Vector3.up * velocidade * Time.deltaTime;

            // Verifica se atingiu a posição superior
            if (transform.position.y >= posicaoCima)
            {
                subindo = false; // Inverte para descer
            }
        }
        // Movimento para baixo
        else
        {
            transform.position += Vector3.down * velocidade * Time.deltaTime;

            // Verifica se atingiu a posição inferior
            if (transform.position.y <= posicaoBaixo)
            {
                subindo = true; // Inverte para subir
            }
        }
    }
}
