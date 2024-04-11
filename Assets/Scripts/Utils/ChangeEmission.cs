using System.Collections;
using UnityEngine;

public class ChangeEmission : MonoBehaviour
{
    // Durée de l'animation en secondes
    readonly float animationDuration = 2f;

    // Référence au material attaché à cet objet
    Renderer rend;

    // Référence au transform du joueur
    Transform player;

    void Start()
    {
        // Obtient le material attaché à cet objet
        rend = GetComponent<Renderer>();

        // Assure que le matériau a l'émission activée
        rend.material.EnableKeyword("_EMISSION");

        // Obtient le joueur
        player = GameObject.Find("Player").transform;

        // Lance la coroutine pour l'animation
        StartCoroutine(EmissionColorLoop());
    }

    // Coroutine pour animer la couleur d'émission
    IEnumerator EmissionColorLoop()
    {
        while (true)
        {
            // Définit les couleurs de départ et de fin en fonction de la position du joueur
            Color startColor = Color.black;
            float intensity = Mathf.Max(1.0f - Vector3.Distance(player.position, transform.position) / 10.0f, 0.0f);
            Color endColor = new(intensity, intensity , 0.0f);

            // Noir à Jaune
            yield return ChangeEmissionColor(startColor, endColor, animationDuration);

            yield return new WaitForSeconds(1f);
            // Jaune à Noir
            yield return ChangeEmissionColor(endColor, startColor, animationDuration);
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
