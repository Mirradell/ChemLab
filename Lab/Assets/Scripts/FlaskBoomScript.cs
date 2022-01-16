using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlaskBoomScript : MonoBehaviour
{
    public Rigidbody floor;
    public List<GameObject> parts;
    [Range(0f, 1f)]
    public float radius;

    private Rigidbody flask;
    [SerializeField]
    private float piecesAmount;
    private float PrevY;

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
        var speed = Mathf.Sqrt(20 * (flask.position.y - floor.position.y));
        piecesAmount = 0;
        if (speed > 3f)
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
    }

    private void OnMouseUp()
    {
        BoomData();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Floor") && piecesAmount > 0)
        {
            CountPartsSpeed();
            piecesAmount = 0;
            Destroy(gameObject);
        }
    }
}
