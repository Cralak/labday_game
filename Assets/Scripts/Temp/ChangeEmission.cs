using System.Collections;
using UnityEngine;

public class ChangeEmission : MonoBehaviour
{
    // Référence au material attaché à cet objet
    private Renderer rend;

    // Durée de l'animation en secondes
    public float animationDuration = 2f;

    void Start()
    {
        // Obtient le material attaché à cet objet
        rend = GetComponent<Renderer>();

        // Assure que le matériau a l'émission activée
        rend.material.EnableKeyword("_EMISSION");

        // Lance la coroutine pour l'animation
        StartCoroutine(EmissionColorLoop());
    }

    // Coroutine pour animer la couleur d'émission
    IEnumerator EmissionColorLoop()
    {
        while (true)
        {
            // Noir à Jaune
            yield return ChangeEmissionColor(Color.black, Color.yellow, animationDuration);

            yield return new WaitForSeconds(1f);
            // Jaune à Noir
            yield return ChangeEmissionColor(Color.yellow, Color.black, animationDuration);
        }
    }

    // Méthode pour animer la couleur d'émission d'une couleur à une autre en une certaine durée
    IEnumerator ChangeEmissionColor(Color startColor, Color endColor, float duration)
    {
        float startTime = Time.time;
        float endTime = startTime + duration;

        while (Time.time < endTime)
        {
            float progress = (Time.time - startTime) / duration;
            rend.material.SetColor("_EmissionColor", Color.Lerp(startColor, endColor, progress));
            yield return null;
        }

        // Assure que la couleur finale est exactement celle spécifiée
        rend.material.SetColor("_EmissionColor", endColor);
    }
}
