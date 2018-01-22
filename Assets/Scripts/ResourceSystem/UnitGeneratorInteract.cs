using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitGeneratorInteract : MonoBehaviour, IInteractable {

    public UnitGenerator m_ug;

    void Start () {
        m_ug.m_ugInteract = this;
        m_ug.m_interactArea.GetComponent<InteractArea>().m_linkedInteract = this;
    }

	public void onTriggerEnter()
	{
		m_ug.m_curResources = Vector3.zero;
		m_ug.m_spriteRenderer.color = Color.red;
	}

	public void onTriggerExit()
	{
		m_ug.m_spriteRenderer.color = Color.white;
	}

	public void Interact(GameObject player, Button button)
    {
		Debug.Log("INTERACTING WITH UNIT GENERATOR");
		Player p = player.GetComponent<Player>();

		// Vector3[metal, oil, rubber]
		if(button == Button.X)
		{
			m_ug.m_curResources.x++;
		}
		else if(button == Button.Y)
		{
			m_ug.m_curResources.y++;
		}
		else if(button == Button.B)
		{
			m_ug.m_curResources.z++;
		}
		else if(button == Button.A)
		{
			if(!m_ug.m_recipeMap.ContainsKey(m_ug.m_curResources))
			{
				Debug.Log("recipe not found");
				m_ug.m_curResources = Vector3.zero;
				return;
			}

			int m = (int)m_ug.m_curResources.x;
			int o = (int)m_ug.m_curResources.y;
			int r = (int)m_ug.m_curResources.z;
			if (p.metal >= m && p.oil >= o && p.rubber >= r)
			{
				GameObject unit = Instantiate(m_ug.m_recipeMap[m_ug.m_curResources]);
				unit.transform.position = m_ug.m_spawnPoint.transform.position;

				p.metal -= m;
				p.oil -= o;
				p.rubber -= r;
			}
			else
			{
				Debug.Log("insuff resource on player");
			}
			m_ug.m_curResources = Vector3.zero;
		}
    }
}


/*
			GameObject heldResource = player.GetComponent<PlayerController>().heldResource;
			if (heldResource != null) // add resource
			{
				Resource r = heldResource.GetComponent<Resource>();
				if (!r)
				{
					Debug.Log("Non-resource given to unit generator");
					return;
				}

				if (r.m_type == Resource.Type.Metal) m_ug.m_curResources.x++;
				else if (r.m_type == Resource.Type.Oil) m_ug.m_curResources.y++;
				else if (r.m_type == Resource.Type.Rubber) m_ug.m_curResources.z++;

				Destroy(heldResource);
			}
			else // build unit
			{
				if (m_ug.m_recipeMap.ContainsKey(m_ug.m_curResources))
				{
					GameObject unit = Instantiate(m_ug.m_recipeMap[m_ug.m_curResources]);
					unit.transform.position = m_ug.m_spawnPoint.transform.position;

					m_ug.m_curResources = Vector3.zero;
				}
				else
				{
					Debug.Log("Current resources don't map to a unit");
				}
			}
			*/
