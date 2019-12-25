using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electrostat : MonoBehaviour
{
    public Rigidbody rb;
    float chargeValue;
    const float ElectrostaticConst = 90f;
    Material chargeColor;
    private void Start()
    {
        chargeValue = Random.Range(-3, 3);
        
    }
    private void FixedUpdate()
    {
        Electrostat[] charges = FindObjectsOfType<Electrostat>();
        foreach(Electrostat charge in charges)
        {
            if(charge != this)
            {
                if (charge.chargeValue > 0)
                {
                    chargeColor = new Material(Shader.Find("Diffuse"));
                    chargeColor.color = new Color(1, 0, 0);
                    charge.GetComponent<Renderer>().material = chargeColor;
                }
                else if (charge.chargeValue < 0)
                {
                    chargeColor = new Material(Shader.Find("Diffuse"));
                    chargeColor.color = new Color(0, 0, 1);
                    charge.GetComponent<Renderer>().material = chargeColor;
                }
                else
                {
                    chargeColor = new Material(Shader.Find("Diffuse"));
                    chargeColor.color = new Color(0, 1, 0);
                    charge.GetComponent<Renderer>().material = chargeColor;
                }
                CoulombicForce(charge);
                //Debug.Log(charge.chargeValue);
            }
        }
    }
    void CoulombicForce(Electrostat AnotherCharge)
    {
        Rigidbody charge2rb = AnotherCharge.rb;
        Vector3 separation = transform.position - AnotherCharge.transform.position;
        float distance = separation.magnitude;
        float forceMagnitude = (chargeValue * AnotherCharge.chargeValue) / Mathf.Pow(distance, 2);
        Vector3 forceVector = forceMagnitude * separation.normalized * ElectrostaticConst;
        charge2rb.AddForce(forceVector);
    }
}
