/*
This file in the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/

var gulp = require('gulp');
var del = require('del');
var config = {
    jsDependencies: [
        "node_modules/core-js/client/shim.min.js",
        "node_modules/zone.js/dist/zone.js",
        "node_modules/reflect-metadata/Reflect.js",
        "node_modules/systemjs/dist/system.src.js"
    ]
};

gulp.task('migrate-js-dependencies', function () {
    gulp.src('node_modules/@angular/**/*').pipe(gulp.dest('Scripts/dependencies/@angular'));
    gulp.src('node_modules/typescript/**/*').pipe(gulp.dest('Scripts/dependencies/typescript'));
    gulp.src('node_modules/rxjs/**/*').pipe(gulp.dest('Scripts/dependencies/rxjs'));
    return gulp.src(config.jsDependencies).pipe(gulp.dest('Scripts/dependencies'));
});

gulp.task('migrate-typings', function () {
    return gulp.src('node_modules/@types/**/*').pipe(gulp.dest('Scripts/@types'));
})
gulp.task('default', ['migrate-js-dependencies', 'migrate-typings'], function () {
    del(['typings']);
});