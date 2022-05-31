using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiretkaScript : MonoBehaviour
{
    [Range(0,1)]
    public float fillAmount;
    [Range(0, 0.1f)]
    public float speed;
    [Range(0, 0.1f)]
    public float dropSpeed;

    private bool isFilling;
    private GameObject drop;

    private void Start()
    {
        drop = transform.GetChild(1).gameObject;
        fillAmount = 0;
        isFilling = false;
    }

    private void Update()
    {
        if (isFilling && fillAmount < 1)
        {
            fillAmount = Mathf.Clamp01(speed + fillAmount);
            gameObject.GetComponent<MeshRenderer>().materials[1].SetFloat("_FillAmount", fillAmount);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("flask"))
        {
            // разветвление, варианты пустая биретка, аналогичный раствор, другой раствор
            //пустая
            if (fillAmount == 0)
            {
                var materials = new Material[]
                {
                    gameObject.GetComponent<MeshRenderer>().materials[0],
                    collision.transform.GetChild(0).GetComponent<MeshRenderer>().material
                };
                gameObject.GetComponent<MeshRenderer>().materials = materials;
                isFilling = true;
            }
            // аналогично
            else if (fillAmount > 0)// && gameObject.GetComponent<MeshRenderer>().materials[1].name == collision.transform.GetChild(0).GetComponent<MeshRenderer>().material.name)
            {
                isFilling = true;
                // а здесь точно что-то надо?
            }   
            else
            {
                // показать баннер вида "бюретка грязная"
                // положить бюретку на место
                // пока заглушка
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        isFilling = false;
    }

    private void OnMouseDown()
    {
        if (fillAmount > 0 && gameObject.GetComponent<Rigidbody>().isKinematic && dropSpeed > 0)
        {
            var obj = Instantiate(drop);
            obj.transform.position = drop.transform.position;
            obj.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", gameObject.GetComponent<MeshRenderer>().materials[1].GetColor("_TopColor")); 
            obj.SetActive(true);

            fillAmount = Mathf.Clamp01(fillAmount - dropSpeed);
            if (fillAmount > 0)
                gameObject.GetComponent<MeshRenderer>().materials[1].SetFloat("_FillAmount", fillAmount);
            else
                gameObject.GetComponent<MeshRenderer>().materials = new Material[] { gameObject.GetComponent<MeshRenderer>().materials[0] };
        }
    }
}
