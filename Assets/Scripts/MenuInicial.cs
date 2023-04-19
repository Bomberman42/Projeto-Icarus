using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuInicial : MonoBehaviour
{
    [SerializeField]
    private AudioSource musicaDoNaruto;
    [SerializeField]
    private AudioSource musicaDoJogo;

    void Start()
    {
        this.musicaDoNaruto.Play();
        this.musicaDoJogo.enabled = false;
    }

    void Update()
    {
        
    }
}
