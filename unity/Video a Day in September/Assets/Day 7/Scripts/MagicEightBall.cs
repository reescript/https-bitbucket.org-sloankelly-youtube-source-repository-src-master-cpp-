using System;
using UnityEngine;
using UnityEngine.UI;

public class MagicEightBall : MonoBehaviour 
{
	string[] phrases = new string[] { "It is certain", "It is decidedly so","Without a doubt","Yes definitely","You may rely on it",
"As I see it, yes",
"Most likely",
"Outlook good",
"Yes",
"Signs point to yes",
"Reply hazy try again",
"Ask again later",
"Better not tell you now",
"Cannot predict now",
"Concentrate and ask again",
"Don't count on it",
"My reply is no",
"My sources say no",
"Outlook not so good",
"Very doubtful"};

    public Text phraseBox;

    public Button button;

    public void Shake()
    {
        button.interactable = false;
        phraseBox.text = phrases.Random();

        var alphaFade = CurveFactory.Create(0f, 1f);
        var scaleFade = CurveFactory.Create(0.5f, 1f);

        Action<float> alphaTick = (t) => phraseBox.SetAlpha(alphaFade.Evaluate(t));
        Action<float> scaleTick = (t) => phraseBox.SetScale(scaleFade.Evaluate(t));


        StartCoroutine(CoroutineFactory.Create(1f, alphaTick + scaleTick, () => button.interactable = true));
    }
}
