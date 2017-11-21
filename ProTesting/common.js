(function(){
    'use strict'; 
var fulfilled = protractor.promise.fulfilled();
var flow = protractor.promise.controlFlow();
var defer = protractor.promise.defer();

var p1= function(){
 flow.execute(function() {
   flow.execute(() => console.log('A'));
   fulfilled.then(() => console.log('B'));
   //flow.execute(() => console.log('C'));
   //fulfilled.then(() => console.log('D'));
 }).then(function() {
   console.log('fin');
 });
};

var p2 = function(){
    p.then(function(){
        flow.execute(function(){
            console.log('b');
        })
    }).then(()=> console.log('a'));
}

var p3 = function(){
    flow.execute(function(){
        flow.execute(()=> console.log('A'));
        defer.promise.then(()=> console.log('B'));
        flow.execute(()=> console.log('C'));
        defer.promise.then(()=> console.log('D'));
        defer.fulfill();
    }).then(function(){
        console.log('fin');
    });
}

var p4 = function(){
    setTimeout(() => console.log('a'));
    ManagedPromise.resolve().then(() => console.log('b'));  // A native promise.
    flow.execute(() => console.log('c'));
    ManagedPromise.resolve().then(() => console.log('d'));
    setTimeout(() => console.log('fin'));
}

 module.exports ={
     p1: p1,
     p2: p2,
     p3: p3,
     p4: P4,
 };
})();

