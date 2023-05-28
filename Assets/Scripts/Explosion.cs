using System.Collections;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float explodeTime = 1.0f;   // The time it takes for the bomb to explode.
    public float explosionRadius = 5f; // The final size of the explosion.

    private float _initialSize;

    void Start()
    {
        // Record the initial size of the bomb.
        _initialSize = transform.localScale.x;
        Explode();
    }

    public void Explode()
    {
        StartCoroutine(ExplodeCoroutine());
    }

    IEnumerator ExplodeCoroutine()
    {
        // Calculate the time at which the bomb will finish exploding.
        float explodeFinishTime = Time.time + explodeTime;

        // While the bomb is still exploding...
        while (Time.time < explodeFinishTime)
        {
            // Calculate how much of the explode time has passed.
            float explodeProgress = 1 - ((explodeFinishTime - Time.time) / explodeTime);

            // Set the size of the bomb based on the explode progress.
            float size = Mathf.Lerp(_initialSize, explosionRadius, explodeProgress);
            Debug.Log("Size: " + size);
            transform.localScale = new Vector3(size, size, size);

            // Wait until the next frame.
            yield return null;
        }

        // The bomb has finished exploding. Destroy it.
        Destroy(gameObject);
    }
}
