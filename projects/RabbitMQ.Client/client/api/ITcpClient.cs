﻿using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace RabbitMQ.Client;

/// <summary>
/// Wrapper interface for standard TCP-client. Provides socket for socket frame handler class.
/// </summary>
/// <remarks>Contains all methods that are currently in use in rabbitmq client.</remarks>
public interface ITcpClient : IDisposable
{
    bool Connected { get; }

    TimeSpan ReceiveTimeout { get; set; }

    Socket Client { get; }

    Task ConnectAsync(string host, int port);
    Task ConnectAsync(IPAddress host, int port);

    NetworkStream GetStream();

    void Close();
}
