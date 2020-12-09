using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SpeechLib;

public class Trigersdetections : MonoBehaviour
{

    private int keys = 0;
    public bool hablando = false;
    public Text txtKeys;

    private SpVoice voice = new SpVoice();
    public GameObject panel;
    public GameObject panelGameOver;
    public GameObject panelCreditos;

    private AudioSource audioSource;

    public AudioClip clipKey;

    public AudioClip clipDorClose;
    public AudioSource audioLlave;
    public AudioSource audioPuerta;
    public AudioSource audioPuertaLock;
    public AudioSource audioGameOver;

    // Start is called before the first frame update
    void Start()
    {
        voice = new SpVoice();

        audioSource = GetComponent<AudioSource>();
        audioLlave = gameObject.GetComponent<AudioSource>();
        mostrarDialogo(""+audioLlave, 5);
        audioPuerta = gameObject.GetComponent<AudioSource>();
        audioPuertaLock = gameObject.GetComponent<AudioSource>();
        audioGameOver = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {}

    void mostrarDialogo(string txtMostrado, int duracion) {
	IEnumerator waiter() {
        panel.gameObject.SetActive(true);
        panel.GetComponent<Text>().text = txtMostrado;
	if(hablando == false){
	 hablando = true;
         voice.Speak(txtMostrado, SpeechVoiceSpeakFlags.SVSFlagsAsync);
	}
	yield return new WaitForSeconds(duracion); // espera
	if (panel.GetComponent<Text>().text == txtMostrado) {
	    hablando = false;
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
            //audioSource.PlayOneShot(clipKey);
            Destroy(other.gameObject);
            audioLlave.Play();
            keys++;
            txtKeys.text = keys.ToString();
            mostrarDialogo("Llave encontrada!", 2);
        }
        if (other.tag == "Enemy") {
            Debug.Log("Te ha tocado el enemigo estas jodido");
            panelGameOver.SetActive(true);
            panel.SetActive(false);
            audioGameOver.Play();
            Time.timeScale = 0;
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
                audioPuerta.Play();
                mostrarDialogo("Has escapado de tu habitacion. Continúa con cuidado.", 4);
            }
            else {
                //audioSource.PlayOneShot(clipDorClose);
                Debug.Log("No tines una llave para pasar por aqui");
                audioPuertaLock.Play();
                Debug.Log("No tienes una llave para pasar por aqui");
                mostrarDialogo("Necesitas una llave para pasar por esta puerta!", 3);
            }
        }
        if (collision.collider.tag == "PuertaFinal") {
            if (keys > 0)
            {
                Destroy(collision.gameObject);
                keys--;
                txtKeys.text = keys.ToString();
                mostrarDialogo("Has logrado escapar de la casa. Felicidades!", 4);
                audioPuerta.Play();
                panelCreditos.SetActive(true);
                panel.SetActive(false);
                esperar(2);
                Time.timeScale = 0;
            }
            else
            {
                audioPuertaLock.Play();
                Debug.Log("No tienes una llave para pasar por aqui");
                mostrarDialogo("Necesitas una llave para pasar por esta puerta!", 3);
            }
        }
    }

    IEnumerator esperar(int seconds)
    {

        yield return new WaitForSeconds(seconds);

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
