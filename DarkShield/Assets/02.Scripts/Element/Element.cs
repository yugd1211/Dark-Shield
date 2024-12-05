using UnityEngine;

public class Element : MonoBehaviour
{
	public enum Type
	{
		Void,
		Fire,
		Water,
	}
	
	public Type type;
	public Color color;
	public float damage;
	public float scale;
}
