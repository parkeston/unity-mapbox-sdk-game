using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UfoConttroller : MonoBehaviour
{
    [SerializeField] private float maxSpeed;
    [SerializeField] private float reloadTime;

    [SerializeField] private GameObject spotPreview;

    [SerializeField] private LayerMask mask;
    // Start is called before the first frame update

    private float timer;

    private new Rigidbody rigidbody;
    private new Camera camera;
    private GameObject hittedGO;
    private GameObject aim;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 input = new Vector3(Input.GetAxis("Horizontal2"),Input.GetAxis("Vertical2"),0)*maxSpeed;
        input = Vector3.ClampMagnitude(input, maxSpeed);
        
        transform.Translate(input*Time.deltaTime);
        //rigidbody.AddForce(input*maxSpeed,ForceMode.Acceleration);

        if (Physics.SphereCast(transform.position, 3, Vector3.down, out RaycastHit info, 500, mask, QueryTriggerInteraction.Ignore))
        {
            if (aim == null)
                aim = Instantiate(spotPreview);
            
            aim.transform.position = info.point;
        }

        if (Input.GetKeyDown(KeyCode.Return) && Time.time>timer)
        {
            print("shoot");
            timer = Time.time + reloadTime;
            if (Physics.SphereCast(transform.position, 3, Vector3.down, out RaycastHit hit))
            {
                if (hit.collider.CompareTag("Player") && hit.collider.GetComponent<MeshRenderer>().enabled)
                {
                    Destroy(hit.collider.gameObject);
                    UIManager.Instance.Win("Ufo win!");
                }
            }
        }
    }
}
