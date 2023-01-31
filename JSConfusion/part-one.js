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



//VAR Keywords: functional and global  scopes.

var x =1;
if(x===1){
    var x = 10; //It changes!
    console.log(x);
}
console.log(x);
