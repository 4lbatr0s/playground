//What are data types in JavaScript ? 

//primitive types
typeof "John Doe" // Returns "string"
typeof 3.14 // Returns "number"
typeof true // Returns "boolean"
typeof 234567890123456789012345678901234567890n // Returns bigint
typeof undefined // Returns "undefined"
typeof null // Returns "object" (kind of a bug in JavaScript)
typeof Symbol('symbol') // Returns Symbol
//objects 
typeof {x:15}
typeof [1,2,3,45]

typeof function abc(){};

//----------------------------------------------------------------

//HOISTING
//fonksiyon ve variabllerin scopein en tepesine js tarafindan yerlestirilmesi durumudur. VAR keywordde ve fonksiyonlarda calisir.

//HOISTING DOESN'T WORK FOR FUNCTION EXPRESSION, ONLY OR FUNCTION DECLARATION.

/*
console.log(arHoisting());

var arHoisting = function() {
   return "hello"; 
}
*/

//use strict to avoid hoisting.

//----------------------------------------------------------------

//Implicit Type Coercion in JavaScript:
//+ haric butun islemlerde string, number'a donusuyor. + da ise Number stringe donusuyor.
//----------------------------------------------------------------

//FALSIFY VE TRUTHY VALUES
//0,-0,0n,NaN, null, undefined haric butun degerler truthy.

//----------------------------------------------------------------
//==vs===
//==: compares only values, ===:compares type and value.
//Example
var a = 12;
var b= '12';

console.log(a==b); //true
console.log(a===b); //false

//----------------------------------------------------------------

//JavaScript is a dynamically typed language, variables don't have value types.Values have. Variables dynamically can change type.

//----------------------------------------------------------------
//FUNCTION EXPRESSION IS DECLARING FUNCTIONS WITH VARIABLES,
//FUNCTION DECLARATION IS DECLARING FUNCTION WITH FUNCTION KEYWORD.

//----------------------------------------------------------------
//IIFE - Immediately Invoked Function Expression

//expression
const iife1 = function(){
    console.log("iife1")
}();

//declaration
(
    function(){
        console.log("iife2")
    }
)();

//----------------------------------------------------------------
//STRICT MODE
/*
 -Makes debugging better
 -Enables to throw errors better
 -Cannot declare global variables in the strict mode.
*/

//----------------------------------------------------------------
//High order functions are functions that take other functions as parameters or return other functions as parameters

//----------------------------------------------------------------
//BIND, CALL AND APPEND
const obj1 = {
 name:'serhat',
 getName(){
  console.log(this.name);
 }
}


const obj2 = {name:'oner'}

obj1.getName.call(obj2);//this indicates the parameter object

//append same with call, only difference is append takes an array as parameter.

//bind: binds two objects and makes parameter object, owner of this.

const obj3 = {
 getSalutation(word, count){
  console.log(`I salute you with the word ${word}, ${count} times ${this.name}`);
 }
}


const obj4 = {
 name:"serhat"
}


const bindedFunc = obj3.getSalutation.bind(obj4, "slav", 4);
bindedFunc();



//----------------------------------------------------------------
//EXEC VS TEST: they're regex functions

const myString = "Hello World";
const myRegex = new RegExp("World");
const result  = myRegex.test(myString); //returns true or false
console.log("regex-Test:", result);

const execResult = myRegex.exec(myString);
console.log("execResult:", execResult);


//----------------------------------------------------------------
//CURRYING: divide a function that takes multiple arguments into multiple functions.
