using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Enviroment : MonoBehaviour
{
    public Sprite[] skins;
    public int[] destroyedIds;

    [System.Serializable]
    public enum EnvType
    {
        TREE, CAR, BUILDING
    }

    public GameObject firePrefab;

    public EnvType type;

    private void OnDrawGizmos()
    {
        transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.y), Mathf.RoundToInt(transform.position.z));
    }

    private void Start()
    {
        var skin = transform.Find("Skin");
        var flip = Random.Range(0, 2) == 1;
        if (flip)
        {
            skin.transform.localScale = new Vector3(-skin.transform.localScale.x, skin.transform.localScale.y, skin.transform.localScale.z);
        }
        switch (type)
        {
            case EnvType.BUILDING:
            case EnvType.TREE:
                skin.position += new Vector3(0f, 0.5f, 0f);
                break;
            case EnvType.CAR:
                skin.position += new Vector3(0f, 0.255f, 0f);
                break;
        }
        var spriterend = skin.GetComponent<SpriteRenderer>();
        var id = Random.Range(0, skins.Length);
        if (Random.Range(0, 100) < 20 && destroyedIds.Contains(id))
        {
            StartCoroutine(LateFire(skin));
        }

        spriterend.sprite = skins[id];
        spriterend.color = Color.white;
    }

    private IEnumerator LateFire(Transform target)
    {
        yield return new WaitForSeconds(Random.Range(0f, 1f));
        Instantiate(firePrefab, target);

    }

}
