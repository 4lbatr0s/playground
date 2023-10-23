//INFO: This module will contain  Fonksiyonlar, Declaration vs Expression, IIFE, First-Class Fonksiyonlar

// Function Declaration:
console.log(
    '---------------Function Declaration--------------------------------'
);
console.log(square(10)); //TIP: Works with HOISTING!
function square(num) {
    return num * num;
}
square(5);
console.log(square(5));
console.log(square); //TIP:Returns function information.
console.log(square()); //NaN

console.log(
    '---------------Function Expression--------------------------------'
);
//TIP: NOT HOISTING! REFERENCE ERROR! BECAUSE ITS ASSIGNED TO VARIABLES (VAR, CONST, LET) THEREFORE ITS NOT A FUNCTION ITS A VARIABLE NOW!
// console.log(sum(10,20));
//TIP: Anonymous functions!
const sum = function (a, b) {
    return a + b;
};
console.log(sum);
console.log(sum()); //NaN

//INFO:In javascript, FUNCTIONS ARE VARIABLES! THEY'RE FIRST CLASS FUNCTIONS!
const myArr = [
    6,
    'Arin',
    function () {
        console.log('Array Element!');
    },
];
myArr[2]();

const myObj = {
    age: 5,
    name: 'Serhat',
    func: function () {
        console.log('Object element!');
    },
};
console.log(myObj.func());

console.log(
    20 +
        function () {
            return 10;
        }
); //TIP: returns 20function(){return 10;} because we didn't invoke the func!
//INFO:IIFE: Immediately invoked function expression
console.log(
    20 +
        (function () {
            return 10;
        })()
); //TIP: returns 30, we invoked the function!

console.log('---------------Function Expression vs Declaration--------------------------------');
//INFO:FUNCTION EXPRESSION VS DECLARATION
/**
 * 1.Function Expression is not HOISTED
 * 2.Function Expression can be anonymous
 * 3.FIRST CLASS FUNCTIONS can be passed as a arguments to another function.
 * 4. FIRST CLASS FUNCTIONS CAN be used as a second function in the return of another function!
 */

//INFO: * 3.FIRST CLASS FUNCTIONS can be passed as a arguments to another function.
const addFive = function (num, func) {
    console.log(num + func());
};

console.log(
    addFive(10, function () {
        return 50;
    })
); //RETURNS 60

console.log('---------------FIRST CLASS FUNCTIONS CAN BE USED AS A RETURN VALUE!--------------------------------');
//INFO:4. FIRST CLASS FUNCTIONS CAN be used as a second function in the return of another function!
//THIS IS CALLED CURRYING.
const myFunc = function (num) {
    return function toDouble() {
        console.log(num * 2);
    };
};
myFunc(8)();//16!
//TIP:Or..


console.log('---------------IIFE IN DECLARATION VS EXPRESSION!--------------------------------');
/**
 * IIFE: Immediately Invoked Function Expression
*/

//Expression:
const calc = function(){
    console.log(12+5);
}() //OUTPUTS 17
console.log(calc);// OUTPUTS Undefined


//Declaration:
//we have to use this paranthesis because when we write function, javascript thinks we're going to use declaration 
//but we don't because its an anonymous function!
(function(){
    console.log(12+5);
})();

//INFO: FUNCTIONS ARE OBJECTS IN JAVASCRIPT!



//INFO:SUMMARY:
/**
 * 1.Function Expression is not HOISTED
 * 2.Function Expression can be anonymous
 * 3.FIRST CLASS FUNCTIONS can be passed as a arguments to another function.
 * 4. FIRST CLASS FUNCTIONS CAN be used as a second function in the return of another function!
 * 5.FUNCTIONS IN JS ARE OBJECTS!
 * 6.WE SHOULD USE AN EXTRA () IN THE DECLARATION WHEN WE USE IIFE
 */