// spec.js
(function () {
    'use strict';

    for (var i = 0; i < 5; i++) {
        console.log(i + "==");
        setTimeout(() => {
            console.log(new Date, i);
        }, 5000);
    }

    console.log(new Date, 9);

    // var output = (i) => {
    //     setTimeout(() => {
    //         console.log(i);
    //     }, 1000)
    // }

    // for (var i = 0; i < 5; i++) {
    //     output(i);
    // }



    // var output = function (i) {
    //     setTimeout(function () {
    //         console.log(new Date, i);
    //     }, 1000);
    // };

    // for (var i = 0; i < 5; i++) {
    //     output(i); // 这里传过去的 i 值被复制了
    // }

    // console.log(new Date, 9);



    // function sayHello(name) {
    //     var text = 'Hello ' + name;
    //     var say = function () { console.log(text); }
    //     say();
    // }
    // sayHello('Joe');

    // function foo1() {
    //     return {
    //         bar: "hello"
    //     };
    // }

    // function foo2() {
    //     return
    //     {
    //         bar: "hello"
    //     };
    // }


//    var a = [1, 2, 3];
//    var b = a.shift();

//    console.log(a);

})();




