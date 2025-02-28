using UnityEngine;

public class CloudInteraction : MonoBehaviour
{
    public ParticleSystem burstParticles;
    public float fadeSpeed = 2f;
    private SpriteRenderer _sprite;

    void Start() => _sprite = GetComponent<SpriteRenderer>();

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && burstParticles != null)
        {
            burstParticles.Play();
            StartCoroutine(FadeCloud());
        }
    }

    private System.Collections.IEnumerator FadeCloud()
    {
        float alpha = 1f;
        while (alpha > 0)
        {
            alpha -= Time.deltaTime * fadeSpeed;
            _sprite.color = new Color(1, 1, 1, alpha);
            yield return null;
        }
        Destroy(gameObject);
    }
}