module ThirstModule {
    export interface IMainScope extends ng.IScope {
        runningProcesses: RunningProcesses[];
        configFile: any;
    }
}
