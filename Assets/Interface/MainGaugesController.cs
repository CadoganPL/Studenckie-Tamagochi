using UnityEngine;
using UnityEngine.UI;

namespace Assets.Interface
{
    public class MainGaugesController : MonoBehaviour
    {
        [SerializeField]
        private Text _energy,_hunger,_knowledge;

        private string EnergyGauge
        {
            set { _energy.text = "Energia " + value; }
        }
        private string HungerGauge
        {
            set { _energy.text = "Głód " + value; }
        }
        private string KnowledgeGauge
        {
            set { _energy.text = "Wiedza " + value; }
        }

        // Use this for initialization
        void Start () {
		
        }
	
        // Update is called once per frame
        void Update () {
		
        }
    }
}
