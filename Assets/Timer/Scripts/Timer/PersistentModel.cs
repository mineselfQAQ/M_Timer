public class PersistentModel
{
    //本日累计(单位---分钟m)
    public static BindableProperty<int> TodayTotalTime { get; } = new BindableProperty<int>()
    {
        Value = 0
    };

    //本月累计(单位---小时h) 暂不启用
    //public static BindableProperty<float> MonthTotalTime { get; } = new BindableProperty<float>()
    //{
    //    Value = 0
    //};

    //总计(单位---小时h)
    public static BindableProperty<float> TotalTime { get; } = new BindableProperty<float>()
    {
        Value = 0
    };

    //好像用不到
    //public static BindableProperty<string> LastStartDay { get; } = new BindableProperty<string>()
    //{
    //    Value = "0001/01/01"//日期默认值
    //};
}
