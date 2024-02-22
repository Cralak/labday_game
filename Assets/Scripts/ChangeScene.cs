using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
    static public IEnumerator GoTo(GameObject player, Vector3 endingPosition, string sceneName)
    {
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();

        CreateBlackScreen();

        playerMovement.enabled = false;
        player.GetComponent<AudioSource>().Pause();

        yield return new WaitForSeconds(0.1f);

        playerMovement.enabled = true;
        player.transform.position = endingPosition;
        SceneManager.LoadScene(sceneName);
    }

    static public IEnumerator GoTo(GameObject player, Vector3 endingPosition)
    {
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();

        GameObject child = CreateBlackScreen();
        Image blackScreen = child.GetComponent<Image>();

        playerMovement.enabled = false;
        player.GetComponent<AudioSource>().Pause();

        for (float i = 0; i <= 1; i += 0.05f)
        {
            Color c = blackScreen.color;
            c.a = i;
            blackScreen.color = c;

            // Wait for a short duration before the next iteration
            yield return new WaitForSeconds(0.005f);
        }

        player.transform.position = endingPosition;

        // Fade out the black screen
        for (float i = 1.0f; i >= 0; i -= 0.05f)
        {
            Color c = blackScreen.color;
            c.a = i;
            blackScreen.color = c;

            // Wait for a short duration before the next iteration
            yield return new WaitForSeconds(0.005f);
        }

        Destroy(child.transform.parent.gameObject);

        yield return new WaitForSeconds(0.1f);

        playerMovement.enabled = true;
    }

    static GameObject CreateBlackScreen()
    {
        GameObject parent = new("LoadScreen");
        Canvas canvas = parent.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        CanvasScaler canvasScaler = parent.AddComponent<CanvasScaler>();
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasScaler.referenceResolution = new Vector2(1920.0f, 1080.0f);
        parent.AddComponent<GraphicRaycaster>();

        GameObject child = new("BlackScreen");
        child.transform.parent = parent.transform;
        child.AddComponent<CanvasRenderer>();
        Image blackScreen = child.AddComponent<Image>();
        blackScreen.color = Color.black;
        RectTransform trans = child.GetComponent<RectTransform>();
        trans.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        trans.sizeDelta = new Vector2(2000.0f, 1500.0f);

        return child;
    }
}
