using UnityEngine;
using System.Collections;

public class ColetarItens : MonoBehaviour {

	
    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Arma2" )
            {

            other.gameObject.SetActive(false);

            }
        if (other.tag == "Arma1")
        {

            other.gameObject.SetActive(false);

        }
    }
}
