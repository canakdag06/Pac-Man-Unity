using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImageAnimation : MonoBehaviour
{
    public Image Image { get; private set; }
    public Sprite[] sprites;
    public int FPS;
    public int Frame { get; private set; }
    public bool loop = true;

    private void Awake()
    {
        Image = GetComponent<Image>();
    }


    void Start()
    {
        float time = 1f / FPS;
        InvokeRepeating(nameof(PlayAnimation), time, time);
    }

    public void StartAnimation()
    {
        if (IsInvoking(nameof(PlayAnimation)))
        {
            CancelInvoke(nameof(PlayAnimation));
        }

        Frame = 0;
        float time = 1f / FPS;

        InvokeRepeating(nameof(PlayAnimation), 0f, time);
    }

    public void StopAnimation()
    {
        CancelInvoke(nameof(PlayAnimation));
    }

    public void PlayAnimation()
    {
        if (!Image.enabled) return;

        Frame++;

        if (Frame >= sprites.Length && loop)
        {
            if (loop)
            {
                Frame = 0;
            }
            else
            {
                StopAnimation();
            }
        }

        if (Frame < sprites.Length)
        {
            Image.sprite = sprites[Frame];
        }
    }

    public void Restart()
    {
        StartAnimation();
    }
}
