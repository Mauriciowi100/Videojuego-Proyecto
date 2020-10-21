using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CargaLevel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CargarNivel (string NombreNivel)
    {
        SceneManager.LoadScene(NombreNivel);
    }

    public void salir()
    {
        Application.Quit();
        Debug.Log("Saliste del juego");
    }
}
