using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHandler : MonoBehaviour {

	public int index;

	void setIndex(int i){
		index = i;
	}
	public int getIndex(){
		return index;
	}

    void OnCollisionEnter(Collision col)
    {
		//print ("Hit");
		//print (col.gameObject.CompareTag ("Player"));
		if (col.gameObject.CompareTag("Player")) {
            col.gameObject.GetComponent<PlayerHandler>().addItem(index);
			Destroy(this.gameObject);
        }
    }

    // Use this for initialization
    void Start () {
		tag = "Item";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
