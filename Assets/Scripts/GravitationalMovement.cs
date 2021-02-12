using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitationalMovement : MonoBehaviour
{
    [SerializeField]
    private Vector3 m_Speed;
    [SerializeField]
    private float m_TargetMass;
    [SerializeField]
    private Vector3 m_Target;
    [SerializeField]
    private float m_Gravity;
    
    private const float TOLERANCE = 0.1f;

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 diff = (m_Target - transform.position);
        float distance = diff.magnitude;
        if (distance < TOLERANCE)
        {
            return;
        }
        
        Vector3 momentum = diff * (m_Gravity * m_TargetMass) / (distance * distance * distance);
        Vector3 delta = m_Speed * Time.fixedDeltaTime + momentum * (Time.fixedDeltaTime * Time.fixedDeltaTime / 2);
        m_Speed += momentum * Time.fixedDeltaTime;
        transform.Translate(delta);
    }
}
