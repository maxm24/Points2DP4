using UnityEngine;

public class Move : MonoBehaviour {

    public float speed = 1.0f;
    public Transform goal;

    void Start() {

        
    }

    void Update() {
        Vector3 direction = goal.position - this.transform.position;
        if(direction.magnitude > 1);
        this.transform.Translate(direction.normalized * speed * Time.deltaTime);
    }
}
