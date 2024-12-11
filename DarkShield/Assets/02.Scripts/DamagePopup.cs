using TMPro;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
	public TextMeshPro textMesh;

	public void SetText(string text)
	{
		textMesh.SetText(text);
	}

	private void Update()
	{
		transform.position += new Vector3(0, 0.5f) * Time.deltaTime;
	}
}
