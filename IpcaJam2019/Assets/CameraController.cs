using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Material chromaticAberration;
    public float chromaticDisplacement = 0;
    bool applying = false;
    float timer = 0;

    private float shakeDuration = 0f;
    public float shakeMagnitude = 0.7f;
    private float dampingSpeed = 1.0f;
    public float defaultShakeDuration = 0.5f;

    // The initial position of the GameObject
    Vector3 initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        chromaticAberration = new Material(Shader.Find("CameraEffect/ChromaticAberration"));
        initialPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        chromaticDisplacement = Mathf.Clamp(chromaticDisplacement, 0, 0.008f);
        if (applying)
        {
            timer += 1 * Time.deltaTime;
            if (timer >= 0 && timer <= 0.3f)
            {
                chromaticDisplacement += 0.002f;
            }

            if (timer >= 0.3 && timer <= 0.5)
            {
                chromaticDisplacement -= 0.0008f;
            }

            if (timer >= 0.6)
            {
                chromaticDisplacement = 0;
                applying = false;
                timer = 0;
            }
        }

        if (shakeDuration > 0)
        {
            transform.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;

            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeDuration = 0f;
            transform.localPosition = initialPosition;
        }
    }

    public void ShakeCamera()
    {
        shakeDuration = defaultShakeDuration;
    }

    public void ApplyChromatic()
    {
        applying = true;
        timer = 0;
    }

    IEnumerator Chromatic()
    {
        chromaticDisplacement += 0.01f;

        if (chromaticDisplacement >= 0.05f)
            chromaticDisplacement -= 0.01f;

        yield return null;
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        chromaticAberration.SetTexture("_MainTex", source);
        chromaticAberration.SetFloat("_Amount", chromaticDisplacement);
        Graphics.Blit(source, destination, chromaticAberration);
        //chromaticAberration = new Material(Shader.Find("CameraEffect/ChromaticAberration"));
    }


}
