"use strict";


//INFO: CLASSES
// constructor function

//1.
//TIP: What is bad about PROTOTYPES? It creates problems when using REFERENCE TYPES!
function Person(name, surname, age) {
    this.name = name;
    this.surname = surname;
    this.age = age;
}
//TIP: We can use the same function with prototypes because evety fullName has the same reference.
Person.prototype.fullName = function() {
    return this.name + " " + this.surname
}

//Now create a reference value with Prototype:
Person.prototype.friends = ["Ela", "Rüzgar"] //EVERY INSTANCE WILL HAVE THE SAME REFERENCE WHEN THEY REACH friends property..
const arin = new Person("Arin", "Çekiç", 5);
const elis = new Person("Elis", "Çekiç", 3);

arin.friends.push("Çınar");
console.log(arin.friends); //OUTPUTS: [Ela, Ruzgar, Cinar]
console.log(elis.friends);//OUTPUTS: [Elis, Ruzgar, Cinar WHY? INFO: BECAUSE THEY'RE USING THE SAME REFERENCE VALUES!



//INFO: 2. CLASS, they're actually function, not CLASS.
class PersonClass {
    constructor(name, surname, age){
        this.name = name;
        this.surname = surname;
        this.age = age;
        this.friends = ["Jake","Paola"]
    }
    fullName() { //TIP: Class methods are pat of PROTOTYPE!
        return this.name + " " + this.surname
    }
}


/**2.1.
 * INFO:WHAT HAPPENS WHEN NEW KEYWROD EXECUTES ? 
 * 1.A PLACE IN THE MEMORY SAVED FOR THE INSTANCE!
 * 2.This keyword points the instance (ariella or suzanne)
 * 3.Constructor methods executes! 
 */
const ariella = new PersonClass("Ariella","Dolores",23);
const suzanne = new PersonClass("Suzanne","Dolores",21);
ariella.friends.push("Elise");
console.log(ariella.friends);
console.log(suzanne.friends);


//INFO: 3.JAVASCRIPT CLASSES ARE FUNCTIONS!
console.log(typeof PersonClass) //outputs Function!

//INFO: 3.1.CLASS DECLARATION!
class Animal{
    constructor(name,age){
        this.name = name;
        this.age = age;
    }
}

//INFO: 3.2.CLASS EXPRESSION!
const Cat = class extends Animal{
    constructor(name,age,sound){
        super(name,age);
        this.sound = sound;
    }
}


