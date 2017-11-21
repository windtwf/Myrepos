(function () {
    'use strict';

    var request = require('request');
    var gutil = require('gulp-util');

    var url = "http://ambientconfiguration.blob.core.windows.net/test/env_prod.json";


    // var ss = JSON.parse(url);
    // gutil.log(ss);

    // var options = {
    //     method: 'GET',
    //     url: 'http://ambientconfiguration.blob.core.windows.net/test/env_prod.json',
    //     // headers: {
    //     //     'User-Agent': gitUser,
    //     //     'Authorization': 'token ' + gitToken,
    //     //     'Accept': 'application/vnd.github.v3.json',
    //     //     'Content-Type': 'application/json'
    //     // },
    //     json: true,
    // };

     var mycde = 
          //gutil.log('sssssssssssssss');
         request(url, function (error, response, body) {
             //return JSON.parse(body);
            // var a = JSON.parse(body);
             gutil.log('vvvvvvvvvvvvvvvv');
              var a = JSON.parse(body);
               gutil.log(a);
             //gutil.log(a['TenantId']);
            // gutil.log(a.TenantId);

        });
    

    // var ss = JSON.parse(mycde);
    // gutil.log(ss);
    
    gutil.log(mycde.TenantId);

  

    // var tbody = request.get(url);
    //  var jsonUrl = "http://ambientconfiguration.blob.core.windows.net/test/env_prod.json";
    //         request(jsonUrl, function(error, respons,body){
    //              //browser.get("www.baidu.com")
    //              gutil.log(body);
    //         })
    //gutil.log(JSON.parse(url));



})();