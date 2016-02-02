/**
  Written by Jailson Brito <jailson@gmail.com> during the Global Game Jam 2016 :)

  How to use this:
  1. Add this Behavior to the First Person Controller
  2. Add the tag _pickupTag to game objects that can be dragged;
**/

using UnityEngine;
using System.Collections;

public class PickupDropdown : MonoBehaviour
{

    [SerializeField]
    float _pickupRange = 5.00f;
    [SerializeField]
    string _pickupTag = "pickup";

    private bool _isHover = false;
    private bool _isHolding = false;
    private GameObject _target;
    Vector3 _screenSpace;
    Vector3 _offset;

    // Update is called once per frame
    void Update()
    {

        Transform cameraTransform = transform.GetChild(0).transform;
<<<<<<< HEAD
        
		Vector3 fwd = cameraTransform.TransformDirection(Vector3.forward);
		RaycastHit hit;
        
		if (Physics.Raycast(cameraTransform.position, fwd, out hit) )
		{
            //Debug.Log(hit.collider.gameObject.name);
			if (hit.distance <= _pickupRange && hit.collider.gameObject.tag == _pickupTag) {
				_isHover = true;
=======
        //Debug.Log(cameraTransform.position + ", " + cameraTransform.rotation);

        Vector3 fwd = cameraTransform.TransformDirection(Vector3.forward);
        RaycastHit hit;
>>>>>>> origin/master

        //Debug.Log(fwd.x + ", " + fwd.y + ", " + fwd.z);

        if (Physics.Raycast(cameraTransform.position, fwd, out hit))
        {
            Debug.Log(hit.collider.gameObject.name);
            if (hit.distance <= _pickupRange && hit.collider.gameObject.tag == _pickupTag)
            {
                _isHover = true;

                if (Input.GetMouseButtonDown(0))
                {
                    _target = hit.collider.gameObject;
                    hold();
                }
            }
            else
            {
                _isHover = false;
            }
        }

        if (_isHolding)
        {
            var curScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenSpace.z);
            var curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + _offset;
            _target.transform.position = curPosition;
        }

<<<<<<< HEAD
	void hold() 
	{
		_isHolding = true;
		_screenSpace = Camera.main.WorldToScreenPoint(_target.transform.position);
        _target.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
		_offset = _target.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenSpace.z));
	}
=======
        if (Input.GetMouseButtonUp(0))
        {
            _isHolding = false;
        }
    }

    void OnGUI()
    {
        if (_isHover == true && _isHolding == false)
        {
            GUI.Box(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 12, 100, 24), "Click to hold");
        }
        else if (_isHolding == true)
        {
            GUI.Box(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 12, 100, 24), "Release to drop");
        }
    }

    void hold()
    {
        _isHolding = true;
        _screenSpace = Camera.main.WorldToScreenPoint(_target.transform.position);
        _offset = _target.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenSpace.z));
    }
>>>>>>> origin/master
}
