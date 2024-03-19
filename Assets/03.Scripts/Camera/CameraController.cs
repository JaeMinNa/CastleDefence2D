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
            // 월드 좌표 = TransformPoint (로컬 좌표)
            Vector3 targetPosition = target.TransformPoint(new Vector3(0, 0, -10f));

            // 목표 위치까지 부드럽게 이동할 때 사용하는 메소드
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, _smoothTime);
        }
    }
}
