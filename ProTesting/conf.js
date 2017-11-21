// conf.js
exports.config = {
    framework: 'jasmine2',
    seleniumAddress: 'http://localhost:4444/wd/hub',
    specs: ['spec.js'],
    //   multiCapabilities:[{
    //       browserName: 'chrome'
    //   },{
    //       browserName : 'internet explorer',
    //       platform: 'ANY',
    //       version: '11'
    //   }],

    //   capabilities: {
    //       browserName: 'internet explorer',
    //       platform: 'ANY',
    //       version: '11'
    //   }
    capabilities: {
        browserName: 'chrome'
    },
    baseUrl: 'https://msdn.microsoft.com/en-us/',

    suites: {
        url: './url.js',
        fs: './fileSystem.js',
        dd: './dd.js',
        prac: './prac.js',
        test: '/test.js'
    },

    onPrepare: function () {
        //For non-angular site, it's better to close synchronization flag.
        browser.ignoreSynchronization = true;
        
    },

      jasmineNodeOpts: {
        showColors: true,
        defaultTimeoutInterval: 1000*1000
    }
}