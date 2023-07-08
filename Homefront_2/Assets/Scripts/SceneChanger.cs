using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;


public class SceneChanger : MonoBehaviour
{
	public void ChangeScene(int _sceneNumber) {
		if(_sceneNumber == 0)
        {
			GameProgressController.GameProgress = null;
        }
		Resume();
		SceneManager.LoadScene(_sceneNumber);
	}
	public void Exit() {
		Application.Quit();
		//EditorApplication.isPlaying = false;
	}
	public void Pause()
    {
		Time.timeScale = 0f;
    }
	public void Resume()
	{
		Time.timeScale = 1f;
	}
}
