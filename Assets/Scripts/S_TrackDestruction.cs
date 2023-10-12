using System.Collections;
using UnityEngine;

public class S_TrackDestruction : MonoBehaviour
{
    public Gradient colorGradient;
    public float timeToChangeColor = 2.0f;
    public float shakeDuration = 1.0f;
    public float shakeIntensity = 0.1f;

    private GameObject visualObject;
    private Renderer objectRenderer;
    private Vector3 initialVisualPosition;

    private void Start()
    {
        CreateVisualObject();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(ChangeColorAndDelete());
        }
    }

    private void CreateVisualObject()
    {
        visualObject = new GameObject("VisualObject");
        visualObject.transform.SetParent(transform);
        visualObject.transform.localPosition = Vector3.zero;
        visualObject.transform.localRotation = Quaternion.identity;
        visualObject.transform.localScale = Vector3.one;

        MeshFilter meshFilter = visualObject.AddComponent<MeshFilter>();
        meshFilter.mesh = GetComponent<MeshFilter>().mesh;

        MeshRenderer meshRenderer = visualObject.AddComponent<MeshRenderer>();
        meshRenderer.material = GetComponent<Renderer>().material;

        GetComponent<MeshFilter>().mesh = null;
        GetComponent<Renderer>().enabled = false;
    }

    private IEnumerator ChangeColorAndDelete()
    {
        objectRenderer = visualObject.GetComponent<Renderer>();
        initialVisualPosition = visualObject.transform.localPosition;

        float elapsedTime = 0.0f;
        while (elapsedTime < timeToChangeColor)
        {
            objectRenderer.material.color = colorGradient.Evaluate(elapsedTime / timeToChangeColor);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        elapsedTime = 0.0f;
        while (elapsedTime < shakeDuration)
        {
            visualObject.transform.localPosition = initialVisualPosition + Random.insideUnitSphere * shakeIntensity;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        visualObject.transform.localPosition = initialVisualPosition;
        Destroy(gameObject);
    }
}