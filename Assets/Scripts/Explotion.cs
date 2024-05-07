
using UnityEngine;

public class Explotion : MonoBehaviour
{
    float time;
    public float existTime;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        GetComponent<AudioSource>().Play();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time >= existTime)
        {
            Destroy(this.gameObject);
        }
    }
}
