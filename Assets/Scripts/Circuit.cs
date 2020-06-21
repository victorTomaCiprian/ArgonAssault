using UnityEngine;
using Cinemachine;

public class Circuit : MonoBehaviour{
	[SerializeField] private float _targetSpeed = 30f;
 
	[SerializeField, Min(0), Tooltip("Set to 0 for instant speed changes")] 
	private float _accelerationRate = 1.5f;
 
	private CinemachineVirtualCamera _camera;
	private CinemachineTrackedDolly _dolly;
	private float _currentSpeed;
 
	// Start is called before the first frame update
	void Start()
	{
		SetupCamera();
	}
 
	// Update is called once per frame
	void Update()
	{
		MoveDolly();
	}
 
 
	private void SetupCamera()
	{
		_camera = GetComponent<CinemachineVirtualCamera>();
		_dolly = _camera.GetCinemachineComponent<CinemachineTrackedDolly>();
		_dolly.m_PositionUnits = CinemachinePathBase.PositionUnits.Distance;
	}
 
 
	private void MoveDolly()
	{
		CalculateSpeed();
		_dolly.m_PathPosition = _dolly.m_PathPosition + _currentSpeed * Time.deltaTime;
 
		//If closed loop, make sure the pathposition will not overflow
		if (_dolly.m_Path.Looped && _dolly.m_PathPosition > _dolly.m_Path.PathLength)
		{
			_dolly.m_PathPosition = _dolly.m_PathPosition - _dolly.m_Path.PathLength;
		}
 
	}
 
	private void CalculateSpeed()
	{
 
		// Set speed to target speed if difference is smaller than acceleration value, or acceleration is set to 0
		if (Mathf.Abs(_currentSpeed - _targetSpeed) <= _accelerationRate | Mathf.Abs(_accelerationRate) < float.Epsilon)
		{
			_currentSpeed = _targetSpeed;
			return;
		}
 
		// Accelerate
		else if (_currentSpeed < _targetSpeed)
		{
			_currentSpeed += _accelerationRate;
		}
 
		// Decelerate
		else
		{
			_currentSpeed -= _accelerationRate;
		}
	}
}
