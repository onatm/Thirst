module ThirstModule {

    export class ConfigUploadService {

        static $inject = ['$http'];

        constructor(private $http: ng.IHttpService) {
        }

        uploadConfigFile(file: any): angular.IHttpPromise<any> {
            var fd = new FormData();
            fd.append('file', file);

            return this.$http.post('/config', fd, {
                transformRequest: angular.identity,
                headers: { 'Content-Type': undefined }
            });
        }

        getConfigFile(): angular.IHttpPromise<InspectProcesses> {
            return this.$http.get('/config');
        }
    }
}
