using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketController : MonoBehaviour
{
    public AudioClip appleSE;
    public AudioClip bombSE;
    AudioSource audioSource;
    GameObject director;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        this.audioSource = GetComponent<AudioSource>();
        this.director = GameObject.Find("GameDirector");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit, Mathf.Infinity)){
                float x = Mathf.RoundToInt(hit.point.x);
                float z = Mathf.RoundToInt(hit.point.z);
                transform.position = new Vector3(x, 0, z);
            }
        }
    }
    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Apple"){
            //Debug.Log("Tag = Apple");
            this.audioSource.PlayOneShot(this.appleSE);
            this.director.GetComponent<GameDirector>().GetApple();
        }
        else{
            //Debug.Log("Tag = Bomb");
            this.audioSource.PlayOneShot(this.bombSE);
            this.director.GetComponent<GameDirector>().GetBoomb();
        }
        Destroy(other.gameObject);
    }
}
