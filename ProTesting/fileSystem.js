(function () {
    'strict';

    var fs = require('fs');

    describe('fs', function () {
        afterEach(function(){
            //var failures = jasmine.getEnv().currentSpec.results_.failedCount;
            //var failures = jasmine.getEnv().versionString();
            var failures = jsApiReporter.specResults()[0].failedExpectations;
            console.log("faileures:  " + failures);
        });
        
        it('fs', function () {
            //browser.get("https://docs.microsoft.com/en-us/active-directory/active-directory-editions#comparing-generally-available-features");

            // delete file
            // fs.unlink('/ttt.txt', (err) => {
            //     if (err) throw err;
            //     console.log('successfully deleted /ttt.txt');
            // });

            // rename file
            console.log('aaaaaaaaaaa');
            // fs.rename('/ttt.txt', '/yyy.txt', (err) => {
            //     if (err) throw err;
                
            //     // show file status
            //     fs.stat('/yyy.txt', (err, stats) => {
            //         if (err) throw err;
            //         console.log(`status: ${JSON.stringify(stats)}`);
            //         //console.log(stats);
            //     });
            // });
        })
    })
})();