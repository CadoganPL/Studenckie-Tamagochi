using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepController : MonoBehaviour
{
    [SerializeField]
    private float _velocity,_jumpStrength;
    public bool Active { get; set; }
    private bool _dead;
    private int _touchCountPreviousFrame;

    private Rigidbody _rb;
	// Use this for initialization
	void Start ()
	{
	    _rb = GetComponent<Rigidbody>();

        Active = true;
	    _dead = false;
	    if (_rb== null)
	    {
	        _rb =gameObject.AddComponent<Rigidbody>();
	    }
	}
	
	// Update is called once per frame
	void Update () {
	    if ((Active && Input.touchCount > _touchCountPreviousFrame) || (Active && Input.GetKeyDown(KeyCode.Space)))
	    {
	        Jump();
	    }
        if(!_dead)Move();
	    _touchCountPreviousFrame = Input.touchCount;
	}


    void Jump()
    {
        Active = false;
        GetComponent<Rigidbody>().AddForce(new Vector3(0, _jumpStrength,0));
        Debug.Log("jump");
    }

    void Move()
    {
        _rb.velocity = (new Vector3(_velocity, _rb.velocity.y, _rb.velocity.z));
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == "Fence")
        {
            for (int i = 0; i < 10; i++)
                EventManager.TriggerEvent("DecreaseProgressBar");
            _dead = true;
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Win")
        {
            for(int i =0; i<10;i++)
            EventManager.TriggerEvent("IncreaseProgressBar");
        }
    }
}
