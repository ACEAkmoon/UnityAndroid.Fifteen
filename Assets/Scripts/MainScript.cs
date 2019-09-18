using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainScript : MonoBehaviour {
    const int size = 4;
    Game game;

    public Text TextMoves;

    void Start()
    {
        game = new Game(size);
        HideButtons();
    }

    public void OnStart()
    {
        game.Start(700 + System.DateTime.Now.Second);
        ShowButtons();
    }

    public void OnClick()
    {
        if (game.Solved())
            return;
        string name = EventSystem.current.currentSelectedGameObject.name;
        int x = int.Parse(name.Substring(0, 1));
        int y = int.Parse(name.Substring(1, 1));
        game.Press(x, y);
        ShowButtons();
        if (game.Solved())
            TextMoves.text = "Game finished in\n" + game.moves + " moves";
    }

    void HideButtons()
    {
        for (int x = 0; x < size; x++)
            for (int y = 0; y < size; y++)
                ShowDigit(0, x, y);
        TextMoves.text = "Welcome Fifteen";
    }

    void ShowButtons()
    {
        for (int x = 0; x < size; x++)
            for (int y = 0; y < size; y++)
                ShowDigit(game.GetDigit(x, y), x, y);
       TextMoves.text = game.moves + " moves";
    }

    void ShowDigit(int digit, int x, int y)
    {
        string name = x + "" + y;
        var button = GameObject.Find(name);
        var text = button.GetComponentInChildren<Text>();
        text.text = DecToHex(digit);
        button.GetComponentInChildren<Image>().color = //set Visible
            (digit > 0) ? Color.white : Color.clear;
    }

    string DecToHex(int digit)
    {
        if (digit == 0) return "";
        if (digit < 10) return digit.ToString();
        return ((char)('A' + digit - 10)).ToString();
    }

}
