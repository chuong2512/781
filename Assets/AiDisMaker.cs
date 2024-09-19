using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiDisMaker : MonoBehaviour
{

    public List<GameObject> Agents = new List<GameObject>();
    public List<Transform> HidingSpots = new List<Transform>();
    void Start()
    {
        HidingSpots.AddRange(HidePos.init.CurrentHidePosition);
        for (int i = 0; i < Agents.Count; i++)
        {
            int randomTextureIndex = Random.Range(0, GameManager.init.Chara_Textures.Count);
            Agents[i].GetComponent<AiHider>().Texture.materials[0].mainTexture = GameManager.init.Chara_Textures[randomTextureIndex];
            print("random index is: " + randomTextureIndex);
            GameManager.init.Chara_Textures.RemoveAt(randomTextureIndex);
        }
        StartCoroutine(SetPositionDelay());
    }

    IEnumerator SetPositionDelay()
    {
       
        yield return new WaitForSeconds(10f);
        for (int i = 0; i < Agents.Count; i++)
        {
            if (HidingSpots.Count<=0)
            {
                HidingSpots.AddRange( HidePos.init.CurrentHidePosition);
            }
            int randomIndex = Random.Range(0, HidingSpots.Count);
            Agents[i].GetComponent<AiHider>().Spot = HidingSpots[randomIndex];
            Agents[i].GetComponent<AiHider>().StartMoving = true;
            HidingSpots.RemoveAt(randomIndex);
          
        }
        
        print("Ai Should Move");
        StartCoroutine(SetPositionDelay());
    }
}
