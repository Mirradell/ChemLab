//#define DEBUG_TRANSFUSION
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transfusion : MonoBehaviour
{
    private const float topLevel = 0.53f;
    private const float bottomLevel = 0f;//-0.26f;

    [SerializeField] GameObject Liquid;
    [SerializeField, Range(0,1)] float FillAmount = 1f;
    [SerializeField, Range(0, 0.2f)] float speed; //максимальная пропускная способность горлышка
    Renderer _renderer;
    float realAmount
    {
        get
        {
            return
               FillAmount;// * topLevel;// - bottomLevel) + bottomLevel;
        }
    }
    
    void Start()
    {
        //  _renderer = Liquid.GetComponent<Renderer>();
        _renderer = Liquid.GetComponent<MeshRenderer>();
    }


    void Update()
    {
        TransfusionLogic();
    }

    private void TransfusionLogic()
    {
        if (FillAmount <= 0)
        {
            FillAmount = 0;
            _renderer.material.SetFloat("_FillAmount", FillAmount);
            return;
        }

        if (_renderer != null)
        {
            _renderer.material.SetFloat("_FillAmount", FillAmount);
            float maxAngle = Mathf.Asin(realAmount) * Mathf.Rad2Deg; // угол наклона выливания
            float unityRotation = transform.rotation.eulerAngles.z;
            // HACK:
            // формула вывода угла наклона колбы
            float rotation = (180 - unityRotation) * Mathf.Sign(Mathf.Sin(unityRotation * Mathf.Deg2Rad)) - 90;
            //float rotation = (90 - unityRotation) * Mathf.Sign(Mathf.Sin(unityRotation * Mathf.Deg2Rad));
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
                if (maxAngle - rotation > speed)
                    FillAmount -= speed;
                else
                    FillAmount -= maxAngle - rotation;
            }

            Debug.Log("maxAngle = " + maxAngle + "\tunityRotation = " + unityRotation 
                    + "\trotation = " + rotation + "\tFillAmount = " + realAmount);
        }
    }
}
