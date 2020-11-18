using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using SpeechLib;

public class Trigersdetections : MonoBehaviour
{

    private int keys = 0;

    public Text txtKeys;

    private SpVoice voice;
    public GameObject panel;

    // Start is called before the first frame update
    void Start()
    {
        voice = new SpVoice();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void mostrarDialogo(string txtMostrado, int duracion) {
	IEnumerator waiter() {
        panel.gameObject.SetActive(true);
        panel.GetComponent<Text>().text = txtMostrado;
        voice.Speak(txtMostrado, SpeechVoiceSpeakFlags.SVSFlagsAsync);
	yield return new WaitForSeconds(duracion); // espera
	if (panel.GetComponent<Text>().text == txtMostrado) {
	    esconderDialogo();
	}
	}
        StartCoroutine(waiter()); //para esperar
    } 

    void esconderDialogo() {
        panel.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag == "Key") {
            Destroy(other.gameObject);
            keys++;
            txtKeys.text = keys.ToString();
            mostrarDialogo("Llave encontrada!", 2);
        }
    }
/*
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Key") {
            esconderDialogo();
        }
    }
*/
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Puerta") {
            if (keys > 0)
            {
                Destroy(collision.gameObject);
                keys--;
                txtKeys.text = keys.ToString();
                mostrarDialogo("Has escapado de tu habitacion. Continua con cuidado.", 4);
            }
            else {
                Debug.Log("No tines una llave para pasar por aqui");
                mostrarDialogo("Necesitas una llave para pasar por esta puerta!", 3);
            }
        }
        if (collision.collider.tag == "PuertaFinal") {
	    mostrarDialogo("Has logrado escapar de la casa. Felicidades!", 4);
	}
    }
	/*
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "Puerta") {
            esconderDialogo();
        }
        if (collision.collider.tag == "PuertaFinal") {
	    esconderDialogo();
	}
    }*/
}
