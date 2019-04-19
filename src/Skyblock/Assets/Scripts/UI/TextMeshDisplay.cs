using UnityEngine;
using System.Collections;

public class TextMeshDisplay : MonoBehaviour {

    [SerializeField]
	private Vector3 moveRate = new Vector3(0, 2F, 0);
    [SerializeField]
	private float lifeSpan = 1.0F;
    [SerializeField]
	private float fadeRate = 2F;

	private TextMesh mesh;
    private Renderer textRenderer;

	void Awake ()
    {
        mesh = GetComponent<TextMesh>();
        textRenderer = GetComponent<Renderer>();

		textRenderer.sortingLayerName = "UI";
		textRenderer.sortingOrder = 1;
	}


    void OnEnable()
    {
        mesh.color = new Color(mesh.color.r, mesh.color.g, mesh.color.b, 1f);
        StartCoroutine(DisplayTextAndDie());
    }


    private IEnumerator DisplayTextAndDie()
    {
        for (float timer = 0; timer < lifeSpan; timer += Time.deltaTime )
        {
            yield return null;
            transform.position += moveRate * Time.deltaTime;
            mesh.color = new Color(mesh.color.r, mesh.color.g, mesh.color.b, mesh.color.a - fadeRate * Time.deltaTime);
        }

        Destroy(this.gameObject);
        //this.gameObject.SetActive(false);
    }


	public void SetText(string newText)
	{
		mesh.text = newText;
	}


	public void SetColor(Color color)
	{
		mesh.color = color;
	}
}
