using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackerParticles : MonoBehaviour
{
    private int count;
    private float speed = 3.5f;
    private float moveDelay = 0.1f;
    private Cell target;
    private Cell.Type type;

    [System.Obsolete]
    public void InputData(int count, Cell target, Cell.Type type, Color color)
    {
        this.count = count;
        this.target = target;
        this.type = type;
        GetComponentInChildren<ParticleSystem>().startColor = color;
    }
    private void Move()
    {    
        this.transform.position = Vector2.MoveTowards(this.transform.position, target.transform.position, speed*Time.deltaTime);
        if (this.transform.position == target.transform.position)
        {
            target.TakeDamage(count, type);
            Destroy(this.gameObject);
        }
    }
    IEnumerator Moving()
    {
        while (true)
        {
            Move();
            yield return new WaitForSeconds(moveDelay);
        }   
    }

    private void Start()
    {
        StartCoroutine(Moving());
    }
}