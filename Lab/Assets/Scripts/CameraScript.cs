using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    Rigidbody capsule;
    Camera camera;
    // Start is called before the first frame update
    void Start()
    {
        // capsule = GetComponent<Rigidbody>();
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal") * .5f;// * 30;
        var vertical = Input.GetAxis("Vertical") * .5f;// * 20;

        if (horizontal != 0 || vertical != 0) // Движение камеры пострелочкам вправо/влево/вверх/вниз
        {
            var yNew = transform.localEulerAngles.y + horizontal;
            if (yNew < 0)
                yNew += 360;
            if (yNew < 340 && yNew > 180)
                yNew = 340;
            else if (yNew > 30 && yNew <= 180)
                yNew = 30;
            
            transform.localEulerAngles = new Vector3(Mathf.Clamp(transform.localEulerAngles.x - vertical, 0f, 20f),
                                                     yNew, 0);
        }

        if ((Input.GetKey(KeyCode.Plus) || Input.GetKey(KeyCode.KeypadPlus) || Input.GetKey(KeyCode.Equals)) && camera.fieldOfView > 10)
            camera.fieldOfView -= 0.1f;

        if ((Input.GetKey(KeyCode.Minus) || Input.GetKey(KeyCode.KeypadMinus)) && camera.fieldOfView < 60)
            camera.fieldOfView += 0.1f;
    }
}
