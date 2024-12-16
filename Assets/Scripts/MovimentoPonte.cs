using UnityEngine;
using System.Collections;

public class RotacaoPonteY : MonoBehaviour
{
    public float anguloMaximo = 105f; 
    public float anguloCentral = 92.15f; 
    public float anguloMinimo = 80f; 
    public float velocidade = 30f; 
    public float pausa = 2f;

    private Coroutine movimentoCoroutine; 

    void Start()
    {
        movimentoCoroutine = StartCoroutine(MovimentarPonte());
    }

    IEnumerator MovimentarPonte()
    {
        while (true)
        {
            yield return RotacionarPara(anguloMaximo);

            yield return RotacionarPara(anguloCentral);
            yield return new WaitForSeconds(pausa);

            yield return RotacionarPara(anguloMinimo);

            yield return RotacionarPara(anguloCentral);
            yield return new WaitForSeconds(pausa);
        }
    }

    IEnumerator RotacionarPara(float anguloDestino)
    {
        float anguloAtual = transform.localEulerAngles.y;

        if (anguloAtual > 180) anguloAtual -= 360;
        if (anguloDestino > 180) anguloDestino -= 360;

        while (Mathf.Abs(anguloAtual - anguloDestino) > 0.1f)
        {
            anguloAtual = Mathf.MoveTowards(anguloAtual, anguloDestino, velocidade * Time.deltaTime);
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, anguloAtual, transform.localEulerAngles.z);
            yield return null;
        }
    }
}
