using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public GameObject animalListPrefab;
    public GameObject listContainer;

    private Transform cameraLookAt;
    private Transform cameraTransform;
    private const float CAMERA_SPEED = 4.0f;

    private void Start()
    {
        // get all content from Resources/Animals/..
        cameraTransform = Camera.main.transform;

        Sprite[] thumbnails = Resources.LoadAll<Sprite>("Animals");
        foreach (Sprite thumbnail in thumbnails)
        {
            // add thumbnail to List 

            GameObject container = Instantiate(animalListPrefab) as GameObject;
            container.GetComponent<Image>().sprite = thumbnail;
            container.transform.SetParent(listContainer.transform, false);


            // get destination onClick action
            string sceneName = thumbnail.name;
            container.GetComponent<Button>().onClick.AddListener(() => LoadDestination(sceneName));
        }
    }

    private void Update()
        // make Camera switch between menus 
    {
        if ( cameraLookAt != null)
        {
            cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation, cameraLookAt.rotation, CAMERA_SPEED * Time.deltaTime);
        }
    }

    // loading destination
    private void LoadDestination(string sceneName)
    {
        SceneManager.LoadScene(sceneName);

        //Adding Scenes to MainMenu
        // Create new NewScene 'Destination1 +n' (File -> New Scene)  
        // Create new assetes folder 'Destinations' and add the created scene 'Destination 1+n' 
        // Make sure 'MainMenu' scene is selected, then go to 'build settings' -> 'add open scenes' 
        // Select another scene and hit 'add open scene' again 

        // Create Path to Destination

    }

    //Camere view on clicked Menu

    public void LookMenu (Transform menuTransform)
    {
        cameraLookAt = menuTransform;
    }
}