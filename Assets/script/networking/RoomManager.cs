using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviourPunCallbacks
{
	public static RoomManager i;
	
	private void Awake()
	{
		if (i != null && i != this)
		{
			Destroy(this);
		}
		
		else
		{
			DontDestroyOnLoad(gameObject);
			i = this;
		}
	}

	// need to call base on photon-inheriting enable, disable

	public override void OnEnable()
	{
		base.OnEnable();
		
		// whenever we switch scene, onsceneloaded will be called
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	public override void OnDisable()
	{
		base.OnDisable();
		
		// no longer call onsceneloaded if this is disabled
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	private void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
	{
		// are we in the actual game?
		bool inGameScene = scene.buildIndex == 1;
		if (inGameScene)
		{
			// unity excludes files not referenced from final build
			// but photon uses strings to refer to its prefabs, since theyre synced over network
			// they technically aren't referred to
			// therefore, we must put photon prefabs in a resources folder
			// to instantiate a photon prefab, it must have a photonview component!
			
			// YOU WERE AT 4:25 on rugbugredfern video 5; 
		}
	}

	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		
	}
}
