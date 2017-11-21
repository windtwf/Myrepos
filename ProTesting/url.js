(function () {
    'strict';

    var url = require('url');
    var request = require('request');
    var gutil = require('gulp-util');
    var Q = require('q');
    //var prac = require('./prac');

    var url = "http://ambientconfiguration.blob.core.windows.net/test/env_prod.json";
    var mycde = function () {
        return request(url, function (error, response, body) {
            //return JSON.parse(body);
          var a = JSON.parse(body);
          return  a;
        });
    }

   // var config = prac.mycde();

    describe('url test', function () {

        it('url test', function () {
            // browser.get('www.baidu.com')
            var jsonUrl = "http://ambientconfiguration.blob.core.windows.net/test/env_prod.json";

            //  mycde('TenantId');

           // mycde('TenantId');

           mycde().then(function(item){
               gutil.log(item.TenantId);
           })

            // var deferred = Q.defer();
            // request(jsonUrl, function(error, respons,body){
            //      //browser.get("www.baidu.com")
            //      var mybody = JSON.parse(body);
            //      if(mybody['CAPSEnvironment']==='https://ambientconfiguration.blob.core.windows.net/environment/prod.jsons'){
            //          console.log('1111111111')
            //      }else{
            //          deferred.reject('ssssssssssssssssss');
            //          //console.log('1111111111')
            //      }
            //      //gutil.log(mybody['CAPSEnvironment']);
            // })

            // return deferred.promise;



        })
    })
})();