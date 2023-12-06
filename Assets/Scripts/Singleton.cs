//个人感觉abstract和protected构造函数只需要添加一个即可
public abstract class Singleton<T> where T : class, new()
{
    //不允许在Mono脚本中new Singleton对象
    protected Singleton() { }

    private static T instance;
    public static T Instance
    {
        get 
        {
            if (instance == null)
            {
                instance = new T();
            }
            return instance;
        }
    }

    public static void Clear()
    {
        instance = null;
    }
}
