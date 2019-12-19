using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundPoundPuzzle : MonoBehaviour
{
    MeshCollider[] mesh;
    Rigidbody[] body;

    public float upWards;
    [SerializeField]
    bool isPost = false;
    [SerializeField]
    bool isWall = false;
    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponentsInChildren<MeshCollider>();
        body = GetComponentsInChildren<Rigidbody>();
       // particles = GetComponentInChildren<ParticleSystem>();

        foreach (var item in mesh)
        {
            item.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
     //   if (Input.GetKeyDown(KeyCode.LeftAlt)) InteractWithMe();
    }
    public void DontInteractWithMe()
    {
        throw new System.NotImplementedException();
    }

    public void InteractWithMe()
    {
        GetComponent<BoxCollider>().enabled = false;

        foreach (var item in mesh)
        {
            item.enabled = true;
        }
        foreach (var item in body)
        {
            item.useGravity = true;
            item.AddForce(Vector3.up * 10, ForceMode.Impulse);
        }

        StartCoroutine(DisableShit());
    }

    IEnumerator DisableShit()
    {
        yield return new WaitForSeconds(0);
        foreach (var item in mesh)
        {
            item.enabled = false;
        }
        yield return new WaitForSeconds(6);
        Destroy(this.gameObject);
    }
}
