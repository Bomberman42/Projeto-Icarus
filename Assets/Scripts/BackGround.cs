using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    [SerializeField]
    private GameObject image1;
    [SerializeField]
    private Color imageColor1;
    [SerializeField]
    private GameObject image2;
    [SerializeField]
    private Color imageColor2;
    [SerializeField]
    private GameObject image3;
    [SerializeField]
    private Color imageColor3;
    [SerializeField]
    private GameObject image4;
    [SerializeField]
    private Color imageColor4;
    [SerializeField]
    private GameObject image5;
    [SerializeField]
    private Color imageColor5;

    void Start()
    {
        this.image1.GetComponent<SpriteRenderer>().color = this.imageColor1;
        this.image2.GetComponent<SpriteRenderer>().color = this.imageColor2;
        this.image3.GetComponent<SpriteRenderer>().color = this.imageColor3;
        this.image4.GetComponent<SpriteRenderer>().color = this.imageColor4;
        this.image5.GetComponent<SpriteRenderer>().color = this.imageColor5;
    }
}
