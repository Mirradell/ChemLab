using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlaskBoomScript : MonoBehaviour
{
    public Rigidbody floor;
    public GameObject puddle;
    public List<GameObject> parts;
    [Range(0f, 1f)]
    public float radius;

    private Rigidbody flask;
    [SerializeField]
    private float piecesAmount;
    [SerializeField]
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        flask = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void BoomData()
    {
        speed = Mathf.Sqrt(20 * (flask.position.y - floor.position.y));
        piecesAmount = 0;
        if (speed > 6f)
            piecesAmount = Random.Range(0, 30);

        if (Random.Range(0, 100) % 30 == 0)
            piecesAmount++;
    }

    private void CountPartsSpeed()
    {
        if (piecesAmount < 2) return;

        for (var i = 0; i < piecesAmount; i++)
        {
            var currentVector = Random.insideUnitSphere * radius; // генерация скорости, +-1 - разные направления
            if (currentVector.y < 0)
                currentVector = new Vector3(currentVector.x, Mathf.Abs(currentVector.y), currentVector.z);

            var part = Instantiate(parts[Random.Range(0, parts.Count)]).GetComponent<Rigidbody>();
            part.position = flask.position;// + new Vector3(currentVector.x, Mathf.Abs(currentVector.y), currentVector.z);
            part.transform.localScale = Vector3.Scale(part.transform.localScale, Random.insideUnitSphere);
            part.AddForce(currentVector * part.mass, ForceMode.Impulse);
        }

        if (gameObject.GetComponent<Transfusion>() != null)
        {
            var waterAmount = gameObject.GetComponent<Transfusion>().FillAmount;
            var puddleObj = Instantiate(puddle);
            puddleObj.transform.position = flask.position + new Vector3(0, 0.1f, 0);
            if (puddleObj.transform.position.y > 1.93f)
                puddleObj.transform.position = new Vector3(puddleObj.transform.position.x, 1.93f, puddleObj.transform.position.z);
            puddleObj.transform.localScale = Vector3.Scale(puddleObj.transform.localScale, new Vector3(waterAmount, 1, waterAmount));
            puddleObj.transform.GetChild(0).GetComponent<MeshRenderer>().material = gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material;
            puddleObj.transform.GetChild(0).GetComponent<MeshRenderer>().material.SetFloat("_FillAmount", 1);
        }
    }

    private void OnMouseUp()
    {
        BoomData();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Floor") && piecesAmount > 2)
        {
            CountPartsSpeed();
            piecesAmount = 0;
            Destroy(gameObject);
        }
    }
}
