using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform lookAt;
    Vector3 positionIncrease;
    public float distance;
    float fixedDist;
    float sensitivityWheel;



    private void Start()
    {
        this.positionIncrease = this.lookAt.forward * this.distance;  // Calcula el incremento de la posicion
        transform.position = this.lookAt.position - this.positionIncrease; //Creamos una posicion orbital
        transform.LookAt(this.lookAt);                                     // La camara mira al mainCam (puntero)
        this.sensitivityWheel = 3f;
    
    }

    void Update()
    {
        this.distance += Input.GetAxis("Mouse ScrollWheel") * this.sensitivityWheel; //Zoom
        this.distance = Mathf.Clamp(distance, 2f, 6f); //Limita la distancia (zoom) para el rango {2,6}
        this.fixedDist = fixDistance();                 //Calculamos la distancia corregida    
    }

    private void LateUpdate()
    {
        this.positionIncrease = this.lookAt.forward * this.fixedDist;
        transform.position = Vector3.Lerp(transform.position, this.lookAt.position - this.positionIncrease, 25f * Time.deltaTime);
        transform.LookAt(this.lookAt);
    }

    float fixDistance()
    {
        RaycastHit hit;                        // Guarda la informacion de la collision
        LayerMask layerMask = 1 << 8;          // Asignamos el layer 8 del player  
        layerMask = ~layerMask;                // Invierte el layer para que colisione con cualquier layer excepto este


        if (Physics.Raycast(this.lookAt.position, -this.lookAt.forward, out hit, this.distance, layerMask))
        {
            Debug.DrawLine(this.lookAt.position, hit.point, Color.red);
            return hit.distance;
        }
        return this.distance;
    }
}
