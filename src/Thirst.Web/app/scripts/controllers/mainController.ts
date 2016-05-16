/// <reference path="../../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="../../../scripts/typings/lodash/lodash.d.ts" />
/// <reference path="../../../Scripts/typings/signalr/signalr.d.ts" />
/// <reference path="../services/configuploadservice.ts" />
/// <reference path="../directives/filemodeldirective.ts" />
/// <reference path="../mainscope.ts" />
/// <reference path="../hubs.ts" />

module ThirstModule {
    export class MainController {

        static $inject = ['$scope', 'configUploadService'];

        isConfigured: boolean = false;

        constructor(private $scope: IMainScope, private configUploadService: ConfigUploadService) {

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

            $.connection.hub.start().done(() => { });
        }

        uploadConfigFile(): void {
            var file = this.$scope.configFile;

            this.configUploadService.uploadConfigFile(file)
                .success(() => {
                    this.isConfigured = true;
                });
        }

        inspectProcesses(): void {
            var thirstHub = $.connection.thirstHub;

            this.configUploadService.getConfigFile()
                .success((data: InspectProcesses) => {
                    thirstHub.server.inspectProcesses(data);
                });
        }
    }
}
