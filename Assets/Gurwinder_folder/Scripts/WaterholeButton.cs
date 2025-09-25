using UnityEngine;
using UnityEngine.UI;

public class WaterholeButton : MonoBehaviour
{
  public GameObject window;  

    void Start()
    {
        
        if (window != null)
        {
            window.SetActive(false);  
        }
    }

    void Update()
    {
        
        if (Input.GetMouseButtonDown(0)) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                
                if (hit.collider.gameObject == gameObject)
                {
                    ToggleWindow(); 
                }
            }
        }
    }

    void ToggleWindow()
    {
       
        if (window != null)
        {
            bool isActive = window.activeSelf;
            window.SetActive(!isActive);  
        }
    }

    

}
