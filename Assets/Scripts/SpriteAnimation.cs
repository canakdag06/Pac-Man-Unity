using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteAnimation : MonoBehaviour
{
    public SpriteRenderer Renderer { get; private set; }
    public Sprite[] sprites;
    public int FPS = 4;
    public int Frame { get; private set; }
    public bool loop = true;

    private void Awake()
    {
        Renderer = GetComponent<SpriteRenderer>();
    }


    void Start()
    {
        float time = 1f / FPS;
        InvokeRepeating(nameof(PlayAnimation), time, time);
    }


    private void PlayAnimation()
    {
        if (!Renderer.enabled) return;

        Frame++;

        if (Frame >= sprites.Length && loop)
        {
            Frame = 0;
        }

        Renderer.sprite = sprites[Frame];
    }
}
