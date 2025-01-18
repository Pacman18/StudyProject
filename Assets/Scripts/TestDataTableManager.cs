using UnityEngine;
using StudyTable;
using StudyTable.Enums;
using UnityEngine.UI;

public class TestDataTableManager : MonoBehaviour
{
    
    void Start()
    {
        var texts = GetComponentsInChildren<Text>();

        var charData_1 = Tables.Character.GetById(1);

        texts[0].text = charData_1.AgentName;

        var charData_2 = Tables.Character.GetById(2);

        texts[1].text = charData_2.Name;

        var charData_3 = Tables.Character.GetById(3);

        texts[2].text = charData_3.Name;

    }

    
    void Update()
    {
        
    }
}
