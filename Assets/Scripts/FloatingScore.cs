using TMPro;
using UnityEngine;

public class FloatingScore : MonoBehaviour
{
    [SerializeField] private float lifeTime = 1f;
    [SerializeField] private float speed = 1f;

    private TextMeshPro text;

    private void Awake()
    {
        text = GetComponent<TextMeshPro>();
    }

    public void Initialize(int score, Vector3 pos)
    {
        text.text = score.ToString();
        transform.position = pos;
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime, Space.World);
        float alpha = text.color.a - (Time.deltaTime / lifeTime);
        text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
    }
}
