/*jslint node: true */
"use strict";

module.exports = function (grunt) {
    var config = {
        cwd: "src/Thirst.Web/",
        app: "app/",
        content: "<%= config.app %>" + "styles/",
        dist: "bin/Debug/" + "<%= config.app %>",
        useMin: false,
        concat: true
    };

    grunt.initConfig({
        config: config,
        clean: {
            default: {
                src: "<%= config.dist %>"
            }
        },
        copy: {
            index: {
                src: "<%= config.app %>" + "index.html",
                dest: "<%= config.dist %>" + "index.html"
            },
            css: {
                src: "<%= config.content %>" + "site.css",
                dest: "<%= config.dist %>" + "css/site.css"
            }
        },
        ts: {
            default: {
                src: ["<%= config.app %>" + "scripts/**/*.ts"],
                out: "<%= config.dist %>" + "js/main.js",
                options: {
                    sourceMap: false
                }
            }
        },
        "bower-mapper": {
            options: {
                mapper: "../../bower.mapper.json",
                components: ["../../bower_components", "../../node_modules"]
            },
            css: {
                src: ["bootstrap"],
                dest: "<%= config.dist %>" + "css/libs.css",
                concat: "<%= config.concat %>",
                useMin: "<%= config.useMin %>"
            },
            fonts: {
                src: ["bootstrap"],
                dest: "<%= config.dist %>" + "fonts"
            },
            js: {
                src: ["jquery", "angular", "bootstrap", "signalr"],
                dest: "<%= config.dist %>" + "js/libs.js",
                concat: "<%= config.concat %>",
                useMin: "<%= config.useMin %>"
            },
        }
    });

    grunt.loadNpmTasks('grunt-contrib-clean');
    grunt.loadNpmTasks('grunt-contrib-copy');
    grunt.loadNpmTasks("grunt-ts");
    grunt.loadNpmTasks('bower-mapper');

    grunt.registerTask('default', ['build']);
    grunt.registerTask('build', ['clean', 'bower-mapper', 'ts', 'copy']);

    process.chdir(config.cwd);
};
