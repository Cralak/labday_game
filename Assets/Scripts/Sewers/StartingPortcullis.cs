using DG.Tweening;
using UnityEngine;

public class StartingPortcullis : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.DOMoveY(1.1f, 14.0f).SetEase(Ease.Linear);
    }
}
