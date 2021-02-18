using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAgent : MonoBehaviour
{
    [SerializeField]
    private float m_Speed;
    [SerializeField]
    private Vector3? m_Target;

    private const float TOLERANCE = 0.1f;
    
    // Update is called once per frame
    void Update()
    {
        if (m_Target == null)
        {
            return;
        }
        float distance = (m_Target.Value - transform.position).magnitude;
        if (distance < TOLERANCE)
        {
            return;
        }
        
        Vector3 dir = (m_Target.Value - transform.position).normalized;
        Vector3 delta = dir * (m_Speed * Time.deltaTime);
        transform.Translate(delta);
    }

    public void SetTarget(Vector3 target)
    {
        m_Target = target;
    }
}
