using UnityEngine;
using System.Collections;

public class SortParticleSystem : MonoBehaviour
{
	public string LayerName = "Particles";

	public void Start()
	{
		particleSystem.renderer.sortingLayerName = LayerName;
	}
}
