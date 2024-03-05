using System;

public class BindableProperty<T> where T : IEquatable<T>
{
    private T m_Value = default(T);

    public T Value
    {
        get
        {
            return m_Value;
        }
        set
        {
            if (!value.Equals(m_Value))
            {
                m_Value = value;
                OnValueChanged?.Invoke(value);
            }
        }
    }

    public Action<T> OnValueChanged;
}