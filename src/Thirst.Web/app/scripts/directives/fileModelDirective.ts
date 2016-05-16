module ThirstModule {

    export class FileModelDirective {

        private static directive($parse: ng.IParseService): ng.IDirective{
            return {
                restrict: 'A',
                link: (scope: any, element: any, attrs: any) => {
                    var model = $parse(attrs.fileModel);
                    var modelSetter = model.assign;
                    element.bind('change', () =>{
                        scope.$apply(() =>{
                            modelSetter(scope, element[0].files[0]);
                        });
                    });
                }
            };
        }

        static factory(): ng.IDirectiveFactory {
            this.directive['$inject'] = ['$parse'];
            return this.directive;
        }
    }
}
