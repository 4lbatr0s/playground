"use strict";

//INFO: In this module, we assess Objects, this, Constructor Functions, new Object(), create.

//1.
const person = {
    name: 'John', //property.
    surname: 'Doe',
    languages:['Turkish', 'English', 'Spanish'],
    fullName: function (){ //function expression, its not a property its a method!
        return this.name + " " + this.surname; //TIP:When a function is called as a method of an object, `this` refers to the object.
    },
    address:{
        city:"Bursa",
        district:"Osmangazi"
    }
}

//2.INFO: Why do we need an object? because we want our variables together, inside a box.


console.log("================================OBJECT LITERAL =================================");
//3.Object literal: we create key value pairs while creating the object!
const secondPerson = {
    name: 'John',
    surname: 'Doe',
    age: 40,
    fullName: function() {
        return this.name + " " + this.surname
    }
}
console.log(secondPerson);

//3.1. Dot Notation:
// dot Notation
console.log(secondPerson.name);
console.log(secondPerson.age);
console.log(secondPerson.fullName());
secondPerson.job = "Student";
console.log(secondPerson);
console.log(secondPerson.job);

//3.2.BRACKET NOTATION:
console.log(secondPerson['name']);
console.log(secondPerson['age']);
console.log(secondPerson['fullName']());//INFO: How to reach to an object method with bracket notation.
console.log(secondPerson['na' + 'me']) // 'na' + 'me' ---> 'name' */ 


//4.CONSTRUCTOR FUNCTIONS: ALTERNATIVE WAY TO CREATE AN OBJECT.
//INFO: We use it to avoid repeating ourselves when we are constantly creating objects belonging to the same properties.

//TIP: How it works:(Just an example)
function Person(name, surname, age)
{
    console.log(this); //outputs empty
    // const obj = {};
    this.name = name;//INFO:When a function is called as a constructor, `this` refers to the new object being constructed.
    this.surname = surname;
    this.age = age;
    this.fullName = function(){
        return this.name + " " + this.surname;
    }

    console.log(this); //outputs our new person object.
}
const person1 = new Person("serhat","oner","25");
const person2 = new Person("oner","serhat","52");
console.log(person1);
console.log(person2);


//INFO:5.CREATE OBJECT WITH Object.create() METHOD!
const person3 = Object.create(person1);
person1.name = "dalalala";
console.log("Person3:",person3);
console.log(person3.name);
console.log(person3.surname);
