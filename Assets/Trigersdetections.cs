using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trigersdetections : MonoBehaviour
{

    private int keys = 0;

    public Text txtKeys;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag == "Key") {
            Destroy(other.gameObject);
            keys++;
            txtKeys.text = keys.ToString();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Puerta") {
            if (keys > 0)
            {
                Destroy(collision.gameObject);
                keys--;
                txtKeys.text = keys.ToString();
            }
            else {
                Debug.Log("No tines una llave para pasr por aqui");
            }
        }
        
    }

}
