using System.Collections;

public class CoroutineHandler : MonoSingleton<CoroutineHandler>
{
    public void AddCorotine(IEnumerator fun)
    {
        StartCoroutine(fun);
    }
}