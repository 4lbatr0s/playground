"use strict";
//INFO: In this module, we study Objects, PROTOTYPES, INHERITANCE, BASIC OBJECTS, __proto__

/**
 * What is Prototype?
 * Every object in JS is created from another object, from an ancestor.
 * From Parent=>Child, parent is the PROTOTYPE of the Child.
*/

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
//person = OBJECT (prototype) + name, surname, languages, address, fullName()
console.log(person);
console.log(person.name);
console.log(person.fullName());
console.log(person.job);//TIP: Returns UNDEFINED.
console.log(person.toString()); //TIP: We have this method due to Prototype (OBJECT)
console.log(person.hasOwnProperty("name"));


//OBJECT==> myObj
const myObj = {};
console.log(myObj.toString());
console.log(myObj.hasOwnProperty("x"));


function Person(name, surname, age)
{
    this.name = name;//INFO:When a function is called as a constructor, `this` refers to the new object being constructed.
    this.surname = surname;
    this.age = age;
    this.fullName = function(){
        return this.name + " " + this.surname;
    }
}


//TIP:ARIELLA == OBJECT + name, surname, age, fullName ===> Person ===> ariella
const ariella = new Person("Ariella", "Glorias", 32);
console.log(ariella);
console.log(ariella.toString());//COMES FROM OBJECT.
console.log(ariella.hasOwnProperty("age"));//true

//INFO: HOW TO OUTPUT PROTOTYPE
console.log(ariella.__proto__);//TIP: Outputs a constructor that includes properties from Person Object and other values from OBJECT.




//INFO: HOW TO OVERRIDE PROTOTYPE METHODS!
const elis = new Person("Elis", "Çekiç", 4); 
console.log(elis);
elis.job = "Child";
console.log(elis);
console.log(elis.toString()); 

// OBJECT + name, surname, age, fullName() ----> Person + job ----> elis
elis.toString = function() {//expression function
    return "ELISSSSS";
}

console.log(elis.toString());


//-----------------HOW TO USE PROTOTYPE TO CREATE COMMON METHODS FOR OBJECTS---------------------

/**
 * We have the fullName method, when we create different instances of objects, each object involves its own method instance in the memory.
 * How to prevent this? TIP: Use prototype, when you create the common properties, methods with PROTOTYPE, there is only one instance of it no more!
 * Every object instance uses the same method or property.
 * INFO: ITS PERFORMANCE WISE!
 */

function Animal(name, age, sound)
{
    this.name = name;
    this.age = age;
    this.sound = sound;
}

Animal.prototype.makeSound = function(){
    console.log(`${this.sound}`)
}

Animal.prototype.family = "Vulpes";

const arabianWolf = new Animal("Vulpes Arabica", 5, "WOOF");
const timberWolf = new Animal("Vulper Timberis", 3, "LOOUUUWW");

console.log(arabianWolf);
console.log(timberWolf);


//-----------------HOW TO USE PROTOTYPE WITH Object.Create() method!---------------------

const lastPerson = {
    name: 'XXXXXXXX',
    surname: 'XXXXXX',
    age: 0,
    fullName: function() {
        return this.name + " " + this.surname
    }
}

const elagon = Object.create(lastPerson);
console.log(elagon);//Returns an empty object!
console.log(elagon.name);//Returns XXXXXXX, HOW? Because Object.create adds name,surname,age,fullName properties to the Prototype.
elagon.name = "elagon";
console.log(elagon); //returns {name: "elagon"}, still got name,surname,age,fullName properties in the prototype...
console.log(elagon.name);//Returns "elagon", 
console.log(elagon.__proto__.name);//Returns "XXXXXX"
elagon.age = 10;
console.log(elagon.hasOwnProperty("age"));//returns true because its in the object!
console.log(elagon.hasOwnProperty("surname"));//returns false because its not in the object, its in the prototype
console.log("surname" in elagon);//returns true because its in the prototype    
