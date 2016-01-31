using UnityEngine;

public class LoadLevel : MonoBehaviour
{
    public string nextLevel;
    void Update(){
        Application.LoadLevel ( nextLevel );
    }
    
    
}