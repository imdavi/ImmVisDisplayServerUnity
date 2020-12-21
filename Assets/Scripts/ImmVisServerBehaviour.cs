using System;
using System.Collections;
using System.Collections.Generic;
using Grpc.Core;
using UnityEngine;

public delegate void Dispatcher(Action action);

public class ImmVisServerBehaviour : UnityDispatcherBehaviour
{
    const string Host = "0.0.0.0";
    const int Port = 50051;

    private Server server;

    public ScatterplotBehaviour scatterplot;

    void Start()
    {
        server = new Server(new List<ChannelOption> { new ChannelOption(ChannelOptions.MaxReceiveMessageLength, int.MaxValue) })
        {
            Services = { ImmVisJupyter.BindService(new ImmVisServiceImpl(scatterplot, ExecuteOnMainThread)) },
            Ports = { new ServerPort("localhost", Port, ServerCredentials.Insecure) }
        };

        server.Start();
    }

    async void OnApplicationQuit()
    {
        await server.ShutdownAsync();
    }
}
