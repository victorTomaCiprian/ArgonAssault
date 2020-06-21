using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour{
	#region Variables
    //Config
    [SerializeField] private float _splashScreenDuration = 5f;
    //State

    //Cached component references
    private int _currentSceneIndex;
	#endregion
	
	#region Unity Methods
    private void Awake() {
	    _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
    
    private void Start() {
	    StartCoroutine(LoadNextSceneRoutine(_splashScreenDuration));
    }

    private void Update(){
        
    }
	#endregion
	

	private IEnumerator LoadNextSceneRoutine(float timeBetweenScenes = 0f) {
		yield return new WaitForSecondsRealtime(timeBetweenScenes);
		SceneManager.LoadScene(_currentSceneIndex + 1);
	}
}
