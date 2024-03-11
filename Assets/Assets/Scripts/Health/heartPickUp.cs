using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heartPickUp : MonoBehaviour
{
    public float healthAmount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Health theHealthPlayer = other.gameObject.GetComponent<Health>();
        }
    }
}
