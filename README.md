![GitHub Actions Workflow Status](https://img.shields.io/github/actions/workflow/status/harrryhsu/TrafficCom.V3/docker-image.yml) ![NuGet Downloads](https://img.shields.io/nuget/dt/TrafficCom.V3)


# TrafficCom.V3

Taiwanese urban traffic communication protocol V3 .NetCore Implementation ([都市交通控制通訊協定](https://www.iot.gov.tw/cp-81-179-d2d72-1.html))

Only the variable message sign control is implemented. 





## Usage

The service can be accessed through both V3Client or MonitorClient interface.

MonitorClient provides a clean simple control interface for updating/querying variable message sign. Not all functionalities are supported. 


    using var v3Client = new V3Client(ip, port, timeout);
    var monitorClient = new MonitorClient(v3Client);
    await monitorClient.SendSingleText(new MonitorTextEntry
    {
        Id = 1,
        Text = "Test",
        Show = true,
        TextColor = MonitorColor.Red,
        BackgroundColor = MonitorColor.Green,
        BlinkInterval = MonitorBlinkInterval.S0P5,
        HBound = 0,
        VBound = 0,
        HSpace = 0,
        VSpace = 0,
    });

V3Client provides a direct messaging interface, the user is expected to understand the content of each messages. 

All messages for variable message sign control is available and named after the command code. For instance the message class for command AFH+10H will be V3RequestXAFX10.

    using var v3Client = new V3Client(ip, port, timeout);
    v3Client.OnMessageReceived += async (sender, con, request) => {
        ...Handle request
        //Reply success update
        await sender.SendAsync(new V3RequestX0FX80());
    }

    // Send clear current display and wait for success update response
    await _client.SendAndWaitForReplyAsync<V3RequestX0FX80>(new V3RequestXAFX14());

    // Send clear current display and ignore response, response will go to OnMessageReceived
    await _client.SendAsync(new V3RequestXAFX14());


