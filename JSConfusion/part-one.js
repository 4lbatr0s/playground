"use strict";


/**
 * INFO: In this part, we are going to work on CONST-LET-VAR and HOISTING.
 */

//LET
let age = 40;
console.log(age);
age = 42;
console.log(age);

//CONST
const nick = "Arin";
console.log(nick);

// name = "Elis";


//WHERE TO USE LET? 
//INFO: You should use let in block scopes.
for (let index = 0; index < 10; index++) {
    const element = index;
    console.log(element);
}

//TIP: HOW CONST WORKS? 
//INFO: We do not change the reference of the array, we change the member of array.
const myArr = [1,2,3];
console.log(myArr);
myArr.pop();
console.log(myArr);


//TIP: same goes here.
const student = {
    name: 'Aris',
    age:5
};
console.log(student);

student.name = "Elis";
console.log(student);

//INFO: What if we don't want Object to change ? TIP: Object.freeze
Object.freeze(student); //Converts it to read only property.
// student.name = "Serhat";
console.log(student);//No eror no change


//TIP: If we try to change REFERENCE, SEE WHAT HAPPENS

// student = {};//Assignment to constant variable. 



//INFO:VAR Keywords: functional and global  scopes.
//INFO:Works like the ref keyword in the C#.
console.log("var keyword area")
var x =1;
if(x===1){
    var x = 10; //It changes because var is global scope!
    console.log(x);
}
console.log(x);

console.log("let keyword area")
//LET Keyword: block scope! if,else else if etc.
let z = 1;
console.log(z);
if(z === 1)
{
    let z = 10; //does not change because this z iz special to the block scope!
    console.log(z);
}
console.log(z);

//INFO:USE CASES
console.log("Use cases of VAR/LET/CONST:");
var test = 10; //We can reach it with window property.
console.log(window.x);
let testlet = 20;
console.log(window.testlet); //undefined

//INFO:HOISTING:
console.log("----------------------------------------------------------------");
console.log("HOISTING:");
console.log(bet); //undefined
var bet=10;

// console.log(betlet); //INFO: Hoisting does not work for LET! REFERENCE ERROR!
// let betlet = 20;

// console.log(betconst);//INFO: Hoisting does not work for CONST! REFERENCE ERROR!
// const betconst = 30;

console.log("Hoisting for functions:");
//INFO: Hoisting works for functions completely! so no undefined at all!
callHelloWorld();

function callHelloWorld() {
    let helloWorld = "Hello World!";
    console.log(helloWorld);
}