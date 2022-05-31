using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleDegreeJoint : MonoBehaviour
{
    public enum JointDegree
    {
        RotateX = 0,
        RotateY = 1,
        RotateZ = 2
    }
    public JointDegree degreeOfFreedom;
    private Vector3 _axis;
    void Start()
    {/*
        _axis = degreeOfFreedom switch
        {
            JointDegree.RotateX => new Vector3(1, 0, 0),
            JointDegree.RotateY => new Vector3(0, 1, 0),
            JointDegree.RotateZ => new Vector3(0, 0, 1),
            _ => _axis
        };//*/
        degreeOfFreedom = JointDegree.RotateZ;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetValue(float value)
    {
        switch (degreeOfFreedom)
        {
            case JointDegree.RotateX:
                transform.localEulerAngles = new Vector3(value, 0, 0);
                break;
            case JointDegree.RotateY:
                transform.localEulerAngles = new Vector3(0, value, 0);
                break;
            case JointDegree.RotateZ:
                transform.localEulerAngles += new Vector3(0, 0, value);
                break;
        }

        
    }

    public Vector3 GetAxis()
    {
        switch (degreeOfFreedom)
        {
            case JointDegree.RotateX:
                return new Vector3(1, 0, 0);
            case JointDegree.RotateY:
                return new Vector3(0, 1, 0);
            case JointDegree.RotateZ:
                return new Vector3(0, 0, 1);
        }

        Debug.LogError("Axis out of range");
        return new Vector3(0, 0, 0);
    }

    /// <summary>
    /// смена оси, циклично
    /// </summary>
    public void ChangeAxis()
    {
        switch (degreeOfFreedom)
        {
            case JointDegree.RotateX:
                degreeOfFreedom = JointDegree.RotateY;
                break;
            case JointDegree.RotateY:
                degreeOfFreedom = JointDegree.RotateZ;
                break;
            case JointDegree.RotateZ:
                degreeOfFreedom = JointDegree.RotateX;
                break;
        }
    }
}



/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleDegreeJoint : MonoBehaviour
{
    public enum JointDegree
    {
        RotateX = 0,
        RotateY = 1,
        RotateZ = 2
    }
    public JointDegree degreeOfFreedom;
    private Vector3 _axis;
    void Start()
    {/*
        _axis = degreeOfFreedom switch
        {
            JointDegree.RotateX => new Vector3(1, 0, 0),
            JointDegree.RotateY => new Vector3(0, 1, 0),
            JointDegree.RotateZ => new Vector3(0, 0, 1),
            _ => _axis
        };//*

        switch (degreeOfFreedom)
        {
            case JointDegree.RotateX:
                _axis = new Vector3(1, 0, 0);
                break;
            case JointDegree.RotateY:
                _axis = new Vector3(0, 1, 0);
                break;
            case JointDegree.RotateZ:
                _axis = new Vector3(0, 0, 1);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    public void SetValue(float value)
    {/*
        transform.localEulerAngles = degreeOfFreedom switch
        {
            JointDegree.RotateX => new Vector3(value, 0, 0),
            JointDegree.RotateY => new Vector3(0, value, 0),
            JointDegree.RotateZ => new Vector3(0, 0, value),
            _ => transform.localEulerAngles
        };//*

        switch (degreeOfFreedom)
        {
            case JointDegree.RotateX:
                transform.localEulerAngles = new Vector3(value, 0, 0);
                break;
            case JointDegree.RotateY:
                transform.localEulerAngles = new Vector3(0, value, 0);
                break;
            case JointDegree.RotateZ:
                transform.localEulerAngles = new Vector3(0, 0, value);
                break;
        }
    }

    public float GetValue()
    {
        return transform.rotation.eulerAngles[(int) degreeOfFreedom];
    }
    
}

//*/