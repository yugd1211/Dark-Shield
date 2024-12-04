using Cinemachine;
using UnityEngine;

public class CameraMoving : MonoBehaviour
{
	private CinemachineVirtualCamera _playerFollowCamera;
	
	private void Awake()
	{
		_playerFollowCamera = GetComponent<CinemachineVirtualCamera>();
	}
	
	private void Start()
	{
		_playerFollowCamera.Follow = GameManager.Instance.player.transform;
	}
}
