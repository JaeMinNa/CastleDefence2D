using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{ 
    [SerializeField] private float _smoothTime = 0.3f;
    [SerializeField] private float _cameraRangeX = 30f;

    private Vector3 velocity = Vector3.zero;
    private Transform target;

    private void Start()
    {
        target = GameManager.I.PlayerManager.Player.transform;
    }

    private void LateUpdate()
    {
        if (target.transform.position.x < _cameraRangeX && target.transform.position.x > -_cameraRangeX)
        {
            // ���� ��ǥ = TransformPoint (���� ��ǥ)
            Vector3 targetPosition = target.TransformPoint(new Vector3(0, 0, -10f));

            // ��ǥ ��ġ���� �ε巴�� �̵��� �� ����ϴ� �޼ҵ�
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, _smoothTime);
        }
    }
}
