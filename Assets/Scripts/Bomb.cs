using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private GameObject explosionPrefab;

    private bool _exploded;
    private float _fuseTime;
    private AnimatorOverrideController _overrideController;

    private void Awake()
    {
        AnimationClip clip = GetComponent<Animator>().runtimeAnimatorController.animationClips.FirstOrDefault(clip => clip.name == "fuse");
        if (clip != null)
            _fuseTime = clip.length;
        else
            _fuseTime = 0f;
    }

    private void Start()
    {
        StartCoroutine(Explode());
    }

    private IEnumerator<WaitForSeconds> Explode()
    {
        yield return new WaitForSeconds(_fuseTime + 0.25f);
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
