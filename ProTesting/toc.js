(function(){
    'use strict';
    
    var existData = require('./existData');
    var conf = require('./conf');
    var controller = require('./controller');
    var host = existData.host;
    var config = conf.config;
    //var topicId = existData.topicId;
    
    function randomInt(high, low){
        console.log(Math.floor(Math.random()*(high-low)+low));
    }
    
    describe('This is testing controller', function(){
        var ctrl = new controller();
        
        it('should input hello and click search button', function(){
            randomInt(9,7);
            browser.get(host.uwp);
            browser.getCurrentUrl().then(function(url){
                console.log('this is current url' + url);
            });
            var urlText = browser.getCurrentUrl().then(function(url2){
                return url2;
            });
            expect(browser.getCurrentUrl()).toBe('https://msdn.microsoft.com/en-us/windows/uwp/get-started/whats-a-uwp');
            ctrl.searchBox.sendKeys('Hello');
            ctrl.searchButton.click();
        });
        
        xit('should execute ask method', function(){
            browser.get(host.uwp);
            ctrl.ask('time');
        })
    });
    
    describe('This is testing TOC', function(){
        xit('Shuold get the toc node account',function(){
            browser.get(host.library);
            // console.log(host.msdn + host.library);
            // console.log(config.baseUrl);
        })
    })
})();