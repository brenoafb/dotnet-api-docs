﻿/// class: System.Runtime.Remoting.Channels.Tcp.TcpChannel
//<snippet1>
using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Security.Permissions;

public class Server
{
[SecurityPermission(SecurityAction.Demand)]
    public static void Main(string[] args)
    {
        //<snippet2>
        // Create the server channel.
        TcpChannel serverChannel = new TcpChannel(9090);
        //</snippet2>

        // Register the server channel.
        ChannelServices.RegisterChannel(serverChannel);

        //<snippet3>
        // Show the name of the channel.
        Console.WriteLine("The name of the channel is {0}.",
            serverChannel.ChannelName);
        //</snippet3>

        //<snippet4>
        // Show the priority of the channel.
        Console.WriteLine("The priority of the channel is {0}.",
            serverChannel.ChannelPriority);
        //</snippet4>

        //<snippet5>
        // Show the URIs associated with the channel.
        ChannelDataStore data = (ChannelDataStore) serverChannel.ChannelData;
        foreach (string uri in data.ChannelUris)
        {
            Console.WriteLine("The channel URI is {0}.", uri);
        }
        //</snippet5>

        // Expose an object for remote calls.
        RemotingConfiguration.RegisterWellKnownServiceType(
            typeof(RemoteObject), "RemoteObject.rem",
            WellKnownObjectMode.Singleton);

        //<snippet6>
        // Parse the channel's URI.
        string[] urls = serverChannel.GetUrlsForUri("RemoteObject.rem");
        if (urls.Length > 0)
        {
            string objectUrl = urls[0];
            string objectUri;
            string channelUri = serverChannel.Parse(objectUrl, out objectUri);
            Console.WriteLine("The object URL is {0}.", objectUrl);
            Console.WriteLine("The object URI is {0}.", objectUri);
            Console.WriteLine("The channel URI is {0}.", channelUri);
        }
        //</snippet6>

        // Wait for the user prompt.
        Console.WriteLine("Press ENTER to exit the server.");
        Console.ReadLine();
    }
}
//</snippet1>
