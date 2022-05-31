using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Titrovanie : MonoBehaviour
{
    // TODO вывести для колбы и капли
    private const float oneInColor = 1 / 255.0f;

    public GameObject Liquid;
    private Material liquidMaterial;

    public bool isFilling;
    [Range(0, 1)]
    public float dropSpeed;

    // Start is called before the first frame update
    void Start()
    {
        liquidMaterial = Liquid.GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isFilling)
        {
            ChangeColor(1);
        }
    }

    public void ChangeColor(float diff)
    {
        // здесь красивый вывод цвета
        // вместо ontInColor сделать умную формулу
        var prevColor = liquidMaterial.GetColor("_TopColor");
        if (prevColor.b < 171 * oneInColor)
            prevColor.b += oneInColor;
        else
            prevColor.r -= oneInColor;
        liquidMaterial.SetColor("_TopColor", prevColor);
    }

    public void Drop()
    {
        ChangeColor(0.1f);
    }
}
