using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocoDaMorte : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameControle.instance.FimDeJogo();
        }
    }
}
