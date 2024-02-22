using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
    static public IEnumerator GoTo(GameObject player, string sceneName, Vector3 endingPosition)
    {
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();

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

        playerMovement.enabled = false;
        player.GetComponent<AudioSource>().Pause(); 

        yield return new WaitForSeconds(0.1f);

        playerMovement.enabled = true;
        player.transform.position = endingPosition;
        SceneManager.LoadScene(sceneName);
    }
}
