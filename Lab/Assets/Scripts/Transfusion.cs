//#define DEBUG_TRANSFUSION
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transfusion : MonoBehaviour
{
    private const float topLevel = 0.53f;
    private const float bottomLevel = 0f;//-0.26f;

    [SerializeField] GameObject Liquid;
    [Range(0,1)] public float FillAmount = 1f;
    [SerializeField, Range(0, 0.2f)] float speed; //максимальная пропускная способность горлышка
    Renderer _renderer;

    public Transfusion fillingFlask;

    public float liquidAway = -1;
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


    public void FixedUpdate()
    {
        TransfusionLogic();
    }

    private void TransfusionLogic()
    {
        if (FillAmount <= 0 || FillAmount > 1)
        {
            FillAmount = FillAmount <= 0 ? 0 : 1;
            liquidAway = 0;
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
            float rotation = unityRotation < 0.0000001f ? 90 : (180 - unityRotation) * Mathf.Sign(Mathf.Sin(unityRotation * Mathf.Deg2Rad)) - 90;

#if DEBUG_TRANSFUSION
        Debug.Log($"ALARM {unityRotation} {rotation} {Mathf.Sign(Mathf.Sin(unityRotation * Mathf.Deg2Rad))}");
        Debug.Log($"{maxAngle} {Mathf.Abs(rotation)}");
#endif

            if (maxAngle > rotation || rotation < 0)
            {
#if DEBUG_TRANSFUSION
            Debug.Log($"ALARM");
#endif
                if (maxAngle - rotation > speed)
                    liquidAway = speed;
                else
                    liquidAway = maxAngle - rotation;

                FillAmount -= liquidAway;
            }
            else
                liquidAway = 0;

            if (fillingFlask != null)
                fillingFlask.FillAmount += liquidAway;
        }
    }
}
