using UnityEngine;

public class FollowObject : MonoBehaviour
{
    
    [SerializeField]
    private GameObject objectToFollow;
    private Transform _transformToFollow;

    // Start is called before the first frame update
    void Start()
    {
        _transformToFollow = objectToFollow.transform;
    }

    // LateUpdate is called once per frame after Update() methods
    void LateUpdate()
    {
        if (_transformToFollow != null)
            transform.position = new Vector3(_transformToFollow.position.x, _transformToFollow.position.y, transform.position.z);
    }
}

