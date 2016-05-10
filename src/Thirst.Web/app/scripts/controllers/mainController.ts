/// <reference path="../../../Scripts/typings/signalr/signalr.d.ts" />
/// <reference path="../hubs.ts" />

module ThirstModule {
    export class MainController {

        constructor() {
            var thirstHub = $.connection.thirstHub;

            thirstHub.client.getRunningProcesses = (message: RunningProcesses) => {
                console.log(message);
            }

            $.connection.hub.start().done(() => {
                thirstHub.server.inspectProcesses({ ProcessNames: ["Thirst.Agent"] });
            });
        }
    }
}
