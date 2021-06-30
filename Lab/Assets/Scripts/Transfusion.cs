//#define DEBUG_TRANSFUSION
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transfusion : MonoBehaviour
{
    private const float topLevel = 0.53f;
    private const float bottomLevel = -0.26f;

    [SerializeField] GameObject Liquid;
    [SerializeField, Range(0,1)] float FillAmount = 1f;
    Renderer _renderer;
    float realAmount
    {
        get
        {
            return
                (1 - FillAmount) * (topLevel - bottomLevel) + bottomLevel;
        }
    }
    
    void Start()
    {
        _renderer = Liquid.GetComponent<Renderer>();
    }


    void Update()
    {
        TransfusionLogic();
    }

    private void TransfusionLogic()
    {
        _renderer.material.SetFloat("_FillAmount", realAmount);
        float maxAngle = Mathf.Asin(FillAmount) * Mathf.Rad2Deg;
        float unityRotation = transform.rotation.eulerAngles.z;
        // HACK:
        // формула вывода угла наклона колбы
        float rotation = (180 - unityRotation) * Mathf.Sign(Mathf.Sin(unityRotation * Mathf.Deg2Rad)) - 90;
        if (rotation < 0)
            rotation += 360;
#if DEBUG_TRANSFUSION
        Debug.Log($"ALARM {unityRotation} {rotation} {Mathf.Sign(Mathf.Sin(unityRotation * Mathf.Deg2Rad))}");
        Debug.Log($"{maxAngle} {Mathf.Abs(rotation)}");
#endif

        if (maxAngle > rotation)
        {
#if DEBUG_TRANSFUSION
            Debug.Log($"ALARM");
#endif

            FillAmount -= (maxAngle - rotation) * 0.01f;
        }
    }
}
