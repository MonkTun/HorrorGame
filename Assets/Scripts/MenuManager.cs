using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

	// Update is called once per frame
	private void Update()
	{
        if (Input.anyKey) LoadGame();
	}

	public void LoadGame()
    {
        SystemManager.Instance.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
