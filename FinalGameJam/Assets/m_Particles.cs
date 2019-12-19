using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class m_Particles : MonoBehaviour
{
    Vector3 reference;
    public float speed;
    float step;
    // Start is called before the first frame update
    void Start()
    {
        reference = new Vector3(0, 2, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(1,0,0) * -speed * Time.deltaTime;   
    }
}
