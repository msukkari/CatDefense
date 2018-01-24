using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public Image mainImage;
    public Image levelSelectImage;

    private bool joystickChanged;
	public GameObject[] menuDisplay;
    private List<Text> levelTextList;
    private ControllerInputManager m_controllerInstance;
    private bool isInLevelSelect;
    private int selectedLevel;

    void Start() {
        levelTextList = new List<Text>();
        foreach(Transform child in levelSelectImage.transform)
        {
			Text t = child.GetComponent<Text> ();
			if(t!=null)
           	 levelTextList.Add(child.GetComponent<Text>());
        }

        m_controllerInstance = ControllerInputManager.GetInstance();
        m_controllerInstance.OnADown += onADown;
        m_controllerInstance.OnLSChange += onLSChange;

        isInLevelSelect = false;
        mainImage.gameObject.SetActive(true);
        levelSelectImage.gameObject.SetActive(false);
        selectedLevel = 0;
        levelTextList[selectedLevel].color = Color.white;
        joystickChanged = false;
    }

    private void OnDestroy()
    {
        m_controllerInstance.OnADown -= onADown;
        m_controllerInstance.OnLSChange -= onLSChange;
    }

    public void onADown()
    {
        if (isInLevelSelect)
        {
            SceneManager.LoadScene(selectedLevel + 1);
        }
        else
        {
            isInLevelSelect = true;
            mainImage.gameObject.SetActive(false);
            levelSelectImage.gameObject.SetActive(true);
        }
    }

    public void onLSChange(float x, float y)
    {
		//Temporary so we can use keyboard input as well
		if (x == 0 && y == 0)
		{
			if (Input.GetKeyDown (KeyCode.S))
			{
				if (selectedLevel < levelTextList.Count - 1)
				{
					levelTextList [selectedLevel].color = Color.black;
					selectedLevel = (selectedLevel + 1) % levelTextList.Count;
					levelTextList [selectedLevel].color = Color.white;
				}
			} else if (Input.GetKeyDown (KeyCode.W))
			{
				if(selectedLevel > 0)
				{
					levelTextList[selectedLevel].color = Color.black;
					selectedLevel = (selectedLevel - 1) % levelTextList.Count;
					levelTextList[selectedLevel].color = Color.white;
				}
			}
		}

        if (!joystickChanged)
        {
            if (y < 0) 
            {
                if(selectedLevel < levelTextList.Count - 1)
                {
                    levelTextList[selectedLevel].color = Color.black;
					selectedLevel = (selectedLevel + 1) % levelTextList.Count;
                    levelTextList[selectedLevel].color = Color.white;

                }
                joystickChanged = true;
            }
            else if (y > 0)
            {
                if(selectedLevel > 0)
                {
                    levelTextList[selectedLevel].color = Color.black;
					selectedLevel = (selectedLevel - 1) % levelTextList.Count;
                    levelTextList[selectedLevel].color = Color.white;
                }
                joystickChanged = true;
            }

            // make sure selectedLevel doesn't exceed bounds
            //selectedLevel = selectedLevel < 0 ? 0 : (selectedLevel >= levelTextList.Count ? levelTextList.Count - 1 : selectedLevel);
        }
        else if (y == 0) joystickChanged = false;

		for (int i = 0; i < menuDisplay.Length; i++)
		{
			menuDisplay[i].SetActive (i == selectedLevel);
		}

    }


 
}
