/// <reference path="../../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../../scripts/typings/lodash/lodash.d.ts" />
/// <reference path="../../../Scripts/typings/signalr/signalr.d.ts" />
/// <reference path="../hubs.ts" />

module ThirstModule {

    export interface IMainScope extends ng.IScope {
        runningProcesses: RunningProcesses[];
    }

    export class MainController {

        constructor($scope: IMainScope) {
            $scope.runningProcesses = [];

            var thirstHub = $.connection.thirstHub;

            thirstHub.client.getRunningProcesses = (message: RunningProcesses) => {
                var existing = _.filter($scope.runningProcesses, x => x.HostName === message.HostName);

                if (existing.length > 0) {
                    existing[0].ProcessNames = message.ProcessNames;
                }
                else {
                    $scope.runningProcesses.push(message);
                }

                $scope.$apply();
            }

            $.connection.hub.start().done(() => {
                thirstHub.server.inspectProcesses({ ProcessNames: ["Thirst.Agent"] });
            });
        }
    }
}
