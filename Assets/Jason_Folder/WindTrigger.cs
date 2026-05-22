using UnityEngine;

public class WindTrigger : MonoBehaviour
{
    public ParticleSystem WindEffect;

    void OnMouseDown()
    {
        ParticleSystem effect = Instantiate(WindEffect, transform.position, Quaternion.identity);
        effect.Play();


        Destroy(effect.gameObject, 5f);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
