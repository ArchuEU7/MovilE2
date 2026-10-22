
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    [Header("Referencias")]
    public GameObject groundPrefab;
    public Transform player;

    [Header("Configuracion")]
    public int initialSegments = 4;
    public float groundY = -3f;
    public float startX = 0f;
    public float spawnAheadDistance = 25f;

    private readonly List<GameObject> segments = new List<GameObject>();
    private float segmentWidth;

    private void Start()
    {
        if (groundPrefab == null || player == null)
        {
            Debug.LogError("GroundSpawner: faltan referencias.");
            enabled = false;
            return;
        }

        SpriteRenderer sr = groundPrefab.GetComponent<SpriteRenderer>();

        if (sr == null || sr.sprite == null)
        {
            Debug.LogError("El prefab necesita un SpriteRenderer con sprite.");
            enabled = false;
            return;
        }

        segmentWidth =
            sr.sprite.bounds.size.x *
            Mathf.Abs(groundPrefab.transform.localScale.x);

        if (segmentWidth <= 0f)
        {
            Debug.LogError("El ancho del terreno no es valido.");
            enabled = false;
            return;
        }

        initialSegments = Mathf.Max(2, initialSegments);

        for (int i = 0; i < initialSegments; i++)
        {
            GameObject segment = Instantiate(
                groundPrefab,
                new Vector3(startX + i * segmentWidth, groundY, 0f),
                Quaternion.identity
            );

            segments.Add(segment);
        }
    }

    private void Update()
    {
        if (segments.Count == 0)
            return;

        while (player.position.x + spawnAheadDistance >
               segments[segments.Count - 1].transform.position.x
               + segmentWidth * 0.5f)
        {
            GameObject firstSegment = segments[0];
            GameObject lastSegment = segments[segments.Count - 1];

            firstSegment.transform.position = new Vector3(
                lastSegment.transform.position.x + segmentWidth,
                groundY,
                0f
            );

            segments.RemoveAt(0);
            segments.Add(firstSegment);
        }
    }
}
