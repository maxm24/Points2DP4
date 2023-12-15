using UnityEngine;

public class Move : MonoBehaviour {

    public float speed = 2.0f;
    public float accuracy = 0.01f;
    public Transform goal;

    void Start() {

        
    }

    void Update() {
        Vector3 direction = goal.position - this.transform.position;
        if(direction.magnitude >  accuracy);
        this.transform.Translate(direction.normalized * speed * Time.deltaTime);
    }
}
