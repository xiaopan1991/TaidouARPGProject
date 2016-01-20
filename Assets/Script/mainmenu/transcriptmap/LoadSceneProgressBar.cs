using UnityEngine;
using System.Collections;

public class LoadSceneProgressBar : MonoBehaviour {

	public static LoadSceneProgressBar _instance;

	private GameObject bg;
	private UISlider progressBar;

	void Awake()
	{
		_instance = this;
		bg = transform.Find("Bg").gameObject;
		progressBar = transform.Find("Bg/ProgressBar").GetComponent<UISlider>();

		gameObject.SetActive(false);
	}

	public void show(AsyncOperation ao)
	{
		gameObject.SetActive(true);
		bg.SetActive(true);
		progressBar.value = ao.progress;
	}
}
