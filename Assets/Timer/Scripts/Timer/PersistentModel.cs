public class PersistentModel
{
    //�����ۼ�(��λ---����m)
    public static BindableProperty<int> TodayTotalTime { get; } = new BindableProperty<int>()
    {
        Value = 0
    };

    //�����ۼ�(��λ---Сʱh) �ݲ�����
    //public static BindableProperty<float> MonthTotalTime { get; } = new BindableProperty<float>()
    //{
    //    Value = 0
    //};

    //�ܼ�(��λ---Сʱh)
    public static BindableProperty<float> TotalTime { get; } = new BindableProperty<float>()
    {
        Value = 0
    };

    //�����ò���
    //public static BindableProperty<string> LastStartDay { get; } = new BindableProperty<string>()
    //{
    //    Value = "0001/01/01"//����Ĭ��ֵ
    //};
}
