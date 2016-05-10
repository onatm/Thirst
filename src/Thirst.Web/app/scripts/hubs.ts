interface RunningProcesses {
    HostName: string;
    ProcessNames: string[];
}

interface InspectProcesses {
    ProcessNames: string[];
}

interface IThirstHubClient {
    getRunningProcesses(message: RunningProcesses);
}

interface IThirstHubServer {
    inspectProcesses(message: InspectProcesses);
}

interface IThirstHubProxy {
    client: IThirstHubClient;
    server: IThirstHubServer;
}

interface SignalR {
    thirstHub: IThirstHubProxy;
}
