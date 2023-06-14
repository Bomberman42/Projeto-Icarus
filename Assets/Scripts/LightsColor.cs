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
        this._render.color = HexToColor("#7B7070");
    }

    public void ErrorEffector()
    {
        this._render.color = Color.red;
    }


    private Color HexToColor(string hex)
    {
        // Remover o símbolo "#" do valor hexadecimal
        hex = hex.Replace("#", "");

        // Converter o valor hexadecimal em componentes RGB
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);

        // Criar e retornar o objeto Color
        return new Color32(r, g, b, 255);
    }


}
