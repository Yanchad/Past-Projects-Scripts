using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExpandScreen : MonoBehaviour
{
    private Camera cam;
    [SerializeField] private float rotationSpeed;
    

    void Start()
    {
        cam = GetComponent<Camera>();
        cam.orthographicSize = 3.5f;
    }

    
    void FixedUpdate()
    {
        
        if (cam.orthographicSize < 5.6f)
        {
            cam.orthographicSize += 0.003f;
            transform.Rotate(new Vector3(-rotationSpeed * Time.deltaTime, 0, 0));
        }
        else if (cam.orthographicSize >= 5.6f)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
