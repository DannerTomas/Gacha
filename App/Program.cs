using GachaSystem.Genshin;

Gacha gacha = new();

SELECT:

Console.WriteLine($"选择你的卡池:(1){gacha.GetGachaPoolRoles(1)} (2){gacha.GetGachaPoolRoles(2)}");
var type = Console.ReadLine();
int gachaType = 1;
try
{
    gachaType = int.Parse(type);
    if (gachaType <= 0 || gachaType > 2)
    {
        Console.WriteLine("该卡池不存在，请重试。");
        goto SELECT;
    }
}
catch
{
    Console.WriteLine("输入的不是整数，请重试。");
    goto SELECT;
}

MAIN:

Console.WriteLine("输入你想抽的次数:(输入CLEAR清空当前卡池记录,输入SWITCH切换卡池) 输入回车来一个十连！");

var times = Console.ReadLine();
int time = 0;
try
{
    switch (times)
    {
        case "":
            time = 10;
            break;
        case "CLEAR":
            gacha.Clear();
            Console.Clear();
            Console.WriteLine("已重置卡池记录！");
            goto MAIN;
        case "SWITCH":
            goto SELECT;
        default:
            time = int.Parse(times);
            break;
    }
}
catch
{
    Console.WriteLine("输入不是整数或无法识别命令，请重试。");
    goto MAIN;
}

for (int count = 0; count < time; count++)
{
    var result = gacha.DoGacha(gachaPool: gachaType);

    if (result.StartsWith("★★★:"))
    {
        Console.ForegroundColor = ConsoleColor.White;
    }
    if (result.StartsWith("★★★★:"))
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
    }
    if (result.StartsWith("★★★★★:"))
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
    }

    Console.WriteLine(result);
    Console.ForegroundColor = ConsoleColor.Gray;
}

Console.WriteLine("抽卡已结束。即将回到程序开头...");
goto MAIN;