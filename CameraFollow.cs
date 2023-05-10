using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // 카메라가 따라다닐 플레이어
    public float cameraHeight = 5f; // 카메라의 y축 위치

    void LateUpdate()
    {
        Vector3 targetPosition = new Vector3(player.position.x, player.position.y + cameraHeight, transform.position.z); // 카메라가 따라다닐 목표 위치 계산
        transform.position = targetPosition; // 카메라 위치 업데이트
    }
}