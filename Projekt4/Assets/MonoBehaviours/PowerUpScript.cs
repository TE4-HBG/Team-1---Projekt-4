using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PowerUpScript : MonoBehaviour
{
    public GameObject SPINNER;
    public AnimationCurve moveCurve;
    public PowerUp powerUp;
    public float rotationSpeed = 360f;
    private void Update()
    {
        SPINNER.transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.HasLayer(Layer.Player))
        {
            
            other.gameObject.GetComponent<Rat>().PowerUp = this.powerUp;


            Destroy(this.gameObject);
        }
    }
    public IEnumerator MoveUp(float secs, int steps)
    {
        for (int i = 0; i <= steps; i++)
        {
            this.transform.localPosition = new Vector3(0f, moveCurve.Evaluate((float)i/steps), 0f);
            yield return new WaitForSeconds(secs / steps);
        }
        yield return null;
    }
}