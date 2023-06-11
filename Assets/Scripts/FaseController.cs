using System;
using System.Linq;
using UnityEngine;

public class FaseController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] buttonsOrder; 
    private float[] timeOfPressedButton;
    [SerializeField]
    private GameObject turnOnObject;
    //private bool isObjectActive;
    [SerializeField]
    private float[] limitsPosition;


    void Start()
    {
        this.timeOfPressedButton = new float[this.buttonsOrder.Length];
    }

    void Update()
    {
        if (this.turnOnObject == null)
        {
            return;
        }

        for (int index =0; index < this.buttonsOrder.Length; index++)
        {
            float time = 0;

            if (this.buttonsOrder[index].GetComponent<ButtonAction>().isActive)
            {
                time = this.buttonsOrder[index].GetComponent<ButtonAction>().timeOfActiveButton;
            }

            this.timeOfPressedButton[index] = time;
        }

        bool isInOrder = this.timeOfPressedButton.SequenceEqual(timeOfPressedButton.Where(x => x != 0).OrderBy(x => x));

        if(isInOrder == true)
        {
            //this.isObjectActive = true;
            TurnOnGameObjects();
        }
        else
        {
            TurnOffGameObjects();   
        }
    }

    private void TurnOnGameObjects()
    {
        if(this.turnOnObject == null)
        {
            return;
        }

        if(this.turnOnObject.transform.position.y <= this.limitsPosition[1])
        {
            return;
        }

        float positionY = this.turnOnObject.transform.position.y + (-5f * Time.deltaTime);

        this.turnOnObject.transform.position = new Vector2(this.turnOnObject.transform.position.x, positionY < this.limitsPosition[1] ? this.limitsPosition[1] : positionY);
    }

    private void TurnOffGameObjects()
    {
        if (this.turnOnObject == null)
        {
            return;
        }
        if (this.turnOnObject.transform.position.y >= this.limitsPosition[0])
        {
            return;
        }

        float positionY = this.turnOnObject.transform.position.y - (-5f * Time.deltaTime);

        this.turnOnObject.transform.position = new Vector2(this.turnOnObject.transform.position.x, positionY > this.limitsPosition[0] ? this.limitsPosition[0] : positionY); 
    }

}



