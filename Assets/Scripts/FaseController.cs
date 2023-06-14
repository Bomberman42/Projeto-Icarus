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
    [SerializeField]
    private float[] limitsPosition;
    public GameObject light1;
    public GameObject light2;
    public GameObject light3;


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
            TurnOnGameObjects();
        }
        else
        {
            if(this.timeOfPressedButton.Where(x => x != 0).Count() == 3)
            {
                this.light1.GetComponent<LightsColor>().ErrorEffector();
                this.light2.GetComponent<LightsColor>().ErrorEffector();
                this.light3.GetComponent<LightsColor>().ErrorEffector();
            }
            else
            {
                if (this.timeOfPressedButton[0] > 0) 
                {
                    this.light1.GetComponent<LightsColor>().EnableEffector();
                }

                if (this.timeOfPressedButton[1] > 0)
                {
                    this.light2.GetComponent<LightsColor>().EnableEffector();
                }

                if (this.timeOfPressedButton[2] > 0)
                {
                    this.light3.GetComponent<LightsColor>().EnableEffector();
                }
            }

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



