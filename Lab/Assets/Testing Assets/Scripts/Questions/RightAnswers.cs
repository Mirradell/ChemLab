using UnityEngine;
using UnityEngine.UI;

public class RightAnswers : MonoBehaviour
{
    public GameObject RightButton;

    public void SetRightAnswer()
    {
        var rightimage = RightButton.GetComponent<Image>();
        var righttempColor = rightimage.color;
        rightimage.color = Color.green;
        
        var tempColorBlock = RightButton.GetComponent<Button>().colors;
        var disabledColor = tempColorBlock.disabledColor;
        disabledColor.a = 0.8f;
        tempColorBlock.disabledColor = disabledColor;
        RightButton.GetComponent<Button>().colors = tempColorBlock;
    }
}