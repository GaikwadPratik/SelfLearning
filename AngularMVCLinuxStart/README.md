# AngularJS2 project with ASP.NET MVC 5 in Visual Studio 2015 Update 3
<h3> These steps can be used with new applicaiton or existing application </h3>

Steps to create from the scratch

1. Create project by following below steps:
	a. File
	b. New Project 
	c. Visual C#
	d. Web
	e. ASP.NET Web Application
	f. Select <b>MVC</b> Template in the next step and click Ok.

2. Add a new <b>package.json</b> file inside the Project so that we can install <i>NodeJS</i> packages.
	To install AngularJS2 packages copy below lines in package.json
		
		{
		  "version": "1.0.0",
		  "name": "augluarstart", <b><u>This should be name of your project all in lower case</u></b>
		  "private": true,
		  "dependencies": {
			"@angular/common": "~2.1.0",
			"@angular/compiler": "~2.1.0",
			"@angular/core": "~2.1.0",
			"@angular/forms": "~2.1.0",
			"@angular/http": "~2.1.0",
			"@angular/platform-browser": "~2.1.0",
			"@angular/platform-browser-dynamic": "~2.1.0",
			"@angular/router": "~3.1.0",
			"@angular/upgrade": "~2.1.0",
			"@types/core-js": "^0.9.34",
			"@types/node": "^6.0.45",
			"angular-in-memory-web-api": "~0.1.5",
			"bootstrap": "^3.3.7",
			"core-js": "^2.4.1",
			"reflect-metadata": "^0.1.8",
			"rxjs": "5.0.0-beta.12",
			"systemjs": "0.19.39",
			"zone.js": "^0.6.25"
		  },
		  "devDependencies": {
			"concurrently": "^3.0.0",
			"lite-server": "^2.2.0",
			"gulp": "^3.9.1",
			"del": "^2.2.2"
		  }
		}

3. Add a new <b>tsconfig.json</b> file inside the Project so that only <b> required <i>*.ts</i> files </b> gets compiled to JS.
	Copy below configuration in tsconfig.json

	<code>
	{
	  "compilerOptions": {
		"noImplicitAny": false,
		"noEmitOnError": true,
		"removeComments": false,
		"sourceMap": true,
		"target": "es5",
		"module": "commonjs",
		"moduleResolution": "node",
		"emitDecoratorMetadata": true,
		"experimentalDecorators": true,
		"suppressImplicitAnyIndexErrors": true
	  },
	  "compileOnSave": true,
	  "files": [[//this is needed because TS 2.0.* will create compilation error for empty declarations
		"Scripts/App/WelcomeModule/app.component.ts",
		"Scripts/App/WelcomeModule/app.module.ts",
		"Scripts/App/WelcomeModule/main.ts"
	  ],
	  "exclude": [
		"node_modules",
		"wwwroot"
	  ]
	}
	</code>

4. Add a gulpfile.js so that we can move only required files inside Scripts folder. Copy below code in it:
	
	
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

	<i> This should be updated as app grows and more AngularJS2 modules are needed to be inclued in Project. </i> 
	Above code just copies files for a basic app.

5. Right click on package.json and select restore packages. This will create node_modules folder and will install node packages inclueded in package.json.

6. Once the installation is completed, select "Show All Files" option from solution explorer and make sure node_modules folder is created.
							 <b> DO NOT INCLUED</b> node_modules folder in project.
7. Go to "Tools-> Task runner". In the new window, right click on <b> default </b> and select run. This will copy appropriate files from node_modules into Scripts folder.

8. Once gulp task runner is completed, include all the newly crated folder <i>(dependencies)</i> under "Scripts" folder in project.

9. Goto _Layout.cshtml file and add below code to run AngularJS2 modules when app runs.

<script src="~/Scripts/Dependencies/shim.min.js"></script>
    <script src="~/Scripts/Dependencies/zone.js"></script>
    <script src="~/Scripts/Dependencies/Reflect.js"></script>
    <script src="~/Scripts/Dependencies/system.src.js"></script>
    <script src="~/Scripts/SystemConfig/systemjs.config.js"></script>
    <script>
        System.import('app').catch(function (err) { console.error(err); });
    </script>

10. Goto Views -> Home -> Index.cshtml <i>(or whichever view needs to load AngularJS2 app)</i> and add below lines:
	<div>
		<first-app></first-app>
	</div> 

11. Now let's hook up angular 2 code. 
	a. Create below folder structure under Scripts folder:
		Scripts
			App
				WelcomeModule
				SystemConfig
	
	b. create systemjs.config.js under SystemConfig folder and copy below code.
		 /**
		 * System configuration for Angular 2 samples
		 * Adjust as necessary for your application needs.
		 */

		(function (global) {
			System.config({
				paths: {
					// paths serve as alias
					'npm:': 'Scripts/Dependencies/'
				},
				// map tells the System loader where to look for things
				map: {
					// our app is within the app folder
					app: 'Scripts/App',

					// angular bundles
					'@angular/core': 'npm:@angular/core/bundles/core.umd.js',
					'@angular/common': 'npm:@angular/common/bundles/common.umd.js',
					'@angular/compiler': 'npm:@angular/compiler/bundles/compiler.umd.js',
					'@angular/platform-browser': 'npm:@angular/platform-browser/bundles/platform-browser.umd.js',
					'@angular/platform-browser-dynamic': 'npm:@angular/platform-browser-dynamic/bundles/platform-browser-dynamic.umd.js',
					'@angular/http': 'npm:@angular/http/bundles/http.umd.js',
					'@angular/router': 'npm:@angular/router/bundles/router.umd.js',
					'@angular/forms': 'npm:@angular/forms/bundles/forms.umd.js',
					// other libraries
					'rxjs': 'npm:rxjs',
					'angular2-in-memory-web-api': 'npm:angular2-in-memory-web-api',
				},

				// packages tells the System loader how to load when no filename and/or no extension
				packages: {
					app: {
						main: './main.js',
						defaultExtension: 'js'
					},
					rxjs: {
						defaultExtension: 'js'
					},
					'angular2-in-memory-web-api': {
						main: './index.js',
						defaultExtension: 'js'
					}
				}
			});
		})(this);
	
	c. Create main.ts under WelcomeModule and copy below code:
		import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';
		import { AppModule } from './app.module';
		platformBrowserDynamic().bootstrapModule(AppModule);
		
	d. create app.module.ts under WelcomeModule and copy below code:
		import { NgModule } from '@angular/core';
		import { BrowserModule } from '@angular/platform-browser';
		import { AppComponent } from './app.component';

		@NgModule({
			imports: [BrowserModule],
			declarations: [AppComponent],
			bootstrap: [AppComponent]
		})

		export class AppModule { }

	e. create app.component.ts under WelcomeModule and copy below code:
		import { Component } from '@angular/core';

		@Component({
			selector: 'first-app',
			template: `<h1> my first angular 2 app</h1><br/>
			<h2>{{title}}<h2>`
			})

			export class AppComponent {
				title = "Hi there, I am from TS Class";
			}

<b> At this point we are all set, just run the app </b>
