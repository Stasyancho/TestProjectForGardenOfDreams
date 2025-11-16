using UnityEngine;

public class FollowingCamera : MonoBehaviour
{
    [SerializeField] private GameObject _character;
    [SerializeField] private float _returnSpeed;

    void Start()
    {
        transform.position = new Vector3(_character.transform.position.x, _character.transform.position.y, -1);
    }

    void Update()
    {
        CameraMove();
    }

    void CameraMove()
    {
        if (_character != null)
        {
            var currentVector = new Vector3(_character.transform.position.x, _character.transform.position.y, -1);
            transform.position = Vector3.Lerp(transform.position, currentVector, _returnSpeed * Time.deltaTime);
        }
    }
}

