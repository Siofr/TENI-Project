using UnityEngine;

[CreateAssetMenu(fileName = "DialogueLine", menuName = "Dialogue/Dialogue Line")]
public class DialogueSO : ScriptableObject
{
    public int speaker;
    public string[] dialogueLines;
}
