using UnityEngine;

public class ClickFireEffect : MonoBehaviour
{
    
    [Header("setting")]
    public GameObject fire1; 
    public float duration = 2.0f; 

    
    void OnMouseDown()
    {
        if (fire1 != null)
        {
            SpawnFire();
        }
    }

    void SpawnFire()
    {
        
        GameObject currentFire = Instantiate(
            fire1,         
            transform.position, 
            Quaternion.identity 
        );

        Destroy(currentFire, duration);
    }
}