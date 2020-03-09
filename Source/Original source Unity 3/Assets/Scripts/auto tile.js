var tiling : float = 10;

function Start()
{
	var texX: float = transform.localScale.x / tiling;
	var texY: float = transform.localScale.y / tiling;
	renderer.material.SetTextureScale ("_MainTex", Vector2(texX,texY));	
}