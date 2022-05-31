using UnityEngine;


public class Kinematics : MonoBehaviour
{
    public float speed = 2;
    public float[] solution;
    [Range(0, 1)]
    public float diff;
    public bool isInActor = false;

    private Vector3 target;
    private SingleDegreeJoint[] joints;
    private GameObject actor = null;

    // Start is called before the first frame update
    public void Beginning()
    {
        joints = GetComponentsInChildren<SingleDegreeJoint>();
        isInActor = false;
        solution = new float[joints.Length];

        // поиск Actor по тегу в детях(мы точно знаем, что ребенок всего один у каждого)
        var tmpObj = this.gameObject;
        while (tmpObj.transform.childCount > 0 || tmpObj.CompareTag("Actor"))
        {
            if (tmpObj.CompareTag("Actor"))
            {
                actor = tmpObj;
                break;
            }
            else
                tmpObj = tmpObj.transform.GetChild(0).gameObject;
        }

        if (actor == null)
            Debug.LogError("Cannot find actor in scene!!!");

        //Solve();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf)
            Solve();
    }

    public void Solve()
    {
        isInActor = false;
        //if (joints == null || actor == null)
            Beginning();

            for (var i = 0; i < joints.Length; i++)
            {
                var jointActor = actor.transform.position - joints[i].transform.position;
                var actorTarget = target - actor.transform.position;
                var jointTarget = target - joints[i].transform.position;

                var angle = Vector3.Angle(jointActor, jointTarget);
                if (angle > 1 && angle < 179)
                {
                    var point = Vector3.SignedAngle(jointActor, jointTarget, joints[i].GetAxis());
                    //  Debug.Log(point);
                    //solution[i] = Mathf.Sign(point);
                    joints[i].SetValue((point));


                    //if (!isInActor && (int)(Random.value * 100) % 33 == 0)
                    //  joints[i].ChangeAxis();
                }
            }
    }
    
    public void SetTarget(Vector3 _target)
    {
        target = _target;
    }

    public void SetWater(Material water)
    {
        var color = water.GetColor("_TopColor");
        var tmp = gameObject;
        while (!tmp.CompareTag("Actor")) 
        {
            tmp.GetComponent<MeshRenderer>().material.SetColor(Shader.PropertyToID("_EmissionColor"), color);
            tmp = tmp.transform.GetChild(0).gameObject;
        }
        tmp.GetComponent<MeshRenderer>().material.SetColor(Shader.PropertyToID("_EmissionColor"), color);
    }

    private Vector3 PredictNewPos(Vector3 previous, Vector3 angle)
    {
        Vector3 res = previous;
        var radAngle = angle.z * Mathf.Deg2Rad;
        if (angle.z != 0)
            return new Vector3(previous.x * Mathf.Cos(radAngle) + previous.y * Mathf.Sin(radAngle),
                              -previous.x * Mathf.Sin(radAngle) + previous.y * Mathf.Cos(radAngle),
                               previous.z);

        
        return res;
    }

    private float InRange(float value)
    {
        while (value < 0)
        {
            value += 360;
        }

        while (value > 360)
        {
            value -= 360;
        }

        return value;
    }
}

/* 
             var jointActor  = actor.transform.position - joints[i].transform.position;
             var actorTarget = target.transform.position - actor.transform.position;
             var jointTarget = target.transform.position - joints[i].transform.position;
            
             var angle = Vector3.Angle(jointActor, jointTarget);
             if (angle > 1 && angle < 179)
             {
                 var point = Vector3.SignedAngle(jointActor, jointTarget, joints[i].GetAxis());

                 Debug.Log(point);
                 joints[i].SetValue(Mathf.Sign(point));

             }//*/


/*using UnityEngine;


public class Kinematics : MonoBehaviour
{
    private SingleDegreeJoint[] joints;
    public float speed = 2;
    public float[] solution;
    // Start is called before the first frame update
    void Start()
    {
        joints = GetComponentsInChildren<SingleDegreeJoint>();
        solution = new float[joints.Length];
        for (var i = 0; i < joints.Length; i++)
        {
            solution[i] = joints[i].GetValue();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Solve();
        for (var i = 0; i < solution.Length; i++)
        {
            joints[i].SetValue(solution[i]);
        }
    }

    private void Solve()
    {
        var delta = Time.deltaTime * speed;
        for (var i = 0; i < solution.Length; i++)
        {
            solution[i] = InRange(solution[i] + (i+1)*delta);
            
        }
    }

    private float InRange(float value)
    {
        while (value < 0)
        {
            value += 360;
        }

        while (value > 360)
        {
            value -= 360;
        }

        return value;
    }
}
//*/