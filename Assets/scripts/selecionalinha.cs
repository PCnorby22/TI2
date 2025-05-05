using UnityEngine;

public class selecionalinha : MonoBehaviour
{
    int x = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        x = Random.Range(0, 3);
    }
    public int linha()
    {
        return this.x;
    }
}
