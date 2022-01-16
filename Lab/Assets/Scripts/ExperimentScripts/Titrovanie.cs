using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Titrovanie : MonoBehaviour
{
    private const float oneInColor = 1 / 255.0f;

    public GameObject Liquid;
    private Material liquidMaterial;

    public bool isFilling;

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
            var prevColor = liquidMaterial.GetColor("_TopColor");
            if (prevColor.b < 171 * oneInColor)
                prevColor.b += oneInColor;
            else
                prevColor.r -= oneInColor;
            liquidMaterial.SetColor("_TopColor", prevColor);
        }
    }
}
