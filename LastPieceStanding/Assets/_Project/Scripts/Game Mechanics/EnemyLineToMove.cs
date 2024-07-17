using System.Collections;
using UnityEngine;

public class EnemyLineToMove : MonoBehaviour
{

    [SerializeField] private Material m_Material;
    [SerializeField] private float m_Speed = 1f;
    
    private Coroutine m_Coroutine;

    
    
    
    
    public void UpdateLineCoordinates(float zLength, float yRotation)
    {
        transform.eulerAngles = new Vector3(0, yRotation, 0);
        var lineRenderer = GetComponent<LineRenderer>();
        Vector3[] positions = new Vector3[2] ;
        lineRenderer.GetPositions(positions);
        Vector3 newPosition = new Vector3(positions[0].x, 0f, zLength);
        positions[1] = newPosition;
        lineRenderer.SetPositions(positions);
    }
    
    private void OnEnable()
    {
        m_Coroutine = StartCoroutine(AnimateLine());
    }

    private void OnDisable()
    {
        if (m_Coroutine != null)
        {
            StopCoroutine(m_Coroutine);
        }
    }


    IEnumerator AnimateLine()
    {
        m_Material = GetComponent<LineRenderer>().material;
        float m_XOffset = 0f;

        while (true)
        {
            m_XOffset += Time.deltaTime * m_Speed;
            m_Material.mainTextureOffset = new Vector2(m_XOffset,0);
            yield return new WaitForEndOfFrame();
        }
    }
}
