using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Cell : MonoBehaviour
{
    public enum Type { Neutral, Friend, Enemy}
    [SerializeField]private TextMesh patrticlesText;
    [SerializeField]private GameObject cellCenter;
    [SerializeField]private GameObject highlightedObj;
    [SerializeField]private AttackerParticles attackerParticles;
    public Type type;
    public int currentPatrticles;
    public int maxSize;
    private Material material;
    
    void Start()
    {
        currentPatrticles = maxSize-1;
        material = cellCenter.GetComponent<Renderer>().material;
        SetColor();
        StartCoroutine(FillingCell());
    }

    IEnumerator FillingCell()
    {
        while (true)
        {
            AddParticles(1);
            UpdateText();
            yield return new WaitForSeconds(0.8f);
        }
    }

    public void DivideCell(Cell target)
    {
        currentPatrticles = currentPatrticles / 2;
        
        AttackerParticles spawnedParticles = Instantiate(attackerParticles, this.transform.position, new Quaternion(0,0,0,0));
        spawnedParticles.InputData(currentPatrticles, target, type, GetColor());
    }


    private void OnTriggerEnter2D(Collider2D other)
    {       
        TouchInput script = other.GetComponentInParent<TouchInput>();
        switch (type)
        {           
            case Type.Friend:
                script.touchedCells.Add(this);
                highlightedObj.SetActive(true);
                break;

            default: 
                script.SetTarget(this);
                break;
        }
    }

    public void HighligthOff()
    {
        highlightedObj.SetActive(false);
    }

    private void UpdateText()
    {
        patrticlesText.text = currentPatrticles.ToString();
    }

    private void GrabCell(Type type)
    {
        this.type = type;
        SetColor();
        NextLevel.instance.CheckEndGame();
    }

    public void TakeDamage(int count, Type attacker)
    {
        if (this.type != attacker)
        {
            if (count >= currentPatrticles)
            {
                currentPatrticles = count - currentPatrticles;
                GrabCell(attacker);
            }
            else
            {
                currentPatrticles -= count;
            }
        }
        else
            AddParticles(count);
        UpdateText();
    }
    private void AddParticles(int count)
    {
        if(currentPatrticles + count < maxSize)
        {
            currentPatrticles += count;
        }
        else
        {
            currentPatrticles = maxSize;
        }
    }

    private void SetColor()
    {
        material.color = GetColor();
    }

    public Color GetColor()
    {
        switch (type)
        {
            case Type.Enemy:
                cellCenter.GetComponent<Renderer>().material.color = Color.red;
                return Color.red;
            case Type.Friend:
                return Color.green;
            case Type.Neutral:
                return Color.white;
            default: return Color.white;
        }
    }
}
