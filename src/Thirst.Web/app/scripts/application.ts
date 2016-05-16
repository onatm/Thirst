/// <reference path="../../scripts/typings/angularjs/angular.d.ts" />
/// <reference path="controllers/maincontroller.ts" />
/// <reference path="services/configuploadservice.ts" />
/// <reference path="directives/filemodeldirective.ts" />

module ThirstModule {
    angular.module("ThirstModule", [])
        .controller("MainController", MainController)
        .service("configUploadService", ConfigUploadService)
        .directive("fileModel", FileModelDirective.factory());
}
