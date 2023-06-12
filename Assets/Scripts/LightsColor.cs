using UnityEngine;

public class LightsColor : MonoBehaviour
{
    private SpriteRenderer _render;

    void Start()
    {
        this._render = GetComponent<SpriteRenderer>();
    }

    public void EnableEffector()
    {
        this._render.color = Color.green;
    }

    public void DisableEffector()
    {
        this._render.color = Color.red;
    }
}
