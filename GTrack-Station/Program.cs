using System.Diagnostics;

internal class Program
{
    // Конфигурация Flowgraph
    class FlowgraphConfig
    {
        public string Type { get; set; }
        public string ExecutablePath { get; set; }
        public bool HasDopplerShift { get; set; }
        public bool HasModulationParams { get; set; }
    }

    // Статусы
    static bool flowgraphRunning = false;
    static bool rotatorRunning = false;

    // Хранение конфигурации
    static FlowgraphConfig flowgraphConfig = new FlowgraphConfig
    {
        Type = "ExampleType",
        ExecutablePath = @"C:\path\to\flowgraph_script.py",
        HasDopplerShift = true,
        HasModulationParams = true
    };

    // Процесс flowgraph
    static Process flowgraphProcess = null;

    public static void Main(string[] args)
    {
        DrawASCII();
        Console.ForegroundColor = ConsoleColor.Green;
        
        // Пример получения команды управления с Az, El, Doppler
        OnControlCommandReceived(az: 10, el: 20, doppler: 300);

        // Пример запуска flowgraph
        StartFlowgraph();

        // Пример получения параметров модуляции от GTrack-Node
        OnModulationParamsReceived(new { Param1 = 123, Param2 = 456 });

        Console.ReadLine();
    }

    private static void DrawASCII()
    {
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine(@"  ___________________                     __               _________ __          __  .__               ");
        Console.WriteLine(@" /  _____/\__    ___/___________    ____ |  | __          /   _____//  |______ _/  |_|__| ____   ____  ");
        Console.WriteLine(@"/   \  ___  |    |  \_  __ \__  \ _/ ___\|  |/ /  ______  \_____  \\   __\__  \\   __\  |/  _ \ /    \ ");
        Console.WriteLine(@"\    \_\  \ |    |   |  | \// __ \\  \___|    <  /_____/  /        \|  |  / __ \|  | |  (  <_> )   |  \");
        Console.WriteLine(@" \______  / |____|   |__|  (____  /\___  >__|_ \         /_______  /|__| (____  /__| |__|\____/|___|  /");
        Console.WriteLine(@"        \/                      \/     \/     \/                 \/           \/                    \/ ");
        Console.WriteLine("\n");
        Console.ResetColor();
    }

    // Обработка команды управления с Az, El, Doppler
    static void OnControlCommandReceived(double az, double el, double doppler)
    {
        Console.WriteLine($"Received control command: Az={az}, El={el}, Doppler={doppler}");

        if (flowgraphRunning && flowgraphConfig.HasDopplerShift)
        {
            SendDopplerToFlowgraph(doppler);
        }

        if (rotatorRunning)
        {
            SendAzElToRotator(az, el);
        }
    }

    // Обработка параметров модуляции от GTrack-Node
    static void OnModulationParamsReceived(dynamic modulationParams)
    {
        Console.WriteLine("Received modulation params");

        if (!flowgraphRunning && flowgraphConfig.HasModulationParams)
        {
            SendModulationParamsToFlowgraph(modulationParams);
        }
    }

    // Запуск flowgraph (python-скрипта)
    static void StartFlowgraph()
    {
        if (flowgraphRunning)
        {
            Console.WriteLine("Flowgraph is already running.");
            return;
        }

        try
        {
            flowgraphProcess = new Process();
            flowgraphProcess.StartInfo.FileName = "python"; // python.exe должен быть в PATH
            flowgraphProcess.StartInfo.Arguments = $"\"{flowgraphConfig.ExecutablePath}\"";
            flowgraphProcess.StartInfo.UseShellExecute = false;
            flowgraphProcess.StartInfo.CreateNoWindow = true;
            flowgraphProcess.Start();

            flowgraphRunning = true;
            Console.WriteLine("Flowgraph started.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error starting flowgraph: {ex.Message}");
        }
    }

    // Отправка Doppler в flowgraph
    static void SendDopplerToFlowgraph(double doppler)
    {
        Console.WriteLine($"Sending Doppler {doppler} to Flowgraph");
        // TODO: здесь логика передачи в Flowgraph
    }

    // Отправка Az и El в Rotator
    static void SendAzElToRotator(double az, double el)
    {
        Console.WriteLine($"Sending Az={az}, El={el} to Rotator");
        // TODO: здесь логика передачи в Rotator
    }

    // Отправка параметров модуляции в Flowgraph
    static void SendModulationParamsToFlowgraph(dynamic modulationParams)
    {
        Console.WriteLine($"Sending modulation params to Flowgraph: {modulationParams}");
        // TODO: здесь логика передачи в Flowgraph
    }

    // Приём телекоманды от GTrack-Node
    static void OnTelemetryCommandReceived(dynamic telemetryData)
    {
        if (flowgraphRunning)
        {
            // Передать в Flowgraph
            Console.WriteLine("Passing telemetry command to Flowgraph");
            // TODO
        }
    }

    // Приём телеметрии от Flowgraph
    static void OnTelemetryFromFlowgraph(dynamic telemetryData)
    {
        // Передать в GTrack-Node
        Console.WriteLine("Passing telemetry from Flowgraph to GTrack-Node");
        // TODO
    }
}